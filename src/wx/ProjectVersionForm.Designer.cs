namespace TPM3.wx
{
    partial class ProjectVersionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectVersionForm));
            this.flex1 = new Common.CustomMergeFlex();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flex1
            // 
            this.flex1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flex1.AllowEditing = false;
            this.flex1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            this.flex1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.flex1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.flex1.ColumnInfo = "2,1,0,0,0,90,Columns:0{Width:14;}\t1{Width:151;Name:\"主要完成人\";Caption:\"主要完成人\";DataTy" +
                "pe:System.String;TextAlign:LeftCenter;TextAlignFixed:CenterCenter;}\t";
            this.flex1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flex1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flex1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.flex1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            this.flex1.Location = new System.Drawing.Point(0, 0);
            this.flex1.Margin = new System.Windows.Forms.Padding(2);
            this.flex1.Name = "flex1";
            this.flex1.Rows.Count = 5;
            this.flex1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
            this.flex1.ShowSort = false;
            this.flex1.Size = new System.Drawing.Size(824, 324);
            this.flex1.TabIndex = 6;
            this.flex1.Tree.Column = 1;
            this.flex1.Tree.Style = ((C1.Win.C1FlexGrid.TreeStyleFlags)(((C1.Win.C1FlexGrid.TreeStyleFlags.Lines | C1.Win.C1FlexGrid.TreeStyleFlags.Symbols)
                        | C1.Win.C1FlexGrid.TreeStyleFlags.Leaf)));
            this.flex1.DoubleClick += new System.EventHandler(this.flex1_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 324);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 64);
            this.panel1.TabIndex = 7;
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(452, 29);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(74, 23);
            this.btCancel.TabIndex = 0;
            this.btCancel.Text = "退 出";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(104, 29);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(126, 23);
            this.btOK.TabIndex = 0;
            this.btOK.Text = "切换到选中版本";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // ProjectVersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 388);
            this.Controls.Add(this.flex1);
            this.Controls.Add(this.panel1);
            this.Name = "ProjectVersionForm";
            this.Text = "项目版本管理";
            this.Load += new System.EventHandler(this.ProjectVersionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Common.CustomMergeFlex flex1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
    }
}