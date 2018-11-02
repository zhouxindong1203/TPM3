using System;
using System.Data;
using System.Windows.Forms;
using TPM3.Sys;
using Common;
using C1.Win.C1FlexGrid;

namespace TPM3.wx
{
    /// <summary>
    /// 切换测试版本
    /// </summary>
    [TypeNameMap("wx.ProjectVersionForm")]
    public partial class ProjectVersionForm : MyBaseForm
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<ProjectVersionForm>(1);
        FlexGridAssist flexAssist1;
        DataTable dtVersionList;

        public ProjectVersionForm()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithFocus(flex1);
            flexAssist1 = new FlexGridAssist(flex1, "ID", "序号");
            flexAssist1.columnList = columnList1;
            flex1.AllowDragging = AllowDraggingEnum.Columns;
            flex1.SelectionMode = SelectionModeEnum.Row;
            flexAssist1.AddHyperColumn("删除");
            flexAssist1.RowNavigate += OnRowNavigate;
        }

        static ProjectVersionForm()
        {
            columnList1.Add("删除", 50);
            columnList1.Add("序号", 50);
            columnList1.Add("版本名称", 100);
            columnList1.Add("版本说明", 250);
            columnList1.Add("版本创建时间", 100);
            columnList1.Add("前向版本名称", 100);
        }

        /// <summary>
        /// 删除选中的版本
        /// </summary>
        void OnRowNavigate(int row, int col, Row r)
        {
            if(IsNull(r["前向版本ID"]))
            {
                MessageBox.Show("该版本是初始版本，不能被删除!!!");
                return;
            }

            foreach(DataRow dr in dtVersionList.Rows)
            {
                if(Equals(r["ID"], dr["前向版本ID"]))
                {
                    MessageBox.Show("该版本还是其它回归版本的前向版本，不能被删除!!!");
                    return;
                }
            }
            DialogResult ret = MessageBox.Show("确认要删除选中的回归版本吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if(ret != DialogResult.Yes) return;
            DBLayer1.DeleteVersion(dbProject, pid, r["ID"]);

            MainForm.mainFrm.VersionChanged = true;
            // 如果删除的是当前打开的版本，则自动切换到前向版本
            if(Equals(currentvid, r["ID"]))
            {
                globalData.currentvid = r["前向版本ID"];
                MainForm.mainFrm.InitFormByVersion();
            }
            // 重新绑定
            OnPageClose(false);
            OnPageCreate();
        }

        public override bool OnPageCreate()
        {
            dtVersionList = DBLayer1.GetProjectVersionList(dbProject, pid, true);
            GridAssist.AddColumn(dtVersionList, "删除");
            foreach(DataRow dr in dtVersionList.Rows)
                dr["删除"] = "删除";

            flexAssist1.DataSource = dtVersionList;
            flexAssist1.OnPageCreate();

            foreach(Row r in flex1.Rows)
            {
                if(r.DataSource == null) continue;
                if(Equals(r["ID"], currentvid))
                {
                    flex1.Row = r.Index;
                    break;
                }
            }

            FlexGridAssist.AutoSizeRows(flex1, 6);
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            flexAssist1.OnPageClose();
            return base.OnPageClose(bClose);
        }

        void ProjectVersionForm_Load(object sender, EventArgs e)
        {
            if(!OnPageCreate())
                this.Close();
        }

        void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        void btOK_Click(object sender, EventArgs e)
        {
            SwitchToSelectVersion();
        }

        void flex1_DoubleClick(object sender, EventArgs e)
        {
            SwitchToSelectVersion();
        }

        /// <summary>
        /// 切换到选中版本
        /// </summary>
        void SwitchToSelectVersion()
        {
            int row = flex1.Row;
            if(row < flex1.Rows.Fixed) return;

            Row r = flex1.Rows[row];
            object vid = r["ID"];  // 选中的版本
            if(Equals(vid, currentvid))
            {
                MessageBox.Show("选中的版本与当前打开的版本相同", "提示");
                return;
            }
            globalData.currentvid = vid;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}