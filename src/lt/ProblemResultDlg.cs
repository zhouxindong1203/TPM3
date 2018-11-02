using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common;
using C1.Win.C1FlexGrid;
using TPM3.wx;
using TPM3.Sys;

namespace TPM3.lt
{
    [TypeNameMap("lt.ProblemResultDlg")]
    public partial class ProblemResultDlg : MyBaseForm
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<ProblemResultDlg>();
        public static readonly string sqlRegressionTest = "select * from CA问题报告单 where 项目ID = ? and 测试版本 = ?";
        protected FlexGridAssist flexAssist1;
        DataTable dtObject, dt;

        public ProblemResultDlg()
        {

            InitializeComponent();
            object previd = DBLayer1.GetPreVersion(dbProject, currentvid);
            dtObject = dbProject.ExecuteDataTable(sqlRegressionTest, pid, previd);
            if (dtObject == null) return;
            SetClassViewDataTable();
            DataView dv = new DataView(dt);
            flexAssist1 = new FlexGridAssist(flex1, null, null);
            flexAssist1.columnList = columnList1;
            flexAssist1.DataSource = dv;
            flexAssist1.OnPageCreate();        
        }
        public DataTable GetClassViewDataTable()
        {
            return dt;
        }
        private void  SetClassViewDataTable()
        {
            dt = new DataTable("回归测试影响域分析");
            GridAssist.AddColumn<bool>(dt, "选择"); 
            GridAssist.AddColumn(dt, "ID", "问题名称", "报告日期", "报告人", "问题描述", "问题级别", "问题类别");

            foreach (DataRow dr in dtObject.Rows)
            {
                DataColumnCollection dcc1 = dt.Columns;
                dcc1["选择"].DefaultValue = false;
                dcc1["ID"].DefaultValue = dr["ID"];
                dcc1["问题名称"].DefaultValue = dr["问题名称"];
                dcc1["报告日期"].DefaultValue = dr["报告日期"];
                dcc1["报告人"].DefaultValue = dr["报告人"];
                dcc1["问题描述"].DefaultValue = dr["问题描述"];
                dcc1["问题级别"].DefaultValue = dr["问题级别"];
                dcc1["问题类别"].DefaultValue = dr["问题类别"];
                dt.Rows.Add();
            }

        }
        static ProblemResultDlg()
        {
            columnList1.Add("选择", 100);
            columnList1.Add("ID", 100, false);
            columnList1.Add("问题名称", 170, false);
            columnList1.Add("报告日期", 170, false);
            columnList1.Add("报告人", 250, false);
            columnList1.Add("问题描述", 100, false);
            columnList1.Add("问题级别", 100, false);
            columnList1.Add("问题类别", 100, false);
 //           columnList1.AddValidator(new NotNullValidator("更动说明"));
        }
        public void SetSelectTag(DataTable dt1)
        {
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataRow drt in dt1.Rows)
                {
                    if (dr["ID"].ToString() == drt["软件问题"].ToString())
                    {
                        dr["选择"] = true;
                        break;
                    }
                }
            }
        }
        public override bool OnPageCreate()
        {

            return true;
        }
    }
}
