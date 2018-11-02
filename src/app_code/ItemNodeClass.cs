using System;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using TPM3.Sys;
using Common;
using Common.Database;
using Z1.tpm;

namespace TPM3.wx
{
    public class ItemNodeTree
    {
        DBAccess _dbProject;
        public DBAccess dbProject
        {
            get
            {
                return _dbProject ?? GlobalData.globalData.dbProject;
            }
            set
            {
                _dbProject = value;
            }
        }

        protected object pid, vid;

        /// <summary>
        /// 子节点列表
        /// </summary>
        public List<ItemNodeTree> childlist = new List<ItemNodeTree>();

        public NodeType nodeType;

        /// <summary>
        /// id: 实体ID， refid: 实测ID
        /// </summary>
        public object id, refid;

        /// <summary>
        /// 所在的数据行
        /// </summary>
        public DataRow dr;

        public ItemNodeTree parent;

        public TestResultSummary summary;

        /// <summary>
        /// 简写码与名称
        /// </summary>
        public string code, name;
        public int index;

        /// <summary>
        /// 对用例有效，是否快捷方式
        /// </summary>
        public bool IsShortCut = true;

        /// <summary>
        /// id是实体ID或者实测ID
        /// </summary>
        public ItemNodeTree this[object _id]
        {
            get
            {
                if(_id == null) return this;
                return childlist.Find(node => _id.Equals(node.id) || _id.Equals(node.refid));
            }
        }

        public ItemNodeTree GetSubNode(object _id, NodeType _nodeType)
        {
            if(Equals(this.id, _id) && this.nodeType == _nodeType) return this;
            foreach(ItemNodeTree node in childlist)
            {
                ItemNodeTree ret = node.GetSubNode(_id, _nodeType);
                if(ret != null) return ret;
            }
            return null;
        }

        /// <summary>
        /// 返回指定条目下的指定ID的用例。
        /// </summary>
        /// <param name="itemid">条目实体ID，null表示非快捷方式用例</param>
        /// <param name="caseid">用例实体ID</param>
        public ItemNodeTree GetTestCaseEntity(object itemid, object caseid)
        {
            if(Equals(this.id, caseid) && nodeType == NodeType.TestCase)
            {
                if(itemid == null && !this.IsShortCut) return this;
                if(itemid != null && Equals(this.parent.id, itemid)) return this;
            }
            foreach(ItemNodeTree node in childlist)
            {
                ItemNodeTree ret = node.GetTestCaseEntity(itemid, caseid);
                if(ret != null) return ret;
            }
            return null;
        }

        /// <summary>
        /// 获取本用例对应的非快捷方式用例对象
        /// 如果本用例就是非快捷方式，则返回自身。
        /// 如果不是测试用例，则返回null
        /// </summary>
        public ItemNodeTree GetTestCaseEntity()
        {
            if(nodeType != NodeType.TestCase) return null;
            if(!this.IsShortCut) return this;
            return this.summary.GetTestCaseEntity(null, this.id);
        }

        public ItemNodeTree AddChild(NodeType _nodeType, object _id, DataRow dr, object code, object name, object index)
        {
            ItemNodeTree item = new ItemNodeTree();
            item.nodeType = _nodeType;
            item.id = _id;
            item.summary = summary;
            item.dr = dr;
            item.parent = this;
            item.code = code.ToString();
            item.name = name.ToString();
            item.index = childlist.Count + 1;  // (int)index; 解决用例章节号的问题(在同级条目之后)
            item.refid = dr["ID"];     // 实测ID

            item.pid = summary.pid;
            item.vid = summary.vid;

            childlist.Add(item);
            item.OnCreate2();
            return item;
        }

        public void DoVisit(VoidFunc<ItemNodeTree> VisitEvent)
        {
            VisitEvent(this);
            foreach(ItemNodeTree item in childlist)
                item.DoVisit(VisitEvent);
        }

        public void OnCreate2()
        {
            if(nodeType == NodeType.TestObject)
                OnCreateTypeList();
            else if(nodeType == NodeType.TestType)
            {
                OnCreateTypeList();
                OnCreateItemList();
            }
            else if(nodeType == NodeType.TestItem)
            {
                OnCreateItemList();
                OnCreateCaseList();
            }
        }

        public void OnCreateTypeList()
        {
            if(summary.dtClassRef == null) return;
            foreach(DataRow dr in summary.dtClassRef.Rows)
            {
                if(nodeType == NodeType.TestObject)
                {
                    if(!Equals(id, dr["所属被测对象ID"])) continue;
                }
                else if(nodeType == NodeType.TestType)
                {
                    if(!Equals(id, dr["父测试类型ID"])) continue;
                }
                else
                    throw new Exception("内部错误");

                AddChild(NodeType.TestType, dr["测试类型ID"], dr, dr["简写码"], dr["测试类型名称"], dr["序号"]);
            }
        }

