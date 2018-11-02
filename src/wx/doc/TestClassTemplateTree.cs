using System.Data;
using System.Windows.Forms;
using Common;
using TPM3.Sys;

namespace TPM3.wx
{
    [TypeNameMap("wx.TestClassTemplateTree")]
    public partial class TestClassTemplateTree : LeftTreeUserControl
    {
        public static ColumnPropList columnList2 = GridAssist.GetColumnPropList<TestClassTemplateTree>();
        const string sqlClass = "select 测试能力ID, 测试能力名称, 简写码, 父节点ID from DC测试类型模板表 order by 序号";

        protected DataTable dtTable;

        public TestClassTemplateTree()
        {
            InitializeComponent();
            treeView1.BeforeSelect += treeView1_BeforeSelect;
        }

        static TestClassTemplateTree()
        {
            columnList2.Add("测试能力名称", 180);
            columnList2.Add("简写码", 70);
        }

        public override bool OnPageCreate()
        {
            dtTable = dbProject.ExecuteDataTable(sqlClass);
            TreeNode root = treeView1.Nodes.Add("定制项目测试类型术语");
            root.Tag = "0";
            InitTree(root.Nodes, root.Tag);
            treeView1.SelectedNode = treeView1.Nodes[0];
            treeView1.ExpandAll();
            return true;
        }

        /// <summary>
        /// 建树的基本思想是：从根节点开始递归调用显示子树
        /// </summary>
        private void InitTree(TreeNodeCollection tnc, object parentID)
        {
            foreach(DataRow dr in dtTable.Rows)
            {
                if(!Equals(dr["父节点ID"], parentID)) continue;
                TreeNode tn = tnc.Add(dr["测试能力名称"].ToString());
                tn.Tag = dr["测试能力ID"];
                InitTree(tn.Nodes, tn.Tag);
            }
        }

        public override Control GetSubForm(TreeNode tnx)
        {
            return new TestClassTemplateList();
        }

        public override void OnShowForm(TreeNode tn, Form f)
        {
            f.TopLevel = false;
            panel1.Controls.Clear();
            panel1.Controls.Add(f);
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            f.Visible = true;
        }

        public override bool OnPageClose(bool bClose)
        {
            return CloseCurrentForm(true, true);
        }
    }
}