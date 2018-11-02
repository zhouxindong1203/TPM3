﻿using System.Collections;
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
    class HGFA
    {

        public Document ExecuteOutput_HGFA(string DocPath, string docName, ArrayList DataTreeList, string SaveFileName, string ProjectID, string TestVerID, bool IfOutputAnnex)
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

          //  outputdoc.OutputChapter_AlreadyHaveItem(DataTreeList);
           
            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

          //  outputdoc.OutputChapter_AlreadyHaveCase(DataTreeList);

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

           // outputdoc.OutputChapter_HGNewAdd("测试项", "");

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

           // outputdoc.OutputChapter_HGNewAdd("测试用例", "");

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();
          
            outputdoc.OutputStartTable();

            int StartNo = 0;
            int flag = 0;
            DocumentBuilder db = new DocumentBuilder(doc);
            while (flag == 0)
            {
                Node node1 = OutputComm.OutputComm.GetNodeByField(doc, "个数", db);
                if (node1 != null)
                {  
                    StartNo = StartNo + 1;                  
                    db.Write(StartNo.ToString());
                }
                else
                {
                   flag =1;
                }
            }
          
            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            outputdoc.OutputChapter_ChangeMuiChapter("测试用例", "设计", true, "", IfOutputAnnex);

            
            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();
           
            outputdoc.ReplaceContent();

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            AsposeCommon.RemoveSectionBreaks(outputdoc.CurrentDoc, SectionStart.Continuous);

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            doc.Save(SaveFileName);

            frmwait.Close();

            return doc;

        }
    }
}
