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
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestUsecaseForm>(1); // "���"
        static ColumnPropList columnList2 = GridAssist.GetColumnPropList<TestUsecaseForm>(2); // "ִ��"

        public static ExistPblRepForm g_existpbldlg = null;

        public Dictionary<string, List<string>[]> p_AccDic;

        #region ����

        private PageType _pageType;
        private NodeTagInfo _taginfo;
        private string _uceid;
        private string _belobjid;
        private string _belobjname;
        private string _belobjabbr;
        private TestTreeForm _ttf;

        #region ���Զ���

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

                string filter = string.Format("������������='{0}'", _taginfo.text);
                DataRow[] rows = this._tblucs.Select(filter);
                if ((rows != null) && (rows.Length > 0))
                    rows[0]["������������"] = value;
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

        #endregion ���Զ���

        #region �������/�ر�

        static TestUsecaseForm()
        {
            //int ww = Screen.PrimaryScreen.WorkingArea.Width;

            columnList1.Add("���", 50);
            columnList1.Add("���뼰����", /*(int)(ww * 0.3)*/200);
            columnList1.Add("�������", /*(int)(ww * 0.2)*/150);
            columnList1.AddValidator(new NotNullValidator("���뼰����"));

            columnList2.Add("���", 50);
            columnList2.Add("���뼰����", /*(int)(ww * 0.25)*/200);
            columnList2.Add("�������", /*(int)(ww * 0.1)*/150);
            columnList2.Add("ʵ����", /*(int)(ww * 0.1)*/150);
            columnList2.Add("����?", 50);
            columnList2.Add("�����ʶ", 100, false);
            columnList2.AddValidator(new NotNullValidator("���뼰����"));
        }
        
        // TestTreeForm���Ҳര��
        public TestUsecaseForm(PageType pageType)
        {
            InitializeComponent();

            this._pageType = pageType;
            this._pblspl = ConstDef.PblSplitter();
        }

        #region ��������ID������������

        private string _uctid;
        private string _itemtid;
        // ֱ����������ID������Ӧ����
        public TestUsecaseForm(string uctid, string itemtid)
        {
            InitializeComponent();

            _uctid = uctid;
            _itemtid = itemtid;

            TreeViewUtils tvu = new TreeViewUtils();
            EnumTreeViewProc proc = new EnumTreeViewProc(InittnForm);
            tvu.FindTreeViewLeaf(TPM3.zxd.ExecStatus.g_ttf.tree, proc);
            if (!BusiLogic.AssertNotNull(tnForm, "�޷���λ������!", "����ʧ��"))
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

            if (_itemtid == null)   // ��������������
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
            else    // ������ָ��ID(���Ǹ��ڵ㼰��ݷ�ʽ)
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

        #endregion ��������ID������������

        public override bool OnPageCreate()
        {
            // ��ȡ������Ϣ
            _taginfo = tnForm.Tag as NodeTagInfo;
            if (_taginfo == null)
                throw (new InvalidOperationException("�޷���ȡ���ڵ㸽����Ϣ!"));

            _uceid = TestUsecase.GetUCEntityID(dbProject, _taginfo.id);
            if (string.Empty.Equals(_uceid))
                throw (new InvalidOperationException("�޷���ȡ��������ʵ��ID!"));

            gridAssist1 = new TrueDBGridAssist(grid2, null, "���");
            if (_pageType == PageType.TestCasePerform)
                gridAssist1.columnList = columnList2;
            else
                gridAssist1.columnList = columnList1;

            // ���ƶ�
            gridAssist1.rowPosition.GetTreeNodeKeyEvent += FrmCommonFunc.GetTreeNodeKey;
            gridAssist1.rowPosition.AfterRowMoveDown += RowMoveEventHandler;
            gridAssist1.rowPosition.AfterRowMoveUp += RowMoveEventHandler;
            gridAssist1.rowPosition.BeforeRowMoveDown += BeforeRowMove;
            gridAssist1.rowPosition.BeforeRowMoveUp += BeforeRowMove;

            // ���ݰ�
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
            this._tbl.Columns.Add("����?", typeof(bool));
            this._tbl.Columns.Add("�����ʶ", typeof(string));

            foreach (DataRow row in this._tbl.Rows)
            {
                if ((DBNull.Value.Equals(row["���ⱨ�浥ID"])) ||
                    (string.Empty.Equals(row["���ⱨ�浥ID"])))
                {
                    row["����?"] = false;
                }
                else
                {
                    row["����?"] = true;
                    row["�����ʶ"] = CommonDB.GenPblSignForStep(dbProject, _pblspl, (string)row["���ⱨ�浥ID"]);
                }
            }
        }

        private bool _datachanged = false; // ����������Ϣ�Ƿ�Ķ�
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

            this.txtbxUCName.Text       = BusiLogic.GetStringFromDB(row["������������"]);
            this.txtbxDesc.Text         = BusiLogic.GetStringFromDB(row["��������"]);
            this.txtbxInit.Text         = BusiLogic.GetStringFromDB(row["�����ĳ�ʼ��"]);
            this.txtbxConstraint.Text   = BusiLogic.GetStringFromDB(row["ǰ���Լ��"]);
            this.txtbxTerm.Text         = BusiLogic.GetStringFromDB(row["���Թ�����ֹ����"]);
            this.txtbxPassCert.Text     = BusiLogic.GetStringFromDB(row["���Խ��������׼"]);
            DesMethod                   = BusiLogic.GetStringFromDB(row["��ʹ�õ���Ʒ���"]);
            this.txtbxTrace.Text        = BusiLogic.GetStringFromDB(row["׷�ٹ�ϵ"]);
            this._designperson          = BusiLogic.GetStringFromDB(row["�����Ա"]);
            //if (_pageType == PageType.TestCaseDesign)
                Person                  = BusiLogic.GetStringFromDB(row["�����Ա"]);
            //else if (_pageType == PageType.TestCasePerform)
                Tester                  = BusiLogic.GetStringFromDB(row["������Ա"]);

            //TestTime                    = BusiLogic.GetDateTimeFromDB(row["����ʱ��"]);
            BusiLogic.GetTestTime(this.dtpTestDate, row["����ʱ��"]);
            this.txtbxExecStatus.Text   = BusiLogic.GetStringFromDB(row["ִ��״̬"]);
            this.cmbExecResult.Text     = BusiLogic.GetStringFromDB(row["ִ�н��"]);
            this.txtbxUnexecReason.Text = BusiLogic.GetStringFromDB(row["δִ��ԭ��"]);
            this.txtbxTrace.Text        = BusiLogic.InitTrace(tnForm);
            MarkClr                     = BusiLogic.GetStringFromDB(row["���"]);

            this.txtbxSign.Text = BusiLogic.GenUCSign(tnForm);

            this._datachanged = false;

            // ������ʾ
            this.expandablePanel1.TitleText = "�������� [" + this.txtbxUCName.Text + "]";
            this.grid2.Caption = "���Բ��� [" + this.txtbxUCName.Text + "]";

            // ֻ����"�������"����²���
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
                    //this.lblPerson.Text = "�����Ա";
                    break;

                case PageType.TestCasePerform:
                    //this.lblPerson.Text = "������Ա";
                    break;

                default:
                    throw (new InvalidOperationException("�Ƿ�����ģʽ!!"));
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
            _unitwidth = this.grid2.Splits[0].DisplayColumns["���뼰����"].Width - 20;

            LoadData();

            if (_pageType == PageType.TestCasePerform)
            {
                // ����δ�رյ�"�ύ����"����
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
            TPM3.zxd.ExecStatus.g_AccFolder = Z1.tpm.DB.ProjectInfo.GetTextContent(dbProject, (string)pid, (string)currentvid, "��Ŀ��Ϣ", "�����ļ���");

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

        // ��ʼ�������Բ�����и�
        private void InitRowsHeight()
        {
            _rowsheight.Clear();

            foreach (DataRow row in _tbl.Rows)
            {
                _rowsheight.Add(Z1Utils.ZString.GetStringLines(BusiLogic.GetStringFromDB(row["���뼰����"]),
                    _unitwidth, _graphics, _font));
            }

            for (int i = 0; i < _rowsheight.Count; i++)
            {
                if ((UserSetting.Default.AutoCaseRowSize > 0) && 
                    (_rowsheight[i] > UserSetting.Default.AutoCaseRowSize))
                    _rowsheight[i] = UserSetting.Default.AutoCaseRowSize;

                if (_rowsheight[i] == 1) // �Ե����ı����ӿ���
                    _rowsheight[i]++;

                this.grid2.Splits[0].Rows[i].Height = _rowsheight[i] * _singleheight;
            }
        }

        private void SetInfoEditable()
        {
            LeftTreeUserControl ltuc = FrmCommonFunc.GetParentFrm(this);
            if ((ltuc != null) && (ltuc is TestTreeForm))
            {
                if ((ltuc as TestTreeForm).IsRegressExec)   // �ع����ִ�н׶�
                {
                    NodeTagInfo tag = tnForm.Tag as NodeTagInfo;
                    if (tag != null)
                    {
                        if (tag.IsRegressCreate)            // �̳ж���
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

            grid.Splits[0].DisplayColumns["���뼰����"].Button = true;
            grid.Splits[0].DisplayColumns["�������"].Button = true;
            grid.Splits[0].DisplayColumns["ʵ����"].Button = true;

            //grid.Columns["���뼰����"].NumberFormat = "FormatText Event";
            //grid.Columns["�������"].NumberFormat = "FormatText Event";
            //grid.Columns["ʵ����"].NumberFormat = "FormatText Event";
            grid.Splits[0].DisplayColumns["���뼰����"].FetchStyle = true;
            grid.Splits[0].DisplayColumns["�������"].FetchStyle = true;
            grid.Splits[0].DisplayColumns["ʵ����"].FetchStyle = true;

            Assembly a = Assembly.GetExecutingAssembly();
            rm = new System.Resources.ResourceManager("TPM3.zxd.clu.tpm3zxd", a);
            Image im = (Image)rm.GetObject("AccBmp");
            grid.Columns["���뼰����"].ButtonPicture = im;
            grid.Columns["�������"].ButtonPicture = im;
            grid.Columns["ʵ����"].ButtonPicture = im;

            grid.EditDropDown = true;
            grid.AllowColMove = false;
            grid.AllowRowSizing = RowSizingEnum.IndividualRows;

            if(_pageType == PageType.TestCasePerform)
                grid.Splits[0].DisplayColumns["�����ʶ"].FetchStyle = true;
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
            // ����"���Բ���"����
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
            //        MessageBox.Show("\'�����Ա\'����Ϊ��!!", "���ݴ���", MessageBoxButtons.OK,
            //             MessageBoxIcon.Error);
            //        return false;
            //    }
            //}

            return true;
        }

        #endregion �������/�ر�

        #region ���ƶ�

        bool BeforeRowMove(DataRow drCur, DataRow drPre)
        {
            return FrmCommonFunc.BeforeRowMove(grid2, drCur, drPre);
        }

        bool RowMoveEventHandler(DataRow drCur, DataRow drPre)
        {
            //���������������ڵ��order�Լ��������½ں�
            return FrmCommonFunc.AfterRowMove(tnForm, drCur, drPre);
        }


        #endregion ���ƶ�

        #endregion ����

        #region �ı���

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
                MessageBox.Show("�����������Ʋ���Ϊ��!!", "���ݴ���", MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
                this.UCName = _taginfo.text;
                e.Cancel = true;
            }

            if (TestUsecase.ExistUCNameFromTbl(_tblucs, input))
            {
                MessageBox.Show("�Ѿ����ڴ����ƵĲ�������, �뻻����������!", "�������������ظ�", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                this.txtbxUCName.Text = _taginfo.text;
                e.Cancel = true;
            }
            else
            {
                string filter = string.Format("������������='{0}'", _taginfo.text);
                DataRow[] rows = this._tblucs.Select(filter);
                if ((rows != null) && (rows.Length > 0))
                    rows[0]["������������"] = input;

                FrmCommonFunc.UpdateUCName(tnForm.TreeView, _taginfo.id, input);
            }
        }

        #endregion �ı���

        #region grid

        #region grid����

        private string _newtid;
        private void grid2_OnAddNew(object sender, EventArgs e)
        {
            object id = FunctionClass.NewGuid;
            object entityid = FunctionClass.NewGuid;
            _newtid = id as string;

            this.grid2.Columns["���"].Value = this._tbl.Rows.Count + 1;
            // ʵ���
            this.grid2.Columns["ʵ��ID"].Value = entityid;
            this.grid2.Columns["��������ID"].Value = _uceid;
            this.grid2.Columns["��ĿID"].Value = pid;
            this.grid2.Columns["�����汾ID"].Value = currentvid;

            // ʵ���
            this.grid2.Columns["ID"].Value = id;
            this.grid2.Columns["����ID"].Value = entityid;
            this.grid2.Columns["���԰汾"].Value = currentvid;

            if (_pageType == PageType.TestCasePerform)
            {
                this.grid2.Columns["����?"].Value = false;
                this.grid2.Columns["�����ʶ"].Value = string.Empty;
            }
        }
        
        private void grid2_AfterInsert(object sender, EventArgs e)
        {
            // ���²���������ִ�����, ͨ�����
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

            if (this.grid2.AddNewMode != AddNewModeEnum.NoAddNew) // ɾ��������¼
                return;

            SaveToDB();

            string steptid = (string)this.grid2.Columns["ID"].Value;
            string stepeid = (string)this.grid2.Columns["ʵ��ID"].Value;
                        
            // ����ɾ���Ĳ����Ƿ��ǵ�ǰ���԰汾������
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

                if (_rowsheight[i] == 1) // �Ե����ı����ӿ���
                    _rowsheight[i]++;

                this.grid2.Splits[0].Rows[i].Height = _rowsheight[i] * _singleheight;
            }
        }

        // �ύ/ɾ�����⡢������ʾ��ȥ��
        private int _row;
        private void grid2_BeforeColEdit(object sender, BeforeColEditEventArgs e)
        {
            if (this.grid2.AddNewMode != AddNewModeEnum.NoAddNew)
                _rowsheight.Add(_singleheight * 2);

            _row = this.grid2.Row;

            C1DataColumn dc = e.Column.DataColumn;

            // �̳ж����Ĳ��Բ����"���뼰����"��"�������"���ɱ༭
            if (this.grid2.AddNewMode == AddNewModeEnum.NoAddNew)
            {
                if (_ttf.IsRegressExec)
                {
                    if ((dc.Caption.Equals("���뼰����")) ||
                        (dc.Caption.Equals("�������")))
                    {
                        string stepeid = (string)this.grid2.Columns["ʵ��ID"].Value;
                        if (!TestUsecase.StepIsSameVer(dbProject, stepeid, currentvid as string))
                        {
                            MessageBox.Show("�̳еĲ��Բ���\"���뼰����\"��\"�������\"���ܱ༭!",
                                "����ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }

            // ȷ�������ύ����ȷ��
            if (dc.Caption.Equals("ʵ����"))
            {
                if (grid2.AddNewMode != AddNewModeEnum.NoAddNew)
                {
                    MessageBox.Show("�²��Բ���δ�ύǰ���ܱ༭\"ʵ����\"!", "��������",
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }

            if (dc.Caption.Equals("����?"))
            {
                string strHasPbl = (string)dc.Value; // ��ʱΪ�༭�����ֵ
                if (strHasPbl.Equals("True")) // �ύ����
                {
                    // "ʵ����"����Ϊ��
                    //string strActRlt = (string)this.grid2.Columns["ʵ����"].Value;
                    string strActRlt = BusiLogic.GetStringFromDB(this.grid2.Columns["ʵ����"].Value);
                    if (string.Empty.Equals(strActRlt))
                    {
                        string steptid = (string)this.grid2.Columns["ID"].Value;
                        if (!TestUsecase.ActualTestHasAcc(dbProject, steptid))
                        {
                            MessageBox.Show("\"ʵ����\"Ϊ��ʱ�����ύ����!!", "������ʾ", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            e.Cancel = true;
                            return;
                        }
                    }

                    string strInput = (string)this.grid2.Columns["���뼰����"].Value;
                    if (string.Empty.Equals(strInput))
                    {
                        MessageBox.Show("\"���뼰����\"����Ϊ��!!", "������ʾ", MessageBoxButtons.OK,
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
                                case 0: // �ύ������
                                    using (ProblemRepForm prf = new ProblemRepForm(this))
                                    {
                                        if (DialogResult.OK == prf.ShowDialog())
                                        {
                                            this.grid2.Columns["���ⱨ�浥ID"].Text = prf.PblID;
                                            if (this.cmbExecResult.Text.Equals(ConstDef.execrlt[1]))
                                            {
                                                this.cmbExecResult.Text = ConstDef.execrlt[2];
                                                this._datachanged = true;
                                                BusiLogic.UpdateUCIcon(_taginfo.id, this.txtbxExecStatus.Text,
                                                    this.cmbExecResult.Text, tnForm);
                                            }
                                            this.grid2.Columns["�����ʶ"].Text = prf.PblSign;
                                        }
                                        else
                                        {
                                            e.Cancel = true;
                                            return;
                                        }
                                    }
                                    break;

                                case 1: // ������������ѡ��
                                    List<PblRep> li = CommonDB.GetPblRepsForObj(dbProject,
                                        _belobjid, _belobjname, _belobjabbr, (string)pid, (string)currentvid);
                                    if (li.Count == 0)
                                    {
                                        MessageBox.Show("��ǰ����������޿��õ����ⱨ�浥����!!", "����ʧ��", MessageBoxButtons.OK,
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
                else // ɾ������
                {
                    if (DialogResult.Yes == MessageBox.Show("ȷʵ��Ϊ������������?\n��ѡ\"��\"��ɾ��������", "ȷ��ɾ��", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    {
                        string stepid = (string)this.grid2.Columns["ID"].Value;
                        string pblid = (string)this.grid2.Columns["���ⱨ�浥ID"].Value;

                        CommonDB.DelPblForStep(dbProject, stepid, pblid);
                        grid2.Columns["�����ʶ"].Value = string.Empty;
                        grid2.Columns["���ⱨ�浥ID"].Value = DBNull.Value;

                        // ����"ִ�н��"
                        string execrlt = ConstDef.execrlt[1];
                        foreach (DataRow row in _tbl.Rows)
                        {
                            if (row.RowState != DataRowState.Deleted)
                            {
                                if (!stepid.Equals((string)row["ID"]))
                                {
                                    if(!DBNull.Value.Equals(row["���ⱨ�浥ID"]))
                                        if (!string.Empty.Equals(row["���ⱨ�浥ID"]))
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
                } // ɾ������
            }
        }

        private void grid2_AfterColEdit(object sender, ColEventArgs e)
        {
            C1DataColumn dc = e.Column.DataColumn;

            if (dc.Caption.Equals("���뼰����"))
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

        // "ʵ����"�б༭
        private void grid2_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
        {
            C1DataColumn dc = e.Column.DataColumn;
            string steptid = (string)grid2.Columns["ID"].Value;

            //if (_ttf.IsRegressExec)
            //{
            //    if ((dc.Caption.Equals("���뼰����")) ||
            //        (dc.Caption.Equals("�������")))
            //    {
            //        string stepeid = (string)this.grid2.Columns["ʵ��ID"].Value;

            //        if (!TestUsecase.StepIsSameVer(dbProject, stepeid, currentvid as string))
            //        {
            //            e.Cancel = true;
            //            return;
            //        }
            //    }
            //}1

            if ((dc.Caption.Equals("ʵ����")) &&
                (!TestUsecase.ActualTestHasAcc(dbProject, (string)grid2.Columns["ID"].Value))) // "ʵ����"��������
            {
                // ��������ʱ, "ʵ����"����Ϊ��
                bool haspbl = (bool)grid2.Columns["����?"].Value;
                if (haspbl)
                {
                    if (string.Empty.Equals(dc.Text))
                    {
                        MessageBox.Show("��������ʱ, \"ʵ����\"����Ϊ��!!", "������ʾ", MessageBoxButtons.OK,
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

        // ����Ƿ������ύ����
        private bool HasExistPblDlg(BeforeColEditEventArgs e)
        {
            if ((g_existpbldlg != null) && (!g_existpbldlg.IsDisposed))
            {
                MessageBox.Show("���ȴ���֮ǰ�ύ��δ��ɵ����ⱨ��!", "������ʾ", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                e.Cancel = true;
                g_existpbldlg.BringToFront();
                g_existpbldlg.Show();

                return true;
            }
            else
                return false;
        }

        // ͨ��"�����ύ��"���û��������������������(������������ѡ��)
        public void UpdateNoPbl(string stepid, bool value)
        {
            string filter = string.Format("ID=\'{0}\'", stepid);
            DataRow[] rows = _tbl.Select(filter);
            if ((rows != null) && (rows.Length != 0))
                rows[0]["����?"] = value;
        }

        // ���ò��Բ����"���ⱨ�浥ID"ֵ
        public void SetStepPblId(string stepid, string pblid, string pblsign, int newcount)
        {
            string filter = string.Format("ID=\'{0}\'", stepid);
            DataRow[] rows = _tbl.Select(filter);
            if ((rows != null) && (rows.Length != 0))
            {
                rows[0]["���ⱨ�浥ID"] = pblid;
                rows[0]["�����ʶ"] = pblsign;

                CommonDB.AddPblCount(dbProject, pblid, newcount);
            }
        }

        // ���ĳ�����Բ����Ƿ�����
        public bool StepAvaliable(string stepid)
        {
            string filter = string.Format("ID=\'{0}\'", stepid);
            DataRow[] rows = _tbl.Select(filter);
            if ((rows != null) && (rows.Length != 0))
                return true;
            else
                return false;
        }

        // ����ӵ�и������������䱳��ɫ
        private void grid2_FetchCellStyle(object sender, FetchCellStyleEventArgs e)
        {
            if (this.grid2.AddNewMode != AddNewModeEnum.NoAddNew)
                return;

            if (e.Column.Name.Equals("�����ʶ"))
            {
                e.CellStyle.ForeColor = Color.Blue;
                e.CellStyle.Font = new Font(this.grid2.Font, FontStyle.Underline);
                return;
            }

            string stepid = (string)this.grid2[e.Row, "ID"];
            int col = -1;
            switch (e.Column.Name)
            {
                case "���뼰����":
                    col = 0;
                    break;

                case "�������":
                    col = 1;
                    break;

                case "ʵ����":
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

        // ��������Ĳ��Բ���
        private void grid2_FetchRowStyle(object sender, FetchRowStyleEventArgs e)
        {
            if (this._pageType == PageType.TestCasePerform)
            {
                if (this.grid2[e.Row, "����?"].ToString().Equals("True"))
                {
                    /*Font fnt = new Font(e.CellStyle.Font.Name,
                        e.CellStyle.Font.Size, FontStyle.Bold);
                    e.CellStyle.Font = fnt;*/
                    e.CellStyle.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    if (!this.grid2[e.Row, "ʵ����"].ToString().Equals(""))
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
                // ����Ƿ�Ϊ����µĴ��¼���
                if ((row < 0) ||
                    (row > this.grid2.RowCount - 1))
                {
                    this.grid2.Cursor = cursor;
                    return;
                }

                if (this.grid2.Splits[0].DisplayColumns[col].Name.Equals("�����ʶ"))
                {
                    using (Graphics g = this.grid2.CreateGraphics())
                    {
                        Rectangle r = this.grid2.Splits[0].GetCellBounds(row, col);
                        string text = this.grid2[row, "�����ʶ"].ToString();
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

                    string pblid = (string)this.grid2[irow, "���ⱨ�浥ID"];

                    //GridHyperLink.g_gridForALink = this.grid2;

                    //BusiLogic.SelectMainFrmNode("zxd.pbl.PblRepsForm", "����ʵʩ-���ⱨ�浥");
                    //(MainForm.mainFrm.CurrentSelectedForm as PblRepsForm).LocatePbl(pblid as object);

                    //GridHyperLink.g_gridForALink = null;

                    // �л�����ǰ��������
                    OnPageClose(false);

                    string url = string.Format("zxd.pbl.PblRepsForm?id={0}", pblid);
                    MainForm.mainFrm.DelayCreateForm(url);
                }
            }
        }

        #endregion grid����

        #region ����

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
            p_stepeid = (string)grid2.Columns["ʵ��ID"].Value;
            p_belong = e.Column.DataColumn.Caption;
            p_row = grid2.Row;
            if (p_belong.Equals("ʵ����"))
            {
                p_rlttext = BusiLogic.GetStringFromDB(grid2.Columns["ʵ����"].Value);
            }

            Rectangle r = grid2.Splits[0].GetCellBounds(grid2.Row, e.ColIndex);
            r.Y = r.Y + grid2.Bounds.Y + r.Height - ConstDef.Offset_line;

            this.radarStatusEditorDDControl1.Bounds = r;
            this.radarStatusEditorDDControl1.OpenDropDown();
        }

        #endregion ����

        // ��̬����"���뼰����""�������""ʵ����"�����п�
        private void grid2_Resize(object sender, EventArgs e)
        {
            if (_pageType == PageType.TestCaseDesign)
            {
                this.grid2.Splits[0].DisplayColumns["���뼰����"].Width = (int)(this.grid2.Width * 0.5);
                this.grid2.Splits[0].DisplayColumns["�������"].Width = (int)(this.grid2.Width * 0.4);
            }
            else if (_pageType == PageType.TestCasePerform)
            {
                this.grid2.Splits[0].DisplayColumns["���뼰����"].Width = (int)(this.grid2.Width * 0.3);
                this.grid2.Splits[0].DisplayColumns["�������"].Width = (int)(this.grid2.Width * 0.2);
                this.grid2.Splits[0].DisplayColumns["ʵ����"].Width = (int)(this.grid2.Width * 0.2);
            }
        }
        
        #endregion grid

        #region ����

        // ��ǰѡ�в�������������
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

        // �˵�����ʾ
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // "����"�˵���
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

        // ���Ʋ��Բ���(���и������ݴ���ڼ�����)
        private void tsmiCopyStep_Click(object sender, EventArgs e)
        {
            if (this.grid2.SelectedRows.Count == 0)
                return;

            List<TestStep> datalist = new List<TestStep>();
            foreach (int rowindex in this.grid2.SelectedRows)
            {
                TestStep ts = new TestStep();
                ts.input = this.grid2[rowindex, "���뼰����"].Equals(DBNull.Value) ? string.Empty :
                    (string)this.grid2[rowindex, "���뼰����"];
                ts.expection = this.grid2[rowindex, "�������"].Equals(DBNull.Value) ? string.Empty :
                    (string)this.grid2[rowindex, "�������"];
                if (_pageType == PageType.TestCasePerform)
                    ts.result = this.grid2[rowindex, "ʵ����"].Equals(DBNull.Value) ? string.Empty :
                        (string)this.grid2[rowindex, "ʵ����"];
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

        // ճ�����Բ���
        private void tsmiPasteStep_Click(object sender, EventArgs e)
        {
            int addlines = 0;
            // ճ������
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
                            
            // ��������"ִ��״̬"(���ڵ�ͼ�꼰��ݷ�ʽͼ��)
            string newsta = BusiLogic.CheckExecSta(_tbl);
            if (!newsta.Equals(ExecStatus))
            {
                ExecStatus = newsta;
                _datachanged = true;
                BusiLogic.UpdateUCIcon(UCTid, newsta, ExecResult, tnForm);
            }
        }

        #endregion ����

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