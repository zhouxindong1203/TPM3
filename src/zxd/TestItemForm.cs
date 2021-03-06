using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Balloon;
using C1.Win.C1TrueDBGrid;
using Common;
using Common.TrueDBGrid;
using DevComponents.DotNetBar;
using TPM3.Sys;
using TPM3.zxd.Helper;
using Z1.tpm;
using Z1.tpm.DB;
using TPM3.zxd.clu;

namespace TPM3.zxd
{
    public partial class TestItemForm : MyBaseForm
    {
        TrueDBGridAssist gridAssist1;
        TrueDBGridAssist gridAssist2;

        static ColumnPropList columnList2 = GridAssist.GetColumnPropList<TestItemForm>(3); // 测试用例grid

        static bool _showpop = true; // 操作提示窗只在程序运行第一次进入测试项窗体时出现

        private DataTable _tbl1;
        private DataTable _tbl2;
        public DataTable Tbl1
        {
            get
            {
                return this._tbl1;
            }
        }

        private NodeTagInfo _taginfo;
        private string _itemeid;

        public bool showUC; // 是否显示用例grid


        #region 属性定义

        public string ItemName
        {
            set
            {
                this.txtbxItemName.Text = value;
                this.expandablePanel1.TitleText = "测试项 [" + value + "]";
                this.grid1.Caption = "测试项子集 [" + value + "]";
                this.grid2.Caption = "测试用例集 [" + value + "]";

                _datachanged = true;
            }
        }

        public string Trace
        {
            get
            {
                if(this.txtbxTrace.Tag == null)
                    return string.Empty;
                else
                    return this.txtbxTrace.Tag.ToString();
            }
            set
            {
                this.txtbxTrace.Tag = value;
                this.txtbxTrace.DisplayValue();
            }
        }

        public string Prior
        {
            get
            {
                if(this.txtbxPrior.Tag == null)
                    return string.Empty;
                else
                    return this.txtbxPrior.Tag.ToString();
            }
            set
            {
                this.txtbxPrior.Tag = value;
                this.txtbxPrior.DisplayValue();
            }
        }

        public DataTable Tbl2
        {
            get
            {
                return _tbl2;
            }
        }
        #endregion 属性定义

        #region grid1/grid2的收缩与扩展

        public const int GridHeadHeight = 30;
        private bool _grid1collapsed = false;
        private bool _grid2collapsed = false;

