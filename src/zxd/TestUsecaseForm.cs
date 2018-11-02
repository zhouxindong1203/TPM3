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
using TPM3.zxd.pbl;
using System.Reflection;

namespace TPM3.zxd
{
    public partial class TestUsecaseForm : MyBaseForm
    {
        private DataTable _tbl;
        private DataTable _tblucs;
        public DataTable TblUCs
        {
            get
            {
                return _tblucs;
            }
        }
        private TrueDBGridAssist gridAssist1;
        private string _pblspl;
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestUsecaseForm>(1); // "设计"
        static ColumnPropList columnList2 = GridAssist.GetColumnPropList<TestUsecaseForm>(2); // "执行"

        public static ExistPblRepForm g_existpbldlg = null;

        public Dictionary<string, List<string>[]> p_AccDic;

        #region 窗体

        private PageType _pageType;
        private NodeTagInfo _taginfo;
        private string _uceid;
        private string _belobjid;
        private string _belobjname;
        private string _belobjabbr;
        private TestTreeForm _ttf;

        #region 属性定义

        public string DesMethod
        {
            get
            {
                if (this.ddttMethod.Tag == null)
                    return string.Empty;
                else
                    return this.ddttMethod.Tag.ToString();
            }
            set
            {
                this.ddttMethod.Tag = value;
                this.ddttMethod.DisplayValue();
            }
        }

        public string Desc
        {
            get
            {
                return this.txtbxDesc.Text;
            }
        }

        public string Init
        {
            get
            {
                return this.txtbxInit.Text;
            }
        }

        public string Constraint
        {
            get
            {
                return this.txtbxConstraint.Text;
            }
        }

        public string Term
        {
            get
            {
                return this.txtbxTerm.Text;
            }
        }

        public string Cert
        {
            get
            {
                return this.txtbxPassCert.Text;
            }
        }

        public string Trace
        {
            get
            {
                return this.txtbxTrace.Text;
            }
        }

        public string Unexec
        {
            get
            {
                return this.txtbxUnexecReason.Text;
            }
        }

        public string Person
        {
            get
            {
                if (this.ddttPerson.Tag == null)
                    return string.Empty;
                else
                    return this.ddttPerson.Tag.ToString();
            }
            set
            {
                this.ddttPerson.Tag = value;
                this.ddttPerson.DisplayValue();
            }
        }

        public string Tester
        {
            get
            {
                if(ddttTester.Tag == null)
                    return string.Empty;
                else
                {
                    return ddttTester.Tag.ToString();
                }
            }

            set
            {
                ddttTester.Tag = value;
                ddttTester.DisplayValue();
            }
        }

        public DateTime TestTime
        {
            get
            {
                if (dtpTestDate.Value == DateTimePicker.MinimumDateTime)
                    return DateTime.MinValue;

                return this.dtpTestDate.Value;
            }
            set
            {
                if (value.Equals(DateTime.MinValue))
                    this.dtpTestDate.Value = DateTimePicker.MinimumDateTime;
                else
                    this.dtpTestDate.Value = value;
            }
        }

        public string UCName
        {
            get
            {
                return this.txtbxUCName.Text;
            }
            set
            {
                this.txtbxUCName.Text = value;
                _datachanged = true;

                string filter = string.Format("测试用例名称='{0}'", _taginfo.text);
                DataRow[] rows = this._tblucs.Select(filter);
                if ((rows != null) && (rows.Length > 0))
                    rows[0]["测试用例名称"] = value;
                FrmCommonFunc.UpdateUCName(tnForm.TreeView, _taginfo.id, value);
            }
        }

        public string UCSign
        {
            get
            {
                return this.txtbxSign.Text;
            }
        }

        public string ExecStatus
        {
            get
            {
                return this.txtbxExecStatus.Text;
            }
            set
            {
                this.txtbxExecStatus.Text = value;
            }
        }

        public string ExecResult
        {
            get
            {
                return this.cmbExecResult.Text;
            }
            set
            {
                this.cmbExecResult.Text = value;
            }
        }

        public DataTable Tbl
        {
            get
            {
                return _tbl;
            }
        }

        public string UCTid
        {
            get
            {
                return _taginfo.id;
            }
        }

        public PageType pageType
        {
            get
            {
                return _pageType;
            }
        }

        private string _designperson;
        public string DesignPerson
        {
            get
            {
                return _designperson;
            }
        }

        private string _markclr;
        public string MarkClr
        {
            get
            {
                return _markclr;
            }
            set
            {
                _markclr = value;
            }
        }

        #endregion 属性定义

        #region 窗体加载/关闭

        static TestUsecaseForm()
        {
            //int ww = Screen.PrimaryScreen.WorkingArea.Width;

            columnList1.Add("序号", 50);
            columnList1.Add("输入及操作", /*(int)(ww * 0.3)*/200);
            columnList1.Add("期望结果", /*(int)(ww * 0.2)*/150);
            columnList1.AddValidator(new NotNullValidator("输入及操作"));

            columnList2.Add("序号", 50);
            columnList2.Add("输入及操作", /*(int)(ww * 0.25)*/200);
            columnList2.Add("期望结果", /*(int)(ww * 0.1)*/150);
            columnList2.Add("实测结果", /*(int)(ww * 0.1)*/150);
            columnList2.Add("问题?", 50);
            columnList2.Add("问题标识", 100, false);
            columnList2.AddValidator(new NotNullValidator("输入及操作"));
        }
        
        // TestTreeForm的右侧窗体
        public TestUsecaseForm(PageType pageType)
        {
            InitializeComponent();

            this._pageType = pageType;
            this._pblspl = ConstDef.PblSplitter();
        }

        #region 利用用例ID弹出用例窗体

        private string _uctid;
        private string _itemtid;
        // 直接利用用例ID弹出对应窗体
        public TestUsecaseForm(string uctid, string itemtid)
        {
            InitializeComponent();

            _uctid = uctid;
            _itemtid = itemtid;

            TreeViewUtils tvu = new TreeViewUtils();
            EnumTreeViewProc proc = new EnumTreeViewProc(InittnForm);
            tvu.FindTreeViewLeaf(TPM3.zxd.ExecStatus.g_ttf.tree, proc);
            if (!BusiLogic.AssertNotNull(tnForm, "无法定位此用例!", "操作失败"))
                throw (new ArgumentException());

            _pageType = PageType.TestCasePerform;
            _pblspl = ConstDef.PblSplitter();
            OnPageCreate();

            grid2.AllowAddNew = false;
            grid2.AllowDelete = false;
            grid2.AllowUpdate = false;
        }

