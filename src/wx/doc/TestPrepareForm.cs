using System.Data;
using Common;

namespace TPM3.wx
{
    /// <summary>
    /// ��ʽ�ϸ��Բ���׼��
    /// </summary>
    [TypeNameMap("wx.TestPrepareForm")]
    public partial class TestPrepareForm : MyUserControl
    {
        public TestPrepareForm()
        {
            InitializeComponent();
        }

        public override bool OnPageCreate()
        {
            if( id == null ) return true;
            if( summary == null )
            {
                summary = new TestResultSummary(pid, currentvid);
                summary.OnCreate();
            }

            DataRow dr = summary[id].dr;
            rich1.SetRichData(dr["���Խ���"] as byte[]);
            rich2.SetRichData(dr["Ӳ��׼��"] as byte[]);
            rich3.SetRichData(dr["���׼��"] as byte[]);
            rich4.SetRichData(dr["��������׼��"] as byte[]);
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            if( id == null ) return true;
            DataRow dr = summary[id].dr;
            if( dr == null ) return true;

            if( rich1.Changed ) dr["���Խ���"] = rich1.GetRichData();
            if( rich2.Changed ) dr["Ӳ��׼��"] = rich2.GetRichData();
            if( rich3.Changed ) dr["���׼��"] = rich3.GetRichData();
            if( rich4.Changed ) dr["��������׼��"] = rich4.GetRichData();
            return dbProject.UpdateDatabase(summary[id].dr.Table, "select ID, ���Խ���, Ӳ��׼��, ���׼��, ��������׼�� from CA�������ʵ���");

        }
    }
}