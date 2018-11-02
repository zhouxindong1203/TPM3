namespace TPM3.Sys
{
    partial class ImageForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ilYesNo = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "project");
            this.imageList1.Images.SetKeyName(1, "obj");
            this.imageList1.Images.SetKeyName(2, "type");
            this.imageList1.Images.SetKeyName(3, "item");
            this.imageList1.Images.SetKeyName(4, "case");
            this.imageList1.Images.SetKeyName(5, "case_k");
            this.imageList1.Images.SetKeyName(6, "unexec");
            this.imageList1.Images.SetKeyName(7, "unexec_k");
            this.imageList1.Images.SetKeyName(8, "partexec");
            this.imageList1.Images.SetKeyName(9, "partexec_k");
            this.imageList1.Images.SetKeyName(10, "partexecp");
            this.imageList1.Images.SetKeyName(11, "partexecp_k");
            this.imageList1.Images.SetKeyName(12, "execp");
            this.imageList1.Images.SetKeyName(13, "execp_k");
            this.imageList1.Images.SetKeyName(14, "arrow");
            // 
            // ilYesNo
            // 
            this.ilYesNo.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilYesNo.ImageStream")));
            this.ilYesNo.TransparentColor = System.Drawing.Color.Transparent;
            this.ilYesNo.Images.SetKeyName(0, "x1.ICO");
            this.ilYesNo.Images.SetKeyName(1, "x2.ICO");
            this.ilYesNo.Images.SetKeyName(2, "x3.ICO");
            this.ilYesNo.Images.SetKeyName(3, "x5.ICO");
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 293);
            this.Name = "ImageForm";
            this.Text = "ImageForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.ImageList ilYesNo;
    }
}