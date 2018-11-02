namespace TPM3.wx
{
    partial class TestProjectSummary
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.TestCaseSummary1 = new TestCaseSummary();
            this.FallMatrixTable1 = new FallMatrixTable();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.SuspendLayout();
            // 
            // testResultObjectControl51
            // 
            this.TestCaseSummary1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestCaseSummary1.Location = new System.Drawing.Point(0, 0);
            this.TestCaseSummary1.Name = "testResultObjectControl51";
            this.TestCaseSummary1.Size = new System.Drawing.Size(548, 282);
            this.TestCaseSummary1.TabIndex = 0;
            // 
            // testResultObjectControl41
            // 
            this.FallMatrixTable1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FallMatrixTable1.Location = new System.Drawing.Point(0, 285);
            this.FallMatrixTable1.Name = "testResultObjectControl41";
            this.FallMatrixTable1.Size = new System.Drawing.Size(548, 166);
            this.FallMatrixTable1.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 282);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(548, 3);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // TestProjectSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TestCaseSummary1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.FallMatrixTable1);
            this.Name = "TestProjectSummary";
            this.Size = new System.Drawing.Size(548, 451);
            this.ResumeLayout(false);

        }

        #endregion

        private TestCaseSummary TestCaseSummary1;
        private FallMatrixTable FallMatrixTable1;
        private System.Windows.Forms.Splitter splitter1;
    }
}
