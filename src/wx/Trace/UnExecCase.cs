using System.Data;
using C1.Win.C1FlexGrid;
using Common;
using TPM3.Sys;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.wx
{
    /// <summary>
    /// δ����ִ�в�������
    /// </summary>
    [TypeNameMap("wx.UnExecCase")]
    public partial class UnExecCase : MyUserControl
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<UnExecCase>();
        FlexGridAssist flexAssist1;

        DataTable dt;
        public UnExecCase()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            flexAssist1 = new FlexGridAssist(flex1, null, null);
            flexAssist1.AddHyperColumn("������������");
            flexAssist1.AddHyperColumn("����������ʶ");
            flexAssist1.columnList = columnList1;
            flexAssist1.RowNavigate += OnRowNavigate;
        }

        static UnExecCase()
        {
            columnList1.Add("���", 50, false);
            columnList1.Add("������������", 100, false);
            columnList1.Add("����������ʶ", 150, false);
            columnList1.Add("ִ��״̬", 70, false);
            columnList1.Add("δִ��ԭ��", 200, "δ����ִ��ԭ��˵��");
        }

        public override bool OnPageCreate()
        {
            if(summary == null)
            {
                summary = new TestResultSummary(pid, currentvid);
                summary.OnCreate();
            }

            TestcaseUnExecutedListVisitClass vc = new TestcaseUnExecutedListVisitClass();
            summary[id].DoVisit(vc.GetUnExecuteTestcaseList);
            dt = vc.dt;

            flexAssist1.DataSource = dt;
            flexAssist1.OnPageCreate();
            FlexGridAssist.AutoSizeRows(flex1, 4);
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            if(dt == null) return true;
            flexAssist1.OnPageClose();
            return dbProject.UpdateDatabase(dt, "select ID, δִ��ԭ�� from CA��������ʵ���");
        }

        void OnRowNavigate(int row, int col, Row r)
        {
            MainForm.mainFrm.DelayCreateForm("zxd.TestTreeForm?type=result&id=" + r["ID"]);
        }
    }

    public class TestcaseUnExecutedListVisitClass
    {
        public DataTable dt;

        public TestcaseUnExecutedListVisitClass()
        {
            dt = new DataTable();
            GridAssist.AddColumn(dt, "ID", "��������ID", "������������", "����������ʶ", "ִ��״̬", "δִ��ԭ��");
            GridAssist.AddColumn<int>(dt, "���");
        }

        public void GetUnExecuteTestcaseList(ItemNodeTree item)
        {
            DataRow drItem = item.dr;
            if(item.nodeType != NodeType.TestCase) return;
            if(item.IsShortCut) return;

            if("����ִ��".Equals(drItem["ִ��״̬"])) return;
            DataRow dr = dt.Rows.Add();
            dr["ID"] = drItem["ID"];    // ����ID
            dr["��������ID"] = drItem["��������ID"];  // ʵ��ID
            dr["���"] = dt.Rows.Count;
            dr["������������"] = drItem["������������"];
            dr["����������ʶ"] = item.GetItemSign();
            dr["ִ��״̬"] = drItem["ִ��״̬"];
            dr["δִ��ԭ��"] = drItem["δִ��ԭ��"];
            dr.AcceptChanges();
        }
    }
}