        public TestUsecaseForm(string uctid)
            : this(uctid, null)
        {
        }

        private bool InittnForm(TreeNode tn)
        {
            NodeTagInfo tag = tn.Tag as NodeTagInfo;
            if (tag == null)
                return true;

            if (tag.nodeType != NodeType.TestCase)
                return true;

            if (_itemtid == null)   // 导航至用例本身
            {
                if ((tag.id.Equals(_uctid)) &&
                    (!tag.IsShortcut))
                {
                    tnForm = tn;
                    return false;
                }
                else
                    return true;                  
            }
            else    // 导航至指定ID(考虑父节点及快捷方式)
            {
                string itemtid = (tn.Parent.Tag as NodeTagInfo).id;
                if ((tag.id.Equals(_uctid)) &&
                    (itemtid.Equals(_itemtid)))
                {
                    tnForm = tn;
                    return false;
                }
                else
                    return true;
            }
        }

        #endregion 利用用例ID弹出用例窗体

        public override bool OnPageCreate()
        {
            // 获取附加信息
            _taginfo = tnForm.Tag as NodeTagInfo;
            if (_taginfo == null)
                throw (new InvalidOperationException("无法获取树节点附加信息!"));

            _uceid = TestUsecase.GetUCEntityID(dbProject, _taginfo.id);
            if (string.Empty.Equals(_uceid))
                throw (new InvalidOperationException("无法获取测试用例实体ID!"));

            gridAssist1 = new TrueDBGridAssist(grid2, null, "序号");
            if (_pageType == PageType.TestCasePerform)
                gridAssist1.columnList = columnList2;
            else
                gridAssist1.columnList = columnList1;

            // 行移动
            gridAssist1.rowPosition.GetTreeNodeKeyEvent += FrmCommonFunc.GetTreeNodeKey;
            gridAssist1.rowPosition.AfterRowMoveDown += RowMoveEventHandler;
            gridAssist1.rowPosition.AfterRowMoveUp += RowMoveEventHandler;
            gridAssist1.rowPosition.BeforeRowMoveDown += BeforeRowMove;
            gridAssist1.rowPosition.BeforeRowMoveUp += BeforeRowMove;

            // 数据绑定
            this._tbl = TestUsecase.GetSteps(dbProject, (string)pid, (string)currentvid, TestUsecase.GetUCEntityID(
                dbProject, _taginfo.id));
            if (_tbl == null)
                return false;

            if (_pageType == PageType.TestCasePerform)
                InitPblCol();

            gridAssist1.DataSource = _tbl;
            gridAssist1.OnPageCreate();

            return true;
        }

        private void InitPblCol()
        {
            this._tbl.Columns.Add("问题?", typeof(bool));
            this._tbl.Columns.Add("问题标识", typeof(string));

            foreach (DataRow row in this._tbl.Rows)
            {
                if ((DBNull.Value.Equals(row["问题报告单ID"])) ||
                    (string.Empty.Equals(row["问题报告单ID"])))
                {
                    row["问题?"] = false;
                }
                else
                {
                    row["问题?"] = true;
                    row["问题标识"] = CommonDB.GenPblSignForStep(dbProject, _pblspl, (string)row["问题报告单ID"]);
                }
            }
        }

        private bool _datachanged = false; // 测试用例信息是否改动
        public bool DataChanged
        {
            set
            {
                _datachanged = value;
            }
        }
        private void LoadData()
        {
            DataRow row = TestUsecase.GetUCInfo(dbProject, _taginfo.id);
            if (row == null)
                return;

            this.txtbxUCName.Text       = BusiLogic.GetStringFromDB(row["测试用例名称"]);
            this.txtbxDesc.Text         = BusiLogic.GetStringFromDB(row["用例描述"]);
            this.txtbxInit.Text         = BusiLogic.GetStringFromDB(row["用例的初始化"]);
            this.txtbxConstraint.Text   = BusiLogic.GetStringFromDB(row["前提和约束"]);
            this.txtbxTerm.Text         = BusiLogic.GetStringFromDB(row["测试过程终止条件"]);
            this.txtbxPassCert.Text     = BusiLogic.GetStringFromDB(row["测试结果评估标准"]);
            DesMethod                   = BusiLogic.GetStringFromDB(row["所使用的设计方法"]);
            this.txtbxTrace.Text        = BusiLogic.GetStringFromDB(row["追踪关系"]);
            this._designperson          = BusiLogic.GetStringFromDB(row["设计人员"]);
            //if (_pageType == PageType.TestCaseDesign)
                Person                  = BusiLogic.GetStringFromDB(row["设计人员"]);
            //else if (_pageType == PageType.TestCasePerform)
                Tester                  = BusiLogic.GetStringFromDB(row["测试人员"]);

            //TestTime                    = BusiLogic.GetDateTimeFromDB(row["测试时间"]);
            BusiLogic.GetTestTime(this.dtpTestDate, row["测试时间"]);
            this.txtbxExecStatus.Text   = BusiLogic.GetStringFromDB(row["执行状态"]);
            this.cmbExecResult.Text     = BusiLogic.GetStringFromDB(row["执行结果"]);
            this.txtbxUnexecReason.Text = BusiLogic.GetStringFromDB(row["未执行原因"]);
            this.txtbxTrace.Text        = BusiLogic.InitTrace(tnForm);
            MarkClr                     = BusiLogic.GetStringFromDB(row["标记"]);

            this.txtbxSign.Text = BusiLogic.GenUCSign(tnForm);

            this._datachanged = false;

            // 更新显示
            this.expandablePanel1.TitleText = "测试用例 [" + this.txtbxUCName.Text + "]";
            this.grid2.Caption = "测试步骤 [" + this.txtbxUCName.Text + "]";

            // 只能在"用例设计"添加新步骤
            if ((_pageType == PageType.TestCasePerform) &&
                (!Z1.tpm.DB.ProjectInfo.HasPreviousVer(dbProject, (string)pid, (string)currentvid)))
                this.grid2.AllowAddNew = false;
            else
                this.grid2.AllowAddNew = true;

            this._belobjid = FrmCommonFunc.GetBelongObjID(tnForm, out _belobjname, out _belobjabbr);

            _ttf = FrmCommonFunc.GetParentFrm(this) as TestTreeForm;
        }

