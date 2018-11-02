using System;
using System.Collections.Generic;
using System.Text;
using TPM3.Sys;
using Z1.tpm;
using System.Windows.Forms;
using System.Xml;
using Common;
using C1.Win.C1TrueDBGrid;
using System.Drawing;
using System.Data;
using Z1Utils.Controls;
using Common.TrueDBGrid;
using Z1.tpm.DB;
using C1.Win.C1FlexGrid;
using TPM3.wx;

namespace TPM3.zxd
{
    /*
     * TPM3.zxd.FrmCommonFunc
     * 项目中一些与窗体有关的功能方法
     * zhouxindong 2008/09/26
     */
    public class FrmCommonFunc
    {
        public delegate bool ProcBeforeDelete(); // 删除操作前调用的操作

        static FrmCommonFunc()
        {
            // "追踪关系"外挂输入
            columnList2 = GridAssist.GetColumnPropList<FrmCommonFunc>(3);
            columnList2.Add("测试依据", 200, "测试依据项的名称");
            columnList2.Add("测试依据说明", 200, "测试依据项出处及简要说明");

            // "优先级"外挂输入
            columnList1 = GridAssist.GetColumnPropList<FrmCommonFunc>(2);
            columnList1.Add("序号", 60);
            columnList1.Add("优先级", 100);
            columnList1.Add("说明", 250);
        }

        #region 窗体类型

        /// <summary>
        ///  获取窗体的PageType(四个不同测试阶段的窗体)
        /// </summary>
        /// <param name="frm"></param>
        /// <returns></returns>
        public static PageType GetFrmPageType(MyBaseForm frm)
        {
            //DocTreeNode dtn = frm.tnForm as DocTreeNode;
            //if (dtn == null)
            //    return PageType.None;

            //XmlElement ele = dtn.nodeElement;
            //string title1 = ele.GetAttribute("title1");
            //PageType ret = FunctionClass.GetEnumFromDescription<PageType>(title1);

            //return ret;

            // 改用url
            if(frm.paramList.ContainsKey("type"))
            {
                switch(frm.paramList["type"])
                {
                    case "require":
                        return PageType.TestRequirement;

                    case "plan":
                        return PageType.TestPlan;

                    case "design":
                        return PageType.TestCaseDesign;

                    case "result":
                        return PageType.TestCasePerform;
                }
            }

            return PageType.None;
        }

        /// <summary>
        /// 根据窗体的类型获取定制的起始序号(四个不同测试阶段)
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static int GetStartOrder(PageType pt)
        {
            int ret = 1;

            switch(pt)
            {
                case PageType.TestRequirement:  // 需求起始章节号
                    ret = MyProjectInfo.GetProjectContent<int>("需求起始章节号");
                    break;

                case PageType.TestPlan:  // 计划起始章节号
                    ret = MyProjectInfo.GetProjectContent<int>("计划起始章节号");
                    break;

                case PageType.TestCaseDesign:
                    ret = MyProjectInfo.GetProjectContent<int>("设计起始章节号");
                    break;

                case PageType.TestCasePerform:
                    ret = MyProjectInfo.GetProjectContent<int>("记录起始章节号");
                    break;
            }

            return ret;
        }

