using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using Common;
using Common.PropertyName;
using TPM3.wx;

namespace TPM3.Sys
{
    using MRSE = EnumListConverter<MyRowSizingEnum>;
    public partial class OptionForm : Form
    {
        public OptionForm()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void OptionForm_Load(object sender, EventArgs e)
        {
            propertyGrid1.SetSelectedObject(ProjectConfigData.Inst);
            propertyGrid2.SetSelectedObject(UserConfigData.Inst);
        }
    }

    public class BaseConfigData : BaseProjectClass
    {
        protected static bool SetConfig(string contentTitle, object value)
        {
            string s = "";
            if(value is Color)  // 颜色特殊处理，先转换成整数
                s = ((Color)value).ToArgb().ToString();
            else
                s = value.ToString();
            return ProjectInfo.SetProjectString(dbProject, pid, contentTitle, s);
        }

        protected static T GetConfig<T>(string ContentTitle)
        {
            return GetConfig(ContentTitle, default(T));
        }

        protected static T GetConfig<T>(string ContentTitle, T defaultValue)
        {
            string s = ProjectInfo.GetProjectString(dbProject, pid, ContentTitle);
            return ProjectInfo.ConvertContent(s, defaultValue);
        }
    }

    /// <summary>
    /// 和项目有关的配置信息
    /// </summary>
    public class ProjectConfigData : BaseConfigData
    {
        public static ProjectConfigData Inst = new ProjectConfigData();
        protected ProjectConfigData() { }

        static ProjectConfigData()
        {
        }

        protected static bool SetConfig2(string contentTitle, int value)
        {
            if(value < 1 || value > 20)
                throw new Exception("章节号的有效范围为1～20!!!");

            string s = value.ToString();
            return ProjectInfo.SetProjectString(dbProject, pid, contentTitle, s);
        }

        const string CategoryChapter = "测试用例/测试项在文档中的起始章节号";

        [Description("测试用例/测试项在测试需求分析中的起始章节号")]
        [PropertyName("测试需求分析", 1)]
        [Category(CategoryChapter)]
        [Obfuscation(Exclude = true)]
        [DefaultValue(5)]
        public int require
        {
            get { return GetConfig<int>("需求起始章节号"); }
            set { SetConfig2("需求起始章节号", value); }
        }

        [Description("测试用例/测试项在测试计划中的起始章节号")]
        [PropertyName("测试计划", 2)]
        [Category(CategoryChapter)]
        [Obfuscation(Exclude = true)]
        [DefaultValue(8)]
        public int plan
        {
            get { return GetConfig<int>("计划起始章节号"); }
            set { SetConfig2("计划起始章节号", value); }
        }

        [Description("测试用例/测试项在测试说明中的起始章节号")]
        [PropertyName("测试说明", 3)]
        [Category(CategoryChapter)]
        [Obfuscation(Exclude = true)]
        [DefaultValue(5)]
        public int design
        {
            get { return GetConfig<int>("设计起始章节号"); }
            set { SetConfig2("设计起始章节号", value); }
        }

        [Description("测试用例/测试项在测试记录中的起始章节号")]
        [PropertyName("测试记录", 4)]
        [Category(CategoryChapter)]
        [Obfuscation(Exclude = true)]
        [DefaultValue(2)]
        public int record
        {
            get { return GetConfig<int>("记录起始章节号"); }
            set { SetConfig2("记录起始章节号", value); }
        }

        [Description("选否时，如果只有一个测试对象，将不显示测试对象一级的标题，所有的测试分类、测试项、测试用例的章节号将往上提升一级")]
        [PropertyName("在单对象时是否显示测试对象一级标题", 11)]
        [Category("文档设置")]
        [Obfuscation(Exclude = true)]
        [DefaultValue(false)]
        public bool showObjectTitle
        {
            get { return MyProjectInfo.GetBoolValue(dbProject, pid, "不提升标题"); }
            set { MyProjectInfo.SetBoolValue(dbProject, pid, "不提升标题", value); }
        }

