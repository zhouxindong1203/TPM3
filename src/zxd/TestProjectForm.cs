using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common.TrueDBGrid;
using Common;
using TPM3.Sys;
using Z1.tpm.DB;
using C1.Win.C1TrueDBGrid;
using System.Collections;
using Z1Utils.Controls;
using Z1.tpm;

namespace TPM3.zxd
{
    public partial class TestProjectForm : MyBaseForm
    {
        #region 窗体

        TrueDBGridAssist gridAssist1;
        public static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestProjectForm>();

        private DataTable _tbl;

        static TestProjectForm()
        {
            columnList1.Add("序号", 50);
            columnList1.Add("被测对象名称", 200);
            columnList1.Add("简写码", 80);
            columnList1.Add("被测对象版本", 120);
            columnList1.Add("测试级别", 120);
            columnList1.AddValidator(new NotNullValidator("被测对象名称"));
            columnList1.AddValidator(new NotNullValidator("简写码"));
            columnList1.AddValidator(new NotNullValidator("被测对象版本"));
        }

        public TestProjectForm()
        {
            InitializeComponent();

            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "序号");
            gridAssist1.columnList = columnList1;

            gridAssist1.rowPosition.GetTreeNodeKeyEvent += FrmCommonFunc.GetTreeNodeKey;

            gridAssist1.rowPosition.BeforeRowMoveDown += BeforeRowMove;
            gridAssist1.rowPosition.BeforeRowMoveUp += BeforeRowMove;

            gridAssist1.rowPosition.AfterRowMoveDown += RowMoveEventHandler;
            gridAssist1.rowPosition.AfterRowMoveUp += RowMoveEventHandler;
        }

        public override bool OnPageCreate()
        {
            this._tbl = TestObj.GetObjectsSPV(dbProject, (string)pid, (string)currentvid);
            if (this._tbl == null)
                return false;

            gridAssist1.DataSource = this._tbl;
            gridAssist1.rowPosition.tnc = tnForm.Nodes;
            gridAssist1.OnPageCreate();

            // 测试级别的ComboBox输入
            ValueItems items = grid1.Columns["测试级别"].ValueItems;
            FrmCommonFunc.TransTestLevel(items);
            grid1.Splits[0].DisplayColumns["测试级别"].DropDownList = true;

            ArrayList li = Z1.tpm.DB.CommonDB.GetSecretLevel(dbProject);
            foreach (string s in li)
                this.cmbDocSec.Items.Add(s);

            this.grid1.AllowRowSizing = (RowSizingEnum)us.rowSizeingList[0];

            return true;
        }

        // 获取"项目基本信息"
        private void LoadData()
        {
            this.txtbxProjName.Text = Z1.tpm.DB.ProjectInfo.GetProjName((string)pid, dbProject);
            this.txtbxProjSign.Text = Z1.tpm.DB.ProjectInfo.GetProjSign((string)pid, dbProject);
            this.txtbxTester.Text = Z1.tpm.DB.ProjectInfo.GetTester((string)pid, dbProject);
            this.cmbDocSec.Text = Z1.tpm.DB.ProjectInfo.GetDocSecret((string)pid, dbProject);

            this._datachanged = false;
        }

        private void TestProjectForm_Load(object sender, EventArgs e)
        {
            FrmCommonFunc.UniformGrid(this.grid1);

            LoadData();
        }

        public override bool OnPageClose(bool bClose)
        {
            SaveData();
            gridAssist1.OnPageClose();

            return SaveToDB();
        }

        private bool SaveToDB()
        {
            // 保存grid至数据库
            this.grid1.UpdateData();
            if (!TestObj.UpdateObjects(dbProject, (string)pid, (string)currentvid, this._tbl))
                return false;
            else
                return true;
        }

        // 保存"项目基本信息"
        private void SaveData()
        {
            if (this._datachanged)
                Z1.tpm.DB.ProjectInfo.SetTextContent((string)pid, dbProject, "项目信息", "密级", this.cmbDocSec.Text);
        }

        #endregion 窗体

        #region Grid操作

        bool BeforeRowMove(DataRow drCur, DataRow drPre)
        {
            return FrmCommonFunc.BeforeRowMove(this.grid1, drCur, drPre);
        }

        bool RowMoveEventHandler(DataRow drCur, DataRow drPre)
        {
            return FrmCommonFunc.AfterRowMove(tnForm, NodeType.TestObject, drCur, drPre);
        }

        private void grid1_OnAddNew(object sender, EventArgs e)
        {
            FrmCommonFunc.OnAddNew_TestObj(this.grid1);
        }

