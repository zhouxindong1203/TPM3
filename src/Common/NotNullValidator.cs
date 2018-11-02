using System;
using System.Windows.Forms;
using C1.Win.C1TrueDBGrid;

namespace Common.TrueDBGrid
{
    public class NotNullValidator : TrueDBGridValidator
    {
        /// <summary>
        /// �ǿ��е�������
        /// </summary>
        string colName, displayName;

        public NotNullValidator(string colName)
        {
            this.colName = colName;
        }

        public NotNullValidator(string colName, string displayName)
            : this(colName)
        {
            this.displayName = displayName;
        }

        public override void Initialize()
        {
            grid.BeforeUpdate -= grid_BeforeUpdate;
            grid.BeforeUpdate += grid_BeforeUpdate;
        }

        private void grid_BeforeUpdate(object sender, CancelEventArgs e)
        {
            if (e.Cancel == true) return;
            object obj = grid.Columns[colName].Value;
            if( GridAssist.IsNull(obj) )
            {
                //string msg = string.Format("'{0}'�в���Ϊ��", displayName ?? colName);
                //MessageBox.Show(msg);
                //grid.Focus();
                //e.Cancel = true;

                //var cur_row = grid.Row - 1;
                //return;

                grid.Delete(grid.RowCount - 1); // grid.RowΪ��ǰ������(base0)
                e.Cancel = true;
            }
        }
    }

    public class MaxLengthValidator : TrueDBGridValidator
    {
        string colName;
        int MaxLength = 0;

        public MaxLengthValidator(string colName, int MaxLength)
        {
            this.colName = colName;
            this.MaxLength = MaxLength;
        }

        public override void Initialize()
        {
            grid.BeforeUpdate -= grid_BeforeUpdate;
            grid.BeforeUpdate += grid_BeforeUpdate;
        }

        private void grid_BeforeUpdate(object sender, CancelEventArgs e)
        {
            if( e.Cancel == true ) return;
            string s = grid.Columns[colName].Value as string;
            if( s != null && s.Length > MaxLength )
            {
                string msg = string.Format("'{0}'�г��ȳ������ֵ{1}", colName, MaxLength);
                MessageBox.Show(msg);
                e.Cancel = true;
                return;
            }
        }
    }
}