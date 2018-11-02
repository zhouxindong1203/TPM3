using System.Data;
using Common;

namespace TPM3.wx
{
    /// <summary>
    /// 正式合格性测试准备
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
            rich1.SetRichData(dr["测试进度"] as byte[]);
            rich2.SetRichData(dr["硬件准备"] as byte[]);
            rich3.SetRichData(dr["软件准备"] as byte[]);
            rich4.SetRichData(dr["其他测试准备"] as byte[]);
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            if( id == null ) return true;
            DataRow dr = summary[id].dr;
            if( dr == null ) return true;

            if( rich1.Changed ) dr["测试进度"] = rich1.GetRichData();
            if( rich2.Changed ) dr["硬件准备"] = rich2.GetRichData();
            if( rich3.Changed ) dr["软件准备"] = rich3.GetRichData();
            if( rich4.Changed ) dr["其他测试准备"] = rich4.GetRichData();
            return dbProject.UpdateDatabase(summary[id].dr.Table, "select ID, 测试进度, 硬件准备, 软件准备, 其他测试准备 from CA被测对象实测表");

        }
    }
}