using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using Common;
using Common.Database;
using TPM3.wx;

namespace TPM3.Sys
{
    /// <summary>
    /// 项目管理页面，包括添加/删除项目，项目用户管理
    /// </summary>
    public partial class ProjectRoleForm : Form
    {
        public ProjectRoleForm()
        {
            InitializeComponent();
        }

        public static DBAccess dbProject
        {
            get { return GlobalData.globalData.dbProject; }
        }

        void DBManageForm_Load(object sender, EventArgs e)
        {
            // 加载人员列表
            DataTable dt = DBLayer1.GetUserList(dbProject);
            foreach(DataRow dr in dt.Rows)
            {
                if(Equals(dr["用户类型"], 0)) continue;
                Scalar2 sc = new Scalar2(dr["ID"], dr["用户名"]);
                checkedListBox1.Items.Add(sc);
                checkedListBox2.Items.Add(sc);
            }

            GetAllProject();
        }

        /// <summary>
        /// 添加项目列表
        /// </summary>
        void GetAllProject()
        {
            cbProject.Items.Clear();
            string sql = "select ID from SYS测试项目表 order by 序号";
            ArrayList al = dbProject.GetObjectList(sql);
            if(al == null) return;
            foreach(object id in al)
            {
                string name = ProjectInfo.GetProjectString(dbProject, id, "项目名称");
                Scalar2 sc = new Scalar2(id, name);
                cbProject.Items.Add(sc);

                //自动选中上一次登录的项目
                if(Equals(id, UserSetting.Default.lastProjectID))
                    cbProject.SelectedItem = sc;
            }
            if(cbProject.Items.Count > 0 && cbProject.SelectedIndex == -1)
                cbProject.SelectedIndex = 0;
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Scalar2 sc = cbProject.SelectedItem as Scalar2;
            DataTable dt = DBLayer1.GetUserList(dbProject, sc.Key, false);
            InitCheckListBox(checkedListBox1, UserType.PM, dt);
            InitCheckListBox(checkedListBox2, UserType.Member, dt);
        }

        static void InitCheckListBox(CheckedListBox clb, UserType ut, DataTable dt)
        {
            for(int i = 0; i < clb.Items.Count; i++)
            {
                Scalar2 sc = clb.Items[i] as Scalar2;
                bool isCheck = GetUserRow(dt, sc.Key, ut) != null;
                clb.SetItemChecked(i, isCheck);
            }
        }

        static void SaveCheckListBox(CheckedListBox clb, UserType ut, object pid, DataTable dt)
        {
            for(int i = 0; i < clb.Items.Count; i++)
            {
                Scalar2 sc = clb.Items[i] as Scalar2;
                DataRow dr = GetUserRow(dt, sc.Key, ut);
                bool isCheck = clb.GetItemChecked(i);
                if(dr == null && isCheck)
                {   // 新建记录
                    dr = dt.NewRow();
                    dr["ID"] = FunctionClass.NewGuid;
                    dr["项目ID"] = pid;
                    dr["用户ID"] = sc.Key;
                    dr["用户角色"] = (int)ut;
                    dt.Rows.Add(dr);
                }
                else if(dr != null && !isCheck)
                    dr.Delete();
            }
        }

        static DataRow GetUserRow(DataTable dt, object uid, UserType ut)
        {
            foreach(DataRow dr in dt.Rows)
            {
                if(dr.RowState == DataRowState.Deleted) continue;
                if(Equals(dr["用户ID"], uid) && (int)ut == (int)dr["用户角色"])
                    return dr;
            }
            return null;
        }

        /// <summary>
        /// 保存当前的所有人员信息
        /// </summary>
        void SaveCurrentProject()
        {
            Scalar2 sc = cbProject.SelectedItem as Scalar2;
            if(sc == null) return;
            object pid = sc.Key;

            string sql = "select * from SYS项目用户表 where 项目ID = ?";
            DataTable dt = dbProject.ExecuteDataTable(sql, pid);
            SaveCheckListBox(checkedListBox1, UserType.PM, pid, dt);
            SaveCheckListBox(checkedListBox2, UserType.Member, pid, dt);
            dbProject.UpdateDatabase(dt, sql);
        }

        void btDelete_Click(object sender, EventArgs e)
        {
            Scalar2 sc = cbProject.SelectedItem as Scalar2;
            if(sc == null) return;

            var ret = MessageBox.Show("确认要从数据库中删除选中的项目吗？", "确认", MessageBoxButtons.OKCancel);
            if(ret != DialogResult.OK) return;
            DBLayer1.DeleteProject(dbProject, sc.Key);
            GetAllProject();
        }

        void btSave_Click(object sender, EventArgs e)
        {
            SaveCurrentProject();
        }

        void btQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btClear_Click(object sender, EventArgs e)
        {
            MyProjectBackup pb = new MyProjectBackup(dbProject, null);
            int i = pb.ClearDatabase();
            MessageBox.Show("清理数据库成功，共删除" + i + "条数据");
        }
    }
}
