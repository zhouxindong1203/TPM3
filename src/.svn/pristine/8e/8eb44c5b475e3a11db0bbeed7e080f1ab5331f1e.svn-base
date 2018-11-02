using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TPM3.Upgrade
{
    public partial class ConvertProcessForm : Form
    {
        private IDatabaseConvertor _convertor;

        public ConvertProcessForm(IDatabaseConvertor convertor)
        {
            InitializeComponent();

            this._convertor = convertor;
            //this.label1.DataBindings.Add("Text", _convertor, "StatusMsg");
            _convertor.StatusChange += new EventHandler(UpdateLabel);

        }

        private void UpdateLabel(object sender, EventArgs ev)
        {
            this.label1.Text = _convertor.StatusMsg;
            //this.label1.Invalidate();
            this.label1.Refresh();
            //Thread.Sleep(100);
        }
    }

    public interface IDatabaseConvertor
    {
        bool StartConvert();
        bool CanConvert();
        string GetStatusMsg();
        string GetDesFileName();

        string StatusMsg { get; set; }

        event EventHandler StatusChange;
    }
}