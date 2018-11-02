using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Common;
using System.Diagnostics;
using System.Threading;
using TPM3.Sys;
using Z1.tpm;
using Z1.tpm.DB;
using C1.Win.C1TrueDBGrid;

namespace TPM3.zxd.pbl
{
    public partial class PblRepsRightForm : MyBaseForm
    {
        #region 窗体

        private string _pblid;
        private TreeView _testtree;
        private PblRepsForm _parentFrm;
        private string _belongobjname;
        private string _belobjabbr = string.Empty;

        private bool _dirview = false;

        public string UseVid
        {
            get
            {
                if (!_dirview)
                    return _parentFrm.UseVid;
                else
                    return (string)currentvid;
            }
        }

        public PblRepsRightForm(string pblid, string objabbr)
        {
            InitializeComponent();

            this._pblid = pblid;
            this._belobjabbr = objabbr;
                        
            this.grid1.AllowColMove = false;

            FrmCommonFunc.PersonEditor(this.txtbxReporter.cm);
        }

        public PblRepsRightForm(string pblid, bool dirview, string belongobjname)
            : this(pblid, string.Empty)
        {
            _dirview = true;
            _belongobjname = belongobjname;
        }

        private PblRep _pr;
        private void PblRepsRightForm_Load(object sender, EventArgs e)
        {
            this.tableLayoutPanel2.Enabled = false; 

            if (!_dirview)
            {
                _parentFrm = this.Parent.Parent.Parent as PblRepsForm;
                _testtree = _parentFrm.ttf.tree;
                _belongobjname = (tnForm.Parent.Parent as TreeNode).Text;
                //_belobjabbr = (tnForm.Parent.Parent as TreeNode).Tag as string;
            }
            else
            {
                if (ExecStatus.g_ttf == null)
                {
                    ExecStatus.g_ttf = new TestTreeForm();
                    ExecStatus.g_ttf.BuildTestTree();
                }
                _testtree = ExecStatus.g_ttf.tree;
            }

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

            // 初始化"问题类别"和"问题级别"RadioButtons
            if (!CommonDB.InitRBsFromDB(MyBaseForm.dbProject, radioPC1.Location,
                radioPC5.Location, btn1, (string)MyBaseForm.pid, UseVid, "类别"))
            {
                //MessageBox.Show("加载\'问题类别\'定义发生错误!", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.Close();
                //return;
            }

            if (!CommonDB.InitRBsFromDB(MyBaseForm.dbProject, radioPL1.Location,
                radioPL5.Location, btn2, (string)MyBaseForm.pid, UseVid, "级别"))
            {
                //MessageBox.Show("加载\'问题级别\'定时发生错误!", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Close();
                //return;
            }

            if (!_dirview)
                _pr = CommonDB.GenPRobj(dbProject, _pblid, _parentFrm.PblSpl, _belongobjname, 
                    _belobjabbr, _parentFrm.tblPblSigns);
            else
                _pr = CommonDB.GenPRobj(dbProject, _pblid, ConstDef.PblSplitter(), _belongobjname,
                    _belobjabbr, 
                    CommonDB.GetPblSigns(dbProject, (string)pid, UseVid));
             InitControl(_pr);
            //if (ExecStatus.g_PblRepsRFormALink != null)
            //{
            //    ExecStatus.g_PblRepsRFormALink.Close();
            //    ExecStatus.g_PblRepsRFormALink = null;
            //}

             if (_dirview ||
                 !_parentFrm.PreRegress)
                 HideRegress();

             FrmCommonFunc.UniformGrid(this.grid1, System.Drawing.Color.DarkBlue,
                 System.Drawing.Color.Gainsboro, 28, 20);
             grid1.Styles["EvenRow"].BackColor = System.Drawing.Color.Honeydew;
             grid1.Styles["OddRow"].BackColor = System.Drawing.Color.LavenderBlush;
             C1DisplayColumn d1 = grid1.Splits[0].DisplayColumns["测试用例章节号"];
             d1.Width = 120;
             d1 = grid1.Splits[0].DisplayColumns["测试用例名称"];
             d1.Width = 150;
        }

        private void HideRegress()
        {
            this.cbRegress.TabStop = false;
            this.rich2.TabStop = false;
            this.tableLayoutPanel3.Visible = false;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanel2, 2);
            this.Height = 660;

