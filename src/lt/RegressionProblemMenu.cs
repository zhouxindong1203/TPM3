using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using Common.Database;
using TPM3.wx;

namespace TPM3.lt
{
    public partial class RegressionProblemMenu : Form
    {
        DataTable testCaseTable, testCaseTablePlb;
        DBAccess dbProject;
        object currentvid, pid;
        RegressionTestInfluenceForm TestInfluenceForm;
        String softwareChangeID;//更动项ID
        String testCaseName;
        String plbID;
        object ID;
        public RegressionProblemMenu(DataTable testCaseTable,DataTable testCaseTablePlb, DBAccess dbProject,
             String softwareChangeID, object currentvid, object pid, RegressionTestInfluenceForm TestInfluenceForm)
        {
            InitializeComponent();
            this.testCaseTable = testCaseTable;
            this.testCaseTablePlb = testCaseTablePlb;
            this.dbProject = dbProject;
            this.softwareChangeID = softwareChangeID;
            this.currentvid = currentvid;
            this.pid = pid;
            this.TestInfluenceForm = TestInfluenceForm;
        }
        public void SetPlbID(String plbID)
        {
            this.plbID = plbID;
        }
        public void SetSelectTestCaseName(String testCaseName, object ID)
        {
            this.testCaseName = testCaseName;
            this.ID = ID;
        }
        private void 影响域分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TestInfluenceForm.AssertPlb(softwareChangeID))
            {
                return;
            }
            RegressionTestDesignForm f = new RegressionTestDesignForm(testCaseTable, testCaseTablePlb, dbProject, softwareChangeID, currentvid, pid);
            DialogResult r = f.ShowDialog();
            if (r == DialogResult.OK)
            {
                TestInfluenceForm.InvalidateForm1(f.GetTestCaseTable(), softwareChangeID);
            }
        }

        private void 添加问题报告单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProblemResultDlg f = new ProblemResultDlg();
            DataTable dt = TestInfluenceForm.GetDataTable(softwareChangeID);
            f.SetSelectTag(dt);
            DialogResult r = f.ShowDialog();
            if (r == DialogResult.OK)
            {
                TestInfluenceForm.InvalidateForm(f.GetClassViewDataTable(), softwareChangeID);
            }

        }

 /*       private void 删除该测试用例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认删除该测试用例！", "删除测试用例", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                TestInfluenceForm.DeleteTestCaseFromBase(softwareChangeID, testCaseName, ID);
            }
        }*/

        //添加非测试问题
        private void 软件扩展功能ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoneTestPlbName plbName = new NoneTestPlbName(dbProject,softwareChangeID, currentvid, pid);
            DialogResult r = plbName.ShowDialog();
            if (r == DialogResult.OK)
            {
                TestInfluenceForm.OnPageClose(true);
                TestInfluenceForm.OnPageCreate();
            }

        }

        private void 添加更动项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestInfluenceForm.AddChangeItem(softwareChangeID);
        }

        private void 删除更动项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestInfluenceForm.DeleteChangeItem(softwareChangeID);
        }

        private void 删除非测试问题ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sqlRegressionTest1 = "SELECT * from HG回归测试问题表 where 软件问题 = ? and 更动项ID = ? and 软件问题类型 = ? and 测试版本 = ? and 项目ID = ? ";
            DataTable dt2 = dbProject.ExecuteDataTable(sqlRegressionTest1, plbID, softwareChangeID, "非测试问题", currentvid, pid);
            if (dt2.Rows.Count <= 0)
            {
                MessageBox.Show("该问题不属于非测试问题！");
                return;
            }
            dbProject.ExecuteNoQuery("delete from HG回归测试问题表 where 软件问题 = ? and 更动项ID = ? and 软件问题类型 = ? and 测试版本 = ? and 项目ID = ? ",
                plbID, softwareChangeID, "非测试问题", currentvid, pid);
            TestInfluenceForm.OnPageClose(true);
            TestInfluenceForm.OnPageCreate();
        }

        private void 删除该测试用例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认删除该更动报告单！", "删除更动报告单", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                TestInfluenceForm.DeleteChangeReport(softwareChangeID);//软件更动单ID
            }

        }

    }
}
