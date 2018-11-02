using Common.RichTextBox;
using System.Windows.Forms;
using TPM3.Sys;

namespace TPM3.wx
{
    partial class SummaryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SummaryForm));
            this.splitter2 = new TPM3.Sys.MySplitter();
            this.grid1 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.grid2 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.rich1 = new Common.RichTextBox.RichTextBoxOle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btAutoAdd = new System.Windows.Forms.LinkLabel();
            this.rb12 = new System.Windows.Forms.RadioButton();
            this.rb11 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rich2 = new Common.RichTextBox.RichTextBoxOle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btAutoAddAbbrv = new System.Windows.Forms.LinkLabel();
            this.rb22 = new System.Windows.Forms.RadioButton();
            this.rb21 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 190);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(799, 3);
            this.splitter2.TabIndex = 1;
            this.splitter2.TabStop = false;
            // 
            // grid1
            // 
            this.grid1.AllowAddNew = true;
            this.grid1.AllowArrows = false;
            this.grid1.AllowDelete = true;
            this.grid1.AllowDrag = true;
            this.grid1.AllowSort = false;
            this.grid1.CaptionHeight = 18;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Left;
            this.grid1.GroupByCaption = "Drag a column header here to group by that column";
            this.grid1.Images.Add(((System.Drawing.Image)(resources.GetObject("grid1.Images"))));
            this.grid1.Location = new System.Drawing.Point(0, 34);
            this.grid1.Name = "grid1";
            this.grid1.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.grid1.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.grid1.PreviewInfo.ZoomFactor = 75D;
            this.grid1.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("grid1.PrintInfo.PageSettings")));
            this.grid1.RowHeight = 16;
            this.grid1.Size = new System.Drawing.Size(431, 122);
            this.grid1.TabIndex = 2;
            this.grid1.Text = "c1TrueDBGrid1";
            this.grid1.PropBag = resources.GetString("grid1.PropBag");
            // 
            // grid2
            // 
            this.grid2.AllowAddNew = true;
            this.grid2.AllowDelete = true;
            this.grid2.CaptionHeight = 18;
            this.grid2.Dock = System.Windows.Forms.DockStyle.Left;
            this.grid2.GroupByCaption = "Drag a column header here to group by that column";
            this.grid2.Images.Add(((System.Drawing.Image)(resources.GetObject("grid2.Images"))));
            this.grid2.Location = new System.Drawing.Point(0, 34);
            this.grid2.Name = "grid2";
            this.grid2.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.grid2.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.grid2.PreviewInfo.ZoomFactor = 75D;
            this.grid2.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("grid2.PrintInfo.PageSettings")));
            this.grid2.RowHeight = 16;
            this.grid2.Size = new System.Drawing.Size(428, 119);
            this.grid2.TabIndex = 4;
            this.grid2.Text = "c1TrueDBGrid2";
            this.grid2.PropBag = resources.GetString("grid2.PropBag");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox2);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(799, 190);
            this.splitContainer1.SplitterDistance = 431;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 33);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(431, 157);
            this.textBox1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(431, 33);
            this.label1.TabIndex = 3;
            this.label1.Text = "文档概述";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(0, 33);
            this.textBox2.Margin = new System.Windows.Forms.Padding(0);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(363, 157);
            this.textBox2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(363, 33);
            this.label2.TabIndex = 3;
            this.label2.Text = "与其它文档的关系";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 193);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.rich1);
            this.splitContainer2.Panel1.Controls.Add(this.grid1);
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rich2);
            this.splitContainer2.Panel2.Controls.Add(this.grid2);
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Size = new System.Drawing.Size(799, 314);
            this.splitContainer2.SplitterDistance = 156;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 5;
            // 
            // rich1
            // 
            this.rich1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rich1.Font = new System.Drawing.Font("宋体", 12F);
            this.rich1.HideSelection = false;
            this.rich1.Location = new System.Drawing.Point(431, 34);
            this.rich1.Name = "rich1";
            this.rich1.OleReadOnly = false;
            this.rich1.Size = new System.Drawing.Size(368, 122);
            this.rich1.TabIndex = 8;
            this.rich1.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btAutoAdd);
            this.panel1.Controls.Add(this.rb12);
            this.panel1.Controls.Add(this.rb11);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 34);
            this.panel1.TabIndex = 9;
            // 
            // btAutoAdd
            // 
            this.btAutoAdd.AutoSize = true;
            this.btAutoAdd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btAutoAdd.Location = new System.Drawing.Point(389, 12);
            this.btAutoAdd.Name = "btAutoAdd";
            this.btAutoAdd.Size = new System.Drawing.Size(131, 12);
            this.btAutoAdd.TabIndex = 9;
            this.btAutoAdd.TabStop = true;
            this.btAutoAdd.Text = "自动添加相关引用文档 ";
            this.toolTip1.SetToolTip(this.btAutoAdd, "自动添加相关的引用文档。比如测试说明自动引用测试计划");
            this.btAutoAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btAutoAdd_LinkClicked);
            // 
            // rb12
            // 
            this.rb12.Location = new System.Drawing.Point(264, 3);
            this.rb12.Name = "rb12";
            this.rb12.Size = new System.Drawing.Size(81, 31);
            this.rb12.TabIndex = 8;
            this.rb12.TabStop = true;
            this.rb12.Text = "Word文档";
            this.rb12.UseVisualStyleBackColor = true;
            this.rb12.CheckedChanged += new System.EventHandler(this.rb11_CheckedChanged);
            // 
            // rb11
            // 
            this.rb11.Checked = true;
            this.rb11.Location = new System.Drawing.Point(166, 3);
            this.rb11.Name = "rb11";
            this.rb11.Size = new System.Drawing.Size(53, 31);
            this.rb11.TabIndex = 8;
            this.rb11.TabStop = true;
            this.rb11.Text = "列表";
            this.rb11.UseVisualStyleBackColor = true;
            this.rb11.CheckedChanged += new System.EventHandler(this.rb11_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 34);
            this.label4.TabIndex = 7;
            this.label4.Text = "引用文档";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rich2
            // 
            this.rich2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rich2.Font = new System.Drawing.Font("宋体", 12F);
            this.rich2.HideSelection = false;
            this.rich2.Location = new System.Drawing.Point(428, 34);
            this.rich2.Name = "rich2";
            this.rich2.OleReadOnly = false;
            this.rich2.Size = new System.Drawing.Size(371, 119);
            this.rich2.TabIndex = 7;
            this.rich2.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btAutoAddAbbrv);
            this.panel2.Controls.Add(this.rb22);
            this.panel2.Controls.Add(this.rb21);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(799, 34);
            this.panel2.TabIndex = 8;
            // 
            // btAutoAddAbbrv
            // 
            this.btAutoAddAbbrv.AutoSize = true;
            this.btAutoAddAbbrv.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btAutoAddAbbrv.Location = new System.Drawing.Point(389, 13);
            this.btAutoAddAbbrv.Name = "btAutoAddAbbrv";
            this.btAutoAddAbbrv.Size = new System.Drawing.Size(89, 12);
            this.btAutoAddAbbrv.TabIndex = 9;
            this.btAutoAddAbbrv.TabStop = true;
            this.btAutoAddAbbrv.Text = "添加相关缩略语";
            this.btAutoAddAbbrv.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btAutoAddAbbrv_LinkClicked);
            // 
            // rb22
            // 
            this.rb22.AutoSize = true;
            this.rb22.Location = new System.Drawing.Point(260, 9);
            this.rb22.Name = "rb22";
            this.rb22.Size = new System.Drawing.Size(81, 18);
            this.rb22.TabIndex = 10;
            this.rb22.TabStop = true;
            this.rb22.Text = "Word文档";
            this.rb22.UseVisualStyleBackColor = true;
            this.rb22.CheckedChanged += new System.EventHandler(this.rb11_CheckedChanged);
            // 
            // rb21
            // 
            this.rb21.AutoSize = true;
            this.rb21.Checked = true;
            this.rb21.Location = new System.Drawing.Point(162, 9);
            this.rb21.Name = "rb21";
            this.rb21.Size = new System.Drawing.Size(53, 18);
            this.rb21.TabIndex = 9;
            this.rb21.TabStop = true;
            this.rb21.Text = "列表";
            this.rb21.UseVisualStyleBackColor = true;
            this.rb21.CheckedChanged += new System.EventHandler(this.rb11_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 34);
            this.label3.TabIndex = 6;
            this.label3.Text = "术语和缩略语";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 507);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "SummaryForm";
            this.Text = "SummaryForm";
            this.Load += new System.EventHandler(this.SummaryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid2)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        C1.Win.C1TrueDBGrid.C1TrueDBGrid grid1;
        C1.Win.C1TrueDBGrid.C1TrueDBGrid grid2;
        SplitContainer splitContainer1;
        SplitContainer splitContainer2;
        TextBox textBox1;
        TextBox textBox2;
        MySplitter splitter2;
        Label label1;
        Label label2;
        Label label3;
        Label label4;
        Panel panel1;
        Panel panel2;
        RadioButton rb22;
        RadioButton rb21;
        RadioButton rb12;
        RadioButton rb11;
        RichTextBoxOle rich2;
        RichTextBoxOle rich1;
        private LinkLabel btAutoAdd;
        private ToolTip toolTip1;
        private LinkLabel btAutoAddAbbrv;
    }
}