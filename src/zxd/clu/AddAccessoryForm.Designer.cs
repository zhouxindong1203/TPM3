namespace TMP3.zxd.clu
{
    partial class AddAccessoryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtbxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbxMemo = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbxSym = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSelectExists = new System.Windows.Forms.RadioButton();
            this.rbNewAcc = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddAcc = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbAccLists = new System.Windows.Forms.ComboBox();
            this.chboxOutput = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "附件名称 ";
            // 
            // txtbxName
            // 
            this.txtbxName.Location = new System.Drawing.Point(87, 23);
            this.txtbxName.Name = "txtbxName";
            this.txtbxName.ReadOnly = true;
            this.txtbxName.Size = new System.Drawing.Size(289, 21);
            this.txtbxName.TabIndex = 1;
            this.txtbxName.TextChanged += new System.EventHandler(this.txtbxName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "备注";
            // 
            // txtbxMemo
            // 
            this.txtbxMemo.Location = new System.Drawing.Point(87, 50);
            this.txtbxMemo.Multiline = true;
            this.txtbxMemo.Name = "txtbxMemo";
            this.txtbxMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbxMemo.Size = new System.Drawing.Size(289, 37);
            this.txtbxMemo.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(430, 229);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(511, 229);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "附件标识";
            // 
            // txtbxSym
            // 
            this.txtbxSym.Location = new System.Drawing.Point(108, 24);
            this.txtbxSym.Name = "txtbxSym";
            this.txtbxSym.ReadOnly = true;
            this.txtbxSym.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtbxSym.Size = new System.Drawing.Size(289, 21);
            this.txtbxSym.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSelectExists);
            this.groupBox1.Controls.Add(this.rbNewAcc);
            this.groupBox1.Location = new System.Drawing.Point(430, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 97);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "添加附件方式";
            // 
            // rbSelectExists
            // 
            this.rbSelectExists.AutoSize = true;
            this.rbSelectExists.CausesValidation = false;
            this.rbSelectExists.Location = new System.Drawing.Point(21, 61);
            this.rbSelectExists.Name = "rbSelectExists";
            this.rbSelectExists.Size = new System.Drawing.Size(119, 16);
            this.rbSelectExists.TabIndex = 1;
            this.rbSelectExists.TabStop = true;
            this.rbSelectExists.Text = "从已有附件中选取";
            this.rbSelectExists.UseVisualStyleBackColor = true;
            this.rbSelectExists.CheckedChanged += new System.EventHandler(this.rbNewAcc_CheckedChanged);
            // 
            // rbNewAcc
            // 
            this.rbNewAcc.AutoSize = true;
            this.rbNewAcc.CausesValidation = false;
            this.rbNewAcc.Location = new System.Drawing.Point(21, 30);
            this.rbNewAcc.Name = "rbNewAcc";
            this.rbNewAcc.Size = new System.Drawing.Size(71, 16);
            this.rbNewAcc.TabIndex = 0;
            this.rbNewAcc.TabStop = true;
            this.rbNewAcc.Text = "新建附件";
            this.rbNewAcc.UseVisualStyleBackColor = true;
            this.rbNewAcc.CheckedChanged += new System.EventHandler(this.rbNewAcc_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAddAcc);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtbxName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtbxMemo);
            this.groupBox2.Location = new System.Drawing.Point(21, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(393, 122);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "新建附件";
            // 
            // btnAddAcc
            // 
            this.btnAddAcc.Location = new System.Drawing.Point(270, 93);
            this.btnAddAcc.Name = "btnAddAcc";
            this.btnAddAcc.Size = new System.Drawing.Size(106, 23);
            this.btnAddAcc.TabIndex = 4;
            this.btnAddAcc.Text = "选择附件...";
            this.btnAddAcc.UseVisualStyleBackColor = true;
            this.btnAddAcc.Click += new System.EventHandler(this.btnAddAcc_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbAccLists);
            this.groupBox3.Location = new System.Drawing.Point(21, 194);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(393, 58);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "从已有附件中选取";
            // 
            // cmbAccLists
            // 
            this.cmbAccLists.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccLists.FormattingEnabled = true;
            this.cmbAccLists.Location = new System.Drawing.Point(87, 20);
            this.cmbAccLists.Name = "cmbAccLists";
            this.cmbAccLists.Size = new System.Drawing.Size(289, 20);
            this.cmbAccLists.TabIndex = 0;
            this.cmbAccLists.SelectedIndexChanged += new System.EventHandler(this.cmbAccLists_SelectedIndexChanged);
            // 
            // chboxOutput
            // 
            this.chboxOutput.AutoSize = true;
            this.chboxOutput.Location = new System.Drawing.Point(451, 132);
            this.chboxOutput.Name = "chboxOutput";
            this.chboxOutput.Size = new System.Drawing.Size(114, 16);
            this.chboxOutput.TabIndex = 7;
            this.chboxOutput.Text = "可否打印(显示)?";
            this.chboxOutput.UseVisualStyleBackColor = true;
            // 
            // AddAccessoryForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(602, 269);
            this.Controls.Add(this.chboxOutput);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtbxSym);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddAccessoryForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加附件";
            this.Load += new System.EventHandler(this.AddAccessoryForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbxMemo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbxSym;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSelectExists;
        private System.Windows.Forms.RadioButton rbNewAcc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbAccLists;
        private System.Windows.Forms.Button btnAddAcc;
        private System.Windows.Forms.CheckBox chboxOutput;
    }
}