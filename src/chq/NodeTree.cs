using System;
using System.Collections;
using System.Data;
using TPM3.wx;

namespace TPM3.chq
{
    class NodeTree
    {
        public string NodeID;
        public int NodeType;
        public string NodeContentID_body;
        public string NodeContentID_test;
        public string NodeContent;
        public string NodeContentJXm;
        public string FirstSonID;
        public string NextBrotherID;
        public int testcaseflag;//1---实例；0---引用
        public int Layer;
        public string TAOutputZJ;
        public string TPOutputZJ;
        public string testcaseOutputZJ;
        public string testrecordOutputZJ;
        public string TAStartZJ = MyProjectInfo.GetProjectContent("需求起始章节号");
        public string TPStartZJ = MyProjectInfo.GetProjectContent("计划起始章节号");
        public string TDStartZJ = MyProjectInfo.GetProjectContent("设计起始章节号");
        public string TLStartZJ = MyProjectInfo.GetProjectContent("记录起始章节号");

        public bool IfTiSheng = TPM3.wx.MyProjectInfo.GetBoolValue(TPM3.Sys.GlobalData.globalData.dbProject, TPM3.Sys.GlobalData.globalData.projectID, "不提升标题");

        public ArrayList LayerList = new ArrayList();

        public string ProjectID = TPM3.Sys.GlobalData.globalData.projectID.ToString();
        public string TestVerID = TPM3.Sys.GlobalData.globalData.currentvid.ToString();

        public string VerStr = "";


        public ArrayList AddNodeToArray(ArrayList LayerList, string NodeID, int NodeType, string NodeContentID_body, string NodeContentID_test, string NodeContent, string NodeContentJXm, string FirstSonID, string NextBrotherID, int testcaseflag, int Layer, string TAOutputZJ, string TPOutputZJ, string testcaseOutputZJ, string testrecordOutputZJ)
        {
            NodeTree layernode = new NodeTree();

            layernode.NodeID = NodeID.ToString();
            layernode.NodeType = NodeType;
            layernode.NodeContentID_body = NodeContentID_body.ToString();
            layernode.NodeContentID_test = NodeContentID_test.ToString();
            layernode.NodeContent = NodeContent;
            layernode.NodeContentJXm = NodeContentJXm;
            layernode.FirstSonID = FirstSonID.ToString();
            layernode.NextBrotherID = NextBrotherID.ToString();
            layernode.testcaseflag = testcaseflag;
            layernode.Layer = Layer;
            layernode.TAOutputZJ = TAOutputZJ;
            layernode.TPOutputZJ = TPOutputZJ;
            layernode.testcaseOutputZJ = testcaseOutputZJ;
            layernode.testrecordOutputZJ = testrecordOutputZJ;

            LayerList.Add(layernode);

            return LayerList;

        }

        public ArrayList UpdateNodeFirstSon(ArrayList LayerList, string NodeID, string FirstSonID)
        {

            NodeTree layernode = new NodeTree();

            layernode = (NodeTree)LayerList[int.Parse(NodeID)];

            layernode.FirstSonID = FirstSonID.ToString();

            return LayerList;

        }

        public ArrayList UpdateNodeNextBrother(ArrayList LayerList, string NodeID, string NextBrotherID)
        {

            NodeTree layernode = new NodeTree();
            layernode = (NodeTree)LayerList[int.Parse(NodeID)];

            layernode.NextBrotherID = NextBrotherID.ToString();

            return LayerList;

        }

        public string GetCurrentVerStr(string TestVerID)
        {
            string CurrentVerStr = "";

            string sqlstate = "select * from SYS测试版本表 where ID=? and 项目ID=? order by 序号";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestVerID, ProjectID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    object PerVer = dr["前向版本ID"];
                    if (PerVer == null)
                    {
                        CurrentVerStr = "";
                    }
                    else
                    {
                        if (PerVer.ToString() != "")
                        {
                            int xuhao = int.Parse(dr["序号"].ToString()) - 1;
                            CurrentVerStr = "(R" + xuhao.ToString() + ")";
                        }
                        else
                        {
                            CurrentVerStr = "";
                        }

                    }
                }
            }

