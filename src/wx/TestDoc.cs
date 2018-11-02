using System;
using System.Data;

namespace TPM3.wx
{
    class TestDoc
    {
        public void GenDoc()
        {
            DataTable dt = TestCaseSummary.GetCountTable(null);
        }
    }
}
