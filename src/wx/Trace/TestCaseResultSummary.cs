using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Common;
using TPM3.Sys;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.wx
{
    /// <summary>
    /// 测试用例执行结果列表
    /// </summary>
    [TypeNameMap("wx.TestCaseResultSummary")]
    public partial class TestCaseResultSummary : MyUserControl
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestCaseResultSummary>();
        FlexGridAssist flexAssist1;

        public TestCaseResultSummary()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            flex1.Styles.Alternate.Clear(StyleElementFlags.BackColor);
            flexAssist1 = new FlexGridAssist(flex1, null, null) { columnList = columnList1 };
            flex1._colName = "序号";
            flexAssist1.AddHyperColumn("测试用例名称");
            flexAssist1.AddHyperColumn("测试用例标识");
            flexAssist1.AddHyperColumn("问题报告单标识");
            flexAssist1.RowNavigate += OnRowNavigate;
            flexAssist1._hyperColumn.ShowHyperColor = false;
            //flexAssist1._hyperColumn.ShowUnderLine = false;
        }

        static TestCaseResultSummary()
        {
            columnList1.Add("序号", 50);
            columnList1.Add("测试用例名称", 100);
            columnList1.Add("测试用例标识", 150);
            columnList1.Add("测试用例章节号", 100, "章节号(测试记录)");
            columnList1.Add("执行状态", 70);
            columnList1.Add("执行结果", 70);
            columnList1.Add("步骤数", 60, "测试点数");
            columnList1.Add("错误步骤", 70);
            columnList1.Add("问题报告单标识", 120);
        }

        public override bool OnPageCreate()
        {
            InitFlex();
            return true;
        }

        void InitFlex()
        {
            if(summary == null)
            {
                summary = new TestResultSummary(pid, currentvid);
                summary.OnCreate();
            }

            TestcaseListVisitClass vc = new TestcaseListVisitClass();
            summary[id].DoVisit(vc.GetTestcaseResultList);

            flexAssist1.DataSource = vc.dtTestcase;
            flexAssist1.OnPageCreate();
            AddMergeColumn(flex1, "序号", "测试用例名称", "测试用例标识", "测试用例章节号", "执行状态", "执行结果", "步骤数");
            FlexGridAssist.AutoSizeRows(flex1, 4);
        }

        public override bool OnPageClose(bool bClose)
        {
            flexAssist1.OnPageClose();
            return true;
        }

        private void flex1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if(e.Col < flex1.Cols.Fixed || e.Row < flex1.Rows.Fixed) return;
            DataRowView drv = flex1.Rows[e.Row].DataSource as DataRowView;
            if(drv == null) return;
            string result = drv["执行结果"] as string;
            if(result == "未通过")
            {
                if(!flex1.Styles.Contains("CaseError"))
                {
                    CellStyle cs = flex1.Styles.Add("CaseError", flex1.Styles.Normal);
                    cs.ForeColor = Color.Red;
                }
                e.Style = flex1.Styles["CaseError"];
            }
            if(result == "通过")
            {
                if(!flex1.Styles.Contains("CasePassed"))
                {
                    CellStyle cs = flex1.Styles.Add("CasePassed", flex1.Styles.Normal);
                    cs.ForeColor = Color.Green;
                }
                e.Style = flex1.Styles["CasePassed"];
            }
            if(e.Col == flex1.Cols.Fixed)
            {
                string imagekey = drv["imagekey"].ToString();
                e.Image = ImageForm.treeNodeImage.Images[imagekey];
            }
        }

        void OnRowNavigate(int row, int col, Row r)
        {
            string colName = flex1.Cols[col].Name;
            if(colName == "问题报告单标识")
            {
                MainForm.mainFrm.DelayCreateForm("zxd.pbl.PblRepsForm?id=" + r["问题报告单ID"]);
            }
            else if(colName == "测试用例名称" || colName == "测试用例标识")
                MainForm.mainFrm.DelayCreateForm("zxd.TestTreeForm?type=result&id=" + r["测试用例实测ID"]);
        }

        void llRepare_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TestcaseListVisitClass_Repare vc = new TestcaseListVisitClass_Repare();
            summary.DoVisit(vc.StartRepare);
            vc.OnSave();
            if(vc.repairCount > 0)
            {
                summary = null;
                InitFlex();
            }
            string msg = string.Format("修复数据库成功,共修复{0}条记录。", vc.repairCount);
            MessageBox.Show(msg);
        }
    }

    public class TestcaseListVisitClass_Repare : BaseProjectClass
    {
        DataTable dtTestcase;
        DataTableGroup dtgStep;
        DataTableMap dtmTestcase;
        string sqlCase2 = "select * from CA测试用例实测表 where 测试版本 = ?";
        public int repairCount = 0;

        public TestcaseListVisitClass_Repare()
        {
            string sqlStep = @"SELECT sr.ID, sr.过程ID, se.测试用例ID, sr.序号, sr.问题报告单ID
                FROM CA测试过程实测表 AS sr INNER JOIN CA测试过程实体表 AS se ON sr.过程ID = se.ID where sr.测试版本 = ? order by sr.序号";
            DataTable dtStep = ExecuteDataTable(sqlStep, currentvid);
            dtgStep = new DataTableGroup(dtStep, "测试用例ID");

            dtTestcase = ExecuteDataTable(sqlCase2, currentvid);
            dtmTestcase = new DataTableMap(dtTestcase, "ID");
        }

        public void StartRepare(ItemNodeTree item)
        {
            DataRow drItem = item.dr;
            if(item.nodeType != NodeType.TestCase) return;
            if(item.IsShortCut) return;

            DataRow drTestcase = dtmTestcase.GetRow(drItem["ID"]);
            if(drTestcase == null) return;

            string execute = drItem["执行状态"].ToString();
            if(execute != "完整执行") return;  // 目前只处理完整执行

            // 从步骤中获取状态
            bool Ispassed = true;
            foreach(DataRow drStep in dtgStep.GetRowList(item.id))
            {
                if(GridAssist.IsNull(drStep["问题报告单ID"])) continue;
                Ispassed = false;
                break;
            }
            string passed = Ispassed ? "通过" : "未通过";
            if(!Equals(drTestcase["执行结果"], passed)) repairCount++;
            GridAssist.SetRowValue(drTestcase, "执行结果", passed);
        }

        public void OnSave()
        {
            dbProject.UpdateDatabase(dtTestcase, sqlCase2);
        }
    }


    public class TestcaseListVisitClass : BaseProjectClass
    {
        public DataTable dtTestcase;
        public string splitter;
        DataTableGroup dtgStep;

        /// <summary>
        /// [fallid, fallSign]
        /// </summary>
        Dictionary<object, string> fallSignMap = new Dictionary<object, string>();

        public TestcaseListVisitClass()
        {
            dtTestcase = new DataTable();
            GridAssist.AddColumn(dtTestcase, "测试用例ID", "测试用例实测ID", "测试用例名称", "测试用例标识",
               "测试用例章节号", "执行状态", "执行结果", "问题报告单ID", "问题报告单标识", "imagekey");
            GridAssist.AddColumn<int>(dtTestcase, "序号", "错误步骤", "步骤数");

            //splitter = ProjectInfo.GetDocString(dbProject, pid, currentvid, null, "问题标识分隔符");
            splitter = Z1.tpm.DB.CommonDB.GetPblSpl(dbProject, (string)pid, (string)currentvid);

            string sqlStep = @"SELECT sr.ID, sr.过程ID, se.测试用例ID, sr.序号, sr.问题报告单ID
                FROM CA测试过程实测表 AS sr INNER JOIN CA测试过程实体表 AS se ON sr.过程ID = se.ID where sr.测试版本 = ? order by sr.序号";
            DataTable dtStep = ExecuteDataTable(sqlStep, currentvid);
            dtgStep = new DataTableGroup(dtStep, "测试用例ID");
        }

        int index = 1;
        public void GetTestcaseResultList(ItemNodeTree item)
        {
            DataRow drItem = item.dr;
            if(item.nodeType != NodeType.TestCase) return;
            if(item.IsShortCut) return;
            object cid = drItem["测试用例ID"];

            DataColumnCollection dcc = dtTestcase.Columns;
            dcc["测试用例实测ID"].DefaultValue = drItem["ID"];
            dcc["测试用例ID"].DefaultValue = cid;
            dcc["序号"].DefaultValue = index++;
            dcc["步骤数"].DefaultValue = dtgStep.GetRowList(cid).Count;
            dcc["测试用例名称"].DefaultValue = drItem["测试用例名称"];
            dcc["测试用例标识"].DefaultValue = item.GetItemSign();
            dcc["测试用例章节号"].DefaultValue = item.GetItemChapter(3);
            dcc["imagekey"].DefaultValue = item.GetIconName();
            string execute = drItem["执行状态"].ToString(), passed = drItem["执行结果"].ToString();
            dcc["执行状态"].DefaultValue = execute;
            if(execute == "未执行") passed = "";
            if(execute == "部分执行" && passed != "未通过") passed = "";
            dcc["执行结果"].DefaultValue = passed;

            bool Ispassed = true;
            foreach(DataRow drStep in dtgStep.GetRowList(item.id))
            {
                object fallid = drStep["问题报告单ID"];
                if(GridAssist.IsNull(fallid)) continue;
                DataRow dr = dtTestcase.Rows.Add();
                dr["错误步骤"] = drStep["序号"];
                dr["问题报告单ID"] = fallid;
                dr["问题报告单标识"] = GetFallSign(fallid);
                Ispassed = false;
            }
            if(Ispassed) dtTestcase.Rows.Add();
        }

        string GetFallSign(object fallid)
        {
            if(!fallSignMap.ContainsKey(fallid))
                fallSignMap[fallid] = Z1.tpm.DB.CommonDB.GenPblSignForStep(dbProject, splitter, fallid.ToString());
            return fallSignMap[fallid];
        }
    }
}
