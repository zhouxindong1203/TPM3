using System.Collections.Generic;
using Common;
using Common.Database;
using TPM3.Sys;

namespace TPM3.wx
{
    public class MyProjectInfo
    {
        public static void OnInit()
        {
            nameIndexMap["测试环境"] = 8;
            nameIndexMap["测试进度"] = 9;
            nameIndexMap["不提升标题"] = 50;  // 单对象时是否提升一级标题
            nameIndexMap["测试用例标题反"] = 51;  // 是否给每个测试用例生成标题

            // 项目定制
            ProjectInfo.root = "00";
            ProjectInfo.TableName = "SYS文档内容表";
            ProjectInfo.VersionColumn = "测试版本";
            ProjectInfo.OleType = "对象";
            ProjectInfo.MaxAvailableTime = 30;

            ProjectInfo.DocDefault[null, "标识版本前缀"] = "R";

            ProjectInfo.DocDefault["需求起始章节号"] = 5;
            ProjectInfo.DocDefault["计划起始章节号"] = 8;
            ProjectInfo.DocDefault["设计起始章节号"] = 5;
            ProjectInfo.DocDefault["记录起始章节号"] = 2;
            ProjectInfo.DocDefault["测试依据分类"] = true;

            string s = "本测试用例的全部测试步骤被执行或因某种原因导致测试步骤无法执行(异常终止)。";
            ProjectInfo.DocDefault["缺省值_用例终止条件"] = s;
            s = "本测试用例的全部测试步骤都通过即标志本用例为\"通过\"。";
            ProjectInfo.DocDefault["缺省值_用例通过准则"] = s;
        }

        /// <summary>
        /// 标志名称到标志所在位置的映射表
        /// </summary>
        static Dictionary<string, int> nameIndexMap = new Dictionary<string, int>();
        public const string BitMapName = "位控变量列表";

        public static bool GetBoolValue(DBAccess dba, object pid, string name)
        {
            return GetBoolValue(dba, pid, null, name);
        }

        public static void SetBoolValue(DBAccess dba, object pid, string name, bool value)
        {
            SetBoolValue(dba, pid, null, name, value);
        }

        public static bool GetBoolValue(DBAccess dba, object pid, object vid, string name)
        {
            string s = ProjectInfo.GetDocString(dba, pid, vid, null, BitMapName);
            long i;
            if(!long.TryParse(s, out i)) i = 0;
            i = (i >> nameIndexMap[name]) & 1;
            return i == 1;
        }

        public static void SetBoolValue(DBAccess dba, object pid, object vid, string name, bool value)
        {
            string s = ProjectInfo.GetDocString(dba, pid, vid, null, BitMapName);
            long i;
            if(!long.TryParse(s, out i)) i = 0;

            long oldi = i;
            long mask = 1L << nameIndexMap[name];
            if(value) // 置1
                i = i | mask;
            else // 置0
                i = i & (~mask);
            if(i == oldi) return; // 不需要更改
            ProjectInfo.SetDocString(dba, pid, vid, null, BitMapName, i.ToString());
        }

        public static T GetProjectContent<T>(string title)
        {
            return ProjectInfo.GetProjectContent<T>(GlobalData.globalData.dbProject, GlobalData.globalData.projectID, title);
        }

        public static string GetProjectContent(string title)
        {
            return GetProjectContent<string>(title);
        }

        /// <summary>
        /// 项目标识号
        /// </summary>
        public static string ProjectCode(DBAccess dba, object pid)
        {
            return ProjectInfo.GetProjectString(dba, pid, "项目标识号");
        }

        /// <summary>
        /// 项目名称
        /// </summary>
        public static string ProjectName(DBAccess dba, object pid)
        {
            return ProjectInfo.GetProjectString(dba, pid, "项目名称");
        }

        /// <summary>
        /// 获取拼装版本序号后的标识号，例如 TPM(R1)
        /// </summary>
        public static string VersionSign(DBAccess dba, object pid, object vid)
        {
            string s1 = ProjectCode(dba, pid);
            string pre = ProjectInfo.GetProjectString(dba, pid, "标识版本前缀");
            int currentVerIndex = DBLayer1.GetVersionIndex(dba, vid);
            if(currentVerIndex > 0)
                s1 += "(" + pre + currentVerIndex + ")";
            return s1;
        }
    }
}
