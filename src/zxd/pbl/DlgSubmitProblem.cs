using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TPM3.zxd.pbl
{
    public partial class DlgSubmitProblem : Form
    {
        public DlgSubmitProblem()
        {
            InitializeComponent();

            this.radioNewProblem.Tag = false;
            this.btnOK.Enabled = false;
        }

        #region public fields

        /// <summary>
        /// 设置或获取提交问题方式(0: 提交新问题; 1: 从已有问题中选取)
        /// 返回-1表示当前无选中项
        /// </summary>
        public int SubmitType
        {
            set
            {
                switch (value)
                {
                    case 0:
                        this.radioNewProblem.Checked = true;
                        break;

                    case 1:
                        this.radioUseExistProblem.Checked = true;
                        break;

                    default:
                        this.radioNewProblem.Checked = false;
                        this.radioUseExistProblem.Checked = false;
                        break;
                }

                ValidateOK();
            }

            get
            {
                if (this.radioNewProblem.Checked)
                    return 0;
                else if (this.radioUseExistProblem.Checked)
                    return 1;
                else
                    return -1;
            }
        }

        #endregion

        private void ValidateOK()
        {
            this.btnOK.Enabled = (bool)this.radioNewProblem.Tag;
        }

        private void radioNewProblem_Click(object sender, EventArgs e)
        {
            this.radioNewProblem.Tag = true;

            ValidateOK();
        }
    }
}