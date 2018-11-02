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
    /// 外键映射列支持类
    /// </summary>
    public class RefrenceColumnMapList : RefrenceColumnMapBase, IDisposable
    {
        C1TrueDBGrid grid;

        Split split
        {
            get { return grid.Splits[0]; }
        }

        /// <summary>
        /// 0 表示下拉框采用 FlexDropDown形式， 1 表示下拉框采用 ListBox形式
        /// </summary>
        public static int DropDownType = 0;

        public RefrenceColumnMapList(C1TrueDBGrid grid)
        {
            this.grid = grid;
            // 自画，显示对应组织信息
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
                if( row >= grid.RowCount )  return null;  // 在新行中
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
                dc.Button = !cm.readOnly;   // 只读时不需要下拉按钮
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

            // 如果不是映射列，则直接返回
            ColumnRefMap cm;
            if( columnMapList.ContainsKey(colName) )
                cm = columnMapList[colName];
            else if( columnMapList.ContainsKey(pt) )
                cm = columnMapList[pt];
            else
                return;

            DataTable dt = cm.dt;
            if( dt == null ) return;

            if( cm.multiSelect == false )   // 单选
            {
                s = cm.GetDisplayString(obj);
                if( s == null )     //  不存在对应报告(已从数据库中删除？)
                    this[row, colName] = null;     // 将该项置空
            }
            else  // 多选
            {   // 组合字符串
                string outputkey;
                s = cm.GetDisplayString(obj, out outputkey);
                // 设置网格值
                if( outputkey == "" ) this[row, colName] = null;   // 所有主键都无效
                if( !Equals(obj, outputkey) )  // 重新设置网络值
                    this[row, colName] = outputkey;
            }
            e.Value = s;
        }

        void grid_ButtonClick(object sender, ColEventArgs e)
        {
            int row = grid.Row, col = e.ColIndex;
            if( grid.AddNewMode == AddNewModeEnum.AddNewCurrent )
            {   // 插入一个Pending空行
                //grid.EditActive = true;
                //FunctionClass.InvokeMethod(split, "beginEdit", (char)0, true);
                return;
                //DataTable dt = GridAssist.GetDataTable(grid.DataSource);
                //dt.Rows.Add();
                //FunctionClass.InvokeMethod(grid, "OnAfterInsert", new EventArgs());
            }

            string colName = e.Column.DataColumn.DataField;
            Point pt = new Point(row, col);

            // 如果不是映射列，则直接返回
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

            // 获取网格的位置
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

            f.TopMost = true; // 始终在最前
            f.Closed += delegate { f_Closed(f, cm, row, col); };
            f.Show();
        }

        /// <summary>
        /// 关键活动选择完毕后，重新设置开始时间和结束时间
        /// </summary>
        void f_Closed(Form f, ColumnRefMap cm, int row, int col)
        {
            if( f.DialogResult != DialogResult.OK ) return;
            grid.EditActive = false;
            DataRow dr = ((DataRowView)grid[cm.row]).Row;
            dr[cm.RefrencedColumnName] = cm.GetStoreValue(f.Tag);
            dr.EndEdit();

            // 触发消息
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
