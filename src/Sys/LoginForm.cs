using System;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Common;
using Common.Database;
using MyDog.Dog;
using TPM3.Upgrade;
using TPM3.wx;
using CheckDog = MyDog.Dog.CheckDog;
using DogType = MyDog.Dog.DogType;

namespace TPM3.Sys
{
    /// <summary>
    /// 登录窗口
    /// </summary>
    public partial class LoginForm : MyBaseForm
    {
        public LoginForm()
        {
            InitializeComponent();
            this.Closing += LoginForm_Closing;
            this.miAbout.Click += LoginForm.miAbout_Click;
            this.miAboutDog.Click += miAboutDog_Click;
            this.miLicenseServer.Click += miLicenseServer_Click;
        }

        /// <summary>
        /// 浏览数据库
        /// </summary>
        void btBrowse1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FileDialog fd = new OpenFileDialog();

            fd.Filter = "项目文件(*.mdb)|*.mdb|所有文件(*.*)|*.*";
            fd.Title = "打开项目文件";
            if(fd.ShowDialog() != DialogResult.OK) return;

            // 打开数据库
            string fileName = fd.FileName;
            if(!us.lastDatabaseList.Contains(fileName))
            {
                while(us.lastDatabaseList.Count >= UserSetting.MaxDatabaseHistoryCount)
                    us.lastDatabaseList.RemoveAt(0);
                us.lastDatabaseList.Add(fileName);
            }
            AddToDropdownList(fileName);
            OpenDatabase(fileName);
        }

        void AddToDropdownList(string fileName)
        {
            allowSelectedIndexChangedEvent = false;
            try
            {
                for(int i = 0; i < cbDatabase.Items.Count; i++)
                {
                    string s = (cbDatabase.Items[i] as Scalar2).Key as string;
                    if(s == fileName)
                    {
                        cbDatabase.SelectedIndex = i;
                        return;
                    }
                }
                if(!File.Exists(fileName)) return;
                string fileNameDisplay = GetSubStringWithTail(fileName, 60) ?? fileName;
                int index = cbDatabase.Items.Add(new Scalar2(fileName, fileNameDisplay));
                cbDatabase.SelectedIndex = index;
            }
            finally
            {
                allowSelectedIndexChangedEvent = true;
            }
        }

        /// <summary>
        /// 返回字符串s的前maxlen个字符。 如果字符串不到长度，则返回null。 maxlen 为0表示不限长，也返回null
        /// 如果超出长度，则返回前 maxlen/2，加上尾部 maxlen/2
        /// </summary>
        static string GetSubStringWithTail(string s, int maxlen)
        {
            if(maxlen <= 0) return null;
            if(s.Length <= 5) return null;   // 少于两个字符不截取

            StringBuilder sbHead = new StringBuilder(), sbTail = new StringBuilder();
            int index1 = 0, index2 = s.Length - 1, len1 = 0, len2 = 0;
            while(index2 >= index1)
            {
                if(len1 + len2 >= maxlen)
                {   // 已经到达最大长度
                    sbHead.Append("…");
                    for(int i = sbTail.Length - 1; i >= 0; i--)
                        sbHead.Append(sbTail[i]);
                    return sbHead.ToString();
                }
                if(len1 + len1 / 2 <= len2)   // 尾部显示长一点
                {   // 在头上加
                    char c = s[index1];
                    sbHead.Append(c);
                    index1++;
                    len1++;
                    if(c > 256) len1++; // 中文算两个字符
                }
                else
                {   // 在尾上加
                    char c = s[index2];
                    sbTail.Append(c);
                    index2--;
                    len2++;
                    if(c > 256) len2++; // 中文算两个字符
                }
            }
            return null;   // 可以全部显示
        }

        void LoginForm_Load(object sender, EventArgs e)
        {
            //label1.Text = GlobalData.ToolName;
            globalData.userID = us.lastUserId;


            if(us.AutoOpenLastDatabase)
            {
                string s = us.LastDatabaseName;

                // 打开上一次的数据库时不用报错
                DBAccess.DBAErrorReportHandler er = DBAccess.globalErrorHandler;
                DBAccess.globalErrorHandler = null;

                foreach(string fileName in us.lastDatabaseList)
                    AddToDropdownList(fileName);

                if(string.IsNullOrEmpty(s) == false)   // 如果允许打开最后的数据库且数据库名不为空，则打开之
                    OpenDatabase(s);

                // 恢复以前状态
                DBAccess.globalErrorHandler = er;
            }

            try
            {
                ProductFace.Face_Close(); // 初始化完毕,关闭封面程序
            }
            catch
            {

            }
        }

