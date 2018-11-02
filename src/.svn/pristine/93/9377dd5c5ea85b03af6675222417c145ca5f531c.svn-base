using System;
using System.Data;
using Common;
using Common.Database;
using Common.RichTextBox;

namespace TPM3.wx
{
    public class DBLayer1
    {
        /// <summary>
        /// 获取当前版本的前向版本
        /// 如果当前版本是第一版，则前向版本为null
        /// </summary>
        public static object GetPreVersion(DBAccess dba, object vid)
        {
            string sql = "select 前向版本ID from SYS测试版本表 where ID = ?";
            object obj = dba.ExecuteScalar(sql, vid);
            return GridAssist.IsNull(obj) ? null : obj;
        }

        /// <summary>
        /// 返回当前页面的版本模式:准备还是执行
        /// </summary>
        public static VersionMode GetVersionMode(DBAccess dba, object pid, object vid)
        {
            string s = ProjectInfo.GetDocString(dba, pid, vid, null, "当前模式");
            return ProjectInfo.ConvertContent(s, VersionMode.Execute);
        }

        /// <summary>
        /// 获取版本的序号，初始测试为0，以后递增
        /// </summary>
        public static int GetVersionIndex(DBAccess dba, object vid)
        {
            string sql = "select 序号 from SYS测试版本表 where ID = ?";
            object obj = dba.ExecuteScalar(sql, vid);
            return ((int)obj) - 1;
        }

        /// <summary>
        /// 从数据库中删除指定的回归版本
        /// </summary>
        public static void DeleteVersion(DBAccess dba, object pid, object vid)
        {
            string sql = "delete from SYS测试版本表 where ID = ?";
            dba.ExecuteNoQuery(sql, vid);
            // CA测试用例与测试项关系表 ?
            string[] tablelist = { "CA被测对象实测表", "CA测试过程实测表", "CA测试类型实测表", "CA测试项实测表", "CA测试用例实测表", "CA问题报告单", "DC测试过程附件表", 
                                   "DC测试资源配置表", "DC测试组织与人员表", "DC计划进度表", "DC术语表", "DC文档修改页", "DC问题标识", "DC引用文件表", 
                                   "HG回归测试未测试原因", "HG软件更动表", "SYS测试依据表","SYS文档内容表" };
            foreach(string t in tablelist)
            {
                sql = "delete from [{0}] where 测试版本 = ?";
                dba.ExecuteNoQuery(string.Format(sql, t), vid);
            }
            // 重新排序号
            DataTable dt = GetProjectVersionList(dba, pid, false);
            GridAssist.SetDataTableIndex(dt, null, "序号");
            UpdateVersionList(dba, dt);
        }

        /// <summary>
        /// 获取当前的项目是哪类模式。缺省为完全模式
        /// </summary>
        public static ProjectStageType GetProjectType(DBAccess dba, object pid)
        {
            string s = ProjectInfo.GetProjectString(dba, pid, "项目类别");  // 返回1、2、3
            return ProjectInfo.ConvertContent(s, ProjectStageType.I类);
        }

        /// <summary>
        /// 设置当前项目的模式
        /// </summary>
        public static void SetProjectType(DBAccess dba, object pid, ProjectStageType pst)
        {
            string s = ((int)pst).ToString();
            ProjectInfo.SetProjectString(dba, pid, "项目类别", s);
        }

        /// <summary>
        /// ----------------------------------------------------------------- 
        /// 获取该项目中的用户列表
        /// </summary>
        public static DataTable GetUserList(DBAccess dba, object pid, bool GetAll)
        {
            //  DataTable dt = GetUserList(dba);
            string sql = @"SELECT u.*, p.用户角色, p.用户ID
                FROM SYS项目用户表 AS p RIGHT JOIN SYS用户表 AS u ON p.用户ID = u.ID
                WHERE (p.用户ID is not null and p.项目ID = ?) ";
            if(GetAll) sql += " OR u.用户类型 = 0 ";
            return dba.ExecuteDataTable(sql, pid);
        }

