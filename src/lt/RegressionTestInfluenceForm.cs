using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Common;
using Common.Database;
using TPM3.chq;
using TPM3.Sys;
using TPM3.wx;
using Z1.tpm;

namespace TPM3.lt
{
    /// <summary>
    /// 回归测试影响域分析
    /// </summary>
    [TypeNameMap("lt.RegressionTestInfluenceForm")]
    public partial class RegressionTestInfluenceForm : MyBaseForm
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<RegressionTestInfluenceForm>(3);
        public static readonly string sqlRegressionTest = "select * from HG回归测试影响域 where 项目ID = ? and 测试版本 = ? order by 序号";
        protected FlexGridAssist flexAssist1;
        DataTable dtObject;
        Color bkcr1, bkcr2;
        TestResultSummary summarypre, summarycur;
        protected bool GroupByDoc = RequireTreeForm.GroupByDoc;
        ClassRegressionItemCaseTable vc;
        object previd;
        public RegressionTestInfluenceForm()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithoutFocus(flex1);
            bkcr1 = flex1.Styles.Alternate.BackColor;
            bkcr2 = flex1.Styles.Normal.BackColor;
            flexAssist1 = new FlexGridAssist(flex1, null, null);
            flexAssist1.columnList = columnList1;
            flexAssist1.doubleClickEdit = true;
 //           flexAssist1.AddHyperColumn("涉及到的测试用例");
//            flexAssist1.RowNavigate += OnRowNavigate;
//            flexAssist1.AddOleColumn("更动说明");
            previd = DBLayer1.GetPreVersion(dbProject, currentvid);

