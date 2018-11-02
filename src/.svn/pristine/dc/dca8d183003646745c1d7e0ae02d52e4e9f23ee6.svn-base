using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Common;
using Common.Database;
using Common.RichTextBox;
using TPM3.wx;

namespace TPM3.Upgrade
{
    public class DatabaseConvert254 : IDatabaseConvertor, IDisposable
    {
        string _srcdatabase;        // 待转换的数据库
        string _emptydatabase;      // 空的目标数据库
        string _desdatabase;        // 转换后的数据库
        public string dbVersion = "";  // 旧库的数据库版本号

        bool _bconvertable = false; // 可转换指示
        DBAccess _dbsrc, _dbdes;

        public event EventHandler StatusChange;  // 转换数据库进度通知事件

        public DatabaseConvert254(string srcdatabase, string emptydatabase)
        {
            this._emptydatabase = emptydatabase;
            this._srcdatabase = srcdatabase;

            _dbsrc = DBAccessFactory.FromAccessFile(srcdatabase).CreateInst();
            _dbsrc.localErrorHandler = DBAccessStaticFunctionClass.ThrowException;

            if(_dbsrc == null)
            {
                _statusmsg = "创建源数据库连接错误";
                _bconvertable = false;
                return;
            }

            try
            {
                string sql = "SELECT 文本内容 FROM 系统文档内容表 where 文档名称 = ? and 内容标题 = ?";
                dbVersion = _dbsrc.ExecuteScalar(sql, "项目信息", "数据库版本") as string;
            }
            catch(Exception ex)
            {

            }
            if(dbVersion != "2.5")
            {
                _statusmsg = "非当前类支持的数据库版本";
                _bconvertable = false;
                return;
            }

            // 创建新的数据库文件
            if(!CreateNewDatabase())
            {
                _statusmsg = "创建新的数据库文件错误";
                _bconvertable = false;
                return;
            }

            _dbdes = DBAccessFactory.FromAccessFile(_desdatabase).CreateInst();
            if(_dbdes == null)
            {
                _statusmsg = "创建目标数据库连接错误";
                _bconvertable = false;
                return;
            }

            //   ClearDatabase();

            StatusMsg = "已正确初始化";
            _bconvertable = true;
        }

