using System;
using System.Data;
using System.Windows.Forms;
using Common;
using Common.Database;
using TPM3.Sys;

namespace TPM3.wx
{
    public partial class AutoAddReffileForm : Form
    {
        public string docname;
        public DataTable dt;
        public object vid;

        public AutoAddReffileForm()
        {
            InitializeComponent();
        }

        void AutoAddReffileForm_Load(object sender, EventArgs e)
        {
            label1.Text = "当前的文档名称：" + docname;
        }

        void btOK_Click(object sender, EventArgs e)
        {
            var global = GlobalData.globalData;
            DBAccess dbProject = global.dbProject;
            projectName = MyProjectInfo.ProjectName(dbProject, global.projectID);
            projectVersionSign = projectSign = MyProjectInfo.ProjectCode(dbProject, global.projectID);

            int currentVerIndex = DBLayer1.GetVersionIndex(dbProject, vid);
            string s2 = ProjectInfo.GetProjectString(dbProject, global.projectID, "标识版本前缀");  // 版本前缀
            if(currentVerIndex > 0)
                projectVersionSign = projectSign + "(" + s2 + currentVerIndex + ")";

            if(radioButton1.Checked)
            {
                for(int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dt.Rows[i];
                    if(dr.RowState != DataRowState.Deleted)
                        dr.Delete();
                }
            }
            if(docname == "测试计划")
            {
                AddRow("{0}•软件评测项目管理计划", "SETC/{1}/TMP_V1.0", 1);
                AddRow("{0}•软件评测质量保证计划", "SETC/{1}/TQAP_V1.0", 2);
                AddRow("{0}•软件评测配置管理计划", "SETC/{1}/CMP_V1.0", 3);
                AddRow("{0}•软件测试需求规格说明", "SETC/{1}/TA_V1.0", 4);
            }
            if(docname == "测试说明")
            {
                AddRow("{0}•软件测试计划", "SETC/{1}/TP_V1.0", 1);
            }
            if(docname == "测试记录")
            {
                AddRow("{0}•软件测试说明", "SETC/{1}/TS_V1.0", 1);
            }
            if(docname == "测试总结")
            {
                AddRow("{0}•软件测试说明", "SETC/{1}/TS_V1.0", 1);
                AddRow("{0}•软件测试记录", "SETC/{1}/TL_V1.0", 2);
            }
            if(docname == "回归测试报告")
            {
                AddRow("{0}•软件测试说明", "SETC/{1}/TS_V1.0", 1);
                AddRow("{0}•软件测试记录", "SETC/{1}/TL_V1.0", 2);
                AddRow("{0}•软件问题报告", "SETC/{1}/SPR_V1.0", 3);
                AddRow("{0}•软件回归测试方案", "SETC/{2}/TD_V1.0", 4);
                AddRow("{0}•软件回归测试记录", "SETC/{2}/TL_V1.0", 5);
            }
            GridAssist.SetDataTableIndex(dt, null, "序号");
            this.Close();
        }

        private string projectName, projectSign, projectVersionSign;
        void AddRow(string name, string sign, int index)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = FunctionClass.NewGuid;
            dr["引用文件标题"] = string.Format(name, projectName, projectSign, projectVersionSign);
            dr["引用文件文档号"] = string.Format(sign, projectName, projectSign, projectVersionSign);
            if(radioButton3.Checked)
                dt.Rows.InsertAt(dr, index - 1);
            else
                dt.Rows.Add(dr);
        }

        void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
