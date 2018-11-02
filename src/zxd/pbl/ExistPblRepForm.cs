using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Common;
using Common.TrueDBGrid;
using TPM3.Sys;
using Z1.tpm;
using Z1.tpm.DB;

namespace TPM3.zxd.pbl
{
    public partial class ExistPblRepForm : Form
    {
        #region ����

        private List<PblRep> _li;
        private MyBaseForm _parentForm; // ����˷�ģʽ������ϲ㴰��
        private string _stepid;         // ���ύ����Ĳ��Բ���ID
        private string _uctid;          // ���ύ���������ʵ��ID;

        private int _curselidx = -1; // ��ǰ�����ʶ����
        private string _vid;

        public ExistPblRepForm(List<PblRep> li, MyBaseForm parentForm, string stepid, string uctid, string vid)
        {
            InitializeComponent();

            this.Height = Math.Min(707, Screen.PrimaryScreen.WorkingArea.Height - 100);

            this._li = li;
            this._parentForm = parentForm;
            this._stepid = stepid;
            this._uctid = uctid;
            this._vid = vid;

            FrmCommonFunc.PersonEditor(this.txtbxReporter.cm);
        }

        private void ExistPblRepForm_Load(object sender, EventArgs e)
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

            foreach (PblRep pr in _li)
            {
                cmbProblemSign.Items.Add(pr);
            }

            cmbProblemSign.SelectedIndex = -1;

            EnableControls(false);

            this.btnOK.Enabled = false;
        }


        #endregion ����

        #region ����

        /// <summary>
        /// �����ʶ
        /// </summary>
        public int ProblemSign
        {
            set
            {
                if ((value < 0) || (value >= cmbProblemSign.Items.Count))
                    cmbProblemSign.SelectedIndex = -1;
                else
                    cmbProblemSign.SelectedIndex = value;
            }

            get
            {
                return cmbProblemSign.SelectedIndex;
            }
        }

