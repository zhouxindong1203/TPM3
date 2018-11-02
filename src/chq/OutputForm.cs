using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using Common;
using Aspose.Words;
using TPM3.Sys;


namespace TPM3.chq
{
    [TypeNameMap("chq.OutputForm")]
    public partial class OutputForm : MyBaseForm
    {
        ArrayList DataTreeList = null;
        public string DocPath;

        public string ProjectID = "";
        public string TestVerID = "";

        public static string CurrentDocName = "";

             
        public OutputForm()
        {
            InitializeComponent();
        }

        public void GetDataTreeList(string TestVerID)
        {
            NodeTree nodetree = new NodeTree();
            nodetree.TestVerID = TestVerID;
            DataTreeList = nodetree.PutNodeToLayerList();
        }

      
        public string GetProjectName()
        {
            string projectname = "";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("SELECT SYS文档内容表.文本内容 FROM SYS文档内容表 WHERE SYS文档内容表.内容标题='项目名称' AND SYS文档内容表.文档名称='项目信息' and SYS文档内容表.项目ID=" + "'" + ProjectID + "'");
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    projectname = dr[0].ToString();

                }
            }

            return projectname;

        }
        public string ThrowlawlessChar(string projectname)
        {
            string newstr = projectname;
            string[] strlist = new string[] {@"\","/",":","：","*","?","<",">","|" };
            foreach(string str in strlist)
            {
                int pos1 = projectname.IndexOf(str);
                if (pos1 != -1)
                {
                    newstr = projectname.Replace(str, "-");
                }
            }

            return newstr;
          
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string CurrentOutputDoc = "";
            string OutputFileName = "";
            string DocPath = "";
            string SelectTemplateDoc = "";

            ProjectID = TPM3.Sys.GlobalData.globalData.projectID.ToString();
            TestVerID = TPM3.Sys.GlobalData.globalData.currentvid.ToString();

            GetDataTreeList(TestVerID);

            string s = Assembly.GetEntryAssembly().Location;
            s = Path.GetDirectoryName(s);
            s = Path.Combine(s, @GlobalData.BaseDirectory + "dot"); 

              if (listBox2.SelectedItem != null)
              {
                  SelectTemplateDoc = s + "\\" + listBox2.SelectedItem.ToString();
              }
              else
              {
                  MessageBox.Show("请选择输出模板！");
                  return;
              }

               s = GlobalData.GetProjectDirName();

                switch (listBox1.SelectedItem.ToString())
                {
                    case "软件测试需求规格说明":

                        CurrentOutputDoc = "需求分析";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件测试需求分析.doc");
                        break;

                    case "软件测试计划":

                        CurrentOutputDoc = "测试计划";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件测试计划.doc");
                        break;

                    case "软件测试说明":

                        CurrentOutputDoc = "测试说明";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件测试说明.doc");
                        break;

                    case "软件测试报告":

                        CurrentOutputDoc = "测试总结";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件测试报告.doc");
                        break;

                    case "软件回归测试方案":
                        CurrentOutputDoc = "回归测试方案";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件回归测试方案.doc");
                        break;

                    //case "软件回归测试文档":

                    //    CurrentOutputDoc = "回归测试文档";
                    //    OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件回归测试文档.doc");
                    //    break;

                    case "软件总测试报告":

                        CurrentOutputDoc = "回归测试报告";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件测试报告.doc");
                        break;

                    case "软件回归测试说明":

                        CurrentOutputDoc = "回归测试说明";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件回归测试说明.doc");
                        break;

                    case "软件回归测试记录表单":

                        CurrentOutputDoc = "回归测试记录";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件回归测试记录表单.doc");
                        break;

                    case "软件回归测试记录实测结果":

                        CurrentOutputDoc = "回归测试记录";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件回归测试记录.doc");
                        break;

                    case "软件回归测试问题报告":

                        CurrentOutputDoc = "回归问题报告";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件回归问题报告.doc");
                        break;


                    case "软件测试记录表单":

                        CurrentOutputDoc = "测试记录";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件测试记录表单.doc");
                        break;

                    case "软件测试记录实测结果":

                        CurrentOutputDoc = "测试记录";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件测试记录.doc");
                        break;

                    case "软件问题报告":

                        CurrentOutputDoc = "问题报告";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_软件问题报告.doc");
                        break;

                    case "将附件导入到附件文件夹":
                        Annexs Annexs = new Annexs();

                        XmlElement ele = docTN.nodeElement;

                        string title1 = ele.GetAttribute("step");

                        if (title1 == "测试设计")
                        {
                            System.Windows.Forms.Application.DoEvents();

                            Annexs.PutAnnexIntoNewFile(SelectTemplateDoc, 1);
                        }
                        else if (title1 == "测试实施")
                        {
                            System.Windows.Forms.Application.DoEvents();

                            Annexs.PutAnnexIntoNewFile(SelectTemplateDoc, 2);

                        }
                        MessageBox.Show("附件已输出到相应文件夹下!");
                        return;
                       

                }

                SaveFileDialog fd = new SaveFileDialog();
                fd.Filter = "Microsoft Word 文档 (*.doc)|*.doc|All files (*.*)|*.*";
                fd.FileName = OutputFileName;
                fd.Title = "确定测试文件名称";
                if (fd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                else
                {
                    System.Windows.Forms.Application.DoEvents();
                   
                    DocPath = fd.FileName;
         //           OutputProcess(CurrentOutputDoc, SelectTemplateDoc, DocPath);

                }
              
                WordProcess WorP = new WordProcess();
              //  WorP.WordUpdateProcess(DocPath);
 
                MessageBox.Show("文档输出完毕!");

        }

        public void OutputProcess(string CurrentOutputDoc, string SelectTemplateDoc, string DocPath, bool IfOutputAnnex)
        {

            if ((listBox1.SelectedItem.ToString() == "软件测试记录表单") || (listBox1.SelectedItem.ToString() == "软件测试记录实测结果"))
            {
                TPM3.chq.TL tl1 = new TPM3.chq.TL();
                tl1.ExecuteOutput_TL(SelectTemplateDoc, "测试记录", DataTreeList, DocPath, listBox1.SelectedItem.ToString(), ProjectID, TestVerID, IfOutputAnnex);
            }

            else if ((listBox1.SelectedItem.ToString() == "软件回归测试记录表单") || (listBox1.SelectedItem.ToString() == "软件回归测试记录实测结果"))
            {
                //TPM3.chq.HGFL_Record tl3 = new TPM3.chq.HGFL_Record();
                //tl3.ExecuteOutput_HGFL_Record(SelectTemplateDoc, "回归测试记录", DataTreeList, DocPath, listBox1.SelectedItem.ToString(), ProjectID, TestVerID);
                TPM3.chq.TL tl2 = new TPM3.chq.TL();
                tl2.ExecuteOutput_TL(SelectTemplateDoc, "回归测试记录", DataTreeList, DocPath, listBox1.SelectedItem.ToString(), ProjectID, TestVerID, IfOutputAnnex);

            }             
            else
            {
                switch (listBox1.SelectedItem.ToString())
                {
                    case "软件测试需求规格说明":
                        TPM3.chq.TA ta1 = new TPM3.chq.TA();
                        ta1.ExecuteOutput_TA(SelectTemplateDoc, "需求分析", DataTreeList, DocPath, ProjectID, TestVerID, IfOutputAnnex);
                        break;

                    case "软件测试计划":
                        TPM3.chq.TP tp1 = new TPM3.chq.TP();
                        tp1.ExecuteOutput_TP(SelectTemplateDoc, "测试计划", DataTreeList, DocPath, ProjectID, TestVerID, IfOutputAnnex);
                        break;

                    case "软件测试说明":
                        TPM3.chq.TS ts1 = new TPM3.chq.TS();
                        ts1.ExecuteOutput_TS(SelectTemplateDoc, "测试说明", DataTreeList, DocPath, ProjectID, TestVerID, IfOutputAnnex);
                        break;

                    case "软件测试报告":

                        TPM3.chq.TR tr1 = new TPM3.chq.TR();
                        
                        //string[] OutputAnnexList = new string[listBox3.SelectedItems.Count];
                        //for (int l = 0; l <= listBox3.SelectedItems.Count - 1; l++)
                        //{
                        //     string AnnexName = listBox3.SelectedItems[l].ToString();
                        //     OutputAnnexList[l] = AnnexName;
                        //}
                        tr1.ExecuteOutput_TR(SelectTemplateDoc, "测试总结", DataTreeList, DocPath, null, ProjectID, TestVerID);                  
                       
                        break;

                    case "软件问题报告":
                        TPM3.chq.PR pr2 = new TPM3.chq.PR();
                        pr2.ExecuteOutput_PR(SelectTemplateDoc, "问题报告", DataTreeList, DocPath, ProjectID, TestVerID);
                        break;

                   
                    case "软件回归测试方案":
                        TPM3.chq.HGFA HGFA = new TPM3.chq.HGFA();
                        //HGFA.ExecuteOutput_HGFA(SelectTemplateDoc, "测试总结", DataTreeList, DocPath, ProjectID, TestVerID);
                        HGFA.ExecuteOutput_HGFA(SelectTemplateDoc, "回归测试方案", DataTreeList, DocPath, ProjectID, TestVerID, IfOutputAnnex);
                        break;

                    //case "软件回归测试文档":
                    //    TPM3.chq.HGWD HGWD = new TPM3.chq.HGWD();
                    //    HGWD.ExecuteOutput_HGWD(SelectTemplateDoc, "回归测试文档", DataTreeList, DocPath, ProjectID, TestVerID);
                    //    break;

                    case "软件总测试报告":
                        TPM3.chq.HG hg1 = new TPM3.chq.HG();
                       // hg1.ExecuteOutput_HG(SelectTemplateDoc, "测试总结", DataTreeList, DocPath, ProjectID, TestVerID);
                      //  hg1.ExecuteOutput_HG(SelectTemplateDoc, "回归测试报告", DataTreeList, DocPath, ProjectID, TestVerID);
                        
                       break;

                    case "软件回归测试说明":
                        //TPM3.chq.HGFL_TestCase ts2 = new TPM3.chq.HGFL_TestCase();
                        //ts2.ExecuteOutput_HGFL_TestCase(SelectTemplateDoc, "回归测试说明", DataTreeList, DocPath, ProjectID, TestVerID);
                        TPM3.chq.TS ts2 = new TPM3.chq.TS();
                        ts2.ExecuteOutput_TS(SelectTemplateDoc, "回归测试说明", DataTreeList, DocPath, ProjectID, TestVerID, IfOutputAnnex);
                        break;

                    case "软件回归测试问题报告":
                        TPM3.chq.HGFL_Question pr1 = new TPM3.chq.HGFL_Question();
                        pr1.ExecuteOutput_HGFL_Question(SelectTemplateDoc, "回归问题报告", DataTreeList, DocPath, ProjectID, TestVerID);
                        break;

                }
            }

        }

        public string GetPathOfFile()
        {
            int position = DocPath.LastIndexOf("\\");
            string Path = DocPath.Substring(0, position);

            if (Path.IndexOf("\\") == -1)
            {
                Path = Path + "\\";
            }

            return Path;

        }

        public bool IfFirstTest()
        {
            string ID = "";
            bool value = false;

            ProjectID = TPM3.Sys.GlobalData.globalData.projectID.ToString();
            TestVerID = TPM3.Sys.GlobalData.globalData.currentvid.ToString();


            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("SELECT SYS测试版本表.ID, SYS测试版本表.序号, SYS测试版本表.项目ID FROM SYS测试版本表 WHERE (SYS测试版本表.前向版本ID) Is Null AND SYS测试版本表.项目ID=?", ProjectID);
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ID = dr[0].ToString();

                }
            }

            if (TestVerID == ID)         
            {
                value = true;
            }
            else
            {
                value = false;
            }

            return value;

        }



        private void OutputForm_Load(object sender, EventArgs e)
        {           
            XmlElement ele = docTN.nodeElement;

            string title1 = ele.GetAttribute("step");
            object outputitem = null;

            //listBox3.Enabled = false;

            if (title1 == "需求分析")
            {
                listBox1.Items.Clear();
                outputitem = "软件测试需求规格说明";
                listBox1.Items.Add(outputitem);
               // SetLocation(false);
             
            }
            else if (title1 == "测试计划")
            {
                if (IfFirstTest() == true)
                {
                    listBox1.Items.Clear();
                    outputitem = "软件测试计划";
                    listBox1.Items.Add(outputitem);
                    //SetLocation(false);
                }
                else
                {
                    listBox1.Items.Clear();
                    outputitem = "软件测试计划";
                    listBox1.Items.Add(outputitem);
                    //SetLocation(false);

                    outputitem = "软件回归测试方案";
                    listBox1.Items.Add(outputitem);

                }

            }
            else if (title1 == "测试设计")
            {
                listBox1.Items.Clear();
                outputitem = "软件测试说明";
                listBox1.Items.Add(outputitem);
                outputitem = "软件测试记录表单";
                listBox1.Items.Add(outputitem);
                outputitem = "将附件导入到附件文件夹";
                listBox1.Items.Add(outputitem);
               // SetLocation(false);


            }
            else if (title1 == "测试实施")
            {
                if (IfFirstTest() == true)
                {
                    listBox1.Items.Clear();
                    outputitem = "软件测试记录实测结果";
                    listBox1.Items.Add(outputitem);
                    outputitem = "软件问题报告";
                    listBox1.Items.Add(outputitem);
                    outputitem = "将附件导入到附件文件夹";
                    listBox1.Items.Add(outputitem);
                    // SetLocation(false);
                }
                else
                {
                    listBox1.Items.Clear();
                    outputitem = "软件测试记录实测结果";
                    listBox1.Items.Add(outputitem);
                    outputitem = "软件回归测试问题报告";
                    listBox1.Items.Add(outputitem);
                    outputitem = "将附件导入到附件文件夹";
                    listBox1.Items.Add(outputitem);
                    // SetLocation(false);

                }

            }
            else if (title1 == "测试总结")
            {
                //listBox3.Enabled = true;

                if (IfFirstTest() == true)
                {
                    listBox1.Items.Clear();
                    outputitem = "软件测试报告";
                    listBox1.Items.Add(outputitem);
                    outputitem = "软件问题报告";
                    listBox1.Items.Add(outputitem);


                }
                else
                {
                    listBox1.Items.Clear();

                    //outputitem = "软件回归测试方案";
                    //listBox1.Items.Add(outputitem);

                    //outputitem = "软件回归测试文档";
                    //listBox1.Items.Add(outputitem);

                    outputitem = "软件总测试报告";
                    listBox1.Items.Add(outputitem);

                    outputitem = "软件回归测试问题报告";
                    listBox1.Items.Add(outputitem);

                    //outputitem = "附录：软件回归测试说明";
                    //listBox1.Items.Add(outputitem);

                    //outputitem = "附录：软件回归测试记录表单";
                    //listBox1.Items.Add(outputitem);

                    //outputitem = "附录：软件回归测试记录实测结果";
                    //listBox1.Items.Add(outputitem);

                  


                }

               
                //SetLocation(true);

                          
            }
            else if (title1=="")//回归测试报告
            {
               // SetLocation(false);

                listBox1.Items.Clear();

                outputitem = "软件回归测试方案";
                listBox1.Items.Add(outputitem);

                //outputitem = "软件回归测试文档";
                //listBox1.Items.Add(outputitem);

                outputitem = "软件回归测试说明";
                listBox1.Items.Add(outputitem);

                outputitem = "软件回归测试记录表单";
                listBox1.Items.Add(outputitem);

                outputitem = "软件回归测试记录实测结果";
                listBox1.Items.Add(outputitem);

                outputitem = "软件回归测试问题报告";
                listBox1.Items.Add(outputitem);

                outputitem = "软件总测试报告";
                listBox1.Items.Add(outputitem);
            }

            listBox1.SelectedIndex = 0;
            //if (listBox1.SelectedItem.ToString() == "软件测试报告")
            //{
            //    Set_AnnexSelect();
            //}
          
        }

        //public void SetLocation(bool flag)
        //{
        //    if (flag == true)
        //    {
        //        groupBox3.Visible = true;
        //        groupBox4.Location = new System.Drawing.Point(294, 280);

        //    }
        //    else
        //    {
        //        groupBox3.Visible = false;
        //        groupBox4.Location = new System.Drawing.Point(88, 280);
        //    }
        //}

        public void GetTemplate(string FileName)
        {
            listBox2.Items.Clear();

            string Filter = "";

            string s = Assembly.GetEntryAssembly().Location;
            s = Path.GetDirectoryName(s);
            s = Path.Combine(s, @GlobalData.BaseDirectory + "dot");

            switch (FileName)
            {
                case "软件测试需求规格说明":
                    Filter = "测试需求规格说明模板*.doc";
                    break;

                case "软件测试计划":
                    Filter = "测试计划模板*.doc";
                    break;

                case "软件测试说明":
                    Filter = "测试说明模板*.doc";
                    break;

                case "软件测试报告":
                    Filter = "测试报告模板*.doc";
                    break;

                case "软件回归测试方案":
                    Filter = "回归测试方案模板*.doc";
                    break;

                //case "软件回归测试文档":
                //    Filter = "回归测试文档模板*.doc";
                //    break;

                case "软件总测试报告":
                    Filter = "回归测试报告模板*.doc";
                    break;

                case "软件回归测试说明":
                    Filter = "回归测试说明模板*.doc";
                    break;

                case "软件回归测试记录表单":
                    Filter = "回归测试记录表单模板*.doc";
                    break;

                case "软件回归测试记录实测结果":
                    Filter = "回归测试记录结果模板*.doc";
                    break;

                case "软件回归测试问题报告":
                    Filter = "回归问题报告模板*.doc";
                    break;
                    
                case "软件测试记录表单":
                    Filter = "测试记录表单模板*.doc";
                    break;

                case "软件测试记录实测结果":
                    Filter = "测试记录结果模板*.doc";
                    break;

                case "软件问题报告":
                    Filter = "问题报告模板*.doc";
                    break;

                case "将附件导入到附件文件夹":
                    Filter = "文档附件模板*.doc";
                    break;

            }

            string[] Files = Directory.GetFiles(s, Filter);

            for (int i = 0; i <= Files.Length - 1; i++)
            {
                string TemplateName = Files[i].ToString();
                int pos = TemplateName.LastIndexOf("\\");

                TemplateName = TemplateName.Substring(pos + 1, TemplateName.Length - pos - 1);
                listBox2.Items.Add(TemplateName);

            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FileName = listBox1.SelectedItem.ToString();

            switch (listBox1.SelectedItem.ToString())
            {
                case "软件测试需求规格说明":                   
                    CurrentDocName = "需求分析";
                    button2.Visible = true;
                    break;

                case "软件测试计划":
                    CurrentDocName = "测试计划";
                    button2.Visible = true;
                    break;

                case "软件测试说明":
                    CurrentDocName = "测试说明";
                    button2.Visible = true;
                    break;

                case "软件测试报告":
                    CurrentDocName = "测试总结";
                    button2.Visible = true;
                    break;

                case "软件回归测试方案":
                    CurrentDocName = "回归测试方案";
                    button2.Visible = true;
                    break;

                //case "软件回归测试文档":
                //    CurrentDocName = "回归测试文档";
                //    button2.Visible = true;
                //    break;

                case "软件总测试报告":
                    //CurrentDocName = "测试总结";
                    CurrentDocName = "回归测试报告";
                    button2.Visible = true;
                    break;

                case "软件回归测试说明":
                    CurrentDocName = "回归测试说明";
                    button2.Visible = true;
                    break;

                case "软件回归测试记录表单":
                    CurrentDocName = "回归测试记录";
                    button2.Visible = true;
                    break;

                case "软件回归测试记录实测结果":
                    CurrentDocName = "回归测试记录";
                    button2.Visible = true;
                    break;

                case "软件回归测试问题报告":
                    CurrentDocName = "回归问题报告";
                    button2.Visible = true;
                    break;

                case "软件测试记录表单":
                    CurrentDocName = "测试记录";
                    button2.Visible = true;
                    break;

                case "软件测试记录实测结果":
                    CurrentDocName = "测试记录";
                    button2.Visible = true;
                    break;

                case "软件问题报告":
                    CurrentDocName = "问题报告";
                    button2.Visible = true;
                    break;

              
            }

            GetTemplate(FileName);

            //if (FileName == "软件测试报告")
            //{
            //    Set_AnnexSelect();
            //}
            //else
            //{
            //    listBox3.Items.Clear();
            //}

        }

        private void listBox1_Click(object sender, EventArgs e)
        {

            string FileName = listBox1.SelectedItem.ToString();

            GetTemplate(FileName);

            //if (FileName == "软件测试报告")
            //{
            //    Set_AnnexSelect();
            //}
            //else
            //{
            //    listBox3.Items.Clear();
            //}
            
            textBox1.Text = "";

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string s = Assembly.GetEntryAssembly().Location;
            s = Path.GetDirectoryName(s);
            s = Path.Combine(s, @GlobalData.BaseDirectory + "dot");

            string TemplateName = s + "\\" + listBox2.SelectedItem.ToString();

            Document doc = new Document(TemplateName);
            string TemplateInfo = doc.BuiltInDocumentProperties.Comments;

            //if (TemplateInfo=="")
            //{
            //    textBox1.Text = "无。";
            //}
            //else
            {
                textBox1.Text = TemplateInfo;
            }

        }

        //public void Set_AnnexSelect()
        //{
        //    listBox3.Items.Clear();

            //XmlElement ele = tn.nodeElement;
            //string title1 = ele.GetAttribute("step");
            
            //if (title1 == "")
            //{
            //    listBox3.Items.Add("回归测试用例");
            //    listBox3.Items.Add("回归测试记录");
            //    listBox3.Items.Add("回归测试问题报告");

            //}
            //else
        //    {
        //        listBox3.Items.Add("测试依据");
        //        listBox3.Items.Add("测试依据与测试项的追踪关系");
        //        listBox3.Items.Add("测试项与测试用例的追踪关系");

        //    }
          
        //}

        private void button2_Click_1(object sender, EventArgs e)
        {
            文档修改页 modpage = new 文档修改页();
            modpage.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cover DocCover = new Cover();
            DocCover.ShowDialog();
        }

      
    

    }
}