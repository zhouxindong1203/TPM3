using System;
using System.Collections.Generic;
using System.Text;
using Z1.tpm.DB;
using TPM3.Sys;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using System.Drawing;

namespace TPM3.zxd
{
    /*
     * TPM3.zxd.ExecStatus
     * 负责保存程序运行期间某些状态
     * (仅在运行期间有效)
     * zhouxindong 2008/10/07
     */ 
    public class ExecStatus
    {
        // 最近用户操作的树节点页面
        public static string g_LastUserOp = string.Empty;

        // 附件文件夹
        public static string g_AccFolder = string.Empty;

        // 问题报告单窗体 -> 测试用例窗体切换时触发事件窗体的引用保存
        //public static Form g_PblRepsFormALink = null;
        //public static Form g_PblRepsRFormALink = null;

        // 测试用例窗体 -> 问题报告单窗体切换时触发事件窗体的引用保存
        //public static Form g_TestTreeFormALink = null;
        //public static Form g_TestUsecaseFormALink = null;

        // 用于通过用例ID弹出相应用例信息窗体时的tnForm初始化
        public static TestTreeForm g_ttf = null;
    }

    public class ConstDef
    {
        // "被测对象"的测试级别
        public static string[] testlevel = { "单元测试", "部件测试", "配置项测试", "系统测试" };

        // "测试类型"的子节点类型
        public static string[] subtype = { "子测试类型", "测试项" };

        // 测试类型+测试项最大级数
        public const int MaxLevel = 6;

        // 每个步骤能够拥有的附件最大数
        public const int MaxAccNum = 10;

        // 章节分隔符
        public const char SectionSep = '.';

        // 问题分级标识间隔符
        public static string PblSplitter()
        {
            return CommonDB.GetPblSpl(MyBaseForm.dbProject, (string)MyBaseForm.pid,
                (string)MyBaseForm.currentvid);
        }

        // 测试用例标识字段分隔符
        public const string UCSignSpl = "_";

        // 执行结果
        public static string[] execrlt = { string.Empty, "通过", "未通过" };

        // 执行状态
        public static string[] execsta = { "未执行", "部分执行", "完整执行" };

        public const string execrlt0 = "";
        public const string execrlt1 = "通过";
        public const string execrlt2 = "未通过";
        public const string execsta0 = "未执行";
        public const string execsta1 = "部分执行";
        public const string execsta2 = "完整执行";

        // 弹出附件窗体时的偏移距离
        public const int Offset_line = 20;

        // 能够输出的附件的最大尺寸
        public const int MaxAccFileSize = 4096 * 1024;

        public const string RegressSign = "回归";

        public static string[] markclr = {"Black", "Silver", "Maroon", "Red",
                                             "LightCoral", "Sienna", "Chocolate",
                                             "Gold", "OliveDrab", "SeaGreen",
                                             "Navy", "BlueViolet"};
    }
}
