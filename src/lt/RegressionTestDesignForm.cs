using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using Crownwood.Magic.Forms;
using Crownwood.Magic.Controls;
using System.Windows.Forms;
using Common.Database;
using Common;
using TPM3.wx;
using TPM3.Sys;
using Z1.tpm;

namespace TPM3.lt
{
    public partial class RegressionTestDesignForm : Form
    {
        DataTable testCaseTable, testCaseTablePlb, testCancelReasonTable;
        DBAccess dbProject;
        String softwareChangeID;//更动项ID
        object currentvid,previd, pid;
        public static readonly string sqlRegressionTest3 = "select * from HG回归测试测试选项取消原因表 order by ID";
        public RegressionTestDesignForm(DataTable testCaseTable, DataTable testCaseTablePlb, DBAccess dbProject,
            String softwareChangeID, object currentvid, object pid)
        {
            InitializeComponent();
            treeView1.ImageList = ImageForm.treeNodeImage;
            this.testCaseTable = testCaseTable;
            this.dbProject = dbProject;
            testCancelReasonTable = dbProject.ExecuteDataTable(sqlRegressionTest3);
            this.softwareChangeID = softwareChangeID;
            this.currentvid = currentvid;
            this.previd = DBLayer1.GetPreVersion(dbProject, currentvid);
            this.pid = pid;
            this.testCaseTablePlb = testCaseTablePlb;
            InitTestCaseTreeView();

        }
        public DataTable GetTestCaseTable()
        {
            return testCaseTable;
        }
        TreeViewClass vc;
        string InitTestCaseTreeView()
        {
            treeView1.Nodes.Clear();
            vc = new TreeViewClass();
            vc.SetPara(treeView1, true, pid, currentvid, dbProject);
            InsertCurAndBeforeVersionToTree();
            treeView1.ExpandAll();
            return null;
        }
        //将当前和以前版本的测试用例、测试项、测试类型、测试对象全放到树中
        private void InsertCurAndBeforeVersionToTree()
        {
            object vid = currentvid;
            if (vid == null) return;
            TestResultSummary summary;
            summary = new TestResultSummary(pid, vid);
            summary.OnCreate();
            vc.AddTreeNode(treeView1.Nodes, summary, summary.name, testCaseTable);
            summary.DoVisit(vc.AddTreeNode);
            vid = DBLayer1.GetPreVersion(dbProject, vid);
            while (vid != null)
            {
                TestResultSummary summary1;
                summary1 = new TestResultSummary(pid, vid);
                summary1.OnCreate();
                summary1.DoVisit(vc.AddTreeNode);
                vid = DBLayer1.GetPreVersion(dbProject, vid);
            }
        }
        Dictionary<ItemNodeTree, TreeNode> treenodemap
        {
            get { return vc.treenodemap; }
        }
        bool banishCheckEvent = false;

        
        private void OK_Click(object sender, EventArgs e)
        {
            dbProject.UpdateDatabase(testCancelReasonTable,sqlRegressionTest3);
/*            if (plbID != "")
            {
                SaveTestCaseOfPlb();//更新问题报告单对应测试过程和测试用例
            }
            else
            {
                SaveTestCaseOfChange();//更新软件扩展更动对应的测试用例
            }*/
 //           this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
//            this.Close();
        }
        bool first = false;
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (cancelTestCaseCheckTag & (!first))
            {
                first = true;
                if (curCancelTreeNode != null)
                {
                    treenodemap[curCancelTreeNode].Checked = true;
                }

            }
            if (banishCheckEvent || cancelTestCaseCheckTag) return;
            TreeNode tn = e.Node;
//            tn.ForeColor = tn.Checked ? Color.Blue : Color.Black;
            if (tn.Checked)
            {   // 其所有父节点都置为选中状态
                banishCheckEvent = true;  // 不再触发其他事件
                TreeNode parent = tn.Parent;
                while (parent != null)
                {
                    parent.Checked = true;
                    parent = parent.Parent;
                }
                banishCheckEvent = false;
                AddTestCaseToTable(tn);
            }

            foreach (TreeNode child in tn.Nodes)
                child.Checked = tn.Checked;
            ItemNodeTree item = tn.Tag as ItemNodeTree;

