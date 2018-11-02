namespace TPM3.lt
{
    partial class RegressionTestDesignForm
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddTestCase = new System.Windows.Forms.Button();
            this.DeleteTestCase = new System.Windows.Forms.Button();
            this.addTestCaseBox = new System.Windows.Forms.TextBox();
            this.AddTestCaseButton = new System.Windows.Forms.Button();
            this.CancelTestCaseButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(568, 303);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCheck);
            this.treeView1.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeSelect);
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Location = new System.Drawing.Point(360, 9);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 1;
            this.OK.Text = "确定";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(455, 9);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "取消";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(393, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 173);
            this.panel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(102, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 26);
            this.button1.TabIndex = 2;
            this.button1.Text = "应用";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(11, 133);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 26);
            this.button3.TabIndex = 0;
            this.button3.Text = "取消";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 27);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 88);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入取消的原因:";
            // 
            // AddTestCase
            // 
            this.AddTestCase.Location = new System.Drawing.Point(445, 105);
            this.AddTestCase.Name = "AddTestCase";
            this.AddTestCase.Size = new System.Drawing.Size(98, 23);
            this.AddTestCase.TabIndex = 3;
            this.AddTestCase.Text = "添加测试用例";
            this.AddTestCase.UseVisualStyleBackColor = true;
            this.AddTestCase.Click += new System.EventHandler(this.AddTestCase_Click);
            // 
            // DeleteTestCase
            // 
            this.DeleteTestCase.Location = new System.Drawing.Point(445, 134);
            this.DeleteTestCase.Name = "DeleteTestCase";
            this.DeleteTestCase.Size = new System.Drawing.Size(98, 23);
            this.DeleteTestCase.TabIndex = 4;
            this.DeleteTestCase.Text = "删除测试用例";
            this.DeleteTestCase.UseVisualStyleBackColor = true;
            this.DeleteTestCase.Click += new System.EventHandler(this.DeleteTestCase_Click);
            // 
            // addTestCaseBox
            // 
            this.addTestCaseBox.Location = new System.Drawing.Point(445, 134);
            this.addTestCaseBox.Name = "addTestCaseBox";
            this.addTestCaseBox.Size = new System.Drawing.Size(121, 21);
            this.addTestCaseBox.TabIndex = 4;
            // 
            // AddTestCaseButton
            // 
            this.AddTestCaseButton.Location = new System.Drawing.Point(513, 161);
            this.AddTestCaseButton.Name = "AddTestCaseButton";
            this.AddTestCaseButton.Size = new System.Drawing.Size(53, 23);
            this.AddTestCaseButton.TabIndex = 5;
            this.AddTestCaseButton.Text = "确认";
            this.AddTestCaseButton.UseVisualStyleBackColor = true;
            this.AddTestCaseButton.Click += new System.EventHandler(this.AddTestCaseButton_Click);
            // 
            // CancelTestCaseButton
            // 
            this.CancelTestCaseButton.Location = new System.Drawing.Point(445, 161);
            this.CancelTestCaseButton.Name = "CancelTestCaseButton";
            this.CancelTestCaseButton.Size = new System.Drawing.Size(53, 23);
            this.CancelTestCaseButton.TabIndex = 6;
            this.CancelTestCaseButton.Text = "取消";
            this.CancelTestCaseButton.UseVisualStyleBackColor = true;
            this.CancelTestCaseButton.Click += new System.EventHandler(this.CancelTestCaseButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Cancel);
            this.panel2.Controls.Add(this.OK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 303);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(568, 35);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.treeView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(568, 303);
            this.panel3.TabIndex = 4;
            // 
            // RegressionTestDesignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 338);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "RegressionTestDesignForm";
            this.Text = "回归测试影响域分析";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button AddTestCase;
        private System.Windows.Forms.Button DeleteTestCase;
        private System.Windows.Forms.TextBox addTestCaseBox;
        private System.Windows.Forms.Button AddTestCaseButton;
        private System.Windows.Forms.Button CancelTestCaseButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;

    }
}