        public static DataTable GetUserList(DBAccess dba)
        {
            string sql = "select * from SYS用户表 order by 序号";
            return dba.ExecuteDataTable(sql);
        }

        public static DataTable GetUserByID(DBAccess dba, object userid)
        {
            string sql = "select * from SYS用户表 where ID = ?";
            return dba.ExecuteDataTable(sql, userid);
        }

        /// <summary>
        /// -----------------------  问题类别列表列表  ---------------------------------------------
        /// </summary>
        static readonly string sqlFallTypeList = "select * from DC问题级别表 where 项目ID = ? and 类型 = ? order by 序号";

        /// <summary>
        /// 获取该项目中的问题列表
        /// </summary>
        /// <param name="type">类别，级别</param>
        public static DataTable GetFallTypeList(DBAccess dba, object pid, string type)
        {
            DataTable dt = dba.ExecuteDataTable(sqlFallTypeList, pid, type);
            dt.Columns["项目ID"].DefaultValue = pid;
            dt.Columns["类型"].DefaultValue = type;
            return dt;
        }

        public static bool UpdateFallTypeList(DBAccess dba, DataTable dt)
        {
            return dba.UpdateDatabase(dt, sqlFallTypeList);
        }

        /// <summary>
        /// -----------------------  术语和缩略语列表  ---------------------------------------------
        /// </summary>
        static readonly string sqlAbbrev = "select * from DC术语表 where 项目ID = ? and 测试版本 = ? and 文档名称 = ? order by 序号";

        /// <summary>
        /// 获取该项目中的术语和缩略语列表
        /// </summary>
        public static DataTable GetAbbrevList(DBAccess dba, object pid, object vid, string docName)
        {
            DataTable dt = dba.ExecuteDataTable(sqlAbbrev, pid, vid, docName);
            dt.Columns["项目ID"].DefaultValue = pid;
            dt.Columns["测试版本"].DefaultValue = vid;
            dt.Columns["文档名称"].DefaultValue = docName;
            return dt;
        }

        public static bool UpdateAbbrevList(DBAccess dba, DataTable dt)
        {
            return dba.UpdateDatabase(dt, sqlAbbrev);
        }

        /// <summary>
        /// -------------------------- 引用文件列表 -----------------------------------------------
        /// </summary>
        static readonly string sqlReffile = "select * from DC引用文件表 where 项目ID = ? and 测试版本 = ? and 文档名称 = ? order by 序号";

        /// <summary>
        /// 获取该项目中的引用文件列表
        /// </summary>
        public static DataTable GetReffileList(DBAccess dba, object pid, object vid, string docName)
        {
            DataTable dt = dba.ExecuteDataTable(sqlReffile, pid, vid, docName);
            dt.Columns["项目ID"].DefaultValue = pid;
            dt.Columns["测试版本"].DefaultValue = vid;
            dt.Columns["文档名称"].DefaultValue = docName;
            return dt;
        }

        public static bool UpdateReffileList(DBAccess dba, DataTable dt)
        {
            return dba.UpdateDatabase(dt, sqlReffile);
        }

        /// <summary>
        /// -------------------------- 资源列表 -----------------------------------------------
        /// </summary>
        static readonly string sqlResource = "select * from DC测试资源配置表 where 项目ID = ? and 测试版本 = ? and 类型 = ? order by 序号";

        /// <summary>
        /// 获取该项目中的引用文件列表
        /// </summary>
        public static DataTable GetResourceList(DBAccess dba, object pid, object vid, string type)
        {
            type = type ?? "通用";
            DataTable dt = dba.ExecuteDataTable(sqlResource, pid, vid, type);
            dt.Columns["项目ID"].DefaultValue = pid;
            dt.Columns["测试版本"].DefaultValue = vid;
            dt.Columns["类型"].DefaultValue = type;
            return dt;
        }

        public static bool UpdateResourceList(DBAccess dba, DataTable dt)
        {
            return dba.UpdateDatabase(dt, sqlResource);
        }

