using System;
using System.Collections.Generic;
using System.Data;
using Common;
using Common.RichTextBox;
using Common.TrueDBGrid;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// 测试环境概述
    /// </summary>
    [TypeNameMap("wx.TestEnviForm")]
    public partial class TestEnviForm : MyBaseForm
    {
        DataTable dt1;
        TrueDBGridAssist gridAssist1;
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestEnviForm>(1);

        Dictionary<RichTextBoxOle, string> map = new Dictionary<RichTextBoxOle, string>();

        public TestEnviForm()
        {
            InitializeComponent();
            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "序号");
            gridAssist1.columnList = columnList1;

            map[rich1] = "测试环境概述";
            map[rich2] = "有效性与差异分析";
            map[rich4] = "安装测试与控制";   // 测试环境安装，为保持兼容
            map[rich5] = "测试环境测试";
            map[rich6] = "测试环境控制";
        }

        static TestEnviForm()
        {
            columnList1.Add("序号", 60);
            columnList1.Add("名称", 100);
            columnList1.Add("配置", 120);
            columnList1.Add("数量", 60);
            columnList1.Add("说明", 150);
            columnList1.Add("维护人", 90);
        }

        private void TestEnviForm_Load(object sender, EventArgs e)
        {
            foreach(var de in map)
                de.Key.SetRichData(ProjectInfo.GetDocContent(dbProject, pid, currentvid, null, de.Value));

            dt1 = DBLayer1.GetResourceList(dbProject, pid, currentvid, null);
            if(dt1 == null) return;
            gridAssist1.DataSource = dt1;
            gridAssist1.OnPageCreate();
        }

        public override bool OnPageClose(bool bClose)
        {
            gridAssist1.OnPageClose();
            if(!DBLayer1.UpdateResourceList(dbProject, dt1)) return false;

            foreach(var de in map)
                ProjectInfo.SetDocContent(dbProject, pid, currentvid, null, de.Value, de.Key.GetRichData());
            return true;
        }
    }
}