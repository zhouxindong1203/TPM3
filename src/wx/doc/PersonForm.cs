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
            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "���") {columnList = columnList1};
        }

        static PersonForm()
        {
            columnList1.Add("���", 60);
            columnList1.Add("��ɫ", 100);
            columnList1.Add("����", 70);
            columnList1.Add("ְ��", 100);
            columnList1.Add("��Ҫְ��", 250);

            columnList2.Add("���", 60);
            columnList2.Add("����", 70);
            columnList2.Add("��ɫ", 100);

            columnList1.AddValidator(new NotNullValidator("����"));
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