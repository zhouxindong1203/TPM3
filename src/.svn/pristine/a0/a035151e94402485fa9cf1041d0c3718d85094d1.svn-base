using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TPM3.zxd.clu
{
    public partial class PasteUsecaseSelForm : Form
    {
        public PasteUsecaseSelForm()
        {
            InitializeComponent();
            PasteSel = 1;
        }

        public void DisableShortcut()
        {
            this.rbShortcut.Enabled = false;
        }

        public int PasteSel
        {
            get
            {
                if (this.rbShortcut.Checked)
                    return 0;
                else if (this.rbPaste.Checked)
                    return 1;
                else
                    return -1;
            }

            set
            {
                switch (value)
                {
                    case 0:
                        this.rbShortcut.Checked = true;
                        break;

                    case 1:
                        this.rbPaste.Checked = true;
                        break;

                    default:
                        this.rbShortcut.Checked = false;
                        this.rbPaste.Checked = false;
                        break;
                }
            }
        }
    }
}