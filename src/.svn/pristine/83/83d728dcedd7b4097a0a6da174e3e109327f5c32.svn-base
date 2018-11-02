using System;
using System.Windows.Forms;
using System.Data;
using Common;
using Common.Database;
using C1.Win.C1FlexGrid;
using TPM3.Sys;

namespace TPM3.wx
{
    public class MyUserControl : UserControl, IBaseTreeForm
    {
        public object id;
        public TestResultSummary summary { get; set; }

        public virtual bool OnPageClose(bool bClose)
        {
            return true;
        }

        public virtual bool OnPageCreate(object key, TreeNode tn)
        {
            return true;
        }

        public virtual bool OnPageCreate()
        {
            return true;
        }

        public DBAccess dbProject
        {
            get
            {
                return GlobalData.globalData.dbProject;
            }
        }

        public static object pid
        {
            get { return GlobalData.globalData.projectID; }
        }

        public static object currentvid
        {
            get { return GlobalData.globalData.currentvid; }
        }

        public static void AddMergeColumn(C1FlexGrid flex, params object[] cols)
        {
            foreach( object col in cols )
            {
                Column c = col is int ? flex.Cols[(int)col] : flex.Cols[(string)col];
                c.AllowMerging = true;
            }
        }
    }

    public class BaseProjectClass
    {
        public static DBAccess dbProject
        {
            get { return GlobalData.globalData.dbProject; }
        }

        public static object pid
        {
            get { return GlobalData.globalData.projectID; }
        }

        public static object currentvid
        {
            get { return GlobalData.globalData.currentvid; }
        }

        public static DataTable ExecuteDataTable(string sql, params object[] paramList)
        {
            return dbProject.ExecuteDataTable(sql, paramList);
        }
    }
}
