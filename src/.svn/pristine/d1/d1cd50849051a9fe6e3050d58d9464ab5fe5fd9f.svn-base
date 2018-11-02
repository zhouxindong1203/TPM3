using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TPM3.zxd.clu
{
    public partial class FindUsecaseForm : Form
    {
        public FindUsecaseForm()
        {
            InitializeComponent();

            this.btnFind.Enabled = false;
            this.txtbxFindWhat.Tag = false;
        }

        private void ValidateOK()
        {
            btnFind.Enabled = (bool)this.txtbxFindWhat.Tag;
        }

        private void txtbxFindWhat_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null)
                return;

            if (tb.Text.Length == 0)
            {
                tb.Tag = false;
                tb.BackColor = Color.LightPink;
            }
            else
            {
                tb.Tag = true;
                tb.BackColor = SystemColors.Window;
            }

            ValidateOK();
        }

        private void txtbxFindWhat_TextChanged(object sender, EventArgs e)
        {
            this.txtbxFindWhat_Validating(sender, null);
        }

        #region public properties

        public string FindWhat
        {
            get
            {
                return this.txtbxFindWhat.Text;
            }
            set
            {
                this.txtbxFindWhat.Text = value;
            }
        }

        #endregion
    }
}