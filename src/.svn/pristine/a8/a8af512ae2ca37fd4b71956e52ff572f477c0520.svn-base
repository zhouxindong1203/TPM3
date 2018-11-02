using System;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using Word;
using System.Threading;
using System.Text;
using WordLastProcess;
using System.ComponentModel;

namespace TPM3.chq
{
    class WordProcess
    {
        public ArrayList AllFields = new ArrayList();
        public ArrayList AllFieldsCode = new ArrayList();
        public Word.Application WordApp;
        public object missing = System.Reflection.Missing.Value;
        public object unit_c = Word.WdUnits.wdCharacter;
        public object unit_l = Word.WdUnits.wdLine;
        public object movecount = 1;
        Word.Document doc = null;
        public Wait frmwait;
              
        public void WordUpdateProcess(ArrayList DocList)
        {
            string DocName = "";
            WordApp = new Word.Application();
            //WordApp.Visible = true;
            //WordApp.Application.WindowState = Word.WdWindowState.wdWindowStateMinimize;
            System.Windows.Forms.Application.DoEvents();

            for (int i = 0; i < DocList.Count; i++)
            {
                DocName = DocList[i].ToString();

                System.Windows.Forms.Application.DoEvents();
                doc = WordLastProcess.Class1.OpenFile(DocName, WordApp);

                frmwait = new Wait();
                frmwait.progressBar1.Maximum = 8;

                frmwait.Show();
                frmwait.progressBar1.Value = 1;
                frmwait.Refresh();

                System.Windows.Forms.Application.DoEvents();

                WordApp.ActiveWindow.View.ShowFieldCodes = true;

                System.Windows.Forms.Application.DoEvents();

                frmwait.progressBar1.Increment(1);
                frmwait.Refresh();

                WordLastProcess.Class1.GetAllFields(doc, AllFields, AllFieldsCode);

                frmwait.progressBar1.Increment(1);
                frmwait.Refresh();

                System.Windows.Forms.Application.DoEvents();

                WordLastProcess.Class1.UpdateDoc(AllFields, AllFieldsCode, WordApp);
                WordApp.ActiveDocument.Save();

                frmwait.progressBar1.Increment(1);
                frmwait.Refresh();

                System.Windows.Forms.Application.DoEvents();

                AllFields.Clear();
                AllFieldsCode.Clear();

                WordLastProcess.Class1.SetPageHeaderOrFooter(1, WordApp, AllFields, AllFieldsCode);

                System.Windows.Forms.Application.DoEvents();

                frmwait.progressBar1.Increment(1);
                frmwait.Refresh();

                System.Windows.Forms.Application.DoEvents();

                AllFields.Clear();
                AllFieldsCode.Clear();

                WordLastProcess.Class1.SetPageHeaderOrFooter(2, WordApp, AllFields, AllFieldsCode);

                System.Windows.Forms.Application.DoEvents();

                frmwait.progressBar1.Increment(1);
                frmwait.Refresh();

                System.Windows.Forms.Application.DoEvents();

                WordApp.ActiveWindow.View.ShowFieldCodes = false;
                WordApp.ActiveWindow.View.ShowPicturePlaceHolders = false;
                WordApp.ActiveDocument.Save();

                System.Windows.Forms.Application.DoEvents();

                frmwait.progressBar1.Increment(1);
                frmwait.Refresh();

                //object what = Word.WdGoToItem.wdGoToLine;
                //object which = Word.WdGoToDirection.wdGoToFirst;
                //object name = "";

                //WordApp.Selection.GoTo(ref what, ref which, ref movecount, ref name);

                //frmwait.progressBar1.Increment(1);
                //frmwait.Refresh();  

                frmwait.Close();

            }
            WordApp.Visible = true;
            WordApp.Application.WindowState = Word.WdWindowState.wdWindowStateMinimize;
                         
        }
    }
}
