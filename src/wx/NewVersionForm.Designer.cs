namespace TPM3.wx
{
    partial class NewVersionForm
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
            this.wizardPage2 = new Crownwood.Magic.Controls.WizardPage();
            this.tbVersionMemo = new System.Windows.Forms.TextBox();
            this.tbVersionName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.wizardPage1 = new Crownwood.Magic.Controls.WizardPage();
            this.flex1 = new Common.CustomMergeFlex();
            this.label2 = new System.Windows.Forms.Label();
            this.wizardPage3 = new Crownwood.Magic.Controls.WizardPage();
            this.label4 = new System.Windows.Forms.Label();
            this.wizardPage2.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).BeginInit();
            this.wizardPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl
            // 
            this.wizardControl.ButtonBackText = "< 上一步";
            this.wizardControl.ButtonCancelText = "取  消";
            this.wizardControl.ButtonFinishText = "完  成";
            this.wizardControl.ButtonNextText = "下一步 >";
            this.wizardControl.SelectedIndex = 0;
            this.wizardControl.Size = new System.Drawing.Size(647, 408);
            this.wizardControl.Title = "欢迎使用创建回归测试项目向导";
            this.wizardControl.WizardPages.AddRange(new Crownwood.Magic.Controls.WizardPage[] {
            this.wizardPage1,
            this.wizardPage2,
            this.wizardPage3});
            // 
            // wizardPage2
            // 
            this.wizardPage2.CaptionTitle = "(选择要进行回归的问题报告单)";
            this.wizardPage2.Controls.Add(this.tbVersionMemo);
            this.wizardPage2.Controls.Add(this.tbVersionName);
            this.wizardPage2.Controls.Add(this.label3);
            this.wizardPage2.Controls.Add(this.label1);
            this.wizardPage2.FullPage = false;
            this.wizardPage2.Location = new System.Drawing.Point(0, 0);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Selected = false;
            this.wizardPage2.Size = new System.Drawing.Size(416, 156);
            this.wizardPage2.SubTitle = "(设置新回归版本的属性)";
            this.wizardPage2.TabIndex = 0;
            this.wizardPage2.Title = "设置新回归版本的属性";
            // 
            // tbVersionMemo
            // 
            this.tbVersionMemo.Location = new System.Drawing.Point(154, 71);
            this.tbVersionMemo.Multiline = true;
            this.tbVersionMemo.Name = "tbVersionMemo";
            this.tbVersionMemo.Size = new System.Drawing.Size(357, 179);
            this.tbVersionMemo.TabIndex = 4;
            // 
            // tbVersionName
            // 
            this.tbVersionName.Location = new System.Drawing.Point(154, 25);
            this.tbVersionName.Name = "tbVersionName";
            this.tbVersionName.Size = new System.Drawing.Size(357, 21);
            this.tbVersionName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "回归版本说明";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "回归版本名称";
            // 
            // wizardPage1
            // 
            this.wizardPage1.CaptionTitle = "(选择要进行回归测试的版本)";
            this.wizardPage1.Controls.Add(this.flex1);
            this.wizardPage1.Controls.Add(this.label2);
            this.wizardPage1.FullPage = false;
            this.wizardPage1.Location = new System.Drawing.Point(0, 0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(416, 156);
            this.wizardPage1.SubTitle = "(选择要进行回归测试的版本)";
            this.wizardPage1.TabIndex = 1;
            this.wizardPage1.Title = "选择要进行回归测试的版本";
            // 
            // flex1
            // 
            this.flex1.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
            this.flex1.AllowEditing = false;
            this.flex1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            this.flex1.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Both;
            this.flex1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.flex1.ColumnInfo = "2,1,0,0,0,90,Columns:0{Width:14;}\t1{Width:151;Name:\"主要完成人\";Caption:\"主要完成人\";Style:" +
                "\"DataType:System.String;TextAlign:LeftCenter;\";StyleFixed:\"TextAlign:CenterCente" +
                "r;\";}\t";
            this.flex1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flex1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flex1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.flex1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            this.flex1.Location = new System.Drawing.Point(0, 35);
            this.flex1.Margin = new System.Windows.Forms.Padding(2);
            this.flex1.Name = "flex1";
            this.flex1.Rows.Count = 5;
            this.flex1.Rows.DefaultSize = 18;
            this.flex1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.RowRange;
            this.flex1.ShowSort = false;
            this.flex1.Size = new System.Drawing.Size(416, 121);
            this.flex1.TabIndex = 7;
            this.flex1.Tree.Column = 1;
            this.flex1.Tree.Style = ((C1.Win.C1FlexGrid.TreeStyleFlags)(((C1.Win.C1FlexGrid.TreeStyleFlags.Lines | C1.Win.C1FlexGrid.TreeStyleFlags.Symbols)
                        | C1.Win.C1FlexGrid.TreeStyleFlags.Leaf)));
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(416, 35);
            this.label2.TabIndex = 10;
            this.label2.Text = "选择要进行回归测试的版本";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // wizardPage3
            // 
            this.wizardPage3.CaptionTitle = "(Page Title)";
            this.wizardPage3.Controls.Add(this.label4);
            this.wizardPage3.FullPage = false;
            this.wizardPage3.Location = new System.Drawing.Point(0, 0);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.Selected = false;
            this.wizardPage3.Size = new System.Drawing.Size(416, 156);
            this.wizardPage3.SubTitle = "(完成)";
            this.wizardPage3.TabIndex = 2;
            this.wizardPage3.Title = "完成";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(53, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(522, 186);
            this.label4.TabIndex = 0;
            this.label4.Text = "    创建新的回归版本成功。现在新的回归版本处于回归准备状态，用户可以在这里设置上一版本的问题处置结果，需求变更影响的测试依据。\r\n\r\n    在做好准备工作后" +
                "，点击工具栏上的进入回归流程后，正式进入回归测试阶段。软件将自动根据影响的测试依据，提取出需要进行回归测试的测试用例。用户可以自行对用例集合进行添加或者删除。";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NewVersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 408);
            this.Name = "NewVersionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "创建回归测试项目";
            this.Load += new System.EventHandler(this.NewVersionForm_Load);
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.wizardPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).EndInit();
            this.wizardPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Crownwood.Magic.Controls.WizardPage wizardPage2;
        private Crownwood.Magic.Controls.WizardPage wizardPage1;
        private Common.CustomMergeFlex flex1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbVersionMemo;
        private System.Windows.Forms.TextBox tbVersionName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private Crownwood.Magic.Controls.WizardPage wizardPage3;
        private System.Windows.Forms.Label label4;
    }
}