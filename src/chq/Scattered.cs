using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common;
using TPM3.wx;
using Z1.tpm.DB;
using TPM3.zxd;

namespace TPM3.chq
{
    class Scattered
    {
        public static string TestVerID;

       

        public string GetTestItemBS(string TestItemID, ArrayList DataTreeList)
        {
            int NodeType;
            string NodeContentID;
            string Value = "";

            NodeTree NodeTree = new NodeTree();

            for (int i = 0; i <= DataTreeList.Count - 1; i++)
            {

                NodeTree = (NodeTree)DataTreeList[i];

                NodeType = NodeTree.NodeType;
                NodeContentID = NodeTree.NodeContentID_test;

                if ((NodeType == 3) && (NodeContentID == TestItemID))
                {
                    Value = NodeTree.NodeContentJXm;
                    break;
                }

            }

            return Value;

        }

        public string GetTestCaseBS(string TestCaseID, ArrayList DataTreeList)
        {
            int NodeType;
            string NodeContentID;
            string Value = "";

            NodeTree NodeTree = new NodeTree();

            for (int i = 0; i <= DataTreeList.Count - 1; i++)
            {

                NodeTree = (NodeTree)DataTreeList[i];

                NodeType = NodeTree.NodeType;
                NodeContentID = NodeTree.NodeContentID_test;

                if ((NodeType == 4) && (NodeContentID == TestCaseID))
                {
                    Value = NodeTree.NodeContentJXm;
                    break;
                }

            }

            return Value;

        }

        public string GetTestTPOrTAOutputZJ(int SelectType, string TestItemID, ArrayList DataTreeList)
        {
            int NodeType;
            string NodeContentID;
            string Value = "";

            NodeTree NodeTree = new NodeTree();

            for (int i = 0; i <= DataTreeList.Count - 1; i++)
            {

                NodeTree = (NodeTree)DataTreeList[i];

                NodeType = NodeTree.NodeType;
                NodeContentID = NodeTree.NodeContentID_test;

                if ((NodeType == 3) && (NodeContentID == TestItemID))
                {
                    if (SelectType == 1)
                    {
                        Value = NodeTree.TAOutputZJ;
                        return Value;
                    }
                    else if (SelectType == 2)
                    {
                        Value = NodeTree.TPOutputZJ;
                        return Value;

                    }

                }

            }

            return Value;

        }
        public string GetTestTPOrTAOutputZJ_DX(int SelectType, string TestItemID, ArrayList DataTreeList)
        {
            int NodeType;
            string NodeContentID;
            string Value = "";

            NodeTree NodeTree = new NodeTree();

            for (int i = 0; i <= DataTreeList.Count - 1; i++)
            {

                NodeTree = (NodeTree)DataTreeList[i];

                NodeType = NodeTree.NodeType;
                NodeContentID = NodeTree.NodeContentID_test;

                if ((NodeType == 3) && (NodeContentID == TestItemID))
                { 
                     Value = NodeTree.TPOutputZJ;
                    
                    //----------------------------------------

                     string[] fields = Value.Split('.');

                    if (fields.Length == 4)
                    {
                          Value = "3.2." + fields[1] + "." + fields[2] + "." + fields[3];
                    }
                    else
                    {
                          Value = "3.2." + fields[1] + "." + fields[2] ;
                    }

                    //----------------------------------------

                     return Value;
                }

            }

            return Value;

        }

        public string GetTestRecordOutputZJ(string TestCaseID, ArrayList DataTreeList)
        {
            int NodeType;
            string NodeContentID;
            string testrecordOutputZJ;


            NodeTree NodeTree = new NodeTree();

            for (int i = 0; i <= DataTreeList.Count - 1; i++)
            {
                NodeTree = (NodeTree)DataTreeList[i];

                NodeType = NodeTree.NodeType;
                NodeContentID = NodeTree.NodeContentID_test;

                if ((NodeType == 4) && (NodeContentID == TestCaseID))
                {
                    testrecordOutputZJ = NodeTree.testrecordOutputZJ;
                    return testrecordOutputZJ;
                }

            }

            return "";

        }
        //public string GetTestRecordOutputZJ_cc(string TestCaseName)
        //{

        //    int NodeType;
        //    string NodeContentID;
        //    // string testrecordOutputZJ;
        //    int fatherid;


        //    NodeTree NodeTree = new NodeTree();

        //    for (int i = 0; i <= DataTreeList.Count - 1; i++)
        //    {
        //        NodeTree = (NodeTree)DataTreeList[i];

        //        NodeType = layernode.NodeType;
        //        NodeContentID = int.Parse(NodeTree.NodeContentID);

        //        if ((NodeType == 4) && (NodeContentID == TestCaseID))
        //        {
        //            fatherid = GetFatherContentID(layernode.NodeID);
        //            LayerNode layernode1 = new LayerNode();
        //            layernode1 = (LayerNode)LayerList[fatherid];


        //            string values = GetTestTPOutputZJ(layernode1.NodeContentID);

        //            int p = values.IndexOf(".");
        //            values = values.Substring(p + 1, values.Length - p - 1);

        //            values = DocContent.GetProjectContent<int>("记录起始章节号").ToString() + "." + values;

        //            return values;


        //        }

        //    }

        //    return "";

        //}


