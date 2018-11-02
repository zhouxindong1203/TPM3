namespace TPM3.wx
{
    partial class TestObjectInfoControl
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
            this.label5 = new System.Windows.Forms.Label();
            this.rich1 = new Common.RichTextBox.RichTextBoxOle();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(515, 30);
            this.label5.TabIndex = 2;
            this.label5.Text = "测试执行情况补充说明";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rich1
            // 
            this.rich1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rich1.Font = new System.Drawing.Font("宋体", 12F);
            this.rich1.Location = new System.Drawing.Point(0, 30);
            this.rich1.Name = "rich1";
            this.rich1.OleReadOnly = false;
            this.rich1.Size = new System.Drawing.Size(515, 414);
            this.rich1.TabIndex = 3;
            this.rich1.Text = "";
            // 
            // TestObjectInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rich1);
            this.Controls.Add(this.label5);
            this.Name = "TestObjectInfoControl";
            this.Size = new System.Drawing.Size(515, 444);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private Common.RichTextBox.RichTextBoxOle rich1;
    }
}
