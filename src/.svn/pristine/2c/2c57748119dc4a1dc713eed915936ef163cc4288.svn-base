using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common.Database;
using Common;
using TPM3.wx;
using TPM3.Sys;
namespace TPM3.lt
{
    public partial class NoneTestPlbName : Form
    {
        DBAccess dbProject;
        object currentvid, pid;
        object ID;
        public NoneTestPlbName(DBAccess dbProject,
             String softwareChangeID, object currentvid, object pid)
        {
            this.dbProject = dbProject;
            this.ID = softwareChangeID;
            this.currentvid = currentvid;
            this.pid = pid;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (NoneTestPlbName1.Text.ToString() == "")
            {
                MessageBox.Show("非测试问题名称不能为空！");
                return;
            }
            string sqlRegressionTest1 = "SELECT * from HG回归测试问题表 where 软件问题 = ? and 更动项ID = ? and 软件问题类型 = ? and 测试版本 = ? and 项目ID = ? ";
            DataTable dt2 = dbProject.ExecuteDataTable(sqlRegressionTest1, NoneTestPlbName1, ID, "非测试问题", currentvid, pid);
            if (dt2.Rows.Count > 0)
            {
                MessageBox.Show("该名称已在该更动项中存在！");
                return;
            }
            dbProject.ExecuteNoQuery("insert into HG回归测试问题表(ID, 项目ID, 软件问题, 软件问题类型, 测试版本, 更动项ID) values(?, ?, ?, ?, ?, ?)",
                FunctionClass.NewGuid, pid, NoneTestPlbName1.Text, "非测试问题", currentvid, ID);
            Close();
        }
    }
}
