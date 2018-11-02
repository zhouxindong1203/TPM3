using System;
using System.Xml;
using Common;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// OleRichTextBox
    /// title属性为空时表示和content属性相同的值
    /// </summary>
    [TypeNameMap("wx.Ole1Form")]
    public partial class Ole1Form : MyBaseForm
    {
        public Ole1Form()
        {
            InitializeComponent();
        }

        string title, content;
        private void Ole1Form_Load(object sender, EventArgs e)
        {
            XmlElement ele = docTN.nodeElement;
            title = ele.GetAttribute("title");
            content = ele.GetAttribute("content");

            if( string.IsNullOrEmpty(content) ) content = docTN.nodeName;
            if( string.IsNullOrEmpty(title) ) title = content;
            label2.Text = title;

            rich1.SetRichData(ProjectInfo.GetDocContent(dbProject, pid, currentvid, null, content));
        }

        public override bool OnPageClose(bool bClose)
        {
            return ProjectInfo.SetDocContent(dbProject, pid, currentvid, null, content, rich1.GetRichData());
        }
    }
}