namespace TPM3.zxd
{
    partial class TestUsecaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestUsecaseForm));
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ddttPerson = new Common.DropDownTextBox();
            this.lblPerson = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtbxTrace = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtbxPassCert = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtbxTerm = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtbxInit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtbxDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtbxSign = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbxUCName = new System.Windows.Forms.TextBox();
            this.ddttMethod = new Common.DropDownTextBox();
            this.txtbxConstraint = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ddttTester = new Common.DropDownTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtbxUnexecReason = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtbxExecStatus = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpTestDate = new System.Windows.Forms.DateTimePicker();
            this.cmbExecResult = new System.Windows.Forms.ComboBox();
            this.grid2 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCopyStep = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPasteStep = new System.Windows.Forms.ToolStripMenuItem();
            this.radarStatusEditorDDControl1 = new DDEditor.AccEditorDDControl();
            this.splitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.expandablePanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radarStatusEditorDDControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.Color.LightSteelBlue;
            this.expandablePanel1.ColorScheme.ItemDesignTimeBorder = System.Drawing.Color.Black;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expandablePanel1.Controls.Add(this.tableLayoutPanel1);
            this.expandablePanel1.Controls.Add(this.tableLayoutPanel2);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandablePanel1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expandablePanel1.Location = new System.Drawing.Point(0, 0);
            this.expandablePanel1.Margin = new System.Windows.Forms.Padding(0);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(693, 509);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.Color = System.Drawing.Color.LightSteelBlue;
            this.expandablePanel1.Style.BackColor2.Color = System.Drawing.Color.LightSteelBlue;
            this.expandablePanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 0;
            this.expandablePanel1.TitleHeight = 30;
            this.expandablePanel1.TitleStyle.BackColor1.Color = System.Drawing.Color.LightSteelBlue;
            this.expandablePanel1.TitleStyle.BackColor2.Color = System.Drawing.Color.LightSteelBlue;
            this.expandablePanel1.TitleStyle.BorderColor.Color = System.Drawing.Color.LightSteelBlue;
            this.expandablePanel1.TitleStyle.BorderWidth = 0;
            this.expandablePanel1.TitleStyle.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "测试用例 []";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.ddttPerson, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblPerson, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtbxTrace, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtbxPassCert, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtbxTerm, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtbxInit, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtbxDesc, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtbxSign, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtbxUCName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ddttMethod, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtbxConstraint, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(693, 375);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // ddttPerson
            // 
            this.ddttPerson.BackColor = System.Drawing.Color.White;
            this.ddttPerson.DataSource = null;
            this.ddttPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddttPerson.Location = new System.Drawing.Point(446, 345);
            this.ddttPerson.Margin = new System.Windows.Forms.Padding(0);
            this.ddttPerson.Multiline = true;
            this.ddttPerson.Name = "ddttPerson";
            this.ddttPerson.ReadOnly = true;
            this.ddttPerson.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.ddttPerson.Size = new System.Drawing.Size(247, 30);
            this.ddttPerson.TabIndex = 22;
            this.ddttPerson.TextChanged += new System.EventHandler(this.ddttPerson_TextChanged);
            // 
            // lblPerson
            // 
            this.lblPerson.AutoSize = true;
            this.lblPerson.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPerson.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPerson.Location = new System.Drawing.Point(346, 345);
            this.lblPerson.Margin = new System.Windows.Forms.Padding(0);
            this.lblPerson.Name = "lblPerson";
            this.lblPerson.Size = new System.Drawing.Size(100, 30);
            this.lblPerson.TabIndex = 21;
            this.lblPerson.Text = "设计人员";
            this.lblPerson.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(0, 345);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 30);
            this.label9.TabIndex = 19;
            this.label9.Text = "设计方法";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbxTrace
            // 
            this.txtbxTrace.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.SetColumnSpan(this.txtbxTrace, 3);
            this.txtbxTrace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxTrace.Location = new System.Drawing.Point(100, 295);
            this.txtbxTrace.Margin = new System.Windows.Forms.Padding(0);
            this.txtbxTrace.Multiline = true;
            this.txtbxTrace.Name = "txtbxTrace";
            this.txtbxTrace.ReadOnly = true;
            this.txtbxTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbxTrace.Size = new System.Drawing.Size(593, 50);
            this.txtbxTrace.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(0, 295);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 50);
            this.label8.TabIndex = 17;
            this.label8.Text = "追踪关系";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbxPassCert
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtbxPassCert, 3);
            this.txtbxPassCert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxPassCert.Location = new System.Drawing.Point(100, 242);
            this.txtbxPassCert.Margin = new System.Windows.Forms.Padding(0);
            this.txtbxPassCert.Multiline = true;
            this.txtbxPassCert.Name = "txtbxPassCert";
            this.txtbxPassCert.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbxPassCert.Size = new System.Drawing.Size(593, 53);
            this.txtbxPassCert.TabIndex = 16;
            this.txtbxPassCert.TextChanged += new System.EventHandler(this.txtbxUCName_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(0, 242);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 53);
            this.label7.TabIndex = 15;
            this.label7.Text = "用例通过准则";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbxTerm
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtbxTerm, 3);
            this.txtbxTerm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxTerm.Location = new System.Drawing.Point(100, 189);
            this.txtbxTerm.Margin = new System.Windows.Forms.Padding(0);
            this.txtbxTerm.Multiline = true;
            this.txtbxTerm.Name = "txtbxTerm";
            this.txtbxTerm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbxTerm.Size = new System.Drawing.Size(593, 53);
            this.txtbxTerm.TabIndex = 14;
            this.txtbxTerm.TextChanged += new System.EventHandler(this.txtbxUCName_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(0, 189);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 53);
            this.label6.TabIndex = 13;
            this.label6.Text = "用例终止条件";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(0, 136);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 53);
            this.label5.TabIndex = 11;
            this.label5.Text = "前提和约束";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbxInit
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtbxInit, 3);
            this.txtbxInit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxInit.Location = new System.Drawing.Point(100, 83);
            this.txtbxInit.Margin = new System.Windows.Forms.Padding(0);
            this.txtbxInit.Multiline = true;
            this.txtbxInit.Name = "txtbxInit";
            this.txtbxInit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbxInit.Size = new System.Drawing.Size(593, 53);
            this.txtbxInit.TabIndex = 10;
            this.txtbxInit.TextChanged += new System.EventHandler(this.txtbxUCName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(0, 83);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 53);
            this.label4.TabIndex = 9;
            this.label4.Text = "用例初始化";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbxDesc
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtbxDesc, 3);
            this.txtbxDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxDesc.Location = new System.Drawing.Point(100, 30);
            this.txtbxDesc.Margin = new System.Windows.Forms.Padding(0);
            this.txtbxDesc.Multiline = true;
            this.txtbxDesc.Name = "txtbxDesc";
            this.txtbxDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbxDesc.Size = new System.Drawing.Size(593, 53);
            this.txtbxDesc.TabIndex = 8;
            this.txtbxDesc.TextChanged += new System.EventHandler(this.txtbxUCName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(0, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 53);
            this.label3.TabIndex = 7;
            this.label3.Text = "测试用例综述";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbxSign
            // 
            this.txtbxSign.BackColor = System.Drawing.Color.White;
            this.txtbxSign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxSign.Location = new System.Drawing.Point(446, 0);
            this.txtbxSign.Margin = new System.Windows.Forms.Padding(0);
            this.txtbxSign.Multiline = true;
            this.txtbxSign.Name = "txtbxSign";
            this.txtbxSign.ReadOnly = true;
            this.txtbxSign.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtbxSign.Size = new System.Drawing.Size(247, 30);
            this.txtbxSign.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(346, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "测试用例标识";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "测试用例名称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbxUCName
            // 
            this.txtbxUCName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxUCName.Location = new System.Drawing.Point(100, 0);
            this.txtbxUCName.Margin = new System.Windows.Forms.Padding(0);
            this.txtbxUCName.Multiline = true;
            this.txtbxUCName.Name = "txtbxUCName";
            this.txtbxUCName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtbxUCName.Size = new System.Drawing.Size(246, 30);
            this.txtbxUCName.TabIndex = 4;
            this.txtbxUCName.TextChanged += new System.EventHandler(this.txtbxUCName_TextChanged);
            this.txtbxUCName.Validating += new System.ComponentModel.CancelEventHandler(this.txtbxUCName_Validating);
            // 
            // ddttMethod
            // 
            this.ddttMethod.BackColor = System.Drawing.Color.White;
            this.ddttMethod.DataSource = null;
            this.ddttMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddttMethod.Location = new System.Drawing.Point(100, 345);
            this.ddttMethod.Margin = new System.Windows.Forms.Padding(0);
            this.ddttMethod.Multiline = true;
            this.ddttMethod.Name = "ddttMethod";
            this.ddttMethod.ReadOnly = true;
            this.ddttMethod.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.ddttMethod.Size = new System.Drawing.Size(246, 30);
            this.ddttMethod.TabIndex = 20;
            this.ddttMethod.TextChanged += new System.EventHandler(this.txtbxUCName_TextChanged);
            // 
            // txtbxConstraint
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtbxConstraint, 3);
            this.txtbxConstraint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxConstraint.Location = new System.Drawing.Point(100, 136);
            this.txtbxConstraint.Margin = new System.Windows.Forms.Padding(0);
            this.txtbxConstraint.Multiline = true;
            this.txtbxConstraint.Name = "txtbxConstraint";
            this.txtbxConstraint.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbxConstraint.Size = new System.Drawing.Size(593, 53);
            this.txtbxConstraint.TabIndex = 12;
            this.txtbxConstraint.TextChanged += new System.EventHandler(this.txtbxUCName_TextChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.ddttTester, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label10, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtbxUnexecReason, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label15, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label14, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtbxExecStatus, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label13, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpTestDate, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbExecResult, 3, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 405);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(693, 104);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // ddttTester
            // 
            this.ddttTester.BackColor = System.Drawing.Color.White;
            this.ddttTester.DataSource = null;
            this.ddttTester.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ddttTester.Location = new System.Drawing.Point(446, 0);
            this.ddttTester.Margin = new System.Windows.Forms.Padding(0);
            this.ddttTester.Multiline = true;
            this.ddttTester.Name = "ddttTester";
            this.ddttTester.ReadOnly = true;
            this.ddttTester.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.ddttTester.Size = new System.Drawing.Size(247, 30);
            this.ddttTester.TabIndex = 31;
            this.ddttTester.TextChanged += new System.EventHandler(this.ddttTester_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(346, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 30);
            this.label10.TabIndex = 30;
            this.label10.Text = "测试人员";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbxUnexecReason
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.txtbxUnexecReason, 3);
            this.txtbxUnexecReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxUnexecReason.Location = new System.Drawing.Point(100, 60);
            this.txtbxUnexecReason.Margin = new System.Windows.Forms.Padding(0);
            this.txtbxUnexecReason.Multiline = true;
            this.txtbxUnexecReason.Name = "txtbxUnexecReason";
            this.txtbxUnexecReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbxUnexecReason.Size = new System.Drawing.Size(593, 45);
            this.txtbxUnexecReason.TabIndex = 29;
            this.txtbxUnexecReason.TextChanged += new System.EventHandler(this.txtbxUCName_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(0, 60);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 45);
            this.label15.TabIndex = 28;
            this.label15.Text = "未完整执行  原因";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(346, 30);
            this.label14.Margin = new System.Windows.Forms.Padding(0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 30);
            this.label14.TabIndex = 26;
            this.label14.Text = "执行结果";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbxExecStatus
            // 
            this.txtbxExecStatus.BackColor = System.Drawing.Color.White;
            this.txtbxExecStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxExecStatus.Location = new System.Drawing.Point(100, 30);
            this.txtbxExecStatus.Margin = new System.Windows.Forms.Padding(0);
            this.txtbxExecStatus.Multiline = true;
            this.txtbxExecStatus.Name = "txtbxExecStatus";
            this.txtbxExecStatus.ReadOnly = true;
            this.txtbxExecStatus.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtbxExecStatus.Size = new System.Drawing.Size(246, 30);
            this.txtbxExecStatus.TabIndex = 25;
            this.txtbxExecStatus.TextChanged += new System.EventHandler(this.txtbxUCName_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(0, 30);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 30);
            this.label13.TabIndex = 24;
            this.label13.Text = "执行状态";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 30);
            this.label11.TabIndex = 20;
            this.label11.Text = "测试时间";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpTestDate
            // 
            this.dtpTestDate.Checked = false;
            this.dtpTestDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpTestDate.Location = new System.Drawing.Point(100, 3);
            this.dtpTestDate.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.dtpTestDate.Name = "dtpTestDate";
            this.dtpTestDate.ShowCheckBox = true;
            this.dtpTestDate.Size = new System.Drawing.Size(246, 23);
            this.dtpTestDate.TabIndex = 21;
            this.dtpTestDate.Value = new System.DateTime(2008, 11, 25, 0, 0, 0, 0);
            this.dtpTestDate.ValueChanged += new System.EventHandler(this.txtbxUCName_TextChanged);
            // 
            // cmbExecResult
            // 
            this.cmbExecResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbExecResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExecResult.Enabled = false;
            this.cmbExecResult.FormattingEnabled = true;
            this.cmbExecResult.ItemHeight = 14;
            this.cmbExecResult.Location = new System.Drawing.Point(446, 33);
            this.cmbExecResult.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.cmbExecResult.Name = "cmbExecResult";
            this.cmbExecResult.Size = new System.Drawing.Size(247, 22);
            this.cmbExecResult.TabIndex = 27;
            this.cmbExecResult.SelectedIndexChanged += new System.EventHandler(this.txtbxUCName_TextChanged);
            // 
            // grid2
            // 
            this.grid2.AllowAddNew = true;
            this.grid2.AllowDelete = true;
            this.grid2.Caption = "测试步骤";
            this.grid2.CaptionHeight = 18;
            this.grid2.ContextMenuStrip = this.contextMenuStrip1;
            this.grid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid2.GroupByCaption = "Drag a column header here to group by that column";
            this.grid2.Images.Add(((System.Drawing.Image)(resources.GetObject("grid2.Images"))));
            this.grid2.Location = new System.Drawing.Point(0, 512);
            this.grid2.Name = "grid2";
            this.grid2.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.grid2.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.grid2.PreviewInfo.ZoomFactor = 75D;
            this.grid2.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("grid2.PrintInfo.PageSettings")));
            this.grid2.RowHeight = 16;
            this.grid2.Size = new System.Drawing.Size(693, 172);
            this.grid2.TabIndex = 10;
            this.grid2.Text = "c1TrueDBGrid1";
            this.grid2.AfterDelete += new System.EventHandler(this.grid2_AfterDelete);
            this.grid2.AfterInsert += new System.EventHandler(this.grid2_AfterInsert);
            this.grid2.BeforeColUpdate += new C1.Win.C1TrueDBGrid.BeforeColUpdateEventHandler(this.grid2_BeforeColUpdate);
            this.grid2.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.grid2_BeforeDelete);
            this.grid2.AfterColEdit += new C1.Win.C1TrueDBGrid.ColEventHandler(this.grid2_AfterColEdit);
            this.grid2.BeforeColEdit += new C1.Win.C1TrueDBGrid.BeforeColEditEventHandler(this.grid2_BeforeColEdit);
            this.grid2.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.grid2_ButtonClick);
            this.grid2.FetchCellStyle += new C1.Win.C1TrueDBGrid.FetchCellStyleEventHandler(this.grid2_FetchCellStyle);
            this.grid2.FetchRowStyle += new C1.Win.C1TrueDBGrid.FetchRowStyleEventHandler(this.grid2_FetchRowStyle);
            this.grid2.OnAddNew += new System.EventHandler(this.grid2_OnAddNew);
            this.grid2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grid2_MouseDown);
            this.grid2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.grid2_MouseMove);
            this.grid2.Resize += new System.EventHandler(this.grid2_Resize);
            this.grid2.PropBag = resources.GetString("grid2.PropBag");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyStep,
            this.tsmiPasteStep});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsmiCopyStep
            // 
            this.tsmiCopyStep.Name = "tsmiCopyStep";
            this.tsmiCopyStep.Size = new System.Drawing.Size(148, 22);
            this.tsmiCopyStep.Text = "复制测试步骤";
            this.tsmiCopyStep.Click += new System.EventHandler(this.tsmiCopyStep_Click);
            // 
            // tsmiPasteStep
            // 
            this.tsmiPasteStep.Name = "tsmiPasteStep";
            this.tsmiPasteStep.Size = new System.Drawing.Size(148, 22);
            this.tsmiPasteStep.Text = "粘贴测试步骤";
            this.tsmiPasteStep.Click += new System.EventHandler(this.tsmiPasteStep_Click);
            // 
            // radarStatusEditorDDControl1
            // 
            this.radarStatusEditorDDControl1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.radarStatusEditorDDControl1.DropDownFormClassName = "DDEditor.AccEditor";
            this.radarStatusEditorDDControl1.Location = new System.Drawing.Point(100, 540);
            this.radarStatusEditorDDControl1.Name = "radarStatusEditorDDControl1";
            this.radarStatusEditorDDControl1.Size = new System.Drawing.Size(201, 18);
            this.radarStatusEditorDDControl1.TabIndex = 12;
            this.radarStatusEditorDDControl1.Tag = null;
            this.radarStatusEditorDDControl1.Visible = false;
            this.radarStatusEditorDDControl1.VisibleButtons = C1.Win.C1Input.DropDownControlButtonFlags.DropDown;
            // 
            // splitter1
            // 
            this.splitter1.BackColor2 = System.Drawing.SystemColors.ControlDarkDark;
            this.splitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.ExpandableControl = this.expandablePanel1;
            this.splitter1.ExpandFillColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitter1.ExpandLineColor = System.Drawing.SystemColors.ControlText;
            this.splitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.splitter1.GripDarkColor = System.Drawing.SystemColors.ControlText;
            this.splitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.splitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.splitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.splitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(209)))), ((int)(((byte)(255)))));
            this.splitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.splitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.splitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.splitter1.HotExpandFillColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitter1.HotExpandLineColor = System.Drawing.SystemColors.ControlText;
            this.splitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.splitter1.HotGripDarkColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.splitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.splitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.splitter1.Location = new System.Drawing.Point(0, 509);
            this.splitter1.MinExtra = 0;
            this.splitter1.MinSize = 0;
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(693, 3);
            this.splitter1.TabIndex = 13;
            this.splitter1.TabStop = false;
            // 
            // TestUsecaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 684);
            this.Controls.Add(this.grid2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.radarStatusEditorDDControl1);
            this.Controls.Add(this.expandablePanel1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "TestUsecaseForm";
            this.Text = "测试用例查看[非修改模式]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestUsecaseForm_FormClosing);
            this.Load += new System.EventHandler(this.TestUsecaseForm_Load);
            this.expandablePanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radarStatusEditorDDControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbxDesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtbxSign;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbxUCName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtbxPassCert;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtbxTerm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtbxConstraint;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtbxInit;
        private System.Windows.Forms.Label label4;
        //private System.Windows.Forms.TextBox ddttPerson;
        private Common.DropDownTextBox ddttPerson;
        private System.Windows.Forms.Label lblPerson;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtbxTrace;
        //private System.Windows.Forms.TextBox ddttMethod;
        private Common.DropDownTextBox ddttMethod;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid2;
        private DDEditor.AccEditorDDControl radarStatusEditorDDControl1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyStep;
        private System.Windows.Forms.ToolStripMenuItem tsmiPasteStep;
        private DevComponents.DotNetBar.ExpandableSplitter splitter1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Common.DropDownTextBox ddttTester;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtbxUnexecReason;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtbxExecStatus;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpTestDate;
        private System.Windows.Forms.ComboBox cmbExecResult;
    }
}