using System;
using System.Data;
using Common;
using Common.TrueDBGrid;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// 问题级别和类别与版本无关
    /// </summary>
    [TypeNameMap("wx.FallTypeForm")]
    public partial class FallTypeForm : MyBaseForm
    {
        DataTable dt1, dt2;
        TrueDBGridAssist gridAssist1, gridAssist2;
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<FallTypeForm>(1);
        static ColumnPropList columnList2 = GridAssist.GetColumnPropList<FallTypeForm>(2);

        public FallTypeForm()
        {
            InitializeComponent();
            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "序号");
            gridAssist2 = new TrueDBGridAssist(grid2, "ID", "序号");
            gridAssist1.columnList = columnList1;
            gridAssist2.columnList = columnList2;
        }

        static FallTypeForm()
        {
            columnList1.Add("序号", 60);
            columnList1.Add("名称", 150, "问题类别名称");
            columnList1.Add("说明", 250);
            columnList1.AddValidator(new MaxLengthValidator("名称", 8));

            columnList2.Add("序号", 60);
            columnList2.Add("名称", 150, "问题级别名称");
            columnList2.Add("说明", 250);
            columnList2.AddValidator(new MaxLengthValidator("名称", 8));
        }

        private void FallTypeForm_Load(object sender, EventArgs e)
        {
            dt1 = DBLayer1.GetFallTypeList(dbProject, pid, "类别");
            if(dt1 == null) return;
            ExpandTable(dt1, 5);
            gridAssist1.DataSource = dt1;
            gridAssist1.OnPageCreate();

            dt2 = DBLayer1.GetFallTypeList(dbProject, pid, "级别");
            if(dt2 == null) return;
            ExpandTable(dt2, 5);
            gridAssist2.DataSource = dt2;
            gridAssist2.OnPageCreate();
        }

        static void ExpandTable(DataTable dt, int max)
        {
            for(int i = dt.Rows.Count; i < max; i++)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = FunctionClass.NewGuid;
                dr["序号"] = 1000 + i;
                dt.Rows.Add(dr);
            }
            GridAssist.SetDataTableIndex(dt, null, "序号");
        }


        public override bool OnPageClose(bool bClose)
        {
            gridAssist1.OnPageClose();
            gridAssist2.OnPageClose();

            if(!DBLayer1.UpdateFallTypeList(dbProject, dt1)) return false;
            if(!DBLayer1.UpdateFallTypeList(dbProject, dt2)) return false;

            return true;
        }
    }
}