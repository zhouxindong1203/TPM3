using System.IO;
using System.Xml;
using Common;
using Common.Database;
using TPM3.wx;

namespace TPM3.Sys
{
    /// <summary>
    /// SN: SE20753-CX-374249
    /// </summary>
    public class GlobalData : GlobalDataBase
    {
        public static GlobalData globalData = new GlobalData();

        /// <summary>
        /// 当前登录的用户类型
        /// </summary>
        public UserType userType;

        /// <summary>
        /// 当前编辑的版本ID
        /// </summary>
        public object currentvid
        {
            get { return us.lastVersionID; }
            set { us.lastVersionID = value; }
        }

        /// <summary>
        /// 工具名称
        /// </summary>
        public static string ToolName = "测试过程管理工具 (TP-Manager) " + UpgradeDatabase.ToolVersion;

        public override bool OpenProjectDatabase(string filename)
        {
            _dbProject = DBAccessFactory.FromAccessFile(filename).CreateInst();
            if(_dbProject == null) return false;
            if(!UpgradeDatabase.CheckDataBase(_dbProject))
            {
                _dbProject.CloseConnection();
                return false;
            }
            return true;
        }

        public static string GetProjectDirName()
        {
            return Path.GetDirectoryName(us.LastDatabaseName);
        }

        /// <summary>
        /// 获取和数据库同级的附件文件夹
        /// </summary>
        public static string GetAttachDirName()
        {
            return GetAttachDirName(us.LastDatabaseName);
        }

        /// <summary>
        /// 获取与指定文件同级的附件文件夹
        /// </summary>
        public static string GetAttachDirName(string filename)
        {
            string s = Path.GetDirectoryName(filename);
            string name = MyProjectInfo.GetProjectContent("项目名称") + "的可打印附件";
            return Path.Combine(s, name);
        }

        /// <summary>
        /// 获取指定节点的参数
        /// 1. 查找其'说明'属性
        /// 2. 查找其'说明'子节点
        /// </summary>
        public static string GetParam(XmlElement ele)
        {
            string s = ele.GetAttribute("说明");
            if(!string.IsNullOrEmpty(s)) return s;
            XmlElement child = ele.SelectSingleNode("说明") as XmlElement;
            if(child == null) return "";

            if(child.ChildNodes.Count == 0) return child.InnerText;
            XmlNode xn = child.ChildNodes[0];
            // 如果是 CData 类型，则直接返回其内容
            if(xn is XmlCDataSection) return xn.InnerText;

            // 否则作为整体返回
            return child.OuterXml;
        }

        const string configfilename = "config.tpm";

#if Package
        public static string BaseDirectory = @"";
#else
        public static string BaseDirectory = @"..\..\";
#endif

        /// <summary>
        /// 读取默认配置
        /// </summary>
        public void OnInit()
        {
            UserSetting.Default = ClassAccesser.ReadConfigFile<UserSetting>(configfilename);
            GridAssist.SetColumnPropList(us.FlexGridColumnProps);
        }

        /// <summary>
        /// 程序退出时是否保存 columnPropMap 对象
        /// </summary>
        public bool SaveColumnPropMap = true;

        /// <summary>
        /// 程序关闭，保存所有变量 ==> Settings.Default
        /// </summary>
        public void OnClose()
        {
            us.FlexGridColumnProps = SaveColumnPropMap ? GridAssist.GetColumnPropList() : null;
            ClassAccesser.SaveToConfigFile(us, configfilename);
        }

        static UserSetting us
        {
            get { return UserSetting.Default; }
        }
    }

    public class GlobalConst
    {
        public static int MaxLevel = 6;

        /// <summary>
        /// 生成测试用例文档时，是否生成用例的章节号
        /// </summary>
        public static bool GenTestCaseTitle
        {
            get { return true; }
        }
    }
}
