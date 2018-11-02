using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml;
using Common;
using TPM3.Sys;


using System.Diagnostics;

namespace TPM3.chq
{       
    public partial class SelectVerForm : Form
    {
       public ArrayList TestVerList = null;
       public ArrayList SelectItemList = null;

        public SelectVerForm()
        {
            InitializeComponent();

            TestVerList = GetTestVerIDList();

            for (int i = 0; i <= TestVerList.Count - 1; i++)
            {
                string sqlstate = "SELECT 文本内容 FROM SYS文档内容表 WHERE 内容标题=? and 测试版本=? and 项目ID=? ";

                DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, "版本名称", TestVerList[i].ToString(), GlobalData.globalData.projectID.ToString());

                if (dt != null && dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string VerStr= dr["文本内容"].ToString();
                        ListBox.Items.Add(VerStr);

                    }

                }

            }
           
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public ArrayList GetTestVerIDList()
        {

            ArrayList TestVerList = new ArrayList();

            string sqlstate = "SELECT SYS测试版本表.ID, SYS测试版本表.项目ID, SYS测试版本表.序号 FROM SYS测试版本表 WHERE SYS测试版本表.项目ID=? ORDER BY SYS测试版本表.序号;";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, GlobalData.globalData.projectID.ToString());

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string TestVerID = dr["ID"].ToString();
                    TestVerList.Add(TestVerID);

                }
            }

            return TestVerList;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择测试报告中要输出的测试版本");
                return;

            }
            SelectItemList = new ArrayList();

            bool Flag = false;
           
            for (int i = 0; i <= TestVerList.Count - 1; i++)
            {
                Flag = false;
                for (int j = 0; j <= ListBox.SelectedItems.Count - 1;j++ )
                {
                    if (ListBox.Items[i].ToString() == ListBox.SelectedItems[j].ToString())
                    {
                        Flag = true;
                        break;
                    }              

                }
                if (Flag == true)
                {
                    SelectItemList.Add(TestVerList[i].ToString());
                }


            }

            SelectVerForm.ActiveForm.Close();

        }
    }
}
