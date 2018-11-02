using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using C1.Win.C1FlexGrid;
using Common;
using Common.Database;

namespace TPM3.wx
{
    /// <summary>
    /// 问题报告交叉矩阵表
    /// </summary>
    public partial class FallMatrixTable : MyUserControl
    {
        FlexGridAssist flexAssist1;

        public FallMatrixTable()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            flexAssist1 = new FlexGridAssist(flex1, null, null);
        }

        public override bool OnPageCreate()
        {
            // [问题类别，问题级别，个数]
            Dictionary<Scalar2, int> fallMap = new Dictionary<Scalar2, int>();
            DataTable dtFallList;

            string sqlFall = "select ID, 同标识序号, 问题类别, 问题级别 from CA问题报告单 where 测试版本 = ? ";
            List<object> arglist = new List<object>();
            arglist.Add(currentvid);
            if(id != null)  // 指定某个对象
            {
                sqlFall += " and 所属被测对象ID = ?";
                arglist.Add(id);
            }
            dtFallList = dbProject.ExecuteDataTable(sqlFall, arglist.ToArray());

            foreach(DataRow dr in dtFallList.Rows)
            {
                Scalar2 sc = new Scalar2(dr["问题类别"], dr["问题级别"]);
                if(!fallMap.ContainsKey(sc)) fallMap[sc] = 0;
                fallMap[sc]++;
            }

            DataTable dtFallMatrix = new DataTable();
            GridAssist.AddColumn(dtFallMatrix, "问题类别名称", "问题类别ID");

            string sqlFallType = "select ID, 名称 from DC问题级别表 where 项目ID = ? and 类型 = ? order by 序号";
            DataTable dtFallLevel = dbProject.ExecuteDataTable(sqlFallType, pid, "级别");
            foreach(DataRow dr in dtFallLevel.Rows)
            {
                if(GridAssist.IsNull(dr["名称"])) continue;
                DataColumn dc = dtFallMatrix.Columns.Add(dr["ID"] as string, typeof(int));
                dc.DefaultValue = 0;
            }

            DataTable dtFallClass = dbProject.ExecuteDataTable(sqlFallType, pid, "类别");
            foreach(DataRow dr in dtFallClass.Rows)
            {
                if(GridAssist.IsNull(dr["名称"])) continue;
                DataRow newRow = dtFallMatrix.Rows.Add();
                newRow["问题类别名称"] = dr["名称"];
                newRow["问题类别ID"] = dr["ID"];
            }

            foreach(Scalar2 sc in fallMap.Keys)
                SetValue(dtFallMatrix, sc.dbValue0, sc.dbValue1, fallMap[sc]);

            flexAssist1.DataSource = dtFallMatrix;
            flex1.Rows[0].Height = 40;
            ColumnCollection cc = flex1.Cols;
            foreach(Column c in cc)
            {
                c.TextAlign = TextAlignEnum.CenterCenter;
                c.TextAlignFixed = TextAlignEnum.CenterCenter;
                c.Width = 120;
            }
            // 重命名列
            cc["问题类别ID"].Visible = false;
            foreach(DataRow dr in dtFallLevel.Rows)
            {
                string col = dr["ID"] as string;
                if(GridAssist.IsNull(dr["名称"])) continue;
                if(cc.Contains(col))
                    cc[col].Caption = dr["名称"] as string;
            }

            return true;
        }

        void SetValue(DataTable dt, object classid, object levelid, int value)
        {
            string col = levelid as string;
            if(!dt.Columns.Contains(col)) return;
            foreach(DataRow dr in dt.Rows)
            {
                if(!Equals(classid, dr["问题类别ID"])) continue;
                dr[col] = value;
            }
        }

        /// <summary>
        /// 画题头
        /// </summary>
        void flex1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if(e.Row == 0)
            {
                e.Style = flex1.Styles.Normal;
                e.Style.TextAlign = TextAlignEnum.CenterCenter;
            }
            if(e.Row == 0 && e.Col == 0)
            {
                Rectangle r = e.Bounds;  // flex1.GetCellRect(e.Row, e.Col);
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawLine(Pens.Black, r.Left, r.Top, r.Right, r.Bottom);
                g.DrawString("级别", flex1.Font, Brushes.Black, 80 + r.Left, 8 + r.Top);
                g.DrawString("类别", flex1.Font, Brushes.Black, 10 + r.Left, 20 + r.Top);

                e.DrawCell(DrawCellFlags.Border);
                e.Handled = true;
            }
            if(e.Row > 0 && e.Col > 0)
            {
                object obj = flex1[e.Row, e.Col];
                if(obj is int)
                {
                    int count = (int)obj;
                    if(!flex1.Styles.Contains("boldcount"))
                    {
                        CellStyle cs = flex1.Styles.Add("boldcount", flex1.Styles.Normal);
                        cs.Font = new Font(flex1.Styles.Normal.Font, FontStyle.Bold);
                    }
                    if(count > 0)
                        e.Style = flex1.Styles["boldcount"];
                }
            }
        }
    }
}
