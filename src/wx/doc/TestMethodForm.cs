using System.Data;
using Common;
using Common.TrueDBGrid;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// 测试用例设计方法
    /// 与版本无关
    /// </summary>
    [TypeNameMap("wx.TestMethodForm")]
    public partial class TestMethodForm : MyBaseForm
    {
        DataTable dt1;
        TrueDBGridAssist gridAssist1;
        public static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestMethodForm>();

        public TestMethodForm()
        {
            InitializeComponent();
            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "序号");
            gridAssist1.columnList = columnList1;
        }

        static TestMethodForm()
        {
            columnList1.Add("序号", 60);
            columnList1.Add("测试用例设计方法", 150);
            columnList1.Add("说明", 250);
        }

        public override bool OnPageCreate()
        {
            dt1 = DBLayer1.GetDesignMethodList(dbProject, pid);
            if(dt1 == null) return false;
            gridAssist1.DataSource = dt1;
            gridAssist1.OnPageCreate();
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            gridAssist1.OnPageClose();
            if(!DBLayer1.UpdateDesignMethodList(dbProject, dt1)) return false;
            return true;
        }
    }
}