            if (GroupByDoc)
                keyIndexCol = "序号";
        }
        static RegressionTestInfluenceForm()
        {
            columnList1.Add("更动报告单号", 100);
            columnList1.Add("更动项", 170);
            columnList1.Add("涉及到的软件问题", 170, false);
            columnList1.Add("测试列表", 250, false);
 //           columnList1.Add("涉及到的测试用例", 250, false);
 //           columnList1.AddValidator(new NotNullValidator("更动说明"));
        }
        void OnRowNavigate(int row, int col, Row r)
        {
            string colName = flex1.Cols[col].Name;
            if (colName == "涉及到的测试用例")
            {
                DataRow dr = dbProject.ExecuteDataRow("SELECT * FROM CA测试用例实体表 WHERE 测试用例名称 = ? and 项目ID = ?;", r["涉及到的测试用例"], pid);
                if (dr == null) return;
                dr = dbProject.ExecuteDataRow("SELECT * FROM CA测试用例实测表 WHERE 测试用例ID = ? and 项目ID = ? and 测试版本 = ?;", dr["ID"], pid, currentvid);
                if (dr == null) return;
                MainForm.mainFrm.DelayCreateForm("zxd.TestTreeForm?type=design&id=" + dr["ID"]);
            }
        }
        public override bool OnPageCreate()
        {
            summarypre = new TestResultSummary(pid, previd);
            summarypre.OnCreate();
            summarycur = new TestResultSummary(pid, currentvid);
            summarycur.OnCreate();
            dtObject = dbProject.ExecuteDataTable(sqlRegressionTest, pid, currentvid);

            if (dtObject == null) return false;
            flexAssist1.DataSource = GetItemCaseTraceView(dtObject);

            flexAssist1.flex.AllowMerging = AllowMergingEnum.Free;
            MyUserControl.AddMergeColumn(flexAssist1.flex, "更动报告单号", "更动项", "测试列表");
            flexAssist1.OnPageCreate(); 
            return true;
        }
        public DataView GetItemCaseTraceView(DataTable dtObject)
        {
            vc = new ClassRegressionItemCaseTable(pid, previd, currentvid);
            vc.GetAllClassItemCase(summarypre, summarycur, dtObject, dbProject, ref flexAssist1.flex);
            DataView dv = new DataView(vc.dt);
            dv.Sort = "序号";
            return dv;
        }
        protected string keyIndexCol = "序号";

        public override bool OnPageClose(bool bClose)
        {
            vc.UpdateDataTable(ref dtObject);
            flexAssist1.OnPageClose();
            if (!dbProject.UpdateDatabase(dtObject, sqlRegressionTest)) return false; 
            return true;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }


        public String GetObjectOfPlbID(String plbID)
        {
            string sqlRegressionTest1 = "SELECT * from CA问题报告单 where ID = ? and 测试版本 = ? and 项目ID = ? ";
            DataTable dt2 = dbProject.ExecuteDataTable(sqlRegressionTest1, plbID, previd, pid);
            if (dt2.Rows.Count == 0)
            {
                return null;
            }
            return dt2.Rows[0]["所属被测对象ID"].ToString();
        }
        public DataTable GetDataTable(object ID)
        {
            string sqlRegressionTest1 = "SELECT * from HG回归测试问题表 where 更动项ID = ? and 软件问题类型 = ? and 测试版本 = ? and 项目ID = ? ";
            DataTable dt2 = dbProject.ExecuteDataTable(sqlRegressionTest1, ID, "测试问题", currentvid, pid);
            return dt2;
        }
        //把选择的问题报告单写回数据库，且重绘制回归测试影响域表
        public void InvalidateForm(DataTable dt, object ID)
        {
            string sqlRegressionTest1 = "SELECT * from HG回归测试问题表 where 更动项ID = ? and 测试版本 = ? and 项目ID = ? ";
            DataTable dt2 = dbProject.ExecuteDataTable(sqlRegressionTest1, ID, currentvid, pid);
            if (dt2 == null) return;
            vc.UpdateDataTable(ref dt2);
             
            //若是未进行分析且还没有添加问题报告单
            if (dt2.Rows.Count == 0 )
            {
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if ((bool)dr["选择"] == false)
                    {
                        continue;
                    }
                    DataRow drt = dt2.Rows.Add();
                    drt["ID"] = FunctionClass.NewGuid;
                    drt["更动项ID"] = ID;
                    drt["项目ID"] = pid;
                    drt["测试版本"] = currentvid;
                    drt["软件问题类型"] = "测试问题";
                    drt["软件问题"] = dr["ID"];
                }
                i++;
            }
            else
            {
                //已包含问题报告单或已进行初次分析
                
                foreach (DataRow dr in dt.Rows)
                {
                    bool tag = false;
                    if ((bool)dr["选择"] == false)
                    {
                        tag = true;
                    }
                    for(int i =0; i<dt2.Rows.Count;i++)
                    {
                        if (dt2.Rows[i].RowState == DataRowState.Deleted)
                        {
                            continue;
                        }
                        if (dt2.Rows[i]["软件问题"].ToString() == dr["ID"].ToString() && (bool)dr["选择"] == true)//判断重名的问题报告单
                        {
                            tag = true;
                            break;
                        }
                        else if (dt2.Rows[i]["软件问题"].ToString() == dr["ID"].ToString() && (bool)dr["选择"] == false)
                        {
                            dt2.Rows[i].Delete();
                            break;
                        }

                    }
                    if (tag)
                    {
                        continue;

                    }
                    //添加该问题报告单
                    DataRow drt = dt2.Rows.Add();
                    drt["ID"] = FunctionClass.NewGuid;
                    drt["更动项ID"] = ID;
                    drt["项目ID"] = pid;
                    drt["测试版本"] = currentvid;
                    drt["软件问题类型"] = "测试问题";
                    drt["软件问题"] = dr["ID"];
                }
                
            }
            if (!dbProject.UpdateDatabase(dt2, sqlRegressionTest1)) return;
            OnPageClose(true);
            flexAssist1.flex.MergedRanges.Clear();//清除合并模式
            OnPageCreate();
        }
        //把进行的回归测试影响域分析结果写回数据库，且重绘制回归测试影响域表
        public void InvalidateForm1( DataTable dt,  object ID)
        {
            String testCaseIDList = "";
            ArrayList testCaseList = new ArrayList();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }
                testCaseList.Add(dr["测试用例ID"]);
            }
            IComparer myComparer = new TestCaseCompareClass(summarycur);
            testCaseList.Sort(myComparer);
            testCaseIDList = "";
            for (int j = 0; j <= testCaseList.Count - 1; j++)
            {
                string AccordID1 = testCaseList[j].ToString();
                if (j != 0)
                {
                    testCaseIDList += ",";
                }
                testCaseIDList += AccordID1;
            }

            if (ID.ToString() != "")
            {
                foreach (DataRow dr in dtObject.Rows)
                {
                    if (dr["ID"].ToString() == ID.ToString() )
                    {
                        dr["涉及到的测试用例"] = testCaseIDList;
                        dr["初次分析标志"] = true;
                    }
                }
            }
           OnPageClose(true);
           OnPageCreate();
        }
        public void AddChangeItem(object ID)
        {
            vc.AddChangeItem(ID, ref dtObject);
//            OnPageClose(true);
 //           flexAssist1.flex.MergedRanges.Clear();//清除合并模式
 //           OnPageCreate();
        }
        public void DeleteChangeItem(object ID)
        {
            vc.DeleteChangeItem(ID, ref dtObject, dbProject);
            OnPageClose(true);
            flexAssist1.flex.MergedRanges.Clear();//清除合并模式
            OnPageCreate();
        }
        //更改软件扩展更动状态
        public void ChangeSoftChangeState(object softwareChangeID)
        {
            vc.AddSoftChange(softwareChangeID, ref dtObject);
            OnPageClose(true);
            flexAssist1.flex.MergedRanges.Clear();//清除合并模式
            OnPageCreate();
        }
        private void flex1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }
            HitTestInfo hti = flex1.HitTest(e.X, e.Y);
            if (hti.Row < 0 || hti.Column < 0)
            {
                return;
            }
            if (hti.Column == 3 && flex1[hti.Row, hti.Column] != null)
            {
                ClickPlbColumn(hti, e);
            }
            else if (hti.Column == 2 && flex1[hti.Row, hti.Column] != null)
            {
                ClickChangeItemColumn(hti, e);
            }
            else if (hti.Column == 1 && flex1[hti.Row, hti.Column] != null)
            {
                ClickChangeReportColumn(hti, e);
            }

        }
        //判断当前更动项是否有软件问题
        public bool AssertPlb(object ID)
        {
            DataTable dt1 = dbProject.ExecuteDataTable("SELECT * FROM HG回归测试问题表 WHERE 更动项ID = ?;", ID);
            if (dt1.Rows.Count<=0)
            {
                MessageBox.Show("当前更动项没有软件问题，不能进行影响域分析！");
                return true;
            }
            return false;
        }
        private void ClickChangeReportColumn(HitTestInfo hti, MouseEventArgs e)
        {
            String softwareChangeID = vc.GetSoftwareChangeID(hti.Row, "软件更动单ID").ToString();
            String ID = vc.GetSoftwareChangeID(hti.Row, "ID").ToString();
            DataTable dt2 = vc.GetTestCaseOfChange(ID);
            DataTable dt3 = vc.GetTestCaseOfChangePlb(ID);
            RegressionProblemMenu menu = new RegressionProblemMenu(dt2, dt3, dbProject, softwareChangeID, currentvid, pid, this);
            if (hti.Type == HitTestTypeEnum.Cell & flex1.Cols[hti.Column].Name == "更动报告单号")
            {
                menu.ChangeReportDelete.Show(flex1, e.X, e.Y);
            }

        }
        private void ClickChangeItemColumn(HitTestInfo hti, MouseEventArgs e)
        {

            String softwareChangeID = vc.GetSoftwareChangeID(hti.Row, "软件更动单ID").ToString();
            String ID = vc.GetSoftwareChangeID(hti.Row, "ID").ToString();
            DataTable dt2 = vc.GetTestCaseOfChange(ID);
            DataTable dt3 = vc.GetTestCaseOfChangePlb(ID);
            RegressionProblemMenu menu = new RegressionProblemMenu(dt2, dt3, dbProject, ID, currentvid, pid, this);
            if (hti.Type == HitTestTypeEnum.Cell & flex1.Cols[hti.Column].Name == "更动项")
            {
                menu.contextMenuStrip1.Show(flex1, e.X, e.Y);
            }
        }
        private void ClickPlbColumn(HitTestInfo hti, MouseEventArgs e)
        {
            String plbID = flex1[hti.Row, hti.Column].ToString();
            String ID = vc.GetSoftwareChangeID(hti.Row, "ID").ToString();
            RegressionProblemMenu menu = new RegressionProblemMenu(null, null, dbProject, ID, currentvid, pid, this);
            if (hti.Type == HitTestTypeEnum.Cell & flex1.Cols[hti.Column].Name == "涉及到的软件问题")
            {
//                menu.RegressionPblMenu.Show(flex1, flex1.Cols[hti.Column].Left, flex1.Rows[hti.Row].Bottom);
                menu.SetPlbID(plbID);
                menu.RegressionPblMenu.Show(flex1, e.X, e.Y);
            }
        }

        private void flex1_AfterAddRow(object sender, RowColEventArgs e)
        {
            
        }

        private void flex1_BeforeAddRow(object sender, RowColEventArgs e)
        {
            vc.AddNewRow(ref dtObject);
            e.Cancel = true;
/*            OnPageClose(true);
            flexAssist1.flex.MergedRanges.Clear();//清除合并模式
            OnPageCreate();*/
        }

        private void flex1_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Col < flex1.Cols.Fixed || e.Row < flex1.Rows.Fixed) return;
            flex1.Rows[e.Row].Height = vc.height[e.Row];
        }



