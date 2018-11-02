namespace TPM3.lt
{
    partial class ProblemResultDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProblemResultDlg));
            this.flex1 = new Common.CustomMergeFlex();
            this.确定 = new System.Windows.Forms.Button();
            this.取消 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).BeginInit();
            this.SuspendLayout();
            // 
            // flex1
            // 
            this.flex1.AllowAddNew = false;
            this.flex1.AllowDelete = false;
            this.flex1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Columns;
            this.flex1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            this.flex1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.flex1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.flex1.AutoClipboard = true;
            this.flex1.BackColor = System.Drawing.Color.White;
            this.flex1.ColumnInfo = resources.GetString("flex1.ColumnInfo");
            this.flex1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flex1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flex1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.flex1.Location = new System.Drawing.Point(0, 0);
            this.flex1.Margin = new System.Windows.Forms.Padding(0);
            this.flex1.Name = "flex1";
            this.flex1.Rows.Count = 6;
            this.flex1.Rows.DefaultSize = 20;
            this.flex1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flex1.ShowSort = false;
            this.flex1.Size = new System.Drawing.Size(763, 281);
            this.flex1.StyleInfo = resources.GetString("flex1.StyleInfo");
            this.flex1.TabIndex = 15;
            this.flex1.Text = "c1FlexGrid1";
            // 
            // 确定
            // 
            this.确定.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.确定.Location = new System.Drawing.Point(551, 284);
            this.确定.Name = "确定";
            this.确定.Size = new System.Drawing.Size(75, 23);
            this.确定.TabIndex = 16;
            this.确定.Text = "确定";
            this.确定.UseVisualStyleBackColor = true;
            // 
            // 取消
            // 
            this.取消.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.取消.Location = new System.Drawing.Point(665, 284);
            this.取消.Name = "取消";
            this.取消.Size = new System.Drawing.Size(75, 23);
            this.取消.TabIndex = 17;
            this.取消.Text = "取消";
            this.取消.UseVisualStyleBackColor = true;
            // 
            // ProblemResultDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 308);
            this.Controls.Add(this.取消);
            this.Controls.Add(this.确定);
            this.Controls.Add(this.flex1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ProblemResultDlg";
            this.Text = "问题报告单选择";
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected Common.CustomMergeFlex flex1;
        private System.Windows.Forms.Button 确定;
        private System.Windows.Forms.Button 取消;
    }
}