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
    /// 软件问题统计表
    /// </summary>
    [TypeNameMap("wx.FallTraceTree")]
    public partial class FallTraceTree : MyUserControl
    {
        FlexGridAssist flexAssist1;

        /// <summary>
        /// 条目的列宽
        /// </summary>
        static int nameColWidth = 220;

        static int index1, index2;

        public FallTraceTree()
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
            FallTraceType traceType = (FallTraceType)comboBox2.SelectedIndex;

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

            FlexTreeClass2 ftc = new FlexTreeClass2(flex1, comboBox1.SelectedIndex, traceType);
            summary.DoVisit(ftc.AddTreeNode);

            if(traceType == FallTraceType.Person)
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

        void InitFlexColumn(FallTraceType traceType)
        {
            string sqlFallType = "select ID, 名称 from DC问题级别表 where 项目ID = ? and 类型 = ? order by 序号";
            if(traceType != FallTraceType.Person)
            {
                string falltype = traceType == FallTraceType.FallType ? "类别" : "级别";
                DataTable dt = dbProject.ExecuteDataTable(sqlFallType, pid, falltype);
                foreach(DataRow dr in dt.Rows)
                {
                    if(GridAssist.IsNull(dr["名称"])) continue;
                    var c = flex1.Cols.Add();
                    c.Name = dr["ID"].ToString();
                    c.Caption = dr["名称"].ToString();
                    c.TextAlign = c.TextAlignFixed = TextAlignEnum.CenterCenter;
                    c.UserData = "统计";
                }
            }
            else
            {
                DataTable dt = dbProject.ExecuteDataTable("select ID, 姓名 from DC测试组织与人员表 where 项目ID = ? order by 序号", pid);
                foreach(DataRow dr in dt.Rows)
                {
                    var c = flex1.Cols.Add();
                    c.Name = dr["ID"].ToString();
                    c.Caption = dr["姓名"].ToString();
                    c.TextAlign = c.TextAlignFixed = TextAlignEnum.CenterCenter;
                    c.Width = 66;
                    c.UserData = "统计";
                }
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

    public class FlexTreeClass2 : BaseProjectClass
    {
        public class FallCountClass
        {
            public Dictionary<string, KeyList> fallMap = new Dictionary<string, KeyList>();
            public KeyList klAll = new KeyList();

            public FlexTreeClass2 parent;

            public void GetFallCount(ItemNodeTree item)
            {
                if(item.nodeType != NodeType.TestCase) return;
                var map = parent.caseFallMap;
                if(!map.ContainsKey(item.id)) return;  // 该用例不存在问题
                var traceType = parent.traceType;
                foreach(object fallid in map[item.id])
                {
                    DataRow dr = GridAssist.GetDataRow(parent.dtFall, "ID", fallid);
                    if(dr == null) continue;    // 冗余项
                    klAll.AddKey(fallid);
                    if(traceType == FallTraceType.FallLevel) AddMap(dr["问题级别"], fallid);
                    if(traceType == FallTraceType.FallType) AddMap(dr["问题类别"], fallid);
                    if(traceType == FallTraceType.Person)
                    {
                        foreach(string pid in KeyList.SplitKey(dr["报告人"], ","))
                            AddMap(pid, fallid);
                    }
                }
            }

            void AddMap(object name, object fallid)
            {
                string s = name.ToString();
                if(!fallMap.ContainsKey(s)) fallMap[s] = new KeyList();
                fallMap[s].AddKey(fallid);
            }
        }

        /// <summary>
        /// [用例ID，[问题ID 列表]]
        /// </summary>
        Dictionary<object, List<object>> caseFallMap = new Dictionary<object, List<object>>();

        DataTable dtFall;

        /// <summary>
        /// 初始化测试步骤表
        /// </summary>
        void InitTestStep(object vid)
        {
            string sql = @"SELECT se.测试用例ID, st.问题报告单ID
                FROM CA测试过程实体表 AS se INNER JOIN CA测试过程实测表 AS st ON se.ID = st.过程ID
                WHERE se.项目ID = ? AND st.测试版本 = ? AND 问题报告单ID is not null";

            DataTable dtStep = ExecuteDataTable(sql, pid, vid);
            foreach(DataRow dr in dtStep.Rows)
            {
                object cid = dr["测试用例ID"], fid = dr["问题报告单ID"];
                if(!caseFallMap.ContainsKey(cid)) caseFallMap[cid] = new List<object>();
                var list = caseFallMap[cid];
                if(!list.Contains(fid)) list.Add(fid);
            }
            dtFall = ExecuteDataTable("select ID, 问题类别, 问题级别, 报告人 from CA问题报告单 WHERE 项目ID = ?", pid);
        }

        C1FlexGrid flex;

        /// <summary>
        /// 统计级别，0 测试对象，1 测试分类， 2 测试项
        /// </summary>
        int summerLevel;

        FallTraceType traceType;

        /// <summary>
        /// 构造函数，条目树
        /// </summary>
        public FlexTreeClass2(C1FlexGrid flex, int summerLevel, FallTraceType traceType)
        {
            this.flex = flex;
            this.summerLevel = summerLevel;
            this.traceType = traceType;
            InitTestStep(currentvid);  // 仅当前版本
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

            var vc = new FallCountClass();
            vc.parent = this;
            item.DoVisit(vc.GetFallCount);

            for(int i = flex.Cols.Fixed; i < flex.Cols.Count; i++)
            {
                if(!Equals(flex.Cols[i].UserData, "统计")) continue;
                string name = flex.Cols[i].Name;
                // r[name] = 0;
                if(vc.fallMap.ContainsKey(name))
                    r[name] = vc.fallMap[name].Count;
            }
            r["NodeType"] = (int)item.nodeType;
            r["全部"] = vc.klAll.Count;
        }

        DataTable dtSummary;
        List<string> fallClassList = new List<string>();

        /// <summary>
        /// 构造函数，基于DataTable
        /// </summary>
        public FlexTreeClass2(int summerLevel, FallTraceType traceType, object vid)
        {
            dtSummary = new DataTable();
            dtSummary.Columns.Add("名称", typeof(string));
            dtSummary.Columns.Add("全部", typeof(int));
            dtSummary.Columns.Add("NodeType", typeof(int));
            dtSummary.Columns.Add("Level", typeof(int));

            fallClassList = InitDataTableColumn(traceType);
            foreach(string s in fallClassList)
                dtSummary.Columns.Add(s, typeof(string));

            this.summerLevel = summerLevel;
            this.traceType = traceType;
            InitTestStep(vid);
        }

        static List<string> InitDataTableColumn(FallTraceType traceType)
        {
            string sqlFallType = "select ID, 名称 from DC问题级别表 where 项目ID = ? and 类型 = ? order by 序号";
            string falltype = traceType == FallTraceType.FallType ? "类别" : "级别";
            DataTable dt = dbProject.ExecuteDataTable(sqlFallType, pid, falltype);
            List<string> list = new List<string>();
            foreach(DataRow dr in dt.Rows)
                list.Add(dr["ID"].ToString());
            return list;
        }

        public void AddDataTableNode(ItemNodeTree item)
        {
            if(item.nodeType == NodeType.TestCase) return;
            if(item.nodeType == NodeType.TestItem && summerLevel < 2) return;
            if(item.nodeType == NodeType.TestType && summerLevel < 1) return;

            DataRow r = dtSummary.Rows.Add();
            r["名称"] = item.name;
            r["Level"] = item.GetLevel();
            r["NodeType"] = (int)item.nodeType;

            var vc = new FallCountClass { parent = this };
            item.DoVisit(vc.GetFallCount);

            foreach(string name in fallClassList)
            {
                int count = vc.fallMap.ContainsKey(name) ? vc.fallMap[name].Count : 0;
                r[name] = count;
            }
            r["全部"] = vc.klAll.Count;
        }

        /// <summary>
        /// 获取问题报告单的分类统计信息
        /// </summary>
        /// <param name="summerLevel">1：统计到测试类型一级。2：统计到测试项一级。</param>
        /// <param name="traceType">按问题类别统计还是按问题级别统计</param>
        /// <param name="vid">查询的版本</param>
        public static DataTable GetFallSummary(int summerLevel, FallTraceType traceType, object vid)
        {
            TestResultSummary summary = new TestResultSummary(pid, vid);
            summary.OnCreate();

            FlexTreeClass2 ftc2 = new FlexTreeClass2(1, traceType, vid);
            summary.DoVisit(ftc2.AddDataTableNode);
            return ftc2.dtSummary;
        }
    }

    public enum FallTraceType
    {
        FallLevel = 0,

        FallType = 1,

        /// <summary>
        /// 按报告人统计。
        /// 可能会与总数不一致
        /// </summary>
        Person = 2
    }
}
