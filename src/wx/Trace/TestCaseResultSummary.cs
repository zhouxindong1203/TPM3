using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Common;
using TPM3.Sys;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.wx
{
    /// <summary>
    /// ��������ִ�н���б�
    /// </summary>
    [TypeNameMap("wx.TestCaseResultSummary")]
    public partial class TestCaseResultSummary : MyUserControl
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestCaseResultSummary>();
        FlexGridAssist flexAssist1;

        public TestCaseResultSummary()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            flex1.Styles.Alternate.Clear(StyleElementFlags.BackColor);
            flexAssist1 = new FlexGridAssist(flex1, null, null) { columnList = columnList1 };
            flex1._colName = "���";
            flexAssist1.AddHyperColumn("������������");
            flexAssist1.AddHyperColumn("����������ʶ");
            flexAssist1.AddHyperColumn("���ⱨ�浥��ʶ");
            flexAssist1.RowNavigate += OnRowNavigate;
            flexAssist1._hyperColumn.ShowHyperColor = false;
            //flexAssist1._hyperColumn.ShowUnderLine = false;
        }

        static TestCaseResultSummary()
        {
            columnList1.Add("���", 50);
            columnList1.Add("������������", 100);
            columnList1.Add("����������ʶ", 150);
            columnList1.Add("���������½ں�", 100, "�½ں�(���Լ�¼)");
            columnList1.Add("ִ��״̬", 70);
            columnList1.Add("ִ�н��", 70);
            columnList1.Add("������", 60, "���Ե���");
            columnList1.Add("������", 70);
            columnList1.Add("���ⱨ�浥��ʶ", 120);
        }

        public override bool OnPageCreate()
        {
            InitFlex();
            return true;
        }

        void InitFlex()
        {
            if(summary == null)
            {
                summary = new TestResultSummary(pid, currentvid);
                summary.OnCreate();
            }

            TestcaseListVisitClass vc = new TestcaseListVisitClass();
            summary[id].DoVisit(vc.GetTestcaseResultList);

            flexAssist1.DataSource = vc.dtTestcase;
            flexAssist1.OnPageCreate();
            AddMergeColumn(flex1, "���", "������������", "����������ʶ", "���������½ں�", "ִ��״̬", "ִ�н��", "������");
            FlexGridAssist.AutoSizeRows(flex1, 4);
        }

        public override bool OnPageClose(bool bClose)
        {
            flexAssist1.OnPageClose();
            return true;
        }

        private void flex1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if(e.Col < flex1.Cols.Fixed || e.Row < flex1.Rows.Fixed) return;
            DataRowView drv = flex1.Rows[e.Row].DataSource as DataRowView;
            if(drv == null) return;
            string result = drv["ִ�н��"] as string;
            if(result == "δͨ��")
            {
                if(!flex1.Styles.Contains("CaseError"))
                {
                    CellStyle cs = flex1.Styles.Add("CaseError", flex1.Styles.Normal);
                    cs.ForeColor = Color.Red;
                }
                e.Style = flex1.Styles["CaseError"];
            }
            if(result == "ͨ��")
            {
                if(!flex1.Styles.Contains("CasePassed"))
                {
                    CellStyle cs = flex1.Styles.Add("CasePassed", flex1.Styles.Normal);
                    cs.ForeColor = Color.Green;
                }
                e.Style = flex1.Styles["CasePassed"];
            }
            if(e.Col == flex1.Cols.Fixed)
            {
                string imagekey = drv["imagekey"].ToString();
                e.Image = ImageForm.treeNodeImage.Images[imagekey];
            }
        }

        void OnRowNavigate(int row, int col, Row r)
        {
            string colName = flex1.Cols[col].Name;
            if(colName == "���ⱨ�浥��ʶ")
            {
                MainForm.mainFrm.DelayCreateForm("zxd.pbl.PblRepsForm?id=" + r["���ⱨ�浥ID"]);
            }
            else if(colName == "������������" || colName == "����������ʶ")
                MainForm.mainFrm.DelayCreateForm("zxd.TestTreeForm?type=result&id=" + r["��������ʵ��ID"]);
        }

        void llRepare_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TestcaseListVisitClass_Repare vc = new TestcaseListVisitClass_Repare();
            summary.DoVisit(vc.StartRepare);
            vc.OnSave();
            if(vc.repairCount > 0)
            {
                summary = null;
                InitFlex();
            }
            string msg = string.Format("�޸����ݿ�ɹ�,���޸�{0}����¼��", vc.repairCount);
            MessageBox.Show(msg);
        }
    }

    public class TestcaseListVisitClass_Repare : BaseProjectClass
    {
        DataTable dtTestcase;
        DataTableGroup dtgStep;
        DataTableMap dtmTestcase;
        string sqlCase2 = "select * from CA��������ʵ��� where ���԰汾 = ?";
        public int repairCount = 0;

        public TestcaseListVisitClass_Repare()
        {
            string sqlStep = @"SELECT sr.ID, sr.����ID, se.��������ID, sr.���, sr.���ⱨ�浥ID
                FROM CA���Թ���ʵ��� AS sr INNER JOIN CA���Թ���ʵ��� AS se ON sr.����ID = se.ID where sr.���԰汾 = ? order by sr.���";
            DataTable dtStep = ExecuteDataTable(sqlStep, currentvid);
            dtgStep = new DataTableGroup(dtStep, "��������ID");

            dtTestcase = ExecuteDataTable(sqlCase2, currentvid);
            dtmTestcase = new DataTableMap(dtTestcase, "ID");
        }

        public void StartRepare(ItemNodeTree item)
        {
            DataRow drItem = item.dr;
            if(item.nodeType != NodeType.TestCase) return;
            if(item.IsShortCut) return;

            DataRow drTestcase = dtmTestcase.GetRow(drItem["ID"]);
            if(drTestcase == null) return;

            string execute = drItem["ִ��״̬"].ToString();
            if(execute != "����ִ��") return;  // Ŀǰֻ��������ִ��

            // �Ӳ����л�ȡ״̬
            bool Ispassed = true;
            foreach(DataRow drStep in dtgStep.GetRowList(item.id))
            {
                if(GridAssist.IsNull(drStep["���ⱨ�浥ID"])) continue;
                Ispassed = false;
                break;
            }
            string passed = Ispassed ? "ͨ��" : "δͨ��";
            if(!Equals(drTestcase["ִ�н��"], passed)) repairCount++;
            GridAssist.SetRowValue(drTestcase, "ִ�н��", passed);
        }

        public void OnSave()
        {
            dbProject.UpdateDatabase(dtTestcase, sqlCase2);
        }
    }


    public class TestcaseListVisitClass : BaseProjectClass
    {
        public DataTable dtTestcase;
        public string splitter;
        DataTableGroup dtgStep;

        /// <summary>
        /// [fallid, fallSign]
        /// </summary>
        Dictionary<object, string> fallSignMap = new Dictionary<object, string>();

        public TestcaseListVisitClass()
        {
            dtTestcase = new DataTable();
            GridAssist.AddColumn(dtTestcase, "��������ID", "��������ʵ��ID", "������������", "����������ʶ",
               "���������½ں�", "ִ��״̬", "ִ�н��", "���ⱨ�浥ID", "���ⱨ�浥��ʶ", "imagekey");
            GridAssist.AddColumn<int>(dtTestcase, "���", "������", "������");

            //splitter = ProjectInfo.GetDocString(dbProject, pid, currentvid, null, "�����ʶ�ָ���");
            splitter = Z1.tpm.DB.CommonDB.GetPblSpl(dbProject, (string)pid, (string)currentvid);

            string sqlStep = @"SELECT sr.ID, sr.����ID, se.��������ID, sr.���, sr.���ⱨ�浥ID
                FROM CA���Թ���ʵ��� AS sr INNER JOIN CA���Թ���ʵ��� AS se ON sr.����ID = se.ID where sr.���԰汾 = ? order by sr.���";
            DataTable dtStep = ExecuteDataTable(sqlStep, currentvid);
            dtgStep = new DataTableGroup(dtStep, "��������ID");
        }

        int index = 1;
        public void GetTestcaseResultList(ItemNodeTree item)
        {
            DataRow drItem = item.dr;
            if(item.nodeType != NodeType.TestCase) return;
            if(item.IsShortCut) return;
            object cid = drItem["��������ID"];

            DataColumnCollection dcc = dtTestcase.Columns;
            dcc["��������ʵ��ID"].DefaultValue = drItem["ID"];
            dcc["��������ID"].DefaultValue = cid;
            dcc["���"].DefaultValue = index++;
            dcc["������"].DefaultValue = dtgStep.GetRowList(cid).Count;
            dcc["������������"].DefaultValue = drItem["������������"];
            dcc["����������ʶ"].DefaultValue = item.GetItemSign();
            dcc["���������½ں�"].DefaultValue = item.GetItemChapter(3);
            dcc["imagekey"].DefaultValue = item.GetIconName();
            string execute = drItem["ִ��״̬"].ToString(), passed = drItem["ִ�н��"].ToString();
            dcc["ִ��״̬"].DefaultValue = execute;
            if(execute == "δִ��") passed = "";
            if(execute == "����ִ��" && passed != "δͨ��") passed = "";
            dcc["ִ�н��"].DefaultValue = passed;

            bool Ispassed = true;
            foreach(DataRow drStep in dtgStep.GetRowList(item.id))
            {
                object fallid = drStep["���ⱨ�浥ID"];
                if(GridAssist.IsNull(fallid)) continue;
                DataRow dr = dtTestcase.Rows.Add();
                dr["������"] = drStep["���"];
                dr["���ⱨ�浥ID"] = fallid;
                dr["���ⱨ�浥��ʶ"] = GetFallSign(fallid);
                Ispassed = false;
            }
            if(Ispassed) dtTestcase.Rows.Add();
        }

        string GetFallSign(object fallid)
        {
            if(!fallSignMap.ContainsKey(fallid))
                fallSignMap[fallid] = Z1.tpm.DB.CommonDB.GenPblSignForStep(dbProject, splitter, fallid.ToString());
            return fallSignMap[fallid];
        }
    }
}
