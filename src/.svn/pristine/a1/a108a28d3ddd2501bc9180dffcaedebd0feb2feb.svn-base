using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace TPM3.chq
{
    public partial class 文档修改页 : Form
    {
        public 文档修改页()
        {
            InitializeComponent();
        }

        string ProjectID = TPM3.Sys.GlobalData.globalData.projectID.ToString();
        string TestVerID = TPM3.Sys.GlobalData.globalData.currentvid.ToString();

        string sqlstate = "";
        OleDbDataAdapter adapter = null;
        OleDbCommandBuilder com =null;
        DataSet ds = null;
        string Docname = "";
   
        private void 文档修改页_Load(object sender, EventArgs e)
        {

            Docname = BatchOutput.CurrentDocName;

            sqlstate = "SELECT DC文档修改页.序号, DC文档修改页.版本号, DC文档修改页.日期, DC文档修改页.所修改章节, DC文档修改页.所修改页, DC文档修改页.备注, DC文档修改页.文档名称, DC文档修改页.项目ID, DC文档修改页.测试版本, DC文档修改页.ID" +
                           " FROM DC文档修改页 WHERE DC文档修改页.项目ID=" + "'" + ProjectID + "'" + " AND DC文档修改页.文档名称=" + "'" + Docname + "'" + " AND DC文档修改页.测试版本=" + "'" + TestVerID + "'" + " ORDER BY DC文档修改页.序号;";

            adapter = new OleDbDataAdapter(sqlstate, (System.Data.OleDb.OleDbConnection)TPM3.Sys.GlobalData.globalData.dbProject.dbConnection);
            com = new OleDbCommandBuilder(adapter);
            ds = new DataSet();

            dataGridView1.Name = "请填写所选择输出文档的修改页信息：";
            adapter.InsertCommand = com.GetInsertCommand();
            adapter.DeleteCommand = com.GetDeleteCommand();
            adapter.UpdateCommand = com.GetUpdateCommand();

            adapter.Fill(ds, "DC文档修改页");
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
                   
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
          
            e.Row.Cells["文档名称"].Value = Docname;
            e.Row.Cells["ID"].Value = Common.FunctionClass.NewGuid;
            e.Row.Cells["项目ID"].Value = ProjectID;
            e.Row.Cells["测试版本"].Value = TestVerID;
            e.Row.Cells["序号"].Value = GetMaxNum() + 1;

        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            adapter.Update(ds, "DC文档修改页");
        }

        private void 文档修改页_FormClosing(object sender, FormClosingEventArgs e)
        {
            adapter.Update(ds, "DC文档修改页");
        }

        public int GetMaxNum()
        {        
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate);
            
            int num = dt.Rows.Count;
            return num;


        }
    }
}
