using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using Common.TrueDBGrid;
using Common;
using Common.Database;
using C1.Win.C1FlexGrid;

namespace TPM3.wx
{
    /// <summary>
    /// 测试对象统计信息，包括用例统计，问题统计，人员统计
    /// </summary>
    public partial class TestObjectSummary : MyUserControl
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestObjectSummary>();
        TrueDBGridAssist gridAssist1;
        FlexGridAssist flexAssist1;

        public TestObjectSummary()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            flexAssist1 = new FlexGridAssist(flex1, null, null);
            flex1.Styles.Alternate.Clear(StyleElementFlags.BackColor);
            flex1.GetMergedRangeEvent += flex1_GetMergedRangeEvent;
            flex1.Rows[0].AllowMerging = true;
            flex1.Rows[5].AllowMerging = true;
            AddMergeColumn(flex1, 0, 1, 2, 3);
            for( int i = 0; i < flex1.Rows.Count; i++ )
                flex1.Rows[i].Height = 26;
            flex1[0, 0] = "设计的测试用例总数";
            flex1[1, 0] = "完全执行的测试用例数";
            flex1[2, 0] = "完全执行的测试用例数";
            flex1[3, 0] = "部分执行的测试用例数";
            flex1[5, 0] = "未执行的测试用例数";
            flex1[1, 2] = "通过的测试用例数";
            flex1[2, 2] = "未通过的测试用例数";
            flex1[3, 2] = "未通过的测试用例数";  // 通过的测试用例数
            flex1[4, 2] = "未通过的测试用例数";
            CellStyle cs = flex1.Styles.Add("粗体");
            cs.Font = new Font(flex1.Font.Name, flex1.Font.Size);
            cs.TextAlign = TextAlignEnum.CenterCenter;
            flex1.Cols[0].Style = flex1.Cols[2].Style = cs;

            cs = flex1.Styles.Add("非粗体");
            cs.Font = new Font(flex1.Font.Name, flex1.Font.Size);
            cs.TextAlign = TextAlignEnum.CenterCenter;
            flex1.Cols[1].Style = flex1.Cols[3].Style = cs;

            gridAssist1 = new TrueDBGridAssist(grid1, null, "序号");
            gridAssist1.columnList = columnList1;
        }

        CellRange flex1_GetMergedRangeEvent(C1FlexGrid sender, int row, int col, bool clip)
        {
            CellRange rg = sender.GetCellRange(row, col);
            int cnt = sender.Cols.Count;

            if( row == 0 || row == 5 )
            {
                if( col >= 1 )
                {
                    rg.c1 = 1;
                    rg.c2 = cnt - 1;
                }
            }
            if( col == 0 || col == 1 )
            {
                if( row == 1 || row == 2 )
                {
                    rg.r1 = 1;
                    rg.r2 = 2;
                }
                else if( row == 3 || row == 4 )
                {
                    rg.r1 = 3;
                    rg.r2 = 4;
                }
            }
            if( col == 2 || col == 3 )
            {
                if( row == 3 || row == 4 )
                {
                    rg.r1 = 3;
                    rg.r2 = 4;
                }
            }
            return rg;
        }

        static TestObjectSummary()
        {
            columnList1.Add("角色", 100);
            columnList1.Add("姓名", 70);
            columnList1.Add("职称", 100);
            columnList1.Add("主要职责", 250);
        }

        public static bool IsNull(object o)
        {
            return GridAssist.IsNull(o);
        }

        public override bool OnPageCreate()
        {
            if( summary == null )
            {
                summary = new TestResultSummary(pid, currentvid);
                summary.OnCreate();
            }

            flexAssist1.OnPageCreate();
            ResultSummaryVisitClass vc = new ResultSummaryVisitClass();

            summary[id].DoVisit(vc.GetTestTime);
            lbBeginTime.Text = lbEndTime.Text = "N/A";
            if( vc.testBeginTime != null )
            {
                lbBeginTime.Text = vc.testBeginTime.Value.ToString("yyyy年MM月dd日");
                lbEndTime.Text = vc.testEndTime.Value.ToString("yyyy年MM月dd日");
            }

            summary[id].DoVisit(vc.GetCaseCount);
            flex1[0, 1] = vc.counts[8];
            flex1[1, 1] = vc.counts[1];
            flex1[1, 3] = vc.counts[4];
            flex1[2, 3] = vc.counts[5];
            flex1[3, 1] = vc.counts[2];
            flex1[3, 3] = vc.counts[7];   // vc.counts[6];
            flex1[4, 3] = vc.counts[7];
            flex1[5, 1] = vc.counts[3];
            //textBox1.Text = vc.GetSummaryString();

            gridAssist1.DataSource = vc.GetTestPersonTable();
            gridAssist1.OnPageCreate();

            testResultObjectControl41.id = summary[id].refid;    // 根据对象的[实体ID]查找[实测ID]
            testResultObjectControl41.OnPageCreate();
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            gridAssist1.OnPageClose();
            return true;
        }
    }
}
