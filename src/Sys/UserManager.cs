using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Common;
using Common.Database;
using TPM3.wx;

namespace TPM3.Sys
{
    /// <summary>
    /// 用户管理窗口
    /// </summary>
    public partial class UserManager : Form
    {
        FlexGridAssist flexAssist;
        public static ColumnPropList columnList1 = FlexGridAssist.GetColumnPropList<UserManager>(3);
        DataTable dtTable;
        static string sqlTable = "select ID, 序号, 用户名, 禁止登录, 用户类型, 备注, '' as 删除, '' as 修改密码 from [SYS用户表] where 用户类型 = ? order by 序号";

        public UserManager()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithFocus(flex1);
            flex1.AllowDragging = AllowDraggingEnum.Both;
            flex1.Styles.Normal.WordWrap = true;

            flexAssist = new FlexGridAssist(flex1, "ID", "序号");
            flexAssist.doubleClickEdit = true;
            flexAssist.deleteClear = true;
            flexAssist.columnList = columnList1;
            flexAssist.AddHyperColumn("删除");
            flexAssist.AddHyperColumn("修改密码");
            flexAssist.RowNavigate += OnRowNavigate;
        }

        public static DBAccess dbProject
        {
            get { return GlobalData.globalData.dbProject; }
        }

        static UserManager()
        {
            columnList1.Add("删除", 60);
            columnList1.Add("修改密码", 60);
            columnList1.Add("序号", 40);
            columnList1.Add("用户名", 80);
            columnList1.Add("禁止登录", 90, "是否禁止登录");
            columnList1.Add("备注", 250);
        }

        void OnRowNavigate(int row, int col, Row r)
        {
            string colName = flex1.Cols[col].Name;
            DataRowView drv = r.DataSource as DataRowView;
            if(colName == "删除")
            {
                DialogResult ret = MessageBox.Show("确认要删除该用户吗？", "确认", MessageBoxButtons.OKCancel);
                if(ret != DialogResult.OK) return;
                drv.Row.Delete();
                GridAssist.SetDataTableIndex(dtTable, null, "序号");
            }
            if(colName == "修改密码")
            {
                PasswordForm f = new PasswordForm(drv["ID"], false);
                f.ShowDialog();
            }
        }

        void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Save()
        {
            flexAssist.OnPageClose();

            var rc = flex1.Rows;
            for(int i = rc.Fixed; i < rc.Count; i++)
            {
                var r = rc[i];
                if(GridAssist.IsNull(r["用户名"]))
                {
                    flex1.StartEditing(i, flex1.Cols["用户名"].Index);
                    throw new Exception("用户名不能为空");
                }
            }

            dbProject.UpdateDatabase(dtTable, sqlTable);
        }

        void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        void UserManager_Load(object sender, EventArgs e)
        {
            dtTable = dbProject.ExecuteDataTable(sqlTable, UserType.PM);
            if(dtTable == null)
            {
                MessageBox.Show("打开人员表失败,请检查数据库!!!");
                return;
            }
            dtTable.Columns["用户类型"].DefaultValue = (int)UserType.PM;
            dtTable.Columns["禁止登录"].DefaultValue = false;

            flexAssist.DataSource = dtTable;
            flexAssist.OnPageCreate();

            FlexGridAssist.AutoSizeRows(flex1, 4);
        }

        void flex1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            DataRowView drv = flex1.Rows[e.Row].DataSource as DataRowView;
            if(drv == null) return;

            string colName = flex1.Cols[e.Col].Name;
            if(colName == "删除" || colName == "修改密码")
                e.Text = colName;
        }

        void btCreateNew_Click(object sender, EventArgs e)
        {
            DataRow dr = dtTable.NewRow();
            dr["ID"] = FunctionClass.NewGuid;
            dr["序号"] = 999999;
            dtTable.Rows.Add(dr);
            GridAssist.SetDataTableIndex(dtTable, null, "序号");
            flex1.StartEditing(flex1.Rows.Count - 1, flex1.Cols["用户名"].Index);
        }
    }
}
