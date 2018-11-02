using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TPM3.Sys;

namespace TPM3.lt
{
    /// <summary>
    /// 项目管理
    /// </summary>
    public partial class ProjectManageForm : MyBaseForm
    {
        string[] sqlList = { "CA被测对象实测表","CA被测对象实体表","CA测试过程实测表","CA测试过程实体表","CA测试类型实测表",  "CA测试类型实体表",
                "CA测试项实测表","CA测试项实体表","CA测试用例实测表","CA测试用例实体表","CA测试用例与测试项关系表", "CA问题报告单",
                "DC测试过程附件表","DC测试资源配置表","DC测试组织与人员表","DC附件实体表","DC计划进度表", "DC密级表",
                "DC术语表","DC文档修改页","DC问题标识","DC问题级别表","DC引用文件表", "HG回归测试未测试原因",
                "SYS测试版本表","SYS测试依据表" };

        public ProjectManageForm()
        {
            InitializeComponent();
            DataTable dt = dbProject.ExecuteDataTable("select * from SYS测试项目表 order by 序号");
            Image myImage = Image.FromFile("adobe1.ico");
            listView1.View = View.LargeIcon;
            listView1.LargeImageList = new ImageList();
            listView1.LargeImageList.Images.Add(myImage);
            foreach(DataRow dr in dt.Rows)
            {
                listView1.Items.Add(dr["ID"].ToString(), 0);
            }
        }


        private void savePathButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog f = new SaveFileDialog();
            f.FileName = tSavePath.Text;
            f.Filter = "项目数据库(*.mdb)|*.mdb|所有文件(*.*)|*.*";
            if(f.ShowDialog() == DialogResult.Cancel) return;
            tSavePath.Text = f.FileName;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            //using (DBAccess dba = DBAccessFactory.FromAccessFile(dtSavePath.Text).CreateInst())
            //{
            //    dba
            //    foreach (string t in sqlList)
            //        dba.ExecuteNoQuery("delete from " + t + " where 项目ID = ? ", pid);
            //}
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
