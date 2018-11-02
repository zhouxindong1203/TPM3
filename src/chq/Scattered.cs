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

        //            values = DocContent.GetProjectContent<int>("��¼��ʼ�½ں�").ToString() + "." + values;

        //            return values;


        //        }

        //    }

        //    return "";

        //}


        public DataTable AddTableColumn(string TableType,string DocName)
        {

            DataTable dt = new DataTable();

            if (TableType == "���԰�����ϵ")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("������", typeof(string));
                dt.Columns.Add("������", typeof(string));
                dt.Columns.Add("��������", typeof(string));
               
            }
            else if (TableType == "��������׷�ٹ�ϵ")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("�������ڼƻ����½�", typeof(string));
                dt.Columns.Add("����������", typeof(string));
                dt.Columns.Add("���������½�", typeof(string));
                dt.Columns.Add("������������", typeof(string));

            }
            else if (TableType == "��������׷�ٹ�ϵ_��Сģʽ")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("�������ʶ", typeof(string));
                dt.Columns.Add("����������", typeof(string));
                dt.Columns.Add("����������ʶ", typeof(string));
                dt.Columns.Add("������������", typeof(string));

            }
            else if (TableType == "ִ�н��ͳ��")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("������������", typeof(string));
                dt.Columns.Add("����������ʶ", typeof(string));
                dt.Columns.Add("ִ�����", typeof(string));
                dt.Columns.Add("ִ�н��", typeof(string));
                dt.Columns.Add("������", typeof(string));
                dt.Columns.Add("�����ʶ", typeof(string));

            }
            else if (TableType == "δ����ִ��ͳ��")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("������������", typeof(string));
                dt.Columns.Add("����������ʶ", typeof(string));
                dt.Columns.Add("ִ��״̬", typeof(string));
                dt.Columns.Add("δִ�л򲿷�ִ��ԭ��", typeof(string));

            }
            else if (TableType == "��������")
            {
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("��������˵��", typeof(string));
                dt.Columns.Add("�������ݱ�ʶ", typeof(string));
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("���ڵ�ID", typeof(string));
                dt.Columns.Add("�½ں�", typeof(string));
                dt.Columns.Add("�Ƿ�׷��", typeof(string));
                dt.Columns.Add("δ׷��ԭ��˵��", typeof(string));
            }
            else if (TableType == "�ع��������")
            {
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("��������˵��", typeof(string));
                dt.Columns.Add("�������ݱ�ʶ", typeof(string));
                dt.Columns.Add("ID", typeof(string));
                dt.Columns.Add("���ڵ�ID", typeof(string));
                dt.Columns.Add("�½ں�", typeof(string));
                dt.Columns.Add("�Ƿ�׷��", typeof(string));
                dt.Columns.Add("δ׷��ԭ��˵��", typeof(string));
            }
            else if (TableType == "����������������׷�ٹ�ϵ")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("���ݱ�ʶ", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("��������˵��", typeof(string));
                dt.Columns.Add("�Ƿ�׷��", typeof(string));
                if (DocName == "�������")
                {
                    dt.Columns.Add("��������������½�", typeof(string));
                }
                else// if (DocName == "���Լƻ�")
                {
                    dt.Columns.Add("�������ڼƻ����½�", typeof(string));
                }

                dt.Columns.Add("����������", typeof(string));

            }
            else if (TableType == "����������������׷�ٹ�ϵ1")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("���ݱ�ʶ", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("�������ʶ", typeof(string));
                dt.Columns.Add("����������", typeof(string));
               
            }

            else if (TableType == "��������������ݵ�׷�ٹ�ϵ")
            {
                dt.Columns.Add("���", typeof(string));
                if (DocName == "�������")
                {
                    dt.Columns.Add("��������������½�", typeof(string));
                }
                else// if (DocName == "���Լƻ�")
                {
                    dt.Columns.Add("�������ڼƻ����½�", typeof(string));
                }
                dt.Columns.Add("����������", typeof(string));
                dt.Columns.Add("�������ʶ", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("��������˵��", typeof(string));

            }
            else if (TableType == "��������������ݵ�׷�ٹ�ϵ1")
            {
                dt.Columns.Add("���", typeof(string));           
                dt.Columns.Add("�������ʶ", typeof(string));
                dt.Columns.Add("����������", typeof(string));
                dt.Columns.Add("�������ݱ�ʶ", typeof(string));
                dt.Columns.Add("��������", typeof(string));

            }
            else if ((TableType == "�ɱ��½�_��������ύ����һ��") || (TableType == "�ɱ��½�_�ع鱻������ύ����һ��"))
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("�����ʶ", typeof(string));
                dt.Columns.Add("�������", typeof(string));
                dt.Columns.Add("���⼶��", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("������ʶ", typeof(string));

            }
            else if (TableType == "��������ͳ��")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("�������ͱ�ʶ", typeof(string));
                dt.Columns.Add("��������˵��", typeof(string));

            }
            else if (TableType == "�μӲ�����Ա")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("��ɫ", typeof(string));
                dt.Columns.Add("����", typeof(string));
                dt.Columns.Add("ְ��", typeof(string));
                dt.Columns.Add("��Ҫְ��", typeof(string));

            }
            else if (TableType == "����ͳ��")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("�Ƿ����", typeof(string));
                dt.Columns.Add("������������½�", typeof(string));
                dt.Columns.Add("������������", typeof(string));

            }
            else if (TableType == "���������漰����")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("����˵��", typeof(string));
                dt.Columns.Add("��������Ӱ�������", typeof(string));
                dt.Columns.Add("��ز�������", typeof(string));

            }
            else if (TableType == "�����漰����")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("������ʩ", typeof(string));
                dt.Columns.Add("Ӱ�������", typeof(string));
                dt.Columns.Add("��������", typeof(string));

            }
            else if (TableType == "ԭ������ͳ��")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("������������", typeof(string));
                dt.Columns.Add("����������ʶ", typeof(string));
                dt.Columns.Add("��������", typeof(string));

            }
            else if (TableType == "��������ͳ��")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("������������", typeof(string));
                dt.Columns.Add("����������ʶ", typeof(string));
                dt.Columns.Add("��������", typeof(string));

            }
            else if (TableType == "δѡȡ����ͳ��")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("������������", typeof(string));
                dt.Columns.Add("����������ʶ", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("δ����ԭ��", typeof(string));
            }
            else if (TableType == "���в�����")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("������", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("�Ƿ�ѡȡ", typeof(string));
                dt.Columns.Add("δѡȡԭ��˵��", typeof(string));

            }
            else if (TableType == "���в�������")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("������", typeof(string));
                dt.Columns.Add("�Ƿ�ѡȡ", typeof(string));
                dt.Columns.Add("δѡȡԭ��˵��", typeof(string));

            }
            else if (TableType == "�ع����������������׷�ٹ�ϵ")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("���ݱ�ʶ", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("�Ƿ�׷��", typeof(string));
                dt.Columns.Add("����������", typeof(string));

            }
            else if (TableType == "�ع��������������ݵ�׷�ٹ�ϵ")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("������", typeof(string));
                dt.Columns.Add("��������", typeof(string));

            }
            else if (TableType == "�ع�����������������׷�ٹ�ϵ")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("������", typeof(string));
                dt.Columns.Add("��������", typeof(string));

            }
            else if (TableType == "�ع����������������׷�ٹ�ϵ")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("������", typeof(string));

            }
            else if (TableType == "���ݱ�ʶ")
            {
                dt.Columns.Add("����ID", typeof(string));
                dt.Columns.Add("���ݱ�ʶ", typeof(string));

            }
            else if (TableType == "���Լƻ��в�������������׷�ٹ�ϵ")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("�������ʶ", typeof(string));
                dt.Columns.Add("����������", typeof(string));
                dt.Columns.Add("��������������½�", typeof(string));
                dt.Columns.Add("�������ڼƻ����½�", typeof(string));

            }

            else if (TableType == "�����Ը���")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("�Ƿ����", typeof(string));
                dt.Columns.Add("������ʶ", typeof(string));
                dt.Columns.Add("����˵��", typeof(string));
                dt.Columns.Add("δ����ԭ��˵��", typeof(string));

            }
            else if (TableType == "�����Ը���_����")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("�Ƿ����", typeof(string));
                dt.Columns.Add("����˵��", typeof(string));
                dt.Columns.Add("δ����ԭ��˵��", typeof(string));
                dt.Columns.Add("Ӱ�������", typeof(string));

            }

            else if ((TableType == "4") || (TableType == "2") || (TableType == "3") || (TableType == "5"))
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("������ʶ", typeof(string));
                dt.Columns.Add("����˵��", typeof(string));
              
            }

            else if (TableType == "Ӱ�������")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("������ʶ", typeof(string));
                dt.Columns.Add("Ӱ�������", typeof(string));
                dt.Columns.Add("����Ҫ��", typeof(string));
                dt.Columns.Add("����������", typeof(string));

            }


            else if (TableType == "�ع�������������������׷�ٹ�ϵ")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("������������", typeof(string));

            }

            else if (TableType == "�������������������׷�ٹ�ϵ")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("������������", typeof(string));

            }

            else if (TableType == "���ȼƻ�")
            {
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("Ԥ�ƿ�ʼʱ��", typeof(string));
                dt.Columns.Add("Ԥ�ƽ���ʱ��", typeof(string));

            } 
            else if (TableType =="CMI������")
            {
                  dt.Columns.Add("CMI����", typeof(string));
                  dt.Columns.Add("CMI��ʶ", typeof(string));
                  dt.Columns.Add("���ʱ��", typeof(string));
                  dt.Columns.Add("��������", typeof(string)); 

            }
            else if (TableType == "�������͵�����ͳ��")
            {
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("1��������", typeof(string));
                dt.Columns.Add("2��������", typeof(string));
                dt.Columns.Add("3��������", typeof(string));
                dt.Columns.Add("4��������", typeof(string));
                dt.Columns.Add("5��������", typeof(string));

            }
            else if (TableType == "�ع���Ը������")
            {
                dt.Columns.Add("���", typeof(string));
                dt.Columns.Add("��������", typeof(string));  
                dt.Columns.Add("��������", typeof(string));
                dt.Columns.Add("�Ƿ����", typeof(string));    
                dt.Columns.Add("������ʶ", typeof(string));
                dt.Columns.Add("����˵��", typeof(string));
                dt.Columns.Add("��������", typeof(string));
              
            }

            return dt;

        }

        public DataTable GetObjectName()
        {
            string sqlstate = OutputComm.OutputComm.GetSqlState_CreateNodeTree("�������", 0, "", TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
            DataTable dt = TPM3.Sys.GlobalData.globalData.dbProject.ExecuteDataTable(sqlstate);
            return dt;

        }

        public string GetDirectTestCaseBS(string TestCaseID, ArrayList DataTreeList)
        {

            int NodeType;
            string NodeContentID;
            string NodeContentJXm;
            int testcaseflag;//1---ʵ����0---����

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

            if (OutputType == "��������")
            {
                FieldName = "�ɱ��½�_��������";
            }
            else if (OutputType == "������")
            {
                FieldName = "�ɱ��½�_������";
            }

            return FieldName;
        }

        public ArrayList GetTestAccording(string TestItemID)
        {

            ArrayList AccordingList = new ArrayList();

            string Values1 = "";

            //DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("select * from SYS�������ݱ� where ��������=1");
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("select * from SYS�������ݱ�");

            string sqlstate = "select ׷�ٹ�ϵ from CA������ʵ��� where ID=?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestItemID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["׷�ٹ�ϵ"] = GridAssist.GetMultiDisplayString(dt1, "ID", "��������", dr["׷�ٹ�ϵ"], ",");
                   
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
                dt0 = GetTestAccordingTableBS("���ݱ�ʶ", DataTreeList, "");
            }
            else
            {
                dt0 = GetTestAccordingTableBS_HG("���ݱ�ʶ", DataTreeList, "");
            }

            ArrayList AccordingBSList = new ArrayList();

            string Values1 = "";

            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("select * from SYS�������ݱ�");

            string sqlstate = "select ׷�ٹ�ϵ from CA������ʵ��� where ID=?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestItemID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Values11 = "";

                    dr["׷�ٹ�ϵ"] = GridAssist.GetMultiDisplayString(dt1, "ID", "ID", dr["׷�ٹ�ϵ"], ",");
                   
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

            sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ��������=1 and ���ڵ�ID='~root' and ���=1";

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

                   // if (QueryType == "��������")
                    {
                        sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵��,�������ݱ�ʶ FROM SYS�������ݱ� where ��ĿID = ? and ���԰汾=? and ��������=1 and ID=?";
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
                                    yy = "����Ϊ��׷����";
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

                        AddDataTable.Rows[xuhaonum]["����ID"] = TestYijuID;
                        AddDataTable.Rows[xuhaonum]["���ݱ�ʶ"] = bs;

                        xuhaonum++;
                     
                    }
                    ////////////////////////////////////////////////////////

                    stack[iStack] = YijuID;
                    iStack = iStack + 1;

                    YijuID_pre = YijuID;
                    YijuID_firstson = "";
                    sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID, �½ں�,�Ƿ�׷�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ��������=1 and ���ڵ�ID=? and ���=1";

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
                    sqlstate = "SELECT ���ڵ�ID FROM SYS�������ݱ� where ID=? and ��ĿID =? and ���԰汾=? and ��������=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_father = dr[0].ToString();
                        }
                    }

                    sqlstate = "SELECT ��� FROM SYS�������ݱ� where ID=? and ��ĿID = ? and ���԰汾=? and ��������=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            xuhao = int.Parse(dr[0].ToString());
                        }
                    }

                    YijuID_brother = "";
                    sqlstate = "SELECT ID FROM SYS�������ݱ� where ���ڵ�ID=? and ���=? and ��ĿID =? and ���԰汾=? and ��������=1 ";

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

           // sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ��������=2 and ���ڵ�ID='~root' and ���=1";
            sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ���ڵ�ID='~root' and ���=1";


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

                    // if (QueryType == "��������")
                    {
                       // sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵��,�������ݱ�ʶ FROM SYS�������ݱ� where ��ĿID = ? and ���԰汾=? and ��������=2 and ID=?";
                             sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵��,�������ݱ�ʶ FROM SYS�������ݱ� where ��ĿID = ? and ���԰汾=? and ID=?";
                      
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
                                    yy = "����Ϊ��׷����";
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

                        AddDataTable.Rows[xuhaonum]["����ID"] = TestYijuID;
                        AddDataTable.Rows[xuhaonum]["���ݱ�ʶ"] = bs;

                        xuhaonum++;

                    }
                    ////////////////////////////////////////////////////////

                    stack[iStack] = YijuID;
                    iStack = iStack + 1;

                    YijuID_pre = YijuID;
                    YijuID_firstson = "";
                   // sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID, �½ں�,�Ƿ�׷�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ��������=2 and ���ڵ�ID=? and ���=1";
                    sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID, �½ں�,�Ƿ�׷�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ���ڵ�ID=? and ���=1";

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
                   // sqlstate = "SELECT ���ڵ�ID FROM SYS�������ݱ� where ID=? and ��ĿID =? and ���԰汾=? and ��������=2 ";
                    sqlstate = "SELECT ���ڵ�ID FROM SYS�������ݱ� where ID=? and ��ĿID =? and ���԰汾=?";


                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_father = dr[0].ToString();
                        }
                    }

                    //sqlstate = "SELECT ��� FROM SYS�������ݱ� where ID=? and ��ĿID = ? and ���԰汾=? and ��������=2 ";
                    sqlstate = "SELECT ��� FROM SYS�������ݱ� where ID=? and ��ĿID = ? and ���԰汾=?";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            xuhao = int.Parse(dr[0].ToString());
                        }
                    }

                    YijuID_brother = "";
                   // sqlstate = "SELECT ID FROM SYS�������ݱ� where ���ڵ�ID=? and ���=? and ��ĿID =? and ���԰汾=? and ��������=2 ";
                    sqlstate = "SELECT ID FROM SYS�������ݱ� where ���ڵ�ID=? and ���=? and ��ĿID =? and ���԰汾=?";

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

            string sqlstate = "SELECT CA���ⱨ�浥.ID AS ���ⱨ�浥ID, CA���ⱨ�浥.�����������ID, CA���ⱨ�浥.һ����ʶ, CA���ⱨ�浥.������ʶ, CA���ⱨ�浥.������ʶ, CA���ⱨ�浥.�ļ���ʶ, CA���ⱨ�浥.ͬ��ʶ���" +
                              " FROM CA���ⱨ�浥 WHERE CA���ⱨ�浥.�����������ID=? ORDER BY CA���ⱨ�浥.һ����ʶ, CA���ⱨ�浥.������ʶ, CA���ⱨ�浥.������ʶ, CA���ⱨ�浥.�ļ���ʶ, CA���ⱨ�浥.ͬ��ʶ���;";

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
            string sqlstate1 = "SELECT DC���⼶���.����, DC���⼶���.���, DC���⼶���.��ĿID, DC���⼶���.���� " +
                               " FROM DC���⼶��� WHERE DC���⼶���.��ĿID=? AND DC���⼶���.����=? ORDER BY DC���⼶���.���;";

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

                    if (FirstSonID != "0")//�ж���
                    {
                        layernode = new NodeTree();
                        layernode = (NodeTree)DataTreeList[int.Parse(FirstSonID)];

                        sonnodetype = layernode.NodeType;
                        if (sonnodetype == 4)//������������
                        {
                            TestCaseSonofOneTestItemList.Add(layernode.NodeID);
                        }

                        NextBrotherID = layernode.NextBrotherID;

                        while (NextBrotherID != "0")
                        {
                            layernode = new NodeTree();
                            layernode = (NodeTree)DataTreeList[int.Parse(NextBrotherID)];

                            sonnodetype = layernode.NodeType;
                            if (sonnodetype == 4)//������������
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

            if (TextStartField == "�ɱ��½�_��������ͳ��")
            {
                sqlstate = "SELECT CA��������ʵ���.������������, CA��������ʵ���.���԰汾, CA��������ʵ���.��ĿID, CA��������ʵ���.�����������ID FROM CA��������ʵ��� INNER JOIN CA��������ʵ��� ON CA��������ʵ���.ID = CA��������ʵ���.��������ID" +
                      " WHERE CA��������ʵ���.���԰汾=? AND CA��������ʵ���.��ĿID=? AND CA��������ʵ���.�����������ID=? ORDER BY CA��������ʵ���.���;";

                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestVerID, ProjectID, ObjectID_body);

            }
            else if ((TextStartField == "�ɱ��½�_��������ύ����һ��") || (TextStartField == "�ɱ��½�_�ع鱻������ύ����һ��")) 
            {
                ArrayList QuestionList = Scattered.GetArrayListInfo(ObjectID, "����ͳ��", DataTreeList);
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
                            dr["���"] = no.ToString();
                            dr["�����ʶ"] = QuestionBS;
                            dr["�������"] = QuestionType;
                            dr["���⼶��"] = QuestionLevel;
                            dr["��������"] = TestCaseNameList[j].ToString();
                            dr["������ʶ"] = TestCaseBSList[j].ToString();

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
                                dr["���"] = no.ToString();
                                dr["�����ʶ"] = QuestionBS;
                                dr["�������"] = QuestionType;
                                dr["���⼶��"] = QuestionLevel;
                                dr["��������"] = TestCaseNameList[j].ToString();
                                dr["������ʶ"] = TestCaseBSList[j].ToString();

                            }


                        }

                    }

                   
                }

            }

            return dt;
        }

        public string GetQuestionTypeName(string TypeID)
        {
            string sqlstate = "SELECT DC���⼶���.����, DC���⼶���.ID, DC���⼶���.��ĿID " +
                              " FROM DC���⼶��� WHERE DC���⼶���.ID=? AND DC���⼶���.����='���' AND DC���⼶���.��ĿID=?";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TypeID, TPM3.Sys.GlobalData.globalData.projectID.ToString());

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                return dt.Rows[0][0].ToString();
            }
            else return "";
        }

        public string GetQuestionLevelName(string LevelID)
        {
            string sqlstate = "SELECT DC���⼶���.����, DC���⼶���.ID, DC���⼶���.��ĿID " +
                              " FROM DC���⼶��� WHERE DC���⼶���.ID=? AND DC���⼶���.����='����' AND DC���⼶���.��ĿID=?";

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
                             dt.Rows[j]["����"] = " ��       ��";
                         }
                         else
                         {
                            dt.Rows[j]["����"] = dt.Rows[j]["����"].ToString();
                         }
                        
                     }

                 }
                                
            }
            dt.Columns.Add("�ո�",typeof(string));         
            for (int j = 0; j <= dt.Rows.Count - 1; j++)
            {
                int Level = int.Parse(dt.Rows[j]["Level"].ToString());
                if (Level == 1)
                {
                    dt.Rows[j]["�ո�"] = " ";
                }
                else if (Level == 2)
                {
                    dt.Rows[j]["�ո�"] = "   ";
                }
                else if (Level == 3)
                {
                    dt.Rows[j]["�ո�"] = "     ";
                }
                else if (Level == 4)
                {
                    dt.Rows[j]["�ո�"] = "       ";
                }
                else if (Level == 5)
                {
                    dt.Rows[j]["�ո�"] = "         ";
                }
                else if (Level == 6)
                {
                    dt.Rows[j]["�ո�"] = "           ";
                }
                else if (Level == 7)
                {
                    dt.Rows[j]["�ո�"] = "             ";
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
                    //=====���
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
                    if (NodeType1 == 3)//������
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
                    //=====���
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
                    if ((NodeType == 4) && (returnflag == true) && (testcaseflag == 1))//ֱ����������
                    {

                        string sqlstate;

                        if (ArrayType == "����ͳ��")
                        {
                            sqlstate = "SELECT CA���Թ���ʵ���.���ⱨ�浥ID, CA���ⱨ�浥.�������, CA���ⱨ�浥.���⼶�� " +
                                       " FROM ((CA��������ʵ��� INNER JOIN CA��������ʵ��� ON CA��������ʵ���.ID = CA��������ʵ���.��������ID) " +
                                       " INNER JOIN (CA���Թ���ʵ��� INNER JOIN CA���Թ���ʵ��� ON CA���Թ���ʵ���.ID = CA���Թ���ʵ���.����ID) ON " +
                                       " CA��������ʵ���.ID = CA���Թ���ʵ���.��������ID) INNER JOIN CA���ⱨ�浥 ON CA���Թ���ʵ���.���ⱨ�浥ID = CA���ⱨ�浥.ID " +
                                       " WHERE CA��������ʵ���.ID=? AND CA���ⱨ�浥.���԰汾=?";

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
                        else if (ArrayType == "�μӲ�����Ա")
                        {
                            sqlstate = "select ������Ա from CA��������ʵ��� where ID=? and ���԰汾=?";
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

            sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ���ڵ�ID='~root' order by ���";

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
                        sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵��,�������ݱ�ʶ FROM SYS�������ݱ� where ��ĿID = ? and ���԰汾=? and ID=?";
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
                                    yy = "����Ϊ��׷����";
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

                        if (QueryType == "�ع��������")
                        {
                            DataRow dr = AddDataTable.Rows.Add();

                            AddDataTable.Rows[xuhaonum]["��������˵��"] = TestYijuShuoming;

                            AddDataTable.Rows[xuhaonum]["�������ݱ�ʶ"] = bs + "\n" + TestYiju;
                            if ((IfAccording == true) && (fatherID != "~root"))
                            {
                                AddDataTable.Rows[xuhaonum]["�Ƿ�׷��"] = "��";
                                AddDataTable.Rows[xuhaonum]["δ׷��ԭ��˵��"] = "";
                            }
                            else
                            {
                                AddDataTable.Rows[xuhaonum]["�Ƿ�׷��"] = "��";
                                AddDataTable.Rows[xuhaonum]["δ׷��ԭ��˵��"] = yy;
                            }

                            xuhaonum = xuhaonum + 1;

                        }
                        else if (QueryType =="�ع����������������׷�ٹ�ϵ")
                        {
                            ArrayList TestItemList = TestItem_OneTestYiJu(TestYijuID, TestVerID);

                            if (TestItemList.Count == 0)
                            {
                                 DataRow dr = AddDataTable.Rows.Add();

                                 dr["���"] = xuhaonum + 1;
                                 dr["���ݱ�ʶ"] = bs;
                                 dr["��������"] = TestYiju;
                                 dr["����������"] = "";

                                 if ((IfAccording == true) && (fatherID != "~root"))
                                 {
                                     dr["�Ƿ�׷��"] = "��";
                                  }
                                 else
                                 {
                                     dr["�Ƿ�׷��"] = "��";
                                     dr["����������"] = "����Ϊ��׷���û����Ӧ������";
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

                                    dr["���"] = xuhaonum + 1;
                                    dr["���ݱ�ʶ"] = bs;
                                    dr["��������"] = bs + "\n" + TestYiju; ;
                                   
                                    if ((IfAccording == true) && (fatherID != "~root"))
                                    {
                                        dr["�Ƿ�׷��"] = "��";
                                        dr["����������"] = TestItemBS + "\n" + TestItemName;
                                    }
                                    else
                                    {
                                        dr["�Ƿ�׷��"] = "��";
                                        dr["����������"] = "����Ϊ��׷���û����Ӧ������";
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
                    sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID, �½ں�,�Ƿ�׷�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ���ڵ�ID=? and ���=1";

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
                    sqlstate = "SELECT ���ڵ�ID FROM SYS�������ݱ� where ID=? and ��ĿID =? and ���԰汾=?";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_father = dr[0].ToString();
                        }
                    }

                    sqlstate = "SELECT ��� FROM SYS�������ݱ� where ID=? and ��ĿID = ? and ���԰汾=?";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            xuhao = int.Parse(dr[0].ToString());
                        }
                    }

                    YijuID_brother = "";
                    sqlstate = "SELECT ID FROM SYS�������ݱ� where ���ڵ�ID=? and ���=? and ��ĿID =? and ���԰汾=?";

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
            string Value = dr==null?"":dr["�������ݱ�ʶ"].ToString();            
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

            sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ��������=1 and ���ڵ�ID='~root' and ���=1";

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
                   
                    if (QueryType == "��������")
                    {
                        sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵��,�������ݱ�ʶ FROM SYS�������ݱ� where ��ĿID = ? and ���԰汾=? and ��������=1 and ID=?";
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
                                    yy = "����Ϊ��׷����";
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

                        AddDataTable.Rows[xuhaonum]["�������ݱ�ʶ"] = bs;
                        if ((IfAccording == true) && (fatherID != "~root"))
                        {
                            AddDataTable.Rows[xuhaonum]["�Ƿ�׷��"] = "��";
                        }
                        else
                        {
                            AddDataTable.Rows[xuhaonum]["�Ƿ�׷��"] = "��";
                        }
                        AddDataTable.Rows[xuhaonum]["δ׷��ԭ��˵��"] = yy;
                        xuhaonum = xuhaonum + 1;

                    }
                    else if (QueryType == "����������������׷�ٹ�ϵ")
                    {
                        sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵��,�������ݱ�ʶ FROM SYS�������ݱ� where ��ĿID = ? and ���԰汾=? and ��������=1 and ID=?";

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
                                    yy = "����Ϊ��׷����";
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
                        AddDataTable.Rows[TableCurrentRowNum]["���"] = TestYiJuNum;
                        AddDataTable.Rows[TableCurrentRowNum]["���ݱ�ʶ"] = bs;
                        if ((IfAccording == true) && (fatherID != "~root"))
                        {
                            AddDataTable.Rows[TableCurrentRowNum]["�Ƿ�׷��"] = "��";
                        }
                        else
                        {
                            AddDataTable.Rows[TableCurrentRowNum]["�Ƿ�׷��"] = "��";
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
                                AddDataTable.Rows[TableCurrentRowNum + jj]["���"] = xuhaonum;
                                AddDataTable.Rows[TableCurrentRowNum + jj]["���ݱ�ʶ"] = bs;
                                if (fatherID != "~root")
                                {
                                    AddDataTable.Rows[TableCurrentRowNum + jj]["�Ƿ�׷��"] = "��";
                                }
                                else
                                {
                                    AddDataTable.Rows[TableCurrentRowNum + jj]["�Ƿ�׷��"] = "��";
                                }
                                
                                if (CurrentDocName == "�������")
                                {
                                    AddDataTable.Rows[TableCurrentRowNum + jj]["��������������½�"] = GetTestTPOrTAOutputZJ(1, TestItemID, DataTreeList);
                                }
                                else 
                                {
                                    if (type == "����")
                                    {
                                        AddDataTable.Rows[TableCurrentRowNum + jj]["�������ڼƻ����½�"] = GetTestTPOrTAOutputZJ_DX(1, TestItemID, DataTreeList);
                                    }
                                    else
                                    {
                                        AddDataTable.Rows[TableCurrentRowNum + jj]["�������ڼƻ����½�"] = GetTestTPOrTAOutputZJ(2, TestItemID, DataTreeList);
                                    }
                                }
                               
                                if (fatherID != "~root")
                                {
                                    AddDataTable.Rows[TableCurrentRowNum + jj]["����������"] = TestItemName;
                                }
                                else
                                {
                                    AddDataTable.Rows[TableCurrentRowNum + jj]["����������"] = yy;
                                }
                                

                            }
                            if (TestItemList.Count == 0)
                            {
                                if (fatherID == "~root")
                                {
                                    AddDataTable.Rows[TableCurrentRowNum]["����������"] = yy;
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
                            AddDataTable.Rows[TableCurrentRowNum]["����������"] = yy;
                            AddDataTable.Rows[TableCurrentRowNum]["���ݱ�ʶ"] = bs;
                            AddDataTable.Rows[TableCurrentRowNum]["�Ƿ�׷��"] = "��";
                            TableCurrentRowNum = TableCurrentRowNum + 1;
                        }

                    }
                    ////////////////////////////////////////////////////////

                    stack[iStack] = YijuID;
                    iStack = iStack + 1;

                    YijuID_pre = YijuID;
                    YijuID_firstson = "";
                    sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID, �½ں�,�Ƿ�׷�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ��������=1 and ���ڵ�ID=? and ���=1";

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
                    sqlstate = "SELECT ���ڵ�ID FROM SYS�������ݱ� where ID=? and ��ĿID =? and ���԰汾=? and ��������=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_father = dr[0].ToString();
                        }
                    }

                    sqlstate = "SELECT ��� FROM SYS�������ݱ� where ID=? and ��ĿID = ? and ���԰汾=? and ��������=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            xuhao = int.Parse(dr[0].ToString());
                        }
                    }

                    YijuID_brother = "";
                    sqlstate = "SELECT ID FROM SYS�������ݱ� where ���ڵ�ID=? and ���=? and ��ĿID =? and ���԰汾=? and ��������=1 ";

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

            sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ��������=1 and ���ڵ�ID='~root' and ���=1";

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
                 
                  //  if (QueryType == "����������������׷�ٹ�ϵ1")
                    {
                        sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵��,�������ݱ�ʶ FROM SYS�������ݱ� where ��ĿID = ? and ���԰汾=? and ��������=1 and ID=?";

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
                                
                                AddDataTable.Rows[TableCurrentRowNum + jj]["���"] = xuhaonum;
                                AddDataTable.Rows[TableCurrentRowNum + jj]["���ݱ�ʶ"] = bs;
                                AddDataTable.Rows[TableCurrentRowNum + jj]["��������"] = YJ;
                                
                                string TestItemBS = "";
                                TestItemBS = GetTestItemBS(TestItemID, DataTreeList);
                                                                  
                                AddDataTable.Rows[TableCurrentRowNum + jj]["����������"] = TestItemName;
                                AddDataTable.Rows[TableCurrentRowNum + jj]["�������ʶ"] = TestItemBS;
                               
                            }
                            if (TestItemList.Count == 0)
                            {
                                AddDataTable.ImportRow(AddRow);
                                xuhaonum = xuhaonum + 1;
                                AddDataTable.Rows[TableCurrentRowNum]["���"] = xuhaonum;
                                AddDataTable.Rows[TableCurrentRowNum]["���ݱ�ʶ"] = bs;
                                AddDataTable.Rows[TableCurrentRowNum]["��������"] = YJ;
                                AddDataTable.Rows[TableCurrentRowNum]["����������"] = "";
                                AddDataTable.Rows[TableCurrentRowNum]["�������ʶ"] = "";

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
                    sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID, �½ں�,�Ƿ�׷�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ��������=1 and ���ڵ�ID=? and ���=1";

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
                    sqlstate = "SELECT ���ڵ�ID FROM SYS�������ݱ� where ID=? and ��ĿID =? and ���԰汾=? and ��������=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_father = dr[0].ToString();
                        }
                    }

                    sqlstate = "SELECT ��� FROM SYS�������ݱ� where ID=? and ��ĿID = ? and ���԰汾=? and ��������=1 ";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            xuhao = int.Parse(dr[0].ToString());
                        }
                    }

                    YijuID_brother = "";
                    sqlstate = "SELECT ID FROM SYS�������ݱ� where ���ڵ�ID=? and ���=? and ��ĿID =? and ���԰汾=? and ��������=1 ";

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
                    //=====���
                    Nodeid = node.NodeID;
                    NodeType = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    if (NodeType == 3)//������
                    {

                        TestItemNum = TestItemNum + 1;

                        ArrayList TestAccordingList = GetTestAccording(NodeContentID);
                      

                        TableCurrentRowNum = TableCurrentRowNum + 1;

                        if (QueryType == "��������������ݵ�׷�ٹ�ϵ")
                        {
                            ArrayList TestAccordingListBS = GetTestAccordingBS(NodeContentID, DataTreeList, 0);

                            if (TestAccordingList.Count >= 1)
                            {
                                for (int i = 0; i <= TestAccordingList.Count - 1; i++)
                                {
                                    DataRow dr = AddDataTable.Rows.Add();

                                    AddDataTable.Rows[TableCurrentRowNum - 1]["���"] = TestItemNum;
                                    if (DocName == "�������")
                                    {
                                        AddDataTable.Rows[TableCurrentRowNum - 1]["��������������½�"] = GetTestTPOrTAOutputZJ(1, NodeContentID, DataTreeList);
                                    }
                                    else //if (DocName == "���Լƻ�")
                                    {
                                        if (type == "����")
                                        {
                                            AddDataTable.Rows[TableCurrentRowNum - 1]["�������ڼƻ����½�"] = GetTestTPOrTAOutputZJ_DX(2, NodeContentID, DataTreeList);
                                        }
                                        else
                                        {
                                            AddDataTable.Rows[TableCurrentRowNum - 1]["�������ڼƻ����½�"] = GetTestTPOrTAOutputZJ(2, NodeContentID, DataTreeList);
                                        }

                                    }
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["����������"] = NodeContent;
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["�������ʶ"] = NodeContentJXm;
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["��������"] = TestAccordingListBS[i].ToString() + "\n" + TestAccordingList[i].ToString();

                                    if (i != TestAccordingList.Count - 1)
                                    {
                                        TableCurrentRowNum = TableCurrentRowNum + 1;
                                    }

                                }
                            }
                            else
                            {
                                DataRow dr = AddDataTable.Rows.Add();
                                AddDataTable.Rows[TableCurrentRowNum - 1]["���"] = TestItemNum;
                                if (DocName == "�������")
                                {
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["��������������½�"] = GetTestTPOrTAOutputZJ(1, NodeContentID, DataTreeList);
                                }
                                else //if (DocName == "���Լƻ�")
                                {
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["�������ڼƻ����½�"] = GetTestTPOrTAOutputZJ(2, NodeContentID, DataTreeList);

                                }
                                AddDataTable.Rows[TableCurrentRowNum - 1]["����������"] = NodeContent;
                                AddDataTable.Rows[TableCurrentRowNum - 1]["�������ʶ"] = NodeContentJXm;
                                AddDataTable.Rows[TableCurrentRowNum - 1]["��������"] = "";
                            }


                        }
                        else if (QueryType == "�ع��������������ݵ�׷�ٹ�ϵ")
                        {
                            ArrayList TestAccordingListBS = GetTestAccordingBS(NodeContentID, DataTreeList, 1);
                          
                            if (TestAccordingList.Count >= 1)
                            {
                                for (int i = 0; i <= TestAccordingList.Count - 1; i++)
                                {
                                    DataRow dr = AddDataTable.Rows.Add();

                                    AddDataTable.Rows[TableCurrentRowNum - 1]["���"] = TestItemNum;

                                    AddDataTable.Rows[TableCurrentRowNum - 1]["������"] = NodeContentJXm + "\n" + NodeContent;
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["��������"] = TestAccordingListBS[i].ToString() + "\n" + TestAccordingList[i].ToString();

                                    if (i != TestAccordingList.Count - 1)
                                    {
                                        TableCurrentRowNum = TableCurrentRowNum + 1;
                                    }

                                }
                            }
                            else
                            {
                                DataRow dr = AddDataTable.Rows.Add();
                                AddDataTable.Rows[TableCurrentRowNum - 1]["���"] = TestItemNum;

                                AddDataTable.Rows[TableCurrentRowNum - 1]["������"] = NodeContentJXm + "\n" + NodeContent;
                                
                                AddDataTable.Rows[TableCurrentRowNum - 1]["��������"] = "";
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
                    //=====���
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
                         TestItemNum = TestItemNum + 1;//����������
                     }

                    if ((NodeType == 4) && (returnflag == true))//��������
                    {
                        if (testcaseflag == 1)
                        {
                            TestcaseTotelNum = TestcaseTotelNum + 1;//������������

                            string ExecuteInfo = "";
                            string PassInfo = "";

                            string sqlstate = "select ִ��״̬,ִ�н�� from CA��������ʵ��� where ID=?";

                            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, NodeContentID);
                            if (dt != null && dt.Rows.Count != 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    ExecuteInfo = dr[0].ToString();
                                    PassInfo = dr[1].ToString();

                                }
                            }
                            if (ExecuteInfo == "����ִ��")
                            {
                                TestcaseAllExecuteNum = TestcaseAllExecuteNum + 1;//ȫ��ִ�еĲ���������

                                if (PassInfo == "ͨ��")
                                {
                                    TestcaseAllExecuteNum_pass = TestcaseAllExecuteNum_pass + 1;//ȫ��ִ��ͨ����������
                                }
                                else if (PassInfo == "δͨ��")
                                {
                                    TestcaseAllExecuteNum_nopass = TestcaseAllExecuteNum_nopass + 1;//ȫ��ִ��δͨ����������
                                }
                            }
                            else if (ExecuteInfo == "����ִ��")
                            {
                                TestcasePartExecuteNum = TestcasePartExecuteNum + 1;//����ִ�е�������

                                if (PassInfo == "ͨ��")
                                {
                                    TestcasePartExecuteNum_pass = TestcasePartExecuteNum_pass + 1;//����ִ��ͨ����������
                                }
                                else if (PassInfo == "δͨ��")
                                {
                                    TestcasePartExecuteNum_nopass = TestcasePartExecuteNum_nopass + 1;//����ִ��δͨ����������
                                }
                            }
                            else if (ExecuteInfo == "δִ��")
                            {
                                TestcaseNoExecuteNum = TestcaseNoExecuteNum + 1;//δִ�е�������
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
                    if (TypeStr == "����")
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

            //string sqlstate  "SELECT CA������ʵ���.ID, CA������ʵ���.����������, CA������ʵ���.���, CA������ʵ���.׷�ٹ�ϵ, CA������ʵ���.���԰汾, CA������ʵ���.��ĿID " +
            //                  " FROM CA������ʵ��� INNER JOIN CA������ʵ��� ON CA������ʵ���.ID = CA������ʵ���.������ID " + " WHERE CA������ʵ���.���԰汾 =?" +
            //                  " =AND CA������ʵ���.��ĿID =? ORDER BY CA������ʵ���.���,CA������ʵ���.ID,CA������ʵ���.���, CA������ʵ���.׷�ٹ�ϵ;";

            //string sqlstate = "SELECT CA������ʵ���.ID, CA������ʵ���.����������, CA������ʵ���.���, CA������ʵ���.׷�ٹ�ϵ, CA������ʵ���.���԰汾, CA������ʵ���.��ĿID, " +
            //                  " CA��������ʵ���.��� FROM (CA��������ʵ��� INNER JOIN (CA������ʵ��� INNER JOIN CA������ʵ��� ON " +
            //                  " CA������ʵ���.ID = CA������ʵ���.������ID) ON CA��������ʵ���.ID = CA������ʵ���.������������ID) INNER JOIN " +
            //                  " CA��������ʵ��� ON CA��������ʵ���.ID = CA��������ʵ���.��������ID WHERE CA������ʵ���.���԰汾=? AND CA������ʵ���.��ĿID=? ORDER BY CA��������ʵ���.���, CA������ʵ���.���;";

            string sqlstate = "SELECT CA������ʵ���.ID, CA������ʵ���.����������, CA������ʵ���.���, CA������ʵ���.׷�ٹ�ϵ, CA������ʵ���.���԰汾, CA������ʵ���.��ĿID" +
                              " FROM CA������ʵ��� INNER JOIN CA������ʵ��� ON CA������ʵ���.ID = CA������ʵ���.������ID " + " WHERE CA������ʵ���.���԰汾=? " +
                              " AND CA������ʵ���.��ĿID=?";
            
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestVerID, TPM3.Sys.GlobalData.globalData.projectID.ToString());

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Relation = dr[3].ToString();
                    string TestIteName = dr[1].ToString();
                    string ID = dr[0].ToString();

                    int position = Relation.IndexOf(YijuID);

                    if (position != -1)//�����������׷�ٹ�ϵ
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

            ValueTable = AddTableColumn("���������漰����", "");

            string sqlstate = "select * from HG���������� where ��ĿID=? and ���԰汾=? order by ���;";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    no = no + 1;
                    string ChangeName = dr["��������"].ToString();
                    string ChangeDetil = dr["����˵��"].ToString();
                    string Analyzed = dr["ID"].ToString();// dr["Ӱ�������"].ToString();
                    //string TestAccordingStr = dr["��ز�������"].ToString();
                    //ArrayList AccordIDList = GetStr_ToList(TestAccordingStr);
                    string TestAccordingStr1 = dr["��ز�������"].ToString();
                    string TestAccordingStr2 = dr["���汾�������"].ToString();

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

                        string sqlstate1 = "select * from SYS�������ݱ� where ID =?";
                        DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, AccordID);

                        if (dt1 != null && dt1.Rows.Count != 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                string According = dr1["��������"].ToString();
                                bool IfAccording = (bool)dr1["�Ƿ�׷��"];
                                string fatherID = dr1["���ڵ�ID"].ToString();
                                string YY = dr1["δ׷��ԭ��˵��"].ToString();

                                DataRow dr2 = ValueTable.Rows.Add();
                               
                                dr2["���"] = no.ToString();
                                dr2["��������"] = ChangeName;
                                dr2["����˵��"] = ChangeDetil;
                                dr2["��������Ӱ�������"] = Analyzed;
                                dr2["��ز�������"] =  According;
                            
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
            
            string sqlstate = "select ǰ��汾ID from SYS���԰汾�� where ��ĿID=? and ID=?";

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

            string sqlstate = "select * from CA���ⱨ�浥 where ��ĿID=? and ���԰汾=?";
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
             string sqlstate = "select �������� from CA���ⱨ�浥 where ��ĿID=? and ���԰汾=? and ID=?";
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

            string sqlstate = "select * from CA���ⱨ�浥 where ID=?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, QuestionID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    QuestionName = dr["����"].ToString();
                }
            }

            return QuestionName;

        }
       
        public DataTable TestAccordingStat_Question(string TableName, string DocName)
        {
            int no = 0;
           
      
            DataTable ValueTable = new DataTable();

            ValueTable = AddTableColumn("�����漰����", "");

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
                                
                string sqlstate = "SELECT CA���Թ���ʵ���.���ⱨ�浥ID, CA��������ʵ���.ID, CA��������ʵ���.������������, CA���ⱨ�浥.���԰汾, CA���ⱨ�浥.������ʩ, CA���ⱨ�浥.Ӱ�������, CA���ⱨ�浥.��������, CA������ʵ���.����������, CA������ʵ���.׷�ٹ�ϵ, CA��������ʵ���.���԰汾, CA��������ʵ���.��ĿID " +
                                 " FROM CA������ʵ��� INNER JOIN ((((CA��������ʵ��� INNER JOIN CA��������ʵ��� ON CA��������ʵ���.ID = CA��������ʵ���.��������ID) INNER JOIN (CA���Թ���ʵ��� INNER JOIN CA���Թ���ʵ��� ON CA���Թ���ʵ���.ID = CA���Թ���ʵ���.����ID) ON CA��������ʵ���.ID = CA���Թ���ʵ���.��������ID) INNER JOIN CA���ⱨ�浥 ON CA���Թ���ʵ���.���ⱨ�浥ID = CA���ⱨ�浥.ID) INNER JOIN (CA����������������ϵ�� INNER JOIN CA������ʵ��� ON CA����������������ϵ��.������ID = CA������ʵ���.ID) ON CA��������ʵ���.ID = CA����������������ϵ��.��������ID) ON CA������ʵ���.ID = CA������ʵ���.������ID " +
                                 " WHERE (((CA���ⱨ�浥.���԰汾)=?) AND ((CA���ⱨ�浥.��ĿID)=?) AND ((CA��������ʵ���.���԰汾)=?) AND ((CA��������ʵ���.��ĿID)=?) AND ((CA���ⱨ�浥.ID)=?)) ORDER BY CA���Թ���ʵ���.���ⱨ�浥ID, CA��������ʵ���.ID;";

                DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, PreVer, TPM3.Sys.GlobalData.globalData.projectID.ToString(), PreVer, TPM3.Sys.GlobalData.globalData.projectID.ToString(), QuestionID);
                if (dt != null && dt.Rows.Count != 0)
                {
                     foreach (DataRow dr in dt.Rows)
                     {

                         if (dr["������ʩ"] is int)
                         {
                             CS = (int)(dr["������ʩ"]);
                         }
                         else
                         {
                             CS = 0;
                         }
                         
                          YX = QuestionID;//dr["Ӱ�������"].ToString();
                       
                          AccordStr = dr["׷�ٹ�ϵ"].ToString();
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
                         //������һ����������

                     }
                    
                         for (int m = 0; m <= According_OneQuestionList.Count-1; m++)
                         {

                                if (CS == 0)
                                {
                                    //dr2["������ʩ"] = "δ����";
                                    //dr2["Ӱ�������"] = " ";
                                    //dr2["��������"] = "";
                                   

                                   
                                }
                                else if (CS == 1)
                                {

                                    no = no + 1;

                                    DataRow dr2 = ValueTable.Rows.Add();

                                    dr2["���"] = no.ToString();
                                    if (m == According_OneQuestionList.Count - 1)
                                    {
                                        dr2["��������"] = QuestionBS + "\n" + QuestionName;
                                    }
                                    else
                                    {
                                        dr2["��������"] = QuestionBS + "\n" + QuestionName;
                                    }
                                   
                                    dr2["������ʩ"] = "����";
                                    dr2["Ӱ�������"] = YX;

                                    string sqlstate1 = "select * from SYS�������ݱ� where ID =?";
                                    DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, According_OneQuestionList[m].ToString());
                                    if ((dt1 != null) && (dt1.Rows.Count > 0))
                                    {
                                        DataRow dr1 = dt1.Rows[0];
                                        string According = dr1["��������"].ToString();

                                        dr2["��������"] = According;
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

            ValueTable = AddTableColumn("�����漰����", "");

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

                string sqlstate = "SELECT CA���Թ���ʵ���.���ⱨ�浥ID, CA��������ʵ���.ID, CA��������ʵ���.������������, CA���ⱨ�浥.���԰汾, CA���ⱨ�浥.������ʩ, CA���ⱨ�浥.Ӱ�������, CA���ⱨ�浥.��������, CA������ʵ���.����������, CA������ʵ���.׷�ٹ�ϵ, CA��������ʵ���.���԰汾, CA��������ʵ���.��ĿID " +
                                 " FROM CA������ʵ��� INNER JOIN ((((CA��������ʵ��� INNER JOIN CA��������ʵ��� ON CA��������ʵ���.ID = CA��������ʵ���.��������ID) INNER JOIN (CA���Թ���ʵ��� INNER JOIN CA���Թ���ʵ��� ON CA���Թ���ʵ���.ID = CA���Թ���ʵ���.����ID) ON CA��������ʵ���.ID = CA���Թ���ʵ���.��������ID) INNER JOIN CA���ⱨ�浥 ON CA���Թ���ʵ���.���ⱨ�浥ID = CA���ⱨ�浥.ID) INNER JOIN (CA����������������ϵ�� INNER JOIN CA������ʵ��� ON CA����������������ϵ��.������ID = CA������ʵ���.ID) ON CA��������ʵ���.ID = CA����������������ϵ��.��������ID) ON CA������ʵ���.ID = CA������ʵ���.������ID " +
                                 " WHERE (((CA���ⱨ�浥.���԰汾)=?) AND ((CA���ⱨ�浥.��ĿID)=?) AND ((CA��������ʵ���.���԰汾)=?) AND ((CA��������ʵ���.��ĿID)=?) AND ((CA���ⱨ�浥.ID)=?)) ORDER BY CA���Թ���ʵ���.���ⱨ�浥ID, CA��������ʵ���.ID;";

                DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, PreVer, TPM3.Sys.GlobalData.globalData.projectID.ToString(), PreVer, TPM3.Sys.GlobalData.globalData.projectID.ToString(), QuestionID);
                if (dt != null && dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        if (dr["������ʩ"] is int)
                        {
                            CS = (int)(dr["������ʩ"]);
                        }
                        else
                        {
                            CS = 0;
                        }

                        YX = QuestionID;//dr["Ӱ�������"].ToString();

                        AccordStr = dr["׷�ٹ�ϵ"].ToString();
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
                        //������һ����������

                    }

                    for (int m = 0; m <= According_OneQuestionList.Count - 1; m++)
                    {
                            no = no + 1;

                            DataRow dr2 = ValueTable.Rows.Add();

                            dr2["���"] = no.ToString();
                            if (m == According_OneQuestionList.Count - 1)
                            {
                                dr2["��������"] = QuestionBS + "\n" + QuestionName;
                            }
                            else
                            {
                                dr2["��������"] = QuestionBS + "\n" + QuestionName;
                            }

                            dr2["������ʩ"] = "����";
                            dr2["Ӱ�������"] = YX;

                            string sqlstate1 = "select * from SYS�������ݱ� where ID =?";
                            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, According_OneQuestionList[m].ToString());
                            if ((dt1 != null) && (dt1.Rows.Count > 0))
                            {
                                DataRow dr1 = dt1.Rows[0];
                                string According = dr1["��������"].ToString();

                                dr2["��������"] = According;
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
                    //=====���
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
                            if (testcaseflag == 1)//ʵ��
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
            Table_GDChangeError = AddTableColumn("�����Ը���_����", "");

            string QuestineID = "";
            string QuestionName = "";
            int xuhao = 0;
            bool IfChange = false;

            string sqlstate1 = "select * from HG���������� where ��ĿID = ? and ���԰汾= ? and ��������=? order by ���";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), Testverid, "1");

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    DataRow dr = Table_GDChangeError.Rows.Add();

                    xuhao = xuhao + 1;
                    dr["���"] = xuhao;


                    QuestineID = dr1["���ⵥ��ʶ"].ToString();
                    if (dr1["���ⵥ����"] != null)
                    {
                        QuestionName = dr1["���ⵥ����"].ToString();
                    }
                    dr["��������"] = QuestineID + "\n" + QuestionName;

                    IfChange = (bool)dr1["�Ƿ����"];
                    if (IfChange == false)
                    {
                        dr["�Ƿ����"] = "��";
                        if (dr1["δ����˵��"] != null)
                        {
                            dr["δ����ԭ��˵��"] = dr1["δ����˵��"].ToString();
                        }
                        else
                        {
                            dr["δ����ԭ��˵��"] = "";
                        }
                     //   dr["������ʶ"] = "";
                        dr["����˵��"] = "";
                    }
                    else
                    {
                        dr["�Ƿ����"] = "��";
                        //if (dr1["������ʶ"] != null)
                        //{
                        //    dr["������ʶ"] = dr1["������ʶ"].ToString();
                        //}
                        //else
                        //{
                        //    dr["������ʶ"] = "";
                        //}

                        if (dr1["����˵��"] != null)
                        {
                            dr["����˵��"] = dr1["����˵��"].ToString();
                        }
                        else
                        {
                            dr["����˵��"] = "";
                        }
                        dr["δ����ԭ��˵��"] = "";
                        if (dr1["Ӱ�������"] != null)
                        {
                            dr["Ӱ�������"] = dr1["ID"].ToString();

                        }
                        else
                        {
                            dr["Ӱ�������"] = "";
                        }

                    }

                }
            }

            return Table_GDChangeError;
        }



        public DataTable GD_ChangeError()
        {

            DataTable Table_GDChangeError = new DataTable();
            Table_GDChangeError = AddTableColumn("�����Ը���", "");

            string QuestineID = "";
            string QuestionName = "";
            int xuhao = 0;
            bool IfChange = false;

            string sqlstate1 = "select * from HG���������� where ��ĿID = ? and ���԰汾= ? and ��������=? order by ���";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, "1");

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    DataRow dr =Table_GDChangeError.Rows.Add();

                    xuhao = xuhao + 1;
                    dr["���"] = xuhao;
                    

                    QuestineID = dr1["���ⵥ��ʶ"].ToString();
                    if (dr1["���ⵥ����"] != null)
                    {
                        QuestionName = dr1["���ⵥ����"].ToString();   
                    }
                    dr["��������"] = QuestineID + "\n" + QuestionName;

                    IfChange= (bool)dr1["�Ƿ����"];
                    if (IfChange == false)
                    {
                        dr["�Ƿ����"] ="��";
                        if (dr1["δ����˵��"]!= null)
                        {
                             dr["δ����ԭ��˵��"] =  dr1["δ����˵��"].ToString();
                        }
                        else 
                        {
                            dr["δ����ԭ��˵��"] = "";
                        }
                        dr["������ʶ"] = "";
                        dr["����˵��"] = "";
                    }
                    else 
                    {
                         dr["�Ƿ����"] ="��";
                         if (dr1["������ʶ"]!= null)
                         {
                            dr["������ʶ"] = dr1["������ʶ"].ToString();
                         }
                         else
                         {
                            dr["������ʶ"] = "";
                         }
                     
                        if ( dr1["����˵��"]!=null)
                        {
                            dr["����˵��"] = dr1["����˵��"].ToString();
                        }
                        else
                        {
                            dr["����˵��"] = "";
                        }  
                        dr["δ����ԭ��˵��"] ="";

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
      
            string sqlstate1 = "select * from HG���������� where ��ĿID = ? and ���԰汾= ? and ��������=? order by ���";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID, changetype);

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    DataRow dr = Table_Change.Rows.Add();

                    xuhao = xuhao + 1;
                    dr["���"] = xuhao;

                    if (dr1["������ʶ"] != null)
                    {
                        dr["������ʶ"] = dr1["������ʶ"].ToString();
                    }
                    else
                    {
                        dr["������ʶ"] = "";
                    }

                    if (dr1["����˵��"] != null)
                    {
                        dr["����˵��"] = dr1["����˵��"].ToString();
                    }
                    else
                    {
                        dr["����˵��"] = "";
                    }
                    
                }
            }

            return Table_Change;
        }


        public DataTable InfectionAnaly()
        {

            DataTable Table_InfectionAnaly = new DataTable();
            Table_InfectionAnaly = AddTableColumn("Ӱ�������", "");

            int xuhao = 0;

            DataRow dr = null;

            string sqlstate1 = "select * from HG���������� where ��ĿID = ? and ���԰汾= ? and �Ƿ����=true and ��������='1' order by ���";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    string QuestionID = dr1["���ⵥID"].ToString();
             
                    dr = Table_InfectionAnaly.Rows.Add();

                    xuhao = xuhao + 1;
                    dr["���"] = xuhao;

                    if (dr1["������ʶ"] != null)
                    {
                        dr["������ʶ"] = dr1["������ʶ"].ToString();
                    }
                    else
                    {
                        dr["������ʶ"] = "";
                    }

                    if (dr1["Ӱ�������"] != null)
                    {
                        dr["Ӱ�������"] = dr1["ID"].ToString();

                    }
                    else
                    {
                        dr["Ӱ�������"] = "";
                    }

                    if (dr1["����Ҫ��"] != null)
                    {
                        dr["����Ҫ��"] = dr1["����Ҫ��"].ToString();
                    }
                    else
                    {
                        dr["����Ҫ��"] = "";
                    }

                    string ItemStr = GetTestItem_AllQuestion_2(QuestionID);
                    dr["����������"] = ItemStr;


                }

            }

            sqlstate1 = "select * from HG���������� where ��ĿID = ? and ���԰汾= ? and ������ʶ<>'' and ������ʶ<>null and ��������<>'1'order by ��������,���";
            dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    dr = Table_InfectionAnaly.Rows.Add();

                    string ID = dr1["ID"].ToString();

                    xuhao = xuhao + 1;
                    dr["���"] = xuhao;

                    if (dr1["������ʶ"] != null)
                    {
                        dr["������ʶ"] = dr1["������ʶ"].ToString();
                    }
                    else
                    {
                        dr["������ʶ"] = "";
                    }

                    if (dr1["Ӱ�������"] != null)
                    {
                        dr["Ӱ�������"] = ID;

                    }
                    else
                    {
                        dr["Ӱ�������"] = "";
                    }

                    if (dr1["����Ҫ��"] != null)
                    {
                        dr["����Ҫ��"] = dr1["����Ҫ��"].ToString();
                    }
                    else
                    {
                        dr["����Ҫ��"] = "";
                    }
                    if (dr1["��ز�������"] != null)//������
                    {
                        string TestItemStr = dr1["��ز�������"].ToString();
                        ArrayList TestItemIDList = GetStr_ToList(TestItemStr);

                        string TestItem= "";

                        if (TestItemIDList.Count > 0)
                        {
                            for (int i = 0; i <= TestItemIDList.Count - 2; i++)
                            {
                                string TestItemID = TestItemIDList[i].ToString();

                                string sqlstate = "select * from CA������ʵ��� where ID =?";
                                DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestItemID);

                                if (dt != null && dt.Rows.Count != 0)
                                {
                                    string TestItemName = dt.Rows[0]["����������"].ToString();
                                    TestItem = TestItem + TestItemName + "\n";
                                }
                            }
                            string TestItemID1 = TestItemIDList[TestItemIDList.Count - 1].ToString();

                            string sqlstate11 = "select * from CA������ʵ��� where ID =?";
                            DataTable dt11 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate11, TestItemID1);

                            if (dt11 != null && dt11.Rows.Count != 0)
                            {
                                string TestItemName = dt11.Rows[0]["����������"].ToString();
                                TestItem = TestItem + TestItemName;
                            }

                            dr["����������"] = TestItem;
                        }
                        else
                        {
                            dr["����������"] = "";
                        }
                    }
                    else
                    {
                        dr["����������"] = "";
                    }                   

                }
            }

            return Table_InfectionAnaly;
        }

        public static string GetTestItem_AllQuestion(string QuestionID)
        {
           
                    string GetTestItem_AllQuestion = "";
                       
                    string SqlState = "SELECT DISTINCT CA���Թ���ʵ���.���ⱨ�浥ID, CA��������ʵ���.ID AS ��������ʵ��ID, CA��������ʵ���.������������, CA���Թ���ʵ���.���԰汾, CA���Թ���ʵ���.��ĿID, CA��������ʵ���.���԰汾, CA��������ʵ���.��ĿID " +
                             " FROM (CA��������ʵ��� INNER JOIN (CA���Թ���ʵ��� INNER JOIN (CA���Թ���ʵ��� INNER JOIN CA���ⱨ�浥 ON CA���Թ���ʵ���.���ⱨ�浥ID = CA���ⱨ�浥.ID) ON CA���Թ���ʵ���.ID = CA���Թ���ʵ���.����ID) ON CA��������ʵ���.ID = CA���Թ���ʵ���.��������ID) INNER JOIN CA��������ʵ��� ON CA��������ʵ���.ID = CA��������ʵ���.��������ID " +
                             " WHERE (((CA���Թ���ʵ���.���ⱨ�浥ID)=?));";
                        
                    DataTable dt2 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(SqlState, QuestionID);

                    if (dt2 != null || dt2.Rows.Count != 0)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            string TestCaseID = dr2[1].ToString();
                            string SqlState3 = "SELECT CA������ʵ���.ID, CA��������ʵ���.ID FROM (CA������ʵ��� INNER JOIN CA������ʵ��� ON CA������ʵ���.ID = CA������ʵ���.������ID) INNER JOIN (CA����������������ϵ�� INNER JOIN CA��������ʵ��� " +
                                              " ON CA����������������ϵ��.��������ID = CA��������ʵ���.ID) ON CA������ʵ���.ID = CA����������������ϵ��.������ID" + " where CA��������ʵ���.ID= ?";
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

            string SqlState = "SELECT DISTINCT CA���Թ���ʵ���.���ⱨ�浥ID, CA��������ʵ���.ID AS ��������ʵ��ID, CA��������ʵ���.������������, CA���Թ���ʵ���.���԰汾, CA���Թ���ʵ���.��ĿID, CA��������ʵ���.���԰汾, CA��������ʵ���.��ĿID " +
                     " FROM (CA��������ʵ��� INNER JOIN (CA���Թ���ʵ��� INNER JOIN (CA���Թ���ʵ��� INNER JOIN CA���ⱨ�浥 ON CA���Թ���ʵ���.���ⱨ�浥ID = CA���ⱨ�浥.ID) ON CA���Թ���ʵ���.ID = CA���Թ���ʵ���.����ID) ON CA��������ʵ���.ID = CA���Թ���ʵ���.��������ID) INNER JOIN CA��������ʵ��� ON CA��������ʵ���.ID = CA��������ʵ���.��������ID " +
                     " WHERE (((CA���Թ���ʵ���.���ⱨ�浥ID)=?));";

            DataTable dt2 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(SqlState, QuestionID);

            if (dt2 != null || dt2.Rows.Count != 0)
            {
                foreach (DataRow dr2 in dt2.Rows)
                {
                    i = i + 1;
                    string TestCaseID = dr2[1].ToString();
                    string SqlState3 = "SELECT CA������ʵ���.����������, CA��������ʵ���.ID FROM (CA������ʵ��� INNER JOIN CA������ʵ��� ON CA������ʵ���.ID = CA������ʵ���.������ID) INNER JOIN (CA����������������ϵ�� INNER JOIN CA��������ʵ��� " +
                                      " ON CA����������������ϵ��.��������ID = CA��������ʵ���.ID) ON CA������ʵ���.ID = CA����������������ϵ��.������ID" + " where CA��������ʵ���.ID= ?";
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
                ValueTable_OldHave = AddTableColumn("ԭ������ͳ��", "");
            }
            else if (OldHaveOrNewAdd == 2)//addnew
            {
                ValueTable_NewAdd = AddTableColumn("��������ͳ��", "");
            }

            ArrayList TestCaseList = GetCurrentVer_TestCaseList(DataTreeList);

            for (int i = 0; i <= TestCaseList.Count - 1;i++ )
            {
                TestCaseInfo TestCaseInfo1 =(TestCaseInfo)TestCaseList[i];

                string TestCaseID = TestCaseInfo1.TestCaseID;
                string TestCaseName = TestCaseInfo1.TestCaseName;
                string TestCaseBS = TestCaseInfo1.TestCaseBS;
                string TestCaseID_ST = "";

                    string sqlstate1 = "select ��������ID from CA��������ʵ��� where ��ĿID = ? and ID = ? and ���԰汾 = ?";
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
                    sqlstate1 = "select * from CA��������ʵ��� where ��ĿID = ? and ��������ID = ? and ���԰汾=? ";
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

                if ((HaveFlag == true) && (OldHaveOrNewAdd==1))//�������е�
                {
                        no_oldHave = no_oldHave + 1;                      
                        ArrayList AccordList = GetTestCaseTestAccording(BestPre_SCID, BestPre_Ver);

                        if (AccordList.Count > 0)
                        {
                            for (int j = 0; j <= AccordList.Count - 1; j++)
                            {
                                DataRow dr2 = ValueTable_OldHave.Rows.Add();

                                dr2["���"] = no_oldHave;
                                dr2["������������"] = TestCaseName;
                                dr2["����������ʶ"] = TestCaseBS;
                                dr2["��������"] = AccordList[j].ToString();

                            }
                        }
                        else
                        {
                            DataRow dr2 = ValueTable_OldHave.Rows.Add();

                            dr2["���"] = no_oldHave;
                            dr2["������������"] = TestCaseName;
                            dr2["����������ʶ"] = TestCaseBS;
                            dr2["��������"] = "";

                        }
                        

                }
                else if ((HaveFlag == false) && (OldHaveOrNewAdd==2))//������
                {
                        no_newAdd = no_newAdd + 1;
                        ArrayList AccordList = GetTestCaseTestAccording(TestCaseID, TestVerID);

                        if (AccordList.Count > 0)
                        {
                            for (int j = 0; j <= AccordList.Count - 1; j++)
                            {
                                DataRow dr3 = ValueTable_NewAdd.Rows.Add();

                                dr3["���"] = no_newAdd;
                                dr3["������������"] = TestCaseName;
                                dr3["����������ʶ"] = TestCaseBS;
                                dr3["��������"] = AccordList[j].ToString();

                            }

                        }
                        else
                        {
                            DataRow dr3 = ValueTable_NewAdd.Rows.Add();

                            dr3["���"] = no_newAdd;
                            dr3["������������"] = TestCaseName;
                            dr3["����������ʶ"] = TestCaseBS;
                            dr3["��������"] = "";

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

            string sqlstate = "SELECT SYS���԰汾��.��ĿID, SYS���԰汾��.���, SYS���԰汾��.ID FROM SYS���԰汾�� WHERE SYS���԰汾��.��ĿID=? AND SYS���԰汾��.ID=? ORDER BY SYS���԰汾��.���;";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

             int XuHao=0;

             if (dt != null && dt.Rows.Count != 0)
             {
                 foreach (DataRow dr in dt.Rows)
                 {
                     XuHao = (int)dr["���"];
                 }
             }

             sqlstate = "SELECT SYS���԰汾��.ID FROM SYS���԰汾�� WHERE SYS���԰汾��.��ĿID=? AND SYS���԰汾��.��� <? ORDER BY SYS���԰汾��.��� DESC;";

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

            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("select * from SYS�������ݱ�");
            
            string sqlstate = "SELECT CA����������������ϵ��.������ID, CA����������������ϵ��.��������ID, CA������ʵ���.���԰汾, CA������ʵ���.׷�ٹ�ϵ " +
                                       " FROM CA������ʵ��� INNER JOIN CA����������������ϵ�� ON CA������ʵ���.ID = CA����������������ϵ��.������ID " +
                                       " WHERE CA����������������ϵ��.��������ID=? AND CA������ʵ���.���԰汾=?";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestCaseCS_ID, testver);
                  
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TestAccording = GridAssist.GetMultiDisplayString(dt1, "ID", "��������", dr["׷�ٹ�ϵ"], ",");
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

            ValueTable = AddTableColumn("δѡȡ����ͳ��", "");

            string sqlstate = "SELECT HG�ع����δ����ԭ��.��ĿID, HG�ع����δ����ԭ��.����ʵ��ID, CA��������ʵ���.������������, HG�ع����δ����ԭ��.�漰����, HG�ع����δ����ԭ��.���԰汾, HG�ع����δ����ԭ��.���, HG�ع����δ����ԭ��.δ����ԭ�� " +
                              " FROM CA��������ʵ��� INNER JOIN HG�ع����δ����ԭ�� ON CA��������ʵ���.ID = HG�ع����δ����ԭ��.����ʵ��ID WHERE HG�ع����δ����ԭ��.��ĿID=? AND HG�ع����δ����ԭ��.���԰汾=? ORDER BY HG�ع����δ����ԭ��.���;";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    no = no + 1;
                    string TestCaseName = dr["������������"].ToString();

                    string According = dr["�漰����"].ToString();
                    string YX = dr["δ����ԭ��"].ToString();

                    ArrayList AccordIDList = GetStr_ToList(According);

                    for (int i = 0; i <= AccordIDList.Count - 1; i++)
                    {
                        string AccordID = AccordIDList[i].ToString();

                        string sqlstate1 = "select * from SYS�������ݱ� where ID =?";
                        DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, AccordID);

                        if (dt1 != null && dt1.Rows.Count != 0)
                        {
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                string AccordStr = dr1["��������"].ToString();
                                DataRow dr2 = ValueTable.Rows.Add();

                                dr2["���"] = no.ToString();
                                dr2["������������"] = TestCaseName;
                                dr2["����������ʶ"] = "";
                                dr2["��������"] = AccordStr;
                                dr2["δ����ԭ��"] = YX;

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

             string sqlstate = "select ������ID from CA������ʵ��� where ID = ?";
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

            string sqlstate = "select ��������ID from CA��������ʵ��� where ID = ?";
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
            string sqlstate = "select ������ID from CA������ʵ��� where ���԰汾=? and ��ĿID =?";
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
            string sqlstate = "select ��������ID from CA��������ʵ��� where ���԰汾=? and ��ĿID =?";
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
                    //=====���
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
                            if (QueryType == "�ع�����������������׷�ٹ�ϵ")
                            {
                                no = no + 1;
                            }
                        }

                    }
                    if (NodeType == 4)
                    {
                        TestCaseName = NodeContent;
                        TestCaseBS = NodeContentJXm;

                        if (QueryType == "�ع�����������������׷�ٹ�ϵ")
                        {
                            DataRow dr2 = ValueTable.Rows.Add();

                            dr2["���"] = no.ToString();
                            dr2["������"] = TestItemBS + "\n" + TestItemName;
                            dr2["��������"] = TestCaseBS + "\n" + TestCaseName;
                          
                        }
                        else if (QueryType == "�ع����������������׷�ٹ�ϵ")
                        {
                            no = no + 1;
                            DataRow dr2 = ValueTable.Rows.Add();

                            dr2["���"] = no.ToString();
                            dr2["������"] = TestItemBS + "\n" + TestItemName;
                            dr2["��������"] = TestCaseBS + "\n" + TestCaseName;


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
            string sqlstate = "select * from CA����������������ϵ�� where ������ID=? and ��ĿID= ?";
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
                    //=====���
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

                        dr2["���"] = no.ToString();
                        dr2["�������ʶ"] = TestItemBS;
                        dr2["����������"] = TestItemName;
                        dr2["��������������½�"] =TAZJ ;
                        dr2["�������ڼƻ����½�"] = TPZJ;
                       
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

            AddDataTable = AddTableColumn("�ع�������������������׷�ٹ�ϵ", DocName);

            iStack = 0;

            sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ���ڵ�ID='~root' order by ���";

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
                    sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID,�½ں�,�Ƿ�׷��,δ׷��ԭ��˵��,�������ݱ�ʶ FROM SYS�������ݱ� where ��ĿID = ? and ���԰汾=? and ID=?";
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
                                yy = "����Ϊ��׷����";
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
                                dr["���"] = xuhaonum;

                                dr["��������"] = bs + "\n" + TestYiju;
                                dr["������������"] = "";
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
                                            dr["���"] = xuhaonum;
                                            dr["��������"] = bs + "\n" + TestYiju;
                                            dr["������������"] = TestCaseList[i].ToString();

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
                    sqlstate = "SELECT ��������,��������˵��,ID,���ڵ�ID, �½ں�,�Ƿ�׷�� FROM SYS�������ݱ� where ��ĿID =? and ���԰汾=? and ���ڵ�ID=? and ���=1";

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
                    sqlstate = "SELECT ���ڵ�ID FROM SYS�������ݱ� where ID=? and ��ĿID =? and ���԰汾=?";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVer);

                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            YijuID_father = dr[0].ToString();
                        }
                    }

                    sqlstate = "SELECT ��� FROM SYS�������ݱ� where ID=? and ��ĿID = ? and ���԰汾=?";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, YijuID_pre, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVer);
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            xuhao = int.Parse(dr[0].ToString());
                        }
                    }

                    YijuID_brother = "";
                    sqlstate = "SELECT ID FROM SYS�������ݱ� where ���ڵ�ID=? and ���=? and ��ĿID =? and ���԰汾=?";

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
                    //=====���
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

                            TestCaseBS = GetDirectTestCaseBS(NodeContentID, DataTreeList);//���Ҳ�������ʵ���ı�ʶ

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
            Table_HGTestCaseGDAccording = AddTableColumn("�������������������׷�ٹ�ϵ", "");

            int xuhao = 0;

            string sqlstate1 = "select * from HG���������� where ��ĿID = ? and ���԰汾= ? and ������ʶ<>'' and ������ʶ<>null order by ��������,���";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVer);

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    bool IfChange1 = (bool)dr1["�Ƿ����"];

                    if (IfChange1 == true)
                    {
                        ArrayList TestCaseList = AllTestCase_MouGD(DataTreeList, dr1["ID"].ToString());

                        if (TestCaseList.Count > 0)
                        {
                            xuhao = xuhao + 1;
                            for (int i = 0; i <= TestCaseList.Count - 1; i++)
                            {
                                DataRow dr = Table_HGTestCaseGDAccording.Rows.Add();

                                dr["���"] = xuhao;
                                dr["��������"] = dr1["������ʶ"].ToString();
                                dr["������������"] = TestCaseList[i].ToString();

                            }
                        }
                        else
                        {
                            DataRow dr = Table_HGTestCaseGDAccording.Rows.Add();
                            xuhao = xuhao + 1;
                            dr["���"] = xuhao;
                            dr["��������"] = dr1["������ʶ"].ToString();
                            dr["������������"] = "";

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
                    //=====���
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

                        string sqlstate1 = "select ׷�ٹ�ϵ from CA������ʵ��� where ID = ?";
                        DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, NodeContentID);
                        string AccordStr = "";

                        if (dt1 != null && dt1.Rows.Count != 0)
                        {
                            AccordStr = dt1.Rows[0]["׷�ٹ�ϵ"].ToString();
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
                            TestCaseBS = GetDirectTestCaseBS(NodeContentID, DataTreeList);//���Ҳ�������ʵ���ı�ʶ

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
            GetJiHuaAP = AddTableColumn("���ȼƻ�", "");

            int num = 0;

            string sqlstate1 = "select * from ZL���ȼƻ��� where ��ĿID = ? and �ƻ����� = ? and ���ڵ�ID = ? order by ���";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), type, "~root");

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    num = num + 1;

                    DataRow dr = GetJiHuaAP.Rows.Add();

                    dr["��������"] = "(" + num.ToString() + ")" + dr1["��������"].ToString();
                    dr["Ԥ�ƿ�ʼʱ��"] = dr1["Ԥ�ƿ�ʼʱ��"].ToString();
                    dr["Ԥ�ƽ���ʱ��"] = dr1["Ԥ�ƽ���ʱ��"].ToString();

                    string ID = dr1["ID"].ToString();

                    string sqlstate2 = "select * from ZL���ȼƻ��� where ��ĿID = ? and �ƻ����� = ? and ���ڵ�ID = ? order by ���";
                    DataTable dt2 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate2, TPM3.Sys.GlobalData.globalData.projectID.ToString(), type, ID);
                    if (dt2 != null && dt2.Rows.Count != 0)
                    {
                        int xuhao = 0;
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            dr = GetJiHuaAP.Rows.Add();

                            xuhao = xuhao + 1;

                            dr["��������"] = "  (" + num.ToString() + "." + xuhao.ToString() + ")" + dr2["��������"].ToString();
                            dr["Ԥ�ƿ�ʼʱ��"] = dr2["Ԥ�ƿ�ʼʱ��"].ToString();
                            dr["Ԥ�ƽ���ʱ��"] = dr2["Ԥ�ƽ���ʱ��"].ToString();

                        }

                    }
                }
            }

               return GetJiHuaAP;
        }


        public DataTable GetCMIItem()
        {

            DataTable GetCMIItem = new DataTable();
            GetCMIItem = AddTableColumn("CMI������", "");

            string sqlCommand = "SELECT ZL���ù������.CMI����, ZL���ù������.CMI��ʶ, ZL���ù������.���ʱ��, ZL���߱�.��������, ZL���ù������.��ĿID, ZL���߱�.���, ZL���ù������.��� " +
                                " FROM ZL���ù������ INNER JOIN ZL���߱� ON ZL���ù������.�������� = ZL���߱�.ID WHERE (ZL���ù������.��������<>Null and ZL���ù������.��������<>'') and ZL���ù������.��ĿID=? ORDER BY ZL���߱�.���, ZL���ù������.���;";
                   
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, TPM3.Sys.GlobalData.globalData.projectID.ToString());
                   
            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    DataRow dr = GetCMIItem.Rows.Add();

                    dr["CMI����"] = dr1["CMI����"].ToString();
                    dr["CMI��ʶ"] = dr1["CMI��ʶ"].ToString();
                    dr["���ʱ��"] = dr1["���ʱ��"].ToString();
                    dr["��������"] = dr1["��������"].ToString();

                }
            }

            sqlCommand = "SELECT ZL���ù������.CMI����, ZL���ù������.CMI��ʶ, ZL���ù������.���ʱ��,  ZL���ù������.��������, ZL���ù������.��ĿID, ZL���ù������.��� FROM ZL���ù������" +
                         " WHERE (ZL���ù������.�������� Is Null Or ZL���ù������.��������='') and ZL���ù������.��ĿID=? ORDER BY ZL���ù������.���;";

                          
            dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, TPM3.Sys.GlobalData.globalData.projectID.ToString());

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    DataRow dr = GetCMIItem.Rows.Add();

                    dr["CMI����"] = dr1["CMI����"].ToString();
                    dr["CMI��ʶ"] = dr1["CMI��ʶ"].ToString();
                    dr["���ʱ��"] = dr1["���ʱ��"].ToString();
                    dr["��������"] = dr1["��������"].ToString();

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
                    //=====���
                    Nodeid = node.NodeID;
                    NodeType = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    if (NodeType == 3)//������
                    {

                        TestItemNum = TestItemNum + 1;

                        ArrayList TestAccordingList = GetTestAccording(NodeContentID);


                        TableCurrentRowNum = TableCurrentRowNum + 1;

                       // if (QueryType == "��������������ݵ�׷�ٹ�ϵ1")
                        {
                            ArrayList TestAccordingListBS = GetTestAccordingBS(NodeContentID, DataTreeList, 0);

                            if (TestAccordingList.Count >= 1)
                            {
                                for (int i = 0; i <= TestAccordingList.Count - 1; i++)
                                {
                                    DataRow dr = AddDataTable.Rows.Add();

                                    AddDataTable.Rows[TableCurrentRowNum - 1]["���"] = TestItemNum;
                                    //if (DocName == "�������")
                                    //{
                                    //    AddDataTable.Rows[TableCurrentRowNum - 1]["��������������½�"] = GetTestTPOrTAOutputZJ(1, NodeContentID, DataTreeList);
                                    //}
                                    //else //if (DocName == "���Լƻ�")
                                    //{
                                    //    AddDataTable.Rows[TableCurrentRowNum - 1]["�������ڼƻ����½�"] = GetTestTPOrTAOutputZJ(2, NodeContentID, DataTreeList);

                                    //}
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["����������"] = NodeContent;
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["�������ʶ"] = NodeContentJXm;
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["�������ݱ�ʶ"] = TestAccordingListBS[i].ToString();
                                    AddDataTable.Rows[TableCurrentRowNum - 1]["��������"] = TestAccordingList[i].ToString();

                                    if (i != TestAccordingList.Count - 1)
                                    {
                                        TableCurrentRowNum = TableCurrentRowNum + 1;
                                    }

                                }
                            }
                            else
                            {
                                DataRow dr = AddDataTable.Rows.Add();
                                AddDataTable.Rows[TableCurrentRowNum - 1]["���"] = TestItemNum;
                                //if (DocName == "�������")
                                //{
                                //    AddDataTable.Rows[TableCurrentRowNum - 1]["��������������½�"] = GetTestTPOrTAOutputZJ(1, NodeContentID, DataTreeList);
                                //}
                                //else //if (DocName == "���Լƻ�")
                                //{
                                //    AddDataTable.Rows[TableCurrentRowNum - 1]["�������ڼƻ����½�"] = GetTestTPOrTAOutputZJ(2, NodeContentID, DataTreeList);

                                //}
                                AddDataTable.Rows[TableCurrentRowNum - 1]["����������"] = NodeContent;
                                AddDataTable.Rows[TableCurrentRowNum - 1]["�������ʶ"] = NodeContentJXm;
                                AddDataTable.Rows[TableCurrentRowNum - 1]["�������ݱ�ʶ"] = "";
                                AddDataTable.Rows[TableCurrentRowNum - 1]["��������"] = "";
                            }


                        }
                        //else if (QueryType == "�ع��������������ݵ�׷�ٹ�ϵ")
                        //{
                        //    ArrayList TestAccordingListBS = GetTestAccordingBS(NodeContentID, DataTreeList, 1);

                        //    if (TestAccordingList.Count >= 1)
                        //    {
                        //        for (int i = 0; i <= TestAccordingList.Count - 1; i++)
                        //        {
                        //            DataRow dr = AddDataTable.Rows.Add();

                        //            AddDataTable.Rows[TableCurrentRowNum - 1]["���"] = TestItemNum;

                        //            AddDataTable.Rows[TableCurrentRowNum - 1]["������"] = NodeContentJXm + "\n" + NodeContent;
                        //            AddDataTable.Rows[TableCurrentRowNum - 1]["��������"] = TestAccordingListBS[i].ToString() + "\n" + TestAccordingList[i].ToString();

                        //            if (i != TestAccordingList.Count - 1)
                        //            {
                        //                TableCurrentRowNum = TableCurrentRowNum + 1;
                        //            }

                        //        }
                        //    }
                        //    else
                        //    {
                        //        DataRow dr = AddDataTable.Rows.Add();
                        //        AddDataTable.Rows[TableCurrentRowNum - 1]["���"] = TestItemNum;

                        //        AddDataTable.Rows[TableCurrentRowNum - 1]["������"] = NodeContentJXm + "\n" + NodeContent;

                        //        AddDataTable.Rows[TableCurrentRowNum - 1]["��������"] = "";
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