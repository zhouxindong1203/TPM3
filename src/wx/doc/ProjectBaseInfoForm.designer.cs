namespace TPM3.wx
{
    partial class ProjectBaseInfoForm
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
            if (disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectBaseInfoForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb202 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb205 = new System.Windows.Forms.TextBox();
            this.tb203 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb201 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb206 = new System.Windows.Forms.TextBox();
            this.tb204 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb107 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flex1 = new Common.CustomMergeFlex();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tb110 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tb102 = new System.Windows.Forms.TextBox();
            this.tb109 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tb101 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb108 = new System.Windows.Forms.TextBox();
            this.tb104 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb103 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tb31 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb32 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tb41 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tb42 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tb106 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tb105 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb202);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tb205);
            this.groupBox1.Controls.Add(this.tb203);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tb201);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb206);
            this.groupBox1.Controls.Add(this.tb204);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(16, 226);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(965, 115);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试项目基本信息(与版本无关)";
            // 
            // cb202
            // 
            this.cb202.FormattingEnabled = true;
            this.cb202.Location = new System.Drawing.Point(563, 22);
            this.cb202.Name = "cb202";
            this.cb202.Size = new System.Drawing.Size(137, 22);
            this.cb202.TabIndex = 5;
            this.cb202.Validating += new System.ComponentModel.CancelEventHandler(this.comboBox_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(466, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 14);
            this.label8.TabIndex = 4;
            this.label8.Text = "测试文档密级";
            // 
            // tb205
            // 
            this.tb205.Location = new System.Drawing.Point(119, 80);
            this.tb205.Name = "tb205";
            this.tb205.ReadOnly = true;
            this.tb205.Size = new System.Drawing.Size(336, 23);
            this.tb205.TabIndex = 3;
            // 
            // tb203
            // 
            this.tb203.Location = new System.Drawing.Point(119, 51);
            this.tb203.Name = "tb203";
            this.tb203.Size = new System.Drawing.Size(336, 23);
            this.tb203.TabIndex = 3;
            this.tb203.TextChanged += new System.EventHandler(this.VersionSignChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "测试项目标识号";
            // 
            // tb201
            // 
            this.tb201.Location = new System.Drawing.Point(119, 22);
            this.tb201.Name = "tb201";
            this.tb201.Size = new System.Drawing.Size(336, 23);
            this.tb201.TabIndex = 1;
            this.tb201.TextChanged += new System.EventHandler(this.VersionSignChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "测试项目名称";
            // 
            // tb206
            // 
            this.tb206.Location = new System.Drawing.Point(563, 80);
            this.tb206.Name = "tb206";
            this.tb206.Size = new System.Drawing.Size(137, 23);
            this.tb206.TabIndex = 7;
            this.toolTip1.SetToolTip(this.tb206, "缺省值为R");
            this.tb206.TextChanged += new System.EventHandler(this.VersionSignChanged);
            // 
            // tb204
            // 
            this.tb204.Location = new System.Drawing.Point(564, 51);
            this.tb204.Name = "tb204";
            this.tb204.Size = new System.Drawing.Size(137, 23);
            this.tb204.TabIndex = 7;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(5, 83);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(105, 14);
            this.label22.TabIndex = 6;
            this.label22.Text = "当前版本标识号";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(466, 83);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 14);
            this.label13.TabIndex = 6;
            this.label13.Text = "回归标识前缀";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(466, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "测试单位名称";
            // 
            // tb107
            // 
            this.tb107.Location = new System.Drawing.Point(119, 122);
            this.tb107.Name = "tb107";
            this.tb107.Size = new System.Drawing.Size(220, 23);
            this.tb107.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 125);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 14);
            this.label12.TabIndex = 8;
            this.label12.Text = "开发方联系人";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.flex1);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Location = new System.Drawing.Point(16, 495);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(965, 357);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "测试文档基本信息(与版本相关)";
            // 
            // flex1
            // 
            this.flex1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            this.flex1.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;
            this.flex1.BackColor = System.Drawing.Color.White;
            this.flex1.ColumnInfo = resources.GetString("flex1.ColumnInfo");
            this.flex1.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw;
            this.flex1.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.flex1.Location = new System.Drawing.Point(16, 36);
            this.flex1.Name = "flex1";
            this.flex1.Rows.Count = 6;
            this.flex1.Rows.DefaultSize = 20;
            this.flex1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.flex1.ShowSort = false;
            this.flex1.Size = new System.Drawing.Size(930, 315);
            this.flex1.StyleInfo = resources.GetString("flex1.StyleInfo");
            this.flex1.TabIndex = 14;
            this.flex1.Text = "c1FlexGrid1";
            this.flex1.Tree.Column = 1;
            this.flex1.Tree.Indent = 10;
            this.flex1.OwnerDrawCell += new C1.Win.C1FlexGrid.OwnerDrawCellEventHandler(this.flex1_OwnerDrawCell);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label20.Location = new System.Drawing.Point(63, 19);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(560, 14);
            this.label20.TabIndex = 0;
            this.label20.Text = "文档标识号拼装方法   {0}: 当前版本标识号      {1}：测试文档版本   {2}：项目名称";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.tb110);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.tb102);
            this.groupBox2.Controls.Add(this.tb109);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.tb101);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tb105);
            this.groupBox2.Controls.Add(this.tb107);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tb106);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.tb108);
            this.groupBox2.Controls.Add(this.tb104);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tb103);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(16, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(965, 197);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测试任务下达方基本信息(与版本无关)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(353, 155);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(91, 14);
            this.label16.TabIndex = 18;
            this.label16.Text = "外场测试地址";
            // 
            // tb110
            // 
            this.tb110.Location = new System.Drawing.Point(468, 152);
            this.tb110.Name = "tb110";
            this.tb110.Size = new System.Drawing.Size(225, 23);
            this.tb110.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(353, 35);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 14);
            this.label15.TabIndex = 16;
            this.label15.Text = "委托方地址";
            // 
            // tb102
            // 
            this.tb102.Location = new System.Drawing.Point(468, 32);
            this.tb102.Name = "tb102";
            this.tb102.Size = new System.Drawing.Size(225, 23);
            this.tb102.TabIndex = 17;
            // 
            // tb109
            // 
            this.tb109.Location = new System.Drawing.Point(119, 152);
            this.tb109.Name = "tb109";
            this.tb109.Size = new System.Drawing.Size(220, 23);
            this.tb109.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 155);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 14);
            this.label14.TabIndex = 14;
            this.label14.Text = "实验室地址";
            // 
            // tb101
            // 
            this.tb101.Location = new System.Drawing.Point(119, 32);
            this.tb101.Name = "tb101";
            this.tb101.Size = new System.Drawing.Size(220, 23);
            this.tb101.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 14);
            this.label9.TabIndex = 12;
            this.label9.Text = "委托方名称";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(353, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 14);
            this.label7.TabIndex = 10;
            this.label7.Text = "开发方联系电话";
            // 
            // tb108
            // 
            this.tb108.Location = new System.Drawing.Point(468, 122);
            this.tb108.Name = "tb108";
            this.tb108.Size = new System.Drawing.Size(225, 23);
            this.tb108.TabIndex = 11;
            // 
            // tb104
            // 
            this.tb104.Location = new System.Drawing.Point(468, 62);
            this.tb104.Name = "tb104";
            this.tb104.Size = new System.Drawing.Size(224, 23);
            this.tb104.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(353, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 14);
            this.label6.TabIndex = 6;
            this.label6.Text = "委托方联系电话";
            // 
            // tb103
            // 
            this.tb103.Location = new System.Drawing.Point(119, 62);
            this.tb103.Name = "tb103";
            this.tb103.Size = new System.Drawing.Size(220, 23);
            this.tb103.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 14);
            this.label10.TabIndex = 4;
            this.label10.Text = "委托方联系人";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tb31);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.tb32);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Location = new System.Drawing.Point(16, 356);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(965, 55);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "被测软件信息(与版本相关)";
            // 
            // tb31
            // 
            this.tb31.Location = new System.Drawing.Point(119, 22);
            this.tb31.Name = "tb31";
            this.tb31.Size = new System.Drawing.Size(170, 23);
            this.tb31.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "被测软件版本";
            // 
            // tb32
            // 
            this.tb32.Location = new System.Drawing.Point(436, 22);
            this.tb32.Name = "tb32";
            this.tb32.Size = new System.Drawing.Size(170, 23);
            this.tb32.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(367, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "需求版本";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tb41);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.tb42);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Location = new System.Drawing.Point(16, 428);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(965, 55);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "测试版本信息(与版本相关)";
            // 
            // tb41
            // 
            this.tb41.Location = new System.Drawing.Point(119, 22);
            this.tb41.Name = "tb41";
            this.tb41.Size = new System.Drawing.Size(170, 23);
            this.tb41.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 14);
            this.label11.TabIndex = 8;
            this.label11.Text = "测试版本名称";
            // 
            // tb42
            // 
            this.tb42.Location = new System.Drawing.Point(436, 22);
            this.tb42.Name = "tb42";
            this.tb42.Size = new System.Drawing.Size(510, 23);
            this.tb42.TabIndex = 11;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(339, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 14);
            this.label17.TabIndex = 10;
            this.label17.Text = "测试版本说明";
            // 
            // tb106
            // 
            this.tb106.Location = new System.Drawing.Point(468, 92);
            this.tb106.Name = "tb106";
            this.tb106.Size = new System.Drawing.Size(225, 23);
            this.tb106.TabIndex = 11;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 95);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(91, 14);
            this.label18.TabIndex = 8;
            this.label18.Text = "软件开发单位";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(353, 95);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 14);
            this.label19.TabIndex = 10;
            this.label19.Text = "开发方地址";
            // 
            // tb105
            // 
            this.tb105.Location = new System.Drawing.Point(119, 92);
            this.tb105.Name = "tb105";
            this.tb105.Size = new System.Drawing.Size(220, 23);
            this.tb105.TabIndex = 9;
            // 
            // ProjectBaseInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(1000, 850);
            this.ClientSize = new System.Drawing.Size(1024, 811);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ProjectBaseInfoForm";
            this.Text = "项目基本信息";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flex1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb201;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb204;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb202;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb203;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb107;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb108;
        private System.Windows.Forms.TextBox tb104;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb103;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb206;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox tb205;
        private System.Windows.Forms.Label label22;
        private Common.CustomMergeFlex flex1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tb31;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb32;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tb110;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tb102;
        private System.Windows.Forms.TextBox tb109;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb101;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tb41;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb42;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tb105;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tb106;
    }
}