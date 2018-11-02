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
    class HGFL_Question
    {

        public Document ExecuteOutput_HGFL_Question(string DocPath, string docName, ArrayList DataTreeList, string SaveFileName,string ProjectID,string TestVerID)
        {

            wait_output frmwait;

            frmwait = new wait_output();
            frmwait.progressBar1.Maximum = 6;
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

            outputdoc.OutputStartTable();

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            outputdoc.OutputChapter_QuestionReport(doc);

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

            outputdoc.ReplaceContent();

            frmwait.progressBar1.Increment(1);
            frmwait.Refresh();
            System.Windows.Forms.Application.DoEvents();

            doc.Save(SaveFileName);

            frmwait.Close();

            return doc;

        }
    }
}
