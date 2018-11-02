namespace TPM3.wx
{
    partial class FallTraceTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FallTraceTree));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.flex1 = new Common.CustomMergeFlex();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.lbWarning = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(474, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(315, 27);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "统计到测试对象",
            "统计到测试分类",
            "统计到测试项"});
            this.comboBox1.Location = new System.Drawing.Point(114, 6);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(158, 22);
            this.comboBox1.TabIndex = 13;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // flex1
            // 
            this.flex1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flex1.AllowEditing = false;
            this.flex1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            this.flex1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.flex1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.flex1.BackColor = System.Drawing.Color.White;
            this.flex1.ColumnInfo = "4,1,0,0,0,100,Columns:0{Width:14;TextAlign:CenterCenter;TextAlignFixed:CenterCent" +
                "er;}\t1{Width:60;DataType:System.String;TextAlign:LeftCenter;TextAlignFixed:Cente" +
                "rCenter;}\t2{Width:200;}\t3{Width:60;}\t";
            this.flex1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flex1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flex1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.flex1.Location = new System.Drawing.Point(0, 88);
            this.flex1.Name = "flex1";
            this.flex1.Rows.Count = 6;
            this.flex1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flex1.ShowSort = false;
            this.flex1.Size = new System.Drawing.Size(809, 378);
            this.flex1.TabIndex = 15;
            this.flex1.Text = "c1FlexGrid1";
            this.flex1.Tree.Column = 1;
            this.flex1.Tree.Indent = 10;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 35);
            this.label3.TabIndex = 12;
            this.label3.Text = "软件问题统计表";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 37);
            this.panel1.TabIndex = 16;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "对问题级别进行统计",
            "对问题类型进行统计",
            "按报告人进行统计"});
            this.comboBox2.Location = new System.Drawing.Point(282, 6);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(5);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(184, 22);
            this.comboBox2.TabIndex = 13;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lbWarning
            // 
            this.lbWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbWarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbWarning.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWarning.ForeColor = System.Drawing.Color.Red;
            this.lbWarning.Location = new System.Drawing.Point(0, 37);
            this.lbWarning.Name = "lbWarning";
            this.lbWarning.Size = new System.Drawing.Size(809, 51);
            this.lbWarning.TabIndex = 17;
            this.lbWarning.Text = "提示：因为一个问题的报告人可以有多个，一个问题也可以被不同测试分类（或测试项）下的用例引用，所以一个问题可以统计在多个人（或测试分类、测试项）的名下，最终导致各子" +
                "项的问题数之和与总数不一致。";
            this.lbWarning.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FallTraceTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flex1);
            this.Controls.Add(this.lbWarning);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FallTraceTree";
            this.Size = new System.Drawing.Size(809, 466);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private Common.CustomMergeFlex flex1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label lbWarning;
    }
}
