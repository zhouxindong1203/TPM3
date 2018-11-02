using System.Data;
using System.Windows.Forms;
using TPM3.Sys;
using Common;
using C1.Win.C1FlexGrid;

namespace TPM3.wx
{
    /// <summary>
    /// 软件更动表
    /// </summary>
    [TypeNameMap("wx.SoftwareModifyForm")]
    public partial class SoftwareModifyForm : MyBaseForm
    {
        FlexGridAssist flexAssist;
        public static ColumnPropList columnList1 = FlexGridAssist.GetColumnPropList<SoftwareModifyForm>(7);
        DataTable dtTable;
        static string sqlTable = "select * from [HG软件更动表] where 测试版本 = ? order by 序号";

        public SoftwareModifyForm()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithFocus(flex1);
            flex1.AllowDragging = AllowDraggingEnum.Columns;
            flex1.Styles.Normal.WordWrap = true;

            flexAssist = new FlexGridAssist(flex1, "ID", "序号");
            flexAssist.doubleClickEdit = true;
            flexAssist.deleteClear = true;
            flexAssist.columnList = columnList1;
            flexAssist.AddOleColumn("影响域分析");

            var cm = flexAssist.refrenceColumnMapList.AddColumnMap("相关测试依据", "", "");
            object previd = DBLayer1.GetPreVersion(dbProject, currentvid);
            RequireTreeForm.InitColumnRefMap(cm, previd);
            cm.allowResize = false;

            cm = flexAssist.refrenceColumnMapList.AddColumnMap("本版本相关依据", "", "");
            RequireTreeForm.InitColumnRefMap(cm, currentvid);
            cm.allowResize = false;
        }

        static SoftwareModifyForm()
        {
            columnList1.Add("序号", 50);
            columnList1.Add("更动名称", 130);
            columnList1.Add("更动说明", 130);
            columnList1.Add("相关测试依据", 200, "上一版本相关测试依据");
            columnList1.Add("本版本相关依据", 200, "本版本相关测试依据");
            columnList1.Add("影响域分析", 130);
        }

        public override bool OnPageCreate()
        {
            dtTable = dbProject.ExecuteDataTable(sqlTable, currentvid);
            if(dtTable == null)
            {
                MessageBox.Show("打开软件更动表失败,请检查数据库!!!");
                return false;
            }
            dtTable.Columns["项目ID"].DefaultValue = pid;
            dtTable.Columns["测试版本"].DefaultValue = currentvid;

            flexAssist.DataSource = dtTable;
            flexAssist.OnPageCreate();

            FlexGridAssist.AutoSizeRows(flex1, 4, 15, 4);
            flex1.Rows[0].Height = 30;
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            flexAssist.OnPageClose();
            dbProject.UpdateDatabase(dtTable, sqlTable);

            return true;
        }
    }
}
