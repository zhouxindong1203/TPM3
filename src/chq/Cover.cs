using System;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using Common;
using Aspose.Words;
using MyDog.Dog;
using Common.RichTextBox;
using TPM3.Sys;
using TPM3.wx;
using System.Data.OleDb;

namespace TPM3.chq
{
    public partial class Cover : Form
    {
        public Cover()
        {
            InitializeComponent();
        }
        public string ProjectID = TPM3.Sys.GlobalData.globalData.projectID.ToString();
        public string TestVerID = TPM3.Sys.GlobalData.globalData.currentvid.ToString();
        public string Docname = "";

        string sqlstate = "";
        OleDbDataAdapter adapter = null;
        DataSet ds = null;
       
        private void Cover_Load(object sender, EventArgs e)
        {
            Docname = BatchOutput.CurrentDocName;
            sqlstate = "select * from 文档封面信息表 where 项目ID=" + "'" + ProjectID + "'" + " and 测试版本=" + "'" + TestVerID + "'" + " and 文档名称=" + "'" + Docname + "'";
    
            //编写人供选

            ArrayList ProjectMenList = GetProjectMen();
            SetCombo(ProjectMenList, comboBox1);
      
            //校对人供选

            string Writer="";
            object value = GetValue("编写人");
            if ((value != null) && (value.GetType().Name != "DBNull"))
            {
                Writer =(string)GetValue("编写人");
            }
            ArrayList MenList = GetAssistMen(Writer, ProjectMenList);
            SetCombo(MenList, comboBox2);
          
                   
            //标审人供选

            ArrayList StandardMenList = GetStandardMen(ProjectMenList);
            SetCombo(StandardMenList, comboBox3);
            comboBox3.SelectedItem = StandardMenList[0].ToString();

            //审核人供选

            ArrayList AuditingMenList = GetAuditingMen();
            SetCombo(AuditingMenList, comboBox4);
            comboBox4.SelectedItem = AuditingMenList[0].ToString();

            //批准人供选

            ArrayList ConfirmMenList = GetConfirmMen();
            SetCombo(ConfirmMenList, comboBox5);
            comboBox5.SelectedItem = ConfirmMenList[0].ToString(); 

            if (IfHaveRecord() == false)
            {
                InsertInto();
            }

            adapter = new OleDbDataAdapter(sqlstate, (System.Data.OleDb.OleDbConnection)TPM3.Sys.GlobalData.globalData.dbProject.dbConnection);
            ds = new DataSet();         
            adapter.Fill(ds, "文档封面信息表");

            if (ds.Tables[0].Rows[0]["编写人"]!=null) comboBox1.SelectedItem = ds.Tables[0].Rows[0]["编写人"].ToString();
            if (ds.Tables[0].Rows[0]["校对人"]!=null) comboBox2.SelectedItem = ds.Tables[0].Rows[0]["校对人"].ToString();
            if (ds.Tables[0].Rows[0]["标审人"]!=null) comboBox3.SelectedItem = ds.Tables[0].Rows[0]["标审人"].ToString();
            if (ds.Tables[0].Rows[0]["审核人"]!=null) comboBox4.SelectedItem = ds.Tables[0].Rows[0]["审核人"].ToString();
            if (ds.Tables[0].Rows[0]["批准人"]!=null) comboBox5.SelectedItem = ds.Tables[0].Rows[0]["批准人"].ToString();

            string Datestr1 = ds.Tables[0].Rows[0]["编写日期"].ToString();
            if (Datestr1 != "")
            {
                DateTime Date1 = ChangeDateTime(Datestr1);
                dateTimePicker1.Value = Date1;
            }
            string Datestr2 = ds.Tables[0].Rows[0]["校对日期"].ToString();
            if (Datestr2 != "")
            {
                DateTime Date2 = ChangeDateTime(Datestr2);
                dateTimePicker2.Value = Date2;
            }
            string Datestr3 = ds.Tables[0].Rows[0]["标审日期"].ToString();
            if (Datestr3 != "")
            {
                DateTime Date3 = ChangeDateTime(Datestr3);
                dateTimePicker3.Value = Date3;
            }
            string Datestr4 = ds.Tables[0].Rows[0]["审核日期"].ToString();
            if (Datestr4 != "")
            {
                DateTime Date4 = ChangeDateTime(Datestr4);
                dateTimePicker4.Value = Date4;
            }
            string Datestr5 = ds.Tables[0].Rows[0]["批准日期"].ToString();
            if (Datestr5 != "")
            {
                DateTime Date5 = ChangeDateTime(Datestr5);
                dateTimePicker5.Value = Date5;
            }

            SetTextBox(MenList); //参加人信息               

        }

        public DateTime ChangeDateTime(string value)
        {
            string str = "";
            string year = "";
            string month = "";
            string day = "";

            int pos = value.IndexOf("-");
            if (pos != -1)
            {
                //year = value.Substring(0, value.Length - pos);
                year = value.Substring(0, pos);
                value = value.Substring(pos + 1, value.Length - pos - 1);

                pos = value.IndexOf("-");
                if (pos != -1)
                {
                    //month = value.Substring(0, value.Length - pos - 1);
                    month = value.Substring(0, pos);
                    day = value.Substring(pos + 1, value.Length - pos - 1);
                    str = year + "-" + month + "-" + day;
                }
                else
                {
                    str = year = "-" + value;

                }
            }
            //str = value;

            DateTime Date = DateTime.Parse(str);
  
            return Date;

        }
     
