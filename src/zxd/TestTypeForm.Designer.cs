namespace TPM3.zxd
{
    partial class TestTypeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestTypeForm));
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rich1 = new Common.RichTextBox.RichTextBoxOle();
            this.label5 = new System.Windows.Forms.Label();
            this.txtbxWorkTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtbxAbbr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbxTypeName = new System.Windows.Forms.TextBox();
            this.cmbSubNodeType = new System.Windows.Forms.ComboBox();
            this.splitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.grid2 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.grid1 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.expandablePanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.Color.LightSteelBlue;
            this.expandablePanel1.ColorScheme.ItemDesignTimeBorder = System.Drawing.Color.Black;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.expandablePanel1.Controls.Add(this.tableLayoutPanel1);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandablePanel1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expandablePanel1.Location = new System.Drawing.Point(0, 0);
            this.expandablePanel1.Margin = new System.Windows.Forms.Padding(0);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(693, 168);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.Color = System.Drawing.Color.LightSteelBlue;
            this.expandablePanel1.Style.BackColor2.Color = System.Drawing.Color.LightSteelBlue;
            this.expandablePanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 0;
            this.expandablePanel1.TitleHeight = 32;
            this.expandablePanel1.TitleStyle.BackColor1.Color = System.Drawing.Color.LightSteelBlue;
            this.expandablePanel1.TitleStyle.BackColor2.Color = System.Drawing.Color.LightSteelBlue;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.Color = System.Drawing.Color.LightSteelBlue;
            this.expandablePanel1.TitleStyle.BorderWidth = 0;
            this.expandablePanel1.TitleStyle.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "测试类型";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.rich1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtbxWorkTime, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtbxAbbr, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtbxTypeName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbSubNodeType, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(693, 136);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // rich1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.rich1, 3);
            this.rich1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rich1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rich1.HideSelection = false;
            this.rich1.Location = new System.Drawing.Point(117, 60);
            this.rich1.Margin = new System.Windows.Forms.Padding(0);
            this.rich1.Name = "rich1";
            this.rich1.OleReadOnly = false;
            this.rich1.Size = new System.Drawing.Size(576, 76);
            this.rich1.TabIndex = 11;
            this.rich1.Text = "";
            this.rich1.TextChanged += new System.EventHandler(this.txtbxTypeName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(0, 60);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 76);
            this.label5.TabIndex = 10;
            this.label5.Text = "测试类型       总体要求";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbxWorkTime
            // 
            this.txtbxWorkTime.BackColor = System.Drawing.Color.Gainsboro;
            this.txtbxWorkTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxWorkTime.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtbxWorkTime.Location = new System.Drawing.Point(463, 30);
            this.txtbxWorkTime.Margin = new System.Windows.Forms.Padding(0);
            this.txtbxWorkTime.Multiline = true;
            this.txtbxWorkTime.Name = "txtbxWorkTime";
            this.txtbxWorkTime.ReadOnly = true;
            this.txtbxWorkTime.Size = new System.Drawing.Size(230, 30);
            this.txtbxWorkTime.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(346, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 30);
            this.label4.TabIndex = 7;
            this.label4.Text = "预计工作时间";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbxAbbr
            // 
            this.txtbxAbbr.BackColor = System.Drawing.Color.White;
            this.txtbxAbbr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxAbbr.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtbxAbbr.Location = new System.Drawing.Point(117, 30);
            this.txtbxAbbr.Margin = new System.Windows.Forms.Padding(0);
            this.txtbxAbbr.Multiline = true;
            this.txtbxAbbr.Name = "txtbxAbbr";
            this.txtbxAbbr.Size = new System.Drawing.Size(229, 30);
            this.txtbxAbbr.TabIndex = 6;
            this.txtbxAbbr.TextChanged += new System.EventHandler(this.txtbxTypeName_TextChanged);
            this.txtbxAbbr.Validating += new System.ComponentModel.CancelEventHandler(this.txtbxAbbr_Validating);
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
            this.label3.Size = new System.Drawing.Size(117, 30);
            this.label3.TabIndex = 5;
            this.label3.Text = "简写码";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.label1.Size = new System.Drawing.Size(117, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "子节点类型";
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
            this.label2.Size = new System.Drawing.Size(117, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "测试类型名称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbxTypeName
            // 
            this.txtbxTypeName.BackColor = System.Drawing.Color.White;
            this.txtbxTypeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxTypeName.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtbxTypeName.Location = new System.Drawing.Point(117, 0);
            this.txtbxTypeName.Margin = new System.Windows.Forms.Padding(0);
            this.txtbxTypeName.Multiline = true;
            this.txtbxTypeName.Name = "txtbxTypeName";
            this.txtbxTypeName.Size = new System.Drawing.Size(229, 30);
            this.txtbxTypeName.TabIndex = 2;
            this.txtbxTypeName.TextChanged += new System.EventHandler(this.txtbxTypeName_TextChanged);
            this.txtbxTypeName.Validating += new System.ComponentModel.CancelEventHandler(this.txtbxTypeName_Validating);
            // 
            // cmbSubNodeType
            // 
            this.cmbSubNodeType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSubNodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubNodeType.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSubNodeType.FormattingEnabled = true;
            this.cmbSubNodeType.Location = new System.Drawing.Point(466, 3);
            this.cmbSubNodeType.Name = "cmbSubNodeType";
            this.cmbSubNodeType.Size = new System.Drawing.Size(224, 22);
            this.cmbSubNodeType.TabIndex = 8;
            this.cmbSubNodeType.SelectedIndexChanged += new System.EventHandler(this.txtbxTypeName_TextChanged);
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
            this.splitter1.Location = new System.Drawing.Point(0, 168);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(693, 3);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // grid2
            // 
            this.grid2.AllowAddNew = true;
            this.grid2.AllowDelete = true;
            this.grid2.Caption = "测试项集";
            this.grid2.CaptionHeight = 18;
            this.grid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid2.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grid2.GroupByCaption = "Drag a column header here to group by that column";
            this.grid2.Images.Add(((System.Drawing.Image)(resources.GetObject("grid2.Images"))));
            this.grid2.Location = new System.Drawing.Point(0, 171);
            this.grid2.Name = "grid2";
            this.grid2.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.grid2.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.grid2.PreviewInfo.ZoomFactor = 75D;
            this.grid2.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("grid2.PrintInfo.PageSettings")));
            this.grid2.RowHeight = 16;
            this.grid2.Size = new System.Drawing.Size(693, 364);
            this.grid2.TabIndex = 2;
            this.grid2.Text = "grid2";
            this.grid2.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.grid2_AfterColUpdate);
            this.grid2.AfterDelete += new System.EventHandler(this.grid2_AfterDelete);
            this.grid2.AfterInsert += new System.EventHandler(this.grid2_AfterInsert);
            this.grid2.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.grid2_BeforeDelete);
            this.grid2.ButtonClick += new C1.Win.C1TrueDBGrid.ColEventHandler(this.grid2_ButtonClick);
            this.grid2.OnAddNew += new System.EventHandler(this.grid2_OnAddNew);
            this.grid2.PropBag = resources.GetString("grid2.PropBag");
            // 
            // grid1
            // 
            this.grid1.AllowAddNew = true;
            this.grid1.AllowDelete = true;
            this.grid1.Caption = "测试子类型集";
            this.grid1.CaptionHeight = 18;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grid1.GroupByCaption = "Drag a column header here to group by that column";
            this.grid1.Images.Add(((System.Drawing.Image)(resources.GetObject("grid1.Images"))));
            this.grid1.Location = new System.Drawing.Point(0, 168);
            this.grid1.Margin = new System.Windows.Forms.Padding(0);
            this.grid1.Name = "grid1";
            this.grid1.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.grid1.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.grid1.PreviewInfo.ZoomFactor = 75D;
            this.grid1.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("grid1.PrintInfo.PageSettings")));
            this.grid1.RowHeight = 16;
            this.grid1.Size = new System.Drawing.Size(693, 367);
            this.grid1.TabIndex = 1;
            this.grid1.Text = "c1TrueDBGrid1";
            this.grid1.AfterColUpdate += new C1.Win.C1TrueDBGrid.ColEventHandler(this.grid1_AfterColUpdate);
            this.grid1.AfterDelete += new System.EventHandler(this.grid1_AfterDelete);
            this.grid1.AfterInsert += new System.EventHandler(this.grid1_AfterInsert);
            this.grid1.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.grid1_BeforeDelete);
            this.grid1.ComboSelect += new C1.Win.C1TrueDBGrid.ColEventHandler(this.grid1_ComboSelect);
            this.grid1.OnAddNew += new System.EventHandler(this.grid1_OnAddNew);
            this.grid1.PropBag = resources.GetString("grid1.PropBag");
            // 
            // TestTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 535);
            this.Controls.Add(this.grid2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.grid1);
            this.Controls.Add(this.expandablePanel1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "TestTypeForm";
            this.Text = "TestTypeForm";
            this.Load += new System.EventHandler(this.TestTypeForm_Load);
            this.expandablePanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbxTypeName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtbxAbbr;
        private System.Windows.Forms.ComboBox cmbSubNodeType;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid2;
        private Common.RichTextBox.RichTextBoxOle rich1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtbxWorkTime;
        private DevComponents.DotNetBar.ExpandableSplitter splitter1;
    }
}