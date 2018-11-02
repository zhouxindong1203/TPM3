using System.Data;
using Common.TrueDBGrid;
using Common;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// DC测试项优先级表，与版本无关
    /// </summary>
    [TypeNameMap("wx.TestPriorityForm")]
    public partial class TestPriorityForm : MyBaseForm
    {
        DataTable dt1;
        TrueDBGridAssist gridAssist1;
        public static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestPriorityForm>();

        public TestPriorityForm()
        {
            InitializeComponent();
            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "序号") {columnList = columnList1};
        }

        static TestPriorityForm()
        {
            columnList1.Add("序号", 60);
            columnList1.Add("优先级", 100);
            columnList1.Add("说明", 250);
        }

        public override bool OnPageCreate()
        {
            dt1 = DBLayer1.GetTestPriorityList(dbProject, pid);
            if( dt1 == null ) return false;
            gridAssist1.DataSource = dt1;
            gridAssist1.OnPageCreate();
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            gridAssist1.OnPageClose();
            if( !DBLayer1.UpdateTestPriorityList(dbProject, dt1) ) return false;
            return true;
        }
    }
}