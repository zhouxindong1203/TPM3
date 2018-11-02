using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;

namespace Common.TrueDBGrid
{
    public class TrueDBGridAssist
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        private string _pos = null;

        /// <summary>
        /// 主键字段
        /// </summary>
        public string key = null;

        internal C1TrueDBGrid grid;

        Split split
        {
            get { return grid.Splits[0]; }
        }

        C1DisplayColumnCollection dcc
        {
            get { return split.DisplayColumns; }
        }

        public GridClipboard gridClipboard = null;

        /// <summary>
        /// 在标题行上单击右键是否弹出菜单
        /// 默认值为 true
        /// </summary>
        public bool rightClickContext = true;

        public TrueDBGridAssist(C1TrueDBGrid grid, string key, string pos)
        {
            this.grid = grid;
            this.key = key;
            this.pos = pos;

            grid.AllowSort = false;
            grid.AlternatingRows = true;
            grid.EvenRowStyle.BackColor = Color.LightCyan;

            grid.MouseDown += grid_MouseDown;
            grid.ColResize += grid_ColResize;
            grid.AfterInsert += grid_AfterInsert;

            gridClipboard = new GridClipboard(grid);
        }

        /// <summary>
        /// 将所有ID列赋值
        /// </summary>
        void grid_AfterInsert(object sender, EventArgs e)
        {
            if(key == null) return;
            DataTable dt = GridAssist.GetDataTable(grid.DataSource);
            if(dt == null) return;

            foreach(DataRow dr in dt.Rows)
            {
                if(dr.RowState == DataRowState.Deleted)
                    continue;

                if(dr.IsNull(key))   // 仅当没有主键时，设置主键
                    dr[key] = FunctionClass.NewGuid;
            }
        }

        const int MinWidth = 40;

        /// <summary>
        /// 保持列最小宽度
        /// </summary>
        void grid_ColResize(object sender, ColResizeEventArgs e)
        {
            C1DisplayColumn c = e.Column;
            if(c.Width < MinWidth) c.Width = MinWidth;
            if(!c.Visible)
            {
                c.Visible = true;
                c.Width = MinWidth;
            }
        }

