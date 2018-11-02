using System;
using System.Windows.Forms;

namespace TPM3.wx
{
    public partial class TestProjectSummary : MyUserControl
    {
        public TestProjectSummary()
        {
            InitializeComponent();
        }

        public override bool OnPageCreate()
        {
            TestCaseSummary1.summary = summary;
            FallMatrixTable1.OnPageCreate();
            TestCaseSummary1.OnPageCreate();
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            FallMatrixTable1.OnPageClose(bClose);
            TestCaseSummary1.OnPageClose(bClose);
            return true;
        }
    }
}
