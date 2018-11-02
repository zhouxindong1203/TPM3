using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Common;
using Common.Database;
using TPM3.Sys;

namespace TPM3.wx
{
    public partial class AutoAddAbbrvListForm : Form
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<AutoAddAbbrvListForm>(1);
        FlexGridAssist flexAssist1;

        public DataTable dt;
        public object pid;

        public AutoAddAbbrvListForm()
        {
            InitializeComponent();

            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            flexAssist1 = new FlexGridAssist(flex1, null, null) { columnList = columnList1, doubleClickEdit = false };
        }

        static AutoAddAbbrvListForm()
        {
            columnList1.Add("术语和缩略语名", 180, "术语和缩略语");
            columnList1.Add("确切定义", 450, false, "说明");
        }

        public static DataTable GetAbbrevList(DBAccess dba, object pid)
        {
            const string sql = "select distinct 术语和缩略语名, 确切定义 from DC术语表 where 项目ID = ?";
            return dba.ExecuteDataTable(sql, pid);
        }

        void AutoAddAbbrvListForm_Load(object sender, EventArgs e)
        {
            DataTable dt2 = GetAbbrevList(GlobalData.globalData.dbProject, pid);
            flexAssist1.DataSource = dt2;
            flexAssist1.OnPageCreate();
            for(int i = flex1.Rows.Fixed; i < flex1.Rows.Count; i++)
                flex1.SetCellCheck(i, flex1.Cols["术语和缩略语名"].Index, CheckEnum.Unchecked);
        }

        void btOK_Click(object sender, EventArgs e)
        {
            int startIndex = 10000;

            for(int i = flex1.Rows.Fixed; i < flex1.Rows.Count; i++)
            {
                if(flex1.GetCellCheck(i, flex1.Cols["术语和缩略语名"].Index) != CheckEnum.Checked) continue;
                Row r = flex1.Rows[i];
                DataRow dr = dt.NewRow();
                dr["ID"] = FunctionClass.NewGuid;
                dr["序号"] = startIndex++;
                dr["术语和缩略语名"] = r["术语和缩略语名"];
                dr["确切定义"] = r["确切定义"];
                dt.Rows.Add(dr);
            }
            GridAssist.SetDataTableIndex(dt, "序号");
            this.Close();
        }

        void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
