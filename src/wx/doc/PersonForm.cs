using System.Data;
using Common;
using Common.TrueDBGrid;
using TPM3.Sys;

namespace TPM3.wx
{
    [TypeNameMap("wx.PersonForm")]
    public partial class PersonForm : MyBaseForm
    {
        DataTable dt1;
        TrueDBGridAssist gridAssist1;
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<PersonForm>(1);
        public static ColumnPropList columnList2 = GridAssist.GetColumnPropList<PersonForm>(2);

        public PersonForm()
        {
            InitializeComponent();
            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "序号") {columnList = columnList1};
        }

        static PersonForm()
        {
            columnList1.Add("序号", 60);
            columnList1.Add("角色", 100);
            columnList1.Add("姓名", 70);
            columnList1.Add("职称", 100);
            columnList1.Add("主要职责", 250);

            columnList2.Add("序号", 60);
            columnList2.Add("姓名", 70);
            columnList2.Add("角色", 100);

            columnList1.AddValidator(new NotNullValidator("姓名"));
        }

        public override bool OnPageCreate()
        {
            dt1 = DBLayer1.GetPersonList(dbProject, pid, currentvid);
            if( dt1 == null ) return false;
            gridAssist1.DataSource = dt1;
            gridAssist1.OnPageCreate();
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            gridAssist1.OnPageClose();
            if( !DBLayer1.UpdatePersonList(dbProject, dt1) ) return false;
            return true;
        }
    }
}