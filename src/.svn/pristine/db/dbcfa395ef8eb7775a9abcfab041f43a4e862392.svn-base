using System;
using System.Collections;
using System.Text;

namespace TPM3.chq
{
    public class QuestionInfo
    {       
            public string QuestionID;
            public string QuestionBS;
            public string QuestionType;
            public string QuestionLevel;
            public string TestCaseID;
            public string TestCaseName;
            public string TestCaseBS;

            public ArrayList AddQuestionToArray(ArrayList QuestionInfoList, string QuestionID, string QuestionBS, string QuestionType, string QuestionLevel, string TestCaseID, string TestCaseName, string TestCaseBS)
            {

                QuestionInfo QuestionInfoNode = new QuestionInfo();

                QuestionInfoNode.QuestionID = QuestionID;
                QuestionInfoNode.QuestionBS = QuestionBS;
                QuestionInfoNode.QuestionType = QuestionType;
                QuestionInfoNode.QuestionLevel = QuestionLevel;
                QuestionInfoNode.TestCaseID = TestCaseID;
                QuestionInfoNode.TestCaseName = TestCaseName;
                QuestionInfoNode.TestCaseBS = TestCaseBS;

                QuestionInfoList.Add(QuestionInfoNode);

                return QuestionInfoList;

            }
       
    }
}
