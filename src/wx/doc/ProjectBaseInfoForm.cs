using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Common;
using TPM3.Sys;

namespace TPM3.wx
{
    [TypeNameMap("wx.ProjectBaseInfoForm")]
    public partial class ProjectBaseInfoForm : MyBaseForm
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<ProjectBaseInfoForm>(7);
        FlexGridAssist flexAssist1;

        public ProjectBaseInfoForm()
        {
            InitializeComponent();
            bl1.Add(new BaseInfo("��ʶ�汾ǰ׺", tb206));
            bl1.Add(new BaseInfo("���Ե�λ", tb204));
            bl1.Add(new BaseInfo("��Ŀ����", tb201));
            bl1.Add(new BaseInfo("��Ŀ��ʶ��", tb203));

            bl2.Add(new BaseInfo("��������汾", tb31));
            bl2.Add(new BaseInfo("����汾", tb32));
            bl2.Add(new BaseInfo("�汾����", tb41));
            bl2.Add(new BaseInfo("�汾˵��", tb42));

            bl1.Add(new BaseInfo("ί�з�����", tb101));
            bl1.Add(new BaseInfo("ί�з���ַ", tb102));
            bl1.Add(new BaseInfo("���췽��ϵ��", tb103));
            bl1.Add(new BaseInfo("���췽��ϵ�绰", tb104));
            bl1.Add(new BaseInfo("������λ", tb105));
            bl1.Add(new BaseInfo("��������ַ", tb106));
            bl1.Add(new BaseInfo("��������ϵ��", tb107));
            bl1.Add(new BaseInfo("��������ϵ�绰", tb108));
            bl1.Add(new BaseInfo("ʵ���ҵ�ַ", tb109));
            bl1.Add(new BaseInfo("�ⳡ���Ե�ַ", tb110));

            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            flexAssist1 = new FlexGridAssist(flex1, null, null) { columnList = columnList1, doubleClickEdit = false };
        }

        static ProjectBaseInfoForm()
        {
            columnList1.Add("�ĵ�����", 130, false);
            columnList1.Add("�汾��", 60);
            columnList1.Add("�ĵ��ļ���", 300, "�ĵ��ļ���(��汾�޹�)");
            columnList1.Add("�ĵ���ʶ��", 180, "�ĵ���ʶ��(��汾�޹�)");
            columnList1.Add("����", 80);
            columnList1.Add("ҳü", 80);
        }

        /// <summary>
        /// �汾�޹ؿؼ��б�
        /// </summary>
        List<BaseInfo> bl1 = new List<BaseInfo>(32);

        /// <summary>
        /// �汾��ؿؼ��б�
        /// </summary>
        List<BaseInfo> bl2 = new List<BaseInfo>();

        /// <summary>
        /// ��ǰ�汾�Ļع���ţ���ʼΪ0���Ժ������
        /// </summary>
        int currentVerIndex;

