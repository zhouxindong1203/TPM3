using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Aspose.Words;
using Common;
using Common.Database;
using MyDog.Dog;
using TestLibrary;
using TPM3.lt;
using TPM3.ProjectManager;
using TPM3.wx;
using TPM3.zxd;
using TPM3.zxd.clu;
using TPM3.zxd.pbl;
using TPM3.zxd.util;
using Z1.tpm;
using Z1.tpm.DB;
using Z1Utils;
using Z1Utils.Controls;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.Sys
{
    public partial class MainForm : LeftTreeUserControl, ICommandObject<IMoveCommand>, ICommandObject<ICopyCommand>, ICommandObject<IPasteCommand>
    {
        public static MainForm mainFrm;

        public MainForm()
        {
            mainFrm = this;
            InitializeComponent();
            treeView1.BeforeSelect += treeView1_BeforeSelect;
            this.Text = GlobalData.ToolName;

            SetCommandObject((IMoveCommand)null);
            SetCommandObject((ICopyCommand)null);
            SetCommandObject((IPasteCommand)null);
            GridAssist.globalMoveCommandObject = this;
            GridAssist.globalCopyObject = this;
            GridAssist.globalPasteObject = this;

            Clipboard.Clear();

#if Package
            statusStrip1.Visible = false;
            miDebug.Visible = false;
#endif
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            timer2.Enabled = true;  // �����鶨ʱ��

            AutoSelectVersion();
            if(!InitFormByVersion())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            _tsitems.Clear();
            _tsitems.Add(tsSwitchToDesign);
            _tsitems.Add(tsSwitchToResult);
            //_tsitems.Add(tsbQuickSwitchVersion);
            _tsitems.Add(tsbStartExecute);
            _tsitems.Add(tsbImportUC);

            VisibleFindUsecase(false);
            VisibleReorder(false);
            VisibleOCI(false);
            VisibleUsecase(false);
        }

        /// <summary>
        /// ���ݵ�ǰѡ�еİ汾��ʼ������
        /// </summary>
        public bool InitFormByVersion()
        {
            ShowControls(true);

            object previd = DBLayer1.GetPreVersion(dbProject, currentvid);
            tsbProjectTypeSwitch.Enabled = previd == null;

            // ���ñ����ı�
            string projectname = Common.ProjectInfo.GetProjectString(dbProject, pid, "��Ŀ����");
            string vername = Common.ProjectInfo.GetDocString(dbProject, pid, currentvid, null, "�汾����");
            ProjectStageType pst = DBLayer1.GetProjectType(dbProject, pid);
            this.Text = string.Format("{0} - {1}({4}) - {2} - {3}", us.LastDatabaseName, projectname, vername, GlobalData.ToolName, FunctionClass.GetEnumDescription(pst));
            if(CreateNaviBarAndMenu() == false) return false;
            return true;
        }

        /// <summary>
        /// �û���¼���Զ�ѡ��汾
        /// </summary>
        static void AutoSelectVersion()
        {
            DataTable dt = DBLayer1.GetProjectVersionList(dbProject, pid, false);

            if(dt.Rows.Count == 0)
            {   // ����Ŀ��û�а汾���Զ�����һ����
            }
            else
            {
                if(GridAssist.GetDataRow(dt, "ID", currentvid) == null)
                    globalData.currentvid = dt.Rows[0]["ID"];   // �Զ�ѡȡ��һ���汾
            }
        }

        bool IsValid = true;

        /// <summary>
        /// ��ʱ�����ܹ���״̬
        /// </summary>
        void timer2_Tick(object sender, EventArgs e)
        {
            if(!IsValid) return;
            if (CheckDog.Inst.dogType == DogType.noDog)
            {   // ����Ч
                IsValid = false;
                MessageBox.Show(CheckDog.Inst.errorMessage);
                this.Close();
            }
        }

        void miClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //public override Control GetSubForm(TreeNode tn2)
        //{
        //    DocTreeNode tn = tn2 as DocTreeNode;
        //    Form f = FormClass.CreateClass(tn.nodeClass) as Form;
        //    return f;
        //}

        static string[] attrList = { "type", "content", "title", "title1", "step", "subform", "Image", "DocName" };
        protected override void OnTreeNodeSelected(TreeNode tn2)
        {
            DocTreeNode tn = tn2 as DocTreeNode;

            string url = tn.nodeClass;
            string param = "";
            foreach(string attr in attrList)
            {
                string attrvalue = tn.nodeElement.GetAttribute(attr);
                if(string.IsNullOrEmpty(attrvalue)) continue;
                param += param.Length == 0 ? "?" : "&";
                param += attr + "=" + attrvalue;
            }
            DelayCreateForm(url + param, false); // �Ѿ��������
        }

        /// <summary>
        /// ��f��ʾ���ұ�������
        /// </summary>
        public override void OnShowForm(TreeNode tn, Form f)
        {
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            panel1.Controls.Clear();
            panel1.Controls.Add(f);
            f.Dock = DockStyle.Fill;
            f.Visible = true;
        }

        string delayurl;

        /// <summary>
        /// �Ƿ񱣴��Ѿ��򿪵Ĵ��ڣ�ȱʡֵfalse
        /// </summary>
        bool saveLast;

        /// <summary>
        /// �ӳٴ�������
        /// </summary>
        public void DelayCreateForm(string url, bool _saveLast = true)
        {
            delayurl = url;
            saveLast = _saveLast;
        }

        void miTest_Click(object sender, EventArgs e)
        {
            DelayCreateForm("wx.PersonForm?id=1&tid=555");
        }

        /// <summary>
        /// ��ʱ��� delayurl ����������
        /// </summary>
        void timer3_Tick(object sender, EventArgs e)
        {
            if(delayurl == null) return;

            if(saveLast)
            {   // ����رղ��ɹ�����ȡ������
                if(CloseCurrentForm(true, true) == false)
                    return;
            }

            Form f = FormClass.GetFormFromUrl(delayurl) as Form;
            string tempurl = delayurl;
            delayurl = null;
            if(f == null) return;

            // �Զ�������ƥ��Ľڵ�
            TreeNode tn2 = GetTreeNodeByUrl(treeView1.Nodes, tempurl);
            if(tn2 != null)
                SetSelectNode(tn2);
            ShowControl(f, tn2);
        }

        /// <summary>
        /// ���Һ�ָ��URL��ƥ������ڵ�
        /// </summary>
        static TreeNode GetTreeNodeByUrl(TreeNodeCollection tnc, string url)
        {
            string className = FormClass.GetClassNameFromUrl(url);
            Dictionary<string, string> dic = FormClass.GetParamsFromUrl(url);

            // ���URL�����Ƿ�ƥ�� 
            Func2<bool, Dictionary<string, string>, XmlElement> attrFixed = (dic2, ele) =>
            {
                foreach(string attr in attrList)
                    if(dic.ContainsKey(attr) && ele.GetAttribute(attr) != dic[attr])
                        return false;
                return true;
            };

            foreach(DocTreeNode tn in tnc)
            {
                TreeNode tnx = GetTreeNodeByUrl(tn.Nodes, url);
                if(tnx != null) return tnx;
                if(tn.nodeClass != className) continue;
                if(!attrFixed(dic, tn.nodeElement)) continue;
                return tn;
            }
            return null;
        }

        void ShowControls(bool b)
        {
            treeView1.Visible = splitter1.Visible = b;
            panel1.Visible = panel2.Visible = b;
            tsbNavigator.Checked = tsbNavigator.Enabled = tsbHelpWindow.Enabled = b;
            tsSwitchToDesign.Enabled = tsSwitchToResult.Enabled = b;
            miCustomTree.Enabled = miClose.Enabled = b;
            //this.tsFindUsecase.Enabled = b;
            //this.tsbReorder.Enabled = b;
            //this.tsDigiOne.Enabled = this.tsDigiTwo.Enabled = this.tsDigiThree.Enabled = this.tsDigiFour.Enabled = b;
            miTraceList.Visible = miOption.Visible = miSplit2.Visible = b;
        }

        /// <summary>
        /// ������ǶXML�����ݴ����������Ͳ˵�
        /// </summary>
        bool CreateNaviBarAndMenu()
        {
            try
            {
                //�������ṹ����XML�ļ�
                XmlElement root = GetConfigFileElement();

                currentSelectedForm = null;  //!!! ��Ҫ����ֹ���±�����һ�ε�ҳ��
                treeView1.Nodes.Clear();

                InsertDocTreeNode(root, treeView1.Nodes, null);
                treeView1.ExpandAll();

                if(treeView1.Nodes.Count > 0)
                {
                    TreeNode tn = treeView1.Nodes[0];
                    while(tn.Nodes.Count > 0) tn = tn.Nodes[0];
                    treeView1.SelectedNode = tn;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public XmlElement GetConfigFileElement()
        {
            try
            {
                object previd = DBLayer1.GetPreVersion(dbProject, currentvid);
                string configFilename = "tree";
                ProjectStageType pst = DBLayer1.GetProjectType(dbProject, pid);
                if(previd != null)     // �ع�ģʽ�����ж��Ƿ�׼���׶�
                {
                    //VersionMode vm = DBLayer1.GetVersionMode(dbProject, pid, currentvid);
                    //configFilename = vm == VersionMode.Execute ? "tree_m" : "tree_p";    // tree_r
                    configFilename = "tree_m";
                    if(pst == ProjectStageType.����) configFilename = "tree-����-�ع�";
                }
                else
                {
                    // ��һ�β��Ը��ݲ�ͬ�����ͷ��ز�ͬ�Ľڵ���
                    if(pst == ProjectStageType.I��) configFilename = "tree-��ȫ";
                    if(pst == ProjectStageType.II��) configFilename = "tree-��";
                    if(pst == ProjectStageType.III��) configFilename = "tree-��С";
                    if(pst == ProjectStageType.����) configFilename = "tree-����";
                }
                configFilename += ".xml";

                string s = FunctionClass.GetEmbedText("TPM3.Config.��Ŀ��." + configFilename);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(s);
                XmlElement root = xmldoc.DocumentElement;
                PreDealDocument(root);
                return root;
            }
            catch
            {
                return null;
            }
        }

        static int PreDealDocument(XmlElement parent)
        {
            XmlNodeList xnl = parent.SelectNodes("TreeNode");
            int level = 0;
            foreach(XmlElement ele in xnl)
            {
                int ret = PreDealDocument(ele);
                if(ret + 1 > level) level = ret + 1;
            }
            // level: 0 Ҷ�ڵ㣬��������Ҷ�ڵ�Ĳ���
            parent.SetAttribute("Level", level.ToString());
            return level;
        }

        //�������ڵ�
        protected void InsertDocTreeNode(XmlElement parent, TreeNodeCollection Nodes, string parentDocName)
        {
            foreach(XmlElement ele in parent.SelectNodes("TreeNode"))
            {
                DocTreeNode tn = new DocTreeNode(ele, panel1);

                tn.helpInfo = GlobalData.GetParam(ele);
                tn.ImageKey = tn.SelectedImageKey = ele.GetAttribute("Icon");
                //SetTreeNodeImage(tn, s1, s2);

                // �ĵ���������д�����(DocNam)����ʾ�ýڵ����ĵ����˽ڵ��������ĵ�����
                // ���û�д����ԣ���ʾ�ýڵ����ĵ��������ݣ��ĵ����Ӹ��ڵ�̳�
                string docName2 = parentDocName;
                if(ele.HasAttribute("DocName"))
                {
                    docName2 = ele.GetAttribute("DocName");
                    if(docName2.Length == 0) // Ϊ�ձ�ʾ�ĵ�����ڵ�����ͬ
                        docName2 = DocTreeNode.GetNodeName(ele);
                }
                tn.documentName = docName2;

                Nodes.Add(tn);
                if(!string.IsNullOrEmpty(tn.nodeClass))
                    tn.ForeColor = Color.DarkBlue;

                // �ݹ����
                InsertDocTreeNode(ele, tn.Nodes, docName2);
                if(DocTreeNode.GetNodeLevel(ele) > 0 && tn.Nodes.Count == 0)
                    tn.Remove();
            }
        }

        //void SetTreeNodeImage(TreeNode tn, string s1, string s2)
        //{
        //    tn.ImageIndex = tn.SelectedImageIndex = 0;
        //    if( string.IsNullOrEmpty(s1) ) return;
        //    tn.ImageKey = s1;
        //    tn.SelectedImageKey = s1;

        //    if( string.IsNullOrEmpty(s2) == false )
        //        tn.SelectedImageKey = s2;
        //}

        IMoveCommand moveCommandObject = null;
        public void SetCommandObject(IMoveCommand commandObject)
        {
            moveCommandObject = commandObject;
            tsbMoveDown.Enabled = tsbMoveUp.Enabled = commandObject != null;
        }

        void tsbMoveUp_Click(object sender, EventArgs e)
        {
            moveCommandObject.OnCommandMoveUp();
        }

        void tsbMoveDown_Click(object sender, EventArgs e)
        {
            moveCommandObject.OnCommandMoveDown();
        }

        ICopyCommand copyCommandObject = null;
        IPasteCommand pasteCommandObject = null;
        public void SetCommandObject(ICopyCommand commandObject)
        {
            copyCommandObject = commandObject;
            tsbCopy.Enabled = commandObject != null;
        }

        public void SetCommandObject(IPasteCommand commandObject)
        {
            pasteCommandObject = commandObject;
            tsbPaste.Enabled = commandObject != null;
        }

        void tsbCopy_Click(object sender, EventArgs e)
        {
            copyCommandObject.OnCommandCopy();
        }

        void tsbPaste_Click(object sender, EventArgs e)
        {
            pasteCommandObject.OnCommandPaste();
        }

        void tsbNavigator_Click(object sender, EventArgs e)
        {
            //treeView1.Visible = splitter1.Visible = tsbNavigator.Checked;
            splitter1.Expanded = tsbNavigator.Checked;

            // ��������������˵�������treeview������ʧЧ������
            //if( !treeView1.Visible )
            //{
            //    TestTreeForm ttf = currentSelectedForm as TestTreeForm;
            //    if( ttf != null )
            //    {
            //        ttf.MyTreeView.Width += 1;
            //    }
            //}
        }

        void miCustomTree_Click(object sender, EventArgs e)
        {
            //if( dbProject == null ) return;
            //ConfigTreeForm f = new ConfigTreeForm();
            //if( f.ShowDialog() != DialogResult.OK ) return;
            //// ���¼������ڵ�
            //CreateNaviBarAndMenu();
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            if(dbProject != null)
                tsbCount.Text = dbProject.GetCounterString();
        }

        void miNew_Click(object sender, EventArgs e)
        {
            //NewProjectInfoForm f = new NewProjectInfoForm();
            //if( f.ShowDialog() != DialogResult.OK ) return;
            //string s = f.ProjectFileName;
            //OpenDatabase(s);
        }

        void miQuit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        void miOption_Click(object sender, EventArgs e)
        {
            if(dbProject == null) return;
            //if( !this.Focus() ) return;
            //IBaseTreeForm bf = currentSelectedForm as IBaseTreeForm;
            //if( bf != null )
            //{
            //    if( !bf.OnPageClose(false) )
            //        return;
            //}

            OptionForm f = new OptionForm();
            DialogResult ret = f.ShowDialog();
            //if( ret != DialogResult.OK ) return;
            //bf.OnPageCreate();
        }

        void miTrace_Click(object sender, EventArgs e)
        {
            if(dbProject == null) return;
            IBaseTreeForm bf = currentSelectedForm as IBaseTreeForm;
            if(bf != null) bf.OnPageClose(false);
            ToolStripItem mi = sender as ToolStripItem;
            TraceForm tf = new TraceForm(mi.Tag);
            tf.Text = mi.Text;
            tf.ShowDialog();
        }

        void miGenCaseDoc_Click(object sender, EventArgs e)
        {
            //TestDoc td = new TestDoc();
            //td.GenDoc();
            var dv = DBLayer2.GetPrevItemList(dbProject, pid, currentvid);
        }

        /// <summary>
        /// ���ָ�����ݿ������
        /// </summary>
        void miBlankMDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "������ݿ�";
            dlg.CheckFileExists = true;
            dlg.Filter = "Access�ļ�(.mdb)|*.mdb";
            if(dlg.ShowDialog() != DialogResult.OK) return;
            string[] sqlList = new[]{"CA�������ʵ���","CA�������ʵ���","CA���Թ���ʵ���","CA���Թ���ʵ���","CA��������ʵ���",  "CA��������ʵ���",
                "CA������ʵ���","CA������ʵ���","CA��������ʵ���","CA��������ʵ���","CA����������������ϵ��", "CA���ⱨ�浥",
                "DC���Թ��̸�����","DC������Դ���ñ�","DC������֯����Ա��","DC����ʵ���","DC�ƻ����ȱ�", "DC�ܼ���",
                "DC�����","DC�ĵ��޸�ҳ","DC�����ʶ","DC���⼶���","DC�����ļ���", "HG�ع����δ����ԭ��",
                "SYS���԰汾��","SYS�������ݱ�"};

            using(DBAccess dba = DBAccessFactory.FromAccessFile(dlg.FileName).CreateInst())
            {
                foreach(string t in sqlList)
                    dba.ExecuteNoQuery("delete from " + t + " where ��ĿID = ? ", pid);
                dba.ExecuteNoQuery("update SYS�ĵ����ݱ� set �ĵ����� = null where �������� = ?", "����");
                dba.ExecuteNoQuery("update SYS�ĵ����ݱ� set �ı����� = null where ���ݱ��� <> ? and ���ݱ��� <> ? and ���ݱ��� <> ? and ���ݱ��� <> ? and ���ݱ��� <> ?  and ���ݱ��� <> ?", "���ݿ�汾", "�ĵ��汾", "�ĵ���ʶ��", "�ĵ�����", "�����ʶ�ָ���", "Ӧ����������");
            }
        }

        /// <summary>
        /// ��ʾ��ѯ��־����
        /// </summary>
        void miDBLog_Click(object sender, EventArgs e)
        {
            DBAccessLogForm f = new DBAccessLogForm();
            f.ShowDialog();
        }

        void tsbHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string s = Path.Combine(GlobalData.globalData.currentDirectory, "TPM.chm");
                Process.Start(s);
            }
            catch(Exception)
            {
                MessageBox.Show("���û��ֲ��ļ�ʧ��!", "����ʧ��", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        void miAboutDog_Click(object sender, EventArgs e)
        {
            string s = GlobalData.ToolName + "\n\n" + "��ǰʹ��:  \n\n     ";
            s += CheckDog.Inst.GetDogTypeName() + "\n\n";
            MessageBox.Show(s, "����ʹ������֤");
        }

        /// <summary>
        /// ��ʾ����Э�������״̬
        /// </summary>
        void miShowLicenseServerState_Click(object sender, EventArgs e)
        {
            //LicenseServerStateForm f = new LicenseServerStateForm();
           // f.ShowDialog();
        }

        void miAbout_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

        /// <summary>
        /// �л����԰汾
        /// </summary>
        void miSelectTestVersion_Click(object sender, EventArgs e)
        {
            IBaseTreeForm bf = currentSelectedForm as IBaseTreeForm;
            if(bf != null) bf.OnPageClose(false);

            ProjectVersionForm f = new ProjectVersionForm();
            if(f.ShowDialog() != DialogResult.OK) return;
            // ���¼���
            InitFormByVersion();
        }

        /// <summary>
        /// �������԰汾(���лع����)
        /// </summary>
        void miCreateVersion_Click(object sender, EventArgs e)
        {
            NewVersionForm f = new NewVersionForm();
            f.ShowDialog();
        }

        /// <summary>
        /// �����汾���ı�����������
        /// </summary>
        public bool VersionChanged = true;
        void tsbQuickSwitchVersion_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripDropDownButton tsb = sender as ToolStripDropDownButton;
            if(VersionChanged)
            {
                tsb.DropDownItems.Clear();
                DataTable dt = DBLayer1.GetProjectVersionList(dbProject, pid, true);
                foreach(DataRow dr in dt.Rows)
                {
                    object vid = dr["ID"];
                    string s = dr["�汾����"].ToString();
                    if(IsNull(dr["ǰ��汾ID"])) s += "(��ʼ�汾)";
                    VersionMode vm = DBLayer1.GetVersionMode(dbProject, pid, vid);
                    if(vm == VersionMode.Prepare) s += "(׼���׶�)";

                    ToolStripItem tsi = tsb.DropDownItems.Add(s);
                    tsi.ToolTipText = Common.ProjectInfo.GetDocString(dbProject, pid, vid, null, "�汾˵��");
                    tsi.Tag = vid;
                    tsi.Click += tsiSwitchVerion_Click;
                }
                VersionChanged = false;
            }
            foreach(ToolStripMenuItem tsi in tsb.DropDownItems)
                tsi.Checked = Equals(tsi.Tag, currentvid);
        }

        /// <summary>
        /// �����л���ָ���汾
        /// </summary>
        void tsiSwitchVerion_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsi = sender as ToolStripMenuItem;
            object vid = tsi.Tag;
            if(Equals(vid, currentvid)) return;   // �汾û�б�

            IBaseTreeForm bf = currentSelectedForm as IBaseTreeForm;
            if(bf != null) bf.OnPageClose(false);

            globalData.currentvid = vid;
            InitFormByVersion();
        }

        void tsbProjectTypeSwitch_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripDropDownButton tsb = sender as ToolStripDropDownButton;
            if(tsb.DropDownItems.Count == 0)
            {
                ToolStripItem tsi1 = tsb.DropDownItems.Add("I��");
                tsi1.Tag = ProjectStageType.I��;

                ToolStripItem tsi2 = tsb.DropDownItems.Add("II��");
                tsi2.Tag = ProjectStageType.II��;

                ToolStripItem tsi3 = tsb.DropDownItems.Add("III��");
                tsi3.Tag = ProjectStageType.III��;

                ToolStripItem tsi4 = tsb.DropDownItems.Add("����");
                tsi4.Tag = ProjectStageType.����;

                foreach(ToolStripItem ddi in tsb.DropDownItems)
                {
                    ddi.Click += tsiSwitchProjectType_Click;
                    ddi.ToolTipText = FunctionClass.GetEnumDescription((ProjectStageType)ddi.Tag);
                }
            }
            foreach(ToolStripMenuItem tsi in tsb.DropDownItems)
                tsi.Checked = (ProjectStageType)tsi.Tag == DBLayer1.GetProjectType(dbProject, pid);
        }

        void tsiSwitchProjectType_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsi = sender as ToolStripMenuItem;
            ProjectStageType pst1 = (ProjectStageType)tsi.Tag;
            if(pst1 == DBLayer1.GetProjectType(dbProject, pid)) return;     // ģʽû�б�

            IBaseTreeForm bf = currentSelectedForm as IBaseTreeForm;
            if(bf != null) bf.OnPageClose(false);

            DBLayer1.SetProjectType(dbProject, pid, pst1);
            InitFormByVersion();
        }

        #region zxd

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TestTreeForm ttf = currentSelectedForm as TestTreeForm;
            if(ttf != null)
            {
                TestUsecaseForm tuf = ttf.rightForm as TestUsecaseForm;
                if(tuf != null)
                {
                    if(!tuf.DataOK())
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            if(!SaveBeforeSwitchForm())
                e.Cancel = true;
        }

        public bool SaveBeforeSwitchForm()
        {
            IBaseTreeForm bf = currentSelectedForm as IBaseTreeForm;
            if(bf != null)
                return bf.OnPageClose(false);
            return true;
        }

        private void splitter1_ExpandedChanged(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            tsbNavigator.Checked = e.NewExpandedValue;
        }

        private void tsSwitchToDesign_Click(object sender, EventArgs e)
        {
            if(!IsPrepare() && IsFirstVersion())
                DelayCreateForm("zxd.TestTreeForm?type=design");
        }

        private void tsSwitchToResult_Click(object sender, EventArgs e)
        {
            if(!IsPrepare())
                DelayCreateForm("zxd.TestTreeForm?type=result");
        }

        public void VisibleFindUsecase(bool v)
        {
            this.tsFindUsecase.Enabled = v;
            //this.toolStripSeparator4.Visible = v;
        }

        public void VisibleReorder(bool v)
        {
            this.tsbReorder.Enabled = v;
        }

        public void VisibleOCI(bool v)
        {
            this.tsDigiOne.Enabled = v;
            this.tsDigiTwo.Enabled = v;
            this.tsDigiThree.Enabled = v;
        }

        public void VisibleUsecase(bool v)
        {
            this.tsDigiFour.Enabled = v;
        }

        private FindUsecaseResultForm _dlgfindresult = null;
        public FindUsecaseResultForm DlgRefFindResult
        {
            set
            {
                _dlgfindresult = value;
            }
            get
            {
                return _dlgfindresult;
            }
        }

        private string _findwhat = string.Empty;
        private List<TreeNode> _findnodes = new List<TreeNode>();

        private void tsFindUsecase_Click(object sender, EventArgs e)
        {
            if(_dlgfindresult == null || _dlgfindresult.IsDisposed)
            {
                using(FindUsecaseForm dlg = new FindUsecaseForm())
                {
                    dlg.Text = "���Ҳ�������";

                    if(DialogResult.OK != dlg.ShowDialog())
                        return;

                    _findnodes.Clear();

                    Cursor oldcursor = this.Cursor;
                    this.Cursor = Cursors.WaitCursor;

                    _findwhat = dlg.FindWhat;

                    TreeViewUtils tvu = new TreeViewUtils();
                    EnumTreeViewProc proc = SearchIt;

                    TestTreeForm ttf = currentSelectedForm as TestTreeForm;
                    if(!BusiLogic.AssertNotNull(ttf, "����״̬�Ƿ�, �޷�ִ�в���!!", "����ʧ��"))
                        return;

                    TreeNode oldselnode = ttf.tree.SelectedNode;
                    tvu.FindTreeViewLeaf(ttf.tree, proc);

                    if(_findnodes.Count == 0)
                    {
                        MessageBox.Show("û���ҵ���������Ĳ�������!", "���ҽ��", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        ttf.tree.SelectedNode = oldselnode;
                    }
                    else
                    {
                        string msg = String.Format("���ҽ��: {0}����������", _findnodes.Count);
                        MessageBox.Show(msg, "���ҽ��", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        _dlgfindresult = new FindUsecaseResultForm(_findnodes);
                        ttf.tree.SelectedNode = _findnodes[0];
                        _findnodes[0].EnsureVisible();

                        _dlgfindresult.Text = "���ҽ��[" + _findnodes.Count + "����¼]";
                        Rectangle r = Screen.GetWorkingArea(this);
                        _dlgfindresult.Location = new Point(r.Right - _dlgfindresult.Width - 150, r.Bottom - _dlgfindresult.Height - 150);

                        _dlgfindresult.Show();
                    }

                    this.Cursor = oldcursor;
                    _findwhat = string.Empty;
                }
            }
            else
            {
                _dlgfindresult.BringToFront();
                _dlgfindresult.Activate();
                Console.Beep();
            }
        }

        private bool SearchIt(TreeNode tn)
        {
            NodeTagInfo tag = tn.Tag as NodeTagInfo;
            if(tag == null)
                return true; // continue search

            if(tag.nodeType == NodeType.TestCase)
                if(!BusiLogic.NodeIsShortcut(tn))
                    if(ZString.MatchFind(_findwhat, ZString.TrimChapter(tn.Text), 1/*����ƥ��*/))
                        _findnodes.Add(tn);
            return true;
        }


        private int _nodevisi = 0; // 1�������, 2��������, 3������, 4��������
        private void ChangeNodeVisi()
        {
            TestTreeForm ttf = currentSelectedForm as TestTreeForm;
            if(ttf == null)
                return;

            TreeView tv = ttf.tree;
            tv.BeginUpdate();
            if(_nodevisi == 4)
            {
                tv.ExpandAll();
            }
            else
            {
                Cursor oldcursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                tv.CollapseAll();

                TreeViewUtils tvu = new TreeViewUtils();
                EnumTreeViewProc proc = SearchNodes;
                tvu.FindTreeViewNode(tv, proc);

                this.Cursor = oldcursor;
            }

            _nodevisi = 0;
            tv.Nodes[0].EnsureVisible();
            tv.EndUpdate();
        }

        private bool SearchNodes(TreeNode tn)
        {
            NodeTagInfo tag = tn.Tag as NodeTagInfo;
            if(tag == null)
                return true;

            switch(_nodevisi)
            {
                case 1: // ���������
                    if(tag.nodeType == NodeType.TestObject)
                        tn.EnsureVisible();
                    break;

                case 2: // ����������
                    if((tag.nodeType == NodeType.TestType) ||
                        (tag.nodeType == NodeType.TestObject))
                        tn.EnsureVisible();
                    break;

                case 3: // ��������
                    if((tag.nodeType == NodeType.TestItem) ||
                        (tag.nodeType == NodeType.TestObject) ||
                        (tag.nodeType == NodeType.TestType))
                        tn.EnsureVisible();
                    break;

                case 4: // ����������
                    if((tag.nodeType == NodeType.TestCase) ||
                        (tag.nodeType == NodeType.TestObject) ||
                        (tag.nodeType == NodeType.TestType) ||
                        (tag.nodeType == NodeType.TestItem))
                        tn.EnsureVisible();
                    break;
            }
            return true;
        }

        #endregion zxd

        private void tsDigiOne_Click(object sender, EventArgs e)
        {
            _nodevisi = 1;
            ChangeNodeVisi();
        }

        private void tsDigiTwo_Click(object sender, EventArgs e)
        {
            _nodevisi = 2;
            ChangeNodeVisi();
        }

        private void tsDigiThree_Click(object sender, EventArgs e)
        {
            _nodevisi = 3;
            ChangeNodeVisi();
        }

        private void tsDigiFour_Click(object sender, EventArgs e)
        {
            _nodevisi = 4;
            ChangeNodeVisi();
        }

        private void tsbReorder_Click(object sender, EventArgs e)
        {
            PblRepsForm prf = currentSelectedForm as PblRepsForm;
            if(prf == null)
                return;

            prf.PerformRecorder();
        }

        private void ���Դ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ExecStatus.g_ttf == null)
            {
                ExecStatus.g_ttf = new TestTreeForm();
                ExecStatus.g_ttf.BuildTestTree();
            }

            //TestUsecaseForm tuf = new TestUsecaseForm("78LA9Q1GLDVU0L");    // ��ָ��ID��������
            //tuf.ShowDialog();
            TestUsecaseForm tuf = new TestUsecaseForm("78AM11OSOL8Q0R", "78AGQC30263UES");    // ���ض�λ�õ�����(�ϲ��������)
            tuf.ShowDialog();
        }

        void �������ⱨ�洰��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PblRepsRightForm frm = new PblRepsRightForm("78NOLCWTWQ6UP5", true, "obj");
            frm.ShowDialog();
        }

        void tsmiUCEntityView_Click(object sender, EventArgs e)
        {
            UsecaseSelView view = new UsecaseSelView();
            view.ShowDialog();
        }

        void regressFromPblToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportFromPbl((string)pid, (string)currentvid, "v5");
        }

        void ExportFromPbl(string pid, string prever, string newver)
        {
            // ���ò�ͬ�ڵ����͵�ͼ��
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.Project] = "project";
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.TestObject] = "obj";
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.TestType] = "type";
            MainTestFrmCommon.NodeTypeImageKeys[NodeType.TestItem] = "item";

            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.NonExecute] = "unexec";
            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.NonExecute_k] = "unexec_k";

            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecute] = "partexec";
            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecute_k] = "partexec_k";
            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecutep] = "partexecp";
            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecutep_k] = "partexecp_k";

            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executed] = "case";
            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executed_k] = "case_k";
            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executedp] = "execp";
            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executedp_k] = "execp_k";

            Regress.ExportFromPbl(dbProject, pid, prever, newver);
        }



        /// <summary>
        /// �ӻع����׼��״̬�������ִ��״̬
        /// </summary>
        void tsbStartExecute_Click(object sender, EventArgs e)
        {
            object o = DBLayer1.GetPreVersion(dbProject, currentvid);
            if(o == null)
            {
                MessageBox.Show("ĿǰΪ���Գ�ʼ�汾�������ɻع�������ݼ����ܡ��������ڻع�汾��ʹ�á�", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //string msg = "ȷ��Ҫ����ǰ�Ĳ��԰汾�Ӳ���׼��ģʽת��Ϊִ��ģʽ��\n\nת������һ�汾����Ҫ�ع���Ե����������Զ����뵽�ð汾��\nҲ�����ڸð汾���ֹ�������ǰ�汾�е�����";
            //DialogResult dr = MessageBox.Show(msg, "ȷ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            //if(dr != DialogResult.OK) return;

            //VersionMode vm = DBLayer1.GetVersionMode(dbProject, pid, currentvid);
            //if(vm == VersionMode.Execute)
            //{
            //    MessageBox.Show("��ǰ����Ϊִ��ģʽ, �޷����˵�׼��ģʽ!", "����ʧ��", MessageBoxButtons.OK,
            //         MessageBoxIcon.Error);
            //    return;
            //}

            //VersionMode vm2 = vm == VersionMode.Execute ? VersionMode.Prepare : VersionMode.Execute;
            //Common.ProjectInfo.SetDocString(dbProject, pid, currentvid, null, "��ǰģʽ", ((int)vm2).ToString());
            string msg = "ȷ��Ҫ���ɻع�������ݺͲ�����ƻ�������?\n\n��֮ǰ�Ѿ����ɹ�, ԭ�ȵ��������ݽ���ʧ!!";
            DialogResult dr = MessageBox.Show(msg, @"ȷ�ϲ���", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if(dr != DialogResult.OK) return;

            InitFormByVersion();
            VersionChanged = true;

            object previd = DBLayer1.GetPreVersion(dbProject, currentvid);
            ExportFromPbl((string)pid, (string)previd, (string)currentvid);
        }
        private void importUCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportUC f = new ImportUC();
            f.ShowDialog();
        }

        private void tsbImportUC_Click(object sender, EventArgs e)
        {
            object o = DBLayer1.GetPreVersion(dbProject, currentvid);
            if(o == null)
            {
                MessageBox.Show("ĿǰΪ���Գ�ʼ�汾, ���빦����Ч!", "������ʾ", MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                return;
            }

            //VersionMode vm = DBLayer1.GetVersionMode(dbProject, pid, currentvid);
            //if(vm == VersionMode.Prepare)
            //{
            //    MessageBox.Show("���빦��ֻ�ڻع����ִ�н׶���Ч!", "������ʾ", MessageBoxButtons.OK,
            //         MessageBoxIcon.Information);
            //    return;
            //}

            ImportUC f = new ImportUC();
            f.ShowDialog();
            TestTreeForm ttf = currentSelectedForm as TestTreeForm;
            if(ttf != null)
            {
                ttf.OnPageCreate();
            }
        }

        private bool IsPrepare()
        {
            VersionMode vm = DBLayer1.GetVersionMode(dbProject, pid, currentvid);
            return vm == VersionMode.Prepare;
        }

        private bool IsFirstVersion()
        {
            object o = DBLayer1.GetPreVersion(dbProject, currentvid);
            return o == null;
        }

        private List<ToolStripItem> _tsitems = new List<ToolStripItem>();
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(_tsitems.Contains(e.ClickedItem))
                SaveBeforeSwitchForm();
        }

        private void tsbImportUC1_Click(object sender, EventArgs e)
        {
            ImportUC1 f = new ImportUC1();
            f.ShowDialog();
        }
        /// <summary>
        /// ������Ŀ�߻�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miProjectPlan_Click(object sender, EventArgs e)
        {
            SelectProjectDB f = new SelectProjectDB();
            if(f.ShowDialog() != DialogResult.OK) return;
            // ���¼���
            string configFilename = "tree_m.xml";

            string s = FunctionClass.GetEmbedText("TPM3.Config." + configFilename);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(s);
            XmlElement root = xmldoc.DocumentElement;
            PreDealDocument(root);
            InitFormByVersion();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            SaveBeforeSwitchForm();
        }

        private void ������ĿToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ������ĿToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectManageForm projManageForm = new ProjectManageForm();
            projManageForm.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DelayCreateForm("lt.RegressionTestInfluenceForm");
        }

        private void clipboardContainsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool b = Clipboard.ContainsData("Test");

            SerialClass uc = Clipboard.GetData("Test") as SerialClass;
        }

        private void _test_menu_aspose_word_Click(object sender, EventArgs e)
        {
            var a = new StreamReader(@"e:\temp\test1.txt", Encoding.UTF8);
            var str = a.ReadToEnd();
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);
            builder.Write(str);
            doc.Save(@"e:\temp\test1.doc");
        }

        private void problemSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

    public class DocTreeNode : TreeNode
    {
        public string nodeName;
        public string nodeClass;
        public string documentName;

        /// <summary>
        /// ��Ӧ�����ڵ��XML�ڵ�
        /// </summary>
        public XmlElement nodeElement;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string helpInfo = "";

        public DocTreeNode(XmlElement element, Panel panel1)
        {
            // �ڵ���
            nodeName = GetNodeName(element);
            base.Text = nodeName;

            if(nodeName.Length == 0)
                throw new Exception("NodeName���Բ���Ϊ��");

            nodeClass = element.GetAttribute("NodeClass");

            nodeElement = element;
        }

        public static string GetNodeName(XmlElement ele)
        {
            string s = ele.GetAttribute("NodeName");
            if(string.IsNullOrEmpty(s)) s = ele.GetAttribute("DocName");
            return s;
        }

        public static string GetNodeID(XmlElement ele)
        {
            return ele.GetAttribute("ID");
        }

        public static int GetNodeLevel(XmlElement ele)
        {
            string s = ele.GetAttribute("Level");
            return int.Parse(s);
        }
    }

    //public class Regress1
    //{
    //    #region �����ⱨ�浥���ɻع�����(ͬ�ڵ���������ݱ�)

    //    private static List<TreeNode> _nodes;   // ���ⱨ�浥���漰��������;�ڵ�
    //    private static List<string> _uctids;    // ���ⱨ�浥���漰����������ʵ��ID
    //    private static string _pid;             // ��ǰ��ĿID
    //    private static string _prever;          // ǰ����԰汾ID
    //    private static string _newver;          // �½��ع���԰汾ID
    //    private static DBAccess _db;            // ���ݿ�
    //    private static Dictionary<string, string> _oldMapNew;   // SYS�������ݱ��е�ID -> �½���ID(���ڵ�ǰ���԰汾)
    //    private static List<string> _requires;  // ���ⱨ���������Ļع����漰������ǰ��汾�е����ݱ�ID
    //    private static Dictionary<string, string> _itemMap;     // ������ʵ���ԭID -> �ع�汾�е�ʵ��ID
    //    private static List<string> _itemtids;      // �����������漰�����в�����
    //    private static List<TreeNode> _itemnodes;   // �����������漰�����в�����ڵ�
    //    private static KeyList _kl;
    //    private static List<string> _pitemtids;     // ������->����->������ʵ��ID
    //    private static List<TreeNode> _liremove;    // �޼�ʱ��ɾ�������ڵ�
    //    /// <summary>
    //    /// ����Ҫ�ع��ǰһ�汾�����ⱨ�浥�����ع��������
    //    /// </summary>
    //    /// <param name="db"></param>
    //    /// <param name="pid"></param>
    //    /// <param name="prever"></param>
    //    /// <param name="newver"></param>
    //    public static void ExportFromPbl(DBAccess db, string pid, string prever, string newver)
    //    {
    //        TreeView tree = new TreeView();
    //        TreeNode root = MainTestFrmCommon.BuildTestTree(tree, db, pid, prever, 1, true);

    //        // ��ȡ������ع�Ĳ�������ʵ��ID
    //        List<string> pbls = CommonDB.GetRegressPbl(db, pid, prever);
    //        List<string> uceids = CommonDB.GetRelationUCs(db, pid, prever, pbls);

    //        if (_uctids != null)
    //            _uctids.Clear();
    //        else
    //            _uctids = new List<string>();
    //        foreach (string eid in uceids)
    //        {
    //            string tid = TestUsecase.GetUCTid(db, eid, pid, prever);
    //            _uctids.Add(tid);
    //        }

    //        // ������, ��ȡ������ع�������ڵ�
    //        if (_nodes != null)
    //            _nodes.Clear();
    //        else
    //            _nodes = new List<TreeNode>();

    //        if (_requires != null)
    //            _requires.Clear();
    //        else
    //            _requires = new List<string>();

    //        if (_itemMap != null)
    //            _itemMap.Clear();
    //        else
    //            _itemMap = new Dictionary<string, string>();

    //        if (_itemtids != null)
    //            _itemtids.Clear();
    //        else
    //            _itemtids = new List<string>();

    //        if (_itemnodes != null)
    //            _itemnodes.Clear();
    //        else
    //            _itemnodes = new List<TreeNode>();

    //        _kl = new KeyList();

    //        if (_pitemtids != null)
    //            _pitemtids.Clear();
    //        else
    //            _pitemtids = new List<string>();

    //        if (_liremove != null)
    //            _liremove.Clear();
    //        else
    //            _liremove = new List<TreeNode>();

    //        _pid = pid;
    //        _prever = prever;
    //        _newver = newver;
    //        _db = db;
    //        _oldMapNew = CommonDB.GenMap(db, pid, prever);

    //        // ���ÿ�������ڵ��Ƿ���Ҫ�ع�
    //        TreeViewUtils tvu = new TreeViewUtils();
    //        Z1Utils.Controls.EnumTreeViewProc proc = new EnumTreeViewProc(SearchIt);
    //        tvu.FindTreeViewLeaf(tree, proc);

    //        // ��������ع�������ڵ㼰�丸�ڵ�������������"�ع�"���(ͬʱ��ȡ�����漰�Ĳ�����)
    //        foreach (TreeNode node in _nodes)
    //        {
    //            MarkNodeToRegress(node);
    //        }

    //        /////////////////////////////////////////////////////////////////////
    //        //-------------------------------------------------------------------
    //        // �������������漰�Ĳ������ݵ���Ĳ�������
    //        List<string> alterreq = GetAlterRequires(db, pid, newver);
    //        //----------------------------------
    //        // ������׷�ٵ�������, ��ò��������漰�����в�������
    //        GetPblRequires(db, pid, prever, alterreq, _pitemtids);

    //        GetPblExtraReqs(db, pbls, alterreq); // �������⸽������

    //        //----------------------------------//

    //        _itemtids = RequiresTraceItem(db, pid, prever, alterreq); // ��ȡ���и��������漰�Ĳ�����

    //        tvu = new TreeViewUtils();
    //        proc = new EnumTreeViewProc(GetAlterItemNodes);
    //        tvu.FindTreeViewNode(tree, proc);   // ��ȡ������ڵ��б�

    //        foreach (TreeNode node in _itemnodes)
    //            MarkNodeToRegress(node);    // ���в�����ڵ㼰�ϲ�ڵ����"�ع�"���

    //        // ���в�����ڵ���ӽڵ����"�ع�"���
    //        foreach (TreeNode node in _itemnodes)
    //        {
    //            tvu = new TreeViewUtils();
    //            proc = new EnumTreeViewProc(MarkChildNodes);
    //            tvu.FindNodeFromNode(node, proc);
    //        }

    //        _requires.AddRange(alterreq);
    //        //-------------------------------------------------------------------
    //        /////////////////////////////////////////////////////////////////////

    //        // �ü���, ȥ�����зǻع�Ľڵ�
    //        tvu = new TreeViewUtils();
    //        proc = new EnumTreeViewProc(TrimIt);
    //        tvu.FindTreeViewNode(tree, proc);

    //        foreach (TreeNode node in _liremove)
    //        {
    //            node.Remove();
    //        }
    //        // �������ÿ���ع��漰�Ľڵ�, �����ݿ��ʵ�����������Ӧ�Ĳ��԰汾��¼
    //        tvu = new TreeViewUtils();
    //        proc = new EnumTreeViewProc(CopyIt);
    //        tvu.FindTreeViewNode(tree, proc);

    //        // ���������ⱨ�浥�����Ļع�������ݱ�
    //        BuildRequires();

    //        Common.ProjectInfo.SetDocString(db, pid, newver, null, "�ع�������", _kl.ToString());
    //    }

    //    private static bool SearchIt(TreeNode tn)
    //    {
    //        NodeTagInfo tag = tn.Tag as NodeTagInfo;
    //        if (tag != null)
    //        {
    //            if (!tag.IsShortcut)
    //            {
    //                if (_uctids.Contains(tag.id))
    //                    _nodes.Add(tn);
    //            }
    //        }

    //        return true; // ����, false��ֹͣ��������
    //    }

    //    private static bool TrimIt(TreeNode tn)
    //    {
    //        NodeTagInfo tag = tn.Tag as NodeTagInfo;
    //        if (tag != null)
    //        {
    //            if (tag.IsShortcut)
    //                _liremove.Add(tn);
    //            else if ((!tag.NeedRegress) &&
    //                (tag.nodeType != NodeType.Project))
    //            {
    //                _liremove.Add(tn);
    //            }
    //        }

    //        return true;
    //    }

    //    private static bool CopyIt(TreeNode tn)
    //    {
    //        NodeTagInfo tag = tn.Tag as NodeTagInfo;
    //        if (tag == null)
    //            return true;

    //        switch (tag.nodeType)
    //        {
    //            case NodeType.TestObject:
    //                CopyObj(tag);
    //                break;

    //            case NodeType.TestType:
    //                CopyType(tag);
    //                break;

    //            case NodeType.TestItem:
    //                CopyItem(tag);
    //                break;

    //            case NodeType.TestCase:
    //                CopyUC(tag, tn.Parent);
    //                CallBack1(tag.eid);
    //                break;
    //        }

    //        return true;
    //    }

    //    private static void CallBack1(string uceid)
    //    {
    //        _kl.AddKey(uceid);
    //    }

    //    private static void CopyObj(NodeTagInfo tag)
    //    {
    //        string sql = "INSERT INTO CA�������ʵ��� (ID, ��ĿID, �������ID, ���, ���԰汾) VALUES (?,?,?,?,?)";
    //        _db.ExecuteNoQuery(sql, FunctionClass.NewGuid, _pid, tag.eid, tag.order, _newver);
    //    }

    //    private static void CopyType(NodeTagInfo tag)
    //    {
    //        string sql = "INSERT INTO CA��������ʵ��� (ID, ��ĿID, ��������ID, ���, ���԰汾) VALUES (?,?,?,?,?)";
    //        _db.ExecuteNoQuery(sql, FunctionClass.NewGuid, _pid, tag.eid, tag.order, _newver);
    //    }

    //    private static void CopyItem(NodeTagInfo tag)
    //    {
    //        string sql = "SELECT ׷�ٹ�ϵ FROM CA������ʵ��� WHERE ID=?";
    //        object o = _db.ExecuteScalar(sql, tag.id);
    //        string s = string.Empty;
    //        if ((o != null) &&
    //            (!DBNull.Value.Equals(o)))
    //            s = ZString.TransUseMap((string)o, _oldMapNew, _requires);
    //        sql = "INSERT INTO CA������ʵ��� (ID, ������ID, ���, ׷�ٹ�ϵ, ��ĿID, ���԰汾) VALUES (?,?,?,?,?,?)";
    //        o = FunctionClass.NewGuid;
    //        _db.ExecuteNoQuery(sql, o, tag.eid, tag.order, s, _pid, _newver);
    //        _itemMap.Add(tag.id, (string)o);
    //    }

    //    private static void CopyUC(NodeTagInfo tag, TreeNode parent)
    //    {
    //        string uctid = FunctionClass.NewGuid as string;
    //        string sql = "INSERT INTO CA��������ʵ��� (ID, ��ĿID, ��������ID, ִ��״̬, ִ�н��, ���԰汾, ���ɷ�ʽ) VALUES (?,?,?,?,?,?,?)";
    //        _db.ExecuteNoQuery(sql, uctid, _pid, tag.eid, "δִ��", string.Empty, _newver, "R");

    //        NodeTagInfo parentTag = parent.Tag as NodeTagInfo;
    //        string itemtid = _itemMap[parentTag.id];
    //        sql = "INSERT INTO CA����������������ϵ�� (ID, ��ĿID, ��������ID, ������ID, ���, ֱ��������־) VALUES (?,?,?,?,?,?)";
    //        _db.ExecuteNoQuery(sql, FunctionClass.NewGuid, _pid, uctid, itemtid, tag.order, true);

    //        // ���ƴ������Ĳ��Բ���
    //        CopyNewVerSteps(_db, _pid, _prever, _newver, tag.eid);
    //    }

    //    // ���Ʋ��������µ����в��Բ���(�γ��²��԰汾�µĲ��������Ĳ��Թ���ʵ���)
    //    public static void CopyNewVerSteps(DBAccess db, string pid, string previd, string newvid, string uceid)
    //    {
    //        string sql = "SELECT CA���Թ���ʵ���.ID, CA���Թ���ʵ���.��� FROM CA���Թ���ʵ��� " +
    //            "INNER JOIN CA���Թ���ʵ��� ON CA���Թ���ʵ���.����ID = CA���Թ���ʵ���.ID WHERE " +
    //            "CA���Թ���ʵ���.��������ID=? AND CA���Թ���ʵ���.��ĿID=? AND CA���Թ���ʵ���.���԰汾=? ORDER BY CA���Թ���ʵ���.���";
    //        DataTable tbl = db.ExecuteDataTable(sql, uceid, pid, previd);
    //        int seq = 1;
    //        sql = "INSERT INTO CA���Թ���ʵ��� (ID, ��ĿID, ����ID, ���԰汾, ���) VALUES (?,?,?,?,?)";
    //        foreach (DataRow row in tbl.Rows)
    //        {
    //            db.ExecuteNoQuery(sql, FunctionClass.NewGuid, pid, row["ID"], newvid, seq++);
    //        }
    //    }

    //    private static void MarkNodeToRegress(TreeNode node)
    //    {
    //        (node.Tag as NodeTagInfo).NeedRegress = true;
    //        TreeNode temp = node.Parent;
    //        NodeTagInfo tag = temp.Tag as NodeTagInfo;
    //        while (tag.nodeType != NodeType.Project)
    //        {
    //            tag.NeedRegress = true;
    //            if (tag.nodeType == NodeType.TestItem)
    //            {
    //                if (!_pitemtids.Contains(tag.id))
    //                    _pitemtids.Add(tag.id);
    //            }
    //            temp = temp.Parent;
    //            tag = temp.Tag as NodeTagInfo;
    //        }
    //    }

    //    private static void BuildRequires()
    //    {
    //        List<string> li = new List<string>(_requires);

    //        string sql = "SELECT * FROM SYS�������ݱ�";
    //        DataTable tbl = _db.ExecuteDataTable(sql);

    //        foreach (string s in _requires)
    //        {
    //            string temp = s;
    //            while (!temp.Equals("~root"))
    //            {
    //                if (!li.Contains(temp))
    //                    li.Add(temp);

    //                string filter = string.Format("ID=\'{0}\'", temp);
    //                DataRow[] rows = tbl.Select(filter);
    //                if (rows.Length == 0)
    //                    break;

    //                temp = (string)rows[0]["���ڵ�ID"];
    //            }
    //        }

    //        // �������ݿ��¼
    //        foreach (string s in li)
    //        {
    //            string filter = string.Format("ID=\'{0}\'", s);
    //            DataRow[] rows = tbl.Select(filter);
    //            if (rows.Length == 0)
    //                continue;

    //            sql = "INSERT INTO SYS�������ݱ� (ID, ��ĿID, ��������, ���, ��������, ��������˵��, " +
    //                "�������ݱ�ʶ, ���ڵ�ID, �½ں�, �Ƿ�׷��, δ׷��ԭ��˵��, ���԰汾) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)";

    //            string parentid = (string)rows[0]["���ڵ�ID"];
    //            if (!parentid.Equals("~root"))
    //                parentid = _oldMapNew[(string)rows[0]["���ڵ�ID"]];

    //            _db.ExecuteNoQuery(sql, _oldMapNew[s], _pid, 2, rows[0]["���"], rows[0]["��������"],
    //                rows[0]["��������˵��"], rows[0]["�������ݱ�ʶ"], parentid, rows[0]["�½ں�"],
    //                rows[0]["�Ƿ�׷��"], rows[0]["δ׷��ԭ��˵��"], _newver);
    //        }
    //    }


    //    // ��ȡ�����ⱨ�浥����׷�ٵ��������������Ӧ��������
    //    private static void GetPblRequires(DBAccess db, string pid, string prever,
    //        List<string> reqs, List<string> itemtids)
    //    {
    //        string sql = "SELECT ID, ׷�ٹ�ϵ FROM CA������ʵ��� WHERE ��ĿID=? AND ���԰汾=?";
    //        DataTable tbl = db.ExecuteDataTable(sql, pid, prever);

    //        foreach (string tid in itemtids)
    //        {
    //            string filter = string.Format("ID=\'{0}\'", tid);
    //            DataRow[] rows = tbl.Select(filter);

    //            if (rows.Length > 0)
    //            {
    //                object o = rows[0]["׷�ٹ�ϵ"];
    //                if ((o != null) &&
    //                    (!DBNull.Value.Equals(o)) &&
    //                    (!(o as string).Equals(string.Empty)))
    //                {
    //                    SplitRequires(o as string, reqs);
    //                }
    //            }
    //        }
    //    }

    //    // ��ȡ�����ⱨ�����ӵĸ�������
    //    private static void GetPblExtraReqs(DBAccess db, List<string> pbls, List<string> reqs)
    //    {
    //        string sql = "SELECT �������� FROM CA���ⱨ�浥 WHERE ID=?";
    //        foreach (string pblid in pbls)
    //        {
    //            object o = db.ExecuteScalar(sql, pblid);
    //            if ((o != null) &&
    //                (!DBNull.Value.Equals(o)) &&
    //                (!(o as string).Equals(string.Empty)))
    //            {
    //                SplitRequires(o as string, reqs);
    //            }
    //        }
    //    }

    //    #endregion �����ⱨ�浥���ɻع�����

    //    #region HG����������

    //    /// <summary>
    //    /// ��ȡĳ����Ŀĳ�����԰汾������������������
    //    /// </summary>
    //    /// <param name="db"></param>
    //    /// <param name="pid"></param>
    //    /// <param name="vid"></param>
    //    /// <returns></returns>
    //    private static List<string> GetAlterRequires(DBAccess db, string pid, string vid)
    //    {
    //        string sql = "SELECT ��ز������� FROM HG���������� WHERE ��ĿID=? AND ���԰汾=?";
    //        DataTable tbl = db.ExecuteDataTable(sql, pid, vid);
    //        List<string> requires = new List<string>();
    //        foreach (DataRow row in tbl.Rows)
    //        {
    //            object o = row["��ز�������"];
    //            if ((o != null) &&
    //                (!DBNull.Value.Equals(o)))
    //            {
    //                SplitRequires((string)o, requires);
    //            }
    //        }

    //        return requires;
    //    }

    //    // ��(XXX,XXX,XXX)�л��ɵ����ַ����ŵ��б���
    //    private static void SplitRequires(string s, List<string> requires)
    //    {
    //        string[] ss = s.Split(',');
    //        foreach (string s1 in ss)
    //        {
    //            if (!requires.Contains(s1))
    //                requires.Add(s1);
    //        }
    //    }

    //    /// <summary>
    //    /// ��ǰһ�汾�Ĳ�����ʵ�������ɸѡ, �������漰���������ݵĲ�����ʵ��ID���õ��б��з���
    //    /// </summary>
    //    /// <param name="db"></param>
    //    /// <param name="pid"></param>
    //    /// <param name="vid"></param>
    //    /// <param name="requires"></param>
    //    /// <returns></returns>
    //    private static List<string> RequiresTraceItem(DBAccess db, string pid, string vid, List<string> requires)
    //    {
    //        string sql = "SELECT ID, ׷�ٹ�ϵ FROM CA������ʵ��� WHERE ��ĿID=? AND ���԰汾=?";
    //        DataTable tbl = db.ExecuteDataTable(sql, pid, vid);
    //        List<string> itemtids = new List<string>();
    //        foreach (DataRow row in tbl.Rows)
    //        {
    //            object o = row["׷�ٹ�ϵ"];
    //            if ((o != null) &&
    //                (!DBNull.Value.Equals(o)))
    //            {
    //                string[] ss = ((string)o).Split(',');
    //                foreach (string s in ss)
    //                {
    //                    if (requires.Contains(s))
    //                    {
    //                        if (!itemtids.Contains((string)row["ID"]))
    //                        {
    //                            itemtids.Add((string)row["ID"]);
    //                            break;
    //                        }
    //                    }
    //                }
    //            }
    //        }

    //        return itemtids;
    //    }

    //    // ������, �������������漰���Ĳ�����ڵ���õ��б�_itemnodes��
    //    private static bool GetAlterItemNodes(TreeNode tn)
    //    {
    //        NodeTagInfo tag = tn.Tag as NodeTagInfo;
    //        if (tag != null)
    //        {
    //            if ((tag.nodeType == NodeType.TestItem) &&
    //                (_itemtids.Contains(tag.id)))
    //                _itemnodes.Add(tn);
    //        }

    //        return true;
    //    }

    //    // �����������ӽڵ����"�ع�"���
    //    private static bool MarkChildNodes(TreeNode tn)
    //    {
    //        NodeTagInfo tag = tn.Tag as NodeTagInfo;

    //        if (!tag.IsShortcut)
    //            tag.NeedRegress = true;

    //        return true;
    //    }

    //    #endregion HG����������

    //    #region HG�ع����δ����ԭ��

    //    // �����¼�¼
    //    public static void InsertUntest(DBAccess db, object pid, object eid, int entitype,
    //        string reason, object trace, object vid, string objtid)
    //    {
    //        string sql = "INSERT INTO HG�ع����δ����ԭ�� (ID, ��ĿID, ʵ��ID, ʵ������, δ����ԭ��, �漰����, ���԰汾, �����������ʵ��ID) " +
    //            "VALUES (?,?,?,?,?,?,?,?)";
    //        db.ExecuteNoQuery(sql, FunctionClass.NewGuid, pid, eid, entitype, reason, trace, vid, objtid);
    //    }

    //    #endregion HG�ع����δ����ԭ��
    //}

}