            if (item.nodeType != NodeType.TestCase) return;
            if (tn.Checked && item.IsShortCut)
            {   // 如果是快捷方式，则把对应实体也置为选中状态
                foreach (ItemNodeTree sub in treenodemap.Keys)
                {
                    if (sub.nodeType != NodeType.TestCase) continue;
                    if (sub.IsShortCut) continue;
                    if (!Equals(sub.id, item.id)) continue;
                    treenodemap[sub].Checked = true;
                }
            }
            else if (!tn.Checked && !item.IsShortCut)
            {  // 如果是实体，则把所有对应快捷方式都置未选中状态
                foreach (ItemNodeTree sub in treenodemap.Keys)
                {
                    if (sub.nodeType != NodeType.TestCase) continue;
                    if (!sub.IsShortCut) continue;
                    if (!Equals(sub.id, item.id)) continue;
                    treenodemap[sub].Checked = false;
                }
            }
        }
        bool cancelTestCaseCheckTag = false;
        bool panelTag = false;
        ItemNodeTree curCancelTreeNode;
        bool passTestCaseCheckTag = false;
        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (addTestCaseButtonTag)
            {
                MessageBox.Show("请按取消键，取消添加测试内容！");
                e.Cancel = true;
                return;
            }
            if (DeleteTestCaseTag)
            {
                this.Controls.Remove(this.DeleteTestCase);
                DeleteTestCaseTag = false;
            }
            if (AddTestCaseTag)
            {
                this.Controls.Remove(this.AddTestCase);
                AddTestCaseTag = false;
            }
            if (banishCheckEvent || passTestCaseCheckTag) return;
            TreeNode tn = e.Node;
            object ID = ((ItemNodeTree)tn.Tag).id;
            if (tn.Checked && ID != null)
            {
                if (vc.ConsiderCheckedInTestOfPlb(ID))
                {
                    curCancelTreeNode = (ItemNodeTree)tn.Tag;
                    cancelTestCaseCheckTag = true;
                    first = false;
                    if (!panelTag)
                    {
                        this.Controls.Add(this.panel1);
                        panelTag = true;
                    }
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cancelTestCaseCheckTag = false;
            curCancelTreeNode = null;
            this.Controls.Remove(this.panel1);
            panelTag = false;
            first = false;
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
               this.Controls.Remove(this.panel1);
                panelTag = false;
                cancelTestCaseCheckTag = false;
                first = false; 
                if (curCancelTreeNode != null)
                {
 /*                    passTestCaseCheckTag = true;
                    treenodemap[curCancelTreeNode].Checked = false;
                    passTestCaseCheckTag = false;
                    //将取消原因放入testCancelReasonTable表中
                    DataColumnCollection dcc = testCancelReasonTable.Columns;
                    dcc["ID"].DefaultValue = FunctionClass.NewGuid;
                    dcc["测试ID"].DefaultValue = curCancelTreeNode.id;
                    dcc["测试类型"].DefaultValue = curCancelTreeNode.nodeType;
                    dcc["问题报告单ID"].DefaultValue = plbID;
                    dcc["项目ID"].DefaultValue = pid;
                    dcc["取消原因"].DefaultValue = textBox1.Text;
                    testCancelReasonTable.Rows.Add();
                    //从相应的表中删除
                    vc.DeleteCheckedInTestOfPlb(curCancelTreeNode.id);
                    vc.DeleteTestCaseTable(curCancelTreeNode.id);*/
                }
                textBox1.Text = "";
                curCancelTreeNode = null;
            }
            else 
            {
                MessageBox.Show("原因不能为空！");
            }

        }
        private void SaveTestCaseOfChange()
        {
            foreach (DataRow drt in testCaseTable.Rows)
            {

                if (drt.RowState == DataRowState.Added)
                {
                    DataRow dr = dbProject.ExecuteDataRow("SELECT * FROM CA测试用例实测表 WHERE 测试用例ID = ? and 项目ID = ? and 测试版本 = ?;", drt["测试用例ID"], drt["项目ID"], drt["测试版本"]);
                    if (dr == null)
                    {
                        //为测试用例实测表添加新条目
                        dbProject.ExecuteNoQuery("insert into CA测试用例实测表(ID, 项目ID, 测试用例ID, 执行状态, 测试版本) values(?, ?, ?, ?, ?)",
                            drt["ID"], drt["项目ID"], drt["测试用例ID"], "未执行", drt["测试版本"]);
                        //添加该轮次的测试项、测试类型、测试对象
                        AddTestCaseParentToTable(drt["测试用例ID"], drt["ID"],true);
                    }
                }
            }
        
        }
        private void SaveTestCaseOfPlb()
        {
            foreach (DataRow drt in testCaseTable.Rows)
            {
                DataRow dr = dbProject.ExecuteDataRow("SELECT * FROM CA测试用例实测表 WHERE 测试用例ID = ? and 项目ID = ? and 测试版本 = ?;", drt["测试用例ID"], drt["项目ID"], drt["测试版本"]);
                if (dr == null)
                {
                    //为测试用例实测表添加新条目
                    drt["ID"] = FunctionClass.NewGuid;
                    dbProject.ExecuteNoQuery("insert into CA测试用例实测表(ID, 项目ID, 执行状态, 测试用例ID, 测试版本) values(?, ?, ?, ?,  ?)",
                        drt["ID"], drt["项目ID"], "未执行",drt["测试用例ID"], drt["测试版本"]);
                    //添加该轮次的测试项、测试类型、测试对象
                    AddTestCaseParentToTable(drt["测试用例ID"], drt["ID"], true);
                }
            }
        }
        public void AddTestContenteParentToTable(object testCaseID, object testCaseReallyID, NodeType nt, ItemNodeTree item)
        {
            ItemNodeTree itemItem = null, typeItem = null, objectItem = null;
            switch(nt)
            {
                case NodeType.TestItem:
                    itemItem = item;
                    typeItem = item.GetLeastParent(NodeType.TestType);
                    objectItem = item.GetLeastParent(NodeType.TestObject);
                    break;
                case NodeType.TestType:
                    typeItem = item;
                    objectItem = item.GetLeastParent(NodeType.TestObject);
                    break;
                case NodeType.TestObject:
                    objectItem = item;
                    break;
                default:
                    break;
            }

            DataRow drt;            
            //注意子测试项的处理？
            if (nt == NodeType.TestItem)
            {
                DataRow dr = dbProject.ExecuteDataRow("SELECT * FROM CA测试项实测表 WHERE 测试项ID = ? and 项目ID = ? and 测试版本 = ?;", itemItem.id, pid, currentvid);
                if (dr == null)
                {
                    //为测试用例实测表添加新条目
                    drt = dbProject.ExecuteDataRow("SELECT * FROM CA测试项实体表 WHERE ID = ? and 项目ID = ? and 所属测试类型ID = ?;", itemItem.id, pid, typeItem.id);
                    if (drt == null)
                    {
                        return;
                    }
                    object itemID = FunctionClass.NewGuid;
                    dbProject.ExecuteNoQuery("insert into CA测试项实测表(ID, 测试项ID, 序号, 项目ID, 测试版本) values(?, ?, ?, ?, ?)",
                        FunctionClass.NewGuid, itemItem.id, typeItem.childlist.Count+1, pid, currentvid);
                    //注意测试项与测试用例关系表的处理()
                    SetItemCaseRelativeTable(itemID, testCaseReallyID);
                }
                else
                {
                    SetItemCaseRelativeTable(dr["ID"], testCaseReallyID);
                    return;
                }
                nt = NodeType.TestType;
            }
            if(nt == NodeType.TestType)
            {
                ////注意子测试类型的处理？
                DataRow dr = dbProject.ExecuteDataRow("SELECT * FROM CA测试类型实测表 WHERE 测试类型ID = ? and 项目ID = ? and 测试版本 = ?;", typeItem.id, pid, currentvid);
                if (dr == null)
                {
                    //为测试用例实测表添加新条目
                    drt = dbProject.ExecuteDataRow("SELECT * FROM CA测试类型实体表 WHERE ID = ? and 项目ID = ? and 所属被测对象ID = ?;", typeItem.id, pid, objectItem.id);
                    if(drt == null)
                    {
                        return;
                    }
                    dbProject.ExecuteNoQuery("insert into CA测试类型实测表(ID, 测试类型ID, 序号, 项目ID, 测试版本) values(?, ?, ?, ?, ?)",
                        FunctionClass.NewGuid, typeItem.id, objectItem.childlist.Count+1, pid, currentvid);
                }
                else
                { 
                    return; 
                }
                nt = NodeType.TestObject;
            }
            if (nt == NodeType.TestObject)
            {
                DataRow dr = dbProject.ExecuteDataRow("SELECT * FROM CA被测对象实测表 WHERE 被测对象ID = ? and 项目ID = ? and 测试版本 = ?;", objectItem.id, pid, currentvid);
                if (dr == null)
                {
                    //为测试用例实测表添加新条目
                    drt = dbProject.ExecuteDataRow("SELECT * FROM CA被测对象实测表 WHERE 项目ID = ? and 测试版本 = ?;", pid, currentvid);
                    if (drt == null)
                    {
                        return;
                    }
                    dbProject.ExecuteNoQuery("insert into CA被测对象实测表(ID, 被测对象ID, 序号, 项目ID, 测试版本) values(?, ?, ?, ?, ?)",
                        FunctionClass.NewGuid, objectItem.id, objectItem.parent.childlist.Count+1, pid, currentvid);
                }
                else
                {
                    return;
                }
            }

        }
        //把该轮次的测试用例、测试项、测试类型、测试对象实测表添加进去;
        //；不涉及实体表，实体表的的操作主要由在treeviw添加删除操作，这里主要操作实测表，
        public void AddTestCaseParentToTable(object testCaseID, object testCaseReallyID, bool testCaseAdded)
        {
            ItemNodeTree item = null, itemItem = null, typeItem = null, objectItem = null;

            bool tag = GetItemAndParent(testCaseID, ref item, ref itemItem, ref typeItem, ref objectItem);
            if (!tag) return;
            DataRow drt;
            object caseID, itemID;
            if (!testCaseAdded)
            {
                drt = dbProject.ExecuteDataRow("SELECT * FROM CA测试用例实测表 WHERE 测试用例ID = ? and 项目ID = ? and 测试版本 = ?;", item.id, pid, currentvid);
                if (drt == null)
                {
                    //为测试用例实测表添加新条目
                    caseID = FunctionClass.NewGuid;
                    dbProject.ExecuteNoQuery("insert into CA测试用例实测表(ID, 项目ID, 执行状态, 测试用例ID, 测试版本) values(?, ?, 未执行, ?, ?)",
                        caseID, pid, item.id, currentvid);
                }
                else
                {
                    return;
                }
            }
            else 
            {
                caseID = testCaseReallyID;
            }
            
            //注意子测试项的处理？
            DataRow dr = dbProject.ExecuteDataRow("SELECT * FROM CA测试项实测表 WHERE 测试项ID = ? and 项目ID = ? and 测试版本 = ?;", itemItem.id, pid, currentvid);
            if (dr == null)
            {
                //为测试用例实测表添加新条目
                drt = dbProject.ExecuteDataRow("SELECT * FROM CA测试项实体表 WHERE ID = ? and 项目ID = ? and 所属测试类型ID = ?;", itemItem.id, pid, typeItem.id);
                if(drt==null)
                {
                    return;
                }
                itemID = FunctionClass.NewGuid;
                dbProject.ExecuteNoQuery("insert into CA测试项实测表(ID, 测试项ID, 序号, 项目ID, 测试版本) values(?, ?, ?, ?, ?)",
                    FunctionClass.NewGuid, itemItem.id, typeItem.childlist.Count+1, pid, currentvid);
                //注意测试项与测试用例关系表的处理()
                SetItemCaseRelativeTable(itemID, caseID);
            }
            else
            {
                SetItemCaseRelativeTable(dr["ID"], caseID);
                return; 
            }
            ////注意子测试类型的处理？
            dr = dbProject.ExecuteDataRow("SELECT * FROM CA测试类型实测表 WHERE 测试类型ID = ? and 项目ID = ? and 测试版本 = ?;", typeItem.id, pid, currentvid);
            if (dr == null)
            {
                //为测试用例实测表添加新条目
                drt = dbProject.ExecuteDataRow("SELECT * FROM CA测试类型实体表 WHERE ID = ? and 项目ID = ? and 所属被测对象ID = ?;", typeItem.id, pid, objectItem.id);
                if(drt == null)
                {
                    return;
                }
                dbProject.ExecuteNoQuery("insert into CA测试类型实测表(ID, 测试类型ID, 序号, 项目ID, 测试版本) values(?, ?, ?, ?, ?)",
                    FunctionClass.NewGuid, typeItem.id, objectItem.childlist.Count+1, pid, currentvid);
            }
            else
            { 
                return; 
            }
            dr = dbProject.ExecuteDataRow("SELECT * FROM CA被测对象实测表 WHERE 被测对象ID = ? and 项目ID = ? and 测试版本 = ?;", objectItem.id, pid, currentvid);
            if (dr == null)
            {
                //为测试用例实测表添加新条目
                drt = dbProject.ExecuteDataRow("SELECT * FROM CA被测对象实测表 WHERE 项目ID = ? and 测试版本 = ?;", pid, currentvid);
                if(drt == null)
                {
                    return;
                }
                dbProject.ExecuteNoQuery("insert into CA被测对象实测表(ID, 被测对象ID, 序号, 项目ID, 测试版本) values(?, ?, ?, ?, ?)",
                    FunctionClass.NewGuid, objectItem.id, objectItem.parent.childlist.Count+1, pid, currentvid);
            }
            else
            { 
                return; 
            }

        }
        public TreeNode GetItemCase(object testCaseID, TreeNode tn)
        {
            TreeNode temptn;
            foreach (TreeNode tr in tn.Nodes)
            {
                if (((ItemNodeTree)tr.Tag).id == testCaseID && ((ItemNodeTree)tr.Tag).nodeType == NodeType.TestCase)
                {
                    return tr;
                }
                temptn = GetItemCase(testCaseID, tr);
                if (temptn != null)
                {
                    return temptn;
                }
            }
            return null;

        }
        //从treeView中找到测试用例和各个父版本
        public bool GetItemAndParent(object testCaseID, ref ItemNodeTree item, ref ItemNodeTree itemItem, ref ItemNodeTree typeItem, ref ItemNodeTree objectItem)
        {
            TreeNode tn = null;
            foreach (TreeNode tr in treeView1.Nodes)
            {
                if (((ItemNodeTree)tr.Tag).id == testCaseID && ((ItemNodeTree)tr.Tag).nodeType == NodeType.TestCase)
                {
                    item = (ItemNodeTree)tr.Tag;
                    tn = tr;
                    break;
                }
                tn = GetItemCase(testCaseID, tr);
                if (tn != null)
                {
                    item = (ItemNodeTree)tn.Tag;
                    break;
                }
                
            }
            if (tn == null)
            {
                return false;
            }
            tn = tn.Parent;
            ItemNodeTree temptn;
            while (tn != null)
            {
                temptn = (ItemNodeTree)(tn.Tag);
                switch (temptn.nodeType)
                {
                    case NodeType.TestItem:
                        itemItem = temptn;
                        break;
                    case NodeType.TestType:
                        typeItem = temptn;
                        break;
                    case NodeType.TestObject:
                        objectItem = temptn;
                        break;
                    default:
                        break;
                }
                tn = tn.Parent;
            }
            return true;

        }
        //建立测试项实测ID与测试用例实测ID之间关系
        private void SetItemCaseRelativeTable(object itemID, object caseID)
        {
            int num = 0;
            DataTable dt = dbProject.ExecuteDataTable("SELECT * FROM CA测试用例与测试项关系表 WHERE 测试项ID = ? and 项目ID = ?;", itemID, pid);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["测试用例ID"].ToString() == caseID.ToString())
                    {
                        return;
                    }
                }
                num = dt.Rows.Count;
            }
            dbProject.ExecuteNoQuery("insert into CA测试用例与测试项关系表(ID, 测试项ID,测试用例ID, 序号, 项目ID, 直接所属标志) values(?, ?, ?, ?, ?, ?)",
                FunctionClass.NewGuid, itemID, caseID, num + 1, pid, true);

        }
        public void AddTestCaseToTable(TreeNode tn)
        {
            if (((ItemNodeTree)tn.Tag).nodeType != NodeType.TestCase) return;
            foreach(DataRow dr in testCaseTable.Rows)
            {
                if(dr["测试用例ID"].ToString() == ((ItemNodeTree)tn.Tag).id.ToString())
                    return;
            }
            DataColumnCollection dcc = testCaseTable.Columns;

            dcc["ID"].DefaultValue = FunctionClass.NewGuid;
            dcc["测试用例ID"].DefaultValue = ((ItemNodeTree)tn.Tag).id;
            dcc["项目ID"].DefaultValue = pid;
            dcc["测试版本"].DefaultValue = currentvid;

            testCaseTable.Rows.Add();
        }
        bool DeleteTestCaseTag = false;
        bool AddTestCaseTag = false;
        ItemNodeTree selectedItem;
        TreeNode selectedTreeNode;
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = e.Node;
            ItemNodeTree　item = (ItemNodeTree)tn.Tag;
            selectedTreeNode = tn;
            selectedItem = item;
            switch (item.nodeType)
            { 
                case NodeType.TestCase:
                    DeleteTestCase.Text = "删除测试用例";
                    if (!DeleteTestCaseTag)
                    {
                        this.Controls.Add(this.DeleteTestCase);
                        DeleteTestCaseTag = true;
                    }
                    if (AddTestCaseTag)
                    {
                        this.Controls.Remove(this.AddTestCase);
                        AddTestCaseTag = false;
                    }
                    break;
                case NodeType.TestItem:
                    DeleteTestCase.Text = "删除测试项";
                    AddTestCase.Text = "添加测试用例";
                    if (!DeleteTestCaseTag)
                    {
                        this.Controls.Add(this.DeleteTestCase);
                        DeleteTestCaseTag = true;
                    }
                    if (!AddTestCaseTag)
                    {
                        this.Controls.Add(this.AddTestCase);
                        AddTestCaseTag = true;
                    }
                    break;
                case NodeType.TestType:
                    DeleteTestCase.Text = "删除测试类型";
                    AddTestCase.Text = "添加测试项";
                    if (!DeleteTestCaseTag)
                    {
                        this.Controls.Add(this.DeleteTestCase);
                        DeleteTestCaseTag = true;
                    }
                    if (!AddTestCaseTag)
                    {
                        this.Controls.Add(this.AddTestCase);
                        AddTestCaseTag = true;
                    }
                    break;
                case NodeType.TestObject:
                    DeleteTestCase.Text = "删除被测对象";
                    AddTestCase.Text = "添加测试类型";
                    if (!DeleteTestCaseTag)
                    {
                        this.Controls.Add(this.DeleteTestCase);
                        DeleteTestCaseTag = true;
                    }
                    if (!AddTestCaseTag)
                    {
                        this.Controls.Add(this.AddTestCase);
                        AddTestCaseTag = true;
                    }
                    break;
                case NodeType.Project:
                    AddTestCase.Text = "添加被测对象";
                    if (DeleteTestCaseTag)
                    {
                        this.Controls.Remove(this.DeleteTestCase);
                        DeleteTestCaseTag = false;
                    }
                    if (!AddTestCaseTag)
                    {
                        this.Controls.Add(this.AddTestCase);
                        AddTestCaseTag = true;
                    }
                    break;
                default:
                    break;
            }

        }

        private void AddTestCase_Click(object sender, EventArgs e)
        {
            addTestCaseButtonTag = true;
            if (DeleteTestCaseTag)
            {
                this.Controls.Remove(this.DeleteTestCase);
                DeleteTestCaseTag = false;
            }
            this.Controls.Add(this.AddTestCaseButton);
            this.Controls.Add(this.CancelTestCaseButton);
            this.Controls.Add(this.addTestCaseBox);
        }

        private void DeleteTestCase_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认删除该测试内容！", "删除测试内容", MessageBoxButtons.OKCancel) == DialogResult.OK)
            { 
                
            }
        }
        bool addTestCaseButtonTag = false;
        private void AddTestCaseButton_Click(object sender, EventArgs e)
        {
            if (addTestCaseBox.Text == "")
            {
                MessageBox.Show("测试内容名不能为空，请重新输入！");
                return;
            }
            if (!AddTestCaseToDB(addTestCaseBox.Text))
            {
                MessageBox.Show("测试内容名已经存在，请重新输入！");
                return;
            }
            addTestCaseButtonTag = false;
            this.Controls.Remove(this.AddTestCaseButton);
            this.Controls.Remove(this.CancelTestCaseButton);
            this.Controls.Remove(this.addTestCaseBox);
        }
        private bool AddTestCaseToDB(String testCaseName)
        {
            if (selectedItem == null)
            {
                return false;
            }
            //首先判断测试内容名在数据库中不重名
            DataRow dr1 = null;
            switch (selectedItem.nodeType)
            { 
                case NodeType.TestItem:
                dr1 = dbProject.ExecuteDataRow("SELECT * FROM CA测试用例实体表 WHERE 测试用例名称 = ?;", testCaseName);
                break;
                case NodeType.TestType:
                dr1 = dbProject.ExecuteDataRow("SELECT * FROM CA测试项实体表 WHERE 测试项名称 = ?;", testCaseName);
                break;
                case NodeType.TestObject:
                dr1 = dbProject.ExecuteDataRow("SELECT * FROM CA测试类型实体表 WHERE 测试类型名称 = ?;", testCaseName);
                break;
                case NodeType.Project:
                dr1 = dbProject.ExecuteDataRow("SELECT * FROM CA被测对象实体表 WHERE 被测对象名称 = ?;", testCaseName);
                break;
                default:
                break;

            }
            if (dr1 != null)
            {
                return false;
            }
            DataRow drt = null;
            object testCaseID = null;
            InsertTestContentToDB(testCaseName, ref drt, ref testCaseID);
            if (drt == null) return false;
            ItemNodeTree item = ((ItemNodeTree)selectedTreeNode.Tag).AddChild(selectedItem.nodeType+1, testCaseID, drt, (((ItemNodeTree)selectedTreeNode.Tag).childlist.Count + 1).ToString(),
                testCaseName, (((ItemNodeTree)selectedTreeNode.Tag).childlist.Count + 1).ToString());
            item.IsShortCut = false;
            vc.AddTreeNode(selectedTreeNode.Nodes, item, testCaseName, testCaseTable);
            return true;
        }
        private bool InsertTestContentToDB(string testCaseName, ref DataRow drt, ref object testCaseID)
        {
            testCaseID = FunctionClass.NewGuid;
            object testCaseRealID = FunctionClass.NewGuid;

            switch (selectedItem.nodeType)
            {
                case NodeType.TestItem:
                    dbProject.ExecuteNoQuery("insert into CA测试用例实体表(ID, 项目ID, 测试用例名称, 创建版本ID) values(?, ?, ?, ?)",
                        testCaseID, pid, testCaseName, currentvid);
                    dbProject.ExecuteNoQuery("insert into CA测试用例实测表(ID, 项目ID, 执行状态, 测试用例ID, 测试版本) values(?, ?, ?, ?, ?)",
                        testCaseRealID, pid, "未执行", testCaseID, currentvid);
                    drt = dbProject.ExecuteDataRow("select * from CA测试用例实测表 where ID = ?", testCaseRealID);
                    AddTestContenteParentToTable(testCaseID, testCaseRealID, selectedItem.nodeType, selectedItem); 
                   break;
                case NodeType.TestType:
                   dbProject.ExecuteNoQuery("insert into CA测试项实体表(ID, 项目ID, 测试项名称, 所属测试类型ID, 创建版本ID) values(?, ?, ?, ?, ?)",
                       testCaseID, pid, testCaseName, selectedItem.id, currentvid);
                   dbProject.ExecuteNoQuery("insert into CA测试项实测表(ID, 项目ID, 序号,  测试项ID, 测试版本) values(?, ?, ?, ?, ?)",
                       testCaseRealID, pid, selectedItem.childlist.Count+1, testCaseID, currentvid);
                   drt = dbProject.ExecuteDataRow("select * from CA测试项实测表 where ID = ?", testCaseRealID);
                   AddTestContenteParentToTable(null, null, selectedItem.nodeType, selectedItem); 
                   break;
                case NodeType.TestObject:
                   dbProject.ExecuteNoQuery("insert into CA测试类型实体表(ID, 项目ID, 测试类型名称, 所属被测对象ID, 子节点类型, 创建版本ID) values(?, ?, ?, ?, ?, ?)",
                       testCaseID, pid, testCaseName, selectedItem.id, 2, currentvid);
                   dbProject.ExecuteNoQuery("insert into CA测试类型实测表(ID, 项目ID, 序号,  测试类型ID, 测试版本) values(?, ?, ?, ?, ?)",
                       testCaseRealID, pid, selectedItem.childlist.Count + 1, testCaseID, currentvid);
                   drt = dbProject.ExecuteDataRow("select * from CA测试类型实测表 where ID = ?", testCaseRealID);
                   AddTestContenteParentToTable(null, null, selectedItem.nodeType, selectedItem); 
                   break;
                case NodeType.Project:
                   dbProject.ExecuteNoQuery("insert into CA被测对象实体表(ID, 项目ID, 被测对象名称, 被测对象版本, 测试级别, 创建版本ID) values(?, ?, ?, ?, ?, ?)",
                       testCaseID, pid, testCaseName,"待定", "单元测试", currentvid);
                   dbProject.ExecuteNoQuery("insert into CA被测对象实测表(ID, 项目ID, 序号,  被测对象ID, 测试版本) values(?, ?, ?, ?, ?)",
                       testCaseRealID, pid, selectedItem.childlist.Count + 1, testCaseID, currentvid);
                   drt = dbProject.ExecuteDataRow("select * from CA被测对象实测表 where ID = ?", testCaseRealID);
                   AddTestContenteParentToTable(null, null, selectedItem.nodeType, selectedItem); 
                   break;
                default:
                    break;

            }
            return true;
        }
        private void CancelTestCaseButton_Click(object sender, EventArgs e)
        {
            addTestCaseButtonTag = false;
            this.Controls.Remove(this.AddTestCaseButton);
            this.Controls.Remove(this.CancelTestCaseButton);
            this.Controls.Remove(this.addTestCaseBox);
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (addTestCaseButtonTag)
            {
                e.Cancel = true;
                MessageBox.Show("请按取消键停止添加测试内容！");
            }
        }
    }
    class TreeViewClass
    {
        public Dictionary<ItemNodeTree, TreeNode> treenodemap = new Dictionary<ItemNodeTree, TreeNode>();
        DataTable testCaseTable;
        DataTable checkTestOfPlb = new DataTable("被选择的测试目标");

        TreeView treeView;
        DBAccess dbProject;
        object currentvid, pid;

        public void SetPara(TreeView tv,  bool objectIDTag1, object pid, object currentvid, DBAccess dbProject)
        {
            GridAssist.AddColumn(checkTestOfPlb, "ID");
            GridAssist.AddColumn<int>(checkTestOfPlb, "测试类型");
            treeView = tv;
            this.currentvid = currentvid;
            this.pid = pid;
            this.dbProject = dbProject;
        }

        public void AddTreeNode(ItemNodeTree item)
        {
            if (item.nodeType == NodeType.Project) return;
            TreeNode parent = GetParentTreeNode(item);
            if (parent == null) return;
            AddTreeNode(parent.Nodes, item, item.name, testCaseTable);
        }
        //首先在Treeview查询item的父节点有没有同名，没有返回null；
        //其次在同名的父节点上查询有没有与item同名的子节点，有则返回null；
        //返回同名父节点。
        private TreeNode GetParentTreeNode(ItemNodeTree item)
        {
            bool tag = false;
            TreeNode parentTreeNode = FindItemFormTree(treeView.Nodes[0], item.parent);
            if (parentTreeNode == null)
            {
                return null;
            }
            tag = ConsiderItemFromParentChild(parentTreeNode, item);
            if (tag)
            {
                return null;
            }
            else
            {
                return parentTreeNode;
            }
        }
        private bool ConsiderItemFromParentChild(TreeNode tempNode, ItemNodeTree item)
        {
            TreeNode chileNode = tempNode.FirstNode;
            while (chileNode != null)
            {
                if (((ItemNodeTree)chileNode.Tag).id.ToString() == item.id.ToString())
                {
                    return true;
                }
                else
                {
                    chileNode = chileNode.NextNode;
                }
            }
            return false;

        }
        //在Treeview查询item有没有同名
        private TreeNode FindItemFormTree(TreeNode tempNode,ItemNodeTree item)
        {
            TreeNode resultNode;
            if (tempNode != null)
            {
                if (((ItemNodeTree)tempNode.Tag).nodeType != item.nodeType)
                {
                    TreeNode chileNode = tempNode.FirstNode;
                    while (chileNode != null)
                    {
                        resultNode = FindItemFormTree(chileNode, item);
                        if (resultNode == null)
                        {
                            chileNode = chileNode.NextNode;
                        }
                        else
                        {
                            return resultNode;
                        }
                    }
                    return null;
                }
                else
                {
                    TreeNode nextNode = tempNode;
                    while (nextNode != null)
                    {
                        if (((ItemNodeTree)nextNode.Tag).name == item.name)
                        {
                            return nextNode;
                        }
                        else
                        {
                            nextNode = nextNode.NextNode;
                        }
                    }
                    return null;
                }
  
            }
            return null;
        }

        public void AddTreeNode(TreeNodeCollection tnc, ItemNodeTree item, string name, DataTable testCaseTable1)
        {
            TreeNode tn = tnc.Add(name);
            string key = item.GetIconName();
            tn.ImageKey = tn.SelectedImageKey = key;
            bool tag = ClassRegressionItemCaseTable.CreateVersionIsCurrent(item.id, item.nodeType, dbProject, pid, currentvid);
            tn.ForeColor = tag ? Color.Green : Color.Black;
            tn.Tag = item;
            testCaseTable = testCaseTable1;
            if (item.nodeType == NodeType.TestCase)
            {
                foreach (DataRow drt in testCaseTable1.Rows)
                {
                    if (drt.RowState == DataRowState.Deleted)
                    {
                        continue;
                    }
                    if(drt["测试用例ID"].ToString() == item.id.ToString())
                    {
                        tn.Checked = true;
                        //将初次选中测试对象、测试类型、测试项、测试用例放到checkTestOfPlb
                        DataColumnCollection dcc = checkTestOfPlb.Columns;
                        dcc["ID"].DefaultValue = item.id;
                        dcc["测试类型"].DefaultValue = item.nodeType;
                        checkTestOfPlb.Rows.Add();
                        ItemNodeTree parent = item.parent;
                        while (parent != null)
                        {
                            dcc["ID"].DefaultValue = parent.id;
                            dcc["测试类型"].DefaultValue = parent.nodeType;
                            checkTestOfPlb.Rows.Add();
                            parent = parent.parent;
                        }
                    }
                }  
            }
            else if (item.nodeType == NodeType.TestObject)
            {
                DataRow dr = dbProject.ExecuteDataRow("SELECT 被测对象版本 FROM CA被测对象实体表 WHERE ID = ? and 项目ID = ?;", item.id, pid);
                tn.Text += "(" + dr["被测对象版本"].ToString() + ")";
            }
            treenodemap[item] = tn;
        }
        public void DeleteTestCaseTable(object ID)
        {
            foreach (DataRow drt in testCaseTable.Rows)
            {
                if (drt["测试用例ID"].ToString() == ID.ToString())
                {
                    drt.Delete();
                    return;
                }
            }
        }
        public bool ConsiderCheckedInTestOfPlb(object ID)
        {
            foreach (DataRow drt in checkTestOfPlb.Rows)
            {
                if (drt.RowState == DataRowState.Deleted)
                {
                    continue;
                }
                if (drt["ID"].ToString() == ID.ToString())
                {
                    return true;
                }
            }
            return false;
        }


        public void DeleteCheckedInTestOfPlb(object ID)
        {
            foreach (DataRow drt in checkTestOfPlb.Rows)
            {
                if (drt.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (drt["ID"].ToString() == ID.ToString())
                {
                    drt.Delete();
                    return;
                }
            }
        } 
    }
 
    public class Set<T> : Dictionary<T, object>
    {
        public void Add(T t)
        {
            this[t] = null;
        }
    }
}
