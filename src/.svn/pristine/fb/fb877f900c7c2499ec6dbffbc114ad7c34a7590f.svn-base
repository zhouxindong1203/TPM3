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
    public partial class TestObjForm : MyBaseForm
    {
        #region 窗体

        TrueDBGridAssist gridAssist1;
        public static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestObjForm>();

        private DataTable _tbl;
        private DataTable _tblobjs; // 被测对象表(用于对象名称和简写码唯一性检测)
        public DataTable TblObjs
        {
            get
            {
                return _tblobjs;
            }
        }

        public string ObjName
        {
            set
            {
                this.txtbxObjName.Text = value;

                string filter = string.Format("被测对象名称='{0}' AND 项目ID='{1}'", _objname, pid);
                DataRow[] rows = this._tblobjs.Select(filter);
                if ((rows != null) && (rows.Length > 0))
                {
                    rows[0]["被测对象名称"] = value;
                    this._objname = value;

                    this.expandablePanel1.TitleText = "被测对象 [" + value + "]";
                    _datachanged = true;
                }
            }
        }
        
        static TestObjForm()
        {
            columnList1.Add("序号", 50);
            columnList1.Add("测试类型名称", 200);
            columnList1.Add("简写码", 80);
            columnList1.Add("子节点类型", 120);

            columnList1.AddValidator(new NotNullValidator("测试类型名称"));
            columnList1.AddValidator(new NotNullValidator("简写码"));
        }

        public TestObjForm()
        {
            InitializeComponent();

            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "序号");
            gridAssist1.columnList = columnList1;

            gridAssist1.rowPosition.GetTreeNodeKeyEvent += FrmCommonFunc.GetTreeNodeKey;

            gridAssist1.rowPosition.AfterRowMoveDown += RowMoveEventHandler;
            gridAssist1.rowPosition.AfterRowMoveUp += RowMoveEventHandler;

            gridAssist1.rowPosition.BeforeRowMoveDown += BeforeRowMove;
            gridAssist1.rowPosition.BeforeRowMoveUp += BeforeRowMove;

            // 定制"测试类型名称"输入编辑器
            FrmCommonFunc.TestTypeEditor(gridAssist1, mapList_afterRowSelectEvent);
        }

        public override bool OnPageCreate()
        {
            NodeTagInfo taginfo = tnForm.Tag as NodeTagInfo;
            if (taginfo == null)
                return false;

            string objeid = TestObj.GetEntityID(dbProject, taginfo.id);
            if (string.Empty.Equals(objeid))
                return false;

            this._tbl = TestType.GetTypesFromObj(dbProject, (string)pid, (string)currentvid, objeid);
            if (this._tbl == null)
                return false;

            this._tblobjs = TestObj.GetObjectsSPV(dbProject, (string)pid, (string)currentvid);

            gridAssist1.DataSource = this._tbl;
            gridAssist1.rowPosition.tnc = tnForm.Nodes;
            gridAssist1.OnPageCreate();

            this._tbl.Columns["所属被测对象ID"].DefaultValue = objeid;
            this._tbl.Columns["父测试类型ID"].DefaultValue = DBNull.Value;
            this._tbl.Columns["子节点类型"].DefaultValue = 2;

            // 列"子节点类型"的ComboBox输入及显示文本与对应值的设置
            ValueItems items = grid1.Columns["子节点类型"].ValueItems;
            FrmCommonFunc.TransSubnodeType(items);

            foreach (string s in ConstDef.testlevel)
                this.cmbTestLevel.Items.Add(s);

            this.grid1.AllowRowSizing = (RowSizingEnum)us.rowSizeingList[1];

            return true;
        }

        // _objname, _objabbr用于被测对象名称和简写码唯一性检测
        private string _objname;
        private string _objabbr;

        private bool _datachanged = false;
        // 获取"被测对象"相关信息
        private void LoadData()
        {
            DataRow row = TestObj.GetObjInfo(dbProject, _taginfo.id);
            if (row == null)
                return;

            this.txtbxObjName.Text = row["被测对象名称"].Equals(DBNull.Value) ? string.Empty : (string)row["被测对象名称"];
            this.txtbxObjAbbr.Text = row["简写码"].Equals(DBNull.Value) ? string.Empty : (string)row["简写码"];
            this.txtbxObjVer.Text = (string)row["被测对象版本"];
            this.cmbTestLevel.Text = (string)row["测试级别"];
            this._datachanged = false;

            this._objname = this.txtbxObjName.Text;
            this._objabbr = this.txtbxObjAbbr.Text;

            this.expandablePanel1.TitleText = "被测对象 [" + this.txtbxObjName.Text + "]";
        }

        private NodeTagInfo _taginfo;
        private void TestObjForm_Load(object sender, EventArgs e)
        {
            _taginfo = tnForm.Tag as NodeTagInfo;
            if (_taginfo == null)
                return;

            FrmCommonFunc.UniformGrid(this.grid1);

            LoadData();

            // "子节点类型"禁止用户输入
            C1DataColumn col = this.grid1.Columns["子节点类型"];
            this.grid1.Splits[0].DisplayColumns[col].DropDownList = true;

            SetInfoEditable();
        }

        // 检测当前被测对象信息能否被编辑(回归测试时继承的对象不能编辑!!)
        private void SetInfoEditable()
        {
            LeftTreeUserControl ltuc = FrmCommonFunc.GetParentFrm(this);
            if ((ltuc != null) && (ltuc is TestTreeForm))
            {
                if ((ltuc as TestTreeForm).IsRegressExec)   // 回归测试执行阶段
                {
                    NodeTagInfo tag = tnForm.Tag as NodeTagInfo;
                    if (tag != null)
                    {
                        if (tag.IsRegressCreate)            // 继承而来
                        {
                            this.txtbxObjName.ReadOnly = true;
                            this.txtbxObjVer.ReadOnly = true;
                            this.txtbxObjAbbr.ReadOnly = true;
                            this.cmbTestLevel.Enabled = true;

                            this.txtbxObjName.BackColor = Color.WhiteSmoke;
                            this.txtbxObjVer.BackColor = Color.WhiteSmoke;
                            this.txtbxObjAbbr.BackColor = Color.WhiteSmoke;
                            this.cmbTestLevel.BackColor = Color.WhiteSmoke;
                        }
                    }
                }
            }
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
            if (!TestType.UpdateType(dbProject, (string)pid, (string)currentvid, this._tbl))
                return false;
            else
                return true;
        }

        // 保存"被测对象"信息
        private void SaveData()
        {
            if (_datachanged)
            {
                string eid = TestObj.GetEntityID(dbProject, _taginfo.id);
                Z1.tpm.DB.TestObj.UpdateObjInfo(dbProject, eid, this.txtbxObjName.Text, this.txtbxObjAbbr.Text,
                    this.txtbxObjVer.Text, this.cmbTestLevel.Text);
            }
        }


        #endregion 窗体

        #region Grid操作

        /// <summary>
        /// 用户选择 测试类型名称 完毕,更新简写码
        /// </summary>
        void mapList_afterRowSelectEvent(ColumnRefMap cm, object key)
        {
            FrmCommonFunc.AfterChangeTestType(cm.dt, this.grid1, tnForm, key);
        }

        bool BeforeRowMove(DataRow drCur, DataRow drPre)
        {
            return FrmCommonFunc.BeforeRowMove(this.grid1, drCur, drPre);
        }

        bool RowMoveEventHandler(DataRow drCur, DataRow drPre)
        {
            return FrmCommonFunc.AfterRowMove(tnForm, NodeType.TestType, drCur, drPre);
        }

        private void grid1_OnAddNew(object sender, EventArgs e)
        {
            FrmCommonFunc.OnAddNew_TestType(this.grid1);
        }

        private void grid1_KeyPress(object sender, KeyPressEventArgs e)
        {
            List<string> filter = new List<string>();
            filter.Add("测试类型名称");
            filter.Add("简写码");

            e.Handled = FrmCommonFunc.OnKeyPress_TestType(this.grid1, filter);
        }

        private string _deltypeeid = null;
        private string _deltypetid = null;
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
                    if (node != null)
                        if (node.Nodes.Count != 0)
                        {
                            MessageBox.Show("此测试类型下非空!!删除前请先删除其所属子节点!!", "操作失败", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            e.Cancel = true;
                            return;
                        }
                }
            }

            FrmCommonFunc.ProcBeforeDelete proc = new FrmCommonFunc.ProcBeforeDelete(SaveToDB);

            bool ret = FrmCommonFunc.OnBeforeDelete_TestType(this.grid1, proc, ref _deltypeeid, ref _deltypetid);
            if (ret)
                e.Cancel = true;
        }

        private void grid1_AfterDelete(object sender, EventArgs e)
        {
            /* 直接删除正在添加的新记录并不会触发本事件 */
            
            FrmCommonFunc.OnAfterDelete_TestType(this.grid1, tnForm, _deltypetid, _deltypeeid, this._tbl);
        }

        private void grid1_AfterInsert(object sender, EventArgs e)
        {
            FrmCommonFunc.OnAfterInsert_TestType(this._tbl, tnForm);
        }

        private void grid1_ComboSelect(object sender, ColEventArgs e)
        {
            if (this.grid1.AddNewMode != AddNewModeEnum.NoAddNew)
                return;

            FrmCommonFunc.BeforeChangeSubtype(this.grid1, tnForm);
        }

        #endregion Grid操作

        #region 事件处理

        private void txtbxObjName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = this.txtbxObjName.Text;
            if (input.Equals(this._objname))
                return;

            if (TestObj.ExistObjNameFromTbl(this._tblobjs, (string)pid, input)) // 被测对象名称重复
            {
                MessageBox.Show("已经存在此名称的被测对象, 请换用其他名称!", "被测对象名称重复", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                this.txtbxObjName.Text = this._objname;
                e.Cancel = true;
            }
            else
            {
                string filter = string.Format("被测对象名称='{0}' AND 项目ID='{1}'", _objname, pid);
                DataRow[] rows = this._tblobjs.Select(filter);
                if ((rows != null) && (rows.Length > 0))
                {
                    rows[0]["被测对象名称"] = input;
                    this._objname = input;

                    //更新树中被测对象名称
                    NodeTagInfo taginfo = tnForm.Tag as NodeTagInfo;
                    taginfo.text = input;
                    tnForm.Text = UIFunc.GenSections(tnForm.Parent, taginfo.order, ConstDef.SectionSep) +
                        taginfo.text;

                    this.expandablePanel1.TitleText = "被测对象 [" + this.txtbxObjName.Text + "]";
                }
            }
        }

        private void txtbxObjName_TextChanged(object sender, EventArgs e)
        {
            this._datachanged = true;
        }

        private void txtbxObjAbbr_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = this.txtbxObjAbbr.Text;
            if (input.Equals(this._objabbr))
                return;

            if (TestObj.ExistObjAbbrFromTbl(this._tblobjs, (string)pid, input)) // 被测对象简写码重复
            {
                MessageBox.Show("被测对象简写码重复!", "数据输入错误", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                this.txtbxObjAbbr.Text = this._objabbr;
                e.Cancel = true;
            }
            else
            {
                string filter = string.Format("简写码='{0}' AND 项目ID='{1}'", _objabbr, pid);
                DataRow[] rows = this._tblobjs.Select(filter);
                if ((rows != null) && (rows.Length > 0))
                {
                    rows[0]["简写码"] = input;
                    this._objabbr = input;

                    // 更新树节点附加信息
                    NodeTagInfo taginfo = tnForm.Tag as NodeTagInfo;
                    taginfo.keySign = input;
                }
            }
        }

        #endregion 事件处理
    }
}