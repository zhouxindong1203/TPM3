using System;
using C1.Win.C1TrueDBGrid;
using System.Data;
using System.Windows.Forms;

namespace Common.TrueDBGrid
{
    public class TreeNodeRelation : TreeNodeRelationBase, IDisposable
    {
        C1TrueDBGrid grid;
        TrueDBGridAssist gridAssist;

        public TreeNodeRelation(TrueDBGridAssist gridAssist)
        {
            this.grid = gridAssist.grid;
            this.gridAssist = gridAssist;

            // 此处不能响应 AfterDeleteRow，否则删除掉记录后就无法知道删的是哪条记录了
            grid.BeforeDelete += grid_BeforeDelete;
            grid.AfterDelete += grid_AfterDelete;
            grid.AfterInsert += grid_AfterInsert;
            grid.AfterColUpdate += grid_AfterColUpdate;
        }

        void IDisposable.Dispose()
        {
            grid.BeforeDelete -= grid_BeforeDelete;
            grid.AfterDelete -= grid_AfterDelete;
            grid.AfterInsert -= grid_AfterInsert;
            grid.AfterColUpdate -= grid_AfterColUpdate;
        }

        private void grid_AfterInsert(object sender, EventArgs e)
        {
            if( tnParent == null ) return;
            DataRow dr = GetLastRowEvent();

            TreeNode tnChild = OnCreateNewNode(dr);

            if( defaultImageKey != null )
                tnChild.ImageKey = tnChild.SelectedImageKey = defaultImageKey;

            // 设置图标
            if( SetTreeNodeEvent != null )
                SetTreeNodeEvent(tnChild, dr);

            tnParent.Expand();
        }

        void grid_AfterColUpdate(object sender, ColEventArgs e)
        {
            if( tnParent == null ) return;
            //如果添加新纪录则退出本过程
            if( grid.AddNewMode != AddNewModeEnum.NoAddNew ) return;
            if( e.Column.DataColumn.DataField != DisplayColumn ) return;

            string text = grid.Columns[DisplayColumn].Value as string;
            object id = grid.Columns[idColumn].Value;

            TreeNode child = GetTreeNodeByKey(id);
            if( child != null )
            {
                child.Text = text;
                if( SetTreeNodeEvent != null )
                    SetTreeNodeEvent(child, null);
            }
        }

        object tempkey = null;

        /// <summary>
        /// 仅保存被删除的行的主键
        /// </summary>
        void grid_BeforeDelete(object sender, CancelEventArgs e)
        {
            tempkey = null;
            if( e.Cancel == true ) return;
            tempkey = grid.Columns[idColumn].Value;
        }

        void grid_AfterDelete(object sender, EventArgs e)
        {
            if( tnParent == null || tempkey == null ) return;
            TreeNode child = GetTreeNodeByKey(tempkey);
            if( child != null ) tnParent.Nodes.Remove(child);
        }

        public Func2<DataRow> GetLastRowEvent;
    }
}
