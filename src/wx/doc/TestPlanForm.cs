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
            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "序号");
            gridAssist1.columnList = columnList1;

            RefrenceColumnMapBase mapList = gridAssist1.refrenceColumnMapList;
            ColumnRefMap cm = mapList.AddColumnMap("主要完成人", GetPersonTable, "ID", "姓名");
            cm.multiSelect = true;
            cm.seperator = ",";
            cm.columnList = PersonForm.columnList2;

            //--- 复制/粘贴支持
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
            columnList1.Add("序号", 60);
            columnList1.Add("工作内容说明", 150);
            columnList1.Add("预计开始时间", 100);
            columnList1.Add("预计完成时间", 100);
            columnList1.Add("主要完成人", 90);
            columnList1.Add("备注", 180);

            columnList1.AddValidator(new NotNullValidator("工作内容说明"));
        }

        public override bool OnPageCreate()
        {
            rich1.SetRichData(ProjectInfo.GetDocContent(dbProject, pid, currentvid, docName, "测试进度计划"));

            dt1 = DBLayer1.GetTestPlanList(dbProject, pid, currentvid);
            if( dt1 == null ) return false;
            gridAssist1.DataSource = dt1;
            gridAssist1.OnPageCreate();

            // ture: WORD文档，false: 列表
            rb12.Checked = MyProjectInfo.GetBoolValue(dbProject, pid, currentvid, "测试进度");
            rb11.Checked = !rb12.Checked;
            rb11_CheckedChanged(null, null);

            grid1.Columns["备注"].Caption = DBLayer1.GetProjectType(dbProject, pid) == ProjectStageType.定型 ? "工作产品" : "备注";
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            gridAssist1.OnPageClose();
            MyProjectInfo.SetBoolValue(dbProject, pid, currentvid, "测试进度", rb12.Checked);
            ProjectInfo.SetDocContent(dbProject, pid, currentvid, docName, "测试进度计划", rich1.GetRichData());
            if( !DBLayer1.UpdateTestPlanList(dbProject, dt1) ) return false;
            return true;
        }

        /// <summary>
        /// ???改为自动将两个时间设置为一致
        /// </summary>
        private void grid1_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
        {
            if( e.Cancel ) return;
            DataRow dr = (grid1[grid1.Row] as DataRowView).Row;
            DateTime? dt11 = GetDateTime(grid1.Columns["预计开始时间"].Value);
            DateTime? dt12 = GetDateTime(grid1.Columns["预计完成时间"].Value);
            if( dt11 == null || dt12 == null ) return;
            if( dt12.Value.Date < dt11.Value.Date )
            {
                MessageBox.Show("预计完成时间不能早于预计开始时间!!!");
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