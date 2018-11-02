using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common.TrueDBGrid;
using Common;
using TPM3.Sys;
using Z1.tpm.DB;
using C1.Win.C1TrueDBGrid;

namespace TPM3.zxd.pbl
{
    /// <summary>
    /// ���������ʶ
    /// </summary> 
    [TypeNameMap("zxd.pbl.CustomPblSignForm")]
    public partial class CustomPblSignForm : MyBaseForm
    {
        #region ����

        DataTable dt1, dt2, dt3, dt4;
        TrueDBGridAssist gridAssist1, gridAssist2, gridAssist3, gridAssist4;

        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<CustomPblSignForm>(1);
        static ColumnPropList columnList2 = GridAssist.GetColumnPropList<CustomPblSignForm>(2);
        static ColumnPropList columnList3 = GridAssist.GetColumnPropList<CustomPblSignForm>(3);
        static ColumnPropList columnList4 = GridAssist.GetColumnPropList<CustomPblSignForm>(4);

        private Font _font;
        private Graphics _graphics;

        static CustomPblSignForm()
        {
            columnList1.Add("���", 50, false);
            columnList1.Add("��ʶ", 200);
            columnList1.Add("����", 350);
            columnList1.AddValidator(new NotNullValidator("��ʶ"));

            columnList2.Add("���", 50, false);
            columnList2.Add("��ʶ", 200);
            columnList2.Add("����", 350);
            columnList2.AddValidator(new NotNullValidator("��ʶ"));

            columnList3.Add("���", 50, false);
            columnList3.Add("��ʶ", 200);
            columnList3.Add("����", 350);
            columnList3.AddValidator(new NotNullValidator("��ʶ"));

            columnList4.Add("���", 50, false);
            columnList4.Add("��ʶ", 200);
            columnList4.Add("����", 350);
            columnList4.AddValidator(new NotNullValidator("��ʶ"));
        }

        public CustomPblSignForm()
        {
            InitializeComponent();

            gridAssist1 = new TrueDBGridAssist(c1TrueDBGrid1, null, "���");
            gridAssist1.columnList = columnList1;

            gridAssist2 = new TrueDBGridAssist(c1TrueDBGrid2, null, "���");
            gridAssist2.columnList = columnList2;

            gridAssist3 = new TrueDBGridAssist(c1TrueDBGrid3, null, "���");
            gridAssist3.columnList = columnList3;

            gridAssist4 = new TrueDBGridAssist(c1TrueDBGrid4, null, "���");
            gridAssist4.columnList = columnList4;
        }

        public override bool OnPageCreate()
        {
            dt1 = CommonDB.GetPblSignForLevel(dbProject, (string)pid, (string)currentvid, 1);
            gridAssist1.DataSource = dt1;
            gridAssist1.OnPageCreate();

            dt2 = CommonDB.GetPblSignForLevel(dbProject, (string)pid, (string)currentvid, 2);
            gridAssist2.DataSource = dt2;
            gridAssist2.OnPageCreate();

            dt3 = CommonDB.GetPblSignForLevel(dbProject, (string)pid, (string)currentvid, 3);
            gridAssist3.DataSource = dt3;
            gridAssist3.OnPageCreate();

            dt4 = CommonDB.GetPblSignForLevel(dbProject, (string)pid, (string)currentvid, 4);
            gridAssist4.DataSource = dt4;
            gridAssist4.OnPageCreate();

            return true;
        }

        private void CustomPblSignForm_Load(object sender, EventArgs e)
        {
            this._font = this.txtPblDemo.Font;
            this._graphics = this.txtPblDemo.CreateGraphics();

            this.txtSplitter.Text = CommonDB.GetPblSpl(dbProject, (string)pid, (string)currentvid);
            this.txtPblDemo.Text = BuildDemoSignsComb(this.txtSplitter.Text);
            this.txtPblDemo.Width = (int)_graphics.MeasureString(this.txtPblDemo.Text, _font).Width;

            FrmCommonFunc.UniformGrid(this.c1TrueDBGrid1);
            FrmCommonFunc.UniformGrid(this.c1TrueDBGrid2);
            FrmCommonFunc.UniformGrid(this.c1TrueDBGrid3);
            FrmCommonFunc.UniformGrid(this.c1TrueDBGrid4);
        }