        /// <summary>
        /// -------------------------- 测试组织与人员表 -----------------------------------------------
        /// </summary>
        static readonly string sqlPerson = "select * from DC测试组织与人员表 where 项目ID = ? and 测试版本 = ? order by 序号";

        public static DataTable GetPersonList(DBAccess dba, object pid, object vid)
        {
            DataTable dt = dba.ExecuteDataTable(sqlPerson, pid, vid);
            dt.Columns["项目ID"].DefaultValue = pid;
            dt.Columns["测试版本"].DefaultValue = vid;
            return dt;
        }

        public static bool UpdatePersonList(DBAccess dba, DataTable dt)
        {
            return dba.UpdateDatabase(dt, sqlPerson);
        }

        /// <summary>
        /// -------------------------- 测试用例设计方法表 -----------------------------------------------
        /// </summary>
        static readonly string sqlDesignMethod = "select * from DC测试用例设计方法表 where 项目ID = ? order by 序号";

        public static DataTable GetDesignMethodList(DBAccess dba, object pid)
        {
            DataTable dt = dba.ExecuteDataTable(sqlDesignMethod, pid);
            dt.Columns["项目ID"].DefaultValue = pid;
            return dt;
        }

        public static bool UpdateDesignMethodList(DBAccess dba, DataTable dt)
        {
            return dba.UpdateDatabase(dt, sqlDesignMethod);
        }

        /// <summary>
        /// -------------------------- 测试项优先级表 -----------------------------------------------
        /// </summary>
        static readonly string sqlTestPriority = "select * from DC测试项优先级表 where 项目ID = ? order by 序号";

        public static DataTable GetTestPriorityList(DBAccess dba, object pid)
        {
            DataTable dt = dba.ExecuteDataTable(sqlTestPriority, pid);
            dt.Columns["项目ID"].DefaultValue = pid;
            return dt;
        }

        public static bool UpdateTestPriorityList(DBAccess dba, DataTable dt)
        {
            return dba.UpdateDatabase(dt, sqlTestPriority);
        }

        /// <summary>
        /// -------------------------- 计划进度表 -----------------------------------------------
        /// </summary>
        static readonly string sqlTestPlan = "select * from DC计划进度表 where 项目ID = ? and 测试版本 = ? order by 序号";

        public static DataTable GetTestPlanList(DBAccess dba, object pid, object vid)
        {
            DataTable dt = dba.ExecuteDataTable(sqlTestPlan, pid, vid);
            dt.Columns["项目ID"].DefaultValue = pid;
            dt.Columns["测试版本"].DefaultValue = vid;
            return dt;
        }

        public static bool UpdateTestPlanList(DBAccess dba, DataTable dt)
        {
            return dba.UpdateDatabase(dt, sqlTestPlan);
        }

        /// <summary>
        /// -------------------------- SYS测试版本表 -----------------------------------------------
        /// </summary>
        static readonly string sqlVersion = "select * from SYS测试版本表 where 项目ID = ? order by 序号";

        /// <summary>
        /// 返回指定项目的所有版本列表
        /// </summary>
        /// <param name="GetExtendProp">true: 获取所有扩展属性，如版本名称，版本说明等</param>
        public static DataTable GetProjectVersionList(DBAccess dba, object pid, bool GetExtendProp)
        {
            DataTable dt = dba.ExecuteDataTable(sqlVersion, pid);
            dt.Columns["项目ID"].DefaultValue = pid;

            if(GetExtendProp)
            {
                Func2<string, object, string> gv = (vid, title) => ProjectInfo.GetDocString(dba, pid, vid, null, title);
                GridAssist.AddColumn(dt, "版本名称", "版本说明", "版本创建时间", "前向版本名称");
                foreach(DataRow dr in dt.Rows)
                {
                    object vid = dr["ID"], previd = dr["前向版本ID"];
                    dr["版本名称"] = gv(vid, "版本名称");
                    dr["版本说明"] = gv(vid, "版本说明");
                    dr["版本创建时间"] = FunctionClass.GetDateTimeString(gv(vid, "版本创建时间"));
                    if(!GridAssist.IsNull(previd))
                        dr["前向版本名称"] = gv(previd, "版本名称");
                }
            }
            return dt;
        }

