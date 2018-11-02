namespace TPM3.wx
{
    partial class RequireItemInfoForm
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
            if(disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RequireItemInfoForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "条款名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "文档标识";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(118, 114);
            this.textBox1.MaxLength = 200;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(209, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RequireItemInfoForm_KeyDown);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(118, 66);
            this.textBox2.MaxLength = 50;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(209, 21);
            this.textBox2.TabIndex = 2;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RequireItemInfoForm_KeyDown);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.AutomaticDelay = 100;
            this.C1SuperTooltip1.AutoPopDelay = 50000;
            this.C1SuperTooltip1.BackColor = System.Drawing.Color.Transparent;
            this.C1SuperTooltip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("C1SuperTooltip1.BackgroundImage")));
            this.C1SuperTooltip1.BorderColor = System.Drawing.SystemColors.HotTrack;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.C1SuperTooltip1.IsBalloon = true;
            this.C1SuperTooltip1.RoundedCorners = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(58, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(220, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "测试条款属性(按ESC取消编辑) ";
            // 
            // RequireItemInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(366, 169);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "RequireItemInfoForm";
            this.Text = "RequireItemInfoForm";
            this.Deactivate += new System.EventHandler(this.RequireItemInfoForm_Deactivate);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RequireItemInfoForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RequireItemInfoForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.Label label3;
    }
}