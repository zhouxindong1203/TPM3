namespace TPM3.wx
{
    partial class TestCaseResultSummary
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestCaseResultSummary));
            this.flex1 = new Common.CustomMergeFlex();
            this.label3 = new System.Windows.Forms.Label();
            this.llRepare = new System.Windows.Forms.LinkLabel();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).BeginInit();
            this.SuspendLayout();
            // 
            // flex1
            // 
            this.flex1.AllowEditing = false;
            this.flex1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            this.flex1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.flex1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.flex1.BackColor = System.Drawing.Color.White;
            this.flex1.ColumnInfo = resources.GetString("flex1.ColumnInfo");
            this.flex1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flex1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flex1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.flex1.Location = new System.Drawing.Point(0, 26);
            this.flex1.Margin = new System.Windows.Forms.Padding(2);
            this.flex1.Name = "flex1";
            this.flex1.Rows.Count = 6;
            this.flex1.Rows.DefaultSize = 18;
            this.flex1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flex1.ShowSort = false;
            this.flex1.Size = new System.Drawing.Size(512, 292);
            this.flex1.StyleInfo = resources.GetString("flex1.StyleInfo");
            this.flex1.TabIndex = 9;
            this.flex1.Text = "c1FlexGrid1";
            this.flex1.OwnerDrawCell += new C1.Win.C1FlexGrid.OwnerDrawCellEventHandler(this.flex1_OwnerDrawCell);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(512, 26);
            this.label3.TabIndex = 8;
            this.label3.Text = "测试用例执行情况与执行结果统计";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llRepare
            // 
            this.llRepare.AutoSize = true;
            this.llRepare.Location = new System.Drawing.Point(317, 9);
            this.llRepare.Name = "llRepare";
            this.llRepare.Size = new System.Drawing.Size(65, 12);
            this.llRepare.TabIndex = 10;
            this.llRepare.TabStop = true;
            this.llRepare.Text = "修复数据库";
            this.C1SuperTooltip1.SetToolTip(this.llRepare, "修复数据库中没有执行结果的问题。<br>\n(仅对完整执行的用例有效。)");
            this.llRepare.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llRepare_LinkClicked);
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
            // TestCaseResultSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.llRepare);
            this.Controls.Add(this.flex1);
            this.Controls.Add(this.label3);
            this.Name = "TestCaseResultSummary";
            this.Size = new System.Drawing.Size(512, 318);
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Common.CustomMergeFlex flex1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel llRepare;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
    }
}
