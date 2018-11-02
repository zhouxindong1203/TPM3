using System;
using Common;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// 测试策略
    /// </summary>
    [TypeNameMap("wx.TestStrategyForm")]
    public partial class TestStrategyForm : MyBaseForm
    {
        public TestStrategyForm()
        {
            InitializeComponent();
        }

        void TestStrategyForm_Load(object sender, EventArgs e)
        {
            rich1.SetRichData(ProjectInfo.GetDocContent(dbProject, pid, currentvid, null, "测试策略"));
            rich2.SetRichData(ProjectInfo.GetDocContent(dbProject, pid, currentvid, null, "测试策略2"));
            rich3.SetRichData(ProjectInfo.GetDocContent(dbProject, pid, currentvid, null, "测试策略3"));
            rich4.SetRichData(ProjectInfo.GetDocContent(dbProject, pid, currentvid, null, "测试策略4"));
        }

        public override bool OnPageClose(bool bClose)
        {
            if(!ProjectInfo.SetDocContent(dbProject, pid, currentvid, null, "测试策略", rich1.GetRichData())) return false;  // 保持兼容性
            if(!ProjectInfo.SetDocContent(dbProject, pid, currentvid, null, "测试策略2", rich2.GetRichData())) return false;
            if(!ProjectInfo.SetDocContent(dbProject, pid, currentvid, null, "测试策略3", rich3.GetRichData())) return false;
            if(!ProjectInfo.SetDocContent(dbProject, pid, currentvid, null, "测试策略4", rich4.GetRichData())) return false;
            return true;
        }
    }
}