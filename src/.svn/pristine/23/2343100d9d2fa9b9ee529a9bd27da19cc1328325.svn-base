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
        const string _sqlObject = "select ID, 被测对象ID, {0} from [CA被测对象实测表] where 测试版本 = ? and 被测对象ID = ? ";

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
            if(dtObject == null) return true; // 尚未有选中的对象
            DataRow dr = dtObject.Rows[0];
            if(rich1.Changed) dr[fieldName] = rich1.GetRichData();
            return dbProject.UpdateDatabase(dtObject, sqlObject);
        }

        /// <summary>
        /// 设置缺省值
        /// </summary>
        void SetDefaultValue(RichTextBoxOle ole1)
        {
            if(fieldName != "质量评估") return;
            byte[] buf = ole1.GetRichData();
            if(!IOleObjectAssist.IsOleBufferEmpty(buf)) return;

            string s = @"通过对{0}软件(软件代码版本: {1})的{2}等方面的测试，测试小组认为：被测对象符合《{0}需求规格说明({3})》中的要求。";
            string projectName = ProjectInfo.GetProjectString(dbProject, pid, "项目名称");
            string softwareVer = ProjectInfo.GetDocString(dbProject, pid, currentvid, null, "被测软件版本");
            string requireVer = ProjectInfo.GetDocString(dbProject, pid, currentvid, null, "需求版本");

            KeyList kl = new KeyList("、");
            object vid = currentvid;
            for(int i = 0; i < 100; i++)
            {   // 防止死循环
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
            if(summary[oid] == null) return;  // 临时，防报错。
            foreach(var iClass in summary[oid].childlist)
            {
                string name = iClass.name;
                if(name.EndsWith("测试"))
                    name = name.Substring(0, name.Length - 2);
                kl.AddKey(name);
            }
        }
    }
}