        public void OnCreateItemList()
        {
            if(summary.dtItemRef == null) return;
            foreach(DataRow dr in summary.dtItemRef.Rows)
            {
                if(nodeType == NodeType.TestType)
                {
                    if(!Equals(id, dr["所属测试类型ID"])) continue;
                }
                else if(nodeType == NodeType.TestItem)
                {
                    if(!Equals(id, dr["父节点ID"])) continue;
                }
                else
                    throw new Exception("内部错误");

                AddChild(NodeType.TestItem, dr["测试项ID"], dr, dr["简写码"], dr["测试项名称"], dr["序号"]);
            }
        }

        public void OnCreateCaseList()
        {
            Debug.Assert(nodeType == NodeType.TestItem);  // 仅有测试项下可以挂用例
            if(summary.dtCaseItem == null) return;

            // 用例序号。表格中的序号可能不准
            int index = 1;
            foreach(DataRow dr in summary.dtCaseItem.Rows)
            {
                if(!Equals(refid, dr["测试项ID"])) continue;     // 此处引用到实测ID

                // 查找实体用例
                DataRow drCase = summary.dtCaseMap.GetRow(dr["测试用例ID"]);
                if(drCase != null)
                {
                    ItemNodeTree item = AddChild(NodeType.TestCase, drCase["测试用例ID"], drCase, (index++).ToString(), drCase["测试用例名称"], dr["序号"]);
                    item.IsShortCut = !(bool)dr["直接所属标志"];
                }
            }
        }

        public string GetItemSign()
        {
            StringBuilder sb = new StringBuilder(64);
            ItemNodeTree item = this;
            while(item != null)
            {
                if(!string.IsNullOrEmpty(item.code))
                {
                    if(sb.Length > 0) sb.Insert(0, "_");
                    sb.Insert(0, item.code);
                }
                item = item.parent;
            }
            //sb.Insert(0, DocContent.GetProjectContent("项目标识号"));
            return sb.ToString();
        }

        /// <summary>
        /// 获取章节号
        /// </summary>
        /// <param name="doctype">0～3: 测试需求分析，测试计划，测试说明，测试记录</param>
        public string GetItemChapter(int doctype)
        {
            // 如果不生成测试用例的章节，则使用测试条目的章节号
            if(this.nodeType == NodeType.TestCase && !GlobalConst.GenTestCaseTitle)
                return parent.GetItemChapter(doctype);

            bool upgradeChapter = !MyProjectInfo.GetBoolValue(dbProject, pid, "不提升标题");
            bool IsSingleObject = this.summary.childlist.Count == 1;
            upgradeChapter = IsSingleObject && upgradeChapter;  // 两个条件都符合才提升标题

            StringBuilder sb = new StringBuilder(64);
            ItemNodeTree item = this;
            while(item != null)
            {
                if(item.nodeType == NodeType.Project) break;
                if(item.nodeType == NodeType.TestObject && upgradeChapter) break;
                sb.Insert(0, item.index);
                sb.Insert(0, ".");
                item = item.parent;
            }
            string[] info = new[] { "需求起始章节号", "计划起始章节号", "设计起始章节号", "记录起始章节号" };
            sb.Insert(0, ProjectInfo.GetProjectContent<int>(dbProject, pid, info[doctype]));
            return sb.ToString();
        }

        /// <summary>
        /// 获取指定类型的父节点(最近)
        /// </summary>
        public ItemNodeTree GetLeastParent(NodeType nt)
        {
            ItemNodeTree item = this;
            while(true)
            {
                if(item == null || item.nodeType == nt) return item;
                item = item.parent;
            }
        }

        /// <summary>
        /// 获取指定类型的父节点(最远)
        /// </summary>
        public ItemNodeTree GetFarestParent(NodeType nt)
        {
            ItemNodeTree item = this;
            while(true)
            {
                if(item.parent == null) return null;
                if(item.nodeType == nt && item.parent.nodeType != nt) return item;
                item = item.parent;
            }
        }

        public string GetIconName()
        {
            string key = null;
            if(nodeType == NodeType.Project) key = "project";
            if(nodeType == NodeType.TestObject) key = "obj";
            if(nodeType == NodeType.TestType) key = "type";
            if(nodeType == NodeType.TestItem) key = "item";
            if(nodeType == NodeType.TestCase)
            {
                string execute = dr["执行状态"] as string;
                string isPass = dr["执行结果"] as string;
                if(execute == "未执行") key = "unexec";
                else if(execute == "部分执行")
                    key = isPass == "未通过" ? "partexecp" : "partexec";
                else
                    key = isPass == "未通过" ? "execp" : "case";
                if(IsShortCut) key += "_k";
            }
            return key;
        }

