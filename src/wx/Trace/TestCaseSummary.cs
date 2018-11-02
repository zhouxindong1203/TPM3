using System;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using Common;
using C1.Win.C1FlexGrid;
using TPM3.Sys;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.wx
{
    /// <summary>
    /// ���������ּ�ͳ�Ʊ�
    /// </summary>
    [TypeNameMap("wx.TestCaseSummary")]
    public partial class TestCaseSummary : MyUserControl
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestCaseSummary>();
        FlexGridAssist flexAssist1;

        public TestCaseSummary()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            flexAssist1 = new FlexGridAssist(flex1, null, null);
            flexAssist1.columnList = columnList1;
            flex1.AllowDragging = AllowDraggingEnum.Columns;
        }

        static TestCaseSummary()
        {
            columnList1.Add("����", 220);
            columnList1.Add("��Ŀ��", 40, "������");
            columnList1.Add("���Խ��", 70, "������ϸ����");
            columnList1.Add("��������", 55, "������");
            columnList1.Add("������������", 53, "����������");
            columnList1.Add("����ִ�н�����", 100);
            columnList1.Add("ִ����", 55, "ִ�е�����");
            columnList1.Add("����ִ����", 40, "����ִ��");
            columnList1.Add("δִ����", 40, "δִ��");
            columnList1.Add("ִ��ͨ��", 40);
            columnList1.Add("ִ��δͨ��", 55);
            columnList1.Add("������", 40, "���Ե�");
            //columnList1.Add("����ִ��ͨ��", 100);
            columnList1.Add("����ִ��δͨ��", 72);
            columnList1["����"].TextAlign = CommonTextAlignEnum.Near;
        }

        public override bool OnPageCreate()
        {
            if(summary == null)
            {
                summary = new TestResultSummary(pid, currentvid);
                summary.OnCreate();
            }
            // �����и�Ϊ����
            flex1.Rows[0].Height = 40;
            // ����InitFlex����
            comboBox1.SelectedIndex = 0;
            return true;
        }

        void InitFlex()
        {
            flex1.BeginInit();
            flex1.Cols.Count = flex1.Cols.Fixed;
            flex1.Rows.Count = flex1.Rows.Fixed;

            Column c = flex1.Cols.Add();
            c.Name = c.Caption = "����";

            foreach(string cn in FlexTreeClass.cols)
            {
                c = flex1.Cols.Add();
                c.Name = c.Caption = cn;
                c.TextAlign = c.TextAlignFixed = TextAlignEnum.CenterCenter;
            }
            c = flex1.Cols.Add();
            c.Name = c.Caption = "tip";
            c.Visible = false;

            c = flex1.Cols.Add();
            c.Name = c.Caption = "NodeType";
            flex1.Cols["���Խ��"].ImageAlign = ImageAlignEnum.CenterCenter;

            c = flex1.Cols.Add();
            c.Name = c.Caption = "����ִ�н�����";

            FlexTreeClass ftc = new FlexTreeClass(flex1, comboBox1.SelectedIndex) { includeShortcut = cbShortCut.Checked };
            summary.DoVisit(ftc.AddTreeNode);
            flex1.EndInit();

            // create manager to display/edit the cell notes
            //CellNoteManager mgr = new CellNoteManager(flex1);

            flexAssist1.OnPageCreate();
        }

        /// <summary>
        /// ���ز�����Ŀͳ����Ϣ
        /// </summary>
        public static DataTable GetCountTable(object oid)
        {
            TestResultSummary summary = new TestResultSummary(pid, currentvid);
            summary.OnCreate();
            ItemNodeTree nt = oid == null ? summary : summary[oid];
            if(nt == null) return null;

            FlexTreeClass ic = new FlexTreeClass(new DataTable());
            nt.DoVisit(ic.GetDataRow);
            return ic.dt;
        }

        public override bool OnPageClose(bool bClose)
        {
            flexAssist1.OnPageClose();
            return true;
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        void cbShortCut_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDisplay();
        }

        void RefreshDisplay()
        {
            flexAssist1.OnPageClose();
            InitFlex();
        }

        void flex1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if(e.Row < flex1.Rows.Fixed) return;
            string name = flex1.Cols[e.Col].Name;
            if(name == "����ִ�н�����")
                DrawRectangle2(e);
            if(name == "���Խ��")
            {
                NodeType nt = (NodeType)(int)flex1[e.Row, "NodeType"];
                if(nt == NodeType.TestItem)
                {
                    int index = (int)flex1[e.Row, e.Col];
                    int image = 2;
                    if(index == 1) image = 0;
                    if(index == 3) image = 1;
                    e.Image = imageList1.Images[image];
                }
                e.Text = "";
            }
        }

        void DrawRectangle(OwnerDrawCellEventArgs e)
        {
            Row r = flex1.Rows[e.Row];
            Column c = flex1.Cols[e.Col];
            if(c.WidthDisplay <= 5) return;

            int max = (int)flex1[flex1.Rows.Fixed, e.Col]; // ��һ��Ϊ����
            if(max == 0) return; // ��ֹ�����
            Rectangle rc = e.Bounds;
            int value = (int)r[e.Col];
            if(value == 0) return;
            rc.Width = ((c.WidthDisplay - 4) * value / max);

            // draw background
            e.DrawCell(DrawCellFlags.Background | DrawCellFlags.Border);

            // draw bar
            rc.Inflate(0, -4);
            rc.X += 1;
            rc.Width += 1;
            e.Graphics.FillRectangle(Brushes.Red, rc);
            rc.Inflate(0, -1);
            rc.X += 1;
            rc.Width -= 2;
            e.Graphics.FillRectangle(Brushes.LightPink, rc);  // ��������

            //int height = rc.Height / 3;
            //rc.Y += height+1;
            //rc.Height -= height+1;
            //e.Graphics.FillRectangle(Brushes.Green, rc);

            //Rectangle rc2 = rc;
            //value = (int)r["ִ����"];
            //int width = (int)(c.WidthDisplay * value / max);
            //rc2.X += width;
            //rc2.Width -= width;
            //e.Graphics.FillRectangle(Brushes.Blue, rc2);

            //value = (int)r["����ִ����"];
            //width = (int)(c.WidthDisplay * value / max);
            //rc2.X += width;
            //rc2.Width -= width;
            //e.Graphics.FillRectangle(Brushes.Red, rc2);

            //rc.Y += height;
            //rc.Height -= height;
            //e.Graphics.FillRectangle(Brushes.Green, rc);

            //rc2 = rc;
            //value = (int)r["ִ��ͨ��"];
            //width = (int)(c.WidthDisplay * value / max);
            //rc2.X += width;
            //rc2.Width -= width;
            //e.Graphics.FillRectangle(Brushes.Red, rc2); // ִ��δͨ��

            //value = (int)r["ִ��δͨ��"];
            //width = (int)(c.WidthDisplay * value / max);
            //rc2.X += width;
            //rc2.Width -= width;
            //e.Graphics.FillRectangle(Brushes.LightPink, rc2); // ����ִ��δͨ��


            //value = (int)r["����ִ��δͨ��"];
            //width = (int)(c.WidthDisplay * value / max);
            //rc2.X += width;
            //rc2.Width -= width;
            //e.Graphics.FillRectangle(Brushes.LightGray, rc2); // ִ��δͨ��

            // draw cell content
            e.DrawCell(DrawCellFlags.Content);
        }

        /// <summary>
        /// ��ʾִ�е�������ռ�ı���
        /// </summary>
        void DrawRectangle2(OwnerDrawCellEventArgs e)
        {
            Row r = flex1.Rows[e.Row];
            Column c = flex1.Cols[e.Col];
            if(c.WidthDisplay <= 5) return;

            int total = cbShortCut.Checked ? (int)r["��������"] : (int)r["������������"]; // ��������
            int execute = (int)r["ִ����"];
            int pass = (int)r["ִ��ͨ��"];
            if(total == 0) return; // ��ֹ�����

            // draw background
            e.DrawCell(DrawCellFlags.Background | DrawCellFlags.Border);

            VoidFunc<int, Brush> DrawFunc = delegate(int width, Brush br)
            {
                Rectangle rc = e.Bounds;
                rc.Width = ((c.WidthDisplay - 2) * width / total);
                // draw bar
                rc.Inflate(0, -4);
                //rc.X += 1;
                rc.Width += 1;
                e.Graphics.FillRectangle(Brushes.Red, rc);
                rc.Inflate(0, -1);
                rc.X += 1;
                rc.Width -= 2;
                e.Graphics.FillRectangle(br, rc);  // ��������
            };

            DrawFunc(total, Brushes.Yellow);
            DrawFunc(execute, Brushes.LightPink);
            if(pass > 0) DrawFunc(pass, Brushes.LightGreen);
            e.DrawCell(DrawCellFlags.Content);
        }

        void flex1_MouseEnterCell(object sender, RowColEventArgs e)
        {
            this.C1SuperTooltip1.Hide(flex1);
            Row r = flex1.Rows[e.Row];
            string msg = null;

            if(flex1.Cols[e.Col].Name == "����ִ�н�����")
            {
                if(e.Row < flex1.Rows.Fixed)
                    msg = "��ɫ��ʾִ��ͨ����<br/>��ɫ��ʾִ��δͨ����<br/>��ɫ��ʾδִ����";
                else
                    msg = r["tip"] as string;
            }
            if(msg != null)
                this.C1SuperTooltip1.SetToolTip(flex1, msg);
        }
    }

    public class FlexTreeClass : BaseProjectClass
    {
        /// <summary>
        /// [����ID��������]
        /// </summary>
        Dictionary<object, int> stepCountMap = new Dictionary<object, int>();

        /// <summary>
        /// ͳ�Ƶ��������Ƿ������ݷ�ʽ
        /// </summary>
        public bool includeShortcut = false;

        /// <summary>
        /// ��ʼ�����Բ����
        /// </summary>
        void InitTestStep()
        {
            string sql = "SELECT ��������ID, Count(��������ID) AS ���� FROM CA���Թ���ʵ��� where ��ĿID = ? GROUP BY ��������ID";
            DataTable dtStep = ExecuteDataTable(sql, pid);
            foreach(DataRow dr in dtStep.Rows)
                stepCountMap[dr["��������ID"]] = (int)dr["����"];
        }

        C1FlexGrid flex;
        /// <summary>
        /// ͳ�Ƽ���0 ���Զ���1 ���Է��࣬ 2 ������
        /// </summary>
        int summerLevel;

        public static string[] cols = { "��������", "ִ����","����ִ����", "δִ����",
            "ִ��ͨ��", "ִ��δͨ��", "����ִ��ͨ��", "����ִ��δͨ��","������������", "��Ŀ��","������", "���Խ��" };

        /// <summary>
        /// ���캯������Ŀ��
        /// </summary>
        public FlexTreeClass(C1FlexGrid flex, int summerLevel)
        {
            this.flex = flex;
            this.summerLevel = summerLevel;
            InitTestStep();
        }

        public void AddTreeNode(ItemNodeTree item)
        {
            if(item.nodeType == NodeType.TestCase) return;
            if(item.nodeType == NodeType.TestItem && summerLevel < 2) return;
            if(item.nodeType == NodeType.TestType && summerLevel < 1) return;

            Row r = flex.Rows.Add();
            r["����"] = item.name;
            Image image = ImageForm.treeNodeImage.Images[item.GetIconName()];
            flex.SetCellImage(r.Index, flex.Cols["����"].Index, image);

            r.IsNode = true;
            r.Node.Level = item.GetLevel();

            ResultSummaryVisitClass vc = new ResultSummaryVisitClass { stepCountMap = stepCountMap, includeShortcut = includeShortcut };
            item.DoVisit(vc.GetCaseCount);
            int[] counts = vc.counts;
            for(int i = 0; i < cols.Length - 1; i++)    // �������һ��
                r[cols[i]] = counts[i];
            r["���Խ��"] = vc.TestResult;
            r["NodeType"] = (int)item.nodeType;

            // create cell notes for every employee
            int total = counts[8];
            string s = "", s2;
            s += "��������:<b> " + total + "</b><br/>";
            s += "<br/>";
            s += "ִ����:<b>" + counts[1] + "</b><br/>";

            s2 = "N/A";
            if(total > 0)
            {
                double f = 100.0f * counts[1] / total;
                s2 = f.ToString("0.#") + "%";
            }
            s += "ִ�б���:<b>" + s2 + "</b><br/>";
            s += "<br/>";

            s += "ִ��ͨ��: <b>" + counts[4] + "</b><br/>";

            s2 = "N/A";
            if(total > 0)
            {
                double f = 100.0f * counts[4] / total;
                s2 = f.ToString("0.#") + "%";
            }
            s += "ִ��ͨ������: <b>" + s2 + "</b>";

            r["tip"] = s;
        }

        public DataTable dt;

        /// <summary>
        /// ���캯��������ͳ�Ʊ�
        /// </summary>
        public FlexTreeClass(DataTable dt)
        {
            this.dt = dt;
            DataColumnCollection dcc = dt.Columns;
            for(int i = 0; i < cols.Length - 1; i++)    // �������һ��
                dcc.Add(cols[i], typeof(int));
            dcc.Add(cols[cols.Length - 1], typeof(int));
            GridAssist.AddColumn<int>(dt, "NodeType", "Level");
            GridAssist.AddColumn(dt, "ID", "����");
            InitTestStep();
        }

        public void GetDataRow(ItemNodeTree item)
        {
            if(item.nodeType == NodeType.TestCase) return;
            ResultSummaryVisitClass vc = new ResultSummaryVisitClass { stepCountMap = stepCountMap, includeShortcut = includeShortcut };
            item.DoVisit(vc.GetCaseCount);

            DataRow dr = dt.Rows.Add();
            for(int i = 0; i < cols.Length - 1; i++)    // �������һ��
                dr[cols[i]] = vc.counts[i];
            dr["���Խ��"] = vc.TestResult;
            dr["ID"] = item.id ?? DBNull.Value;
            dr["NodeType"] = (int)item.nodeType;
            dr["����"] = item.name;
            dr["Level"] = item.GetLevel();
        }
    }
}
