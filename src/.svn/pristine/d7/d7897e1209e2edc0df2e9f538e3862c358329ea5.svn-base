using System;
using System.Data;
using System.Windows.Forms;
using Common.Database;
using TPM3.Sys;
using Common;
using C1.Win.C1FlexGrid;
using Z1.tpm.DB;
using ProjectInfo = Common.ProjectInfo;

namespace TPM3.wx
{
    /// <summary>
    /// 纠错性更动说明
    /// </summary>
    public partial class RegressModifyControl2 : UserControl
    {
        public static ColumnPropList columnList1 = FlexGridAssist.GetColumnPropList<RegressModifyControl2>(3);
        static string sqlTable = "select * from [HG软件更动表] where 测试版本 = ? and 更动类型 = ? order by 序号";
        FlexGridAssist flexAssist;
        DataTable dtTable;
        public string modifyType;

        public RegressModifyControl2()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithFocus(flex1);
            flex1.AllowDragging = AllowDraggingEnum.Columns;
            flex1.Styles.Normal.WordWrap = true;

            flexAssist = new FlexGridAssist(flex1, "ID", "序号") { doubleClickEdit = true, deleteClear = true, columnList = columnList1 };
        }

        static RegressModifyControl2()
        {
            columnList1.Add("序号", 40);
            columnList1.Add("问题单标识", 100, false, "问题标识");
            columnList1.Add("问题单名称", 100, false, "问题名称");
            columnList1.Add("是否更动", 65);
            columnList1.Add("更动标识", 150);
            columnList1.Add("更动说明", 260);
            columnList1.Add("未更动说明", 200);
        }

        public bool OnPageCreate()
        {
            dtTable = dbProject.ExecuteDataTable(sqlTable, currentvid, modifyType);
            dtTable.Columns["项目ID"].DefaultValue = pid;
            dtTable.Columns["测试版本"].DefaultValue = currentvid;
            dtTable.Columns["是否更动"].DefaultValue = true;
            dtTable.Columns["更动类型"].DefaultValue = modifyType;

            cbImport.Checked = GetImportLastVersionFallString() != false.ToString();
            cbImport_CheckedChanged(null, null);
            InitOK = true;
            return true;
        }

        string GetImportLastVersionFallString()
        {
            // 以前误将此属性保存为项目级属性。为保持兼容，先从版本级属性取值，如果有值则为改正后的格式，直接返回。
            // 否则从项目级属性取修改前保存的值
            string s1 = ProjectInfo.GetDocString(dbProject, pid, currentvid, null, "是否导入上一版本问题");
            if(!string.IsNullOrEmpty(s1)) return s1;
            return ProjectInfo.GetProjectString(dbProject, pid, "是否导入上一版本问题");
        }

        public bool OnPageClose()
        {
            flexAssist.OnPageClose();
            dbProject.UpdateDatabase(dtTable, sqlTable);
            ProjectInfo.SetDocString(dbProject, pid, currentvid, null, "是否导入上一版本问题", cbImport.Checked.ToString());
            return true;
        }

        bool InitOK = false;
        void cbImport_CheckedChanged(object sender, EventArgs e)
        {
            if(cbImport.Checked)
            {
                const string sqlFallList = "SELECT * FROM CA问题报告单 WHERE 测试版本 = ? order by 所属被测对象ID, 同标识序号 ";
                object previd = DBLayer1.GetPreVersion(dbProject, currentvid);
                DataTable dtFall = dbProject.ExecuteDataTable(sqlFallList, previd);
                string splitter = CommonDB.GetPblSpl(dbProject, (string)pid, (string)previd);

                // 依次加入所有的问题单
                int index = 1000000;
                foreach(DataRow drFall in dtFall.Rows)
                {
                    object fid = drFall["ID"];
                    DataRow drModify = GridAssist.GetDataRow(dtTable, "问题单ID", fid);
                    if(drModify != null) continue; // 已经被加入更动表
                    drModify = dtTable.NewRow();
                    drModify["ID"] = FunctionClass.NewGuid;
                    drModify["序号"] = index++;
                    drModify["问题单ID"] = fid;
                    drModify["问题单名称"] = drFall["名称"];
                    drModify["问题单标识"] = CommonDB.GenPblSignForStep(dbProject, splitter, fid.ToString());
                    drModify["相关测试依据"] = GetRelateRequire(dbProject, previd, fid);
                    dtTable.Rows.Add(drModify);
                }
                GridAssist.SetDataTableIndex(dtTable, "序号");
            }
            else
            {
                if(InitOK)
                {
                    DialogResult dr2 = MessageBox.Show("确认取消导入上一版本的问题吗？\n\n取消后，原来导入的问题及输入的影响域分析将丢失！！！", "确认", MessageBoxButtons.YesNo);
                    if(dr2 == DialogResult.No)
                    {
                        cbImport.Checked = true;  // 恢复选中状态
                        return;
                    }
                }

                for(int i = dtTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtTable.Rows[i];
                    if(dr.RowState == DataRowState.Deleted) continue;
                    dr.Delete();
                }
            }
            flexAssist.DataSource = dtTable;
            flexAssist.OnPageCreate();

            FlexGridAssist.AutoSizeRows(flex1, 2);
        }

        static string GetRelateRequire(DBAccess dba, object previd, object fallid)
        {
            string sql = @"SELECT s1.测试用例ID, i2.追踪关系, i2.测试项ID
                FROM (CA测试用例与测试项关系表 AS ci INNER JOIN (CA测试用例实测表 AS c2 
                INNER JOIN ((CA测试过程实体表 AS s1 INNER JOIN CA测试过程实测表 AS s2 ON s1.ID = s2.过程ID) 
                INNER JOIN CA测试用例实体表 AS c1 ON s1.测试用例ID = c1.ID) ON c2.测试用例ID = c1.ID) ON ci.测试用例ID = c2.ID) 
                INNER JOIN CA测试项实测表 AS i2 ON ci.测试项ID = i2.ID
                WHERE s2.测试版本 = ? AND s2.问题报告单ID = ? AND c2.测试版本 = ?";

            KeyList kl = new KeyList();
            DataTable dtRequire = dba.ExecuteDataTable(sql, previd, fallid, previd);
            foreach(DataRow dr in dtRequire.Rows)
            {
                kl.AddKeyList(dr["测试项ID"]);
            }
            return kl.ToString();
        }

        public static DBAccess dbProject
        {
            get { return globalData.dbProject; }
        }

        public static GlobalData globalData
        {
            get { return GlobalData.globalData; }
        }

        public static object pid
        {
            get { return globalData.projectID; }
        }

        public static object currentvid
        {
            get { return globalData.currentvid; }
        }
    }
}