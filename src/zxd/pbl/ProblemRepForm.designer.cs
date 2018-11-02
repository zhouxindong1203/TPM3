namespace TPM3.zxd.pbl
{
    partial class ProblemRepForm
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
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.gbPblCat = new System.Windows.Forms.GroupBox();
            this.radioPC5 = new System.Windows.Forms.RadioButton();
            this.radioPC4 = new System.Windows.Forms.RadioButton();
            this.radioPC3 = new System.Windows.Forms.RadioButton();
            this.radioPC2 = new System.Windows.Forms.RadioButton();
            this.radioPC1 = new System.Windows.Forms.RadioButton();
            this.gbPblLel = new System.Windows.Forms.GroupBox();
            this.radioPL5 = new System.Windows.Forms.RadioButton();
            this.radioPL4 = new System.Windows.Forms.RadioButton();
            this.radioPL3 = new System.Windows.Forms.RadioButton();
            this.radioPL2 = new System.Windows.Forms.RadioButton();
            this.radioPL1 = new System.Windows.Forms.RadioButton();
            this.tbMemo = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rich1 = new Common.RichTextBox.RichTextBoxOle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbFouthSign = new System.Windows.Forms.ComboBox();
            this.dtFindDate = new System.Windows.Forms.DateTimePicker();
            this.txtbxUsecaseID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkFouthSign = new System.Windows.Forms.CheckBox();
            this.cmbThirdSign = new System.Windows.Forms.ComboBox();
            this.checkThirdSign = new System.Windows.Forms.CheckBox();
            this.cmbSecondSign = new System.Windows.Forms.ComboBox();
            this.checkSecondSign = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFirstSign = new System.Windows.Forms.ComboBox();
            this.checkFirstSign = new System.Windows.Forms.CheckBox();
            this.txtbxProblemID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtbxPblName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtReportDate = new System.Windows.Forms.DateTimePicker();
            this.txtbxUsecaseName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtbxReporter = new Common.DropDownTextBox();
            this.lblReporter = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbPblCat.SuspendLayout();
            this.gbPblLel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 308);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "问题描述";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 567);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "附注及建议";
            // 
            // gbPblCat
            // 
            this.gbPblCat.Controls.Add(this.radioPC5);
            this.gbPblCat.Controls.Add(this.radioPC4);
            this.gbPblCat.Controls.Add(this.radioPC3);
            this.gbPblCat.Controls.Add(this.radioPC2);
            this.gbPblCat.Controls.Add(this.radioPC1);
            this.gbPblCat.Location = new System.Drawing.Point(20, 206);
            this.gbPblCat.Name = "gbPblCat";
            this.gbPblCat.Size = new System.Drawing.Size(554, 41);
            this.gbPblCat.TabIndex = 2;
            this.gbPblCat.TabStop = false;
            this.gbPblCat.Text = "问题类别*";
            // 
            // radioPC5
            // 
            this.radioPC5.AutoSize = true;
            this.radioPC5.Location = new System.Drawing.Point(439, 16);
            this.radioPC5.Name = "radioPC5";
            this.radioPC5.Size = new System.Drawing.Size(77, 16);
            this.radioPC5.TabIndex = 4;
            this.radioPC5.TabStop = true;
            this.radioPC5.Text = "问题类别5";
            this.radioPC5.UseVisualStyleBackColor = true;
            this.radioPC5.Click += new System.EventHandler(this.radioPC1_Click);
            // 
            // radioPC4
            // 
            this.radioPC4.AutoSize = true;
            this.radioPC4.Location = new System.Drawing.Point(339, 16);
            this.radioPC4.Name = "radioPC4";
            this.radioPC4.Size = new System.Drawing.Size(77, 16);
            this.radioPC4.TabIndex = 3;
            this.radioPC4.TabStop = true;
            this.radioPC4.Text = "问题类别4";
            this.radioPC4.UseVisualStyleBackColor = true;
            this.radioPC4.Click += new System.EventHandler(this.radioPC1_Click);
            // 
            // radioPC3
            // 
            this.radioPC3.AutoSize = true;
            this.radioPC3.Location = new System.Drawing.Point(239, 16);
            this.radioPC3.Name = "radioPC3";
            this.radioPC3.Size = new System.Drawing.Size(77, 16);
            this.radioPC3.TabIndex = 2;
            this.radioPC3.TabStop = true;
            this.radioPC3.Text = "问题类别3";
            this.radioPC3.UseVisualStyleBackColor = true;
            this.radioPC3.Click += new System.EventHandler(this.radioPC1_Click);
            // 
            // radioPC2
            // 
            this.radioPC2.AutoSize = true;
            this.radioPC2.Location = new System.Drawing.Point(139, 16);
            this.radioPC2.Name = "radioPC2";
            this.radioPC2.Size = new System.Drawing.Size(77, 16);
            this.radioPC2.TabIndex = 1;
            this.radioPC2.TabStop = true;
            this.radioPC2.Text = "问题类别2";
            this.radioPC2.UseVisualStyleBackColor = true;
            this.radioPC2.Click += new System.EventHandler(this.radioPC1_Click);
            // 
            // radioPC1
            // 
            this.radioPC1.AutoSize = true;
            this.radioPC1.Location = new System.Drawing.Point(39, 16);
            this.radioPC1.Name = "radioPC1";
            this.radioPC1.Size = new System.Drawing.Size(77, 16);
            this.radioPC1.TabIndex = 0;
            this.radioPC1.TabStop = true;
            this.radioPC1.Text = "问题类别1";
            this.radioPC1.UseVisualStyleBackColor = true;
            this.radioPC1.Click += new System.EventHandler(this.radioPC1_Click);
            // 
            // gbPblLel
            // 
            this.gbPblLel.Controls.Add(this.radioPL5);
            this.gbPblLel.Controls.Add(this.radioPL4);
            this.gbPblLel.Controls.Add(this.radioPL3);
            this.gbPblLel.Controls.Add(this.radioPL2);
            this.gbPblLel.Controls.Add(this.radioPL1);
            this.gbPblLel.Location = new System.Drawing.Point(20, 253);
            this.gbPblLel.Name = "gbPblLel";
            this.gbPblLel.Size = new System.Drawing.Size(554, 37);
            this.gbPblLel.TabIndex = 3;
            this.gbPblLel.TabStop = false;
            this.gbPblLel.Text = "问题级别*";
            // 
            // radioPL5
            // 
            this.radioPL5.AutoSize = true;
            this.radioPL5.Location = new System.Drawing.Point(439, 14);
            this.radioPL5.Name = "radioPL5";
            this.radioPL5.Size = new System.Drawing.Size(77, 16);
            this.radioPL5.TabIndex = 4;
            this.radioPL5.TabStop = true;
            this.radioPL5.Text = "问题类别5";
            this.radioPL5.UseVisualStyleBackColor = true;
            this.radioPL5.Click += new System.EventHandler(this.radioPL1_Click);
            // 
            // radioPL4
            // 
            this.radioPL4.AutoSize = true;
            this.radioPL4.Location = new System.Drawing.Point(339, 14);
            this.radioPL4.Name = "radioPL4";
            this.radioPL4.Size = new System.Drawing.Size(77, 16);
            this.radioPL4.TabIndex = 3;
            this.radioPL4.TabStop = true;
            this.radioPL4.Text = "问题级别4";
            this.radioPL4.UseVisualStyleBackColor = true;
            this.radioPL4.Click += new System.EventHandler(this.radioPL1_Click);
            // 
            // radioPL3
            // 
            this.radioPL3.AutoSize = true;
            this.radioPL3.Location = new System.Drawing.Point(239, 14);
            this.radioPL3.Name = "radioPL3";
            this.radioPL3.Size = new System.Drawing.Size(77, 16);
            this.radioPL3.TabIndex = 2;
            this.radioPL3.TabStop = true;
            this.radioPL3.Text = "问题级别3";
            this.radioPL3.UseVisualStyleBackColor = true;
            this.radioPL3.Click += new System.EventHandler(this.radioPL1_Click);
            // 
            // radioPL2
            // 
            this.radioPL2.AutoSize = true;
            this.radioPL2.Location = new System.Drawing.Point(139, 14);
            this.radioPL2.Name = "radioPL2";
            this.radioPL2.Size = new System.Drawing.Size(77, 16);
            this.radioPL2.TabIndex = 1;
            this.radioPL2.TabStop = true;
            this.radioPL2.Text = "问题级别2";
            this.radioPL2.UseVisualStyleBackColor = true;
            this.radioPL2.Click += new System.EventHandler(this.radioPL1_Click);
            // 
            // radioPL1
            // 
            this.radioPL1.AutoSize = true;
            this.radioPL1.Location = new System.Drawing.Point(39, 14);
            this.radioPL1.Name = "radioPL1";
            this.radioPL1.Size = new System.Drawing.Size(77, 16);
            this.radioPL1.TabIndex = 0;
            this.radioPL1.TabStop = true;
            this.radioPL1.Text = "问题级别1";
            this.radioPL1.UseVisualStyleBackColor = true;
            this.radioPL1.Click += new System.EventHandler(this.radioPL1_Click);
            // 
            // tbMemo
            // 
            this.tbMemo.AcceptsReturn = true;
            this.tbMemo.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbMemo.Location = new System.Drawing.Point(20, 582);
            this.tbMemo.Multiline = true;
            this.tbMemo.Name = "tbMemo";
            this.tbMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMemo.Size = new System.Drawing.Size(554, 61);
            this.tbMemo.TabIndex = 7;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(404, 650);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(485, 649);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rich1
            // 
            this.rich1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rich1.HideSelection = false;
            this.rich1.Location = new System.Drawing.Point(20, 323);
            this.rich1.Name = "rich1";
            this.rich1.OleReadOnly = false;
            this.rich1.Size = new System.Drawing.Size(556, 230);
            this.rich1.TabIndex = 5;
            this.rich1.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbFouthSign);
            this.groupBox3.Controls.Add(this.dtFindDate);
            this.groupBox3.Controls.Add(this.txtbxUsecaseID);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.checkFouthSign);
            this.groupBox3.Controls.Add(this.cmbThirdSign);
            this.groupBox3.Controls.Add(this.checkThirdSign);
            this.groupBox3.Controls.Add(this.cmbSecondSign);
            this.groupBox3.Controls.Add(this.checkSecondSign);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cmbFirstSign);
            this.groupBox3.Controls.Add(this.checkFirstSign);
            this.groupBox3.Controls.Add(this.txtbxProblemID);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(20, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(554, 104);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "定制问题标识";
            // 
            // cmbFouthSign
            // 
            this.cmbFouthSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFouthSign.FormattingEnabled = true;
            this.cmbFouthSign.Location = new System.Drawing.Point(369, 44);
            this.cmbFouthSign.Name = "cmbFouthSign";
            this.cmbFouthSign.Size = new System.Drawing.Size(168, 20);
            this.cmbFouthSign.TabIndex = 7;
            this.cmbFouthSign.SelectedIndexChanged += new System.EventHandler(this.cmbFirstSign_SelectedIndexChanged);
            // 
            // dtFindDate
            // 
            this.dtFindDate.Location = new System.Drawing.Point(291, 97);
            this.dtFindDate.Name = "dtFindDate";
            this.dtFindDate.Size = new System.Drawing.Size(168, 21);
            this.dtFindDate.TabIndex = 3;
            this.dtFindDate.Visible = false;
            // 
            // txtbxUsecaseID
            // 
            this.txtbxUsecaseID.BackColor = System.Drawing.SystemColors.Control;
            this.txtbxUsecaseID.Location = new System.Drawing.Point(369, -3);
            this.txtbxUsecaseID.Name = "txtbxUsecaseID";
            this.txtbxUsecaseID.ReadOnly = true;
            this.txtbxUsecaseID.Size = new System.Drawing.Size(168, 21);
            this.txtbxUsecaseID.TabIndex = 7;
            this.txtbxUsecaseID.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(289, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "测试用例标识";
            this.label5.Visible = false;
            // 
            // checkFouthSign
            // 
            this.checkFouthSign.AutoSize = true;
            this.checkFouthSign.Location = new System.Drawing.Point(291, 46);
            this.checkFouthSign.Name = "checkFouthSign";
            this.checkFouthSign.Size = new System.Drawing.Size(72, 16);
            this.checkFouthSign.TabIndex = 6;
            this.checkFouthSign.Text = "四级标识";
            this.checkFouthSign.UseVisualStyleBackColor = true;
            this.checkFouthSign.CheckedChanged += new System.EventHandler(this.checkFirstSign_CheckedChanged);
            // 
            // cmbThirdSign
            // 
            this.cmbThirdSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThirdSign.FormattingEnabled = true;
            this.cmbThirdSign.Location = new System.Drawing.Point(369, 18);
            this.cmbThirdSign.Name = "cmbThirdSign";
            this.cmbThirdSign.Size = new System.Drawing.Size(168, 20);
            this.cmbThirdSign.TabIndex = 5;
            this.cmbThirdSign.SelectedIndexChanged += new System.EventHandler(this.cmbFirstSign_SelectedIndexChanged);
            // 
            // checkThirdSign
            // 
            this.checkThirdSign.AutoSize = true;
            this.checkThirdSign.Location = new System.Drawing.Point(291, 20);
            this.checkThirdSign.Name = "checkThirdSign";
            this.checkThirdSign.Size = new System.Drawing.Size(72, 16);
            this.checkThirdSign.TabIndex = 4;
            this.checkThirdSign.Text = "三级标识";
            this.checkThirdSign.UseVisualStyleBackColor = true;
            this.checkThirdSign.CheckedChanged += new System.EventHandler(this.checkFirstSign_CheckedChanged);
            // 
            // cmbSecondSign
            // 
            this.cmbSecondSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSecondSign.FormattingEnabled = true;
            this.cmbSecondSign.Location = new System.Drawing.Point(101, 44);
            this.cmbSecondSign.Name = "cmbSecondSign";
            this.cmbSecondSign.Size = new System.Drawing.Size(168, 20);
            this.cmbSecondSign.TabIndex = 3;
            this.cmbSecondSign.SelectedIndexChanged += new System.EventHandler(this.cmbFirstSign_SelectedIndexChanged);
            // 
            // checkSecondSign
            // 
            this.checkSecondSign.AutoSize = true;
            this.checkSecondSign.Location = new System.Drawing.Point(21, 46);
            this.checkSecondSign.Name = "checkSecondSign";
            this.checkSecondSign.Size = new System.Drawing.Size(72, 16);
            this.checkSecondSign.TabIndex = 2;
            this.checkSecondSign.Text = "二级标识";
            this.checkSecondSign.UseVisualStyleBackColor = true;
            this.checkSecondSign.CheckedChanged += new System.EventHandler(this.checkFirstSign_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(232, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "发现日期";
            this.label3.Visible = false;
            // 
            // cmbFirstSign
            // 
            this.cmbFirstSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFirstSign.FormattingEnabled = true;
            this.cmbFirstSign.Location = new System.Drawing.Point(101, 18);
            this.cmbFirstSign.Name = "cmbFirstSign";
            this.cmbFirstSign.Size = new System.Drawing.Size(168, 20);
            this.cmbFirstSign.TabIndex = 1;
            this.cmbFirstSign.SelectedIndexChanged += new System.EventHandler(this.cmbFirstSign_SelectedIndexChanged);
            // 
            // checkFirstSign
            // 
            this.checkFirstSign.AutoSize = true;
            this.checkFirstSign.Location = new System.Drawing.Point(21, 20);
            this.checkFirstSign.Name = "checkFirstSign";
            this.checkFirstSign.Size = new System.Drawing.Size(72, 16);
            this.checkFirstSign.TabIndex = 0;
            this.checkFirstSign.Text = "一级标识";
            this.checkFirstSign.UseVisualStyleBackColor = true;
            this.checkFirstSign.CheckedChanged += new System.EventHandler(this.checkFirstSign_CheckedChanged);
            // 
            // txtbxProblemID
            // 
            this.txtbxProblemID.Location = new System.Drawing.Point(101, 70);
            this.txtbxProblemID.Name = "txtbxProblemID";
            this.txtbxProblemID.ReadOnly = true;
            this.txtbxProblemID.Size = new System.Drawing.Size(436, 21);
            this.txtbxProblemID.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "问题标识";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtbxPblName);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.dtReportDate);
            this.groupBox4.Controls.Add(this.txtbxUsecaseName);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtbxReporter);
            this.groupBox4.Controls.Add(this.lblReporter);
            this.groupBox4.Location = new System.Drawing.Point(20, 122);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(554, 78);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "基本信息";
            // 
            // txtbxPblName
            // 
            this.txtbxPblName.Location = new System.Drawing.Point(99, 47);
            this.txtbxPblName.Name = "txtbxPblName";
            this.txtbxPblName.Size = new System.Drawing.Size(168, 21);
            this.txtbxPblName.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "问题名称";
            // 
            // dtReportDate
            // 
            this.dtReportDate.Location = new System.Drawing.Point(369, 20);
            this.dtReportDate.Name = "dtReportDate";
            this.dtReportDate.Size = new System.Drawing.Size(168, 21);
            this.dtReportDate.TabIndex = 5;
            // 
            // txtbxUsecaseName
            // 
            this.txtbxUsecaseName.Location = new System.Drawing.Point(369, 47);
            this.txtbxUsecaseName.Name = "txtbxUsecaseName";
            this.txtbxUsecaseName.ReadOnly = true;
            this.txtbxUsecaseName.Size = new System.Drawing.Size(168, 21);
            this.txtbxUsecaseName.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(289, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "测试用例名称";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(289, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "报告日期";
            // 
            // txtbxReporter
            // 
            this.txtbxReporter.BackColor = System.Drawing.Color.White;
            this.txtbxReporter.DataSource = null;
            this.txtbxReporter.Location = new System.Drawing.Point(99, 20);
            this.txtbxReporter.Multiline = true;
            this.txtbxReporter.Name = "txtbxReporter";
            this.txtbxReporter.ReadOnly = true;
            this.txtbxReporter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbxReporter.Size = new System.Drawing.Size(168, 21);
            this.txtbxReporter.TabIndex = 1;
            this.txtbxReporter.TextChanged += new System.EventHandler(this.CheckTextChanged);
            // 
            // lblReporter
            // 
            this.lblReporter.AutoSize = true;
            this.lblReporter.Location = new System.Drawing.Point(19, 24);
            this.lblReporter.Name = "lblReporter";
            this.lblReporter.Size = new System.Drawing.Size(47, 12);
            this.lblReporter.TabIndex = 0;
            this.lblReporter.Text = "报告人*";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ProblemRepForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(0, 695);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(601, 695);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.rich1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbMemo);
            this.Controls.Add(this.gbPblLel);
            this.Controls.Add(this.gbPblCat);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(6, 568);
            this.Name = "ProblemRepForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "问题报告编辑窗口";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProblemRepForm_FormClosing);
            this.Load += new System.EventHandler(this.ProblemRepForm_Load);
            this.gbPblCat.ResumeLayout(false);
            this.gbPblCat.PerformLayout();
            this.gbPblLel.ResumeLayout(false);
            this.gbPblLel.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox gbPblCat;
        private System.Windows.Forms.RadioButton radioPC4;
        private System.Windows.Forms.RadioButton radioPC3;
        private System.Windows.Forms.RadioButton radioPC2;
        private System.Windows.Forms.RadioButton radioPC1;
        private System.Windows.Forms.GroupBox gbPblLel;
        private System.Windows.Forms.RadioButton radioPL4;
        private System.Windows.Forms.RadioButton radioPL3;
        private System.Windows.Forms.RadioButton radioPL2;
        private System.Windows.Forms.RadioButton radioPL1;
        private System.Windows.Forms.TextBox tbMemo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private Common.RichTextBox.RichTextBoxOle rich1;
        private System.Windows.Forms.RadioButton radioPC5;
        private System.Windows.Forms.RadioButton radioPL5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkFirstSign;
        private System.Windows.Forms.TextBox txtbxProblemID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSecondSign;
        private System.Windows.Forms.CheckBox checkSecondSign;
        private System.Windows.Forms.ComboBox cmbFirstSign;
        private System.Windows.Forms.ComboBox cmbFouthSign;
        private System.Windows.Forms.CheckBox checkFouthSign;
        private System.Windows.Forms.ComboBox cmbThirdSign;
        private System.Windows.Forms.CheckBox checkThirdSign;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtReportDate;
        private System.Windows.Forms.DateTimePicker dtFindDate;
        private System.Windows.Forms.TextBox txtbxUsecaseName;
        private System.Windows.Forms.TextBox txtbxUsecaseID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Common.DropDownTextBox txtbxReporter;
        private System.Windows.Forms.Label lblReporter;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtbxPblName;
        private System.Windows.Forms.Label label2;
    }
}