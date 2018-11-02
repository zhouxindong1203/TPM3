using System;
using System.Windows.Forms;
using Common;

namespace TPM3.Sys
{
    public partial class LeftTreeUserControl : MyBaseForm
    {
        /// <summary>
        /// 当前打开的页面
        /// </summary>
        protected Control currentSelectedForm = null;

        /// <summary>
        /// 保存并关闭当前打开的页面
        /// </summary>
        /// <param name="bClose">false:仅保存,不关闭。 true:保存成功后关闭</param>
        /// <returns>成功返回true</returns>
        public bool CloseCurrentForm(bool bClose, bool bForceClose)
        {
            if( currentSelectedForm == null ) return true;
            IBaseTreeForm bf = currentSelectedForm as IBaseTreeForm;
            if( bf != null )  // 普通窗口，不需要保存
            {
                bool ret = bf.OnPageClose(bClose);
                if( ret == false && bForceClose )
                    ret = ConfirmQuit();   // 是否强行关闭窗口
                if( ret == false ) return false;
            }

            // 如果关闭页面成功
            if( bClose == true )
            {
                currentSelectedForm.Hide();
                if( currentSelectedForm is Form )
                    (currentSelectedForm as Form).Close();

                currentSelectedForm = null;
            }
            return true;
        }

        public static bool ConfirmQuit()
        {
            string s = "保存页面时发生错误，关闭该窗口将丢失所做的修改，确定要关闭窗口吗？";
            DialogResult dr = MessageBox.Show(s, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if( dr == DialogResult.Yes )
                return true;   // 强行关闭窗口
            return false;
        }

        public virtual void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if( !InvokeEvent ) return;

            // 如果关闭不成功，则取消选择
            if( CloseCurrentForm(true, true) == false )
            {
                e.Cancel = true;
                return;
            }

            OnTreeNodeSelected(e.Node);
        }

        bool InvokeEvent = true;

        /// <summary>
        /// 设置树的选中节点，不触发选中事件
        /// </summary>
        protected void SetSelectNode(TreeNode tn2)
        {
            InvokeEvent = false;
            tn2.TreeView.SelectedNode = tn2;
            InvokeEvent = true;
        }

        protected virtual void OnTreeNodeSelected(TreeNode tn2)
        {
            // 获取要在右边显示的窗口
            Control f = GetSubForm(tn2);
            ShowControl(f, tn2);
        }

        public void ShowControl(Control f, TreeNode tn2)
        {
            if( f == null ) return;

            //if( f.Created == false )
            //{
            //    bool v = f.Visible;
            //    //c.Show();
            //    f.Visible = true;
            //    f.Visible = v;
            //}

            IBaseTreeForm ibf = f as IBaseTreeForm;
            bool ret = true;
            if( ibf != null )    // 初始化
            {
                if( ibf is MyBaseForm )
                    (ibf as MyBaseForm).tnForm = tn2;
                ret = ibf.OnPageCreate();
            }

            // 如果初始化成功，则显示之
            if( ret == true )
            {
                OnShowControl(tn2, f);
                currentSelectedForm = f;
            }

            f.Focus();
        }

        public virtual void OnShowControl(TreeNode tn, Control c)
        {
            OnShowForm(tn, c as Form);
        }

        public virtual void OnShowForm(TreeNode tn, Form f)
        {
            f.Show();
        }

        public virtual Control GetSubForm(TreeNode tn)
        {
            return null;
        }

        //////////////////////////////////////////////////////////////
        //
        // below added by zhouxindong

        public Control CurrentSelectedForm
        {
            get
            {
                return currentSelectedForm;
            }
        }

        public override bool OnPageClose(bool bClose)
        {
            IBaseTreeForm bf = currentSelectedForm as IBaseTreeForm;
            if( bf != null )  // 普通窗口，不需要保存
                return bf.OnPageClose(bClose);
            return true;
        }
    }

    interface INoCloseWindow
    {
    }
}