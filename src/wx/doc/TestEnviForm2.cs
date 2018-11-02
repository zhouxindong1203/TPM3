using System;
using System.Collections.Generic;
using Common;
using Common.RichTextBox;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// 测试环境概述(定型测评大纲用)
    /// </summary>
    [TypeNameMap("wx.TestEnviForm2")]
    public partial class TestEnviForm2 : MyBaseForm
    {
        Dictionary<RichTextBoxOle, string> map = new Dictionary<RichTextBoxOle, string>();
        public TestEnviForm2()
        {
            InitializeComponent();
            map[rich1] = "测试环境概述";
            map[rich2] = "测评场所";
            map[rich3] = "测评数据";
            map[rich4] = "有效性与差异分析";
        }

        private void TestEnviForm2_Load(object sender, EventArgs e)
        {
            foreach(var de in map)
                de.Key.SetRichData(ProjectInfo.GetDocContent(dbProject, pid, currentvid, null, de.Value));
        }

        public override bool OnPageClose(bool bClose)
        {
            foreach(var de in map)
                ProjectInfo.SetDocContent(dbProject, pid, currentvid, null, de.Value, de.Key.GetRichData());
            return true;
        }
    }
} 