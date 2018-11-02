using System;
using System.Data;
using System.Collections.Generic;
using Common;
using TPM3.Sys;
using NodeType = Z1.tpm.NodeType;

namespace TPM3.wx
{
    public class ResultSummaryVisitClass : BaseProjectClass
    {
        /// <summary>
        /// 0:  总用例数，
        /// 1:  执行数
        /// 2:  部分执行数，
        /// 3:  未执行数，
        /// 4:  执行通过， 
        /// 5:  执行未通过，
        /// 6:  部分执行通过，
        /// 7:  部分执行未通过，
        /// 8:  独立的用例数，
        /// 9:  条目数
        /// 10: 步骤数
        /// </summary>
        public int[] counts = new int[11];

        public bool includeShortcut = false;

        public Dictionary<object, int> stepCountMap;

        /// <summary>
        /// 当测试项下面的所有用例都为“通过”的情况下，为“符合”；
        /// 当至少有一个测试用例为“不通过”时，为“不符合” ；
        /// 当存在已执行测试用例为“通过”且存在“未执行”或“部分执行”时，为“待确定”。
        /// 如果叶节点的测试项下没有任何测试用例，测试项的状态为“待确定”。
        /// 0: 无用例。1: 未通过。 2:待确定。 3:通过
        /// </summary>
        public int TestResult = 0;

        public KeyList personList = new KeyList();

        /// <summary>
        /// 测试项计数是否可以加一
        /// </summary>
        static bool CanAddItemCount(ItemNodeTree item)
        {
            if(ProjectConfigData.GetSummaryItemType() == SummaryItemType.All)
                return true;
            // 判断有没有下级的测试项。如果有则不加
            foreach(var subitem in item.childlist)
                if(subitem.nodeType == NodeType.TestItem)
                    return false;
            return true;
        }

        public void GetCaseCount(ItemNodeTree item)
        {
            if(item.nodeType == NodeType.TestItem)
            {
                if(CanAddItemCount(item))
                    counts[9]++;
            }
            if(item.nodeType != NodeType.TestCase) return;
            DataRow dr = item.dr;

            string pass = dr["执行结果"] as string;
            string execute = dr["执行状态"] as string;

            counts[0]++;
            if(TestResult == 0 || TestResult == 3)
            {
                TestResult = 3;
                if(execute == "未执行")
                    TestResult = 2;
                else if(execute == "完整执行")
                {
                    if(pass == "通过") TestResult = 3;
                    else TestResult = 1;
                }
                else if(execute == "部分执行")
                {
                    if(pass == "未通过") TestResult = 1;
                    else TestResult = 2;
                }
            }
            if(TestResult == 2)
            {
                if(execute == "完整执行" && pass != "通过")
                    TestResult = 1;
                if(execute == "部分执行" && pass == "未通过")
                    TestResult = 1;
            }

            if(!item.IsShortCut) counts[8]++;

            if(item.IsShortCut && !includeShortcut) return;

            if(execute == "未执行")
                counts[3]++;
            else if(execute == "完整执行")
            {
                counts[1]++;
                if(pass == "通过") counts[4]++;
                else counts[5]++;
            }
            else if(execute == "部分执行")
            {
                counts[2]++;
                if(pass == "未通过") counts[7]++;
                else counts[6]++;
            }

            if(stepCountMap != null && stepCountMap.ContainsKey(item.id))
                counts[10] += stepCountMap[item.id];
            personList.AddKeyList(dr["测试人员"]);
        }

        public string GetSummaryString()
        {
            string s = "该被测对象共设计了{0}个测试用例；\r\n执行的测试用例个数是{1}；\r\n执行通过的测试用例个数是{4}；\r\n执行未通过的测试用例个数是{5}；\r\n部分执行的测试用例个数是{2}；\r\n部分执行通过的测试用例个数是{6}；\r\n部分执行未通过的测试用例个数是{7}；\r\n未执行的测试用例个数是{3}。";
            string msg = string.Format(s, counts[0], counts[1], counts[2], counts[3], counts[4], counts[5], counts[6], counts[7]);
            return msg;
        }

        public DataTable GetTestPersonTable()
        {
            DataTable dt = DBLayer1.GetPersonList(dbProject, pid, currentvid);
            if(dt == null) return null;
            string pl = personList.ToString();
            foreach(DataRow dr in dt.Rows)
            {
                if(!KeyList.IsKeyExist(pl, dr["ID"]))
                    dr.Delete();
            }
            dt.AcceptChanges();
            return dt;
        }

        public DateTime? testBeginTime = null, testEndTime = null;

        /// <summary>
        /// 获取测试起始-结束时间
        /// </summary>
        public void GetTestTime(ItemNodeTree item)
        {
            if(item.nodeType != NodeType.TestCase) return;
            // 未执行的用例不参加判断
            if("未执行".Equals(item.dr["执行状态"])) return;

            if(GridAssist.IsNull(item.dr["测试时间"])) return;
            DateTime testTime = (DateTime)item.dr["测试时间"];
            if(testBeginTime == null || testTime < testBeginTime.Value)
                testBeginTime = testTime;
            if(testEndTime == null || testTime > testEndTime.Value)
                testEndTime = testTime;
        }
    }
}
