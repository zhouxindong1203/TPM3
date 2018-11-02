namespace TPM3.lt
{
    partial class RegressionTestInfluenceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegressionTestInfluenceForm));
            this.label1 = new System.Windows.Forms.Label();
            this.flex1 = new Common.CustomMergeFlex();
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(230, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "回归测试影响域分析";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // flex1
            // 
            this.flex1.AllowAddNew = true;
            this.flex1.AllowDelete = true;
            this.flex1.AllowFreezing = C1.Win.C1FlexGrid.AllowFreezingEnum.Columns;
            this.flex1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            this.flex1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.flex1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.flex1.AutoClipboard = true;
            this.flex1.BackColor = System.Drawing.Color.White;
            this.flex1.ColumnInfo = resources.GetString("flex1.ColumnInfo");
            this.flex1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flex1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flex1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.flex1.Location = new System.Drawing.Point(0, 0);
            this.flex1.Margin = new System.Windows.Forms.Padding(0);
            this.flex1.Name = "flex1";
            this.flex1.Rows.Count = 6;
            this.flex1.Rows.DefaultSize = 20;
            this.flex1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flex1.ShowSort = false;
            this.flex1.Size = new System.Drawing.Size(764, 334);
            this.flex1.StyleInfo = resources.GetString("flex1.StyleInfo");
            this.flex1.TabIndex = 14;
            this.flex1.Text = "c1FlexGrid1";
            this.flex1.AfterAddRow += new C1.Win.C1FlexGrid.RowColEventHandler(this.flex1_AfterAddRow);
            this.flex1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.flex1_MouseClick);
            this.flex1.BeforeAddRow += new C1.Win.C1FlexGrid.RowColEventHandler(this.flex1_BeforeAddRow);
            this.flex1.OwnerDrawCell += new C1.Win.C1FlexGrid.OwnerDrawCellEventHandler(this.flex1_OwnerDrawCell);
            // 
            // RegressionTestInfluenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 334);
            this.Controls.Add(this.flex1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "RegressionTestInfluenceForm";
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        protected Common.CustomMergeFlex flex1;
    }
}