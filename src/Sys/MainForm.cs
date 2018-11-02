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
            timer2.Enabled = true;  // 激活检查定时器

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
        /// 根据当前选中的版本初始化窗体
        /// </summary>
        public bool InitFormByVersion()
        {
            ShowControls(true);

            object previd = DBLayer1.GetPreVersion(dbProject, currentvid);
            tsbProjectTypeSwitch.Enabled = previd == null;

            // 设置标题文本
            string projectname = Common.ProjectInfo.GetProjectString(dbProject, pid, "项目名称");
            string vername = Common.ProjectInfo.GetDocString(dbProject, pid, currentvid, null, "版本名称");
            ProjectStageType pst = DBLayer1.GetProjectType(dbProject, pid);
            this.Text = string.Format("{0} - {1}({4}) - {2} - {3}", us.LastDatabaseName, projectname, vername, GlobalData.ToolName, FunctionClass.GetEnumDescription(pst));
            if(CreateNaviBarAndMenu() == false) return false;
            return true;
        }

        /// <summary>
        /// 用户登录后自动选择版本
        /// </summary>
        static void AutoSelectVersion()
        {
            DataTable dt = DBLayer1.GetProjectVersionList(dbProject, pid, false);

            if(dt.Rows.Count == 0)
            {   // 该项目中没有版本，自动创建一个？
            }
            else
            {
                if(GridAssist.GetDataRow(dt, "ID", currentvid) == null)
                    globalData.currentvid = dt.Rows[0]["ID"];   // 自动选取第一个版本
            }
        }

        bool IsValid = true;

        /// <summary>
        /// 定时检查加密狗的状态
        /// </summary>
        void timer2_Tick(object sender, EventArgs e)
        {
            if(!IsValid) return;
            if (CheckDog.Inst.dogType == DogType.noDog)
            {   // 狗无效
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
            DelayCreateForm(url + param, false); // 已经保存过了
        }

        /// <summary>
        /// 将f显示在右边网格内
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
        /// 是否保存已经打开的窗口，缺省值false
        /// </summary>
        bool saveLast;

        /// <summary>
        /// 延迟创建窗口
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
        /// 定时检查 delayurl 并创建窗口
        /// </summary>
        void timer3_Tick(object sender, EventArgs e)
        {
            if(delayurl == null) return;

            if(saveLast)
            {   // 如果关闭不成功，则取消导航
                if(CloseCurrentForm(true, true) == false)
                    return;
            }

            Form f = FormClass.GetFormFromUrl(delayurl) as Form;
            string tempurl = delayurl;
            delayurl = null;
            if(f == null) return;

            // 自动查找最匹配的节点
            TreeNode tn2 = GetTreeNodeByUrl(treeView1.Nodes, tempurl);
            if(tn2 != null)
                SetSelectNode(tn2);
            ShowControl(f, tn2);
        }

        /// <summary>
        /// 查找和指定URL最匹配的树节点
        /// </summary>
        static TreeNode GetTreeNodeByUrl(TreeNodeCollection tnc, string url)
        {
            string className = FormClass.GetClassNameFromUrl(url);
            Dictionary<string, string> dic = FormClass.GetParamsFromUrl(url);

            // 检查URL参数是否匹配 
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
        /// 根据内嵌XML的内容创建导航树和菜单
        /// </summary>
        bool CreateNaviBarAndMenu()
        {
            try
            {
                //读入树结构定义XML文件
                XmlElement root = GetConfigFileElement();

                currentSelectedForm = null;  //!!! 重要，防止重新保存上一次的页面
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
                if(previd != null)     // 回归模式，再判断是否准备阶段
                {
                    //VersionMode vm = DBLayer1.GetVersionMode(dbProject, pid, currentvid);
                    //configFilename = vm == VersionMode.Execute ? "tree_m" : "tree_p";    // tree_r
                    configFilename = "tree_m";
                    if(pst == ProjectStageType.定型) configFilename = "tree-定型-回归";
                }
                else
                {
                    // 第一次测试根据不同的类型返回不同的节点树
                    if(pst == ProjectStageType.I类) configFilename = "tree-完全";
                    if(pst == ProjectStageType.II类) configFilename = "tree-简化";
                    if(pst == ProjectStageType.III类) configFilename = "tree-最小";
                    if(pst == ProjectStageType.定型) configFilename = "tree-定型";
                }
                configFilename += ".xml";

                string s = FunctionClass.GetEmbedText("TPM3.Config.项目树." + configFilename);
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
            // level: 0 叶节点，其它：到叶节点的层数
            parent.SetAttribute("Level", level.ToString());
            return level;
        }

        //插入树节点
        protected void InsertDocTreeNode(XmlElement parent, TreeNodeCollection Nodes, string parentDocName)
        {
            foreach(XmlElement ele in parent.SelectNodes("TreeNode"))
            {
                DocTreeNode tn = new DocTreeNode(ele, panel1);

                tn.helpInfo = GlobalData.GetParam(ele);
                tn.ImageKey = tn.SelectedImageKey = ele.GetAttribute("Icon");
                //SetTreeNodeImage(tn, s1, s2);

                // 文档名。如果有此属性(DocNam)，表示该节点是文档，此节点内容是文档名。
                // 如果没有此属性，表示该节点是文档的子内容，文档名从父节点继承
                string docName2 = parentDocName;
                if(ele.HasAttribute("DocName"))
                {
                    docName2 = ele.GetAttribute("DocName");
                    if(docName2.Length == 0) // 为空表示文档名与节点名相同
                        docName2 = DocTreeNode.GetNodeName(ele);
                }
                tn.documentName = docName2;

                Nodes.Add(tn);
                if(!string.IsNullOrEmpty(tn.nodeClass))
                    tn.ForeColor = Color.DarkBlue;

                // 递归插入
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

            // 下面代码解决隐藏了导航树后treeview滚动条失效的问题
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
            //// 重新加载树节点
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
        /// 清空指定数据库的内容
        /// </summary>
        void miBlankMDB_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "清空数据库";
            dlg.CheckFileExists = true;
            dlg.Filter = "Access文件(.mdb)|*.mdb";
            if(dlg.ShowDialog() != DialogResult.OK) return;
            string[] sqlList = new[]{"CA被测对象实测表","CA被测对象实体表","CA测试过程实测表","CA测试过程实体表","CA测试类型实测表",  "CA测试类型实体表",
                "CA测试项实测表","CA测试项实体表","CA测试用例实测表","CA测试用例实体表","CA测试用例与测试项关系表", "CA问题报告单",
                "DC测试过程附件表","DC测试资源配置表","DC测试组织与人员表","DC附件实体表","DC计划进度表", "DC密级表",
                "DC术语表","DC文档修改页","DC问题标识","DC问题级别表","DC引用文件表", "HG回归测试未测试原因",
                "SYS测试版本表","SYS测试依据表"};

            using(DBAccess dba = DBAccessFactory.FromAccessFile(dlg.FileName).CreateInst())
            {
                foreach(string t in sqlList)
                    dba.ExecuteNoQuery("delete from " + t + " where 项目ID = ? ", pid);
                dba.ExecuteNoQuery("update SYS文档内容表 set 文档内容 = null where 内容类型 = ?", "对象");
                dba.ExecuteNoQuery("update SYS文档内容表 set 文本内容 = null where 内容标题 <> ? and 内容标题 <> ? and 内容标题 <> ? and 内容标题 <> ? and 内容标题 <> ?  and 内容标题 <> ?", "数据库版本", "文档版本", "文档标识号", "文档名称", "问题标识分隔符", "应用软件名称");
            }
        }

        /// <summary>
        /// 显示查询日志窗口
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
                MessageBox.Show("打开用户手册文件失败!", "操作失败", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        void miAboutDog_Click(object sender, EventArgs e)
        {
            string s = GlobalData.ToolName + "\n\n" + "当前使用:  \n\n     ";
            s += CheckDog.Inst.GetDogTypeName() + "\n\n";
            MessageBox.Show(s, "关于使用许可证");
        }

        /// <summary>
        /// 显示网络协议服务器状态
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
        /// 切换测试版本
        /// </summary>
        void miSelectTestVersion_Click(object sender, EventArgs e)
        {
            IBaseTreeForm bf = currentSelectedForm as IBaseTreeForm;
            if(bf != null) bf.OnPageClose(false);

            ProjectVersionForm f = new ProjectVersionForm();
            if(f.ShowDialog() != DialogResult.OK) return;
            // 重新加载
            InitFormByVersion();
        }

        /// <summary>
        /// 创建测试版本(进行回归测试)
        /// </summary>
        void miCreateVersion_Click(object sender, EventArgs e)
        {
            NewVersionForm f = new NewVersionForm();
            f.ShowDialog();
        }

        /// <summary>
        /// 仅当版本被改变后才重新载入
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
                    string s = dr["版本名称"].ToString();
                    if(IsNull(dr["前向版本ID"])) s += "(初始版本)";
                    VersionMode vm = DBLayer1.GetVersionMode(dbProject, pid, vid);
                    if(vm == VersionMode.Prepare) s += "(准备阶段)";

                    ToolStripItem tsi = tsb.DropDownItems.Add(s);
                    tsi.ToolTipText = Common.ProjectInfo.GetDocString(dbProject, pid, vid, null, "版本说明");
                    tsi.Tag = vid;
                    tsi.Click += tsiSwitchVerion_Click;
                }
                VersionChanged = false;
            }
            foreach(ToolStripMenuItem tsi in tsb.DropDownItems)
                tsi.Checked = Equals(tsi.Tag, currentvid);
        }

        /// <summary>
        /// 快速切换到指定版本
        /// </summary>
        void tsiSwitchVerion_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsi = sender as ToolStripMenuItem;
            object vid = tsi.Tag;
            if(Equals(vid, currentvid)) return;   // 版本没有变

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
                ToolStripItem tsi1 = tsb.DropDownItems.Add("I类");
                tsi1.Tag = ProjectStageType.I类;

                ToolStripItem tsi2 = tsb.DropDownItems.Add("II类");
                tsi2.Tag = ProjectStageType.II类;

                ToolStripItem tsi3 = tsb.DropDownItems.Add("III类");
                tsi3.Tag = ProjectStageType.III类;

                ToolStripItem tsi4 = tsb.DropDownItems.Add("定型");
                tsi4.Tag = ProjectStageType.定型;

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
            if(pst1 == DBLayer1.GetProjectType(dbProject, pid)) return;     // 模式没有变

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
                    dlg.Text = "查找测试用例";

                    if(DialogResult.OK != dlg.ShowDialog())
                        return;

                    _findnodes.Clear();

                    Cursor oldcursor = this.Cursor;
                    this.Cursor = Cursors.WaitCursor;

                    _findwhat = dlg.FindWhat;

                    TreeViewUtils tvu = new TreeViewUtils();
                    EnumTreeViewProc proc = SearchIt;

                    TestTreeForm ttf = currentSelectedForm as TestTreeForm;
                    if(!BusiLogic.AssertNotNull(ttf, "运行状态非法, 无法执行查找!!", "操作失败"))
                        return;

                    TreeNode oldselnode = ttf.tree.SelectedNode;
                    tvu.FindTreeViewLeaf(ttf.tree, proc);

                    if(_findnodes.Count == 0)
                    {
                        MessageBox.Show("没有找到符合输入的测试用例!", "查找结果", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        ttf.tree.SelectedNode = oldselnode;
                    }
                    else
                    {
                        string msg = String.Format("查找结果: {0}个测试用例", _findnodes.Count);
                        MessageBox.Show(msg, "查找结果", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        _dlgfindresult = new FindUsecaseResultForm(_findnodes);
                        ttf.tree.SelectedNode = _findnodes[0];
                        _findnodes[0].EnsureVisible();

                        _dlgfindresult.Text = "查找结果[" + _findnodes.Count + "条记录]";
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
                    if(ZString.MatchFind(_findwhat, ZString.TrimChapter(tn.Text), 1/*部分匹配*/))
                        _findnodes.Add(tn);
            return true;
        }


        private int _nodevisi = 0; // 1被测对象, 2测试类型, 3测试项, 4测试用例
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
                case 1: // 至被测对象
                    if(tag.nodeType == NodeType.TestObject)
                        tn.EnsureVisible();
                    break;

                case 2: // 至测试类型
                    if((tag.nodeType == NodeType.TestType) ||
                        (tag.nodeType == NodeType.TestObject))
                        tn.EnsureVisible();
                    break;

                case 3: // 至测试项
                    if((tag.nodeType == NodeType.TestItem) ||
                        (tag.nodeType == NodeType.TestObject) ||
                        (tag.nodeType == NodeType.TestType))
                        tn.EnsureVisible();
                    break;

                case 4: // 至测试用例
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

        private void 测试窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ExecStatus.g_ttf == null)
            {
                ExecStatus.g_ttf = new TestTreeForm();
                ExecStatus.g_ttf.BuildTestTree();
            }

            //TestUsecaseForm tuf = new TestUsecaseForm("78LA9Q1GLDVU0L");    // 打开指定ID用例窗体
            //tuf.ShowDialog();
            TestUsecaseForm tuf = new TestUsecaseForm("78AM11OSOL8Q0R", "78AGQC30263UES");    // 打开特定位置的用例(上层测试项及快捷)
            tuf.ShowDialog();
        }

        void 测试问题报告窗体ToolStripMenuItem_Click(object sender, EventArgs e)
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
            // 设置不同节点类型的图标
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
        /// 从回归测试准备状态进入测试执行状态
        /// </summary>
        void tsbStartExecute_Click(object sender, EventArgs e)
        {
            object o = DBLayer1.GetPreVersion(dbProject, currentvid);
            if(o == null)
            {
                MessageBox.Show("目前为测试初始版本。“生成回归基础数据集功能”仅可以在回归版本中使用。", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //string msg = "确认要将当前的测试版本从测试准备模式转换为执行模式吗？\n\n转换后上一版本中需要回归测试的用例将会自动导入到该版本。\n也可以在该版本中手工添加以前版本中的用例";
            //DialogResult dr = MessageBox.Show(msg, "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            //if(dr != DialogResult.OK) return;

            //VersionMode vm = DBLayer1.GetVersionMode(dbProject, pid, currentvid);
            //if(vm == VersionMode.Execute)
            //{
            //    MessageBox.Show("当前程序为执行模式, 无法回退到准备模式!", "操作失败", MessageBoxButtons.OK,
            //         MessageBoxIcon.Error);
            //    return;
            //}

            //VersionMode vm2 = vm == VersionMode.Execute ? VersionMode.Prepare : VersionMode.Execute;
            //Common.ProjectInfo.SetDocString(dbProject, pid, currentvid, null, "当前模式", ((int)vm2).ToString());
            string msg = "确认要生成回归测试依据和测试设计基础集吗?\n\n若之前已经生成过, 原先的所有数据将丢失!!";
            DialogResult dr = MessageBox.Show(msg, @"确认操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
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
                MessageBox.Show("目前为测试初始版本, 导入功能无效!", "操作提示", MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                return;
            }

            //VersionMode vm = DBLayer1.GetVersionMode(dbProject, pid, currentvid);
            //if(vm == VersionMode.Prepare)
            //{
            //    MessageBox.Show("导入功能只在回归测试执行阶段有效!", "操作提示", MessageBoxButtons.OK,
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
        /// 进入项目策划窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miProjectPlan_Click(object sender, EventArgs e)
        {
            SelectProjectDB f = new SelectProjectDB();
            if(f.ShowDialog() != DialogResult.OK) return;
            // 重新加载
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

        private void 导入项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 导出项目ToolStripMenuItem_Click(object sender, EventArgs e)
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
        /// 对应本树节点的XML节点
        /// </summary>
        public XmlElement nodeElement;

        /// <summary>
        /// 帮助信息
        /// </summary>
        public string helpInfo = "";

        public DocTreeNode(XmlElement element, Panel panel1)
        {
            // 节点名
            nodeName = GetNodeName(element);
            base.Text = nodeName;

            if(nodeName.Length == 0)
                throw new Exception("NodeName属性不能为空");

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
    //    #region 由问题报告单生成回归数据(同期导入更动依据表)

    //    private static List<TreeNode> _nodes;   // 问题报告单所涉及的所有沿途节点
    //    private static List<string> _uctids;    // 问题报告单所涉及的所有用例实测ID
    //    private static string _pid;             // 当前项目ID
    //    private static string _prever;          // 前向测试版本ID
    //    private static string _newver;          // 新建回归测试版本ID
    //    private static DBAccess _db;            // 数据库
    //    private static Dictionary<string, string> _oldMapNew;   // SYS测试依据表中的ID -> 新建的ID(用于当前测试版本)
    //    private static List<string> _requires;  // 问题报告所引出的回归所涉及的需求前向版本中的依据表ID
    //    private static Dictionary<string, string> _itemMap;     // 测试项实测表原ID -> 回归版本中的实测ID
    //    private static List<string> _itemtids;      // 更动依据所涉及的所有测试项
    //    private static List<TreeNode> _itemnodes;   // 更动依据所涉及的所有测试项节点
    //    private static KeyList _kl;
    //    private static List<string> _pitemtids;     // 由问题->用例->测试项实测ID
    //    private static List<TreeNode> _liremove;    // 修剪时待删除的树节点
    //    /// <summary>
    //    /// 由需要回归的前一版本的问题报告单导出回归测试用例
    //    /// </summary>
    //    /// <param name="db"></param>
    //    /// <param name="pid"></param>
    //    /// <param name="prever"></param>
    //    /// <param name="newver"></param>
    //    public static void ExportFromPbl(DBAccess db, string pid, string prever, string newver)
    //    {
    //        TreeView tree = new TreeView();
    //        TreeNode root = MainTestFrmCommon.BuildTestTree(tree, db, pid, prever, 1, true);

    //        // 获取所有需回归的测试用例实测ID
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

    //        // 利用树, 获取所有需回归的用例节点
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

    //        // 检测每个用例节点是否需要回归
    //        TreeViewUtils tvu = new TreeViewUtils();
    //        Z1Utils.Controls.EnumTreeViewProc proc = new EnumTreeViewProc(SearchIt);
    //        tvu.FindTreeViewLeaf(tree, proc);

    //        // 给所有需回归的用例节点及其父节点至被测对象打上"回归"标记(同时获取问题涉及的测试项)
    //        foreach (TreeNode node in _nodes)
    //        {
    //            MarkNodeToRegress(node);
    //        }

    //        /////////////////////////////////////////////////////////////////////
    //        //-------------------------------------------------------------------
    //        // 插入软件更动涉及的测试依据导入的测试用例
    //        List<string> alterreq = GetAlterRequires(db, pid, newver);
    //        //----------------------------------
    //        // 由问题追踪到测试项, 获得测试项所涉及的所有测试依据
    //        GetPblRequires(db, pid, prever, alterreq, _pitemtids);

    //        GetPblExtraReqs(db, pbls, alterreq); // 添加问题附加依据

    //        //----------------------------------//

    //        _itemtids = RequiresTraceItem(db, pid, prever, alterreq); // 获取所有更动依据涉及的测试项

    //        tvu = new TreeViewUtils();
    //        proc = new EnumTreeViewProc(GetAlterItemNodes);
    //        tvu.FindTreeViewNode(tree, proc);   // 获取测试项节点列表

    //        foreach (TreeNode node in _itemnodes)
    //            MarkNodeToRegress(node);    // 所有测试项节点及上层节点打上"回归"标记

    //        // 所有测试项节点的子节点打上"回归"标记
    //        foreach (TreeNode node in _itemnodes)
    //        {
    //            tvu = new TreeViewUtils();
    //            proc = new EnumTreeViewProc(MarkChildNodes);
    //            tvu.FindNodeFromNode(node, proc);
    //        }

    //        _requires.AddRange(alterreq);
    //        //-------------------------------------------------------------------
    //        /////////////////////////////////////////////////////////////////////

    //        // 裁剪树, 去除所有非回归的节点
    //        tvu = new TreeViewUtils();
    //        proc = new EnumTreeViewProc(TrimIt);
    //        tvu.FindTreeViewNode(tree, proc);

    //        foreach (TreeNode node in _liremove)
    //        {
    //            node.Remove();
    //        }
    //        // 针对树中每个回归涉及的节点, 在数据库的实测表中生成相应的测试版本记录
    //        tvu = new TreeViewUtils();
    //        proc = new EnumTreeViewProc(CopyIt);
    //        tvu.FindTreeViewNode(tree, proc);

    //        // 生成由问题报告单导出的回归测试依据表
    //        BuildRequires();

    //        Common.ProjectInfo.SetDocString(db, pid, newver, null, "回归用例表", _kl.ToString());
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

    //        return true; // 继续, false则停止搜索返回
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
    //        string sql = "INSERT INTO CA被测对象实测表 (ID, 项目ID, 被测对象ID, 序号, 测试版本) VALUES (?,?,?,?,?)";
    //        _db.ExecuteNoQuery(sql, FunctionClass.NewGuid, _pid, tag.eid, tag.order, _newver);
    //    }

    //    private static void CopyType(NodeTagInfo tag)
    //    {
    //        string sql = "INSERT INTO CA测试类型实测表 (ID, 项目ID, 测试类型ID, 序号, 测试版本) VALUES (?,?,?,?,?)";
    //        _db.ExecuteNoQuery(sql, FunctionClass.NewGuid, _pid, tag.eid, tag.order, _newver);
    //    }

    //    private static void CopyItem(NodeTagInfo tag)
    //    {
    //        string sql = "SELECT 追踪关系 FROM CA测试项实测表 WHERE ID=?";
    //        object o = _db.ExecuteScalar(sql, tag.id);
    //        string s = string.Empty;
    //        if ((o != null) &&
    //            (!DBNull.Value.Equals(o)))
    //            s = ZString.TransUseMap((string)o, _oldMapNew, _requires);
    //        sql = "INSERT INTO CA测试项实测表 (ID, 测试项ID, 序号, 追踪关系, 项目ID, 测试版本) VALUES (?,?,?,?,?,?)";
    //        o = FunctionClass.NewGuid;
    //        _db.ExecuteNoQuery(sql, o, tag.eid, tag.order, s, _pid, _newver);
    //        _itemMap.Add(tag.id, (string)o);
    //    }

    //    private static void CopyUC(NodeTagInfo tag, TreeNode parent)
    //    {
    //        string uctid = FunctionClass.NewGuid as string;
    //        string sql = "INSERT INTO CA测试用例实测表 (ID, 项目ID, 测试用例ID, 执行状态, 执行结果, 测试版本, 生成方式) VALUES (?,?,?,?,?,?,?)";
    //        _db.ExecuteNoQuery(sql, uctid, _pid, tag.eid, "未执行", string.Empty, _newver, "R");

    //        NodeTagInfo parentTag = parent.Tag as NodeTagInfo;
    //        string itemtid = _itemMap[parentTag.id];
    //        sql = "INSERT INTO CA测试用例与测试项关系表 (ID, 项目ID, 测试用例ID, 测试项ID, 序号, 直接所属标志) VALUES (?,?,?,?,?,?)";
    //        _db.ExecuteNoQuery(sql, FunctionClass.NewGuid, _pid, uctid, itemtid, tag.order, true);

    //        // 复制此用例的测试步骤
    //        CopyNewVerSteps(_db, _pid, _prever, _newver, tag.eid);
    //    }

    //    // 复制测试用例下的所有测试步骤(形成新测试版本下的测试用例的测试过程实测表)
    //    public static void CopyNewVerSteps(DBAccess db, string pid, string previd, string newvid, string uceid)
    //    {
    //        string sql = "SELECT CA测试过程实体表.ID, CA测试过程实测表.序号 FROM CA测试过程实体表 " +
    //            "INNER JOIN CA测试过程实测表 ON CA测试过程实测表.过程ID = CA测试过程实体表.ID WHERE " +
    //            "CA测试过程实体表.测试用例ID=? AND CA测试过程实体表.项目ID=? AND CA测试过程实测表.测试版本=? ORDER BY CA测试过程实测表.序号";
    //        DataTable tbl = db.ExecuteDataTable(sql, uceid, pid, previd);
    //        int seq = 1;
    //        sql = "INSERT INTO CA测试过程实测表 (ID, 项目ID, 过程ID, 测试版本, 序号) VALUES (?,?,?,?,?)";
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

    //        string sql = "SELECT * FROM SYS测试依据表";
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

    //                temp = (string)rows[0]["父节点ID"];
    //            }
    //        }

    //        // 复制数据库记录
    //        foreach (string s in li)
    //        {
    //            string filter = string.Format("ID=\'{0}\'", s);
    //            DataRow[] rows = tbl.Select(filter);
    //            if (rows.Length == 0)
    //                continue;

    //            sql = "INSERT INTO SYS测试依据表 (ID, 项目ID, 依据类型, 序号, 测试依据, 测试依据说明, " +
    //                "测试依据标识, 父节点ID, 章节号, 是否追踪, 未追踪原因说明, 测试版本) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)";

    //            string parentid = (string)rows[0]["父节点ID"];
    //            if (!parentid.Equals("~root"))
    //                parentid = _oldMapNew[(string)rows[0]["父节点ID"]];

    //            _db.ExecuteNoQuery(sql, _oldMapNew[s], _pid, 2, rows[0]["序号"], rows[0]["测试依据"],
    //                rows[0]["测试依据说明"], rows[0]["测试依据标识"], parentid, rows[0]["章节号"],
    //                rows[0]["是否追踪"], rows[0]["未追踪原因说明"], _newver);
    //        }
    //    }


    //    // 获取由问题报告单向上追踪到测试项的所有相应测试依据
    //    private static void GetPblRequires(DBAccess db, string pid, string prever,
    //        List<string> reqs, List<string> itemtids)
    //    {
    //        string sql = "SELECT ID, 追踪关系 FROM CA测试项实测表 WHERE 项目ID=? AND 测试版本=?";
    //        DataTable tbl = db.ExecuteDataTable(sql, pid, prever);

    //        foreach (string tid in itemtids)
    //        {
    //            string filter = string.Format("ID=\'{0}\'", tid);
    //            DataRow[] rows = tbl.Select(filter);

    //            if (rows.Length > 0)
    //            {
    //                object o = rows[0]["追踪关系"];
    //                if ((o != null) &&
    //                    (!DBNull.Value.Equals(o)) &&
    //                    (!(o as string).Equals(string.Empty)))
    //                {
    //                    SplitRequires(o as string, reqs);
    //                }
    //            }
    //        }
    //    }

    //    // 获取由问题报告添加的附加依据
    //    private static void GetPblExtraReqs(DBAccess db, List<string> pbls, List<string> reqs)
    //    {
    //        string sql = "SELECT 附加依据 FROM CA问题报告单 WHERE ID=?";
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

    //    #endregion 由问题报告单生成回归数据

    //    #region HG软件更动表

    //    /// <summary>
    //    /// 获取某个项目某个测试版本的软件更动测试依据
    //    /// </summary>
    //    /// <param name="db"></param>
    //    /// <param name="pid"></param>
    //    /// <param name="vid"></param>
    //    /// <returns></returns>
    //    private static List<string> GetAlterRequires(DBAccess db, string pid, string vid)
    //    {
    //        string sql = "SELECT 相关测试依据 FROM HG软件更动表 WHERE 项目ID=? AND 测试版本=?";
    //        DataTable tbl = db.ExecuteDataTable(sql, pid, vid);
    //        List<string> requires = new List<string>();
    //        foreach (DataRow row in tbl.Rows)
    //        {
    //            object o = row["相关测试依据"];
    //            if ((o != null) &&
    //                (!DBNull.Value.Equals(o)))
    //            {
    //                SplitRequires((string)o, requires);
    //            }
    //        }

    //        return requires;
    //    }

    //    // 将(XXX,XXX,XXX)切换成单个字符串放到列表中
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
    //    /// 对前一版本的测试项实测表进行筛选, 将所有涉及到更动依据的测试项实测ID放置到列表中返回
    //    /// </summary>
    //    /// <param name="db"></param>
    //    /// <param name="pid"></param>
    //    /// <param name="vid"></param>
    //    /// <param name="requires"></param>
    //    /// <returns></returns>
    //    private static List<string> RequiresTraceItem(DBAccess db, string pid, string vid, List<string> requires)
    //    {
    //        string sql = "SELECT ID, 追踪关系 FROM CA测试项实测表 WHERE 项目ID=? AND 测试版本=?";
    //        DataTable tbl = db.ExecuteDataTable(sql, pid, vid);
    //        List<string> itemtids = new List<string>();
    //        foreach (DataRow row in tbl.Rows)
    //        {
    //            object o = row["追踪关系"];
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

    //    // 遍历树, 将更动依据所涉及到的测试项节点放置到列表_itemnodes中
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

    //    // 测试项所有子节点打上"回归"标记
    //    private static bool MarkChildNodes(TreeNode tn)
    //    {
    //        NodeTagInfo tag = tn.Tag as NodeTagInfo;

    //        if (!tag.IsShortcut)
    //            tag.NeedRegress = true;

    //        return true;
    //    }

    //    #endregion HG软件更动表

    //    #region HG回归测试未测试原因

    //    // 添加新记录
    //    public static void InsertUntest(DBAccess db, object pid, object eid, int entitype,
    //        string reason, object trace, object vid, string objtid)
    //    {
    //        string sql = "INSERT INTO HG回归测试未测试原因 (ID, 项目ID, 实体ID, 实体类型, 未测试原因, 涉及依据, 测试版本, 所属被测对象实测ID) " +
    //            "VALUES (?,?,?,?,?,?,?,?)";
    //        db.ExecuteNoQuery(sql, FunctionClass.NewGuid, pid, eid, entitype, reason, trace, vid, objtid);
    //    }

    //    #endregion HG回归测试未测试原因
    //}

}








