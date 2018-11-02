using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using Common;
using C1.Win.C1FlexGrid;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.wx
{
    /// <summary>
    /// 附件与用例追踪控件
    /// </summary>
    [TypeNameMap("wx.AttachCaseTrace")]
    public partial class AttachCaseTrace : MyUserControl
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<AttachCaseTrace>(1);
        protected FlexGridAssist flexAssist1;

        public AttachCaseTrace()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            bkcr1 = flex1.Styles.Alternate.BackColor;
            bkcr2 = flex1.Styles.Normal.BackColor;
            flex1.Styles.Alternate.Clear(StyleElementFlags.BackColor);
            flex1._colName = "附件ID";

            flexAssist1 = new FlexGridAssist(flex1, null, null) { columnList = columnList1 };
            flexAssist1.AddHyperColumn("测试用例章节");
            flexAssist1.AddHyperColumn("测试用例名称");
            flexAssist1.RowNavigate += OnRowNavigate;
            lbTitle.Text = "附件-测试用例对应表";
            comboBox1.SelectedIndex = 1;
        }

        static AttachCaseTrace()
        {
            columnList1.Add("附件序号", 50, "序号");
            columnList1.Add("附件名称", 250);
            columnList1.Add("是否输出", 70, "可否打印(显示)");
            //columnList1.Add("测试用例标识", 100);
            columnList1.Add("测试用例章节", 160);
            columnList1.Add("测试用例名称", 160);
            columnList1.Add("测试过程序号", 70);
            columnList1.Add("所属列", 80);
        }

        public override bool OnPageCreate()
        {
            flex1.DataSource = GetAttachCaseTraceView(DocumentType);
            AddMergeColumn(flex1, "附件序号", "附件名称", "是否输出");
            flexAssist1.OnPageCreate();
            FlexGridAssist.AutoSizeRows(flex1, 4);
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            flexAssist1.OnPageClose();
            return true;
        }

        void OnRowNavigate(int row, int col, Row r)
        {
            //MainForm.mainFrm.SelectTestTree(r["测试用例ID"]);
        }

        public static DataView GetAttachCaseTraceView(int documentType)
        {
            DataView dv = GetAttachCaseTraceBaseView(documentType);
            dv.Sort = "附件序号, 测试用例序号, 测试过程序号";
            return dv;
        }

        protected static DataView GetAttachCaseTraceBaseView(int documentType)
        {
            TestResultSummary summary = new TestResultSummary(pid, currentvid);
            summary.OnCreate();
            AttachCaseTraceVisitClass vc = new AttachCaseTraceVisitClass(documentType);

            vc.AddColList("输入及操作", "期望结果");
            summary.DoVisit(vc.GetAttachTraceInfo);

            if(documentType == 3)  // 测试记录，包括实测结果列
            {
                vc.AddColList("实测结果");
                summary.DoVisit(vc.GetAttachTraceInfo);
            }
            return new DataView(vc.dtTrace);
        }

        Color bkcr1, bkcr2;
        protected string keyIndexCol = "附件序号";
        void flex1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if(e.Row < flex1.Rows.Fixed || e.Col < flex1.Cols.Fixed) return;
            int index = (int)flex1[e.Row, keyIndexCol];
            Color bkcr = (index % 2) == 0 ? bkcr1 : bkcr2;
            e.Style.BackColor = bkcr;
        }

        /// <summary>
        /// 文档类型: 2:测试说明，3:测试记录
        /// </summary>
        protected int DocumentType
        {
            get { return comboBox1.SelectedIndex + 2; }
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnPageClose(false);
            OnPageCreate();
        }
    }

    /// <summary>
    /// 测试用例-附件对应表
    /// </summary>
    [TypeNameMap("wx.CaseAttachTrace")]
    public class CaseAttachTrace : AttachCaseTrace
    {
        static ColumnPropList columnList2 = GridAssist.GetColumnPropList<CaseAttachTrace>(2);

        public CaseAttachTrace()
        {
            flex1._colName = "测试用例ID";
            flexAssist1.columnList = columnList2;
            keyIndexCol = "测试用例序号";
            lbTitle.Text = "测试用例-附件对应表";
        }

        static CaseAttachTrace()
        {
            columnList2.Add("测试用例章节", 140);
            columnList2.Add("测试用例名称", 140);
            columnList2.Add("测试过程序号", 70);
            columnList2.Add("所属列", 80);
            columnList2.Add("附件名称", 240);
            columnList2.Add("是否输出", 70, "可否打印(显示)");
            //columnList2.Add("测试用例标识", 100);
        }

        public override bool OnPageCreate()
        {
            flex1.DataSource = GetCaseAttachTraceView(DocumentType);
            AddMergeColumn(flex1, "测试用例章节", "测试用例名称");
            flexAssist1.OnPageCreate();
            FlexGridAssist.AutoSizeRows(flex1, 4);
            return true;
        }

        public static DataView GetCaseAttachTraceView(int documentType)
        {
            DataView dv = GetAttachCaseTraceBaseView(documentType);
            dv.Sort = "测试用例序号, 测试过程序号, 所属列, 附件序号";
            return dv;
        }
    }

    public class AttachCaseTraceVisitClass : BaseProjectClass
    {
        public DataTable dtCaseAttach, dtTrace;
        public int documentType;

        DataTableMap dtmAttach, dtmStepRef, dtmStepEntity;

        /// <summary>
        /// 附件所在的列号
        /// </summary>
        string[] colList;

        /// <summary>
        /// 设置要检查的列号
        /// </summary>
        public void AddColList(params string[] s)
        {
            this.colList = s;
        }

        public AttachCaseTraceVisitClass(int documentType)
        {
            dtCaseAttach = ExecuteDataTable("select ID, 测试过程ID, 附件实体ID, 附件所属 from DC测试过程附件表 where 测试版本 = ?", currentvid);
            DataTable dtAttach = ExecuteDataTable("select ID, 附件名称, 输出与否 from DC附件实体表 where 项目ID = ?", pid);
            dtmAttach = new DataTableMap(dtAttach, "ID");
            this.documentType = documentType;

            string sqlStep = @"SELECT sr.ID, sr.过程ID, se.测试用例ID, sr.序号, sr.问题报告单ID
                FROM CA测试过程实测表 AS sr INNER JOIN CA测试过程实体表 AS se ON sr.过程ID = se.ID where sr.测试版本 = ?";
            DataTable dtStep = ExecuteDataTable(sqlStep, currentvid);
            dtmStepRef = new DataTableMap(dtStep, "ID");
            dtmStepEntity = new DataTableMap(dtStep, "过程ID");

            // 查找dtCaseAttach的每一行的用例ID
            //GridAssist.AddColumn(dtCaseAttach, "测试用例ID");
            //foreach( DataRow dr in dtCaseAttach.Rows )
            //{
            //    bool isEntity = IsEntityAttach(dr["附件所属"] as string);

            //    Func<object, DataRow> GetRow = isEntity ? dtmStepEntity.GetRow : dtmStepRef.GetRow;
            //    DataRow drStep = GetRow(dr["测试过程ID"]);
            //    if( drStep == null ) continue;
            //    dr["测试用例ID"] = drStep["测试用例ID"];
            //}

            dtTrace = new DataTable();
            GridAssist.AddColumn(dtTrace, "附件ID", "附件名称", "测试用例ID", "测试用例标识", "测试用例章节", "测试用例名称", "测试过程ID", "所属列");
            GridAssist.AddColumn<int>(dtTrace, "附件序号", "测试用例序号", "测试过程序号");  // 总序号，排序用
            GridAssist.AddColumn<bool>(dtTrace, "是否输出");
        }

        int caseIndex = 0, attachIndex = 1;
        // [attach id, Attach index]
        Dictionary<object, int> attachIndexMap = new Dictionary<object, int>();
        // [case id, case index]
        Dictionary<object, int> caseIndexMap = new Dictionary<object, int>();

        /// <summary>
        /// [case id,  list of attach id ]
        /// 用例与其所属的所有附件列表，用于防止一个用例中引用多次相同附件时添加多次的问题
        /// </summary>
        //Dictionary<object, List<object>> caseAttachMap = new Dictionary<object, List<object>>();

        public void GetAttachTraceInfo(ItemNodeTree item)
        {
            if(item.nodeType != NodeType.TestCase) return;
            DataRow drItem = item.dr;
            object caseid = item.id;
            if(item.IsShortCut) return;

            foreach(string col in colList)
            {
                foreach(DataRow dr in dtCaseAttach.Rows)
                {
                    if(!Equals(dr["附件所属"], col)) continue;
                    bool isEntity = IsEntityAttach(col);

                    Common.Func<object, DataRow> GetRow = isEntity ? (Common.Func<object, DataRow>)dtmStepEntity.GetRow : dtmStepRef.GetRow;
                    DataRow drStep = GetRow(dr["测试过程ID"]);
                    if(drStep == null) continue;
                    if(!Equals(drStep["测试用例ID"], caseid)) continue;

                    DataRow drAttach = dtmAttach.GetRow(dr["附件实体ID"]);
                    if(drAttach == null) continue;

                    object attachid = drAttach["ID"];

                    //if(!caseAttachMap.ContainsKey(caseid)) caseAttachMap[caseid] = new List<object>();
                    //List<object> attachlist = caseAttachMap[caseid];
                    //if(attachlist.Contains(attachid)) continue; // 已经被加入
                    //else attachlist.Add(attachid);

                    if(!attachIndexMap.ContainsKey(attachid)) attachIndexMap[attachid] = attachIndex++;
                    if(!caseIndexMap.ContainsKey(caseid)) caseIndexMap[caseid] = caseIndex++;

                    DataRow drTrace = dtTrace.Rows.Add();
                    drTrace["附件序号"] = attachIndexMap[attachid];
                    drTrace["附件ID"] = attachid;
                    drTrace["附件名称"] = drAttach["附件名称"];
                    drTrace["是否输出"] = drAttach["输出与否"];
                    drTrace["测试用例ID"] = caseid;
                    drTrace["测试用例序号"] = caseIndexMap[caseid];
                    drTrace["测试用例标识"] = item.GetItemSign();
                    drTrace["测试用例章节"] = item.GetItemChapter(documentType); // 测试记录章节号
                    drTrace["测试用例名称"] = item.name;
                    drTrace["测试过程ID"] = dr["测试过程ID"];
                    drTrace["所属列"] = col;
                    drTrace["测试过程序号"] = drStep["序号"];
                }
            }
        }

        static bool IsEntityAttach(string attachType)
        {
            return "实测结果" != attachType;
        }
    }
}
