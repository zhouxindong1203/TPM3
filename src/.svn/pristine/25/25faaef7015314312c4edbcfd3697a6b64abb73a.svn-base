using System.Data;
using C1.Win.C1FlexGrid;
using Common;
using Common.Database;
using TPM3.Sys;
using Z1.tpm.DB;

namespace TPM3.wx
{
    /// <summary>
    /// 上一版本的软件问题报告单所影响的需求
    /// </summary>
    [TypeNameMap("wx.RegressFallRequireTraceForm")]
    public partial class RegressFallRequireTraceForm : MyBaseForm
    {
        FlexGridAssist flexAssist;
        public static ColumnPropList columnList1 = FlexGridAssist.GetColumnPropList<RegressFallRequireTraceForm>(5);
        DataTable dtTable;

        public RegressFallRequireTraceForm()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithFocus(flex1);
            flex1.AllowDragging = AllowDraggingEnum.Columns;
            flex1.Styles.Normal.WordWrap = true;
            flex1._colName = "所属被测对象ID";

            flexAssist = new FlexGridAssist(flex1, null, null);
            flexAssist.columnList = columnList1;
            flexAssist.AddOleColumn("影响域分析");

            object previd = DBLayer1.GetPreVersion(dbProject, currentvid);
            var maplist = flexAssist.refrenceColumnMapList;
            var cm = maplist.AddColumnMap("相关测试依据", "", "");
            RequireTreeForm.InitColumnRefMap(cm, previd);
            cm = maplist.AddColumnMap("附加依据", "", "");
            RequireTreeForm.InitColumnRefMap(cm, previd);

            maplist.AddColumnMap("所属被测对象ID", "ID", "被测对象名称");
        }

        static RegressFallRequireTraceForm()
        {
            columnList1.Add("所属被测对象ID", 100, false, "所属被测对象");
            columnList1.Add("问题报告单标识", 100, false);
            columnList1.Add("报告日期", 80, false);
            columnList1.Add("相关测试依据", 230, false, "上一版本相关测试依据");
            columnList1.Add("影响域分析", 200);
            columnList1.Add("附加依据", 230, "上版本附加相关依据");
        }

        const string sqlFallList = "SELECT ID, 报告日期, 所属被测对象ID, 影响域分析, 附加依据 FROM CA问题报告单 WHERE 测试版本=? AND 处理措施=? order by 所属被测对象ID, 同标识序号 ";
        public static DataTable GetFallRequireTraceTable(DBAccess dba, object previd)
        {
            DataTable dtFall = dba.ExecuteDataTable(sqlFallList, previd, 1);
            GridAssist.AddColumn(dtFall, "问题报告单标识", "相关测试依据");
            GridAssist.AddColumn<int>(dtFall, "序号");

            string splitter = CommonDB.GetPblSpl(dba, (string)pid, (string)previd);

            foreach(DataRow dr in dtFall.Rows)
            {
                dr["相关测试依据"] = GetRelateRequire(dba, previd, dr["ID"]);
                dr["问题报告单标识"] = CommonDB.GenPblSignForStep(dba, splitter, dr["ID"].ToString());
            }
            GridAssist.SetDataTableIndex(dtFall, null, "序号");
            dtFall.AcceptChanges();
            return dtFall;
        }

        static string GetRelateRequire(DBAccess dba, object previd, object fallid)
        {
            string sql = @"SELECT s1.测试用例ID, i2.追踪关系
                FROM (CA测试用例与测试项关系表 AS ci INNER JOIN (CA测试用例实测表 AS c2 
                INNER JOIN ((CA测试过程实体表 AS s1 INNER JOIN CA测试过程实测表 AS s2 ON s1.ID = s2.过程ID) 
                INNER JOIN CA测试用例实体表 AS c1 ON s1.测试用例ID = c1.ID) ON c2.测试用例ID = c1.ID) ON ci.测试用例ID = c2.ID) 
                INNER JOIN CA测试项实测表 AS i2 ON ci.测试项ID = i2.ID
                WHERE s2.测试版本 = ? AND s2.问题报告单ID = ? AND c2.测试版本 = ?";

            KeyList kl = new KeyList();
            DataTable dtRequire = dba.ExecuteDataTable(sql, previd, fallid, previd);
            foreach(DataRow dr in dtRequire.Rows)
            {
                kl.AddKeyList(dr["追踪关系"]);
            }
            return kl.ToString();
        }

        public override bool OnPageCreate()
        {
            object previd = DBLayer1.GetPreVersion(dbProject, currentvid);
            string sql = @"SELECT o2.ID, o1.被测对象名称 FROM CA被测对象实体表 AS o1 
                INNER JOIN CA被测对象实测表 AS o2 ON o1.ID = o2.被测对象ID WHERE o2.测试版本 = ?";
            DataTable dtObjectList = dbProject.ExecuteDataTable(sql, previd);
            flexAssist.refrenceColumnMapList["所属被测对象ID"].DataSource = dtObjectList;

            dtTable = GetFallRequireTraceTable(dbProject, previd);
            flexAssist.DataSource = dtTable;
            flexAssist.OnPageCreate();

            flex1.Cols["所属被测对象ID"].AllowMerging = true;

            FlexGridAssist.AutoSizeRows(flex1, 4, 25, 4);
            flex1.Rows[0].Height = 30;
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            flexAssist.OnPageClose();
            return dbProject.UpdateDatabase(dtTable, sqlFallList);
        }
    }
}
