namespace TPM3.zxd.util
{
    partial class UsecaseSelView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsecaseSelView));
            this.grid1 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.grid2 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnUnselAll = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbWrapText = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid1.BackColor = System.Drawing.Color.Gray;
            this.grid1.Caption = "测试用例实体";
            this.grid1.CaptionHeight = 28;
            this.grid1.Cursor = System.Windows.Forms.Cursors.Default;
            this.grid1.EditDropDown = false;
            this.grid1.GroupByCaption = "Drag a column header here to group by that column";
            this.grid1.Images.Add(((System.Drawing.Image)(resources.GetObject("grid1.Images"))));
            this.grid1.Location = new System.Drawing.Point(0, 0);
            this.grid1.Name = "grid1";
            this.grid1.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.grid1.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.grid1.PreviewInfo.ZoomFactor = 75;
            this.grid1.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("grid1.PrintInfo.PageSettings")));
            this.grid1.Size = new System.Drawing.Size(869, 620);
            this.grid1.TabIndex = 0;
            this.grid1.Text = "c1TrueDBGrid1";
            this.grid1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grid1_KeyPress);
            this.grid1.PropBag = resources.GetString("grid1.PropBag");
            // 
            // grid2
            // 
            this.grid2.BackColor = System.Drawing.Color.Gray;
            this.grid2.Cursor = System.Windows.Forms.Cursors.Default;
            this.grid2.GroupByCaption = "Drag a column header here to group by that column";
            this.grid2.Images.Add(((System.Drawing.Image)(resources.GetObject("grid2.Images"))));
            this.grid2.Location = new System.Drawing.Point(66, 198);
            this.grid2.Name = "grid2";
            this.grid2.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.grid2.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.grid2.PreviewInfo.ZoomFactor = 75;
            this.grid2.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("grid2.PrintInfo.PageSettings")));
            this.grid2.Size = new System.Drawing.Size(791, 385);
            this.grid2.TabIndex = 1;
            this.grid2.TabStop = false;
            this.grid2.Text = "c1TrueDBGrid1";
            this.grid2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grid1_KeyPress);
            this.grid2.VisibleChanged += new System.EventHandler(this.grid2_VisibleChanged);
            this.grid2.PropBag = resources.GetString("grid2.PropBag");
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectAll.Location = new System.Drawing.Point(12, 637);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            // 
            // btnUnselAll
            // 
            this.btnUnselAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUnselAll.Location = new System.Drawing.Point(93, 637);
            this.btnUnselAll.Name = "btnUnselAll";
            this.btnUnselAll.Size = new System.Drawing.Size(75, 23);
            this.btnUnselAll.TabIndex = 3;
            this.btnUnselAll.Text = "取消全选";
            this.btnUnselAll.UseVisualStyleBackColor = true;
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Location = new System.Drawing.Point(611, 637);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 4;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Location = new System.Drawing.Point(692, 637);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "导入";
            this.btnImport.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(773, 637);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // cbWrapText
            // 
            this.cbWrapText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbWrapText.AutoSize = true;
            this.cbWrapText.Checked = true;
            this.cbWrapText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWrapText.Location = new System.Drawing.Point(786, 7);
            this.cbWrapText.Name = "cbWrapText";
            this.cbWrapText.Size = new System.Drawing.Size(72, 16);
            this.cbWrapText.TabIndex = 8;
            this.cbWrapText.Text = "自动换行";
            this.cbWrapText.UseVisualStyleBackColor = true;
            this.cbWrapText.CheckedChanged += new System.EventHandler(this.cbWrapText_CheckedChanged);
            // 
            // UsecaseSelView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 672);
            this.Controls.Add(this.cbWrapText);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnUnselAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.grid2);
            this.Controls.Add(this.grid1);
            this.Name = "UsecaseSelView";
            this.Text = "测试用例实体选用";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.UsecaseSelView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid1;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid2;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnselAll;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox cbWrapText;
    }
}