        private Graphics _graphics;
        private int _singleheight;
        private IList<int> _rowsheight = new List<int>();
        private Font _font;
        private int _unitwidth;
        private void TestUsecaseForm_Load(object sender, EventArgs e)
        {
            switch (_pageType)
            {
                case PageType.TestCaseDesign:
                    this.tableLayoutPanel2.Visible = false;
                    this.expandablePanel1.Height -= this.tableLayoutPanel2.Height;
                    //this.lblPerson.Text = "设计人员";
                    break;

                case PageType.TestCasePerform:
                    //this.lblPerson.Text = "测试人员";
                    break;

                default:
                    throw (new InvalidOperationException("非法运行模式!!"));
            }

            foreach (string s in ConstDef.execrlt)
                this.cmbExecResult.Items.Add(s);

            FrmCommonFunc.DesignMethodEditor(this.ddttMethod.cm);
            FrmCommonFunc.PersonEditor(this.ddttPerson.cm);
            FrmCommonFunc.PersonEditor(ddttTester.cm);

            FrmCommonFunc.UniformGrid(this.grid2, 36, System.Drawing.Color.LightYellow);
            SetGridStyle(this.grid2);

            _graphics = this.grid2.CreateGraphics();
            _singleheight = (int)this.grid2.Font.GetHeight() + 4;
            _font = this.grid2.Font;
            _unitwidth = this.grid2.Splits[0].DisplayColumns["输入及操作"].Width - 20;

            LoadData();

            if (_pageType == PageType.TestCasePerform)
            {
                // 处理未关闭的"提交问题"窗体
                if ((g_existpbldlg != null) && (!g_existpbldlg.IsDisposed))
                {
                    if (g_existpbldlg.UCtid.Equals(_taginfo.id))
                    {
                        UpdateNoPbl(g_existpbldlg.StepID, true);
                        g_existpbldlg.ParentFrm = this;
                    }
                }
            }

            p_AccDic = TestUsecase.GetAccForUC(dbProject, (string)pid, (string)currentvid, _uceid);
            TPM3.zxd.ExecStatus.g_AccFolder = Z1.tpm.DB.ProjectInfo.GetTextContent(dbProject, (string)pid, (string)currentvid, "项目信息", "附件文件夹");

            _tblucs = TestUsecase.GetUsecaseForProj(dbProject, (string)pid, (string)currentvid);

            if (BusiLogic.NodeIsShortcut(tnForm))
                DisableInput();

            DrawCurNode(false);

            //if (TPM3.zxd.ExecStatus.g_TestUsecaseFormALink != null)
            //{
            //    TPM3.zxd.ExecStatus.g_TestUsecaseFormALink.Close();
            //    TPM3.zxd.ExecStatus.g_TestUsecaseFormALink = null;
            //}
            LoadLayout();
            SetInfoEditable();

            InitRowsHeight();
        }