        public override bool OnPageClose(bool bClose)
        {
            gridAssist1.OnPageClose();
            if(!CommonDB.UpdatePblSignForLevel(dbProject, dt1, (string)pid, (string)currentvid, 1))
                return false;

            gridAssist2.OnPageClose();
            if(!CommonDB.UpdatePblSignForLevel(dbProject, dt2, (string)pid, (string)currentvid, 2))
                return false;

            gridAssist3.OnPageClose();
            if(!CommonDB.UpdatePblSignForLevel(dbProject, dt3, (string)pid, (string)currentvid, 3))
                return false;

            gridAssist4.OnPageClose();
            if(!CommonDB.UpdatePblSignForLevel(dbProject, dt4, (string)pid, (string)currentvid, 4))
                return false;

            CommonDB.UpdatePblSpl(dbProject, (string)pid, (string)currentvid, this.txtSplitter.Text);

            return true;
        }

        #endregion ����

        #region �ڲ�����

        private string BuildDemoSignsComb(string splitter)
        {
            string[] strsign = new[] { string.Empty, string.Empty, string.Empty, string.Empty };

            if(c1TrueDBGrid1.RowCount > 0)
                strsign[0] = c1TrueDBGrid1[0, "��ʶ"].ToString();
            if(c1TrueDBGrid2.RowCount > 0)
                strsign[1] = c1TrueDBGrid2[0, "��ʶ"].ToString();
            if(c1TrueDBGrid3.RowCount > 0)
                strsign[2] = c1TrueDBGrid3[0, "��ʶ"].ToString();
            if(c1TrueDBGrid4.RowCount > 0)
                strsign[3] = c1TrueDBGrid4[0, "��ʶ"].ToString();

            StringBuilder strbld = new StringBuilder();
            bool bHead = false;

            foreach(string str in strsign)
            {
                if(!str.Equals(string.Empty))
                {
                    if(bHead)
                        strbld.Append(splitter);
                    bHead = true;
                    strbld.Append(str);
                }
            }

            if(bHead)
                strbld.Append(splitter);
            strbld.Append(1);

            return strbld.ToString();
        }

        #endregion �ڲ�����

        #region grid

        private void c1TrueDBGrid1_OnAddNew(object sender, EventArgs e)
        {
            GridOnAddNew(this.c1TrueDBGrid1, 1);
        }

        private void c1TrueDBGrid2_OnAddNew(object sender, EventArgs e)
        {
            GridOnAddNew(this.c1TrueDBGrid2, 2);
        }

        private void c1TrueDBGrid3_OnAddNew(object sender, EventArgs e)
        {
            GridOnAddNew(this.c1TrueDBGrid3, 3);
        }

        private void c1TrueDBGrid4_OnAddNew(object sender, EventArgs e)
        {
            GridOnAddNew(this.c1TrueDBGrid4, 4);
        }

        private void GridOnAddNew(C1TrueDBGrid grid, int level)
        {
            grid.Columns["ID"].Value = FunctionClass.NewGuid;
            grid.Columns["��ĿID"].Value = pid;
            grid.Columns["���԰汾"].Value = currentvid;
            grid.Columns["����"].Value = level;
        }

        #endregion grid

        #region �¼�����

        private void txtSplitter_TextChanged(object sender, EventArgs e)
        {
            this.txtPblDemo.Text = BuildDemoSignsComb(this.txtSplitter.Text);
            this.txtPblDemo.Width = (int)_graphics.MeasureString(this.txtPblDemo.Text, _font).Width;
        }

        private void c1TrueDBGrid1_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            e.Cancel = GridBeforeDelete(this.c1TrueDBGrid1, 1);
        }

        private void c1TrueDBGrid2_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            e.Cancel = GridBeforeDelete(this.c1TrueDBGrid2, 2);
        }

        private void c1TrueDBGrid3_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            e.Cancel = GridBeforeDelete(this.c1TrueDBGrid3, 3);
        }

        private void c1TrueDBGrid4_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            e.Cancel = GridBeforeDelete(this.c1TrueDBGrid4, 4);
        }

        private bool GridBeforeDelete(C1TrueDBGrid grid, int level)
        {
            string id = (string)grid.Columns["ID"].Value;
            bool hasuse = CommonDB.HasUsePblSign(dbProject, id, level);
            if(hasuse) // �����ʶ��ʹ��
            {
                MessageBox.Show("��\'�����ʶ\'��\'���ⱨ�浥\'����, ����ɾ��!!", "ɾ��ʧ��",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return true;
            }
            else
                return false;
        }

        #endregion �¼�����
    }
}