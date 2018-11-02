using System.Data;
using Common;
using Common.RichTextBox;

namespace TPM3.wx
{
    public partial class TestObjectInfoControl : MyUserControl
    {
        public TestObjectInfoControl()
        {
            InitializeComponent();
        }

        public string fieldName, title;

        DataTable dtObject;
        const string _sqlObject = "select ID, �������ID, {0} from [CA�������ʵ���] where ���԰汾 = ? and �������ID = ? ";

        public string sqlObject
        {
            get { return string.Format(_sqlObject, fieldName); }
        }

        public override bool OnPageCreate()
        {
            label5.Text = title;
            dtObject = dbProject.ExecuteDataTable(sqlObject, currentvid, id);
            if(dtObject == null || dtObject.Rows.Count == 0) return false;
            DataRow dr = dtObject.Rows[0];

            rich1.SetRichData(dr[fieldName] as byte[]);
            SetDefaultValue(rich1);
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            if(dtObject == null) return true; // ��δ��ѡ�еĶ���
            DataRow dr = dtObject.Rows[0];
            if(rich1.Changed) dr[fieldName] = rich1.GetRichData();
            return dbProject.UpdateDatabase(dtObject, sqlObject);
        }

        /// <summary>
        /// ����ȱʡֵ
        /// </summary>
        void SetDefaultValue(RichTextBoxOle ole1)
        {
            if(fieldName != "��������") return;
            byte[] buf = ole1.GetRichData();
            if(!IOleObjectAssist.IsOleBufferEmpty(buf)) return;

            string s = @"ͨ����{0}���(�������汾: {1})��{2}�ȷ���Ĳ��ԣ�����С����Ϊ�����������ϡ�{0}������˵��({3})���е�Ҫ��";
            string projectName = ProjectInfo.GetProjectString(dbProject, pid, "��Ŀ����");
            string softwareVer = ProjectInfo.GetDocString(dbProject, pid, currentvid, null, "��������汾");
            string requireVer = ProjectInfo.GetDocString(dbProject, pid, currentvid, null, "����汾");

            KeyList kl = new KeyList("��");
            object vid = currentvid;
            for(int i = 0; i < 100; i++)
            {   // ��ֹ��ѭ��
                GetAllTestClass(kl, id, vid);
                vid = DBLayer1.GetPreVersion(dbProject, vid);
                if(vid == null) break;
            }

            string s2 = string.Format(s, projectName, softwareVer, kl, requireVer);
            var buf2 = IOleObjectAssist.GetByteFromString(s2);
            rich1.SetRichData(buf2);
            rich1.SetBufferChanged();
        }

        static void GetAllTestClass(KeyList kl, object oid, object vid)
        {
            var summary = new TestResultSummary(pid, vid) { dblevel = 2 };
            summary.OnCreate();
            if(summary[oid] == null) return;  // ��ʱ��������
            foreach(var iClass in summary[oid].childlist)
            {
                string name = iClass.name;
                if(name.EndsWith("����"))
                    name = name.Substring(0, name.Length - 2);
                kl.AddKey(name);
            }
        }
    }
}
