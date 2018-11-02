using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TPM3.zxd.clu
{
    public partial class InputText : Form
    {
        public InputText()
        {
            InitializeComponent();
        }

        public string InputReason
        {
            get
            {
                return this.txtbxReason.Text;
            }
        }
    }
}
