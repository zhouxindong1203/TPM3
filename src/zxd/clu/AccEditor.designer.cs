namespace DDEditor
{
    partial class AccEditor
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
            this.button1 = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.chbSelectAll = new System.Windows.Forms.CheckBox();
            this.btnViewContent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(213, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "返回";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 24);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(258, 132);
            this.checkedListBox1.TabIndex = 14;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "附件列表";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 184);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 23);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(67, 184);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(57, 23);
            this.btnDel.TabIndex = 17;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // chbSelectAll
            // 
            this.chbSelectAll.AutoSize = true;
            this.chbSelectAll.Location = new System.Drawing.Point(12, 162);
            this.chbSelectAll.Name = "chbSelectAll";
            this.chbSelectAll.Size = new System.Drawing.Size(72, 16);
            this.chbSelectAll.TabIndex = 18;
            this.chbSelectAll.Text = "全部选中";
            this.chbSelectAll.UseVisualStyleBackColor = true;
            this.chbSelectAll.CheckedChanged += new System.EventHandler(this.chbSelectAll_CheckedChanged);
            // 
            // btnViewContent
            // 
            this.btnViewContent.Location = new System.Drawing.Point(122, 184);
            this.btnViewContent.Name = "btnViewContent";
            this.btnViewContent.Size = new System.Drawing.Size(57, 23);
            this.btnViewContent.TabIndex = 19;
            this.btnViewContent.Text = "查看";
            this.btnViewContent.UseVisualStyleBackColor = true;
            this.btnViewContent.Click += new System.EventHandler(this.btnViewContent_Click);
            // 
            // AccEditor
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(282, 216);
            this.Controls.Add(this.btnViewContent);
            this.Controls.Add(this.chbSelectAll);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.button1);
            this.Name = "AccEditor";
            this.Options = ((C1.Win.C1Input.DropDownFormOptionsFlags)((C1.Win.C1Input.DropDownFormOptionsFlags.Focusable | C1.Win.C1Input.DropDownFormOptionsFlags.NoPostOnEnter)));
            this.Text = "AbsTimeEditor";
            this.PostChanges += new System.EventHandler(this.AccEditor_PostChanges);
            this.Open += new System.EventHandler(this.AccEditor_Open);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.CheckBox chbSelectAll;
        private System.Windows.Forms.Button btnViewContent;

    }
}