namespace TPM3.zxd.pbl
{
    partial class DlgSubmitProblem
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioUseExistProblem = new System.Windows.Forms.RadioButton();
            this.radioNewProblem = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(12, 94);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(96, 94);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioUseExistProblem);
            this.groupBox1.Controls.Add(this.radioNewProblem);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "问题提交方式";
            // 
            // radioUseExistProblem
            // 
            this.radioUseExistProblem.AutoSize = true;
            this.radioUseExistProblem.Location = new System.Drawing.Point(27, 42);
            this.radioUseExistProblem.Name = "radioUseExistProblem";
            this.radioUseExistProblem.Size = new System.Drawing.Size(119, 16);
            this.radioUseExistProblem.TabIndex = 2;
            this.radioUseExistProblem.TabStop = true;
            this.radioUseExistProblem.Text = "从已有问题中选取";
            this.radioUseExistProblem.UseVisualStyleBackColor = true;
            this.radioUseExistProblem.Click += new System.EventHandler(this.radioNewProblem_Click);
            // 
            // radioNewProblem
            // 
            this.radioNewProblem.AutoSize = true;
            this.radioNewProblem.Location = new System.Drawing.Point(27, 20);
            this.radioNewProblem.Name = "radioNewProblem";
            this.radioNewProblem.Size = new System.Drawing.Size(83, 16);
            this.radioNewProblem.TabIndex = 1;
            this.radioNewProblem.TabStop = true;
            this.radioNewProblem.Text = "提交新问题";
            this.radioNewProblem.UseVisualStyleBackColor = true;
            this.radioNewProblem.Click += new System.EventHandler(this.radioNewProblem_Click);
            // 
            // DlgSubmitProblem
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(183, 126);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgSubmitProblem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "提交问题";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioUseExistProblem;
        private System.Windows.Forms.RadioButton radioNewProblem;
    }
}