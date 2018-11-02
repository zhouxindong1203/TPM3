using System;
using System.Data;
using Common;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.wx
{
    /// <summary>
    /// 测试用例简明信息
    /// </summary>
    [TypeNameMap("wx.TestcaseSummeryInfoControl")]
    public partial class TestcaseSummeryInfoControl : MyUserControl
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestcaseSummeryInfoControl>();
        FlexGridAssist flexAssist1;

        DataTable dt;
        public TestcaseSummeryInfoControl()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithFocus(flex1);
            flexAssist1 = new FlexGridAssist(flex1, null, null);
            flexAssist1.columnList = columnList1;
            flexAssist1.doubleClickEdit = true;

            RefrenceColumnMapBase mapList = flexAssist1.refrenceColumnMapList;
            ColumnRefMap cm;
            foreach(string s in new[] { "测试人员", "设计人员" })
            {
                cm = mapList.AddColumnMap(s, () => DBLayer1.GetPersonList(dbProject, pid, currentvid), "ID", "姓名");
                cm.multiSelect = true;
                cm.seperator = ",";
                cm.columnList = PersonForm.columnList2;
            }

            cm = mapList.AddColumnMap("所使用的设计方法", "SELECT * FROM DC测试用例设计方法表 where 项目ID = ? ORDER BY 序号", "ID", "测试用例设计方法");
            cm.columnList = TestMethodForm.columnList1;
            cm.multiSelect = true;
            cm.seperator = ",";
            cm.AddParams(pid);
            cm.columnList = TestMethodForm.columnList1;
        }

        static TestcaseSummeryInfoControl()
        {
            columnList1.Add("序号", 50, false);
            columnList1.Add("测试用例名称", 250, false);
            columnList1.Add("测试用例标识", 150, false);
            columnList1.Add("所使用的设计方法", 100, "设计方法");
            columnList1.Add("设计人员", 100);
            columnList1.Add("测试人员", 100);
            columnList1.Add("测试时间", 80);
#if !Package
            columnList1.Add("用例的初始化", 100);
            columnList1.Add("前提和约束", 100);
#endif
        }

        public override bool OnPageCreate()
        {
            if(summary == null)
            {
                summary = new TestResultSummary(pid, currentvid);
                summary.OnCreate();
            }

            InnerVisitClass vc = new InnerVisitClass();
            summary[id].DoVisit(vc.GetTestcaseList);
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
            DataTable dt2 = dt.Copy();  // 写回 实体表
            // 测试用例ID ==> ID
            dt2.Columns.Remove("ID");
            dt2.Columns["测试用例ID"].ColumnName = "ID";
            dbProject.UpdateDatabase(dt2, "select ID, 设计人员, 所使用的设计方法, 用例的初始化, 前提和约束 from CA测试用例实体表");

            dbProject.UpdateDatabase(dt, "select ID, 测试人员, 测试时间 from CA测试用例实测表");
            return true;
        }

        public class InnerVisitClass
        {
            public DataTable dt;

            public InnerVisitClass()
            {
                dt = new DataTable();
                GridAssist.AddColumn(dt, "ID", "测试用例ID", "测试用例名称", "测试用例标识");
                GridAssist.AddMemoColumn(dt, "测试人员", "设计人员", "所使用的设计方法", "用例的初始化", "前提和约束");
                GridAssist.AddColumn<DateTime>(dt, "测试时间");
                GridAssist.AddColumn<int>(dt, "序号");
            }

            public void GetTestcaseList(ItemNodeTree item)
            {
                DataRow drItem = item.dr;
                if(item.nodeType != NodeType.TestCase) return;
                if(item.IsShortCut) return;

                DataRow dr = dt.Rows.Add();
                foreach(string s in new[] { "ID", "测试用例ID", "测试用例名称", "测试人员", "测试时间", "设计人员", "所使用的设计方法", "用例的初始化", "前提和约束" })
                    dr[s] = drItem[s];     // 实测ID， 实体ID
                dr["序号"] = dt.Rows.Count;
                dr["测试用例标识"] = item.GetItemSign();
                dr.AcceptChanges();
            }
        }
    }
}
