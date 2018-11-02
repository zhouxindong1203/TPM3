using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Z1.tpm.DB;
using TPM3.zxd;
using System.Data.Common;
using TPM3.Sys;
using Z1.tpm;

namespace TMP3.zxd.clu
{
    public partial class AddAccessoryForm : Form
    {
        public string p_filename;
        public string p_fullpath;
        public byte[] p_bdata;
        public string p_ext;

        public AddAccessoryForm()
        {
            InitializeComponent();
        }

        private void AddAccessoryForm_Load(object sender, EventArgs e)
        {
            this.btnOK.Enabled = false;

            InitExistAccsList();
        }

        #region 事件处理

        private void cmbAccLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateOK();
        }

        private void txtbxName_TextChanged(object sender, EventArgs e)
        {
            ValidateOK();
        }
        
        private void ValidateOK()
        {
            if (AddStyle == 0)
                btnOK.Enabled = !this.txtbxName.Text.Equals(string.Empty);
            else if(AddStyle == 1)
            {
                btnOK.Enabled = (AccListsSelIndex != -1);
            }
        }

        private void rbNewAcc_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbNewAcc.Checked)
            {
                this.groupBox2.Enabled = true;
                this.groupBox3.Enabled = false;
                this.chboxOutput.Enabled = true;
            }
            else
            {
                this.groupBox2.Enabled = false;
                this.groupBox3.Enabled = true;
                this.chboxOutput.Enabled = false;
            }

            ValidateOK();
        }

        private void btnAddAcc_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "添加附件";
            dlg.ShowReadOnly = true;
            dlg.ValidateNames = true;
            dlg.CheckFileExists = true;
            dlg.ReadOnlyChecked = true;
            dlg.Filter = "所有文件(*.*)|*.*|位图文件(.bmp)|*.bmp|图像文件(.jpg)|*.jpg|Word文档(.doc)|*.doc";
            dlg.FilterIndex = 1;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FileInfo fi;
                try
                {
                    fi = new FileInfo(dlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("读取附件文件出错: " + ex.Message, "添加附件失败", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                if (fi.Length > ConstDef.MaxAccFileSize)
                {
                    // 文件尺寸大于阈值时不能打印输出
                    if (this.Output)
                        this.Output = false;
                    this.chboxOutput.Enabled = false;
                }
                else
                    this.chboxOutput.Enabled = true;

                p_fullpath = dlg.FileName;
                p_filename = Path.GetFileName(p_fullpath);

                p_ext = Path.GetExtension(p_filename);

                AccName = p_filename;
            }
        }

        #endregion 事件处理

        #region 属性

        // 添加附件方式
        public int AddStyle
        {
            get
            {
                if (this.rbNewAcc.Checked)
                    return 0;
                else if (this.rbSelectExists.Checked)
                    return 1;
                else
                    return -1;
            }
            set
            {
                switch (value)
                {
                    case 0:
                        this.rbNewAcc.Checked = true;
                        break;

                    case 1:
                        this.rbSelectExists.Checked = true;
                        break;

                    default:
                        this.rbNewAcc.Checked = false;
                        this.rbSelectExists.Checked = false;
                        break;
                }
            }
        }
                
        public int AccListsSelIndex
        {
            get
            {
                return this.cmbAccLists.SelectedIndex;
            }
            set
            {
                if (value < 0 || value > cmbAccLists.Items.Count - 1)
                    this.cmbAccLists.SelectedIndex = -1;
                else
                    this.cmbAccLists.SelectedIndex = value;
            }
        }

        public Accessory SelExistAcc
        {
            get
            {
                return this.cmbAccLists.SelectedItem as Accessory;
            }
        }

        public string AccName
        { 
            get
            {
                return this.txtbxName.Text;
            }
            set
            {
                this.txtbxName.Text = value;
            }
        }

        public string Memo
        {
            get
            {
                return this.txtbxMemo.Text;
            }
            set
            {
                this.txtbxMemo.Text = value;
            }
        }
                
        public string AccSym
        {
            get
            {
                return this.txtbxSym.Text;
            }
            set
            {
                this.txtbxSym.Text = value;
            }
        }
                
        public bool Output
        {
            get
            {
                return this.chboxOutput.Checked;
            }
            set
            {
                this.chboxOutput.Checked = value;
            }
        }

        #endregion 属性

        #region 内部方法

        // 利用当前项目已有附件填充ComboList
        private void InitExistAccsList()
        {
            DataTable tbl = TestUsecase.GetAccsForProj(MyBaseForm.dbProject, (string)MyBaseForm.pid);

            foreach (DataRow row in tbl.Rows)
            {
                Accessory a = new Accessory();
                a.eid        = (string)row["ID"];
                a.name      = (string)row["附件名称"];
                a.usenum    = (int)row["关联数"];
                a.output    = (bool)row["输出与否"];

                this.cmbAccLists.Items.Add(a);
            }
        }

        #endregion 内部方法

    }
}