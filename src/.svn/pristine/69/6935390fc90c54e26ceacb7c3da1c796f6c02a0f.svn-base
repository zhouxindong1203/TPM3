using System.Data;
using C1.Win.C1FlexGrid;
using Common;
using TPM3.Sys;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.wx
{
    /// <summary>
    /// 未完整执行测试用例
    /// </summary>
    [TypeNameMap("wx.UnExecCase")]
    public partial class UnExecCase : MyUserControl
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<UnExecCase>();
        FlexGridAssist flexAssist1;

        DataTable dt;
        public UnExecCase()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            flexAssist1 = new FlexGridAssist(flex1, null, null);
            flexAssist1.AddHyperColumn("测试用例名称");
            flexAssist1.AddHyperColumn("测试用例标识");
            flexAssist1.columnList = columnList1;
            flexAssist1.RowNavigate += OnRowNavigate;
        }

        static UnExecCase()
        {
            columnList1.Add("序号", 50, false);
            columnList1.Add("测试用例名称", 100, false);
            columnList1.Add("测试用例标识", 150, false);
            columnList1.Add("执行状态", 70, false);
            columnList1.Add("未执行原因", 200, "未完整执行原因说明");
        }

        public override bool OnPageCreate()
        {
            if(summary == null)
            {
                summary = new TestResultSummary(pid, currentvid);
                summary.OnCreate();
            }

            TestcaseUnExecutedListVisitClass vc = new TestcaseUnExecutedListVisitClass();
            summary[id].DoVisit(vc.GetUnExecuteTestcaseList);
            dt = vc.dt;

            flexAssist1.DataSource = dt;
            flexAssist1.OnPageCreate();
            FlexGridAssist.AutoSizeRows(flex1, 4);
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            if(dt == null) return true;
            flexAssist1.OnPageClose();
            return dbProject.UpdateDatabase(dt, "select ID, 未执行原因 from CA测试用例实测表");
        }

        void OnRowNavigate(int row, int col, Row r)
        {
            MainForm.mainFrm.DelayCreateForm("zxd.TestTreeForm?type=result&id=" + r["ID"]);
        }
    }

    public class TestcaseUnExecutedListVisitClass
    {
        public DataTable dt;

        public TestcaseUnExecutedListVisitClass()
        {
            dt = new DataTable();
            GridAssist.AddColumn(dt, "ID", "测试用例ID", "测试用例名称", "测试用例标识", "执行状态", "未执行原因");
            GridAssist.AddColumn<int>(dt, "序号");
        }

        public void GetUnExecuteTestcaseList(ItemNodeTree item)
        {
            DataRow drItem = item.dr;
            if(item.nodeType != NodeType.TestCase) return;
            if(item.IsShortCut) return;

            if("完整执行".Equals(drItem["执行状态"])) return;
            DataRow dr = dt.Rows.Add();
            dr["ID"] = drItem["ID"];    // 引用ID
            dr["测试用例ID"] = drItem["测试用例ID"];  // 实体ID
            dr["序号"] = dt.Rows.Count;
            dr["测试用例名称"] = drItem["测试用例名称"];
            dr["测试用例标识"] = item.GetItemSign();
            dr["执行状态"] = drItem["执行状态"];
            dr["未执行原因"] = drItem["未执行原因"];
            dr.AcceptChanges();
        }
    }
}