        /// <summary>
        /// 获取对象层次关系
        /// 0: 测试项目。 1: 测试对象。 2:测试分类。 3:测试条目(对一级分类)
        /// </summary>
        public int GetLevel()
        {
            ItemNodeTree item = this;
            int level = 0;
            while(item.parent != null)
            {
                item = item.parent;
                level++;
            }
            return level;
        }
    }

    public class TestResultSummary : ItemNodeTree
    {
        DataTable dtObjectEntity, dtClassEntity, dtItemEntity, dtCaseEntity;
        public DataTable dtObjectRef, dtClassRef, dtItemRef, dtCaseRef;

        /// <summary>
        /// 测试用例与测试项关系
        /// </summary>
        public DataTable dtCaseItem;

        /// <summary>
        /// 针对实测ID索引
        /// </summary>
        public DataTableMap dtCaseMap;

        DataTable ExecuteDataTable(string sql, params object[] paramList)
        {
            return dbProject.ExecuteDataTable(sql, paramList);
        }

        public TestResultSummary(object pid, object vid)
        {
            this.pid = pid;
            this.vid = vid;
        }

        /// <summary>
        /// 4: 读到用例级
        /// 3: 测试项级
        /// 2: 测试分类
        /// 1: 测试对象
        /// </summary>
        public int dblevel = 4;

        public string
        sqlObject1 = "select * from CA被测对象实体表 where 项目ID = ?",
        sqlObject2 = "select * from CA被测对象实测表 where 测试版本 = ? order by 序号",
        sqlClass1 = "select * from CA测试类型实体表 where 项目ID = ?",
        sqlClass2 = "select * from CA测试类型实测表 where 测试版本 = ? order by 序号",
        sqlItem1 = "select * from CA测试项实体表 where 项目ID = ?",
        sqlItem2 = "select * from CA测试项实测表 where 测试版本 = ? order by 序号",
        sqlCase1 = "select * from CA测试用例实体表 where 项目ID = ?",
        sqlCase2 = "select * from CA测试用例实测表 where 测试版本 = ?",
        sqlCaseItem = "select * from CA测试用例与测试项关系表 where 项目ID = ? order by 序号";

        public void OnCreate()
        {
            ReadDatabase();
            this.summary = this;
            this.name = ProjectInfo.GetProjectString(dbProject, pid, "项目名称");
            this.nodeType = NodeType.Project;
            childlist.Clear();
            foreach(DataRow dr in dtObjectRef.Rows)
                AddChild(NodeType.TestObject, dr["被测对象ID"], dr, dr["简写码"], dr["被测对象名称"], dr["序号"]);
        }

        void ReadDatabase()
        {
            dtObjectEntity = ExecuteDataTable(sqlObject1, pid);
            dtObjectRef = ExecuteDataTable(sqlObject2, vid);
            MergeTable(dtObjectRef, dtObjectEntity, "被测对象ID");

            if(dblevel <= 1) return;
            dtClassEntity = ExecuteDataTable(sqlClass1, pid);
            dtClassRef = ExecuteDataTable(sqlClass2, vid);
            MergeTable(dtClassRef, dtClassEntity, "测试类型ID");

            if(dblevel <= 2) return;
            dtItemEntity = ExecuteDataTable(sqlItem1, pid);
            dtItemRef = ExecuteDataTable(sqlItem2, vid);
            MergeTable(dtItemRef, dtItemEntity, "测试项ID");

            if(dblevel <= 3) return;
            dtCaseEntity = ExecuteDataTable(sqlCase1, pid);
            dtCaseRef = ExecuteDataTable(sqlCase2, vid);
            MergeTable(dtCaseRef, dtCaseEntity, "测试用例ID");

            dtCaseItem = ExecuteDataTable(sqlCaseItem, pid);
            dtCaseMap = new DataTableMap(dtCaseRef, "ID");
        }

        /// <summary>
        /// 将 dtEntity 拼装到表 dtRef上，dtRef的 refcol 列引用到 dtEntity 的主键 上
        /// </summary>
        public static void MergeTable(DataTable dtRef, DataTable dtEntity, string refcol)
        {
            List<string> colList = new List<string>();

            // 添加所有dtRef中不存在的列
            foreach(DataColumn dc in dtEntity.Columns)
            {
                string col = dc.ColumnName;
                if(dtRef.Columns.Contains(col)) continue;
                dtRef.Columns.Add(col, dtEntity.Columns[col].DataType);
                colList.Add(col);
            }
            for(int i = dtRef.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dtRef.Rows[i];
                DataRow drEntity = GridAssist.GetDataRow(dtEntity, "ID", dr[refcol]);
                if(drEntity == null)
                {
                    dr.Delete();
                    continue;
                }
                foreach(string col in colList)
                    dr[col] = drEntity[col];  // 复制数据
            }
            dtRef.AcceptChanges();
        }
    }
}
