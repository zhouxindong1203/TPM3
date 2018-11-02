using System.Data;
using Common;
using Common.Database;
using Z1.tpm;

namespace TPM3.wx
{
    public class DBLayer2
    {
        public static DataTable GetHGUntestReason(DBAccess dba, object pid, object vid)
        {
            string sql = @"select * from HG回归测试未测试原因 where 项目ID = ? and 测试版本 = ?";
            return dba.ExecuteDataTable(sql, pid, vid);
        }

        public static DataTable GetRequireList(DBAccess dba, object pid, object vid)
        {
            string sql = "select * from [SYS测试依据表] where 测试版本 = ? order by 序号";
            return dba.ExecuteDataTable(sql, vid);
        }

        /// <summary>
        /// 返回前向的测试项，包括
        /// 1. 本版本中回归的测试项
        /// 2. 本版本未测试但在[回归测试未测试原因]中说明的
        /// </summary>
        public static DataView GetPrevItemList(DBAccess dba, object pid, object vid)
        {
            object previd = DBLayer1.GetPreVersion(dba, vid);
            if(previd == null) return null;   // 没有前向版本

            // 前向版本的所有条目
            var summary2 = new TestResultSummary(pid, previd) { dbProject = dba, dblevel = 3 };
            summary2.OnCreate();

            Inner_GetPrevItemList inner = new Inner_GetPrevItemList();
            inner.OnInit1(dba, pid, vid, previd);
            summary2.DoVisit(inner.GetAllTestItem);
            DataTable dt = inner.dt;
            GridAssist.SetDataTableIndex(dt, "测试项ID", "测试项序号");
            GridAssist.SetDataTableIndex(dt, "测试类型ID", "测试类型序号");
            return new DataView(dt) { Sort = "测试类型序号,测试项序号" };
        }

        /// <summary>
        /// 返回前向的测试用例，包括
        /// 1. 本版本中回归的测试用例
        /// 2. 本版本未测试但在[回归测试未测试原因]中说明的
        /// </summary>
        public static DataView GetPrevCaseList(DBAccess dba, object pid, object vid)
        {
            object previd = DBLayer1.GetPreVersion(dba, vid);
            if(previd == null) return null;   // 没有前向版本

            // 前向版本的所有条目
            var summary2 = new TestResultSummary(pid, previd) { dbProject = dba, dblevel = 4 };
            summary2.OnCreate();

            Inner_GetPrevItemList inner = new Inner_GetPrevItemList();
            inner.OnInit2(dba, pid, vid);
            summary2.DoVisit(inner.GetAllTestCase);
            DataTable dt = inner.dt;
            GridAssist.SetDataTableIndex(dt, "测试用例ID", "测试用例序号");
            GridAssist.SetDataTableIndex(dt, "测试项ID", "测试项序号");
            GridAssist.SetDataTableIndex(dt, "测试类型ID", "测试类型序号");
            return new DataView(dt) { Sort = "测试类型序号,测试项序号,测试用例序号" };
        }

        class Inner_GetPrevItemList
        {
            public DataTable dt;

            /// <summary>
            /// 当前版本的用例/条目表
            /// </summary>
            TestResultSummary summary1;

            DataTable dtHGUntestReason, dtRequire;

            public void OnInit1(DBAccess dba, object pid, object vid, object previd)
            {
                summary1 = new TestResultSummary(pid, vid) { dbProject = dba, dblevel = 3 };
                summary1.OnCreate();
                dtHGUntestReason = GetHGUntestReason(dba, pid, vid);
                dtRequire = GetRequireList(dba, pid, previd);

                dt = new DataTable();
                GridAssist.AddColumn(dt, "测试对象ID", "测试类型ID", "测试类型名称", "测试类型标识", "测试项ID", "测试项标识", "测试项名称");
                GridAssist.AddColumn<int>(dt, "测试类型序号", "测试项序号");
                GridAssist.AddMemoColumn(dt, "说明", "涉及依据");
                GridAssist.AddColumn<bool>(dt, "是否选取");
            }

            public void GetAllTestItem(ItemNodeTree item)
            {
                if(item.nodeType != NodeType.TestItem) return;
                DataRow drNew = dt.NewRow();
                drNew["是否选取"] = true;

                var item2 = summary1.GetSubNode(item.id, NodeType.TestItem);  // 在当前版本中查找条目
                if(item2 == null)
                {   // 当前版本中没有,再看未测试原因
                    DataRow drHG = GridAssist.GetDataRow(dtHGUntestReason, "实体ID", item.id);
                    if(drHG == null) return;
                    drNew["是否选取"] = false;
                    drNew["说明"] = drHG["未测试原因"];
                }

                var itemObject = item.GetLeastParent(NodeType.TestObject);
                drNew["测试对象ID"] = itemObject.id;

                var itemClass = item.GetLeastParent(NodeType.TestType);
                drNew["测试类型ID"] = itemClass.id;
                drNew["测试类型标识"] = itemClass.GetItemSign();
                drNew["测试类型名称"] = itemClass.name;

                drNew["测试项ID"] = item.id;
                drNew["测试项标识"] = item.GetItemSign();
                drNew["测试项名称"] = item.name;

                drNew["涉及依据"] = GridAssist.GetMultiDisplayString(dtRequire, "ID", "测试依据", item.dr["追踪关系"], "\n");
                dt.Rows.Add(drNew);
            }

            public void OnInit2(DBAccess dba, object pid, object vid)
            {
                summary1 = new TestResultSummary(pid, vid) { dbProject = dba, dblevel = 4 };
                summary1.OnCreate();
                dtHGUntestReason = GetHGUntestReason(dba, pid, vid);

                dt = new DataTable();
                GridAssist.AddColumn(dt, "测试对象ID", "测试类型ID", "测试类型名称", "测试类型标识", "测试项ID", "测试项标识", "测试项名称", "测试用例ID", "测试用例标识", "测试用例名称");
                GridAssist.AddColumn<int>(dt, "测试类型序号", "测试项序号", "测试用例序号");
                GridAssist.AddMemoColumn(dt, "说明");
                GridAssist.AddColumn<bool>(dt, "是否选取");
            }

            public void GetAllTestCase(ItemNodeTree item)
            {
                if(item.nodeType != NodeType.TestCase) return;
                DataRow drNew = dt.NewRow();
                drNew["是否选取"] = true;

                var item2 = summary1.GetSubNode(item.id, NodeType.TestCase);  // 在当前版本中查找用例
                if(item2 == null)
                {   // 当前版本中没有,再看未测试原因
                    DataRow drHG = GridAssist.GetDataRow(dtHGUntestReason, "实体ID", item.id);
                    if(drHG == null) return;
                    drNew["是否选取"] = false;
                    drNew["说明"] = drHG["未测试原因"];
                }

                var itemObject = item.GetLeastParent(NodeType.TestObject);
                drNew["测试对象ID"] = itemObject.id;

                var itemClass = item.GetLeastParent(NodeType.TestType);
                drNew["测试类型ID"] = itemClass.id;
                drNew["测试类型标识"] = itemClass.GetItemSign();
                drNew["测试类型名称"] = itemClass.name;

                var itemItem = item.GetLeastParent(NodeType.TestItem);
                drNew["测试项ID"] = itemItem.id;
                drNew["测试项标识"] = itemItem.GetItemSign();
                drNew["测试项名称"] = itemItem.name;

                drNew["测试用例ID"] = item.id;
                drNew["测试用例标识"] = item.GetItemSign();
                drNew["测试用例名称"] = item.name;

                dt.Rows.Add(drNew);
            }
        }
    }
}
