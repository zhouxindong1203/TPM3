using System;
using System.Windows.Forms;
using Common;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// ���Խ������������7����ҳ�棬�������������Խ��
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

            testObjectInfoControl1.fieldName = "����ִ�����_����";
            testObjectInfoControl1.title = "����ִ���������˵��";
            testObjectInfoControl2.fieldName = "��������";
            testObjectInfoControl2.title = "�Ա���������������";
            testObjectInfoControl3.fieldName = "�Ľ�����";
            testObjectInfoControl3.title = "�Ա������ĸĽ�����";

            TreeNode root = treeView1.Nodes.Add("��Ŀ");
            foreach( ItemNodeTree item in summary.childlist )
            {
                int index = 0;
                TreeNode tn = root.Nodes.Add("", item.dr["�����������"] as string, "����", "����");
                tn.Nodes.Add("", "�������������ִ�и���", "ͳ��", "ͳ��").Tag = index++;
                tn.Nodes.Add("", "��������ִ�������ִ�н��", "ͳ��", "ͳ��").Tag = index++;
                tn.Nodes.Add("", "δ����ִ�еĲ�������", "ͳ��", "ͳ��").Tag = index++;
                tn.Nodes.Add("", "����ִ���������˵��", "word", "word").Tag = index++;
                tn.Nodes.Add("", "��������", "word", "word").Tag = index++;
                tn.Nodes.Add("", "�Ľ�����", "word", "word").Tag = index++;
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