        /// <summary>
        /// 打开指定数据库，获取所有的用户名
        /// </summary>
        bool OpenDatabase(string filename)
        {
            if(string.IsNullOrEmpty(filename)) return false;
            if(globalData.CloseProjectDatabase() == false)  // 先关闭之前的数据库
                return false;

            cbProject.Items.Clear();
            cbUserName.Items.Clear();

            bool ret = globalData.OpenProjectDatabase(filename);
            if(ret == false) return false;
            AddToDropdownList(filename);
            us.LastDatabaseName = filename;

            GetAllProject();

            // 数据库是否只读
            FileInfo fi = new FileInfo(filename);
            if((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                MessageBox.Show("正在打开的数据库文件具有只读属性，所有修改无法保存。\n\n请去除掉文件的只读属性后再重新打开此数据库！\n\n数据库文件名： " + filename);

            return true;
        }

        void GetAllProject()
        {
            cbUserName.Items.Clear();
            InitProjectList(dbProject, cbProject, us.lastProjectID);
        }

        public static void InitProjectList(DBAccess dba, ComboBox cbProject, object lastid)
        {
            cbProject.Items.Clear();
            DataTable dt = DBLayer1.GetProjectList(dba, "项目名称");

            foreach(DataRow dr in dt.Rows)
            {
                string prjName = dr["项目名称"].ToString();
                prjName = GetSubStringWithTail(prjName, 60) ?? prjName;
                Scalar2 sc = new Scalar2(dr["ID"], prjName);
                cbProject.Items.Add(sc);

                //自动选中上一次登录的项目
                if(Equals(dr["ID"], lastid))
                    cbProject.SelectedItem = sc;
            }
            if(cbProject.Items.Count > 0 && cbProject.SelectedIndex == -1)
                cbProject.SelectedIndex = 0;
        }

        private void cbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbUserName.Items.Clear();
            Scalar2 sc = cbProject.SelectedItem as Scalar2;
            System.Diagnostics.Debug.Assert(sc != null);
            globalData.projectID = sc.Key;  // 选中的项目名
            GetAllUser();
        }

        void GetAllUser()
        {
            // 获取项目策划用户名
            DataTable dt = DBLayer1.GetUserList(dbProject, globalData.projectID, true);
            if(dt == null) return;  // 获取用户名失败

            Common.Func<object, int?> finduser = uid =>
            {
                for(int i = 0; i < cbUserName.Items.Count; i++)
                {
                    UserInfo ui = cbUserName.Items[i] as UserInfo;
                    if(Equals(ui.userID, uid))
                        return i;
                }
                return null;
            };

            var items = cbUserName.Items;
            foreach(DataRow dr in dt.Rows)
            {
                object uid = dr["ID"];
                object role = IsNull(dr["用户角色"]) ? dr["用户类型"] : dr["用户角色"];

                int? index = finduser(uid);
                if(index == null)
                {
                    UserInfo ui = new UserInfo();
                    ui.userID = uid;
                    ui.userName = dr["用户名"].ToString();
                    ui.userType = (UserType)role;
                    ui.password = dr["密码"].ToString();
                    ui.forbidLogin = (bool)dr["禁止登录"];
                    items.Add(ui);

                    //自动选中上一次登录的人员
                    if(Equals(globalData.userID, uid))
                        cbUserName.SelectedItem = ui;
                }
                else
                {   // 同一用户以不同的角色出现，仅列出权限最高的角色
                    UserInfo ui = items[index.Value] as UserInfo;
                    if((int)ui.userType > (int)role)
                    {
                        ui.userType = (UserType)role;
                        items.RemoveAt(index.Value);
                        items.Insert(index.Value, ui);
                    }
                }
            }

            if(cbUserName.Items.Count > 0 && cbUserName.SelectedIndex == -1)
                cbUserName.SelectedIndex = 0;
        }

        void LoginForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            globalData.OnClose();
        }

        /// <summary>
        /// "退出"按钮
        /// </summary>
        private void btQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// "登录"按钮
        /// </summary>
        private void btLogin_Click(object sender, EventArgs e)
        {
            // 获取用户信息
            UserInfo ui = cbUserName.SelectedItem as UserInfo;
            if(ui == null) return;

            // 检查加密狗信息
            if(CheckDog.Inst.dogType == DogType.noDog)
            {
                //MessageBox.Show(CheckDog.Inst.errorMessage);
                //return;
            }

            if(ui.forbidLogin)
            {
                MessageBox.Show("该用户已经被禁用。如果想使用该用户登录，请与项目负责人联系。");
                return;
            }

            // 验证密码
            if(UserInfo.CheckPassword(ui.password, tbPassword.Text) == false)
            {
                MessageBox.Show("密码输入错误");
                return;
            }

            globalData.userName = ui.userName;
            globalData.userID = ui.userID;
            globalData.userType = ui.userType;

            // 保存最后登录的用户名
            us.lastUserId = globalData.userID;
            us.lastProjectID = globalData.projectID;

            // 写用户最后登录时间与登录次数
            //string sql = "UPDATE [sys人员] SET 登录次数 = 登录次数+1, 上次登录时间 = now() WHERE 人员ID = ? ";
            //dbProject.ExecuteNoQuery(sql, ui.userID);

            // 写登录日志
            //sql = "insert into [ZSYS人员登录日志] (项目ID, 人员ID, 人员姓名, 登录机器名) VALUES ( ?, ?, ?, ?) ";
            //dbProject.ExecuteNoQuery(sql, projectID, ui.userID, ui.userName, Environment.MachineName);


            // 创建窗口
            this.Hide();

            MainForm mf = new MainForm();
            DialogResult dr = mf.ShowDialog();

            if(dr == DialogResult.Cancel)
                this.Close();
            else
            {
                tbPassword.Text = "";
                this.Show();
                OpenDatabase(null);
            }
        }

