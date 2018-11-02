using System;
using System.Data;
using System.Windows.Forms;
using Common;
using Common.Database;
using TPM3.Sys;
using System.Text;
using Z1.tpm.DB;

namespace TPM3.wx
{
    /// <summary>
    /// 新建项目
    /// </summary>
    public partial class NewProjectForm : Form
    {
        public NewProjectForm()
        {
            InitializeComponent();
        }

        void NewProjectForm_Load(object sender, EventArgs e)
        {
            radioButton_CheckedChanged(null, null);
        }

        void btBrowse1_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            f.FileName = tbPath1.Text;
            f.Filter = "项目数据库(*.mdb)|*.mdb|所有文件(*.*)|*.*";
            if(f.ShowDialog() == DialogResult.Cancel) return;
            tbPath1.Text = f.FileName;
            radioButton_CheckedChanged(null, null);
        }

        void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            cbProject.Items.Clear();
            panel1.Visible = !radioButton1.Checked;
            panel2.Visible = radioButton1.Checked;
            label3.Visible = tbPath1.Visible = btBrowse1.Visible = radioButton3.Checked;

            if(radioButton2.Checked)
            {   // 使用本项目中的数据库
                LoginForm.InitProjectList(dbProject, cbProject, null);
            }
            if(radioButton3.Checked)
            {   // 使用其它项目中的数据库


                using(DBAccess dba = DBAccessFactory.FromAccessFile(tbPath1.Text).CreateInst(DBAccessStaticFunctionClass.NullMessage))
                {
                    if(dba == null) return;
                    LoginForm.InitProjectList(dba, cbProject, null);
                }
            }
        }

        void CreateProject()
        {
            if(IsNull(tbProjectName.Text)) ThrowException("项目名称不能为空");
            if(IsNull(tbProjectSign.Text)) ThrowException("项目标识不能为空");
            if(radioButton1.Checked)
                if(IsNull(tbVersionName.Text)) ThrowException("初始版本号不能为空");

            Scalar2 sc = cbProject.SelectedItem as Scalar2;
            if(!radioButton1.Checked && sc == null)
                ThrowException("必须选中模板项目");

            DBAccess dba = null;
            if(radioButton2.Checked)
                dba = dbProject;
            if(radioButton3.Checked)
                dba = DBAccessFactory.FromAccessFile(tbPath1.Text).CreateInst(DBAccessStaticFunctionClass.ThrowException);

            DataSet ds = null;
            if(dba != null)
            {   // 导出模板项目
                MyProjectBackup bk1 = new MyProjectBackup(dbProject, sc.Key);
                bk1.BackupProject();
                ds = bk1.ds;
            }

            object newpid = DBLayer1.CreateNewProject(dbProject, tbProjectName.Text, tbProjectSign.Text);
            UserSetting.Default.lastProjectID = newpid;   // 缺省选中新建的项目


            //*******************************************************************************************************
            // added by zhouxindong at 2009/07/01

            if((ds != null) && (checkBox1.Checked)) // 基于模板库生成初始测试版本
            {
                object srcpid = sc.Key; // 源数据库中模板项目的项目ID

                object srcvid = CommonDB.GetInitVersion(dba, srcpid);
                if(srcvid == null)
                    MessageBox.Show("无法获取源项目的初始测试版本ID!\n", "创建项目失败", MessageBoxButtons.OK,
                         MessageBoxIcon.Error);
                else
                {
                    CreateFromTpl cft = new CreateFromTpl(dbProject, srcvid, srcpid, newpid, ds);
                    cft.CreateNewPrj();
                }
            }

            //*******************************************************************************************************//

            if(radioButton3.Checked)
                ((IDisposable)dba).Dispose();

            if((ds != null) && (!checkBox1.Checked))
            {
                MyProjectBackup bk2 = new MyProjectBackup(dbProject, newpid);
                bk2.ds = ds;
                bk2.RestoreProject();
            }

            // 如果没有使用模板，则缺省创建一个版本


            if(radioButton1.Checked)
            {
                object vid = DBLayer1.CreateNewVersion(dbProject, newpid, null, tbVersionName.Text, tbVersionMemo.Text);
                GlobalData.globalData.currentvid = vid;
            }
        }

        void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                CreateProject();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        DBAccess dbProject { get { return GlobalData.globalData.dbProject; } }
        static bool IsNull(object obj) { return GridAssist.IsNull(obj); }
        static bool ThrowException(string msg) { throw new Exception(msg); }
    }


    public class MyProjectBackup : ProjectBackup<MyProjectBackup>
    {
        public MyProjectBackup(DBAccess dbProject, object projectID)
            : base(dbProject, projectID)
        {
        }

        static MyProjectBackup()
        {
            SysTableName = "SYS测试项目表";

            AddTable("SYS测试版本表", "前向版本ID");
            AddTable("SYS测试依据表", "父节点ID", "测试版本");
            AddTable("SYS文档内容表", "测试版本");
            AddTable("SYS项目用户表");
            AddTable("HG软件更动表", "相关测试依据", "测试版本");       // 引用测试依据表



            //AddTable("DC测试类型模板表", "父节点ID");
            AddTable("DC测试项优先级表");
            AddTable("DC测试用例设计方法表");
            AddTable("DC测试资源配置表", "测试版本");
            AddTable("DC测试组织与人员表", "测试版本");
            AddTable("DC计划进度表", "主要完成人", "测试版本");
            AddTable("DC术语表", "测试版本");
            AddTable("DC文档修改页", "测试版本");
            AddTable("DC问题标识", "测试版本");
            AddTable("DC问题级别表");
            AddTable("DC引用文件表", "测试版本");

            AddTable("CA被测对象实体表");
            AddTable("CA被测对象实测表", "被测对象ID", "测试版本");
            AddTable("CA测试类型实体表", "所属被测对象ID", "父测试类型ID");
            AddTable("CA测试类型实测表", "测试类型ID", "测试版本");
            AddTable("CA测试项实体表", "所属测试类型ID", "父节点ID");
            AddTable("CA测试项实测表", "测试项ID", "追踪关系", "测试版本");
            AddTable("CA测试用例实体表", "设计人员");                                    // 引用测试人员表


            AddTable("CA测试用例实测表", "测试人员", "测试用例ID", "测试版本");          // 引用测试人员表


            AddTable("CA测试用例与测试项关系表", "测试用例ID", "测试项ID", "旧用例ID");
            AddTable("CA问题报告单", "问题类别", "问题级别", "所属被测对象ID", "报告人", "测试版本");  //??? 一级标识？
            AddTable("CA测试过程实体表", "测试用例ID");
            AddTable("CA测试过程实测表", "过程ID", "问题报告单ID", "测试版本");          // 引用问题单


            AddTable("DC附件实体表");
            AddTable("DC测试过程附件表", "测试过程ID", "附件实体ID", "测试版本");

            AddTable("HG回归测试未测试原因", "用例实体ID", "涉及依据", "测试版本");
        }
    }
}