        [Description("选否时，测试用例不生成分级标题")]
        [PropertyName("是否生成测试用例标题", 12)]
        [Category("文档设置")]
        [Obfuscation(Exclude = true)]
        [DefaultValue(true)]
        public bool GenTestCaseTitle
        {
            get { return !MyProjectInfo.GetBoolValue(dbProject, pid, "测试用例标题反"); }
            set { MyProjectInfo.SetBoolValue(dbProject, pid, "测试用例标题反", !value); }
        }

        [Description("测试用例追踪到测试计划还是测试需求规格说明")]
        [PropertyName("测试用例追踪到的文档类型", 13)]
        [Category("文档设置")]
        [TypeConverter(typeof(EnumListConverter<TraceTypeEnum>))]
        [Obfuscation(Exclude = true)]
        [DefaultValue(TraceTypeEnum.TestPlan)]
        public TraceTypeEnum CaseTraceType
        {
            get { return GetConfig<TraceTypeEnum>("测试用例追踪到的文档类型", TraceTypeEnum.TestPlan); }
            set { SetConfig("测试用例追踪到的文档类型", (int)value); }
        }

        [Description("用例终止条件的缺省值")]
        [PropertyName("用例终止条件", 21)]
        [Category("缺省值")]
        [Editor(typeof(MultiLineEditorWrap), typeof(UITypeEditor))]
        [Obfuscation(Exclude = true)]
        public string caseStopCondition
        {
            get
            {
                string s = GetConfig<string>("缺省值_用例终止条件");
                return s;
            }
            set { SetConfig("缺省值_用例终止条件", value); }
        }

        [Description("用例通过准则的缺省值")]
        [PropertyName("用例通过准则", 22)]
        [Category("缺省值")]
        [Editor(typeof(MultiLineEditorWrap), typeof(UITypeEditor))]
        [Obfuscation(Exclude = true)]
        public string casePassCondition
        {
            get
            {
                string s = GetConfig<string>("缺省值_用例通过准则");
                return s;
            }
            set { SetConfig("缺省值_用例通过准则", value); }
        }

        [Description("统计测试项的方式")]
        [PropertyName("统计测试项的方式", 31)]
        [Category("跟踪与统计设置")]
        [TypeConverter(typeof(EnumListConverter<SummaryItemType>))]
        [Obfuscation(Exclude = true)]
        [DefaultValue(SummaryItemType.All)]
        public SummaryItemType summaryItemType
        {
            get { return GetSummaryItemType(); }
            set { UserSetting.Default.summaryItemType = (int)value; }
        }

        public static SummaryItemType GetSummaryItemType()
        {
            SummaryItemType sit = (SummaryItemType)UserSetting.Default.summaryItemType;
            if(sit == SummaryItemType.NoLeaf) return sit;
            return SummaryItemType.All;
        }
    }

    /// <summary>
    /// 用户个性化设置
    /// </summary>
    public class UserConfigData : BaseConfigData
    {
        public static UserConfigData Inst = new UserConfigData();
        protected UserConfigData() { }

        //[Description("检查并口软件狗将降低软件运行速度，如果无必要，可将此选项设置为‘false’")]
        //[PropertyName("是否检查并口软件狗", 11)]
        //[Category("软件狗设置")]
        //[Obfuscation(Exclude = true)]
        //[DefaultValue(true)]
        //public bool checkBKDog
        //{
        //    get { return CheckDog.UseBKDog; }
        //    set
        //    {
        //        CheckDog.UseBKDog = value;
        //        UserSetting.Default.checkBKDog = value;
        //    }
        //}

        const string c2 = "行高设置方式";

        [Description("测试对象列表的行高方式")]
        [PropertyName("测试对象列表", 0)]
        [Category(c2)]
        [TypeConverter(typeof(MRSE))]
        [Obfuscation(Exclude = true)]
        [DefaultValue(MyRowSizingEnum.IndividualRows)]
        public MyRowSizingEnum TestObjectList
        {
            get { return GetMyRowSizingEnum(0); }
            set { SetMyRowSizingEnum(0, value); }
        }

