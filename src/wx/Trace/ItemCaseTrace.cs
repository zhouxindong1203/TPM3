using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using Common;
using TPM3.Sys;
using C1.Win.C1FlexGrid;
using Scalar2 = Common.Database.Scalar2;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.wx
{
    /// <summary>
    /// 测试项-测试用例追踪表
    /// </summary>
    [TypeNameMap("wx.ItemCaseTrace")]
    public partial class ItemCaseTrace : MyUserControl
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<ItemCaseTrace>(1);
        protected FlexGridAssist flexAssist1;

        Color bkcr1, bkcr2;
        public ItemCaseTrace()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            bkcr1 = flex1.Styles.Alternate.BackColor;
            bkcr2 = flex1.Styles.Normal.BackColor;
            flex1.Styles.Alternate.Clear(StyleElementFlags.BackColor);
            flex1._colName = "对象ID";
            flex1.AddMapCol("条目ID", "条目序号", "条目章节", "条目名称");
            flex1.AddMapCol("分类ID", "分类名称");

            flexAssist1 = new FlexGridAssist(flex1, null, null) { columnList = columnList1 };
            flexAssist1.AddHyperColumn("用例章节");
            flexAssist1.AddHyperColumn("用例名称");
            flexAssist1.RowNavigate += OnRowNavigate;
            flexAssist1._hyperColumn.CanRowNavigate += CanRowNavigate;
            lbTitle.Text = "测试项-测试用例对应表";
        }

        static ItemCaseTrace()
        {
            columnList1.Add("对象名称", 75);
            columnList1.Add("分类名称", 75);
            columnList1.Add("条目序号", 40, "序号");
            columnList1.Add("条目章节", 120, "测试项在测试计划中的章节");
            columnList1.Add("条目名称", 150, "测试项名称");
            columnList1.Add("用例章节", 160, "测试项/测试用例在测试说明中的章节");
            columnList1.Add("用例名称", 160, "测试项/测试用例名称");
        }

        public override bool OnPageCreate()
        {
            summary = new TestResultSummary(pid, currentvid);
            summary.OnCreate();
            cbObject.Items.Clear();
            cbObject.Items.Add(new Scalar2(null, "(全部被测对象)"));
            foreach(ItemNodeTree item in summary.childlist)    // 遍历所有对象
                cbObject.Items.Add(new Scalar2(item.id, item.name));
            cbObject.SelectedIndex = 0;
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            flexAssist1.OnPageClose();
            return true;
        }

        void OnRowNavigate(int row, int col, Row r)
        {
            //MainForm.mainFrm.SelectTestTree(r["用例ID"]);
        }

        bool CanRowNavigate(int row, int col)
        {
            DataRowView drv = flex1.Rows[row].DataSource as DataRowView;
            string imagekey = drv["imagekey"].ToString();
            return imagekey != "" && imagekey != "item";
        }

        public static DataView GetItemCaseTraceView(object _pid, object _vid, TestResultSummary summary, object oid, int itemDocumentType, int caseDocumentType)
        {
            if(summary == null)
            {
                summary = new TestResultSummary(_pid, _vid);
                summary.OnCreate();
            }
            ClassItemCaseTable vc = new ClassItemCaseTable { itemDocumentType = itemDocumentType, caseDocumentType = caseDocumentType };
            summary[oid].DoVisit(vc.GetAllItem);  // 先对条目进行编号
            summary[oid].DoVisit(vc.GetAllClassItemCase);
            DataView dv = new DataView(vc.dt) { Sort = "条目序号" };
            return dv;

        }

        public DataView GetItemCaseTraceView(object oid, int itemDocumentType, int caseDocumentType)
        {
            return GetItemCaseTraceView(null, null, summary, oid, itemDocumentType, caseDocumentType);
        }

        private void cbObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnPageClose(false);
            Scalar2 sc = cbObject.Items[cbObject.SelectedIndex] as Scalar2;
            flex1.DataSource = GetItemCaseTraceView(sc.Key, 1, 2);
            AddMergeColumn(flex1, "对象名称", "分类名称", "条目序号", "条目章节", "条目名称");
            flexAssist1.OnPageCreate();
            FlexGridAssist.AutoSizeRows(flex1, 4);
        }

        void flex1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if(e.Col < flex1.Cols.Fixed || e.Row < flex1.Rows.Fixed) return;
            DataRowView drv = flex1.Rows[e.Row].DataSource as DataRowView;
            if(drv == null) return;
            if(e.Col == flex1.Cols["用例名称"].Index)
            {
                string imagekey = drv["imagekey"].ToString();
                if(imagekey == "")
                {
                    if(!flex1.Styles.Contains("Red"))
                    {
                        CellStyle cs = flex1.Styles.Add("Red", flex1.Styles.Normal);
                        cs.ForeColor = Color.Red;
                    }
                    e.Style = flex1.Styles["Red"];
                    e.Text = "该测试项下未设计测试子项/测试用例！";
                }
                else
                    e.Image = ImageForm.treeNodeImage.Images[imagekey];
            }
        }
    }

    class ClassItemCaseTable
    {
        public DataTable dt;
        public int itemDocumentType, caseDocumentType;

        public ClassItemCaseTable()
        {
            dt = new DataTable("分类条目用例");
            GridAssist.AddColumn(dt, "对象ID", "对象名称", "对象标识",
                "分类ID", "分类名称", "分类标识",
                "条目ID", "条目名称", "条目标识", "条目章节",
                "用例ID", "用例名称", "用例标识", "引用用例标识", "用例章节", "imagekey");
            GridAssist.AddColumn<int>(dt, "条目序号");
        }

        int itemIndex = 1;

        // [itemID, index]
        Dictionary<object, int> itemIndexMap = new Dictionary<object, int>();

        /// <summary>
        /// 对条目进行编号 ==> itemIndexMap
        /// </summary>
        public void GetAllItem(ItemNodeTree item)
        {
            if(item.nodeType != NodeType.TestItem) return;
            itemIndexMap[item.id] = itemIndex++;
        }

        public void GetAllClassItemCase(ItemNodeTree item)
        {
            if(item.nodeType != NodeType.TestItem) return;
            ItemNodeTree itemObject = item.GetFarestParent(NodeType.TestObject);
            var itemClass = item.GetLeastParent(NodeType.TestType);

            DataColumnCollection dcc = dt.Columns;
            dcc["对象ID"].DefaultValue = itemObject.id;
            dcc["对象名称"].DefaultValue = itemObject.name;
            dcc["对象标识"].DefaultValue = itemObject.GetItemSign();

            dcc["分类ID"].DefaultValue = itemClass.id;
            dcc["分类名称"].DefaultValue = itemClass.name;
            dcc["分类标识"].DefaultValue = itemClass.GetItemSign();

            dcc["条目ID"].DefaultValue = item.id;
            dcc["条目名称"].DefaultValue = item.name;
            dcc["条目标识"].DefaultValue = item.GetItemSign();
            dcc["条目章节"].DefaultValue = item.GetItemChapter(itemDocumentType);
            dcc["条目序号"].DefaultValue = itemIndexMap[item.id];

            foreach(ItemNodeTree child in item.childlist)
            {   // 对每个用例或者条目，增加一条记录
                DataRow dr = dt.Rows.Add();
                dr["用例ID"] = child.id;
                dr["用例名称"] = child.name;
                dr["用例标识"] = child.GetItemSign();
                if(child.nodeType == NodeType.TestCase && child.IsShortCut)
                    dr["引用用例标识"] = child.GetTestCaseEntity().GetItemSign();
                dr["用例章节"] = child.GetItemChapter(caseDocumentType);
                dr["imagekey"] = child.GetIconName();
            }

            if(item.childlist.Count == 0)     // 该测试项下没有对应的测试用例
                dt.Rows.Add();
        }
    }
}
