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
    /// ���Ի�������
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
            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "���");
            gridAssist1.columnList = columnList1;

            map[rich1] = "���Ի�������";
            map[rich2] = "��Ч����������";
            map[rich4] = "��װ���������";   // ���Ի�����װ��Ϊ���ּ���
            map[rich5] = "���Ի�������";
            map[rich6] = "���Ի�������";
        }

        static TestEnviForm()
        {
            columnList1.Add("���", 60);
            columnList1.Add("����", 100);
            columnList1.Add("����", 120);
            columnList1.Add("����", 60);
            columnList1.Add("˵��", 150);
            columnList1.Add("ά����", 90);
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