        void grid_MouseDown(object sender, MouseEventArgs e)
        {
            if(grid.IsDisposed) return;

            if(e.Button == MouseButtons.Right)
            {   // 单击右键调整列属性
                if(!rightClickContext) return;
                if(columnList == null) return;  // 未设置
                if(e.X < 0 || e.Y < 0) return;
                PointAtEnum pe = grid.PointAt(e.X, e.Y);
                if(pe != PointAtEnum.AtColumnHeader) return;

                ContextMenu popupMenu = new ContextMenu();
                popupMenu.MenuItems.Add(new MenuItem("设置列属性", flex_SetColoumProp));
                popupMenu.Show(grid, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// 用户在固定行上点击右键时，选择设置列属性时调用此函数
        /// 弹出设置对话框
        /// </summary>
        void flex_SetColoumProp(object sender, EventArgs e)
        {
            // 先获取属性
            GetFlexColumnProp();

            Func<string, bool> IsColExist = colName => GetDisplayColumn(grid, colName) != null;
            ColumnPropSettingForm f = new ColumnPropSettingForm(columnList, IsColExist);
            DialogResult dr = f.ShowDialog();

            if(dr == DialogResult.OK)
                SetFlexColumnProp();
        }

        /// <summary>
        /// 读取数据库后调此函数
        /// </summary>
        public void OnPageCreate()
        {
            // 所有标题居中
            foreach(C1DisplayColumn dc in dcc)
            {
                dc.HeadingStyle.HorizontalAlignment = AlignHorzEnum.Center;
                dc.Style.WrapText = true;
            }

            if(rowPosition != null)
                rowPosition.OnPageCreate();

            // 设置flex列属性，必需在 flexTreeObject 之后
            SetFlexColumnProp();

            if(_refrenceColumnMapList != null)
                _refrenceColumnMapList.OnPageCreate();

            grid.CaptionStyle.BackColor = Color.LightYellow;
            grid.CaptionStyle.HorizontalAlignment = AlignHorzEnum.Near;
            grid.CaptionHeight = 30;

            split.CaptionHeight = 30;
            split.CaptionStyle.BackColor = Color.LightYellow;
            split.CaptionStyle.HorizontalAlignment = AlignHorzEnum.Near;
            split.ColumnCaptionHeight = 36;
        }

        public void OnPageClose()
        {
            grid.EditActive = false;

            if(grid.DataSource != null)  // 强制保存数据
                grid.BindingContext[grid.DataSource].EndCurrentEdit();

            // 保存列属性
            GetFlexColumnProp();

            if(_refrenceColumnMapList != null)
                _refrenceColumnMapList.OnPageClose();
        }

        //---------------------  事件支持  -------------------------------

        //---------------------  数据源支持  -------------------------------

        public delegate bool OnSetDataSourceHandler(C1TrueDBGrid sender, object dataSource);

        /// <summary>
        /// 设置数据源时触发该事件
        /// </summary>
        public OnSetDataSourceHandler OnSetDataSourceEvent;

        /// <summary>
        /// 设置Flex的数据源，如果有排序，则排序之
        /// </summary>
        public object DataSource
        {
            set
            {
                if(value == null) return;
                if(OnSetDataSourceEvent != null)
                {
                    if(OnSetDataSourceEvent(grid, value))
                        return;
                }

                if(value is DataTable)
                {
                    DataView dv = new DataView((DataTable)value);
                    if(pos != null) dv.Sort = pos;
                    grid.DataSource = dv;
                }
                else if(value is DataView)
                {
                    grid.DataSource = value;
                }
                else
                    throw new Exception("无效数据源!!!");
            }
        }

        //---------------------------------  树节点关联  -------------------------------

        /// <summary>
        /// 导航列支持对象
        /// </summary>
        private TreeNodeRelation _treeNodeRelation = null;

        public TreeNodeRelation treeNodeRelation
        {
            get
            {
                if(_treeNodeRelation == null)    // 只有需要时才创建对象
                    _treeNodeRelation = new TreeNodeRelation(this);
                return _treeNodeRelation;
            }
        }

        public void SetTreeNodeRelation(TreeNode tn, string display)
        {
            treeNodeRelation.SetTreeNodeRelation(tn, key, display);
        }

        //---------------------------------  序号支持  -------------------------------
        public RowPosition rowPosition = null;

        /// <summary>
        /// 如果设置了排序字段，则创建排序对象
        /// </summary>
        public string pos
        {
            get { return _pos; }
            set
            {
                if(_pos == value) return;
                _pos = value;
                if(_pos != null)
                    rowPosition = new RowPosition(grid, _pos, key);
            }
        }

        //---------------------------------  列属性支持  -------------------------------
        public ColumnPropList columnList;

        /// <summary>
        /// 设置 flex 的列属性,  columnList ==> flex.Cols
        /// </summary>
        void SetFlexColumnProp()
        {
            if(columnList == null) return;

            // 保存行高
            grid.RowHeight = columnList.RowHeight > 0 ? columnList.RowHeight : 44;

            int position = 0;
            foreach(string name in columnList.Keys())
            {
                ColumnProperty cp = columnList[name];
                if(cp.neverShow) continue;
                if(cp.loadFromSetting) continue;

                C1DisplayColumn dc = GetDisplayColumn(grid, name);
                if(dc == null) continue;  // 不存在该列

                int index = dcc.IndexOf(dc);
                dcc.RemoveAt(index);
                dcc.Insert(position, dc);
                dc.Visible = cp.visible;
                dc.Width = cp.width;
                if(cp.TextAlign != null)
                    dc.Style.HorizontalAlignment = GetAlign(cp.TextAlign.Value);
                dc.Style.VerticalAlignment = AlignVertEnum.Center;

                if(cp.allowEdit != null)
                    dc.Locked = !cp.allowEdit.Value;

                if(cp.format != null)
                    dc.DataColumn.NumberFormat = cp.format;

                //!!! 如果不加这句，隐藏列可能被拖出来
                if(!dc.Visible) dc.AllowSizing = false;

                string cap = cp.updateCaption ?? cp.caption;
                if(cap != null)
                    dc.DataColumn.Caption = cap;
                position++;

            }
            // hide all other columns
            for(int i = position; i < dcc.Count; i++)
            {
                dcc[i].Visible = false;
                //!!! 如果不加这句，隐藏列可能被拖出来
                dcc[i].AllowSizing = false;
            }
            columnList.OnPageCreate(grid);
        }

        AlignHorzEnum GetAlign(CommonTextAlignEnum te)
        {
            AlignHorzEnum ta = AlignHorzEnum.Center;
            switch(te)
            {
                case CommonTextAlignEnum.General:
                    ta = AlignHorzEnum.General;
                    break;
                case CommonTextAlignEnum.Near:
                    ta = AlignHorzEnum.Near;
                    break;
                case CommonTextAlignEnum.Far:
                    ta = AlignHorzEnum.Far;
                    break;
                default:
                    ta = AlignHorzEnum.Center;
                    break;
            }
            return ta;
        }

        /// <summary>
        /// 获取 flex 的列属性,  flex.Cols ==> columnList
        /// </summary>
        void GetFlexColumnProp()
        {
            if(columnList == null) return;

            // 保存行高
            columnList.RowHeight = grid.RowHeight;

            int index = 0;
            foreach(C1DisplayColumn dc in dcc)
            {
                string colName = dc.DataColumn.DataField;
                ColumnProperty cp = columnList[colName];
                if(cp == null) continue;
                if(cp.visible) cp.width = dc.Width;
                columnList.Moveto(index, colName);  // 保存列位置
                index++;
            }
        }

        //---------------------------------  映射支持  -------------------------------
        /// <summary>
        /// 外部映射列
        /// </summary>
        private RefrenceColumnMapList _refrenceColumnMapList = null;
        public RefrenceColumnMapList refrenceColumnMapList
        {
            get { return _refrenceColumnMapList ?? (_refrenceColumnMapList = new RefrenceColumnMapList(grid)); }
        }

        //---------------------------------  静态函数支持  -----------------------------
        /// <summary>
        /// 获取指定名称的列值，如果不存在，则返回null
        /// </summary>
        public static C1DisplayColumn GetDisplayColumn(C1TrueDBGrid grid, string col)
        {
            foreach(C1DisplayColumn dc in grid.Splits[0].DisplayColumns)
                if(dc.DataColumn.DataField == col)
                    return dc;
            return null;
        }
    }
}