        [Description("测试分类列表的行高方式")]
        [PropertyName("测试分类列表", 1)]
        [Category(c2)]
        [TypeConverter(typeof(MRSE))]
        [Obfuscation(Exclude = true)]
        [DefaultValue(MyRowSizingEnum.IndividualRows)]
        public MyRowSizingEnum TestTypeList
        {
            get { return GetMyRowSizingEnum(1); }
            set { SetMyRowSizingEnum(1, value); }
        }

        [Description("测试项列表的行高方式")]
        [PropertyName("测试项列表", 2)]
        [Category(c2)]
        [TypeConverter(typeof(MRSE))]
        [Obfuscation(Exclude = true)]
        [DefaultValue(MyRowSizingEnum.IndividualRows)]
        public MyRowSizingEnum TestItemList
        {
            get { return GetMyRowSizingEnum(2); }
            set { SetMyRowSizingEnum(2, value); }
        }

        [Description("测试用例列表的行高方式")]
        [PropertyName("测试用例列表", 3)]
        [Category(c2)]
        [TypeConverter(typeof(MRSE))]
        [Obfuscation(Exclude = true)]
        [DefaultValue(MyRowSizingEnum.IndividualRows)]
        public MyRowSizingEnum TestCaseList
        {
            get { return GetMyRowSizingEnum(3); }
            set { SetMyRowSizingEnum(3, value); }
        }

        [Description("测试步骤列表的行高方式")]
        [PropertyName("测试步骤列表", 4)]
        [Category(c2)]
        [TypeConverter(typeof(MRSE))]
        [Obfuscation(Exclude = true)]
        [DefaultValue(MyRowSizingEnum.IndividualRows)]
        public MyRowSizingEnum TestStepList
        {
            get { return GetMyRowSizingEnum(4); }
            set { SetMyRowSizingEnum(4, value); }
        }

        [Description("测试用例自动设置的最大行高(单位:字符行数)")]
        [PropertyName("最大自动行高", 5)]
        [Category(c2)]
        [Obfuscation(Exclude = true)]
        [DefaultValue(5)]
        public int AutoCaseRowSize
        {
            get { return UserSetting.Default.AutoCaseRowSize; }
            set
            {
                if(value < 2 || value > 50)
                    throw new Exception("行高的有效范围为2～50!!!");
                UserSetting.Default.AutoCaseRowSize = value;
            }
        }

        static MyRowSizingEnum GetMyRowSizingEnum(int index)
        {
            RowSizingEnum r = (RowSizingEnum)UserSetting.Default.rowSizeingList[index];
            return r == RowSizingEnum.AllRows ? MyRowSizingEnum.AllRows : MyRowSizingEnum.IndividualRows;
        }

        static void SetMyRowSizingEnum(int index, MyRowSizingEnum mr)
        {
            RowSizingEnum r = RowSizingEnum.IndividualRows;
            if(mr == MyRowSizingEnum.AllRows) r = RowSizingEnum.AllRows;
            UserSetting.Default.rowSizeingList[index] = (int)r;
        }
    }

    public enum MyRowSizingEnum
    {
        /// <summary>
        /// All rows will be sized to the same height or width.
        /// </summary>
        [EnumDescription("所有行高相同")]
        AllRows = 1,

        /// <summary>
        /// Rows can be sized indepentently.
        /// </summary>
        [EnumDescription("所有行高分别设置")]
        IndividualRows = 2,
    }

    /// <summary>
    /// 测试用例追踪类型
    /// </summary>
    public enum TraceTypeEnum
    {
        /// <summary>
        /// 测试计划--缺省值
        /// </summary>
        [EnumDescription("测试计划")]
        TestPlan = 1,

        [EnumDescription("测试需求规格说明")]
        TestRequire = 2,
    }
}