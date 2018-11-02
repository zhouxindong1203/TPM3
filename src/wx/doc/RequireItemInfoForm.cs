using System;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Common;

namespace TPM3.wx
{
    /// <summary>
    /// 需求章节号与文档标识生成
    /// </summary>
    public partial class RequireItemInfoForm : Form
    {
        /// <summary>
        /// 代表需求行
        /// </summary>
        public Row dr;

        /// <summary>
        /// [任务分配单]时间区间设置
        /// </summary>
        public RequireItemInfoForm(Row drRequire, Rectangle parentPos)
        {
            InitializeComponent();
            this.dr = drRequire;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            // 定位
            MultiColumnDropDown.SetLocation(this, parentPos);

            string msg;
            textBox1.Text = dr["测试依据"].ToString();
            if(dr.Node.Level == 0)  // 文档
            {
                label2.Text = "文件标识";
                textBox2.Text = dr["测试依据标识"].ToString();
                msg = @"仅对测试条款所在的文档有效，代表文件的标识号，测试条款项不需要手工填写。
<br/>最终的测试条款标识为：<b>文件标识_章节号</b>。<br/>
例如：文件标识为Req，测试条款章节号为2.1.2.1，则测试条款标识为<b>Req_2.1.2.1</b>";
            }
            else
            {
                label2.Text = "章节号";
                textBox2.Text = dr["章节号"].ToString();
                msg = @"测试条款文档没有章节号，仅有测试条款项有章节号。
<br/>最终的测试条款标识为：<b>文档标识_章节号</b>。<br/>
例如：文档标识为Req，测试条款章节号为2.1.2.1，则测试条款标识为<b>Req_2.1.2.1</b>";
            }
            this.C1SuperTooltip1.SetToolTip(label2, msg);
            this.C1SuperTooltip1.SetToolTip(textBox2, msg);
        }

        void RequireItemInfoForm_Deactivate(object sender, EventArgs e)
        {
            Accept(0);
        }

        void RequireItemInfoForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Escape:	// cancel editing on escape key
                    e.Handled = true;
                    Accept(-1);
                    break;
            }
        }

        bool bClosed = false;

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="row">0: 确认，-1: 取消</param>
        private void Accept(int row)
        {
            if(bClosed) return;

            if(row == -1) // 取消
            {
                this.DialogResult = DialogResult.Cancel;   // 确认选择
            }
            else
            {
                string s = GetValue();
                if(s != null)
                {   // 出错
                    MessageBox.Show(s);
                    return;
                }
                this.DialogResult = DialogResult.OK;
            }
            bClosed = true;
            dr.Grid.Invalidate();  // 刷新网格控件
            this.Close();
        }

        string GetValue()
        {
            string s = textBox2.Text;
            if(GridAssist.IsNull(textBox1.Text)) return "条款名称不能为空";
            if(GridAssist.IsNull(s)) return "条款章节号或者文件标识不能为空";
            dr["测试依据"] = textBox1.Text;

            // 如果章节号不含点，则自动为上一级章节号+当前章节号
            //if(dr.Node.Level >= 2 && !s.Contains(".") && !s.Contains("_"))
            //{
            //    var drParent = dr.Node.GetNode(NodeTypeEnum.Parent).Row;
            //    s = drParent["章节号"] + "_" + s;
            //}

            if(dr.Node.Level == 0)  // 文档
                dr["测试依据标识"] = s;
            else
                dr["章节号"] = s;

            return null;
        }

        void RequireItemInfoForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, Size.Width - 2, Size.Height - 2);
        }
    }
}
