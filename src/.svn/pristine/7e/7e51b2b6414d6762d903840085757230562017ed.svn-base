using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;

namespace Common.TrueDBGrid
{
    /// <summary>
    /// 序号支持类
    /// 上下移时可与树节点关联
    /// SE20753-UB-666559
    /// </summary>
    public class RowPosition : RowPositionBase, IMoveCommand, IDisposable
    {
        C1TrueDBGrid grid;

        public RowPosition(C1TrueDBGrid grid, string pos, string key)
            : base(grid, pos, key)
        {
            this.grid = grid;
 
            if (pos != null)
            {
                // 处理主键、序号、默认值
                grid.AfterInsert += ResetPosition;
                grid.AfterDelete += ResetPosition;
           }
        }

        public bool OnPageCreate()
        {
            if( pos != null )    // 位置列只读
            {
                grid.Splits[0].DisplayColumns[pos].Style.HorizontalAlignment = AlignHorzEnum.Center;
                grid.Splits[0].DisplayColumns[pos].Locked = true;
                ResetPosition();
            }
            return true;
        }

        /// <summary>
        /// 接受下移命令
        /// </summary>
        public override void OnCommandMoveDown()
        {
            if( pos == null || dt == null ) return;

            grid.EditActive = false;
            ResetPosition();
            int r = grid.Row;

            if( r < 0 || r >= grid.RowCount - 1 )   // 已经是最后一行(除去新行)
                return;

            DataRow r1 = (grid[r] as DataRowView).Row;
            DataRow r2 = (grid[r + 1] as DataRowView).Row;

            if( !OnBeforeRowMoveDown(r1, r2) ) return;

            object temp = r1[pos];
            r1[pos] = r2[pos];
            r2[pos] = temp;

            grid.Row = r + 1;
            grid.SelectedRows.Clear();
            grid.SelectedRows.Add(r + 1);
            grid.Invalidate();

            AfterRowMoveDown(r1, r2);
        }

        /// <summary>
        /// 接受上移命令
        /// </summary>
        public override void OnCommandMoveUp()
        {
            if( pos == null || dt == null ) return;

            grid.EditActive = false;   // 防止窜行
            ResetPosition();
            int r = grid.Row;
            if( r <= 0 || r > grid.RowCount - 1 )   // 已经是第一行
                return;

            DataRow r1 = (grid[r] as DataRowView).Row;
            DataRow r2 = (grid[r - 1] as DataRowView).Row;

            if( !OnBeforeRowMoveUp(r1, r2) ) return;

            object temp = (int)r1[pos];
            r1[pos] = r2[pos];
            r2[pos] = temp;

            grid.Row = r - 1;
            grid.SelectedRows.Clear();
            grid.SelectedRows.Add(r - 1);
            grid.Invalidate();

            AfterRowMoveUp(r1, r2);
        }
        public override void OnCommandMoveFirst()
        {
        }
        public override void OnCommandMoveLast()
        {
        }
        /// <summary>
        /// 数据源
        /// </summary>
        protected override object dt
        {
            get { return grid.DataSource; }
        }

        public override void Dispose()
        {
            if( pos != null )
            {
                grid.AfterInsert -= ResetPosition;
                grid.AfterDelete -= ResetPosition;
            }
            base.Dispose();
        }

        protected override bool IsReadOnly
        {
            get
            {
                if( pos == null || dt == null ) return true;
                if( grid.Splits[0].Locked ) return true;   // 只读的网格不可以上下移动
                if( CanMove == false ) return true;
                return false;
            }
        }

        public override void GetRowViewList(List<DataRow> list)
        {
            for( int i = 0; i < grid.RowCount; i++ )
            {
                DataRowView drv = grid[i] as DataRowView;
                if( drv != null ) list.Add(drv.Row);
            }
        }
    }
}
