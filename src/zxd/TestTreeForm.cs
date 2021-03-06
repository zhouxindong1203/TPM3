using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Crownwood.Magic.Controls;
using TPM3.Sys;
using Common;
using System.Xml;
using TPM3.zxd.Helper;
using TpmCoreData;
using Z1.tpm;
using Z1Utils.Controls;
using Z1.tpm.DB;
using System.Data.OleDb;
using Aspose.Words;
using Common.Aspose;
using Common.Database;
using Common.RichTextBox;
using TPM3.zxd.clu;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.zxd
{
    [TypeNameMap("zxd.TestTreeForm")]
    public partial class TestTreeForm : LeftTreeUserControl, ICopyCommand, IPasteCommand
    {
        DataTable m_dt;
        public TestTreeForm()
        {
            InitializeComponent();
            tree.BeforeSelect += treeView1_BeforeSelect;

        }

        private void TestTreeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (GridHyperLink.g_gridForALink != null)
            //{
            //    e.Cancel = true;
            //    this.Hide();
            //    ExecStatus.g_TestTreeFormALink = this;
            //}

            if (MainForm.mainFrm.DlgRefFindResult != null)
            {
                MainForm.mainFrm.DlgRefFindResult.Close();
                MainForm.mainFrm.DlgRefFindResult = null;
            }

            ExecStatus.g_ttf = new TestTreeForm();
            ExecStatus.g_ttf.BuildTestTree();

            MainForm.mainFrm.VisibleOCI(false);
            MainForm.mainFrm.VisibleFindUsecase(false);
            MainForm.mainFrm.VisibleUsecase(false);
        }

        #region IPasteCommand Members

        void IPasteCommand.OnCommandPaste()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region ICopyCommand Members

        void ICopyCommand.OnCommandCopy()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region 拖放

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            var screen_pt = MousePosition;
            var client_pt = tree.PointToClient(screen_pt);
                        
            var hitinfo = tree.HitTest(client_pt.X, client_pt.Y);
            if (hitinfo.Node == null)
                return;

            tree.SelectedNode = hitinfo.Node;

            if (TreeNodeHelper.GetObjectDragDropEffect(e.Item,
                new HashSet<NodeType> {NodeType.TestItem, NodeType.TestCase}) == DragDropEffects.Move)
            {
                var node_type = TreeNodeHelper.GetNodeType(e.Item as TreeNode);
                if (node_type == NodeType.TestCase)
                {
                    CopyUCOperate(e.Item as TreeNode);
                }
                else if (node_type == NodeType.TestItem)
                {
                    miCopyItem.PerformClick();
                }

                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void treeView1_DragOver(object sender, DragEventArgs e)
        {
            var target_node = TreeNodeHelper.GetCurPosTreeNode(sender, e.X, e.Y);
            if (target_node == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            var moved_node = (TreeNode)e.Data.GetData(typeof(TreeNode));
            if (!TreeNodeHelper.CanReceivePos(moved_node, target_node))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            var target_node_type = TreeNodeHelper.GetNodeType(target_node);
            var move_node_type = TreeNodeHelper.GetNodeType(moved_node);
            if (!TreeNodeHelper.CanReceiveDrop(move_node_type, target_node_type))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            e.Effect = DragDropEffects.Move;
            tree.Focus();
            tree.SelectedNode = target_node;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            #region delete
            //var moved_node = (TreeNode)e.Data.GetData(typeof(TreeNode)/*"System.Windows.Forms.TreeNode"*/);
            //根据鼠标坐标确定要移动到的目标节点
            //Point pt;
            //TreeNode targeNode;
            
            //pt = ((TreeView)(sender)).PointToClient(new Point(e.X, e.Y));
            //targeNode = tree.GetNodeAt(pt);
            //if (targeNode == null || targeNode.Parent == moved_node || targeNode == moved_node || targeNode == moved_node.Parent)
            //    return;

            //var target_node_type = TreeNodeHelper.GetNodeType(targeNode);
            //var move_node_type = TreeNodeHelper.GetNodeType(moved_node);

            //if (!TreeNodeHelper.CanReceiveDrop(move_node_type, target_node_type))
            //{
            //    e.Effect = DragDropEffects.None;
            //    return;
            //}

            //如果目标节点无子节点则添加为同级节点,反之添加到下级节点的未端
            //TreeNode NewMoveNode = (TreeNode)moved_node.Clone();
            //if (targeNode.Nodes.Count == 0)
            //{
            //    targeNode.Parent.Nodes.Insert(targeNode.Index, NewMoveNode);
            //}
            //else
            //{
            //    targeNode.Nodes.Insert(targeNode.Nodes.Count, NewMoveNode);
            //}
            ////更新当前拖动的节点选择
            //tree.SelectedNode = NewMoveNode;
            ////展开目标节点,便于显示拖放效果
            //targeNode.Expand();
            //移除拖放的节点
            //moveNode.Remove();
            #endregion

            var moved_node = (TreeNode)e.Data.GetData(typeof(TreeNode));
            var moved_node_type = TreeNodeHelper.GetNodeType(moved_node);
            if (moved_node_type == NodeType.None)
                return;

            var target_node = TreeNodeHelper.GetCurPosTreeNode(sender, e.X, e.Y);
            if (target_node == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            var target_node_type = TreeNodeHelper.GetNodeType(target_node);

            if (moved_node_type == NodeType.TestCase)
            {
                PasteUc(target_node);
            }
            else if (moved_node_type == NodeType.TestItem)
            {
                if (target_node_type == NodeType.TestItem)
                    PasteItemToItem(target_node);
                else if (target_node_type == NodeType.TestType)
                    PasteItemToType(target_node);
            }
        }

        #endregion

        #region override methods

        // 当前树节点变换时, 先关闭当前右侧页面, 再创建对应新页面
        public override void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            base.treeView1_BeforeSelect(sender, e);
        }

        public bool showuc;
        public bool IsRegressExec; // 是否"回归执行阶段"
        // 根据主导航树节点的选中, 创建应用窗体
        public override bool OnPageCreate()
        {
            this._pageType = FrmCommonFunc.GetFrmPageType(this);

            int startorder = FrmCommonFunc.GetStartOrder(this._pageType);
            showuc = FrmCommonFunc.ShowUsecase(this._pageType);

            // 设置不同节点类型的图标
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.Project] = "project";
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.TestObject] = "obj";
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.TestType] = "type";
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.TestItem] = "item";

            // 设置测试用例各执行状态的图标
            if (showuc)
            {
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.NonExecute] = "unexec";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.NonExecute_k] = "unexec_k";

                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecute] = "partexec";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecute_k] = "partexec_k";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecutep] = "partexecp";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecutep_k] = "partexecp_k";

                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executed] = "case";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executed_k] = "case_k";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executedp] = "execp";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executedp_k] = "execp_k";
            }
            TreeNode root = MainTestFrmCommon.BuildTestTree(this.tree, dbProject, (string)pid, (string)currentvid, startorder, showuc);
            if (showuc)
            {
                m_dt = dbProject.ExecuteDataTable("SELECT * FROM CA测试用例实体表");
                SetNewUseCaseState(root, m_dt);
            }
            // 展开树, 至测试项可见
            Cursor oldcursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            TreeViewUtils tvu = new TreeViewUtils();
            EnumTreeViewProc proc = new EnumTreeViewProc(ExpandToItems);
            tvu.FindTreeViewNode(this.tree, proc);

            this.tree.SelectedNode = root;
            this.ActiveControl = this.tree;
            this.Cursor = oldcursor;

            MainForm.mainFrm.VisibleOCI(true);
            if ((_pageType == PageType.TestCasePerform) ||
                (_pageType == PageType.TestCaseDesign))
            {
                MainForm.mainFrm.VisibleFindUsecase(true);
                MainForm.mainFrm.VisibleUsecase(true);
            }
            else
            {
                MainForm.mainFrm.VisibleFindUsecase(false);
                MainForm.mainFrm.VisibleUsecase(false);
            }

            return true;
        }
        public void SetNewUseCaseState(TreeNode tn, DataTable dt)
        { 
            TreeNode nextNode;
            foreach (TreeNode tr in tn.Nodes)
            {
                if (((NodeTagInfo)tr.Tag).nodeType == NodeType.TestCase)
                {
                    foreach (DataRow dr in dt.Rows)
                    { 
                        if(dr["ID"].ToString() == ((NodeTagInfo)tr.Tag).eid)
                        {
                            if (dr["设计人员"].ToString() == "" || dr["设计人员"] == null)
                            {
                                tr.ForeColor = System.Drawing.Color.LightPink;
                            }
                            else 
                            {
                                //tr.ForeColor = System.Drawing.Color.Black;
                            }
                            break;
                        }
                    }

                    nextNode = tr.NextNode;
                    while (nextNode != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        { 
                            if(dr["ID"].ToString() == ((NodeTagInfo)nextNode.Tag).eid)
                            {
                                if (dr["设计人员"].ToString() == "" || dr["设计人员"] == null)
                                {
                                    nextNode.ForeColor = System.Drawing.Color.LightPink;
                                }
                                else
                                {
                                    //nextNode.ForeColor = System.Drawing.Color.Black;
                                }
                                break;
                            }
                        }
                        nextNode = nextNode.NextNode;
                    }
                    return;
                }
                SetNewUseCaseState(tr, dt);
            }

        }
        public void BuildTestTree()
        {
            BuildTestTree((string)currentvid);
        }

        public void BuildTestTree(string vid)
        {
            int startorder = FrmCommonFunc.GetStartOrder(PageType.TestCasePerform);
            showuc = FrmCommonFunc.ShowUsecase(PageType.TestCasePerform);

            // 设置不同节点类型的图标
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.Project] = "project";
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.TestObject] = "obj";
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.TestType] = "type";
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.TestItem] = "item";

            // 设置测试用例各执行状态的图标
            if (showuc)
            {
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.NonExecute] = "unexec";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.NonExecute_k] = "unexec_k";

                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecute] = "partexec";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecute_k] = "partexec_k";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecutep] = "partexecp";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecutep_k] = "partexecp_k";

                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executed] = "case";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executed_k] = "case_k";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executedp] = "execp";
                MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executedp_k] = "execp_k";
            }

            MainTestFrmCommon.BuildTestTree(this.tree, dbProject, (string)pid, vid, startorder, showuc);
        }

        private bool ExpandToItems(TreeNode node)
        {
            NodeTagInfo taginfo = node.Tag as NodeTagInfo;
            if (taginfo == null)
                return false; // 停止搜索树

            if ((taginfo.nodeType == NodeType.TestItem) ||
                (taginfo.nodeType == NodeType.TestType) ||
                (taginfo.nodeType == NodeType.TestObject))
                node.EnsureVisible();

            return true;
        }

        private void TestTreeForm_Load(object sender, EventArgs e)
        {
            TestUsecase.g_TestTree = this.tree;

            //if (ExecStatus.g_TestTreeFormALink != null)
            //{
            //    ExecStatus.g_TestTreeFormALink.Close();
            //    ExecStatus.g_TestTreeFormALink = null;
            //}

            if (paramList.ContainsKey("id"))
            {
                string id = paramList["id"];
                BusiLogic.FindUsecaseNode(tree, id, true);
            }
            else
            {
                if (!string.Empty.Equals(ExecStatus.g_LastUserOp))
                    this.tree.SelectedNode = FrmCommonFunc.GetTreeNode(this.tree, ExecStatus.g_LastUserOp);
            }

            IsRegressExec = BusiLogic.IsRegressExec();

            tree.ItemDrag += treeView1_ItemDrag;
            //tree.DragEnter += treeView1_DragEnter;
            tree.DragDrop += treeView1_DragDrop;
            tree.DragOver += treeView1_DragOver;
        }

        // 关闭窗体
        public override bool OnPageClose(bool bClose)
        {
            // 关闭窗体时保存当前操作界面, 以便本次运行中恢复
            TreeNode tn = this.tree.SelectedNode;
            if (tn != null)
            {
                NodeTagInfo taginfo = tn.Tag as NodeTagInfo;
                if (taginfo != null)
                {
                    ExecStatus.g_LastUserOp = (taginfo.id);
                }
            }

            return base.CloseCurrentForm(bClose, false);
        }

        // 根据本窗体左侧节点树的选取, 生成右侧对应窗体
        public override Control GetSubForm(TreeNode node)
        {
            if (showuc)
            {
                m_dt = dbProject.ExecuteDataTable("SELECT * FROM CA测试用例实体表");
                SetNewUseCaseState(this.tree.TopNode, m_dt);
            }

            NodeTagInfo taginfo = node.Tag as NodeTagInfo;
            if (taginfo == null)
                return null;

            Form f = new Form();

            switch (taginfo.nodeType)
            {
                case NodeType.Project:      // 项目
                    f = new TestProjectForm();
                    break;

                case NodeType.TestObject:   // 被测对象
                    f = new TestObjForm();
                    break;

                case NodeType.TestType:     // 测试类型
                    f = new TestTypeForm();
                    break;

                case NodeType.TestItem:     // 测试项
                    f = new TestItemForm();
                    (f as TestItemForm).showUC = showuc;
                    break;

                case NodeType.TestCase:     // 测试用例
                    f = new TestUsecaseForm(this._pageType);
                    break;

                default:
                    f = new Form();
                    break;
            }
            return f;
        }

        // 将窗体f显示在this.panel1中
        public override void OnShowForm(TreeNode tn, Form f)
        {
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            this.panel1.Controls.Clear();
            this.panel1.Controls.Add(f);
            f.Dock = DockStyle.Fill;
            f.Visible = true;

            this._rightForm = f;
        }

        #endregion override methods

        #region 窗体级成员变量

        // 本窗体所对应的主导航树节点
        private PageType _pageType;
        public PageType pageType
        {
            get
            {
                return this._pageType;
            }
            set
            {
                this._pageType = value;
            }
        }

        private Form _rightForm;
        public Form rightForm
        {
            get
            {
                return this._rightForm;
            }
            set
            {
                this._rightForm = value;
            }
        }

        #endregion 窗体级成员变量

        #region 右键菜单

        #region 菜单项显示

        private void tree_MouseUp(object sender, MouseEventArgs e)
        {
            //this.Parent.Parent.Parent.Text = string.Format("X={0}, Y={1}", e.X, e.Y);
            //return;

            // 进行命中测试
            TreeViewHitTestInfo hitinfo = this.tree.HitTest(e.X, e.Y);

            if (hitinfo.Node != null)
            {
                // 处理右键消息
                if (e.Button != MouseButtons.Right)
                    return;

                this.tree.SelectedNode = hitinfo.Node;

                // 节点类型检测
                NodeTagInfo info = hitinfo.Node.Tag as NodeTagInfo;
                if (info == null)
                    return;

                Point p = this.tree.PointToScreen(e.Location);

                switch (info.nodeType)
                {
                    case NodeType.Project:
                        contextMenuProject.Show(p);
                        break;

                    case NodeType.TestObject:
                        this._context_menu_obj.Show(p);
                        break;

                    case NodeType.TestType:

                        //TestTypeSub tts = TestType.GetSubType(dbProject, (string)pid,
                        //    TestType.GetEntityID(dbProject, info.id));
                        //switch (tts)
                        //{
                        //    case TestTypeSub.NonDefinition:
                        //        return;

                        //    case TestTypeSub.SubType:
                        //        miNewSubtype.Text = "新建测试子类型(&N)...";
                        //        break;

                        //    case TestTypeSub.TestItem:
                        //        miNewSubtype.Text = "新建测试项(&N)...";
                        //        break;

                        //    default:
                        //        return;
                        //}
                        this.contextMenuTestType.Show(p);
                        break;

                    case NodeType.TestItem:
                        this.contextMenuTestItem.Show(p);
                        break;

                    case NodeType.TestCase:
                        this.contextMenuUsecase.Show(p);
                        break;

                    case NodeType.TestStep:
                        break;

                    default:
                        return;
                }
            }
        }

        private void EnableMenuItem(TreeNode node, ToolStripMenuItem mi)
        {
            if (node.Nodes.Count == 0)
                mi.Enabled = false;
            else
                mi.Enabled = true;
        }

        // 回归测试时禁止用户编辑由上
        private void EnableRenameItem(TreeNode node, ToolStripMenuItem mi)
        {
            if (IsRegressExec &&
                ((node.Tag as NodeTagInfo).IsRegressCreate))
                mi.Enabled = false;
            else
                mi.Enabled = true;
        }

        // "项目"右键菜单
        private void contextMenuProject_Opening(object sender, CancelEventArgs e)
        {
            EnableMenuItem(tree.SelectedNode, miCollapseProj);
        }

        // "被测对象"右键菜单
        private void contextMenuTestObj_Opening(object sender, CancelEventArgs e)
        {
            EnableMenuItem(tree.SelectedNode, miCollapseObj);
            EnableRenameItem(tree.SelectedNode, miRenameObj);
        }

        // 测试用例右键菜单
        private void contextMenuUsecase_Opening(object sender, CancelEventArgs e)
        {
            this.miCollapseUC.Visible = false;
            this.miNewStep.Visible = false;
            this.miModifyUC.Visible = false;
            this.toolStripSeparator8.Visible = false;

            bool shortcut = BusiLogic.NodeIsShortcut(this.tree.SelectedNode);
            this.miCopyUC.Enabled = !shortcut;
            //this.miDelUC.Enabled = !shortcut;
            this.miRenameUC.Enabled = !shortcut;
            this.tsmiMark.Enabled = !shortcut;

            if (this.miRenameUC.Enabled)
            {
                EnableRenameItem(tree.SelectedNode, miRenameUC);
            }
        }

        // 测试项右键菜单
        private void contextMenuTestItem_Opening(object sender, CancelEventArgs e)
        {
            EnableMenuItem(tree.SelectedNode, miCollapseItem);
            EnableRenameItem(tree.SelectedNode, miRenameItem);

            // 确定"粘贴"是否有效
            if ((BusiLogic.IsTestItemInClipboard() &&
                SatisfyLevelsLimit()
                && (!IsPasteOnSelf())
                && (!IsInside())) ||
                BusiLogic.IsUsecaseInClipboard() || BusiLogic.IsStpmsUcInClipboard() || BusiLogic.IsStpmsItemInClipboard())
            {
                this.miPasteIorU.Enabled = true;
            }
            else
                this.miPasteIorU.Enabled = false;

            // 屏蔽"新建测试子项"、"新建测试用例"和"修改"
            this.miNewSubitem.Visible = false;
            this.miNewUsecase.Visible = false;
            this.toolStripSeparator6.Visible = false;
            this.miModifyItem.Visible = false;
        }

        // 测试类型右键菜单
        private void contextMenuTestType_Opening(object sender, CancelEventArgs e)
        {
            EnableMenuItem(tree.SelectedNode, miCollapseType);
            EnableRenameItem(tree.SelectedNode, miRenameType);

            // 确定"粘贴"是否有效
            if ((BusiLogic.IsTestItemInClipboard() &&
                SatisfyLevelsLimit() &&
                ((currentSelectedForm as TestTypeForm) != null) &&
                ((currentSelectedForm as TestTypeForm).SubNodeType == TestTypeSub.TestItem)) ||
                (BusiLogic.IsStpmsItemInClipboard() && ((currentSelectedForm as TestTypeForm) != null) &&
                ((currentSelectedForm as TestTypeForm).SubNodeType == TestTypeSub.TestItem)))
            {
                this.miPasteItem.Enabled = true;
            }
            else
                this.miPasteItem.Enabled = false;
        }

        #endregion 菜单项显示

        #region 复制/粘贴

        // "测试用例" -> "复制"菜单项
        private void miCopyUC_Click(object sender, EventArgs e)
        {
            CopyUCOperate(tree.SelectedNode);
        }

        private void CopyUCOperate(TreeNode node)
        {
            NodeTagInfo taginfo = node.Tag as NodeTagInfo;
            if (taginfo == null)
            {
                MessageBox.Show("无法获取此树节点的附加信息!", "操作错误", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (taginfo.nodeType != NodeType.TestCase)
            {
                MessageBox.Show("此树节点非测试用例节点!", "操作错误", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            TestUsecaseForm tuf = currentSelectedForm as TestUsecaseForm;
            if (tuf == null)
            {
                MessageBox.Show("此树节点对应的右侧窗体非TestUsecaseForm!", "操作错误", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            tuf.OnPageClose(false);
            tuf.OnPageCreate();

            // 将此用例的所有测试步骤复制到剪贴板输入
            List<TestStep> datalist = new List<TestStep>();
            foreach (DataRow row in tuf.Tbl.Rows)
            {
                TestStep ts = new TestStep();

                ts.input = row["输入及操作"].Equals(DBNull.Value) ? string.Empty : (string)row["输入及操作"];
                ts.expection = row["期望结果"].Equals(DBNull.Value) ? string.Empty : (string)row["期望结果"];
                if (tuf.pageType == PageType.TestCasePerform)
                    ts.result = row["实测结果"].Equals(DBNull.Value) ? string.Empty : (string)row["实测结果"];

                GetStepAllAccs((string)row["ID"], (string)row["实体ID"],
                               ref ts.input_accs, ref ts.expect_accs, ref ts.result_accs);


                datalist.Add(ts);
            }

            // 获取测试用例数据
            TestUC ucinfo = new TestUC();
            ucinfo.id = taginfo.id;
            ucinfo.name = tuf.UCName;
            ucinfo.desc = tuf.Desc;
            ucinfo.init = tuf.Init;
            ucinfo.constraint = tuf.Constraint;
            ucinfo.term = tuf.Term;
            ucinfo.cert = tuf.Cert;
            ucinfo.trace = tuf.Trace;
            ucinfo.method = tuf.DesMethod;
            if (tuf.pageType == PageType.TestCaseDesign)
            {
                ucinfo.designperson = tuf.Person;
                ucinfo.testperson = string.Empty;
                ucinfo.testtime = DateTime.MinValue;
                ucinfo.execsta = ConstDef.execsta0;
                ucinfo.execrlt = ConstDef.execrlt0;
                ucinfo.unexec = string.Empty;
            }
            else if (tuf.pageType == PageType.TestCasePerform)
            {
                ucinfo.designperson = tuf.DesignPerson;
                ucinfo.testperson = tuf.Tester;
                ucinfo.testtime = tuf.TestTime;
                ucinfo.execsta = tuf.ExecStatus;
                ucinfo.execrlt = tuf.ExecResult;
                ucinfo.unexec = tuf.Unexec;
            }

            DBType dbtype;
            if (dbProject.databaseType == Common.Database.DatabaseType.Access)
                dbtype = DBType.Access;
            else if (dbProject.databaseType == Common.Database.DatabaseType.SQLServer)
                dbtype = DBType.SQL;
            else
                dbtype = DBType.Invalid;

            BusiLogic.CopyUCToClipboard(dbProject.dbConnection.ConnectionString,
                ucinfo, datalist, dbtype);

        }

        private void GetStepAllAccs(string tid, string eid, ref List<Acc> inputs,
            ref List<Acc> expects, ref List<Acc> results)
        {
            GetSpecialAccs(tid, eid, "输入及操作", ref inputs);
            GetSpecialAccs(tid, eid, "期望结果", ref expects);
            GetSpecialAccs(tid, eid, "实测结果", ref results);
        }

        private void GetSpecialAccs(string tid, string eid, string belong, ref List<Acc> accs)
        {
            string sql = "SELECT * FROM DC测试过程附件表 WHERE 项目ID=? AND 测试过程ID=? AND 附件所属=? AND 测试版本=?";
            DataTable tbl;
            tbl = dbProject.ExecuteDataTable(sql, pid, belong.Equals("实测结果") ? tid : eid, belong, currentvid);
            foreach (DataRow row in tbl.Rows)
            {
                sql = "SELECT * FROM DC附件实体表 WHERE ID=? AND 输出与否";
                DataRow row1 = dbProject.ExecuteDataRow(sql, row["附件实体ID"], true);
                if (row1 != null)
                {
                    Acc acc = new Acc();
                    acc.Name = (string)row1["附件名称"];
                    acc.Content = (byte[]) row1["附件内容"];
                    acc.Type = (string) row1["附件类型"];
                    acc.Memo = (string) row1["备注"];
                    acc.Path = (string) row1["对应原文件路径"];
                    acc.Output = true;
                    accs.Add(acc);
                }
            }
        }

        // 粘贴 "测试用例"或"测试项"
        private void miPasteIorU_Click(object sender, EventArgs e)
        {
            if (BusiLogic.IsUsecaseInClipboard())
            {
                PasteUc(tree.SelectedNode);
            }
            else if (BusiLogic.IsTestItemInClipboard())
            {
                PasteItemToItem(tree.SelectedNode);
            }
            else if(BusiLogic.IsStpmsUcInClipboard())
            {
                UsecaseAction ucAction =
                    Clipboard.GetData(GlobalClassNameAttribute.GetGlobalName(typeof (UsecaseAction))) as UsecaseAction;
                BusiLogic.PasteUCFromStpms(ucAction, (this.tree.SelectedNode.Tag as NodeTagInfo).id,
                                           (CurrentSelectedForm as TestItemForm).Tbl2, this.tree.SelectedNode);

            }
            else if(BusiLogic.IsStpmsItemInClipboard())
            {
                string globalName = GlobalClassNameAttribute.GetGlobalName(typeof(TestItemAction));
                if (!Clipboard.ContainsData(globalName))
                    return;
                TestItemAction itemAction = Clipboard.GetData(globalName) as TestItemAction;

                TestItemForm tif = currentSelectedForm as TestItemForm;
                if (tif == null)
                {
                    MessageBox.Show("目标树节点的右侧窗体非预期类型!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                tif.SaveToDB();
                NodeTagInfo tag = this.tree.SelectedNode.Tag as NodeTagInfo;
                string itemeid = TestItem.GetEntityID(dbProject, tag.id);
                int newseq = tif.Tbl1.Rows.Count + 1;
                BusiLogic.PasteTestItem(itemAction, dbProject, NodeType.TestItem, itemeid, newseq, tree.SelectedNode,
                                        tif.Tbl1);

            }
        }

        #region 复制粘贴

        private void PasteUc(TreeNode target_node)
        {
            if (Clipboard.ContainsData("CPUsecase"))
            {
                CPUsecase cpu = Clipboard.GetData("CPUsecase") as CPUsecase;
                if (cpu != null) // 粘贴当前版本工具的用例
                    BusiLogic.PasteUC(cpu, (target_node.Tag as NodeTagInfo).id,
                        (CurrentSelectedForm as TestItemForm).Tbl2, target_node);
            }
            else if (Clipboard.ContainsData("CPUsecaseOld"))
            {
                CPUsecaseOld cpu = Clipboard.GetData("CPUsecaseOld") as CPUsecaseOld;
                if (cpu != null) // 粘贴旧版本工具的用例
                {
                    BusiLogic.PasteUCOld(cpu, (target_node.Tag as NodeTagInfo).id,
                        (currentSelectedForm as TestItemForm).Tbl2, target_node);
                }
            }
        }

        private void PasteItemToItem(TreeNode target_node)
        {
            CPTestItem cpi = Clipboard.GetData("CPTestItem") as CPTestItem;
            if (cpi == null)
            {
                MessageBox.Show("剪贴板数据格式错误!", "数据格式错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (target_node == null)
            {
                MessageBox.Show("无选中的目标树节点!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NodeTagInfo tag = target_node.Tag as NodeTagInfo;
            if (tag == null)
            {
                MessageBox.Show("无法存取目标树节点的附加数据!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TestItemForm tif = currentSelectedForm as TestItemForm;
            if (tif == null)
            {
                MessageBox.Show("目标树节点的右侧窗体非预期类型!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool bChildCopy = false;
            using (PasteItemSelForm isf = new PasteItemSelForm())
            {
                if (isf.ShowDialog() == DialogResult.OK)
                    bChildCopy = (isf.PasteSel == 0) ? false : true;
                else
                    return;
            }

            tif.SaveToDB();

            string itemeid = TestItem.GetEntityID(dbProject, tag.id);

            bool bSameDB = dbProject.dbConnection.ConnectionString.Equals(cpi.ConnStr) ? true : false;
            DBAccess dbsrc = dbProject;
            if (!bSameDB)
            {
                OleDbConnectionStringBuilder connstr = new OleDbConnectionStringBuilder(cpi.ConnStr);
                dbsrc = DBAccessFactory.FromAccessFile(connstr.DataSource).CreateInst();
            }

            int newseq = tif.Tbl1.Rows.Count + 1;

            bool bCopyAll = (_pageType == PageType.TestCasePerform) ? true : false;

            CPUtils.PasteTestItem(dbsrc, dbProject, NodeType.TestItem, cpi.dataList[0], itemeid,
                newseq, (string)pid, (string)currentvid, bChildCopy, bCopyAll, bSameDB, target_node, tif.Tbl1);
            FrmCommonFunc.ReorderUCAfterPasteItem(target_node);
        }

        private void PasteItemToType(TreeNode target_node)
        {
            CPTestItem cpi = Clipboard.GetData("CPTestItem") as CPTestItem;
            if (cpi == null)
            {
                MessageBox.Show("剪贴板数据格式错误!", "数据格式错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (target_node == null)
            {
                MessageBox.Show("无选中的目标树节点!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NodeTagInfo tag = target_node.Tag as NodeTagInfo;
            if (tag == null)
            {
                MessageBox.Show("无法存取目标树节点的附加数据!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TestTypeForm ttf = currentSelectedForm as TestTypeForm;
            if (ttf == null)
            {
                MessageBox.Show("目标树节点的右侧窗体非预期类型!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool bChildCopy = false;
            using (PasteItemSelForm isf = new PasteItemSelForm())
            {
                if (isf.ShowDialog() == DialogResult.OK)
                    bChildCopy = (isf.PasteSel == 0) ? false : true;
                else
                    return;
            }

            ttf.SaveToDB();

            string typeeid = TestType.GetEntityID(dbProject, tag.id);

            bool bSameDB = dbProject.dbConnection.ConnectionString.Equals(cpi.ConnStr) ? true : false;
            DBAccess dbsrc = dbProject;
            if (!bSameDB)
            {
                OleDbConnectionStringBuilder connstr = new OleDbConnectionStringBuilder(cpi.ConnStr);
                dbsrc = DBAccessFactory.FromAccessFile(connstr.DataSource).CreateInst();
            }

            int newseq = ttf.Tbl.Rows.Count + 1;

            bool bCopyAll = (_pageType == PageType.TestCasePerform) ? true : false;

            CPUtils.PasteTestItem(dbsrc, dbProject, NodeType.TestType, cpi.dataList[0], typeeid,
                                  newseq, (string)pid, (string)currentvid, bChildCopy, bCopyAll, bSameDB, target_node,
                                  ttf.Tbl);
        }

        #endregion

        // 测试项 -> 复制
        private void miCopyItem_Click(object sender, EventArgs e)
        {
            OnPageClose(false);

            TestUsecaseForm tuf1 = rightForm as TestUsecaseForm;
            if (tuf1 != null)
                tuf1.OnPageClose(false);

            int level = BusiLogic.GetDownLevel(this.tree.SelectedNode); // 源节点的子测试项级数(含自身)
            List<string> datalist = new List<string>();
            datalist.Add((this.tree.SelectedNode.Tag as NodeTagInfo).id);
                        
            DBType dbtype;
            if (dbProject.databaseType == Common.Database.DatabaseType.Access)
                dbtype = DBType.Access;
            else if (dbProject.databaseType == Common.Database.DatabaseType.SQLServer)
                dbtype = DBType.SQL;
            else
                dbtype = DBType.Invalid;

            // 保存数据至数据库
            TestItemForm tif = currentSelectedForm as TestItemForm;
            if (tif == null)
            {
                MessageBox.Show("树节点右侧窗体非预期类型!\n无法保存数据至数据库!", "操作失败", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            tif.SaveData();
            tif.SaveToDB();

            BusiLogic.CopyItemToClipboard(dbProject.dbConnection.ConnectionString,
                datalist, level, dbtype);
        }

        // 测试类型 -> 粘贴
        private void miPasteItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsData("CPTestItem"))
            {
                PasteItemToType(tree.SelectedNode);
            }
            else if (Clipboard.ContainsData(GlobalClassNameAttribute.GetGlobalName(typeof(TestItemAction))))
            {
                string globalName = GlobalClassNameAttribute.GetGlobalName(typeof (TestItemAction));
                if (!Clipboard.ContainsData(globalName))
                    return;

                TestItemAction itemAction = Clipboard.GetData(globalName) as TestItemAction;
                TestTypeForm ttf = currentSelectedForm as TestTypeForm;
                if (ttf == null)
                {
                    MessageBox.Show("目标树节点的右侧窗体非预期类型!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ttf.SaveToDB();
                NodeTagInfo tag = this.tree.SelectedNode.Tag as NodeTagInfo;
                string typeeid = TestType.GetEntityID(dbProject, tag.id);
                int newseq = ttf.Tbl.Rows.Count + 1;
                BusiLogic.PasteTestItem(itemAction, dbProject, NodeType.TestType, typeeid, newseq, tree.SelectedNode,
                                        ttf.Tbl);
            }
        }

        #endregion 复制/粘贴

        #region 菜单->"项目"

        // "折叠/展开"
        private void miCollapseProj_Click(object sender, EventArgs e)
        {
            TreeNode node = this.tree.SelectedNode;
            if (node.IsExpanded)
                node.Collapse();
            else
            {
                node.ExpandAll();
                node.EnsureVisible();
            }
        }

        #endregion 菜单->"项目"

        #region 菜单->"被测对象"

        // "删除此被测对象"
        private void miDelObj_Click(object sender, EventArgs e)
        {
            if (IsRegressExec)
            {
                if (BusiLogic.HasChildNode(tree.SelectedNode))
                {
                    MessageBox.Show("此被测对象下非空!!删除前请先删除其所属子节点!!", "操作失败", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    DeleteTestObj();
                }
            }
            else
            {
                if (MessageBox.Show("确实要删除此被测对象吗?\n(此被测对象下所有节点将一并删除!)",
                    "删除被测对象", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;

                DeleteTestObj();
            }
        }

        private void DeleteTestObj()
        {
            NodeTagInfo tag = tree.SelectedNode.Tag as NodeTagInfo;
            if (tag == null)
            {
                MessageBox.Show("无法获取树节点的附加数据!", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string objeid = TestObj.GetEntityID(dbProject, tag.id);
            TestObj.DeleteObj(dbProject, (string)pid, (string)currentvid, objeid);

            UIFunc.DeleteTreeNode(tree.SelectedNode);
        }
        // "重命名" (此方法可共用)
        private void miRenameObj_Click(object sender, EventArgs e)
        {
            _inedit = true;
            _nodeLabel = TrimSequence(tree.SelectedNode.Text, out _surLabel);
            tree.SelectedNode.Text = _nodeLabel;

            tree.SelectedNode.BeginEdit();
        }

        private void _pbl_sheets_Click(object sender, EventArgs e)
        {
            var tag = tree.SelectedNode.Tag as NodeTagInfo;
            if (tag == null)
            {
                MessageBox.Show("无法获取树节点的附加数据!", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                        
            string sqlstate = "select * from CA问题报告单 where 项目ID=? and 测试版本=? and 所属被测对象ID=? order by 同标识序号";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, (string)pid, (string)currentvid, tag.id);
            //rownew["问题描述"] = IOleObjectAssist.GetByteFromString(row["问题描述"].ToString());
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                DataRow dr = dt.Rows[i];
                string name = dr["名称"].ToString();

                // 在“问题描述”字段，数据类型为ole对象
                // 输入文本或使用word编辑后都为byte[]类型，但存取方式不一样
                var pbl_desc_bytes = (byte[])dr["问题描述"];

                string str_desc = null;
                str_desc = IOleObjectAssist.GetStringFromByte(pbl_desc_bytes); // 获取直接输入的文本
                if (str_desc == null)
                    str_desc = ((Document) AsposeFactory.GetDocumentFromRich(pbl_desc_bytes)).GetText(); // 激活Word输入的文本


            }

        }

        //private string GetOleString(byte[] ole_bytes)
        //{


        //}
        
        #endregion 菜单->"被测对象"

        #region 菜单->"测试类型"

        // "删除此测试类型"
        private void miDelType_Click(object sender, EventArgs e)
        {
            if (IsRegressExec)
            {
                if (BusiLogic.HasChildNode(tree.SelectedNode))
                {
                    MessageBox.Show("此测试类型下非空!!删除前请先删除其所属子节点!!", "操作失败", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    DeleteTestType();
                }
            }
            else
            {
                if (MessageBox.Show("确实要删除此测试类型吗?\n(此测试类型下所有节点将一并删除!)",
                    "删除测试类型", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
                DeleteTestType();
            }
        }

        private void DeleteTestType()
        {
            NodeTagInfo tag = tree.SelectedNode.Tag as NodeTagInfo;
            if (tag == null)
            {
                MessageBox.Show("无法获取树节点的附加数据!", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string typeeid = TestType.GetEntityID(dbProject, tag.id);
            TestType.DeleteType(dbProject, (string)pid, (string)currentvid, typeeid);

            UIFunc.DeleteTreeNode(tree.SelectedNode);
        }

        #endregion 菜单->"测试类型"

        #region 菜单->"测试项"

        // "删除此测试项"
        private void miDelItem_Click(object sender, EventArgs e)
        {
            if (IsRegressExec)
            {
                string itemeid = TestItem.GetEntityID(dbProject, (tree.SelectedNode.Tag as NodeTagInfo).id);
                if (!TestItem.IsSameVer(dbProject, itemeid, currentvid as string))
                {
                    InputText input = new InputText();
                    if (input.ShowDialog() == DialogResult.OK)
                    {
                        object trace = TestItem.GetItemTrace(dbProject, (tree.SelectedNode.Tag as NodeTagInfo).id);
                        string objtid = FrmCommonFunc.GetBelongObjID(tree.SelectedNode);
                        Regress.InsertUntest(dbProject, pid, itemeid, 2, input.InputReason, trace, currentvid, objtid);
                        DeleteTestItem();
                    }
                    else
                        return;
                }
                else
                {
                    if (MessageBox.Show("确实要删除此测试项吗?\n(此测试项下所有节点将一并删除!)",
                        "删除测试项", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;

                    DeleteTestItem();
                }
            }
            else
            {
                if (MessageBox.Show("确实要删除此测试项吗?\n(此测试项下所有节点将一并删除!)",
                    "删除测试项", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;

                DeleteTestItem();
            }
        }

        private void DeleteTestItem()
        {
            NodeTagInfo tag = tree.SelectedNode.Tag as NodeTagInfo;
            if (tag == null)
            {
                MessageBox.Show("无法获取树节点的附加数据!", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 先删除测试项节点, 删除节点会导致TestItem窗口关闭,
            // 触发保存数据库动作, 如果先行调用后面的两行代码会导致数据库并发错误
            UIFunc.DeleteTreeNode(tree.SelectedNode);

            string itemeid = TestItem.GetEntityID(dbProject, tag.id);
            TestItem.DeleteItem(dbProject, (string)pid, (string)currentvid, itemeid);
        }

        #endregion 菜单->"测试项"

        #region 菜单->"测试用例"

        // "删除此测试用例"
        private void miDelUC_Click(object sender, EventArgs e)
        {
            if (IsRegressExec)
            {
                string uceid = TestUsecase.GetUCEntityID(dbProject, (tree.SelectedNode.Tag as NodeTagInfo).id);
                if (!TestUsecase.IsSameVer(dbProject, uceid, currentvid as string)) // 继承的上个版本的测试用例
                {
                    InputText input = new InputText();
                    if (input.ShowDialog() == DialogResult.OK)
                    {
                        string objtid = FrmCommonFunc.GetBelongObjID(tree.SelectedNode);
                        string itemtid = FrmCommonFunc.GetParentID(tree.SelectedNode);
                        Regress.InsertUntest(dbProject, pid, uceid, 1, input.InputReason, string.Empty, currentvid, objtid + "," + itemtid);
                        DeleteUC();
                    }
                    else
                        return;
                }
                else
                {
                    if (MessageBox.Show("确实要删除此测试用例吗?",
                        "删除测试用例", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;

                    DeleteUC();
                }
            }
            else
            {
                if (MessageBox.Show("确实要删除此测试用例吗?",
                    "删除测试用例", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;

                DeleteUC();
            }
        }

        private void DeleteUC()
        {
            NodeTagInfo tag = tree.SelectedNode.Tag as NodeTagInfo;
            NodeTagInfo partag = tree.SelectedNode.Parent.Tag as NodeTagInfo;
            if ((tag == null) ||
                (partag == null))
            {
                MessageBox.Show("无法获取树节点的附加数据!", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TestUsecase.g_TestTree = tree;
            TestUsecase.DeleteUsecase(dbProject, (string)pid, (string)currentvid, partag.id, tag.id, !tag.IsShortcut);

            UIFunc.DeleteTreeNode(tree.SelectedNode);
        }

        private void tsmiBlack_Click(object sender, EventArgs e)
        {
            MarkForeColor(0);
        }

        private void tsmiSilver_Click(object sender, EventArgs e)
        {
            MarkForeColor(1);
        }

        private void tsmiMaroon_Click(object sender, EventArgs e)
        {
            MarkForeColor(2);
        }

        private void tsmiRed_Click(object sender, EventArgs e)
        {
            MarkForeColor(3);
        }

        private void tsmiLightCoral_Click(object sender, EventArgs e)
        {
            MarkForeColor(4);
        }

        private void tsmiSienna_Click(object sender, EventArgs e)
        {
            MarkForeColor(5);
        }

        private void tsmiChocolate_Click(object sender, EventArgs e)
        {
            MarkForeColor(6);
        }

        private void tsmiGold_Click(object sender, EventArgs e)
        {
            MarkForeColor(7);
        }

        private void tsmiOliveDrab_Click(object sender, EventArgs e)
        {
            MarkForeColor(8);
        }

        private void tsmiSeaGreen_Click(object sender, EventArgs e)
        {
            MarkForeColor(9);
        }

        private void tsmiNavy_Click(object sender, EventArgs e)
        {
            MarkForeColor(10);
        }

        private void tsmiBlueViolet_Click(object sender, EventArgs e)
        {
            MarkForeColor(11);
        }

        private string _clrnodetid;
        private Color _clr;
        private void MarkForeColor(int index)
        {
            try
            {
                tree.SelectedNode.ForeColor = Color.FromName(ConstDef.markclr[index]);
                _clr = tree.SelectedNode.ForeColor;

                TestUsecaseForm tuf = CurrentSelectedForm as TestUsecaseForm;
                if (tuf != null)
                {
                    tuf.MarkClr = ConstDef.markclr[index];
                    tuf.DataChanged = true;
                    _clrnodetid = tuf.UCTid;

                    TreeViewUtils tvu = new TreeViewUtils();
                    EnumTreeViewProc proc = new EnumTreeViewProc(ChangeForeClrToLink);
                    tvu.FindTreeViewLeaf(this.tree, proc);

                }
                tree.Refresh();
            }
            catch (ArgumentException)
            {
            }
        }

        private bool ChangeForeClrToLink(TreeNode node)
        {
            NodeTagInfo tag = node.Tag as NodeTagInfo;
            if (tag != null)
            {
                if (tag.id.Equals(_clrnodetid))
                {
                    node.ForeColor = _clr;
                    node.TreeView.Refresh();                 
                }
            }

            return true;
        }

        #endregion 菜单->"测试用例"

        #region 树节点文本编辑

        private bool _inedit = false;               // 控制树节点文本编辑启动方式
        private string _nodeLabel = string.Empty;   // 编辑前树节点的文本部分
        private string _surLabel = string.Empty;    // 编辑前树节点的章节部分
        private void tree_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (!_inedit)
                e.CancelEdit = true;
        }

        private void tree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            _inedit = false;

            if ((e.Label == null) ||
                (e.Label.Length == 0) ||
                (e.Label.Trim().Length == 0))
            {
                e.CancelEdit = true;
                tree.SelectedNode.Text = _surLabel + _nodeLabel;
                return;
            }

            NodeTagInfo tag = tree.SelectedNode.Tag as NodeTagInfo;
            if (tag == null)
                return;

            switch (tag.nodeType)
            {
                case NodeType.TestObject:

                    //-------------------------------------------------------------------------------------------//
                    //e.Node.Text = _nodeCur.Text;
                    //e.CancelEdit = true;        // 无此语句, 对树节点文本的赋值无效, 控件仍旧用输入更新文本标签
                    //-------------------------------------------------------------------------------------------//
                    TestObjForm tof = currentSelectedForm as TestObjForm;
                    if (!BusiLogic.AssertNotNull(tof, "树节点右侧窗体非预期类型!", "操作失败"))
                        return;

                    // 先检查是否有重复
                    if (!e.Label.Equals(_nodeLabel))
                    {
                        if (TestObj.ExistObjNameFromTbl(tof.TblObjs, (string)pid, e.Label))
                        {
                            MessageBox.Show("已经存在此名称的被测对象, 请换用其他名称!", "被测对象名称重复", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                            e.CancelEdit = true;
                            tree.SelectedNode.Text = _surLabel + _nodeLabel;
                            return;
                        }
                    }

                    e.Node.Text = _surLabel + e.Label;
                    e.CancelEdit = true;

                    tof.ObjName = e.Label;
                    tag.text = e.Label;

                    break;

                case NodeType.TestType:

                    TestTypeForm ttf = currentSelectedForm as TestTypeForm;
                    if (!BusiLogic.AssertNotNull(ttf, "树节点右侧窗体非预期类型!", "操作失败"))
                        return;

                    e.Node.Text = _surLabel + e.Label;
                    e.CancelEdit = true;

                    ttf.TypeName = e.Label;
                    tag.text = e.Label;

                    break;

                case NodeType.TestItem:

                    TestItemForm tif = currentSelectedForm as TestItemForm;
                    if (!BusiLogic.AssertNotNull(tif, "树节点右侧窗体非预期类型!", "操作失败"))
                        return;

                    e.Node.Text = _surLabel + e.Label;
                    e.CancelEdit = true;

                    tif.ItemName = e.Label;
                    tag.text = e.Label;

                    break;

                case NodeType.TestCase:

                    TestUsecaseForm tuf = currentSelectedForm as TestUsecaseForm;
                    if (!BusiLogic.AssertNotNull(tuf, "树节点右侧窗体非预期类型!", "操作失败"))
                        return;

                    // 唯一性检测
                    if (e.Label.Equals(_nodeLabel))
                        return;

                    if(e.Label.ToLower().Equals(_nodeLabel.ToLower()))
                    {
                        e.Node.Text = _surLabel + e.Label;
                        e.CancelEdit = true;

                        tuf.UCName = e.Label;

                        return;
                    }

                    if (TestUsecase.ExistUCNameFromTbl(tuf.TblUCs, e.Label))
                    {
                        MessageBox.Show("已经存在此名称的测试用例, 请换用其他名称!", "测试用例名称重复", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        e.CancelEdit = true;
                        tree.SelectedNode.Text = _surLabel + _nodeLabel;
                        return;
                    }

                    e.Node.Text = _surLabel + e.Label;
                    e.CancelEdit = true;

                    tuf.UCName = e.Label;

                    break;

                case NodeType.TestStep:
                    break;

                default:
                    break;
            }
        }

        #endregion 树节点文本编辑

        #endregion 右键菜单

        #region 内部方法

        // 检测源节点和目标节点的级数是否超限
        private bool SatisfyLevelsLimit()
        {
            CPTestItem cpi = Clipboard.GetData("CPTestItem") as CPTestItem;
            if (cpi == null)
                return false;
            if (cpi.nodetype != NodeType.TestItem)
                return false;
            int levels = BusiLogic.GetTypeItemLevel(this.tree.SelectedNode) + cpi.Level;
            if (levels > ConstDef.MaxLevel)
                return false;
            else
                return true;
        }

        // 检测源节点和目标节点是否为同一节点
        private bool IsPasteOnSelf()
        {
            CPTestItem cpi = Clipboard.GetData("CPTestItem") as CPTestItem;
            if (cpi == null)
                return false;
            if (cpi.nodetype != NodeType.TestItem)
                return false;

            NodeTagInfo tag = this.tree.SelectedNode.Tag as NodeTagInfo;
            if (tag == null)
                return false;

            if ((dbProject.dbConnection.ConnectionString.Equals(cpi.ConnStr)) &&
                (tag.id.Equals(cpi.dataList[0])))
                return true;
            else
                return false;
        }

        // 检测目标节点是否位于源节点的树内(会导致循环复制)
        private bool IsInside()
        {
            CPTestItem cpi = Clipboard.GetData("CPTestItem") as CPTestItem;
            if (cpi == null)
                return false;
            if (cpi.nodetype != NodeType.TestItem)
                return false;
            if(!cpi.ConnStr.Equals(dbProject.dbConnection.ConnectionString))
                return false;

            TreeNode node = this.tree.SelectedNode;
            NodeTagInfo tag = node.Tag as NodeTagInfo;
            if (tag == null)
                return false;

            while (tag.nodeType == NodeType.TestItem)
            {
                if (tag.id.Equals(cpi.dataList[0]))
                    return true;
                node = node.Parent;
                tag = node.Tag as NodeTagInfo;
                if (tag == null)
                    return false;
            }
            return false;
        }

        /// <summary>
        /// 将text中的(X.X)删除
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string TrimSequence(string text, out string surlable)
        {
            int index = text.IndexOf(')');
            surlable = text.Substring(0, index + 1);
            return text.Substring(index + 1);
        }

        #endregion 内部方法
    }
}