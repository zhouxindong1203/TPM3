using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Common;
using TPM3.Sys;

namespace TPM3.wx
{
    [TypeNameMap("wx.ProjectBaseInfoForm")]
    public partial class ProjectBaseInfoForm : MyBaseForm
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<ProjectBaseInfoForm>(7);
        FlexGridAssist flexAssist1;

        public ProjectBaseInfoForm()
        {
            InitializeComponent();
            bl1.Add(new BaseInfo("标识版本前缀", tb206));
            bl1.Add(new BaseInfo("测试单位", tb204));
            bl1.Add(new BaseInfo("项目名称", tb201));
            bl1.Add(new BaseInfo("项目标识号", tb203));

            bl2.Add(new BaseInfo("被测软件版本", tb31));
            bl2.Add(new BaseInfo("需求版本", tb32));
            bl2.Add(new BaseInfo("版本名称", tb41));
            bl2.Add(new BaseInfo("版本说明", tb42));

            bl1.Add(new BaseInfo("委托方名称", tb101));
            bl1.Add(new BaseInfo("委托方地址", tb102));
            bl1.Add(new BaseInfo("交办方联系人", tb103));
            bl1.Add(new BaseInfo("交办方联系电话", tb104));
            bl1.Add(new BaseInfo("开发单位", tb105));
            bl1.Add(new BaseInfo("开发方地址", tb106));
            bl1.Add(new BaseInfo("开发方联系人", tb107));
            bl1.Add(new BaseInfo("开发方联系电话", tb108));
            bl1.Add(new BaseInfo("实验室地址", tb109));
            bl1.Add(new BaseInfo("外场测试地址", tb110));

            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            flexAssist1 = new FlexGridAssist(flex1, null, null) { columnList = columnList1, doubleClickEdit = false };
        }

        static ProjectBaseInfoForm()
        {
            columnList1.Add("文档名称", 130, false);
            columnList1.Add("版本号", 60);
            columnList1.Add("文档文件名", 300, "文档文件名(与版本无关)");
            columnList1.Add("文档标识号", 180, "文档标识号(与版本无关)");
            columnList1.Add("日期", 80);
            columnList1.Add("页眉", 80);
        }

        /// <summary>
        /// 版本无关控件列表
        /// </summary>
        List<BaseInfo> bl1 = new List<BaseInfo>(32);

        /// <summary>
        /// 版本相关控件列表
        /// </summary>
        List<BaseInfo> bl2 = new List<BaseInfo>();

        /// <summary>
        /// 当前版本的回归序号，初始为0，以后递增。
        /// </summary>
        int currentVerIndex;