        /// <summary>
        /// �����û�ѡȡ�����ⱨ�浥��ID
        /// </summary>
        public string SelectedPblId
        {
            get
            {
                if (ProblemSign == -1)
                    return string.Empty;

                PblRep pr = _li[ProblemSign] as PblRep;
                if (pr == null)
                    return string.Empty;
                else
                    return pr.id;
            }
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
        /// �������
        /// </summary>
        public int ProblemCategory
        {
            set
            {
                switch (value)
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
                if (radioPC1.Checked)
                    return 0;
                else if (radioPC2.Checked)
                    return 1;
                else if (radioPC3.Checked)
                    return 2;
                else if (radioPC4.Checked)
                    return 3;
                else if (radioPC5.Checked)
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
        /// ���⼶��
        /// </summary>
        public int ProblemLevel
        {
            set
            {
                switch (value)
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
                if (radioPL1.Checked)
                    return 0;
                else if (radioPL2.Checked)
                    return 1;
                else if (radioPL3.Checked)
                    return 2;
                else if (radioPL4.Checked)
                    return 3;
                else if (radioPL5.Checked)
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

        public string Memo
        {
            set
            {
                tbMemo.Text = value;
            }
            get
            {
                return tbMemo.Text;
            }
        }

        public string StepID
        {
            get
            {
                return this._stepid;
            }
        }

        public string UCtid
        {
            get
            {
                return this._uctid;
            }
        }

        public MyBaseForm ParentFrm
        {
            get
            {
                return this._parentForm;
            }
            set
            {
                this._parentForm = value;
            }
        }

        #endregion ����

        #region �¼�����

        private void cmbProblemSign_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb == null)
                return;

            int selectindex = cmb.SelectedIndex;
            if ((selectindex < 0) || (selectindex >= cmb.Items.Count))
            {
                EnableControls(false);
                return;
            }

            EnableControls(true);

            if (_curselidx == selectindex)
                return;

            // ��������
            if ((_curselidx != -1) && (_datachanged))
            {
                PblRep pr = _li[_curselidx] as PblRep;
                if (pr != null)
                    UpdatePbl(pr);
            }

            PblRep prb = _li[selectindex] as PblRep;
            if(prb == null)
                return;

            _curselidx  = selectindex;

            // ��ֵ
            Reporter = prb.reporter;
            ReportDate = prb.repdate;
            FrmCommonFunc.SelectOneInRadios(prb.pblcat, radioPC1, radioPC2, radioPC3, radioPC4, radioPC5);
            FrmCommonFunc.SelectOneInRadios(prb.pbllel, radioPL1, radioPL2, radioPL3, radioPL4, radioPL5);
            ProblemDescrition = prb.pbldes;
            this.tbMemo.Text = prb.pblmemo;
            _datachanged = false;

            FrmCommonFunc.InitHyperLinkGrid(this.grid1, prb, _parentForm.tnForm.TreeView, _vid);

            this.btnOK.Enabled = true;
        }

        private bool _datachanged = false;
        private void txtbxReporter_TextChanged(object sender, EventArgs e)
        {
            if (sender is DropDownTextBox)
            {
                _datachanged = true;

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

        private void dtReportDate_ValueChanged(object sender, EventArgs e)
        {
            _datachanged = true;
        }

        private bool _cancelbtn = true; // ����û��˳���ʽ
        private void btnOK_Click(object sender, EventArgs e)
        {
            _cancelbtn = false;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancelbtn = true;
            Close();
        }

        private void ExistPblRepForm_FormClosing(object sender, FormClosingEventArgs e)
        {                  
            TestUsecaseForm.g_existpbldlg = null;
            TestUsecaseForm tuf = _parentForm as TestUsecaseForm;
    
            if (_cancelbtn) // "ȡ��"������ύ
            {
                if (tuf == null)
                {
                    MessageBox.Show("��������״̬δ֪! ��رյ�ǰ����, �������г���!!", "������ʾ", MessageBoxButtons.OK,
                         MessageBoxIcon.Error);
                    return;
                }
                else if(!tuf.IsDisposed)
                {
                    tuf.UpdateNoPbl(_stepid, false);
                }
            }
            else // "�ύ����"
            {
                // �������
                if (Reporter.Equals(string.Empty))
                {
                    errorProvider1.SetIconAlignment(this.txtbxReporter, ErrorIconAlignment.MiddleRight);
                    errorProvider1.SetError(this.txtbxReporter, "\'������\'����Ϊ��!!");
                    e.Cancel = true;
                    return;
                }
                else
                {
                    PblRep pr = (PblRep)this.cmbProblemSign.SelectedItem;

                    // ����λ���ύ����
                    if ((tuf != null) && (!tuf.IsDisposed))
                    {
                        if (!tuf.StepAvaliable(_stepid)) // ���Բ����Ѿ�������
                        {
                            MessageBox.Show("���ύ����Ĳ��Բ����Ѿ���ɾ��!�����޷��ύ!!", "������ʾ", MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
                            return;
                        }

                        if (tuf.ExecResult.Equals(ConstDef.execrlt[1]))
                        {
                            tuf.ExecResult = ConstDef.execrlt[2];
                            tuf.DataChanged = true;
                            BusiLogic.UpdateUCIcon(_uctid, tuf.ExecStatus, tuf.ExecResult, tuf.tnForm);
                        }

                        tuf.SetStepPblId(_stepid, pr.id, pr.ToString(), pr.usecount + 1);

                        // ��������ⱨ�浥���޸�
                        if (_datachanged)
                        {
                            UpdatePbl(pr);
                        }
                    }
                    else // ����������ҳ��
                    {
                        MessageBox.Show("�����浼�������ύ���������ִ�д���, ��ʱ�޷��ύ����!!", "������ʾ", MessageBoxButtons.OK,
                             MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
        }
        #endregion �¼�����

        #region �ڲ�����

        private void UpdatePbl(PblRep pr)
        {
            pr.reporter = Reporter;
            pr.repdate  = ReportDate;
            pr.pblcat   = GetPblCatID();
            pr.pbllel   = GetPblLelID();
            pr.pbldes   = ProblemDescrition;
            pr.pblmemo  = this.tbMemo.Text;

            CommonDB.UpdatePbl(MyBaseForm.dbProject, pr);
        }

        private void EnableControls(bool bEnable)
        {
            this.txtbxReporter.Enabled  = bEnable;
            this.dtReportDate.Enabled   = bEnable;
            this.grid1.Enabled          = bEnable;
            this.groupBox1.Enabled      = bEnable;
            this.groupBox2.Enabled      = bEnable;
            this.rich1.Enabled          = bEnable;
            this.tbMemo.Enabled         = bEnable;
        }

        #endregion �ڲ�����
    }
}