        DataTable dt;
        public override bool OnPageCreate()
        {
            currentVerIndex = DBLayer1.GetVersionIndex(dbProject, currentvid);

            ArrayList al2 = dbProject.GetObjectList("select �ܼ� from DC�ܼ��� order by ID");
            foreach(string s in al2)
                cb202.Items.Add(s);
            cb202.Text = ProjectInfo.GetProjectString(dbProject, pid, "�ܼ�");

            foreach(BaseInfo bi in bl1)   // ��汾�޹�
                bi.control.Text = ProjectInfo.GetDocString(dbProject, pid, null, bi.DocumentName, bi.ContentTitle);

            foreach(BaseInfo bi in bl2)
                bi.control.Text = ProjectInfo.GetDocString(dbProject, pid, currentvid, bi.DocumentName, bi.ContentTitle);

            dt = new DataTable();
            GridAssist.AddMemoColumn(dt, "�ĵ�����", "���ݿ�����", "�汾��", "�ĵ��ļ���", "�ĵ���ʶ��", "����", "ҳü");
            if(currentVerIndex == 0)
            {   // �ǻع����
                ProjectStageType pst = DBLayer1.GetProjectType(dbProject, pid);
                if(pst == ProjectStageType.I��)
                {
                    dt.Rows.Add("����������˵��", "�������");
                    dt.Rows.Add("���Լƻ�", "���Լƻ�");
                    dt.Rows.Add("����˵��", "����˵��");
                }
                if(pst == ProjectStageType.II��)
                {
                    dt.Rows.Add("���Լƻ�", "���Լƻ�");
                    dt.Rows.Add("����˵��", "����˵��");
                }
                if(pst == ProjectStageType.III��)
                {
                    dt.Rows.Add("���Է���", "����˵��");
                }
                if(pst == ProjectStageType.����)
                {
                    dt.Rows.Add("���Ͳ������", "���Լƻ�");
                    dt.Rows.Add("����˵��", "����˵��");
                }
                dt.Rows.Add("���Լ�¼", "���Լ�¼");
                dt.Rows.Add("���Ա���", "�����ܽ�");
                dt.Rows.Add("���ⱨ��", "���ⱨ��");
            }
            else
            {
                dt.Rows.Add("�ع���Է���", "�ع���Է���");
                //dt.Rows.Add("�ع�����ĵ�", "�ع�����ĵ�");
                //dt.Rows.Add("�ع����˵��", "�ع����˵��");
                dt.Rows.Add("�ع����ⱨ��", "�ع����ⱨ��");
                dt.Rows.Add("�ع���Լ�¼", "�ع���Լ�¼");
                dt.Rows.Add("�ع���Ա���", "�ع���Ա���");
            }
            foreach(DataRow dr in dt.Rows)
            {
                string dbName = dr["���ݿ�����"].ToString();
                dr["�汾��"] = ProjectInfo.GetDocString(dbProject, pid, currentvid, dbName, "�ĵ��汾");
                dr["����"] = ProjectInfo.GetDocString(dbProject, pid, currentvid, dbName, "����");
                dr["�ĵ���ʶ��"] = ProjectInfo.GetDocString(dbProject, pid, null, dbName, "�ĵ���ʶ��");   // �汾�޹�
                dr["�ĵ��ļ���"] = ProjectInfo.GetDocString(dbProject, pid, null, dbName, "�ĵ��ļ���");   // �汾�޹�
                if(GridAssist.IsNull(dr["�ĵ��ļ���"])) dr["�ĵ��ļ���"] = "{2}" + dr["�ĵ�����"];  // ȱʡֵ
                dr["ҳü"] = ProjectInfo.GetDocString(dbProject, pid, currentvid, dbName, "�ĵ�ҳü");
            }
            flexAssist1.DataSource = dt;
            flexAssist1.OnPageCreate();
            FlexGridAssist.AutoSizeRows(flex1, 4);
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            ProjectInfo.SetProjectString(dbProject, pid, "�ܼ�", cb202.Text);

            foreach(BaseInfo bi in bl1)
                ProjectInfo.SetDocString(dbProject, pid, null, bi.DocumentName, bi.ContentTitle, bi.control.Text);

            foreach(BaseInfo bi in bl2)
                ProjectInfo.SetDocString(dbProject, pid, currentvid, bi.DocumentName, bi.ContentTitle, bi.control.Text);

            flexAssist1.OnPageClose();
            foreach(DataRow dr in dt.Rows)
            {
                string dbName = dr["���ݿ�����"].ToString();
                ProjectInfo.SetDocString(dbProject, pid, currentvid, dbName, "�ĵ��汾", dr["�汾��"].ToString());
                ProjectInfo.SetDocString(dbProject, pid, currentvid, dbName, "����", dr["����"].ToString());
                ProjectInfo.SetDocString(dbProject, pid, null, dbName, "�ĵ���ʶ��", dr["�ĵ���ʶ��"].ToString());   // �汾�޹�
                ProjectInfo.SetDocString(dbProject, pid, null, dbName, "�ĵ��ļ���", dr["�ĵ��ļ���"].ToString());   // �汾�޹�
                ProjectInfo.SetDocString(dbProject, pid, currentvid, dbName, "�ĵ�ҳü", dr["ҳü"].ToString());
            }
            return true;
        }

        private void comboBox_Validating(object sender, CancelEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if(cb.Text == "") return;
            int index = cb.FindStringExact(cb.Text);
            if(index < 0) //return -1 if nothing was find
            {
                string sql;
                sql = "insert into �ܼ��� (�ܼ�) values (?)";
                dbProject.ExecuteNoQuery(sql, cb.Text);
                cb.Items.Add(cb.Text);
            }
        }

        void VersionSignChanged(object sender, System.EventArgs e)
        {
            string sign = tb203.Text;  // ��Ŀ��ʶ��
            string s2 = tb206.Text;  // �汾ǰ׺
            if(currentVerIndex > 0)
                sign += "/" + s2 + currentVerIndex;
            tb205.Text = sign;
            flex1.Invalidate();
        }

        void flex1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if(e.Row < flex1.Rows.Fixed || e.Col < flex1.Cols.Fixed) return;
            DataRow dr = ((DataRowView)flex1.Rows[e.Row].DataSource).Row;

            string name = flex1.Cols[e.Col].Name;
            if(name == "�ĵ���ʶ��" || name == "�ĵ��ļ���" || name == "ҳü")
            {
                try
                {
                    e.Text = string.Format(dr[name].ToString(), tb205.Text, dr["�汾��"], tb201.Text);
                }
                catch { }
            }
        }
    }
}