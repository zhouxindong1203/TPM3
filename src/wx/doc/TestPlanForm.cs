using System;
using System.Data;
using System.Windows.Forms;
using Common;
using Common.TrueDBGrid;
using TPM3.Sys;
using C1.Win.C1TrueDBGrid;

namespace TPM3.wx
{
    [TypeNameMap("wx.TestPlanForm")]
    public partial class TestPlanForm : MyBaseForm
    {
        DataTable dt1;
        TrueDBGridAssist gridAssist1;
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestPlanForm>(1);

        public TestPlanForm()
        {
            InitializeComponent();
            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "���");
            gridAssist1.columnList = columnList1;

            RefrenceColumnMapBase mapList = gridAssist1.refrenceColumnMapList;
            ColumnRefMap cm = mapList.AddColumnMap("��Ҫ�����", GetPersonTable, "ID", "����");
            cm.multiSelect = true;
            cm.seperator = ",";
            cm.columnList = PersonForm.columnList2;

            //--- ����/ճ��֧��
            gridAssist1.gridClipboard.CopyEvent += grid_CopyEvent;
            gridAssist1.gridClipboard.PasteEvent += grid_PasteEvent;
            grid1.Dock = DockStyle.Fill;
        }

        void grid_CopyEvent(object sender, EventArgs e)
        {
        }

        void grid_PasteEvent(object sender, EventArgs e)
        {
        }

        static TestPlanForm()
        {
            columnList1.Add("���", 60);
            columnList1.Add("��������˵��", 150);
            columnList1.Add("Ԥ�ƿ�ʼʱ��", 100);
            columnList1.Add("Ԥ�����ʱ��", 100);
            columnList1.Add("��Ҫ�����", 90);
            columnList1.Add("��ע", 180);

            columnList1.AddValidator(new NotNullValidator("��������˵��"));
        }

        public override bool OnPageCreate()
        {
            rich1.SetRichData(ProjectInfo.GetDocContent(dbProject, pid, currentvid, docName, "���Խ��ȼƻ�"));

            dt1 = DBLayer1.GetTestPlanList(dbProject, pid, currentvid);
            if( dt1 == null ) return false;
            gridAssist1.DataSource = dt1;
            gridAssist1.OnPageCreate();

            // ture: WORD�ĵ���false: �б�
            rb12.Checked = MyProjectInfo.GetBoolValue(dbProject, pid, currentvid, "���Խ���");
            rb11.Checked = !rb12.Checked;
            rb11_CheckedChanged(null, null);

            grid1.Columns["��ע"].Caption = DBLayer1.GetProjectType(dbProject, pid) == ProjectStageType.���� ? "������Ʒ" : "��ע";
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            gridAssist1.OnPageClose();
            MyProjectInfo.SetBoolValue(dbProject, pid, currentvid, "���Խ���", rb12.Checked);
            ProjectInfo.SetDocContent(dbProject, pid, currentvid, docName, "���Խ��ȼƻ�", rich1.GetRichData());
            if( !DBLayer1.UpdateTestPlanList(dbProject, dt1) ) return false;
            return true;
        }

        /// <summary>
        /// ???��Ϊ�Զ�������ʱ������Ϊһ��
        /// </summary>
        private void grid1_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
        {
            if( e.Cancel ) return;
            DataRow dr = (grid1[grid1.Row] as DataRowView).Row;
            DateTime? dt11 = GetDateTime(grid1.Columns["Ԥ�ƿ�ʼʱ��"].Value);
            DateTime? dt12 = GetDateTime(grid1.Columns["Ԥ�����ʱ��"].Value);
            if( dt11 == null || dt12 == null ) return;
            if( dt12.Value.Date < dt11.Value.Date )
            {
                MessageBox.Show("Ԥ�����ʱ�䲻������Ԥ�ƿ�ʼʱ��!!!");
                e.Cancel = true;
            }
        }

        static DateTime? GetDateTime(object obj)
        {
            DateTime dt;
            bool ret = DateTime.TryParse(obj.ToString(), out dt);
            if( !ret ) return null;
            return dt;
        }

        void rb11_CheckedChanged(object sender, EventArgs e)
        {
            grid1.Visible = rb11.Checked;
            rich1.Visible = rb12.Checked;
        }
    }
}