        // "被测对象名称"和"简写码"的唯一性检测
        private void grid1_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
        {
            string oldvalue = string.Empty;
            string newvalue = string.Empty;

            if (!DBNull.Value.Equals(e.OldValue))
                oldvalue = (string)e.OldValue;

            if (!DBNull.Value.Equals(e.Column.DataColumn.Value))
                newvalue = (string)e.Column.DataColumn.Value;

            if (oldvalue.Equals(newvalue)) // 未改动
                return;

            // "被测对象名称"唯一性检测
            if (e.Column.DataColumn.Caption.Equals("被测对象名称"))
            {
                if (TestObj.ExistObjNameFromTbl(this._tbl, (string)pid, newvalue))
                {
                    MessageBox.Show("已经存在此名称的被测对象, 请换用其他名称!", "被测对象名称重复", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    e.Column.DataColumn.Text = oldvalue;
                    return;
                }

                //更新树中被测对象名称
                foreach (TreeNode nd in tnForm.Nodes)
                {
                    NodeTagInfo taginfo = nd.Tag as NodeTagInfo;
                    if ((taginfo.id.Equals((string)grid1.Columns["ID"].Value)) &
                        (taginfo.nodeType == NodeType.TestObject))
                    {
                        taginfo.text = newvalue;
                        nd.Text = UIFunc.GenSections(tnForm, taginfo.order, ConstDef.SectionSep) + taginfo.text;
                        break;
                    }
                }
            }

            // "简写码"唯一性检测
            if (e.Column.DataColumn.Caption.Equals("简写码"))
            {
                if (TestObj.ExistObjAbbrFromTbl(this._tbl, (string)pid, newvalue))
                {
                    MessageBox.Show("被测对象简写码重复!", "数据输入错误", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    e.Column.DataColumn.Text = oldvalue;
                    return;
                }

                //更新相应树节点的附加信息
                foreach (TreeNode nd in tnForm.Nodes)
                {
                    NodeTagInfo taginfo = nd.Tag as NodeTagInfo;
                    if ((taginfo.id.Equals((string)grid1.Columns["ID"].Value)) &
                        (taginfo.nodeType == NodeType.TestObject))
                    {
                        taginfo.keySign = (string)grid1.Columns["简写码"].Value;
                        break;
                    }
                }
            }
        }

        private string _delobjid = null;
        private string _delobjtid = null;
        private void grid1_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            if (this.grid1.AddNewMode != AddNewModeEnum.NoAddNew) // 删除新增记录
                return;

            LeftTreeUserControl ltuc = FrmCommonFunc.GetParentFrm(this);
            if ((ltuc != null) && (ltuc is TestTreeForm))
            {
                if ((ltuc as TestTreeForm).IsRegressExec)
                {
                    TreeNode node = FrmCommonFunc.GetTreeNode(tnForm, (string)grid1.Columns["ID"].Value);
                    if(node != null)
                        if (node.Nodes.Count != 0)
                        {
                            MessageBox.Show("此被测对象下非空!!删除前请先删除其所属子节点!!", "操作失败", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            e.Cancel = true;
                            return;
                        }
                        else
                        {
                            if (!SaveToDB())    // 先将被测对象grid更新到数据库
                            {
                                e.Cancel = true;
                                return;
                            }

                            this._delobjid = (string)grid1.Columns["被测对象ID"].Value;
                            this._delobjtid = (string)grid1.Columns["ID"].Value;
                            return;
                        }
                }
            }

            if (MessageBox.Show("确实要删除此被测对象吗?\n(此被测对象下所有节点将一并删除!)",
                "删除被测对象", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            if (!SaveToDB())    // 先将被测对象grid更新到数据库
            {
                e.Cancel = true;
                return;
            }

            this._delobjid = (string)grid1.Columns["被测对象ID"].Value;
            this._delobjtid = (string)grid1.Columns["ID"].Value;
        }

        private void grid1_AfterDelete(object sender, EventArgs e)
        {
            /* 直接删除正在添加的新记录并不会触发本事件 */

            //更新数据库,并删除其下的所有子节点
            TestObj.DeleteObj(dbProject, (string)pid, (string)currentvid, this._delobjid);

            //删除对应树节点
            TreeNode node = FrmCommonFunc.GetTreeNode(tnForm, _delobjtid);
            UIFunc.DeleteTreeNode(node);

            this._tbl.AcceptChanges();
        }

        private void grid1_AfterInsert(object sender, EventArgs e)
        {
            // 向树中添加新节点
            TreeNode node = new TreeNode();

            NodeTagInfo taginfo = new NodeTagInfo();
            int index = this._tbl.Rows.Count - 1;

            taginfo.id = (string)_tbl.Rows[index]["ID"];
            taginfo.nodeType = NodeType.TestObject;
            taginfo.keySign = (string)_tbl.Rows[index]["简写码"];
            taginfo.order = (int)_tbl.Rows[index]["序号"];
            taginfo.text = (string)_tbl.Rows[index]["被测对象名称"];
            taginfo.verId = (string)currentvid;

            node.Text = UIFunc.GenSections(tnForm, taginfo.order, ConstDef.SectionSep) +
                taginfo.text;
            node.Tag = taginfo;
            UIFunc.SetNodeImageKey(node, NodeType.TestObject);

            tnForm.Nodes.Add(node);

            if (!tnForm.IsExpanded)
                tnForm.Expand();
        }

        #endregion Grid操作

        #region 事件处理

        private bool _datachanged = false;
        private void cmbDocSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            _datachanged = true;
        }

        #endregion 事件处理
    }
}