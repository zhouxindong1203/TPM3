using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TPM3.Sys;
using Z1.tpm.DB;
using Z1.tpm;
using C1.Win.C1TrueDBGrid;

namespace TPM3.zxd.util
{
    /*
     * TPM3.zxd.util.ImportUC: 只在回归执行阶段有效
     * 用于回归测试从前版本中导入测试用例

     * zhouxindong 2009/04/15
     */ 
    public partial class ImportUC : Form
    {
        public ImportUC()
        {
            InitializeComponent();

            _curver = MyBaseForm.currentvid as string;
            _prever = CommonDB.GetPreVersion(MyBaseForm.dbProject, (string)MyBaseForm.pid, _curver);
            _pid = MyBaseForm.pid as string;
        }

        #region 窗体加载

        private string _prever; // 前向测试版本
        private string _curver; // 当前测试版本
        private string _pid;    // 项目ID

        // 加载前向版本测试对象树

        private void LoadTestTree(TreeView tree, string ver)
        {
            int startorder = FrmCommonFunc.GetStartOrder(Z1.tpm.PageType.TestCasePerform);

            // 设置不同节点类型的图标

            MainTestFrmCommon.NodeTypeImageKeys[NodeType.Project] = "project";
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.TestObject] = "obj";
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.TestType] = "type";
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.TestItem] = "item";

            TreeNode root = MainTestFrmCommon.BuildTestTree(tree, 
                MyBaseForm.dbProject, _pid, ver, startorder, false);

            // 展开树, 至测试项可见
            // 展开树, 至测试项可见
            Cursor oldcursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            root.ExpandAll();

            tree.SelectedNode = root;
            tree.HideSelection = false;
            this.Cursor = oldcursor;
        }

        private void ImportUC_Load(object sender, EventArgs e)
        {
            LoadTestTree(this.tree1, _prever);
            LoadTestTree(this.tree2, _curver);
        }

        #endregion 窗体加载

        #region 数据绑定

        // 窗体初始加载时显示当前项目的所有可用测试用例实体

        private void FirstLoadForm()
        {
            DataTable tblparent = TestUsecase.GetUsecasesEntityForProj(MyBaseForm.dbProject, _pid);
            tblparent.TableName = "Entity";
            //DataTable tblchild = TestUsecase.GetUsecasesTestForProj(MyBaseForm.dbProject, _pid);
            DataTable tblchild = new DataTable();
            tblchild.TableName = "Test";

            tblparent.Columns.Add("选用", typeof(bool));
            tblparent.Columns.Add("序号", typeof(int));

            int index = 1;
            foreach (DataRow row in tblparent.Rows)
            {
                row["选用"] = false;
                row["序号"] = index++;
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(tblparent);
            ds.Tables.Add(tblchild);

            //ds.Relations.Add("et", ds.Tables["Entity"].Columns["实体ID"],
            //    ds.Tables["Test"].Columns["测试用例ID"]);

            grid1.ChildGrid = grid2;

            grid1.DataSource = ds;
            grid1.DataMember = "Entity";

            SetGridStyle();
            SetGrid1DisplayCol();
        }

        private void LoadUCFromItem(string itemtid)
        {
            DataTable tblparent = TestUsecase.GetUsecaseFromItem(MyBaseForm.dbProject, itemtid, _pid, _prever);
            tblparent.TableName = "Entity";
            //DataTable tblchild = TestUsecase.GetUsecasesTestForProj(MyBaseForm.dbProject, _pid);
            DataTable tblchild = new DataTable();
            tblchild.TableName = "Test";

            tblparent.Columns.Add("选用", typeof(bool));

            foreach (DataRow row in tblparent.Rows)
                row["选用"] = false;

            DataSet ds = new DataSet();
            ds.Tables.Add(tblparent);
            ds.Tables.Add(tblchild);

            //ds.Relations.Add("et", ds.Tables["Entity"].Columns["实体ID"],
            //    ds.Tables["Test"].Columns["测试用例ID"]);

            grid1.ChildGrid = grid2;
            grid1.DataSource = ds;
            grid1.DataMember = "Entity";

            SetGridStyle();
            SetGrid1DisplayCol();
        }

        private void LoadEmptyTbl()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(int));
            dt.Columns.Add("选用", typeof(bool));
            dt.Columns.Add("测试用例名称", typeof(string));
            dt.Columns.Add("用例描述", typeof(string));
            dt.Columns.Add("用例的初始化", typeof(string));
            dt.Columns.Add("前提和约束", typeof(string));
            dt.Columns.Add("设计时间", typeof(DateTime));

            this.grid1.SetDataBinding(dt, "");
            SetGridStyle();
            SetGrid1DisplayCol();
        }

        private void SetGridStyle()
        {
            FrmCommonFunc.UniformGrid(grid2, System.Drawing.Color.Black,
                System.Drawing.SystemColors.Control, 20, 20);
            FrmCommonFunc.UniformGrid(grid1, System.Drawing.Color.Black,
                System.Drawing.SystemColors.Control, 20, 20);

            //grid1.AlternatingRows = true;
            //grid2.AlternatingRows = true;

            //grid1.Styles["EvenRow"].BackColor = System.Drawing.Color.Honeydew;
            //grid1.Styles["OddRow"].BackColor = System.Drawing.Color.LavenderBlush;
            //grid2.Styles["EvenRow"].BackColor = System.Drawing.Color.Honeydew;
            //grid2.Styles["OddRow"].BackColor = System.Drawing.Color.LavenderBlush;

            grid1.AllowAddNew = false;
            grid1.AllowDelete = false;

            grid2.AllowAddNew = false;
            grid2.AllowDelete = false;

            grid1.AllowRowSizing = RowSizingEnum.IndividualRows;
        }

        private void SetGrid1DisplayCol()
        {
            C1DisplayColumn d1 = grid1.Splits[0].DisplayColumns["序号"];
            C1DisplayColumn d2 = grid1.Splits[0].DisplayColumns["选用"];
            C1DisplayColumn d3 = grid1.Splits[0].DisplayColumns["测试用例名称"];
            C1DisplayColumn d4 = grid1.Splits[0].DisplayColumns["用例描述"];
            C1DisplayColumn d5 = grid1.Splits[0].DisplayColumns["用例的初始化"];
            C1DisplayColumn d6 = grid1.Splits[0].DisplayColumns["前提和约束"];
            C1DisplayColumn d7 = grid1.Splits[0].DisplayColumns["设计时间"];

            grid1.Splits[0].DisplayColumns.Clear();
            grid1.Splits[0].DisplayColumns.Insert(0, d1);
            grid1.Splits[0].DisplayColumns.Insert(1, d2);
            grid1.Splits[0].DisplayColumns.Insert(2, d3);
            grid1.Splits[0].DisplayColumns.Insert(3, d4);
            grid1.Splits[0].DisplayColumns.Insert(4, d5);
            grid1.Splits[0].DisplayColumns.Insert(5, d6);
            grid1.Splits[0].DisplayColumns.Insert(6, d7);

            d1.Width = 35;
            d2.Width = 30;
            d3.Width = 180;
            d4.Width = 200;
            d5.Width = 150;
            d6.Width = 150;
            d7.Width = 100;

            foreach (C1DisplayColumn dc in grid1.Splits[0].DisplayColumns)
            {
                dc.Style.VerticalAlignment = AlignVertEnum.Center;
                dc.Style.WrapText = true;
            }

            grid1.MarqueeStyle = MarqueeEnum.SolidCellBorder;
            grid1.EditDropDown = true;
        }

        private void SetGrid2DisplayCol()
        {
            C1DisplayColumn d1 = grid2.Splits[0].DisplayColumns["版本"];
            C1DisplayColumn d2 = grid2.Splits[0].DisplayColumns["测试时间"];
            C1DisplayColumn d3 = grid2.Splits[0].DisplayColumns["执行状态"];
            C1DisplayColumn d4 = grid2.Splits[0].DisplayColumns["执行结果"];
            C1DisplayColumn d5 = grid2.Splits[0].DisplayColumns["未执行原因"];

            grid2.Splits[0].DisplayColumns.Clear();
            grid2.Splits[0].DisplayColumns.Insert(0, d1);
            grid2.Splits[0].DisplayColumns.Insert(1, d2);
            grid2.Splits[0].DisplayColumns.Insert(2, d3);
            grid2.Splits[0].DisplayColumns.Insert(3, d4);
            grid2.Splits[0].DisplayColumns.Insert(4, d5);

            d1.Width = 100;
            d2.Width = 100;
            d3.Width = 80;
            d4.Width = 80;
            d5.Width = 300;

            foreach (C1DisplayColumn dc in grid2.Splits[0].DisplayColumns)
            {
                dc.Style.VerticalAlignment = AlignVertEnum.Center;
                dc.Style.WrapText = true;
            }

            grid2.Width = d1.Width + d2.Width + d3.Width + d4.Width + d5.Width +
                grid2.Splits[0].RecordSelectorWidth + 8;

            grid2.MarqueeStyle = MarqueeEnum.SolidCellBorder;
            grid2.EditDropDown = true;
        }

        private void grid2_VisibleChanged(object sender, EventArgs e)
        {
            if (grid2.Visible)
            {
                object eid = grid1.Columns["实体ID"].Value;
                DataTable t = TestUsecase.GetUsecasesTest(MyBaseForm.dbProject,
                    _pid, (string)eid);

                grid2.DataSource = null;
                grid2.DataSource = t;

                SetGrid2DisplayCol();

                grid2.Height = t.Rows.Count * (grid2.RowHeight + 3) + 28;
            }
        }

        // 屏蔽用户输入
        private void grid1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        #endregion 数据绑定

        #region 控件事件处理

        private void cbWrapText_CheckedChanged(object sender, EventArgs e)
        {
            SetWrapText(grid1, cbWrapText.Checked);
            SetWrapText(grid2, cbWrapText.Checked);
        }

        private void SetWrapText(C1TrueDBGrid grid, bool wrap)
        {
            foreach (C1DisplayColumn dc in grid.Splits[0].DisplayColumns)
                dc.Style.WrapText = wrap;
        }

        private void tree1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            NodeTagInfo tag = node.Tag as NodeTagInfo;
            if (tag == null)
                return;

            switch (tag.nodeType)
            {
                case NodeType.Project:
                    FirstLoadForm();
                    break;

                case NodeType.TestObject:
                    LoadEmptyTbl();
                    break;

                case NodeType.TestType:
                    LoadEmptyTbl();
                    break;

                case NodeType.TestItem:
                    LoadUCFromItem(tag.id);
                    break;

                default:
                    LoadEmptyTbl();
                    break;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SetCheck(true);
        }

        private void btnUnselAll_Click(object sender, EventArgs e)
        {
            SetCheck(false);
        }

        private void SetCheck(bool b)
        {
            DataTable tbl = GetBoundTbl();
            if (tbl == null)
                return;

            foreach (DataRow row in tbl.Rows)
                row["选用"] = b;
        }

        private DataTable GetBoundTbl()
        {
            object o = this.grid1.DataSource;
            if (o is DataSet)
                return (o as DataSet).Tables["Entity"];
            else if (o is DataTable)
                return o as DataTable;
            else
                return null;
        }

        private void tree2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            NodeTagInfo tag = node.Tag as NodeTagInfo;
            if (tag == null)
                return;

            switch (tag.nodeType)
            {
                case NodeType.TestItem:
                    this.btnImport.Enabled = true;
                    break;

                default:
                    this.btnImport.Enabled = false;
                    break;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            DataTable tbl = GetBoundTbl();
            if (tbl == null)
            {
                MessageBox.Show("无法获取绑定的数据表, 导入操作失败!", "操作失败", MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
                return;
            }

            NodeTagInfo tag = tree2.SelectedNode.Tag as NodeTagInfo;
            if (tag == null)
            {
                MessageBox.Show("无法获取目标测试项的附加信息!!", "操作失败", MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
                return;
            }

            if (tag.nodeType != NodeType.TestItem)
            {
                MessageBox.Show("目标节点非测试项!!", "操作失败", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            int startseq = TestItem.GetUCCount(MyBaseForm.dbProject, _pid, tag.id);
            int count = 0;
            foreach (DataRow row in tbl.Rows)
            {
                bool sel = (bool)row["选用"];
                if ((sel) &&
                    (!TestUsecase.IsInclude(MyBaseForm.dbProject, _pid, _curver, (string)row["实体ID"])))
                {
                    string uctid = TestUsecase.InsertToTestTbl(MyBaseForm.dbProject,
                        _pid, (string)row["实体ID"], ConstDef.execsta[0], string.Empty, _curver, _prever);

                    TestItem.InsertToRela(MyBaseForm.dbProject,
                        _pid, uctid, tag.id, ++startseq, true);

                    count++;
                }
            }
            MessageBox.Show("共导入测试用例: [" + count.ToString() + "]个!", "操作完成", MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
        }

        #endregion 控件事件处理
    }
}
