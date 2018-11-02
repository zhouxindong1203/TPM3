using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common;
using Common.Database;
using TPM3.zxd;
using Z1.tpm.DB;
using TPM3.Sys;
using Z1.tpm;

namespace TPM3.zxd.pbl
{
    public partial class ProblemRepForm : Form
    {
        #region ����

        private Form _parentfrm;
        private string _belongobjid;
        private string _belongobjabr;
        private string _pblspl;

        public ProblemRepForm(Form parentfrm)
        {
            InitializeComponent();
            this.Height = Math.Min(727, Screen.PrimaryScreen.WorkingArea.Height - 100);

            _parentfrm = parentfrm;

            FrmCommonFunc.PersonEditor(this.txtbxReporter.cm);
        }

        private void ProblemRepForm_Load(object sender, EventArgs e)
        {
            RadioButton[] btn1 = new RadioButton[5];
            btn1[0] = radioPC1;
            btn1[1] = radioPC2;
            btn1[2] = radioPC3;
            btn1[3] = radioPC4;
            btn1[4] = radioPC5;

            RadioButton[] btn2 = new RadioButton[5];
            btn2[0] = radioPL1;
            btn2[1] = radioPL2;
            btn2[2] = radioPL3;
            btn2[3] = radioPL4;
            btn2[4] = radioPL5;

            this.txtbxUsecaseID.Text = (_parentfrm as TestUsecaseForm).UCSign;
            this.txtbxUsecaseName.Text = (_parentfrm as TestUsecaseForm).UCName;

            // ��ʼ��"�������"��"���⼶��"RadioButtons
            if (!CommonDB.InitRBsFromDB(MyBaseForm.dbProject, radioPC1.Location,
                radioPC5.Location, btn1, (string)MyBaseForm.pid, (string)MyBaseForm.currentvid,
                "���"))
            {
                this.btnCancel.PerformClick();
                this.Close();
                return;
            }

            if (!CommonDB.InitRBsFromDB(MyBaseForm.dbProject, radioPL1.Location,
                radioPL5.Location, btn2, (string)MyBaseForm.pid, (string)MyBaseForm.currentvid,
                "����"))
            {
                this.btnCancel.PerformClick();
                Close();
                return;
            }

            NodeTagInfo tag = FrmCommonFunc.GetObjTagInfo((_parentfrm as MyBaseForm).tnForm);
            this._belongobjabr = tag.keySign;
            this._belongobjid = tag.id;
            this._pblspl = ConstDef.PblSplitter();

            // ��ʼ�����������ʶѡ��
            CommonDB.InitPblSignToCombo(MyBaseForm.dbProject, this.cmbFirstSign, this.checkFirstSign,
                (string)MyBaseForm.pid, (string)MyBaseForm.currentvid, 1);

            CommonDB.InitPblSignToCombo(MyBaseForm.dbProject, this.cmbSecondSign, this.checkSecondSign,
                (string)MyBaseForm.pid, (string)MyBaseForm.currentvid, 2);

            CommonDB.InitPblSignToCombo(MyBaseForm.dbProject, this.cmbThirdSign, this.checkThirdSign,
                (string)MyBaseForm.pid, (string)MyBaseForm.currentvid, 3);

            CommonDB.InitPblSignToCombo(MyBaseForm.dbProject, this.cmbFouthSign, this.checkFouthSign,
                (string)MyBaseForm.pid, (string)MyBaseForm.currentvid, 4);

            this.txtbxReporter.Focus();
            ReportDate = DateTime.Today;

            UpdatePblSign();
        }

        #endregion ����

        #region ����

        public PblSign First
        {
            get
            {
                if (this.checkFirstSign.Checked)
                    return this.cmbFirstSign.SelectedItem as PblSign;
                else
                    return null;
            }
        }

        public PblSign Second
        {
            get
            {
                if (this.checkSecondSign.Checked)
                    return this.cmbSecondSign.SelectedItem as PblSign;
                else
                    return null;
            }
        }

        public PblSign Third
        {
            get
            {
                if (this.checkThirdSign.Checked)
                    return this.cmbThirdSign.SelectedItem as PblSign;
                else
                    return null;
            }
        }

        public PblSign Fouth
        {
            get
            {
                if (this.checkFouthSign.Checked)
                    return this.cmbFouthSign.SelectedItem as PblSign;
                else
                    return null;
            }
        }

        private string GetPblSignID(PblSign ps)
        {
            if (ps == null)
                return string.Empty;
            else
                return ps.id;
        }

