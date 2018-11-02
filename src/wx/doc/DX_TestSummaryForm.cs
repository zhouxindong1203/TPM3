using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// 定型：测评过程概述
    /// </summary>
    [TypeNameMap("wx.DX_TestSummaryForm")]
    public partial class DX_TestSummaryForm : MyBaseForm
    {
        /// <summary>
        /// 分别代表版本不相关与版本相关的节点
        /// </summary>
        List<_DocNode> nodeList1 = new List<_DocNode>(), nodeList2 = new List<_DocNode>();

        public DX_TestSummaryForm()
        {
            InitializeComponent();

            nodeList1.Add(new _DocNode("测评过程概述"));
            nodeList1.Add(new _DocNode("测试需求分析和测试筹划"));
            nodeList1.Add(new _DocNode("测试设计和实现"));
            nodeList1.Add(new _DocNode("测试方法说明"));
            nodeList1.Add(new _DocNode("测试有效性说明"));

            nodeList2.Add(new _DocNode("测试执行情况说明", "可变章节_测试执行情况说明") { IsVersion = true });
            nodeList2.Add(new _DocNode("问题解决情况说明") { IsVersion = true });
        }

        class _DocNode
        {
            public string title, content;
            public bool IsVersion = false;

            public _DocNode(string _title, string _content = null)
            {
                title = _title;
                content = _content ?? _title;
            }
        }

        void DX_TestSummaryForm_Load(object sender, EventArgs e)
        {
            foreach(_DocNode dn in nodeList1)
            {
                TreeNode tn = treeView1.Nodes.Add(dn.title);
                tn.Tag = dn;
                tn.ImageKey = tn.SelectedImageKey = "word";
            }

            DataTable dt = DBLayer1.GetProjectVersionList(dbProject, pid, true);
            foreach(DataRow dr in dt.Rows)
            {
                object vid = dr["ID"];
                string s = dr["版本名称"].ToString();
                if(GridAssist.IsNull(dr["前向版本ID"])) s += "(初始版本)";

                TreeNode tnVersion = treeView1.Nodes.Add(s);
                tnVersion.Tag = vid;
                tnVersion.ImageKey = tnVersion.SelectedImageKey = "ver";

                foreach(_DocNode dn in nodeList2)
                {
                    TreeNode tn = tnVersion.Nodes.Add(dn.title);
                    tn.Tag = dn;
                    tn.ImageKey = tn.SelectedImageKey = "word";
                }
            }
            treeView1.ExpandAll();
            OnDisplayNode(treeView1.Nodes[0]);
        }

        void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OnDisplayNode(e.Node);
        }

        TreeNode lastNode = null;

        void OnDisplayNode(TreeNode tn)
        {
            if(!(tn.Tag is _DocNode)) return;  // 不是文档节点
            OnSaveNode(lastNode);    // 先保存上一个节点

            _DocNode dn = tn.Tag as _DocNode;
            object vid = dn.IsVersion ? tn.Parent.Tag : null;
            byte[] buf = ProjectInfo.GetDocContent(dbProject, pid, vid, null, dn.content);
            rich1.SetRichData(buf);
            string tip = dn.IsVersion ? " (与版本相关)" : " (与版本无关)";
            lbTitle.Text = dn.title + tip;
            lastNode = tn;
        }

        void OnSaveNode(TreeNode tn)
        {
            if(tn == null) return;
            _DocNode dn = tn.Tag as _DocNode;
            object vid = dn.IsVersion ? tn.Parent.Tag : null;
            ProjectInfo.SetDocContent(dbProject, pid, vid, null, dn.content, rich1.GetRichData());
        }

        public override bool OnPageClose(bool bClose)
        {
            OnSaveNode(lastNode);
            return true;
        }
    }
}