using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using C1.Win.C1FlexGrid;
using Common;
using TPM3.Sys;
using Z1.tpm;

namespace TPM3.wx
{
    /// <summary>
    /// 测试用例分类统计表：按人员、设计方法
    /// </summary>
    [TypeNameMap("wx.CaseTraceTree")]
    public partial class CaseTraceTree : MyUserControl
    {
        FlexGridAssist flexAssist1;

        /// <summary>
        /// 条目的列宽
        /// </summary>
        static int nameColWidth = 220;

        DataTableGroup dtpCaseStep;

        static int index1, index2;

        public CaseTraceTree()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            flexAssist1 = new FlexGridAssist(flex1, null, null);
            flex1.AllowDragging = AllowDraggingEnum.Columns;
        }

        public override bool OnPageCreate()
        {
            if(summary == null)
            {
                summary = new TestResultSummary(pid, currentvid);
                summary.OnCreate();
            }
            // 触发InitFlex函数
            comboBox1.SelectedIndex = index1;
            comboBox2.SelectedIndex = index2;
            return true;
        }

        void InitFlex()
        {
            CaseTraceType traceType = (CaseTraceType)comboBox2.SelectedIndex;

            flex1.BeginInit();
            flex1.Rows[0].Height = 22;

            flex1.Cols.Count = flex1.Cols.Fixed;
            flex1.Rows.Count = flex1.Rows.Fixed;
            Column c = flex1.Cols.Add();
            c.Name = c.Caption = "名称";
            c.TextAlign = TextAlignEnum.LeftCenter;
            c.Width = nameColWidth;

            c = flex1.Cols.Add();
            c.Name = c.Caption = "全部";
            c.Width = 50;

            InitFlexColumn(traceType);

            c = flex1.Cols.Add();
            c.Name = c.Caption = "NodeType";
            c.Visible = false;

            // 初始化测试步骤表
            if(dtpCaseStep == null)
            {
                DataTable dtStep = dbProject.ExecuteDataTable("SELECT 测试用例ID, 评估标准 FROM CA测试过程实体表");
                dtpCaseStep = new DataTableGroup(dtStep, "测试用例ID");
            }

            var ftc = new CaseFlexTreeClass2(flex1, comboBox1.SelectedIndex, traceType, dtpCaseStep);
            summary.DoVisit(ftc.AddTreeNode);

            if(IsTracePerson(traceType))
            {  // 如果按人员统计，则要删除掉那些没有提交问题的人员
                foreach(Column c2 in flex1.Cols)
                {
                    if(!Equals(c2.UserData, "统计")) continue;
                    object obj = flex1[1, c2.Name];
                    if(GridAssist.IsNull(obj) || Equals(obj, 0))
                        c2.Visible = false;
                }
            }

            flex1.EndInit();
            flex1.Invalidate();

            flexAssist1.OnPageCreate();
        }

        public static bool IsTracePerson(CaseTraceType traceType)
        {
            return traceType == CaseTraceType.DesignPerson || traceType == CaseTraceType.TestPerson;
        }

        void InitFlexColumn(CaseTraceType traceType)
        {
            string sql = !IsTracePerson(traceType) ? "select ID, 测试用例设计方法 from DC测试用例设计方法表 where 项目ID = ? order by 序号" : "select ID, 姓名 from DC测试组织与人员表 where 项目ID = ? order by 序号";
            DataTable dt = dbProject.ExecuteDataTable(sql, pid);
            foreach(DataRow dr in dt.Rows)
            {
                var c = flex1.Cols.Add();
                c.Name = dr[0].ToString();
                c.Caption = dr[1].ToString();
                c.TextAlign = c.TextAlignFixed = TextAlignEnum.CenterCenter;
                c.Width = 80;
                c.UserData = "统计";
            }
        }

