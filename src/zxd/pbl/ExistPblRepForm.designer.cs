namespace TPM3.zxd.pbl
{
    partial class ExistPblRepForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExistPblRepForm));
            this.rich1 = new Common.RichTextBox.RichTextBoxOle();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tbMemo = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioPL5 = new System.Windows.Forms.RadioButton();
            this.radioPL4 = new System.Windows.Forms.RadioButton();
            this.radioPL3 = new System.Windows.Forms.RadioButton();
            this.radioPL2 = new System.Windows.Forms.RadioButton();
            this.radioPL1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioPC5 = new System.Windows.Forms.RadioButton();
            this.radioPC4 = new System.Windows.Forms.RadioButton();
            this.radioPC3 = new System.Windows.Forms.RadioButton();
            this.radioPC2 = new System.Windows.Forms.RadioButton();
            this.radioPC1 = new System.Windows.Forms.RadioButton();
            this.dtReportDate = new System.Windows.Forms.DateTimePicker();
            this.dtFindDate = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbxReporter = new Common.DropDownTextBox();
            this.lblReporter = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbProblemSign = new System.Windows.Forms.ComboBox();
            this.grid1 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // rich1
            // 
            this.rich1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rich1.HideSelection = false;
            this.rich1.Location = new System.Drawing.Point(27, 313);
            this.rich1.Name = "rich1";
            this.rich1.OleReadOnly = false;
            this.rich1.Size = new System.Drawing.Size(556, 218);
            this.rich1.TabIndex = 12;
            this.rich1.Text = "";
            this.rich1.TextChanged += new System.EventHandler(this.dtReportDate_ValueChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(508, 634);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "ȡ��";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(427, 634);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "ȷ��";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbMemo
            // 
            this.tbMemo.AcceptsReturn = true;
            this.tbMemo.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMemo.Location = new System.Drawing.Point(29, 559);
            this.tbMemo.Multiline = true;
            this.tbMemo.Name = "tbMemo";
            this.tbMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMemo.Size = new System.Drawing.Size(554, 59);
            this.tbMemo.TabIndex = 14;
            this.tbMemo.TextChanged += new System.EventHandler(this.dtReportDate_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioPL5);
            this.groupBox2.Controls.Add(this.radioPL4);
            this.groupBox2.Controls.Add(this.radioPL3);
            this.groupBox2.Controls.Add(this.radioPL2);
            this.groupBox2.Controls.Add(this.radioPL1);
            this.groupBox2.Location = new System.Drawing.Point(29, 245);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(554, 39);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "���⼶��";
            // 
            // radioPL5
            // 
            this.radioPL5.AutoSize = true;
            this.radioPL5.Location = new System.Drawing.Point(439, 15);
            this.radioPL5.Name = "radioPL5";
            this.radioPL5.Size = new System.Drawing.Size(77, 16);
            this.radioPL5.TabIndex = 4;
            this.radioPL5.TabStop = true;
            this.radioPL5.Text = "�������5";
            this.radioPL5.UseVisualStyleBackColor = true;
            this.radioPL5.CheckedChanged += new System.EventHandler(this.dtReportDate_ValueChanged);
            // 
            // radioPL4
            // 
            this.radioPL4.AutoSize = true;
            this.radioPL4.Location = new System.Drawing.Point(339, 15);
            this.radioPL4.Name = "radioPL4";
            this.radioPL4.Size = new System.Drawing.Size(77, 16);
            this.radioPL4.TabIndex = 3;
            this.radioPL4.TabStop = true;
            this.radioPL4.Text = "���⼶��4";
            this.radioPL4.UseVisualStyleBackColor = true;
            this.radioPL4.CheckedChanged += new System.EventHandler(this.dtReportDate_ValueChanged);
            // 
            // radioPL3
            // 
            this.radioPL3.AutoSize = true;
            this.radioPL3.Location = new System.Drawing.Point(239, 15);
            this.radioPL3.Name = "radioPL3";
            this.radioPL3.Size = new System.Drawing.Size(77, 16);
            this.radioPL3.TabIndex = 2;
            this.radioPL3.TabStop = true;
            this.radioPL3.Text = "���⼶��3";
            this.radioPL3.UseVisualStyleBackColor = true;
            this.radioPL3.CheckedChanged += new System.EventHandler(this.dtReportDate_ValueChanged);
            // 
            // radioPL2
            // 
            this.radioPL2.AutoSize = true;
            this.radioPL2.Location = new System.Drawing.Point(139, 15);
            this.radioPL2.Name = "radioPL2";
            this.radioPL2.Size = new System.Drawing.Size(77, 16);
            this.radioPL2.TabIndex = 1;
            this.radioPL2.TabStop = true;
            this.radioPL2.Text = "���⼶��2";
            this.radioPL2.UseVisualStyleBackColor = true;
            this.radioPL2.CheckedChanged += new System.EventHandler(this.dtReportDate_ValueChanged);
            // 
            // radioPL1
            // 
            this.radioPL1.AutoSize = true;
            this.radioPL1.Location = new System.Drawing.Point(39, 15);
            this.radioPL1.Name = "radioPL1";
            this.radioPL1.Size = new System.Drawing.Size(77, 16);
            this.radioPL1.TabIndex = 0;
            this.radioPL1.TabStop = true;
            this.radioPL1.Text = "���⼶��1";
            this.radioPL1.UseVisualStyleBackColor = true;
            this.radioPL1.CheckedChanged += new System.EventHandler(this.dtReportDate_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioPC5);
            this.groupBox1.Controls.Add(this.radioPC4);
            this.groupBox1.Controls.Add(this.radioPC3);
            this.groupBox1.Controls.Add(this.radioPC2);
            this.groupBox1.Controls.Add(this.radioPC1);
            this.groupBox1.Location = new System.Drawing.Point(29, 199);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(554, 40);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "�������";
            // 
            // radioPC5
            // 
            this.radioPC5.AutoSize = true;
            this.radioPC5.Location = new System.Drawing.Point(439, 16);
            this.radioPC5.Name = "radioPC5";
            this.radioPC5.Size = new System.Drawing.Size(77, 16);
            this.radioPC5.TabIndex = 4;
            this.radioPC5.TabStop = true;
            this.radioPC5.Text = "�������5";
            this.radioPC5.UseVisualStyleBackColor = true;
            this.radioPC5.CheckedChanged += new System.EventHandler(this.dtReportDate_ValueChanged);
            // 
            // radioPC4
            // 
            this.radioPC4.AutoSize = true;
            this.radioPC4.Location = new System.Drawing.Point(339, 16);
            this.radioPC4.Name = "radioPC4";
            this.radioPC4.Size = new System.Drawing.Size(77, 16);
            this.radioPC4.TabIndex = 3;
            this.radioPC4.TabStop = true;
            this.radioPC4.Text = "�������4";
            this.radioPC4.UseVisualStyleBackColor = true;
            this.radioPC4.CheckedChanged += new System.EventHandler(this.dtReportDate_ValueChanged);
            // 
            // radioPC3
            // 
            this.radioPC3.AutoSize = true;
            this.radioPC3.Location = new System.Drawing.Point(239, 16);
            this.radioPC3.Name = "radioPC3";
            this.radioPC3.Size = new System.Drawing.Size(77, 16);
            this.radioPC3.TabIndex = 2;
            this.radioPC3.TabStop = true;
            this.radioPC3.Text = "�������3";
            this.radioPC3.UseVisualStyleBackColor = true;
            this.radioPC3.CheckedChanged += new System.EventHandler(this.dtReportDate_ValueChanged);
            // 
            // radioPC2
            // 
            this.radioPC2.AutoSize = true;
            this.radioPC2.Location = new System.Drawing.Point(139, 16);
            this.radioPC2.Name = "radioPC2";
            this.radioPC2.Size = new System.Drawing.Size(77, 16);
            this.radioPC2.TabIndex = 1;
            this.radioPC2.TabStop = true;
            this.radioPC2.Text = "�������2";
            this.radioPC2.UseVisualStyleBackColor = true;
            this.radioPC2.CheckedChanged += new System.EventHandler(this.dtReportDate_ValueChanged);
            // 
            // radioPC1
            // 
            this.radioPC1.AutoSize = true;
            this.radioPC1.Location = new System.Drawing.Point(39, 16);
            this.radioPC1.Name = "radioPC1";
            this.radioPC1.Size = new System.Drawing.Size(77, 16);
            this.radioPC1.TabIndex = 0;
            this.radioPC1.TabStop = true;
            this.radioPC1.Text = "�������1";
            this.radioPC1.UseVisualStyleBackColor = true;
            this.radioPC1.CheckedChanged += new System.EventHandler(this.dtReportDate_ValueChanged);
            // 
            // dtReportDate
            // 
            this.dtReportDate.Location = new System.Drawing.Point(415, 37);
            this.dtReportDate.Name = "dtReportDate";
            this.dtReportDate.Size = new System.Drawing.Size(168, 21);
            this.dtReportDate.TabIndex = 7;
            this.dtReportDate.ValueChanged += new System.EventHandler(this.dtReportDate_ValueChanged);
            // 
            // dtFindDate
            // 
            this.dtFindDate.Location = new System.Drawing.Point(131, 0);
            this.dtFindDate.Name = "dtFindDate";
            this.dtFindDate.Size = new System.Drawing.Size(168, 21);
            this.dtFindDate.TabIndex = 5;
            this.dtFindDate.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 544);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "��ע���޸Ľ���";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 298);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "��������";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "��������";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "��������";
            this.label3.Visible = false;
            // 
            // txtbxReporter
            // 
            this.txtbxReporter.BackColor = System.Drawing.Color.White;
            this.txtbxReporter.DataSource = null;
            this.txtbxReporter.Location = new System.Drawing.Point(86, 38);
            this.txtbxReporter.Multiline = true;
            this.txtbxReporter.Name = "txtbxReporter";
            this.txtbxReporter.ReadOnly = true;
            this.txtbxReporter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbxReporter.Size = new System.Drawing.Size(242, 21);
            this.txtbxReporter.TabIndex = 3;
            this.txtbxReporter.TextChanged += new System.EventHandler(this.txtbxReporter_TextChanged);
            // 
            // lblReporter
            // 
            this.lblReporter.AutoSize = true;
            this.lblReporter.Location = new System.Drawing.Point(27, 41);
            this.lblReporter.Name = "lblReporter";
            this.lblReporter.Size = new System.Drawing.Size(41, 12);
            this.lblReporter.TabIndex = 2;
            this.lblReporter.Text = "������";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "�����ʶ";
            // 
            // cmbProblemSign
            // 
            this.cmbProblemSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProblemSign.FormattingEnabled = true;
            this.cmbProblemSign.Location = new System.Drawing.Point(86, 12);
            this.cmbProblemSign.Name = "cmbProblemSign";
            this.cmbProblemSign.Size = new System.Drawing.Size(497, 20);
            this.cmbProblemSign.TabIndex = 1;
            this.cmbProblemSign.SelectedIndexChanged += new System.EventHandler(this.cmbProblemSign_SelectedIndexChanged);
            // 
            // grid1
            // 
            this.grid1.AllowUpdate = false;
            this.grid1.AlternatingRows = true;
            this.grid1.Caption = "�����Ĳ�������";
            this.grid1.GroupByCaption = "Drag a column header here to group by that column";
            this.grid1.Images.Add(((System.Drawing.Image)(resources.GetObject("grid1.Images"))));
            this.grid1.Location = new System.Drawing.Point(29, 65);
            this.grid1.Name = "grid1";
            this.grid1.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.grid1.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.grid1.PreviewInfo.ZoomFactor = 75D;
            this.grid1.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("grid1.PrintInfo.PageSettings")));
            this.grid1.Size = new System.Drawing.Size(554, 128);
            this.grid1.TabIndex = 8;
            this.grid1.Text = "c1TrueDBGrid1";
            this.grid1.PropBag = resources.GetString("grid1.PropBag");
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ExistPblRepForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(0, 675);
            this.ClientSize = new System.Drawing.Size(611, 668);
            this.ControlBox = false;
            this.Controls.Add(this.grid1);
            this.Controls.Add(this.cmbProblemSign);
            this.Controls.Add(this.rich1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbMemo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtReportDate);
            this.Controls.Add(this.dtFindDate);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtbxReporter);
            this.Controls.Add(this.lblReporter);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ExistPblRepForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "���ύ�����ⱨ�浥";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExistPblRepForm_FormClosing);
            this.Load += new System.EventHandler(this.ExistPblRepForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Common.RichTextBox.RichTextBoxOle rich1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbMemo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioPL5;
        private System.Windows.Forms.RadioButton radioPL4;
        private System.Windows.Forms.RadioButton radioPL3;
        private System.Windows.Forms.RadioButton radioPL2;
        private System.Windows.Forms.RadioButton radioPL1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioPC5;
        private System.Windows.Forms.RadioButton radioPC4;
        private System.Windows.Forms.RadioButton radioPC3;
        private System.Windows.Forms.RadioButton radioPC2;
        private System.Windows.Forms.RadioButton radioPC1;
        private System.Windows.Forms.DateTimePicker dtReportDate;
        private System.Windows.Forms.DateTimePicker dtFindDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Common.DropDownTextBox txtbxReporter;
        private System.Windows.Forms.Label lblReporter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbProblemSign;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}