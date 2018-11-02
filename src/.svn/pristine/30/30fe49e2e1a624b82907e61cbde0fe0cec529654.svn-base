using System;
using System.Data;
using System.Windows.Forms;
using Common.Database;
using TPM3.wx;

namespace TPM3.Sys
{
    public partial class PasswordForm : Form
    {
        object userid;
        bool CheckOldPass;

        public PasswordForm(object userid, bool CheckOldPass)
        {
            InitializeComponent();

            this.userid = userid;
            this.CheckOldPass = CheckOldPass;
        }

        public static DBAccess dbProject
        {
            get { return GlobalData.globalData.dbProject; }
        }

        void PasswordForm_Load(object sender, EventArgs e)
        {
            DataTable dt = DBLayer1.GetUserByID(dbProject, userid);
            if(dt == null || dt.Rows.Count == 0) MessageBox.Show("查找用户失败");
            textBox1.Text = dt.Rows[0]["用户名"].ToString();
            if(!CheckOldPass)
            {
                label2.Visible = tbPass1.Visible = false;
                this.Height -= 42;
            }
        }

        /// <summary>
        /// 保存用户密码
        /// </summary>
        void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Save()
        {
            if(tbPass2.Text != tbPass3.Text)
                throw new Exception("两次输入的新密码必须相同");

            if(CheckOldPass)
            {
                DataTable dt = DBLayer1.GetUserByID(dbProject, userid);
                if(dt == null || dt.Rows.Count == 0) MessageBox.Show("查找用户失败");
                string oldps = dt.Rows[0]["密码"].ToString();
                if(!UserInfo.CheckPassword(oldps, tbPass1.Text))
                    throw new Exception("验证旧密码失败");
            }

            string sql = "update SYS用户表 set 密码 = ? where ID = ?";
            dbProject.ExecuteNoQuery(sql, UserInfo.EncrpPass(tbPass2.Text), userid);
        }

        void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