        public bool StartConvert()
        {
            try
            {
                if(!_bconvertable)
                    return false;

                //if(!ConvertSysDocContent())
                //    return false;

                //ConvertAssisTable();

                //ConvertAllUsecases();

                //if(!ConvertObjTable())
                //    return false;

                _dbdes.CloseConnection();
                _dbsrc.CloseConnection();
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void ClearDatabase()
        {
            string sql = "DELETE FROM 测试项优先级表";
            _dbdes.ExecuteNoQuery(sql);

            sql = "DELETE FROM 测试用例设计方法表";
            _dbdes.ExecuteNoQuery(sql);

            sql = "DELETE FROM 测试类型模板表";
            _dbdes.ExecuteNoQuery(sql);

            sql = "DELETE FROM 测试种类表";
            _dbdes.ExecuteNoQuery(sql);

            sql = "DELETE FROM 问题级别表";
            _dbdes.ExecuteNoQuery(sql);

            sql = "DELETE FROM 问题类别表";
            _dbdes.ExecuteNoQuery(sql);

            sql = "DELETE FROM 质量特性";
            _dbdes.ExecuteNoQuery(sql);
        }

        /// <summary>
        /// 新数据库名 = 旧数据库名  +  版本号
        /// </summary>
        bool CreateNewDatabase()
        {
            string ext = Path.GetExtension(_srcdatabase);
            _desdatabase = Path.ChangeExtension(_srcdatabase, UpgradeDatabase.DBVersion + ext);

            File.Copy(_emptydatabase, _desdatabase, true);
            FileInfo fi = new FileInfo(_desdatabase);
            if(fi.IsReadOnly)
            {
                fi.Attributes = fi.Attributes & ~FileAttributes.ReadOnly;
            }
            return true;
        }

        private string _statusmsg = "未初始化";
        public string StatusMsg
        {
            get { return _statusmsg; }
            set
            {
                _statusmsg = value;

                OnStatusChange(this, EventArgs.Empty);
            }
        }

        private void OnStatusChange(object sender, EventArgs ev)
        {
            if(StatusChange != null)
                StatusChange(sender, ev);
        }

        /// <summary>
        /// 将原数据库的表"项目公共信息表"转换为现版本数据库的"系统文档内容表"
        /// {原数据表中的字段"项目ID"和"对所有被测对象的说明"的数据没有导入}
        /// </summary>
        /// <returns></returns>
        private bool ConvertSysDocContent()
        {
            if(!_bconvertable)
                return false;

            string sql = "SELECT * FROM 项目公共信息表";
            DataRow dr = _dbsrc.ExecuteDataRow(sql);
            if(dr == null)
            {
                _statusmsg = @"原数据库表{项目公共信息表}无记录";
                return false;
            }

            DocContentDB db = new DocContentDB(_dbdes);
            string[] titleList = new[] { "项目名称", "项目标识号", "项目数据库名称", "项目简写码", "测试单位", "版本", "测试分类级", "测试级", "密级", "被测软件版本", "对应程序版本", "测试分类说明", "被测软件名称", "被测软件标识", "开发单位" };
            foreach(string title in titleList)
                db.SetProjectContent(null, title, dr[title].ToString());

            StatusMsg = "成功导入项目公共信息表至系统文档内容表!";
            return true;
        }

        /// <summary>
        /// 转换被测对象表
        /// {'测试执行情况_补充'字段没有导入}
        /// </summary>
        /// <returns></returns>
        private bool ConvertObjTable()
        {
            if(!_bconvertable)
                return false;

            string sql = "SELECT * FROM 被测对象表 ORDER BY 被测对象ID";
            DataTable dtdes = _dbdes.ExecuteDataTable(sql);

            int index = 1;
            DataTable table = _dbsrc.ExecuteDataTable(sql);
            foreach(DataRow row in table.Rows)
            {
                DataRow rownew = dtdes.NewRow();

                rownew["序号"] = index++;
                rownew["被测对象名称"] = row["被测对象名称"];
                rownew["简写码"] = row["简写码"];
                rownew["测试开始日期"] = row["测试开始日期"];
                rownew["测试结束日期"] = row["测试结束日期"];
                rownew["测试执行情况"] = row["测试执行情况"];
                /*rownew["质量评估"]      = row["质量评估"];
                rownew["改进建议"]      = row["改进建议"];
                rownew["测试进度"]      = row["测试进度对象"];
                rownew["硬件准备"]      = row["硬件准备对象"];
                rownew["软件准备"]      = row["软件准备对象"];
                rownew["其它测试准备"]  = row["其它测试准备对象"];*/
                rownew["测试执行情况_补充"] = IOleObjectAssist.GetByteFromString(row["测试执行情况_补充"].ToString());

                if(!DBNull.Value.Equals(row["质量评估"]))
                    rownew["质量评估"] = IOleObjectAssist.ConvertAccessOleObject(row["质量评估"]);
                if(!DBNull.Value.Equals(row["改进建议"]))
                    rownew["改进建议"] = IOleObjectAssist.ConvertAccessOleObject(row["改进建议"]);

                rownew["测试进度"] = ConvertOleText(row, "测试进度对象与否", "测试进度对象", "测试进度文本");
                rownew["硬件准备"] = ConvertOleText(row, "硬件准备对象与否", "硬件准备对象", "硬件准备文本");
                rownew["软件准备"] = ConvertOleText(row, "软件准备对象与否", "软件准备对象", "软件准备文本");
                rownew["其它测试准备"] = ConvertOleText(row, "其它测试准备对象与否", "其它测试准备对象", "其它测试准备文本");

                rownew["版本"] = "待定";
                rownew["测试级别"] = "未定义";

                dtdes.Rows.Add(rownew);
                _dbdes.UpdateDatabase(dtdes, sql);

                int objid = (int)_dbdes.ExecuteScalar("SELECT top 1 被测对象ID FROM 被测对象表 ORDER BY 被测对象ID DESC");

                // 导入原数据库的每个被测对象的测试类型表(测试表)
                ConvertTypeTable(objid, (int)row["被测对象ID"]);
            }

            StatusMsg = "导入被测对象表成功!";
            return true;
        }

        static object ConvertOleText(DataRow dr, string IsObject, string obj, string txt)
        {
            object ret = DBNull.Value;
            if(false.Equals(dr[IsObject]))
                ret = IOleObjectAssist.GetByteFromString(dr[txt].ToString());
            else if(!GridAssist.IsNull(dr[obj]))
                ret = IOleObjectAssist.ConvertAccessOleObject(dr[obj]);
            return ret;
        }

        /// <summary>
        /// 导入每个被测对象的测试类型表(测试表)
        /// </summary>
        private void ConvertTypeTable(int desobjid, int srcobjid)
        {
            string sqladd = "SELECT * FROM 测试类型表";
            string sqlOld = "SELECT a.测试ID, a.所属被测对象ID, a.所属测试集ID, b.测试种类, b.测试的名称, b.简写码, c.质量特性ID, c.特性名称, c.简写码, b.ID FROM (质量特性 AS c INNER JOIN 测试集表 AS b ON c.质量特性ID = b.质量特性ID) INNER JOIN 测试表 AS a ON b.ID = a.所属测试集ID where a.所属被测对象ID = ? ORDER BY c.质量特性ID, b.ID";
            string sqlGetNewID = "select top 1 测试类型ID from 测试类型表 order by 测试类型ID DESC";

            int index = 1, qualityIndex = 1;
            DataTable dtDest = _dbdes.ExecuteDataTableSchema(sqladd);
            DataTable dtOld = _dbsrc.ExecuteDataTable(sqlOld, srcobjid);

            // 质量特性ID映射表， [旧ID, 新ID]
            Dictionary<object, object> qualityMap = new Dictionary<object, object>();

            foreach(DataRow dr in dtOld.Rows)
            {
                int testLevel = (short)dr["测试种类"];
                object qualityID = dr["质量特性ID"];
                DataRow rownew;
                if(testLevel == 2 && !qualityMap.ContainsKey(qualityID))
                {   // 先增加质量特性节点
                    rownew = dtDest.NewRow();
                    rownew["序号"] = qualityIndex++;
                    rownew["测试类型名称"] = dr["特性名称"];
                    rownew["简写码"] = dr["c.简写码"];
                    rownew["所属被测对象ID"] = desobjid;
                    rownew["父测试类型ID"] = 0;
                    rownew["子节点类型"] = 1;   // 1代表子测试类型，2代表测试项，二者选其一
                    dtDest.Rows.Add(rownew);
                    _dbdes.UpdateDatabase(dtDest, sqladd);
                    object newid = _dbdes.ExecuteScalar(sqlGetNewID);
                    qualityMap[qualityID] = newid;
                    index = 1; // 子特性重新计数
                }

                rownew = dtDest.NewRow();
                rownew["序号"] = index++;
                rownew["测试类型名称"] = dr["测试的名称"];
                rownew["简写码"] = dr["b.简写码"];
                rownew["所属被测对象ID"] = testLevel == 1 ? desobjid : 0;
                rownew["父测试类型ID"] = testLevel == 1 ? 0 : qualityMap[qualityID];
                rownew["子节点类型"] = 2;

                dtDest.Rows.Add(rownew);
                _dbdes.UpdateDatabase(dtDest, sqladd);
                // 导入原数据库中的测试条目(测试项)
                int typeid = (int)_dbdes.ExecuteScalar(sqlGetNewID);

                // 导入测试类型下的所有测试条目(测试项)
                ConvertItemTable(typeid, (int)dr["测试ID"]);
            }

            StatusMsg = @"成功导入'测试类型表'表!";
        }

        private Dictionary<int, int> newoldcaseids = new Dictionary<int, int>(); // 保存导入后的测试用例新旧ID(Key: 旧ID, Value: 新ID)
        private void ConvertAllUsecases()
        {
            // 导入测试用例前先导入所有的问题报告单
            ConvertAllPbl();

            newoldcaseids.Clear();

            string sql = "SELECT * FROM 测试用例 ORDER BY 测试用例ID";
            DataTable dtdes = _dbdes.ExecuteDataTable(sql);

            int converted = 1;
            int total;
            DataTable table = _dbsrc.ExecuteDataTable(sql);
            total = table.Rows.Count;
            foreach(DataRow row in table.Rows)
            {
                DataRow rownew = dtdes.NewRow();

                rownew["测试用例名称"] = row["测试用例名称"];
                rownew["追踪关系"] = row["追踪关系"];
                rownew["用例描述"] = row["用例描述"];
                rownew["用例的初始化"] = row["用例的初始化"];
                rownew["测试过程终止条件"] = row["测试过程终止条件"];
                rownew["测试结果评估标准"] = row["测试结果评估标准"];
                rownew["前提和约束"] = row["前提和约束"];
                rownew["执行与否"] = row["执行与否"];
                //rownew["测试人员"]          = row["测试人员"];
                rownew["测试人员"] = ConvertRepresation(peoples, row["测试人员"].ToString());
                rownew["测试时间"] = row["测试时间"];
                rownew["测试结论"] = row["测试结论"];
                rownew["通过与否"] = row["通过与否"];
                rownew["备注"] = row["备注"];
                //rownew["所使用的设计方法"]  = row["所使用的设计方法"];
                //rownew["设计人员"]          = row["设计人员"];
                rownew["所使用的设计方法"] = ConvertRepresation(designmethods, row["所使用的设计方法"].ToString());
                rownew["设计人员"] = ConvertRepresation(peoples, row["设计人员"].ToString());
                rownew["设计时间"] = row["设计时间"];
                rownew["未执行原因"] = row["未执行原因"];

                dtdes.Rows.Add(rownew);
                _dbdes.UpdateDatabase(dtdes, sql);

                DataTable dt1 = _dbdes.ExecuteDataTable(sql);
                int caseid = (int)dt1.Rows[dt1.Rows.Count - 1]["测试用例ID"];

                newoldcaseids[(int)row["测试用例ID"]] = caseid;

                // 导入每个测试用例的过程
                string sqlStep = "SELECT * FROM 测试过程 where 测试用例ID=? ORDER BY 序号";

                DataTable dtStep2 = _dbdes.ExecuteDataTableSchema(sqlStep);
                DataTable dtStep1 = _dbsrc.ExecuteDataTable(sqlStep, row["测试用例ID"]);
                foreach(DataRow row1 in dtStep1.Rows)
                {
                    DataRow rownew1 = dtStep2.NewRow();

                    rownew1["测试用例ID"] = caseid;
                    rownew1["序号"] = row1["序号"];
                    rownew1["输入及操作"] = row1["输入及操作"];
                    rownew1["期望结果"] = row1["期望结果"];
                    rownew1["评估标准"] = row1["评估标准"];
                    rownew1["实测结果"] = row1["实测结果"];
                    if((!DBNull.Value.Equals(row1["问题报告单ID"])) &&
                        ((bool)row1["是否出现问题"]))
                    {
                        int pblid = (int)row1["问题报告单ID"];
                        if(newoldpbls.ContainsKey(pblid))
                        {
                            rownew1["问题报告单ID"] = newoldpbls[pblid];
                        }
                    }
                    dtStep2.Rows.Add(rownew1);
                }
                _dbdes.UpdateDatabase(dtStep2, sqlStep);


                StatusMsg = @"正在导入测试用例: " + converted + @" (共 " + total + " 个)";
                converted++;
            }

            StatusMsg = @"成功导入'测试用例'表!";
        }

        static string ConvertRepresation(Dictionary<string, int> dic, string text)
        {
            StringBuilder strbld = new StringBuilder(32);

            string[] s = text.Split(',', '，', '、');
            foreach(string str in s)
            {
                if(dic.ContainsKey(str))
                {
                    strbld.Append(dic[str]);
                    strbld.Append(',');
                }
            }

            return strbld.ToString();
        }

        private Dictionary<int, int> newoldpbls = new Dictionary<int, int>(); // 保存新旧问题报告单中的问题ID(Key: 旧ID, Value: 新ID)
        private Dictionary<string, int> pblsigns = new Dictionary<string, int>(); // 保存问题报告单同一被测对象标识的最大序号
        private void ConvertAllPbl()
        {
            newoldpbls.Clear();
            pblsigns.Clear();

            string sql = "SELECT * FROM 问题报告单";
            DataTable dtdes = _dbdes.ExecuteDataTable(sql);
            int index = 1;
            using(DataTable table = _dbsrc.ExecuteDataTable(sql))
            {
                foreach(DataRow row in table.Rows)
                {
                    DataRow rownew = dtdes.NewRow();

                    rownew["序号"] = index++;
                    rownew["发现日期"] = row["发现日期"];
                    rownew["报告日期"] = row["报告日期"];
                    //rownew["报告人"]            = row["报告人"];
                    rownew["报告人"] = ConvertRepresation(peoples, row["报告人"].ToString());
                    rownew["程序文档名称"] = row["程序文档名称"];
                    rownew["问题类别"] = row["问题类别"];
                    rownew["问题级别"] = row["问题级别"];
                    rownew["问题追踪"] = row["问题追踪"];
                    rownew["问题描述"] = IOleObjectAssist.GetByteFromString(row["问题描述"].ToString());
                    rownew["附注及修改建议"] = row["附注及修改建议"];
                    rownew["一级标识"] = "FALL";
                    rownew["二级标识"] = "";
                    rownew["三级标识"] = "";
                    rownew["四级标识"] = "";

                    string strobj = GetObjFromPbl(row["问题标识"].ToString());
                    rownew["所属被测对象"] = strobj;

                    if(pblsigns.ContainsKey(strobj))
                        pblsigns[strobj]++;
                    else
                    {
                        pblsigns[strobj] = 1;
                    }

                    rownew["同标识序号"] = pblsigns[strobj];
                    dtdes.Rows.Add(rownew);
                    _dbdes.UpdateDatabase(dtdes, sql);

                    DataTable dt1 = _dbdes.ExecuteDataTable(sql);
                    int pblid = (int)dt1.Rows[dt1.Rows.Count - 1]["问题ID"];

                    newoldpbls[(int)row["问题ID"]] = pblid;
                }
            }

            StatusMsg = @"成功导入'问题报告单'表!";
        }

        static string GetObjFromPbl(string pblsign)
        {
            string[] strsplits = pblsign.Split('_');
            return strsplits[strsplits.Length - 2];
        }

        private void ConvertAssisTable()
        {
            ConvertPriorityTable();
            ConvertTraceTable();

            ConvertDesignMethod();
            ConvertPeople();

            ConvertEnv();
            ConvertTypeTempl();

            ConvertCategory();
            ConvertSchedule();

            ConvertPblLevel();
            ConvertPblCategory();

            ConvertCite();
            ConvertQualify();
        }

        void ConvertQualify()
        {
            string sql = "SELECT * FROM 质量特性 ORDER BY 质量特性ID";
            DataTable dtdes = _dbdes.ExecuteDataTable(sql);

            using(DataTable table = _dbsrc.ExecuteDataTable(sql))
            {
                foreach(DataRow row in table.Rows)
                {
                    DataRow rownew = dtdes.NewRow();

                    rownew["特性名称"] = row["特性名称"];
                    rownew["简写码"] = row["简写码"];
                    rownew["简要说明"] = row["简要说明"];
                    rownew["重要性等级"] = row["重要性等级"];
                    rownew["采用技术"] = row["采用技术"];

                    dtdes.Rows.Add(rownew);
                }

                _dbdes.UpdateDatabase(dtdes, sql);
            }
        }

        private void ConvertCite()
        {
            const string sql = "SELECT * FROM 引用文件及术语表 ORDER BY ID";
            const string sql1 = "SELECT * FROM 引用文件表";
            const string sql2 = "SELECT * FROM 术语表";
            string[] docnamelist = new[] { "测试计划", "测试说明", "测试总结", "需求分析" };

            DataTable dtdes1 = _dbdes.ExecuteDataTableSchema(sql1);
            DataTable dtdes2 = _dbdes.ExecuteDataTableSchema(sql2);

            DataTable table = _dbsrc.ExecuteDataTable(sql);
            int index1 = 1;
            int index2 = 1;
            DocContentDB db = new DocContentDB(_dbdes);
            foreach(DataRow dr in table.Rows)
            {
                //System.Int32 typenum = (System.Int32)row["类型"];
                string typenum = dr["类型"].ToString();
                string docName = docnamelist[(short)dr["所属文档编号"] - 1];
                switch(typenum)
                {
                    case "1": // 引用文件
                        DataRow rownew = dtdes1.NewRow();

                        rownew["序号"] = index1++;
                        rownew["文档名称"] = docName;
                        rownew["引用文件文档号"] = dr["引用文件文档号"];
                        rownew["引用文件标题"] = dr["引用文件标题"];
                        rownew["编写单位及作者"] = dr["编写单位及作者"];
                        rownew["出版日期"] = dr["出版日期"];

                        dtdes1.Rows.Add(rownew);
                        break;

                    case "2": // 术语
                        DataRow rownew2 = dtdes2.NewRow();

                        rownew2["序号"] = index2++;
                        rownew2["文档名称"] = docName;
                        rownew2["术语和缩略语名"] = dr["术语和缩略语名"];
                        rownew2["确切定义"] = dr["确切定义"];

                        dtdes2.Rows.Add(rownew2);
                        break;
                    case "3": // 文档概述
                        db.SetContentString(null, docName, "文档概述", dr["文档概述"].ToString());
                        break;
                }
            }
            _dbdes.UpdateDatabase(dtdes1, sql1);
            _dbdes.UpdateDatabase(dtdes2, sql2);

            StatusMsg = @"导入'引用文件及术语表'!";
        }

        private void ConvertPblLevel()
        {
            string sql = "SELECT * FROM 问题级别表 ORDER BY 问题级别ID";
            DataTable dtdes = _dbdes.ExecuteDataTable(sql);

            using(DataTable table = _dbsrc.ExecuteDataTable(sql))
            {
                foreach(DataRow row in table.Rows)
                {
                    DataRow rownew = dtdes.NewRow();

                    rownew["序号"] = row["序号"];
                    rownew["问题级别名称"] = row["问题级别名称"];

                    dtdes.Rows.Add(rownew);
                }

                _dbdes.UpdateDatabase(dtdes, sql);
            }

            StatusMsg = @"导入'问题级别表'!";
        }

        private void ConvertPblCategory()
        {
            string sql = "SELECT * FROM 问题类别表 ORDER BY 问题类别ID";
            DataTable dtdes = _dbdes.ExecuteDataTable(sql);

            using(DataTable table = _dbsrc.ExecuteDataTable(sql))
            {
                foreach(DataRow row in table.Rows)
                {
                    DataRow rownew = dtdes.NewRow();

                    rownew["序号"] = row["序号"];
                    rownew["问题类别名称"] = row["问题类别名称"];

                    dtdes.Rows.Add(rownew);
                }

                _dbdes.UpdateDatabase(dtdes, sql);
            }

            StatusMsg = @"导入'问题类别表'!";
        }

        private void ConvertSchedule()
        {
            const string sql = "SELECT * FROM 计划进度表 order by ID";
            DataTable dtdes = _dbdes.ExecuteDataTableSchema(sql);

            DataTable table = _dbsrc.ExecuteDataTable(sql);
            int index = 1;
            foreach(DataRow row in table.Rows)
            {
                DataRow drNew = dtdes.NewRow();
                drNew["序号"] = index++;
                drNew["工作内容说明"] = row["工作内容说明"];
                drNew["预计开始时间"] = row["预计开始时间"];
                drNew["预计完成时间"] = row["预计完成时间"];
                drNew["主要完成人"] = ConvertRepresation(peoples, row["主要完成人"].ToString());
                drNew["备注"] = row["备注"];
                dtdes.Rows.Add(drNew);
                _dbdes.UpdateDatabase(dtdes, sql);
            }

            StatusMsg = @"导入'计划进度表'!";
        }

        private void ConvertCategory()
        {
            string sql = "SELECT * FROM 测试种类表";
            DataTable dtdes = _dbdes.ExecuteDataTable(sql);

            using(DataTable table = _dbsrc.ExecuteDataTable(sql))
            {
                foreach(DataRow row in table.Rows)
                {
                    DataRow rownew = dtdes.NewRow();

                    rownew["测试种类名称"] = row["测试种类名称"];

                    dtdes.Rows.Add(rownew);
                }

                _dbdes.UpdateDatabase(dtdes, sql);
            }

            StatusMsg = @"导入'测试种类表'!";
        }

        private void ConvertTypeTempl()
        {
            string sql = "SELECT * FROM 测试集表";
            string sqladd = "SELECT * FROM 测试类型模板表";
            DataTable dtdes = _dbdes.ExecuteDataTable(sqladd);

            using(DataTable table = _dbsrc.ExecuteDataTable(sql))
            {
                foreach(DataRow row in table.Rows)
                {
                    DataRow rownew = dtdes.NewRow();

                    rownew["测试能力名称"] = row["测试的名称"];
                    rownew["简写码"] = row["简写码"];
                    rownew["简要说明"] = row["简要说明"];
                    rownew["重要性等级"] = row["重要性等级"];
                    rownew["父节点ID"] = 0;
                    rownew["序号"] = 1;

                    dtdes.Rows.Add(rownew);
                    _dbdes.UpdateDatabase(dtdes, sqladd);
                }
            }

            StatusMsg = @"导入'测试类型模板表'!";
        }

        private void ConvertEnv()
        {
            const string sql = "SELECT * FROM 测试环境和系统概述表 order by ID";
            const string sqladd1 = "SELECT * FROM 测试资源配置表";

            DataTable dtdes1 = _dbdes.ExecuteDataTable(sqladd1);

            DocContentDB db = new DocContentDB(_dbdes);

            int index = 1;
            DataTable table = _dbsrc.ExecuteDataTable(sql);
            foreach(DataRow dr in table.Rows)
            {
                byte[] buf;
                switch((short)dr["类型"])
                {
                    case 2:
                        DataRow rownew = dtdes1.NewRow();
                        rownew["序号"] = index++;
                        rownew["名称"] = dr["名称"];
                        rownew["用途"] = dr["用途"];
                        rownew["数量"] = dr["数量"];
                        rownew["说明"] = dr["说明"];
                        rownew["维护人"] = dr["维护人"];
                        dtdes1.Rows.Add(rownew);
                        break;
                    case 1:  // 测试环境概述
                        buf = IOleObjectAssist.ConvertAccessOleObject(dr["测试环境概述"]);
                        db.SetContentBuffer(null, "项目信息", "测试环境概述", buf);
                        break;
                    case 3:  // 测试环境的安装测试与控制
                        buf = IOleObjectAssist.ConvertAccessOleObject(dr["安装测试与控制"]);
                        db.SetContentBuffer(null, "项目信息", "安装测试与控制", buf);
                        break;
                    case 4:  // 系统概述
                        buf = IOleObjectAssist.ConvertAccessOleObject(dr["系统概述"]);
                        db.SetContentBuffer(null, "项目信息", "被测软件概述", buf);
                        break;
                    case 5:  // 与其它测试计划的关系
                        db.SetContentString(null, "测试计划", "与其它文档的关系", dr["与其它测试计划的关系"].ToString());
                        break;
                    case 6:  // 数据记录、整理和分析
                        buf = IOleObjectAssist.ConvertAccessOleObject(dr["数据整理"]);
                        db.SetContentBuffer(null, "项目信息", "数据记录整理", buf);
                        break;
                    case 8:  // 其他要求
                        buf = IOleObjectAssist.ConvertAccessOleObject(dr["其他要求"]);
                        db.SetContentBuffer(null, "项目信息", "其它要求", buf);
                        break;
                    case 9:  // 终止条件
                        buf = IOleObjectAssist.GetByteFromString(dr["终止条件"].ToString());
                        db.SetContentBuffer(null, "项目信息", "项目终止条件", buf);
                        break;
                    case 10:  // 与其它文档的关系
                        db.SetContentString(null, "需求分析", "与其它文档的关系", dr["与其它测试计划的关系"].ToString());
                        break;
                }
            }
            _dbdes.UpdateDatabase(dtdes1, sqladd1);

            StatusMsg = @"导入'测试环境和系统概述表'!";
        }

        private Dictionary<string, int> prioritys = new Dictionary<string, int>();  // 优先级对应表
        private void ConvertPriorityTable()
        {
            prioritys.Clear();

            string sql = "SELECT * FROM 优先级表 ORDER BY 优先级ID";
            string sqladd = "SELECT * FROM 测试项优先级表";
            DataTable dtdes = _dbdes.ExecuteDataTable(sqladd);

            int index = 1;
            using(DataTable table = _dbsrc.ExecuteDataTable(sql))
            {
                foreach(DataRow row in table.Rows)
                {
                    DataRow rownew = dtdes.NewRow();

                    rownew["序号"] = index++;
                    rownew["优先级"] = row["优先级"];
                    rownew["说明"] = row["说明"];

                    dtdes.Rows.Add(rownew);
                    _dbdes.UpdateDatabase(dtdes, sqladd);

                    DataTable dt = _dbdes.ExecuteDataTable(sqladd);

                    prioritys[row["优先级"].ToString()] = (int)dt.Rows[dt.Rows.Count - 1]["优先级ID"];
                }
            }

            StatusMsg = @"成功导入'测试项优先级表'!";
        }

        private Dictionary<int, string> traces = new Dictionary<int, string>();     // 追踪关系(测试依据)对应表
        private void ConvertTraceTable()
        {
            traces.Clear();

            string sql = "SELECT * FROM 追踪关系表 ORDER BY 序号";
            string sqladd = "SELECT * FROM 测试依据表";
            DataTable dtdes = _dbdes.ExecuteDataTable(sqladd);

            int index = 1;
            using(DataTable table = _dbsrc.ExecuteDataTable(sql))
            {
                foreach(DataRow row in table.Rows)
                {
                    DataRow rownew = dtdes.NewRow();

                    rownew["ID"] = FunctionClass.NewGuid;
                    rownew["序号"] = index++;
                    rownew["测试依据"] = row["追踪关系"];
                    rownew["测试依据说明"] = row["追踪关系说明"];
                    rownew["父节点ID"] = "~root";
                    rownew["章节号"] = row["序号"];

                    // 对于数据库v2.1加入新字段"是否追踪", 其缺省值为True
                    rownew["是否追踪"] = true;

                    dtdes.Rows.Add(rownew);
                    _dbdes.UpdateDatabase(dtdes, sqladd);

                    traces[(int)row["ID"]] = rownew["ID"].ToString();
                }
            }

            StatusMsg = @"成功导入'追踪关系表'!";
        }

        private Dictionary<string, int> designmethods = new Dictionary<string, int>(); // 测试用例设计方法表
        private void ConvertDesignMethod()
        {
            designmethods.Clear();

            string sql = "SELECT * FROM 测试用例设计方法表 ORDER BY ID";
            string sqladd = "SELECT * FROM 测试用例设计方法表";
            DataTable dtdes = _dbdes.ExecuteDataTable(sqladd);

            int index = 1;
            using(DataTable table = _dbsrc.ExecuteDataTable(sql))
            {
                foreach(DataRow row in table.Rows)
                {
                    DataRow rownew = dtdes.NewRow();

                    rownew["序号"] = index++;
                    rownew["测试用例设计方法"] = row["测试用例设计方法"];

                    dtdes.Rows.Add(rownew);
                    _dbdes.UpdateDatabase(dtdes, sqladd);

                    DataTable dt = _dbdes.ExecuteDataTable(sql);
                    designmethods[row["测试用例设计方法"].ToString()] = (int)dt.Rows[dt.Rows.Count - 1]["ID"];
                }
            }

            StatusMsg = @"成功导入'测试用例设计方法表'!";
        }

        private Dictionary<string, int> peoples = new Dictionary<string, int>(); // 测试组织与人员
        private void ConvertPeople()
        {
            peoples.Clear();

            string sql = "SELECT * FROM 测试组织与人员表 order by 序号";
            DataTable dtdes = _dbdes.ExecuteDataTableSchema(sql);

            int index = 1;
            DataTable dt = _dbsrc.ExecuteDataTable("SELECT * FROM 测试组织与人员表 ORDER BY ID");
            foreach(DataRow dr in dt.Rows)
            {
                DataRow drnew = dtdes.NewRow();
                drnew["序号"] = index++;
                drnew["角色"] = dr["角色"];
                drnew["姓名"] = dr["姓名"];
                drnew["职称"] = dr["职称"];
                drnew["主要职责"] = dr["主要职责"];

                dtdes.Rows.Add(drnew);
            }
            _dbdes.UpdateDatabase(dtdes, sql);

            dt = _dbdes.ExecuteDataTable(sql);
            foreach(DataRow dr in dt.Rows)
                peoples[dr["姓名"].ToString()] = (int)dr["ID"];
        }


        private void ConvertItemTable(int destypeid, int srctypeid)
        {
            string sql = "SELECT * FROM 测试条目 where 所属测试ID=? order by 序号";
            string sqladd = "SELECT * FROM 测试项表";
            string sqlNewID = "SELECT top 1 测试项ID FROM 测试项表 order by 测试项ID DESC";

            DataTable dtDest = _dbdes.ExecuteDataTableSchema(sqladd);
            DataTable table = _dbsrc.ExecuteDataTable(sql, srctypeid);
            int index = 1, converted = 1, total = table.Rows.Count;
            foreach(DataRow row in table.Rows)
            {
                DataRow rownew = dtDest.NewRow();

                rownew["序号"] = index++;
                rownew["测试项名称"] = row["测试条目名称"];
                rownew["简写码"] = row["简写码"];
                rownew["测试项说明及测试要求"] = row["测试条目说明及测试要求"];
                rownew["测试方法说明"] = row["测试方法说明"];
                //rownew["追踪关系"]                  = row["追踪关系"];
                rownew["追踪关系"] = GetTraces((int)row["测试条目ID"]);
                rownew["终止条件"] = row["终止条件"];
                //rownew["优先级"]                    = row["优先级"];
                int prio = GetPriority(row["优先级"].ToString());
                if(prio != 0)
                    rownew["优先级"] = prio;

                rownew["下属测试用例说明"] = row["下属测试用例说明"];
                rownew["所属测试类型ID"] = destypeid;
                rownew["父节点ID"] = 0;

                dtDest.Rows.Add(rownew);
                _dbdes.UpdateDatabase(dtDest, sqladd);
                int itemid = (int)_dbdes.ExecuteScalar(sqlNewID);

                // 之前测试用例已经导入完毕, 现构建"测试用例与测试项关系表"
                string sql1 = "SELECT * FROM 测试用例与测试项关系表";
                string sql2 = "SELECT * FROM 测试条目与测试用例关系 where 测试条目ID=? ORDER BY 序号";

                DataTable dtdes1 = _dbdes.ExecuteDataTableSchema(sql1);
                int index1 = 1;
                DataTable table1 = _dbsrc.ExecuteDataTable(sql2, (int)row["测试条目ID"]);
                foreach(DataRow row1 in table1.Rows)
                {
                    int srccaseid = (int)row1["测试用例ID"];
                    if(newoldcaseids.ContainsKey(srccaseid))
                    {
                        int descaseid = newoldcaseids[srccaseid];
                        DataRow rownew1 = dtdes1.NewRow();

                        rownew1["测试用例ID"] = descaseid;
                        rownew1["测试项ID"] = itemid;
                        rownew1["序号"] = index1++;
                        rownew1["直接所属标志"] = row1["直接所属标志"];

                        dtdes1.Rows.Add(rownew1);
                    }
                    _dbdes.UpdateDatabase(dtdes1, sql1);
                }

                StatusMsg = @"正在导入测试项: " + converted + @"  (共 " + total + " )个";
                converted++;
            }

            StatusMsg = @"成功导入'测试项表'表!";
        }

        private string GetTraces(int itemid)
        {
            string sql = "SELECT * FROM 测试条目与追踪关系对应表 where 测试条目ID=? ORDER BY 追踪关系ID";
            StringBuilder strbld = new StringBuilder(128);
            using(DataTable table = _dbsrc.ExecuteDataTable(sql, itemid))
            {
                foreach(DataRow row in table.Rows)
                {
                    int traceid = (int)row["追踪关系ID"];
                    if(traces.ContainsKey(traceid))
                    {
                        strbld.Append(traces[traceid]);
                        strbld.Append(',');
                    }
                }
            }

            return strbld.ToString();
        }

        private int GetPriority(string txtprio)
        {
            return prioritys.ContainsKey(txtprio) ? prioritys[txtprio] : 0;
        }



        public bool CanConvert()
        {
            return _bconvertable;
        }

        public string GetStatusMsg()
        {
            return _statusmsg;
        }

        public string GetDesFileName()
        {
            return _desdatabase;
        }

        public void Dispose()
        {
            if(_dbsrc != null)
                ((IDisposable)_dbsrc).Dispose();
            if(_dbdes != null)
                ((IDisposable)_dbdes).Dispose();
        }
    }
}