        public static bool UpdateVersionList(DBAccess dba, DataTable dt)
        {
            return dba.UpdateDatabase(dt, sqlVersion);
        }

        /// <summary>
        /// 创建新的版本
        /// </summary>
        /// <returns>版本ID</returns>
        public static object CreateNewVersion(DBAccess dbProject, object pid, object previd, string name, string memo)
        {
            object vid = FunctionClass.NewGuid;
            DataTable dt = GetProjectVersionList(dbProject, pid, false);
            DataRow dr = dt.NewRow();
            dr["ID"] = vid;
            dr["项目ID"] = pid;
            dr["序号"] = 99999999;
            dr["前向版本ID"] = previd ?? DBNull.Value;
            dr["版本状态"] = 0;
            dt.Rows.Add(dr);
            GridAssist.SetDataTableIndex(dt, null, "序号");
            UpdateVersionList(dbProject, dt);

            CopyVersionContent(dbProject, pid, previd, vid, name);

            VoidFunc<string, string> sv = (title, value) => ProjectInfo.SetDocString(dbProject, pid, vid, null, title, value);
            sv("版本名称", name);
            sv("版本说明", memo);
            sv("版本创建时间", DateTime.Now.ToString());
            VersionMode vm = previd == null ? VersionMode.Execute : VersionMode.Prepare;
            sv("当前模式", ((int)vm).ToString());
            return vid;
        }

        /// <summary>
        /// 复制版本内容
        /// vid1 ==> vid2
        /// </summary>
        public static void CopyVersionContent(DBAccess dbProject, object pid, object vid1, object vid2, string name)
        {
            if(vid1 == null) return;

            // 复制[SYS文档内容表]中的所有内容
            string sql = "select * from SYS文档内容表 where 项目ID = ? and 测试版本 = ?";
            DataTable dt = dbProject.ExecuteDataTable(sql, pid, vid1);
            foreach(DataRow dr in dt.Rows)
            {
                string docname = dr["文档名称"].ToString(), title = dr["内容标题"].ToString(), type = dr["内容类型"].ToString();
                if(ProjectInfo.OleType == type)
                    ProjectInfo.SetDocContent(dbProject, pid, vid2, docname, title, dr["文档内容"] as byte[]);
                else
                    ProjectInfo.SetDocString(dbProject, pid, vid2, docname, title, dr["文本内容"].ToString());
            }

            // 设置缺少的测试报告声明
            string msg = @"一、测试报告中的结论仅适用于被测对象的特定版本({0})。

二、测试报告的印章及签字应齐全，须经测试人员和测试实验室领导签字方为有效。

三、测试报告应被完整使用。
";
            msg = string.Format(msg, "");
            ProjectInfo.SetDocContent(dbProject, pid, vid2, null, "测试报告声明", IOleObjectAssist.GetByteFromString(msg));

            VoidFunc<DataTable> fn = dt2 =>
             {
                 foreach(DataRow dr in dt2.Rows)
                 {
                     dr["ID"] = FunctionClass.NewGuid;
                     dr["测试版本"] = vid2;
                     dr.AcceptChanges();
                     dr.SetAdded();
                 }
             };

            // 复制[DC术语表]
            sql = "select * from DC术语表 where 项目ID = ? and 测试版本 = ? order by 序号";
            dt = dbProject.ExecuteDataTable(sql, pid, vid1);
            fn(dt);
            UpdateAbbrevList(dbProject, dt);

            // 复制[DC引用文件表]
            sql = "select * from DC引用文件表 where 项目ID = ? and 测试版本 = ? order by 序号";
            dt = dbProject.ExecuteDataTable(sql, pid, vid1);
            fn(dt);
            UpdateReffileList(dbProject, dt);

            // 复制[DC测试资源配置表]
            sql = "select * from DC测试资源配置表 where 项目ID = ? and 测试版本 = ? order by 序号";
            dt = dbProject.ExecuteDataTable(sql, pid, vid1);
            fn(dt);
            UpdateResourceList(dbProject, dt);
        }

