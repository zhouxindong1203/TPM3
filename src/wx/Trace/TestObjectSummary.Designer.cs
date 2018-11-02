using Common;

namespace TPM3.wx
{
    partial class TestObjectSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestObjectSummary));
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grid1 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.flex1 = new Common.CustomMergeFlex();
            this.testResultObjectControl41 = new FallMatrixTable();
            this.lbBeginTime = new System.Windows.Forms.Label();
            this.lbEndTime = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(528, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "测试执行情况";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.78908F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.21092F));
            this.tableLayoutPanel1.Controls.Add(this.grid1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.testResultObjectControl41, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(530, 808);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // grid1
            // 
            this.grid1.AllowUpdate = false;
            this.grid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid1.CaptionHeight = 18;
            this.tableLayoutPanel1.SetColumnSpan(this.grid1, 2);
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.GroupByCaption = "Drag a column header here to group by that column";
            this.grid1.Images.Add(((System.Drawing.Image)(resources.GetObject("grid1.Images"))));
            this.grid1.Location = new System.Drawing.Point(1, 419);
            this.grid1.Margin = new System.Windows.Forms.Padding(0);
            this.grid1.Name = "grid1";
            this.grid1.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.grid1.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.grid1.PreviewInfo.ZoomFactor = 75;
            this.grid1.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("grid1.PrintInfo.PageSettings")));
            this.grid1.RowHeight = 20;
            this.grid1.Size = new System.Drawing.Size(528, 388);
            this.grid1.TabIndex = 5;
            this.grid1.Text = "c1TrueDBGrid1";
            this.grid1.PropBag = resources.GetString("grid1.PropBag");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbBeginTime);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 55);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "测试开始时间";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbEndTime);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(274, 1);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(255, 55);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "测试结束时间";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 2);
            this.panel3.Controls.Add(this.flex1);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 57);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(528, 180);
            this.panel3.TabIndex = 0;
            // 
            // flex1
            // 
            this.flex1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flex1.AllowEditing = false;
            this.flex1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            this.flex1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.flex1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.flex1.BackColor = System.Drawing.Color.White;
            this.flex1.ColumnInfo = "4,0,0,0,0,100,Columns:0{Width:200;TextAlign:CenterCenter;TextAlignFixed:CenterCen" +
                "ter;}\t1{Width:60;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:Cent" +
                "erCenter;}\t2{Width:200;}\t3{Width:60;}\t";
            this.flex1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flex1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flex1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.flex1.Location = new System.Drawing.Point(0, 18);
            this.flex1.Margin = new System.Windows.Forms.Padding(2);
            this.flex1.Name = "flex1";
            this.flex1.Rows.Count = 6;
            this.flex1.Rows.Fixed = 0;
            this.flex1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flex1.ShowSort = false;
            this.flex1.Size = new System.Drawing.Size(528, 162);
            this.flex1.TabIndex = 5;
            this.flex1.Text = "c1FlexGrid1";
            // 
            // testResultObjectControl41
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.testResultObjectControl41, 2);
            this.testResultObjectControl41.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testResultObjectControl41.Location = new System.Drawing.Point(4, 241);
            this.testResultObjectControl41.Name = "testResultObjectControl41";
            this.testResultObjectControl41.Size = new System.Drawing.Size(522, 174);
            this.testResultObjectControl41.TabIndex = 6;
            // 
            // lbBeginTime
            // 
            this.lbBeginTime.BackColor = System.Drawing.Color.White;
            this.lbBeginTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbBeginTime.Location = new System.Drawing.Point(0, 30);
            this.lbBeginTime.Margin = new System.Windows.Forms.Padding(0);
            this.lbBeginTime.Name = "lbBeginTime";
            this.lbBeginTime.Size = new System.Drawing.Size(272, 25);
            this.lbBeginTime.TabIndex = 1;
            this.lbBeginTime.Text = "label4";
            this.lbBeginTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbEndTime
            // 
            this.lbEndTime.BackColor = System.Drawing.Color.White;
            this.lbEndTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEndTime.Location = new System.Drawing.Point(0, 30);
            this.lbEndTime.Margin = new System.Windows.Forms.Padding(0);
            this.lbEndTime.Name = "lbEndTime";
            this.lbEndTime.Size = new System.Drawing.Size(255, 25);
            this.lbEndTime.TabIndex = 1;
            this.lbEndTime.Text = "label4";
            this.lbEndTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TestResultObjectControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(530, 808);
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TestResultObjectControl1";
            this.Size = new System.Drawing.Size(504, 433);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid grid1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private CustomMergeFlex flex1;
        private FallMatrixTable testResultObjectControl41;
        private System.Windows.Forms.Label lbBeginTime;
        private System.Windows.Forms.Label lbEndTime;

    }
}
