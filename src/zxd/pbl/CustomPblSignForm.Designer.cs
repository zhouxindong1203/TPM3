namespace TPM3.zxd.pbl
{
    partial class CustomPblSignForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomPblSignForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.c1TrueDBGrid1 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.c1TrueDBGrid2 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.c1TrueDBGrid3 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.c1TrueDBGrid4 = new C1.Win.C1TrueDBGrid.C1TrueDBGrid();
            this.txtPblDemo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSplitter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid2)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid3)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid4)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(766, 713);
            this.splitContainer1.SplitterDistance = 331;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.c1TrueDBGrid1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.c1TrueDBGrid2);
            this.splitContainer2.Size = new System.Drawing.Size(766, 331);
            this.splitContainer2.SplitterDistance = 155;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 0;
            // 
            // c1TrueDBGrid1
            // 
            this.c1TrueDBGrid1.AllowAddNew = true;
            this.c1TrueDBGrid1.AllowDelete = true;
            this.c1TrueDBGrid1.CaptionHeight = 18;
            this.c1TrueDBGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1TrueDBGrid1.GroupByCaption = "Drag a column header here to group by that column";
            this.c1TrueDBGrid1.Images.Add(((System.Drawing.Image)(resources.GetObject("c1TrueDBGrid1.Images"))));
            this.c1TrueDBGrid1.Location = new System.Drawing.Point(0, 0);
            this.c1TrueDBGrid1.Name = "c1TrueDBGrid1";
            this.c1TrueDBGrid1.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1TrueDBGrid1.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1TrueDBGrid1.PreviewInfo.ZoomFactor = 75;
            this.c1TrueDBGrid1.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("c1TrueDBGrid1.PrintInfo.PageSettings")));
            this.c1TrueDBGrid1.RowHeight = 16;
            this.c1TrueDBGrid1.Size = new System.Drawing.Size(766, 155);
            this.c1TrueDBGrid1.TabIndex = 0;
            this.c1TrueDBGrid1.Text = "c1TrueDBGrid1";
            this.c1TrueDBGrid1.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.c1TrueDBGrid1_BeforeDelete);
            this.c1TrueDBGrid1.OnAddNew += new System.EventHandler(this.c1TrueDBGrid1_OnAddNew);
            this.c1TrueDBGrid1.AfterDelete += new System.EventHandler(this.txtSplitter_TextChanged);
            this.c1TrueDBGrid1.AfterUpdate += new System.EventHandler(this.txtSplitter_TextChanged);
            this.c1TrueDBGrid1.AfterInsert += new System.EventHandler(this.txtSplitter_TextChanged);
            this.c1TrueDBGrid1.PropBag = resources.GetString("c1TrueDBGrid1.PropBag");
            // 
            // c1TrueDBGrid2
            // 
            this.c1TrueDBGrid2.AllowAddNew = true;
            this.c1TrueDBGrid2.AllowDelete = true;
            this.c1TrueDBGrid2.CaptionHeight = 18;
            this.c1TrueDBGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1TrueDBGrid2.GroupByCaption = "Drag a column header here to group by that column";
            this.c1TrueDBGrid2.Images.Add(((System.Drawing.Image)(resources.GetObject("c1TrueDBGrid2.Images"))));
            this.c1TrueDBGrid2.Location = new System.Drawing.Point(0, 0);
            this.c1TrueDBGrid2.Name = "c1TrueDBGrid2";
            this.c1TrueDBGrid2.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1TrueDBGrid2.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1TrueDBGrid2.PreviewInfo.ZoomFactor = 75;
            this.c1TrueDBGrid2.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("c1TrueDBGrid2.PrintInfo.PageSettings")));
            this.c1TrueDBGrid2.RowHeight = 16;
            this.c1TrueDBGrid2.Size = new System.Drawing.Size(766, 171);
            this.c1TrueDBGrid2.TabIndex = 0;
            this.c1TrueDBGrid2.Text = "c1TrueDBGrid2";
            this.c1TrueDBGrid2.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.c1TrueDBGrid2_BeforeDelete);
            this.c1TrueDBGrid2.OnAddNew += new System.EventHandler(this.c1TrueDBGrid2_OnAddNew);
            this.c1TrueDBGrid2.AfterDelete += new System.EventHandler(this.txtSplitter_TextChanged);
            this.c1TrueDBGrid2.AfterUpdate += new System.EventHandler(this.txtSplitter_TextChanged);
            this.c1TrueDBGrid2.AfterInsert += new System.EventHandler(this.txtSplitter_TextChanged);
            this.c1TrueDBGrid2.PropBag = resources.GetString("c1TrueDBGrid2.PropBag");
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.c1TrueDBGrid3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(766, 377);
            this.splitContainer3.SplitterDistance = 177;
            this.splitContainer3.SplitterWidth = 5;
            this.splitContainer3.TabIndex = 0;
            // 
            // c1TrueDBGrid3
            // 
            this.c1TrueDBGrid3.AllowAddNew = true;
            this.c1TrueDBGrid3.AllowDelete = true;
            this.c1TrueDBGrid3.CaptionHeight = 18;
            this.c1TrueDBGrid3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1TrueDBGrid3.GroupByCaption = "Drag a column header here to group by that column";
            this.c1TrueDBGrid3.Images.Add(((System.Drawing.Image)(resources.GetObject("c1TrueDBGrid3.Images"))));
            this.c1TrueDBGrid3.Location = new System.Drawing.Point(0, 0);
            this.c1TrueDBGrid3.Name = "c1TrueDBGrid3";
            this.c1TrueDBGrid3.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1TrueDBGrid3.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1TrueDBGrid3.PreviewInfo.ZoomFactor = 75;
            this.c1TrueDBGrid3.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("c1TrueDBGrid3.PrintInfo.PageSettings")));
            this.c1TrueDBGrid3.RowHeight = 16;
            this.c1TrueDBGrid3.Size = new System.Drawing.Size(766, 177);
            this.c1TrueDBGrid3.TabIndex = 0;
            this.c1TrueDBGrid3.Text = "c1TrueDBGrid3";
            this.c1TrueDBGrid3.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.c1TrueDBGrid3_BeforeDelete);
            this.c1TrueDBGrid3.OnAddNew += new System.EventHandler(this.c1TrueDBGrid3_OnAddNew);
            this.c1TrueDBGrid3.AfterDelete += new System.EventHandler(this.txtSplitter_TextChanged);
            this.c1TrueDBGrid3.AfterUpdate += new System.EventHandler(this.txtSplitter_TextChanged);
            this.c1TrueDBGrid3.AfterInsert += new System.EventHandler(this.txtSplitter_TextChanged);
            this.c1TrueDBGrid3.PropBag = resources.GetString("c1TrueDBGrid3.PropBag");
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.c1TrueDBGrid4);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.txtPblDemo);
            this.splitContainer4.Panel2.Controls.Add(this.label2);
            this.splitContainer4.Panel2.Controls.Add(this.txtSplitter);
            this.splitContainer4.Panel2.Controls.Add(this.label1);
            this.splitContainer4.Size = new System.Drawing.Size(766, 195);
            this.splitContainer4.SplitterDistance = 143;
            this.splitContainer4.SplitterWidth = 5;
            this.splitContainer4.TabIndex = 0;
            // 
            // c1TrueDBGrid4
            // 
            this.c1TrueDBGrid4.AllowAddNew = true;
            this.c1TrueDBGrid4.AllowDelete = true;
            this.c1TrueDBGrid4.CaptionHeight = 18;
            this.c1TrueDBGrid4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1TrueDBGrid4.GroupByCaption = "Drag a column header here to group by that column";
            this.c1TrueDBGrid4.Images.Add(((System.Drawing.Image)(resources.GetObject("c1TrueDBGrid4.Images"))));
            this.c1TrueDBGrid4.Location = new System.Drawing.Point(0, 0);
            this.c1TrueDBGrid4.Name = "c1TrueDBGrid4";
            this.c1TrueDBGrid4.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1TrueDBGrid4.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1TrueDBGrid4.PreviewInfo.ZoomFactor = 75;
            this.c1TrueDBGrid4.PrintInfo.PageSettings = ((System.Drawing.Printing.PageSettings)(resources.GetObject("c1TrueDBGrid4.PrintInfo.PageSettings")));
            this.c1TrueDBGrid4.RowHeight = 16;
            this.c1TrueDBGrid4.Size = new System.Drawing.Size(766, 143);
            this.c1TrueDBGrid4.TabIndex = 1;
            this.c1TrueDBGrid4.Text = "c1TrueDBGrid4";
            this.c1TrueDBGrid4.BeforeDelete += new C1.Win.C1TrueDBGrid.CancelEventHandler(this.c1TrueDBGrid4_BeforeDelete);
            this.c1TrueDBGrid4.OnAddNew += new System.EventHandler(this.c1TrueDBGrid4_OnAddNew);
            this.c1TrueDBGrid4.AfterDelete += new System.EventHandler(this.txtSplitter_TextChanged);
            this.c1TrueDBGrid4.AfterUpdate += new System.EventHandler(this.txtSplitter_TextChanged);
            this.c1TrueDBGrid4.AfterInsert += new System.EventHandler(this.txtSplitter_TextChanged);
            this.c1TrueDBGrid4.PropBag = resources.GetString("c1TrueDBGrid4.PropBag");
            // 
            // txtPblDemo
            // 
            this.txtPblDemo.BackColor = System.Drawing.Color.White;
            this.txtPblDemo.ForeColor = System.Drawing.Color.Red;
            this.txtPblDemo.Location = new System.Drawing.Point(278, 20);
            this.txtPblDemo.Name = "txtPblDemo";
            this.txtPblDemo.ReadOnly = true;
            this.txtPblDemo.Size = new System.Drawing.Size(474, 23);
            this.txtPblDemo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "问题标识示例";
            // 
            // txtSplitter
            // 
            this.txtSplitter.Location = new System.Drawing.Point(125, 20);
            this.txtSplitter.MaxLength = 1;
            this.txtSplitter.Name = "txtSplitter";
            this.txtSplitter.Size = new System.Drawing.Size(24, 23);
            this.txtSplitter.TabIndex = 1;
            this.txtSplitter.TextChanged += new System.EventHandler(this.txtSplitter_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "字段间分隔符";
            // 
            // CustomPblSignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 713);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "CustomPblSignForm";
            this.Text = "CustomPblSignForm";
            this.Load += new System.EventHandler(this.CustomPblSignForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid2)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid3)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1TrueDBGrid4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1TrueDBGrid1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1TrueDBGrid2;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1TrueDBGrid3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private C1.Win.C1TrueDBGrid.C1TrueDBGrid c1TrueDBGrid4;
        private System.Windows.Forms.TextBox txtSplitter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPblDemo;
        private System.Windows.Forms.Label label2;
    }
}