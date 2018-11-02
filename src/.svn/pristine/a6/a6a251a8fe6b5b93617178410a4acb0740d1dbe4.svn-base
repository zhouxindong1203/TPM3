using System.Data;
using Common;
using Common.TrueDBGrid;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// 测试类型模板列表
    /// </summary>
    [TypeNameMap("wx.TestClassTemplateList")]
    public partial class TestClassTemplateList : MyBaseForm
    {
        public static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestClassTemplateList>(1);
        public static ColumnPropList columnList2 = GridAssist.GetColumnPropList<TestClassTemplateList>(3);
        static readonly string sql1 = "select * from DC测试类型模板表 where 父节点ID = ?";

        TrueDBGridAssist gridAssist1;
        DataTable dtTable;
        const string root = "0";

        public TestClassTemplateList()
        {
            InitializeComponent();
            gridAssist1 = new TrueDBGridAssist(grid1, "测试能力ID", "序号");
            gridAssist1.columnList = columnList1;
        }

        static TestClassTemplateList()
        {
            columnList1.Add("序号", 50);
            columnList1.Add("测试能力名称", 120, "测试类型名称");
            columnList1.Add("简写码", 70);
            columnList1.Add("简要说明", 230);

            columnList1.AddValidator(new NotNullValidator("简写码"));
            columnList1.AddValidator(new NotNullValidator("测试能力名称"));
            columnList1.RowHeight = 30;

            //columnList2.Add("序号", 50);
            columnList2.Add("测试能力名称", 120, "测试类型名称");
            columnList2.Add("简写码", 70);
            columnList2.Add("简要说明", 300);
        }

        public override bool OnPageCreate()
        {
            grid1.Caption = "定制测试类型，独立于项目。在此做的修改将影响到所有的项目";
            dtTable = dbProject.ExecuteDataTable(sql1, root);
            if(dtTable == null) return false;
            dtTable.Columns["父节点ID"].DefaultValue = root;

            gridAssist1.DataSource = dtTable;
            gridAssist1.OnPageCreate();
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            gridAssist1.OnPageClose();
            if(!dbProject.UpdateDatabase(dtTable, sql1)) return false;
            return true;
        }

        private void grid1_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
        {
            //if(e.Column.Name.Equals("测试类型名称"))
            //{
            //    object v = this.grid1[this.grid1.Row, "测试类型名称"];
            //    if(DBNull.Value.Equals(v))
            //        return;

            //    if(ExistTestTypeWithName((string)v))
            //    {
            //        MessageBox.Show("已存在此名称的测试类型!", "测试类型重名", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        this.grid1[this.grid1.Row, "测试类型名称"] = string.Empty;
            //    }
            //}
        }

        //private bool ExistTestTypeWithName(string name)
        //{
        //    string sql = "SELECT * FROM DC测试类型模板表 WHERE 测试能力名称 = ?";
        //    DataRow row = dbProject.ExecuteDataRow(sql, name);
        //    return row != null;
        //}
    }
}