using System;
using System.Data;
using System.Xml;
using System.Windows.Forms;
using Common;
using Common.TrueDBGrid;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// 文档概述
    /// </summary>
    [TypeNameMap("wx.SummaryForm")]
    public partial class SummaryForm : MyBaseForm
    {
        /// <summary>
        /// 是否显示"与其他文档的关系"
        /// </summary>
        //bool showTitle2
        //{
        //    get { return !splitContainer1.Panel2Collapsed; }
        //}

        DataTable dt1, dt2;
        TrueDBGridAssist gridAssist1, gridAssist2;

        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<SummaryForm>(13);
        static ColumnPropList columnList2 = GridAssist.GetColumnPropList<SummaryForm>(23);

        public SummaryForm()
        {
            InitializeComponent();
            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "序号") { columnList = columnList1 };
            gridAssist2 = new TrueDBGridAssist(grid2, "ID", "序号") { columnList = columnList2 };
            grid1.Dock = grid2.Dock = DockStyle.Fill;
            columnList1.RowHeight = columnList2.RowHeight = 30;
        }

        static SummaryForm()
        {
            columnList1.Add("序号", 60);
            columnList1.Add("引用文件标题", 180, "名称");
            columnList1.Add("引用文件文档号", 180, "标识");
            columnList1.Add("出版日期", 90, "日期");
            columnList1.Add("编写单位及作者", 120, "单位");

            columnList2.Add("序号", 60);
            columnList2.Add("术语和缩略语名", 180, "术语和缩略语");
            columnList2.Add("确切定义", 450, "说明");
        }

        void SummaryForm_Load(object sender, EventArgs e)
        {
            XmlElement ele = docTN.nodeElement;

            string docName2 = ele.GetAttribute("DocName");
            if(IsNull(docName2)) docName2 = docTN.documentName;
            textBox1.Text = ProjectInfo.GetDocString(dbProject, pid, currentvid, docName2, "文档概述");
            textBox2.Text = ProjectInfo.GetDocString(dbProject, pid, currentvid, docName2, "与其它文档的关系");
            rich1.SetRichData(ProjectInfo.GetDocContent(dbProject, pid, currentvid, docName2, "引用文档"));
            rich2.SetRichData(ProjectInfo.GetDocContent(dbProject, pid, currentvid, docName2, "术语和缩略语"));

            // 可用值：Word,列表
            rb11.Checked = ProjectInfo.GetDocString(dbProject, pid, currentvid, docName2, "引用文档选择") != "Word";
            rb12.Checked = !rb11.Checked;
            rb21.Checked = ProjectInfo.GetDocString(dbProject, pid, currentvid, docName2, "缩略语选择") != "Word";
            rb22.Checked = !rb21.Checked;
            rb11_CheckedChanged(null, null);

            dt1 = DBLayer1.GetReffileList(dbProject, pid, currentvid, docName2);
            if(dt1 == null) return;
            gridAssist1.DataSource = dt1;
            gridAssist1.OnPageCreate();

            dt2 = DBLayer1.GetAbbrevList(dbProject, pid, currentvid, docName2);
            if(dt2 == null) return;
            gridAssist2.DataSource = dt2;
            gridAssist2.OnPageCreate();
        }

        public override bool OnPageClose(bool bClose)
        {
            gridAssist1.OnPageClose();
            gridAssist2.OnPageClose();

            string docName2 = docTN.documentName;
            ProjectInfo.SetDocString(dbProject, pid, currentvid, docName2, "文档概述", textBox1.Text);
            ProjectInfo.SetDocString(dbProject, pid, currentvid, docName2, "与其它文档的关系", textBox2.Text);
            ProjectInfo.SetDocContent(dbProject, pid, currentvid, docName2, "引用文档", rich1.GetRichData());
            ProjectInfo.SetDocContent(dbProject, pid, currentvid, docName2, "术语和缩略语", rich2.GetRichData());

            ProjectInfo.SetDocString(dbProject, pid, currentvid, docName2, "引用文档选择", rb11.Checked ? "列表" : "Word");
            ProjectInfo.SetDocString(dbProject, pid, currentvid, docName2, "缩略语选择", rb21.Checked ? "列表" : "Word");

            if(!DBLayer1.UpdateReffileList(dbProject, dt1)) return false;
            if(!DBLayer1.UpdateAbbrevList(dbProject, dt2)) return false;

            return true;
        }

        void rb11_CheckedChanged(object sender, EventArgs e)
        {
            grid1.Visible = rb11.Checked;
            rich1.Visible = rb12.Checked;
            grid2.Visible = rb21.Checked;
            rich2.Visible = rb22.Checked;
        }

        void btAutoAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var f = new AutoAddReffileForm { dt = dt1, vid = currentvid, docname = docName };
            f.ShowDialog();
        }

        void btAutoAddAbbrv_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var f = new AutoAddAbbrvListForm { dt = dt2, pid = pid };
            f.ShowDialog();
        }
    }
}