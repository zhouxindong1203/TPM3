using System;
using System.Data;
using System.Drawing;
using Common;
using TPM3.Sys;
using C1.Win.C1FlexGrid;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.wx
{
    /// <summary>
    /// 需求与条目追踪关系
    /// </summary>
    [TypeNameMap("wx.RequireItemTrace")]
    public partial class RequireItemTrace : MyUserControl
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<RequireItemTrace>(1);
        protected FlexGridAssist flexAssist1;
        protected bool GroupByDoc = RequireTreeForm.GroupByDoc;

        public RequireItemTrace()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            bkcr1 = flex1.Styles.Alternate.BackColor;
            bkcr2 = flex1.Styles.Normal.BackColor;
            flex1.AddMapCol("依据ID", "测试依据", "测试依据说明", "是否追踪");
            flex1.AddMapCol("测试依据类别ID", "测试依据类别");

            flexAssist1 = new FlexGridAssist(flex1, null, null);
            flexAssist1.columnList = columnList1;
            //flexAssist1.AddHyperColumn("测试用例章节");
            //flexAssist1.AddHyperColumn("测试用例名称");
            //flexAssist1.RowNavigate += OnRowNavigate;
            label3.Text = "测试依据-测试项追踪表";
            checkBox1.Text = "仅显示没有追踪到测试项的测试依据";
            if(GroupByDoc)
                keyIndexCol = "测试依据类别序号";
        }

        static RequireItemTrace()
        {
            columnList1.Add("测试依据类别", 100);
            columnList1.Add("测试依据", 150, RequireTreeForm.title1);
            columnList1.Add("测试依据说明", 170, RequireTreeForm.title2);
            columnList1.Add("是否追踪", 70);
            columnList1.Add("条目需求章节号", 100, "测试项在需求中的章节");
            columnList1.Add("条目计划章节号", 100, "测试项在计划中的章节");
            //columnList1.Add("条目标识", 150, "测试项标识");
            columnList1.Add("条目名称", 150, "测试项名称或不追踪原因说明");
        }

        public override bool OnPageCreate()
        {
            flex1.DataSource = GetRequireItemTraceView();
            AfterInitFlex();
            return true;
        }

        protected virtual void AfterInitFlex()
        {
            AddMergeColumn(flex1, "测试依据", "测试依据说明", "测试依据类别", "是否追踪");
            flexAssist1.OnPageCreate();
            flex1.Cols["测试依据类别"].Visible = GroupByDoc;
            FlexGridAssist.AutoSizeRows(flex1, 2);
        }

        public static void SetDataViewFilter(DataView dv)
        {
            string s = dv.RowFilter ?? "";
            if(s != "") s += " and ";
            s += "((条目ID is null and 是否追踪 = true ) or 依据ID is null)";
            dv.RowFilter = s;
        }

        public override bool OnPageClose(bool bClose)
        {
            flexAssist1.OnPageClose();
            return true;
        }

        public static DataView GetRequireItemTraceView()
        {
            TestResultSummary summary = new TestResultSummary(pid, currentvid);
            summary.OnCreate();
            RequireItemTraceVisitClass vc = new RequireItemTraceVisitClass(false, currentvid);
            summary.DoVisit(vc.GetRequireTraceInfo);
            vc.AddUnusedRequire();
            DataView dv = new DataView(vc.dtTrace);
            dv.Sort = "测试依据序号, 条目序号";
            if(RequireTreeForm.GroupByDoc)  // 文档项不能被引用
                dv.RowFilter = "测试依据类别 is not null";
            return dv;
        }

        Color bkcr1, bkcr2;
        protected string keyIndexCol = "测试依据序号";
        void flex1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if(e.Row < flex1.Rows.Fixed || e.Col < flex1.Cols.Fixed) return;
            if(keyIndexCol != null)
            {
                int index = (int)flex1[e.Row, keyIndexCol];
                Color bkcr = (index % 2) == 0 ? bkcr1 : bkcr2;
                e.Style.BackColor = bkcr;
            }

            e.Style.ForeColor = Color.Black;
            DataRowView drv = flex1.Rows[e.Row].DataSource as DataRowView;
            string name = flex1.Cols[e.Col].Name;
            if(name == "对象名称" || name == "测试依据类别") return;

            if(name == "是否追踪")
            {
                int image = true.Equals(drv["是否追踪"]) ? 1 : 0;
                e.Image = ImageForm._imageForm.ilYesNo.Images[image];
                e.Text = "";
                return;
            }

            if((bool)drv["warning"])
                e.Style.ForeColor = Color.Red;
        }

        string oldfilter = null;
        void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            flexAssist1.OnPageClose();

            DataView dv = flex1.DataSource as DataView;
            if(oldfilter == null) oldfilter = dv.RowFilter ?? "";
            if(checkBox1.Checked)
                SetDataViewFilter(dv);
            else
                dv.RowFilter = oldfilter;

            flexAssist1.DataSource = dv;
            AfterInitFlex();
        }
    }

    [TypeNameMap("wx.ItemRequireTrace")]
    public class ItemRequireTrace : RequireItemTrace
    {
        static ColumnPropList columnList2 = GridAssist.GetColumnPropList<ItemRequireTrace>(1);

        public ItemRequireTrace()
        {
            flex1._colName = "对象ID";
            flex1.AddMapCol("条目ID", "条目计划章节号", "条目需求章节号", "条目标识", "条目名称");
            flexAssist1.columnList = columnList2;
            keyIndexCol = "条目序号";
            label3.Text = "测试项-测试依据追踪表";
            checkBox1.Text = "仅显示没有追踪到测试依据的测试项";
        }

        static ItemRequireTrace()
        {
            columnList2.Add("对象名称", 100);
            columnList2.Add("条目需求章节号", 75, "测试项在需求中的章节");
            columnList2.Add("条目计划章节号", 75, "测试项在计划中的章节");
            //columnList2.Add("条目标识", 150, "测试项标识");
            columnList2.Add("条目名称", 150, "测试项名称");
            columnList2.Add("测试依据", 150, RequireTreeForm.title1);
            columnList2.Add("测试依据说明", 170, RequireTreeForm.title2);
        }

        public override bool OnPageCreate()
        {
            flex1.DataSource = GetItemRequireTraceView();
            AfterInitFlex();
            return true;
        }

        protected override void AfterInitFlex()
        {
            AddMergeColumn(flex1, "对象名称", "条目计划章节号", "条目需求章节号", "条目标识", "条目名称");
            flexAssist1.OnPageCreate();
            FlexGridAssist.AutoSizeRows(flex1, 4);
        }

        public static DataView GetItemRequireTraceView()
        {
            TestResultSummary summary = new TestResultSummary(pid, currentvid);
            summary.OnCreate();
            RequireItemTraceVisitClass vc = new RequireItemTraceVisitClass(true, currentvid);
            summary.DoVisit(vc.GetRequireTraceInfo);
            DataView dv = new DataView(vc.dtTrace);
            dv.Sort = "条目序号, 测试依据序号";
            return dv;
        }
    }

    public class RequireItemTraceVisitClass : BaseProjectClass
    {
        public DataTable dtRequire, dtTrace;
        DataTableMap dtmRequire;

        bool itemRequireTrace;
        public RequireItemTraceVisitClass(bool itemRequireTrace, object vid)
        {
            this.itemRequireTrace = itemRequireTrace;
            dtRequire = RequireTreeForm.GetRequireTreeTable(itemRequireTrace, vid);
            dtRequire.Columns.Add("used", typeof(int));    // 是否被添加，1: 有效，2: 被添加
            dtmRequire = new DataTableMap(dtRequire, "ID");
            foreach(DataRow dr in dtRequire.Rows)
                dr["used"] = 1;

            dtTrace = new DataTable();
            GridAssist.AddColumn(dtTrace, "依据ID", "测试依据", "测试依据说明", "测试依据类别ID", "测试依据类别");
            GridAssist.AddColumn<bool>(dtTrace, "是否追踪", "warning");
            GridAssist.AddColumn<int>(dtTrace, "测试依据序号", "测试依据类别序号", "条目序号");    // 条目序号:总序号，排序用
            GridAssist.AddColumn(dtTrace, "对象ID", "对象名称", "条目ID", "条目名称", "条目标识", "条目计划章节号", "条目需求章节号");

            dtTrace.Columns["warning"].DefaultValue = false;
        }

        int itemIndex = 1;

        public void GetRequireTraceInfo(ItemNodeTree item)
        {
            if(item.nodeType != NodeType.TestItem) return;
            ItemNodeTree itemObject = item.GetFarestParent(NodeType.TestObject);

            DataRow drItem = item.dr;
            itemIndex++;

            DataColumnCollection dcc = dtTrace.Columns;
            dcc["对象ID"].DefaultValue = itemObject.id;
            dcc["对象名称"].DefaultValue = itemObject.name;
            dcc["条目ID"].DefaultValue = item.id;
            dcc["条目序号"].DefaultValue = itemIndex;
            dcc["条目名称"].DefaultValue = item.name;
            dcc["条目标识"].DefaultValue = item.GetItemSign();
            dcc["条目计划章节号"].DefaultValue = item.GetItemChapter(1);
            dcc["条目需求章节号"].DefaultValue = item.GetItemChapter(0);

            bool findRequire = false;
            foreach(string req in KeyList.SplitKey(drItem["追踪关系"]))
            {
                DataRow drRequire = dtmRequire.GetRow(req);
                if(drRequire == null) continue;
                if(!(bool)drRequire["是否追踪"]) continue; // 该条需求不可被追踪

                drRequire["used"] = 2;
                findRequire = true;
                DataRow drTrace = dtTrace.Rows.Add();
                drTrace["依据ID"] = drRequire["ID"];
                foreach(string col in new[] { "测试依据序号", "测试依据", "测试依据说明", "测试依据类别ID", "测试依据类别", "测试依据类别序号", "是否追踪" })
                    drTrace[col] = drRequire[col];
            }

            if(itemRequireTrace && !findRequire)  // 添加没有需求的条目
            {
                DataRow drTrace = dtTrace.Rows.Add();
                drTrace["测试依据"] = "该处没有测试依据！";
                drTrace["warning"] = true;
            }
        }

        /// <summary>
        /// 添加所有未被引用的需求项
        /// </summary>
        public void AddUnusedRequire()
        {
            foreach(DataRow drRequire in dtRequire.Rows)
            {
                if(!Equals(drRequire["used"], 1)) continue;
                if(Equals(drRequire["父节点ID"], GlobalData.rootID)) continue;  // 根节点不加
                DataRow drTrace = dtTrace.Rows.Add();
                drTrace["依据ID"] = drRequire["ID"];
                foreach(string col in new[] { "测试依据序号", "测试依据", "测试依据说明", "测试依据类别ID", "测试依据类别", "测试依据类别序号", "是否追踪" })
                    drTrace[col] = drRequire[col];

                if((bool)drRequire["是否追踪"])
                {
                    drTrace["条目名称"] = "未被引用的需求项";
                    drTrace["warning"] = true;
                }
                else if(GridAssist.IsNull(drRequire["未追踪原因说明"]))
                {
                    drTrace["条目名称"] = "未填写不追踪原因说明";
                    drTrace["warning"] = true;
                }
                else
                {
                    drTrace["条目名称"] = drRequire["未追踪原因说明"];
                    drTrace["warning"] = false;
                }

                foreach(string col in new[] { "对象ID", "对象名称", "条目ID", "条目序号", "条目标识", "条目计划章节号", "条目需求章节号" })
                    drTrace[col] = DBNull.Value;
            }
        }
    }
}
