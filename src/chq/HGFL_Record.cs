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
    class HGFL_Record
    {

        public Document ExecuteOutput_HGFL_Record(string DocPath, string docName, ArrayList DataTreeList, string SaveFileName, string DocSonName, string ProjectID, string TestVerID, bool IfOutputAnnex)
        {

            wait_output frmwait;

            frmwait = new wait_output();
            frmwait.progressBar1.Maximum = 3;
            System.Windows.Forms.Application.DoEvents();

            frmwait.Show();
            frmwait.progressBar1.Value = 1;
            System.Windows.Forms.Application.DoEvents();

            OutputDoc outputdoc = new OutputDoc();

            Document doc = new Document(DocPath);

            outputdoc.DataTreeList = DataTreeList;
            outputdoc.DocumentName = docName;
            outputdoc.CurrentDoc = doc;
            outputdoc.DocSonName = DocSonName;
            outputdoc.ProjectID = ProjectID;
            outputdoc.TestVerID = TestVerID;

            outputdoc.AddCoverInfo();

            outputdoc.OutputChapter_ChangeMuiChapter("测试用例", "总结", false, "", IfOutputAnnex);

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