        /// <summary>
        /// ������
        /// </summary>
        public string Reporter
        {                        
            get
            {
                if (this.txtbxReporter.Tag == null)
                    return string.Empty;
                else
                    return (string)this.txtbxReporter.Tag;
            }
            set
            {
                this.txtbxReporter.Tag = value;
                this.txtbxReporter.DisplayValue();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime ReportDate
        {
            set
            {
                dtReportDate.Value = value;
            }
            get
            {
                return dtReportDate.Value;
            }
        }

        /// <summary>
        /// �������(0:�������; 1:�ĵ�����; 2:��ƴ���; 3:��������)
        /// if return -1 means none option be selected!!
        /// </summary>
        public int ProblemCategory
        {
            set
            {
                switch( value )
                {
                case 0:
                    radioPC1.Checked = true;
                    break;

                case 1:
                    radioPC2.Checked = true;
                    break;

                case 2:
                    radioPC3.Checked = true;
                    break;

                case 3:
                    radioPC4.Checked = true;
                    break;

                case 4:
                    radioPC5.Checked = true;
                    break;

                default:
                    radioPC1.Checked = false;
                    radioPC2.Checked = false;
                    radioPC3.Checked = false;
                    radioPC4.Checked = false;
                    radioPC5.Checked = false;

                    break;
                }
            }

            get
            {
                if( radioPC1.Checked )
                    return 0;
                else if( radioPC2.Checked )
                    return 1;
                else if( radioPC3.Checked )
                    return 2;
                else if( radioPC4.Checked )
                    return 3;
                else if( radioPC5.Checked )
                    return 4;
                else
                    return -1;
            }
        }

        private string GetPblCatID()
        {
            switch (ProblemCategory)
            {
                case 0:
                    return (string)this.radioPC1.Tag;

                case 1:
                    return (string)this.radioPC2.Tag;

                case 2:
                    return (string)this.radioPC3.Tag;

                case 3:
                    return (string)this.radioPC4.Tag;

                case 4:
                    return (string)this.radioPC5.Tag;

                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// ���⼶��(0:��������; 1:���ش���; 2:һ�����; 3:��΢����)
        ///  if return -1 means none option be selected!!
        /// </summary>
        public int ProblemLevel
        {
            set
            {
                switch( value )
                {
                case 0:
                    radioPL1.Checked = true;
                    break;

                case 1:
                    radioPL2.Checked = true;
                    break;

                case 2:
                    radioPL3.Checked = true;
                    break;

                case 3:
                    radioPL4.Checked = true;
                    break;

                case 4:
                    radioPL5.Checked = true;
                    break;

                default:
                    radioPL1.Checked = false;
                    radioPL2.Checked = false;
                    radioPL3.Checked = false;
                    radioPL4.Checked = false;
                    radioPL5.Checked = false;

                    break;

                }
            }

            get
            {
                if( radioPL1.Checked )
                    return 0;
                else if( radioPL2.Checked )
                    return 1;
                else if( radioPL3.Checked )
                    return 2;
                else if( radioPL4.Checked )
                    return 3;
                else if( radioPL5.Checked )
                    return 4;
                else
                    return -1;
            }
        }

        private string GetPblLelID()
        {
            switch (ProblemLevel)
            {
                case 0:
                    return (string)radioPL1.Tag;

                case 1:
                    return (string)radioPL2.Tag;

                case 2:
                    return (string)radioPL3.Tag;

                case 3:
                    return (string)radioPL4.Tag;

                case 4:
                    return (string)radioPL5.Tag;

                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// ��������(ole����)
        /// </summary>
        public byte[] ProblemDescrition
        {
            set
            {
                rich1.SetRichData(value);
            }
            get
            {
                return rich1.GetRichData();
            }
        }

        // �������ⱨ�浥�󷵻���ID
        private string _pblid = string.Empty;
        public string PblID
        {
            get
            {
                return _pblid;
            }
            set
            {
                _pblid = value;
            }
        }

        public string PblSign
        {
            get
            {
                return this.txtbxProblemID.Text;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string PblName
        {
            set
            {
                this.txtbxPblName.Text = value;
            }
            get
            {
                return this.txtbxPblName.Text;
            }
        }

        #endregion ����

        #region �¼�������

        /// <summary>
        /// ��"������"�ֶε��������ʵʱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckTextChanged(object sender, System.EventArgs e)
        {
            if( sender is DropDownTextBox )
            {
                DropDownTextBox ddt = (DropDownTextBox)sender;
                if (ddt.Text.Length == 0)
                {
                    lblReporter.Text = "������*";
                    errorProvider1.SetIconAlignment(this.txtbxReporter, ErrorIconAlignment.MiddleRight);
                    errorProvider1.SetError(this.txtbxReporter, "\'������\'����Ϊ��!!");
                }

                else
                {
                    lblReporter.Text = "������";
                    errorProvider1.SetError((Control)sender, string.Empty);
                }
            }
        }

        private void radioPC1_Click(object sender, EventArgs e)
        {
            this.gbPblCat.Text = "�������";
            errorProvider1.SetError((Control)sender, string.Empty);
        }

        private void radioPL1_Click(object sender, EventArgs e)
        {
            this.gbPblLel.Text = "���⼶��";
            errorProvider1.SetError((Control)sender, string.Empty);
        }

        private void checkFirstSign_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if( cb == null )
                return;

            switch( cb.Checked )
            {
            case true:
                if( cb == this.checkFirstSign )
                    this.cmbFirstSign.Enabled = true;
                else if( cb == this.checkSecondSign )
                    this.cmbSecondSign.Enabled = true;
                else if( cb == this.checkThirdSign )
                    this.cmbThirdSign.Enabled = true;
                else if( cb == this.checkFouthSign )
                    this.cmbFouthSign.Enabled = true;

                break;

            case false:
                if( cb == this.checkFirstSign )
                    this.cmbFirstSign.Enabled = false;
                else if( cb == this.checkSecondSign )
                    this.cmbSecondSign.Enabled = false;
                else if( cb == this.checkThirdSign )
                    this.cmbThirdSign.Enabled = false;
                else if( cb == this.checkFouthSign )
                    this.cmbFouthSign.Enabled = false;

                break;
            }

            UpdatePblSign();
        }

        private void cmbFirstSign_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePblSign();
        }

        private void ProblemRepForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_cancalbtn)
            {
                // 1. ���"������"��"�������"��"���⼶��"�Ƿ���д
                if (Reporter.Equals(string.Empty))
                {
                    errorProvider1.SetIconAlignment(this.txtbxReporter, ErrorIconAlignment.MiddleRight);
                    errorProvider1.SetError(this.txtbxReporter, "\'������\'����Ϊ��!!");
                    e.Cancel = true;
                    return;
                }

                if (ProblemCategory == -1)
                {
                    errorProvider1.SetIconAlignment(this.gbPblCat, ErrorIconAlignment.MiddleRight);
                    errorProvider1.SetError(this.gbPblCat, "\'�������\'����Ϊ��!!");
                    e.Cancel = true;
                    return;
                }

                if (ProblemLevel == -1)
                {
                    errorProvider1.SetIconAlignment(this.gbPblLel, ErrorIconAlignment.MiddleRight);
                    errorProvider1.SetError(this.gbPblLel, "\'���⼶��\'����Ϊ��!!");
                    e.Cancel = true;
                    return;
                }

                // 2. ����"���ⱨ�浥"��¼, ���������ݿ�
                PblRep pr = new PblRep();
                pr.id = (string)FunctionClass.NewGuid;
                pr.pid = (string)MyBaseForm.pid;
                pr.vid = (string)MyBaseForm.currentvid;
                pr.repdate = ReportDate;
                pr.reporter = Reporter;

                pr.pblcat = GetPblCatID();
                pr.pbllel = GetPblLelID();
                pr.pbldes = ProblemDescrition;
                pr.pblmemo = this.tbMemo.Text;

                pr.firstid = GetPblSignID(First);
                pr.secondid = GetPblSignID(Second);
                pr.thirdid = GetPblSignID(Third);
                pr.fouthid = GetPblSignID(Fouth);
                pr.sameseq = this._sameseq;
                pr.belongobjid = this._belongobjid;
                pr.usecount = 1;

                pr.regress = 0;
                byte[] b = new byte[6] { 120, 156, 98, 4, 8, 32 };
                pr.effanalyse = b;
                pr.pblname = PblName;

                CommonDB.AddNewPbl(MyBaseForm.dbProject, pr);
                PblID = pr.id;
            }
        }

        private bool _cancalbtn = true;
        private void btnOK_Click(object sender, EventArgs e)
        {
            _cancalbtn = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancalbtn = true;
        }
        #endregion �¼�������

        #region �ڲ�����

        private int _sameseq;
        private void UpdatePblSign()
        {
            StringBuilder strbld = new StringBuilder();

            strbld.Append(this._belongobjabr);

            if (First != null)
            {
                strbld.Append(_pblspl);
                strbld.Append(First.sign);
            }

            if (Second != null)
            {
                strbld.Append(_pblspl);
                strbld.Append(Second.sign);
            }

            if (Third != null)
            {
                strbld.Append(_pblspl);
                strbld.Append(Third.sign);
            }

            if (Fouth != null)
            {
                strbld.Append(_pblspl);
                strbld.Append(Fouth.sign);
            }

            strbld.Append(_pblspl);
            _sameseq = CommonDB.GetPblSignComSeq(MyBaseForm.dbProject,
                GetPblSignID(First), GetPblSignID(Second), GetPblSignID(Third), GetPblSignID(Fouth), _belongobjid);
            strbld.Append(_sameseq);

            this.txtbxProblemID.Text = strbld.ToString();
        }

        #endregion �ڲ�����
    }
}