            return CurrentVerStr;

        }

        public ArrayList PutNodeToLayerList()
        {

            int savenodeid1, savenodeid11, savenodeid2, savenodeid3, savenodeid33, savenodeid4;
            string nodeid, nodecontentid_body, nodecontentid_test;
            int layer;
            string jxm;
            string fatherTAOutputCapter = "";
            string fatherTPOutputCapter = "";

            VerStr = GetCurrentVerStr(TestVerID);

            PutTestObjectToLayerList(VerStr);//加被测对象层

            savenodeid1 = LayerList.Count - 1;
            savenodeid11 = LayerList.Count - 1;

            for(int i = 0; i <= savenodeid1; i++)//加被测对象下属第一层测试类型
            {
                NodeTree layernode = new NodeTree();
                layernode = (NodeTree)LayerList[i];

                nodeid = layernode.NodeID;
                nodecontentid_body = layernode.NodeContentID_body;
                nodecontentid_test = layernode.NodeContentID_test;
                layer = layernode.Layer;
                jxm = layernode.NodeContentJXm;
                fatherTAOutputCapter = layernode.TAOutputZJ;
                fatherTPOutputCapter = layernode.TPOutputZJ;

                PutTestTypeToLayerList(nodeid, nodecontentid_body, nodecontentid_test, 1, layer, jxm, fatherTAOutputCapter, fatherTPOutputCapter);

            }
            while(savenodeid1 != LayerList.Count - 1)//加第一级测试类型层下属多层测试类型
            {
                savenodeid2 = LayerList.Count - 1;
                for(int i = savenodeid1 + 1; i <= savenodeid2; i++)
                {

                    NodeTree layernode = new NodeTree();
                    layernode = (NodeTree)LayerList[i];

                    nodeid = layernode.NodeID;
                    nodecontentid_body = layernode.NodeContentID_body;
                    nodecontentid_test = layernode.NodeContentID_test;
                    layer = layernode.Layer;
                    jxm = layernode.NodeContentJXm;
                    fatherTAOutputCapter = layernode.TAOutputZJ;
                    fatherTPOutputCapter = layernode.TPOutputZJ;

                    PutTestTypeToLayerList(nodeid, nodecontentid_body, nodecontentid_test, 2, layer, jxm, fatherTAOutputCapter, fatherTPOutputCapter);//

                }
                savenodeid1 = savenodeid2;

            }

            savenodeid3 = LayerList.Count - 1;
            savenodeid33 = LayerList.Count - 1;

            for(int k = savenodeid11 + 1; k <= savenodeid3; k++)
            {

                NodeTree layernode = new NodeTree();
                layernode = (NodeTree)LayerList[k];

                if(layernode.NodeType == 2)
                {

                    nodeid = layernode.NodeID;
                    nodecontentid_body = layernode.NodeContentID_body;
                    nodecontentid_test = layernode.NodeContentID_test;
                    layer = layernode.Layer;
                    jxm = layernode.NodeContentJXm;
                    fatherTAOutputCapter = layernode.TAOutputZJ;
                    fatherTPOutputCapter = layernode.TPOutputZJ;


                    PutTestItemToLayerList(nodeid, nodecontentid_body, nodecontentid_test, 1, layer, jxm, fatherTAOutputCapter, fatherTPOutputCapter);//

                }
            }

            while(savenodeid3 != LayerList.Count - 1)//
            {
                savenodeid4 = LayerList.Count - 1;
                for(int i = savenodeid3 + 1; i <= savenodeid4; i++)
                {

                    NodeTree layernode = new NodeTree();
                    layernode = (NodeTree)LayerList[i];

                    nodeid = layernode.NodeID;
                    nodecontentid_body = layernode.NodeContentID_body;
                    nodecontentid_test = layernode.NodeContentID_test;
                    layer = layernode.Layer;
                    jxm = layernode.NodeContentJXm;
                    fatherTAOutputCapter = layernode.TAOutputZJ;
                    fatherTPOutputCapter = layernode.TPOutputZJ;


                    PutTestItemToLayerList(nodeid, nodecontentid_body, nodecontentid_test, 2, layer, jxm, fatherTAOutputCapter, fatherTPOutputCapter);//

                }
                savenodeid3 = savenodeid4;

            }

            for(int k = savenodeid33 + 1; k <= LayerList.Count - 1; k++)
            {

                NodeTree layernode = new NodeTree();
                layernode = (NodeTree)LayerList[k];

                if(layernode.NodeType == 3)
                {

                    nodeid = layernode.NodeID;
                    nodecontentid_body = layernode.NodeContentID_body;
                    nodecontentid_test = layernode.NodeContentID_test;
                    layer = layernode.Layer;
                    jxm = layernode.NodeContentJXm;

                    fatherTPOutputCapter = layernode.TPOutputZJ;


                    PutTestCaseToLayerList(nodeid, nodecontentid_body, nodecontentid_test, layer, jxm, fatherTPOutputCapter);//

                }
            }

            return LayerList;

        }
        //如果该节点有测试类型儿子，找出该节点的最末一个测试类型儿子，修改它的下一兄弟节点为下属第一个测试项
        //如果该节点没有测试类型儿子，修改该节点的第一儿子节点为下属第一个测试项
        public void UpdateNodeRelation_TestItem(string nodeid, string testitemnodeid)
        {

            int NodeType;

            string NodeContent;
            string NodeContentJXm;
            string FirstSonID;
            string NextBrotherID;


            NodeTree layernode = new NodeTree();
            layernode = (NodeTree)LayerList[int.Parse(nodeid)];

            NodeType = layernode.NodeType;

            NodeContent = layernode.NodeContent;
            NodeContentJXm = layernode.NodeContentJXm;
            FirstSonID = layernode.FirstSonID;
            NextBrotherID = layernode.NextBrotherID;

            if(int.Parse(FirstSonID) != 0)//已置过，有测试类型儿子
            {
                layernode = new NodeTree();
                layernode = (NodeTree)LayerList[int.Parse(FirstSonID)];

                NextBrotherID = layernode.NextBrotherID;
                while(int.Parse(NextBrotherID) != 0)
                {
                    layernode = new NodeTree();
                    layernode = (NodeTree)LayerList[int.Parse(NextBrotherID)];

                    NextBrotherID = layernode.NextBrotherID;

                }

                layernode.UpdateNodeNextBrother(LayerList, layernode.NodeID, testitemnodeid);

            }
            else//未置过，没有测试类型儿子
            {
                layernode.UpdateNodeFirstSon(LayerList, nodeid, testitemnodeid);

            }

        }

        //如果该节点有测试项儿子，找出该节点的最末一个测试项儿子，修改它的下一兄弟节点为下属第一个测试用例
        //如果该节点没有测试项儿子，修改该节点的第一儿子节点为下属第一个测试用例
        public void UpdateNodeRelation_TestCase(string nodeid, string testcasenodeid)
        {

            int NodeType;

            string NodeContent;
            string NodeContentJXm;
            string FirstSonID;
            string NextBrotherID;

            NodeTree layernode = new NodeTree();
            layernode = (NodeTree)LayerList[int.Parse(nodeid)];

            NodeType = layernode.NodeType;

            NodeContent = layernode.NodeContent;
            NodeContentJXm = layernode.NodeContentJXm;
            FirstSonID = layernode.FirstSonID;
            NextBrotherID = layernode.NextBrotherID;

            if(int.Parse(FirstSonID) != 0)//已置过，有测试项儿子
            {
                layernode = new NodeTree();
                layernode = (NodeTree)LayerList[int.Parse(FirstSonID)];

                NextBrotherID = layernode.NextBrotherID;
                while(int.Parse(NextBrotherID) != 0)
                {
                    layernode = new NodeTree();
                    layernode = (NodeTree)LayerList[int.Parse(NextBrotherID)];

                    NextBrotherID = layernode.NextBrotherID;

                }

                layernode.UpdateNodeNextBrother(LayerList, layernode.NodeID, testcasenodeid);

            }
            else//未置过，没有测试项儿子
            {
                layernode.UpdateNodeFirstSon(LayerList, nodeid, testcasenodeid);

            }

        }
        public void PutTestObjectToLayerList(string VerStr)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {

            string sqlstate = null;
            int NodeType;
            string NodeContentID_body, NodeContentID_test;
            string NodeContent;
            string NodeContentJXm;
            string NodeID = "0";
            NodeTree layernode;
            int testcaseflag = 0;
            int layer = 0;
            string TAOutputChapter = "";
            string TPOutputChapter = "";
            DataTable dt;
            int xuhao = 0;

            sqlstate = OutputComm.OutputComm.GetSqlState_CreateNodeTree("被测对象", 0, "", ProjectID, TestVerID);

            dt = TPM3.Sys.GlobalData.globalData.dbProject.ExecuteDataTable(sqlstate);
            if(dt != null && dt.Rows.Count != 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    xuhao = xuhao + 1;
                    NodeType = 1;
                    NodeContentID_body = dr[0].ToString();
                    NodeContentID_test = dr[1].ToString();
                    NodeContent = dr[2].ToString();
                    if (VerStr != "")
                    {
                        NodeContentJXm = dr[3].ToString() + VerStr;
                    }
                    else
                    {
                        NodeContentJXm = dr[3].ToString();
                    }
                    layer = 1;

                    if((IfHaveMulTestObject() == true) || ((IfHaveMulTestObject() == false) && (IfTiSheng == true)))//多个被测对象　或　一个被测对象不须抬升
                    {

                        TAOutputChapter = TAStartZJ + "." + xuhao;
                        TPOutputChapter = TPStartZJ + "." + xuhao;
                    }
                    else
                    {

                        TAOutputChapter = TAStartZJ;
                        TPOutputChapter = TPStartZJ;
                    }

                    layernode = new NodeTree();
                    int temp = int.Parse(NodeID) + 1;
                    LayerList = AddNodeToArray(LayerList, NodeID, NodeType, NodeContentID_body, NodeContentID_test, NodeContent, NodeContentJXm, "0", temp.ToString(), testcaseflag, layer, TAOutputChapter, TPOutputChapter, "", "");

                    int NodeID_int = int.Parse(NodeID) + 1;
                    NodeID = NodeID_int.ToString();

                }

            }

            if(LayerList.Count >= 1)
            {

                layernode = (NodeTree)LayerList[LayerList.Count - 1];
                int temp = LayerList.Count - 1;
                layernode.UpdateNodeNextBrother(LayerList, temp.ToString(), "0");
            }

        }
        public void PutTestTypeToLayerList(string FatherNodeID, string fatherobjectid_body, string fatherobjectid_test, int testtypelayer, int fatherlayer, string fatherjxm, string fatherTAOutputCapter, string fatherTPOutputCapter)
        {

            string sqlstate = null;
            int NodeID = LayerList.Count;
            int NodeType;
            string NodeContentID_body, NodeContentID_test;
            string NodeContent;
            string NodeContentJXm;
            int num = 0;
            NodeTree layernode;
            int testcaseflag = 0;
            int layer;
            string TAOutputCapter = "";
            string TPOutputCapter = "";
            int xuhao = 0;

            if(testtypelayer == 1)
            {
                sqlstate = OutputComm.OutputComm.GetSqlState_CreateNodeTree("测试类型", 1, fatherobjectid_body, ProjectID, TestVerID);
            }
            else if(testtypelayer == 2)
            {
                sqlstate = OutputComm.OutputComm.GetSqlState_CreateNodeTree("测试类型", 2, fatherobjectid_body, ProjectID, TestVerID);
            }

            DataTable dt = TPM3.Sys.GlobalData.globalData.dbProject.ExecuteDataTable(sqlstate);
            if(dt != null && dt.Rows.Count != 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    xuhao = xuhao + 1;
                    NodeType = 2;
                    NodeContentID_body = dr[0].ToString();
                    NodeContentID_test = dr[1].ToString();
                    NodeContent = dr[2].ToString();
                    NodeContentJXm = fatherjxm + "_" + dr[3].ToString();
                    layer = fatherlayer + 1;

                    TAOutputCapter = fatherTAOutputCapter + "." + xuhao;
                    TPOutputCapter = fatherTPOutputCapter + "." + xuhao;

                    layernode = new NodeTree();
                    int temp = NodeID + 1;
                    LayerList = AddNodeToArray(LayerList, NodeID.ToString(), NodeType, NodeContentID_body, NodeContentID_test, NodeContent, NodeContentJXm, "0", temp.ToString(), testcaseflag, layer, TAOutputCapter, TPOutputCapter, "", "");

                    if(num == 0)
                    {
                        layernode = new NodeTree();
                        layernode = (NodeTree)LayerList[int.Parse(FatherNodeID)];
                        layernode.UpdateNodeFirstSon(LayerList, FatherNodeID, NodeID.ToString());

                    }

                    num = num + 1;
                    NodeID = NodeID + 1;

                }

            }

            if(LayerList.Count >= 1)
            {

                layernode = (NodeTree)LayerList[LayerList.Count - 1];
                int temp = LayerList.Count - 1;
                layernode.UpdateNodeNextBrother(LayerList, temp.ToString(), "0");
            }

        }
        public void PutTestItemToLayerList(string FatherNodeID, string testtypeid_body, string testtypeid_test, int testtemlayer, int fatherlayer, string fatherjxm, string fatherTAoutputcapter, string fatherTPoutputcapter)
        {

            string sqlstate = null;
            int NodeID = LayerList.Count;
            int NodeType;
            string NodeContentID_body, NodeContentID_test;
            string NodeContent;
            string NodeContentJXm;
            int num = 0;
            NodeTree layernode;
            int testcaseflag = 0;
            int layer;
            string TAOutputCapter = "";
            string TPOutputCapter = "";
            int xuhao = 0;

            if(testtemlayer == 1)
            {
                sqlstate = OutputComm.OutputComm.GetSqlState_CreateNodeTree("测试项", 1, testtypeid_body, ProjectID, TestVerID);
            }
            else if(testtemlayer == 2)
            {
                sqlstate = OutputComm.OutputComm.GetSqlState_CreateNodeTree("测试项", 2, testtypeid_body, ProjectID, TestVerID);
            }

            DataTable dt = TPM3.Sys.GlobalData.globalData.dbProject.ExecuteDataTable(sqlstate);
            if(dt != null && dt.Rows.Count != 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    xuhao = xuhao + 1;
                    NodeType = 3;
                    NodeContentID_body = dr[0].ToString();
                    NodeContentID_test = dr[1].ToString();
                    NodeContent = dr[2].ToString();
                    NodeContentJXm = fatherjxm + "_" + dr[3].ToString();
                    layer = fatherlayer + 1;

                    TAOutputCapter = fatherTAoutputcapter + "." + xuhao;
                    TPOutputCapter = fatherTPoutputcapter + "." + xuhao;

                    layernode = new NodeTree();
                    int temp = NodeID + 1;

                    LayerList = layernode.AddNodeToArray(LayerList, NodeID.ToString(), NodeType, NodeContentID_body, NodeContentID_test, NodeContent, NodeContentJXm, "0", temp.ToString(), testcaseflag, layer, TAOutputCapter, TPOutputCapter, "", "");


                    if(num == 0 && testtemlayer == 1)
                    {
                        UpdateNodeRelation_TestItem(FatherNodeID, NodeID.ToString());
                    }
                    else if(num == 0 && testtemlayer == 2)
                    {

                        layernode = new NodeTree();
                        layernode = (NodeTree)LayerList[int.Parse(FatherNodeID)];
                        layernode.UpdateNodeFirstSon(LayerList, FatherNodeID, NodeID.ToString());

                    }

                    num = num + 1;
                    NodeID = NodeID + 1;

                }
            }

            if(LayerList.Count >= 1)
            {

                layernode = (NodeTree)LayerList[LayerList.Count - 1];
                int temp = LayerList.Count - 1;
                layernode.UpdateNodeNextBrother(LayerList, temp.ToString(), "0");
            }

        }
        public int GetTestItemNum(string TestItemID)
        {
            string sqlstate = OutputComm.OutputComm.GetSqlState_CreateNodeTree("测试子项个数", 0, TestItemID, ProjectID, TestVerID);
            DataTable dt = TPM3.Sys.GlobalData.globalData.dbProject.ExecuteDataTable(sqlstate);
            int count = dt.Rows.Count;

            return count;

        }
        public void PutTestCaseToLayerList(string FatherNodeID, string fatherobjectid_body, string fatherobjectid_test, int fatherlayer, string fatherjxm, string fatherTPOutputCapter)
        {

            string sqlstate = null;
            int NodeID = LayerList.Count;
            int NodeType;
            string NodeContentID_body, NodeContentID_test;
            string NodeContent;
            string NodeContentJXm;
            NodeTree layernode;
            int testcaseflag = 0;
            int layer;
            int TestCaseNum = 0;

            int TestItemNum = GetTestItemNum(fatherobjectid_body);

            sqlstate = OutputComm.OutputComm.GetSqlState_CreateNodeTree("测试用例", 0, fatherobjectid_test, ProjectID, TestVerID);

            DataTable dt = TPM3.Sys.GlobalData.globalData.dbProject.ExecuteDataTable(sqlstate);
            if(dt != null && dt.Rows.Count != 0)
            {
                foreach(DataRow dr in dt.Rows)
                {

                    NodeType = 4;
                    NodeContentID_body = dr[0].ToString();
                    NodeContentID_test = dr[1].ToString();
                    NodeContent = dr[2].ToString();
                    if(TestCaseNum + 1 < 10)
                    {

                        NodeContentJXm = fatherjxm + "_0" + (TestCaseNum + 1);
                    }
                    else
                    {

                        NodeContentJXm = fatherjxm + "_" + (TestCaseNum + 1);

                    }
                    if((bool)dr[3] == true)
                    {
                        testcaseflag = 1;
                    }
                    else
                    {
                        testcaseflag = 0;
                    }

                    layer = fatherlayer + 1;

                    layernode = new NodeTree();

                    int position = fatherTPOutputCapter.IndexOf(".");
                    string TestCaseOutputCapter = TDStartZJ + "." + fatherTPOutputCapter.Substring(position + 1, fatherTPOutputCapter.Length - position - 1) + "." + (TestItemNum + TestCaseNum + 1);
                    string TestRecordOutputCapter = TLStartZJ + "." + fatherTPOutputCapter.Substring(position + 1, fatherTPOutputCapter.Length - position - 1) + "." + (TestItemNum + TestCaseNum + 1);

                    int temp = NodeID + 1;
                    LayerList = layernode.AddNodeToArray(LayerList, NodeID.ToString(), NodeType, NodeContentID_body, NodeContentID_test, NodeContent, NodeContentJXm, "0", temp.ToString(), testcaseflag, layer, "", "", TestCaseOutputCapter, TestRecordOutputCapter);

                    if(TestCaseNum == 0)
                    {
                        UpdateNodeRelation_TestCase(FatherNodeID, NodeID.ToString());
                    }

                    TestCaseNum = TestCaseNum + 1;

                    NodeID = NodeID + 1;

                }
            }

            if(LayerList.Count >= 1)
            {

                layernode = (NodeTree)LayerList[LayerList.Count - 1];
                int temp = LayerList.Count - 1;
                layernode.UpdateNodeNextBrother(LayerList, temp.ToString(), "0");
            }

        }
        public Boolean IfHaveMulTestObject()
        {
            int objectnum = 0;
            string sqlstate = OutputComm.OutputComm.GetSqlState_CreateNodeTree("被测对象个数", 0, "", ProjectID, TestVerID);

            DataTable dt = TPM3.Sys.GlobalData.globalData.dbProject.ExecuteDataTable(sqlstate);
            if(dt != null && dt.Rows.Count != 0)
            {
                objectnum = dt.Rows.Count;
            }

            if(objectnum > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