        DataTable dt;
        public override bool OnPageCreate()
        {
            currentVerIndex = DBLayer1.GetVersionIndex(dbProject, currentvid);

            ArrayList al2 = dbProject.GetObjectList("select 密级 from DC密级表 order by ID");
            foreach(string s in al2)
                cb202.Items.Add(s);
            cb202.Text = ProjectInfo.GetProjectString(dbProject, pid, "密级");

            foreach(BaseInfo bi in bl1)   // 与版本无关
                bi.control.Text = ProjectInfo.GetDocString(dbProject, pid, null, bi.DocumentName, bi.ContentTitle);

            foreach(BaseInfo bi in bl2)
                bi.control.Text = ProjectInfo.GetDocString(dbProject, pid, currentvid, bi.DocumentName, bi.ContentTitle);

            dt = new DataTable();
            GridAssist.AddMemoColumn(dt, "文档名称", "数据库名称", "版本号", "文档文件名", "文档标识号", "日期", "页眉");
            if(currentVerIndex == 0)
            {   // 非回归测试
                ProjectStageType pst = DBLayer1.GetProjectType(dbProject, pid);
                if(pst == ProjectStageType.I类)
                {
                    dt.Rows.Add("测试需求规格说明", "需求分析");
                    dt.Rows.Add("测试计划", "测试计划");
                    dt.Rows.Add("测试说明", "测试说明");
                }
                if(pst == ProjectStageType.II类)
                {
                    dt.Rows.Add("测试计划", "测试计划");
                    dt.Rows.Add("测试说明", "测试说明");
                }
                if(pst == ProjectStageType.III类)
                {
                    dt.Rows.Add("测试方案", "测试说明");
                }
                if(pst == ProjectStageType.定型)
                {
                    dt.Rows.Add("定型测评大纲", "测试计划");
                    dt.Rows.Add("测试说明", "测试说明");
                }
                dt.Rows.Add("测试记录", "测试记录");
                dt.Rows.Add("测试报告", "测试总结");
                dt.Rows.Add("问题报告", "问题报告");
            }
            else
            {
                dt.Rows.Add("回归测试方案", "回归测试方案");
                //dt.Rows.Add("回归测试文档", "回归测试文档");
                //dt.Rows.Add("回归测试说明", "回归测试说明");
                dt.Rows.Add("回归问题报告", "回归问题报告");
                dt.Rows.Add("回归测试记录", "回归测试记录");
                dt.Rows.Add("回归测试报告", "回归测试报告");
            }
            foreach(DataRow dr in dt.Rows)
            {
                string dbName = dr["数据库名称"].ToString();
                dr["版本号"] = ProjectInfo.GetDocString(dbProject, pid, currentvid, dbName, "文档版本");
                dr["日期"] = ProjectInfo.GetDocString(dbProject, pid, currentvid, dbName, "日期");
                dr["文档标识号"] = ProjectInfo.GetDocString(dbProject, pid, null, dbName, "文档标识号");   // 版本无关
                dr["文档文件名"] = ProjectInfo.GetDocString(dbProject, pid, null, dbName, "文档文件名");   // 版本无关
                if(GridAssist.IsNull(dr["文档文件名"])) dr["文档文件名"] = "{2}" + dr["文档名称"];  // 缺省值
                dr["页眉"] = ProjectInfo.GetDocString(dbProject, pid, currentvid, dbName, "文档页眉");
            }
            flexAssist1.DataSource = dt;
            flexAssist1.OnPageCreate();
            FlexGridAssist.AutoSizeRows(flex1, 4);
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            ProjectInfo.SetProjectString(dbProject, pid, "密级", cb202.Text);

            foreach(BaseInfo bi in bl1)
                ProjectInfo.SetDocString(dbProject, pid, null, bi.DocumentName, bi.ContentTitle, bi.control.Text);

            foreach(BaseInfo bi in bl2)
                ProjectInfo.SetDocString(dbProject, pid, currentvid, bi.DocumentName, bi.ContentTitle, bi.control.Text);

            flexAssist1.OnPageClose();
            foreach(DataRow dr in dt.Rows)
            {
                string dbName = dr["数据库名称"].ToString();
                ProjectInfo.SetDocString(dbProject, pid, currentvid, dbName, "文档版本", dr["版本号"].ToString());
                ProjectInfo.SetDocString(dbProject, pid, currentvid, dbName, "日期", dr["日期"].ToString());
                ProjectInfo.SetDocString(dbProject, pid, null, dbName, "文档标识号", dr["文档标识号"].ToString());   // 版本无关
                ProjectInfo.SetDocString(dbProject, pid, null, dbName, "文档文件名", dr["文档文件名"].ToString());   // 版本无关
                ProjectInfo.SetDocString(dbProject, pid, currentvid, dbName, "文档页眉", dr["页眉"].ToString());
            }
            return true;
        }

        private void comboBox_Validating(object sender, CancelEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if(cb.Text == "") return;
            int index = cb.FindStringExact(cb.Text);
            if(index < 0) //return -1 if nothing was find
            {
                string sql;
                sql = "insert into 密级表 (密级) values (?)";
                dbProject.ExecuteNoQuery(sql, cb.Text);
                cb.Items.Add(cb.Text);
            }
        }

        void VersionSignChanged(object sender, System.EventArgs e)
        {
            string sign = tb203.Text;  // 项目标识号
            string s2 = tb206.Text;  // 版本前缀
            if(currentVerIndex > 0)
                sign += "/" + s2 + currentVerIndex;
            tb205.Text = sign;
            flex1.Invalidate();
        }

        void flex1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if(e.Row < flex1.Rows.Fixed || e.Col < flex1.Cols.Fixed) return;
            DataRow dr = ((DataRowView)flex1.Rows[e.Row].DataSource).Row;

            string name = flex1.Cols[e.Col].Name;
            if(name == "文档标识号" || name == "文档文件名" || name == "页眉")
            {
                try
                {
                    e.Text = string.Format(dr[name].ToString(), tb205.Text, dr["版本号"], tb201.Text);
                }
                catch { }
            }
        }
    }
}