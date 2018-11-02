using System;
using System.Windows.Forms;
using Common;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// 测试结果概述，包括7个子页面，按对象描述测试结果
    /// </summary>
    [TypeNameMap("wx.TestResultForm")]
    public partial class TestResultForm : LeftTreeUserControl
    {
        public TestResultForm()
        {
            InitializeComponent();
            treeView1.BeforeSelect += treeView1_BeforeSelect;
            AddUserControl(testObjectSummary1,
                testCaseResultSummary1, unExecCase1,
                testObjectInfoControl1, testObjectInfoControl2,
                testObjectInfoControl3, testProjectSummary1);
        }

        MyUserControl[] controllist;
        void AddUserControl(params MyUserControl[] controllist)
        {
            foreach( Control c in controllist )
            {
                c.Visible = false;
                c.Dock = DockStyle.Fill;
            }
            this.controllist = controllist;
        }

        TestResultSummary summary;
        void TestResultForm_Load(object sender, EventArgs e)
        {
            summary = new TestResultSummary(pid, currentvid);
            summary.OnCreate();
            testProjectSummary1.summary = summary;
            testObjectSummary1.summary = summary;
            unExecCase1.summary = summary;
            testCaseResultSummary1.summary = summary;

            testObjectInfoControl1.fieldName = "测试执行情况_补充";
            testObjectInfoControl1.title = "测试执行情况补充说明";
            testObjectInfoControl2.fieldName = "质量评估";
            testObjectInfoControl2.title = "对被测对象的质量评估";
            testObjectInfoControl3.fieldName = "改进建议";
            testObjectInfoControl3.title = "对被测对象的改进建议";

            TreeNode root = treeView1.Nodes.Add("项目");
            foreach( ItemNodeTree item in summary.childlist )
            {
                int index = 0;
                TreeNode tn = root.Nodes.Add("", item.dr["被测对象名称"] as string, "对象", "对象");
                tn.Nodes.Add("", "测试用例设计与执行概述", "统计", "统计").Tag = index++;
                tn.Nodes.Add("", "测试用例执行情况与执行结果", "统计", "统计").Tag = index++;
                tn.Nodes.Add("", "未完整执行的测试用例", "统计", "统计").Tag = index++;
                tn.Nodes.Add("", "测试执行情况补充说明", "word", "word").Tag = index++;
                tn.Nodes.Add("", "质量评估", "word", "word").Tag = index++;
                tn.Nodes.Add("", "改进建议", "word", "word").Tag = index++;
                tn.Tag = item.id;
            }
            treeView1.SelectedNode = root;
            treeView1.ExpandAll();
        }

        public override Control GetSubForm(TreeNode tn)
        {
            if( tn.Level == 0 )
            {
                return testProjectSummary1;
            }
            else if( tn.Level == 1 )
            {
                testObjectSummary1.id = tn.Tag;
                return testObjectSummary1;
            }
            else
            {
                int index = (int)tn.Tag;
                MyUserControl uc = controllist[index];
                uc.id = tn.Parent.Tag;
                return uc;
            }
        }

        public override void OnShowControl(TreeNode tn, Control c)
        {
            c.Show();
        }
    }
}