namespace TPM3.wx
{
    partial class RegressModifyControl1
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
            if(disposing && (components != null))
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
            this.flex1 = new Common.CustomMergeFlex();
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).BeginInit();
            this.SuspendLayout();
            // 
            // flex1
            // 
            this.flex1.AllowAddNew = true;
            this.flex1.AllowDelete = true;
            this.flex1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flex1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.flex1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.flex1.ColumnInfo = "4,1,0,0,0,90,Columns:0{Width:14;}\t";
            this.flex1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flex1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flex1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.flex1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            this.flex1.Location = new System.Drawing.Point(0, 0);
            this.flex1.Name = "flex1";
            this.flex1.Rows.Count = 6;
            this.flex1.Rows.DefaultSize = 18;
            this.flex1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
            this.flex1.ShowSort = false;
            this.flex1.Size = new System.Drawing.Size(601, 373);
            this.flex1.TabIndex = 8;
            this.flex1.Tree.Column = 1;
            this.flex1.Tree.Style = ((C1.Win.C1FlexGrid.TreeStyleFlags)(((C1.Win.C1FlexGrid.TreeStyleFlags.Lines | C1.Win.C1FlexGrid.TreeStyleFlags.Symbols) 
            | C1.Win.C1FlexGrid.TreeStyleFlags.Leaf)));
            // 
            // RegressModifyControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flex1);
            this.Name = "RegressModifyControl1";
            this.Size = new System.Drawing.Size(601, 373);
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Common.CustomMergeFlex flex1;
    }
}