        public ArrayList GetProjectMen()
        {
            ArrayList ProjectMenList = new ArrayList();

            string sqlstate = "SELECT DC测试组织与人员表.项目ID, DC测试组织与人员表.测试版本, DC测试组织与人员表.姓名, DC测试组织与人员表.序号 " +
                              " FROM DC测试组织与人员表 WHERE DC测试组织与人员表.项目ID=? AND DC测试组织与人员表.测试版本=? ORDER BY DC测试组织与人员表.序号;";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TPM3.Sys.GlobalData.globalData.currentvid.ToString());

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string Men = dr["姓名"].ToString();
                    ProjectMenList.Add(Men);
                }
            }

            return ProjectMenList;
        }

        public ArrayList GetAssistMen(string Writer, ArrayList ProjectMenList)//参加人列表、校对人列表
        {
            ArrayList AssistMenList = new ArrayList();
            for (int i = 0; i <= ProjectMenList.Count - 1; i++)
            {
                string ProjectMen = ProjectMenList[i].ToString();
                if (ProjectMen != Writer)
                {
                    AssistMenList.Add(ProjectMen);
                }
            }
            return AssistMenList;

        }      

        public ArrayList GetStandardMen(ArrayList ProjectMenList)//标审人列表

        {
            ArrayList StandardMenList = new ArrayList();
            string[] team = { "张卫祥", "张慧颖" };

            foreach (string str in team)
            {
                bool flag = false;
                for (int i = 0; i <= ProjectMenList.Count - 1; i++)
                {
                    string men = ProjectMenList[i].ToString();
                    if (men == str)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    StandardMenList.Add(str);
                }
            }

            return StandardMenList;

        }

        public ArrayList GetAuditingMen()
        {
            ArrayList AuditingMenList = new ArrayList();

            AuditingMenList.Add("董锐");
            AuditingMenList.Add("李文溯");

            return AuditingMenList;
        }

        public ArrayList GetConfirmMen()
        {
            ArrayList ConfirmMenList = new ArrayList();
            ConfirmMenList.Add("王泗宏");
         

            return ConfirmMenList;
        }

        public string GetWriter()
        {
            string Writer = "";

            string sqlstate = "select 编写人 from 文档封面信息表 where 项目ID=? and 测试版本=? and 文档名称=?";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, TestVerID, Docname);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Writer = dr["编写人"].ToString();
                }
            }

            return Writer;

        }

        public object GetValue(string Filter)
        {
            object value = null;

            string sqlstate = "select * from 文档封面信息表 where 项目ID=? and 测试版本=? and 文档名称=?";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, TestVerID, Docname);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    switch (Filter)
                    {
                        case "编写人":
                            value = dr["编写人"];
                            break;
                        case "编写日期":
                            value = dr["编写日期"];
                            break;
                        case "参加人":
                            value = dr["参加人"];
                            break;
                        case "校对人":
                            value = dr["校对人"];
                            break;
                        case "校对日期":
                            value = dr["校对日期"];
                            break;
                        case "标审人":
                            value = dr["标审人"];
                            break;
                        case "标审日期":
                            value = dr["标审日期"];
                            break;
                        case "审核人":
                            value = dr["审核人"];
                            break;
                        case "审核日期":
                            value = dr["审核日期"];
                            break;
                        case "批准人":
                            value = dr["批准人"];
                            break;
                        case "批准日期":
                            value = dr["批准日期"];
                            break;
                    }
                    
                }
            }

            return value;

        }

        public void SetTextBox(ArrayList DataList)
        {
            string str = "";
            if (DataList.Count > 0)
            {
                for (int i = 0; i < DataList.Count - 1; i++)
                {
                    string men = DataList[i].ToString();
                    str = str + men + "、";

                }
                str = str + DataList[DataList.Count - 1].ToString();
                textBox1.Text = str;

            }
            else
            {
                textBox1.Text = "";
            }
         
        }

        public void SetCombo(ArrayList DataList, ComboBox comboBox)
        {
            comboBox.Items.Clear();

            comboBox.BeginUpdate();
            for (int i = 0; i <= DataList.Count - 1; i++)
            {
                comboBox.Items.Add(DataList[i].ToString());
            }
            comboBox.EndUpdate();
        }

        private void Cover_FormClosing(object sender, FormClosingEventArgs e)
        {
            string value1 = "";
            string value2 = "";
            string value3 = "";
            string value4 = "";
            string value5 = "";

            if (comboBox1.SelectedItem!=null) value1 = comboBox1.SelectedItem.ToString();
            if (comboBox2.SelectedItem != null) value2 = comboBox2.SelectedItem.ToString();
            if (comboBox3.SelectedItem != null) value3 = comboBox3.SelectedItem.ToString();
            if (comboBox4.SelectedItem != null) value4 = comboBox4.SelectedItem.ToString();
            if (comboBox5.SelectedItem != null) value5 = comboBox5.SelectedItem.ToString();
            DateTime Date1 = (DateTime)dateTimePicker1.Value;
            DateTime Date2 = (DateTime)dateTimePicker2.Value;
            DateTime Date3 = (DateTime)dateTimePicker3.Value;
            DateTime Date4 = (DateTime)dateTimePicker4.Value;
            DateTime Date5 = (DateTime)dateTimePicker5.Value;

            string DataStr1 = Date1.Year + "-" + Date1.Month + "-" + Date1.Day;
            string DataStr2 = Date2.Year + "-" + Date2.Month + "-" + Date2.Day;
            string DataStr3 = Date3.Year + "-" + Date3.Month + "-" + Date3.Day;
            string DataStr4 = Date4.Year + "-" + Date4.Month + "-" + Date4.Day;
            string DataStr5 = Date5.Year + "-" + Date5.Month + "-" + Date5.Day;

            string UpdateState = "Update 文档封面信息表 set 编写人= " + "'" + value1 + "'" + ",编写日期= " + "'" + DataStr1 + "'"  + ",参加人=" + "'" + textBox1.Text  + "'" + ",校对人=" + "'" + value2 + "'" + ",校对日期=" + "'" + DataStr2 + "'" + ",标审人=" + "'" + value3 + "'" + ",标审日期=" + "'" + DataStr3 + "'" + ",审核人=" + "'" + value4 + "'" + ",审核日期=" + "'" + DataStr4 + "'" + ",批准人=" + "'" + value5 + "'" + ",批准日期=" + "'" + DataStr5 + "'" + " where 项目ID=" + "'" + ProjectID + "'" + " and 测试版本=" + "'" + TestVerID + "'" + "  and 文档名称=" + "'" + Docname + "'";

            OleDbCommand comm = new OleDbCommand();
            comm.CommandText = UpdateState;
            comm.Connection = (System.Data.OleDb.OleDbConnection)TPM3.Sys.GlobalData.globalData.dbProject.dbConnection;
            comm.ExecuteNonQuery();
       
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList ProjectMenList = GetProjectMen();
            //校对人供选

            string Writer = comboBox1.SelectedItem.ToString();
            ArrayList MenList = GetAssistMen(Writer, ProjectMenList);
            SetCombo(MenList, comboBox2);
            SetTextBox(MenList); //参加人信息  

        }

        private void InsertInto()
        {

            string IDValue = Common.FunctionClass.NewGuid.ToString();

            DateTime Date5 = DateTime.Today;
            string DataStr5 = Date5.Year + "-" + Date5.Month + "-" + Date5.Day;

            DateTime Date4 = Date5.AddDays(-1);
            string DataStr4 = Date4.Year + "-" + Date4.Month + "-" + Date4.Day;

            DateTime Date3 = Date4.AddDays(-1);
            string DataStr3 = Date3.Year + "-" + Date3.Month + "-" + Date3.Day;

            DateTime Date2 = Date3.AddDays(-1);
            string DataStr2 = Date2.Year + "-" + Date2.Month + "-" + Date2.Day;

            DateTime Date1 = Date2.AddDays(-1);
            string DataStr1 = Date1.Year + "-" + Date1.Month + "-" + Date1.Day;

            string sqlstate = "Insert Into 文档封面信息表(ID,项目ID,测试版本,文档名称,编写日期,校对日期,标审日期,审核日期,批准日期) values (" + "'" + IDValue + "'" + "," + "'" + ProjectID + "'" + "," + "'" + TestVerID + "'" + "," + "'" + Docname + "'" + "," + "'" + DataStr1 + "'" + "," + "'" + DataStr2 + "'"+ "," + "'" + DataStr3 + "'" + "," + "'" + DataStr4 + "'" + "," + "'" + DataStr5 + "'" + ")";
            OleDbCommand comm = new OleDbCommand();
            comm.CommandText = sqlstate;
            comm.Connection = (System.Data.OleDb.OleDbConnection)TPM3.Sys.GlobalData.globalData.dbProject.dbConnection;
            comm.ExecuteNonQuery();

        }

        public bool IfHaveRecord()
        {
            string sqlstate = "select * from 文档封面信息表 where 项目ID=? and 测试版本=? and 文档名称=?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, TestVerID, Docname);
            if (dt != null && dt.Rows.Count != 0)
            {
                return true;
            }
            return false;

        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            DateTime Date5 = (DateTime)dateTimePicker5.Value;

            SetTime(Date5);
           
        }

        public void SetTime(DateTime Date5)
        {

            DateTime Date4 = Date5.AddDays(-1);
            DateTime Date3 = Date4.AddDays(-1);
            DateTime Date2 = Date3.AddDays(-1);
            DateTime Date1 = Date2.AddDays(-1);

            dateTimePicker4.Value = Date4;
            dateTimePicker3.Value = Date3;
            dateTimePicker2.Value = Date2;
            dateTimePicker1.Value = Date1;


        }
    }
}
