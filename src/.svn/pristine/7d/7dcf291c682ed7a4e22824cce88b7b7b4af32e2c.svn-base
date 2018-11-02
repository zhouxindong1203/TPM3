using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using Common.Database;
using Common;
using TPM3.wx;

namespace TPM3.Sys
{
    public partial class MyBaseForm : Form, IBaseTreeForm
    {
        public MyBaseForm()
        {
            InitializeComponent();
        }

        public virtual bool OnPageCreate()
        {
            return true;
        }

        public virtual bool OnPageClose(bool bClose)
        {
            return true;
        }

        public virtual bool OnPageCreate(object key, TreeNode tn)
        {
            return true;
        }

        public static DBAccess dbProject
        {
            get { return globalData.dbProject; }
        }

        public static GlobalData globalData
        {
            get { return GlobalData.globalData; }
        }

        public static object pid
        {
            get { return globalData.projectID; }
        }

        public static object currentvid
        {
            get { return globalData.currentvid; }
        }

        /// <summary>
        /// 返回当前页面的版本模式:准备还是执行
        /// </summary>
        public VersionMode versionMode
        { 
            get { return DBLayer1.GetVersionMode(dbProject, pid, currentvid); } 
        }

        public static bool IsNull(object o)
        {
            return GridAssist.IsNull(o);
        }

        public DataTable GetPersonTable()
        {
            return DBLayer1.GetPersonList(dbProject, pid, currentvid);
        }

        public static string GetPersonName(DataTable dtPerson, object key)
        {
            return GridAssist.GetMultiDisplayString(dtPerson, "ID", "姓名", key, ",");
        }

        /// <summary>
        /// 该窗口对应的树节点ID
        /// </summary>
        public TreeNode tnForm;

        protected DocTreeNode docTN
        {
            get { return tnForm as DocTreeNode; }
        }

        protected string docName
        {
            get { return docTN.documentName; }
        }

        protected object baseFormKey
        {
            get { return tnForm == null ? null : tnForm.Tag; }
        }

        public static UserSetting us
        {
            get { return UserSetting.Default; }
        }

        /// <summary>
        /// 页面所属的文档
        /// </summary>
        public string formDocType = null;

        /// <summary>
        /// 窗体URL
        /// </summary>
        public string baseFormUrl;

        /// <summary>
        /// 窗体参数列表
        /// </summary>
        public Dictionary<string, string> paramList = new Dictionary<string, string>();

        /// <summary>
        /// 窗口类名称
        /// </summary>
        public string formClass;

        /// <summary>
        /// 设置窗体字符串
        /// </summary>
        public void SetFormUrl(string url)
        {
            this.baseFormUrl = url;
            this.paramList = FormClass.GetParamsFromUrl(url);
            this.formClass = FormClass.GetClassNameFromUrl(url);
        }
    }
}