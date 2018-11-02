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

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("SELECT SYS�ĵ����ݱ�.�ı����� FROM SYS�ĵ����ݱ� WHERE SYS�ĵ����ݱ�.���ݱ���='��Ŀ����' AND SYS�ĵ����ݱ�.�ĵ�����='��Ŀ��Ϣ' and SYS�ĵ����ݱ�.��ĿID=" + "'" + ProjectID + "'");
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
            string[] strlist = new string[] {@"\","/",":","��","*","?","<",">","|" };
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
                  MessageBox.Show("��ѡ�����ģ�壡");
                  return;
              }

               s = GlobalData.GetProjectDirName();

                switch (listBox1.SelectedItem.ToString())
                {
                    case "�������������˵��":

                        CurrentOutputDoc = "�������";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_��������������.doc");
                        break;

                    case "������Լƻ�":

                        CurrentOutputDoc = "���Լƻ�";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_������Լƻ�.doc");
                        break;

                    case "�������˵��":

                        CurrentOutputDoc = "����˵��";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_�������˵��.doc");
                        break;

                    case "������Ա���":

                        CurrentOutputDoc = "�����ܽ�";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_������Ա���.doc");
                        break;

                    case "����ع���Է���":
                        CurrentOutputDoc = "�ع���Է���";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_����ع���Է���.doc");
                        break;

                    //case "����ع�����ĵ�":

                    //    CurrentOutputDoc = "�ع�����ĵ�";
                    //    OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_����ع�����ĵ�.doc");
                    //    break;

                    case "����ܲ��Ա���":

                        CurrentOutputDoc = "�ع���Ա���";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_������Ա���.doc");
                        break;

                    case "����ع����˵��":

                        CurrentOutputDoc = "�ع����˵��";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_����ع����˵��.doc");
                        break;

                    case "����ع���Լ�¼��":

                        CurrentOutputDoc = "�ع���Լ�¼";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_����ع���Լ�¼��.doc");
                        break;

                    case "����ع���Լ�¼ʵ����":

                        CurrentOutputDoc = "�ع���Լ�¼";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_����ع���Լ�¼.doc");
                        break;

                    case "����ع�������ⱨ��":

                        CurrentOutputDoc = "�ع����ⱨ��";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_����ع����ⱨ��.doc");
                        break;


                    case "������Լ�¼��":

                        CurrentOutputDoc = "���Լ�¼";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_������Լ�¼��.doc");
                        break;

                    case "������Լ�¼ʵ����":

                        CurrentOutputDoc = "���Լ�¼";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_������Լ�¼.doc");
                        break;

                    case "������ⱨ��":

                        CurrentOutputDoc = "���ⱨ��";
                        OutputFileName = Path.Combine(s, ThrowlawlessChar(GetProjectName()) + "_������ⱨ��.doc");
                        break;

                    case "���������뵽�����ļ���":
                        Annexs Annexs = new Annexs();

                        XmlElement ele = docTN.nodeElement;

                        string title1 = ele.GetAttribute("step");

                        if (title1 == "�������")
                        {
                            System.Windows.Forms.Application.DoEvents();

                            Annexs.PutAnnexIntoNewFile(SelectTemplateDoc, 1);
                        }
                        else if (title1 == "����ʵʩ")
                        {
                            System.Windows.Forms.Application.DoEvents();

                            Annexs.PutAnnexIntoNewFile(SelectTemplateDoc, 2);

                        }
                        MessageBox.Show("�������������Ӧ�ļ�����!");
                        return;
                       

                }

                SaveFileDialog fd = new SaveFileDialog();
                fd.Filter = "Microsoft Word �ĵ� (*.doc)|*.doc|All files (*.*)|*.*";
                fd.FileName = OutputFileName;
                fd.Title = "ȷ�������ļ�����";
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
 
                MessageBox.Show("�ĵ�������!");

        }

        public void OutputProcess(string CurrentOutputDoc, string SelectTemplateDoc, string DocPath, bool IfOutputAnnex)
        {

            if ((listBox1.SelectedItem.ToString() == "������Լ�¼��") || (listBox1.SelectedItem.ToString() == "������Լ�¼ʵ����"))
            {
                TPM3.chq.TL tl1 = new TPM3.chq.TL();
                tl1.ExecuteOutput_TL(SelectTemplateDoc, "���Լ�¼", DataTreeList, DocPath, listBox1.SelectedItem.ToString(), ProjectID, TestVerID, IfOutputAnnex);
            }

            else if ((listBox1.SelectedItem.ToString() == "����ع���Լ�¼��") || (listBox1.SelectedItem.ToString() == "����ع���Լ�¼ʵ����"))
            {
                //TPM3.chq.HGFL_Record tl3 = new TPM3.chq.HGFL_Record();
                //tl3.ExecuteOutput_HGFL_Record(SelectTemplateDoc, "�ع���Լ�¼", DataTreeList, DocPath, listBox1.SelectedItem.ToString(), ProjectID, TestVerID);
                TPM3.chq.TL tl2 = new TPM3.chq.TL();
                tl2.ExecuteOutput_TL(SelectTemplateDoc, "�ع���Լ�¼", DataTreeList, DocPath, listBox1.SelectedItem.ToString(), ProjectID, TestVerID, IfOutputAnnex);

            }             
            else
            {
                switch (listBox1.SelectedItem.ToString())
                {
                    case "�������������˵��":
                        TPM3.chq.TA ta1 = new TPM3.chq.TA();
                        ta1.ExecuteOutput_TA(SelectTemplateDoc, "�������", DataTreeList, DocPath, ProjectID, TestVerID, IfOutputAnnex);
                        break;

                    case "������Լƻ�":
                        TPM3.chq.TP tp1 = new TPM3.chq.TP();
                        tp1.ExecuteOutput_TP(SelectTemplateDoc, "���Լƻ�", DataTreeList, DocPath, ProjectID, TestVerID, IfOutputAnnex);
                        break;

                    case "�������˵��":
                        TPM3.chq.TS ts1 = new TPM3.chq.TS();
                        ts1.ExecuteOutput_TS(SelectTemplateDoc, "����˵��", DataTreeList, DocPath, ProjectID, TestVerID, IfOutputAnnex);
                        break;

                    case "������Ա���":

                        TPM3.chq.TR tr1 = new TPM3.chq.TR();
                        
                        //string[] OutputAnnexList = new string[listBox3.SelectedItems.Count];
                        //for (int l = 0; l <= listBox3.SelectedItems.Count - 1; l++)
                        //{
                        //     string AnnexName = listBox3.SelectedItems[l].ToString();
                        //     OutputAnnexList[l] = AnnexName;
                        //}
                        tr1.ExecuteOutput_TR(SelectTemplateDoc, "�����ܽ�", DataTreeList, DocPath, null, ProjectID, TestVerID);                  
                       
                        break;

                    case "������ⱨ��":
                        TPM3.chq.PR pr2 = new TPM3.chq.PR();
                        pr2.ExecuteOutput_PR(SelectTemplateDoc, "���ⱨ��", DataTreeList, DocPath, ProjectID, TestVerID);
                        break;

                   
                    case "����ع���Է���":
                        TPM3.chq.HGFA HGFA = new TPM3.chq.HGFA();
                        //HGFA.ExecuteOutput_HGFA(SelectTemplateDoc, "�����ܽ�", DataTreeList, DocPath, ProjectID, TestVerID);
                        HGFA.ExecuteOutput_HGFA(SelectTemplateDoc, "�ع���Է���", DataTreeList, DocPath, ProjectID, TestVerID, IfOutputAnnex);
                        break;

                    //case "����ع�����ĵ�":
                    //    TPM3.chq.HGWD HGWD = new TPM3.chq.HGWD();
                    //    HGWD.ExecuteOutput_HGWD(SelectTemplateDoc, "�ع�����ĵ�", DataTreeList, DocPath, ProjectID, TestVerID);
                    //    break;

                    case "����ܲ��Ա���":
                        TPM3.chq.HG hg1 = new TPM3.chq.HG();
                       // hg1.ExecuteOutput_HG(SelectTemplateDoc, "�����ܽ�", DataTreeList, DocPath, ProjectID, TestVerID);
                      //  hg1.ExecuteOutput_HG(SelectTemplateDoc, "�ع���Ա���", DataTreeList, DocPath, ProjectID, TestVerID);
                        
                       break;

                    case "����ع����˵��":
                        //TPM3.chq.HGFL_TestCase ts2 = new TPM3.chq.HGFL_TestCase();
                        //ts2.ExecuteOutput_HGFL_TestCase(SelectTemplateDoc, "�ع����˵��", DataTreeList, DocPath, ProjectID, TestVerID);
                        TPM3.chq.TS ts2 = new TPM3.chq.TS();
                        ts2.ExecuteOutput_TS(SelectTemplateDoc, "�ع����˵��", DataTreeList, DocPath, ProjectID, TestVerID, IfOutputAnnex);
                        break;

                    case "����ع�������ⱨ��":
                        TPM3.chq.HGFL_Question pr1 = new TPM3.chq.HGFL_Question();
                        pr1.ExecuteOutput_HGFL_Question(SelectTemplateDoc, "�ع����ⱨ��", DataTreeList, DocPath, ProjectID, TestVerID);
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


            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("SELECT SYS���԰汾��.ID, SYS���԰汾��.���, SYS���԰汾��.��ĿID FROM SYS���԰汾�� WHERE (SYS���԰汾��.ǰ��汾ID) Is Null AND SYS���԰汾��.��ĿID=?", ProjectID);
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

            if (title1 == "�������")
            {
                listBox1.Items.Clear();
                outputitem = "�������������˵��";
                listBox1.Items.Add(outputitem);
               // SetLocation(false);
             
            }
            else if (title1 == "���Լƻ�")
            {
                if (IfFirstTest() == true)
                {
                    listBox1.Items.Clear();
                    outputitem = "������Լƻ�";
                    listBox1.Items.Add(outputitem);
                    //SetLocation(false);
                }
                else
                {
                    listBox1.Items.Clear();
                    outputitem = "������Լƻ�";
                    listBox1.Items.Add(outputitem);
                    //SetLocation(false);

                    outputitem = "����ع���Է���";
                    listBox1.Items.Add(outputitem);

                }

            }
            else if (title1 == "�������")
            {
                listBox1.Items.Clear();
                outputitem = "�������˵��";
                listBox1.Items.Add(outputitem);
                outputitem = "������Լ�¼��";
                listBox1.Items.Add(outputitem);
                outputitem = "���������뵽�����ļ���";
                listBox1.Items.Add(outputitem);
               // SetLocation(false);


            }
            else if (title1 == "����ʵʩ")
            {
                if (IfFirstTest() == true)
                {
                    listBox1.Items.Clear();
                    outputitem = "������Լ�¼ʵ����";
                    listBox1.Items.Add(outputitem);
                    outputitem = "������ⱨ��";
                    listBox1.Items.Add(outputitem);
                    outputitem = "���������뵽�����ļ���";
                    listBox1.Items.Add(outputitem);
                    // SetLocation(false);
                }
                else
                {
                    listBox1.Items.Clear();
                    outputitem = "������Լ�¼ʵ����";
                    listBox1.Items.Add(outputitem);
                    outputitem = "����ع�������ⱨ��";
                    listBox1.Items.Add(outputitem);
                    outputitem = "���������뵽�����ļ���";
                    listBox1.Items.Add(outputitem);
                    // SetLocation(false);

                }

            }
            else if (title1 == "�����ܽ�")
            {
                //listBox3.Enabled = true;

                if (IfFirstTest() == true)
                {
                    listBox1.Items.Clear();
                    outputitem = "������Ա���";
                    listBox1.Items.Add(outputitem);
                    outputitem = "������ⱨ��";
                    listBox1.Items.Add(outputitem);


                }
                else
                {
                    listBox1.Items.Clear();

                    //outputitem = "����ع���Է���";
                    //listBox1.Items.Add(outputitem);

                    //outputitem = "����ع�����ĵ�";
                    //listBox1.Items.Add(outputitem);

                    outputitem = "����ܲ��Ա���";
                    listBox1.Items.Add(outputitem);

                    outputitem = "����ع�������ⱨ��";
                    listBox1.Items.Add(outputitem);

                    //outputitem = "��¼������ع����˵��";
                    //listBox1.Items.Add(outputitem);

                    //outputitem = "��¼������ع���Լ�¼��";
                    //listBox1.Items.Add(outputitem);

                    //outputitem = "��¼������ع���Լ�¼ʵ����";
                    //listBox1.Items.Add(outputitem);

                  


                }

               
                //SetLocation(true);

                          
            }
            else if (title1=="")//�ع���Ա���
            {
               // SetLocation(false);

                listBox1.Items.Clear();

                outputitem = "����ع���Է���";
                listBox1.Items.Add(outputitem);

                //outputitem = "����ع�����ĵ�";
                //listBox1.Items.Add(outputitem);

                outputitem = "����ع����˵��";
                listBox1.Items.Add(outputitem);

                outputitem = "����ع���Լ�¼��";
                listBox1.Items.Add(outputitem);

                outputitem = "����ع���Լ�¼ʵ����";
                listBox1.Items.Add(outputitem);

                outputitem = "����ع�������ⱨ��";
                listBox1.Items.Add(outputitem);

                outputitem = "����ܲ��Ա���";
                listBox1.Items.Add(outputitem);
            }

            listBox1.SelectedIndex = 0;
            //if (listBox1.SelectedItem.ToString() == "������Ա���")
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
                case "�������������˵��":
                    Filter = "����������˵��ģ��*.doc";
                    break;

                case "������Լƻ�":
                    Filter = "���Լƻ�ģ��*.doc";
                    break;

                case "�������˵��":
                    Filter = "����˵��ģ��*.doc";
                    break;

                case "������Ա���":
                    Filter = "���Ա���ģ��*.doc";
                    break;

                case "����ع���Է���":
                    Filter = "�ع���Է���ģ��*.doc";
                    break;

                //case "����ع�����ĵ�":
                //    Filter = "�ع�����ĵ�ģ��*.doc";
                //    break;

                case "����ܲ��Ա���":
                    Filter = "�ع���Ա���ģ��*.doc";
                    break;

                case "����ع����˵��":
                    Filter = "�ع����˵��ģ��*.doc";
                    break;

                case "����ع���Լ�¼��":
                    Filter = "�ع���Լ�¼��ģ��*.doc";
                    break;

                case "����ع���Լ�¼ʵ����":
                    Filter = "�ع���Լ�¼���ģ��*.doc";
                    break;

                case "����ع�������ⱨ��":
                    Filter = "�ع����ⱨ��ģ��*.doc";
                    break;
                    
                case "������Լ�¼��":
                    Filter = "���Լ�¼��ģ��*.doc";
                    break;

                case "������Լ�¼ʵ����":
                    Filter = "���Լ�¼���ģ��*.doc";
                    break;

                case "������ⱨ��":
                    Filter = "���ⱨ��ģ��*.doc";
                    break;

                case "���������뵽�����ļ���":
                    Filter = "�ĵ�����ģ��*.doc";
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
                case "�������������˵��":                   
                    CurrentDocName = "�������";
                    button2.Visible = true;
                    break;

                case "������Լƻ�":
                    CurrentDocName = "���Լƻ�";
                    button2.Visible = true;
                    break;

                case "�������˵��":
                    CurrentDocName = "����˵��";
                    button2.Visible = true;
                    break;

                case "������Ա���":
                    CurrentDocName = "�����ܽ�";
                    button2.Visible = true;
                    break;

                case "����ع���Է���":
                    CurrentDocName = "�ع���Է���";
                    button2.Visible = true;
                    break;

                //case "����ع�����ĵ�":
                //    CurrentDocName = "�ع�����ĵ�";
                //    button2.Visible = true;
                //    break;

                case "����ܲ��Ա���":
                    //CurrentDocName = "�����ܽ�";
                    CurrentDocName = "�ع���Ա���";
                    button2.Visible = true;
                    break;

                case "����ع����˵��":
                    CurrentDocName = "�ع����˵��";
                    button2.Visible = true;
                    break;

                case "����ع���Լ�¼��":
                    CurrentDocName = "�ع���Լ�¼";
                    button2.Visible = true;
                    break;

                case "����ع���Լ�¼ʵ����":
                    CurrentDocName = "�ع���Լ�¼";
                    button2.Visible = true;
                    break;

                case "����ع�������ⱨ��":
                    CurrentDocName = "�ع����ⱨ��";
                    button2.Visible = true;
                    break;

                case "������Լ�¼��":
                    CurrentDocName = "���Լ�¼";
                    button2.Visible = true;
                    break;

                case "������Լ�¼ʵ����":
                    CurrentDocName = "���Լ�¼";
                    button2.Visible = true;
                    break;

                case "������ⱨ��":
                    CurrentDocName = "���ⱨ��";
                    button2.Visible = true;
                    break;

              
            }

            GetTemplate(FileName);

            //if (FileName == "������Ա���")
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

            //if (FileName == "������Ա���")
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
            //    textBox1.Text = "�ޡ�";
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
            //    listBox3.Items.Add("�ع��������");
            //    listBox3.Items.Add("�ع���Լ�¼");
            //    listBox3.Items.Add("�ع�������ⱨ��");

            //}
            //else
        //    {
        //        listBox3.Items.Add("��������");
        //        listBox3.Items.Add("����������������׷�ٹ�ϵ");
        //        listBox3.Items.Add("�����������������׷�ٹ�ϵ");

        //    }
          
        //}

        private void button2_Click_1(object sender, EventArgs e)
        {
            �ĵ��޸�ҳ modpage = new �ĵ��޸�ҳ();
            modpage.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cover DocCover = new Cover();
            DocCover.ShowDialog();
        }

      
    

    }
}