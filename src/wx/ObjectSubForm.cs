using System;
using System.Windows.Forms;
using System.Xml;
using Common;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// 测试对象树窗口，用来包括子对象
    /// </summary>
    [TypeNameMap("wx.ObjectSubForm")]
    public partial class ObjectSubForm : MyBaseForm
    {
        public TestResultSummary summary;
        MyUserControl subform;

        public ObjectSubForm()
        {
            InitializeComponent();
        }

        void UnExecCaseForm_Load(object sender, EventArgs e)
        {
            summary = new TestResultSummary(pid, currentvid);
            XmlElement ele = docTN.nodeElement;
            string sub = ele.GetAttribute("subform");
            string addroot = ele.GetAttribute("AddRoot");

            subform = FormClass.CreateClass(sub) as MyUserControl;
            subform.Dock = DockStyle.Fill;
            subform.Visible = true;
            this.Controls.Add(subform);
            this.Controls.SetChildIndex(subform, 0);

            summary.OnCreate();
            subform.summary = summary;

            TreeNodeCollection parent = treeView1.Nodes;

            if( addroot.ToLower() != "false" )
            {
                TreeNode root = new TreeNode(ele.GetAttribute("NodeName"));
                root.ImageKey = "unexec";
                root.SelectedImageKey = "unexec";
                root.Tag = null;
                treeView1.Nodes.Add(root);
                parent = root.Nodes;
            }

            foreach( ItemNodeTree item in summary.childlist )
            {
                TreeNode tn2 = new TreeNode(item.dr["被测对象名称"] as string);
                tn2.ImageKey = "obj";
                tn2.SelectedImageKey = "obj";
                tn2.Tag = item.id;
                parent.Add(tn2);
            }
            treeView1.ExpandAll();
            if( treeView1.Nodes.Count > 0 )
                treeView1.SelectedNode = treeView1.Nodes[0];
        }

        public override bool OnPageClose(bool bClose)
        {
            return subform.OnPageClose(true);
        }

        void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if( !OnPageClose(true) )
            {
                e.Cancel = true;
                return;
            }
            summary.OnCreate();
            subform.id = e.Node.Tag;
            subform.OnPageCreate();
        }
    }
}