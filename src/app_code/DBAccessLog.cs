using System;
using System.Data;
using Common;
using Common.Database;

namespace TPM3.wx
{
    /// <summary>
    /// 数据查询日志类
    /// </summary>
    public static class DBAccessLog
    {
        /// <summary>
        /// 初始化日志管理器
        /// </summary>
        public static void OnInit()
        {
            DBAccess.AccessEvent += DBAccess_AccessEvent;
            GridAssist.AddColumn(dtLog, "sql", "类型","时间");
            GridAssist.AddColumn<int>(dtLog, "序号", "影响行数");
        }

        const int MaxSql = 256;
        static int index = 0;

        /// <summary>
        /// 保存sql语句
        /// </summary>
        public static void DBAccess_AccessEvent(string sql, int count, DataRowState rs)
        {
            if( dtLog.Rows.Count >= MaxSql ) dtLog.Rows.RemoveAt(0);
            DataRow dr = dtLog.Rows.Add();
            dr["sql"] = sql;
            dr["类型"] = GetType(rs);
            dr["序号"] = ++index;
            dr["影响行数"] = count;
            dr["时间"] = DateTime.Now.ToString("HH:mm:ss");
        }

        static DataTable dtLog = new DataTable();

        public static void ClearLog()
        {
            dtLog.Rows.Clear();
            index = 0;
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public static object DataSource
        {
            get { return dtLog; }
        }

        static string GetType(DataRowState rs)
        {
            string s = "";
            switch( rs )
            {
            case DataRowState.Unchanged:
                s = "查询语句";
                break;
            case DataRowState.Added:
                s = "插入语句";
                break;
            case DataRowState.Deleted:
                s = "删除语句";
                break;
            case DataRowState.Modified:
                s = "更新语句";
                break;
            }
            return s;
        }
    }
}