/*        private void ClickCaseColumn(HitTestInfo hti, MouseEventArgs e)
        {
            String testCaseName = flex1[hti.Row, hti.Column].ToString();
            String softwareChangeID = vc.GetSoftwareChangeID(hti.Row, "软件更动单ID").ToString();
            String ID = vc.GetSoftwareChangeID(hti.Row, "ID").ToString();
            if (testCaseName == "" || testCaseName == null)
            {
                MessageBox.Show("当前框内无测试用例");
                return;
            }
            RegressionProblemMenu plbMenu = new RegressionProblemMenu(null, dbProject, null, null, softwareChangeID, currentvid, pid, this);
            if (hti.Type == HitTestTypeEnum.Cell & flex1.Cols[hti.Column].Name == "涉及到的测试用例")
            {
                //                menu.RegressionPblMenu.Show(flex1, flex1.Cols[hti.Column].Left, flex1.Rows[hti.Row].Bottom);
                plbMenu.SetSelectTestCaseName(testCaseName, ID);
                plbMenu.testCaseDelete.Show(flex1, e.X, e.Y);
            }
        }*/
        public void DeleteChangeReport(object softwareChangeID)
        {
            if (softwareChangeID == null)
            {
                return;
            }
            string sqlRegressionTest = "select * from HG回归测试影响域 where 软件更动单ID = ?";
            DataTable dt1 = dbProject.ExecuteDataTable(sqlRegressionTest, softwareChangeID);
            foreach (DataRow dr in dt1.Rows)
            {
                string sql = "delete from HG回归测试问题表 WHERE 更动项ID = ?";
                dbProject.ExecuteNoQuery(sql, dr["ID"]);
            }

            string sql0 = "delete from HG回归测试影响域 WHERE 软件更动单ID = ?";
            dbProject.ExecuteNoQuery(sql0, softwareChangeID);
            OnPageClose(true);
            flexAssist1.flex.MergedRanges.Clear();//清除合并模式
            OnPageCreate();
    
        }
        public void DeleteTestCaseFromBase(object softwareChangeID, String testCaseName, object ID)
        {
            if(softwareChangeID == null || testCaseName == null || ID == null)
            {
                return;
            }
            DataRow dr2 = dbProject.ExecuteDataRow("select ID from CA测试用例实体表 where 项目ID = ? and 测试用例名称 = ?", pid, testCaseName);
            //从回归影响域中删除该测试用例
            bool findedTag = false;
            bool isIDTag1 = false;
            bool isIDTag = false;
            foreach (DataRow dr in dtObject.Rows)
            {
                isIDTag = false;
                if (dr["ID"].ToString() == ID.ToString())
                {
                    isIDTag = true;
                    isIDTag1 = true;
                }
                else if (findedTag == true)
                {
                    continue;
                }
                Scattered scatter = new Scattered();
                ArrayList AccordIDList = scatter.GetStr_ToList(dr["涉及到的测试用例"].ToString());
                if (AccordIDList.Count <= 0)
                {
                    continue;
                }
                for (int i = 0; i <= AccordIDList.Count - 1; i++)
                {
                    string AccordID = AccordIDList[i].ToString();
                    if (AccordID == dr2["ID"].ToString())
                    {
                        if(isIDTag)
                        {
                            //删除该测试用例
                            AccordIDList.RemoveAt(i);
                            dr["涉及到的测试用例"] = "";
                            if (AccordIDList.Count == 0)
                            {
                                dr["初次分析标志"] = (object)false;
                            }
                            for (int j = 0; j <= AccordIDList.Count - 1; j++)
                            {
                                string AccordID1 = AccordIDList[j].ToString();
                                if (j != 0)
                                {
                                    dr["涉及到的测试用例"] += ",";
                                }
                                dr["涉及到的测试用例"] += AccordID1;
                            }
                            break;
                        }
                        else if (!findedTag)
                        {
                            //判断该测试用例是否在其他软件更动单中使用
                            findedTag = true;
                            break;
                        }
                    }
                }
                if (findedTag && isIDTag1)
                {
                    break;
                }
            }
            //若没有，从测试用例实测表中删除该测试用例
            if (!findedTag)
            {
                string sql = "delete from CA测试用例实测表 where 测试用例ID = ? and 项目ID = ? and 测试版本 = ?";
                dbProject.ExecuteNoQuery(sql, dr2["ID"], pid, currentvid);
                //查找测试用例实体表，若创建版本和当前版本一致则从测试用例实体表中删除该测试用例
                sql = "delete from CA测试用例实体表 where ID = ? and 项目ID = ? and 创建版本ID = ?";
                dbProject.ExecuteNoQuery(sql, dr2["ID"], pid, currentvid);
            }
            OnPageClose(true);
            flexAssist1.flex.MergedRanges.Clear();//清除合并模式
            OnPageCreate();
             
        }
  
    }
    class ClassRegressionItemCaseTable
    {
        public DataTable dt;
        protected object pid, previd, curid;
        public int[] height = new int[100];
        int m_number;
        Point[] pointArray = new Point[100];
        int pointNum = 0;
        public ClassRegressionItemCaseTable(object pid, object previd, object curid)
        {
            dt = new DataTable("回归测试影响域分析");
            GridAssist.AddColumn(dt, "ID", "软件更动单ID", "更动项", "更动报告单号", "涉及到的软件问题", "涉及到的测试用例", "测试列表", "测试问题涉及的测试用例", "项目ID", "测试版本");
            GridAssist.AddColumn<int>(dt, "序号");
            GridAssist.AddColumn<bool>(dt, "初次分析标志");
            this.pid = pid;
            this.previd = previd;
            this.curid = curid;
            m_number = 0;
            for (int i = 0; i < 100; i++)
            {
                height[i] = 20;
            }
        }

        public int NumberAdd()
        {
            m_number++;
            return m_number;
        }
        public object GetSoftwareChangeID(int rownum, string colname)
        {
            return dt.Rows[rownum - 1][colname];
        }
        public void AddNewRow( ref DataTable dtObject)
        {
            DataColumnCollection dcc1 = dt.Columns;
            dcc1["ID"].DefaultValue = FunctionClass.NewGuid;
            dcc1["软件更动单ID"].DefaultValue = FunctionClass.NewGuid;
            dcc1["序号"].DefaultValue = NumberAdd().ToString();
            dcc1["更动报告单号"].DefaultValue = "";
            dcc1["更动项"].DefaultValue = "";
            dcc1["涉及到的软件问题"].DefaultValue = "";
            dcc1["测试问题涉及的测试用例"].DefaultValue = "";
            dcc1["涉及到的测试用例"].DefaultValue = "";
            dcc1["测试列表"].DefaultValue = "";
            dcc1["初次分析标志"].DefaultValue = false;
            dt.Rows.Add();
            DataColumnCollection dcc = dtObject.Columns;
            dcc["ID"].DefaultValue = dcc1["ID"].DefaultValue;
            dcc["软件更动单ID"].DefaultValue = dcc1["软件更动单ID"].DefaultValue;
            dcc["序号"].DefaultValue = dcc1["序号"].DefaultValue;
            dcc["更动项"].DefaultValue = dcc1["更动项"].DefaultValue;
            dcc["更动报告单号"].DefaultValue = dcc1["更动报告单号"].DefaultValue;
            dcc["涉及到的测试用例"].DefaultValue = dcc1["涉及到的测试用例"].DefaultValue;
            dcc["测试问题涉及的测试用例"].DefaultValue = dcc1["测试问题涉及的测试用例"].DefaultValue;
            dcc["初次分析标志"].DefaultValue = dcc1["初次分析标志"].DefaultValue;
            dcc["项目ID"].DefaultValue = pid;
            dcc["测试版本"].DefaultValue = curid;
            dtObject.Rows.Add();  
        }
        public bool GetSoftChangeTag(object softChangeID)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["软件更动单ID"].ToString() == softChangeID.ToString() && dr["涉及到的软件问题"].ToString() == "")
                {

                    return true;
                }
            }
            return false;
        }
        //添加更动项
        public void AddChangeItem(object ID, ref DataTable dtObject)
        {
            DataColumnCollection dcc1 = dt.Columns;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ID"].ToString() == ID.ToString())
                {

                    dcc1["ID"].DefaultValue = FunctionClass.NewGuid;
                    dcc1["软件更动单ID"].DefaultValue = dr["软件更动单ID"];
                    dcc1["序号"].DefaultValue = dr["序号"];
                    dcc1["更动项"].DefaultValue = "";
                    dcc1["更动报告单号"].DefaultValue = dr["更动报告单号"];
                    dcc1["测试问题涉及的测试用例"].DefaultValue = "";
                    dcc1["涉及到的测试用例"].DefaultValue = "";
                    dcc1["涉及到的软件问题"].DefaultValue = "";
                    dcc1["测试列表"].DefaultValue = "";
                    dcc1["初次分析标志"].DefaultValue = false;
                    dt.Rows.Add();
                    break;
                }
            }
            DataColumnCollection dcc = dtObject.Columns;
            dcc["ID"].DefaultValue = dcc1["ID"].DefaultValue;
            dcc["软件更动单ID"].DefaultValue = dcc1["软件更动单ID"].DefaultValue;
            dcc["序号"].DefaultValue = dcc1["序号"].DefaultValue;
            dcc["更动项"].DefaultValue = dcc1["更动项"].DefaultValue;
            dcc["更动报告单号"].DefaultValue = dcc1["更动报告单号"].DefaultValue;
            dcc["涉及到的测试用例"].DefaultValue = dcc1["涉及到的测试用例"].DefaultValue;
            dcc["测试问题涉及的测试用例"].DefaultValue = dcc1["测试问题涉及的测试用例"].DefaultValue;
            dcc["初次分析标志"].DefaultValue = dcc1["初次分析标志"].DefaultValue;
            dcc["项目ID"].DefaultValue = pid;
            dcc["测试版本"].DefaultValue = curid;
            dtObject.Rows.Add();    
            
        }
        public void DeleteChangeItem(object ID, ref DataTable dtObject, DBAccess dbProject)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].RowState == DataRowState.Deleted)
                {
                    continue;
                }
                if (dt.Rows[i]["ID"].ToString() == ID.ToString())
                {
                    dt.Rows[i].Delete();
                }

            }
            string sql = "delete from HG回归测试问题表 where 更动项ID = ?";
            dbProject.ExecuteNoQuery(sql, ID);
            for (int i = 0; i < dtObject.Rows.Count; i++)
            {
                if (dtObject.Rows[i].RowState == DataRowState.Deleted)
                {
                    continue;
                }
                if (dtObject.Rows[i]["ID"].ToString() == ID.ToString())
                {
                    dtObject.Rows[i].Delete();
                    break;
                }
            }

        }
        //添加软件扩展更动
        public void AddSoftChange(object ID, ref DataTable dtO)
        {
            DataColumnCollection dcc1 = dt.Columns;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ID"].ToString() == ID.ToString())
                {

                    dcc1["ID"].DefaultValue = FunctionClass.NewGuid;
                    dcc1["软件更动单ID"].DefaultValue = ID;
                    dcc1["序号"].DefaultValue = dr["序号"];
                    dcc1["更动说明"].DefaultValue = dr["更动说明"];
                    dcc1["更动报告单号"].DefaultValue = "";
                    dcc1["涉及到的软件问题"].DefaultValue = "";
                    dcc1["涉及到的测试项"].DefaultValue = "";
                    dcc1["涉及到的测试用例"].DefaultValue = "";
                    dcc1["初次分析标志"].DefaultValue = false;
                    dt.Rows.Add();
                    break;
                }
            }
            DataColumnCollection dcc = dtO.Columns;
            dcc["ID"].DefaultValue = dcc1["ID"].DefaultValue;
            dcc["软件更动单ID"].DefaultValue = dcc1["软件更动单ID"].DefaultValue;
            dcc["序号"].DefaultValue = dcc1["序号"].DefaultValue;
            dcc["更动说明"].DefaultValue = dcc1["更动说明"].DefaultValue;
            dcc["更动报告单号"].DefaultValue = dcc1["更动报告单号"].DefaultValue;
            dcc["涉及到的软件问题"].DefaultValue = dcc1["涉及到的软件问题"].DefaultValue;
            dcc["涉及到的测试用例"].DefaultValue = dcc1["涉及到的测试用例"].DefaultValue;
            dcc["初次分析标志"].DefaultValue = dcc1["初次分析标志"].DefaultValue;
            dcc["项目ID"].DefaultValue = pid;
            dcc["测试版本"].DefaultValue = curid;
            dtO.Rows.Add();    
        }
        public void DeleteSoftChange(object softChangeID, ref DataTable dtO)
        {
            //后续还要做测试用例的删除
            for (int i = 0; i < dt.Rows.Count; i++ )
            {
                if (dt.Rows[i]["软件更动单ID"].ToString() == softChangeID.ToString() && dt.Rows[i]["涉及到的软件问题"].ToString() == "")
                {
                    dt.Rows[i].Delete();
                    i--;
                }

            }
            foreach (DataRow dr in dtO.Rows)
            {
                if (dr["软件更动单ID"].ToString() == softChangeID.ToString() && dr["涉及到的软件问题"].ToString() == "")
                {
                    dr.Delete();
                    break;
                }
            }
        }
        public void UpdateDataTable(ref DataTable dtO)
        {
            foreach (DataRow drtO in dtO.Rows)
            {
                if (drtO.RowState == DataRowState.Deleted)
                {
                    continue;
                }
                foreach (DataRow drt in dt.Rows)
                {
                    if (drt.RowState == DataRowState.Deleted)
                    {
                        continue;
                    }
                    if (drtO["ID"].ToString() == drt["ID"].ToString())
                    {
                        drtO["更动项"] = drt["更动项"];
                        drtO["更动报告单号"] = drt["更动报告单号"];
                        break ;
                    }
                }
            }
        }
        public DataTable GetTestCaseOfChange(object ID)
        {
            DataTable testCaseTable = new DataTable("涉及的测试用例");
            GridAssist.AddColumn(testCaseTable, "ID", "测试项ID", "测试用例ID","项目ID", "测试版本");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ID"].ToString() == ID.ToString() && dr["涉及到的测试用例"].ToString() != "")
                {
                    Scattered scatter = new Scattered();
                    ArrayList AccordIDList = scatter.GetStr_ToList(dr["涉及到的测试用例"].ToString());
                    if (AccordIDList.Count <= 0)
                    {
                        break;
                    }
                    for (int i = 0; i <= AccordIDList.Count - 1; i++)
                    {
                        object AccordID = AccordIDList[i];
                        DataColumnCollection dcc = testCaseTable.Columns;

                        dcc["ID"].DefaultValue = dr["ID"];//基本没用
                        dcc["测试用例ID"].DefaultValue = AccordID;
                        dcc["项目ID"].DefaultValue = pid;
                        dcc["测试版本"].DefaultValue = curid;
                        testCaseTable.Rows.Add();
                    }
                    break;
                }
            }
            return testCaseTable;
            
        }
        public DataTable GetTestCaseOfChangePlb(object ID)
        {
            DataTable testCaseTable = new DataTable("测试问题涉及的测试用例");
            GridAssist.AddColumn(testCaseTable, "ID", "测试项ID", "测试用例ID","项目ID", "测试版本");
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ID"].ToString() == ID.ToString() && dr["测试问题涉及的测试用例"].ToString() != "")
                {
                    Scattered scatter = new Scattered();
                    ArrayList AccordIDList = scatter.GetStr_ToList(dr["测试问题涉及的测试用例"].ToString());
                    if (AccordIDList.Count <= 0)
                    {
                        break;
                    }
                    for (int i = 0; i <= AccordIDList.Count - 1; i++)
                    {
                        object AccordID = AccordIDList[i];
                        DataColumnCollection dcc = testCaseTable.Columns;

                        dcc["ID"].DefaultValue = dr["ID"];//基本没用
                        dcc["测试用例ID"].DefaultValue = AccordID;
                        dcc["项目ID"].DefaultValue = pid;
                        dcc["测试版本"].DefaultValue = curid;
                        testCaseTable.Rows.Add();
                    }
                    break;
                }
            }
            return testCaseTable;
            
        }
        
        public void GetAllClassItemCase(TestResultSummary summarypre, TestResultSummary summarycur, DataTable dtObject, 
            DBAccess dbProject, ref C1FlexGrid flex)
        {
            object lastPlbID = "开始";
            object lastOrder = 0;
            int lineNum = 1;
            int num = 0;
            foreach (DataRow drt in dtObject.Rows)
            {
                DataColumnCollection dcc = dt.Columns;
                dcc["ID"].DefaultValue = drt["ID"];//和更动项ID一致
                dcc["更动报告单号"].DefaultValue = drt["更动报告单号"];
                dcc["软件更动单ID"].DefaultValue = drt["软件更动单ID"];//和更动报告单号一致
                dcc["序号"].DefaultValue = drt["序号"];
                m_number = (int)drt["序号"];
                dcc["更动项"].DefaultValue = drt["更动项"];
                dcc["初次分析标志"].DefaultValue = drt["初次分析标志"];
                if ((bool)drt["初次分析标志"])
                {
                    dcc["涉及到的测试用例"].DefaultValue = drt["涉及到的测试用例"];
                }
                string testCaseOut = "";
                num = SetSoftPlb(summarypre, summarycur, drt["ID"], dt, dbProject, (bool)drt["初次分析标志"], drt["涉及到的测试用例"], ref testCaseOut, lineNum);//设置涉及到的软件问题和测试列表
                lineNum += num;
                if (!(bool)drt["初次分析标志"])
                {
                    drt["涉及到的测试用例"] = testCaseOut;
                    drt["测试问题涉及的测试用例"] = testCaseOut;
                    dcc["涉及到的测试用例"].DefaultValue = drt["涉及到的测试用例"];
                    dcc["测试问题涉及的测试用例"].DefaultValue = drt["测试问题涉及的测试用例"];
                    drt["初次分析标志"] = true;
                    dcc["初次分析标志"].DefaultValue = drt["初次分析标志"];
                }

            }
        }
        //设置软件问题
        public int SetSoftPlb(TestResultSummary summarypre, TestResultSummary summarycur, object softChangeID, DataTable dtt, DBAccess dbProject, bool isAnalysisTag, object testCaseList, ref string testCaseOut, int lineNum)
        {
            int lineSum = 0;
            if (softChangeID.ToString() == "")
            {
                dtt.Rows.Add();
                lineSum++;
            }
            else
            {
                DataTable dt1 = dbProject.ExecuteDataTable("SELECT * FROM HG回归测试问题表 WHERE 更动项ID = ?;", softChangeID);
                if (!isAnalysisTag)
                {
                    lineSum = SetTestCaseAndListFromPlb(summarypre, dt1, dtt, dbProject, ref testCaseOut, lineNum);
                }
                else
                {
                    bool tag = false;
                    string testList = "";
                    int num = 0;
                    foreach (DataRow drt1 in dt1.Rows)
                    {
                        DataRow dr = dtt.Rows.Add();
                        lineSum++;
                        if (drt1["软件问题类型"].ToString() == "非测试问题")
                        {
                            dr["涉及到的软件问题"] = drt1["软件问题"];
                        }
                        else 
                        {
                            dr["涉及到的软件问题"] = GetPlbSign(dbProject, drt1["软件问题"]);
                        }
                        if (!tag)
                        {
                            tag = true;
                            Scattered scatter = new Scattered();
                            testList = "";
                            ArrayList AccordIDList = scatter.GetStr_ToList(testCaseList.ToString());

                            num = SetTestList(summarycur, ref testList, AccordIDList);
                            dr["测试列表"] = testList;
                        }
                        else 
                        {
                            dr["测试列表"] = testList;
                        
                        }
                    }
                    if (num > lineSum)
                    {
                        int addHeight = (num - lineSum) * 20 / lineSum + 1;
                        for (int i = 0; i < lineSum; i++)
                        {
                            height[lineNum + i] += addHeight;
                        }
                    }

                    if (dt1.Rows.Count <= 0)
                    {
                        dtt.Rows.Add();
                        lineSum++;
                    }
                }
            }
            return lineSum;
        }
        private String GetPlbSign(DBAccess dbProject, object plbID)
        {
            String plbSign = "";
            DataRow dr1 = dbProject.ExecuteDataRow("SELECT * FROM CA问题报告单 WHERE ID = ?;", plbID);
            DataRow dr2 = dbProject.ExecuteDataRow("SELECT 简写码" +
                           " FROM CA被测对象实体表 INNER JOIN CA被测对象实测表  ON CA被测对象实测表.被测对象ID = CA被测对象实体表.ID " +
                           " WHERE CA被测对象实测表.ID=? ;", dr1["所属被测对象ID"]);
            plbSign = dr2["简写码"].ToString() + "_" + dr1["同标识序号"].ToString() + "[" + dr1["名称"].ToString() + "]";
            return plbSign;
            
        }
        public bool GetForColor(int row, int col)
        {
            for (int i = 0; i < pointNum; i++)
            {
                if (pointArray[i].X == row && pointArray[i].Y == col)
                {
                    return true;
                }
            }
            return false;
        }
        //直接从测试用例中获得
        private int SetTestList(TestResultSummary summary, ref string testList, ArrayList AccordIDList)
        {
            int lineNum = 0;
            testList = "";
            //首先找到一个测试用例，找到其测试对象，测试类型，测试项//前提默认测试用例已经排序
            ItemNodeTree curObject = null, curType = null, curItem = null, curTestCase = null;
            ItemNodeTree tempItem;
            for (int i = 0; i <= AccordIDList.Count - 1; i++)
            {
                string AccordID = AccordIDList[i].ToString();
                curTestCase = summary.GetSubNode(AccordID, NodeType.TestCase);
                if (curTestCase == null) continue;
                tempItem = curTestCase.GetLeastParent(NodeType.TestObject);
                if (curObject == null)
                {
                    testList += "◇┈□" + tempItem.name + "\n";
                    curObject = tempItem;
                    lineNum++;

                }
                else if (curObject.name != tempItem.name)
                {
                    testList += "◇┈□" + tempItem.name + "\n";
                    curObject = tempItem;
                    lineNum++;
                }
                tempItem = curTestCase.GetLeastParent(NodeType.TestType);
                if (curType == null)
                {
                    testList += "┆  ├┈□" + tempItem.name + "\n";
                    curType = tempItem;
                    lineNum++;

                }
                else if (curType.name != tempItem.name)
                {
                    testList += "┆  ├┈□" + tempItem.name + "\n";
                    curType = tempItem;
                    lineNum++;
                }
                tempItem = curTestCase.GetLeastParent(NodeType.TestItem);
                if (curItem == null)
                {
                    testList += "┆  ┆  ├┈□" + tempItem.name + "\n";
                    curItem = tempItem;
                    lineNum++;

                }
                else if (curItem.name != tempItem.name)
                {
                    testList += "┆  ┆  ├┈□" + tempItem.name + "\n";
                    curItem = tempItem;
                    lineNum++;
                }
                testList += "┆  ┆  ┆  ├┈□" + curTestCase.name + "\n";
                lineNum++;
            }
            return lineNum;
        }
        //从前向版本中找测试问题涉及到的测试用例
        private int SetTestCaseAndListFromPlb(TestResultSummary summarypre, DataTable dtt1, DataTable dtt, DBAccess dbProject, ref string testCaseOut, int lineNum)
        {
            int lineSum = 0;
            //首先找到测试问题涉及到的测试用例
            string sqlRegressionTest = "SELECT DISTINCT CA测试过程实测表.问题报告单ID, CA测试过程实测表.过程ID, CA测试过程实测表.测试版本, CA测试过程实测表.项目ID, CA测试过程实体表.ID,CA测试过程实体表.测试用例ID" +
                           " FROM CA测试过程实测表 INNER JOIN CA测试过程实体表  ON CA测试过程实测表.过程ID = CA测试过程实体表.ID " +
                           " WHERE CA测试过程实测表.问题报告单ID=? AND CA测试过程实测表.测试版本=? AND CA测试过程实测表.项目ID=?;";
            DataTable dt2;
            ArrayList testCaseList = new ArrayList();
            bool tag = false;
            foreach (DataRow dr in dtt1.Rows)
            {
                if (dr["软件问题类型"].ToString() == "测试问题")
                {
                    dt2 = dbProject.ExecuteDataTable(sqlRegressionTest, dr["软件问题"], previd, pid);
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        tag = false;
                        for (int i = 0; i < testCaseList.Count; i++)
                        {
                            if (testCaseList[i].ToString() == dr2["测试用例ID"].ToString())
                            {
                                tag = true;
                                break;
                            }
                        }
                        if (!tag)
                        {
                            testCaseList.Add(dr2["测试用例ID"]);
                        }
                    }

                }
            }
            IComparer myComparer = new TestCaseCompareClass(summarypre);
            testCaseList.Sort(myComparer);
            testCaseOut = "";
            for (int j = 0; j <= testCaseList.Count - 1; j++)
            {
                string AccordID1 = testCaseList[j].ToString();
                if (j != 0)
                {
                    testCaseOut += ",";
                }
                testCaseOut += AccordID1;
            }

            tag = false;
            string testList = "";
            int num = 0;
            foreach (DataRow drt1 in dtt1.Rows)
            {
                DataRow dr = dtt.Rows.Add();
                lineSum++;
                if (drt1["软件问题类型"].ToString() == "非测试问题")
                {
                    dr["涉及到的软件问题"] = drt1["软件问题"];
                }
                else
                {
                    dr["涉及到的软件问题"] = GetPlbSign(dbProject, drt1["软件问题"]);
                }
                if (!tag)
                {
                    tag = true;
                    num = SetTestList(summarypre, ref testList, testCaseList);
                    dr["测试列表"] = testList;
                }
                else
                {
                    dr["测试列表"] = testList;

                }
            }
            if (num > lineSum)
            {
                int addHeight = (num - lineSum) * 20 / lineSum + 1;
                for (int i = 0; i < lineSum; i++)
                {
                    height[lineNum + i] += addHeight;
                }
            }
            if (dtt1.Rows.Count <= 0)
            {
                lineSum++;
                dtt.Rows.Add();
            }
            return lineSum;
        }
        private int SetTestCaseTable(string TestProblemStr, TestResultSummary summary, DataTable dt, ref C1FlexGrid flex,int curline, DBAccess dbProject)
        {
            Scattered scatter = new Scattered();

            ArrayList AccordIDList = scatter.GetStr_ToList(TestProblemStr);
            int num = 0;
            object itemO = "";
            bool tag;
            for (int i = 0; i <= AccordIDList.Count - 1; i++)
            {
                string AccordID = AccordIDList[i].ToString();
                ItemNodeTree item = summary.GetSubNode(AccordID, NodeType.TestCase);
                if (item == null) continue;
                DataRow dr = dt.Rows.Add();
                tag = CreateVersionIsCurrent(item.id, NodeType.TestCase, dbProject, pid, curid);
                dr["用例ID"] = item.id;
                if (tag)
                {
                    pointArray[pointNum].X = curline + i;
                    pointArray[pointNum].Y = 5;
                    pointNum++;
                }
                dr["涉及到的测试用例"] = item.name;
                item = item.GetLeastParent(NodeType.TestItem);
                tag = CreateVersionIsCurrent(item.id, NodeType.TestItem, dbProject, pid, curid);
                dr["条目ID"] = item.id;
                if (tag)
                {
                    pointArray[pointNum].X = curline + i;
                    pointArray[pointNum].Y = 4;
                    pointNum++;
                }
                dr["涉及到的测试项"] = item.name;
                if (i == 0)
                {
                    itemO = dr["涉及到的测试项"];
                }
                else
                {
                    if (itemO == dr["涉及到的测试项"])
                    {
                        num++;
                    }
                    else 
                    {
                        if (num > 0)
                        {//自定义合并
                            CellRange cr;
                            cr = flex.GetCellRange(curline + i - 1 - num , 4, curline + i - 1 , 4);
                            flex.MergedRanges.Add(cr);
                            num = 0;
                        }
                        itemO = dr["涉及到的测试项"];
                    }
                }
            }
            if (num > 0)
            {
                CellRange cr;
                cr = flex.GetCellRange(curline + AccordIDList.Count - 1 - num, 4, curline + AccordIDList.Count - 1, 4);
                flex.MergedRanges.Add(cr);
            }

            if (AccordIDList.Count == 0)
            {
                DataRow dr = dt.Rows.Add();
                dr["涉及到的测试用例"] = "";
                return 1;
            }
            return AccordIDList.Count;
        }
        public static bool CreateVersionIsCurrent(object itemID, NodeType itemType, DBAccess dbProject, object pid, object curid)
        {
            DataRow dr = null;
            switch (itemType)
            { 
                case NodeType.TestCase:
                    dr = dbProject.ExecuteDataRow("SELECT * FROM CA测试用例实体表 WHERE ID = ? and 项目ID = ? and 创建版本ID = ?;", itemID, pid, curid);
                    break;
                case NodeType.TestItem:
                    dr = dbProject.ExecuteDataRow("SELECT * FROM CA测试项实体表 WHERE ID = ? and 项目ID = ? and 创建版本ID = ?;", itemID, pid, curid);
                    break;
                case NodeType.TestType:
                    dr = dbProject.ExecuteDataRow("SELECT * FROM CA测试类型实体表 WHERE ID = ? and 项目ID = ? and 创建版本ID = ?;", itemID, pid, curid);
                    break;
                case NodeType.TestObject:
                    dr = dbProject.ExecuteDataRow("SELECT * FROM CA被测对象实体表 WHERE ID = ? and 项目ID = ? and 创建版本ID = ?;", itemID, pid, curid);
                    break;
                default:
                    break;
            }
            if (dr == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //将问题报告单添加到回归测试域表中
        private int SetProblemItemCase(String AccordID, TestResultSummary summary, DataTable dt, DBAccess dbProject, ref C1FlexGrid flex, int curline)
        {
            string sqlRegressionTest = "SELECT DISTINCT CA测试过程实测表.问题报告单ID, CA测试过程实测表.过程ID, CA测试过程实测表.测试版本, CA测试过程实测表.项目ID, CA测试过程实体表.ID,CA测试过程实体表.测试用例ID" +
                           " FROM CA测试过程实测表 INNER JOIN CA测试过程实体表  ON CA测试过程实测表.过程ID = CA测试过程实体表.ID " +
                           " WHERE CA测试过程实测表.问题报告单ID=? AND CA测试过程实测表.测试版本=? AND CA测试过程实测表.项目ID=?;";
            DataTable dt2 = dbProject.ExecuteDataTable(sqlRegressionTest, AccordID, previd, pid);
            int num = 0;
            object itemO = "";
            int i = 0;
            bool tag;
            foreach (DataRow drt in dt2.Rows)
            {   // 对每个用例或者条目，增加一条记录
                ItemNodeTree item = summary.GetSubNode(drt["测试用例ID"], NodeType.TestCase);
                if (item == null) continue;
                
                DataRow dr = dt.Rows.Add();
                tag = CreateVersionIsCurrent(item.id, NodeType.TestCase, dbProject, pid, curid);
                dr["用例ID"] = item.id;
                if (tag)
                {
                    pointArray[pointNum].X = curline + num;
                    pointArray[pointNum].Y = 5;
                    pointNum++;
                }
                dr["涉及到的测试用例"] = item.name;
                item = item.GetLeastParent(NodeType.TestItem);
                tag = CreateVersionIsCurrent(item.id, NodeType.TestItem, dbProject, pid, curid);
                dr["条目ID"] = item.id;
                if (tag)
                {
                    pointArray[pointNum].X = curline + num;
                    pointArray[pointNum].Y = 4;
                    pointNum++;
                }
                dr["涉及到的测试项"] = item.name;
                if (i == 0)
                {//自定义合并
                    itemO = dr["涉及到的测试项"];
                }
                else
                {
                    if (itemO == dr["涉及到的测试项"])
                    {
                        num++;
                    }
                    else
                    {
                        if (num > 0)
                        {
                            CellRange cr;
                            cr = flex.GetCellRange(curline, 4, curline + num, 4);
                            flex.MergedRanges.Add(cr);
                            curline += num + 1;
                            num = 0;
                        }
                        else
                        {
                            curline++;
                        }
                        itemO = dr["涉及到的测试项"];
                    }
                }
                i++;
            }
            if (num > 0)
            {
                CellRange cr;
                cr = flex.GetCellRange(curline, 4, curline + num, 4);
                flex.MergedRanges.Add(cr);
            }
            if (dt2.Rows.Count == 0)     // 该测试项下没有对应的测试用例
            {
                DataRow dr = dt.Rows.Add();
                dr["涉及到的测试用例"] = "";
                return 1;
            }
            return dt2.Rows.Count;

        }
    }

    public class TestCaseCompareClass : IComparer  {
      TestResultSummary summary;
      public TestCaseCompareClass(TestResultSummary summary)
      {
          this.summary = summary;
      }

      // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
      int IComparer.Compare( Object x, Object y )  
      {
            ItemNodeTree tempItem1, tempItem2, case1, case2;
            case1 = summary.GetSubNode(x, NodeType.TestCase);
            case2 = summary.GetSubNode(y, NodeType.TestCase);
            if (case1 == null)
            {
                return -1;
            }
            else if(case2 == null )
            {
                return 1;
            }
            tempItem1 = case1.GetLeastParent(NodeType.TestObject);
            tempItem2 = case2.GetLeastParent(NodeType.TestObject);
            if (tempItem1.index > tempItem2.index)
            {
                return 1;
            }
            else if (tempItem1.index < tempItem2.index)
            {
                return -1;
            }
            tempItem1 = case1.GetLeastParent(NodeType.TestType);
            tempItem2 = case2.GetLeastParent(NodeType.TestType);
            if (tempItem1.index > tempItem2.index)
            {
                return 1;
            }
            else if (tempItem1.index < tempItem2.index)
            {
                return -1;
            }
            tempItem1 = case1.GetLeastParent(NodeType.TestItem);
            tempItem2 = case2.GetLeastParent(NodeType.TestItem);
            if (tempItem1.index > tempItem2.index)
            {
                return 1;
            }
            else if (tempItem1.index < tempItem2.index)
            {
                return -1;
            }
            if (case1.index > case2.index)
            {
                return 1;
            }
            else if (case1.index < case2.index)
            {
                return -1;
            }
            else
            {
                return 0;
            }
      }

   }

}
