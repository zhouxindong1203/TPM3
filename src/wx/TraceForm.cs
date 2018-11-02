using System;
using System.Data;
using System.Windows.Forms;
using Common;

namespace TPM3.Sys
{
    public partial class TraceForm : Form
    {
        Control obj;
        public TraceForm(object baseControl)
        {
            InitializeComponent();
            obj = FormClass.CreateClass(baseControl as string) as Control;
            this.panel2.Controls.Add(obj);
            obj.Dock = DockStyle.Fill;
            obj.Visible = true;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width - 100;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height - 100;
        }

        void TraceForm_Load(object sender, EventArgs e)
        {
            IBaseTreeForm bf = obj as IBaseTreeForm;
            if( bf != null )
                bf.OnPageCreate();
        }

        void TraceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IBaseTreeForm bf = obj as IBaseTreeForm;
            if( bf != null )
                bf.OnPageClose(true);
        }

        void btQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}