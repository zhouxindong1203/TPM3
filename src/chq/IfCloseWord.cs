using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TPM3.chq
{
    public partial class IfCloseWord : Form
    {
        public bool JXFlag = false;

        public IfCloseWord()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void IfCloseWord_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            JXFlag = true;
            IfCloseWord.ActiveForm.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            JXFlag = false;
            IfCloseWord.ActiveForm.Close();
        }
    }
}
