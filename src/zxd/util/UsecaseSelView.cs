using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common;
using Common.TrueDBGrid;
using Z1.tpm.DB;
using TPM3.Sys;
using C1.Win.C1TrueDBGrid;

namespace TPM3.zxd.util
{
    /// <summary>
    /// ����Ŀ�Ĳ�������ʵ����ʾ��ѡ�ô���
    /// </summary>
    public partial class UsecaseSelView : Form
    {
        private DataSet _ds = new DataSet();
        private DataTable _tblparent;
        private DataTable _tblchild;

        public UsecaseSelView()
        {
            InitializeComponent();

            _tblparent = TestUsecase.GetUsecasesEntityForProj(MyBaseForm.dbProject, (string)MyBaseForm.pid);
            _tblparent.TableName = "Entity";
            _tblchild = TestUsecase.GetUsecasesTestForProj(MyBaseForm.dbProject,
                (string)MyBaseForm.pid);
            _tblchild.TableName = "Test";

            this._tblparent.Columns.Add("ѡ��", typeof(bool));
            this._tblparent.Columns.Add("���", typeof(int));

            int index = 1;
            foreach (DataRow row in this._tblparent.Rows)
            {
                row["ѡ��"] = false;
                row["���"] = index++;
            }

            _ds.Tables.Add(_tblparent);
            _ds.Tables.Add(_tblchild);

            _ds.Relations.Add("et", _ds.Tables["Entity"].Columns["ID"],
                _ds.Tables["Test"].Columns["��������ID"]);

            grid1.ChildGrid = grid2;
        }

        private void UsecaseSelView_Load(object sender, EventArgs e)
        {
            grid1.DataSource = _ds;
            grid1.DataMember = "Entity";

            SetGridStyle();
            SetGrid1DisplayCol();
        }

        private void SetGridStyle()
        {
            FrmCommonFunc.UniformGrid(grid2, System.Drawing.Color.Black,
                System.Drawing.SystemColors.Control, 20, 20);
            FrmCommonFunc.UniformGrid(grid1, System.Drawing.Color.Black,
                System.Drawing.SystemColors.Control, 20, 20);

            //grid1.AlternatingRows = true;
            //grid2.AlternatingRows = true;

            //grid1.Styles["EvenRow"].BackColor = System.Drawing.Color.Honeydew;
            //grid1.Styles["OddRow"].BackColor = System.Drawing.Color.LavenderBlush;
            //grid2.Styles["EvenRow"].BackColor = System.Drawing.Color.Honeydew;
            //grid2.Styles["OddRow"].BackColor = System.Drawing.Color.LavenderBlush;

            grid1.AllowAddNew = false;
            grid1.AllowDelete = false;

            grid2.AllowAddNew = false;
            grid2.AllowDelete = false;

            grid1.AllowRowSizing = RowSizingEnum.IndividualRows;
        }

        private void SetGrid1DisplayCol()
        {
            C1DisplayColumn d1 = grid1.Splits[0].DisplayColumns["���"];
            C1DisplayColumn d2 = grid1.Splits[0].DisplayColumns["ѡ��"];
            C1DisplayColumn d3 = grid1.Splits[0].DisplayColumns["������������"];
            C1DisplayColumn d4 = grid1.Splits[0].DisplayColumns["��������"];
            C1DisplayColumn d5 = grid1.Splits[0].DisplayColumns["�����ĳ�ʼ��"];
            C1DisplayColumn d6 = grid1.Splits[0].DisplayColumns["ǰ���Լ��"];
            C1DisplayColumn d7 = grid1.Splits[0].DisplayColumns["���ʱ��"];

            grid1.Splits[0].DisplayColumns.Clear();
            grid1.Splits[0].DisplayColumns.Insert(0, d1);
            grid1.Splits[0].DisplayColumns.Insert(1, d2);
            grid1.Splits[0].DisplayColumns.Insert(2, d3);
            grid1.Splits[0].DisplayColumns.Insert(3, d4);
            grid1.Splits[0].DisplayColumns.Insert(4, d5);
            grid1.Splits[0].DisplayColumns.Insert(5, d6);
            grid1.Splits[0].DisplayColumns.Insert(6, d7);

            d1.Width = 30;
            d2.Width = 30;
            d3.Width = 160;
            d4.Width = 300;
            d5.Width = 260;
            d6.Width = 260;
            d7.Width = 100;

            foreach(C1DisplayColumn dc in grid1.Splits[0].DisplayColumns)
            {
                dc.Style.VerticalAlignment = AlignVertEnum.Center;
                dc.Style.WrapText = true;
            }

            grid1.MarqueeStyle = MarqueeEnum.SolidCellBorder;
            grid1.EditDropDown = true;
        }

        private void SetGrid2DisplayCol()
        {
            C1DisplayColumn d1 = grid2.Splits[0].DisplayColumns["���԰汾"];
            C1DisplayColumn d2 = grid2.Splits[0].DisplayColumns["����ʱ��"];
            C1DisplayColumn d3 = grid2.Splits[0].DisplayColumns["ִ��״̬"];
            C1DisplayColumn d4 = grid2.Splits[0].DisplayColumns["ִ�н��"];
            C1DisplayColumn d5 = grid2.Splits[0].DisplayColumns["δִ��ԭ��"];

            grid2.Splits[0].DisplayColumns.Clear();
            grid2.Splits[0].DisplayColumns.Insert(0, d1);
            grid2.Splits[0].DisplayColumns.Insert(1, d2);
            grid2.Splits[0].DisplayColumns.Insert(2, d3);
            grid2.Splits[0].DisplayColumns.Insert(3, d4);
            grid2.Splits[0].DisplayColumns.Insert(4, d5);

            d1.Width = 80;
            d2.Width = 100;
            d3.Width = 80;
            d4.Width = 80;
            d5.Width = 400;

            foreach (C1DisplayColumn dc in grid2.Splits[0].DisplayColumns)
            {
                dc.Style.VerticalAlignment = AlignVertEnum.Center;
                dc.Style.WrapText = true;
            }

            grid2.Width = d1.Width + d2.Width + d3.Width + d4.Width + d5.Width +
                grid2.Splits[0].RecordSelectorWidth + 8;

            grid2.MarqueeStyle = MarqueeEnum.SolidCellBorder;
            grid2.EditDropDown = true;
        }

        private void grid2_VisibleChanged(object sender, EventArgs e)
        {
            if (grid2.Visible)
            {
                object eid = grid1.Columns["ID"].Value;
                DataTable t = TestUsecase.GetUsecasesTest(MyBaseForm.dbProject,
                    (string)MyBaseForm.pid, (string)eid);

                grid2.DataSource = null;
                grid2.DataSource = t;

                SetGrid2DisplayCol();

                grid2.Height = t.Rows.Count * (grid2.RowHeight + 3) + 28;
            }
        }

        private void grid1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbWrapText_CheckedChanged(object sender, EventArgs e)
        {
            SetWrapText(grid1, cbWrapText.Checked);
            SetWrapText(grid2, cbWrapText.Checked);
        }

        private void SetWrapText(C1TrueDBGrid grid, bool wrap)
        {
            foreach (C1DisplayColumn dc in grid.Splits[0].DisplayColumns)
                dc.Style.WrapText = wrap;
        }
    }
}