        public DataTable AddTableColumn(string TableType,string DocName)
        {

            DataTable dt = new DataTable();

            if (TableType == "测试包含关系")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试类", typeof(string));
                dt.Columns.Add("测试项", typeof(string));
                dt.Columns.Add("测试用例", typeof(string));
               
            }
            else if (TableType == "测试用例追踪关系")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试项在计划的章节", typeof(string));
                dt.Columns.Add("测试项名称", typeof(string));
                dt.Columns.Add("测试用例章节", typeof(string));
                dt.Columns.Add("测试用例名称", typeof(string));

            }
            else if (TableType == "测试用例追踪关系_最小模式")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试项标识", typeof(string));
                dt.Columns.Add("测试项名称", typeof(string));
                dt.Columns.Add("测试用例标识", typeof(string));
                dt.Columns.Add("测试用例名称", typeof(string));

            }
            else if (TableType == "执行结果统计")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试用例名称", typeof(string));
                dt.Columns.Add("测试用例标识", typeof(string));
                dt.Columns.Add("执行情况", typeof(string));
                dt.Columns.Add("执行结果", typeof(string));
                dt.Columns.Add("错误步骤", typeof(string));
                dt.Columns.Add("问题标识", typeof(string));

            }
            else if (TableType == "未完整执行统计")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试用例名称", typeof(string));
                dt.Columns.Add("测试用例标识", typeof(string));
                dt.Columns.Add("执行状态", typeof(string));
                dt.Columns.Add("未执行或部分执行原因", typeof(string));

            }
            else if (TableType == "测试依据")
            {
                dt.Columns.Add("测试依据", typeof(string));
                dt.Columns.Add("测试依据说明", typeof(string));
                dt.Columns.Add("测试依据标识", typeof(string));
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("父节点ID", typeof(string));
                dt.Columns.Add("章节号", typeof(string));
                dt.Columns.Add("是否追踪", typeof(string));
                dt.Columns.Add("未追踪原因说明", typeof(string));
            }
            else if (TableType == "回归测试依据")
            {
                dt.Columns.Add("测试依据", typeof(string));
                dt.Columns.Add("测试依据说明", typeof(string));
                dt.Columns.Add("测试依据标识", typeof(string));
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("父节点ID", typeof(string));
                dt.Columns.Add("章节号", typeof(string));
                dt.Columns.Add("是否追踪", typeof(string));
                dt.Columns.Add("未追踪原因说明", typeof(string));
            }
            else if (TableType == "测试依据与测试项的追踪关系")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("依据标识", typeof(string));
                dt.Columns.Add("测试依据", typeof(string));
                dt.Columns.Add("测试依据说明", typeof(string));
                dt.Columns.Add("是否追踪", typeof(string));
                if (DocName == "需求分析")
                {
                    dt.Columns.Add("测试项在需求的章节", typeof(string));
                }
                else// if (DocName == "测试计划")
                {
                    dt.Columns.Add("测试项在计划的章节", typeof(string));
                }

                dt.Columns.Add("测试项名称", typeof(string));

            }
            else if (TableType == "测试依据与测试项的追踪关系1")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("依据标识", typeof(string));
                dt.Columns.Add("测试依据", typeof(string));
                dt.Columns.Add("测试项标识", typeof(string));
                dt.Columns.Add("测试项名称", typeof(string));
               
            }

            else if (TableType == "测试项与测试依据的追踪关系")
            {
                dt.Columns.Add("序号", typeof(string));
                if (DocName == "需求分析")
                {
                    dt.Columns.Add("测试项在需求的章节", typeof(string));
                }
                else// if (DocName == "测试计划")
                {
                    dt.Columns.Add("测试项在计划的章节", typeof(string));
                }
                dt.Columns.Add("测试项名称", typeof(string));
                dt.Columns.Add("测试项标识", typeof(string));
                dt.Columns.Add("测试依据", typeof(string));
                dt.Columns.Add("测试依据说明", typeof(string));

            }
            else if (TableType == "测试项与测试依据的追踪关系1")
            {
                dt.Columns.Add("序号", typeof(string));           
                dt.Columns.Add("测试项标识", typeof(string));
                dt.Columns.Add("测试项名称", typeof(string));
                dt.Columns.Add("测试依据标识", typeof(string));
                dt.Columns.Add("测试依据", typeof(string));

            }
            else if ((TableType == "可变章节_被测对象提交问题一览") || (TableType == "可变章节_回归被测对象提交问题一览"))
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("问题标识", typeof(string));
                dt.Columns.Add("问题类别", typeof(string));
                dt.Columns.Add("问题级别", typeof(string));
                dt.Columns.Add("用例名称", typeof(string));
                dt.Columns.Add("用例标识", typeof(string));

            }
            else if (TableType == "测试类型统计")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试类型", typeof(string));
                dt.Columns.Add("测试类型标识", typeof(string));
                dt.Columns.Add("测试类型说明", typeof(string));

            }
            else if (TableType == "参加测试人员")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("角色", typeof(string));
                dt.Columns.Add("姓名", typeof(string));
                dt.Columns.Add("职称", typeof(string));
                dt.Columns.Add("主要职责", typeof(string));

            }
            else if (TableType == "附件统计")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("附件名称", typeof(string));
                dt.Columns.Add("是否输出", typeof(string));
                dt.Columns.Add("测试用例输出章节", typeof(string));
                dt.Columns.Add("测试用例名称", typeof(string));

            }
            else if (TableType == "其他更动涉及依据")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("更动名称", typeof(string));
                dt.Columns.Add("更动说明", typeof(string));
                dt.Columns.Add("其它更动影响域分析", typeof(string));
                dt.Columns.Add("相关测试依据", typeof(string));

            }
            else if (TableType == "问题涉及依据")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("软件问题", typeof(string));
                dt.Columns.Add("处理措施", typeof(string));
                dt.Columns.Add("影响域分析", typeof(string));
                dt.Columns.Add("测试依据", typeof(string));

            }
            else if (TableType == "原有用例统计")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试用例名称", typeof(string));
                dt.Columns.Add("测试用例标识", typeof(string));
                dt.Columns.Add("测试依据", typeof(string));

            }
            else if (TableType == "新增用例统计")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试用例名称", typeof(string));
                dt.Columns.Add("测试用例标识", typeof(string));
                dt.Columns.Add("测试依据", typeof(string));

            }
            else if (TableType == "未选取用例统计")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试用例名称", typeof(string));
                dt.Columns.Add("测试用例标识", typeof(string));
                dt.Columns.Add("测试依据", typeof(string));
                dt.Columns.Add("未测试原因", typeof(string));
            }
            else if (TableType == "已有测试项")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试类型", typeof(string));
                dt.Columns.Add("测试项", typeof(string));
                dt.Columns.Add("测试依据", typeof(string));
                dt.Columns.Add("是否选取", typeof(string));
                dt.Columns.Add("未选取原因说明", typeof(string));

            }
            else if (TableType == "已有测试用例")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试用例", typeof(string));
                dt.Columns.Add("测试项", typeof(string));
                dt.Columns.Add("是否选取", typeof(string));
                dt.Columns.Add("未选取原因说明", typeof(string));

            }
            else if (TableType == "回归测试依据与测试项的追踪关系")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("依据标识", typeof(string));
                dt.Columns.Add("测试依据", typeof(string));
                dt.Columns.Add("是否追踪", typeof(string));
                dt.Columns.Add("测试项名称", typeof(string));

            }
            else if (TableType == "回归测试项与测试依据的追踪关系")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试项", typeof(string));
                dt.Columns.Add("测试依据", typeof(string));

            }
            else if (TableType == "回归测试项与测试用例的追踪关系")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试项", typeof(string));
                dt.Columns.Add("测试用例", typeof(string));

            }
            else if (TableType == "回归测试用例与测试项的追踪关系")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试用例", typeof(string));
                dt.Columns.Add("测试项", typeof(string));

            }
            else if (TableType == "依据标识")
            {
                dt.Columns.Add("依据ID", typeof(string));
                dt.Columns.Add("依据标识", typeof(string));

            }
            else if (TableType == "测试计划中测试项到测试需求的追踪关系")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试项标识", typeof(string));
                dt.Columns.Add("测试项名称", typeof(string));
                dt.Columns.Add("测试项在需求的章节", typeof(string));
                dt.Columns.Add("测试项在计划的章节", typeof(string));

            }

            else if (TableType == "纠错性更动")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("软件问题", typeof(string));
                dt.Columns.Add("是否更动", typeof(string));
                dt.Columns.Add("更动标识", typeof(string));
                dt.Columns.Add("更动说明", typeof(string));
                dt.Columns.Add("未更动原因说明", typeof(string));

            }
            else if (TableType == "纠错性更动_定型")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("软件问题", typeof(string));
                dt.Columns.Add("是否更动", typeof(string));
                dt.Columns.Add("更动说明", typeof(string));
                dt.Columns.Add("未更动原因说明", typeof(string));
                dt.Columns.Add("影响域分析", typeof(string));

            }

            else if ((TableType == "4") || (TableType == "2") || (TableType == "3") || (TableType == "5"))
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("更动标识", typeof(string));
                dt.Columns.Add("更动说明", typeof(string));
              
            }

            else if (TableType == "影响域分析")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("更动标识", typeof(string));
                dt.Columns.Add("影响域分析", typeof(string));
                dt.Columns.Add("测试要求", typeof(string));
                dt.Columns.Add("测试项名称", typeof(string));

            }


            else if (TableType == "回归测试依据与测试用例的追踪关系")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("测试依据", typeof(string));
                dt.Columns.Add("测试用例名称", typeof(string));

            }

            else if (TableType == "软件更动与测试用例的追踪关系")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("软件更动", typeof(string));
                dt.Columns.Add("测试用例名称", typeof(string));

            }

            else if (TableType == "进度计划")
            {
                dt.Columns.Add("工作内容", typeof(string));
                dt.Columns.Add("预计开始时间", typeof(string));
                dt.Columns.Add("预计结束时间", typeof(string));

            } 
            else if (TableType =="CMI配置项")
            {
                  dt.Columns.Add("CMI名称", typeof(string));
                  dt.Columns.Add("CMI标识", typeof(string));
                  dt.Columns.Add("入库时间", typeof(string));
                  dt.Columns.Add("基线名称", typeof(string)); 

            }
            else if (TableType == "测试类型的问题统计")
            {
                dt.Columns.Add("测试类型", typeof(string));
                dt.Columns.Add("1级问题数", typeof(string));
                dt.Columns.Add("2级问题数", typeof(string));
                dt.Columns.Add("3级问题数", typeof(string));
                dt.Columns.Add("4级问题数", typeof(string));
                dt.Columns.Add("5级问题数", typeof(string));

            }
            else if (TableType == "回归测试更动情况")
            {
                dt.Columns.Add("序号", typeof(string));
                dt.Columns.Add("更动类型", typeof(string));  
                dt.Columns.Add("软件问题", typeof(string));
                dt.Columns.Add("是否更动", typeof(string));    
                dt.Columns.Add("更动标识", typeof(string));
                dt.Columns.Add("更动说明", typeof(string));
                dt.Columns.Add("用例名称", typeof(string));
              
            }

            return dt;

        }

        public DataTable GetObjectName()
        {
            string sqlstate = OutputComm.OutputComm.GetSqlState_CreateNodeTree("被测对象", 0, "", TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
            DataTable dt = TPM3.Sys.GlobalData.globalData.dbProject.ExecuteDataTable(sqlstate);
            return dt;

        }

        public string GetDirectTestCaseBS(string TestCaseID, ArrayList DataTreeList)
        {

            int NodeType;
            string NodeContentID;
            string NodeContentJXm;
            int testcaseflag;//1---实例；0---引用

            NodeTree layernode = new NodeTree();

            for (int i = 0; i <= DataTreeList.Count - 1; i++)
            {

                layernode = (NodeTree)DataTreeList[i];

                NodeType = layernode.NodeType;
                NodeContentID = layernode.NodeContentID_test;
                testcaseflag = layernode.testcaseflag;

                if ((NodeType == 4) && (NodeContentID == TestCaseID) && (testcaseflag == 1))
                {
                    NodeContentJXm = layernode.NodeContentJXm;
                    return NodeContentJXm;
                }

            }

            return "";

        }

        public string GetFieldName(string OutputType)
        {
            string FieldName = "";

            if (OutputType == "测试用例")
            {
                FieldName = "可变章节_测试用例";
            }
            else if (OutputType == "测试项")
            {
                FieldName = "可变章节_测试项";
            }

            return FieldName;
        }

        public ArrayList GetTestAccording(string TestItemID)
        {

            ArrayList AccordingList = new ArrayList();

            string Values1 = "";

            //DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("select * from SYS测试依据表 where 依据类型=1");
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("select * from SYS测试依据表");

            string sqlstate = "select 追踪关系 from CA测试项实测表 where ID=?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestItemID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["追踪关系"] = GridAssist.GetMultiDisplayString(dt1, "ID", "测试依据", dr["追踪关系"], ",");
                   
                    Values1 = dr[0].ToString();
                    if (Values1 == "")
                    {
                        return AccordingList;
                    }
                    else
                    {
                        int position1 = Values1.IndexOf(",");

                        while (position1 != -1)
                        {
                            string Values11 = Values1.Substring(0, position1);

                            AccordingList.Add(Values11);

                            Values1 = Values1.Substring(position1 + 1, Values1.Length - position1 - 1);

                            position1 = Values1.IndexOf(",");

                        }
                        AccordingList.Add(Values1);
                    }
                }

            }

            return AccordingList;

        }



        public ArrayList GetTestAccordingBS(string TestItemID, ArrayList DataTreeList,int TestType)
        {
            
            DataTable dt0 = null;

            if (TestType == 0)
            {
                dt0 = GetTestAccordingTableBS("依据标识", DataTreeList, "");
            }
            else
            {
                dt0 = GetTestAccordingTableBS_HG("依据标识", DataTreeList, "");
            }

            ArrayList AccordingBSList = new ArrayList();

            string Values1 = "";

            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("select * from SYS测试依据表");

            string sqlstate = "select 追踪关系 from CA测试项实测表 where ID=?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestItemID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Values11 = "";

                    dr["追踪关系"] = GridAssist.GetMultiDisplayString(dt1, "ID", "ID", dr["追踪关系"], ",");
                   
                    Values1 = dr[0].ToString();
                    if (Values1 == "")
                    {
                        return AccordingBSList;
                    }
                    else
                    {
                        int position1 = Values1.IndexOf(",");

                        while (position1 != -1)
                        {
                            Values11 = Values1.Substring(0, position1);

                            if (dt0.Rows.Count > 0)
                            {
                                for (int k = 0; k <= dt0.Rows.Count - 1;k++ )
                                {
                                    DataRow dr1 = dt0.Rows[k];
                                    string id = dr1[0].ToString();
                                    if (id == Values11)
                                    {
                                        Values11 = dr1[1].ToString();

                                    }

                                }
                            }

                            AccordingBSList.Add(Values11);

                            Values1 = Values1.Substring(position1 + 1, Values1.Length - position1 - 1);

                            position1 = Values1.IndexOf(",");

                        }

                        if (dt0.Rows.Count > 0)
                        {
                            for (int k = 0; k <= dt0.Rows.Count - 1; k++)
                            {
                                DataRow dr1 = dt0.Rows[k];
                                string id = dr1[0].ToString();
                                if (id == Values1)
                                {
                                    Values11 = dr1[1].ToString();

                                }

                            }
                        }

                        AccordingBSList.Add(Values11);
                    }
                }

            }
          
            return AccordingBSList;

        }

        public DataTable GetTestAccordingTableBS(string QueryType, ArrayList DataTreeList, string DocName)
        {

            int iStack;
            string[] stack = new string[10];
            string YijuID = "";
            string YijuID_pre = "";
            string YijuID_firstson = "";
            string YijuID_brother = "";
            string YijuID_father = "";
            string sqlstate = "";
            string wdbs = "";

            int xuhao = 0;
            int xuhaonum = 0;

            DataTable dt;
            DataRow AddDataRow = null;
            DataTable AddDataTable = new DataTable();
            ArrayList BSlist = new ArrayList();

            DataTable YiJuTable = RequireTreeForm.GetRequireTreeTable(true, TestVerID);

            AddDataTable = AddTableColumn(QueryType, DocName);

            iStack = 0;

            sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 依据类型=1 and 父节点ID='~root' and 序号=1";

            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    YijuID = dr[2].ToString();
                    AddDataRow = dr;

                }
            }
            if (YijuID == "")
            {
                return AddDataTable;
            }

            do
            {
                while (YijuID != "")
                {
                    ///////////////////////////////////////////////////////////////////////////

                   // if (QueryType == "测试依据")
                    {
                        sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明,测试依据标识 FROM SYS测试依据表 where 项目ID = ? and 测试版本=? and 依据类型=1 and ID=?";
                        dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, YijuID);

                        string TestYiju;
                        string TestYijuShuoming;
                        string TestYijuID="";
                        string ZJ = "";
                        bool IfAccording = false;
                        string fatherID = "";
                        string yy = "";

                        if (dt != null && dt.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                TestYiju = dr[0].ToString();
                                TestYijuShuoming = dr[1].ToString();
                                TestYijuID = dr[2].ToString();
                                ZJ = dr[4].ToString();
                                IfAccording = (bool)dr[5];
                                fatherID = dr[3].ToString();
                                yy = dr[6].ToString();

                                if (fatherID == "~root")
                                {
                                    wdbs = dr[7].ToString();
                                    yy = "此项为不追踪项";
                                }
                                else
                                {
                                    if (IfAccording == true)
                                    {
                                        yy = "";
                                    }
                                }

                                AddDataTable.ImportRow(dr);

                            }
                        }

                        string bs = "";
                        if (fatherID == "~root")
                        {
                            bs = wdbs;
                        }
                        else
                        {
                            //bs = wdbs + "_" + ZJ;
                            bs = GetYiJuBS(YiJuTable, TestYijuID);
                        }

                        AddDataTable.Rows[xuhaonum]["依据ID"] = TestYijuID;
                        AddDataTable.Rows[xuhaonum]["依据标识"] = bs;

                        xuhaonum++;
                     
                    }
                    ////////////////////////////////////////////////////////

                    stack[iStack] = YijuID;
                    iStack = iStack + 1;

                    YijuID_pre = YijuID;
                    YijuID_firstson = "";
                    sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID, 章节号,是否追踪 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 依据类型=1 and 父节点ID=? and 序号=1";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, YijuID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_firstson = dr[2].ToString();
                        }
                    }

                    //-----push(stack,p)                                      
                    if (YijuID_firstson != "")
                    {
                        YijuID = YijuID_firstson;
                    }
                    else
                    {
                        YijuID = "";
                    }

                }
                if (iStack == 0)
                {
                    return AddDataTable;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    YijuID_pre = stack[iStack];

                    YijuID_father = "";
                    sqlstate = "SELECT 父节点ID FROM SYS测试依据表 where ID=? and 项目ID =? and 测试版本=? and 依据类型=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_father = dr[0].ToString();
                        }
                    }

                    sqlstate = "SELECT 序号 FROM SYS测试依据表 where ID=? and 项目ID = ? and 测试版本=? and 依据类型=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            xuhao = int.Parse(dr[0].ToString());
                        }
                    }

                    YijuID_brother = "";
                    sqlstate = "SELECT ID FROM SYS测试依据表 where 父节点ID=? and 序号=? and 项目ID =? and 测试版本=? and 依据类型=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_father, xuhao + 1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_brother = dr[0].ToString();
                        }
                    }

                    if (YijuID_brother != "")
                    {
                        YijuID = YijuID_brother;
                    }
                    else
                    {
                        YijuID = "";

                    }

                }
            } while (true);

        }

        public DataTable GetTestAccordingTableBS_HG(string QueryType, ArrayList DataTreeList, string DocName)
        {

            int iStack;
            string[] stack = new string[10];
            string YijuID = "";
            string YijuID_pre = "";
            string YijuID_firstson = "";
            string YijuID_brother = "";
            string YijuID_father = "";
            string sqlstate = "";
            string wdbs = "";

            int xuhao = 0;
            int xuhaonum = 0;

            DataTable dt;
            DataRow AddDataRow = null;
            DataTable AddDataTable = new DataTable();
            ArrayList BSlist = new ArrayList();

            DataTable YiJuTable = RequireTreeForm.GetRequireTreeTable(true, TestVerID);

            AddDataTable = AddTableColumn(QueryType, DocName);

            iStack = 0;

           // sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 依据类型=2 and 父节点ID='~root' and 序号=1";
            sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 父节点ID='~root' and 序号=1";


            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    YijuID = dr[2].ToString();
                    AddDataRow = dr;

                }
            }
            if (YijuID == "")
            {
                return AddDataTable;
            }

            do
            {
                while (YijuID != "")
                {
                    ///////////////////////////////////////////////////////////////////////////

                    // if (QueryType == "测试依据")
                    {
                       // sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明,测试依据标识 FROM SYS测试依据表 where 项目ID = ? and 测试版本=? and 依据类型=2 and ID=?";
                             sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明,测试依据标识 FROM SYS测试依据表 where 项目ID = ? and 测试版本=? and ID=?";
                      
                        dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, YijuID);

                        string TestYiju;
                        string TestYijuShuoming;
                        string TestYijuID = "";
                        string ZJ = "";
                        bool IfAccording = false;
                        string fatherID = "";
                        string yy = "";

                        if (dt != null && dt.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                TestYiju = dr[0].ToString();
                                TestYijuShuoming = dr[1].ToString();
                                TestYijuID = dr[2].ToString();
                                ZJ = dr[4].ToString();
                                IfAccording = (bool)dr[5];
                                fatherID = dr[3].ToString();
                                yy = dr[6].ToString();

                                if (fatherID == "~root")
                                {
                                    wdbs = dr[7].ToString();
                                    yy = "此项为不追踪项";
                                }
                                else
                                {
                                    if (IfAccording == true)
                                    {
                                        yy = "";
                                    }
                                }

                                AddDataTable.ImportRow(dr);

                            }
                        }

                        string bs = "";
                        if (fatherID == "~root")
                        {
                            bs = wdbs;
                        }
                        else
                        {
                            //bs = wdbs + "_" + ZJ;
                            bs = GetYiJuBS(YiJuTable, TestYijuID);
                        }

                        AddDataTable.Rows[xuhaonum]["依据ID"] = TestYijuID;
                        AddDataTable.Rows[xuhaonum]["依据标识"] = bs;

                        xuhaonum++;

                    }
                    ////////////////////////////////////////////////////////

                    stack[iStack] = YijuID;
                    iStack = iStack + 1;

                    YijuID_pre = YijuID;
                    YijuID_firstson = "";
                   // sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID, 章节号,是否追踪 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 依据类型=2 and 父节点ID=? and 序号=1";
                    sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID, 章节号,是否追踪 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 父节点ID=? and 序号=1";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, YijuID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_firstson = dr[2].ToString();
                        }
                    }

                    //-----push(stack,p)                                      
                    if (YijuID_firstson != "")
                    {
                        YijuID = YijuID_firstson;
                    }
                    else
                    {
                        YijuID = "";
                    }

                }
                if (iStack == 0)
                {
                    return AddDataTable;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    YijuID_pre = stack[iStack];

                    YijuID_father = "";
                   // sqlstate = "SELECT 父节点ID FROM SYS测试依据表 where ID=? and 项目ID =? and 测试版本=? and 依据类型=2 ";
                    sqlstate = "SELECT 父节点ID FROM SYS测试依据表 where ID=? and 项目ID =? and 测试版本=?";


                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_father = dr[0].ToString();
                        }
                    }

                    //sqlstate = "SELECT 序号 FROM SYS测试依据表 where ID=? and 项目ID = ? and 测试版本=? and 依据类型=2 ";
                    sqlstate = "SELECT 序号 FROM SYS测试依据表 where ID=? and 项目ID = ? and 测试版本=?";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            xuhao = int.Parse(dr[0].ToString());
                        }
                    }

                    YijuID_brother = "";
                   // sqlstate = "SELECT ID FROM SYS测试依据表 where 父节点ID=? and 序号=? and 项目ID =? and 测试版本=? and 依据类型=2 ";
                    sqlstate = "SELECT ID FROM SYS测试依据表 where 父节点ID=? and 序号=? and 项目ID =? and 测试版本=?";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_father, xuhao + 1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_brother = dr[0].ToString();
                        }
                    }

                    if (YijuID_brother != "")
                    {
                        YijuID = YijuID_brother;
                    }
                    else
                    {
                        YijuID = "";

                    }

                }
            } while (true);

        }


        public ArrayList ReSortList(ArrayList QuestionInfoList, String ObjectID)
        {
            ArrayList QIDlist = new ArrayList();
            ArrayList SortQuestionList = new ArrayList();

            string sqlstate = "SELECT CA问题报告单.ID AS 问题报告单ID, CA问题报告单.所属被测对象ID, CA问题报告单.一级标识, CA问题报告单.二级标识, CA问题报告单.三级标识, CA问题报告单.四级标识, CA问题报告单.同标识序号" +
                              " FROM CA问题报告单 WHERE CA问题报告单.所属被测对象ID=? ORDER BY CA问题报告单.一级标识, CA问题报告单.二级标识, CA问题报告单.三级标识, CA问题报告单.四级标识, CA问题报告单.同标识序号;";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ObjectID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string QID = dr[0].ToString();
                    QIDlist.Add(QID);

                }
            }

            for (int i = 0; i <= QIDlist.Count - 1; i++)
            {
                string QID = QIDlist[i].ToString();
                for (int j = 0; j <= QuestionInfoList.Count - 1; j++)
                {
                    QuestionInfo QuestionInfoNode = (QuestionInfo)QuestionInfoList[j];
                    string xuhao2 = QuestionInfoNode.QuestionID.ToString();

                    if (QID == xuhao2)
                    {
                        SortQuestionList.Add(QuestionInfoNode);
           
                    }

                }
            }
            return SortQuestionList;

        }
        public int GetQuestionNumAccordClassLevel(ArrayList QuestionInfoList, string ClassName, string LevelName)//%%%%%%%%%%%%%%%%%%%%%%%%%
        {
            int num = 0;

            for (int i = 0; i <= QuestionInfoList.Count - 1; i++)
            {
                QuestionInfo QuestionInfoNode = (QuestionInfo)QuestionInfoList[i];
                string QuestionLevelName = GetQuestionLevelName(QuestionInfoNode.QuestionLevel);
                string QuestionClassName = GetQuestionTypeName(QuestionInfoNode.QuestionType);

                if ((LevelName == QuestionLevelName) && (ClassName == QuestionClassName))
                {
                    num = num + 1;
                }

            }
            return num;

        }

        public ArrayList QuestionLevelList(string Type1)//%%%%%%%%%%%%%%%%%%%%%%%%%
        {
            ArrayList LevelList = new ArrayList();
            string sqlstate1 = "SELECT DC问题级别表.名称, DC问题级别表.序号, DC问题级别表.项目ID, DC问题级别表.类型 " +
                               " FROM DC问题级别表 WHERE DC问题级别表.项目ID=? AND DC问题级别表.类型=? ORDER BY DC问题级别表.序号;";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), Type1);
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Level = dr[0].ToString();
                    if (Level != "")
                    {
                        LevelList.Add(Level);
                    }

                }

            }
            return LevelList;
        }

        public ArrayList ChangeList(ArrayList QuestionInfoList)
        {
            bool Flag = true;

            ArrayList QuestionInfoList1 = new ArrayList();
            for (int j = 0; j <= QuestionInfoList.Count - 1; j++)
            {
                QuestionInfo QuestionInfoNode = (QuestionInfo)QuestionInfoList[j];
                Flag = true;
                for (int k = 0; k <= QuestionInfoList1.Count - 1; k++)
                {
                    QuestionInfo QuestionInfoNode1 = (QuestionInfo)QuestionInfoList1[k];
                    if (QuestionInfoNode.QuestionID == QuestionInfoNode1.QuestionID)
                    {
                        Flag = false;
                        break;
                    }

                }
                if (Flag == true)
                {
                    QuestionInfoList1.Add(QuestionInfoNode);
                }
                
            }

            return QuestionInfoList1;
        }

        public string GetTestCaseOutputZJ_1(string TestCaseID, string NodeID, ArrayList DataTreeList)
        {

            int NodeType;
            string NodeContentID;
            string testcaseOutputZJ;
            string ID;

            NodeTree layernode = new NodeTree();

            for (int i = 0; i <= DataTreeList.Count - 1; i++)
            {
                layernode = (NodeTree)DataTreeList[i];

                NodeType = layernode.NodeType;
                NodeContentID = layernode.NodeContentID_test;

                ID = layernode.NodeID;

                if ((NodeType == 4) && (NodeContentID == TestCaseID) && (ID == NodeID))
                {
                    testcaseOutputZJ = layernode.testcaseOutputZJ;
                    return testcaseOutputZJ;
                }

            }

            return "";

        }

        public ArrayList GetTestCaseNodeIDSonOfTestItem(string TestItemNodeID, ArrayList DataTreeList)
        {

            int NodeType;
            string NodeID;
            int sonnodetype;
            string NextBrotherID;

            ArrayList TestCaseSonofOneTestItemList = new ArrayList();

            NodeTree layernode = new NodeTree();

            for (int i = 0; i <= DataTreeList.Count - 1; i++)
            {
                layernode = (NodeTree)DataTreeList[i];

                NodeType = layernode.NodeType;
                NodeID = layernode.NodeID;

                if ((NodeType == 3) && (NodeID == TestItemNodeID))
                {
                    string FirstSonID = layernode.FirstSonID;

                    if (FirstSonID != "0")//有儿子
                    {
                        layernode = new NodeTree();
                        layernode = (NodeTree)DataTreeList[int.Parse(FirstSonID)];

                        sonnodetype = layernode.NodeType;
                        if (sonnodetype == 4)//测试用例儿子
                        {
                            TestCaseSonofOneTestItemList.Add(layernode.NodeID);
                        }

                        NextBrotherID = layernode.NextBrotherID;

                        while (NextBrotherID != "0")
                        {
                            layernode = new NodeTree();
                            layernode = (NodeTree)DataTreeList[int.Parse(NextBrotherID)];

                            sonnodetype = layernode.NodeType;
                            if (sonnodetype == 4)//测试用例儿子
                            {
                                TestCaseSonofOneTestItemList.Add(layernode.NodeID);
                            }

                            NextBrotherID = layernode.NextBrotherID;

                        }

                    }

                }

            }

            return TestCaseSonofOneTestItemList;

        }

        public static void TestCaseBSOrNameList_OneQuestion(ref ArrayList TestCaseBSList,ref ArrayList TestCaseNameList,ArrayList QuestionInfoList, string QuestionID)
        {
           
            for (int i = 0; i <= QuestionInfoList.Count - 1; i++)
            {
                QuestionInfo QuestionInfoNode = new QuestionInfo();

                QuestionInfoNode = (QuestionInfo)QuestionInfoList[i];

                if (QuestionInfoNode.QuestionID == QuestionID)
                {
                    string testcasebs = "";
                    string testcasename = "";
                    testcasebs = QuestionInfoNode.TestCaseBS;
                    testcasename = QuestionInfoNode.TestCaseName;
                    
                    bool flag = false;

                    for (int j = 0; j <= TestCaseBSList.Count - 1; j++)
                    {
                        if (TestCaseBSList[j].ToString() == testcasebs)
                        {
                            flag = true;                            
                        }                        
                    }
                    if (flag == false)
                    {
                        TestCaseBSList.Add(testcasebs);
                        TestCaseNameList.Add(testcasename);
                    }
                    
                }
            }
           
        }

        public DataTable GetUnderHaveTableData(string TextStartField, String ObjectID,string ObjectID_body, String TestVerID, string ProjectID, ArrayList DataTreeList)
        {
            DataTable dt = null;
            string sqlstate = "";

            if (TextStartField == "可变章节_测试类型统计")
            {
                sqlstate = "SELECT CA测试类型实体表.测试类型名称, CA测试类型实测表.测试版本, CA测试类型实测表.项目ID, CA测试类型实体表.所属被测对象ID FROM CA测试类型实体表 INNER JOIN CA测试类型实测表 ON CA测试类型实体表.ID = CA测试类型实测表.测试类型ID" +
                      " WHERE CA测试类型实测表.测试版本=? AND CA测试类型实测表.项目ID=? AND CA测试类型实体表.所属被测对象ID=? ORDER BY CA测试类型实测表.序号;";

                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestVerID, ProjectID, ObjectID_body);

            }
            else if ((TextStartField == "可变章节_被测对象提交问题一览") || (TextStartField == "可变章节_回归被测对象提交问题一览")) 
            {
                ArrayList QuestionList = Scattered.GetArrayListInfo(ObjectID, "问题统计", DataTreeList);
                QuestionList = ReSortList(QuestionList, ObjectID);

                dt = AddTableColumn(TextStartField,"");

                ArrayList AlreadyList = new ArrayList();
                int rowsnum = 0;
                for (int i = 0; i <= QuestionList.Count - 1; i++)
                {
                    QuestionInfo QuestionInfoNode = new QuestionInfo();
                    QuestionInfoNode = (QuestionInfo)QuestionList[i];

                    string QuestionID = QuestionInfoNode.QuestionID;
                   
                    if (i == 0)
                    {
                        AlreadyList.Add(QuestionID);

                        string QuestionBS = QuestionInfoNode.QuestionBS;
                        string QuestionType = GetQuestionTypeName(QuestionInfoNode.QuestionType);
                        string QuestionLevel = GetQuestionLevelName(QuestionInfoNode.QuestionLevel);

                        ArrayList TestCaseNameList = new ArrayList();
                        ArrayList TestCaseBSList = new ArrayList();

                        Scattered.TestCaseBSOrNameList_OneQuestion(ref TestCaseBSList,ref TestCaseNameList, QuestionList, QuestionID);

                        rowsnum = rowsnum + 1;

                        for (int j = 0; j <= TestCaseNameList.Count - 1; j++)
                        {
                            DataRow dr = dt.Rows.Add();
                            int no = rowsnum;
                            dr["序号"] = no.ToString();
                            dr["问题标识"] = QuestionBS;
                            dr["问题类别"] = QuestionType;
                            dr["问题级别"] = QuestionLevel;
                            dr["用例名称"] = TestCaseNameList[j].ToString();
                            dr["用例标识"] = TestCaseBSList[j].ToString();

                        }
                    }
                    else
                    {
                        bool haveflag = false;
                        for (int l = 0; l <= AlreadyList.Count - 1;l++ )
                        {
                            if (QuestionID == AlreadyList[l].ToString())
                            {
                                haveflag = true;
                                break;                               
                            }

                        }
                        if (haveflag == false)
                        {
                            AlreadyList.Add(QuestionID);

                            string QuestionBS = QuestionInfoNode.QuestionBS;
                            string QuestionType = GetQuestionTypeName(QuestionInfoNode.QuestionType);
                            string QuestionLevel = GetQuestionLevelName(QuestionInfoNode.QuestionLevel);

                            ArrayList TestCaseNameList = new ArrayList();
                            ArrayList TestCaseBSList = new ArrayList();

                            Scattered.TestCaseBSOrNameList_OneQuestion(ref TestCaseBSList, ref TestCaseNameList, QuestionList, QuestionID);

                            rowsnum = rowsnum + 1;
                            for (int j = 0; j <= TestCaseNameList.Count - 1; j++)
                            {
                                DataRow dr = dt.Rows.Add();
                                int no = rowsnum;
                                dr["序号"] = no.ToString();
                                dr["问题标识"] = QuestionBS;
                                dr["问题类别"] = QuestionType;
                                dr["问题级别"] = QuestionLevel;
                                dr["用例名称"] = TestCaseNameList[j].ToString();
                                dr["用例标识"] = TestCaseBSList[j].ToString();

                            }


                        }

                    }

                   
                }

            }

            return dt;
        }

        public string GetQuestionTypeName(string TypeID)
        {
            string sqlstate = "SELECT DC问题级别表.名称, DC问题级别表.ID, DC问题级别表.项目ID " +
                              " FROM DC问题级别表 WHERE DC问题级别表.ID=? AND DC问题级别表.类型='类别' AND DC问题级别表.项目ID=?";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TypeID, TPM3.Sys.GlobalData.globalData.projectID.ToString());

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                return dt.Rows[0][0].ToString();
            }
            else return "";
        }

        public string GetQuestionLevelName(string LevelID)
        {
            string sqlstate = "SELECT DC问题级别表.名称, DC问题级别表.ID, DC问题级别表.项目ID " +
                              " FROM DC问题级别表 WHERE DC问题级别表.ID=? AND DC问题级别表.类型='级别' AND DC问题级别表.项目ID=?";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, LevelID, TPM3.Sys.GlobalData.globalData.projectID.ToString());
            
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                return dt.Rows[0][0].ToString();
            }
            else return "";
        }

        public DataTable GetStatTable(string ObjectID)
        {
            DataTable dt = new DataTable();

            dt = TestCaseSummary.GetCountTable(ObjectID);

            for(int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                 if (dt.Columns[i].ColumnName=="Level")
                 {
                     for (int j = 0; j <= dt.Rows.Count - 1;j++)
                     {
                         int Level = int.Parse(dt.Rows[j][i].ToString());
                         if (Level == 1)
                         {
                             dt.Rows[j]["名称"] = " 合       计";
                         }
                         else
                         {
                            dt.Rows[j]["名称"] = dt.Rows[j]["名称"].ToString();
                         }
                        
                     }

                 }
                                
            }
            dt.Columns.Add("空格",typeof(string));         
            for (int j = 0; j <= dt.Rows.Count - 1; j++)
            {
                int Level = int.Parse(dt.Rows[j]["Level"].ToString());
                if (Level == 1)
                {
                    dt.Rows[j]["空格"] = " ";
                }
                else if (Level == 2)
                {
                    dt.Rows[j]["空格"] = "   ";
                }
                else if (Level == 3)
                {
                    dt.Rows[j]["空格"] = "     ";
                }
                else if (Level == 4)
                {
                    dt.Rows[j]["空格"] = "       ";
                }
                else if (Level == 5)
                {
                    dt.Rows[j]["空格"] = "         ";
                }
                else if (Level == 6)
                {
                    dt.Rows[j]["空格"] = "           ";
                }
                else if (Level == 7)
                {
                    dt.Rows[j]["空格"] = "             ";
                }
            
            }
                       
            return dt;

        }

        public class TestItemNode
        {
            public string TestItemID;//----ID
            public string TestItemName;
        }

        public bool IfHaveItem_OneObject(string TestObjectID, ArrayList DataTreeList)
        {

            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;
            string Nodeid;
            int NodeType1 = 0;
            string NextBrotherID;
            string NodeContentID = "0";
            string NodeContentJXm = "";
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;
            string SaveObjectID = "";
            bool IfHaveItem = false;

            iStack = 0;

            if (DataTreeList.Count <= 0)
            {
                return IfHaveItem;
            }

            NodeTree node = (NodeTree)DataTreeList[0];

            do
            {
                while ((node != null) && (node.NodeType <= 3))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType1 = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    int btno = layer + 1;

                    if (NodeType1 == 1)
                    {
                        SaveObjectID = NodeContentID;
                    }
                    if (NodeType1 == 3)//测试项
                    {
                        if (SaveObjectID == TestObjectID)
                        {
                            IfHaveItem = true;
                            return IfHaveItem;
                        }

                    }

                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }

                }
                if (iStack == 0)
                {
                    return IfHaveItem;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }
            } while (true);
        }

        public static ArrayList GetArrayListInfo(string TestObjectID, string ArrayType, ArrayList DataTreeList)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {

            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;
            string Nodeid;
            int NodeType;

            string NextBrotherID;
            string NodeContentID;
            string NodeContentJXm;
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;

            object NumRow = 1;

            ArrayList InfoList = new ArrayList();
            Boolean returnflag = false;

            DataTable dt;

            iStack = 0;

            if (DataTreeList.Count <= 0)
            {
                return InfoList;
            }

            NodeTree node = (NodeTree)DataTreeList[0];

            do
            {
                while ((node != null) && (node.NodeType <= 4))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    if ((layer == 1) && (NodeContentID == TestObjectID))
                    {
                        returnflag = true;

                    }
                    else if ((layer == 1) && (NodeContentID != TestObjectID))
                    {
                        if (returnflag == true)
                        {
                            return InfoList;
                        }

                    }
                    if ((NodeType == 4) && (returnflag == true) && (testcaseflag == 1))//直属测试用例
                    {

                        string sqlstate;

                        if (ArrayType == "问题统计")
                        {
                            sqlstate = "SELECT CA测试过程实测表.问题报告单ID, CA问题报告单.问题类别, CA问题报告单.问题级别 " +
                                       " FROM ((CA测试用例实体表 INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID) " +
                                       " INNER JOIN (CA测试过程实体表 INNER JOIN CA测试过程实测表 ON CA测试过程实体表.ID = CA测试过程实测表.过程ID) ON " +
                                       " CA测试用例实体表.ID = CA测试过程实体表.测试用例ID) INNER JOIN CA问题报告单 ON CA测试过程实测表.问题报告单ID = CA问题报告单.ID " +
                                       " WHERE CA测试用例实测表.ID=? AND CA问题报告单.测试版本=?";

                            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, NodeContentID, TestVerID);

                            if (dt != null && dt.Rows.Count != 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    string QuestinID = "0";
                                    string QuestionBS = "";
                                    string QuestionType = "";
                                    string QuestionLevel = "";

                                    if (dr[0].ToString() != "")
                                    {
                                        QuestinID = dr[0].ToString();
                                        QuestionBS = CommonDB.GenPblSignForStep(TPM3.Sys.GlobalData.globalData.dbProject, ConstDef.PblSplitter(), QuestinID);
                                        QuestionType = dr[1].ToString();
                                        QuestionLevel = dr[2].ToString();

                                        QuestionInfo QuestionInfoNode = new QuestionInfo();
                                        InfoList = QuestionInfoNode.AddQuestionToArray(InfoList, QuestinID, QuestionBS, QuestionType, QuestionLevel, NodeContentID, NodeContent, NodeContentJXm);

                                    }

                                }

                            }
                        }
                        else if (ArrayType == "参加测试人员")
                        {
                            sqlstate = "select 测试人员 from CA测试用例实测表 where ID=? and 测试版本=?";
                            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, NodeContentID, TestVerID);

                            if (dt != null && dt.Rows.Count != 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (dr[0].ToString() != "")
                                    {
                                        ArrayList list1 = OutputComm.OutputComm.RemoveSplit(dr[0].ToString());
                                        for (int l = 0; l <= list1.Count - 1; l++)
                                        {
                                            int HaveFlag = 0;

                                            for (int m = 0; m <= InfoList.Count - 1; m++)
                                            {
                                                if (list1[l].ToString() == InfoList[m].ToString())
                                                {
                                                    HaveFlag = 1;
                                                }
                                            }
                                            if (HaveFlag == 0)
                                            {
                                                InfoList.Add(list1[l].ToString());
                                            }

                                        }

                                    }
                                }

                            }
                        }


                    }

                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }
                }

                if (iStack == 0)
                {              
                     return InfoList;
                   
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }

            } while (true);

        }

        public DataTable HGTestAccording(string QueryType,ArrayList DataTreeList, string DocName)
        {

            int iStack;
            string[] stack = new string[10];
            string YijuID = "";
            string YijuID_pre = "";
            string YijuID_firstson = "";
            string YijuID_brother = "";
            string YijuID_father = "";
            string sqlstate = "";
            string wdbs = "";

            int xuhao = 0;
            int xuhaonum = 0;

            DataTable dt;
            DataRow AddDataRow = null;
            DataTable AddDataTable = new DataTable();
            ArrayList BSlist = new ArrayList();

            DataTable YiJuTable = RequireTreeForm.GetRequireTreeTable(true, TestVerID);

            AddDataTable = AddTableColumn(QueryType, DocName);

            iStack = 0;

            sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 父节点ID='~root' order by 序号";

            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
            if (dt.Rows.Count==0)
            {
                return AddDataTable;
            }

            DataRow dr1 = dt.Rows[0];
            YijuID = dr1[2].ToString();
            AddDataRow = dr1;
                          
            if (YijuID == "")
            {
                return AddDataTable;
            }

            do
            {
                while (YijuID != "")
                {
                    ///////////////////////////////////////////////////////////////////////////                    
                        sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明,测试依据标识 FROM SYS测试依据表 where 项目ID = ? and 测试版本=? and ID=?";
                        dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, YijuID);

                        string TestYiju="";
                        string TestYijuShuoming="";
                        string TestYijuID = "" ;
                        string ZJ = "";
                        bool IfAccording = false;
                        string fatherID = "";
                        string yy = "";

                        if (dt != null && dt.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                TestYiju = dr[0].ToString();
                                TestYijuShuoming = dr[1].ToString();
                                TestYijuID = dr[2].ToString();
                                ZJ = dr[4].ToString();
                                IfAccording = (bool)dr[5];
                                fatherID = dr[3].ToString();
                                yy = dr[6].ToString();

                                if (fatherID == "~root")
                                {
                                    wdbs = dr[7].ToString();
                                    yy = "此项为不追踪项";
                                }
                                else
                                {
                                    if (IfAccording == true)
                                    {
                                        yy = "";
                                    }
                                }

                            }
                        }

                        string bs = "";
                        if (fatherID == "~root")
                        {
                            bs = wdbs;
                        }
                        else
                        {
                            //bs = wdbs + "_" + ZJ;
                            bs = GetYiJuBS(YiJuTable, TestYijuID);
                        }

                        if (QueryType == "回归测试依据")
                        {
                            DataRow dr = AddDataTable.Rows.Add();

                            AddDataTable.Rows[xuhaonum]["测试依据说明"] = TestYijuShuoming;

                            AddDataTable.Rows[xuhaonum]["测试依据标识"] = bs + "\n" + TestYiju;
                            if ((IfAccording == true) && (fatherID != "~root"))
                            {
                                AddDataTable.Rows[xuhaonum]["是否追踪"] = "是";
                                AddDataTable.Rows[xuhaonum]["未追踪原因说明"] = "";
                            }
                            else
                            {
                                AddDataTable.Rows[xuhaonum]["是否追踪"] = "否";
                                AddDataTable.Rows[xuhaonum]["未追踪原因说明"] = yy;
                            }

                            xuhaonum = xuhaonum + 1;

                        }
                        else if (QueryType =="回归测试依据与测试项的追踪关系")
                        {
                            ArrayList TestItemList = TestItem_OneTestYiJu(TestYijuID, TestVerID);

                            if (TestItemList.Count == 0)
                            {
                                 DataRow dr = AddDataTable.Rows.Add();

                                 dr["序号"] = xuhaonum + 1;
                                 dr["依据标识"] = bs;
                                 dr["测试依据"] = TestYiju;
                                 dr["测试项名称"] = "";

                                 if ((IfAccording == true) && (fatherID != "~root"))
                                 {
                                     dr["是否追踪"] = "是";
                                  }
                                 else
                                 {
                                     dr["是否追踪"] = "否";
                                     dr["测试项名称"] = "该项为不追踪项，没有相应测试项";
                                 }
                                
                            }
                            else
                            {
                                for (int j = 0; j <= TestItemList.Count - 1; j++)
                                {
                                    TestItemNode TestItemNode1 = new TestItemNode();
                                    TestItemNode1 = (TestItemNode)TestItemList[j];

                                    string TestItemName = TestItemNode1.TestItemName;
                                    string TestItemID = TestItemNode1.TestItemID;
                                    string TestItemBS = GetTestItemBS(TestItemID, DataTreeList);

                                    DataRow dr = AddDataTable.Rows.Add();

                                    dr["序号"] = xuhaonum + 1;
                                    dr["依据标识"] = bs;
                                    dr["测试依据"] = bs + "\n" + TestYiju; ;
                                   
                                    if ((IfAccording == true) && (fatherID != "~root"))
                                    {
                                        dr["是否追踪"] = "是";
                                        dr["测试项名称"] = TestItemBS + "\n" + TestItemName;
                                    }
                                    else
                                    {
                                        dr["是否追踪"] = "否";
                                        dr["测试项名称"] = "该项为不追踪项，没有相应测试项";
                                    }

                                }
                            }

                            xuhaonum = xuhaonum + 1;

                        }                   
                    ////////////////////////////////////////////////////////

                    stack[iStack] = YijuID;
                    iStack = iStack + 1;

                    YijuID_pre = YijuID;
                    YijuID_firstson = "";
                    sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID, 章节号,是否追踪 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 父节点ID=? and 序号=1";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, YijuID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_firstson = dr[2].ToString();
                        }
                    }

                    //-----push(stack,p)                                      
                    if (YijuID_firstson != "")
                    {
                        YijuID = YijuID_firstson;
                    }
                    else
                    {
                        YijuID = "";
                    }

                }
                if (iStack == 0)
                {
                    return AddDataTable;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    YijuID_pre = stack[iStack];

                    YijuID_father = "";
                    sqlstate = "SELECT 父节点ID FROM SYS测试依据表 where ID=? and 项目ID =? and 测试版本=?";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_father = dr[0].ToString();
                        }
                    }

                    sqlstate = "SELECT 序号 FROM SYS测试依据表 where ID=? and 项目ID = ? and 测试版本=?";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            xuhao = int.Parse(dr[0].ToString());
                        }
                    }

                    YijuID_brother = "";
                    sqlstate = "SELECT ID FROM SYS测试依据表 where 父节点ID=? and 序号=? and 项目ID =? and 测试版本=?";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_father, xuhao + 1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_brother = dr[0].ToString();
                        }
                    }

                    if (YijuID_brother != "")
                    {
                        YijuID = YijuID_brother;
                    }
                    else
                    {
                        YijuID = "";

                    }

                }
            } while (true);

        }

        public bool IfStrHavePoint(string Str)
        {
            bool Value = false;

            int p = Str.IndexOf(".");
            if ( p!= -1)
            {
                Value = true;
            }

            return Value;
        }

        public string GetYiJuBS(DataTable YiJuTable, string YijuID)
        {
           
            DataRow dr = GridAssist.GetDataRow(YiJuTable, "ID", YijuID);
            string Value = dr==null?"":dr["测试依据标识"].ToString();            
            return Value;
        }
                                  
        public DataTable GetTestAccordingTable(string QueryType, ArrayList DataTreeList, string CurrentDocName, string type)
        {

            int iStack;
            string[] stack = new string[10];
            string YijuID = "";
            string YijuID_pre = "";
            string YijuID_firstson = "";
            string YijuID_brother = "";
            string YijuID_father = "";
            string sqlstate = "";
            string wdbs = "";

           // int rootnum = 0;
            int xuhao = 0;
            int xuhaonum = 0;

            int TableCurrentRowNum = 0;
            int TestYiJuNum = 0;

            DataTable dt;
            DataRow AddDataRow = null;
            DataTable AddDataTable = new DataTable();
            ArrayList BSlist = new ArrayList();

            AddDataTable = AddTableColumn(QueryType, CurrentDocName);

            DataTable YiJuTable = RequireTreeForm.GetRequireTreeTable(true, TestVerID);

            iStack = 0;

            sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 依据类型=1 and 父节点ID='~root' and 序号=1";

            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    YijuID = dr[2].ToString();
                    AddDataRow = dr;

                }
            }
            if (YijuID == "")
            {
                return AddDataTable;
            }

            do
            {
                while (YijuID != "")
                {
                    ///////////////////////////////////////////////////////////////////////////
                   
                    if (QueryType == "测试依据")
                    {
                        sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明,测试依据标识 FROM SYS测试依据表 where 项目ID = ? and 测试版本=? and 依据类型=1 and ID=?";
                        dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, YijuID);

                        string TestYiju;
                        string TestYijuShuoming;
                        string TestYijuID="";
                        string ZJ = "";
                        bool IfAccording = false;
                        string fatherID = "";
                        string yy = "";
                      
                        if (dt != null && dt.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                TestYiju = dr[0].ToString();
                                TestYijuShuoming = dr[1].ToString();
                                TestYijuID = dr[2].ToString();
                                ZJ = dr[4].ToString();
                                IfAccording = (bool)dr[5];
                                fatherID = dr[3].ToString();
                                yy = dr[6].ToString();

                                if (fatherID == "~root")
                                {
                                    wdbs = dr[7].ToString();
                                    yy = "此项为不追踪项";
                                }
                                else  
                                {
                                    if (IfAccording == true)
                                    {
                                        yy = "";
                                    }
                                }
                                                              
                                AddDataTable.ImportRow(dr);
                            
                            }
                        }

                        string bs = "";
                        if (fatherID == "~root")
                        {                          
                            bs = wdbs;                            
                        }
                        else
                        {
                            //bs = wdbs + "_" + ZJ;
                            bs = GetYiJuBS(YiJuTable, TestYijuID);
                        }

                        AddDataTable.Rows[xuhaonum]["测试依据标识"] = bs;
                        if ((IfAccording == true) && (fatherID != "~root"))
                        {
                            AddDataTable.Rows[xuhaonum]["是否追踪"] = "是";
                        }
                        else
                        {
                            AddDataTable.Rows[xuhaonum]["是否追踪"] = "否";
                        }
                        AddDataTable.Rows[xuhaonum]["未追踪原因说明"] = yy;
                        xuhaonum = xuhaonum + 1;

                    }
                    else if (QueryType == "测试依据与测试项的追踪关系")
                    {
                        sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明,测试依据标识 FROM SYS测试依据表 where 项目ID = ? and 测试版本=? and 依据类型=1 and ID=?";

                        dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, YijuID);

                        bool IfAccording = false;
                        string ZJ = "";
                        string fatherID = "";
                        string yy = "";
                        string TestYijuID = "";
                        
                        DataRow AddRow = null;

                        if (dt != null && dt.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                IfAccording = (bool)dr[5];
                                ZJ = dr[4].ToString();
                                fatherID = dr[3].ToString();
                                yy = dr[6].ToString();
                                TestYijuID = dr[2].ToString();
                               
                                if (fatherID == "~root")
                                {
                                    wdbs = dr[7].ToString();
                                    yy = "此项为不追踪项";
                                }
                                else
                                {
                                    if (IfAccording == true)
                                    {
                                        yy = "";
                                    }

                                }
                                AddRow = dr;
                            }
                        }

                        string bs = "";
                        if (fatherID == "~root")
                        {
                           bs = wdbs;
                        }
                        else
                        {
                            //bs = wdbs + "_" + ZJ;
                            bs = GetYiJuBS(YiJuTable, TestYijuID);
                        }

                        TestYiJuNum = TestYiJuNum + 1;

                        AddDataTable.ImportRow(AddRow);

                        xuhaonum = xuhaonum + 1;
                        AddDataTable.Rows[TableCurrentRowNum]["序号"] = TestYiJuNum;
                        AddDataTable.Rows[TableCurrentRowNum]["依据标识"] = bs;
                        if ((IfAccording == true) && (fatherID != "~root"))
                        {
                            AddDataTable.Rows[TableCurrentRowNum]["是否追踪"] = "是";
                        }
                        else
                        {
                            AddDataTable.Rows[TableCurrentRowNum]["是否追踪"] = "否";
                        }

                        ArrayList TestItemList = TestItem_OneTestYiJu(YijuID, TestVerID);

                        for (int jj = 0; jj <= TestItemList.Count - 2; jj++)
                        {
                            AddDataTable.ImportRow(AddRow);
                        }

                        if (IfAccording == true)
                        {
                            for (int jj = 0; jj <= TestItemList.Count - 1; jj++)
                            {
                                TestItemNode TestItemNode1 = new TestItemNode();
                                TestItemNode1 = (TestItemNode)TestItemList[jj];

                                string TestItemName = TestItemNode1.TestItemName;
                                string TestItemID = TestItemNode1.TestItemID;
                                AddDataTable.Rows[TableCurrentRowNum + jj]["序号"] = xuhaonum;
                                AddDataTable.Rows[TableCurrentRowNum + jj]["依据标识"] = bs;
                                if (fatherID != "~root")
                                {
                                    AddDataTable.Rows[TableCurrentRowNum + jj]["是否追踪"] = "是";
                                }
                                else
                                {
                                    AddDataTable.Rows[TableCurrentRowNum + jj]["是否追踪"] = "否";
                                }
                                
                                if (CurrentDocName == "需求分析")
                                {
                                    AddDataTable.Rows[TableCurrentRowNum + jj]["测试项在需求的章节"] = GetTestTPOrTAOutputZJ(1, TestItemID, DataTreeList);
                                }
                                else 
                                {
                                    if (type == "定型")
                                    {
                                        AddDataTable.Rows[TableCurrentRowNum + jj]["测试项在计划的章节"] = GetTestTPOrTAOutputZJ_DX(1, TestItemID, DataTreeList);
                                    }
                                    else
                                    {
                                        AddDataTable.Rows[TableCurrentRowNum + jj]["测试项在计划的章节"] = GetTestTPOrTAOutputZJ(2, TestItemID, DataTreeList);
                                    }
                                }
                               
                                if (fatherID != "~root")
                                {
                                    AddDataTable.Rows[TableCurrentRowNum + jj]["测试项名称"] = TestItemName;
                                }
                                else
                                {
                                    AddDataTable.Rows[TableCurrentRowNum + jj]["测试项名称"] = yy;
                                }
                                

                            }
                            if (TestItemList.Count == 0)
                            {
                                if (fatherID == "~root")
                                {
                                    AddDataTable.Rows[TableCurrentRowNum]["测试项名称"] = yy;
                                }
                                TableCurrentRowNum = TableCurrentRowNum + 1;
                            }
                            else
                            {
                                TableCurrentRowNum = TableCurrentRowNum + TestItemList.Count;
                            }

                        }
                        else
                        {
                            AddDataTable.Rows[TableCurrentRowNum]["测试项名称"] = yy;
                            AddDataTable.Rows[TableCurrentRowNum]["依据标识"] = bs;
                            AddDataTable.Rows[TableCurrentRowNum]["是否追踪"] = "否";
                            TableCurrentRowNum = TableCurrentRowNum + 1;
                        }

                    }
                    ////////////////////////////////////////////////////////

                    stack[iStack] = YijuID;
                    iStack = iStack + 1;

                    YijuID_pre = YijuID;
                    YijuID_firstson = "";
                    sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID, 章节号,是否追踪 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 依据类型=1 and 父节点ID=? and 序号=1";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, YijuID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_firstson = dr[2].ToString();
                        }
                    }

                    //-----push(stack,p)                                      
                    if (YijuID_firstson != "")
                    {
                        YijuID = YijuID_firstson;
                    }
                    else
                    {
                        YijuID = "";
                    }

                }
                if (iStack == 0)
                {
                    return AddDataTable;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    YijuID_pre = stack[iStack];

                    YijuID_father = "";
                    sqlstate = "SELECT 父节点ID FROM SYS测试依据表 where ID=? and 项目ID =? and 测试版本=? and 依据类型=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_father = dr[0].ToString();
                        }
                    }

                    sqlstate = "SELECT 序号 FROM SYS测试依据表 where ID=? and 项目ID = ? and 测试版本=? and 依据类型=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            xuhao = int.Parse(dr[0].ToString());
                        }
                    }

                    YijuID_brother = "";
                    sqlstate = "SELECT ID FROM SYS测试依据表 where 父节点ID=? and 序号=? and 项目ID =? and 测试版本=? and 依据类型=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_father, xuhao + 1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_brother = dr[0].ToString();
                        }
                    }

                    if (YijuID_brother != "")
                    {
                        YijuID = YijuID_brother;
                    }
                    else
                    {
                        YijuID = "";

                    }

                }
            } while (true);

        }

        public DataTable GetTestAccordingTable_JH(string QueryType, ArrayList DataTreeList, string DocName)
        {

            int iStack;
            string[] stack = new string[10];
            string YijuID = "";
            string YijuID_pre = "";
            string YijuID_firstson = "";
            string YijuID_brother = "";
            string YijuID_father = "";
            string sqlstate = "";
            string wdbs = "";

            int xuhao = 0;
            int xuhaonum = 0;

            int TableCurrentRowNum = 0;
            
            DataTable dt;
            DataRow AddDataRow = null;
            DataTable AddDataTable = new DataTable();
            ArrayList BSlist = new ArrayList();

            AddDataTable = AddTableColumn(QueryType, DocName);

            DataTable YiJuTable = RequireTreeForm.GetRequireTreeTable(true, TestVerID);

            iStack = 0;

            sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 依据类型=1 and 父节点ID='~root' and 序号=1";

            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    YijuID = dr[2].ToString();
                    AddDataRow = dr;

                }
            }
            if (YijuID == "")
            {
                return AddDataTable;
            }

            do
            {
                while (YijuID != "")
                {
                    ///////////////////////////////////////////////////////////////////////////
                 
                  //  if (QueryType == "测试依据与测试项的追踪关系1")
                    {
                        sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明,测试依据标识 FROM SYS测试依据表 where 项目ID = ? and 测试版本=? and 依据类型=1 and ID=?";

                        dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, YijuID);

                        bool IfAccording = false;
                        string ZJ = "";
                        string fatherID = "";
                        string yy = "";
                        string TestYijuID = "";
                        string YJ = "";

                        DataRow AddRow = null;

                        if (dt != null && dt.Rows.Count != 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                YJ = dr[0].ToString();
                                IfAccording = (bool)dr[5];
                                ZJ = dr[4].ToString();
                                fatherID = dr[3].ToString();
                                yy = dr[6].ToString();
                                TestYijuID = dr[2].ToString();                            
                                AddRow = dr;
                            }
                        }

                        string bs = "";
                        if (fatherID == "~root")
                        {
                            bs = wdbs;
                        }
                        else
                        {
                            bs = GetYiJuBS(YiJuTable, TestYijuID);
                        }                    

                        if ((IfAccording == true) && (fatherID != "~root"))
                        {
                            ArrayList TestItemList = TestItem_OneTestYiJu(YijuID, TestVerID);
                            
                            for (int jj = 0; jj <= TestItemList.Count - 1; jj++)
                            {
                                if (jj == 0)
                                {
                                    xuhaonum = xuhaonum + 1;
                                }

                                AddDataTable.ImportRow(AddRow);

                                TestItemNode TestItemNode1 = new TestItemNode();
                                TestItemNode1 = (TestItemNode)TestItemList[jj];

                                string TestItemName = TestItemNode1.TestItemName;
                                string TestItemID = TestItemNode1.TestItemID;
                                
                                AddDataTable.Rows[TableCurrentRowNum + jj]["序号"] = xuhaonum;
                                AddDataTable.Rows[TableCurrentRowNum + jj]["依据标识"] = bs;
                                AddDataTable.Rows[TableCurrentRowNum + jj]["测试依据"] = YJ;
                                
                                string TestItemBS = "";
                                TestItemBS = GetTestItemBS(TestItemID, DataTreeList);
                                                                  
                                AddDataTable.Rows[TableCurrentRowNum + jj]["测试项名称"] = TestItemName;
                                AddDataTable.Rows[TableCurrentRowNum + jj]["测试项标识"] = TestItemBS;
                               
                            }
                            if (TestItemList.Count == 0)
                            {
                                AddDataTable.ImportRow(AddRow);
                                xuhaonum = xuhaonum + 1;
                                AddDataTable.Rows[TableCurrentRowNum]["序号"] = xuhaonum;
                                AddDataTable.Rows[TableCurrentRowNum]["依据标识"] = bs;
                                AddDataTable.Rows[TableCurrentRowNum]["测试依据"] = YJ;
                                AddDataTable.Rows[TableCurrentRowNum]["测试项名称"] = "";
                                AddDataTable.Rows[TableCurrentRowNum]["测试项标识"] = "";

                                TableCurrentRowNum = TableCurrentRowNum + 1;

                            }
                            else
                            {
                                TableCurrentRowNum = TableCurrentRowNum + TestItemList.Count;
                               // TableCurrentRowNum = TableCurrentRowNum + 1;
                            }

                        }
                    }
                    ////////////////////////////////////////////////////////

                    stack[iStack] = YijuID;
                    iStack = iStack + 1;

                    YijuID_pre = YijuID;
                    YijuID_firstson = "";
                    sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID, 章节号,是否追踪 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 依据类型=1 and 父节点ID=? and 序号=1";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, YijuID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_firstson = dr[2].ToString();
                        }
                    }

                    //-----push(stack,p)                                      
                    if (YijuID_firstson != "")
                    {
                        YijuID = YijuID_firstson;
                    }
                    else
                    {
                        YijuID = "";
                    }

                }
                if (iStack == 0)
                {
                    return AddDataTable;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    YijuID_pre = stack[iStack];

                    YijuID_father = "";
                    sqlstate = "SELECT 父节点ID FROM SYS测试依据表 where ID=? and 项目ID =? and 测试版本=? and 依据类型=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_father = dr[0].ToString();
                        }
                    }

                    sqlstate = "SELECT 序号 FROM SYS测试依据表 where ID=? and 项目ID = ? and 测试版本=? and 依据类型=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            xuhao = int.Parse(dr[0].ToString());
                        }
                    }

                    YijuID_brother = "";
                    sqlstate = "SELECT ID FROM SYS测试依据表 where 父节点ID=? and 序号=? and 项目ID =? and 测试版本=? and 依据类型=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_father, xuhao + 1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_brother = dr[0].ToString();
                        }
                    }

                    if (YijuID_brother != "")
                    {
                        YijuID = YijuID_brother;
                    }
                    else
                    {
                        YijuID = "";

                    }

                }
            } while (true);

        }

        public DataTable Output_TreeTypeDataTable(string QueryType, ArrayList DataTreeList, string DocName, string type)
        {

            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;

            string Nodeid;
            int NodeType = 0;

            string NextBrotherID;
            string NodeContentID = "0";
            string NodeContentJXm = "";
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;
            int TableCurrentRowNum = 0;
            int TestItemNum = 0;

            iStack = 0;

            DataTable AddDataTable=new DataTable();

            if (DataTreeList.Count <= 0)
            {
                return AddDataTable;
            }
            AddDataTable = AddTableColumn(QueryType, DocName);

            NodeTree node = (NodeTree)DataTreeList[0];

            do
            {
                while ((node != null) && (node.NodeType <= 3))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    if (NodeType == 3)//测试项
                    {

                        TestItemNum = TestItemNum + 1;

                        ArrayList TestAccordingList = GetTestAccording(NodeContentID);
                      

                        TableCurrentRowNum = TableCurrentRowNum + 1;

                        if (QueryType == "测试项与测试依据的追踪关系")
                        {
                            ArrayList TestAccordingListBS = GetTestAccordingBS(NodeContentID, DataTreeList, 0);

                            if (TestAccordingList.Count >= 1)
                            {
                                for (int i = 0; i <= TestAccordingList.Count - 1; i++)
                                {
                                    DataRow dr = AddDataTable.Rows.Add();

                                    AddDataTable.Rows[TableCurrentRowNum - 1]["序号"] = TestItemNum;
                                    if (DocName == "需求分析")
                                    {
                                        AddDataTable.Rows[TableCurrentRowNum - 1]["测试项在需求的章节"] = GetTestTPOrTAOutputZJ(1, NodeContentID, DataTreeList);
                                    }
                                    else //if (DocName == "测试计划")
                                    {
                                        if (type == "定型")
                                        {
                                            AddDataTable.Rows[TableCurrentRowNum - 1]["测试项在计划的章节"] = GetTestTPOrTAOutputZJ_DX(2, NodeContentID, DataTreeList);
                                        }
                                        else
                                        {
                                            AddDataTable.Rows[TableCurrentRowNum - 1]["测试项在计划的章节"] = GetTestTPOrTAOutputZJ(2, NodeContentID, DataTreeList);
                                        }

                                    }
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["测试项名称"] = NodeContent;
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["测试项标识"] = NodeContentJXm;
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["测试依据"] = TestAccordingListBS[i].ToString() + "\n" + TestAccordingList[i].ToString();

                                    if (i != TestAccordingList.Count - 1)
                                    {
                                        TableCurrentRowNum = TableCurrentRowNum + 1;
                                    }

                                }
                            }
                            else
                            {
                                DataRow dr = AddDataTable.Rows.Add();
                                AddDataTable.Rows[TableCurrentRowNum - 1]["序号"] = TestItemNum;
                                if (DocName == "需求分析")
                                {
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["测试项在需求的章节"] = GetTestTPOrTAOutputZJ(1, NodeContentID, DataTreeList);
                                }
                                else //if (DocName == "测试计划")
                                {
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["测试项在计划的章节"] = GetTestTPOrTAOutputZJ(2, NodeContentID, DataTreeList);

                                }
                                AddDataTable.Rows[TableCurrentRowNum - 1]["测试项名称"] = NodeContent;
                                AddDataTable.Rows[TableCurrentRowNum - 1]["测试项标识"] = NodeContentJXm;
                                AddDataTable.Rows[TableCurrentRowNum - 1]["测试依据"] = "";
                            }


                        }
                        else if (QueryType == "回归测试项与测试依据的追踪关系")
                        {
                            ArrayList TestAccordingListBS = GetTestAccordingBS(NodeContentID, DataTreeList, 1);
                          
                            if (TestAccordingList.Count >= 1)
                            {
                                for (int i = 0; i <= TestAccordingList.Count - 1; i++)
                                {
                                    DataRow dr = AddDataTable.Rows.Add();

                                    AddDataTable.Rows[TableCurrentRowNum - 1]["序号"] = TestItemNum;

                                    AddDataTable.Rows[TableCurrentRowNum - 1]["测试项"] = NodeContentJXm + "\n" + NodeContent;
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["测试依据"] = TestAccordingListBS[i].ToString() + "\n" + TestAccordingList[i].ToString();

                                    if (i != TestAccordingList.Count - 1)
                                    {
                                        TableCurrentRowNum = TableCurrentRowNum + 1;
                                    }

                                }
                            }
                            else
                            {
                                DataRow dr = AddDataTable.Rows.Add();
                                AddDataTable.Rows[TableCurrentRowNum - 1]["序号"] = TestItemNum;

                                AddDataTable.Rows[TableCurrentRowNum - 1]["测试项"] = NodeContentJXm + "\n" + NodeContent;
                                
                                AddDataTable.Rows[TableCurrentRowNum - 1]["测试依据"] = "";
                            }



                        }

                        
                    }

                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }

                }
                if (iStack == 0)
                {
                    return AddDataTable;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }
            } while (true);

        }

        public ArrayList GetTestCaseNum_AccordingType(string TestObjectID, ArrayList DataTreeList, string TypeStr)
        {
            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;
            string Nodeid;
            int NodeType;

            string NextBrotherID;
            string NodeContentID;
            string NodeContentJXm;
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;

            ArrayList TestCaseNumList = new ArrayList();
            Boolean returnflag = false;

            int TestItemNum = 0;
            int TestcaseTotelNum = 0;
            int TestcaseAllExecuteNum = 0;
            int TestcaseAllExecuteNum_pass = 0;
            int TestcaseAllExecuteNum_nopass = 0;
            int TestcasePartExecuteNum = 0;
            int TestcasePartExecuteNum_pass = 0;
            int TestcasePartExecuteNum_nopass = 0;
            int TestcaseNoExecuteNum = 0;

            iStack = 0;
            DataTable dt;

            if (DataTreeList.Count <= 0)
            {
                return TestCaseNumList;
            }

            NodeTree node = (NodeTree)DataTreeList[0];

            do
            {
                while ((node != null) && (node.NodeType <= 4))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    if ((layer == 1) && (NodeContentID == TestObjectID))
                    {
                        returnflag = true;

                    }
                    else if ((layer == 1) && (NodeContentID != TestObjectID))
                    {
                        if (returnflag == true)
                        {
                            TestCaseNumList.Add(TestcaseTotelNum);
                            TestCaseNumList.Add(TestcaseAllExecuteNum);
                            TestCaseNumList.Add(TestcaseAllExecuteNum_pass);
                            TestCaseNumList.Add(TestcaseAllExecuteNum_nopass);
                            TestCaseNumList.Add(TestcasePartExecuteNum);
                            TestCaseNumList.Add(TestcasePartExecuteNum_pass);
                            TestCaseNumList.Add(TestcasePartExecuteNum_nopass);
                            TestCaseNumList.Add(TestcaseNoExecuteNum);

                            return TestCaseNumList;

                        }
                    }

                     if (NodeType == 3)
                     {
                         TestItemNum = TestItemNum + 1;//测试项总数
                     }

                    if ((NodeType == 4) && (returnflag == true))//测试用例
                    {
                        if (testcaseflag == 1)
                        {
                            TestcaseTotelNum = TestcaseTotelNum + 1;//测试用例总数

                            string ExecuteInfo = "";
                            string PassInfo = "";

                            string sqlstate = "select 执行状态,执行结果 from CA测试用例实测表 where ID=?";

                            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, NodeContentID);
                            if (dt != null && dt.Rows.Count != 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    ExecuteInfo = dr[0].ToString();
                                    PassInfo = dr[1].ToString();

                                }
                            }
                            if (ExecuteInfo == "完整执行")
                            {
                                TestcaseAllExecuteNum = TestcaseAllExecuteNum + 1;//全部执行的测试用例数

                                if (PassInfo == "通过")
                                {
                                    TestcaseAllExecuteNum_pass = TestcaseAllExecuteNum_pass + 1;//全部执行通过的用例数
                                }
                                else if (PassInfo == "未通过")
                                {
                                    TestcaseAllExecuteNum_nopass = TestcaseAllExecuteNum_nopass + 1;//全部执行未通过的用例数
                                }
                            }
                            else if (ExecuteInfo == "部分执行")
                            {
                                TestcasePartExecuteNum = TestcasePartExecuteNum + 1;//部分执行的用例数

                                if (PassInfo == "通过")
                                {
                                    TestcasePartExecuteNum_pass = TestcasePartExecuteNum_pass + 1;//部分执行通过的用例数
                                }
                                else if (PassInfo == "未通过")
                                {
                                    TestcasePartExecuteNum_nopass = TestcasePartExecuteNum_nopass + 1;//部分执行未通过的用例数
                                }
                            }
                            else if (ExecuteInfo == "未执行")
                            {
                                TestcaseNoExecuteNum = TestcaseNoExecuteNum + 1;//未执行的用例数
                            }

                        }
                    }

                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }
                }

                if (iStack == 0)
                {
                    if (TypeStr == "定型")
                    {
                        TestCaseNumList.Add(TestItemNum);
                    }
                    
                    TestCaseNumList.Add(TestcaseTotelNum);
                    TestCaseNumList.Add(TestcaseAllExecuteNum);
                    TestCaseNumList.Add(TestcaseAllExecuteNum_pass);
                    TestCaseNumList.Add(TestcaseAllExecuteNum_nopass);
                    TestCaseNumList.Add(TestcasePartExecuteNum);
                    TestCaseNumList.Add(TestcasePartExecuteNum_pass);
                    TestCaseNumList.Add(TestcasePartExecuteNum_nopass);
                    TestCaseNumList.Add(TestcaseNoExecuteNum);

                    return TestCaseNumList;

                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }

            } while (true);
        }

        public ArrayList TestItem_OneTestYiJu(string YijuID,string TestVerID)
        {

            bool Flag = true;
            ArrayList TestItemOneTestYiJuList = new ArrayList();

            //string sqlstate  "SELECT CA测试项实测表.ID, CA测试项实体表.测试项名称, CA测试项实测表.序号, CA测试项实测表.追踪关系, CA测试项实测表.测试版本, CA测试项实测表.项目ID " +
            //                  " FROM CA测试项实体表 INNER JOIN CA测试项实测表 ON CA测试项实体表.ID = CA测试项实测表.测试项ID " + " WHERE CA测试项实测表.测试版本 =?" +
            //                  " =AND CA测试项实测表.项目ID =? ORDER BY CA测试项实测表.序号,CA测试项实测表.ID,CA测试项实测表.序号, CA测试项实测表.追踪关系;";

            //string sqlstate = "SELECT CA测试项实测表.ID, CA测试项实体表.测试项名称, CA测试项实测表.序号, CA测试项实测表.追踪关系, CA测试项实测表.测试版本, CA测试项实测表.项目ID, " +
            //                  " CA测试类型实测表.序号 FROM (CA测试类型实体表 INNER JOIN (CA测试项实体表 INNER JOIN CA测试项实测表 ON " +
            //                  " CA测试项实体表.ID = CA测试项实测表.测试项ID) ON CA测试类型实体表.ID = CA测试项实体表.所属测试类型ID) INNER JOIN " +
            //                  " CA测试类型实测表 ON CA测试类型实体表.ID = CA测试类型实测表.测试类型ID WHERE CA测试项实测表.测试版本=? AND CA测试项实测表.项目ID=? ORDER BY CA测试类型实测表.序号, CA测试项实测表.序号;";

            string sqlstate = "SELECT CA测试项实测表.ID, CA测试项实体表.测试项名称, CA测试项实测表.序号, CA测试项实测表.追踪关系, CA测试项实测表.测试版本, CA测试项实测表.项目ID" +
                              " FROM CA测试项实体表 INNER JOIN CA测试项实测表 ON CA测试项实体表.ID = CA测试项实测表.测试项ID " + " WHERE CA测试项实测表.测试版本=? " +
                              " AND CA测试项实测表.项目ID=?";
            
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestVerID, TPM3.Sys.GlobalData.globalData.projectID.ToString());

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Relation = dr[3].ToString();
                    string TestIteName = dr[1].ToString();
                    string ID = dr[0].ToString();

                    int position = Relation.IndexOf(YijuID);

                    if (position != -1)//测试项包含该追踪关系
                    {
                        for (int i = 0; i <= TestItemOneTestYiJuList.Count - 1; i++)
                        {
                            TestItemNode TestItemNode1 = new TestItemNode();
                            TestItemNode1 = (TestItemNode)TestItemOneTestYiJuList[i];

                            if (TestItemNode1.TestItemID == ID)
                            {
                                Flag = false;
                            }
                            else
                            {
                                Flag = true;
                            }
                        }

                        if (Flag == true)
                        {
                            TestItemNode TestItemNode1 = new TestItemNode();
                            TestItemNode1.TestItemID = ID;
                            TestItemNode1.TestItemName = TestIteName;
                            TestItemOneTestYiJuList.Add(TestItemNode1);
                        }

                    }
                }

            }

            return TestItemOneTestYiJuList;

        }

        public DataTable TestAccordingStat_ElseChange(string TableName,string DocName)
        {
            int no = 0;

            DataTable ValueTable = new DataTable();

            DataTable YiJuTable = RequireTreeForm.GetRequireTreeTable(true, TestVerID);

            ValueTable = AddTableColumn("其他更动涉及依据", "");

            string sqlstate = "select * from HG软件更动表 where 项目ID=? and 测试版本=? order by 序号;";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    no = no + 1;
                    string ChangeName = dr["更动名称"].ToString();
                    string ChangeDetil = dr["更动说明"].ToString();
                    string Analyzed = dr["ID"].ToString();// dr["影响域分析"].ToString();
                    //string TestAccordingStr = dr["相关测试依据"].ToString();
                    //ArrayList AccordIDList = GetStr_ToList(TestAccordingStr);
                    string TestAccordingStr1 = dr["相关测试依据"].ToString();
                    string TestAccordingStr2 = dr["本版本相关依据"].ToString();

                    ArrayList AccordIDList1 = GetStr_ToList(TestAccordingStr1);
                    ArrayList AccordIDList2 = GetStr_ToList(TestAccordingStr2);
                    ArrayList AccordIDList = AccordIDList1;

                    if (AccordIDList2.Count!=0)

                    {
                         string id = "";
                         string id2 = "";
                         bool flag = false;
                         for (int j = 0; j <= AccordIDList2.Count - 1; j++)
                         {
                               flag = false;
                               id2 = AccordIDList2[j].ToString();
                               for (int i = 0; i <= AccordIDList.Count - 1; i++)
                               {
                                    id = AccordIDList[i].ToString();
                                    if (id2 == id)
                                    {
                                         flag = true;
                                    }
                                }
                               if (flag == false)
                               {
                                    AccordIDList.Add(id2);
                               }
                           }

                    }
                    

                    for (int i = 0; i <= AccordIDList.Count - 1; i++)
                    {
                        string AccordID = AccordIDList[i].ToString();

                    
                        string bs = GetYiJuBS(YiJuTable, AccordID);

                        string sqlstate1 = "select * from SYS测试依据表 where ID =?";
                        DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, AccordID);

                        if (dt1 != null && dt1.Rows.Count != 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                string According = dr1["测试依据"].ToString();
                                bool IfAccording = (bool)dr1["是否追踪"];
                                string fatherID = dr1["父节点ID"].ToString();
                                string YY = dr1["未追踪原因说明"].ToString();

                                DataRow dr2 = ValueTable.Rows.Add();
                               
                                dr2["序号"] = no.ToString();
                                dr2["更动名称"] = ChangeName;
                                dr2["更动说明"] = ChangeDetil;
                                dr2["其它更动影响域分析"] = Analyzed;
                                dr2["相关测试依据"] =  According;
                            
                            }

                        }

                    }
                }

            }

      
            return ValueTable;

        }

        public ArrayList GetStr_ToList(string TestAccordingStr)
        {

            ArrayList AccordingList = new ArrayList();

            if (TestAccordingStr == "")
            {
                return AccordingList;
            }
            else
            {
                int position1 = TestAccordingStr.IndexOf(",");

                while (position1 != -1)
                {
                    string Values11 = TestAccordingStr.Substring(0, position1);

                    AccordingList.Add(Values11);

                    TestAccordingStr = TestAccordingStr.Substring(position1 + 1, TestAccordingStr.Length - position1 - 1);

                    position1 = TestAccordingStr.IndexOf(",");

                }

                AccordingList.Add(TestAccordingStr);

            }
                
            return AccordingList;

        }
        public String GetPreVersion()
        {
            string ver = "";
            
            string sqlstate = "select 前向版本ID from SYS测试版本表 where 项目ID=? and ID=?";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
            
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ver = dr[0].ToString();
                }
            }

            return ver;

        }
       
        public ArrayList GetQuestionList(string PreVer)
        {
            ArrayList QuestionList = new ArrayList();

            string sqlstate = "select * from CA问题报告单 where 项目ID=? and 测试版本=?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), PreVer);

            if (dt != null && dt.Rows.Count != 0)
            {
                string QuestionID = "";
                foreach (DataRow dr in dt.Rows)
                {
                    QuestionID = dr["ID"].ToString();
                    QuestionList.Add(QuestionID);
                }
               
            }

            return QuestionList;

        }

        public ArrayList Add_ExtraAccord(ArrayList According_OneQuestionList, string QuestionID, string PreVer)
        {
             string sqlstate = "select 附加依据 from CA问题报告单 where 项目ID=? and 测试版本=? and ID=?";
             DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), PreVer, QuestionID);
             if (dt != null && dt.Rows.Count != 0)
             {
                 foreach (DataRow dr in dt.Rows)
                 {
                     string ExtraAccord = dr[0].ToString();
                     ArrayList AccordIDList = GetStr_ToList(ExtraAccord);

                     for (int i = 0; i <= AccordIDList.Count - 1;i++ )
                     {
                         string SingleExtra = AccordIDList[i].ToString();
                         bool haveflag = false;
                         for (int j = 0; j <= According_OneQuestionList.Count - 1;j++ )
                         {
                             string According = According_OneQuestionList[j].ToString();
                             if (SingleExtra == According)
                             {
                                 haveflag = true;
                                 break;
                             }

                         }
                         if (haveflag == false)
                         {
                             According_OneQuestionList.Add(SingleExtra);

                         }


                     }
                 }
             }

             return According_OneQuestionList;



        }

        public string GetQuestionName(string QuestionID)
        {
            string QuestionName = "";

            string sqlstate = "select * from CA问题报告单 where ID=?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, QuestionID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    QuestionName = dr["名称"].ToString();
                }
            }

            return QuestionName;

        }
       
        public DataTable TestAccordingStat_Question(string TableName, string DocName)
        {
            int no = 0;
           
      
            DataTable ValueTable = new DataTable();

            ValueTable = AddTableColumn("问题涉及依据", "");

            string PreVer = GetPreVersion();

            ArrayList QuestionList = GetQuestionList(PreVer);

            for (int i = 0; i <= QuestionList.Count - 1;i++ )
            {
                string QuestionID = QuestionList[i].ToString();
                string QuestionName = GetQuestionName(QuestionID);
                string QuestionBS = CommonDB.GenPblSignForStep(TPM3.Sys.GlobalData.globalData.dbProject, ConstDef.PblSplitter(), QuestionID);
                int CS=0;
                string YX = "";
                string AccordStr = "";
       
                ArrayList According_OneQuestionList = new ArrayList();
                bool Flag = false;
             
               // no = no + 1;
                                
                string sqlstate = "SELECT CA测试过程实测表.问题报告单ID, CA测试用例实测表.ID, CA测试用例实体表.测试用例名称, CA问题报告单.测试版本, CA问题报告单.处理措施, CA问题报告单.影响域分析, CA问题报告单.问题名称, CA测试项实体表.测试项名称, CA测试项实测表.追踪关系, CA测试用例实测表.测试版本, CA测试用例实测表.项目ID " +
                                 " FROM CA测试项实体表 INNER JOIN ((((CA测试用例实体表 INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID) INNER JOIN (CA测试过程实体表 INNER JOIN CA测试过程实测表 ON CA测试过程实体表.ID = CA测试过程实测表.过程ID) ON CA测试用例实体表.ID = CA测试过程实体表.测试用例ID) INNER JOIN CA问题报告单 ON CA测试过程实测表.问题报告单ID = CA问题报告单.ID) INNER JOIN (CA测试用例与测试项关系表 INNER JOIN CA测试项实测表 ON CA测试用例与测试项关系表.测试项ID = CA测试项实测表.ID) ON CA测试用例实测表.ID = CA测试用例与测试项关系表.测试用例ID) ON CA测试项实体表.ID = CA测试项实测表.测试项ID " +
                                 " WHERE (((CA问题报告单.测试版本)=?) AND ((CA问题报告单.项目ID)=?) AND ((CA测试用例实测表.测试版本)=?) AND ((CA测试用例实测表.项目ID)=?) AND ((CA问题报告单.ID)=?)) ORDER BY CA测试过程实测表.问题报告单ID, CA测试用例实测表.ID;";

                DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, PreVer, TPM3.Sys.GlobalData.globalData.projectID.ToString(), PreVer, TPM3.Sys.GlobalData.globalData.projectID.ToString(), QuestionID);
                if (dt != null && dt.Rows.Count != 0)
                {
                     foreach (DataRow dr in dt.Rows)
                     {

                         if (dr["处理措施"] is int)
                         {
                             CS = (int)(dr["处理措施"]);
                         }
                         else
                         {
                             CS = 0;
                         }
                         
                          YX = QuestionID;//dr["影响域分析"].ToString();
                       
                          AccordStr = dr["追踪关系"].ToString();
                          ArrayList AccordIDList = GetStr_ToList(AccordStr);

                          for (int j = 0; j <= AccordIDList.Count - 1; j++)
                          {
                              Flag = false;
                              string AccordID = AccordIDList[j].ToString();
                              for (int k = 0;k <= According_OneQuestionList.Count - 1;k++)
                              {
                                  string Alreadyhave = According_OneQuestionList[k].ToString();
                                  if (AccordID == Alreadyhave)
                                  {
                                      Flag = true;
                                  }
                              }
                              if (Flag == false)
                              {
                                  According_OneQuestionList.Add(AccordID);
                              }

                          }

                          According_OneQuestionList = Add_ExtraAccord(According_OneQuestionList, QuestionID, PreVer);
                         //待增加一个依据排序

                     }
                    
                         for (int m = 0; m <= According_OneQuestionList.Count-1; m++)
                         {

                                if (CS == 0)
                                {
                                    //dr2["处理措施"] = "未更动";
                                    //dr2["影响域分析"] = " ";
                                    //dr2["测试依据"] = "";
                                   

                                   
                                }
                                else if (CS == 1)
                                {

                                    no = no + 1;

                                    DataRow dr2 = ValueTable.Rows.Add();

                                    dr2["序号"] = no.ToString();
                                    if (m == According_OneQuestionList.Count - 1)
                                    {
                                        dr2["软件问题"] = QuestionBS + "\n" + QuestionName;
                                    }
                                    else
                                    {
                                        dr2["软件问题"] = QuestionBS + "\n" + QuestionName;
                                    }
                                   
                                    dr2["处理措施"] = "更动";
                                    dr2["影响域分析"] = YX;

                                    string sqlstate1 = "select * from SYS测试依据表 where ID =?";
                                    DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, According_OneQuestionList[m].ToString());
                                    if ((dt1 != null) && (dt1.Rows.Count > 0))
                                    {
                                        DataRow dr1 = dt1.Rows[0];
                                        string According = dr1["测试依据"].ToString();

                                        dr2["测试依据"] = According;
                                    }
                                    
                                }

                               
                               

                         }                   
                }
            }

            return ValueTable;

        }

        public DataTable TestAccordingStat_Question_DX(string TableName, string DocName, string PreVer)
        {
            int no = 0;

            DataTable ValueTable = new DataTable();

            ValueTable = AddTableColumn("问题涉及依据", "");

            ArrayList QuestionList = GetQuestionList(PreVer);

            for (int i = 0; i <= QuestionList.Count - 1; i++)
            {
                string QuestionID = QuestionList[i].ToString();
                string QuestionName = GetQuestionName(QuestionID);
                string QuestionBS = CommonDB.GenPblSignForStep(TPM3.Sys.GlobalData.globalData.dbProject, ConstDef.PblSplitter(), QuestionID);
                int CS = 0;
                string YX = "";
                string AccordStr = "";

                ArrayList According_OneQuestionList = new ArrayList();
                bool Flag = false;

                string sqlstate = "SELECT CA测试过程实测表.问题报告单ID, CA测试用例实测表.ID, CA测试用例实体表.测试用例名称, CA问题报告单.测试版本, CA问题报告单.处理措施, CA问题报告单.影响域分析, CA问题报告单.问题名称, CA测试项实体表.测试项名称, CA测试项实测表.追踪关系, CA测试用例实测表.测试版本, CA测试用例实测表.项目ID " +
                                 " FROM CA测试项实体表 INNER JOIN ((((CA测试用例实体表 INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID) INNER JOIN (CA测试过程实体表 INNER JOIN CA测试过程实测表 ON CA测试过程实体表.ID = CA测试过程实测表.过程ID) ON CA测试用例实体表.ID = CA测试过程实体表.测试用例ID) INNER JOIN CA问题报告单 ON CA测试过程实测表.问题报告单ID = CA问题报告单.ID) INNER JOIN (CA测试用例与测试项关系表 INNER JOIN CA测试项实测表 ON CA测试用例与测试项关系表.测试项ID = CA测试项实测表.ID) ON CA测试用例实测表.ID = CA测试用例与测试项关系表.测试用例ID) ON CA测试项实体表.ID = CA测试项实测表.测试项ID " +
                                 " WHERE (((CA问题报告单.测试版本)=?) AND ((CA问题报告单.项目ID)=?) AND ((CA测试用例实测表.测试版本)=?) AND ((CA测试用例实测表.项目ID)=?) AND ((CA问题报告单.ID)=?)) ORDER BY CA测试过程实测表.问题报告单ID, CA测试用例实测表.ID;";

                DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, PreVer, TPM3.Sys.GlobalData.globalData.projectID.ToString(), PreVer, TPM3.Sys.GlobalData.globalData.projectID.ToString(), QuestionID);
                if (dt != null && dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        if (dr["处理措施"] is int)
                        {
                            CS = (int)(dr["处理措施"]);
                        }
                        else
                        {
                            CS = 0;
                        }

                        YX = QuestionID;//dr["影响域分析"].ToString();

                        AccordStr = dr["追踪关系"].ToString();
                        ArrayList AccordIDList = GetStr_ToList(AccordStr);

                        for (int j = 0; j <= AccordIDList.Count - 1; j++)
                        {
                            Flag = false;
                            string AccordID = AccordIDList[j].ToString();
                            for (int k = 0; k <= According_OneQuestionList.Count - 1; k++)
                            {
                                string Alreadyhave = According_OneQuestionList[k].ToString();
                                if (AccordID == Alreadyhave)
                                {
                                    Flag = true;
                                }
                            }
                            if (Flag == false)
                            {
                                According_OneQuestionList.Add(AccordID);
                            }

                        }

                        According_OneQuestionList = Add_ExtraAccord(According_OneQuestionList, QuestionID, PreVer);
                        //待增加一个依据排序

                    }

                    for (int m = 0; m <= According_OneQuestionList.Count - 1; m++)
                    {
                            no = no + 1;

                            DataRow dr2 = ValueTable.Rows.Add();

                            dr2["序号"] = no.ToString();
                            if (m == According_OneQuestionList.Count - 1)
                            {
                                dr2["软件问题"] = QuestionBS + "\n" + QuestionName;
                            }
                            else
                            {
                                dr2["软件问题"] = QuestionBS + "\n" + QuestionName;
                            }

                            dr2["处理措施"] = "更动";
                            dr2["影响域分析"] = YX;

                            string sqlstate1 = "select * from SYS测试依据表 where ID =?";
                            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, According_OneQuestionList[m].ToString());
                            if ((dt1 != null) && (dt1.Rows.Count > 0))
                            {
                                DataRow dr1 = dt1.Rows[0];
                                string According = dr1["测试依据"].ToString();

                                dr2["测试依据"] = According;
                            }

                    }
                }
            }

            return ValueTable;

        }

        public class TestCaseInfo
        {
           public string TestCaseID;
           public string TestCaseName;
           public string TestCaseBS;

        }

        public ArrayList GetCurrentVer_TestCaseList(ArrayList DataTreeList)
        {

            ArrayList TestCaseList = new ArrayList();

            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;
            string Nodeid;
            int NodeType1 = 0;

            string NextBrotherID;
            string NodeContentID = "0";
            string NodeContentJXm = "";
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;
           
            iStack = 0;
         
            if (DataTreeList.Count <= 0)
            {
                return TestCaseList;
            }

            NodeTree node = (NodeTree)DataTreeList[0];

            do
            {
                while ((node != null) && (node.NodeType <= 4))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType1 = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    int btno = layer + 1;

                    if (NodeType1 == 4)
                    {                      
                            if (testcaseflag == 1)//实例
                            {
                                TestCaseInfo TestCaseInfo1 = new TestCaseInfo();
                                TestCaseInfo1.TestCaseID = NodeContentID;
                                TestCaseInfo1.TestCaseName = NodeContent;
                                TestCaseInfo1.TestCaseBS = NodeContentJXm;

                                TestCaseList.Add(TestCaseInfo1);
                               
                            }
                 
                       
                    }

                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }

                }
                if (iStack == 0)
                {
                    return TestCaseList;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }
            } while (true);

        }

        public DataTable GD_ChangeError_DX(string Testverid)
        {

            DataTable Table_GDChangeError = new DataTable();
            Table_GDChangeError = AddTableColumn("纠错性更动_定型", "");

            string QuestineID = "";
            string QuestionName = "";
            int xuhao = 0;
            bool IfChange = false;

            string sqlstate1 = "select * from HG软件更动表 where 项目ID = ? and 测试版本= ? and 更动类型=? order by 序号";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), Testverid, "1");

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    DataRow dr = Table_GDChangeError.Rows.Add();

                    xuhao = xuhao + 1;
                    dr["序号"] = xuhao;


                    QuestineID = dr1["问题单标识"].ToString();
                    if (dr1["问题单名称"] != null)
                    {
                        QuestionName = dr1["问题单名称"].ToString();
                    }
                    dr["软件问题"] = QuestineID + "\n" + QuestionName;

                    IfChange = (bool)dr1["是否更动"];
                    if (IfChange == false)
                    {
                        dr["是否更动"] = "否";
                        if (dr1["未更动说明"] != null)
                        {
                            dr["未更动原因说明"] = dr1["未更动说明"].ToString();
                        }
                        else
                        {
                            dr["未更动原因说明"] = "";
                        }
                     //   dr["更动标识"] = "";
                        dr["更动说明"] = "";
                    }
                    else
                    {
                        dr["是否更动"] = "是";
                        //if (dr1["更动标识"] != null)
                        //{
                        //    dr["更动标识"] = dr1["更动标识"].ToString();
                        //}
                        //else
                        //{
                        //    dr["更动标识"] = "";
                        //}

                        if (dr1["更动说明"] != null)
                        {
                            dr["更动说明"] = dr1["更动说明"].ToString();
                        }
                        else
                        {
                            dr["更动说明"] = "";
                        }
                        dr["未更动原因说明"] = "";
                        if (dr1["影响域分析"] != null)
                        {
                            dr["影响域分析"] = dr1["ID"].ToString();

                        }
                        else
                        {
                            dr["影响域分析"] = "";
                        }

                    }

                }
            }

            return Table_GDChangeError;
        }



        public DataTable GD_ChangeError()
        {

            DataTable Table_GDChangeError = new DataTable();
            Table_GDChangeError = AddTableColumn("纠错性更动", "");

            string QuestineID = "";
            string QuestionName = "";
            int xuhao = 0;
            bool IfChange = false;

            string sqlstate1 = "select * from HG软件更动表 where 项目ID = ? and 测试版本= ? and 更动类型=? order by 序号";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, "1");

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    DataRow dr =Table_GDChangeError.Rows.Add();

                    xuhao = xuhao + 1;
                    dr["序号"] = xuhao;
                    

                    QuestineID = dr1["问题单标识"].ToString();
                    if (dr1["问题单名称"] != null)
                    {
                        QuestionName = dr1["问题单名称"].ToString();   
                    }
                    dr["软件问题"] = QuestineID + "\n" + QuestionName;

                    IfChange= (bool)dr1["是否更动"];
                    if (IfChange == false)
                    {
                        dr["是否更动"] ="否";
                        if (dr1["未更动说明"]!= null)
                        {
                             dr["未更动原因说明"] =  dr1["未更动说明"].ToString();
                        }
                        else 
                        {
                            dr["未更动原因说明"] = "";
                        }
                        dr["更动标识"] = "";
                        dr["更动说明"] = "";
                    }
                    else 
                    {
                         dr["是否更动"] ="是";
                         if (dr1["更动标识"]!= null)
                         {
                            dr["更动标识"] = dr1["更动标识"].ToString();
                         }
                         else
                         {
                            dr["更动标识"] = "";
                         }
                     
                        if ( dr1["更动说明"]!=null)
                        {
                            dr["更动说明"] = dr1["更动说明"].ToString();
                        }
                        else
                        {
                            dr["更动说明"] = "";
                        }  
                        dr["未更动原因说明"] ="";

                    }
                                   
                }
            }

            return  Table_GDChangeError;
        }


        public DataTable GD_Change(string changetype)
        {

            DataTable Table_Change = new DataTable();
            Table_Change = AddTableColumn(changetype, "");
            
            int xuhao = 0;
      
            string sqlstate1 = "select * from HG软件更动表 where 项目ID = ? and 测试版本= ? and 更动类型=? order by 序号";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, changetype);

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    DataRow dr = Table_Change.Rows.Add();

                    xuhao = xuhao + 1;
                    dr["序号"] = xuhao;

                    if (dr1["更动标识"] != null)
                    {
                        dr["更动标识"] = dr1["更动标识"].ToString();
                    }
                    else
                    {
                        dr["更动标识"] = "";
                    }

                    if (dr1["更动说明"] != null)
                    {
                        dr["更动说明"] = dr1["更动说明"].ToString();
                    }
                    else
                    {
                        dr["更动说明"] = "";
                    }
                    
                }
            }

            return Table_Change;
        }


        public DataTable InfectionAnaly()
        {

            DataTable Table_InfectionAnaly = new DataTable();
            Table_InfectionAnaly = AddTableColumn("影响域分析", "");

            int xuhao = 0;

            DataRow dr = null;

            string sqlstate1 = "select * from HG软件更动表 where 项目ID = ? and 测试版本= ? and 是否更动=true and 更动类型='1' order by 序号";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    string QuestionID = dr1["问题单ID"].ToString();
             
                    dr = Table_InfectionAnaly.Rows.Add();

                    xuhao = xuhao + 1;
                    dr["序号"] = xuhao;

                    if (dr1["更动标识"] != null)
                    {
                        dr["更动标识"] = dr1["更动标识"].ToString();
                    }
                    else
                    {
                        dr["更动标识"] = "";
                    }

                    if (dr1["影响域分析"] != null)
                    {
                        dr["影响域分析"] = dr1["ID"].ToString();

                    }
                    else
                    {
                        dr["影响域分析"] = "";
                    }

                    if (dr1["测试要求"] != null)
                    {
                        dr["测试要求"] = dr1["测试要求"].ToString();
                    }
                    else
                    {
                        dr["测试要求"] = "";
                    }

                    string ItemStr = GetTestItem_AllQuestion_2(QuestionID);
                    dr["测试项名称"] = ItemStr;


                }

            }

            sqlstate1 = "select * from HG软件更动表 where 项目ID = ? and 测试版本= ? and 更动标识<>'' and 更动标识<>null and 更动类型<>'1'order by 更动类型,序号";
            dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    dr = Table_InfectionAnaly.Rows.Add();

                    string ID = dr1["ID"].ToString();

                    xuhao = xuhao + 1;
                    dr["序号"] = xuhao;

                    if (dr1["更动标识"] != null)
                    {
                        dr["更动标识"] = dr1["更动标识"].ToString();
                    }
                    else
                    {
                        dr["更动标识"] = "";
                    }

                    if (dr1["影响域分析"] != null)
                    {
                        dr["影响域分析"] = ID;

                    }
                    else
                    {
                        dr["影响域分析"] = "";
                    }

                    if (dr1["测试要求"] != null)
                    {
                        dr["测试要求"] = dr1["测试要求"].ToString();
                    }
                    else
                    {
                        dr["测试要求"] = "";
                    }
                    if (dr1["相关测试依据"] != null)//测试项
                    {
                        string TestItemStr = dr1["相关测试依据"].ToString();
                        ArrayList TestItemIDList = GetStr_ToList(TestItemStr);

                        string TestItem= "";

                        if (TestItemIDList.Count > 0)
                        {
                            for (int i = 0; i <= TestItemIDList.Count - 2; i++)
                            {
                                string TestItemID = TestItemIDList[i].ToString();

                                string sqlstate = "select * from CA测试项实体表 where ID =?";
                                DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestItemID);

                                if (dt != null && dt.Rows.Count != 0)
                                {
                                    string TestItemName = dt.Rows[0]["测试项名称"].ToString();
                                    TestItem = TestItem + TestItemName + "\n";
                                }
                            }
                            string TestItemID1 = TestItemIDList[TestItemIDList.Count - 1].ToString();

                            string sqlstate11 = "select * from CA测试项实体表 where ID =?";
                            DataTable dt11 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate11, TestItemID1);

                            if (dt11 != null && dt11.Rows.Count != 0)
                            {
                                string TestItemName = dt11.Rows[0]["测试项名称"].ToString();
                                TestItem = TestItem + TestItemName;
                            }

                            dr["测试项名称"] = TestItem;
                        }
                        else
                        {
                            dr["测试项名称"] = "";
                        }
                    }
                    else
                    {
                        dr["测试项名称"] = "";
                    }                   

                }
            }

            return Table_InfectionAnaly;
        }

        public static string GetTestItem_AllQuestion(string QuestionID)
        {
           
                    string GetTestItem_AllQuestion = "";
                       
                    string SqlState = "SELECT DISTINCT CA测试过程实测表.问题报告单ID, CA测试用例实测表.ID AS 测试用例实测ID, CA测试用例实体表.测试用例名称, CA测试过程实测表.测试版本, CA测试过程实测表.项目ID, CA测试用例实测表.测试版本, CA测试用例实测表.项目ID " +
                             " FROM (CA测试用例实体表 INNER JOIN (CA测试过程实体表 INNER JOIN (CA测试过程实测表 INNER JOIN CA问题报告单 ON CA测试过程实测表.问题报告单ID = CA问题报告单.ID) ON CA测试过程实体表.ID = CA测试过程实测表.过程ID) ON CA测试用例实体表.ID = CA测试过程实体表.测试用例ID) INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID " +
                             " WHERE (((CA测试过程实测表.问题报告单ID)=?));";
                        
                    DataTable dt2 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(SqlState, QuestionID);

                    if (dt2 != null || dt2.Rows.Count != 0)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            string TestCaseID = dr2[1].ToString();
                            string SqlState3 = "SELECT CA测试项实体表.ID, CA测试用例实测表.ID FROM (CA测试项实体表 INNER JOIN CA测试项实测表 ON CA测试项实体表.ID = CA测试项实测表.测试项ID) INNER JOIN (CA测试用例与测试项关系表 INNER JOIN CA测试用例实测表 " +
                                              " ON CA测试用例与测试项关系表.测试用例ID = CA测试用例实测表.ID) ON CA测试项实测表.ID = CA测试用例与测试项关系表.测试项ID" + " where CA测试用例实测表.ID= ?";
                            DataTable dt3 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(SqlState3, TestCaseID);
                            int i = 0;
                            if (dt3 != null || dt3.Rows.Count != 0)
                            {
                                foreach (DataRow dr3 in dt3.Rows)
                                {
                                    i = i + 1;
                                    string TestItemID= dr3[0].ToString();
                                    GetTestItem_AllQuestion = GetTestItem_AllQuestion + TestItemID;
                                    if (i < dt3.Rows.Count)
                                    {
                                        GetTestItem_AllQuestion = GetTestItem_AllQuestion  + ",";
                                    }

                                }
                               
                            }

                        }
                    }
               
                    return GetTestItem_AllQuestion;
        
        }

        public string GetTestItem_AllQuestion_2(string QuestionID)
        {

            string GetTestItem_AllQuestion = "";
            string TestItemName = "";
            int i = 0;

            string SqlState = "SELECT DISTINCT CA测试过程实测表.问题报告单ID, CA测试用例实测表.ID AS 测试用例实测ID, CA测试用例实体表.测试用例名称, CA测试过程实测表.测试版本, CA测试过程实测表.项目ID, CA测试用例实测表.测试版本, CA测试用例实测表.项目ID " +
                     " FROM (CA测试用例实体表 INNER JOIN (CA测试过程实体表 INNER JOIN (CA测试过程实测表 INNER JOIN CA问题报告单 ON CA测试过程实测表.问题报告单ID = CA问题报告单.ID) ON CA测试过程实体表.ID = CA测试过程实测表.过程ID) ON CA测试用例实体表.ID = CA测试过程实体表.测试用例ID) INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID " +
                     " WHERE (((CA测试过程实测表.问题报告单ID)=?));";

            DataTable dt2 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(SqlState, QuestionID);

            if (dt2 != null || dt2.Rows.Count != 0)
            {
                foreach (DataRow dr2 in dt2.Rows)
                {
                    i = i + 1;
                    string TestCaseID = dr2[1].ToString();
                    string SqlState3 = "SELECT CA测试项实体表.测试项名称, CA测试用例实测表.ID FROM (CA测试项实体表 INNER JOIN CA测试项实测表 ON CA测试项实体表.ID = CA测试项实测表.测试项ID) INNER JOIN (CA测试用例与测试项关系表 INNER JOIN CA测试用例实测表 " +
                                      " ON CA测试用例与测试项关系表.测试用例ID = CA测试用例实测表.ID) ON CA测试项实测表.ID = CA测试用例与测试项关系表.测试项ID" + " where CA测试用例实测表.ID= ?";
                    DataTable dt3 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(SqlState3, TestCaseID);
                  
                    if (dt3 != null || dt3.Rows.Count != 0)
                    {
                        foreach (DataRow dr3 in dt3.Rows)
                        {               
                            TestItemName = dr3[0].ToString();
                            GetTestItem_AllQuestion = GetTestItem_AllQuestion + TestItemName;
                            if (i < dt3.Rows.Count)
                            {
                                GetTestItem_AllQuestion = GetTestItem_AllQuestion + "\n"; ;
                            } 

                        }

                    }

                }


                
            }

            return GetTestItem_AllQuestion;

        }

       

        public DataTable TestCaseStat_OldHaveOrAddNew(int OldHaveOrNewAdd, ArrayList DataTreeList)
        {
            int no_oldHave = 0;
            int no_newAdd = 0;
         
            DataTable ValueTable_OldHave = new DataTable();
            DataTable ValueTable_NewAdd = new DataTable();

            if (OldHaveOrNewAdd == 1)//oldhave
            {
                ValueTable_OldHave = AddTableColumn("原有用例统计", "");
            }
            else if (OldHaveOrNewAdd == 2)//addnew
            {
                ValueTable_NewAdd = AddTableColumn("新增用例统计", "");
            }

            ArrayList TestCaseList = GetCurrentVer_TestCaseList(DataTreeList);

            for (int i = 0; i <= TestCaseList.Count - 1;i++ )
            {
                TestCaseInfo TestCaseInfo1 =(TestCaseInfo)TestCaseList[i];

                string TestCaseID = TestCaseInfo1.TestCaseID;
                string TestCaseName = TestCaseInfo1.TestCaseName;
                string TestCaseBS = TestCaseInfo1.TestCaseBS;
                string TestCaseID_ST = "";

                    string sqlstate1 = "select 测试用例ID from CA测试用例实测表 where 项目ID = ? and ID = ? and 测试版本 = ?";
                    DataTable dt11 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestCaseID, TestVerID);

                    if (dt11 != null && dt11.Rows.Count != 0)
                    {
                        foreach (DataRow dr11 in dt11.Rows)
                        {
                            TestCaseID_ST = dr11[0].ToString();
                        }
                    }

                    ArrayList TestVerList = GetTestVerList(TestVerID);

                bool HaveFlag = false;
                string BestPre_SCID = "";
                string BestPre_Ver = "";

                for (int j = 0; j <= TestVerList.Count - 1; j++)
                {
                    string TestVer = TestVerList[j].ToString();
                    sqlstate1 = "select * from CA测试用例实测表 where 项目ID = ? and 测试用例ID = ? and 测试版本=? ";
                    DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestCaseID_ST, TestVer);

                    if (dt1 != null && dt1.Rows.Count != 0)
                    {
                        HaveFlag = true;
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            BestPre_SCID = dr1["ID"].ToString();
                            BestPre_Ver = TestVer;
                        }
                    }
                }

                if ((HaveFlag == true) && (OldHaveOrNewAdd==1))//引用已有的
                {
                        no_oldHave = no_oldHave + 1;                      
                        ArrayList AccordList = GetTestCaseTestAccording(BestPre_SCID, BestPre_Ver);

                        if (AccordList.Count > 0)
                        {
                            for (int j = 0; j <= AccordList.Count - 1; j++)
                            {
                                DataRow dr2 = ValueTable_OldHave.Rows.Add();

                                dr2["序号"] = no_oldHave;
                                dr2["测试用例名称"] = TestCaseName;
                                dr2["测试用例标识"] = TestCaseBS;
                                dr2["测试依据"] = AccordList[j].ToString();

                            }
                        }
                        else
                        {
                            DataRow dr2 = ValueTable_OldHave.Rows.Add();

                            dr2["序号"] = no_oldHave;
                            dr2["测试用例名称"] = TestCaseName;
                            dr2["测试用例标识"] = TestCaseBS;
                            dr2["测试依据"] = "";

                        }
                        

                }
                else if ((HaveFlag == false) && (OldHaveOrNewAdd==2))//新增的
                {
                        no_newAdd = no_newAdd + 1;
                        ArrayList AccordList = GetTestCaseTestAccording(TestCaseID, TestVerID);

                        if (AccordList.Count > 0)
                        {
                            for (int j = 0; j <= AccordList.Count - 1; j++)
                            {
                                DataRow dr3 = ValueTable_NewAdd.Rows.Add();

                                dr3["序号"] = no_newAdd;
                                dr3["测试用例名称"] = TestCaseName;
                                dr3["测试用例标识"] = TestCaseBS;
                                dr3["测试依据"] = AccordList[j].ToString();

                            }

                        }
                        else
                        {
                            DataRow dr3 = ValueTable_NewAdd.Rows.Add();

                            dr3["序号"] = no_newAdd;
                            dr3["测试用例名称"] = TestCaseName;
                            dr3["测试用例标识"] = TestCaseBS;
                            dr3["测试依据"] = "";

                        }
                    }
            }

            if (OldHaveOrNewAdd == 1)//oldhave
            {
                return ValueTable_OldHave;
            }
            else//newadd
            {
                return ValueTable_NewAdd;
            }    

        }

        public ArrayList GetTestVerList(string CurrentTestVer)
        {
            ArrayList TestVerList = new ArrayList();

            string sqlstate = "SELECT SYS测试版本表.项目ID, SYS测试版本表.序号, SYS测试版本表.ID FROM SYS测试版本表 WHERE SYS测试版本表.项目ID=? AND SYS测试版本表.ID=? ORDER BY SYS测试版本表.序号;";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

             int XuHao=0;

             if (dt != null && dt.Rows.Count != 0)
             {
                 foreach (DataRow dr in dt.Rows)
                 {
                     XuHao = (int)dr["序号"];
                 }
             }

             sqlstate = "SELECT SYS测试版本表.ID FROM SYS测试版本表 WHERE SYS测试版本表.项目ID=? AND SYS测试版本表.序号 <? ORDER BY SYS测试版本表.序号 DESC;";

             dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), XuHao);

             if (dt != null && dt.Rows.Count != 0)
             {
                 foreach (DataRow dr in dt.Rows)
                 {
                     string TestVer = dr["ID"].ToString();
                     TestVerList.Add(TestVer);
                 }
             }

             return TestVerList;          

        }

        public ArrayList GetTestCaseTestAccording(string TestCaseCS_ID,string testver)
        {
            ArrayList TestCaseAccordingList = new ArrayList();
            ArrayList valuelist = new ArrayList();

            string TestAccording = "";

            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("select * from SYS测试依据表");
            
            string sqlstate = "SELECT CA测试用例与测试项关系表.测试项ID, CA测试用例与测试项关系表.测试用例ID, CA测试项实测表.测试版本, CA测试项实测表.追踪关系 " +
                                       " FROM CA测试项实测表 INNER JOIN CA测试用例与测试项关系表 ON CA测试项实测表.ID = CA测试用例与测试项关系表.测试项ID " +
                                       " WHERE CA测试用例与测试项关系表.测试用例ID=? AND CA测试项实测表.测试版本=?";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestCaseCS_ID, testver);
                  
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TestAccording = GridAssist.GetMultiDisplayString(dt1, "ID", "测试依据", dr["追踪关系"], ",");
                    valuelist = GetStr_ToList(TestAccording);
                    for (int j = 0; j <= valuelist.Count - 1;j++ )
                    {
                        string accord = valuelist[j].ToString();
                        bool haveflag = false;
                        for (int k = 0; k <= TestCaseAccordingList.Count - 1;k++ )
                        {
                            if (TestCaseAccordingList[k].ToString() == accord)
                            {
                                haveflag = true;
                            }
                        }
                        if (haveflag == false)
                        {
                            TestCaseAccordingList.Add(accord);
                        }

                    }

                }
            }

            return TestCaseAccordingList;
           
        }

        public DataTable TestCaseStat_Should_ButNoExectue()
        {

            int no = 0;

            DataTable ValueTable = new DataTable();

            ValueTable = AddTableColumn("未选取用例统计", "");

            string sqlstate = "SELECT HG回归测试未测试原因.项目ID, HG回归测试未测试原因.用例实体ID, CA测试用例实体表.测试用例名称, HG回归测试未测试原因.涉及依据, HG回归测试未测试原因.测试版本, HG回归测试未测试原因.序号, HG回归测试未测试原因.未测试原因 " +
                              " FROM CA测试用例实体表 INNER JOIN HG回归测试未测试原因 ON CA测试用例实体表.ID = HG回归测试未测试原因.用例实体ID WHERE HG回归测试未测试原因.项目ID=? AND HG回归测试未测试原因.测试版本=? ORDER BY HG回归测试未测试原因.序号;";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    no = no + 1;
                    string TestCaseName = dr["测试用例名称"].ToString();

                    string According = dr["涉及依据"].ToString();
                    string YX = dr["未测试原因"].ToString();

                    ArrayList AccordIDList = GetStr_ToList(According);

                    for (int i = 0; i <= AccordIDList.Count - 1; i++)
                    {
                        string AccordID = AccordIDList[i].ToString();

                        string sqlstate1 = "select * from SYS测试依据表 where ID =?";
                        DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, AccordID);

                        if (dt1 != null && dt1.Rows.Count != 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                string AccordStr = dr1["测试依据"].ToString();
                                DataRow dr2 = ValueTable.Rows.Add();

                                dr2["序号"] = no.ToString();
                                dr2["测试用例名称"] = TestCaseName;
                                dr2["测试用例标识"] = "";
                                dr2["测试依据"] = AccordStr;
                                dr2["未测试原因"] = YX;

                            }

                        }

                    }


                }

            }

            return ValueTable;

        }

        public bool IfInIt_Item(ArrayList TestItemPerVerList, string TestItemID)
        {

            string TestItemID_body = "";

             string sqlstate = "select 测试项ID from CA测试项实测表 where ID = ?";
             DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestItemID);

             if (dt != null && dt.Rows.Count != 0)
             {
                 foreach (DataRow dr in dt.Rows)
                 {
                     TestItemID_body = dr[0].ToString();
                 }
             }


            bool IfInItFlag = false;
            for (int i = 0; i <= TestItemPerVerList.Count - 1;i++ )
            {
                if (TestItemID_body == TestItemPerVerList[i].ToString())
                {
                    IfInItFlag = true;
                    break;
                }
            }

            return IfInItFlag;

        }
        public bool IfInIt_Case(ArrayList TestCasePerVerList, string TestCaseID)
        {

            string TestCaseID_body = "";

            string sqlstate = "select 测试用例ID from CA测试用例实测表 where ID = ?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestCaseID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TestCaseID_body = dr[0].ToString();
                }
            }


            bool IfInItFlag = false;
            for (int i = 0; i <= TestCasePerVerList.Count - 1; i++)
            {
                if (TestCaseID_body == TestCasePerVerList[i].ToString())
                {
                    IfInItFlag = true;
                    break;
                }
            }

            return IfInItFlag;

        }
        public ArrayList TestItemList_PerVer()
        {
            ArrayList TestItemPerVerList = new ArrayList();

            string PerV = GetPreVersion();
            string sqlstate = "select 测试项ID from CA测试项实测表 where 测试版本=? and 项目ID =?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, PerV, TPM3.Sys.GlobalData.globalData.projectID.ToString());

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string TestItemID = dr[0].ToString();
                    TestItemPerVerList.Add(TestItemID);

                }
            }

            return TestItemPerVerList;

        }

        public ArrayList TestCaseList_PerVer()
        {
            ArrayList TestCasePerVerList = new ArrayList();

            string PerV = GetPreVersion();
            string sqlstate = "select 测试用例ID from CA测试用例实测表 where 测试版本=? and 项目ID =?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, PerV, TPM3.Sys.GlobalData.globalData.projectID.ToString());

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string TestCaseID = dr[0].ToString();
                    TestCasePerVerList.Add(TestCaseID);

                }
            }

            return TestCasePerVerList;

        }

        public class NewAddItem
        {
            public string TestObjectID="";
            public string TestItemID="";

        }

        public class NewAddCase
        {
            public string TestObjectID="";
            public string TestCaseID = "";
           
        }

        public bool IfInFilter(string Value,string TestObjectID)
        {
            bool flag = false;
            int pos = Value.IndexOf(TestObjectID + ",");
            if (pos!=-1)
            {
                flag = true;
            }
            return flag;

        }

        public DataTable HGTestItemAndCaseAccording(string QueryType, ArrayList DataTreeList)
        {

            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;
            string Nodeid;
            int NodeType;

            string NextBrotherID;
            string NodeContentID;
            string NodeContentJXm;
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;

            string TestItemName = "";
            string TestItemBS = "";

            string TestCaseName = "";
            string TestCaseBS = "";

            object NumRow = 1;
            int no = 0;

            int flag = 0;

            DataTable ValueTable = new DataTable();
            

            iStack = 0;

            if (DataTreeList.Count <= 0)
            {
                return ValueTable;

            }


            ValueTable = AddTableColumn(QueryType, "");

            NodeTree node = (NodeTree)DataTreeList[0];

            do
            {
                while ((node != null) && (node.NodeType <= 4))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    if (NodeType == 3)
                    {
                        TestItemName = NodeContent;
                        TestItemBS = NodeContentJXm;

                        if (IfHaveTestCase(NodeContentID) == true)
                        {
                            if (QueryType == "回归测试项与测试用例的追踪关系")
                            {
                                no = no + 1;
                            }
                        }

                    }
                    if (NodeType == 4)
                    {
                        TestCaseName = NodeContent;
                        TestCaseBS = NodeContentJXm;

                        if (QueryType == "回归测试项与测试用例的追踪关系")
                        {
                            DataRow dr2 = ValueTable.Rows.Add();

                            dr2["序号"] = no.ToString();
                            dr2["测试项"] = TestItemBS + "\n" + TestItemName;
                            dr2["测试用例"] = TestCaseBS + "\n" + TestCaseName;
                          
                        }
                        else if (QueryType == "回归测试用例与测试项的追踪关系")
                        {
                            no = no + 1;
                            DataRow dr2 = ValueTable.Rows.Add();

                            dr2["序号"] = no.ToString();
                            dr2["测试项"] = TestItemBS + "\n" + TestItemName;
                            dr2["测试用例"] = TestCaseBS + "\n" + TestCaseName;


                        }                     

                    }

                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }
                }

                if (iStack == 0)
                {
                    if (flag == 0)
                    {
                        no = no - 1;
                    }
                    return ValueTable;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }

            } while (true);

        }

        public bool IfHaveTestCase(string TestItemID)
        {
            bool flag = false;
            string sqlstate = "select * from CA测试用例与测试项关系表 where 测试项ID=? and 项目ID= ?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestItemID, TPM3.Sys.GlobalData.globalData.projectID.ToString());

            if (dt.Rows.Count != 0)
            {
                flag = true;
            }

            return flag;
        }


        public DataTable TPtoTA(string QueryType,ArrayList DataTreeList)
        {

            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;
            string Nodeid;
            int NodeType;

            string NextBrotherID;
            string NodeContentID;
            string NodeContentJXm;
            string NodeContent;
            string FirstSonID = "0";
            int layer;
         
            string TestItemName = "";
            string TestItemBS = "";

            object NumRow = 1;
            int no = 0;

            int flag = 0;

            DataTable ValueTable = new DataTable();


            iStack = 0;

            if (DataTreeList.Count <= 0)
            {
                return ValueTable;

            }


            ValueTable = AddTableColumn(QueryType, "");

            NodeTree node = (NodeTree)DataTreeList[0];

            do
            {
                while ((node != null) && (node.NodeType <= 4))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                  
                    layer = node.Layer;

                    if (NodeType == 3)
                    {
                        TestItemName = NodeContent;
                        TestItemBS = NodeContentJXm;

                        string TAZJ = GetTestTPOrTAOutputZJ(1, NodeContentID, DataTreeList);
                        string TPZJ = GetTestTPOrTAOutputZJ(2, NodeContentID, DataTreeList);

                        no = no + 1;

                        DataRow dr2 = ValueTable.Rows.Add();

                        dr2["序号"] = no.ToString();
                        dr2["测试项标识"] = TestItemBS;
                        dr2["测试项名称"] = TestItemName;
                        dr2["测试项在需求的章节"] =TAZJ ;
                        dr2["测试项在计划的章节"] = TPZJ;
                       
                    }
                   
                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }
                }

                if (iStack == 0)
                {
                    if (flag == 0)
                    {
                        no = no - 1;
                    }
                    return ValueTable;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }

            } while (true);

        }
        public bool IfInT(ArrayList CaseList,string CaseStr)
        {
            for (int i = 0; i <= CaseList.Count - 1;i++ )
            {
                if (CaseList[i].ToString() == CaseStr)
                {
                    return true;
                }
            }
            return false;

        }

        public DataTable HGTestCase_Xuqiu_According(ArrayList DataTreeList, string DocName, string TestVer)
        {

            int iStack;
            string[] stack = new string[10];
            string YijuID = "";
            string YijuID_pre = "";
            string YijuID_firstson = "";
            string YijuID_brother = "";
            string YijuID_father = "";
            string sqlstate = "";
            string wdbs = "";

            int xuhao = 0;
            int xuhaonum = 0;

            DataTable dt;
            DataRow AddDataRow = null;
            DataTable AddDataTable = new DataTable();
            ArrayList BSlist = new ArrayList();

            DataTable YiJuTable = RequireTreeForm.GetRequireTreeTable(true, TestVer);

            AddDataTable = AddTableColumn("回归测试依据与测试用例的追踪关系", DocName);

            iStack = 0;

            sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 父节点ID='~root' order by 序号";

            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVer);
            if (dt.Rows.Count == 0)
            {
                return AddDataTable;
            }

            DataRow dr1 = dt.Rows[0];
            YijuID = dr1[2].ToString();
            AddDataRow = dr1;

            if (YijuID == "")
            {
                return AddDataTable;
            }

            do
            {
                while (YijuID != "")
                {
                    ///////////////////////////////////////////////////////////////////////////                    
                    sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID,章节号,是否追踪,未追踪原因说明,测试依据标识 FROM SYS测试依据表 where 项目ID = ? and 测试版本=? and ID=?";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVer, YijuID);

                    string TestYiju = "";
                    string TestYijuShuoming = "";
                    string TestYijuID = "";
                    string ZJ = "";
                    bool IfAccording = false;
                    string fatherID = "";
                    string yy = "";

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            TestYiju = dr[0].ToString();
                            TestYijuShuoming = dr[1].ToString();
                            TestYijuID = dr[2].ToString();
                            ZJ = dr[4].ToString();
                            IfAccording = (bool)dr[5];
                            fatherID = dr[3].ToString();
                            yy = dr[6].ToString();

                            if (fatherID == "~root")
                            {
                                wdbs = dr[7].ToString();
                                yy = "此项为不追踪项";
                            }
                            else
                            {
                                if (IfAccording == true)
                                {
                                    yy = "";
                                }
                            }

                        }
                    }

                    string bs = "";
                    if (fatherID == "~root")
                    {
                        bs = wdbs;
                    }
                    else
                    {
                        //bs = wdbs + "_" + ZJ;
                        bs = GetYiJuBS(YiJuTable, TestYijuID);
                    }
                    
                        ArrayList TestItemList = TestItem_OneTestYiJu(TestYijuID, TestVerID);

                        if (TestItemList.Count == 0)
                        {
                            if ((IfAccording == true) && (fatherID != "~root"))
                            {
                                DataRow dr = AddDataTable.Rows.Add();

                                xuhaonum = xuhaonum + 1;
                                dr["序号"] = xuhaonum;

                                dr["测试依据"] = bs + "\n" + TestYiju;
                                dr["测试用例名称"] = "";
                            }

                        }
                        else
                        {
                            ArrayList Listcase_Oneitem = new ArrayList();

                            for (int j = 0; j <= TestItemList.Count - 1; j++)
                            {
                                TestItemNode TestItemNode1 = new TestItemNode();
                                TestItemNode1 = (TestItemNode)TestItemList[j];

                                string TestItemName = TestItemNode1.TestItemName;
                                string TestItemID = TestItemNode1.TestItemID;
                                string TestItemBS = GetTestItemBS(TestItemID, DataTreeList);

                               
                                if ((IfAccording == true) && (fatherID != "~root"))
                                {
                                    ArrayList TestCaseList = AllTestCase_MouTestItem(DataTreeList, TestItemID);
                                    if ((j == 0) && (TestCaseList.Count>0))
                                    {
                                       xuhaonum = xuhaonum + 1;
                                    }
                                    for (int i = 0; i <= TestCaseList.Count - 1; i++ )
                                    {                                        
                                        if (IfInT(Listcase_Oneitem, TestCaseList[i].ToString())==false)
                                        {
                                            DataRow dr = AddDataTable.Rows.Add();
                                            dr["序号"] = xuhaonum;
                                            dr["测试依据"] = bs + "\n" + TestYiju;
                                            dr["测试用例名称"] = TestCaseList[i].ToString();

                                            Listcase_Oneitem.Add(TestCaseList[i].ToString());
                                        }

                                    }
                                }
                                
                            }
                        }

                        

                    
                    ////////////////////////////////////////////////////////

                    stack[iStack] = YijuID;
                    iStack = iStack + 1;

                    YijuID_pre = YijuID;
                    YijuID_firstson = "";
                    sqlstate = "SELECT 测试依据,测试依据说明,ID,父节点ID, 章节号,是否追踪 FROM SYS测试依据表 where 项目ID =? and 测试版本=? and 父节点ID=? and 序号=1";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVer, YijuID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_firstson = dr[2].ToString();
                        }
                    }

                    //-----push(stack,p)                                      
                    if (YijuID_firstson != "")
                    {
                        YijuID = YijuID_firstson;
                    }
                    else
                    {
                        YijuID = "";
                    }

                }
                if (iStack == 0)
                {
                    return AddDataTable;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    YijuID_pre = stack[iStack];

                    YijuID_father = "";
                    sqlstate = "SELECT 父节点ID FROM SYS测试依据表 where ID=? and 项目ID =? and 测试版本=?";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVer);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_father = dr[0].ToString();
                        }
                    }

                    sqlstate = "SELECT 序号 FROM SYS测试依据表 where ID=? and 项目ID = ? and 测试版本=?";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVer);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            xuhao = int.Parse(dr[0].ToString());
                        }
                    }

                    YijuID_brother = "";
                    sqlstate = "SELECT ID FROM SYS测试依据表 where 父节点ID=? and 序号=? and 项目ID =? and 测试版本=?";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_father, xuhao + 1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVer);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_brother = dr[0].ToString();
                        }
                    }

                    if (YijuID_brother != "")
                    {
                        YijuID = YijuID_brother;
                    }
                    else
                    {
                        YijuID = "";

                    }
                }
            } while (true);

        }

        public ArrayList AllTestCase_MouTestItem(ArrayList DataTreeList, string TestItemID)
        {

            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;
            string Nodeid;
            int NodeType;

            string NextBrotherID;
            string NodeContentID;
            string NodeContentJXm;
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;

            //string TestItemName = "";
            //string TestItemBS = "";

            string TestCaseName = "";
            string TestCaseBS = "";

            object NumRow = 1;
            int flag = 0;

            ArrayList TestCaseList = new ArrayList();
         
            iStack = 0;

            if (DataTreeList.Count <= 0)
            {
                return TestCaseList;

            }          

            NodeTree node = (NodeTree)DataTreeList[0];

            do
            {
                while ((node != null) && (node.NodeType <= 4))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    if (NodeType == 3)
                    {
                        flag = 0;

                        if (NodeContentID == TestItemID)
                        {
                            flag = 1;
                          
                            if (IfHaveTestCase(NodeContentID) == false)
                            {
                                return TestCaseList;
                            }
                        }
                    }
                    if (NodeType == 4)
                    {
                        if (flag == 1)
                        {
                            TestCaseName = NodeContent;
                           // TestCaseBS = NodeContentJXm;

                           // TestCaseList.Add(TestCaseBS + "\n" + TestCaseName);

                            TestCaseBS = GetDirectTestCaseBS(NodeContentID, DataTreeList);//查找测试用例实例的标识

                            if (IfInTestCaseList(TestCaseList, TestCaseBS, TestCaseName) == false)
                            {
                                TestCaseList.Add(TestCaseBS + "\n" + TestCaseName);
                            }



                        }

                    }

                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }
                }

                if (iStack == 0)
                {
                    return TestCaseList;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }

            } while (true);

        }

        public DataTable HGTestCase_GD_According(ArrayList DataTreeList, string TestVer)
        {

            DataTable Table_HGTestCaseGDAccording = new DataTable();
            Table_HGTestCaseGDAccording = AddTableColumn("软件更动与测试用例的追踪关系", "");

            int xuhao = 0;

            string sqlstate1 = "select * from HG软件更动表 where 项目ID = ? and 测试版本= ? and 更动标识<>'' and 更动标识<>null order by 更动类型,序号";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVer);

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    bool IfChange1 = (bool)dr1["是否更动"];

                    if (IfChange1 == true)
                    {
                        ArrayList TestCaseList = AllTestCase_MouGD(DataTreeList, dr1["ID"].ToString());

                        if (TestCaseList.Count > 0)
                        {
                            xuhao = xuhao + 1;
                            for (int i = 0; i <= TestCaseList.Count - 1; i++)
                            {
                                DataRow dr = Table_HGTestCaseGDAccording.Rows.Add();

                                dr["序号"] = xuhao;
                                dr["软件更动"] = dr1["更动标识"].ToString();
                                dr["测试用例名称"] = TestCaseList[i].ToString();

                            }
                        }
                        else
                        {
                            DataRow dr = Table_HGTestCaseGDAccording.Rows.Add();
                            xuhao = xuhao + 1;
                            dr["序号"] = xuhao;
                            dr["软件更动"] = dr1["更动标识"].ToString();
                            dr["测试用例名称"] = "";

                        }

                    }
                   
                }
            }

            return Table_HGTestCaseGDAccording;

        }

        public ArrayList AllTestCase_MouGD(ArrayList DataTreeList, string GD_ID)
        {

            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;
            string Nodeid;
            int NodeType;

            string NextBrotherID;
            string NodeContentID;
            string NodeContentJXm;
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;
            string TestCaseName = "";
            string TestCaseBS = "";

            object NumRow = 1;
            int flag = 0;

            ArrayList TestCaseList = new ArrayList();

            iStack = 0;

            if (DataTreeList.Count <= 0)
            {
                return TestCaseList;

            }

            NodeTree node = (NodeTree)DataTreeList[0];

            do
            {
                while ((node != null) && (node.NodeType <= 4))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    if (NodeType == 3)
                    {
                        flag = 0;

                        string sqlstate1 = "select 追踪关系 from CA测试项实测表 where ID = ?";
                        DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, NodeContentID);
                        string AccordStr = "";

                        if (dt1 != null && dt1.Rows.Count != 0)
                        {
                            AccordStr = dt1.Rows[0]["追踪关系"].ToString();
                        }

                        ArrayList AccordIDList = GetStr_ToList(AccordStr);

                        for (int j = 0; j <= AccordIDList.Count - 1; j++)
                        {
                            string AccordID = AccordIDList[j].ToString();
                            if (AccordID == GD_ID)
                            {
                                flag = 1;
                            }
                                
                        }
                        if (flag == 1)
                        {
                            if (IfHaveTestCase(NodeContentID) == false)
                            {
                                return TestCaseList;
                            }
                        }                        
                    }
                    if (NodeType == 4)
                    {
                        if (flag == 1)
                        {
                            TestCaseName = NodeContent;  
                           //TestCaseBS = NodeContentJXm;
                            TestCaseBS = GetDirectTestCaseBS(NodeContentID, DataTreeList);//查找测试用例实例的标识

                            if (IfInTestCaseList(TestCaseList, TestCaseBS, TestCaseName) == false)
                            {                               
                                TestCaseList.Add(TestCaseBS + "\n" + TestCaseName);
                            }

                        }

                    }

                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }
                }

                if (iStack == 0)
                {
                    return TestCaseList;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }

            } while (true);

        }

        public bool IfInTestCaseList(ArrayList TestCaseList, string TestCaseBS, string TestCaseName)
        {
            string str2 = TestCaseBS + "\n" + TestCaseName;

            for (int i = 0; i <= TestCaseList.Count - 1; i++)
            {
                string str = TestCaseList[i].ToString();
                
                if (str2 == str)
                {
                    return true;
                }

            }
            return false;

        }

        public DataTable GetJiHuaAP(string type)
        {

            DataTable GetJiHuaAP = new DataTable();
            GetJiHuaAP = AddTableColumn("进度计划", "");

            int num = 0;

            string sqlstate1 = "select * from ZL进度计划表 where 项目ID = ? and 计划类型 = ? and 父节点ID = ? order by 序号";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), type, "~root");

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    num = num + 1;

                    DataRow dr = GetJiHuaAP.Rows.Add();

                    dr["工作内容"] = "(" + num.ToString() + ")" + dr1["工作内容"].ToString();
                    dr["预计开始时间"] = dr1["预计开始时间"].ToString();
                    dr["预计结束时间"] = dr1["预计结束时间"].ToString();

                    string ID = dr1["ID"].ToString();

                    string sqlstate2 = "select * from ZL进度计划表 where 项目ID = ? and 计划类型 = ? and 父节点ID = ? order by 序号";
                    DataTable dt2 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate2, TPM3.Sys.GlobalData.globalData.projectID.ToString(), type, ID);
                    if (dt2 != null && dt2.Rows.Count != 0)
                    {
                        int xuhao = 0;
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            dr = GetJiHuaAP.Rows.Add();

                            xuhao = xuhao + 1;

                            dr["工作内容"] = "  (" + num.ToString() + "." + xuhao.ToString() + ")" + dr2["工作内容"].ToString();
                            dr["预计开始时间"] = dr2["预计开始时间"].ToString();
                            dr["预计结束时间"] = dr2["预计结束时间"].ToString();

                        }

                    }
                }
            }

               return GetJiHuaAP;
        }


        public DataTable GetCMIItem()
        {

            DataTable GetCMIItem = new DataTable();
            GetCMIItem = AddTableColumn("CMI配置项", "");

            string sqlCommand = "SELECT ZL配置管理项表.CMI名称, ZL配置管理项表.CMI标识, ZL配置管理项表.入库时间, ZL基线表.基线名称, ZL配置管理项表.项目ID, ZL基线表.序号, ZL配置管理项表.序号 " +
                                " FROM ZL配置管理项表 INNER JOIN ZL基线表 ON ZL配置管理项表.所属基线 = ZL基线表.ID WHERE (ZL配置管理项表.所属基线<>Null and ZL配置管理项表.所属基线<>'') and ZL配置管理项表.项目ID=? ORDER BY ZL基线表.序号, ZL配置管理项表.序号;";
                   
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, TPM3.Sys.GlobalData.globalData.projectID.ToString());
                   
            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    DataRow dr = GetCMIItem.Rows.Add();

                    dr["CMI名称"] = dr1["CMI名称"].ToString();
                    dr["CMI标识"] = dr1["CMI标识"].ToString();
                    dr["入库时间"] = dr1["入库时间"].ToString();
                    dr["基线名称"] = dr1["基线名称"].ToString();

                }
            }

            sqlCommand = "SELECT ZL配置管理项表.CMI名称, ZL配置管理项表.CMI标识, ZL配置管理项表.入库时间,  ZL配置管理项表.所属基线, ZL配置管理项表.项目ID, ZL配置管理项表.序号 FROM ZL配置管理项表" +
                         " WHERE (ZL配置管理项表.所属基线 Is Null Or ZL配置管理项表.所属基线='') and ZL配置管理项表.项目ID=? ORDER BY ZL配置管理项表.序号;";

                          
            dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, TPM3.Sys.GlobalData.globalData.projectID.ToString());

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    DataRow dr = GetCMIItem.Rows.Add();

                    dr["CMI名称"] = dr1["CMI名称"].ToString();
                    dr["CMI标识"] = dr1["CMI标识"].ToString();
                    dr["入库时间"] = dr1["入库时间"].ToString();
                    dr["基线名称"] = dr1["所属基线"].ToString();

                }
            }

            return GetCMIItem;
        }


        public DataTable Output_TreeTypeDataTable_JH(string QueryType, ArrayList DataTreeList, string DocName)
        {

            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;

            string Nodeid;
            int NodeType = 0;

            string NextBrotherID;
            string NodeContentID = "0";
            string NodeContentJXm = "";
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;
            int TableCurrentRowNum = 0;
            int TestItemNum = 0;

            iStack = 0;

            DataTable AddDataTable = new DataTable();

            if (DataTreeList.Count <= 0)
            {
                return AddDataTable;
            }
            AddDataTable = AddTableColumn(QueryType, DocName);

            NodeTree node = (NodeTree)DataTreeList[0];

            do
            {
                while ((node != null) && (node.NodeType <= 3))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    if (NodeType == 3)//测试项
                    {

                        TestItemNum = TestItemNum + 1;

                        ArrayList TestAccordingList = GetTestAccording(NodeContentID);


                        TableCurrentRowNum = TableCurrentRowNum + 1;

                       // if (QueryType == "测试项与测试依据的追踪关系1")
                        {
                            ArrayList TestAccordingListBS = GetTestAccordingBS(NodeContentID, DataTreeList, 0);

                            if (TestAccordingList.Count >= 1)
                            {
                                for (int i = 0; i <= TestAccordingList.Count - 1; i++)
                                {
                                    DataRow dr = AddDataTable.Rows.Add();

                                    AddDataTable.Rows[TableCurrentRowNum - 1]["序号"] = TestItemNum;
                                    //if (DocName == "需求分析")
                                    //{
                                    //    AddDataTable.Rows[TableCurrentRowNum - 1]["测试项在需求的章节"] = GetTestTPOrTAOutputZJ(1, NodeContentID, DataTreeList);
                                    //}
                                    //else //if (DocName == "测试计划")
                                    //{
                                    //    AddDataTable.Rows[TableCurrentRowNum - 1]["测试项在计划的章节"] = GetTestTPOrTAOutputZJ(2, NodeContentID, DataTreeList);

                                    //}
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["测试项名称"] = NodeContent;
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["测试项标识"] = NodeContentJXm;
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["测试依据标识"] = TestAccordingListBS[i].ToString();
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["测试依据"] = TestAccordingList[i].ToString();

                                    if (i != TestAccordingList.Count - 1)
                                    {
                                        TableCurrentRowNum = TableCurrentRowNum + 1;
                                    }

                                }
                            }
                            else
                            {
                                DataRow dr = AddDataTable.Rows.Add();
                                AddDataTable.Rows[TableCurrentRowNum - 1]["序号"] = TestItemNum;
                                //if (DocName == "需求分析")
                                //{
                                //    AddDataTable.Rows[TableCurrentRowNum - 1]["测试项在需求的章节"] = GetTestTPOrTAOutputZJ(1, NodeContentID, DataTreeList);
                                //}
                                //else //if (DocName == "测试计划")
                                //{
                                //    AddDataTable.Rows[TableCurrentRowNum - 1]["测试项在计划的章节"] = GetTestTPOrTAOutputZJ(2, NodeContentID, DataTreeList);

                                //}
                                AddDataTable.Rows[TableCurrentRowNum - 1]["测试项名称"] = NodeContent;
                                AddDataTable.Rows[TableCurrentRowNum - 1]["测试项标识"] = NodeContentJXm;
                                AddDataTable.Rows[TableCurrentRowNum - 1]["测试依据标识"] = "";
                                AddDataTable.Rows[TableCurrentRowNum - 1]["测试依据"] = "";
                            }


                        }
                        //else if (QueryType == "回归测试项与测试依据的追踪关系")
                        //{
                        //    ArrayList TestAccordingListBS = GetTestAccordingBS(NodeContentID, DataTreeList, 1);

                        //    if (TestAccordingList.Count >= 1)
                        //    {
                        //        for (int i = 0; i <= TestAccordingList.Count - 1; i++)
                        //        {
                        //            DataRow dr = AddDataTable.Rows.Add();

                        //            AddDataTable.Rows[TableCurrentRowNum - 1]["序号"] = TestItemNum;

                        //            AddDataTable.Rows[TableCurrentRowNum - 1]["测试项"] = NodeContentJXm + "\n" + NodeContent;
                        //            AddDataTable.Rows[TableCurrentRowNum - 1]["测试依据"] = TestAccordingListBS[i].ToString() + "\n" + TestAccordingList[i].ToString();

                        //            if (i != TestAccordingList.Count - 1)
                        //            {
                        //                TableCurrentRowNum = TableCurrentRowNum + 1;
                        //            }

                        //        }
                        //    }
                        //    else
                        //    {
                        //        DataRow dr = AddDataTable.Rows.Add();
                        //        AddDataTable.Rows[TableCurrentRowNum - 1]["序号"] = TestItemNum;

                        //        AddDataTable.Rows[TableCurrentRowNum - 1]["测试项"] = NodeContentJXm + "\n" + NodeContent;

                        //        AddDataTable.Rows[TableCurrentRowNum - 1]["测试依据"] = "";
                        //    }



                        //}


                    }

                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }

                }
                if (iStack == 0)
                {
                    return AddDataTable;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }
            } while (true);

        }

    }
}