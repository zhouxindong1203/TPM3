namespace TPM3.wx
{
    partial class TestPriorityForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestPriorityForm));
            this.grid1 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.AllowAddNew = true;
            this.grid1.AllowDelete = true;
            this.grid1.CaptionHeight = 18;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.GroupByCaption = "Drag a column header here to group by that column";
            this.grid1.Images.Add(((System.Drawing.Image)(resources.GetObject("grid1.Images"))));
            this.grid1.Location = new System.Drawing.Point(0, 0);
            this.grid1.Name = "grid1";
            this.grid1.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.grid1.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.grid1.PreviewInfo.ZoomFactor = 75;
            this.grid1.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("grid1.PrintInfo.PageSettings")));
            this.grid1.RowHeight = 16;
            this.grid1.Size = new System.Drawing.Size(341, 310);
            this.grid1.TabIndex = 5;
            this.grid1.Text = "c1TrueDBGrid1";
            this.grid1.PropBag = resources.GetString("grid1.PropBag");
            // 
            // TestPriorityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 310);
            this.Controls.Add(this.grid1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "TestPriorityForm";
            this.Text = "TestPriorityForm";
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid1;
    }
}