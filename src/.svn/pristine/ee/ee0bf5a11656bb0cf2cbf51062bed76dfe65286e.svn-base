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


namespace TPM3.chq
{
    class TR
    {
        public Document ExecuteOutput_TR(string DocPath, string docName, ArrayList DataTreeList, string SaveFileName,string[] AnnexList,string ProjectID,string TestVerID)
        {

            wait_output frmwait;

            frmwait = new wait_output();
            frmwait.progressBar1.Maximum = 9;
            System.Windows.Forms.Application.DoEvents();

            frmwait.Show();
            frmwait.progressBar1.Value = 1;
            System.Windows.Forms.Application.DoEvents();

            OutputDoc outputdoc = new OutputDoc();

            Document doc = new Document(DocPath);

            outputdoc.DataTreeList = DataTreeList;
            outputdoc.DocumentName = docName;
            outputdoc.CurrentDoc = doc;
            outputdoc.ProjectID = ProjectID;
            outputdoc.TestVerID = TestVerID;

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            outputdoc.AddCoverInfo();

            int AnnexNum = 0;
            int AllNum = 0;

            //string[] strlist = new string[3] { "测试依据", "测试依据与测试项的追踪关系", "测试项与测试用例的追踪关系" };
            //foreach (string str in strlist)
            //{
            //    ProcessOutputAnnex_NoHave(doc, str, AnnexList, ref AnnexNum, ref AllNum);

            //}

            outputdoc.OutputChapter_HaveChangelessSonChapter("测试总结章节", "可变章节_测试总结", doc);
           
            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            outputdoc.OutputChapter_HaveChangelessSonChapter("质量评估章节", "可变章节_质量评估", doc);

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            outputdoc.OutputChapter_UnderHaveTable("提交问题一览", "提交问题一览表", "提交问题统计", "可变章节_被测对象提交问题一览", doc);

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            outputdoc.OutputStartTable();

            //if (IfAnnexHave("测试项与测试用例的追踪关系", AnnexList) == true)
            //{
            //    outputdoc.OutputChapter_TestCaseAccording(true);
            //}

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            string[] UnOrdFields = new string[2] { "交办方信息", "测试项目基本信息" };
            foreach (string tablename in UnOrdFields)
            {
                outputdoc.FillUnOrdFields(tablename);
            }

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            outputdoc.ReplaceContent();

            AsposeCommon.RemoveSectionBreaks(outputdoc.CurrentDoc, SectionStart.Continuous);

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            doc.Save(SaveFileName);

            frmwait.Close();

            return doc;

        }

        public bool IfAnnexHave(string AnnexName, string[] AnnexList)
        {
            bool HaveFlag = false;
            foreach (string OutputAnnex in AnnexList)
            {
                if (OutputAnnex == AnnexName)
                {
                    HaveFlag = true;
                }
            }

            return HaveFlag;

        }

        public void ProcessOutputAnnex_NoHave(Document doc, string AnnexName, string[] AnnexList, ref int CurrentNum, ref int AllNum)
        {
            bool HaveFlag = false;
            Section Section;
                      
            foreach (string OutputAnnex in AnnexList) 
            {
                if (OutputAnnex == AnnexName)
                {
                    HaveFlag = true;
                }
            }

            if (HaveFlag == false)
            {
                AllNum = AllNum + 1;
                DocumentBuilder db = new DocumentBuilder(doc);
                if (AnnexName == "测试项与测试用例的追踪关系")
                {
                    Section = OutputComm.OutputComm.GetNodeInSection(AnnexName + "1", doc, db);
                    if (Section != null)
                    {
                        Section.Range.Delete();
                    }
                    Section = OutputComm.OutputComm.GetNodeInSection(AnnexName + "2", doc, db);
                    if (Section != null)
                    {
                        Section.Range.Delete();
                    }
                    Section = OutputComm.OutputComm.GetNodeInSection(AnnexName + "3", doc, db);
                    if (Section != null)
                    {
                        Section.Range.Delete();
                    }
                }
                else
                {
                    Section = OutputComm.OutputComm.GetNodeInSection(AnnexName, doc, db);
                    if (Section != null)
                    {
                        Section.Range.Delete();
                    }

                }

            }
            else
            {
                AllNum = AllNum + 1;
                CurrentNum = CurrentNum + 1;
          
                DocumentBuilder db = new DocumentBuilder(doc);

                while (true)
                {
                    if (db.MoveToMergeField("附录" + AllNum.ToString()) != false)
                    {
                      if (CurrentNum==1)
                      {
                          db.Write("附录A");
                      }
                      else if (CurrentNum==2)
                      {
                          db.Write("附录B");
                      }
                      else if (CurrentNum==3)
                      {
                          db.Write("附录C");
                      }
                        
                    }
                    else
                    {
                        break;
                    }
                }
            }

        }

        
    }
}