        /// <summary>
        /// 根据窗体类型判断是否显示测试用例节点
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static bool ShowUsecase(PageType pt)
        {
            if((pt == PageType.TestCaseDesign) ||
                (pt == PageType.TestCasePerform))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 基于左右侧窗格的形式, 由右侧内嵌窗体获取其外围窗体
        /// </summary>
        /// <param name="frm"></param>
        /// <returns></returns>
        public static LeftTreeUserControl GetParentFrm(MyBaseForm frm)
        {
            Control o = frm;
            while (!(o is LeftTreeUserControl))
            {
                o = o.Parent;
                if (o == null)
                    break;
            }

            if (o == null)
                return null;
            else
                return o as LeftTreeUserControl;
        }

        #endregion 窗体类型

        #region grid外观设置

        /// <summary>
        /// 设置grid外观样式
        /// </summary>
        /// <param name="grid"></param>
        public static void UniformGrid(C1TrueDBGrid grid)
        {
            grid.Styles["Heading"].ForeColor = System.Drawing.Color.Navy;
            grid.Styles["Heading"].BackColor = System.Drawing.Color.LightSteelBlue;
            //grid.Splits[0].RecordSelectors = false;

            grid.Styles["Caption"].BackColor = System.Drawing.Color.LightGray;
            grid.Styles["Caption"].HorizontalAlignment = AlignHorzEnum.Near;
            /*grid.Styles["Caption"].Font = new Font(grid.Styles["Caption"].Font.Name,
                (float)(grid.Styles["Caption"].Font.Size * 1.2));*/

            grid.CaptionHeight = 30;

            //grid.ExtendRightColumn = true;

            grid.Splits[0].ColumnCaptionHeight = 36;
            grid.RowHeight = 24;
        }

        // 指定行高
        public static void UniformGrid(C1TrueDBGrid grid, int rh)
        {
            UniformGrid(grid);
            grid.RowHeight = rh;
        }

        public static void UniformGrid(C1TrueDBGrid grid, int rh, Color capbc)
        {
            UniformGrid(grid, rh);
            grid.Styles["Caption"].BackColor = capbc;
        }

        public static void UniformGrid(C1TrueDBGrid grid, Color fc, Color bc, int colheight, int rowheight)
        {
            grid.Styles["Heading"].ForeColor = fc;
            grid.Styles["Heading"].BackColor = bc;

            grid.Splits[0].ColumnCaptionHeight = colheight;
            grid.RowHeight = rowheight;
        }

        #endregion grid外观设置

        #region 树节点操作

        // 获取树节点的ID附加数据
        public static object GetTreeNodeKey(TreeNode node)
        {
            NodeTagInfo taginfo = node.Tag as NodeTagInfo;
            if(taginfo == null)
                return null;

            return taginfo.id;
        }

        // 列移动前(返回false取消移动)
        public static bool BeforeRowMove(C1TrueDBGrid grid, DataRow drCur, DataRow drPre)
        {
            if(grid.AddNewMode != AddNewModeEnum.NoAddNew)
            {
                MessageBox.Show("新记录提交之前不能移动!", "操作提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return false;
            }
            else
                return true;
        }

        // 列移动后
        public static bool AfterRowMove(TreeNode parent, NodeType ntReq, DataRow drCur, DataRow drPre)
        {
            //交换被测对象树节点的order以及更新其章节号
            foreach(TreeNode ndpre in parent.Nodes)
            {
                NodeTagInfo taginfo = ndpre.Tag as NodeTagInfo;
                if(taginfo == null)
                    continue;

                if((taginfo.id.Equals((string)drPre["ID"])) &
                    (taginfo.nodeType == ntReq))
                {
                    MoveNode(drCur, drPre, ndpre);
                    break;
                }
            }
            return true;
        }

        public static bool AfterRowMove(TreeNode parent, DataRow drCur, DataRow drPre)
        {
            //交换被测对象树节点的order以及更新其章节号
            foreach(TreeNode ndpre in parent.Nodes)
            {
                NodeTagInfo taginfo = ndpre.Tag as NodeTagInfo;
                if(taginfo == null)
                    continue;

                if(taginfo.id.Equals((string)drPre["ID"]))
                {
                    MoveNode(drCur, drPre, ndpre);
                    break;
                }
            }
            return true;
        }

        // 在TrueDBGrid中移动记录后更新树节点文本及其所有子节点的文本
        // 此时DataRow已交换, 树节点也已交换
        public static void MoveNode(DataRow drCur, DataRow drPre, TreeNode ndpre)
        {
            NodeTagInfo tagpre = ndpre.Tag as NodeTagInfo;
            if(tagpre == null)
                return;

            int preorder = tagpre.order;

            TreeNode ndcur = new TreeNode();

            if((int)drCur["序号"] < (int)drPre["序号"])    //上移(序号已交换)
                ndcur = ndpre.PrevNode;     // 树节点已交换
            else
                ndcur = ndpre.NextNode;

            NodeTagInfo tagcur = ndcur.Tag as NodeTagInfo;
            if(tagcur == null)
                return;

            // 交换两个树节点附加信息中的序号值
            tagpre.order = tagcur.order;
            tagcur.order = preorder;

            // 更新树节点文本
            ndpre.Text = UIFunc.GenSections(ndpre.Parent, tagpre.order, ConstDef.SectionSep) + tagpre.text;
            ndcur.Text = UIFunc.GenSections(ndcur.Parent, tagcur.order, ConstDef.SectionSep) + tagcur.text;

            //修改ndpre下所有子节点文本
            UpdateSubNodeAfterMove(ndpre);

            //修改ndcur下所有子节点文本
            UpdateSubNodeAfterMove(ndcur);
        }

        protected static void UpdateSubNodeAfterMove(TreeNode parent)
        {
            foreach(TreeNode node in parent.Nodes)
            {
                NodeTagInfo taginfo = node.Tag as NodeTagInfo;
                if(taginfo == null)
                    continue;

                node.Text = UIFunc.GenSections(parent, taginfo.order, ConstDef.SectionSep) + taginfo.text;

                UpdateSubNodeAfterMove(node);
            }
        }

        // 由实测ID获取对应的树节点(Tag为NodeTagInfo)
        private static string _tid;
        private static TreeNode _node;
        public static TreeNode GetTreeNode(TreeNode parent, string tid)
        {
            _node = null;
            _tid = tid;

            TreeViewUtils tvu = new TreeViewUtils();
            EnumTreeViewProc proc = new EnumTreeViewProc(SearchNode);
            tvu.FindNodeFromNode(parent, proc);

            return _node;
        }

        private static bool SearchNode(TreeNode node)
        {
            NodeTagInfo taginfo = node.Tag as NodeTagInfo;
            if(taginfo == null)
                return false; // 停止搜索树

            if(taginfo.id.Equals(_tid) &&
                (!taginfo.IsShortcut))
            {
                _node = node;
                return false;
            }

            return true;
        }

        private static bool _shortcut;
        public static TreeNode GetUCTreeNode(TreeNode parent, string tid, bool shortcut)
        {
            _node = null;
            _tid = tid;
            _shortcut = shortcut;

            TreeViewUtils tvu = new TreeViewUtils();
            EnumTreeViewProc proc = new EnumTreeViewProc(SearchUCNode);
            tvu.FindNodeFromNode(parent, proc);

            return _node;
        }

        private static bool SearchUCNode(TreeNode node)
        {
            NodeTagInfo taginfo = node.Tag as NodeTagInfo;
            if (taginfo == null)
                return false;

            if(taginfo.id.Equals(_tid) &&
                (taginfo.IsShortcut.Equals(_shortcut)))
            {
                _node = node;
                return false;
            }

            return true;
        }

        public static TreeNode GetTreeNode(TreeView tree, string tid)
        {
            _node = null;
            _tid = tid;

            TreeViewUtils tvu = new TreeViewUtils();
            EnumTreeViewProc proc = new EnumTreeViewProc(SearchNode);
            tvu.FindTreeViewNode(tree, proc);

            return _node;
        }

        // 搜索树找出Tag与id相同的节点(Tag为string)
        private static TreeNode _node1;
        private static string _id1;
        public static TreeNode GetTreeNode1(TreeView tree, string id)
        {
            _node1 = null;
            _id1 = id;

            TreeViewUtils tvu = new TreeViewUtils();
            EnumTreeViewProc proc = new EnumTreeViewProc(SearchNode1);
            tvu.FindTreeViewLeaf(tree, proc);

            return _node1;
        }

        private static bool SearchNode1(TreeNode node)
        {
            string id = node.Tag as string;
            if((id == null) ||
                id.Equals(string.Empty))
                return true;

            if(id.Equals(_id1))
            {
                _node1 = node;
                return false;
            }
            else
                return true;
        }

        // 定位待插入的新测试项节点的位置(返回插入的order位置)
        public static int LocateInsertItem(TreeNode parent)
        {
            int order = 1;

            foreach(TreeNode node in parent.Nodes)
            {
                NodeTagInfo tag = node.Tag as NodeTagInfo;
                if(tag == null)
                    continue;

                if(tag.nodeType == NodeType.TestItem)
                    order++;

                // 所有同级的测试用例序号后移一位
                if(tag.nodeType == NodeType.TestCase)
                {
                    tag.order += 1;
                    node.Text = UIFunc.GenSections(parent, tag.order, '.') + tag.text;
                }
            }

            return order;
        }

        // 获得此树节点所属被测对象的ID(tid)
        public static string GetBelongObjID(TreeNode node, out string objname, out string objabbr)
        {
            NodeTagInfo tag = node.Tag as NodeTagInfo;
            TreeNode cur = node;

            while((tag != null) && (tag.nodeType != NodeType.TestObject))
            {
                cur = cur.Parent;
                if(cur != null)
                    tag = cur.Tag as NodeTagInfo;
                else
                    tag = null;
            }

            if(tag == null)
            {
                objname = string.Empty;
                objabbr = string.Empty;
                return string.Empty;
            }
            else
            {
                objname = tag.text;
                objabbr = tag.keySign;
                return tag.id;
            }
        }

        public static string GetBelongObjID(TreeNode node)
        {
            string objname;
            string objabbr;

            return GetBelongObjID(node, out objname, out objabbr);
        }

        /// <summary>
        /// 获取父节点的附加信息: 实测ID
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string GetParentID(TreeNode node)
        {
            TreeNode parent = node.Parent;
            if (parent == null)
                return string.Empty;
            else
            {
                NodeTagInfo tag = parent.Tag as NodeTagInfo;
                if (tag == null)
                    return string.Empty;

                return tag.id;
            }
        }

        #endregion 树节点操作

        #region 定制输入编辑器

        // 定制"测试类型名称"输入编辑器
        public static void TestTypeEditor(TrueDBGridAssist gridAssist1,
            Common.VoidFunc<ColumnRefMap, object> afterRowSelectEvent)
        {
            // 测试类型外挂输入
            string sqlClass = "SELECT * FROM DC测试类型模板表 ORDER BY 序号";

            RefrenceColumnMapBase mapList = gridAssist1.refrenceColumnMapList;
            ColumnRefMap cm = mapList.AddColumnMap("测试类型名称", sqlClass, "测试能力ID", "测试能力名称");
            cm.flexParentCol = "父节点ID";
            cm._rootParentID = "0";
            cm.convertKey = false;
            cm.AddParams(GlobalData.globalData.projectID);
            cm.allowUserInput = true; // false可禁止用户输入, 但双击时不再创建新记录
            cm.columnList = TestClassTemplateList.columnList2;

            mapList.afterRowSelectEvent += afterRowSelectEvent;
        }

        /// <summary>
        /// 用户选择 测试类型名称 完毕,更新简写码
        /// </summary>
        public static void AfterChangeTestType(DataTable dt, C1TrueDBGrid grid, TreeNode parent, object key)
        {
            if(dt == null)
                return;

            DataRow dr = GridAssist.GetDataRow(dt, "测试能力ID", key);
            if(dr == null)
                return;

            grid.Columns["简写码"].Value = dr["简写码"];
            grid.Columns["测试类型名称"].Value = dr["测试能力名称"];

            // 相应树节点文本及附加信息的更新
            TreeNode node = FrmCommonFunc.GetTreeNode(parent, (string)grid.Columns["ID"].Value);
            if(node != null)
            {
                NodeTagInfo tag = node.Tag as NodeTagInfo;
                if(tag != null)
                {
                    tag.text = (string)grid.Columns["测试类型名称"].Value;
                    tag.keySign = (string)grid.Columns["简写码"].Value;

                    node.Text = UIFunc.GenSections(parent, tag.order, ConstDef.SectionSep) +
                        tag.text;
                }
            }
        }

        // 定制"优先级"输入编辑器
        private static ColumnPropList columnList1;
        public static void PriorLevelEditor(TrueDBGridAssist gridAssist1)
        {
            RefrenceColumnMapBase mapList = gridAssist1.refrenceColumnMapList;
            ColumnRefMap cm = mapList.AddColumnMap("优先级", GetPriorLevel, "ID", "优先级");

            cm.columnList = columnList1;
        }

        private static DataTable GetPriorLevel()
        {
            return CommonDB.GetPriorLevel(MyBaseForm.dbProject, (string)MyBaseForm.pid);
        }

        // 将DropDownTextBox挂接"优先级"输入
        public static void PriorTextBox(DropDownTextBox tb)
        {
            ColumnRefMap cm2 = tb.cm;
            cm2.getDataSourceEvent = GetPriorLevel;
            cm2.multiSelect = false;
            cm2.flexKey = "ID";
            cm2.flexDisplay = "优先级";
            cm2.columnList = columnList1;
        }

        #region 定制"追踪关系"输入编辑器

        private static ColumnPropList columnList2;
        public static void TraceEditor(TrueDBGridAssist gridAssist1, object vid)
        {
            RefrenceColumnMapBase mapList = gridAssist1.refrenceColumnMapList;

            ColumnRefMap cm = mapList.AddColumnMap("追踪关系", "ID", "测试依据");

            RequireTreeForm.InitColumnRefMap(cm, vid);
        }

        public static void TraceTextBox(DropDownTextBox tb, object vid)
        {
            RequireTreeForm.InitColumnRefMap(tb.cm, vid);
        }

        //static Font font1, font2;
        //static void OnCreateFormReadyEventHandler(Form f)
        //{
        //    MultiColumnDropDown f2 = f as MultiColumnDropDown;
        //    C1FlexGrid flex = f2.flex1;
        //    flex.OwnerDrawCell += dropDownflex_OwnerDrawCell;

        //    if(font1 == null)
        //    {
        //        font1 = flex.Styles.Normal.Font;
        //        font2 = new Font(font1, FontStyle.Bold);
        //    }
        //}

        //static void dropDownflex_OwnerDrawCell(object sender, C1.Win.C1FlexGrid.OwnerDrawCellEventArgs e)
        //{
        //    C1FlexGrid flex = sender as C1FlexGrid;
        //    if(e.Row < flex.Rows.Fixed || e.Col < flex.Cols.Fixed) return;
        //    Row row = flex.Rows[e.Row];
        //    e.Style.Font = font1;
        //    if(row.Node.Level == 0)
        //    {   // 用黑体字显示
        //        e.Style.Font = font2;
        //    }
        //    if(flex.Cols["测试依据"].Index == e.Col && row.Node.Level == 0)
        //        e.Image = ImageForm.treeNodeImage.Images["project"];
        //}

        #endregion 定制"追踪关系"输入编辑器

        // "设计/测试人员"输入
        public static void PersonEditor(ColumnRefMap cm)
        {
            cm.DataSource = DBLayer1.GetPersonList(MyBaseForm.dbProject, MyBaseForm.pid,
                MyBaseForm.currentvid);
            cm.flexKey = "ID";
            cm.flexDisplay = "姓名";
            cm.columnList = wx.PersonForm.columnList2;
        }

        // "设计方法"输入
        public static void DesignMethodEditor(ColumnRefMap cm)
        {
            cm.DataSource = DBLayer1.GetDesignMethodList(MyBaseForm.dbProject, MyBaseForm.pid);
            cm.flexKey = "ID";
            cm.flexDisplay = "测试用例设计方法";
            cm.columnList = wx.TestMethodForm.columnList1;
        }

        #endregion 定制输入编辑器

        #region Grid

        /// <summary>
        /// 用户修改了Grid的某列后的一些操作
        /// 更新附加信息类的相关字段
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="id">ID的列名</param>
        /// <param name="coltext">用于名称的列名(null或空时表示不对此列操作)</param>
        /// <param name="colabbr">用于简写码的列名(null或空时表示不对此列操作</param>
        /// <param name="parent">所在树的父节点</param>
        public static void GridAfterColUpdate(C1TrueDBGrid grid, string id, string coltext, string colabbr, TreeNode parent)
        {
            //如果添加新纪录则退出本过程
            if(grid.AddNewMode != AddNewModeEnum.NoAddNew)
                return;

            string tid = (string)grid.Columns[id].Value;
            TreeNode node = FrmCommonFunc.GetTreeNode(parent, tid);
            if(node == null)
                return;

            NodeTagInfo taginfo = node.Tag as NodeTagInfo;
            if(taginfo == null)
                return;

            if((coltext != null) && (!coltext.Equals(string.Empty)))
            {
                taginfo.text = (string)grid.Columns[coltext].Value;
                node.Text = UIFunc.GenSections(parent, taginfo.order, ConstDef.SectionSep) + taginfo.text;
            }

            if((colabbr != null) && (!colabbr.Equals(string.Empty)))
            {
                taginfo.keySign = (string)grid.Columns[colabbr].Value;
            }
        }

        // 挂接测试类型"子节点类型"显示值与数值
        public static void TransSubnodeType(ValueItems items)
        {
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox;
            for(int i = 0; i < ConstDef.subtype.Length; i++)
            {
                ValueItem item = new ValueItem();
                item.DisplayValue = ConstDef.subtype[i];
                item.Value = (i + 1).ToString();
                items.Values.Add(item);
            }
            items.Translate = true;
        }

        // 挂接被测对象的"测试级别"
        public static void TransTestLevel(ValueItems items)
        {
            items.Presentation = C1.Win.C1TrueDBGrid.PresentationEnum.ComboBox;
            foreach(string s in ConstDef.testlevel)
            {
                ValueItem item = new ValueItem();
                item.Value = s;
                items.Values.Add(item);
            }
        }

        // 检测pt是否位于grid的标题条区域(区域的高度由head决定)
        public static bool InGridHead(C1TrueDBGrid grid, int head, Point pt)
        {
            Rectangle r = grid.Bounds;

            r = new Rectangle(r.X, r.Y, r.Width, head);

            return r.Contains(pt);
        }

        #endregion Grid

        #region 被测对象

        // 添加新记录
        public static void OnAddNew_TestObj(C1TrueDBGrid grid)
        {
            object id = FunctionClass.NewGuid;
            object entityid = FunctionClass.NewGuid;

            grid.Columns["ID"].Value = id;
            grid.Columns["实体ID"].Value = entityid;
            grid.Columns["被测对象ID"].Value = entityid;
            grid.Columns["项目ID"].Value = MyBaseForm.pid;
            grid.Columns["测试版本"].Value = MyBaseForm.currentvid;

            grid.Columns["测试级别"].Value = ConstDef.testlevel[0];
            grid.Columns["被测对象版本"].Value = "待定";
            grid.Columns["创建版本ID"].Value = MyBaseForm.currentvid;
        }

        /// <summary>
        /// 由当前节点得到所属被测对象节点的附加信息类对象
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static NodeTagInfo GetObjTagInfo(TreeNode node)
        {
            if(node == null)
                return null;

            TreeNode parentnode = node.Parent;
            while(parentnode != null)
            {
                NodeTagInfo info = parentnode.Tag as NodeTagInfo;
                if(info == null)
                    return null;

                if(info.nodeType == NodeType.TestObject)
                    return info;
                else
                    parentnode = parentnode.Parent;
            }

            return null;
        }

        #endregion 被测对象

        #region 测试类型

        // 添加新记录
        public static void OnAddNew_TestType(C1TrueDBGrid grid)
        {
            object id = FunctionClass.NewGuid;
            object entityid = FunctionClass.NewGuid;

            grid.Columns["ID"].Value = id;
            grid.Columns["实体ID"].Value = entityid;
            grid.Columns["测试类型ID"].Value = entityid;
            grid.Columns["项目ID"].Value = MyBaseForm.pid;
            grid.Columns["测试版本"].Value = MyBaseForm.currentvid;
            grid.Columns["创建版本ID"].Value = MyBaseForm.currentvid;
        }

        // 设置列禁止用户输入
        public static bool OnKeyPress_TestType(C1TrueDBGrid grid, List<string> filter)
        {
            int col = grid.Col;
            string colname = grid.Splits[0].DisplayColumns[col].Name;
            if(filter.Contains(colname))
                return true;
            else
                return false;
        }

        // 删除记录前
        public static bool OnBeforeDelete_TestType(C1TrueDBGrid grid, ProcBeforeDelete proc, ref string eid, ref string tid)
        {
            if(MessageBox.Show("确实要删除此测试类型吗?\n(此测试类型下所有节点将一并删除!)",
                "删除测试类型", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return true;

            bool ret = proc();
            if(!ret)    // 先将grid更新到数据库
                return true;

            eid = (string)grid.Columns["测试类型ID"].Value;
            tid = (string)grid.Columns["ID"].Value;

            return false;
        }

        // 删除记录后
        public static void OnAfterDelete_TestType(C1TrueDBGrid grid, TreeNode parent, string tid, string eid, DataTable tbl)
        {
            //更新数据库,并删除其下的所有子节点
            TestType.DeleteType(MyBaseForm.dbProject, (string)MyBaseForm.pid, (string)MyBaseForm.currentvid, eid);

            //删除对应树节点
            TreeNode node = FrmCommonFunc.GetTreeNode(parent, tid);
            UIFunc.DeleteTreeNode(node);

            tbl.AcceptChanges();
        }

        // 添加新记录后
        public static void OnAfterInsert_TestType(DataTable tbl, TreeNode parent)
        {
            // 向树中添加新节点
            TreeNode node = new TreeNode();

            NodeTagInfo taginfo = new NodeTagInfo();
            int index = tbl.Rows.Count - 1;

            taginfo.id = (string)tbl.Rows[index]["ID"];
            taginfo.nodeType = NodeType.TestType;
            taginfo.keySign = (string)tbl.Rows[index]["简写码"];
            taginfo.order = (int)tbl.Rows[index]["序号"];
            taginfo.text = (string)tbl.Rows[index]["测试类型名称"];
            taginfo.verId = (string)MyBaseForm.currentvid;

            node.Text = UIFunc.GenSections(parent, taginfo.order, ConstDef.SectionSep) +
                taginfo.text;
            node.Tag = taginfo;
            UIFunc.SetNodeImageKey(node, NodeType.TestType);

            parent.Nodes.Add(node);

            if(!parent.IsExpanded)
                parent.Expand();
        }

        // 检测是否允许修改"测试类型"的"子节点类型"
        public static void BeforeChangeSubtype(C1TrueDBGrid grid, TreeNode parent)
        {
            // 只有在测试类型无子节点时才允许修改"子节点类型"
            if(grid.Columns["子节点类型"].DataChanged)
            {
                string tid = (string)grid.Columns["ID"].Value;
                if((tid == null) ||
                    (string.Empty.Equals(tid)))
                {
                    grid.Columns["子节点类型"].DataChanged = false;
                    return;
                }

                TreeNode node = FrmCommonFunc.GetTreeNode(parent, tid);
                if(node == null)
                {
                    grid.Columns["子节点类型"].DataChanged = false;
                    return;
                }

                if(node.Nodes.Count != 0)
                {
                    MessageBox.Show("子节点类型修改前, 请先删除其下原有纪录!", "操作无效", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    grid.Columns["子节点类型"].DataChanged = false;
                }
            }
        }

        #endregion 测试类型

        #region 测试项

        // 删除记录前
        public static bool OnBeforeDelete_TestItem(C1TrueDBGrid grid, ProcBeforeDelete proc, ref string eid, ref string tid, bool showconfirm)
        {
            if (showconfirm)
            {
                if (MessageBox.Show("确实要删除此测试项吗?\n(此测试项下所有节点将一并删除!)",
                    "删除测试项", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return true;
            }

            bool ret;
            if (proc != null)
            {
                ret = proc();
                if (!ret)
                    return true;
            }

            eid = (string)grid.Columns["测试项ID"].Value;
            tid = (string)grid.Columns["ID"].Value;

            return false;
        }

        // 删除记录后
        public static void OnAfterDelete_TestItem(C1TrueDBGrid grid, TreeNode parent, string tid, string eid, DataTable tbl)
        {
            //更新数据库,并删除其下的所有子节点
            TestItem.DeleteItem(MyBaseForm.dbProject, (string)MyBaseForm.pid, (string)MyBaseForm.currentvid, eid);

            //删除对应树节点
            TreeNode node = FrmCommonFunc.GetTreeNode(parent, tid);
            UIFunc.DeleteTreeNode(node);

            tbl.AcceptChanges();
        }

        public static void OnAfterInsert_TestItem(DataTable tbl, TreeNode parent)
        {
            // 向树中添加新节点
            TreeNode node = new TreeNode();

            NodeTagInfo taginfo = new NodeTagInfo();
            int index = tbl.Rows.Count - 1;

            taginfo.id = (string)tbl.Rows[index]["ID"];
            taginfo.nodeType = NodeType.TestItem;
            taginfo.keySign = (string)tbl.Rows[index]["简写码"];
            taginfo.order = (int)tbl.Rows[index]["序号"];
            taginfo.text = (string)tbl.Rows[index]["测试项名称"];
            taginfo.verId = (string)MyBaseForm.currentvid;

            node.Text = UIFunc.GenSections(parent, taginfo.order, ConstDef.SectionSep) +
                taginfo.text;
            node.Tag = taginfo;
            UIFunc.SetNodeImageKey(node, NodeType.TestItem);

            parent.Nodes.Add(node);

            if(!parent.IsExpanded)
                parent.Expand();
        }

        // 粘贴测试项后其后的测试用例序号加1
        public static void ReorderUCAfterPasteItem(TreeNode parent)
        {
            foreach(TreeNode node in parent.Nodes)
            {
                NodeTagInfo tag = node.Tag as NodeTagInfo;
                if(tag == null)
                    continue;
                if(tag.nodeType == NodeType.TestCase)
                {
                    tag.order = tag.order + 1;
                    node.Text = UIFunc.GenSections(parent, tag.order, ConstDef.SectionSep) + tag.text;
                }
            }
        }

        #endregion 测试项

        #region 测试用例

        // 创建显示列"测试用例类型", 并赋值
        public static void GenUCTypeCol(DataTable tbl)
        {
            tbl.Columns.Add("测试用例类型", typeof(byte));

            // 列"测试用例类型"赋值
            foreach(DataRow row in tbl.Rows)
            {
                string tid = (string)row["ID"];

                if(true.Equals(row["直接所属标志"]))
                {
                    switch((string)row["执行状态"])
                    {
                        case ConstDef.execsta0:
                            row["测试用例类型"] = (byte)ExecuteStatus.NonExecute;
                            break;

                        case ConstDef.execsta1:
                            if(TestUsecase.UCHasProblem(MyBaseForm.dbProject,
                                tid, (string)MyBaseForm.pid, (string)MyBaseForm.currentvid))
                                row["测试用例类型"] = (byte)ExecuteStatus.PartExecutep;
                            else
                                row["测试用例类型"] = (byte)ExecuteStatus.PartExecute;
                            break;

                        case ConstDef.execsta2:
                            if(TestUsecase.UCHasProblem(MyBaseForm.dbProject,
                                tid, (string)MyBaseForm.pid, (string)MyBaseForm.currentvid))
                                row["测试用例类型"] = (byte)ExecuteStatus.Executedp;
                            else
                                row["测试用例类型"] = (byte)ExecuteStatus.Executed;
                            break;

                        default:
                            row["测试用例类型"] = (byte)ExecuteStatus.NonExecute;
                            break;
                    }
                }
                else
                {
                    switch((string)row["执行状态"])
                    {
                        case ConstDef.execsta0:
                            row["测试用例类型"] = (byte)ExecuteStatus.NonExecute_k;
                            break;

                        case ConstDef.execsta1:
                            if(TestUsecase.UCHasProblem(MyBaseForm.dbProject, tid,
                                (string)MyBaseForm.pid, (string)MyBaseForm.currentvid))
                                row["测试用例类型"] = (byte)ExecuteStatus.PartExecutep_k;
                            else
                                row["测试用例类型"] = (byte)ExecuteStatus.PartExecute_k;
                            break;

                        case ConstDef.execsta2:
                            if(TestUsecase.UCHasProblem(MyBaseForm.dbProject, tid,
                                (string)MyBaseForm.pid, (string)MyBaseForm.currentvid))
                                row["测试用例类型"] = (byte)ExecuteStatus.Executedp_k;
                            else
                                row["测试用例类型"] = (byte)ExecuteStatus.Executed_k;
                            break;

                        default:
                            row["测试用例类型"] = (byte)ExecuteStatus.NonExecute_k;
                            break;
                    }
                }
            }
        }

        // 由测试用例获取其测试用例类型信息(TrueDBGrid列中的图标显示)
        public static byte GetUCType(TestUC ucinfo, bool shortcut)
        {
            if(!shortcut)
            {
                switch(ucinfo.execsta)
                {
                    case ConstDef.execsta0:
                        return (byte)ExecuteStatus.NonExecute;

                    case ConstDef.execsta1:
                        if(ConstDef.execrlt2.Equals(ucinfo.execrlt))
                            return (byte)ExecuteStatus.PartExecutep;
                        else
                            return (byte)ExecuteStatus.PartExecute;

                    case ConstDef.execsta2:
                        if(ConstDef.execrlt2.Equals(ucinfo.execrlt))
                            return (byte)ExecuteStatus.Executedp;
                        else
                            return (byte)ExecuteStatus.Executed;

                    default:
                        return (byte)ExecuteStatus.NonExecute;
                }
            }
            else
            {
                switch(ucinfo.execsta)
                {
                    case ConstDef.execsta0:
                        return (byte)ExecuteStatus.NonExecute_k;

                    case ConstDef.execsta1:
                        if(ConstDef.execrlt2.Equals(ucinfo.execrlt))
                            return (byte)ExecuteStatus.PartExecutep_k;
                        else
                            return (byte)ExecuteStatus.PartExecute_k;

                    case ConstDef.execsta2:
                        if(ConstDef.execrlt2.Equals(ucinfo.execrlt))
                            return (byte)ExecuteStatus.Executedp_k;
                        else
                            return (byte)ExecuteStatus.Executed_k;

                    default:
                        return (byte)ExecuteStatus.NonExecute_k;
                }
            }
        }

        // 根据用例不同执行状态, 显示相应图标(数据显示方式转换)
        public static void TransUCType(ValueItems items, ImageList imageList1)
        {
            foreach(string s in Enum.GetNames(typeof(ExecuteStatus)))
            {
                ValueItem item = new ValueItem();
                item.DisplayValue = imageList1.Images[s];
                item.Value = ((byte)((ExecuteStatus)Enum.Parse(typeof(ExecuteStatus), s, false))).ToString();
                items.Values.Add(item);
            }

            items.Translate = true;
        }

        // 删除记录前
        public static bool OnBeforeDelete_TestUC(C1TrueDBGrid grid, ProcBeforeDelete proc, ref string itemtid, ref string uctid)
        {
            if (proc != null)
            {
                bool ret = proc();
                if (!ret)
                    return true;
            }

            itemtid = (string)grid.Columns["测试项ID"].Value;
            uctid = (string)grid.Columns["ID"].Value;

            return false;
        }

        // 删除记录后
        public static void OnAfterDelete_TestUC(bool shortcut, TreeNode parent, string itemtid, string uctid, DataTable tbl)
        {
            //更新数据库,并删除其下的所有子节点
            TestUsecase.g_TestTree = parent.TreeView;
            TestUsecase.DeleteUsecase(MyBaseForm.dbProject, (string)MyBaseForm.pid, (string)MyBaseForm.currentvid,
                    itemtid, uctid, !shortcut);

            //删除对应树节点
            //TreeNode node = FrmCommonFunc.GetTreeNode(parent, uctid);
            //if(node != null)
            //    UIFunc.DeleteTreeNode(node);
            TreeNode node = FrmCommonFunc.GetUCTreeNode(parent, uctid, shortcut);
            UIFunc.DeleteTreeNode(node);

            tbl.AcceptChanges();
        }

        public static void OnAfterInsert_TestUC(DataTable tbl, TreeNode parent)
        {
            // 向树中添加新节点
            TreeNode node = new TreeNode();

            NodeTagInfo taginfo = new NodeTagInfo();
            int index = tbl.Rows.Count - 1;

            taginfo.id = (string)tbl.Rows[index]["ID"];
            taginfo.nodeType = NodeType.TestCase;
            taginfo.keySign = ((int)tbl.Rows[index]["序号"]).ToString(); // 用于标识符

            if(Equals(null, parent.LastNode))  //新添加记录是唯一节点
                taginfo.order = 1;
            else
                taginfo.order = (parent.LastNode.Tag as NodeTagInfo).order + 1;

            taginfo.text = (string)tbl.Rows[index]["测试用例名称"];
            taginfo.verId = (string)MyBaseForm.currentvid;

            node.Text = UIFunc.GenSections(parent, taginfo.order, ConstDef.SectionSep) +
                taginfo.text;
            node.Tag = taginfo;
            UIFunc.SetNodeImageKey(node, NodeType.TestCase);

            parent.Nodes.Add(node);

            if(!parent.IsExpanded)
                parent.Expand();
        }

        private static string _uctid;               // 欲更新快捷方式的用例实测ID
        private static ExecuteStatus _newstatus;   // 当前执行状态(图标的ImageKey)
        /// <summary>
        /// 同步更新某用例的所有快捷方式
        /// </summary>
        /// <param name="uctid"></param>
        /// <param name="newstatus"></param>
        /// <param name="tree"></param>
        /// <param name="tbl"></param>
        public static void UpdateShortcutIcons(string uctid, ExecuteStatus newstatus, TreeView tree)
        {
            _uctid = uctid;
            _newstatus = newstatus;

            TreeViewUtils tvu = new TreeViewUtils();
            EnumTreeViewProc proc = new EnumTreeViewProc(DoUpdateShortcutIcons);
            tvu.FindTreeViewLeaf(tree, proc);
        }

        private static bool DoUpdateShortcutIcons(TreeNode node)
        {
            NodeTagInfo taginfo = node.Tag as NodeTagInfo;
            if((taginfo != null) && (taginfo.nodeType == NodeType.TestCase))
            {
                if(taginfo.id.Equals(_uctid))
                {
                    TreeNode parent = node.Parent;
                    if(parent != null)
                    {
                        NodeTagInfo parinfo = parent.Tag as NodeTagInfo;
                        if((parinfo != null) && (parinfo.nodeType == NodeType.TestItem))
                        {
                            if(TestUsecase.IsShortcut(MyBaseForm.dbProject,
                                parinfo.id, _uctid)) // 用例的快捷方式
                            {
                                UIFunc.SetNodeImageKey(node, MainTestFrmCommon.ExecStatusImageKeys[_newstatus],
                                    MainTestFrmCommon.ExecStatusImageKeys[_newstatus]);
                            }
                        }
                    }
                }
            }

            return true;
        }

        // 插入测试用例树节点
        public static void AddUCNode(TestUC ucinfo, TreeNode parent, int index, bool shortcut)
        {
            // 向树中添加新节点
            TreeNode node = new TreeNode();

            NodeTagInfo taginfo = new NodeTagInfo();

            taginfo.id = ucinfo.id;
            taginfo.nodeType = NodeType.TestCase;
            taginfo.keySign = index.ToString(); // 用于标识符

            if(Equals(null, parent.LastNode))  //新添加记录是唯一节点
                taginfo.order = 1;
            else
                taginfo.order = (parent.LastNode.Tag as NodeTagInfo).order + 1;

            taginfo.text = ucinfo.name;
            taginfo.verId = (string)MyBaseForm.currentvid;
            taginfo.IsShortcut = shortcut;

            node.Text = UIFunc.GenSections(parent, taginfo.order, ConstDef.SectionSep) +
                taginfo.text;
            node.Tag = taginfo;

            UIFunc.SetUCNodeImageKey(node, ucinfo, shortcut);

            parent.Nodes.Add(node);
            if(!parent.IsExpanded)
                parent.Expand();
        }

        private static string _updtid;
        private static string _newtext;
        // 更新测试用例的文本(包括所有快捷方式的树节点文本和附加信息)
        public static void UpdateUCName(TreeView tree, string tid, string newtext)
        {
            _updtid = tid;
            _newtext = newtext;
            TreeViewUtils tvu = new TreeViewUtils();
            Z1Utils.Controls.EnumTreeViewProc proc = new EnumTreeViewProc(UpdateIt);
            tvu.FindTreeViewLeaf(tree, proc);
        }

        private static bool UpdateIt(TreeNode tn)
        {
            NodeTagInfo tag = tn.Tag as NodeTagInfo;
            if(tag == null)
                return true;

            if(_updtid.Equals(tag.id))
            {
                tag.text = _newtext;
                tn.Text = UIFunc.GenSections(tn.Parent, tag.order, ConstDef.SectionSep) + tag.text;
            }

            return true;
        }
        #endregion 测试用例

        #region 问题报告单

        // 以Tag(ID)置于选中(问题类别/问题级别按钮组选中某一)
        public static void SelectOneInRadios(string seltagid, params RadioButton[] radios)
        {
            foreach(RadioButton rb in radios)
            {
                if(seltagid.Equals((string)rb.Tag))
                {
                    //rb.Select();
                    rb.Checked = true;
                    rb.PerformClick(); // 触发相应事件
                    return;
                }
            }
        }

        private static TreeView _tree;
        // 初始化grid use HypeLink(与问题报告单相关联的测试用例列表)
        public static void InitHyperLinkGrid(C1TrueDBGrid grid, PblRep pr, TreeView tree, string vid)
        {
            List<string> li = CommonDB.GetUCsSubmitPbl(MyBaseForm.dbProject,
                pr.id, (string)MyBaseForm.pid, vid);

            DataTable dt = new DataTable();
            dt.Columns.Add("测试用例章节号", typeof(string));
            dt.Columns.Add("测试用例名称", typeof(HyperLink));
            dt.Columns.Add("测试用例标识", typeof(HyperLink));

            dt.Columns["测试用例章节号"].ReadOnly = true;

            string section = string.Empty;
            string name = string.Empty;
            string sign = string.Empty;

            HyperLinkActivate proc1 = new HyperLinkActivate(HyperLinkDo);
            _tree = tree;

            foreach(string s in li)
            {
                TreeViewUtils tvu = new TreeViewUtils();
                Z1Utils.Controls.EnumTreeViewProc proc = new EnumTreeViewProc(SearchIt);

                _curnode = null;
                _curtid = s;
                _taginfo = null;
                tvu.FindTreeViewLeaf(tree, proc);

                if(_curnode != null)
                {
                    section = UIFunc.GenSections(_curnode.Parent, _taginfo.order, '.');
                    sign = BusiLogic.GenUCSign(_curnode);
                    dt.Rows.Add(new object[] {section, new HyperLink(_curtid, _taginfo.text, _taginfo.text, proc1), 
                        new HyperLink(_curtid, sign, sign, proc1)});
                }
            }

            GridHyperLink gridlink = new GridHyperLink(grid);
            gridlink.AddLinkCol("测试用例名称");
            gridlink.AddLinkCol("测试用例标识");
            gridlink.InitGrid(dt);
        }

        private static TreeNode _curnode;
        private static string _curtid;
        private static NodeTagInfo _taginfo;
        private static bool SearchIt(TreeNode tn)
        {
            NodeTagInfo tag = tn.Tag as NodeTagInfo;
            if(tag == null)
                return true; // continue search

            if(tag.id.Equals(_curtid))
            {
                _curnode = tn;
                _taginfo = tag;
                return false; // stop search
            }
            else
                return true;
        }

        private static void HyperLinkDo(string tid)
        {
            if(MainForm.mainFrm.CurrentSelectedForm == null)
            {
                //BusiLogic.SelectMainFrmNode("zxd.TestTreeForm", "提交测试用例执行结果");
                //_tree = (MainForm.mainFrm.CurrentSelectedForm as TestTreeForm).tree; 
                //BusiLogic.FindUsecaseNode(_tree, tid, true);
                MainForm.mainFrm.DelayCreateForm("zxd.TestTreeForm?type=result&id=" + tid);
            }
            else
            {
                // 切换窗体前保存原窗体数据
                IBaseTreeForm bf = MainForm.mainFrm.CurrentSelectedForm as IBaseTreeForm;
                if(bf != null)
                    bf.OnPageClose(false);

                TestTreeForm ttf = MainForm.mainFrm.CurrentSelectedForm as TestTreeForm;
                if(ttf == null)
                {
                    //BusiLogic.SelectMainFrmNode("zxd.TestTreeForm", "提交测试用例执行结果");
                    //_tree = (MainForm.mainFrm.CurrentSelectedForm as TestTreeForm).tree;
                    //BusiLogic.FindUsecaseNode(_tree, tid, true);
                    MainForm.mainFrm.DelayCreateForm("zxd.TestTreeForm?type=result&id=" + tid);
                }
                else
                {
                    _tree = (MainForm.mainFrm.CurrentSelectedForm as TestTreeForm).tree;
                    BusiLogic.FindUsecaseNode(_tree, tid, true);
                }
            }
        }

        #endregion 问题报告单
    }
}
