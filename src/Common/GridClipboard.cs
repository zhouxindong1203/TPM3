using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using Common;
using C1.Win.C1TrueDBGrid;

namespace Common.TrueDBGrid
{
    public class GridClipboard : GridClipboardBase
    {
        C1TrueDBGrid grid;

        C1DisplayColumnCollection dcc
        {
            get { return grid.Splits[0].DisplayColumns; }
        }

        /// <summary>
        /// 剪贴板数据格式名
        /// </summary>
        public static string _clipboradname = "dbgrid";

        public GridClipboard(C1TrueDBGrid grid)
            : base(grid)
        {
            this.grid = grid;
        }

        bool GetSelection(List<int> rows, List<int> cols)
        {
            if( grid.SelectedRows.Count == 0 && grid.SelectedCols.Count == 0 )
                return false;

            if( grid.SelectedRows.Count == 0 )
            {  // 当前处于选中列状态，将所有行加入列表
                for( int i = 0; i < grid.RowCount; i++ )
                    rows.Add(i);
            }
            else
            {
                foreach( int i in grid.SelectedRows )
                    rows.Add(i);
            }
            rows.Sort();

            for( int i = 0; i < dcc.Count; i++ )
            {
                if( grid.SelectedCols.Count == 0 || grid.SelectedCols.IndexOf(dcc[i].DataColumn) >= 0 )
                    if( dcc[i].Visible )
                        cols.Add(i);
            }
            return true;
        }

        /// <summary>
        /// 将被选中的网格的数据写入数据集，再序列化写入剪贴板
        /// </summary>
        public override void DoCopy()
        {
            // 当前选中的行列集合
            List<int> rows = new List<int>(), cols = new List<int>();
            if( !GetSelection(rows,cols) )
            {   // 没有被选中行列
                Clipboard.Clear();
                return;
            }

            DataTable dt = new DataTable(_clipboradname);
            foreach( int i in cols )
                dt.Columns.Add(i.ToString(), typeof(object));

            foreach( int i in rows )
            {
                DataRow dr = dt.Rows.Add();
                foreach( int j in cols )
                    dr[j.ToString()] = grid[i, dcc[j].DataColumn.DataField];
            }

            string s = ClassAccesser.WriteDataTableToString(dt);
            // 在剪贴板上写下两种类型的数据
            IDataObject data = new DataObject();
            data.SetData(_clipboradname, Compression.CompressString(s));
            data.SetData(s);
            Clipboard.SetDataObject(data);
        }

        public override void DoPaste()
        {
            IDataObject data = Clipboard.GetDataObject();
            if( !data.GetDataPresent(_clipboradname) ) return;

            byte[] buf = data.GetData(_clipboradname) as byte[];
            if( buf == null ) return;
            string s = Compression.DeCompressString(buf);
            // 将剪贴板中的内容反序列化至数据集
            DataTable dt = ClassAccesser.ReadDataTableFromString(s);

            // 当前选中的行列集合
            List<int> rows = new List<int>(), cols = new List<int>();
            if( !GetSelection(rows, cols) )
            {   // 没有被选中行列
                return;
            }

            for( int r = 0; r < Math.Min(rows.Count, dt.Rows.Count); r++ )
            {
                DataRow dr = dt.Rows[r];
                DataRowView gridrow = grid[rows[r]] as DataRowView;

                int dtc = 0;  // Datatable 中的列号
                for( int c = 0; c < Math.Min(cols.Count, dt.Columns.Count); c++ )
                {
                    if( CanColumnCopy(c) )
                    {   // 如果列只读，则不写该列，但跳过相应数据列
                        C1DisplayColumn dc = dcc[c];
                        gridrow[dc.DataColumn.DataField] = dr[dtc];
                        ColEventArgs arg = ClassAccesser.CreateObject<ColEventArgs>(c, dc);
                        ClassAccesser.InvokeMethod(grid, "OnAfterColUpdate", arg);
                    }
                    dtc++;
                }
            }
            grid.Invalidate();
        }

        private bool CanColumnCopy(int c)
        {
            if( dcc[c].Locked ) return false;
            return true;
        }
    }
}
