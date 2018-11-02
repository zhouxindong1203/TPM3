using System.Data;
using System.Windows.Forms;
using Common.Database;
using TPM3.Sys;
using Common;
using C1.Win.C1FlexGrid;

namespace TPM3.wx
{
    /// <summary>
    /// 适应性改造更动说明、等
    /// </summary>
    public partial class RegressModifyControl1 : UserControl
    {
        public static ColumnPropList columnList1 = FlexGridAssist.GetColumnPropList<RegressModifyControl1>(2);
        static string sqlTable = "select * from [HG软件更动表] where 测试版本 = ? and 更动类型 = ? order by 序号";
        FlexGridAssist flexAssist;
        DataTable dtTable;

        /// <summary>
        /// 更动类型
        /// </summary>
        public string modifyType;

        public RegressModifyControl1()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithFocus(flex1);
            flex1.AllowDragging = AllowDraggingEnum.Columns;
            flex1.Styles.Normal.WordWrap = true;

            flexAssist = new FlexGridAssist(flex1, "ID", "序号") { doubleClickEdit = true, deleteClear = true, columnList = columnList1 };
        }

        static RegressModifyControl1()
        {
            columnList1.Add("序号", 50);
            columnList1.Add("更动标识", 150);
            columnList1.Add("更动说明", 430);
        }

        public bool OnPageCreate()
        {
            dtTable = dbProject.ExecuteDataTable(sqlTable, currentvid, modifyType);
            if(dtTable == null)
            {
                MessageBox.Show("打开软件更动表失败,请检查数据库!!!");
                return false;
            }
            dtTable.Columns["项目ID"].DefaultValue = pid;
            dtTable.Columns["测试版本"].DefaultValue = currentvid;
            dtTable.Columns["是否更动"].DefaultValue = true;
            dtTable.Columns["更动类型"].DefaultValue = modifyType;

            flexAssist.DataSource = dtTable;
            flexAssist.OnPageCreate();

            FlexGridAssist.AutoSizeRows(flex1, 2);
            return true;
        }

        public bool OnPageClose()
        {
            flexAssist.OnPageClose();
            dbProject.UpdateDatabase(dtTable, sqlTable);

            return true;
        }

        public static DBAccess dbProject
        {
            get { return globalData.dbProject; }
        }

        public static GlobalData globalData
        {
            get { return GlobalData.globalData; }
        }

        public static object pid
        {
            get { return globalData.projectID; }
        }

        public static object currentvid
        {
            get { return globalData.currentvid; }
        }
    }
}
