using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Aspose.Words;
using Aspose.Words.Tables;
using Aspose.Words.Reporting;
using Aspose.Words.Drawing;
using TPM3.chq;
using Common.Aspose;
using Common;
using System.IO;
using TPM3.wx;
using System.Xml;
using TPM3.Sys;



namespace TPM3.chq
{
    class Annexs
    {
        Scattered Scattered = new Scattered();
        int num = 0;
        string tempfilename = "";
        string TestCaseStr = "";
        string AnnexName = "";
        Document doc = null;
        string AnnexMuLuName = "";

        public void PutAnnexIntoNewFile(string DocPath, int type1)
        {
            ArrayList TestCaseList = new ArrayList();
            ArrayList OutputAnnex = new ArrayList();
                    
            AnnexMuLuName = TPM3.Sys.GlobalData.GetAttachDirName();
            DataView dv = null;

            if (type1 == 1)
            {
                dv = TPM3.wx.AttachCaseTrace.GetAttachCaseTraceView(2);              
                AnnexMuLuName = AnnexMuLuName + "\\����˵���пɴ�ӡ����";
            }
            else if (type1 == 2)
            {
                dv = TPM3.wx.AttachCaseTrace.GetAttachCaseTraceView(3);              
                AnnexMuLuName = AnnexMuLuName + "\\���Լ�¼ʵ�����пɴ�ӡ����";
            }
            System.Windows.Forms.Application.DoEvents();

            if (dv.Count > 0)
            {
                if (Directory.Exists(AnnexMuLuName) == true)
                {
                    string[] filelist = Directory.GetFiles(AnnexMuLuName);

                    for (int k = 0; k <= filelist.Length - 1; k++)
                    {
                        string Filename = filelist[k];
                        File.Delete(Filename);
                    }
                }
                else
                {
                    Directory.CreateDirectory(AnnexMuLuName);
                }
            }
            else
            {
                if (Directory.Exists(AnnexMuLuName) == true)
                {
                    string[] filelist = Directory.GetFiles(AnnexMuLuName);

                    for (int k = 0; k <= filelist.Length - 1; k++)
                    {
                        string Filename = filelist[k];
                        File.Delete(Filename);
                    }

                }
                if (type1 == 1)
                {
                    MessageBox.Show("����˵�����޸�����");
                }
                else if (type1 == 2)
                {
                    MessageBox.Show("���Լ�¼ʵ�������޸�����");
                }

                return;
            }
            System.Windows.Forms.Application.DoEvents();

            for (int i = 0; i < dv.Count; i++)
            {
                DataRow dr = dv.Table.Rows[i];
                bool flag = true;

                for (int j = 0; j <= OutputAnnex.Count - 1; j++)
                {
                    string alreadyoutputname = OutputAnnex[j].ToString();
                    if (dr["��������"].ToString() == alreadyoutputname)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                if (flag == true)
                {
                    TestCaseStr = "";
                    OutputDoc outputdoc = new OutputDoc();
                    doc = new Document(DocPath);
                   
                    string NewFileName = "";
                    string IfOut = "";

                    OutputAnnex.Add(dr["��������"].ToString());

                    num = num + 1;

                    AnnexName = dr["��������"].ToString();
                    IfOut = dr["�Ƿ����"].ToString();
                    TestCaseList = GetTestCaseList_OneAnnex_1(dr["��������"].ToString(), type1);

                    if (IfOut != "False")
                    {
                        tempfilename = Path.Combine(AnnexMuLuName, "temp$" + AnnexName);

                        if (File.Exists(tempfilename) == true)
                        {
                            File.Delete(tempfilename);
                        }

                        FileStream fs = new FileStream(tempfilename, FileMode.CreateNew);

                        string sqlstate = "select ��������,�������� from DC����ʵ��� where ��������=?";
                        DataTable dt1 = TPM3.Sys.GlobalData.globalData.dbProject.ExecuteDataTable(sqlstate, AnnexName);
                        string AnnexType = "";
                        if (dt1 != null && dt1.Rows.Count != 0)
                        {
                            System.Windows.Forms.Application.DoEvents();

                            DataRow dr1 = dt1.Rows[0];
                            AnnexType = dr1["��������"].ToString();
                            byte[] buf = dr1["��������"] as byte[];
                            if (buf != null)
                                fs.Write(buf, 0, buf.Length);
                            fs.Flush();
                            fs.Close();
                           
                        }
                        
                        string AnnexName1 = AnnexName.Substring(0, AnnexName.LastIndexOf("."));
                        NewFileName = "����" + num.ToString() + "_" + AnnexName1;
                        NewFileName = Path.Combine(AnnexMuLuName, NewFileName);

                        for (int j = 0; j <= TestCaseList.Count - 2; j++) TestCaseStr = TestCaseStr + TestCaseList[j].ToString() + "��";
                        TestCaseStr = TestCaseStr + TestCaseList[TestCaseList.Count - 1].ToString();

                        DocumentBuilder db = new DocumentBuilder(doc);
                        if (db.MoveToMergeField("�������"))
                        {
                            db.Write(num.ToString());
                        }
                        if (db.MoveToMergeField("��������"))
                        {
                            db.Write(AnnexName);
                        }
                        if (db.MoveToMergeField("ʹ��λ��"))
                        {
                            db.Write(TestCaseStr);
                        }
                        if (AnnexType==".doc")
                        {
                            System.Windows.Forms.Application.DoEvents();

                            Document doc1 = new Document(tempfilename);

                            System.Windows.Forms.Application.DoEvents();

                            Node Node1 = OutputComm.OutputComm.GetNodeByField(doc, "��������", null);

                            System.Windows.Forms.Application.DoEvents();

                            AsposeCommon.InsertDocument(Node1, doc1);
                            System.Windows.Forms.Application.DoEvents();
                            AsposeCommon.RemoveAllMarkField(doc);
                            System.Windows.Forms.Application.DoEvents();
                            doc.Save(NewFileName + ".doc");
                            System.Windows.Forms.Application.DoEvents();
                        }
                        else
                        {
                            System.Windows.Forms.Application.DoEvents();

                            doc.Save(NewFileName + ".doc");

                            WordLastProcess.Class1.AnnexProcess(tempfilename, NewFileName + ".doc", AnnexType);
                        }
                        
                     }                       
                        if (File.Exists(tempfilename) == true)
                        {
                            File.Delete(tempfilename);
                        }
                        System.Windows.Forms.Application.DoEvents();

                    }
                }
            }


        public void PutoutAnnex(Document CurrentDoc, int type1)
        {

            ArrayList TestCaseList = new ArrayList();
            ArrayList OutputAnnex = new ArrayList();

            AnnexMuLuName = TPM3.Sys.GlobalData.GetAttachDirName();
            DataView dv = null;

            if (type1 == 1)
            {
                dv = TPM3.wx.AttachCaseTrace.GetAttachCaseTraceView(2);
              //  AnnexMuLuName = AnnexMuLuName + "\\����˵���пɴ�ӡ����";
            }
            else if (type1 == 2)
            {
                dv = TPM3.wx.AttachCaseTrace.GetAttachCaseTraceView(3);
              //  AnnexMuLuName = AnnexMuLuName + "\\���Լ�¼ʵ�����пɴ�ӡ����";
            }
            System.Windows.Forms.Application.DoEvents();

            for (int i = 0; i < dv.Count; i++)
            {
               
                DataRow dr = dv.Table.Rows[i];
                bool flag = true;

                for (int j = 0; j <= OutputAnnex.Count - 1; j++)
                {
                    string alreadyoutputname = OutputAnnex[j].ToString();
                    if (dr["��������"].ToString() == alreadyoutputname)
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                if (flag == true)
                {
                    TestCaseStr = "";
                   // OutputDoc outputdoc = new OutputDoc();
                  //  doc = new Document(DocPath);

                    string NewFileName = "";
                    string IfOut = "";

                    OutputAnnex.Add(dr["��������"].ToString());

                    num = num + 1;

                    AnnexName = dr["��������"].ToString();
                    IfOut = dr["�Ƿ����"].ToString();
                    TestCaseList = GetTestCaseList_OneAnnex_1(dr["��������"].ToString(), type1);

                    if (IfOut != "False")
                    {

                        string AnnexName1 = AnnexName.Substring(0, AnnexName.LastIndexOf("."));
                        NewFileName = "����" + num.ToString() + "_" + AnnexName1;
                        NewFileName = Path.Combine(AnnexMuLuName, NewFileName);

                        for (int j = 0; j <= TestCaseList.Count - 2; j++) TestCaseStr = TestCaseStr + TestCaseList[j].ToString() + "��";
                        TestCaseStr = TestCaseStr + TestCaseList[TestCaseList.Count - 1].ToString();

                        DocumentBuilder db = new DocumentBuilder(CurrentDoc);

                        db.MoveToMergeField("���������������");     
                                                             
                        db.Writeln("�������:" + num.ToString());
                    
                        db.Writeln("��������:" + AnnexName);
                     
                        db.Writeln("�漰����:" + TestCaseStr);
                     
                      //  if (AnnexType == ".doc")
                        {
                            System.Windows.Forms.Application.DoEvents();


                           // sqlstate = "select �ĵ����� from SYS�ĵ����ݱ� WHERE ���ݱ���=? and ��ĿID=? and ���԰汾=?";
                           //DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, contentTitle, ProjectID, TestVerID);





                            OutputComm.OutputComm.AddField("��������", db);

                            //Value = OutputComm.OutputComm.OLEObjectProcess(dt1, doc, "��������");
                            //if (Value != null)
                            //{
                            //    db.MoveToMergeField("��������");
                            //    db.Writeln(Value.ToString());
                      
                            //}

                        }

                        if (i < dv.Count-1)
                        {
                            OutputComm.OutputComm.AddField("���������������", db);
                        }
                       
                    }
                    
   //                 System.Windows.Forms.Application.DoEvents();

                }
            }
        }

        private void HandleMergeImage(object sender, MergeImageFieldEventArgs e)
        {
            if (e.FieldName.Equals("��������"))
            {               
                e.ImageFileName = tempfilename;
            }
        }
       
        public ArrayList GetTestCaseList_OneAnnex_1(string AnnexName,int type1)
        {

            ArrayList TestCaseList = new ArrayList();

            DataView dv = null;

            if (type1 == 1)
            {
                dv = TPM3.wx.AttachCaseTrace.GetAttachCaseTraceView(2);
            }
            else if (type1 == 2)
            {
                dv = TPM3.wx.AttachCaseTrace.GetAttachCaseTraceView(3);
            }

            for (int i = 0; i < dv.Count; i++)
            {
                DataRow dr = dv.Table.Rows[i];
                string name = dr["��������"].ToString();
                if (name == AnnexName)
                {
                    string TestCaseName = dr["������������"].ToString();

                    TestCaseList.Add(TestCaseName);
                }
            }
            return TestCaseList;
        }
    }
}