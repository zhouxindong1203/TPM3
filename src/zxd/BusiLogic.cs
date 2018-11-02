using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Common;
using Common.Database;
using TPM3.Sys;
using TPM3.wx;
using TPM3.zxd.clu;
using TpmCoreData;
using Z1.tpm;
using Z1.tpm.DB;
using Z1Utils;
using Z1Utils.Controls;
using ProjectInfo = Z1.tpm.DB.ProjectInfo;

namespace TPM3.zxd
{
    /*
     * TPM3.zxd.BusiLogic
     * 测试过程管理工具业务逻辑功能
     * zhouxindong 2008/10/23
     */
    public class BusiLogic
    {
        #region 树节点类型级数检测

        // 检测测试类型节点总级数
        public static int GetTestTypeLevel(TreeNode node)
        {
            int i = 0;
            NodeTagInfo taginfo = node.Tag as NodeTagInfo;
            if(taginfo == null)
                return i;

            while(taginfo.nodeType == NodeType.TestType)
            {
                i++;
                node = node.Parent;
                taginfo = node.Tag as NodeTagInfo;
                if(taginfo == null)
                    break;
            }

            return i;
        }

        // 检测测试类型+测试项节点的总级数
        public static int GetTypeItemLevel(TreeNode node)
        {
            int i = 0;
            NodeTagInfo taginfo = node.Tag as NodeTagInfo;
            if(taginfo == null)
                return i;

            while((taginfo.nodeType == NodeType.TestType) ||
                (taginfo.nodeType == NodeType.TestItem))
            {
                i++;
                node = node.Parent;
                taginfo = node.Tag as NodeTagInfo;
                if(taginfo == null)
                    break;
            }

            return i;
        }

        // 获取节点node所在的级数(符合类型集为set)
        public static int GetLevels(TreeNode node, List<NodeType> set)
        {
            int i = 0;
            NodeTagInfo taginfo = node.Tag as NodeTagInfo;
            if(taginfo == null)
                return i;

            while(set.Contains(taginfo.nodeType))
            {
                i++;
                node = node.Parent;
                taginfo = node.Tag as NodeTagInfo;
                if(taginfo == null)
                    break;
            }

            return i;
        }

        // nodestart 到 nodeend之间的级数(由下至上)
        public static int GetLevels(TreeNode nodestart, TreeNode nodeend)
        {
            TreeNode temnode = nodestart;
            int level = 1;
            while(temnode != nodeend)
            {
                level++;
                temnode = temnode.Parent;
            }

            return level;
        }

        #region 由上至下检测级数

        // 获取节点之下包括的级数(包括本身, 不包括测试用例级)(测试项级)
        public static int GetDownLevel(TreeNode node)
        {
            int maxlevel = 1;
            GetAllChilds(node, NodeType.TestItem);
            foreach(TreeNode n in _nodes)
            {
                int level = GetLevels(n, node);
                if(level > maxlevel)
                    maxlevel = level;
            }

            return maxlevel;
        }

        // 此处是返回所有指定类型的节点, 应该是返回所有指定类型的叶节点
        // 正确性没有影响, 会影响运行效率, 方便时改之!(扩充类库)
        private static NodeType _nodetype;
        private static List<TreeNode> _nodes;
        private static void GetAllChilds(TreeNode node, NodeType nodetype)
        {
            _nodetype = nodetype;
            _nodes = new List<TreeNode>();

            TreeViewUtils tvu = new TreeViewUtils();
            Z1Utils.Controls.EnumTreeViewProc proc = new EnumTreeViewProc(SearchIt1);
            tvu.FindNodeFromNode(node, proc);
        }

        private static bool SearchIt1(TreeNode tn)
        {
            NodeTagInfo tag = tn.Tag as NodeTagInfo;
            if(tag == null)
                return true;
            if(tag.nodeType == _nodetype)
                _nodes.Add(tn);

            return true;
        }

        #endregion 由上至下检测级数

        #endregion 树节点类型级数检测

        #region 赋值

        // 从数据库表中读取文本值
        public static string GetStringFromDB(object ori)
        {
            if(!DBNull.Value.Equals(ori))
                return (string)ori;
            else
                return string.Empty;
        }

        // 从数据库表中读取时间值(为空时赋值为日期最小值)
        public static DateTime GetDateTimeFromDB(object ori)
        {
            if(!DBNull.Value.Equals(ori))
                return (DateTime)ori;
            else
                return DateTime.MinValue;
        }

        public static void GetTestTime(DateTimePicker dtp, object ori)
        {
            if (!DBNull.Value.Equals(ori))
            {
                dtp.Checked = true;
                dtp.Value = (DateTime)ori;
            }
            else
                dtp.Checked = false;
        }

        public static DateTime SetTestTime(DateTimePicker dtp)
        {
            if (dtp.Checked)
                return dtp.Value;
            else
                return DateTime.MinValue;
        }

        #endregion 赋值

        #region 用例标识

        // 生成测试用例标识
        public static string GenUCSign(TreeNode node)
        {
            TreeNode t = node;
            StringBuilder strbld = new StringBuilder();
            NodeTagInfo tag = t.Tag as NodeTagInfo;
            if(tag == null)
                return strbld.ToString();

            strbld.Append(tag.keySign);

            t = t.Parent;
            tag = t.Tag as NodeTagInfo;
            while(tag.nodeType != NodeType.Project)
            {
                strbld.Insert(0, ConstDef.UCSignSpl);
                strbld.Insert(0, tag.keySign);
                t = t.Parent;
                tag = t.Tag as NodeTagInfo;
            }
            return strbld.ToString();
        }

        #endregion 用例标识

        #region 数据库字段缺省值

        // "CA测试项实体表" -> "终止条件"
        public const string TermiCondi = "满足测试要求或测试过程无法正常进行";

        // "CA测试用例实体表" -> "测试过程终止条件"
        public const string StepTermiCondi = "本测试用例的全部测试步骤被执行或因某种原因导致测试步骤无法执行(异常终止)";

        // "CA测试用例实体表" -> "测试结果评估标准"
        public const string Evaluation = "本测试用例的全部测试步骤都通过即标志本用例为\"通过\"";

        #endregion 数据库字段缺省值


        #region 测试用例"执行状态"和"执行结果"更新

        // 某测试用例添加新步骤后
        public static void UCAfterAddNewStep(string uctid, ref string execsta, ref string execrlt, TreeNode node)
        {
            // 更新测试用例的执行状态和执行结果
            // ("完整执行"且"通过") -> ("部分执行"且"")
            if((execsta.Equals(ConstDef.execsta[2])) &
                (execrlt.Equals(ConstDef.execrlt[1])))
            {
                execsta = ConstDef.execsta[1];

                UIFunc.SetNodeImageKey(node, MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecute],
                    MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecute]);

