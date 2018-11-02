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
    class DXBG
    {
        public Document ExecuteOutput_DXBG(string DocPath, string docName, ArrayList DataTreeList, string SaveFileName, string ProjectID, string TestVerID, ArrayList TestVerList)
        {
            int tableno = 0;
            wait_output frmwait;

            frmwait = new wait_output();
            frmwait.progressBar1.Maximum = 11;
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

            outputdoc.OutputChapter_TestExecuteInfo("可变章节_测试执行情况说明", doc, TestVerList);
            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();
           
            
            outputdoc.OutputChapter_HGSum("测试总结章节", "可变章节_回归测试总结", doc, TestVerList, "定型");
            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            outputdoc.OutputChapter_ProSum("问题总结章节", "可变章节_问题总结", doc, TestVerList);

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

           
            outputdoc.OutputChapter_HaveChangelessSonChapter("质量评估章节", "可变章节_质量评估", doc);

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            outputdoc.OutputChapter_UnderHaveTable_HG("提交问题一览", "提交问题一览表", "提交问题统计", "可变章节_回归被测对象提交问题一览", doc, TestVerList);

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            outputdoc.TestVerID = TestVerID;//+++++++++
            outputdoc.OutputStartTable();

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