        private void grid1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if(FrmCommonFunc.InGridHead(this.grid1, GridHeadHeight, e.Location))
                {
                    if(this._grid1collapsed) // 待扩展
                    {
                        this.splitContainer1.SplitterDistance = this.splitContainer1.Height / 2;
                        this._grid1collapsed = false;
                    }
                    else
                    {
                        this.splitContainer1.SplitterDistance = GridHeadHeight;
                        this._grid1collapsed = true;
                    }
                }
            }
        }

        private void grid2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if(FrmCommonFunc.InGridHead(this.grid2, GridHeadHeight, e.Location))
                {
                    if(this._grid2collapsed)
                    {
                        this.splitContainer1.SplitterDistance = this.splitContainer1.Height / 2;
                        this._grid2collapsed = false;
                    }
                    else
                    {
                        this.splitContainer1.SplitterDistance = this.splitContainer1.Height -
                            this.splitContainer1.Panel2MinSize;
                        this._grid2collapsed = true;
                    }
                }
            }
        }

        // 屏幕右下角显示操作提示窗体
        private AlertCustom m_AlertOnLoad = null;
        private void ShowLoadAlert()
        {
            m_AlertOnLoad = new AlertCustom();
            Rectangle r = Screen.GetWorkingArea(this);
            m_AlertOnLoad.Location = new Point(r.Right - m_AlertOnLoad.Width, r.Bottom - m_AlertOnLoad.Height);
            m_AlertOnLoad.AutoClose = true;
            m_AlertOnLoad.AutoCloseTimeOut = 10;
            m_AlertOnLoad.AlertAnimation = eAlertAnimation.BottomToTop;
            m_AlertOnLoad.AlertAnimationDuration = 300;
            m_AlertOnLoad.Show(false);

            _showpop = false;
        }

        #endregion grid1/grid2的收缩与扩展

        #region 窗体

        #region 窗体加载/关闭

        static TestItemForm()
        {
            columnList2.Add("测试用例类型", 30, false, "");
            columnList2.Add("序号", 80);
            columnList2.Add("测试用例名称", 300);

            columnList2.AddValidator(new NotNullValidator("测试用例名称"));
        }

        public TestItemForm()
        {
            InitializeComponent();
        }

        public override bool OnPageCreate()
        {
            // 获取附加信息
            _taginfo = tnForm.Tag as NodeTagInfo;
            if(_taginfo == null)
                throw (new InvalidOperationException("无法获取树节点附加信息!"));

            _itemeid = TestItem.GetEntityID(dbProject, _taginfo.id);
            if(string.Empty.Equals(_itemeid))
                throw (new InvalidOperationException("无法获取测试项实体ID!"));

            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "序号");
            gridAssist1.columnList = TestTypeForm.columnList2
                ;
            gridAssist1.rowPosition.GetTreeNodeKeyEvent += FrmCommonFunc.GetTreeNodeKey;
            gridAssist1.rowPosition.AfterRowMoveDown += RowMoveEventHandler;
            gridAssist1.rowPosition.AfterRowMoveUp += RowMoveEventHandler;
            gridAssist1.rowPosition.BeforeRowMoveDown += BeforeRowMove1;
            gridAssist1.rowPosition.BeforeRowMoveUp += BeforeRowMove1;

            gridAssist2 = new TrueDBGridAssist(grid2, "ID", "序号");
            gridAssist2.columnList = columnList2;

            gridAssist2.rowPosition.GetTreeNodeKeyEvent += FrmCommonFunc.GetTreeNodeKey;
            gridAssist2.rowPosition.AfterRowMoveDown += RowMoveEventHandler;
            gridAssist2.rowPosition.AfterRowMoveUp += RowMoveEventHandler;
            gridAssist2.rowPosition.BeforeRowMoveDown += BeforeRowMove2;
            gridAssist2.rowPosition.BeforeRowMoveUp += BeforeRowMove2;

            // "优先级"和"追踪关系"输入编辑器
            FrmCommonFunc.PriorLevelEditor(gridAssist1);
            FrmCommonFunc.TraceEditor(gridAssist1, currentvid);

            //--- 复制/粘贴支持(测试项)
            //gridAssist1.gridClipboard.CopyEvent += grid_CopyEvent1;
            //gridAssist1.gridClipboard.PasteEvent += grid_PasteEvent1;

            //--- 复制/粘贴支持(测试用例)
            //gridAssist2.gridClipboard.CopyEvent += grid_CopyEvent2;
            //gridAssist2.gridClipboard.PasteEvent += grid_PasteEvent2;

            // 数据绑定(grid1)
            this._tbl1 = TestItem.GetItemsFromItem(dbProject, (string)pid,
                (string)currentvid, _itemeid);
            gridAssist1.DataSource = this._tbl1;
            gridAssist1.rowPosition.tnc = tnForm.Nodes;
            gridAssist1.OnPageCreate();

            // 数据绑定(grid2)
            this._tbl2 = TestUsecase.GetUsecaseFromItem(dbProject, _taginfo.id, (string)pid,
                (string)currentvid);
            FrmCommonFunc.GenUCTypeCol(this._tbl2);

            gridAssist2.DataSource = this._tbl2;
            gridAssist2.rowPosition.tnc = tnForm.Nodes;
            gridAssist2.OnPageCreate();

            ValueItems items = grid2.Columns["测试用例类型"].ValueItems;
            FrmCommonFunc.TransUCType(items, imageList1);

            // 达到最大级数, 不能再创建子测试项
            int level = BusiLogic.GetTypeItemLevel(tnForm);
            if(level >= ConstDef.MaxLevel)
                this.splitContainer1.Panel1Collapsed = true;

            if(!showUC) // 不显示测试用例grid
            {
                if(splitContainer1.Panel1Collapsed)
                    splitContainer1.Visible = false;
                else
                    splitContainer1.Panel2Collapsed = true;
            }

            this.grid1.AllowRowSizing = (RowSizingEnum)us.rowSizeingList[2];
            this.grid2.AllowRowSizing = (RowSizingEnum)us.rowSizeingList[3];

            return true;
        }

        private bool _datachanged = false; // 测试项信息是否改动
        private void LoadData()
        {
            DataRow row = TestItem.GetTestItemInfo(dbProject, _taginfo.id);
            if(row == null)
                return;

            this.txtbxItemName.Text = BusiLogic.GetStringFromDB(row["测试项名称"]);
            this.txtbxAbbr.Text = BusiLogic.GetStringFromDB(row["简写码"]);
            this.txtbxDesc.Text = BusiLogic.GetStringFromDB(row["测试项说明及测试要求"]);
            this.txtbxMethod.Text = BusiLogic.GetStringFromDB(row["测试方法说明"]);
            this.tbEnough.Text = BusiLogic.GetStringFromDB(row["充分性要求"]);
            this.txtbx_certify.Text = BusiLogic.GetStringFromDB(row["评判标准"]);
            this.txtbx_constrain.Text = BusiLogic.GetStringFromDB(row["约束条件"]);
            Trace = BusiLogic.GetStringFromDB(row["追踪关系"]);
            this.txtbxTermi.Text = BusiLogic.GetStringFromDB(row["终止条件"]);
            Prior = BusiLogic.GetStringFromDB(row["优先级"]);

            this._datachanged = false;

            // 更新显示
            this.expandablePanel1.TitleText = "测试项 [" + this.txtbxItemName.Text + "]";
            this.grid1.Caption = "测试子项集 [" + this.txtbxItemName.Text + "]";
            this.grid2.Caption = "测试用例集 [" + this.txtbxItemName.Text + "]";
        }

        private void TestItemForm_Load(object sender, EventArgs e)
        {
            this.splitContainer1.Panel1MinSize = GridHeadHeight;
            this.splitContainer1.Panel2MinSize = GridHeadHeight;

            FrmCommonFunc.UniformGrid(this.grid1, 36, System.Drawing.Color.LightYellow);
            FrmCommonFunc.UniformGrid(this.grid2, 30, System.Drawing.Color.LightYellow);

            this.grid1.MarqueeStyle = MarqueeEnum.SolidCellBorder;

            this.grid1.Splits[0].DisplayColumns["测试项说明及测试要求"].Button = true;
            this.grid1.Splits[0].DisplayColumns["测试方法说明"].Button = true;
            this.grid1.Splits[0].DisplayColumns["追踪关系"].Button = true;
            this.grid1.Splits[0].DisplayColumns["终止条件"].Button = true;
            this.grid1.Splits[0].DisplayColumns["充分性要求"].Button = true;
            this.grid1.Splits[0].DisplayColumns["评判标准"].Button = true;
            this.grid1.Splits[0].DisplayColumns["约束条件"].Button = true;

            this.grid1.EditDropDown = true;

            FrmCommonFunc.TraceTextBox(this.txtbxTrace, currentvid);
            FrmCommonFunc.PriorTextBox(this.txtbxPrior);

            LoadData();

            this.grid2.Splits[0].DisplayColumns["测试用例类型"].Style.HorizontalAlignment = AlignHorzEnum.Center;

            if(_showpop)
                ShowLoadAlert();

            LoadLayout();

            SetInfoEditable();
        }

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
                            this.txtbxItemName.ReadOnly = true;
                            this.txtbxAbbr.ReadOnly = true;
                            this.txtbxDesc.ReadOnly = true;
                            this.txtbxMethod.ReadOnly = true;
                            this.txtbxTermi.ReadOnly = true;
                            this.txtbxPrior.ReadOnly = true;

                            this.txtbxItemName.BackColor = Color.WhiteSmoke;
                            this.txtbxAbbr.BackColor = Color.WhiteSmoke;
                            this.txtbxDesc.BackColor = Color.WhiteSmoke;
                            this.txtbxMethod.BackColor = Color.WhiteSmoke;
                            this.txtbxTermi.BackColor = Color.WhiteSmoke;
                            this.txtbxPrior.BackColor = Color.WhiteSmoke;
                        }
                    }
                }
            }
        }

        private void LoadLayout()
        {
            if (us.ItemInfoHeight != -1)
                splitter1.SplitPosition = us.ItemInfoHeight;

            if (showUC)
            {
                if (!float.IsNaN(us.ItemUCSplit))
                {
                    int newHeight = (int)(this.splitContainer1.Height * us.ItemUCSplit + 0.5);
                    Rectangle r = Screen.GetWorkingArea(this);
                    if ((newHeight > 0) && (newHeight <= r.Bottom))
                    {
                        try
                        {
                            this.splitContainer1.SplitterDistance = newHeight;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }
                }
            }
        }

        public void SaveData()
        {
            if(_datachanged)
            {
                TestItem.UpdateItemInfo(dbProject, _itemeid, _taginfo.id, txtbxItemName.Text,
                    txtbxAbbr.Text, txtbxDesc.Text, txtbxMethod.Text, Trace,
                    txtbxTermi.Text, Prior, tbEnough.Text, txtbx_certify.Text, txtbx_constrain.Text);
            }
            _datachanged = false;
        }

        public bool SaveToDB()
        {
            // 保存"测试子项"集数据
            this.grid1.UpdateData();
            if(!TestItem.UpdateItem(dbProject, (string)pid, (string)currentvid, _tbl1))
                return false;

            // 保存"测试用例"集数据
            if(showUC)
            {
                this.grid2.UpdateData();
                if(!TestUsecase.UpdateUsecase(dbProject, (string)pid, (string)currentvid, _tbl2))
                    return false;
            }

            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            SaveData();
            gridAssist1.OnPageClose();
            gridAssist2.OnPageClose();

            SaveLayout();

            return SaveToDB();
        }

        private void SaveLayout()
        {
            if (showUC)
                us.ItemUCSplit = (float)splitContainer1.SplitterDistance / (float)splitContainer1.Height;

            us.ItemInfoHeight = splitter1.SplitPosition;
        }

        #endregion 窗体加载/关闭

        #region grid行上下移动

        bool BeforeRowMove1(DataRow drCur, DataRow drPre)
        {
            return FrmCommonFunc.BeforeRowMove(grid1, drCur, drPre);
        }

        bool BeforeRowMove2(DataRow drCur, DataRow drPre)
        {
            return FrmCommonFunc.BeforeRowMove(grid2, drCur, drPre);
        }

        bool RowMoveEventHandler(DataRow drCur, DataRow drPre)
        {
            //交换测试类型树节点的order以及更新其章节号
            return FrmCommonFunc.AfterRowMove(tnForm, drCur, drPre);
        }

        #endregion grid行上下移动

        #region 复制/粘贴

        //void grid_CopyEvent1(object sender, EventArgs e)
        //{
        //    MessageBox.Show(sender.GetType().ToString());
        //}

        //void grid_PasteEvent1(object sender, EventArgs e)
        //{
        //}

        //void grid_CopyEvent2(object sender, EventArgs e)
        //{
        //    MessageBox.Show("grid2_copyevent!");
        //}

        //void grid_PasteEvent2(object sender, EventArgs e)
        //{
        //}

        #endregion 复制/粘贴

        #region 文本框

        private void txtbxItemName_TextChanged(object sender, EventArgs e)
        {
            _datachanged = true;
        }

        private void txtbxItemName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = this.txtbxItemName.Text;

            //更新树中测试项
            NodeTagInfo taginfo = tnForm.Tag as NodeTagInfo;
            if(taginfo == null)
                return;

            taginfo.text = input;
            tnForm.Text = UIFunc.GenSections(tnForm.Parent, taginfo.order, ConstDef.SectionSep) +
                taginfo.text;

            this.expandablePanel1.TitleText = "测试项 [" + input + "]";
            this.grid1.Caption = "测试项子集 [" + input + "]";
            this.grid2.Caption = "测试用例集 [" + input + "]";
        }

        private void txtbxAbbr_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = this.txtbxAbbr.Text;

            NodeTagInfo taginfo = tnForm.Tag as NodeTagInfo;
            if(taginfo == null)
                return;

            taginfo.keySign = input;
        }

        #endregion 文本框

        #endregion 窗体

        #region grid1

        private void grid1_ButtonClick(object sender, ColEventArgs e)
        {
            this.grid1.EditActive = true;
        }

        private void grid1_OnAddNew(object sender, EventArgs e)
        {
            object id = FunctionClass.NewGuid;
            object entityid = FunctionClass.NewGuid;

            this.grid1.Columns["ID"].Value = id;
            this.grid1.Columns["实体ID"].Value = entityid;
            this.grid1.Columns["测试项ID"].Value = entityid;
            this.grid1.Columns["项目ID"].Value = pid;
            this.grid1.Columns["测试版本"].Value = currentvid;
            this.grid1.Columns["序号"].Value = _tbl1.Rows.Count + 1;

            this.grid1.Columns["父节点ID"].Value = _itemeid;
            this.grid1.Columns["所属测试类型ID"].Value = DBNull.Value;
            //this.grid1.Columns["终止条件"].Value = BusiLogic.TermiCondi;
            this.grid1.Columns["创建版本ID"].Value = currentvid;

            grid1.Columns["充分性要求"].Value = DefaultText.GetItemSufficientDefaultText();
            this.grid1.Columns["终止条件"].Value = DefaultText.GetItemTerminateDefaultText();
            grid1.Columns["评判标准"].Value = DefaultText.GetItemEvaluateDefaultText();
            grid1.Columns["约束条件"].Value = DefaultText.GetItemConstrainDefaultText();

        }

        private void grid1_AfterColUpdate(object sender, ColEventArgs e)
        {
            string col = e.Column.Name;
            if(col.Equals("测试项名称"))
                FrmCommonFunc.GridAfterColUpdate(this.grid1, "ID", "测试项名称", string.Empty, tnForm);
            else if(col.Equals("简写码"))
                FrmCommonFunc.GridAfterColUpdate(this.grid1, "ID", string.Empty, "简写码", tnForm);
        }

        private void grid1_AfterInsert(object sender, EventArgs e)
        {
            //向树中添加新节点
            TreeNode node = new TreeNode();

            NodeTagInfo taginfo = new NodeTagInfo();
            int index = _tbl1.Rows.Count - 1;

            taginfo.id = (string)_tbl1.Rows[index]["ID"];
            taginfo.nodeType = NodeType.TestItem;
            taginfo.keySign = (string)_tbl1.Rows[index]["简写码"];
            taginfo.order = FrmCommonFunc.LocateInsertItem(tnForm);
            taginfo.text = (string)_tbl1.Rows[index]["测试项名称"];
            taginfo.verId = (string)currentvid;

            node.Text = UIFunc.GenSections(tnForm, taginfo.order, ConstDef.SectionSep) +
                taginfo.text;
            node.Tag = taginfo;
            UIFunc.SetNodeImageKey(node, NodeType.TestItem);

            tnForm.Nodes.Insert(taginfo.order - 1, node);

            if(!tnForm.IsExpanded)
                tnForm.Expand();

        }

        private string _delitemeid = null;
        private string _delitemtid = null;
        private void grid1_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            if(this.grid1.AddNewMode != AddNewModeEnum.NoAddNew) // 删除新增记录
                return;

            if (!SaveToDB())
            {
                e.Cancel = true;
                return;
            }

            LeftTreeUserControl ltuc = FrmCommonFunc.GetParentFrm(this);
            if ((ltuc != null) && (ltuc is TestTreeForm))
            {
                if ((ltuc as TestTreeForm).IsRegressExec)
                {
                    string itemeid = this.grid1.Columns["实体ID"].Value as string;
                    if (!TestItem.IsSameVer(dbProject, itemeid, currentvid as string))
                    {
                        InputText input = new InputText();
                        if (input.ShowDialog() == DialogResult.OK)
                        {
                            object trace = TestItem.GetItemTrace(dbProject, this.grid1.Columns["ID"].Value as string);
                            string objtid = FrmCommonFunc.GetBelongObjID(tnForm);
                            Regress.InsertUntest(dbProject, pid, itemeid, 2, input.InputReason, trace, currentvid, objtid);

                            FrmCommonFunc.OnBeforeDelete_TestItem(this.grid1, null, ref _delitemeid, ref _delitemtid, false);
                        }
                        else
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                    else
                    {
                        if (FrmCommonFunc.OnBeforeDelete_TestItem(this.grid1, null, ref _delitemeid, ref _delitemtid, true))
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
                else
                {
                    if (FrmCommonFunc.OnBeforeDelete_TestItem(this.grid1, null, ref _delitemeid, ref _delitemtid, true))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void grid1_AfterDelete(object sender, EventArgs e)
        {
            /* 直接删除正在添加的新记录并不会触发本事件 */
            FrmCommonFunc.OnAfterDelete_TestItem(this.grid1, tnForm, _delitemtid, _delitemeid, this._tbl1);
        }
        #endregion grid1

        #region grid2

        private void grid2_OnAddNew(object sender, EventArgs e)
        {
            object id = FunctionClass.NewGuid;
            object eid = FunctionClass.NewGuid;
            object rid = FunctionClass.NewGuid;

            this.grid2.Columns["序号"].Value = _tbl2.Rows.Count + 1;
            this.grid2.Columns["ID"].Value = id;
            this.grid2.Columns["项目ID"].Value = pid;
            this.grid2.Columns["测试用例ID"].Value = eid;
            this.grid2.Columns["执行状态"].Value = ConstDef.execsta[0];
            this.grid2.Columns["执行结果"].Value = string.Empty;
            //this.grid2.Columns["测试时间"].Value            = DateTime.Today; // 注意:不能用DateTime.Now, 其数据格式与数据库不匹配
            this.grid2.Columns["测试用例类型"].Value = 2;
            this.grid2.Columns["测试版本"].Value = currentvid;

            this.grid2.Columns["实体ID"].Value = eid;
            this.grid2.Columns["测试过程终止条件"].Value = /*BusiLogic.StepTermiCondi*/DefaultText.GetCaseTerminateDefaultText();
            this.grid2.Columns["测试结果评估标准"].Value = /*BusiLogic.Evaluation*/DefaultText.GetCasePassDefaultText();
            this.grid2.Columns["创建版本ID"].Value = currentvid;

            this.grid2.Columns["关系表ID"].Value = rid;
            this.grid2.Columns["实测ID"].Value = id;
            this.grid2.Columns["测试项ID"].Value = _taginfo.id;
            this.grid2.Columns["直接所属标志"].Value = true;
        }

        private void grid2_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
        {
            // "测试用例名称"唯一性检测
            string oldvalue = string.Empty;
            string newvalue = string.Empty;

            if(!DBNull.Value.Equals(e.OldValue))
                oldvalue = (string)e.OldValue;

            if(!DBNull.Value.Equals(e.Column.DataColumn.Value))
                newvalue = (string)e.Column.DataColumn.Value;

            if(oldvalue.Equals(newvalue)) // 未改动
                return;

            // "测试用例名称"唯一性检测
            if(e.Column.DataColumn.Caption.Equals("测试用例名称"))
            {
                if(TestUsecase.ExistUCNameFromTbl(this._tbl2, newvalue))
                {
                    MessageBox.Show("已经存在此名称的测试用例, 请换用其他名称!", "测试用例名称重复", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    e.Column.DataColumn.Text = oldvalue;
                    return;
                }

                //更新树中测试用例名称(包括所有其快捷方式)
                FrmCommonFunc.UpdateUCName(tnForm.TreeView, (string)grid2.Columns["ID"].Value, newvalue);
            }
        }

        private string _itemtid = null;
        private string _uctid = null;
        private bool _shortcut = false;
        private void grid2_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            if (this.grid2.AddNewMode != AddNewModeEnum.NoAddNew) // 删除新增记录
                return;

            if (!SaveToDB())
            {
                e.Cancel = true;
                return;
            }

            LeftTreeUserControl ltuc = FrmCommonFunc.GetParentFrm(this);
            if ((ltuc != null) && (ltuc is TestTreeForm))
            {
                if ((ltuc as TestTreeForm).IsRegressExec)
                {
                    string uceid = this.grid2.Columns["实体ID"].Value as string;
                    if (!TestUsecase.IsSameVer(dbProject, uceid, currentvid as string))
                    {
                        InputText input = new InputText();
                        if (input.ShowDialog() == DialogResult.OK)
                        {
                            string objtid = FrmCommonFunc.GetBelongObjID(tnForm);
                            string itemtid = FrmCommonFunc.GetParentID(tnForm);
                            Regress.InsertUntest(dbProject, pid, uceid, 1, input.InputReason, string.Empty, currentvid, objtid + "," + itemtid);
                            FrmCommonFunc.OnBeforeDelete_TestUC(this.grid2, null, ref _itemtid, ref _uctid);
                        }
                        else
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                    else
                    {
                        FrmCommonFunc.OnBeforeDelete_TestUC(this.grid2, null, ref _itemtid, ref _uctid);
                    }
                }
                else
                {
                    FrmCommonFunc.OnBeforeDelete_TestUC(this.grid2, null, ref _itemtid, ref _uctid);
                }
            }

            _shortcut = !((bool)this.grid2.Columns["直接所属标志"].Value);
        }

        private void grid2_AfterDelete(object sender, EventArgs e)
        {
            FrmCommonFunc.OnAfterDelete_TestUC(_shortcut, tnForm, _itemtid, _uctid, this._tbl2);
        }

        private void grid2_AfterInsert(object sender, EventArgs e)
        {
            FrmCommonFunc.OnAfterInsert_TestUC(this._tbl2, tnForm);
        }

        private void grid2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string tid = (string)grid2.Columns["ID"].Value;
            e.Handled = IsShortcut(tid);
        }

        private bool IsShortcut(string tid)
        {
            foreach(TreeNode node in tnForm.Nodes)
            {
                NodeTagInfo tag = node.Tag as NodeTagInfo;
                if(tag != null)
                {
                    if(tag.id.Equals(tid))
                        return tag.IsShortcut;
                }
            }

            return false;
        }

        #endregion grid2
    }
}