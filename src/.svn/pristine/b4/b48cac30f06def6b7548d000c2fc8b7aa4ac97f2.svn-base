using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TPM3.zxd.clu
{
    public partial class PasteItemSelForm : Form
    {
        public PasteItemSelForm()
        {
            InitializeComponent();
            PasteSel = 0;
        }

        public int PasteSel
        {
            get
            {
                if (this.rbPasteSelf.Checked)
                    return 0;
                else if (this.rbPasteAll.Checked)
                    return 1;
                else
                    return -1;
            }

            set
            {
                switch (value)
                {
                    case 0:
                        this.rbPasteSelf.Checked = true;
                        break;

                    case 1:
                        this.rbPasteAll.Checked = true;
                        break;

                    default:
                        this.rbPasteSelf.Checked = false;
                        this.rbPasteAll.Checked = false;
                        break;
                }
            }
        }
        public void DisablePasteSub()
        {
            this.rbPasteAll.Enabled = false;
        }
    }
}