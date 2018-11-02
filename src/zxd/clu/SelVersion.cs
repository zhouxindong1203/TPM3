using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TPM3.wx;
using Common.Database;
using TPM3.Sys;
using Z1.tpm.DB;
using Z1.tpm;

namespace TPM3.zxd.clu
{
    public partial class SelVersion : Form
    {
        private DBAccess _db;

        private bool _pidvalue = false;
        private bool _vidvalue = false;
        private bool _firstld = true;

        public SelVersion(DBAccess db)
        {
            InitializeComponent();

            this._db = db;
        }

        private void SelVersion_Load(object sender, EventArgs e)
        {
            LoadProjs();
        }

        private void LoadProjs()
        {
            this.comboBox2.DataSource = null;

            this.comboBox2.DataSource = ProjectInfo.GetAllProjs(_db); // 此处会引发comboBox2的SelectedIndexChanged事件
            this.comboBox2.DisplayMember = "Name";
            this.comboBox2.ValueMember = "ID";

            _firstld = false;

            if (PID != null)
            {
                _pidvalue = true;
                LoadVers(PID.ID);
            }
            else
            {
                _pidvalue = false;
                LoadVers(null);
                CheckOK();
            }
        }

        private void LoadVers(object pid)
        {
            this.comboBox1.DataSource = null;

            if (pid == null)
            {
                _vidvalue = false;
                CheckOK();
                return;
            }

            this.comboBox1.DataSource = ProjectInfo.GetAllVersForProj(_db, pid);
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "ID";

            if (VerID != null)
            {
                _vidvalue = true;
                CheckOK();
            }
            else
            {
                _vidvalue = false;
                CheckOK();
            }
        }

        public aVersion VerID
        {
            get
            {
                if (this.comboBox1.SelectedIndex != -1)
                    return this.comboBox1.SelectedItem as aVersion;
                else
                    return null;
            }
        }

        public aVersion PID
        {
            get
            {
                if (this.comboBox2.SelectedIndex != -1)
                    return this.comboBox2.SelectedItem as aVersion;
                else
                    return null;
            }
        }

        private void CheckOK()
        {
            this.btnOK.Enabled = _vidvalue && _pidvalue;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!_firstld)
                LoadVers(PID.ID);
        }
    }
}
