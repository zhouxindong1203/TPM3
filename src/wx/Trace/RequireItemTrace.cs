using System;
using System.Data;
using System.Drawing;
using Common;
using TPM3.Sys;
using C1.Win.C1FlexGrid;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.wx
{
    /// <summary>
    /// ��������Ŀ׷�ٹ�ϵ
    /// </summary>
    [TypeNameMap("wx.RequireItemTrace")]
    public partial class RequireItemTrace : MyUserControl
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<RequireItemTrace>(1);
        protected FlexGridAssist flexAssist1;
        protected bool GroupByDoc = RequireTreeForm.GroupByDoc;

        public RequireItemTrace()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            bkcr1 = flex1.Styles.Alternate.BackColor;
            bkcr2 = flex1.Styles.Normal.BackColor;
            flex1.AddMapCol("����ID", "��������", "��������˵��", "�Ƿ�׷��");
            flex1.AddMapCol("�����������ID", "�����������");

            flexAssist1 = new FlexGridAssist(flex1, null, null);
            flexAssist1.columnList = columnList1;
            //flexAssist1.AddHyperColumn("���������½�");
            //flexAssist1.AddHyperColumn("������������");
            //flexAssist1.RowNavigate += OnRowNavigate;
            label3.Text = "��������-������׷�ٱ�";
            checkBox1.Text = "����ʾû��׷�ٵ�������Ĳ�������";
            if(GroupByDoc)
                keyIndexCol = "��������������";
        }

        static RequireItemTrace()
        {
            columnList1.Add("�����������", 100);
            columnList1.Add("��������", 150, RequireTreeForm.title1);
            columnList1.Add("��������˵��", 170, RequireTreeForm.title2);
            columnList1.Add("�Ƿ�׷��", 70);
            columnList1.Add("��Ŀ�����½ں�", 100, "�������������е��½�");
            columnList1.Add("��Ŀ�ƻ��½ں�", 100, "�������ڼƻ��е��½�");
            //columnList1.Add("��Ŀ��ʶ", 150, "�������ʶ");
            columnList1.Add("��Ŀ����", 150, "���������ƻ�׷��ԭ��˵��");
        }

        public override bool OnPageCreate()
        {
            flex1.DataSource = GetRequireItemTraceView();
            AfterInitFlex();
            return true;
        }

        protected virtual void AfterInitFlex()
        {
            AddMergeColumn(flex1, "��������", "��������˵��", "�����������", "�Ƿ�׷��");
            flexAssist1.OnPageCreate();
            flex1.Cols["�����������"].Visible = GroupByDoc;
            FlexGridAssist.AutoSizeRows(flex1, 2);
        }

        public static void SetDataViewFilter(DataView dv)
        {
            string s = dv.RowFilter ?? "";
            if(s != "") s += " and ";
            s += "((��ĿID is null and �Ƿ�׷�� = true ) or ����ID is null)";
            dv.RowFilter = s;
        }

        public override bool OnPageClose(bool bClose)
        {
            flexAssist1.OnPageClose();
            return true;
        }

        public static DataView GetRequireItemTraceView()
        {
            TestResultSummary summary = new TestResultSummary(pid, currentvid);
            summary.OnCreate();
            RequireItemTraceVisitClass vc = new RequireItemTraceVisitClass(false, currentvid);
            summary.DoVisit(vc.GetRequireTraceInfo);
            vc.AddUnusedRequire();
            DataView dv = new DataView(vc.dtTrace);
            dv.Sort = "�����������, ��Ŀ���";
            if(RequireTreeForm.GroupByDoc)  // �ĵ���ܱ�����
                dv.RowFilter = "����������� is not null";
            return dv;
        }

        Color bkcr1, bkcr2;
        protected string keyIndexCol = "�����������";
        void flex1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if(e.Row < flex1.Rows.Fixed || e.Col < flex1.Cols.Fixed) return;
            if(keyIndexCol != null)
            {
                int index = (int)flex1[e.Row, keyIndexCol];
                Color bkcr = (index % 2) == 0 ? bkcr1 : bkcr2;
                e.Style.BackColor = bkcr;
            }

            e.Style.ForeColor = Color.Black;
            DataRowView drv = flex1.Rows[e.Row].DataSource as DataRowView;
            string name = flex1.Cols[e.Col].Name;
            if(name == "��������" || name == "�����������") return;

            if(name == "�Ƿ�׷��")
            {
                int image = true.Equals(drv["�Ƿ�׷��"]) ? 1 : 0;
                e.Image = ImageForm._imageForm.ilYesNo.Images[image];
                e.Text = "";
                return;
            }

            if((bool)drv["warning"])
                e.Style.ForeColor = Color.Red;
        }

        string oldfilter = null;
        void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            flexAssist1.OnPageClose();

            DataView dv = flex1.DataSource as DataView;
            if(oldfilter == null) oldfilter = dv.RowFilter ?? "";
            if(checkBox1.Checked)
                SetDataViewFilter(dv);
            else
                dv.RowFilter = oldfilter;

            flexAssist1.DataSource = dv;
            AfterInitFlex();
        }
    }

    [TypeNameMap("wx.ItemRequireTrace")]
    public class ItemRequireTrace : RequireItemTrace
    {
        static ColumnPropList columnList2 = GridAssist.GetColumnPropList<ItemRequireTrace>(1);

        public ItemRequireTrace()
        {
            flex1._colName = "����ID";
            flex1.AddMapCol("��ĿID", "��Ŀ�ƻ��½ں�", "��Ŀ�����½ں�", "��Ŀ��ʶ", "��Ŀ����");
            flexAssist1.columnList = columnList2;
            keyIndexCol = "��Ŀ���";
            label3.Text = "������-��������׷�ٱ�";
            checkBox1.Text = "����ʾû��׷�ٵ��������ݵĲ�����";
        }

        static ItemRequireTrace()
        {
            columnList2.Add("��������", 100);
            columnList2.Add("��Ŀ�����½ں�", 75, "�������������е��½�");
            columnList2.Add("��Ŀ�ƻ��½ں�", 75, "�������ڼƻ��е��½�");
            //columnList2.Add("��Ŀ��ʶ", 150, "�������ʶ");
            columnList2.Add("��Ŀ����", 150, "����������");
            columnList2.Add("��������", 150, RequireTreeForm.title1);
            columnList2.Add("��������˵��", 170, RequireTreeForm.title2);
        }

        public override bool OnPageCreate()
        {
            flex1.DataSource = GetItemRequireTraceView();
            AfterInitFlex();
            return true;
        }

        protected override void AfterInitFlex()
        {
            AddMergeColumn(flex1, "��������", "��Ŀ�ƻ��½ں�", "��Ŀ�����½ں�", "��Ŀ��ʶ", "��Ŀ����");
            flexAssist1.OnPageCreate();
            FlexGridAssist.AutoSizeRows(flex1, 4);
        }

        public static DataView GetItemRequireTraceView()
        {
            TestResultSummary summary = new TestResultSummary(pid, currentvid);
            summary.OnCreate();
            RequireItemTraceVisitClass vc = new RequireItemTraceVisitClass(true, currentvid);
            summary.DoVisit(vc.GetRequireTraceInfo);
            DataView dv = new DataView(vc.dtTrace);
            dv.Sort = "��Ŀ���, �����������";
            return dv;
        }
    }

    public class RequireItemTraceVisitClass : BaseProjectClass
    {
        public DataTable dtRequire, dtTrace;
        DataTableMap dtmRequire;

        bool itemRequireTrace;
        public RequireItemTraceVisitClass(bool itemRequireTrace, object vid)
        {
            this.itemRequireTrace = itemRequireTrace;
            dtRequire = RequireTreeForm.GetRequireTreeTable(itemRequireTrace, vid);
            dtRequire.Columns.Add("used", typeof(int));    // �Ƿ���ӣ�1: ��Ч��2: �����
            dtmRequire = new DataTableMap(dtRequire, "ID");
            foreach(DataRow dr in dtRequire.Rows)
                dr["used"] = 1;

            dtTrace = new DataTable();
            GridAssist.AddColumn(dtTrace, "����ID", "��������", "��������˵��", "�����������ID", "�����������");
            GridAssist.AddColumn<bool>(dtTrace, "�Ƿ�׷��", "warning");
            GridAssist.AddColumn<int>(dtTrace, "�����������", "��������������", "��Ŀ���");    // ��Ŀ���:����ţ�������
            GridAssist.AddColumn(dtTrace, "����ID", "��������", "��ĿID", "��Ŀ����", "��Ŀ��ʶ", "��Ŀ�ƻ��½ں�", "��Ŀ�����½ں�");

            dtTrace.Columns["warning"].DefaultValue = false;
        }

        int itemIndex = 1;

        public void GetRequireTraceInfo(ItemNodeTree item)
        {
            if(item.nodeType != NodeType.TestItem) return;
            ItemNodeTree itemObject = item.GetFarestParent(NodeType.TestObject);

            DataRow drItem = item.dr;
            itemIndex++;

            DataColumnCollection dcc = dtTrace.Columns;
            dcc["����ID"].DefaultValue = itemObject.id;
            dcc["��������"].DefaultValue = itemObject.name;
            dcc["��ĿID"].DefaultValue = item.id;
            dcc["��Ŀ���"].DefaultValue = itemIndex;
            dcc["��Ŀ����"].DefaultValue = item.name;
            dcc["��Ŀ��ʶ"].DefaultValue = item.GetItemSign();
            dcc["��Ŀ�ƻ��½ں�"].DefaultValue = item.GetItemChapter(1);
            dcc["��Ŀ�����½ں�"].DefaultValue = item.GetItemChapter(0);

            bool findRequire = false;
            foreach(string req in KeyList.SplitKey(drItem["׷�ٹ�ϵ"]))
            {
                DataRow drRequire = dtmRequire.GetRow(req);
                if(drRequire == null) continue;
                if(!(bool)drRequire["�Ƿ�׷��"]) continue; // �������󲻿ɱ�׷��

                drRequire["used"] = 2;
                findRequire = true;
                DataRow drTrace = dtTrace.Rows.Add();
                drTrace["����ID"] = drRequire["ID"];
                foreach(string col in new[] { "�����������", "��������", "��������˵��", "�����������ID", "�����������", "��������������", "�Ƿ�׷��" })
                    drTrace[col] = drRequire[col];
            }

            if(itemRequireTrace && !findRequire)  // ���û���������Ŀ
            {
                DataRow drTrace = dtTrace.Rows.Add();
                drTrace["��������"] = "�ô�û�в������ݣ�";
                drTrace["warning"] = true;
            }
        }

        /// <summary>
        /// �������δ�����õ�������
        /// </summary>
        public void AddUnusedRequire()
        {
            foreach(DataRow drRequire in dtRequire.Rows)
            {
                if(!Equals(drRequire["used"], 1)) continue;
                if(Equals(drRequire["���ڵ�ID"], GlobalData.rootID)) continue;  // ���ڵ㲻��
                DataRow drTrace = dtTrace.Rows.Add();
                drTrace["����ID"] = drRequire["ID"];
                foreach(string col in new[] { "�����������", "��������", "��������˵��", "�����������ID", "�����������", "��������������", "�Ƿ�׷��" })
                    drTrace[col] = drRequire[col];

                if((bool)drRequire["�Ƿ�׷��"])
                {
                    drTrace["��Ŀ����"] = "δ�����õ�������";
                    drTrace["warning"] = true;
                }
                else if(GridAssist.IsNull(drRequire["δ׷��ԭ��˵��"]))
                {
                    drTrace["��Ŀ����"] = "δ��д��׷��ԭ��˵��";
                    drTrace["warning"] = true;
                }
                else
                {
                    drTrace["��Ŀ����"] = drRequire["δ׷��ԭ��˵��"];
                    drTrace["warning"] = false;
                }

                foreach(string col in new[] { "����ID", "��������", "��ĿID", "��Ŀ���", "��Ŀ��ʶ", "��Ŀ�ƻ��½ں�", "��Ŀ�����½ں�" })
                    drTrace[col] = DBNull.Value;
            }
        }
    }
}