            this.tableLayoutPanel2.Enabled = true;
        }

        private bool _datachanged = false;
        private void InitControl(PblRep pr)
        {
            if (pr == null)
                return;

            ProblemSign = pr.ToString();
            Reporter = pr.reporter;
            ReportDate = pr.repdate;
            FrmCommonFunc.SelectOneInRadios(pr.pblcat, radioPC1, radioPC2, radioPC3, radioPC4, radioPC5);
            FrmCommonFunc.SelectOneInRadios(pr.pbllel, radioPL1, radioPL2, radioPL3, radioPL4, radioPL5);
            ProblemDescrition = pr.pbldes;
            Memo = pr.pblmemo;
            NeedRegress = pr.regress;
            EffectAnalyse = pr.effanalyse;
            PblName = pr.pblname;

            _datachanged = false;

            FrmCommonFunc.InitHyperLinkGrid(this.grid1, pr, _testtree, UseVid);
        }

        private void PblRepsRightForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //OnPageClose(false);
        }

        public override bool OnPageClose(bool bClose)
        {
            if ((ProblemCategory == -1) ||
                (ProblemLevel == -1))
            {
                MessageBox.Show("\'问题类别\'或\'问题级别\'为空, 数据没有保存!", "数据保存失败",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_datachanged)
                UpdatePbl(_pr);
            return true;
        }

        #endregion 窗体

        #region public properties

        /// <summary>
        /// 问题标识
        /// </summary>
        public string ProblemSign
        {
            set
            {
                txtbxPblSign.Text = value;
            }
        }

        /// <summary>
        /// 问题名称
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

        /// <summary>
        /// 报告人
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
        /// 报告日期
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
        /// 问题类别
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
        /// 问题级别
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

        /// <summary>
        /// 问题描述
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

        /// <summary>
        /// 附注及修改建议
        /// </summary>
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

        /// <summary>
        /// 影响域分析
        /// </summary>
        public byte[] EffectAnalyse
        {
            set
            {
                rich2.SetRichData(value);
            }
            get
            {
                return rich2.GetRichData();
            }
        }

        /// <summary>
        /// 处理措施 0:不更改 1:更改
        /// </summary>
        public int NeedRegress
        {
            get
            {
                if (this.cbRegress.Checked)
                    return 1;
                else
                    return 0;
            }
            set
            {
                switch (value)
                {
                    case 0:
                        this.cbRegress.Checked = false;
                        break;

                    case 1:
                        this.cbRegress.Checked = true;
                        break;

                    default:
                        this.cbRegress.Checked = false;
                        break;
                }
            }
        }

        #endregion

        #region 事件处理

        private void txtbxReporter_TextChanged(object sender, EventArgs e)
        {
            if (sender is DropDownTextBox)
            {
                _datachanged = true;

                DropDownTextBox ddt = (DropDownTextBox)sender;
                if (ddt.Text.Length == 0)
                {
                    label2.Text = "报告人*";
                    errorProvider1.SetIconAlignment(this.txtbxReporter, ErrorIconAlignment.MiddleRight);
                    errorProvider1.SetError(this.txtbxReporter, "\'报告人\'不能为空!!");
                }

                else
                {
                    label2.Text = "报告人";
                    errorProvider1.SetError((Control)sender, string.Empty);
                }
            }
        }

        private void dtReportDate_ValueChanged(object sender, EventArgs e)
        {
            this._datachanged = true;
        }

        /// <summary>
        /// 同步更新左树相应树节点文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbxPblName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tnForm.Text = Z1Utils.ZString.ReplaceNodeText(tnForm.Text, PblName);
        }

        #endregion 事件处理

        #region 内部方法

        private void UpdatePbl(PblRep pr)
        {
            pr.reporter = Reporter;
            pr.repdate = ReportDate;
            pr.pblcat = GetPblCatID();
            pr.pbllel = GetPblLelID();
            pr.pbldes = ProblemDescrition;
            pr.pblmemo = this.tbMemo.Text;
            pr.regress = NeedRegress;
            pr.effanalyse = EffectAnalyse;
            pr.pblname = PblName;

            CommonDB.UpdatePbl(MyBaseForm.dbProject, pr);
        }


        #endregion 内部方法
    }
}