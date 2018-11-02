using System;
using System.Data;
using System.Windows.Forms;
using Common;
using C1.Win.C1FlexGrid;
using TPM3.Sys;

namespace TPM3.wx
{
    public partial class DBAccessLogForm : MyBaseForm
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<DBAccessLogForm>(6);
        FlexGridAssist flexAssist1;

        public DBAccessLogForm()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            flexAssist1 = new FlexGridAssist(flex1, null, null);
            flexAssist1.columnList = columnList1;
            flex1.AllowDragging = AllowDraggingEnum.Columns;
        }

        static DBAccessLogForm()
        {
            columnList1.Add("���", 50);
            columnList1.Add("ʱ��", 70);
            columnList1.Add("����", 60);
            columnList1.Add("Ӱ������", 60);
            columnList1.Add("sql", 700);

            columnList1["���"].TextAlign = CommonTextAlignEnum.Center;
            columnList1["ʱ��"].TextAlign = CommonTextAlignEnum.Center;
            columnList1["����"].TextAlign = CommonTextAlignEnum.Center;
            columnList1["Ӱ������"].TextAlign = CommonTextAlignEnum.Center;
        }

        void DBAccessLogForm_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
        }

        void RefreshFlex()
        {
            flexAssist1.OnPageClose();
            if( dbProject != null )
                label1.Text = dbProject.GetCounterString();
            DataTable dt = (DataTable)DBAccessLog.DataSource;
            DataView dv = new DataView(dt);
            if( cbFilter.SelectedIndex > 0 ) dv.RowFilter = " ���� = '" + cbFilter.SelectedItem + "'";
            flexAssist1.DataSource = dv;
            flexAssist1.OnPageCreate();
            FlexGridAssist.AutoSizeRows(flex1, 4);
        }

        void btClear_Click(object sender, EventArgs e)
        {
            DBAccessLog.ClearLog();
            RefreshFlex();
        }

        void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFlex();
        }
    }
}