        public override bool OnPageClose(bool bClose)
        {
            flexAssist1.OnPageClose();
            if(flex1.Cols.Contains("名称"))
                nameColWidth = flex1.Cols["名称"].Width;
            return true;
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnPageClose(false);
            InitFlex();
            if(comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex >= 0)
            {
                index1 = comboBox1.SelectedIndex;
                index2 = comboBox2.SelectedIndex;
            }
        }
    }

    public class CaseFlexTreeClass2 : BaseProjectClass
    {
        public class CaseCountClass
        {
            // [统计项的ID， 计数]
            public Dictionary<string, int> countMap = new Dictionary<string, int>();
            public int klAllCount;

            public CaseFlexTreeClass2 parent;

            public void GetFallCount(ItemNodeTree item)
            {
                if(item.nodeType != NodeType.TestCase) return;
                if(item.IsShortCut) return;
                var traceType = parent.traceType;
                DataRow drCase = item.dr;

                if(CaseTraceTree.IsTracePerson(traceType))
                {
                    string col = traceType == CaseTraceType.DesignPerson ? "设计人员" : "测试人员";
                    foreach(string pid in KeyList.SplitKey(drCase[col], ","))
                        AddMap(pid);
                    klAllCount++;
                }
                else if(traceType == CaseTraceType.CaseDesignMethod)
                {
                    foreach(string pid in KeyList.SplitKey(drCase["所使用的设计方法"], ","))
                        AddMap(pid);
                    klAllCount++;
                }
                else if(traceType == CaseTraceType.StepDesignMethod)
                {
                    var stepList = parent.dtpCaseStep.GetRowList(drCase["测试用例ID"]);
                    foreach(DataRow drStep in stepList)
                        foreach(string pid in KeyList.SplitKey(drStep["评估标准"], ","))
                            AddMap(pid);
                    klAllCount += stepList.Count;
                }
            }

            void AddMap(object name)
            {
                string s = name.ToString();
                if(!countMap.ContainsKey(s)) countMap[s] = 0;
                countMap[s]++;
            }
        }

        DataTableGroup dtpCaseStep;
        C1FlexGrid flex;

        /// <summary>
        /// 统计级别，0 测试对象，1 测试分类， 2 测试项

        /// </summary>
        int summerLevel;

        CaseTraceType traceType;

        /// <summary>
        /// 构造函数，条目树

        /// </summary>
        public CaseFlexTreeClass2(C1FlexGrid flex, int summerLevel, CaseTraceType traceType, DataTableGroup dtpCaseStep)
        {
            this.flex = flex;
            this.summerLevel = summerLevel;
            this.traceType = traceType;
            this.dtpCaseStep = dtpCaseStep;
        }

        public void AddTreeNode(ItemNodeTree item)
        {
            if(item.nodeType == NodeType.TestCase) return;
            if(item.nodeType == NodeType.TestItem && summerLevel < 2) return;
            if(item.nodeType == NodeType.TestType && summerLevel < 1) return;

            Row r = flex.Rows.Add();
            r["名称"] = item.name;
            Image image = ImageForm.treeNodeImage.Images[item.GetIconName()];
            flex.SetCellImage(r.Index, flex.Cols["名称"].Index, image);

            r.IsNode = true;
            r.Node.Level = item.GetLevel();

            var vc = new CaseCountClass {parent = this};
            item.DoVisit(vc.GetFallCount);

            for(int i = flex.Cols.Fixed; i < flex.Cols.Count; i++)
            {
                if(!Equals(flex.Cols[i].UserData, "统计")) continue;
                string name = flex.Cols[i].Name;
                // r[name] = 0;
                if(vc.countMap.ContainsKey(name))
                    r[name] = vc.countMap[name];
            }
            r["NodeType"] = (int)item.nodeType;
            r["全部"] = vc.klAllCount;
        }
    }

    public enum CaseTraceType
    {
        DesignPerson = 0,
        TestPerson = 1,
        CaseDesignMethod = 2,
        StepDesignMethod = 3,
    }
}
