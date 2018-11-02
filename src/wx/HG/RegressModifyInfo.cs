using Common;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// 软件更动说明，包括4个部分
    /// </summary>
    [TypeNameMap("wx.RegressModifyInfo")]
    public partial class RegressModifyInfo : MyBaseForm
    {
        public RegressModifyInfo()
        {
            InitializeComponent();
            flex0.modifyType = "1";  // 纠错性更动
            flex1.modifyType = "2";  // 适应性更动
            flex2.modifyType = "3";  // 完善性功能
            flex3.modifyType = "4";  // 预防性更动
            flex4.modifyType = "5";  // 其他更动
        }

        public override bool OnPageCreate()
        {
            flex0.OnPageCreate();
            flex1.OnPageCreate();
            flex2.OnPageCreate();
            flex3.OnPageCreate();
            flex4.OnPageCreate();
            return true;
        }

        public override bool OnPageClose(bool bClose)
        {
            flex0.OnPageClose();
            flex1.OnPageClose();
            flex2.OnPageClose();
            flex3.OnPageClose();
            flex4.OnPageClose();
            return true;
        }
    }
}