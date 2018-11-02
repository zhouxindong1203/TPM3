using System.Data;
using Common;
using Common.TrueDBGrid;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// ��������ģ���б�
    /// </summary>
    [TypeNameMap("wx.TestClassTemplateList")]
    public partial class TestClassTemplateList : MyBaseForm
    {
        public static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestClassTemplateList>(1);
        public static ColumnPropList columnList2 = GridAssist.GetColumnPropList<TestClassTemplateList>(3);
        static readonly string sql1 = "select * from DC��������ģ��� where ���ڵ�ID = ?";

        TrueDBGridAssist gridAssist1;
        DataTable dtTable;
        const string root = "0";

        public TestClassTemplateList()
        {
            InitializeComponent();
            gridAssist1 = new TrueDBGridAssist(grid1, "��������ID", "���");
            gridAssist1.columnList = columnList1;
        }

        static TestClassTemplateList()
        {
            columnList1.Add("���", 50);
            columnList1.Add("������������", 120, "������������");
            columnList1.Add("��д��", 70);
            columnList1.Add("��Ҫ˵��", 230);

            columnList1.AddValidator(new NotNullValidator("��д��"));
            columnList1.AddValidator(new NotNullValidator("������������"));
            columnList1.RowHeight = 30;

            //columnList2.Add("���", 50);
            columnList2.Add("������������", 120, "������������");
            columnList2.Add("��д��", 70);
            columnList2.Add("��Ҫ˵��", 300);
        }

        public override bool OnPageCreate()
        {
            grid1.Caption = "���Ʋ������ͣ���������Ŀ���ڴ������޸Ľ�Ӱ�쵽���е���Ŀ";
            dtTable = dbProject.ExecuteDataTable(sql1, root);
            if(dtTable == null) return false;
            dtTable.Columns["���ڵ�ID"].DefaultValue = root;

            gridAssist1.DataSource = dtTable;
            gridAssist1.OnPageCreate();
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            gridAssist1.OnPageClose();
            if(!dbProject.UpdateDatabase(dtTable, sql1)) return false;
            return true;
        }

        private void grid1_AfterColEdit(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
        {
            //if(e.Column.Name.Equals("������������"))
            //{
            //    object v = this.grid1[this.grid1.Row, "������������"];
            //    if(DBNull.Value.Equals(v))
            //        return;

            //    if(ExistTestTypeWithName((string)v))
            //    {
            //        MessageBox.Show("�Ѵ��ڴ����ƵĲ�������!", "������������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        this.grid1[this.grid1.Row, "������������"] = string.Empty;
            //    }
            //}
        }

        //private bool ExistTestTypeWithName(string name)
        //{
        //    string sql = "SELECT * FROM DC��������ģ��� WHERE ������������ = ?";
        //    DataRow row = dbProject.ExecuteDataRow(sql, name);
        //    return row != null;
        //}
    }
}