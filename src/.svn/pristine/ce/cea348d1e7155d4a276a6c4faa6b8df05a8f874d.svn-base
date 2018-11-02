using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TPM3.Sys;
using Z1.tpm;

namespace TPM3.zxd.clu
{
    public partial class FindUsecaseResultForm : Form
    {
        private List<TreeNode> _nodes;
        public FindUsecaseResultForm(List<TreeNode> nodes)
        {
            InitializeComponent();

            _nodes = nodes;
            FillListBox();
            this.listBox1.SelectedIndex = 0;
        }

        private void FillListBox()
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                NodeTagInfo tag = _nodes[i].Tag as NodeTagInfo;
                if (tag != null)
                    this.listBox1.Items.Add(tag.text);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MainForm.mainFrm.DlgRefFindResult = null;
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.listBox1.SelectedIndex;
            if (index == -1)
                return;

            if ((index < 0) ||
                (index >= _nodes.Count))
                return;

            // 获取相关数据
            TreeNode tn = _nodes[index];
            if ((tn == null) ||
                (tn.GetType() != typeof(TreeNode)))
                return;

            TestTreeForm ttf = MainForm.mainFrm.CurrentSelectedForm as TestTreeForm;
            if (ttf == null)
            {
                Close();
                return;
            }

            ttf.tree.SelectedNode = tn;
            tn.EnsureVisible();
        }
    }
}