        /// <summary>
        /// 关于对话框
        /// </summary>
        public static void miAbout_Click(object sender, EventArgs e)
        {
            AboutBox1 af = new AboutBox1();
            af.ShowDialog();
        }

        public static void miAboutDog_Click(object sender, EventArgs e)
        {
            string s = GlobalData.ToolName + "\n\n" + "当前使用:  \n\n     ";
            s += CheckDog.Inst.GetDogTypeName() + "\n\n";
            MessageBox.Show(s, "关于使用许可证");
        }

        /// <summary>
        /// 创建新项目，并打开之
        /// </summary>
        void miNewProject_Click(object sender, EventArgs e)
        {
            if(dbProject == null)
            {
                MessageBox.Show("没有打开的数据库");
                return;
            }

            // 检查加密狗信息
            if(CheckDog.Inst.dogType == DogType.noDog)
            {
                //MessageBox.Show(CheckDog.Inst.errorMessage);
                //return;
            }

            NewProjectForm f = new NewProjectForm();
            DialogResult dr = f.ShowDialog();
            if(dr == DialogResult.Cancel) return;
            GetAllProject();
        }

        public static void miLicenseServer_Click(object sender, EventArgs e)
        {
           // LicenseServerStateForm f = new LicenseServerStateForm();
            //f.ShowDialog();
        }

        /// <summary>
        /// 项目角色分配
        /// </summary>
        void miProjectRole_Click(object sender, EventArgs e)
        {
            if(dbProject == null)
            {
                MessageBox.Show("没有打开的数据库");
                return;
            }
            ProjectRoleForm f = new ProjectRoleForm();
            f.ShowDialog();
            GetAllProject();
        }

        /// <summary>
        /// 人员管理
        /// </summary>
        void miUserManage_Click(object sender, EventArgs e)
        {
            if(dbProject == null)
            {
                MessageBox.Show("没有打开的数据库");
                return;
            }
            UserManager f = new UserManager();
            f.ShowDialog();
            GetAllProject();
        }

        void miConvertDatabase_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "转换数据库";
            dlg.ValidateNames = true;
            dlg.CheckFileExists = true;
            dlg.Filter = "Access文件(.mdb)|*.mdb";
            dlg.FilterIndex = 0;

            if(dlg.ShowDialog() != DialogResult.OK) return;

            string s = Path.Combine(Application.StartupPath, GlobalData.BaseDirectory + @"Config\例子.mdb");
            DatabaseConvert254 dbconvertor = new DatabaseConvert254(dlg.FileName, s);
            ConvertProcessForm cpf = new ConvertProcessForm(dbconvertor);
            cpf.Show(this);

            if(dbconvertor.StartConvert()) // 数据库转换成功
            {
                cpf.Close();
                MessageBox.Show("转换数据库由" + dbconvertor.dbVersion + "至" + UpgradeDatabase.DBVersion + "成功!!\n生成数据库文件: " + dbconvertor.GetDesFileName(), "操作成功", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                OpenDatabase(dbconvertor.GetDesFileName());  // 打开转换后的数据库
            }
            else // 数据库转换失败
            {
                cpf.Close();
                MessageBox.Show("转换数据库失败!\n运行状态: " + dbconvertor.GetStatusMsg(), "操作失败", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        bool allowSelectedIndexChangedEvent = true;

        void cbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!allowSelectedIndexChangedEvent) return;
            Scalar2 sc = cbDatabase.SelectedItem as Scalar2;
            if(sc == null) return;
            string fileName = sc.Key as string;
            if(GridAssist.IsNull(fileName)) return;
            OpenDatabase(fileName);
        }
    }

    public class UserInfo
    {
        public object userID = null;
        public string userName = null;
        public UserType userType = 0;
        public string password = null;
        public bool forbidLogin = false;

        public override string ToString()
        {
            if(userName == null) return "";
            string format = "{0}     ({1})";
            string s = string.Format(format, userName, FunctionClass.GetEnumDescription(userType));
            return s;
        }

        /// <summary>
        /// 验证用户输入的密码是否正确
        /// </summary>
        /// <param name="uiPs">数据库中保存的密码信息</param>
        /// <param name="ps">用户输入的密码</param>
        /// <returns>正确返回true，失败返回false</returns>
        public static bool CheckPassword(string uiPs, string ps)
        {
            if(string.IsNullOrEmpty(uiPs))
                return true;
            if(uiPs == EncrpPass(ps))
                return true;
            return false;
        }

        public static string EncrpPass(string pass)
        {
            string s = CodeAssist.GetMD5Code(pass);
            s = CodeAssist.EncodeString36(s);
            return s;
        }
    }
}