        // 初始化各测试步骤的行高
        private void InitRowsHeight()
        {
            _rowsheight.Clear();

            foreach (DataRow row in _tbl.Rows)
            {
                _rowsheight.Add(Z1Utils.ZString.GetStringLines(BusiLogic.GetStringFromDB(row["输入及操作"]),
                    _unitwidth, _graphics, _font));
            }

            for (int i = 0; i < _rowsheight.Count; i++)
            {
                if ((UserSetting.Default.AutoCaseRowSize > 0) && 
                    (_rowsheight[i] > UserSetting.Default.AutoCaseRowSize))
                    _rowsheight[i] = UserSetting.Default.AutoCaseRowSize;

                if (_rowsheight[i] == 1) // 对单行文本做加宽处理
                    _rowsheight[i]++;

                this.grid2.Splits[0].Rows[i].Height = _rowsheight[i] * _singleheight;
            }
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
                            this.txtbxUCName.ReadOnly = true;
                            this.txtbxDesc.ReadOnly = true;
                            this.txtbxInit.ReadOnly = true;
                            this.txtbxConstraint.ReadOnly = true;
                            this.txtbxTerm.ReadOnly = true;
                            this.txtbxPassCert.ReadOnly = true;
                            this.ddttMethod.Enabled = false;

                            this.txtbxUCName.BackColor = Color.WhiteSmoke;
                            this.txtbxDesc.BackColor = Color.WhiteSmoke;
                            this.txtbxInit.BackColor = Color.WhiteSmoke;
                            this.txtbxConstraint.BackColor = Color.WhiteSmoke;
                            this.txtbxTerm.BackColor = Color.WhiteSmoke;
                            this.txtbxPassCert.BackColor = Color.WhiteSmoke;
                            this.ddttMethod.BackColor = Color.WhiteSmoke;
                        }
                    }
                }
            }
        }

        private void LoadLayout()
        {
            if (us.UsecaseInfoHeight != -1)
                this.splitter1.SplitPosition = us.UsecaseInfoHeight;
        }

        private void DisableInput()
        {
            grid2.AllowAddNew = false;
            grid2.AllowDelete = false;
            grid2.AllowUpdate = false;
            this.grid2.ButtonClick -= new C1.Win.C1TrueDBGrid.ColEventHandler(this.grid2_ButtonClick);

            this.txtbxDesc.ReadOnly = true;
            this.txtbxInit.ReadOnly = true;
            this.txtbxConstraint.ReadOnly = true;
            this.txtbxTerm.ReadOnly = true;
            this.txtbxPassCert.ReadOnly = true;
            this.ddttMethod.Enabled = false;
            this.ddttPerson.Enabled = false;
            this.ddttTester.Enabled = false;
            this.dtpTestDate.Enabled = false;
            this.txtbxExecStatus.ReadOnly = true;
            this.cmbExecResult.Enabled = false;
            this.txtbxUnexecReason.ReadOnly = true;
            this.txtbxTrace.ReadOnly = true;

            HyperLinkActivateTB proc = new HyperLinkActivateTB(LinkForTB);
            TextBoxHyperLink tbhl = new TextBoxHyperLink(this.txtbxUCName, proc);
            tbhl = new TextBoxHyperLink(this.txtbxSign, proc);

            this.txtbxUCName.BackColor          = System.Drawing.Color.WhiteSmoke;
            this.txtbxSign.BackColor            = System.Drawing.Color.WhiteSmoke;
            this.txtbxDesc.BackColor            = System.Drawing.Color.WhiteSmoke;
            this.txtbxInit.BackColor            = System.Drawing.Color.WhiteSmoke;
            this.txtbxConstraint.BackColor      = System.Drawing.Color.WhiteSmoke;
            this.txtbxTerm.BackColor            = System.Drawing.Color.WhiteSmoke;
            this.txtbxPassCert.BackColor        = System.Drawing.Color.WhiteSmoke;
            this.txtbxExecStatus.BackColor      = System.Drawing.Color.WhiteSmoke;
            this.txtbxUnexecReason.BackColor    = System.Drawing.Color.WhiteSmoke;
            this.txtbxTrace.BackColor           = System.Drawing.Color.WhiteSmoke;
            this.ddttMethod.BackColor           = System.Drawing.Color.WhiteSmoke;
            this.ddttPerson.BackColor           = System.Drawing.Color.WhiteSmoke;
            this.ddttTester.BackColor           = System.Drawing.Color.WhiteSmoke;
            this.cmbExecResult.BackColor        = System.Drawing.Color.WhiteSmoke;
        }

        private void LinkForTB()
        {
            TreeViewUtils tvu = new TreeViewUtils();
            EnumTreeViewProc proc = new EnumTreeViewProc(FindIt);
            tvu.FindTreeViewLeaf(tnForm.TreeView, proc);
        }

        private bool FindIt(TreeNode tn)
        {
            NodeTagInfo tag = tn.Tag as NodeTagInfo;
            if (tag == null)
                return true;

            if (tag.nodeType != NodeType.TestCase)
                return true;

            if (tag.IsShortcut)
                return true;

            if (tag.id.Equals(_taginfo.id))
            {
                tnForm.TreeView.SelectedNode = tn;
                return false;
            }
            else
                return true;
        }

        private System.Resources.ResourceManager rm;
        private void SetGridStyle(C1TrueDBGrid grid)
        {
            grid.MarqueeStyle = C1.Win.C1TrueDBGrid.MarqueeEnum.SolidCellBorder;

            grid.Splits[0].DisplayColumns["输入及操作"].Button = true;
            grid.Splits[0].DisplayColumns["期望结果"].Button = true;
            grid.Splits[0].DisplayColumns["实测结果"].Button = true;

            //grid.Columns["输入及操作"].NumberFormat = "FormatText Event";
            //grid.Columns["期望结果"].NumberFormat = "FormatText Event";
            //grid.Columns["实测结果"].NumberFormat = "FormatText Event";
            grid.Splits[0].DisplayColumns["输入及操作"].FetchStyle = true;
            grid.Splits[0].DisplayColumns["期望结果"].FetchStyle = true;
            grid.Splits[0].DisplayColumns["实测结果"].FetchStyle = true;

            Assembly a = Assembly.GetExecutingAssembly();
            rm = new System.Resources.ResourceManager("TPM3.zxd.clu.tpm3zxd", a);
            Image im = (Image)rm.GetObject("AccBmp");
            grid.Columns["输入及操作"].ButtonPicture = im;
            grid.Columns["期望结果"].ButtonPicture = im;
            grid.Columns["实测结果"].ButtonPicture = im;

            grid.EditDropDown = true;
            grid.AllowColMove = false;
            grid.AllowRowSizing = RowSizingEnum.IndividualRows;

            if(_pageType == PageType.TestCasePerform)
                grid.Splits[0].DisplayColumns["问题标识"].FetchStyle = true;
            grid.FetchRowStyles = true;

        }

        private void SaveData()
        {
            if (_datachanged)
            {
                bool bdesign = false;
                if (_pageType == PageType.TestCaseDesign)
                    bdesign = true;

                UCInfo info = new UCInfo();
                info.id         = _taginfo.id;
                info.name       = this.txtbxUCName.Text;
                info.desc       = this.txtbxDesc.Text;
                info.init       = this.txtbxInit.Text;
                info.constraint = this.txtbxConstraint.Text;
                info.term       = this.txtbxTerm.Text;
                info.cert       = this.txtbxPassCert.Text;
                info.method     = DesMethod;
                info.trace      = this.txtbxTrace.Text;

                //if (bdesign)
                    info.designperson = Person;
                //else
                    info.testperson = Tester;

                info.testtime = BusiLogic.SetTestTime(this.dtpTestDate);
                info.execsta = this.txtbxExecStatus.Text;
                info.execrlt = this.cmbExecResult.Text;
                info.unexec = this.txtbxUnexecReason.Text;
                info.markclr = MarkClr;

                TestUsecase.UpdateUCInfo(dbProject, _uceid, _taginfo.id, info, bdesign);

                _datachanged = false;
            }
        }

        private bool SaveToDB()
        {            
            // 保存"测试步骤"数据
            this.grid2.UpdateData();
            if (!TestUsecase.UpdateStep(dbProject, (string)pid, (string)currentvid, _tbl))
                return false;

            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            if (!DataOK())
                return false;

            DrawCurNode(true);
            SaveData();
            gridAssist1.OnPageClose();

            SaveLayout();
            return SaveToDB();
        }

        private void SaveLayout()
        {
            us.UsecaseInfoHeight = splitter1.SplitPosition;
        }

        private void TestUsecaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !DataOK();
        }

        public bool DataOK()
        {
            if (this.UCName.Equals(string.Empty))
                return false;

            //if (this._pageType == PageType.TestCaseDesign)
            //{
            //    if ((Person == null) || (Person.Equals(string.Empty)))
            //    {
            //        MessageBox.Show("\'设计人员\'不能为空!!", "数据错误", MessageBoxButtons.OK,
            //             MessageBoxIcon.Error);
            //        return false;
            //    }
            //}

            return true;
        }

        #endregion 窗体加载/关闭

        #region 行移动

        bool BeforeRowMove(DataRow drCur, DataRow drPre)
        {
            return FrmCommonFunc.BeforeRowMove(grid2, drCur, drPre);
        }

        bool RowMoveEventHandler(DataRow drCur, DataRow drPre)
        {
            //交换测试类型树节点的order以及更新其章节号
            return FrmCommonFunc.AfterRowMove(tnForm, drCur, drPre);
        }


        #endregion 行移动

        #endregion 窗体

        #region 文本框

        private void txtbxUCName_TextChanged(object sender, EventArgs e)
        {
            _datachanged = true;
        }

        private void txtbxUCName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = this.UCName;
            if (input.Equals(_taginfo.text))
                return;

            if (input.ToLower().Equals(_taginfo.text.ToLower()))
            {
                FrmCommonFunc.UpdateUCName(tnForm.TreeView, _taginfo.id, input);
                return;
            }

            if (input.Equals(string.Empty))
            {
                MessageBox.Show("测试用例名称不能为空!!", "数据错误", MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
                this.UCName = _taginfo.text;
                e.Cancel = true;
            }

            if (TestUsecase.ExistUCNameFromTbl(_tblucs, input))
            {
                MessageBox.Show("已经存在此名称的测试用例, 请换用其他名称!", "测试用例名称重复", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                this.txtbxUCName.Text = _taginfo.text;
                e.Cancel = true;
            }
            else
            {
                string filter = string.Format("测试用例名称='{0}'", _taginfo.text);
                DataRow[] rows = this._tblucs.Select(filter);
                if ((rows != null) && (rows.Length > 0))
                    rows[0]["测试用例名称"] = input;

                FrmCommonFunc.UpdateUCName(tnForm.TreeView, _taginfo.id, input);
            }
        }

        #endregion 文本框

        #region grid

        #region grid操作

        private string _newtid;
        private void grid2_OnAddNew(object sender, EventArgs e)
        {
            object id = FunctionClass.NewGuid;
            object entityid = FunctionClass.NewGuid;
            _newtid = id as string;

            this.grid2.Columns["序号"].Value = this._tbl.Rows.Count + 1;
            // 实体表
            this.grid2.Columns["实体ID"].Value = entityid;
            this.grid2.Columns["测试用例ID"].Value = _uceid;
            this.grid2.Columns["项目ID"].Value = pid;
            this.grid2.Columns["创建版本ID"].Value = currentvid;

            // 实测表
            this.grid2.Columns["ID"].Value = id;
            this.grid2.Columns["过程ID"].Value = entityid;
            this.grid2.Columns["测试版本"].Value = currentvid;

            if (_pageType == PageType.TestCasePerform)
            {
                this.grid2.Columns["问题?"].Value = false;
                this.grid2.Columns["问题标识"].Value = string.Empty;
            }
        }
        
        private void grid2_AfterInsert(object sender, EventArgs e)
        {
            // 更新测试用例的执行与否, 通过与否
            string execstatus = this.txtbxExecStatus.Text;
            string execresult = this.cmbExecResult.Text;

            BusiLogic.UCAfterAddNewStep(_taginfo.id, ref execstatus, ref execresult, tnForm);

            this.txtbxExecStatus.Text = execstatus;
            this.cmbExecResult.Text = execresult;
            this._datachanged = true;

            List<string>[] liarr = new List<string>[3];
            List<string> li = new List<string>();
            liarr[0] = li;
            li = new List<string>();
            liarr[1] = li;
            li = new List<string>();
            liarr[2] = li;

            p_AccDic.Add(_newtid, liarr);
        }

        private void grid2_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            _rowsheight.RemoveAt(this.grid2.Row);

            if (this.grid2.AddNewMode != AddNewModeEnum.NoAddNew) // 删除新增记录
                return;

            SaveToDB();

            string steptid = (string)this.grid2.Columns["ID"].Value;
            string stepeid = (string)this.grid2.Columns["实体ID"].Value;
                        
            // 检测待删除的步骤是否是当前测试版本所创建
            //if (!TestUsecase.StepIsSameVer(dbProject, stepeid, currentvid as string))
            //{
            //    e.Cancel = true;
            //    return;
            //}

            TestUsecase.DeleteStep(dbProject, steptid, stepeid);

            p_AccDic.Remove(steptid);
        }

        private void grid2_AfterDelete(object sender, EventArgs e)
        {            
            this._tbl.AcceptChanges();

            string execsta = this.txtbxExecStatus.Text;
            string execrlt = this.cmbExecResult.Text;

            BusiLogic.UpdateAfterDelStep(this._taginfo.id, ref execsta, ref execrlt, this._tbl,
                tnForm);

            this.txtbxExecStatus.Text = execsta;
            this.cmbExecResult.Text = execrlt;
            this._datachanged = true;

            for (int i = 0; i < _rowsheight.Count; i++)
            {
                if ((UserSetting.Default.AutoCaseRowSize > 0) &&
                    (_rowsheight[i] > UserSetting.Default.AutoCaseRowSize))
                    _rowsheight[i] = UserSetting.Default.AutoCaseRowSize;

                if (_rowsheight[i] == 1) // 对单行文本做加宽处理
                    _rowsheight[i]++;

                this.grid2.Splits[0].Rows[i].Height = _rowsheight[i] * _singleheight;
            }
        }

        // 提交/删除问题、附件显示的去除
        private int _row;
        private void grid2_BeforeColEdit(object sender, BeforeColEditEventArgs e)
        {
            if (this.grid2.AddNewMode != AddNewModeEnum.NoAddNew)
                _rowsheight.Add(_singleheight * 2);

            _row = this.grid2.Row;

            C1DataColumn dc = e.Column.DataColumn;

            // 继承而来的测试步骤的"输入及操作"和"期望结果"不可编辑
            if (this.grid2.AddNewMode == AddNewModeEnum.NoAddNew)
            {
                if (_ttf.IsRegressExec)
                {
                    if ((dc.Caption.Equals("输入及操作")) ||
                        (dc.Caption.Equals("期望结果")))
                    {
                        string stepeid = (string)this.grid2.Columns["实体ID"].Value;
                        if (!TestUsecase.StepIsSameVer(dbProject, stepeid, currentvid as string))
                        {
                            MessageBox.Show("继承的测试步骤\"输入及操作\"和\"期望结果\"不能编辑!",
                                "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }

            // 确保问题提交的正确性
            if (dc.Caption.Equals("实测结果"))
            {
                if (grid2.AddNewMode != AddNewModeEnum.NoAddNew)
                {
                    MessageBox.Show("新测试步骤未提交前不能编辑\"实测结果\"!", "操作错误",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }

            if (dc.Caption.Equals("问题?"))
            {
                string strHasPbl = (string)dc.Value; // 此时为编辑后的新值
                if (strHasPbl.Equals("True")) // 提交问题
                {
                    // "实测结果"不能为空
                    //string strActRlt = (string)this.grid2.Columns["实测结果"].Value;
                    string strActRlt = BusiLogic.GetStringFromDB(this.grid2.Columns["实测结果"].Value);
                    if (string.Empty.Equals(strActRlt))
                    {
                        string steptid = (string)this.grid2.Columns["ID"].Value;
                        if (!TestUsecase.ActualTestHasAcc(dbProject, steptid))
                        {
                            MessageBox.Show("\"实测结果\"为空时不能提交问题!!", "操作提示", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            e.Cancel = true;
                            return;
                        }
                    }

                    string strInput = (string)this.grid2.Columns["输入及操作"].Value;
                    if (string.Empty.Equals(strInput))
                    {
                        MessageBox.Show("\"输入及操作\"不能为空!!", "操作提示", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }

                    if (HasExistPblDlg(e))
                        return;

                    using (DlgSubmitProblem dlg = new DlgSubmitProblem())
                    {
                        if (DialogResult.OK == dlg.ShowDialog())
                        {
                            switch (dlg.SubmitType)
                            {
                                case 0: // 提交新问题
                                    using (ProblemRepForm prf = new ProblemRepForm(this))
                                    {
                                        if (DialogResult.OK == prf.ShowDialog())
                                        {
                                            this.grid2.Columns["问题报告单ID"].Text = prf.PblID;
                                            if (this.cmbExecResult.Text.Equals(ConstDef.execrlt[1]))
                                            {
                                                this.cmbExecResult.Text = ConstDef.execrlt[2];
                                                this._datachanged = true;
                                                BusiLogic.UpdateUCIcon(_taginfo.id, this.txtbxExecStatus.Text,
                                                    this.cmbExecResult.Text, tnForm);
                                            }
                                            this.grid2.Columns["问题标识"].Text = prf.PblSign;
                                        }
                                        else
                                        {
                                            e.Cancel = true;
                                            return;
                                        }
                                    }
                                    break;

                                case 1: // 从已有问题中选择
                                    List<PblRep> li = CommonDB.GetPblRepsForObj(dbProject,
                                        _belobjid, _belobjname, _belobjabbr, (string)pid, (string)currentvid);
                                    if (li.Count == 0)
                                    {
                                        MessageBox.Show("当前被测对象下无可用的问题报告单数据!!", "操作失败", MessageBoxButtons.OK,
                                             MessageBoxIcon.Stop);
                                        return;
                                    }

                                    string stepid = (string)this.grid2.Columns["ID"].Value;
                                    g_existpbldlg = new ExistPblRepForm(li, this, stepid, _taginfo.id, (string)currentvid);

                                    g_existpbldlg.ProblemSign = -1;
                                    g_existpbldlg.ProblemCategory = -1;
                                    g_existpbldlg.ProblemLevel = -1;

                                    g_existpbldlg.Show();

                                    break; 

                                default:

                                    break;
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
                else // 删除问题
                {
                    if (DialogResult.Yes == MessageBox.Show("确实认为不存在问题吗?\n若选\"是\"将删除该问题", "确认删除", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        string stepid = (string)this.grid2.Columns["ID"].Value;
                        string pblid = (string)this.grid2.Columns["问题报告单ID"].Value;

                        CommonDB.DelPblForStep(dbProject, stepid, pblid);
                        grid2.Columns["问题标识"].Value = string.Empty;
                        grid2.Columns["问题报告单ID"].Value = DBNull.Value;

                        // 更新"执行结果"
                        string execrlt = ConstDef.execrlt[1];
                        foreach (DataRow row in _tbl.Rows)
                        {
                            if (row.RowState != DataRowState.Deleted)
                            {
                                if (!stepid.Equals((string)row["ID"]))
                                {
                                    if(!DBNull.Value.Equals(row["问题报告单ID"]))
                                        if (!string.Empty.Equals(row["问题报告单ID"]))
                                        {
                                            execrlt = ConstDef.execrlt[2];
                                            break;
                                        }
                                }
                            }
                        }
                        this.cmbExecResult.Text = execrlt;
                        this._datachanged = true;
                        BusiLogic.UpdateUCIcon(_taginfo.id, ExecStatus, execrlt, tnForm);
                    }
                    else
                        e.Cancel = true;
                } // 删除问题
            }
        }

        private void grid2_AfterColEdit(object sender, ColEventArgs e)
        {
            C1DataColumn dc = e.Column.DataColumn;

            if (dc.Caption.Equals("输入及操作"))
            {
                int newlines = Z1Utils.ZString.GetStringLines(dc.Text, _unitwidth, _graphics, _font);
                _rowsheight[_row] = newlines;

                if ((UserSetting.Default.AutoCaseRowSize > 0) &&
                    (_rowsheight[_row] > UserSetting.Default.AutoCaseRowSize))
                    _rowsheight[_row] = UserSetting.Default.AutoCaseRowSize;

                if (_rowsheight[_row] == 1)
                    _rowsheight[_row]++;

                this.grid2.Splits[0].Rows[_row].Height = _rowsheight[_row] * _singleheight;
            }
        }

        // "实测结果"列编辑
        private void grid2_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
        {
            C1DataColumn dc = e.Column.DataColumn;
            string steptid = (string)grid2.Columns["ID"].Value;

            //if (_ttf.IsRegressExec)
            //{
            //    if ((dc.Caption.Equals("输入及操作")) ||
            //        (dc.Caption.Equals("期望结果")))
            //    {
            //        string stepeid = (string)this.grid2.Columns["实体ID"].Value;

            //        if (!TestUsecase.StepIsSameVer(dbProject, stepeid, currentvid as string))
            //        {
            //            e.Cancel = true;
            //            return;
            //        }
            //    }
            //}1

            if ((dc.Caption.Equals("实测结果")) &&
                (!TestUsecase.ActualTestHasAcc(dbProject, (string)grid2.Columns["ID"].Value))) // "实测结果"不带附件
            {
                // 存在问题时, "实测结果"不能为空
                bool haspbl = (bool)grid2.Columns["问题?"].Value;
                if (haspbl)
                {
                    if (string.Empty.Equals(dc.Text))
                    {
                        MessageBox.Show("存在问题时, \"实测结果\"不能为空!!", "操作提示", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        dc.Value = e.OldValue;

                        return;
                    }
                }

                string newsta = BusiLogic.CheckExecSta(this._tbl, steptid, dc.Text);
                DataTable tblcopy = this._tbl.Copy();
                tblcopy.AcceptChanges();
                string newrlt = BusiLogic.CheckExecRlt(newsta, tblcopy);

                BusiLogic.UpdateUCIcon(_taginfo.id, newsta, newrlt, tnForm);
                this.txtbxExecStatus.Text = newsta;
                this.cmbExecResult.Text = newrlt;
                this._datachanged = true;
            }
        }

        // 检测是否正在提交问题
        private bool HasExistPblDlg(BeforeColEditEventArgs e)
        {
            if ((g_existpbldlg != null) && (!g_existpbldlg.IsDisposed))
            {
                MessageBox.Show("请先处理之前提交尚未完成的问题报告!", "操作提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                e.Cancel = true;
                g_existpbldlg.BringToFront();
                g_existpbldlg.Show();

                return true;
            }
            else
                return false;
        }

        // 通过"问题提交单"的用户操作结果更新有无问题(从已有问题中选择)
        public void UpdateNoPbl(string stepid, bool value)
        {
            string filter = string.Format("ID=\'{0}\'", stepid);
            DataRow[] rows = _tbl.Select(filter);
            if ((rows != null) && (rows.Length != 0))
                rows[0]["问题?"] = value;
        }

        // 设置测试步骤的"问题报告单ID"值
        public void SetStepPblId(string stepid, string pblid, string pblsign, int newcount)
        {
            string filter = string.Format("ID=\'{0}\'", stepid);
            DataRow[] rows = _tbl.Select(filter);
            if ((rows != null) && (rows.Length != 0))
            {
                rows[0]["问题报告单ID"] = pblid;
                rows[0]["问题标识"] = pblsign;

                CommonDB.AddPblCount(dbProject, pblid, newcount);
            }
        }

        // 检测某个测试步骤是否正在
        public bool StepAvaliable(string stepid)
        {
            string filter = string.Format("ID=\'{0}\'", stepid);
            DataRow[] rows = _tbl.Select(filter);
            if ((rows != null) && (rows.Length != 0))
                return true;
            else
                return false;
        }

        // 对于拥有附件的列设置其背景色
        private void grid2_FetchCellStyle(object sender, FetchCellStyleEventArgs e)
        {
            if (this.grid2.AddNewMode != AddNewModeEnum.NoAddNew)
                return;

            if (e.Column.Name.Equals("问题标识"))
            {
                e.CellStyle.ForeColor = Color.Blue;
                e.CellStyle.Font = new Font(this.grid2.Font, FontStyle.Underline);
                return;
            }

            string stepid = (string)this.grid2[e.Row, "ID"];
            int col = -1;
            switch (e.Column.Name)
            {
                case "输入及操作":
                    col = 0;
                    break;

                case "期望结果":
                    col = 1;
                    break;

                case "实测结果":
                    col = 2;
                    break;
            }
            if (col == -1)
                return;


            if (p_AccDic != null && p_AccDic.ContainsKey(stepid))
            {
                if (p_AccDic[stepid][col].Count != 0)
                    e.CellStyle.BackColor = System.Drawing.Color.MistyRose;
            }
        }

        // 存在问题的测试步骤
        private void grid2_FetchRowStyle(object sender, FetchRowStyleEventArgs e)
        {
            if (this._pageType == PageType.TestCasePerform)
            {
                if (this.grid2[e.Row, "问题?"].ToString().Equals("True"))
                {
                    /*Font fnt = new Font(e.CellStyle.Font.Name,
                        e.CellStyle.Font.Size, FontStyle.Bold);
                    e.CellStyle.Font = fnt;*/
                    e.CellStyle.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (!this.grid2[e.Row, "实测结果"].ToString().Equals(""))
                        e.CellStyle.ForeColor = System.Drawing.Color.Blue;
                }
            }

            
        }

        private void grid2_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor cursor = Cursors.Default;

            int row, col;
            if (this.grid2.CellContaining(e.X, e.Y, out row, out col))
            {
                // 检测是否为最底下的待新加行
                if ((row < 0) ||
                    (row > this.grid2.RowCount - 1))
                {
                    this.grid2.Cursor = cursor;
                    return;
                }

                if (this.grid2.Splits[0].DisplayColumns[col].Name.Equals("问题标识"))
                {
                    using (Graphics g = this.grid2.CreateGraphics())
                    {
                        Rectangle r = this.grid2.Splits[0].GetCellBounds(row, col);
                        string text = this.grid2[row, "问题标识"].ToString();
                        int width = (int)g.MeasureString(text, this.grid2.Font).Width;
                        if (e.X - r.Left <= width)
                            cursor = Cursors.Hand;
                    }
                }
            }

            this.grid2.Cursor = cursor;
        }

        private void grid2_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.grid2.Cursor == Cursors.Hand)
            {
                int irow, icol;

                if (this.grid2.CellContaining(e.X, e.Y, out irow, out icol))
                {
                    this.grid2.Row = irow;

                    string pblid = (string)this.grid2[irow, "问题报告单ID"];

                    //GridHyperLink.g_gridForALink = this.grid2;

                    //BusiLogic.SelectMainFrmNode("zxd.pbl.PblRepsForm", "测试实施-问题报告单");
                    //(MainForm.mainFrm.CurrentSelectedForm as PblRepsForm).LocatePbl(pblid as object);

                    //GridHyperLink.g_gridForALink = null;

                    // 切换窗体前保存数据
                    OnPageClose(false);

                    string url = string.Format("zxd.pbl.PblRepsForm?id={0}", pblid);
                    MainForm.mainFrm.DelayCreateForm(url);
                }
            }
        }

        #endregion grid操作

        #region 附件

        public string p_steptid;
        public string p_stepeid;
        public string p_belong;
        public int p_row;
        public string p_rlttext;
        private void grid2_ButtonClick(object sender, ColEventArgs e)
        {
            if (this.grid2.AddNewMode != AddNewModeEnum.NoAddNew)
                return;

            p_steptid = (string)grid2.Columns["ID"].Value;
            p_stepeid = (string)grid2.Columns["实体ID"].Value;
            p_belong = e.Column.DataColumn.Caption;
            p_row = grid2.Row;
            if (p_belong.Equals("实测结果"))
            {
                p_rlttext = BusiLogic.GetStringFromDB(grid2.Columns["实测结果"].Value);
            }

            Rectangle r = grid2.Splits[0].GetCellBounds(grid2.Row, e.ColIndex);
            r.Y = r.Y + grid2.Bounds.Y + r.Height - ConstDef.Offset_line;

            this.radarStatusEditorDDControl1.Bounds = r;
            this.radarStatusEditorDDControl1.OpenDropDown();
        }

        #endregion 附件

        // 动态调整"输入及操作""期望结果""实测结果"输入列宽
        private void grid2_Resize(object sender, EventArgs e)
        {
            if (_pageType == PageType.TestCaseDesign)
            {
                this.grid2.Splits[0].DisplayColumns["输入及操作"].Width = (int)(this.grid2.Width * 0.5);
                this.grid2.Splits[0].DisplayColumns["期望结果"].Width = (int)(this.grid2.Width * 0.4);
            }
            else if (_pageType == PageType.TestCasePerform)
            {
                this.grid2.Splits[0].DisplayColumns["输入及操作"].Width = (int)(this.grid2.Width * 0.3);
                this.grid2.Splits[0].DisplayColumns["期望结果"].Width = (int)(this.grid2.Width * 0.2);
                this.grid2.Splits[0].DisplayColumns["实测结果"].Width = (int)(this.grid2.Width * 0.2);
            }
        }
        
        #endregion grid

        #region 杂项

        // 当前选中测试用例树绘制
        private void DrawCurNode(bool bClear)
        {
            Rectangle r = tnForm.Bounds;
            try
            {
                Graphics g = tnForm.TreeView.CreateGraphics();
                Pen p;
                if (bClear)
                    p = new Pen(Color.White, 2);
                else
                    p = new Pen(Color.Blue, 2);
                g.DrawRectangle(p, r);
            }
            catch (Exception)
            {
            }
        }

        // 菜单项显示
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // "复制"菜单项
            if (this.grid2.SelectedRows.Count == 0)
                tsmiCopyStep.Enabled = false;
            else
                tsmiCopyStep.Enabled = true;

            if (!BusiLogic.NodeIsShortcut(tnForm))
            {
                if (BusiLogic.IsStepInClipboard())
                    tsmiPasteStep.Enabled = true;
                else
                    tsmiPasteStep.Enabled = false;
            }
            else
                tsmiPasteStep.Enabled = false;
        }

        // 复制测试步骤(所有复制数据存放在剪贴板)
        private void tsmiCopyStep_Click(object sender, EventArgs e)
        {
            if (this.grid2.SelectedRows.Count == 0)
                return;

            List<TestStep> datalist = new List<TestStep>();
            foreach (int rowindex in this.grid2.SelectedRows)
            {
                TestStep ts = new TestStep();
                ts.input = this.grid2[rowindex, "输入及操作"].Equals(DBNull.Value) ? string.Empty :
                    (string)this.grid2[rowindex, "输入及操作"];
                ts.expection = this.grid2[rowindex, "期望结果"].Equals(DBNull.Value) ? string.Empty :
                    (string)this.grid2[rowindex, "期望结果"];
                if (_pageType == PageType.TestCasePerform)
                    ts.result = this.grid2[rowindex, "实测结果"].Equals(DBNull.Value) ? string.Empty :
                        (string)this.grid2[rowindex, "实测结果"];
                datalist.Add(ts);
            }

            DBType dbtype;
            if (dbProject.databaseType == Common.Database.DatabaseType.Access)
                dbtype = DBType.Access;
            else if (dbProject.databaseType == Common.Database.DatabaseType.SQLServer)
                dbtype = DBType.SQL;
            else
                dbtype = DBType.Invalid;

            BusiLogic.CopyStepsToClipboard(dbProject.dbConnection.ConnectionString, datalist, dbtype);
        }

        // 粘贴测试步骤
        private void tsmiPasteStep_Click(object sender, EventArgs e)
        {
            int addlines = 0;
            // 粘贴数据
            if (_pageType == PageType.TestCaseDesign)
                addlines = BusiLogic.PasteStep(_uceid, this.grid2.Row, true, this._tbl);
            else if (_pageType == PageType.TestCasePerform)
                addlines = BusiLogic.PasteStep(_uceid, this.grid2.Row, false, this._tbl);
            else
                return;

            for (int i = 0; i < addlines; i++)
                _rowsheight.Add(_singleheight);

            int rowindex = this.grid2.Row;
            this.grid2.Row = 0;
            this.grid2.Row = rowindex;
                            
            // 更新用例"执行状态"(树节点图标及快捷方式图标)
            string newsta = BusiLogic.CheckExecSta(_tbl);
            if (!newsta.Equals(ExecStatus))
            {
                ExecStatus = newsta;
                _datachanged = true;
                BusiLogic.UpdateUCIcon(UCTid, newsta, ExecResult, tnForm);
            }
        }

        #endregion 杂项

        private void ddttPerson_TextChanged(object sender, EventArgs e)
        {
            _datachanged = true;
            if (string.IsNullOrEmpty(ddttPerson.Text))
            {
                tnForm.ForeColor = Color.LightPink;
            }
            else
            {
                tnForm.ForeColor = Color.Black;
            }
        }

        private void ddttTester_TextChanged(object sender, EventArgs e)
        {
            _datachanged = true;
        }
    }
}