                // 同步更新所有此用例的快捷方式
                FrmCommonFunc.UpdateShortcutIcons(uctid, ExecuteStatus.PartExecute_k, node.TreeView);
            }

            if((execsta.Equals(ConstDef.execsta[2])) &
                (execrlt.Equals(ConstDef.execrlt[2])))
            {
                execsta = ConstDef.execsta[1];

                UIFunc.SetNodeImageKey(node, MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecutep],
                    MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecutep]);

                FrmCommonFunc.UpdateShortcutIcons(uctid, ExecuteStatus.PartExecutep_k, node.TreeView);
            }
        }

        // 删除某测试步骤后更新用测的"执行状态"和"执行结果"(tbl之前已经AcceptChanges)
        public static void UpdateAfterDelStep(string uctid, ref string execsta, ref string execrlt, DataTable tbl, TreeNode node)
        {
            execsta = CheckExecSta(tbl);
            execrlt = CheckExecRlt(execsta, tbl);

            UpdateUCIcon(uctid, execsta, execrlt, node);
        }

        // 检测"执行状态"(tbl之前已经AcceptChanges)
        public static string CheckExecSta(DataTable tbl)
        {
            int total = 0;
            int exec = 0;
            int unexec = 0;

            if(tbl.Rows.Count == 0)
                return ConstDef.execsta[0];

            total = tbl.Rows.Count;
            foreach(DataRow row in tbl.Rows)
            {
                if((DBNull.Value.Equals(row["实测结果"])) ||
                    (string.Empty.Equals((string)row["实测结果"])))
                {
                    if(!TestUsecase.ActualTestHasAcc(MyBaseForm.dbProject, (string)row["ID"]))
                        unexec++;
                    else
                        exec++;
                }
                else
                    exec++;
            }

            if(total == unexec)
                return ConstDef.execsta[0];
            else if(total == exec)
                return ConstDef.execsta[2];
            else
                return ConstDef.execsta[1];
        }

        // 检测"执行状态"(newtext为新的"实测结果"文本)(tbl之前并没有AcceptChanges)
        public static string CheckExecSta(DataTable tbl, string steptid, string newtext)
        {
            DataTable tblcopy = tbl.Copy();
            tblcopy.AcceptChanges();
            string filter = string.Format("ID=\'{0}\'", steptid);
            DataRow[] rows = tblcopy.Select(filter);
            if((rows == null) || (rows.Length == 0))
                throw (new ArgumentException("数据查询无记录返回!!"));

            rows[0]["实测结果"] = newtext;

            return CheckExecSta(tblcopy);
        }

        // 检测"执行结果"(tbl之前已经AcceptChanges)
        public static string CheckExecRlt(string execsta, DataTable tbl)
        {
            int total = 0;
            int pass = 0;
            int unpass = 0;

            if(tbl.Rows.Count == 0)
                return string.Empty;

            total = tbl.Rows.Count;

            foreach(DataRow row in tbl.Rows)
            {
                if((DBNull.Value.Equals(row["问题报告单ID"])) ||
                    (string.Empty.Equals((string)row["问题报告单ID"])))
                    pass++;
                else
                    unpass++;
            }

            if(unpass != 0)
                return ConstDef.execrlt[2];

            switch(execsta)
            {
                case ConstDef.execsta0:
                    return string.Empty;

                case ConstDef.execsta1:
                    return ConstDef.execrlt[1];

                case ConstDef.execsta2:
                    return ConstDef.execrlt[1];

                default:
                    return string.Empty;
            }
        }

        // 根据用例的"执行状态"和"执行结果"更新用例树节点的图标及所有快捷方式图标
        public static void UpdateUCIcon(string uctid, string execsta, string execrlt, TreeNode node)
        {
            // 更新用例图标显示状态
            switch(execsta)
            {
                case ConstDef.execsta0:
                    UIFunc.SetNodeImageKey(node, MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.NonExecute],
                        MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.NonExecute]);
                    FrmCommonFunc.UpdateShortcutIcons(uctid, ExecuteStatus.NonExecute_k, node.TreeView);

                    break;

                case ConstDef.execsta1:
                    if(execrlt.Equals(ConstDef.execrlt[1]))
                    {
                        UIFunc.SetNodeImageKey(node, MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecute],
                            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecute]);
                        FrmCommonFunc.UpdateShortcutIcons(uctid, ExecuteStatus.PartExecute_k, node.TreeView);
                    }
                    else if(execrlt.Equals(ConstDef.execrlt[2]))
                    {
                        UIFunc.SetNodeImageKey(node, MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecutep],
                            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecutep]);
                        FrmCommonFunc.UpdateShortcutIcons(uctid, ExecuteStatus.PartExecutep_k, node.TreeView);
                    }

                    break;

                case ConstDef.execsta2:
                    if(execrlt.Equals(ConstDef.execrlt[1]))
                    {
                        UIFunc.SetNodeImageKey(node, MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executed],
                            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executed]);
                        FrmCommonFunc.UpdateShortcutIcons(uctid, ExecuteStatus.Executed_k, node.TreeView);
                    }
                    else if(execrlt.Equals(ConstDef.execrlt[2]))
                    {
                        UIFunc.SetNodeImageKey(node, MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executedp],
                            MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.Executedp]);
                        FrmCommonFunc.UpdateShortcutIcons(uctid, ExecuteStatus.Executedp_k, node.TreeView);
                    }

                    break;
            }
        }

        #endregion 测试用例"执行状态"和"执行结果"更新

        #region 测试用例对应树节点定位

        private static string _findcaseid;
        private static bool _expanednode;
        private static TreeView _tree;
        /// <summary>
        /// 定位到某个测试用例树节点
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="uctid"></param>
        /// <param name="expanedNode"></param>
        public static void FindUsecaseNode(TreeView tree, string uctid, bool expanedNode)
        {
            _findcaseid = uctid;
            _expanednode = expanedNode;
            _tree = tree;

            TreeViewUtils tvu = new TreeViewUtils();
            Z1Utils.Controls.EnumTreeViewProc proc = new EnumTreeViewProc(SearchIt);
            tvu.FindTreeViewLeaf(tree, proc);
        }

        private static bool SearchIt(TreeNode tn)
        {
            NodeTagInfo tag = tn.Tag as NodeTagInfo;
            if(tag == null)
                return true;

            if(_findcaseid.Equals(tag.id))
                if(!tag.IsShortcut)
                {
                    _tree.SelectedNode = tn;
                    if(_expanednode)
                        tn.EnsureVisible();
                    return false;
                }

            return true;
        }

        // 检测树节点是否为测试用例快捷方式(无法判断时返回true)
        public static bool NodeIsShortcut(TreeNode node)
        {
            NodeTagInfo tag = node.Tag as NodeTagInfo;
            if(tag == null)
                return true;

            return tag.IsShortcut;
        }

        #endregion 测试用例对应树节点定位


        #region 主窗体树节点定位

        // 在主窗体上选中某个树节点(采用url定位方式, 此方法不用)
        public static TreeNode SelectMainFrmNode(/*ref TreeView tree, */string frmname, string frmtitle1)
        {
            TreeNode returnNode = null;

            foreach(TreeNode node in MainForm.mainFrm.treeView1.Nodes)
            {
                if(returnNode != null)
                    break;

                DocTreeNode docnode = node as DocTreeNode;
                if(docnode == null)
                    continue;

                if(docnode.nodeClass.Equals(frmname) &&
                    (docnode.nodeElement.GetAttribute("title1").Equals(frmtitle1)))
                {
                    returnNode = node;
                    break;
                }

                // 遍历子节点
                foreach(TreeNode node1 in node.Nodes)
                {
                    if(returnNode != null)
                        break;

                    RecurseFindNode(node1, ref returnNode, frmname, frmtitle1);
                }

            }

            if(returnNode != null)
            {
                MainForm.mainFrm.treeView1.SelectedNode = returnNode;
                returnNode.EnsureVisible();
            }

            return returnNode;
        }

        private static void RecurseFindNode(TreeNode node, ref TreeNode returnNode, string frmname, string frmtitle1)
        {
            DocTreeNode docnode = node as DocTreeNode;
            if(docnode != null)
            {
                string nc = docnode.nodeClass;
                string title1 = docnode.nodeElement.GetAttribute("title1");
                if(nc.Equals(frmname) &&
                    (title1.Equals(frmtitle1)))
                {
                    returnNode = node;
                    return;
                }

                if(node.Nodes.Count > 0)
                {
                    foreach(TreeNode n in node.Nodes)
                    {
                        RecurseFindNode(n, ref returnNode, frmname, frmtitle1);
                    }
                }
                return;
            }
        }


        #endregion 主窗体树节点定位

        #region 文件及目录

        // 创建附件文件夹
        public static bool CreateAccFolder(DBAccess db, string pid, string vid)
        {
            if(ExecStatus.g_AccFolder.Equals(string.Empty)) // 无附件文件夹信息
            {
                FolderBrowserDialog odlgFolder = new FolderBrowserDialog();
                try
                {
                    odlgFolder.RootFolder = Environment.SpecialFolder.MyComputer;
                    odlgFolder.Description = "选择存放附件的文件夹";

                    if((odlgFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                    {
                        ExecStatus.g_AccFolder = odlgFolder.SelectedPath;
                        ProjectInfo.InsertAccFolderInfo(db, pid, vid, ExecStatus.g_AccFolder);
                    }
                    else
                        return false;
                }
                catch(Exception)
                {
                    return false;
                }
            }

            if(!Directory.Exists(ExecStatus.g_AccFolder))
            {
                try
                {
                    Directory.CreateDirectory(ExecStatus.g_AccFolder);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("附件文件夹创建失败!\n" + ex.Message, "操作失败", MessageBoxButtons.OK,
                         MessageBoxIcon.Error);
                    ExecStatus.g_AccFolder = string.Empty;
                    ProjectInfo.SetTextContent(db, pid, vid, "项目信息", "附件文件夹", string.Empty);
                    return false;
                }
            }

            ProjectInfo.SetTextContent(db, pid, vid, "项目信息", "附件文件夹", ExecStatus.g_AccFolder);
            return true;
        }

        // 复制文件
        public static bool CopyFile(string srcfile, string desfile)
        {
            if(!File.Exists(srcfile))
            {
                MessageBox.Show("源文件不存在!", "复制文件失败", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            try
            {
                if(File.Exists(desfile))
                {
                    DialogResult usersel = MessageBox.Show("目标文件已经存在!\n是否替换?(Y/N)", "目标文件存在", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                    switch(usersel)
                    {
                        case DialogResult.OK:
                            File.Copy(srcfile, desfile, true);
                            return true;

                        case DialogResult.Cancel:
                            return false;
                    }
                }
                else
                {
                    File.Copy(srcfile, desfile, true);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("复制文件时发生错误: " + ex.Message, "操作失败", MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 以只读方式打开文件, 返回文件的字节数组
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static byte[] GetBytesFromFile(string filename)
        {
            // 检测文件是否存在
            FileInfo file = new FileInfo(filename);
            if(!file.Exists)
                return null;

            // 检测文件长度是否为零
            int len = (int)file.Length;
            if(len == 0)
                return null;

            FileStream fStream = null;
            try
            {
                fStream = file.OpenRead();
            }
            catch(DirectoryNotFoundException ex)
            {
                MessageBox.Show("Occur DirectoryNotFoundException: {0}", ex.Message);
                return null;
            }
            catch(IOException ex)
            {
                MessageBox.Show("Occur IOException: {0}", ex.Message);
                return null;
            }
            catch(UnauthorizedAccessException ex)
            {
                MessageBox.Show("Occur UnauthorizedAccessException: {0}", ex.Message);
                return null;
            }

            byte[] bData = new byte[len];
            try
            {
                fStream.Read(bData, 0, len);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Occur error during read file: {0}", ex.Message);
                return null;
            }
            finally
            {
                fStream.Close();
            }

            return bData;
        }

        /// <summary>
        /// 将字节数组写回文件中
        /// </summary>
        /// <param name="filename"></param>
        public static void SetBytesToFile(string filename, byte[] bData)
        {
            if((bData == null) |
                (bData.Length == 0))
                return;

            FileStream file = null;
            try
            {
                file = new FileStream(filename, FileMode.Create);
                file.Seek(0, SeekOrigin.Begin);
                file.Write(bData, 0, bData.Length);
            }
            catch(IOException ex)
            {
                MessageBox.Show("Occur error during write file: {0}", ex.Message);
            }
            finally
            {
                file.Close();
            }
        }

        #endregion 文件及目录

        #region 复制/粘贴

        // 注: !!由于测试项的复制/粘贴涉及到数据库访问
        // 所以其相关代码放到数据库操作层类中

        #region 剪贴板内容检测

        public static bool IsTestItemInClipboard()
        {
            if(Clipboard.ContainsData("CPTestItem"))
                return true;
            else
                return false;
        }

        public static bool IsUsecaseInClipboard()
        {
            if (Clipboard.ContainsData("CPUsecase"))
                return true;
            else if (Clipboard.ContainsData("CPUsecaseOld")) // 兼容粘贴旧工具的用例
                return true;
            else
                return false;
        }

        public static bool IsStepInClipboard()
        {
            if(Clipboard.ContainsData("CPStep"))
                return true;
            else
                return false;
        }

        public static bool IsStpmsUcInClipboard()
        {
            return Clipboard.ContainsData(GlobalClassNameAttribute.GetGlobalName(typeof (UsecaseAction)));
        }

        public static bool IsStpmsItemInClipboard()
        {
            return Clipboard.ContainsData(GlobalClassNameAttribute.GetGlobalName(typeof (TestItemAction)));
        }

        #endregion 剪贴板内容检测

        #region 复制

        // 复制测试步骤
        public static void CopyStepsToClipboard(string connstr, List<TestStep> datalist, DBType dbtype)
        {
            Clipboard.Clear();
            CPStep cps = new CPStep(connstr, datalist, dbtype);
            Clipboard.SetData("CPStep", cps);
        }

        // 复制测试用例
        public static void CopyUCToClipboard(string connser, TestUC ucinfo, List<TestStep> datalist, DBType dbtype)
        {
            Clipboard.Clear();
            CPUsecase cpu = new CPUsecase(connser, ucinfo, datalist, dbtype);
            Clipboard.SetData("CPUsecase", cpu);
        }

        public static void CopyItemToClipboard(string connstr, List<string> datalist, int level, DBType dbtype)
        {
            Clipboard.Clear();
            CPTestItem cpi = new CPTestItem(connstr, datalist, level, dbtype);
            Clipboard.SetData("CPTestItem", cpi);
        }

        #endregion 复制

        #region 粘贴

        /// <summary>
        /// 粘贴测试步骤(粘贴到DataTable中)
        /// </summary>
        /// <param name="uceid">目标测试用例实体ID</param>
        /// <param name="index">插入位置</param>
        /// <param name="bCopyDesign">只粘贴设计信息</param>
        /// <returns></returns>
        public static int PasteStep(string uceid, int index, bool bCopyDesign, DataTable tbl)
        {
            if (Clipboard.ContainsData("CPStep"))
            {
                CPStep cps = Clipboard.GetData("CPStep") as CPStep;
                if (cps == null)
                    return 0;

                if (cps.nodetype != NodeType.TestStep)
                    return 0;

                List<TestStep> datalist = cps.dataList;
                if (datalist == null)
                    return 0;

                int count = datalist.Count;

                // 插入后的记录序号重排
                foreach (DataRow row in tbl.Rows)
                {
                    if ((int)row["序号"] > index)
                        row["序号"] = (int)row["序号"] + count;
                }

                foreach (TestStep ts in datalist)
                {
                    DataRow row = tbl.NewRow();

                    object id = FunctionClass.NewGuid;
                    object entityid = FunctionClass.NewGuid;

                    row["ID"] = id;
                    row["项目ID"] = MyBaseForm.pid;
                    row["过程ID"] = entityid;
                    row["测试版本"] = MyBaseForm.currentvid;
                    row["实体ID"] = entityid;
                    row["测试用例ID"] = uceid;
                    row["输入及操作"] = ts.input;
                    row["期望结果"] = ts.expection;

                    if (!bCopyDesign)
                    {
                        row["实测结果"] = ts.result;
                        row["问题?"] = false;
                        row["问题标识"] = string.Empty;
                    }

                    row["序号"] = index + 1;
                    row["创建版本ID"] = MyBaseForm.currentvid;
                    tbl.Rows.InsertAt(row, index++);
                }

                return datalist.Count;
            }
            else
                return 0;
        }

        // 粘贴 "测试用例"
        public static void PasteUC(CPUsecase cpu, string itemtid, DataTable tbl, TreeNode parent)
        {
            using(PasteUsecaseSelForm dlg = new PasteUsecaseSelForm())
            {
                dlg.Text = "选择粘贴测试用例的方式";
                dlg.PasteSel = 1;

                // 跨库复制时无法建立快捷方式
                if(!cpu.ConnStr.Equals(MyBaseForm.dbProject.dbConnection.ConnectionString))
                    dlg.DisableShortcut();

                // 检测目标测试项是否已经建立此用例的快捷方式
                if(TestItem.ItemHasShortcut(tbl, itemtid, cpu.UCInfo.id))
                    dlg.DisableShortcut();

                // 检测目标测试项是否就是此用例的父测试项
                if(TestItem.PasteUCOnSelfItem(tbl, itemtid, cpu.UCInfo.id))
                    dlg.DisableShortcut();

                if(DialogResult.OK == dlg.ShowDialog())
                {
                    if(dlg.PasteSel == 0)   // 粘贴快捷方式
                    {
                        AddShortcut(tbl, cpu.UCInfo.id, itemtid, cpu.UCInfo);
                        FrmCommonFunc.AddUCNode(cpu.UCInfo, parent, tbl.Rows.Count, true);
                    }
                    else if(dlg.PasteSel == 1)  // 粘贴实例
                    {
                        TestUC newinfo = new TestUC();
                        string uceid = AddUC(tbl, itemtid, cpu.UCInfo, parent.TreeView, newinfo);
                        PasteStepsForUC(uceid, cpu.dataList);
                        FrmCommonFunc.AddUCNode(newinfo, parent, tbl.Rows.Count, false);
                    }
                    else
                        return;
                }
            }
        }

        // 粘贴"测试用例"(兼容旧版本测试过程工具)
        public static void PasteUCOld(CPUsecaseOld cpu, string itemtid, DataTable tbl, TreeNode parent)
        {
            MessageBox.Show("正在从旧版本测试过程工具粘贴用例!\n由于数据格式的更改, 只能粘贴部分数据!!\n",
                "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            using (DBAccess dbold = DBAccessFactory.FromAccessFile(cpu.DatabaseName).CreateInst())
            {
                DataRow r = CPOldUC.GetUCInfo(dbold, cpu.ID);
                if (r != null)
                {
                    TestUC ucinfo = new TestUC();
                    ucinfo.execsta      = GetStringFromDB(r["执行与否"]);
                    ucinfo.name         = GetStringFromDB(r["测试用例名称"]);
                    ucinfo.term         = GetStringFromDB(r["测试过程终止条件"]);
                    ucinfo.cert         = GetStringFromDB(r["测试结果评估标准"]);
                    ucinfo.testtime     = GetDateTimeFromDB(r["测试时间"]);
                    ucinfo.execrlt      = GetStringFromDB(r["通过与否"]);
                    ucinfo.desc         = GetStringFromDB(r["用例描述"]);
                    ucinfo.init         = GetStringFromDB(r["用例的初始化"]);
                    ucinfo.constraint   = GetStringFromDB(r["前提和约束"]);
                    ucinfo.trace        = GetStringFromDB(r["追踪关系"]);
                    ucinfo.unexec       = GetStringFromDB(r["未执行原因"]);

                    TestUC newinfo = new TestUC();
                    string uceid = AddUC(tbl, itemtid, ucinfo, parent.TreeView, newinfo);
                    PasteStepsForUCOld(uceid, CPOldUC.GetSteps(dbold, cpu.ID));
                    FrmCommonFunc.AddUCNode(newinfo, parent, tbl.Rows.Count, false);
                }
            }
        }

        /// <summary>
        /// 从Stpms中粘贴测试用例数据
        /// </summary>
        /// <param name="ucAction"></param>
        /// <param name="itemtid"></param>
        /// <param name="tbl"></param>
        /// <param name="parent"></param>
        public static void PasteUCFromStpms(UsecaseAction ucAction, string itemtid, DataTable tbl, TreeNode parent)
        {
            TestUC newinfo = new TestUC();
            TestUC ucinfo = GenTestUC(ucAction);
            string uceid = AddUC(tbl, itemtid, ucinfo, parent.TreeView, newinfo);
            PasteStepsForUC(uceid, GetSteps(ucAction));
            FrmCommonFunc.AddUCNode(newinfo, parent, tbl.Rows.Count, false);
        }

        #endregion 粘贴

        #region 内部方法

        // 粘贴测试用例快捷方式(CA测试用例与测试项关系表记录加入到DataTable)
        private static void AddShortcut(DataTable tbl, string uctid, string itemtid, TestUC ucinfo)
        {
            DataRow row = tbl.NewRow();

            row["ID"] = uctid;
            row["关系表ID"] = FunctionClass.NewGuid;
            row["项目ID"] = MyBaseForm.pid;
            row["实测ID"] = uctid;
            row["测试项ID"] = itemtid;
            row["直接所属标志"] = false;
            row["测试用例名称"] = ucinfo.name;
            row["序号"] = tbl.Rows.Count + 1;
            row["测试用例类型"] = FrmCommonFunc.GetUCType(ucinfo, true);

            tbl.Rows.Add(row);
        }

        // 粘贴测试用例(用例实测表, 用例实体表, 关系表记录加入到DataTable中)(不复制问题报告单和附件)
        // 返回生成的用例实体ID
        private static string AddUC(DataTable tbl, string itemtid, TestUC ucinfo, TreeView tree, TestUC newinfo)
        {
            DataRow row = tbl.NewRow();

            object relid = FunctionClass.NewGuid;   // 关系表ID
            object tid = FunctionClass.NewGuid;     // 用例实测ID
            object eid = FunctionClass.NewGuid;     // 用例实体ID

            TestUsecase.BeforeGenUniName();

            row["直接所属标志"] = true;
            row["执行状态"] = ucinfo.execsta;
            row["测试版本"] = MyBaseForm.currentvid;
            row["ID"] = tid;
            row["测试用例ID"] = eid;
            row["实体ID"] = eid;
            //row["测试用例名称"] = TestUsecase.GenUniqueUsecaseName(ucinfo.name + "的复制", tree);
            row["测试用例名称"] = TestUsecase.GenUniqueUsecaseName(ucinfo.name, tree);
            row["关系表ID"] = relid;
            row["项目ID"] = MyBaseForm.pid;
            row["实测ID"] = tid;
            row["测试项ID"] = itemtid;
            row["序号"] = tbl.Rows.Count + 1;
            row["测试过程终止条件"] = ucinfo.term;
            row["测试结果评估标准"] = ucinfo.cert;
            row["测试时间"] = ucinfo.testtime;
            if(ucinfo.execrlt.Equals(ConstDef.execrlt2))
            {
                row["执行结果"] = ConstDef.execrlt1;
                ucinfo.execrlt = ConstDef.execrlt1;
            }
            else
                row["执行结果"] = ucinfo.execrlt;

            row["用例描述"] = ucinfo.desc;
            row["用例的初始化"] = ucinfo.init;
            row["前提和约束"] = ucinfo.constraint;
            row["追踪关系"] = ucinfo.trace;
            row["所使用的设计方法"] = ucinfo.method;
            row["设计人员"] = ucinfo.designperson;
            row["测试人员"] = ucinfo.testperson;
            row["未执行原因"] = ucinfo.unexec;
            row["测试用例类型"] = FrmCommonFunc.GetUCType(ucinfo, false);

            newinfo.id = (string)tid;
            newinfo.name = (string)row["测试用例名称"];
            newinfo.execsta = (string)row["执行状态"];
            newinfo.execrlt = (string)row["执行结果"];

            tbl.Rows.Add(row);

            return (string)eid;
        }

        // 粘贴测试用例时复制其测试步骤
        private static void PasteStepsForUC(string uceid, List<TestStep> datalist)
        {
            DataTable tbl = TestUsecase.GetSteps(MyBaseForm.dbProject, (string)MyBaseForm.pid,
                (string)MyBaseForm.currentvid, uceid);

            int count = 1;
            foreach(TestStep ts in datalist)
            {
                object tid = FunctionClass.NewGuid;
                object eid = FunctionClass.NewGuid;

                DataRow row = tbl.NewRow();

                row["序号"]         = count++;
                row["ID"]           = tid;
                row["项目ID"]       = MyBaseForm.pid;
                row["过程ID"]       = eid;
                row["测试版本"]     = MyBaseForm.currentvid;
                row["实测结果"]     = ts.result;
                row["实体ID"]       = eid;
                row["测试用例ID"]   = uceid;
                row["输入及操作"]   = ts.input;
                row["期望结果"]     = ts.expection;
                row["创建版本ID"]   = MyBaseForm.currentvid;

                tbl.Rows.Add(row);

                PasteAccs(tid, eid, "输入及操作", ts.input_accs);
                PasteAccs(tid, eid, "期望结果", ts.expect_accs);
                PasteAccs(tid, eid, "实测结果", ts.result_accs);
            }

            TestUsecase.UpdateStep(MyBaseForm.dbProject, (string)MyBaseForm.pid,
                (string)MyBaseForm.currentvid, tbl);
        }

        private static void PasteAccs(object tid, object eid, string belong, List<Acc> accs)
        {
            int sequence = 1;
            foreach (var acc in accs)
            {
                object id1 = FunctionClass.NewGuid;
                object id2 = FunctionClass.NewGuid;

                string sql = "INSERT INTO DC测试过程附件表 (ID, 项目ID, 测试过程ID, " +
                    "序号, 附件实体ID, 附件所属, 测试版本) VALUES (?,?,?,?,?,?,?)";
                MyBaseForm.dbProject.ExecuteNoQuery(sql, id1, MyBaseForm.pid, belong.Equals("实测结果") ? tid : eid, sequence++, id2, belong, MyBaseForm.currentvid);

                sql = "INSERT INTO DC附件实体表 (ID, 项目ID, 附件名称, 附件内容, " +
                      "附件类型, 备注, 对应原文件路径, 关联数, 输出与否) VALUES (?,?,?,?,?,?,?,?,?)";
                MyBaseForm.dbProject.ExecuteNoQuery(sql, id2, MyBaseForm.pid, acc.Name, acc.Content,
                                                    acc.Type, acc.Memo, acc.Path, 1, acc.Output);
            }
        }

        // 粘贴测试用例时复制其测试步骤(与旧版本兼容)
        private static void PasteStepsForUCOld(string uceid, DataTable tbl)
        {
            DataTable dt = TestUsecase.GetSteps(MyBaseForm.dbProject, (string)MyBaseForm.pid,
                (string)MyBaseForm.currentvid, uceid);

            int count = 1;
            foreach (DataRow row in tbl.Rows)
            {
                object tid = FunctionClass.NewGuid;
                object eid = FunctionClass.NewGuid;

                DataRow r = dt.NewRow();

                r["序号"]       = count++;
                r["ID"]         = tid;
                r["项目ID"]     = MyBaseForm.pid;
                r["过程ID"]     = eid;
                r["测试版本"]   = MyBaseForm.currentvid;
                r["实测结果"]   = GetStringFromDB(row["实测结果"]);
                r["实体ID"]     = eid;
                r["测试用例ID"] = uceid;
                r["输入及操作"] = GetStringFromDB(row["输入及操作"]);
                r["期望结果"]   = GetStringFromDB(row["期望结果"]);
                r["创建版本ID"] = MyBaseForm.currentvid;

                dt.Rows.Add(r);
            }

            TestUsecase.UpdateStep(MyBaseForm.dbProject, (string)MyBaseForm.pid,
                (string)MyBaseForm.currentvid, dt);
        }

        #endregion 内部方法

        #endregion 复制/粘贴

        #region 代码简化

        // 断言obj引用非空
        public static bool AssertNotNull(object obj, string info, string caption)
        {
            if(obj == null)
            {
                MessageBox.Show(info, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        #endregion 代码简化

        #region 追踪关系

        // 测试用例中追踪关系
        public static string InitTrace(TreeNode tn)
        {
            TreeNode itemnode = tn.Parent;
            if(itemnode == null)
                return string.Empty;

            string labeltext = itemnode.Text;

            int reqstart = MyProjectInfo.GetProjectContent<int>("需求起始章节号");
            int prostart = MyProjectInfo.GetProjectContent<int>("计划起始章节号");

            var global = GlobalData.globalData;
            string reqver = ProjectInfo.GetTextContent(global.dbProject, (string)global.projectID, 
                (string)global.currentvid, "需求分析", "文档版本");
            string prover = ProjectInfo.GetTextContent(global.dbProject, (string)global.projectID,
                (string)global.currentvid, "测试计划", "文档版本");
            if (reqver.Equals(string.Empty))
                reqver = "Non-Version";
            if (prover.Equals(string.Empty))
                prover = "Non-Version";

            string strreq = string.Empty;
            string strpro = string.Empty;

            string req = ZString.Split1(labeltext, reqstart.ToString(), out strreq);
            string pro = ZString.Split1(labeltext, prostart.ToString(), out strpro);

            string newreq = "测试需求分析" + "(" + reqver + ")" + " " + req + " " + strreq;
            string newpro = "测试计划" + "(" + prover + ")" + " " + pro + " " + strpro;

            TraceTypeEnum tte = ProjectConfigData.Inst.CaseTraceType;

            switch(tte)
            {
                case TraceTypeEnum.TestPlan:
                    return newpro;

                case TraceTypeEnum.TestRequire:
                    return newreq;
            }

            return string.Empty;
        }

        #endregion 追踪关系

        #region 回归测试

        /// <summary>
        /// 检测当前运行状态, 是否处于回归测试的执行阶段
        /// </summary>
        /// <returns></returns>
        public static bool IsRegressExec()
        {
            object o = DBLayer1.GetPreVersion(MyBaseForm.dbProject, MyBaseForm.currentvid);
            if (o == null)
                return false;
            
            VersionMode vm = DBLayer1.GetVersionMode(MyBaseForm.dbProject, MyBaseForm.pid, MyBaseForm.currentvid);
            if (vm == VersionMode.Prepare)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 检测某个树节点是否有子节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool HasChildNode(TreeNode node)
        {
            if (node.Nodes.Count != 0)
                return true;
            else
                return false;
        }

        #endregion 回归测试

        #region 与STPMS交换数据

        #region usecase

        private static TestUC GenTestUC(UsecaseAction ucAction)
        {
            TestUC ucinfo = new TestUC
                                {
                                    execsta = ucAction.ExecuteState,
                                    name = ucAction.UcEntity.Name,
                                    term = ucAction.UcEntity.Terminate,
                                    cert = ucAction.UcEntity.Evalution,
                                    testtime = ucAction.ExecuteDate,
                                    execrlt = ucAction.ExecuteResult,
                                    desc = ucAction.UcEntity.Description,
                                    init = ucAction.UcEntity.Initial,
                                    constraint = ucAction.UcEntity.Constrain,
                                    unexec = ucAction.UnexecuteReason,
                                    trace = string.Empty,
                                    method = string.Empty,
                                    designperson = string.Empty,
                                    testperson = string.Empty
                                };
            return ucinfo;
        }

        private static List<TestStep> GetSteps(UsecaseAction ucAction)
        {
            SortedList<int, TestStep> ssteps = new SortedList<int, TestStep>();
            var steps = new List<TestStep>();

            foreach (var stepEntity in ucAction.UcEntity.StepEntities)
            {
                TestStepEntity entity = stepEntity;

                var stepAction = stepEntity.StepActions.Find(item => item.Vid == ucAction.Vid &&
                                                                     entity.Pid == ucAction.UcEntity.Pid);
                if (stepAction == null)
                    continue;

                TestStep testStep = new TestStep
                                        {
                                            sequence = stepAction.Sequence,
                                            input = string.IsNullOrEmpty(entity.Input) ? string.Empty : entity.Input,
                                            expection =
                                                string.IsNullOrEmpty(entity.Expect) ? string.Empty : entity.Expect,
                                            result =
                                                string.IsNullOrEmpty(stepAction.Result)
                                                    ? string.Empty
                                                    : stepAction.Result,
                                        };
                ssteps.Add(testStep.sequence, testStep);
            }

            foreach (var testStep in ssteps)
            {
                steps.Add(testStep.Value);
            }

            return steps;
        }

        #endregion usecase

        #region testitem

        public static string PasteTestItem(TestItemAction itemAction, DBAccess dbdes, NodeType destype,
            string parenteid, int newseq, TreeNode target, DataTable tbl)
        {
            string sql;

            var newitemtid = (string)FunctionClass.NewGuid;
            var newitemeid = (string)FunctionClass.NewGuid;
            TreeNode node;

            switch (destype)
            {
                case NodeType.TestType: // 粘贴到某个测试类型下

                    sql = "INSERT INTO CA测试项实测表 (ID, 测试项ID, 序号, 项目ID, 测试版本) " +
                        "VALUES (?, ?, ?, ?, ?)";
                    dbdes.ExecuteNoQuery(sql, newitemtid, newitemeid, newseq, MyBaseForm.pid, MyBaseForm.currentvid);

                    sql = "INSERT INTO CA测试项实体表 (ID, 项目ID, 测试项名称, 简写码, 测试项说明及测试要求, 测试方法说明, " +
                        "终止条件, 所属测试类型ID, 父节点ID) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    dbdes.ExecuteNoQuery(sql, newitemeid, MyBaseForm.pid, itemAction.ItemEntity.Name, itemAction.ItemEntity.Abbr,
                        itemAction.ItemEntity.Requirement, itemAction.ItemEntity.Method, 
                        itemAction.ItemEntity.Terminate, parenteid, DBNull.Value);

                    node = GenItemNodeForType(target, newitemtid, newseq, itemAction); // 生成树节点

                    CopyItemSubNode(itemAction, dbdes, newitemtid, node);

                    AddNewToTbl(tbl, newitemtid, newitemeid, newseq, parenteid, true, itemAction);

                    return newitemtid;

                case NodeType.TestItem: // 粘贴到某个测试项下

                    sql = "INSERT INTO CA测试项实测表 (ID, 测试项ID, 序号, 项目ID, 测试版本) " +
                        "VALUES (?, ?, ?, ?, ?)";
                    dbdes.ExecuteNoQuery(sql, newitemtid, newitemeid, newseq, MyBaseForm.pid, MyBaseForm.currentvid);

                    sql = "INSERT INTO CA测试项实体表 (ID, 项目ID, 测试项名称, 简写码, 测试项说明及测试要求, 测试方法说明, " +
                        "终止条件, 所属测试类型ID, 父节点ID) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    dbdes.ExecuteNoQuery(sql, newitemeid, MyBaseForm.pid, itemAction.ItemEntity.Name, itemAction.ItemEntity.Abbr,
                        itemAction.ItemEntity.Requirement, itemAction.ItemEntity.Method, itemAction.ItemEntity.Terminate, 
                        DBNull.Value, parenteid);

                    node = GenItemNodeForItem(target, newitemtid, newseq, itemAction);

                        //string srcparenteid = TestItem.GetEntityID(dbsrc, srcitemtid);
                        CopyItemSubNode(itemAction, dbdes, newitemtid, node);


                    AddNewToTbl(tbl, newitemtid, newitemeid, newseq, parenteid, false, itemAction);

                    return newitemtid;
            }

            return string.Empty;
        }

        /// <summary>
        /// 粘贴测试项的子节点(仅包括测试用例, 不包括子测试项)
        /// </summary>
        /// <param name="itemAction"></param>
        /// <param name="dbdes"></param>
        /// <param name="desparenttid"></param>
        /// <param name="parent"></param>
        private static void CopyItemSubNode(TestItemAction itemAction, DBAccess dbdes, string desparenttid, TreeNode parent)
        {
            // 复制其下属测试用例
            int seq = 1;
            foreach(UsecaseAction usecaseAction in itemAction.UcActions)
            {
                PasteUsecase(usecaseAction, dbdes, desparenttid, parent, seq++);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ucAction"></param>
        /// <param name="dbdes" />
        /// <param name="desitemtid"></param>
        /// <param name="parent"></param>
        /// <param name="seq"></param>
        /// <returns></returns>
        private static void PasteUsecase(UsecaseAction ucAction, DBAccess dbdes, string desitemtid, TreeNode parent, int seq)
        {
            // 测试用例实测表和实体表生成新记录
            string uceid;
            string newname;
            string uctid = CopyUsecase(ucAction, dbdes, parent, out uceid, out newname);

            // 添加记录到"测试用例与测试项关系表"
            NewRecordRela(dbdes, uctid, desitemtid, seq);

            // 添加此测试用例下所有测试过程到新建的用例中
            PasteTestStep(ucAction, dbdes,  uceid);

            GenUCNodeForItem(parent, uctid, seq, ucAction, newname);
        }

        #region 内部方法


        // "CA测试用例实测表"和"CA测试用例实体表"产生新记录
        private static string CopyUsecase(UsecaseAction ucAction, DBAccess dbdes, TreeNode parent, out string neweid,
            out string newname)
        {
            CheckNullVale(ucAction);
            string newtid = (string)FunctionClass.NewGuid;
            neweid = (string)FunctionClass.NewGuid;

            TestUsecase.BeforeGenUniName();

            newname = TestUsecase.GenUniqueUsecaseName((string)ucAction.UcEntity.Name, parent.TreeView);

            string sql = "INSERT INTO CA测试用例实测表 (ID, 项目ID, 测试用例ID, 执行状态, 执行结果, 测试时间, " +
                         "未执行原因, 测试版本) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            dbdes.ExecuteNoQuery(sql, newtid, MyBaseForm.pid, neweid, ucAction.ExecuteState, ucAction.ExecuteResult,
                ucAction.ExecuteDate, ucAction.UnexecuteReason, MyBaseForm.currentvid);
            sql = "INSERT INTO CA测试用例实体表 (ID, 项目ID, 测试用例名称, 用例描述, 用例的初始化, 测试过程终止条件, " +
                "测试结果评估标准, 前提和约束) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            dbdes.ExecuteNoQuery(sql, neweid, MyBaseForm.pid, newname, ucAction.UcEntity.Description,
                ucAction.UcEntity.Initial, ucAction.UcEntity.Terminate, ucAction.UcEntity.Evalution, ucAction.UcEntity.Constrain);

            return newtid;
        }

        private static void CheckNullVale(UsecaseAction ucAction)
        {
            if (string.IsNullOrEmpty(ucAction.UnexecuteReason))
                ucAction.UnexecuteReason = string.Empty;
        }

        // "CA测试用例与测试项关系表"产生新记录
        private static void NewRecordRela(DBAccess dbdes, string uctid, string itemtid, int seq)
        {
            string sql = "INSERT INTO CA测试用例与测试项关系表 (ID, 项目ID, 测试用例ID, 测试项ID, 序号, 直接所属标志) " +
                "VALUES (?, ?, ?, ?, ?, ?)";
            dbdes.ExecuteNoQuery(sql, FunctionClass.NewGuid, MyBaseForm.pid, uctid, itemtid, seq, true);
        }

        // 复制某个测试用例下所有测试步骤
        private static void PasteTestStep(UsecaseAction ucAction, DBAccess dbdes, string desuceid)
        {
            List<TestStep> steps = GetSteps(ucAction);
            foreach (TestStep step in steps)
            {
                var tid = FunctionClass.NewGuid;
                var eid = FunctionClass.NewGuid;

                string sql = "INSERT INTO CA测试过程实测表 (ID, 项目ID, 过程ID, 测试版本, 序号, 实测结果) " +
                             "VALUES (?, ?, ?, ?, ?, ?)";
                dbdes.ExecuteNoQuery(sql, tid, MyBaseForm.pid, eid, MyBaseForm.currentvid, step.sequence, step.result);


                sql = "INSERT INTO CA测试过程实体表 (ID, 测试用例ID, 项目ID, 输入及操作, 期望结果) " +
                      "VALUES (?, ?, ?, ?, ?)";
                dbdes.ExecuteNoQuery(sql, eid, desuceid, MyBaseForm.pid, step.input, step.expection);
            }
        }

        // 为测试类型新加测试项树节点
        private static TreeNode GenItemNodeForType(TreeNode parent, string tid, int newseq, TestItemAction itemAction)
        {
            // 向树中添加新节点
            TreeNode node = new TreeNode();

            NodeTagInfo taginfo = new NodeTagInfo
                                      {
                                          id = tid,
                                          nodeType = NodeType.TestItem,
                                          keySign = itemAction.ItemEntity.Abbr,
                                          order = newseq,
                                          text = itemAction.ItemEntity.Name,
                                          verId = MyBaseForm.currentvid as string
                                      };

            node.Text = UIFunc.GenSections(parent, taginfo.order, '.') +
                taginfo.text;
            node.Tag = taginfo;
            UIFunc.SetNodeImageKey(node, NodeType.TestItem);

            parent.Nodes.Add(node);

            if (!parent.IsExpanded)
                parent.Expand();

            return node;
        }

        // 为测试项新加测试项树节点
        private static TreeNode GenItemNodeForItem(TreeNode parent, string tid, int newseq, TestItemAction itemAction)
        {
            //向树中添加新节点
            TreeNode node = new TreeNode();

            NodeTagInfo taginfo = new NodeTagInfo
                                      {
                                          id = tid,
                                          nodeType = NodeType.TestItem,
                                          keySign = itemAction.ItemEntity.Abbr,
                                          order = newseq,
                                          text = itemAction.ItemEntity.Name,
                                          verId = MyBaseForm.currentvid as string,
                                      };

            node.Text = UIFunc.GenSections(parent, taginfo.order, '.') +
                taginfo.text;
            node.Tag = taginfo;
            UIFunc.SetNodeImageKey(node, NodeType.TestItem);

            parent.Nodes.Insert(taginfo.order - 1, node);

            if (!parent.IsExpanded)
                parent.Expand();

            return node;
        }

        // 为测试项新加测试用例树节点
        private static void GenUCNodeForItem(TreeNode parent, string tid, int newseq, UsecaseAction ucAction, string newname)
        {
            // 向树中添加新节点
            TreeNode node = new TreeNode();

            NodeTagInfo taginfo = new NodeTagInfo();

            taginfo.id = tid;
            taginfo.nodeType = NodeType.TestCase;
            taginfo.keySign = newseq.ToString(); // 用于标识符

            if (Equals(null, parent.LastNode)) //新添加记录是唯一节点
                taginfo.order = 1;
            else
                taginfo.order = (parent.LastNode.Tag as NodeTagInfo).order + 1;

            taginfo.text = newname;
            taginfo.verId = MyBaseForm.currentvid as string;
            taginfo.IsShortcut = false;

            node.Text = UIFunc.GenSections(parent, taginfo.order, '.') +
                        taginfo.text;
            node.Tag = taginfo;

            TestUC ucinfo = new TestUC();


            ucinfo.execrlt = ucAction.ExecuteResult;
            ucinfo.execsta = ucAction.ExecuteState;


            UIFunc.SetUCNodeImageKey(node, ucinfo, taginfo.IsShortcut);

            parent.Nodes.Add(node);
            if (!parent.IsExpanded)
                parent.Expand();
        }

        // 将记录添加到DataTable中
        private static void AddNewToTbl(DataTable tbl, string tid, string eid, int seq,
            string parentid, bool belongType, TestItemAction itemAction)
        {
            DataRow nr = tbl.NewRow();
            nr["ID"] = tid;
            nr["测试项ID"] = eid;
            nr["测试项名称"] = itemAction.ItemEntity.Name;
            nr["简写码"] = itemAction.ItemEntity.Abbr;
            nr["序号"] = seq;
            nr["测试项说明及测试要求"] = itemAction.ItemEntity.Requirement;
            nr["测试方法说明"] = itemAction.ItemEntity.Method;
            nr["终止条件"] = itemAction.ItemEntity.Terminate;
            if (belongType)
            {
                nr["所属测试类型ID"] = parentid;
                nr["父节点ID"] = DBNull.Value;
            }
            else
            {
                nr["所属测试类型ID"] = DBNull.Value;
                nr["父节点ID"] = parentid;
            }

            nr["实体ID"] = eid;
            nr["项目ID"] = MyBaseForm.pid;
            nr["测试版本"] = MyBaseForm.currentvid;

            tbl.Rows.Add(nr);
            tbl.AcceptChanges();
        }

        #endregion 内部方法


        #endregion testitem

        #endregion 与STPMS交换数据


    }


    public enum PblNodeType
    {
        Unknown = 0,

        TestObj = 1,

        PblSigns = 2,
    }
}
