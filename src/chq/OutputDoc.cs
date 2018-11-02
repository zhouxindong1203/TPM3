﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using Aspose.Words;
using Aspose.Words.Reporting;
using Aspose.Words.Tables;
using Common;
using TPM3.Sys; 
using TPM3.wx;
using TPM3.zxd;
using TPM3.zxd.Helper;
using TPM3.zxd.pbl;
using Z1.tpm.DB;
using Common.Aspose;

namespace TPM3.chq
{
    class OutputDoc
    {
        public string DocumentName;
        
        public string DocSonName = "";

        public Document CurrentDoc;
        public string ProjectID;
        public string TestVerID;

        public ArrayList DataTreeList;

        Scattered Scattered = new Scattered();

        public int tableNum = 0;

        public bool IfTiSheng = MyProjectInfo.GetBoolValue(TPM3.Sys.GlobalData.globalData.dbProject, TPM3.Sys.GlobalData.globalData.projectID, "不提升标题");

        void DocContent_MergeField(object sender, MergeFieldEventArgs e)
        {
            string fieldName = e.FieldName;
            string[] fields = fieldName.Split(':');
            string docName = "", contentTitle = "";
            string Result = "";
            string Result1 = "无。";
            DataTable dt;

            if (fields[0] == "文本")
            {
                docName = DocumentName;
                contentTitle = fields[1].Replace(" ", ""); ;
            }
            else if (fields[0] == "项目")
            {
                docName = "项目信息";
                contentTitle = fields[1].Replace(" ", "");
            }

            string sqlstate = "";

            if ((contentTitle == "项目名称") || (contentTitle == "密级") || (contentTitle == "测试单位") || (contentTitle == "项目名称") || (contentTitle == "委托方名称") || (contentTitle == "委托方地址") || (contentTitle == "实验室地址") || (contentTitle == "外场测试地址") || (contentTitle == "交办方名称") || (contentTitle == "交办方地址") || (contentTitle == "代码行数") || (contentTitle == "有效行数") || (contentTitle == "开发语言") || (contentTitle == "运行平台"))
            {
                sqlstate = "SELECT 内容类型 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=?";
                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, contentTitle);
            }
            else if ((contentTitle == "被测软件概述") || (contentTitle == "测试任务概述") || (contentTitle == "项目终止条件") || (contentTitle == "安装测试与控制") || (contentTitle == "数据记录整理") || (contentTitle == "被测软件评价准则和方法") || (contentTitle == "测试情况说明") || (contentTitle == "被测软件版本") || (contentTitle == "文档有效性声明"))
            {
                sqlstate = "SELECT 内容类型 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=? AND 测试版本=?";
                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, contentTitle, TestVerID);
            }
            else if ((contentTitle == "文档标识号") || (contentTitle == "文档名称"))
            {
                sqlstate = "SELECT 内容类型 FROM SYS文档内容表 WHERE 项目ID=? AND 文档名称=? AND 内容标题=?";
                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, docName, contentTitle);
            }
            else if (contentTitle == "文档页眉")
            {
                sqlstate = "SELECT 内容类型 FROM SYS文档内容表 WHERE 项目ID=? AND 文档名称=? AND 内容标题=? AND 测试版本=?";
                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, docName, contentTitle, TestVerID);
            }
            else if ((contentTitle == "委托方名称") || (contentTitle == "委托方地址") || (contentTitle == "交办方联系人") || (contentTitle == "交办方联系电话") || (contentTitle == "开发单位") || (contentTitle == "开发方地址") || (contentTitle == "开发方联系人") || (contentTitle == "开发方联系电话") || (contentTitle == "代码行数") || (contentTitle == "有效行数") || (contentTitle == "开发语言") || (contentTitle == "运行平台"))
            {
                sqlstate = "SELECT 内容类型 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=?";
                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, contentTitle);

            }
            else if ((contentTitle == "测试总体要求") || (contentTitle == "测评场所") || (contentTitle == "测评数据") || (contentTitle == "定型测评通过准则"))
            {
                sqlstate = "SELECT 内容类型 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=?";
                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, contentTitle);

            }
            else if ((contentTitle == "配置管理活动") || (contentTitle == "产品审核") || (contentTitle == "过程审核") || (contentTitle == "质量评审") || (contentTitle == "不符合项跟踪和验证") || (contentTitle == "测评分包") || (contentTitle == "安全保密与知识产权保护"))
            {
                sqlstate = "SELECT 内容类型 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=?";
                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, contentTitle);

            }
            else if ((contentTitle == "测评过程概述") || (contentTitle == "测试需求分析和测试筹划") || (contentTitle == "测试设计和实现") || (contentTitle == "测试方法说明") || (contentTitle == "测试有效性说明"))
            {
                sqlstate = "SELECT 内容类型 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=?";
                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, contentTitle);

            }
           
            else
            {
                sqlstate = "SELECT 内容类型 FROM SYS文档内容表 WHERE 项目ID=? AND 文档名称=? AND 内容标题=? AND 测试版本=?";
                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, docName, contentTitle, TestVerID);
            }

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Result = dr["内容类型"].ToString();
                }
            }
            else
            {
                if (contentTitle == "文档页眉")
                {
                    Result = "文本";
                }

            }

            if (Result == "文本")
            {
                if ((contentTitle == "项目名称") || (contentTitle == "密级") || (contentTitle == "测试单位") || (contentTitle == "项目名称") || (contentTitle == "委托方名称") || (contentTitle == "委托方地址") || (contentTitle == "实验室地址") || (contentTitle == "外场测试地址") || (contentTitle == "交办方名称") || (contentTitle == "交办方地址") || (contentTitle == "代码行数") || (contentTitle == "有效行数") || (contentTitle == "开发语言") || (contentTitle == "运行平台"))
                {
                    sqlstate = "select 文本内容 from SYS文档内容表 WHERE 内容标题=? and 项目ID=?";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, contentTitle, ProjectID);
                }
                else if ((contentTitle == "被测软件概述") || (contentTitle == "测试任务概述") || (contentTitle == "项目终止条件") || (contentTitle == "安装测试与控制") || (contentTitle == "数据记录整理") || (contentTitle == "被测软件评价准则和方法") || (contentTitle == "测试情况说明") || (contentTitle == "被测软件版本") || (contentTitle == "文档有效性声明"))
                {
                    sqlstate = "select 文本内容 from SYS文档内容表 WHERE 内容标题=? and 项目ID=? and 测试版本=?";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, contentTitle, ProjectID, TestVerID);
                }
                else if ((contentTitle == "文档标识号") || (contentTitle == "文档名称"))
                {
                    sqlstate = "select 文本内容 from SYS文档内容表 WHERE 文档名称=? AND 内容标题=? and 项目ID=?";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, docName, contentTitle, ProjectID);
                }
                else if (contentTitle == "文档页眉")
                {
                    sqlstate = "select 文本内容 from SYS文档内容表 WHERE 文档名称=? AND 内容标题=? and 项目ID=? AND 测试版本=?";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, docName, contentTitle, ProjectID, TestVerID);
                }
                else if ((contentTitle == "委托方名称") || (contentTitle == "委托方地址") || (contentTitle == "交办方联系人") || (contentTitle == "交办方联系电话") || (contentTitle == "开发单位") || (contentTitle == "开发方地址") || (contentTitle == "开发方联系人") || (contentTitle == "开发方联系电话") || (contentTitle == "代码行数") || (contentTitle == "有效行数") || (contentTitle == "开发语言") || (contentTitle == "运行平台"))
                {
                    sqlstate = "SELECT 文本内容 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=?";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, contentTitle);

                }
                else if ((contentTitle == "测试总体要求") || (contentTitle == "测评场所") || (contentTitle == "测评数据") || (contentTitle == "定型测评通过准则"))
                {
                    sqlstate = "SELECT 文本内容 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=?";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, contentTitle);

                }
                else if ((contentTitle == "配置管理活动") || (contentTitle == "产品审核") || (contentTitle == "过程审核") || (contentTitle == "质量评审") || (contentTitle == "不符合项跟踪和验证") || (contentTitle == "测评分包") || (contentTitle == "安全保密与知识产权保护"))
                {
                    sqlstate = "SELECT 文本内容 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=?";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, contentTitle);

                }
                else if ((contentTitle == "测评过程概述") || (contentTitle == "测试需求分析和测试筹划") || (contentTitle == "测试设计和实现") || (contentTitle == "测试方法说明") || (contentTitle == "测试有效性说明"))
                {
                    sqlstate = "SELECT 文本内容 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=?";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, contentTitle);

                }
                else
                {
                    sqlstate = "select 文本内容 from SYS文档内容表 WHERE 文档名称=? AND 内容标题=? and 项目ID=? and 测试版本=?";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, docName, contentTitle, ProjectID, TestVerID);
                }

                if (dt != null && dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Result1 = dr["文本内容"].ToString();
                        if (contentTitle == "文档页眉")
                        {
                            if (Result1 == "")
                            {
                                Result1 = GetQushengYM(DocumentName);

                            }

                        }
                        if (Result1 == "")
                        {
                            Result1 = "无。";

                        }
                        if (contentTitle == "文档标识号")
                        {
                            Result1 = ChangeDocBS(Result1, docName);
                        }
                        if (contentTitle == "测试任务概述")
                        {
                            if ((DocumentName == "测试总结") || (DocumentName == "问题报告") || (DocumentName == "测试记录") || (DocumentName == "回归测试报告"))
                            {
                                if (GetVerInfo() != "")
                                {
                                    Result1 = Result1 + GetVerInfo();
                                }
                            }
                        }
                        

                    }

                }
                else
                {
                    if (contentTitle == "文档页眉")
                    {
                        Result1 = GetQushengYM(DocumentName);
                    }

                }

               e.Text = Result1;

            }
            else if (Result == "对象")
            {
                object Value = null;
                DataTable dt1;

                if ((contentTitle == "项目名称") || (contentTitle == "密级") || (contentTitle == "测试单位") || (contentTitle == "项目名称"))
                {
                    sqlstate = "select 文档内容 from SYS文档内容表 WHERE 内容标题=? and 项目ID=?";
                    dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, contentTitle, ProjectID);

                }
                else if ((contentTitle == "被测软件概述") || (contentTitle == "测试任务概述") || (contentTitle == "项目终止条件") || (contentTitle == "安装测试与控制") || (contentTitle == "数据记录整理") || (contentTitle == "测试情况说明"))
                {
                    sqlstate = "select 文档内容 from SYS文档内容表 WHERE 内容标题=? and 项目ID=? and 测试版本=?";
                    dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, contentTitle, ProjectID, TestVerID);

                }
                else if ((contentTitle == "文档标识号") || (contentTitle == "文档名称"))
                {
                    sqlstate = "select 文档内容 from SYS文档内容表 WHERE 文档名称=? AND 内容标题=? and 项目ID=?";
                    dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, docName, contentTitle, ProjectID);

                }
                else if ((contentTitle == "测评过程概述") || (contentTitle == "测试需求分析和测试筹划") || (contentTitle == "测试设计和实现") || (contentTitle == "测试方法说明") || (contentTitle == "测试有效性说明"))
                {
                    sqlstate = "select 文档内容 from SYS文档内容表 WHERE 内容标题=? and 项目ID=?";
                    dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, contentTitle, ProjectID);

                }
                else
                {
                    sqlstate = "select 文档内容 from SYS文档内容表 WHERE 文档名称=? AND 内容标题=? and 项目ID=? and 测试版本=?";
                    dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, docName, contentTitle, ProjectID, TestVerID);

                }

                Value = OutputComm.OutputComm.OLEObjectProcess(dt1, CurrentDoc, e.FieldName);
                if (Value != null)
                {
                    if ((Value.ToString() == "无。") && ((contentTitle == "测试策略") || (contentTitle == "测试策略2") || (contentTitle == "测试策略3") || (contentTitle == "测试策略4")))
                    {
                        e.Text = "";
                    }
                    else
                    {
                        e.Text = Value.ToString();

                    }
                }

            }
            else
            {
                e.Text = "";
            }

            if (contentTitle == "测试用例附件")
            {
                Annexs Annexs = new Annexs();
                Annexs.PutoutAnnex(CurrentDoc, 2);

            }

            if (contentTitle == "测试任务概述")
            {
                if ((DocumentName == "测试总结") || (DocumentName == "问题报告") || (DocumentName == "测试记录") || (DocumentName == "回归测试报告"))
                {
                    if (GetVerInfo() != "")
                    {
                        e.Text = e.Text + GetVerInfo();
                    }
                }
            }
        }
        public string GetQushengYM(string DocumentName)
        {

            string ProjectName = "";
            string YMname = "";
            string sqlstate = "select 文本内容 from SYS文档内容表 WHERE 内容标题=? and 项目ID=?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, "项目名称", ProjectID);
          
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProjectName = dr["文本内容"].ToString();
                }
            }

            switch (DocumentName)
            {
                case "需求分析":
                    YMname = ProjectName + "•" + "软件测试需求规格说明";
                    break;
                case "测试计划":
                    YMname = ProjectName + "•" + "软件测试计划";
                    break;
                case "测试说明":
                    YMname = ProjectName + "•" + "软件测试说明";
                    break;
                case "测试记录":
                    YMname = ProjectName + "•" + "软件测试记录";
                    break;
                case "问题报告":
                    YMname = ProjectName + "•" + "软件问题报告";
                    break;
                case "测试总结":
                    YMname = ProjectName + "•" + "软件测试报告";
                    break;
                case "回归测试方案":
                    YMname = ProjectName + "•" + "软件回归测试方案";
                    break;
                case "回归测试记录":
                    YMname = ProjectName + "•" + "软件回归测试记录";
                    break;
                case "回归测试报告":
                    YMname = ProjectName + "•" + "软件测试报告";
                    break;
                case "回归问题报告":
                    YMname = ProjectName + "•" + "软件回归问题报告";
                    break;

            }

            return YMname;
           
        }

        public string GetVerInfo()
        {

            string VerInfo = "";

            string sqlstate_1 = "select 文本内容 from SYS文档内容表 WHERE 内容标题=? and 项目ID=? and 测试版本=?";
            DataTable dt_1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate_1, "被测软件版本", ProjectID, TestVerID);

            if (dt_1 != null && dt_1.Rows.Count != 0)
            {
                DataRow dr = dt_1.Rows[0];
                VerInfo = "本次测试的软件版本为" + dr[0].ToString() + "。";
            }

            return VerInfo;

        }

        public string ChangeDocBS(string DocBSstr, string docName)
        {

            string ProjectBS = "";
            string DocVersion = "";
            DataTable dt;
            string sqlstate = "";
            string PreStr = "";

            sqlstate = "SELECT 文本内容, 内容标题 FROM SYS文档内容表 WHERE 项目ID=? AND 文档名称=? AND 内容标题=?";

            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, "项目信息", "项目标识号");
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ProjectBS = dr["文本内容"].ToString();
                }
            }

            sqlstate = "SELECT 文本内容, 内容标题, 文档名称 FROM SYS文档内容表 WHERE 内容标题 =? AND 文档名称 =? and 项目ID=? AND 测试版本=?";

            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, "文档版本", docName, ProjectID, TestVerID);
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DocVersion = dr["文本内容"].ToString();
                }
            }


            sqlstate = "select 前向版本ID from SYS测试版本表 where ID=? and 项目ID=?";
            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestVerID, ProjectID);
            if (dt != null && dt.Rows.Count != 0)
            {
                if (dt.Rows[0][0].ToString() != "")//回归测试
                {
                    string temp = "select 文本内容 from SYS文档内容表 where 项目ID=? and 内容标题=?";
                    DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(temp, ProjectID, "标识版本前缀");

                    if (dt1 != null && dt1.Rows.Count != 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            PreStr = dr1["文本内容"].ToString();
                        }
                    }
                    string TestInfo = GetCurrentVerStr(TestVerID);
                    int pos1 = TestInfo.IndexOf("第");
                    int pos2 = TestInfo.IndexOf("次");
                    string cishu = TestInfo.Substring(pos1 + 1, pos2 - pos1 - 1);

                   // DocBSstr = DocBSstr.Replace("{0}", ProjectBS + "/" + PreStr + cishu + "/").ToUpper();
                    DocBSstr = DocBSstr.Replace("{0}", ProjectBS + "/" + PreStr + cishu).ToUpper();
                    DocBSstr = DocBSstr.Replace("{1}", DocVersion).ToUpper();

                }
                else//首次测试
                {
                    DocBSstr = DocBSstr.Replace("{0}", ProjectBS).ToUpper();
                    DocBSstr = DocBSstr.Replace("{1}", DocVersion).ToUpper();
                }

            }
            else//首次测试
            {
                DocBSstr = DocBSstr.Replace("{0}", ProjectBS).ToUpper();
                DocBSstr = DocBSstr.Replace("{1}", DocVersion).ToUpper();

            }

            return DocBSstr;

        }

        public void FillUnOrdFieldsInSysConentTable(string[] FiledList, string sqlstate)
        {

            DocumentBuilder builder = new DocumentBuilder(CurrentDoc);
            string sqlCommand = "";

            foreach (string Field in FiledList)
            {
                if (builder.MoveToMergeField(Field))
                {
                    if (sqlstate == "")
                    {
                        sqlCommand = "SELECT 文本内容 FROM SYS文档内容表 WHERE 文档名称='项目信息' AND 内容标题 =" + "'" + Field + "'" + " AND 项目ID =" + "'" + ProjectID + "'";

                    }
                    else
                    {
                        sqlCommand = sqlstate;
                    }
                    DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand);
                    if ((dt != null) && (dt.Rows.Count > 0))
                    {
                        builder.Write(dt.Rows[0][0].ToString());
                    }
                    else
                    {
                        builder.Write("");
                    }
                }
            }

        }
        public void FillUnOrdFields(string tableName)
        {
            string[] FiledList;

            switch (tableName)
            {
                case "交办方信息":

                    FiledList = new string[6] { "交办方名称", "交办方地址", "交办方联系人", "交办方联系电话", "开发方联系电话", "开发单位" };

                    FillUnOrdFieldsInSysConentTable(FiledList, "");

                    break;

                case "测试项目基本信息":

                    FiledList = new string[4] { "项目名称", "项目标识号", "测试单位", "密级" };

                    FillUnOrdFieldsInSysConentTable(FiledList, "");

                    break;

            }
        }

        public void FillTableFields(string tableName)
        {
            string sqlCommand;
            DataTable dt = new DataTable();
            Table table;

            Scattered.TestVerID = TestVerID;

            switch (tableName)
            {
                case "DC引用文件表":
                    sqlCommand = "Select 引用文件标题,引用文件文档号,编写单位及作者,出版日期 From DC引用文件表 Where 文档名称 =? and 项目ID =? and 测试版本=? Order by 序号";
                    //if (DocumentName == "回归测试报告")
                    //{
                    //    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, "测试总结", ProjectID, TestVerID);
                    //}
                    //else
                    {
                        dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, DocumentName, ProjectID, TestVerID);
                    }
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);
                    break;

                case "DC术语表":
                    sqlCommand = "Select 术语和缩略语名,确切定义 From DC术语表 Where 文档名称 =? and 项目ID =? and 测试版本=? Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, DocumentName, ProjectID, TestVerID);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);
                    break;

                case "DC测试组织与人员表":
                    sqlCommand = "Select 角色,姓名,职称,主要职责 From DC测试组织与人员表 Where 项目ID =? and 测试版本=? Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, TestVerID);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);
                    break;

                case "DC计划进度表":
                    sqlCommand = "Select 工作内容说明,预计开始时间,预计完成时间,主要完成人,备注 From DC计划进度表 Where 项目ID =? and 测试版本=? Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, TestVerID);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);
                    break;

                case "DC测试资源配置表":
                    sqlCommand = "Select 名称,配置,数量,说明,维护人 From DC测试资源配置表 Where 项目ID = ? and 测试版本=? Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, TestVerID);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);
                    break;

                case "被测对象基本信息":
                    sqlCommand = "SELECT CA被测对象实体表.被测对象名称, CA被测对象实体表.被测对象版本, CA被测对象实体表.测试级别, CA被测对象实测表.测试版本, CA被测对象实测表.项目ID " +
                                 " FROM CA被测对象实体表 INNER JOIN CA被测对象实测表 ON CA被测对象实体表.ID = CA被测对象实测表.被测对象ID WHERE CA被测对象实测表.测试版本=? AND CA被测对象实测表.项目ID=? ORDER BY CA被测对象实测表.序号;";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, TestVerID, ProjectID);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);
                    break;

                case "文档修改记录":
                    sqlCommand = "SELECT DC文档修改页.版本号, DC文档修改页.日期, DC文档修改页.所修改章节, DC文档修改页.所修改页, DC文档修改页.备注, DC文档修改页.项目ID, DC文档修改页.测试版本, DC文档修改页.文档名称" +
                                " FROM DC文档修改页 WHERE DC文档修改页.项目ID=? AND DC文档修改页.测试版本=? AND DC文档修改页.文档名称=? ORDER BY DC文档修改页.序号;";

                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, TestVerID, DocumentName);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);

                    break;

                case "SYS测试依据表":

                    dt = Scattered.GetTestAccordingTable("测试依据", DataTreeList, "","");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);

                    break;

                case "回归测试依据":

                    dt = Scattered.HGTestAccording("回归测试依据", DataTreeList, "");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);

                    break;

                case "回归测试依据与测试项的追踪关系":

                    dt = Scattered.HGTestAccording("回归测试依据与测试项的追踪关系", DataTreeList, "");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);

                    table = OutputComm.OutputComm.GetNodeInTable("回归测试依据与测试项的追踪关系序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "测试依据与测试项的追踪关系定型":

                    dt = Scattered.GetTestAccordingTable("测试依据与测试项的追踪关系", DataTreeList, DocumentName, "定型");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    table = OutputComm.OutputComm.GetNodeInTable("依据与测试项关系序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "测试依据与测试项的追踪关系":

                    dt = Scattered.GetTestAccordingTable("测试依据与测试项的追踪关系", DataTreeList, DocumentName, "");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    table = OutputComm.OutputComm.GetNodeInTable("依据与测试项关系序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "测试依据与测试项的追踪关系1":

                    dt = Scattered.GetTestAccordingTable_JH("测试依据与测试项的追踪关系1", DataTreeList, DocumentName);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    table = OutputComm.OutputComm.GetNodeInTable("依据与测试项关系1序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "测试项与测试依据的追踪关系定型":

                    dt = Scattered.Output_TreeTypeDataTable("测试项与测试依据的追踪关系", DataTreeList, DocumentName, "定型");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    table = OutputComm.OutputComm.GetNodeInTable("测试项与依据关系序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "测试项与测试依据的追踪关系":

                    dt = Scattered.Output_TreeTypeDataTable("测试项与测试依据的追踪关系", DataTreeList, DocumentName, "");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    table = OutputComm.OutputComm.GetNodeInTable("测试项与依据关系序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "测试项与测试依据的追踪关系1":

                    dt = Scattered.Output_TreeTypeDataTable_JH("测试项与测试依据的追踪关系1", DataTreeList, DocumentName);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    table = OutputComm.OutputComm.GetNodeInTable("测试项与依据关系1序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "回归测试项与测试依据的追踪关系":

                    dt = Scattered.Output_TreeTypeDataTable("回归测试项与测试依据的追踪关系", DataTreeList, DocumentName, "");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    table = OutputComm.OutputComm.GetNodeInTable("回归测试项与测试依据的追踪关系序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;


                case "附件统计":
                    DataView dv = null;
                    if ((DocumentName == "测试说明") || (DocSonName == "软件测试记录表单"))
                    {
                        dv = TPM3.wx.AttachCaseTrace.GetAttachCaseTraceView(2);
                    }
                    else if ((DocumentName == "回归测试说明") || (DocSonName == "软件回归测试记录表单"))
                    {
                        dv = TPM3.wx.AttachCaseTrace.GetAttachCaseTraceView(2);
                    }
                    else if ((DocSonName == "软件测试记录实测结果") || (DocSonName == "软件回归测试记录实测结果"))
                    {
                        dv = TPM3.wx.AttachCaseTrace.GetAttachCaseTraceView(3);
                    }

                    //FillView(CurrentDoc, dv, true, tableName);
                    dt = dv.Table;
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);

                    table = OutputComm.OutputComm.GetNodeInTable("附件统计序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "参加测试人员":

                    break;

                case "执行结果统计":

                    break;

                case "问题类别":
                    sqlCommand = "SELECT 名称 as 问题类别名称, 说明 as 问题类别说明 from DC问题级别表 WHERE 类型 =? AND 项目ID=? ORDER BY 序号;";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, "类别", ProjectID);
                    dt.TableName = "问题类别";
                    FillTable(CurrentDoc, dt, true, tableName);
                    break;

                case "问题级别":
                    sqlCommand = "SELECT 名称 as 问题级别名称, 说明 as 问题级别说明 from DC问题级别表 WHERE 类型 =? AND 项目ID=? ORDER BY 序号;";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, "级别", ProjectID);
                    dt.TableName = "问题级别";
                    FillTable(CurrentDoc, dt, true, tableName);
                    break;

                case "ZL组织与人员表_项目":
                    sqlCommand = "Select 人员姓名, 职称, 人员职责 From ZL组织人员表 Where 项目ID = ? and (组织名称=? or 组织名称=?) Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, "项目负责人","软件测试组");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    table = OutputComm.OutputComm.GetNodeInTable("组织名称序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);
                    break;

                case "项目组织与人员表":
                    sqlCommand = "Select  组织名称, 人员姓名, 职称, 人员职责 From ZL组织人员表 Where 项目ID = ? and (组织名称=? or 组织名称=?) Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, "项目负责人", "软件测试组");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    //table = OutputComm.OutputComm.GetNodeInTable("项目组织与人员表序号", CurrentDoc, null);
                    //MergeTable(table, tableName, dt, CurrentDoc);
                    break;

                case "ZL组织与人员表_质保":
                    sqlCommand = "Select 人员姓名, 职称, 人员职责 From ZL组织人员表 Where 项目ID = ? and 组织名称=? Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, "质量保证员");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    break;

                case "ZL组织与人员表_配置":
                    sqlCommand = "Select 人员姓名, 职称, 人员职责 From ZL组织人员表 Where 项目ID = ? and 组织名称=? Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, "配置管理员");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    break;

                case "配置组织与人员表":
                    sqlCommand = "Select 组织名称, 人员姓名, 职称, 人员职责 From ZL组织人员表 Where 项目ID = ? and (组织名称=? or 组织名称=?)  Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, "配置控制委员会", "配置管理员");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);

                    table = OutputComm.OutputComm.GetNodeInTable("配置组织与人员表序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);
                    break;

                case "质保组织与人员表":
                    sqlCommand = "Select 人员姓名, 职称, 人员职责 From ZL组织人员表 Where 项目ID = ? and 组织名称=? Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, "项目负责人");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    break;

                case "质保组织与人员表2":
                    sqlCommand = "Select 人员姓名, 职称, 人员职责 From ZL组织人员表 Where 项目ID = ? and 组织名称=? Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, "质量保证员");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName); 
                    break;

                case "ZL资源表":
                    sqlCommand = "Select 资源名称, 资源标识, 数量, 用途 From ZL资源信息表 Where 项目ID = ? Order by 类别, 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);
                    break;

                case "配置资源表":
                    sqlCommand = "Select 资源名称, 资源标识, 数量, 用途 From ZL资源信息表 Where 项目ID = ? and 类别= ? Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, "配置");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);
                    break;

                case "质保资源表":
                    sqlCommand = "Select 资源名称, 资源标识, 数量, 用途 From ZL资源信息表 Where 项目ID = ? and 类别= ? Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, "质保");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);
                    break; 
                    
                case "基线表":
                    sqlCommand = "Select 基线名称, 基线标识, 预期到达时间 From ZL基线表 Where 项目ID = ? Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    break;

                case "CMI管理项表":
                    dt = Scattered.GetCMIItem();
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    table = OutputComm.OutputComm.GetNodeInTable("CMI管理项表序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);
                   
                    break;

                case "项目进度计划表":
                    dt = Scattered.GetJiHuaAP("1");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                   
                    break;

                case "配置管理计划表":
                    dt = Scattered.GetJiHuaAP("3");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);   
                    break;
                    
                case "质量保证计划表":
                    dt = Scattered.GetJiHuaAP("2");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);                        
                    break;

                case "风险分析表":
                    sqlCommand = "Select 风险事件, 风险原因, 风险优先级, 缓解措施 From ZL风险分析与评估表 Where 项目ID = ? Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);
                    break;

                case "项目跟踪监督表":
                    sqlCommand = "Select 工作内容, 预计开始时间, 完成人 From ZL进度计划表 Where 项目ID = ? and 计划类型 = ? Order by 序号";
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlCommand, ProjectID, "4");
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, true, tableName);
                    break;

                case "问题涉及依据":
                    dt = Scattered.TestAccordingStat_Question("问题涉及依据", DocumentName);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    table = OutputComm.OutputComm.GetNodeInTable("问题涉及依据表序号", CurrentDoc, null);

                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "其他更动涉及依据":
                    dt = Scattered.TestAccordingStat_ElseChange("其他更动涉及依据", DocumentName);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    table = OutputComm.OutputComm.GetNodeInTable("其他更动涉及依据表序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "原有用例统计":
                    dt = Scattered.TestCaseStat_OldHaveOrAddNew(1, DataTreeList);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    table = OutputComm.OutputComm.GetNodeInTable("原有用例统计序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "新增用例统计":
                    dt = Scattered.TestCaseStat_OldHaveOrAddNew(2, DataTreeList);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    table = OutputComm.OutputComm.GetNodeInTable("新增用例统计序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "未选取用例统计":

                    dt = Scattered.TestCaseStat_Should_ButNoExectue();
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);

                    table = OutputComm.OutputComm.GetNodeInTable("未选取用例统计序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "回归测试项与测试用例的追踪关系":
                    dt = Scattered.HGTestItemAndCaseAccording("回归测试项与测试用例的追踪关系", DataTreeList);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);

                    table = OutputComm.OutputComm.GetNodeInTable("回归测试项与测试用例的追踪关系序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "回归测试用例与测试项的追踪关系":
                    dt = Scattered.HGTestItemAndCaseAccording("回归测试用例与测试项的追踪关系", DataTreeList);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);

                    table = OutputComm.OutputComm.GetNodeInTable("回归测试用例与测试项的追踪关系序号", CurrentDoc, null);
                    MergeTable(table, tableName, dt, CurrentDoc);

                    break;

                case "测试计划中测试项到测试需求的追踪关系":

                    dt = Scattered.TPtoTA("测试计划中测试项到测试需求的追踪关系", DataTreeList);
                    dt.TableName = tableName;
                    FillTable(CurrentDoc, dt, false, tableName);
                    break;

                case "纠错性更动":

                     dt = Scattered.GD_ChangeError();
                     dt.TableName = tableName;
                     FillTable(CurrentDoc, dt, false, tableName);
                     
                     break;

                case "适应性改造更动":
                     dt = Scattered.GD_Change("2");
                     dt.TableName = tableName;
                     FillTable(CurrentDoc, dt, false, tableName);
                     break;

                case "新增功能说明":
                     dt = Scattered.GD_Change("3");
                     dt.TableName = tableName;
                     FillTable(CurrentDoc, dt, false, tableName);
                     break;

                case "预防性更动说明":
                     dt = Scattered.GD_Change("4");
                     dt.TableName = tableName;
                     FillTable(CurrentDoc, dt, false, tableName);
                     break;

                case "其他更动说明":
                     dt = Scattered.GD_Change("5");
                     dt.TableName = tableName;
                     FillTable(CurrentDoc, dt, false, tableName);
                     break;

                case "影响域分析":
                     dt = Scattered.InfectionAnaly();
                     dt.TableName = tableName;
                     FillTable(CurrentDoc, dt, false, tableName);
                     break;

                case "回归测试依据与测试用例的追踪关系":
                     dt = Scattered.HGTestCase_Xuqiu_According(DataTreeList, DocumentName, TestVerID);
                     dt.TableName = tableName;
                     FillTable(CurrentDoc, dt, false, tableName);                   
                     table = OutputComm.OutputComm.GetNodeInTable("回归测试依据与测试用例的追踪关系序号", CurrentDoc, null);
                     MergeTable(table, tableName, dt, CurrentDoc);
                     break;

                case "软件更动与测试用例的追踪关系":
                     dt = Scattered.HGTestCase_GD_According(DataTreeList, TestVerID);
                     dt.TableName = tableName;
                     FillTable(CurrentDoc, dt, false, tableName);
                     table = OutputComm.OutputComm.GetNodeInTable("软件更动与测试用例的追踪关系序号", CurrentDoc, null);
                     MergeTable(table, tableName, dt, CurrentDoc);
                     break;

                case "测试用例追踪关系1":
                     dt = TestCaseAccording_ZXTF();
                     dt.TableName = tableName;
                     FillTable(CurrentDoc, dt, false, tableName);
                     table = OutputComm.OutputComm.GetNodeInTable("测试用例追踪关系1序号", CurrentDoc, null);
                     MergeTable(table, tableName, dt, CurrentDoc);
                     break;

                default:
                    // System.Windows.Forms.MessageBox.Show("模板中存在错误的表格类型:" + tableName);
                    break;
            }

        }

        private DataTable GetAttachCaseTraceView(int p)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public void MergeTable(Table table, string TableName, DataTable dt, Document doc)
        {

            string[] FieldNameList = null;

            switch (TableName)
            {
                case "测试项与测试依据的追踪关系":
                    FieldNameList = new string[3] { "测试项与依据关系测试项章节", "测试项与依据关系测试项名称", "测试项与依据关系测试项标识" };
                    break;

                case "测试项与测试依据的追踪关系定型":
                    FieldNameList = new string[3] { "测试项与依据关系测试项章节", "测试项与依据关系测试项名称", "测试项与依据关系测试项标识" };
                    break;
                
                case "测试项与测试依据的追踪关系1":
                    FieldNameList = new string[3] { "测试项与依据关系1序号", "测试项与依据关系1测试项标识", "测试项与依据关系1测试项名称" };
                    break;

                case "测试依据与测试项的追踪关系":
                    FieldNameList = new string[4] { "依据与测试项关系依据标识", "依据与测试项关系依据", "依据与测试项关系说明", "依据与测试项关系追踪" };
                    break;

                case "测试依据与测试项的追踪关系定型":
                    FieldNameList = new string[4] { "依据与测试项关系依据标识", "依据与测试项关系依据", "依据与测试项关系说明", "依据与测试项关系追踪" };
                    break;

                case "测试依据与测试项的追踪关系1":
                    FieldNameList = new string[3] { "依据与测试项关系1序号", "依据与测试项关系1依据标识", "依据与测试项关系1依据" };
                    break;

                case "测试用例追踪关系":
                    FieldNameList = new string[2] { "测试项在计划的章节", "测试项名称" };
                    break;

                case "测试用例追踪关系1":
                    FieldNameList = new string[2] { "测试项标识", "测试项名称" };
                    break;

                case "执行结果统计":
                    FieldNameList = new string[6] { "测试用例名称", "测试用例标识", "执行状态", "执行结果", "错误步骤", "问题标识" };
                    break;

                case "未完整执行统计":
                    FieldNameList = new string[4] { "测试用例名称", "测试用例标识", "执行状态", "未执行或部分执行原因" };
                    break;

                case "提交问题一览":
                    FieldNameList = new string[3] { "问题标识", "问题类别", "问题级别" };
                    break;

                case "附件统计":
                    FieldNameList = new string[2] { "附件名称", "是否输出" };
                    break;

                case "其他更动涉及依据":
                    FieldNameList = new string[3] { "软件更动", "软件更动说明", "其它更动影响域分析" };
                    break;

                case "问题涉及依据":
                    FieldNameList = new string[3] { "软件问题", "是否更动", "影响域分析" };
                    break;

                case "原有用例统计":
                    FieldNameList = new string[2] { "测试用例名称", "测试用例标识" };
                    break;

                case "新增用例统计":
                    FieldNameList = new string[2] { "测试用例名称", "测试用例标识" };
                    break;

                case "未选取用例统计":
                    FieldNameList = new string[3] { "测试用例名称", "测试用例标识", "未测试原因" };
                    break;

                case "已有测试项":
                    FieldNameList = new string[4] { "测试类型", "测试项", "是否选取", "未选取原因说明" };
                    break;

                case "已有测试用例":
                    FieldNameList = new string[1] { "测试项" };
                    break;

                case "回归测试依据与测试项的追踪关系":
                    FieldNameList = new string[1] { "测试依据" };
                    break;

                case "回归测试项与测试依据的追踪关系":
                    FieldNameList = new string[1] { "测试项" };
                    break;

                case "回归测试项与测试用例的追踪关系":
                    FieldNameList = new string[1] { "测试项" };
                    break;

                case "回归测试用例与测试项的追踪关系":
                    FieldNameList = new string[1] { "测试项" };
                    break;

                case "测试包含关系":
                    FieldNameList = new string[2] { "测试类", "测试项" };
                    break;

                case "回归测试依据与测试用例的追踪关系":
                    FieldNameList = new string[1] { "测试依据"};
                    break;

                case "软件更动与测试用例的追踪关系":
                    FieldNameList = new string[1] { "软件更动"};
                    break;

                case "CMI管理项表":
                    FieldNameList = new string[1] { "所属基线合并" };
                    break;

                case "ZL组织与人员表_项目":
                    FieldNameList = new string[1] { "组织名称合并" };
                    break;

                case "配置组织与人员表":
                    FieldNameList = new string[1] { "配置组织合并" };
                    break;

                case "项目组织与人员表":
                    FieldNameList = new string[1] { "项目组织合并" };
                    break;

                //case "回归测试更动一览":
                //    FieldNameList = new string[6] { "序号", "更动类型", "软件问题", "是否更动", "更动标识", "更动说明" };
                //    break;

                case "回归测试更动一览":
                    FieldNameList = new string[3] { "序号", "更动标识", "更动说明" };
                    break;
                       
            }

            if ((TableName == "问题涉及依据") || (TableName == "其他更动涉及依据"))
            {
                OutputComm.OutputComm.MergeTable_AccordingColumn(2, FieldNameList, table, dt, doc, false);
                OutputComm.OutputComm.MergeColumn_2(1, table, 0, 0, dt, false);
            }
            else
            {
                if ((TableName == "回归测试用例与测试项的追踪关系") || (TableName == "测试依据与测试项的追踪关系1") || (TableName == "测试项与测试依据的追踪关系1") || (TableName == "回归测试更动一览"))
                {
                    OutputComm.OutputComm.MergeTable_AccordingColumn(1, FieldNameList, table, dt, doc, true);
                    OutputComm.OutputComm.MergeColumn_2(1, table, 0, 0, dt, true);
                }
                else if (TableName == "测试包含关系")
                {
                    // OutputComm.OutputComm.MergeTable_AccordingColumn(1, FieldNameList, table, dt, doc, true);
                    OutputComm.OutputComm.MergeTable_AccordingColumn(2, FieldNameList, table, dt, doc, true);
                    OutputComm.OutputComm.MergeColumn_2(1, table, 0, 0, dt, true);
                }
                else if (TableName == "CMI管理项表")
                {
                    OutputComm.OutputComm.MergeTable_AccordingColumn(1, FieldNameList, table, dt, doc, true);
                    OutputComm.OutputComm.MergeColumn_2(1, table, 0, 0, dt, true);
                }
                else if (TableName == "项目组织与人员表")
                {
                    OutputComm.OutputComm.MergeTable_AccordingColumn(1, FieldNameList, table, dt, doc, true);
                    OutputComm.OutputComm.MergeColumn_2(1, table, 0, 0, dt, true);
                }
                else if (TableName == "配置组织与人员表")
                {
                    OutputComm.OutputComm.MergeTable_AccordingColumn(1, FieldNameList, table, dt, doc, true);
                   
                }
                else
                {
                    OutputComm.OutputComm.MergeTable_AccordingColumn(2, FieldNameList, table, dt, doc, true);
                    OutputComm.OutputComm.MergeColumn_2(1, table, 0, 0, dt, true);

                }


            }

        }

        public static void RemoveSectionBreaks(Document doc, SectionStart? ss)
        {
            foreach (Section section in doc.Sections)
            {
                if (ss == null || section.PageSetup.SectionStart == ss.Value)
                {
                    Section prevSection = (Section)section.PreviousSibling;
                    if (prevSection == null) continue;
                    section.Remove();
                    prevSection.AppendContent(section);
                }
            }
        }

        public void OutputStartTable()
        {
            string[] mailMergeFieldsNames = CurrentDoc.MailMerge.GetFieldNames();

            foreach (string strFieldName in mailMergeFieldsNames)
            {
                string[] strArray = strFieldName.Split(':');

                if (strArray[0] != "TableStart") continue;

                string tableName = strArray[1];
                FillTableFields(tableName);

            }

        }

        public void ReplaceSingleTestTypeStatTable(Document doc1, DataTable dt, int TableNo, string Name)
        {

            Scattered.TestVerID = TestVerID;
            DataTable TestTypedt = Scattered.AddTableColumn("测试类型统计", "");

            int i = 0;

            foreach (DataRow dr in dt.Rows)
            {
                string TestTypeName = dr[0].ToString();

                DataRow dr1 = TestTypedt.Rows.Add();

                TestTypedt.Rows[i]["测试类型"] = TestTypeName;
                TestTypedt.Rows[i]["序号"] = i + 1;
                i = i + 1;

            }
            DocumentBuilder db = new DocumentBuilder(doc1);
            OutputComm.OutputComm.ReplaceTableName("与被测对象有关", TableNo, Name, db);

            TestTypedt.TableName = "测试类型统计";
            FillTable(doc1, TestTypedt, false, "");

        }

        public void ReplaceQuestionInfoStatTable(Document doc1, DataTable dt, int TableNo, string Name)
        {
            DocumentBuilder db = new DocumentBuilder(doc1);
            OutputComm.OutputComm.ReplaceTableName("与被测对象有关", TableNo, Name, db);

            dt.TableName = "提交问题一览";
            FillTable(doc1, dt, false, "");

        }

        public void OutputChapter_UnderHaveTable(string OutputType, string BSField, string BSChapter, string TextStartField, Document doc)//每个被测对象下跟一个表
        {
            int TableNo = 0;

            IfTiSheng = ResetIfTiSheng();
            Scattered.TestVerID = TestVerID;

            DocumentBuilder db = new DocumentBuilder(doc);
            Section Section = OutputComm.OutputComm.GetNodeInSection(BSChapter, doc, db);

            if (Section != null)
            {
                DataTable dt = Scattered.GetObjectName();
                if (dt.Rows.Count == 0)
                {
                    db.MoveToMergeField(TextStartField);
                    db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                    db.Writeln("无。");
                }
                else
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        string ObjectName = dr["被测对象名称"].ToString();
                        string ObjectID = dr["被测对象实测ID"].ToString();
                        string ObjectID_body = dr["被测对象实体ID"].ToString();

                        DataTable dt1 = Scattered.GetUnderHaveTableData(TextStartField, ObjectID, ObjectID_body, TestVerID, ProjectID, DataTreeList);

                        Document doc1 = OutputComm.OutputComm.AddTableUnderChapter(TextStartField, db, ObjectName, dt1, Section, CurrentDoc, IfTiSheng, "标题 2");

                        if (dt1.Rows.Count > 0)
                        {
                            TableNo = TableNo + 1;

                            if (OutputType == "测试类型统计")
                            {
                                ReplaceSingleTestTypeStatTable(doc1, dt1, TableNo, ObjectName);
                            }
                            else if (OutputType == "提交问题一览")
                            {
                                ReplaceQuestionInfoStatTable(doc1, dt1, TableNo, ObjectName);
                                Table table = OutputComm.OutputComm.GetNodeInTable(BSField, doc1, null);
                                MergeTable(table, OutputType, dt1, doc1);

                            }

                            OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, doc, null, db);

                        }
                        else
                        {
                            db.MoveToMergeField("准备替换");
                            db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                            db.Writeln("无。");

                        }
                    }
                }

                Node node1 = OutputComm.OutputComm.GetNodeByField(doc, TextStartField, db);
                if (node1 != null)
                {
                    node1.Remove();
                }

                Section.Remove();

            }
        }
        public string GetTestItemID_body(string ItemID_Test)
        {
            string TestItemID_body = "";
            string sqlstate = "select * from CA测试项实测表 where ID=?";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ItemID_Test);
            dt.TableName = "CA测试项实测表";
            if (dt.Rows.Count > 0)
            {
                TestItemID_body = dt.Rows[0]["测试项ID"].ToString();
            }
            return TestItemID_body;
        }

        public bool IfTestSonItem(string TestItemID)
        {
            int xh = 0;
            string sqlstate = "SELECT CA测试项实体表.父节点ID, CA测试项实测表.ID, CA测试项实测表.项目ID, CA测试项实测表.测试版本, CA测试项实测表.序号, CA测试项实体表.测试项名称 " +
                              " FROM CA测试项实体表 INNER JOIN CA测试项实测表 ON CA测试项实体表.ID = CA测试项实测表.测试项ID " +
                              " WHERE CA测试项实测表.ID=? AND CA测试项实测表.项目ID=? AND CA测试项实测表.测试版本=?";

               DataTable  dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestItemID, ProjectID, TestVerID);

               if (dt != null && dt.Rows.Count != 0)
               {
                     xh = (int)dt.Rows[0][4];

                     if (xh == 1) return false;

                     else return true;

                }
                  
               else return true;
               
        }

        public void OutputChapter_ChangeMuiChapter(string OutputType, string SonType, bool AddNew, string TypeStr, bool IfOutputAnnex)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        //不确定标题章节
        {
            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;
            string Nodeid;
            int NodeType1 = 0;

            string NextBrotherID;
            string NodeContentID = "0";
            string NodeContentJXm = "";
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;
            int TestObjectNum = 0;
            int TestCaseNum = 0;

            int FirstTestItem = 1;//2011-09-19+++
            int FirstCase_EachItem = 1;//2011-09-19+++
            int FirstTestType = 1;//2011-09-19+++

            bool IfKJPreHaveTestCase = false;


            iStack = 0;

            Scattered.TestVerID = TestVerID;

            DocumentBuilder db = new DocumentBuilder(CurrentDoc);
            Section Section = null;

            IfTiSheng = ResetIfTiSheng();
            if (TypeStr == "定型")
            {
                IfTiSheng = true;
            }

            if (OutputType == "测试用例")
            {
                Section = OutputComm.OutputComm.GetNodeInSection("测试用例表", CurrentDoc, db);
            }
            else if (OutputType == "测试项")
            {
                Section = OutputComm.OutputComm.GetNodeInSection("测试项信息表", CurrentDoc, db);
            }

            Section TestStatSection = OutputComm.OutputComm.GetNodeInSection("测试设计情况统计表", CurrentDoc, db);

            if (DataTreeList.Count <= 0)
            {
                if (OutputType == "测试用例")
                {
                    db.MoveToMergeField("可变章节_测试用例");
                    db.ParagraphFormat.Style = CurrentDoc.Styles["文本首行缩进"];
                    db.Writeln("无。");
                }
                else if (OutputType == "测试项")
                {
                    db.MoveToMergeField("可变章节_测试项");
                    db.ParagraphFormat.Style = CurrentDoc.Styles["文本首行缩进"];
                    db.Writeln("无。");
                }
                Section.Remove();

                if (TestStatSection != null)
                {
                    TestStatSection.Remove();
                }

                return;
            }

            //++++++++++++++++++++
            ArrayList TestCaseList = null;
            if (AddNew == true)
            {
                TestCaseList = Scattered.TestCaseList_PerVer();

            }

            //++++++++++++++++++++
            string FieldName = Scattered.GetFieldName(OutputType);

            DataView dv = TPM3.wx.AttachCaseTrace.GetAttachCaseTraceView(3);            

            NodeTree node = (NodeTree)DataTreeList[0];

            try
            {
                do
                {
                    while ((node != null) && (node.NodeType <= 4))
                    {
                        //=====输出
                        Nodeid = node.NodeID;
                        NodeType1 = node.NodeType;
                        FirstSonID = node.FirstSonID;
                        NextBrotherID = node.NextBrotherID;
                        NodeContent = node.NodeContent;
                        NodeContentID = node.NodeContentID_test;
                        NodeContentJXm = node.NodeContentJXm;
                        testcaseflag = node.testcaseflag;
                        layer = node.Layer;

                        int btno = layer + 1;

                        if (NodeType1 == 1)
                        {
                            TestObjectNum = TestObjectNum + 1;

                            DataTable dt = null;
                            if (OutputType == "测试用例")
                            {
                                if (SonType == "设计")
                                {
                                    dt = Scattered.GetStatTable(node.NodeContentID_body);
                                    dt.TableName = "测试设计情况_设计";

                                }
                                else if (SonType == "总结")
                                {
                                    dt = Scattered.GetStatTable(node.NodeContentID_body);
                                    dt.TableName = "测试设计情况_总结";

                                }

                            }
                            else if (OutputType == "测试项")
                            {
                                dt = Scattered.GetStatTable(node.NodeContentID_body);
                                dt.TableName = "测试设计情况_需求";
                            }

                            Document doc1 = null;
                            if (TypeStr != "定型")
                            {
                                doc1 = OutputComm.OutputComm.MuiChapterProcess_1(FieldName, NodeContent, db,
                                    TestStatSection, CurrentDoc, btno, IfTiSheng);
                            }


                            if (doc1 != null)
                            {
                                DocumentBuilder db1 = new DocumentBuilder(doc1);

                                FillTable(doc1, dt, false, "");

                                OutputComm.OutputComm.ReplaceTableName("与被测对象有关", TestObjectNum, NodeContent, db1);

                                OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, CurrentDoc, TestStatSection,
                                    db);
                            }

                        }

                        if (NodeType1 == 2)
                        {
                            if (OutputType == "测试用例") //2011-09-19+++
                            {
                                if (FirstTestType == 1)
                                {
                                    OutputComm.OutputComm.MuiChapterProcess_2(FieldName, CurrentDoc, btno, NodeContent,
                                        IfTiSheng, false);
                                    FirstTestType = 0;
                                    FirstTestItem = 1;
                                }
                                else
                                {
                                    OutputComm.OutputComm.MuiChapterProcess_2(FieldName, CurrentDoc, btno, NodeContent,
                                        IfTiSheng, true);
                                    FirstTestItem = 1;

                                }
                            }
                            else
                            {
                                OutputComm.OutputComm.MuiChapterProcess_2(FieldName, CurrentDoc, btno, NodeContent,
                                    IfTiSheng, false);

                            }

                        }
                        if (NodeType1 == 3)
                        {
                            NodeContent = RemoveHC(NodeContent);
                            IfKJPreHaveTestCase = false;

                            if (OutputType == "测试用例")
                            {
                                if (FirstTestItem == 1) //2011-09-19+++
                                {
                                    OutputComm.OutputComm.MuiChapterProcess_2(FieldName, CurrentDoc, btno, NodeContent,
                                        IfTiSheng, false);
                                    FirstTestItem = 0;
                                    FirstCase_EachItem = 1;
                                }
                                else //2011-09-19+++
                                {
                                    if (IfTestSonItem(NodeContentID) == false)
                                    {
                                        OutputComm.OutputComm.MuiChapterProcess_2(FieldName, CurrentDoc, btno,
                                            NodeContent, IfTiSheng, false);
                                    }
                                    else
                                    {
                                        OutputComm.OutputComm.MuiChapterProcess_2(FieldName, CurrentDoc, btno,
                                            NodeContent, IfTiSheng, true);
                                    }

                                    FirstCase_EachItem = 1;

                                }

                            }
                            else if (OutputType == "测试项")
                            {
                                string TestItemBodyID = GetTestItemID_body(NodeContentID);

                                if (IfHaveSonItem(TestItemBodyID) == false)
                                {
                                    OutputComm.OutputComm.MuiChapterProcess_3(FieldName, CurrentDoc, btno, NodeContent,
                                        IfTiSheng, 1, 0, false, false, false);
                                    Document doc1 = new Document();
                                    DocumentBuilder db1 = new DocumentBuilder();
                                    OutputComm.OutputComm.ImportSec(doc1, Section);
                                    ReplaceSingle_ItemOrTestCase(NodeContentID, doc1, NodeContentJXm, OutputType, false,
                                        null, null, IfOutputAnnex, TypeStr);
                                    OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, CurrentDoc, null, db);

                                }
                                else
                                {
                                    OutputComm.OutputComm.MuiChapterProcess_3(FieldName, CurrentDoc, btno, NodeContent,
                                        IfTiSheng, 1, 0, false, false, true);

                                }

                            }
                        }
                        if (NodeType1 == 4)
                        {
                            if (OutputType == "测试用例")
                            {
                                NodeContent = RemoveHC(NodeContent);
                                if (testcaseflag == 1)
                                {
                                    IfKJPreHaveTestCase = true;

                                    TestCaseNum = TestCaseNum + 1;

                                    if (FirstCase_EachItem == 1) //2011-09-19+++
                                    {
                                        OutputComm.OutputComm.MuiChapterProcess_3(FieldName, CurrentDoc, btno,
                                            NodeContent, IfTiSheng, 2, TestCaseNum, false, false, false);
                                        FirstCase_EachItem = 0;
                                    }
                                    else //2011-09-19+++
                                    {
                                        OutputComm.OutputComm.MuiChapterProcess_3(FieldName, CurrentDoc, btno,
                                            NodeContent, IfTiSheng, 2, TestCaseNum, true, false, false);

                                    }

                                    Document doc1 = new Document();

                                    OutputComm.OutputComm.ImportSec(doc1, Section);
                                    ReplaceSingle_ItemOrTestCase(NodeContentID, doc1, NodeContentJXm, OutputType, AddNew,
                                        TestCaseList, dv, IfOutputAnnex, "");

                                    OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, CurrentDoc, null, db);

                                }
                                else
                                {
                                    TestCaseNum = TestCaseNum + 1;
                                    string referenceBS = Scattered.GetDirectTestCaseBS(NodeContentID, DataTreeList);
                                    OutputComm.OutputComm.MuiChapterProcess_3(FieldName, CurrentDoc, btno, NodeContent,
                                        IfTiSheng, 2, TestCaseNum, false, IfKJPreHaveTestCase, false);

                                    db.MoveToMergeField("准备替换");
                                    db.Writeln("见测试用例" + referenceBS + "。");

                                }
                            }
                        }

                        stack[iStack] = node;
                        iStack = iStack + 1;

                        node_pre = node;

                        //-----push(stack,p)                                      
                        if (FirstSonID != "0")
                        {
                            node = (NodeTree) DataTreeList[int.Parse(FirstSonID)];
                        }
                        else
                        {
                            node = null;
                        }

                    }
                    if (iStack == 0)
                    {
                        if (OutputType == "测试用例")
                        {

                            //for (int j = 0; j <= TestCaseNum - 1; j++)
                            //{
                            //    Node ChangeNode = OutputComm.OutputComm.GetNodeByField(CurrentDoc, "用例前域" + (j + 1).ToString(), db);

                            //    if (ChangeNode.NodeType == NodeType.Paragraph)
                            //    {
                            //        Paragraph pg = (Paragraph)ChangeNode;
                            //        pg.Remove();
                            //    }
                            //    else
                            //    {
                            //        Paragraph pg = (Paragraph)ChangeNode.GetAncestor(NodeType.Paragraph);
                            //        pg.Remove();
                            //    }

                            //}

                            db.MoveToMergeField("可变章节_测试用例");
                        }
                        else if (OutputType == "测试项")
                        {
                            db.MoveToMergeField("可变章节_测试项");
                        }
                        Section.Remove();

                        if (TestStatSection != null)
                        {
                            TestStatSection.Remove();
                        }

                        BatchOutput.jihua_sepc = false;

                        return;
                    }
                    else // -----pop(stack,p)
                    {
                        iStack = iStack - 1;
                        node_pre = stack[iStack];

                        if (node_pre.NextBrotherID != "0")
                        {
                            node = (NodeTree) DataTreeList[int.Parse(node_pre.NextBrotherID)];
                        }
                        else
                        {
                            node = null;

                        }

                    }
                } while (true);
            }
            catch (Exception e)
            {
                var str = e.Message;
                throw e;
            }


        }

        public void OutputChapter_QuestionReport(Document doc)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {

            Scattered.TestVerID = TestVerID;

            IfTiSheng = ResetIfTiSheng();

            DocumentBuilder db = new DocumentBuilder(CurrentDoc);

            Section section = OutputComm.OutputComm.GetNodeInSection("问题报告一章", CurrentDoc, db);

            DataTable dt = Scattered.GetObjectName();

            if (dt.Rows.Count == 0)
            {
                db.MoveToMergeField("可变章节_问题报告单");
                db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                db.Writeln("无。");
            }
            else
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string ObjectName = dr["被测对象名称"].ToString();
                    string ObjectID = dr["被测对象实测ID"].ToString();

                    PblRepsForm frm = new PblRepsForm();
                    frm.SimulateFormLoad(); // added by zhouxindong for init pblid->pblname dictionary
                    Dictionary<string, List<string>> retli = frm.GetPblsForObj(ObjectID);
                    List<string> li = new List<string>();

                    if (retli.Count > 0)
                    {
                        OutputComm.OutputComm.MuiChapterProcess_1("可变章节_问题报告单", ObjectName + "提交的问题", db, null, CurrentDoc, 2, IfTiSheng);

                        foreach (string QuestionDir in retli.Keys)
                        {
                            if (QuestionDir != "")
                            {
                                OutputComm.OutputComm.MuiChapterProcess_2("可变章节_问题报告单", doc, 3, QuestionDir, IfTiSheng, false);//2011-09-19******//

                            }

                            li = retli[QuestionDir];
                            for (int m = 0; m <= li.Count - 1; m++)
                            {
                                db.MoveToMergeField("可变章节_问题报告单");
                                db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                                OutputComm.OutputComm.AddField("准备替换", db);
                                OutputComm.OutputComm.AddField("可变章节_问题报告单", db);

                                string RequestID = li[m].ToString();

                                Document doc1 = new Document();
                                DocumentBuilder db1 = new DocumentBuilder(doc1);
                                OutputComm.OutputComm.ImportSec(doc1, section);

                                ReplaceQuestion(RequestID, doc1, ObjectName, db1);

                                OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, CurrentDoc, null, db);

                            }
                        }
                    }
                    else
                    {
                        OutputComm.OutputComm.MuiChapterProcess_1("可变章节_问题报告单", ObjectName + "提交的问题", db, null, CurrentDoc, 2, IfTiSheng);

                        db.MoveToMergeField("可变章节_问题报告单");
                        db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                        db.Writeln("此被测对象无提交问题。");
                    }
                }

                db.MoveToMergeField("可变章节_问题报告单");
                section.Range.Delete();
            }

        }

        public void ReplaceQuestion(string QuestionID, Document doc, string ObjectName, DocumentBuilder db)
        {

            Scattered.TestVerID = TestVerID;
            string Value = "";

            ArrayList LevelList = Scattered.QuestionLevelList("级别");
            ArrayList ClassList = Scattered.QuestionLevelList("类别");

            string SqlState = "select 问题级别 from CA问题报告单 where ID=?";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(SqlState, QuestionID);
            if (dt1 != null)
            {
                DataRow dr = dt1.Rows[0];
                Value = Scattered.GetQuestionLevelName(dr[0].ToString());
            }

            for (int i = 1; i <= LevelList.Count; i++)
            {
                db.MoveToMergeField("级别" + i.ToString());
                db.Write(LevelList[i - 1].ToString());

                if (Value == LevelList[i - 1].ToString())
                {
                    string s = Assembly.GetEntryAssembly().Location;
                    s = Path.GetDirectoryName(s);
                    s = Path.Combine(s, @GlobalData.BaseDirectory + "dot\\Picture\\Question.bmp");
                    Image image = Image.FromFile(s);

                    if (image != null)
                    {
                        db.MoveToMergeField(i.ToString() + "级");
                        db.InsertImage(image);
                    }
                }
                else
                {
                    db.MoveToMergeField(i.ToString() + "级");
                }

            }

            SqlState = "select 问题类别 from CA问题报告单 where ID=?";
            dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(SqlState, QuestionID);
            if (dt1 != null)
            {
                DataRow dr = dt1.Rows[0];
                Value = Scattered.GetQuestionTypeName(dr[0].ToString());
            }

            for (int i = 1; i <= ClassList.Count; i++)
            {
                db.MoveToMergeField("类别" + i.ToString());
                db.Write(ClassList[i - 1].ToString());

                if (Value == ClassList[i - 1].ToString())
                {
                    string s = Assembly.GetEntryAssembly().Location;
                    s = Path.GetDirectoryName(s);
                    s = Path.Combine(s, @GlobalData.BaseDirectory + "dot\\Picture\\Question.bmp");
                    Image image = Image.FromFile(s);

                    if (image != null)
                    {
                        db.MoveToMergeField(i.ToString() + "类");
                        db.InsertImage(image);
                    }
                }
                else
                {
                    db.MoveToMergeField(i.ToString() + "类");
                }

            }

            OutputComm.OutputComm.QuestionReportSupplyProcess(doc, db, "类别", "级别", 5);


            if (db.MoveToMergeField("测试用例在记录的章节+测试用例名称+测试用例标识"))
            {

                SqlState = "SELECT DISTINCT CA测试过程实测表.问题报告单ID, CA测试用例实测表.ID AS 测试用例实测ID, CA测试用例实体表.测试用例名称, CA测试过程实测表.测试版本, CA测试过程实测表.项目ID, CA测试用例实测表.测试版本, CA测试用例实测表.项目ID " +
                           " FROM (CA测试用例实体表 INNER JOIN (CA测试过程实体表 INNER JOIN (CA测试过程实测表 INNER JOIN CA问题报告单 ON CA测试过程实测表.问题报告单ID = CA问题报告单.ID) ON CA测试过程实体表.ID = CA测试过程实测表.过程ID) ON CA测试用例实体表.ID = CA测试过程实体表.测试用例ID) INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID " +
                           " WHERE (((CA测试过程实测表.问题报告单ID)=?) AND ((CA测试过程实测表.测试版本)=?) AND ((CA测试过程实测表.项目ID)=?) AND ((CA测试用例实测表.测试版本)=?) AND ((CA测试用例实测表.项目ID)=?));";

                string TestCaseInfo = "";
                DataTable dt2 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(SqlState, QuestionID, TestVerID, ProjectID, TestVerID, ProjectID);
                int currenti = 0;
                if (dt2 != null || dt2.Rows.Count != 0)
                {
                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        string TestCaseID = dr2[1].ToString();
                        string TestCaseName = dr2[2].ToString();
                        string TestCaseBS = Scattered.GetDirectTestCaseBS(TestCaseID, DataTreeList);
                        string TestCaseZjinRecord;

                        //if (GlobalConst.GenTestCaseTitle == true)
                        {
                            TestCaseZjinRecord = Scattered.GetTestRecordOutputZJ(TestCaseID, DataTreeList);
                        }
                        //else
                        //{
                        //    TestCaseZjinRecord = GetTestRecordOutputZJ_cc(TestCaseName);
                        //}

                        currenti = currenti + 1;

                        if (currenti == dt2.Rows.Count)
                        {
                            TestCaseInfo = TestCaseInfo + TestCaseZjinRecord + "  " + TestCaseName + "（" + TestCaseBS + "）";
                        }
                        else
                        {
                            TestCaseInfo = TestCaseInfo + TestCaseZjinRecord + "  " + TestCaseName + "（" + TestCaseBS + "）" + "\n";
                        }
                    }

                    db.Write(TestCaseInfo);

                }

            }

            if (db.MoveToMergeField("问题标识"))
            {
                string bs = CommonDB.GenPblSignForStep(TPM3.Sys.GlobalData.globalData.dbProject, ConstDef.PblSplitter(), QuestionID);
                db.Write(bs);
            }
            if (db.MoveToMergeField("被测对象"))
            {
                db.Write(ObjectName);
            }

            Node node = OutputComm.OutputComm.GetNodeByField(doc, "问题描述", db);
            if (node != null)
            {
                SqlState = "select 问题描述 from CA问题报告单 where ID=?";
                dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(SqlState, QuestionID);

                object Value1 = OutputComm.OutputComm.OLEObjectProcess_1(dt1, doc, node);
                if (Value1 != null)
                {
                    db.Write(Value1.ToString());
                }
            }

            node = OutputComm.OutputComm.GetNodeByField(doc, "附注及修改建议", db);
            if (node != null)
            {
                SqlState = "select 附注及修改建议 from CA问题报告单 where ID=?";
                dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(SqlState, QuestionID);

                if ((dt1 != null) && (dt1.Rows.Count > 0))
                {
                    string fz = dt1.Rows[0][0].ToString();
                    if (fz == "")
                    {
                        db.Write("无。");
                    }
                    else
                    {
                        db.Write(fz);
                    }
                }
                else
                {
                    db.Write("无。");
                }

            }
            if (db.MoveToMergeField("报告人"))
            {
                SqlState = "select 报告人 from CA问题报告单 where ID=?";
                dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(SqlState, QuestionID);

                if (dt1 != null)
                {
                    DataRow dr = dt1.Rows[0];
                    Value = GetInChangeStr("人员", dr["报告人"].ToString());
                }

                db.Write(Value.ToString());

            }
            if (db.MoveToMergeField("报告日期"))
            {
                SqlState = "select 报告日期 from CA问题报告单 where ID=?";
                dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(SqlState, QuestionID);

                if (dt1 != null)
                {
                    DataRow dr = dt1.Rows[0];
                    DateTime ColumnValue1 = (DateTime)dr[0];

                    string Monthvalue = "";
                    string Dayvalue = "";

                    Monthvalue = ColumnValue1.Month.ToString();
                    if (int.Parse(ColumnValue1.Month.ToString()) < 10)
                    {
                        Monthvalue = "0" + ColumnValue1.Month.ToString();
                    }

                    Dayvalue = ColumnValue1.Day.ToString();
                    if (int.Parse(ColumnValue1.Day.ToString()) < 10)
                    {
                        Dayvalue = "0" + ColumnValue1.Day.ToString();

                    }

                    Value = ColumnValue1.Year.ToString() + "-" + Monthvalue + "-" + Dayvalue;

                }

                db.Write(Value.ToString());

            }

        }

        public bool IfHaveSonItem(string Item_ID)//实体ID
        {
            bool IfHaveSon = false;

            string sqlstate = "select * from CA测试项实体表 where 父节点ID=?";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, Item_ID);
            dt.TableName = "CA测试项实体表";
            if (dt.Rows.Count > 0)
            {
                IfHaveSon = true;
            }

            return IfHaveSon;

        }


        public void ReplaceSingle_ItemOrTestCase(string ID, Document doc, string BS, string Type, bool AndNew, ArrayList TestCaseList,DataView dv,bool IfOutputAnnex,string typestr)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {
            int flag1 = 0;
            Scattered.TestVerID = TestVerID;

            if (Type == "测试项")
            {
                string sqlstate = "SELECT CA测试项实体表.测试项名称, CA测试项实体表.优先级, CA测试项实测表.追踪关系,CA测试项实体表.测试方法说明, CA测试项实体表.终止条件, CA测试项实体表.测试项说明及测试要求, CA测试项实测表.ID, CA测试项实体表.充分性要求, CA测试项实体表.约束条件, CA测试项实体表.评判标准" +
                              " FROM CA测试项实体表 INNER JOIN CA测试项实测表 ON CA测试项实体表.ID = CA测试项实测表.测试项ID WHERE CA测试项实测表.ID=?";

                DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ID);
                dt.TableName = "CA测试项实体表";
                DataRow dr = dt.Rows[0];

                DocumentBuilder db = new DocumentBuilder(doc);

                string tempValue = "";
                if (db.MoveToMergeField("测试项名称"))
                {
                    db.Write(RemoveHC(dr["测试项名称"].ToString()));
                }
                if (db.MoveToMergeField("测试项标识"))
                {
                    db.Write(BS);
                }
                if (db.MoveToMergeField("充分性要求"))
                {
                    db.Write(dr["充分性要求"].ToString());
                }
                if (db.MoveToMergeField("约束条件"))
                {
                    db.Write(dr["约束条件"].ToString());
                }
                if (db.MoveToMergeField("评判标准"))
                {
                    db.Write(dr["评判标准"].ToString());
                }
                if (db.MoveToMergeField("优先级"))
                {
                    tempValue = GetInChangeStr("测试项优先级", dr["优先级"].ToString());
                    db.Write(tempValue);
                }
                if (db.MoveToMergeField("追踪关系"))
                {
                    //----------new
                    if ((DocumentName == "需求分析") || (DocumentName == "回归测试方案") || (typestr == "定型"))
                    {
                        ArrayList TestAccordingList = Scattered.GetTestAccording(ID);
                        ArrayList TestAccordingListBS = Scattered.GetTestAccordingBS(ID, DataTreeList, 0);

                        if (TestAccordingList.Count >= 1)
                        {
                            for (int i = 0; i <= TestAccordingList.Count - 1; i++)
                            {
                                if (i == TestAccordingList.Count - 1)
                                {
                                    db.Write(TestAccordingListBS[i].ToString() + "—" + TestAccordingList[i].ToString());
                                }
                                else
                                {
                                    db.Write(TestAccordingListBS[i].ToString() + "—" + TestAccordingList[i].ToString() + "\n");
                                }
                            }

                        }
                    }
                    else if ((DocumentName == "测试计划") && (BatchOutput.jihua_sepc == false) && (typestr != "定型"))
                    {
                        string TPZJ = Scattered.GetTestTPOrTAOutputZJ(1, ID, DataTreeList);
                        db.Write("测试需求规格说明第" + TPZJ + "节");

                    }
                    else if ((DocumentName == "测试计划") && (BatchOutput.jihua_sepc == true) && (typestr != "定型"))
                    {

                        ArrayList TestAccordingList = Scattered.GetTestAccording(ID);
                        ArrayList TestAccordingListBS = Scattered.GetTestAccordingBS(ID, DataTreeList, 0);

                        if (TestAccordingList.Count >= 1)
                        {
                            for (int i = 0; i <= TestAccordingList.Count - 1; i++)
                            {
                                if (i == TestAccordingList.Count - 1)
                                {
                                    db.Write(TestAccordingListBS[i].ToString() + "—" + TestAccordingList[i].ToString());
                                }
                                else
                                {
                                    db.Write(TestAccordingListBS[i].ToString() + "—" + TestAccordingList[i].ToString() + "\n");
                                }
                            }

                        }

                    }
                    //---------old
                    //tempValue = GetInChangeStr("测试项追踪关系", dr["追踪关系"].ToString());
                    //db.Write(tempValue);
                }

                doc.MailMerge.Execute(dr);

            }
            else if (Type == "测试用例")
            {
                //string sqlstate= "SELECT CA测试用例实体表.测试用例名称, CA测试用例实体表.用例描述, CA测试用例实体表.用例的初始化, CA测试用例实体表.前提和约束, " +
                //                 " CA测试用例实体表.所使用的设计方法, CA测试用例实体表.测试过程终止条件, CA测试用例实体表.测试结果评估标准, CA测试用例实体表.设计人员, CA测试用例实测表.ID, " +
                //                 " CA测试用例实测表.执行状态, CA测试用例实测表.执行结果, CA测试用例实测表.测试人员, CA测试用例实测表.测试时间, CA测试用例实测表.测试结论, CA测试用例实测表.未执行原因" +
                //                 " FROM CA测试用例实体表 INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID WHERE CA测试用例实测表.ID=? AND CA测试用例实测表.项目ID=? AND CA测试用例实测表.测试版本=?";

                string sqlstate = "SELECT CA测试用例实体表.测试用例名称,CA测试用例实体表.ID,CA测试用例实体表.用例描述, CA测试用例实体表.用例的初始化, CA测试用例实体表.前提和约束, CA测试用例实体表.所使用的设计方法, CA测试用例实体表.测试过程终止条件, CA测试用例实体表.测试结果评估标准, CA测试用例实体表.设计人员, CA测试用例实测表.ID, CA测试用例实测表.执行状态, CA测试用例实测表.执行结果, CA测试用例实测表.测试人员, CA测试用例实测表.测试时间, CA测试用例实测表.测试结论, CA测试用例实测表.未执行原因, CA测试用例与测试项关系表.测试项ID, CA测试项实体表.测试项名称" +
                                  " FROM (CA测试项实测表 INNER JOIN (CA测试用例与测试项关系表 INNER JOIN (CA测试用例实体表 INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID) ON CA测试用例与测试项关系表.测试用例ID = CA测试用例实测表.ID) ON CA测试项实测表.ID = CA测试用例与测试项关系表.测试项ID) INNER JOIN CA测试项实体表 ON CA测试项实测表.测试项ID = CA测试项实体表.ID " +
                                  " WHERE CA测试用例实测表.ID=? AND CA测试用例实测表.项目ID=? AND CA测试用例实测表.测试版本=? AND CA测试用例与测试项关系表.直接所属标志=True;";//=====2011-09-20////----加最后一个条件

                DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ID, ProjectID, TestVerID);
                dt.TableName = "CA测试用例实体表";
                if (dt.Rows.Count > 0)
                {

                    DataRow dr = dt.Rows[0];

                    DocumentBuilder db = new DocumentBuilder(doc);

                    if (db.MoveToMergeField("测试用例名称"))
                    {
                        db.Write(RemoveHC(dr["测试用例名称"].ToString()));
                    }

                    if (db.MoveToMergeField("追踪关系"))
                    {
                        string zj = "";
                        string itemname = dr["测试项名称"].ToString();

                        if (IfFirstTest() == true)//=====2011-09-20////----回归测试中用例对条目的追踪不再加计划中测试条目的章节号
                        {
                            zj = Scattered.GetTestTPOrTAOutputZJ(2, dr["测试项ID"].ToString(), DataTreeList);
                            db.Write(zj + " " + itemname);
                        }
                        else
                        {
                            db.Write(itemname);
                        }

                    }
                    if (db.MoveToMergeField("测试用例标识"))
                    {
                        db.Write(BS);
                    }
                    if (db.MoveToMergeField("所使用的设计方法"))
                    {
                        string tempValue = GetInChangeStr("测试用例所使用的设计方法", dr["所使用的设计方法"].ToString());
                        db.Write(tempValue);
                    }

                    if (AndNew == true)
                    {
                        if (Scattered.IfInIt_Case(TestCaseList, ID) == false)
                        {
                            if (db.MoveToMergeField("是否新增"))
                            {
                                db.Write("是");
                            }
                        }
                        else
                        {
                               if (db.MoveToMergeField("是否新增"))
                              {
                                  db.Write("否");
                              }
                        }


                    }

                    if (db.MoveToMergeField("设计人员"))
                    {
                        string tempValue = GetInChangeStr("人员", dr["设计人员"].ToString());
                        db.Write(tempValue);
                    }
                    if (db.MoveToMergeField("测试人员"))
                    {
                        string tempValue = GetInChangeStr("人员", dr["测试人员"].ToString());
                        db.Write(tempValue);
                    }
                    if (db.MoveToMergeField("测试时间"))
                    {
                        flag1 = 1;
                        try
                        {
                            DateTime time = (DateTime)dr["测试时间"];
                            string timestr = time.Year.ToString() + "-" + time.Month.ToString() + "-" + time.Day.ToString();
                            db.Write(timestr);
                        }
                        catch
                        {
                            db.Write("");
                        }
                    }
                    if (db.MoveToMergeField("问题单标识"))
                    {
                        ArrayList BJList = new ArrayList();

                        sqlstate = "SELECT CA测试用例实测表.测试用例ID, CA测试过程实测表.序号, CA测试过程实测表.问题报告单ID, CA测试过程实测表.测试版本, CA测试过程实测表.项目ID " +
                                  " FROM (CA测试用例实体表 INNER JOIN (CA测试过程实体表 INNER JOIN CA测试过程实测表 ON CA测试过程实体表.ID = CA测试过程实测表.过程ID) ON CA测试用例实体表.ID = CA测试过程实体表.测试用例ID) INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID " +
                                  " WHERE (((CA测试过程实测表.问题报告单ID) Is Not Null) AND ((CA测试用例实测表.ID)=?) AND ((CA测试用例实测表.测试版本)=?) AND ((CA测试用例实测表.项目ID)=?) AND ((CA测试过程实测表.测试版本)=?) AND ((CA测试过程实测表.项目ID)=?)) ORDER BY CA测试过程实测表.序号;";

                        DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ID, TestVerID, ProjectID, TestVerID, ProjectID);
                        int kk = 0;
                        string pingstr = "";
                        if (dt1 != null && dt1.Rows.Count != 0)
                        {
                            bool haveflag = false;
                            foreach (DataRow dr1 in dt1.Rows)
                            {
                                kk++;

                                foreach (string liststr in BJList)
                                {
                                    if (liststr == dr1[2].ToString())
                                    {
                                        haveflag = true;
                                    }
                                    else
                                    {
                                        haveflag = false;
                                    }

                                }

                                if (haveflag == false)
                                {
                                    if (kk != dt1.Rows.Count)
                                    {
                                        pingstr = pingstr + CommonDB.GenPblSignForStep(TPM3.Sys.GlobalData.globalData.dbProject, ConstDef.PblSplitter(), dr1[2].ToString()) + "、";
                                    }
                                    else
                                    {
                                        pingstr = pingstr + CommonDB.GenPblSignForStep(TPM3.Sys.GlobalData.globalData.dbProject, ConstDef.PblSplitter(), dr1[2].ToString());
                                    }

                                    BJList.Add(dr1[2].ToString());
                                }


                            }

                        }
                        db.Write(pingstr);

                    }

                    doc.MailMerge.Execute(dr);

                    if (flag1 == 1)//输出记录
                    {
                        dt = EditTestStepColumn_TL(dv, ID);

                        dt.TableName = "测试过程";
                        FillTable(doc, dt, false, "测试过程");

                        if (IfOutputAnnex == true)
                        {
                            db.MoveToDocumentEnd();

                            PutoutAnnex(doc, 2, db, dr[1].ToString());
                        }

                    }
                    else//输出说明
                    {
                        dt = EditTestStepColumn_TS(dv, ID);

                        dt.TableName = "测试过程";
                        FillTable(doc, dt, false, "测试过程");
                        if (IfOutputAnnex == true)
                        {
                            db.MoveToDocumentEnd();
                            PutoutAnnex(doc, 1, db, dr[1].ToString());
                        }

                    }
                }
            }
            
        }

        public string GetTestCaseBodyID(string TestCaseID_test)
        {
            string TestCase_body = "";
            string  sqlstate = "select 测试用例ID from CA测试用例实测表 where ID=?";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestCaseID_test);

            if (dt != null && dt.Rows.Count != 0)
            {
                TestCase_body = dt.Rows[0][0].ToString();
            }

            return TestCase_body;

        }

        public DataTable EditTestStepColumn_TS(DataView dv,string TestCaseID)
        {
            string  sqlstate = "SELECT CA测试过程实体表.输入及操作, CA测试过程实体表.期望结果, CA测试用例实测表.测试用例ID, CA测试过程实测表.实测结果, CA测试过程实测表.序号, CA测试过程实测表.测试版本 " +
                               " FROM (CA测试用例实体表 INNER JOIN (CA测试过程实体表 INNER JOIN CA测试过程实测表 ON CA测试过程实体表.ID = CA测试过程实测表.过程ID) ON CA测试用例实体表.ID = CA测试过程实体表.测试用例ID) INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID " +
                               " WHERE CA测试用例实测表.ID=? AND CA测试过程实测表.测试版本=? ORDER BY CA测试过程实测表.序号;";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestCaseID, TestVerID);

            if (dt != null && dt.Rows.Count != 0)
            {
                TestCaseID = GetTestCaseBodyID(TestCaseID);
  
                foreach (DataRow dr in dt.Rows)
                {
                    string xuhao = dr["序号"].ToString();

                    string returnstr = IfhaveAnnex(dv,TestCaseID,"输入及操作",xuhao);
                    if (returnstr!="")
                    {
                        dr.BeginEdit();
                        dr["输入及操作"] = dr["输入及操作"].ToString() + returnstr;
                        dr.EndEdit();
                    }
                   
                }
            }
            return dt;

        }
        public DataTable EditTestStepColumn_TL(DataView dv, string TestCaseID)
        {
            string sqlstate = "SELECT CA测试过程实体表.输入及操作, CA测试过程实体表.期望结果, CA测试用例实测表.测试用例ID, CA测试过程实测表.实测结果, CA测试过程实测表.序号, CA测试过程实测表.测试版本 " +
                               " FROM (CA测试用例实体表 INNER JOIN (CA测试过程实体表 INNER JOIN CA测试过程实测表 ON CA测试过程实体表.ID = CA测试过程实测表.过程ID) ON CA测试用例实体表.ID = CA测试过程实体表.测试用例ID) INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID " +
                               " WHERE CA测试用例实测表.ID=? AND CA测试过程实测表.测试版本=? ORDER BY CA测试过程实测表.序号;";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestCaseID, TestVerID);

            if (dt != null && dt.Rows.Count != 0)
            {
                TestCaseID = GetTestCaseBodyID(TestCaseID);

                foreach (DataRow dr in dt.Rows)
                {
                    string xuhao = dr["序号"].ToString();

                    string returnstr = IfhaveAnnex(dv, TestCaseID, "输入及操作", xuhao);
                    if (returnstr != "")
                    {
                        dr.BeginEdit();
                        dr["输入及操作"] = dr["输入及操作"].ToString() + returnstr;
                        dr.EndEdit();
                    }
                    returnstr = IfhaveAnnex(dv, TestCaseID, "期望结果", xuhao);
                    if (returnstr != "")
                    {
                        dr.BeginEdit();
                        dr["期望结果"] = dr["期望结果"].ToString() + returnstr;
                        dr.EndEdit();
                    }
                    returnstr = IfhaveAnnex(dv, TestCaseID, "实测结果", xuhao);
                    if (returnstr != "")
                    {
                        dr.BeginEdit();
                        dr["实测结果"] = dr["实测结果"].ToString() + returnstr;
                        dr.EndEdit();
                    }
                }
            }
            return dt;

        }
                    
        public string IfhaveAnnex(DataView dv,string TestCaseID,string Column,string StepNo)
        {
            string AnnexInfo="";
          
            for (int i = 0; i <= dv.Count - 1; i++)
            {
                DataRow dr = dv.Table.Rows[i];

                string TestCaseID1 = dr["测试用例ID"].ToString();
                string Column1 = dr["所属列"].ToString();
                string StepNo1 = dr["测试过程序号"].ToString();
                string AnnexName = dr["附件名称"].ToString();

                if ((TestCaseID1 == TestCaseID) &&( Column1 == Column) && (StepNo1==StepNo))
                {
                    AnnexInfo = "\r" + "附件见：" + AnnexName;
                    return AnnexInfo;

                }
           }

            return AnnexInfo;

        }

        public void PutoutAnnex(Document doc, int type1, DocumentBuilder db, string TestCaseID)
        {

            ArrayList TestCaseList = new ArrayList();
            ArrayList OutputAnnex = new ArrayList();
            DataView dv = null;
            string tempfilename ="";
            bool flag = true;

            string AnnexMuLuName = TPM3.Sys.GlobalData.GetAttachDirName();
                                          
            if (type1 == 1)
            {
                dv = TPM3.wx.AttachCaseTrace.GetAttachCaseTraceView(2);              
                AnnexMuLuName = AnnexMuLuName + "\\测试说明中可打印附件";
             
            }
            else if (type1 == 2)
            {
                dv = TPM3.wx.AttachCaseTrace.GetAttachCaseTraceView(3);              
                AnnexMuLuName = AnnexMuLuName + "\\测试记录实测结果中可打印附件";
            }

            if (Directory.Exists(AnnexMuLuName) == true)
            {
                string[] filelist = Directory.GetFiles(AnnexMuLuName);

                for (int k = 0; k <= filelist.Length - 1; k++)
                {
                    string Filename = filelist[k];
                    File.Delete(Filename);
                }
            }
            else
            {
                Directory.CreateDirectory(AnnexMuLuName);
            }

          
            for (int i = 0; i <= dv.Count - 1; i++)
            {
                DataRow dr = dv.Table.Rows[i];

                string TestCaseID1 = dr["测试用例ID"].ToString();

                if (TestCaseID1 == TestCaseID)
                {
                    string AnnexID = dr["附件ID"].ToString();
                    string AnnexName = dr["附件名称"].ToString();
                    string IfOut = dr["是否输出"].ToString();

                    if (IfOut != "False")
                    {
                        for (int j = 0; j <= OutputAnnex.Count - 1;j++ )
                        {
                            if (OutputAnnex[j].ToString() == AnnexName)
                            {
                                flag = false;
                            }

                        }
                        if (flag == true)
                        {
                            db.Writeln("附件： " + AnnexName);

                            tempfilename = Path.Combine(AnnexMuLuName, "temp$" + AnnexName);

                            // modified by zhouxindong
                            //var temp_annex_name = FileTypeTxtToDoc(AnnexName);
                            //tempfilename = Path.Combine(AnnexMuLuName, "temp$" + temp_annex_name);

                            if (File.Exists(tempfilename) == true)
                            {
                                File.Delete(tempfilename);
                            }

                            var fs = new FileStream(tempfilename, FileMode.CreateNew);

                            string sqlstate = "select 附件内容,附件类型 from DC附件实体表 where 附件名称=?";
                            DataTable dt1 = TPM3.Sys.GlobalData.globalData.dbProject.ExecuteDataTable(sqlstate, AnnexName);
                            string AnnexType = "";
                            if (dt1 != null && dt1.Rows.Count != 0)
                            {
                                System.Windows.Forms.Application.DoEvents();

                                DataRow dr1 = dt1.Rows[0];
                                AnnexType = dr1["附件类型"].ToString();
                                // modified by zhouxindong
                                //if (AnnexType == ".txt")
                                //    AnnexType = ".doc";

                                byte[] buf = dr1["附件内容"] as byte[];
                                if (buf != null)
                                    fs.Write(buf, 0, buf.Length);

                                fs.Flush();
                                fs.Close();

                                BreakType breakType = BreakType.PageBreak;

                                if ((AnnexType == ".doc") || (AnnexType == ".docx"))
                                {
                                    OutputComm.OutputComm.AddField("附件内容", db);//=======================
                                    Document doc1 = new Document(tempfilename);

                                    Node Node1 = OutputComm.OutputComm.GetNodeByField(doc, "附件内容", null);

                                    AsposeCommon.InsertDocument(Node1, doc1);
                                    System.Windows.Forms.Application.DoEvents();

                                    db.MoveToDocumentEnd();
                                    db.InsertBreak(breakType);

                                }
                                else if ((AnnexType == ".txt") || (AnnexType == "")) //modified by zhouxindong
                                {
                                    OutputComm.OutputComm.AddField("附件内容", db);//=======================
                                    var utf8_file = ConvertToUtf8(tempfilename);
                                    var new_doc = ConverTxtToDoc(utf8_file);

                                    Document doc1 = new Document(new_doc);

                                    Node Node1 = OutputComm.OutputComm.GetNodeByField(doc, "附件内容", null);

                                    AsposeCommon.InsertDocument(Node1, doc1);
                                    System.Windows.Forms.Application.DoEvents();

                                    db.MoveToDocumentEnd();
                                    db.InsertBreak(breakType);
                                    File.Delete(utf8_file);
                                    File.Delete(new_doc);
                                }
                               
                                else if ((AnnexType == ".jpg") || (AnnexType == ".bmp"))
                                {
                                    db.InsertImage(buf);
                                    db.InsertBreak(breakType);
                                }


                            }
                            OutputAnnex.Add(AnnexName);

                        }
                        }
                   
                       if (File.Exists(tempfilename) == true)
                       {
                             File.Delete(tempfilename);
                        }
                         
                          System.Windows.Forms.Application.DoEvents();

                            }
                        }
                      
                    }

        private string FileTypeTxtToDoc(string filename, string suffix = ".doc")
        {
            if (string.IsNullOrEmpty(filename))
                return filename;

            var dot_index = filename.LastIndexOf('.');
            if (dot_index == -1)
                return filename;

            var file_ext = filename.Substring(dot_index + 1, 3);
            if (file_ext == "txt") return filename.Substring(0, dot_index) + suffix;
            return filename;
        }

        private string ConverTxtToDoc(string txtfile)
        {
            var reader = new StreamReader(txtfile, Encoding.UTF8);
            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);
            foreach (var line in reader.Lines())
            {
                builder.Writeln(line);
            }

            var random = Guid.NewGuid().ToString() + ".doc";
            doc.Save(random);
            reader.Close();
            return random;
        }

        public static Encoding GetTxtFileEncodeType(string filename)
        {
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(fs);
            var buffer = br.ReadBytes(2);
            br.Close();
            fs.Close();
            if (buffer[0] >= 0xEF)
            {
                if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                    return Encoding.UTF8;
                if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                    return Encoding.BigEndianUnicode;
                if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                    return Encoding.Unicode;
                return Encoding.Default;
            }
            return Encoding.Default;
        }

        public static string ConvertToUtf8(string filename)
        {
            var encode_type = GetTxtFileEncodeType(filename);
            if (Equals(encode_type, Encoding.UTF8) ||
                Equals(encode_type, Encoding.Unicode) ||
                Equals(encode_type, Encoding.BigEndianUnicode))
                return filename;

            var temp_file = Guid.NewGuid() + ".txt";
            var sr = new StreamReader(filename, Encoding.Default);
            var str = sr.ReadToEnd();
            sr.Close();
            var sw = new StreamWriter(temp_file, false, Encoding.UTF8);
            sw.Write(str);
            sw.Close();
            return temp_file;
        }

        public void OutputChapter_HaveChangelessSonChapter(string BSField, string TextStartField, Document doc)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        //每个被测对象下跟固定标题
        {
            int TestObjectNum = 0;
            int StartTableNo = 0;
            string ResetCaption = "";

            Scattered.TestVerID = TestVerID;

            IfTiSheng = ResetIfTiSheng();

            Document doc1 = null;
            DocumentBuilder db1 = null;

            DocumentBuilder db = new DocumentBuilder(doc);
            Section Section = OutputComm.OutputComm.GetNodeInSection(BSField, doc, db);
            Section TestStatSection = OutputComm.OutputComm.GetNodeInSection("测试设计情况统计表", CurrentDoc, db);

            DataTable dt = Scattered.GetObjectName();

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {

                TestObjectNum = TestObjectNum + 1;

                doc1 = new Document();
                db1 = new DocumentBuilder(doc1);

                DataRow dr = dt.Rows[i];
                string ObjectName = dr["被测对象名称"].ToString();
                string ObjectID = dr["被测对象实测ID"].ToString();
                string ObjectID_body = dr["被测对象实体ID"].ToString();

                if (TestStatSection != null)
                {

                    db.MoveToMergeField(TextStartField);

                    OutputComm.OutputComm.ChangelessSonChapterProcess_1(db, doc, IfTiSheng, ObjectName);

                    if (i < dt.Rows.Count - 1)
                    {
                        db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                        OutputComm.OutputComm.AddField(TextStartField, db);
                    }

                    doc1 = new Document();
                    db1 = new DocumentBuilder(doc1);
                    OutputComm.OutputComm.ImportSec(doc1, TestStatSection);

                    DataTable dt1 = null;
                    dt1 = Scattered.GetStatTable(ObjectID_body);
                    dt1.TableName = "测试设计情况_总结";

                    FillTable(doc1, dt1, false, "");

                    StartTableNo = StartTableNo + 1;
                    OutputComm.OutputComm.ReplaceTableName("与被测对象有关", StartTableNo, ObjectName, db1);
                    OutputComm.OutputComm.ChangeChapterEndProcess("准备替换1", doc1, CurrentDoc, null, db);

                    doc1 = new Document();
                    db1 = new DocumentBuilder(doc1);
                    OutputComm.OutputComm.ImportSec(doc1, Section);

                    if (TextStartField == "可变章节_测试总结")
                    {
                        ReplaceEveryItemInEachTestSumUp(db1, doc1, ObjectID, ObjectID_body, ObjectName, ref StartTableNo, "一般");
                    }

                    OutputComm.OutputComm.ChangeChapterEndProcess("准备替换2", doc1, doc, null, db);

                }
                else
                {
                    db.MoveToMergeField(TextStartField);

                    OutputComm.OutputComm.ChangelessSonChapterProcess_2(db, doc, IfTiSheng, ObjectName);

                    if (i < dt.Rows.Count - 1)
                    {
                        db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                        OutputComm.OutputComm.AddField(TextStartField, db);
                    }

                    doc1 = new Document();
                    db1 = new DocumentBuilder(doc1);
                    OutputComm.OutputComm.ImportSec(doc1, Section);

                    if (TextStartField == "可变章节_测试准备")
                    {
                        string[] PrepareItem = new string[4] { "测试进度", "软件准备", "硬件准备", "其他测试准备" };
                        ReplaceSingleChangelessChapter(doc1, db1, ObjectID, "", PrepareItem, false);
                        ResetCaption = "调整标题";

                    }
                    else if (TextStartField == "可变章节_测试总结")
                    {
                        ReplaceEveryItemInEachTestSumUp(db1, doc1, ObjectID, ObjectID_body, ObjectName, ref StartTableNo, "一般");
                        ResetCaption = "调整标题";
                    }
                    else if (TextStartField == "可变章节_质量评估")
                    {
                        string[] EvaluateItem = new string[2] { "质量评估", "改进建议" };
                        ReplaceSingleChangelessChapter(doc1, db1, ObjectID, "", EvaluateItem, false);
                        ResetCaption = "调整标题1";
                    }

                    OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, doc, null, db);

                }
            }
            Section.Remove();
            if (TestStatSection != null)
            {
                TestStatSection.Remove();
            }

            OutputComm.OutputComm.ReSetStyles(doc, ResetCaption, "标题 2", db, IfTiSheng);

        }

        public void OutputChapter_HGSum(string BSField, string TextStartField, Document doc, ArrayList TestVerList, string TypeStr)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {

            int StartTableNo = 0;

            Scattered.TestVerID = TestVerID;

            IfTiSheng = ResetIfTiSheng();

            DocumentBuilder db = new DocumentBuilder(doc);
            Section Section = OutputComm.OutputComm.GetNodeInSection(BSField, doc, db);
                     
            for (int j = 0; j <= TestVerList.Count - 1; j++)
            {
                TestVerID = TestVerList[j].ToString();
                string TestVerStr = GetCurrentVerStr_2(TestVerID);

                Scattered.TestVerID = TestVerID;
                DataTable dt = Scattered.GetObjectName();

                NodeTree nodetree = new NodeTree();
                nodetree.TestVerID = TestVerID;
                DataTreeList = nodetree.PutNodeToLayerList();

                db.MoveToMergeField(TextStartField);

                if (TypeStr == "一般")
                {
                    db.ParagraphFormat.Style = doc.Styles["标题 2"];
                }
                else if (TypeStr == "定型")
                {
                    db.ParagraphFormat.Style = doc.Styles["标题 3"];
                }
                db.Writeln(TestVerStr);
                
                string Result1 = "";
                string sqlstate = "SELECT 内容类型,文本内容 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=? AND 测试版本=?";
                DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, "被测软件版本", TestVerID);
                if (dt1 != null && dt1.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        Result1 = dr["文本内容"].ToString();
                        if (Result1 != "")
                        {
                            db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                            db.Writeln("   本次测试的被测对象版本为：" + Result1);
                        }
                        else
                        {
                            db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                            db.Writeln("   本次测试的被测对象版本为：");

                        }
                    }
                }

                OutputComm.OutputComm.AddField(TextStartField, db);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string ObjectName = dr["被测对象名称"].ToString();
                    string ObjectID = dr["被测对象实测ID"].ToString();
                    string ObjectID_body = dr["被测对象实体ID"].ToString();

                    db.MoveToMergeField(TextStartField);

                    if (TypeStr == "一般")
                    {
                        OutputComm.OutputComm.ChangelessSonChapterProcess_3_HG(db, doc, IfTiSheng, ObjectName);
                    }
                    else if (TypeStr == "定型")
                    {
                        OutputComm.OutputComm.AddField("准备替换", db);
                        db.Writeln("");
                    }
                                                        
                    if (i < dt.Rows.Count - 1)
                    {
                        db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                        OutputComm.OutputComm.AddField(TextStartField, db);
                    }

                    Document doc1 = new Document();
                    DocumentBuilder db1 = new DocumentBuilder(doc1);
                    OutputComm.OutputComm.ImportSec(doc1, Section);
                  
                    if (TextStartField == "可变章节_回归测试总结")
                    {
                        ReplaceEveryItemInEachTestSumUp(db1, doc1, ObjectID, ObjectID_body, ObjectName, ref StartTableNo, TypeStr);                                           
                    }
                    OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, doc, null, db);
                  
                }
            }

            tableNum = StartTableNo;
            Section.Remove();

            if (TypeStr == "一般")
            {
                OutputComm.OutputComm.ReSetStyles(doc, "调整标题", "标题 3", db, IfTiSheng);
            }

        }

        public void OutputChapter_ProSum(string BSField, string TextStartField, Document doc, ArrayList TestVerList)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {

            Scattered.TestVerID = TestVerID;
     
            DocumentBuilder db = new DocumentBuilder(doc);
            Section Section = OutputComm.OutputComm.GetNodeInSection(BSField, doc, db);

            for (int j = 0; j <= TestVerList.Count - 1; j++)
            {
                TestVerID = TestVerList[j].ToString();
                string TestVerStr = GetCurrentVerStr_2(TestVerID);

                Scattered.TestVerID = TestVerID;
                DataTable dt = Scattered.GetObjectName();

                NodeTree nodetree = new NodeTree();
                nodetree.TestVerID = TestVerID;
                DataTreeList = nodetree.PutNodeToLayerList();

                db.MoveToMergeField(TextStartField);
                db.ParagraphFormat.Style = doc.Styles["标题 3"];
                db.Writeln(TestVerStr);

                db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                OutputComm.OutputComm.AddField(TextStartField, db);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string ObjectName = dr["被测对象名称"].ToString();
                    string ObjectID = dr["被测对象实测ID"].ToString();
                    string ObjectID_body = dr["被测对象实体ID"].ToString();

                    db.MoveToMergeField(TextStartField);

                    db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                    OutputComm.OutputComm.AddField("准备替换", db);
                    db.Writeln("");
                    
                    //if (i < dt.Rows.Count - 1)
                    //{
                    //    db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                    //    OutputComm.OutputComm.AddField(TextStartField, db);
                    //}
               
                    Document doc1 = new Document();
                    DocumentBuilder db1 = new DocumentBuilder(doc1);
                    OutputComm.OutputComm.ImportSec(doc1, Section);

                    if (TextStartField == "可变章节_问题总结")
                    {
                       //QuestionSumUp(db1, doc1, ObjectID, ObjectID_body, ObjectName, ref StartTableNo, TestVerID);
                        QuestionSumUp(db1, doc1, ObjectID, ObjectID_body, ObjectName, ref tableNum, TestVerID);
                    }
                    OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, doc, null, db);

                }
            }

            Section.Remove();
            OutputComm.OutputComm.ReSetStyles(doc, "调整表名格式", "表名", db, IfTiSheng);

        }
        public void OutputChapter_TestExecuteInfo(string TextStartField, Document doc, ArrayList TestVerList)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {

            Scattered.TestVerID = TestVerID;

            DocumentBuilder db = new DocumentBuilder(doc);
          
            for (int j = 0; j <= TestVerList.Count - 1; j++)
            {
                TestVerID = TestVerList[j].ToString();
                string TestVerStr = GetCurrentVerStr_2(TestVerID);

                Scattered.TestVerID = TestVerID;
                DataTable dt = Scattered.GetObjectName();

                NodeTree nodetree = new NodeTree();
                nodetree.TestVerID = TestVerID;
                DataTreeList = nodetree.PutNodeToLayerList();

                db.MoveToMergeField(TextStartField);

                db.ParagraphFormat.Style = doc.Styles["标题 4"];
                db.Write(" ");
                db.Writeln(TestVerStr);

                string Result1 = "";
                object ValueObject = null;

                string sqlstate = "SELECT 内容类型,文本内容 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=? AND 测试版本=?";
                DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, "可变章节_测试执行情况说明", TestVerID);
                if (dt1 != null && dt1.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        Result1 = dr["内容类型"].ToString();

                        if (Result1 == "文本")
                        {
                            string Result = dr["文本内容"].ToString();
                            db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                            db.Write("      ");
                            db.Writeln(Result);
                        }
                        else if (Result1 == "对象")
                        {
                            sqlstate = "SELECT 文档内容 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=? AND 测试版本=?";
                            dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, "可变章节_测试执行情况说明", TestVerID);
                            ValueObject = OutputComm.OutputComm.OLEObjectProcess(dt1, CurrentDoc, "可变章节_测试执行情况说明");
                            if (ValueObject != null)
                            {
                                if (ValueObject.ToString() == "无。")
                                {
                                    db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                                    db.Writeln("");
                                }
                                else
                                {
                                    db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                                    db.Write("      ");
                                    db.Writeln(ValueObject.ToString());

                                }
                            }
                        }

                    }
                }
                
                OutputComm.OutputComm.AddField(TextStartField, db);
            }        
        }

        public void ReplaceSingleChangelessChapter(Document doc, DocumentBuilder db, string FilterID, string TestObjectID_body, string[] Items, bool IfIsTime)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {
            if (IfIsTime == false)
            {
                foreach (string Item in Items)
                {
                    Node node = OutputComm.OutputComm.GetNodeByField(doc, Item, db);

                    if (node != null)
                    {
                        string sqlstate = "select " + Item + " from CA被测对象实测表 where ID=?";

                        DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, FilterID);

                        object Value = OutputComm.OutputComm.OLEObjectProcess_1(dt1, doc, node);

                        if (Value != null)
                        {
                            db.Write(Value.ToString());
                        }

                    }

                }
            }
            else if (IfIsTime == true)
            {
                string ValueStr = "";

                TestResultSummary summary = new TestResultSummary(ProjectID, TestVerID);
                summary.OnCreate();

                ResultSummaryVisitClass vc = new ResultSummaryVisitClass();
                summary[TestObjectID_body].DoVisit(vc.GetTestTime);

                if (db.MoveToMergeField("测试开始日期"))
                {
                    ValueStr = "";
                    if (vc.testBeginTime != null)
                    {
                        ValueStr = vc.testBeginTime.Value.Date.Year + "-" + vc.testBeginTime.Value.Date.Month + "-" + vc.testBeginTime.Value.Date.Day;
                    }

                    db.Write(ValueStr);
                }
                if (db.MoveToMergeField("测试结束日期"))
                {
                    ValueStr = "";
                    if (vc.testEndTime != null)
                    {
                        ValueStr = vc.testEndTime.Value.Date.Year + "-" + vc.testEndTime.Value.Date.Month + "-" + vc.testEndTime.Value.Date.Day;
                    }

                    db.Write(ValueStr);
                }

            }

        }

        public ArrayList GetTestVerIDList()
        {

            ArrayList TestVerList = new ArrayList();

            string sqlstate = "SELECT SYS测试版本表.ID, SYS测试版本表.项目ID, SYS测试版本表.序号 FROM SYS测试版本表 WHERE SYS测试版本表.项目ID=? ORDER BY SYS测试版本表.序号;";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string TestVerID = dr["ID"].ToString();
                    TestVerList.Add(TestVerID);

                }
            }

            return TestVerList;

        }

        public string GetCurrentVerStr(string TestVerID)
        {

            string CurrentVerStr = "";

            string sqlstate = "select * from SYS测试版本表 where ID=? and 项目ID=? order by 序号";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestVerID, ProjectID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    object PerVer = dr["前向版本ID"];
                    if (PerVer == null)
                    {
                        CurrentVerStr = "首次测试";
                    }
                    else
                    {
                        if (PerVer.ToString() != "")
                        {
                            int xuhao = int.Parse(dr["序号"].ToString()) - 1;
                            CurrentVerStr = "第" + xuhao.ToString() + "次回归测试";
                        }
                        else
                        {
                            CurrentVerStr = "首次测试";
                        }

                    }
                }
            }


            return CurrentVerStr;

        }
        public string GetCurrentVerStr_2(string TestVerID)
        {

            string CurrentVerStr = "";
            string sqlstate = "SELECT 文本内容 FROM SYS文档内容表 WHERE 内容标题=? and 测试版本=? and 项目ID=? ";

                DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, "版本名称", TestVerID, GlobalData.globalData.projectID.ToString());

                if (dt != null && dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CurrentVerStr = dr["文本内容"].ToString();
                    }
                }


            return CurrentVerStr;

        }
        public bool IfFirstTest()
        {
            bool Value = false;
            string sqlstate = "SELECT SYS测试版本表.ID, SYS测试版本表.序号, SYS测试版本表.前向版本ID, SYS测试版本表.项目ID FROM SYS测试版本表 " +
                              " WHERE (((SYS测试版本表.序号)=1) AND ((SYS测试版本表.前向版本ID) Is Null) AND ((SYS测试版本表.项目ID)=?));";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID);

            if (dt != null && dt.Rows.Count != 0)
            {
                DataRow dr = dt.Rows[0];
                string FirstVerID = dr[0].ToString();
                if (TestVerID == FirstVerID)
                {
                    Value = true;
                }

            }

            return Value;

        }

        public void ReplaceEveryItemInEachTestSumUp(DocumentBuilder db, Document doc, string TestObjectID, string TestObjectID_body, string ObjectName, ref int StartTableNo,string TypeStr)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {
            bool IfHaveRecord;
            Table table = null;

            ReplaceSingleChangelessChapter(doc, db, TestObjectID, TestObjectID_body, null, true);

            IfHaveRecord = ReplaceTestMen(TestObjectID, doc);
            if (IfHaveRecord == true)
            {
                StartTableNo = StartTableNo + 1;
                OutputComm.OutputComm.ReplaceTableName("与被测对象有关", StartTableNo, ObjectName, db);
            }
            OutputComm.OutputComm.TableIfHaveRecordProcess(IfHaveRecord, db, doc, 1, "参加测试人员");

            //table = OutputComm.OutputComm.GetNodeInTable("依据与测试项关系序号", doc, null);
            //if (table != null)
            //{
            //    DataTable dt = null;
            //    if (IfFirstTest() == true)
            //    {
            //        dt = Scattered.GetTestAccordingTable("测试依据与测试项的追踪关系", DataTreeList, "", "");
            //    }
            //    else
            //    {
            //        dt = Scattered.HGTestAccording("回归测试依据与测试项的追踪关系", DataTreeList, "");
            //    }
            //    dt.TableName = "测试依据与测试项的追踪关系";
            //    FillTable(doc, dt, false, "测试依据与测试项的追踪关系");

            //    MergeTable(table, "测试依据与测试项的追踪关系", dt, doc);
            //    if (dt.Rows.Count > 0)
            //    {
            //        StartTableNo = StartTableNo + 1;
            //        OutputComm.OutputComm.ReplaceTableName("与被测对象有关", StartTableNo, ObjectName, db);
            //    }
            //}

            //OutputComm.OutputComm.TableIfHaveRecordProcess(IfHaveRecord, db, doc, 1, "测试依据与测试项的追踪关系");

            table = OutputComm.OutputComm.GetNodeInTable("测试包含关系表", doc, null);
            IfHaveRecord = TestBhRelation(TestObjectID, doc, table);
            if (IfHaveRecord == true)
            {
                StartTableNo = StartTableNo + 1;
                OutputComm.OutputComm.ReplaceTableName("与被测对象有关", StartTableNo, ObjectName, db);
            }
            else
            {
                OutputComm.OutputComm.TableIfHaveRecordProcess(IfHaveRecord, db, doc, 1, "测试包含关系");
            }
      
            if (TypeStr == "定型")
            {
                ReplaceTestCaseNum_DX(db, TestObjectID);
            }
            else if (TypeStr == "一般")
            {
                ReplaceTestCaseNum(db, TestObjectID);
            }

            StartTableNo = StartTableNo + 1;
            OutputComm.OutputComm.ReplaceTableName("与被测对象有关", StartTableNo, ObjectName, db);

            if (TypeStr == "一般")
            {
                StartTableNo = StartTableNo + 1;
                ReplaceQuestionClassNum(TestObjectID, doc, db);
                OutputComm.OutputComm.ReplaceTableName("与被测对象有关", StartTableNo, ObjectName, db);
               
            }

            string[] TableItem = new string[2] { "执行结果统计", "未完整执行统计" };
            foreach (string TableName in TableItem)
            {
                IfHaveRecord = TestResultStat(TableName, db, doc, TestObjectID);
                
                if (IfHaveRecord == true)
                {
                    StartTableNo = StartTableNo + 1;
                    OutputComm.OutputComm.ReplaceTableName("与被测对象有关", StartTableNo, ObjectName, db);                   
                }
                else
                {
                    OutputComm.OutputComm.TableIfHaveRecordProcess(IfHaveRecord, db, doc, 1, TableName + "表");
                }
               
            }
            
            table = OutputComm.OutputComm.GetNodeInTable("回归测试依据与测试用例的追踪关系序号", doc, null);
            if (table != null)
            {
                DataTable dt = Scattered.HGTestCase_Xuqiu_According(DataTreeList, DocumentName, TestVerID);

                if (dt.Rows.Count > 0)
                {
                    dt.TableName = "回归测试依据与测试用例的追踪关系";
                    FillTable(doc, dt, false, "回归测试依据与测试用例的追踪关系");

                    MergeTable(table, "回归测试依据与测试用例的追踪关系", dt, doc);

                    StartTableNo = StartTableNo + 1;
                    OutputComm.OutputComm.ReplaceTableName("与被测对象有关", StartTableNo, ObjectName, db);

                }
                else
                {
                    OutputComm.OutputComm.TableIfHaveRecordProcess(false, db, doc, 1, "回归测试依据与测试用例的追踪关系");
                }

            }

            table = OutputComm.OutputComm.GetNodeInTable("软件更动与测试用例的追踪关系序号", doc, null);
            if (table != null)
            {
                DataTable dt = Scattered.HGTestCase_GD_According(DataTreeList, TestVerID);
                if (dt.Rows.Count > 0)
                {
                    dt.TableName = "软件更动与测试用例的追踪关系";
                    FillTable(doc, dt, false, "软件更动与测试用例的追踪关系");

                    MergeTable(table, "软件更动与测试用例的追踪关系", dt, doc);

                    StartTableNo = StartTableNo + 1;
                    OutputComm.OutputComm.ReplaceTableName("与被测对象有关", StartTableNo, ObjectName, db);

                }
                else
                {
                    OutputComm.OutputComm.TableIfHaveRecordProcess(false, db, doc, 1, "软件更动与测试用例的追踪关系");
                }

            }

            string[] ReplaceItems = new string[1] { "测试执行情况_补充" };
            ReplaceSingleChangelessChapter(doc, db, TestObjectID, "", ReplaceItems, false);

        }
        public void QuestionStat(Document doc, string testverID)
        {

            string sqlstate = "select ID from DC问题级别表 where 项目ID=? and 类型=? ";
            DataTable dt0 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, "级别");

            DataTable dt = FlexTreeClass2.GetFallSummary(1, FallTraceType.FallLevel, testverID);

            DataTable dtnew = Scattered.AddTableColumn("测试类型的问题统计", "");

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string TestType = dr["NodeType"].ToString();
                    if (TestType == "3")
                    {
                        DataRow dr1 = dtnew.Rows.Add();

                        dr1["测试类型"] = dr["名称"].ToString();
                        string column1 = dt0.Rows[0]["ID"].ToString();
                        dr1["1级问题数"] = dr[column1].ToString();
                        string column2 = dt0.Rows[1]["ID"].ToString();
                        dr1["2级问题数"] = dr[column2].ToString();
                        string column3 = dt0.Rows[2]["ID"].ToString();
                        dr1["3级问题数"] = dr[column3].ToString();
                        string column4 = dt0.Rows[3]["ID"].ToString();
                        dr1["4级问题数"] = dr[column4].ToString();
                        string column5 = dt0.Rows[4]["ID"].ToString();
                        dr1["5级问题数"] = dr[column5].ToString();
                    }
                }               
            }

            dtnew.TableName = "测试类型的问题统计";
            FillTable(doc, dtnew, false, "");

        }

        public int GetQuestionNum()
        {
            int num = 0;

            string sqlstate = "select * from CA问题报告单 where 项目ID=? and 测试版本=? ";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, TestVerID);

            num = dt.Rows.Count;

            return num;

        }
        public string GetNextVerID(string testverID)
        {
            string NextVerID = "";

            string sqlstate = "select ID from SYS测试版本表 where 项目ID=? and 前向版本ID=? ";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, TestVerID);

            if (dt != null && dt.Rows.Count != 0)
            {
                NextVerID = dt.Rows[0][0].ToString();
            }


            return NextVerID;
        }

        public void QuestionSumUp(DocumentBuilder db, Document doc, string TestObjectID, string TestObjectID_body, string ObjectName, ref int StartTableNo, string testverID)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {
           
            ReplaceSingleChangelessChapter(doc, db, TestObjectID, TestObjectID_body, null, true);
             
            StartTableNo = StartTableNo + 1;
            ReplaceQuestionClassNum(TestObjectID, doc, db);   
            OutputComm.OutputComm.ReplaceTableName("与被测对象有关", StartTableNo, ObjectName, db);
           
            StartTableNo = StartTableNo + 1;
            QuestionStat(doc, testverID);
            OutputComm.OutputComm.ReplaceTableName("与被测对象有关", StartTableNo, ObjectName, db);

            StartTableNo = StartTableNo + 1;

            string NextVerID = GetNextVerID(testverID);
            DataTable dt0 = Scattered.GD_ChangeError_DX(NextVerID);
            dt0.TableName = "纠错性更动_定型";
            FillTable(doc, dt0, false, "纠错性更动_定型");
        
            OutputComm.OutputComm.ReplaceTableName("与被测对象有关", StartTableNo, ObjectName, db);

            db.MoveToMergeField("问题个数");
            db.Write(GetQuestionNum().ToString());

            db.MoveToMergeField("问题个数");
            db.Write(GetQuestionNum().ToString());

            db.MoveToMergeField("问题解决情况说明");
            
            string sqlstate = "SELECT 文档内容 FROM SYS文档内容表 WHERE 项目ID=? AND 内容标题=? AND 测试版本=?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, "问题解决情况说明", TestVerID);
            object ValueObject = OutputComm.OutputComm.OLEObjectProcess(dt, CurrentDoc, "问题解决情况说明");
            if (ValueObject != null)
            {
                if (ValueObject.ToString() == "无。")
                {
                    db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                    db.Writeln("");
                }
                else
                {
                    db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                    db.Writeln(ValueObject.ToString());

                }
            }

            string projectname = "";
            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("SELECT SYS文档内容表.文本内容 FROM SYS文档内容表 WHERE SYS文档内容表.内容标题='项目名称' AND SYS文档内容表.文档名称='项目信息' and SYS文档内容表.项目ID=" + "'" + ProjectID + "'");
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    projectname = dr[0].ToString();
                }
            }

            //db.MoveToMergeField("问题报告名称");
            //db.Write(projectname + "•软件问题报告");

        }

        public void ReplaceTestCaseNum(DocumentBuilder db, string TestObjectID)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {
            Scattered.TestVerID = TestVerID;
            ArrayList TestCaseNumList = Scattered.GetTestCaseNum_AccordingType(TestObjectID, DataTreeList,"一般");

            string[] ReplaceItems = new string[8] { "用例总数", "执行用例数", "完全执行通过", "完全执行未通过", "部分执行用例数", "部分执行通过", "部分执行未通过", "未执行用例数" };

            OutputComm.OutputComm.ReplaceTRStatNum(TestCaseNumList, ReplaceItems, db);

        }
        public void ReplaceTestCaseNum_DX(DocumentBuilder db, string TestObjectID)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {
            Scattered.TestVerID = TestVerID;
            ArrayList TestCaseNumList = Scattered.GetTestCaseNum_AccordingType(TestObjectID, DataTreeList, "定型");

            string[] ReplaceItems = new string[9] {"测试项总数", "用例总数", "执行用例数", "完全执行通过", "完全执行未通过", "部分执行用例数", "部分执行通过", "部分执行未通过", "未执行用例数" };

            OutputComm.OutputComm.ReplaceTRStatNum(TestCaseNumList, ReplaceItems, db);

        }

        public void ReplaceQuestionClassNum(string TestObjectID, Document doc, DocumentBuilder db)//%%%%%%%%%%%%%%%%%%%%%%%%%
        {
            Scattered.TestVerID = TestVerID;
            ArrayList LevelList = Scattered.QuestionLevelList("级别");
            ArrayList ClassList = Scattered.QuestionLevelList("类别");

            for (int i = 1; i <= ClassList.Count; i++)
            {
                db.MoveToMergeField("问题类别" + i.ToString());
                db.Write(ClassList[i - 1].ToString());

            }
            for (int j = 1; j <= LevelList.Count; j++)
            {
                db.MoveToMergeField("问题级别" + j.ToString());
                db.Write(LevelList[j - 1].ToString());

            }
            ArrayList QuestionList = Scattered.GetArrayListInfo(TestObjectID, "问题统计", DataTreeList);
            QuestionList = Scattered.ChangeList(QuestionList);

            for (int k = 1; k <= ClassList.Count; k++)
            {
                for (int kk = 1; kk <= LevelList.Count; kk++)
                {
                    int num = Scattered.GetQuestionNumAccordClassLevel(QuestionList, ClassList[k - 1].ToString(), LevelList[kk - 1].ToString());
                    db.MoveToMergeField("个数" + k.ToString() + "-" + kk.ToString());
                    db.Write(num.ToString());

                }
            }

            OutputComm.OutputComm.QuestionSupplyProcess(doc, db, "问题类别", "问题级别", 5);

        }

        public string GetCurrentVerStr_3(string TestVerID)
        {
            string CurrentVerStr = "";

            string sqlstate = "select * from SYS测试版本表 where ID=? and 项目ID=? order by 序号";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestVerID, ProjectID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    object PerVer = dr["前向版本ID"];
                    if (PerVer == null)
                    {
                        CurrentVerStr = "";
                    }
                    else
                    {
                        if (PerVer.ToString() != "")
                        {
                            int xuhao = int.Parse(dr["序号"].ToString()) - 1;
                            CurrentVerStr = "(R" + xuhao.ToString() + ")";
                        }
                        else
                        {
                            CurrentVerStr = "";
                        }

                    }
                }
            }

            return CurrentVerStr;

        }

        public bool TestBhRelation(string TestObjectID, Document doc, Table table)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {
            string save = "";
            int num = 0;
            int p = 0;
            string values1 = "";
            string values2 = "";

            DataView dv = ItemCaseTrace.GetItemCaseTraceView(ProjectID, TestVerID, null, TestObjectID, 1, 2);

            DataTable dt = new DataTable();
            dt = Scattered.AddTableColumn("测试包含关系", "");

            for (int i = 0; i <= dv.Table.Rows.Count - 1; i++)
            {
                DataRow dr = dv.Table.Rows[i];

                string ifitem = dr["imagekey"].ToString();
                if (ifitem == "item")//适用于二级目录的情况
                {
                    continue;
                }

                string TestType = dr[4].ToString() + "\n" + dr[5].ToString();
                
                string tempstr = dr[8].ToString();
             
                
                    string TestInfo = GetCurrentVerStr_3(TestVerID);                 
                    if (TestInfo != "")
                    {
                        p = tempstr.IndexOf("_");
                        values1 = tempstr.Substring(p + 1, tempstr.Length - p - 1);
                        values2 = tempstr.Substring(0, p);
                        tempstr = values2 + TestInfo + "_" + values1;
                    }

                
                string TestItem = dr[7].ToString() + "\n" + tempstr;

                string testcasestr = dr[12].ToString();
                if (testcasestr != "")
                {
                    p = testcasestr.LastIndexOf("_");
                    values1 = testcasestr.Substring(p + 1, testcasestr.Length - p - 1);
                    values2 = testcasestr.Substring(0, p);

                    if (int.Parse(values1) < 10)
                    {
                        values1 = "_0" + values1;
                        testcasestr = values2 + values1;
                    }

                    TestInfo = GetCurrentVerStr_3(TestVerID);
                    if (TestInfo != "")
                    {
                        p = testcasestr.IndexOf("_");
                        values1 = testcasestr.Substring(p + 1, testcasestr.Length - p - 1);
                        values2 = testcasestr.Substring(0, p);
                        testcasestr = values2 + TestInfo + "_" + values1;
                    }



                    string TestCase = dr[11].ToString() + "\n" + testcasestr;


                    if (dr[13].ToString() != "")
                    {
                        string tempstr0 = dr[13].ToString();
                        int p1 = tempstr0.LastIndexOf("_");
                        string values11 = tempstr0.Substring(p1 + 1, tempstr0.Length - p1 - 1);
                        string values22 = tempstr0.Substring(0, p1);
                        if (int.Parse(values11) < 10)
                        {
                            values11 = "_0" + values11;
                            tempstr0 = values22 + values11;
                        }

                        TestCase = TestCase + "\n" + "引用用例标识为" + tempstr0;
                    }



                    DataRow dr1 = dt.Rows.Add();
                    if (dr[4].ToString() != save)
                    {
                        num = num + 1;
                    }
                    dr1["序号"] = num;
                    dr1["测试类"] = TestType;
                    dr1["测试项"] = TestItem;
                    dr1["测试用例"] = TestCase;

                    save = dr[4].ToString();
                }

            }

            dt.TableName = "测试包含关系";
            FillTable(doc, dt, false, "测试包含关系");
            if (dt.Rows.Count > 0)
            {
                // Table table = OutputComm.OutputComm.GetNodeInTable("测试包含关系表", doc, null);
                MergeTable(table, "测试包含关系", dt, doc);

            }

            if (dt.Rows.Count > 0) return true;
            else return false;
        }


        public bool ReplaceTestMen(string TestObjectID, Document doc)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {
            Scattered.TestVerID = TestVerID;
            int No = 0;
            DataTable MenDt = Scattered.AddTableColumn("参加测试人员", "");

            ArrayList menList = Scattered.GetArrayListInfo(TestObjectID, "参加测试人员", DataTreeList);

            for (int i = 0; i <= menList.Count - 1; i++)
            {
                string Sqlstate = "select 角色,姓名,职称,主要职责 from DC测试组织与人员表 where ID=?";
                DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(Sqlstate, menList[i].ToString());

                if (dt != null && dt.Rows.Count != 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {  
                        No = No + 1;
                        DataRow Mendr = MenDt.Rows.Add();

                        Mendr["序号"] = No;
                        Mendr["角色"] = dr[0].ToString();
                        Mendr["姓名"] = dr[1].ToString();
                        Mendr["职称"] = dr[2].ToString();
                        Mendr["主要职责"] = dr[3].ToString();

                    }
                }
            }

            MenDt.TableName = "参加测试人员";
            FillTable(doc, MenDt, false, "参加测试人员");

            if (MenDt.Rows.Count > 0) return true;
            else return false;
        }

        public void ReplaceContent()
        {
            CurrentDoc.MailMerge.MergeField -= DocContent_MergeField;
            CurrentDoc.MailMerge.MergeField += DocContent_MergeField;
            CurrentDoc.MailMerge.Execute(new string[0], new object[0]);
        }

        public void FillTable(Document doc, DataTable dt, bool IfAddXuHao, string tableName)
        {

            if (IfAddXuHao == true)
            {
                dt.Columns.Add("序号", typeof(string));
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    dt.Rows[k]["序号"] = k + 1;
                }
            }

            doc.MailMerge.MergeField += new MergeFieldEventHandler(HandleMergeField_Process);
            doc.MailMerge.ExecuteWithRegions(dt);


            bool IfHaveValue = false;
            if (tableName == "DC引用文件表")
            {
                if (dt.Rows.Count == 0)//前面没内容
                {
                    IfHaveValue = AddExperObject("引用文档", "引用文档" + "引入文件", false);
                }
                else
                {
                    IfHaveValue = AddExperObject("引用文档", "引用文档" + "引入文件", true);
                }
            }
            if (tableName == "DC术语表")
            {
                if (dt.Rows.Count == 0)//前面没内容
                {
                    IfHaveValue = AddExperObject("术语和缩略语", "术语和缩略语" + "引入文件", false);
                }
                else
                {
                    IfHaveValue = AddExperObject("术语和缩略语", "术语和缩略语" + "引入文件", true);
                }
            }
            if (tableName == "DC计划进度表")
            {
                if (dt.Rows.Count == 0)//前面没内容
                {
                    IfHaveValue = AddExperObject("测试进度计划", "测试进度计划" + "引入文件", false);
                }
                else
                {
                    IfHaveValue = AddExperObject("测试进度计划", "测试进度计划" + "引入文件", true);
                }
            }

            if ((tableName != "执行结果统计表") && (tableName != "未完整执行统计表"))
            {
                DocumentBuilder db = new DocumentBuilder(doc);
                if (tableName == "附件统计")
                {
                    if (dt.Rows.Count == 0)
                    {
                        OutputComm.OutputComm.TableIfHaveRecordProcess(false, db, doc, 2, tableName);

                    }
                    else OutputComm.OutputComm.TableIfHaveRecordProcess(true, db, doc, 2, tableName);
                }
                else
                {
                    if ((tableName == "DC术语表") || (tableName == "DC引用文件表") || (tableName == "DC计划进度表") || (tableName == "问题涉及依据"))
                    {
                        if (IfHaveValue == true)
                        {
                            if (dt.Rows.Count == 0) OutputComm.OutputComm.TableIfHaveRecordProcess_1(false, db, doc, 1, tableName);
                            else OutputComm.OutputComm.TableIfHaveRecordProcess(true, db, doc, 1, tableName);
                        }
                        else
                        {
                            if (dt.Rows.Count == 0) OutputComm.OutputComm.TableIfHaveRecordProcess(false, db, doc, 1, tableName);
                            else OutputComm.OutputComm.TableIfHaveRecordProcess(true, db, doc, 1, tableName);
                        }

                    }
                    else
                    {
                        if (dt.Rows.Count == 0) OutputComm.OutputComm.TableIfHaveRecordProcess(false, db, doc, 1, tableName);
                        else OutputComm.OutputComm.TableIfHaveRecordProcess(true, db, doc, 1, tableName);

                    }

                }

            }

        }

        public void FillView(Document doc, DataView dv, bool IfAddXuHao, string tableName)
        {

            if (IfAddXuHao == true)
            {
                dv.Table.Columns.Add("序号", typeof(int));
                for (int k = 0; k < dv.Table.Rows.Count; k++)
                {
                    dv.Table.Rows[k]["序号"] = k + 1;
                }
            }

            dv.Table.TableName = tableName;
            if (IfAddXuHao == true)
            {
                dv.Sort = "序号";
            }
            doc.MailMerge.ExecuteWithRegions(dv);

        }

        public void HandleMergeField_Process(object sender, MergeFieldEventArgs e)
        {

            Scattered.TestVerID = TestVerID;

            if (e.FieldName.Equals("出版日期"))
            {
                object DateObject = e.FieldValue;

                e.Text = DateObject.ToString();

            }
            if ((e.FieldName.Equals("预计开始时间")) || (e.FieldName.Equals("预计完成时间")))
            {
                if (e.FieldValue != null)
                {
                    try
                    {
                        if (e.FieldValue is DateTime)
                        {
                            DateTime DateObject = (DateTime)e.FieldValue;
                            if (DateObject != null)
                            {
                                e.Text = DateObject.Year.ToString() + "-" + DateObject.Month.ToString() + "-" + DateObject.Day.ToString();
                            }
                        }
                       
                    }
                    catch
                    {
                        e.Text = "";
                    }
                }


            }
            if (e.FieldName.Equals("主要完成人"))
            {
                e.Text = GetInChangeStr("人员", e.FieldValue.ToString());

            }
            if (e.FieldName.Equals("测试结果"))
            {
                if (int.Parse(e.FieldValue.ToString()) == 0)
                {
                    e.Text = "无用例";
                }
                else if (int.Parse(e.FieldValue.ToString()) == 1)
                {
                    e.Text = "未通过";
                }
                else if (int.Parse(e.FieldValue.ToString()) == 2)
                {
                    e.Text = "待定";
                }
                else if (int.Parse(e.FieldValue.ToString()) == 3)
                {
                    e.Text = "通过";
                }

            }
            if (e.FieldName.Equals("NodeType"))
            {

                AddImage(e.Document, int.Parse(e.FieldValue.ToString()));

            }

            if (e.FieldName.Equals("影响域分析"))//放对象
            {
               // string PreVer = Scattered.GetPreVersion();

                string ID = e.FieldValue.ToString();
                if (ID != "")
                {
                    string sqlstate = "select 影响域分析 from HG软件更动表 where ID=?";
                    DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ID);

                    object Value = OutputComm.OutputComm.OLEObjectProcess(dt, CurrentDoc, "影响域分析");
                    if (Value != null)
                    {
                        e.Text = Value.ToString();
                    }

                }
                else
                {
                    e.Text = "";

                }              

            }
            if (e.FieldName.Equals("其它更动影响域分析"))//放对象
            {
                string ID = e.FieldValue.ToString();
                string sqlstate = "select 影响域分析 from HG软件更动表 where ID =? and 项目ID=? and 测试版本=?";

                DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ID, ProjectID, TestVerID);
                object Value = OutputComm.OutputComm.OLEObjectProcess(dt, CurrentDoc, "其它更动影响域分析");
                if (Value != null)
                {
                    e.Text = Value.ToString();
                }


            }

        }

        public static string GetInChangeStr(string StrType, string OldValue)
        {
            DataTable dt = null;
            string ValueStr = "";

            switch (StrType)
            {
                case "人员":
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("select * from DC测试组织与人员表");
                    ValueStr = GridAssist.GetMultiDisplayString(dt, "ID", "姓名", OldValue, ",");
                    break;

                case "测试项优先级":
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("select * from DC测试项优先级表");
                    ValueStr = GridAssist.GetMultiDisplayString(dt, "ID", "优先级", OldValue, ",");
                    break;

                case "测试项追踪关系":
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("select * from SYS测试依据表");
                    ValueStr = GridAssist.GetMultiDisplayString(dt, "ID", "测试依据", OldValue, "\n");
                    break;

                case "测试用例所使用的设计方法":
                    dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable("select * from DC测试用例设计方法表");
                    ValueStr = GridAssist.GetMultiDisplayString(dt, "ID", "测试用例设计方法", OldValue, "\n");
                    break;

            }

            return ValueStr;

        }

        public void ReplaceAndMergeTable(DataTable dt, Document doc, string FieldName, string TableName, DocumentBuilder db)
        {
            if (dt.Rows.Count > 0)
            {
                dt.TableName = TableName;
                FillTable(doc, dt, false, "");
                Table table = OutputComm.OutputComm.GetNodeInTable(FieldName, doc, null);
                MergeTable(table, TableName, dt, doc);

            }

        }

        public void OutputChapter_TestCaseAccording(bool IfIsAnnex)
        {

            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;

            string Nodeid;
            int NodeType1 = 0;

            string NextBrotherID;
            string NodeContentID = "0";
            string NodeContentJXm = "";
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;

            int TestObjectNum = 0;
            int TestItemNum = 0;
            DataTable TestItemAndCasedt = null;

            bool PreObjectIfHaveTable = false;

            DataRow dr = null;
            Document doc1 = null;
            DocumentBuilder db1 = null;

            iStack = 0;

            Scattered.TestVerID = TestVerID;

            IfTiSheng = ResetIfTiSheng();

            DocumentBuilder db = new DocumentBuilder(CurrentDoc);
            Section TestCaseAccordingSection = OutputComm.OutputComm.GetNodeInSection("测试用例追踪关系表", CurrentDoc, db);

            if (DataTreeList.Count <= 0)
            {
                TestCaseAccordingSection.Remove();

                Node node1 = OutputComm.OutputComm.GetNodeByField(CurrentDoc, "准备替换", db);
                if (node1 != null)
                {
                    node1.Range.Delete();
                }

                db.MoveToMergeField("可变章节_测试用例的追踪关系");
                db.ParagraphFormat.Style = CurrentDoc.Styles["文本首行缩进"];
                db.Writeln("无。");

                return;
            }


            NodeTree node = (NodeTree)DataTreeList[0];

            do
            {
                while ((node != null) && (node.NodeType <= 3))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType1 = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    int btno = layer + 1;

                    if (NodeType1 == 1)
                    {

                        TestItemNum = 0;

                        if ((TestObjectNum > 0) && (PreObjectIfHaveTable == true))
                        {
                            ReplaceAndMergeTable(TestItemAndCasedt, doc1, "序号", "测试用例追踪关系", db1);
                            OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, CurrentDoc, null, db);

                        }

                        TestItemAndCasedt = Scattered.AddTableColumn("测试用例追踪关系", "");

                        TestObjectNum = TestObjectNum + 1;

                        if (Scattered.IfHaveItem_OneObject(NodeContentID, DataTreeList) == true)
                        {

                            OutputComm.OutputComm.MuiChapterProcess_5("可变章节_测试用例的追踪关系", CurrentDoc, 2, NodeContent, IfTiSheng, IfIsAnnex, "表名");

                            doc1 = new Document();
                            db1 = new DocumentBuilder(doc1);
                            OutputComm.OutputComm.ImportSec(doc1, TestCaseAccordingSection);

                            //db1.ParagraphFormat.Style = doc1.Styles["表名"];

                            OutputComm.OutputComm.ReplaceTableName("与被测对象有关", TestObjectNum, NodeContent, db1);

                            PreObjectIfHaveTable = true;

                        }
                        else
                        {
                            OutputComm.OutputComm.MuiChapterProcess_6("可变章节_测试用例的追踪关系", CurrentDoc, 2, NodeContent, IfTiSheng, IfIsAnnex);
                            PreObjectIfHaveTable = false;
                        }

                    }

                    if (NodeType1 == 3)
                    {

                        bool IfHaveSon = false;
                        TestItemNum = TestItemNum + 1;

                        string sqlstate1 = "SELECT CA测试项实体表.测试项名称, CA测试项实测表.ID, CA测试项实体表.父节点ID, CA测试项实测表.序号, CA测试项实测表.项目ID, CA测试项实测表.测试版本" +
                                           " FROM CA测试项实体表 INNER JOIN CA测试项实测表 ON CA测试项实体表.ID = CA测试项实测表.测试项ID WHERE CA测试项实体表.父节点ID=? " +
                                           " AND CA测试项实测表.项目ID=? AND CA测试项实测表.测试版本=? ORDER BY CA测试项实测表.序号;";

                        DataTable itemdt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, node.NodeContentID_body, ProjectID, TestVerID);
                        for (int testitemnum = 0; testitemnum <= itemdt.Rows.Count - 1; testitemnum++)//对应有测试子项的情况
                        {

                            IfHaveSon = true;
                            dr = TestItemAndCasedt.Rows.Add();

                            dr["序号"] = TestItemNum;
                            dr["测试项在计划的章节"] = Scattered.GetTestTPOrTAOutputZJ(2, NodeContentID, DataTreeList);
                            dr["测试项名称"] = RemoveHC(NodeContent);

                            string testsonitemid = itemdt.Rows[testitemnum]["ID"].ToString();//测试子项章节
                            string values1 = Scattered.GetTestTPOrTAOutputZJ(2, testsonitemid, DataTreeList);
                            int p = values1.IndexOf(".");
                            values1 = values1.Substring(p + 1, values1.Length - p - 1);
                            values1 = MyProjectInfo.GetProjectContent("设计起始章节号") + "." + values1;

                            dr["测试用例章节"] = values1;
                            dr["测试用例名称"] = RemoveHC(itemdt.Rows[testitemnum]["测试项名称"].ToString());

                        }

                        ////////////////////////////////////////////////////////////////////

                        ArrayList TestCaseList = Scattered.GetTestCaseNodeIDSonOfTestItem(Nodeid, DataTreeList);

                        string sqlstate = "SELECT CA测试用例与测试项关系表.测试项ID, CA测试用例与测试项关系表.序号, CA测试用例实体表.测试用例名称, CA测试用例实测表.ID, CA测试用例实测表.项目ID, CA测试用例实测表.测试版本 " +
                                         " FROM CA测试用例实体表 INNER JOIN (CA测试用例实测表 INNER JOIN CA测试用例与测试项关系表 ON CA测试用例实测表.ID = CA测试用例与测试项关系表.测试用例ID) ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID" +
                                         " WHERE CA测试用例与测试项关系表.测试项ID=? AND CA测试用例实测表.项目ID=? AND CA测试用例实测表.测试版本=? ORDER BY CA测试用例与测试项关系表.序号;";

                        DataTable testcasedt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, NodeContentID, ProjectID, TestVerID);
                        int tempj = 0;

                        for (int testcasenum = 0; testcasenum <= testcasedt.Rows.Count - 1; testcasenum++)
                        {

                            IfHaveSon = true;

                            dr = TestItemAndCasedt.Rows.Add();

                            dr["序号"] = TestItemNum;
                            dr["测试项在计划的章节"] = Scattered.GetTestTPOrTAOutputZJ(2, NodeContentID, DataTreeList);
                            dr["测试项名称"] = RemoveHC(NodeContent);

                            string TestCaseID = testcasedt.Rows[testcasenum]["ID"].ToString();
                            string TestCaseNodeID = TestCaseList[tempj].ToString();

                            dr["测试用例章节"] = Scattered.GetTestCaseOutputZJ_1(TestCaseID, TestCaseNodeID, DataTreeList);
                            dr["测试用例名称"] = RemoveHC(testcasedt.Rows[testcasenum]["测试用例名称"].ToString());

                            tempj++;

                        }

                        if (IfHaveSon == false)
                        {
                            dr = TestItemAndCasedt.Rows.Add();

                            dr["序号"] = TestItemNum;
                            dr["测试项在计划的章节"] = Scattered.GetTestTPOrTAOutputZJ(2, NodeContentID, DataTreeList);
                            dr["测试项名称"] = NodeContent;
                            dr["测试用例章节"] = "";
                            dr["测试用例名称"] = "该测试项下没有设计测试用例!";

                        }

                    }

                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }

                }
                if (iStack == 0)
                {
                    if (PreObjectIfHaveTable == true)
                    {
                        ReplaceAndMergeTable(TestItemAndCasedt, doc1, "序号", "测试用例追踪关系", db1);
                        
                        OutputComm.OutputComm.ChangeChapterEndProcess("可变章节_测试用例的追踪关系", doc1, CurrentDoc, null, db);

                    }

                    TestCaseAccordingSection.Remove();

                    Node node1 = OutputComm.OutputComm.GetNodeByField(CurrentDoc, "准备替换", db);
                    if (node1 != null)
                    {
                        node1.Range.Delete();
                    }
                    node1 = OutputComm.OutputComm.GetNodeByField(CurrentDoc, "可变章节_测试用例的追踪关系", db);
                    if (node1 != null)
                    {
                        node1.Range.Delete();
                    }

                    return;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }
            } while (true);

        }

        public bool TestResultStat(string OutputType, DocumentBuilder db, Document doc, string TestObjectID)
        {

            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;
            string Nodeid;
            int NodeType1 = 0;

            string NextBrotherID;
            string NodeContentID = "0";
            string NodeContentJXm = "";
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;
            bool IsThisObjectFlag = false;
            DataTable StatTable = null;
            int TableCurrentRowNum = 0;
            Table TestStatTable = null;
            int TestObjectNum = 0;

            iStack = 0;
            Scattered.TestVerID = TestVerID;

            if (DataTreeList.Count <= 0)
            {
                return false;
            }

            if (OutputType == "执行结果统计")
            {
                StatTable = Scattered.AddTableColumn(OutputType, "");
                TestStatTable = OutputComm.OutputComm.GetNodeInTable("执行结果统计表", doc, db);
            }
            else if (OutputType == "未完整执行统计")
            {
                StatTable = Scattered.AddTableColumn(OutputType, "");
                TestStatTable = OutputComm.OutputComm.GetNodeInTable("未完整执行统计表", doc, db);
            }

            NodeTree node = (NodeTree)DataTreeList[0];

            do
            {
                while ((node != null) && (node.NodeType <= 4))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType1 = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    int btno = layer + 1;

                    if (NodeType1 == 1)
                    {
                        TestObjectNum = TestObjectNum + 1;

                        IsThisObjectFlag = false;

                        if (TestObjectID == NodeContentID)
                        {
                            IsThisObjectFlag = true;
                        }

                    }

                    if ((NodeType1 == 4) && (IsThisObjectFlag == true) && (testcaseflag == 1))
                    // if ((NodeType1 == 4) && (IsThisObjectFlag == true))
                    {
                        string sqlstate = "";
                        DataTable dt = null;

                        if (OutputType == "执行结果统计")
                        {
                            TableCurrentRowNum = TableCurrentRowNum + 1;

                            sqlstate = "SELECT CA测试用例实测表.ID, CA测试用例实测表.测试用例ID, CA测试用例实体表.测试用例名称, CA测试用例实测表.执行状态, CA测试用例实测表.执行结果, CA测试过程实测表.序号, CA测试过程实测表.问题报告单ID, CA测试过程实测表.测试版本, CA测试过程实测表.项目ID " +
                                       " FROM ((CA测试用例实体表 INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID) LEFT JOIN CA测试过程实体表 ON CA测试用例实体表.ID = CA测试过程实体表.测试用例ID) LEFT JOIN CA测试过程实测表 ON CA测试过程实体表.ID = CA测试过程实测表.过程ID " +
                                       " WHERE (((CA测试用例实测表.ID)=?) AND (Not (CA测试过程实测表.问题报告单ID) Is Null) AND ((CA测试用例实测表.测试版本)=?) AND ((CA测试用例实测表.项目ID)=?) AND ((CA测试过程实测表.测试版本)=?) AND ((CA测试过程实测表.项目ID)=?))" +
                                       " ORDER BY CA测试过程实测表.序号;";
                            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, NodeContentID, TestVerID, ProjectID, TestVerID, ProjectID);

                            if (dt != null && dt.Rows.Count != 0)//提交过问题
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    DataRow Statdr = StatTable.Rows.Add();
                                    Statdr["序号"] = TableCurrentRowNum;
                                    Statdr["测试用例名称"] = dr[2].ToString();
                                    Statdr["测试用例标识"] = NodeContentJXm;
                                    Statdr["执行情况"] = dr[3].ToString();
                                    Statdr["执行结果"] = dr[4].ToString();
                                    Statdr["错误步骤"] = dr[5].ToString();
                                    Statdr["问题标识"] = CommonDB.GenPblSignForStep(TPM3.Sys.GlobalData.globalData.dbProject, ConstDef.PblSplitter(), dr[6].ToString());

                                }

                            }
                            else//未提交问题
                            {
                                sqlstate = "SELECT CA测试用例实测表.ID, CA测试用例实测表.测试用例ID, CA测试用例实体表.测试用例名称, CA测试用例实测表.执行状态, CA测试用例实测表.执行结果" +
                                             " FROM CA测试用例实体表 INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID WHERE CA测试用例实测表.ID=?";

                                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, NodeContentID);
                                DataRow dr = dt.Rows[0];

                                DataRow Statdr = StatTable.Rows.Add();
                                Statdr["序号"] = TableCurrentRowNum;
                                Statdr["测试用例名称"] = NodeContent;
                                Statdr["测试用例标识"] = NodeContentJXm;
                                Statdr["执行情况"] = dr[3].ToString();
                                Statdr["执行结果"] = dr[4].ToString();
                                Statdr["错误步骤"] = "";
                                Statdr["问题标识"] = "";

                            }

                        }
                        else if (OutputType == "未完整执行统计")
                        {

                            sqlstate = " SELECT CA测试用例实测表.测试用例ID, CA测试用例实体表.测试用例名称, CA测试用例实测表.执行状态, CA测试用例实测表.未执行原因" +
                                      " FROM CA测试用例实体表 INNER JOIN CA测试用例实测表 ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID WHERE CA测试用例实测表.执行状态<>" + "'" + "完整执行" + "'" + " AND CA测试用例实测表.ID=?";

                            dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, NodeContentID);
                            if (dt != null && dt.Rows.Count != 0)
                            {
                                TableCurrentRowNum = TableCurrentRowNum + 1;

                                DataRow dr = dt.Rows[0];

                                DataRow Statdr = StatTable.Rows.Add();
                                Statdr["序号"] = TableCurrentRowNum;
                                Statdr["测试用例名称"] = NodeContent;
                                Statdr["测试用例标识"] = NodeContentJXm;
                                Statdr["执行状态"] = dr[2].ToString();
                                Statdr["未执行或部分执行原因"] = dr[3].ToString();

                            }
                        }
                    }

                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }

                }
                if (iStack == 0)
                {
                    if (OutputType == "执行结果统计")
                    {
                        StatTable.TableName = "执行结果统计";
                        FillTable(doc, StatTable, false, "执行结果统计表");
                    }
                    else if (OutputType == "未完整执行统计")
                    {
                        StatTable.TableName = "未完整执行统计";
                        FillTable(doc, StatTable, false, "未完整执行统计表");
                    }

                    MergeTable(TestStatTable, OutputType, StatTable, doc);

                    if (StatTable.Rows.Count > 0) return true;
                    else return false;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }
            } while (true);

        }

        public void AddImage(Document doc1, int type)
        {
            Image image = null;

            string s = Assembly.GetEntryAssembly().Location;
            s = Path.GetDirectoryName(s);

            //if (type == 2)
            //{
            //    s = Path.Combine(s, @GlobalData.BaseDirectory + "dot\\Picture\\TestObject.bmp");
            //}
            if (type == 3)
            {
                s = Path.Combine(s, @GlobalData.BaseDirectory + "dot\\Picture\\TestClass.bmp");
                image = Image.FromFile(s);
            }
            else if (type == 4)
            {
                s = Path.Combine(s, @GlobalData.BaseDirectory + "dot\\Picture\\TestItem1.bmp");
                image = Image.FromFile(s);
            }

            if (image != null)
            {
                DocumentBuilder docb = new DocumentBuilder(doc1);
                docb.MoveToMergeField("NodeType");
                docb.InsertImage(image);
            }
            else
            {
                DocumentBuilder docb = new DocumentBuilder(doc1);
                docb.MoveToMergeField("NodeType");
                docb.Write("");
            }
        }

        public bool ResetIfTiSheng()
        {

            bool ValueType = false;

            string sqlstate = " SELECT * from CA被测对象实测表 where 项目ID=? and 测试版本=?";
            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, TestVerID);
            if (dt.Rows.Count == 1)
            {
                ValueType = true;
            }
            else
            {
                ValueType = false;

            }

            if (ValueType == false)//多个对象
            {
                IfTiSheng = true;
            }

            return IfTiSheng;

        }

        public bool AddExperObject(string filterstr, string Nodestr, bool IfOutput_No)
        {

            bool returnv = false;

            DocumentBuilder db = new DocumentBuilder(CurrentDoc);

            string sqlstate = "select 文档内容 from SYS文档内容表 where 项目ID=? and 测试版本=? and 文档名称=? and 内容标题=?";
            DataTable dt = null;
                 
            if ((DocumentName == "回归测试方案") && (Nodestr == "测试进度计划引入文件"))
            {
                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, TestVerID, "项目信息", filterstr);
            }
            else
            {
                dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, TestVerID, DocumentName, filterstr);
            }

            object Value = OutputComm.OutputComm.OLEObjectProcess_NoPutNo(dt, CurrentDoc, Nodestr, IfOutput_No);

            if (Value != null)
            {
                if ((string)Value != "无。")
                {
                    db.MoveToMergeField(Nodestr);
                    db.ParagraphFormat.Style = CurrentDoc.Styles["文本首行缩进"];
                    db.Write(Value.ToString());
                    returnv = true;
                }
                else
                {
                    Node node = OutputComm.OutputComm.GetNodeByField(CurrentDoc, Nodestr, db);
                    if (node != null)
                    {
                        node.Remove();
                    }
                    returnv = false;
                }

            }
            else//ole-object
            {
                Node node = OutputComm.OutputComm.GetNodeByField(CurrentDoc, Nodestr, db);
                if (node != null)
                {
                    node.Remove();
                }
                returnv = true;
            }

            return returnv;
        }
        public DataTable GetTable(string TestObjectID, int type)
        {
            DataTable dt1 = null;

            DataView dv = null;
            int num = 0;

            string save = "";

            if (type == 1)
            {
                num = 0;

                dt1 = Scattered.AddTableColumn("已有测试项", "");

                dv = DBLayer2.GetPrevItemList(TPM3.Sys.MyBaseForm.dbProject, ProjectID, TestVerID);
                DataTable dt = dv.Table;

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["测试对象ID"].ToString() == TestObjectID)
                    {
                        DataRow dr1 = dt1.Rows.Add();

                        if (save != dr[2].ToString())
                        {
                            num = num + 1;
                        }

                        dr1["序号"] = num;
                        dr1["测试类型"] = dr[2].ToString() + "\n" + dr[3].ToString();
                        dr1["测试项"] = dr[6].ToString() + "\n" + dr[5].ToString();
                        dr1["测试依据"] = dr[10].ToString();
                        if ((bool)(dr[11]) == true)
                        {
                            dr1["是否选取"] = "是";
                        }
                        else
                        {
                            dr1["是否选取"] = "否";
                        }
                        dr1["未选取原因说明"] = dr[9].ToString();

                        save = dr[2].ToString();

                    }
                }
            }
            else
            {
                num = 0;

                dt1 = Scattered.AddTableColumn("已有测试用例", "");

                dv = DBLayer2.GetPrevCaseList(TPM3.Sys.MyBaseForm.dbProject, ProjectID, TestVerID);
                DataTable dt = dv.Table;

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["测试对象ID"].ToString() == TestObjectID)
                    {
                        DataRow dr1 = dt1.Rows.Add();

                        if (save != dr[6].ToString())
                        {
                            num = num + 1;
                        }
                        dr1["序号"] = num;
                        dr1["测试项"] = dr[6].ToString() + "\n" + dr[5].ToString();
                        dr1["测试用例"] = dr[9].ToString() + "\n" + dr[8].ToString();
                        if ((bool)dr[14] == true)
                        {
                            dr1["是否选取"] = "是";
                        }
                        else
                        {
                            dr1["是否选取"] = "否";
                        }
                        dr1["未选取原因说明"] = dr[13].ToString();

                        save = dr[6].ToString();

                    }
                }
            }

            return dt1;
        }

        public void OutputChapter_AlreadyHaveItem(ArrayList DataTreeList)
        {
            int linenum = 1;

            Document doc1 = new Document();
            DocumentBuilder db1 = new DocumentBuilder(doc1);

          //  int TestObjectNum = 3;
            IfTiSheng = ResetIfTiSheng();

            Scattered.TestVerID = TestVerID;

            DocumentBuilder db = new DocumentBuilder(CurrentDoc);
            Section TableSection = OutputComm.OutputComm.GetNodeInSection("已有测试项表", CurrentDoc, db);

            DataTable AlreadyHaveTable = null;

            string sqlstate = " SELECT CA被测对象实体表.ID, CA被测对象实体表.被测对象名称, CA被测对象实测表.测试版本, CA被测对象实测表.项目ID, CA被测对象实测表.序号" +
                              " FROM CA被测对象实体表 INNER JOIN CA被测对象实测表 ON CA被测对象实体表.ID = CA被测对象实测表.被测对象ID" +
                              " WHERE (((CA被测对象实测表.测试版本)=?) AND ((CA被测对象实测表.项目ID)=?)) ORDER BY CA被测对象实测表.序号;";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestVerID, ProjectID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    linenum = 1;
                    string TestObjectID = dr[0].ToString();
                    string TestObjectName = dr[1].ToString();

                    AlreadyHaveTable = GetTable(TestObjectID, 1);

                    if (AlreadyHaveTable.Rows.Count > 0)
                    {
                        OutputComm.OutputComm.MuiChapterProcess_5("可变章节_回归测试已有测试项", CurrentDoc, 4, TestObjectName, IfTiSheng, false, "表名");

                        doc1 = new Document();
                        db1 = new DocumentBuilder(doc1);
                        OutputComm.OutputComm.ImportSec(doc1, TableSection);

                     //   TestObjectNum = TestObjectNum + 1;
                     //   OutputComm.OutputComm.ReplaceTableName("与被测对象有关", TestObjectNum, TestObjectName, db1);
                      
                        ReplaceAndMergeTable(AlreadyHaveTable, doc1, "序号", "已有测试项", db1);
                                          
                        OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, CurrentDoc, null, db);

                    }

                }
            }

           

            TableSection.Remove();

            Node node1 = OutputComm.OutputComm.GetNodeByField(CurrentDoc, "准备替换", db);
            if (node1 != null)
            {
                node1.Range.Delete();
            }
            node1 = OutputComm.OutputComm.GetNodeByField(CurrentDoc, "可变章节_回归测试已有测试项", db);
            if (node1 != null)
            {
                node1.Range.Delete();
            }


        }

        public void OutputChapter_AlreadyHaveCase(ArrayList DataTreeList)
        {
            int linenum = 1;

            Scattered.TestVerID = TestVerID;

            Document doc1 = new Document();
            DocumentBuilder db1 = new DocumentBuilder(doc1);

           // int TestObjectNum = 5;
            IfTiSheng = ResetIfTiSheng();

            DocumentBuilder db = new DocumentBuilder(CurrentDoc);
            Section TableSection = OutputComm.OutputComm.GetNodeInSection("已有测试用例表", CurrentDoc, db);

            DataTable AlreadyHaveTable = null;
            string sqlstate = " SELECT CA被测对象实体表.ID, CA被测对象实体表.被测对象名称, CA被测对象实测表.测试版本, CA被测对象实测表.项目ID, CA被测对象实测表.序号" +
                            " FROM CA被测对象实体表 INNER JOIN CA被测对象实测表 ON CA被测对象实体表.ID = CA被测对象实测表.被测对象ID" +
                            " WHERE (((CA被测对象实测表.测试版本)=?) AND ((CA被测对象实测表.项目ID)=?)) ORDER BY CA被测对象实测表.序号;";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, TestVerID, ProjectID);

            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    linenum = 1;

                    string TestObjectID = dr[0].ToString();
                    string TestObjectName = dr[1].ToString();

                    AlreadyHaveTable = GetTable(TestObjectID, 2);

                    if (AlreadyHaveTable.Rows.Count > 0)
                    {
                        OutputComm.OutputComm.MuiChapterProcess_5("可变章节_回归测试已有测试用例", CurrentDoc, 4, TestObjectName, IfTiSheng, false, "表名");

                        doc1 = new Document();
                        db1 = new DocumentBuilder(doc1);
                        OutputComm.OutputComm.ImportSec(doc1, TableSection);
                      //  TestObjectNum = TestObjectNum + 1;
                      //  OutputComm.OutputComm.ReplaceTableName("与被测对象有关", TestObjectNum, TestObjectName, db1);

                        ReplaceAndMergeTable(AlreadyHaveTable, doc1, "序号", "已有测试用例", db1);
                        OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, CurrentDoc, null, db);

                    }
                }
            }

            TableSection.Remove();

            Node node1 = OutputComm.OutputComm.GetNodeByField(CurrentDoc, "准备替换", db);
            if (node1 != null)
            {
                node1.Range.Delete();
            }
            node1 = OutputComm.OutputComm.GetNodeByField(CurrentDoc, "可变章节_回归测试已有测试用例", db);
            if (node1 != null)
            {
                node1.Range.Delete();
            }

        }

        public void OutputChapter_HGNewAdd(string OutputType, string SonType, bool IfOutputAnnex)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        {
            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;
            string Nodeid;
            int NodeType1 = 0;

            string NextBrotherID;
            string NodeContentID = "0";
            string NodeContentJXm = "";
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;
            int TestObjectNum = 0;

            ArrayList TestItemList = null;
            ArrayList TestCaseList = null;
            bool IfHaveNewItem = false;
            bool IfHaveNewCase = false;
            int TestCaseNum = 0;

            int First = 1;//2011-09-19+++

            Scattered.TestVerID = TestVerID;

            iStack = 0;

            DocumentBuilder db = new DocumentBuilder(CurrentDoc);
            Section Section = null;

            IfTiSheng = ResetIfTiSheng();

            if (OutputType == "测试用例")
            {
                Section = OutputComm.OutputComm.GetNodeInSection("测试用例表", CurrentDoc, db);
            }
            else if (OutputType == "测试项")
            {
                Section = OutputComm.OutputComm.GetNodeInSection("测试项信息表", CurrentDoc, db);
            }

            if (DataTreeList.Count <= 0)
            {
                if (OutputType == "测试用例")
                {
                    db.MoveToMergeField("可变章节_测试用例");
                    db.ParagraphFormat.Style = CurrentDoc.Styles["文本首行缩进"];
                    db.Writeln("无。");
                }
                else if (OutputType == "测试项")
                {
                    db.MoveToMergeField("可变章节_测试项");
                    db.ParagraphFormat.Style = CurrentDoc.Styles["文本首行缩进"];
                    db.Writeln("无。");
                }
                Section.Remove();

                return;
            }
            if (OutputType == "测试项")
            {
                TestItemList = Scattered.TestItemList_PerVer();
            }
            else if (OutputType == "测试用例")
            {
                TestCaseList = Scattered.TestCaseList_PerVer();
            }

            DataView dv = TPM3.wx.AttachCaseTrace.GetAttachCaseTraceView(3);


            string FieldName = Scattered.GetFieldName(OutputType);

            NodeTree node = (NodeTree)DataTreeList[0];

            do
            {
                while ((node != null) && (node.NodeType <= 4))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType1 = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    int btno = layer + 1 + 2;

                    if (NodeType1 == 1)
                    {
                        TestObjectNum = TestObjectNum + 1;

                        DataTable dt = null;
                        Document doc1 = OutputComm.OutputComm.MuiChapterProcess_1(FieldName, NodeContent, db, null, CurrentDoc, btno, IfTiSheng);

                        if (doc1 != null)
                        {
                            DocumentBuilder db1 = new DocumentBuilder(doc1);

                            FillTable(doc1, dt, false, "");

                            OutputComm.OutputComm.ReplaceTableName("与被测对象有关", TestObjectNum, NodeContent, db1);

                            OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, CurrentDoc, null, db);
                        }

                    }
                    if (NodeType1 == 3)
                    {
                        //if (OutputType == "测试用例")
                        //{
                        // OutputComm.OutputComm.MuiChapterProcess_2(FieldName, CurrentDoc, btno, NodeContent, IfTiSheng);
                        //}
                        if (OutputType == "测试项")
                        {
                            if (Scattered.IfInIt_Item(TestItemList, NodeContentID) == false)//addnew
                            {
                                IfHaveNewItem = true;

                                string TestItemBodyID = GetTestItemID_body(NodeContentID);

                                if (IfHaveSonItem(TestItemBodyID) == false)
                                {
                                    OutputComm.OutputComm.MuiChapterProcess_3(FieldName, CurrentDoc, 5, NodeContent, IfTiSheng, 1, 0, false, false, false);
                                    Document doc1 = new Document();
                                    DocumentBuilder db1 = new DocumentBuilder();
                                    OutputComm.OutputComm.ImportSec(doc1, Section);
                                    ReplaceSingle_ItemOrTestCase(NodeContentID, doc1, NodeContentJXm, OutputType, false, null, null, IfOutputAnnex,"");
                                    OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, CurrentDoc, null, db);
                                }
                                else
                                {
                                    OutputComm.OutputComm.MuiChapterProcess_3(FieldName, CurrentDoc, 5, NodeContent, IfTiSheng, 1, 0, false, false, true);

                                }

                            }
                        }
                    }
                    if (NodeType1 == 4)
                    {
                        if (OutputType == "测试用例")
                        {
                            if (testcaseflag == 1)
                            {
                                if (Scattered.IfInIt_Case(TestCaseList, NodeContentID) == false)//addnew
                                {
                                    TestCaseNum = TestCaseNum + 1;
                                    IfHaveNewCase = true;
                                    if (First == 1)
                                    {
                                        OutputComm.OutputComm.MuiChapterProcess_3(FieldName, CurrentDoc, 5, NodeContent, IfTiSheng, 2, TestCaseNum, false,false,false);//2011-09-19+++
                                        First = 0;
                                    }
                                    else
                                    {
                                        OutputComm.OutputComm.MuiChapterProcess_3(FieldName, CurrentDoc, 5, NodeContent, IfTiSheng, 2, TestCaseNum, true,false,false);//2011-09-19+++
                                    }
                                    Document doc1 = new Document();
                                    DocumentBuilder db1 = new DocumentBuilder();
                                    OutputComm.OutputComm.ImportSec(doc1, Section);
                                    ReplaceSingle_ItemOrTestCase(NodeContentID, doc1, NodeContentJXm, OutputType, false, null, dv, IfOutputAnnex,"");
                                    OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, CurrentDoc, null, db);
                                }

                            }
                            //else
                            //{
                            //    string referenceBS = Scattered.GetDirectTestCaseBS(NodeContentID, DataTreeList);
                            //    OutputComm.OutputComm.MuiChapterProcess_3(FieldName, CurrentDoc, 5, NodeContent, IfTiSheng);
                            //    db.MoveToMergeField("准备替换");
                            //    db.Write("见测试用例" + referenceBS + "。");

                            //}
                        }
                    }

                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }

                }
                if (iStack == 0)
                {
                    if (OutputType == "测试用例")
                    {
                        db.MoveToMergeField("可变章节_测试用例");
                    }
                    else if (OutputType == "测试项")
                    {
                        db.MoveToMergeField("可变章节_测试项");
                    }
                    Section.Remove();

                    if ((OutputType == "测试项") && (IfHaveNewItem == false))
                    {
                        db.ParagraphFormat.Style = CurrentDoc.Styles["文本首行缩进"];
                        db.Writeln("无。");
                    }

                    if ((OutputType == "测试用例") && (IfHaveNewCase == false))
                    {
                        db.ParagraphFormat.Style = CurrentDoc.Styles["文本首行缩进"];
                        db.Writeln("无。");
                    }

                    return;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }
            } while (true);

           

        }

        public void OutputChapter_UnderHaveTable_HG(string OutputType, string BSField, string BSChapter, string TextStartField, Document doc, ArrayList TestVerList)
        {

            int TableNo = 0;

            IfTiSheng = ResetIfTiSheng();
            Scattered.TestVerID = TestVerID;

            DocumentBuilder db = new DocumentBuilder(doc);
            Section Section = OutputComm.OutputComm.GetNodeInSection(BSChapter, doc, db);

            if (Section != null)
            {
               // ArrayList TestVerList = GetTestVerIDList();
                for (int j = 0; j <= TestVerList.Count - 1; j++)
                {
                    TestVerID = TestVerList[j].ToString();
                    string TestVerStr = GetCurrentVerStr_2(TestVerID);

                    Scattered.TestVerID = TestVerID;
                    DataTable dt = Scattered.GetObjectName();

                    if (dt.Rows.Count == 0)
                    {
                        db.MoveToMergeField(TextStartField);
                        db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                        db.Writeln("无。");
                    }
                    else
                    {

                        NodeTree nodetree = new NodeTree();
                        nodetree.TestVerID = TestVerID;
                        DataTreeList = nodetree.PutNodeToLayerList();

                        db.MoveToMergeField(TextStartField);

                        db.ParagraphFormat.Style = doc.Styles["标题 2"];
                        db.Writeln(TestVerStr);

                        OutputComm.OutputComm.AddField(TextStartField, db);

                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            string ObjectName = dr["被测对象名称"].ToString();
                            string ObjectID = dr["被测对象实测ID"].ToString();
                            string ObjectID_body = dr["被测对象实体ID"].ToString();

                            DataTable dt1 = Scattered.GetUnderHaveTableData(TextStartField, ObjectID, ObjectID_body, TestVerID, ProjectID, DataTreeList);

                            Document doc1 = OutputComm.OutputComm.AddTableUnderChapter(TextStartField, db, ObjectName, dt1, Section, CurrentDoc, IfTiSheng, "标题 3");

                            if (dt1.Rows.Count > 0)
                            {
                                TableNo = TableNo + 1;

                                if (OutputType == "测试类型统计")
                                {
                                    ReplaceSingleTestTypeStatTable(doc1, dt1, TableNo, ObjectName);
                                }
                                else if (OutputType == "提交问题一览")
                                {
                                    ReplaceQuestionInfoStatTable(doc1, dt1, TableNo, ObjectName);
                                    Table table = OutputComm.OutputComm.GetNodeInTable(BSField, doc1, null);
                                    MergeTable(table, OutputType, dt1, doc1);
                                }

                                OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, doc, null, db);

                            }
                            else
                            {
                                db.MoveToMergeField("准备替换");
                                db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                                db.Writeln("无。");

                            }
                        }

                    }
                }

                Node node1 = OutputComm.OutputComm.GetNodeByField(doc, TextStartField, db);
                if (node1 != null)
                {
                    node1.Remove();
                }
                Section.Remove();

            }
        }

        public void AddCoverInfo()
        {
            string sqlstate = "select * from 文档封面信息表 where 项目ID=? and 测试版本=? and 文档名称=?";

            DataTable dt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, ProjectID, TestVerID, DocumentName);

            if (dt != null && dt.Rows.Count != 0)
            {
                DocumentBuilder db = new DocumentBuilder(CurrentDoc);

                DataRow dr = dt.Rows[0];

                if (db.MoveToMergeField("编写人"))
                {
                    db.Write(dr["编写人"].ToString());
                }
                if (db.MoveToMergeField("参加人"))
                {
                    db.Write(dr["参加人"].ToString());
                }
                if (db.MoveToMergeField("校对人"))
                {
                    db.Write(dr["校对人"].ToString());
                }
                if (db.MoveToMergeField("标审人"))
                {
                    db.Write(dr["标审人"].ToString());
                }
                if (db.MoveToMergeField("审核人"))
                {
                    db.Write(dr["审核人"].ToString());
                }
                if (db.MoveToMergeField("批准人"))
                {
                    db.Write(dr["批准人"].ToString());
                }

                string temp = "";
                if (db.MoveToMergeField("编写日期"))
                {
                    temp = dr["编写日期"].ToString();
                    db.Write(DateProcess(temp));

                }

                if (db.MoveToMergeField("校对日期"))
                {
                    temp = dr["校对日期"].ToString();
                    db.Write(DateProcess(temp));
                }

                if (db.MoveToMergeField("标审日期"))
                {
                    temp = dr["标审日期"].ToString();
                    db.Write(DateProcess(temp));
                }

                if (db.MoveToMergeField("审核日期"))
                {
                    temp = dr["审核日期"].ToString();
                    db.Write(DateProcess(temp));
                }

                if (db.MoveToMergeField("批准日期"))
                {
                    temp = dr["批准日期"].ToString();
                    db.Write(DateProcess(temp));
                }

            }
        }

        public string RemoveHC(string content)
        {
            string[] returnstr = content.Split('\r');

            return returnstr[0];

        }

        public string DateProcess(string OldDate)
        {
            string Value = "";

            string[] Date = OldDate.Split('-');
            if (int.Parse(Date[1]) < 10)
            {
                Date[1] = "0" + Date[1];
            }
            if (int.Parse(Date[2]) < 10)
            {
                Date[2] = "0" + Date[2];
            }

            Value = Date[0] + "-" + Date[1] + "-" + Date[2];

            return Value;

        }

        public DataTable TestCaseAccording_ZXTF()
        {
            int iStack;
            NodeTree[] stack = new NodeTree[10];
            NodeTree node_pre;

            string Nodeid;
            int NodeType1 = 0;

            string NextBrotherID;
            string NodeContentID = "0";
            string NodeContentJXm = "";
            string NodeContent;
            string FirstSonID = "0";
            int layer;
            int testcaseflag;         
            int TestItemNum = 0;
            DataTable TestItemAndCasedt = null;
            DataRow dr = null;
          
            iStack = 0;

            Scattered.TestVerID = TestVerID;

            NodeTree node = (NodeTree)DataTreeList[0];

            TestItemAndCasedt = Scattered.AddTableColumn("测试用例追踪关系_最小模式", "");
            TestItemNum = 0;

            do
            {
                while ((node != null) && (node.NodeType <= 3))
                {
                    //=====输出
                    Nodeid = node.NodeID;
                    NodeType1 = node.NodeType;
                    FirstSonID = node.FirstSonID;
                    NextBrotherID = node.NextBrotherID;
                    NodeContent = node.NodeContent;
                    NodeContentID = node.NodeContentID_test;
                    NodeContentJXm = node.NodeContentJXm;
                    testcaseflag = node.testcaseflag;
                    layer = node.Layer;

                    int btno = layer + 1;

                    if (NodeType1 == 3)
                    {
                       // bool IfHaveSon = false;
                        TestItemNum = TestItemNum + 1;

                        //string sqlstate1 = "SELECT CA测试项实体表.测试项名称, CA测试项实测表.ID, CA测试项实体表.父节点ID, CA测试项实测表.序号, CA测试项实测表.项目ID, CA测试项实测表.测试版本" +
                        //                   " FROM CA测试项实体表 INNER JOIN CA测试项实测表 ON CA测试项实体表.ID = CA测试项实测表.测试项ID WHERE CA测试项实体表.父节点ID=? " +
                        //                   " AND CA测试项实测表.项目ID=? AND CA测试项实测表.测试版本=? ORDER BY CA测试项实测表.序号;";

                        //DataTable itemdt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, node.NodeContentID_body, ProjectID, TestVerID);
                        //if (itemdt.Rows.Count > 0)
                        //{
                        //    for (int testitemnum = 0; testitemnum <= itemdt.Rows.Count - 1; testitemnum++)
                        //    {

                              

                        //    }
                        //}

                        ////////////////////////////////////////////////////////////////////

                        ArrayList TestCaseList = Scattered.GetTestCaseNodeIDSonOfTestItem(Nodeid, DataTreeList);

                        if (TestCaseList.Count == 0)
                        {
                            dr = TestItemAndCasedt.Rows.Add();

                            dr["序号"] = TestItemNum;
                            string TestItemBS = Scattered.GetTestItemBS(NodeContentID, DataTreeList);
                            dr["测试项标识"] = TestItemBS;
                            dr["测试项名称"] = NodeContent;
                            dr["测试用例标识"] = "";
                            dr["测试用例名称"] = "该测试项下没有设计测试用例!";

                        }
                        else
                        {
                            string sqlstate = "SELECT CA测试用例与测试项关系表.测试项ID, CA测试用例与测试项关系表.序号, CA测试用例实体表.测试用例名称, CA测试用例实测表.ID, CA测试用例实测表.项目ID, CA测试用例实测表.测试版本 " +
                                                                    " FROM CA测试用例实体表 INNER JOIN (CA测试用例实测表 INNER JOIN CA测试用例与测试项关系表 ON CA测试用例实测表.ID = CA测试用例与测试项关系表.测试用例ID) ON CA测试用例实体表.ID = CA测试用例实测表.测试用例ID" +
                                                                    " WHERE CA测试用例与测试项关系表.测试项ID=? AND CA测试用例实测表.项目ID=? AND CA测试用例实测表.测试版本=? ORDER BY CA测试用例与测试项关系表.序号;";

                            DataTable testcasedt = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate, NodeContentID, ProjectID, TestVerID);
                            int tempj = 0;

                            for (int testcasenum = 0; testcasenum <= testcasedt.Rows.Count - 1; testcasenum++)
                            {
                                //IfHaveSon = true;

                                dr = TestItemAndCasedt.Rows.Add();

                                dr["序号"] = TestItemNum;
                                // dr["测试项在计划的章节"] = Scattered.GetTestTPOrTAOutputZJ(2, NodeContentID, DataTreeList);
                                string TestItemBS = Scattered.GetTestItemBS(NodeContentID, DataTreeList);
                                dr["测试项标识"] = TestItemBS;
                                dr["测试项名称"] = RemoveHC(NodeContent);

                                string TestCaseID = testcasedt.Rows[testcasenum]["ID"].ToString();
                                string TestCaseNodeID = TestCaseList[tempj].ToString();

                                //dr["测试用例章节"] = Scattered.GetTestCaseOutputZJ_1(TestCaseID, TestCaseNodeID, DataTreeList);

                                dr["测试用例标识"] = Scattered.GetTestCaseBS(TestCaseID, DataTreeList);
                                dr["测试用例名称"] = RemoveHC(testcasedt.Rows[testcasenum]["测试用例名称"].ToString());

                                tempj++;

                            }
                        }

                    }

                    stack[iStack] = node;
                    iStack = iStack + 1;

                    node_pre = node;

                    //-----push(stack,p)                                      
                    if (FirstSonID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(FirstSonID)];
                    }
                    else
                    {
                        node = null;
                    }

                }
                if (iStack == 0)
                {
                    return TestItemAndCasedt;
                }
                else// -----pop(stack,p)
                {
                    iStack = iStack - 1;
                    node_pre = stack[iStack];

                    if (node_pre.NextBrotherID != "0")
                    {
                        node = (NodeTree)DataTreeList[int.Parse(node_pre.NextBrotherID)];
                    }
                    else
                    {
                        node = null;

                    }

                }
            } while (true);

        }

        public void Output_Annex()
        {
        }

        public void OutputChapter_HGGDTestCase_Table(string OutputType, string BSField, string BSChapter, string TextStartField, Document doc, ArrayList TestVerList)
        {

            int TableNo = 0;

            IfTiSheng = ResetIfTiSheng();
            Scattered.TestVerID = TestVerID;

            DocumentBuilder db = new DocumentBuilder(doc);
            Section Section = OutputComm.OutputComm.GetNodeInSection(BSChapter, doc, db);

            if (Section != null)
            {
                for (int j = 0; j <= TestVerList.Count - 1; j++)
                {
                    TestVerID = TestVerList[j].ToString();
                    string TestVerStr = GetCurrentVerStr_2(TestVerID);

                    Scattered.TestVerID = TestVerID;
                    DataTable dt = Scattered.GetObjectName();

                    if (dt.Rows.Count == 0)
                    {
                        db.MoveToMergeField(TextStartField);
                        db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                        db.Writeln("无。");
                    }
                    else
                    {

                        NodeTree nodetree = new NodeTree();
                        nodetree.TestVerID = TestVerID;
                        DataTreeList = nodetree.PutNodeToLayerList();

                        db.MoveToMergeField(TextStartField);

                        db.ParagraphFormat.Style = doc.Styles["标题 2"];
                        db.Writeln(TestVerStr);

                        OutputComm.OutputComm.AddField(TextStartField, db);

                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            DataRow dr = dt.Rows[i];
                            string ObjectName = dr["被测对象名称"].ToString();
                            string ObjectID = dr["被测对象实测ID"].ToString();
                            string ObjectID_body = dr["被测对象实体ID"].ToString();

                            DataTable dt1 = GD_TestCase();                                      
                            Document doc1 = OutputComm.OutputComm.AddTableUnderChapter(TextStartField, db, ObjectName, dt1, Section, CurrentDoc, IfTiSheng, "标题 3");
                            
                            if (dt1.Rows.Count > 0)
                            {
                                TableNo = TableNo + 1;

                                DocumentBuilder db1 = new DocumentBuilder(doc1);
                                OutputComm.OutputComm.ReplaceTableName("与被测对象有关", TableNo, ObjectName, db1);

                                dt1.TableName = "回归测试更动一览";
                                FillTable(doc1, dt1, false, "");

                                Table table = OutputComm.OutputComm.GetNodeInTable(BSField, doc1, null);
                                MergeTable(table, OutputType, dt1, doc1);                               
                                OutputComm.OutputComm.ChangeChapterEndProcess("准备替换", doc1, doc, null, db);
                                
                            }
                            else
                            {
                                db.MoveToMergeField("准备替换");
                                db.ParagraphFormat.Style = doc.Styles["文本首行缩进"];
                                db.Writeln("无。");

                            }
                        }

                    }
                }

                Node node1 = OutputComm.OutputComm.GetNodeByField(doc, TextStartField, db);
                if (node1 != null)
                {
                    node1.Remove();
                }
                Section.Remove();

            }
        }
        public string GetGDType(string type)
        {
            string GDType = "";
            if (type == "1")
            {
                GDType = "纠错性更动";
            }
            else if (type == "2")
            {
                GDType = "适应性更动";
            }
            else if (type == "3")
            {
                GDType = "完善性更动";
            }
            else if (type == "4")
            {
                GDType = "预防性更动";
            }
            else if (type == "5")
            {
                GDType = "其他更动";
            }
            return GDType;
        }

      //  public DataTable GD_TestCase()
      //  {

      //      DataTable GD_TestCase_1 = new DataTable();
      //      GD_TestCase_1 = Scattered.AddTableColumn("回归测试更动情况", "");

      //      int xuhao = 0;
      //      DataRow dr = null;

      //      string sqlstate1 = "select * from HG软件更动表 where 项目ID = ? and 测试版本= ? order by 更动类型,序号";
      //      DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

      //      if (dt1 != null && dt1.Rows.Count != 0)
      //      {
      //          foreach (DataRow dr1 in dt1.Rows)
      //          {
      //              bool IfChange1 = (bool)dr1["是否更动"];
      //              string QuestionName = dr1["问题单名称"].ToString();
      //              string QuestionBS = dr1["问题单标识"].ToString();
      //              string GDType = dr1["更动类型"].ToString();
                    
      //              if (IfChange1 == true)
      //              {
      //                  ArrayList TestCaseList = Scattered.AllTestCase_MouGD(DataTreeList, dr1["ID"].ToString());

      //                  if (TestCaseList.Count > 0)
      //                  {
      //                      xuhao = xuhao + 1;
      //                      for (int i = 0; i <= TestCaseList.Count - 1; i++)
      //                      {
      //                          dr = GD_TestCase_1.Rows.Add();

      //                          dr["序号"] = xuhao;
      //                          dr["软件问题"] = QuestionBS;
      //                          dr["是否更动"] = "是";
      //                          dr["更动类型"] = GetGDType(GDType);
      //                          dr["更动标识"] = dr1["更动标识"].ToString();
      //                          dr["更动说明"] = dr1["更动说明"].ToString();               
      //                          dr["用例名称"] = TestCaseList[i].ToString();

      //                      }
      //                  }
      //                  else
      //                  {
      //                      dr = GD_TestCase_1.Rows.Add();
      //                      xuhao = xuhao + 1;
      //                      dr["序号"] = xuhao;
      //                      dr["软件问题"] = QuestionBS;
      //                      dr["是否更动"] = "是";
      //                      dr["更动类型"] = GetGDType(GDType);
      //                      dr["更动标识"] = dr1["更动标识"].ToString();
      //                      dr["更动说明"] = dr1["更动说明"].ToString();
      //                      dr["用例名称"] = "";

      //                  }

      //              }
      //              else
      //              {
      //                  dr = GD_TestCase_1.Rows.Add();
      //                  xuhao = xuhao + 1;
      //                  dr["序号"] = xuhao;
      //                  dr["软件问题"] = QuestionBS;
      //                  dr["是否更动"] = "否";
      //                  dr["更动类型"] = "";
      //                  dr["更动标识"] = "";
      //                  dr["更动说明"] = dr1["未更动说明"].ToString();
      //                  dr["用例名称"] = "";
                      
      //              }
                   
      //          }
      //      }

      //      return GD_TestCase_1;

      //}

        public DataTable GD_TestCase()
        {

            DataTable GD_TestCase_1 = new DataTable();
            GD_TestCase_1 = Scattered.AddTableColumn("回归测试更动情况", "");

            int xuhao = 0;
            DataRow dr = null;

            string sqlstate1 = "select * from HG软件更动表 where 项目ID = ? and 测试版本= ? order by 更动类型,序号";
            DataTable dt1 = TPM3.Sys.MyBaseForm.dbProject.ExecuteDataTable(sqlstate1, TPM3.Sys.GlobalData.globalData.projectID.ToString(), TestVerID);

            if (dt1 != null && dt1.Rows.Count != 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    bool IfChange1 = (bool)dr1["是否更动"];
                    //string QuestionName = dr1["问题单名称"].ToString();
                    //string QuestionBS = dr1["问题单标识"].ToString();
                    //string GDType = dr1["更动类型"].ToString();

                    if (IfChange1 == true)
                    {
                        ArrayList TestCaseList = Scattered.AllTestCase_MouGD(DataTreeList, dr1["ID"].ToString());

                        if (TestCaseList.Count > 0)
                        {
                            xuhao = xuhao + 1;
                            for (int i = 0; i <= TestCaseList.Count - 1; i++)
                            {
                                dr = GD_TestCase_1.Rows.Add();

                                dr["序号"] = xuhao;
                             //   dr["软件问题"] = QuestionBS;
                            //    dr["是否更动"] = "是";
                            //    dr["更动类型"] = GetGDType(GDType);
                                dr["更动标识"] = dr1["更动标识"].ToString();
                                dr["更动说明"] = dr1["更动说明"].ToString();
                                dr["用例名称"] = TestCaseList[i].ToString();

                            }
                        }
                        else
                        {
                            dr = GD_TestCase_1.Rows.Add();
                            xuhao = xuhao + 1;
                            dr["序号"] = xuhao;
                         //   dr["软件问题"] = QuestionBS;
                          //  dr["是否更动"] = "是";
                         //   dr["更动类型"] = GetGDType(GDType);
                            dr["更动标识"] = dr1["更动标识"].ToString();
                            dr["更动说明"] = dr1["更动说明"].ToString();
                            dr["用例名称"] = "";

                        }

                    }
                    //else
                    //{
                    //    dr = GD_TestCase_1.Rows.Add();
                    //    xuhao = xuhao + 1;
                    //    dr["序号"] = xuhao;
                    //    dr["软件问题"] = QuestionBS;
                    //    dr["是否更动"] = "否";
                    //    dr["更动类型"] = "";
                    //    dr["更动标识"] = "";
                    //    dr["更动说明"] = dr1["未更动说明"].ToString();
                    //    dr["用例名称"] = "";

                    //}

                }
            }

            return GD_TestCase_1;

        }
        
    }
}
