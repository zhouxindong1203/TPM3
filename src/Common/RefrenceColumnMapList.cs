using System;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;
using Common.Database;

namespace Common.TrueDBGrid
{
    /// <summary>
    /// ���ӳ����֧����
    /// </summary>
    public class RefrenceColumnMapList : RefrenceColumnMapBase, IDisposable
    {
        C1TrueDBGrid grid;

        Split split
        {
            get { return grid.Splits[0]; }
        }

        /// <summary>
        /// 0 ��ʾ��������� FlexDropDown��ʽ�� 1 ��ʾ��������� ListBox��ʽ
        /// </summary>
        public static int DropDownType = 0;

        public RefrenceColumnMapList(C1TrueDBGrid grid)
        {
            this.grid = grid;
            // �Ի�����ʾ��Ӧ��֯��Ϣ
            grid.FormatText += grid_FormatText;
            grid.ButtonClick += grid_ButtonClick;
            //CellStyle s = flex.Styles.Add("RefrenceColumnMapList");
            //s.TextAlign = TextAlignEnum.LeftCenter;
            //s.WordWrap = true;
            //s.ComboList = "...";
        }
        
        object this[int row, string name]
        {
            get
            {
                if( row >= grid.RowCount )  return null;  // ��������
                DataRowView drv = grid[row] as DataRowView;
                if( drv == null ) return null;
                return drv[name];
            }
            set
            {
                DataRowView drv = grid[row] as DataRowView;
                drv[name] = value;
            }
        }

        public bool OnPageCreate()
        {
            DBAccess dbProject = GlobalDataBase.global.dbProject;
            DataTable dt = GridAssist.GetDataTable(grid.DataSource);
            if( dt == null ) return true;

            foreach( ColumnRefMap cm in columnMapList.Values )
            {
                cm.OnPageCreate();
                if( !string.IsNullOrEmpty(cm.flexSql) )
                    cm.DataSource = dbProject.ExecuteDataTable(cm.flexSql, cm.paramList);
                if( cm.dt == null || cm.RefrencedColumnName == null ) continue;

                C1DisplayColumn dc = TrueDBGridAssist.GetDisplayColumn(grid, cm.RefrencedColumnName);
                if( dc == null ) continue;
                dc.Button = !cm.readOnly;   // ֻ��ʱ����Ҫ������ť
                dc.Locked = !cm.allowUserInput;
                grid.Columns[cm.RefrencedColumnName].NumberFormat = "FormatText Event";
            }
            return true;
        }

        public bool OnPageClose()
        {
            return true;
        }

        void grid_FormatText(object sender, FormatTextEventArgs e)
        {
            int row = e.Row, col = e.ColIndex;
            string s, colName = e.Column.DataField;

            object obj = this[row, colName];
            if( GridAssist.IsNull(obj) ) return;

            Point pt = new Point(row, col);

            // �������ӳ���У���ֱ�ӷ���
            ColumnRefMap cm;
            if( columnMapList.ContainsKey(colName) )
                cm = columnMapList[colName];
            else if( columnMapList.ContainsKey(pt) )
                cm = columnMapList[pt];
            else
                return;

            DataTable dt = cm.dt;
            if( dt == null ) return;

            if( cm.multiSelect == false )   // ��ѡ
            {
                s = cm.GetDisplayString(obj);
                if( s == null )     //  �����ڶ�Ӧ����(�Ѵ����ݿ���ɾ����)
                    this[row, colName] = null;     // �������ÿ�
            }
            else  // ��ѡ
            {   // ����ַ���
                string outputkey;
                s = cm.GetDisplayString(obj, out outputkey);
                // ��������ֵ
                if( outputkey == "" ) this[row, colName] = null;   // ������������Ч
                if( !Equals(obj, outputkey) )  // ������������ֵ
                    this[row, colName] = outputkey;
            }
            e.Value = s;
        }

        void grid_ButtonClick(object sender, ColEventArgs e)
        {
            int row = grid.Row, col = e.ColIndex;
            if( grid.AddNewMode == AddNewModeEnum.AddNewCurrent )
            {   // ����һ��Pending����
                //grid.EditActive = true;
                //FunctionClass.InvokeMethod(split, "beginEdit", (char)0, true);
                return;
                //DataTable dt = GridAssist.GetDataTable(grid.DataSource);
                //dt.Rows.Add();
                //FunctionClass.InvokeMethod(grid, "OnAfterInsert", new EventArgs());
            }

            string colName = e.Column.DataColumn.DataField;
            Point pt = new Point(row, col);

            // �������ӳ���У���ֱ�ӷ���
            ColumnRefMap cm;
            if( columnMapList.ContainsKey(colName) == true )
            {
                cm = columnMapList[colName];
                cm.row = row;
                cm.col = col;
            }
            else
                return;

            if( cm.dt == null ) return;
            string idCol = cm.RefrencedColumnName;

            // ��ȡ�����λ��
            Rectangle rc = split.GetCellBounds(row, col);
            rc = grid.RectangleToScreen(rc);

            Form f = null;
            if( DropDownType == 0 )
            {
                MultiColumnDropDown flexForm = new MultiColumnDropDown(rc, this[row, idCol], cm);
                Debug.Assert(cm.flexParentCol == null || (cm.flexParentCol != null && cm._rootParentID != null));
                f = flexForm;
            }
            else if( DropDownType == 1 )
            {
                f = new MultiColumnDropDownListBox(rc, this[row, idCol], cm);
            }

            f.TopMost = true; // ʼ������ǰ
            f.Closed += delegate { f_Closed(f, cm, row, col); };
            f.Show();
        }

        /// <summary>
        /// �ؼ��ѡ����Ϻ��������ÿ�ʼʱ��ͽ���ʱ��
        /// </summary>
        void f_Closed(Form f, ColumnRefMap cm, int row, int col)
        {
            if( f.DialogResult != DialogResult.OK ) return;
            grid.EditActive = false;
            DataRow dr = ((DataRowView)grid[cm.row]).Row;
            dr[cm.RefrencedColumnName] = cm.GetStoreValue(f.Tag);
            dr.EndEdit();

            // ������Ϣ
            ColEventArgs arg = ClassAccesser.CreateObject<ColEventArgs>(0, split.DisplayColumns[cm.RefrencedColumnName]);
            ClassAccesser.InvokeMethod(grid, "OnAfterColUpdate", arg);

            if( afterRowSelectEvent != null )
                afterRowSelectEvent(cm, f.Tag);
            grid.Invalidate();
        }

        public void Dispose()
        {
            grid.FormatText -= grid_FormatText;
            grid.ButtonClick -= grid_ButtonClick;
        }
    }
}