        /// <summary>
        /// -------------------------- SYS测试项目表 -----------------------------------------------
        /// </summary>
        static readonly string sqlProject = "select * from SYS测试项目表 order by 序号";

        /// <summary>
        /// 获取项目列表及其它信息
        /// </summary>
        /// <param name="dba"></param>
        /// <param name="cols">其它查询的项目信息</param>
        public static DataTable GetProjectList(DBAccess dba, params string[] cols)
        {
            DataTable dt = dba.ExecuteDataTable(sqlProject);
            GridAssist.AddColumn(dt, cols);
            foreach(DataRow dr in dt.Rows)
                foreach(string col in cols)
                    dr[col] = ProjectInfo.GetProjectString(dba, dr["ID"], col);
            dt.AcceptChanges();
            return dt;
        }

        public static bool UpdateProjectList(DBAccess dba, DataTable dt)
        {
            return dba.UpdateDatabase(dt, sqlProject);
        }

        /// <summary>
        /// 在项目表中创建新的项目
        /// </summary>
        public static object CreateNewProject(DBAccess dbProject, string name, string sign)
        {
            object pid = FunctionClass.NewGuid;
            // 在项目表中添加表项目
            DataTable dtProject = GetProjectList(dbProject);
            DataRow dr = dtProject.NewRow();
            dr["ID"] = pid;
            dr["序号"] = 9999999;
            dtProject.Rows.Add(dr);
            GridAssist.SetDataTableIndex(dtProject, null, "序号");
            UpdateProjectList(dbProject, dtProject);

            ProjectInfo.SetProjectString(dbProject, pid, "项目名称", name);
            ProjectInfo.SetProjectString(dbProject, pid, "项目标识", sign);
            return pid;
        }

        public static void DeleteProject(DBAccess dba, object pid)
        {
            MyProjectBackup pb = new MyProjectBackup(dba, pid);
            pb.DeleteProjectInfomation();

            string sql = "delete from SYS测试项目表 where ID = ?";
            dba.ExecuteNoQuery(sql, pid);
        }
    }

    public enum UserType
    {
        /// <summary>
        /// 系统管理员 = 0
        /// </summary>
        [EnumDescription("系统管理员")]
        SysAdmin = 0,

        /// <summary>
        /// 项目负责人 = 1，或者非系统管理员(对用户表)
        /// </summary>
        [EnumDescription("项目负责人")]
        PM = 1,

        /// <summary>
        /// 项目参加人 = 2
        /// </summary>
        [EnumDescription("项目参加人")]
        Member = 2,

        /// <summary>
        /// 浏览用户 = 3
        /// </summary>
        [EnumDescription("浏览用户")]
        Guest = 3
    }

    /// <summary>
    /// 回归测试所处的阶段
    /// </summary>
    public enum VersionMode
    {
        /// <summary>
        /// 回归准备阶段 = 1
        /// </summary>
        Prepare = 1,

        /// <summary>
        /// 回归执行阶段 = 2
        /// </summary>
        Execute = 2,
    }

    /// <summary>
    /// 项目类型
    /// </summary>
    public enum ProjectStageType
    {
        /// <summary>
        /// 完全模式
        /// </summary>
        [EnumDescription("完全模式")]
        I类 = 1,

        /// <summary>
        /// 简化模式
        /// </summary>
        [EnumDescription("简化模式")]
        II类 = 2,

        /// <summary>
        /// 最小模式
        /// </summary>
        [EnumDescription("最小模式")]
        III类 = 3,

        /// <summary>
        /// 定型模式
        /// </summary>
        [EnumDescription("定型模式")]
        定型 = 4,
    }
}
