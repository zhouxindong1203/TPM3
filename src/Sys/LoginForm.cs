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
    /// ��¼����
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
        /// ������ݿ�
        /// </summary>
        void btBrowse1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FileDialog fd = new OpenFileDialog();

            fd.Filter = "��Ŀ�ļ�(*.mdb)|*.mdb|�����ļ�(*.*)|*.*";
            fd.Title = "����Ŀ�ļ�";
            if(fd.ShowDialog() != DialogResult.OK) return;

            // �����ݿ�
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
        /// �����ַ���s��ǰmaxlen���ַ��� ����ַ����������ȣ��򷵻�null�� maxlen Ϊ0��ʾ���޳���Ҳ����null
        /// ����������ȣ��򷵻�ǰ maxlen/2������β�� maxlen/2
        /// </summary>
        static string GetSubStringWithTail(string s, int maxlen)
        {
            if(maxlen <= 0) return null;
            if(s.Length <= 5) return null;   // ���������ַ�����ȡ

            StringBuilder sbHead = new StringBuilder(), sbTail = new StringBuilder();
            int index1 = 0, index2 = s.Length - 1, len1 = 0, len2 = 0;
            while(index2 >= index1)
            {
                if(len1 + len2 >= maxlen)
                {   // �Ѿ�������󳤶�
                    sbHead.Append("��");
                    for(int i = sbTail.Length - 1; i >= 0; i--)
                        sbHead.Append(sbTail[i]);
                    return sbHead.ToString();
                }
                if(len1 + len1 / 2 <= len2)   // β����ʾ��һ��
                {   // ��ͷ�ϼ�
                    char c = s[index1];
                    sbHead.Append(c);
                    index1++;
                    len1++;
                    if(c > 256) len1++; // �����������ַ�
                }
                else
                {   // ��β�ϼ�
                    char c = s[index2];
                    sbTail.Append(c);
                    index2--;
                    len2++;
                    if(c > 256) len2++; // �����������ַ�
                }
            }
            return null;   // ����ȫ����ʾ
        }

        void LoginForm_Load(object sender, EventArgs e)
        {
            //label1.Text = GlobalData.ToolName;
            globalData.userID = us.lastUserId;


            if(us.AutoOpenLastDatabase)
            {
                string s = us.LastDatabaseName;

                // ����һ�ε����ݿ�ʱ���ñ���
                DBAccess.DBAErrorReportHandler er = DBAccess.globalErrorHandler;
                DBAccess.globalErrorHandler = null;

                foreach(string fileName in us.lastDatabaseList)
                    AddToDropdownList(fileName);

                if(string.IsNullOrEmpty(s) == false)   // ����������������ݿ������ݿ�����Ϊ�գ����֮
                    OpenDatabase(s);

                // �ָ���ǰ״̬
                DBAccess.globalErrorHandler = er;
            }

            try
            {
                ProductFace.Face_Close(); // ��ʼ�����,�رշ������
            }
            catch
            {

            }
        }

        /// <summary>
        /// ��ָ�����ݿ⣬��ȡ���е��û���
        /// </summary>
        bool OpenDatabase(string filename)
        {
            if(string.IsNullOrEmpty(filename)) return false;
            if(globalData.CloseProjectDatabase() == false)  // �ȹر�֮ǰ�����ݿ�
                return false;

            cbProject.Items.Clear();
            cbUserName.Items.Clear();

            bool ret = globalData.OpenProjectDatabase(filename);
            if(ret == false) return false;
            AddToDropdownList(filename);
            us.LastDatabaseName = filename;

            GetAllProject();

            // ���ݿ��Ƿ�ֻ��
            FileInfo fi = new FileInfo(filename);
            if((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                MessageBox.Show("���ڴ򿪵����ݿ��ļ�����ֻ�����ԣ������޸��޷����档\n\n��ȥ�����ļ���ֻ�����Ժ������´򿪴����ݿ⣡\n\n���ݿ��ļ����� " + filename);

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
            DataTable dt = DBLayer1.GetProjectList(dba, "��Ŀ����");

            foreach(DataRow dr in dt.Rows)
            {
                string prjName = dr["��Ŀ����"].ToString();
                prjName = GetSubStringWithTail(prjName, 60) ?? prjName;
                Scalar2 sc = new Scalar2(dr["ID"], prjName);
                cbProject.Items.Add(sc);

                //�Զ�ѡ����һ�ε�¼����Ŀ
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
            globalData.projectID = sc.Key;  // ѡ�е���Ŀ��
            GetAllUser();
        }

        void GetAllUser()
        {
            // ��ȡ��Ŀ�߻��û���
            DataTable dt = DBLayer1.GetUserList(dbProject, globalData.projectID, true);
            if(dt == null) return;  // ��ȡ�û���ʧ��

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
                object role = IsNull(dr["�û���ɫ"]) ? dr["�û�����"] : dr["�û���ɫ"];

                int? index = finduser(uid);
                if(index == null)
                {
                    UserInfo ui = new UserInfo();
                    ui.userID = uid;
                    ui.userName = dr["�û���"].ToString();
                    ui.userType = (UserType)role;
                    ui.password = dr["����"].ToString();
                    ui.forbidLogin = (bool)dr["��ֹ��¼"];
                    items.Add(ui);

                    //�Զ�ѡ����һ�ε�¼����Ա
                    if(Equals(globalData.userID, uid))
                        cbUserName.SelectedItem = ui;
                }
                else
                {   // ͬһ�û��Բ�ͬ�Ľ�ɫ���֣����г�Ȩ����ߵĽ�ɫ
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
        /// "�˳�"��ť
        /// </summary>
        private void btQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// "��¼"��ť
        /// </summary>
        private void btLogin_Click(object sender, EventArgs e)
        {
            // ��ȡ�û���Ϣ
            UserInfo ui = cbUserName.SelectedItem as UserInfo;
            if(ui == null) return;

            // �����ܹ���Ϣ
            if(CheckDog.Inst.dogType == DogType.noDog)
            {
                //MessageBox.Show(CheckDog.Inst.errorMessage);
                //return;
            }

            if(ui.forbidLogin)
            {
                MessageBox.Show("���û��Ѿ������á������ʹ�ø��û���¼��������Ŀ��������ϵ��");
                return;
            }

            // ��֤����
            if(UserInfo.CheckPassword(ui.password, tbPassword.Text) == false)
            {
                MessageBox.Show("�����������");
                return;
            }

            globalData.userName = ui.userName;
            globalData.userID = ui.userID;
            globalData.userType = ui.userType;

            // ��������¼���û���
            us.lastUserId = globalData.userID;
            us.lastProjectID = globalData.projectID;

            // д�û�����¼ʱ�����¼����
            //string sql = "UPDATE [sys��Ա] SET ��¼���� = ��¼����+1, �ϴε�¼ʱ�� = now() WHERE ��ԱID = ? ";
            //dbProject.ExecuteNoQuery(sql, ui.userID);

            // д��¼��־
            //sql = "insert into [ZSYS��Ա��¼��־] (��ĿID, ��ԱID, ��Ա����, ��¼������) VALUES ( ?, ?, ?, ?) ";
            //dbProject.ExecuteNoQuery(sql, projectID, ui.userID, ui.userName, Environment.MachineName);


            // ��������
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
        /// ���ڶԻ���
        /// </summary>
        public static void miAbout_Click(object sender, EventArgs e)
        {
            AboutBox1 af = new AboutBox1();
            af.ShowDialog();
        }

        public static void miAboutDog_Click(object sender, EventArgs e)
        {
            string s = GlobalData.ToolName + "\n\n" + "��ǰʹ��:  \n\n     ";
            s += CheckDog.Inst.GetDogTypeName() + "\n\n";
            MessageBox.Show(s, "����ʹ������֤");
        }

        /// <summary>
        /// ��������Ŀ������֮
        /// </summary>
        void miNewProject_Click(object sender, EventArgs e)
        {
            if(dbProject == null)
            {
                MessageBox.Show("û�д򿪵����ݿ�");
                return;
            }

            // �����ܹ���Ϣ
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
        /// ��Ŀ��ɫ����
        /// </summary>
        void miProjectRole_Click(object sender, EventArgs e)
        {
            if(dbProject == null)
            {
                MessageBox.Show("û�д򿪵����ݿ�");
                return;
            }
            ProjectRoleForm f = new ProjectRoleForm();
            f.ShowDialog();
            GetAllProject();
        }

        /// <summary>
        /// ��Ա����
        /// </summary>
        void miUserManage_Click(object sender, EventArgs e)
        {
            if(dbProject == null)
            {
                MessageBox.Show("û�д򿪵����ݿ�");
                return;
            }
            UserManager f = new UserManager();
            f.ShowDialog();
            GetAllProject();
        }

        void miConvertDatabase_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "ת�����ݿ�";
            dlg.ValidateNames = true;
            dlg.CheckFileExists = true;
            dlg.Filter = "Access�ļ�(.mdb)|*.mdb";
            dlg.FilterIndex = 0;

            if(dlg.ShowDialog() != DialogResult.OK) return;

            string s = Path.Combine(Application.StartupPath, GlobalData.BaseDirectory + @"Config\����.mdb");
            DatabaseConvert254 dbconvertor = new DatabaseConvert254(dlg.FileName, s);
            ConvertProcessForm cpf = new ConvertProcessForm(dbconvertor);
            cpf.Show(this);

            if(dbconvertor.StartConvert()) // ���ݿ�ת���ɹ�
            {
                cpf.Close();
                MessageBox.Show("ת�����ݿ���" + dbconvertor.dbVersion + "��" + UpgradeDatabase.DBVersion + "�ɹ�!!\n�������ݿ��ļ�: " + dbconvertor.GetDesFileName(), "�����ɹ�", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                OpenDatabase(dbconvertor.GetDesFileName());  // ��ת��������ݿ�
            }
            else // ���ݿ�ת��ʧ��
            {
                cpf.Close();
                MessageBox.Show("ת�����ݿ�ʧ��!\n����״̬: " + dbconvertor.GetStatusMsg(), "����ʧ��", MessageBoxButtons.OK,
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
        /// ��֤�û�����������Ƿ���ȷ
        /// </summary>
        /// <param name="uiPs">���ݿ��б����������Ϣ</param>
        /// <param name="ps">�û����������</param>
        /// <returns>��ȷ����true��ʧ�ܷ���false</returns>
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