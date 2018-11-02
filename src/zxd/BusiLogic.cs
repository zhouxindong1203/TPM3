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
     * ���Թ��̹�������ҵ���߼�����
     * zhouxindong 2008/10/23
     */
    public class BusiLogic
    {
        #region ���ڵ����ͼ������

        // ���������ͽڵ��ܼ���
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

        // ����������+������ڵ���ܼ���
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

        // ��ȡ�ڵ�node���ڵļ���(�������ͼ�Ϊset)
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

        // nodestart �� nodeend֮��ļ���(��������)
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

        #region �������¼�⼶��

        // ��ȡ�ڵ�֮�°����ļ���(��������, ����������������)(�����)
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

        // �˴��Ƿ�������ָ�����͵Ľڵ�, Ӧ���Ƿ�������ָ�����͵�Ҷ�ڵ�
        // ��ȷ��û��Ӱ��, ��Ӱ������Ч��, ����ʱ��֮!(�������)
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

        #endregion �������¼�⼶��

        #endregion ���ڵ����ͼ������

        #region ��ֵ

        // �����ݿ���ж�ȡ�ı�ֵ
        public static string GetStringFromDB(object ori)
        {
            if(!DBNull.Value.Equals(ori))
                return (string)ori;
            else
                return string.Empty;
        }

        // �����ݿ���ж�ȡʱ��ֵ(Ϊ��ʱ��ֵΪ������Сֵ)
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

        #endregion ��ֵ

        #region ������ʶ

        // ���ɲ���������ʶ
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

        #endregion ������ʶ

        #region ���ݿ��ֶ�ȱʡֵ

        // "CA������ʵ���" -> "��ֹ����"
        public const string TermiCondi = "�������Ҫ�����Թ����޷���������";

        // "CA��������ʵ���" -> "���Թ�����ֹ����"
        public const string StepTermiCondi = "������������ȫ�����Բ��豻ִ�л���ĳ��ԭ���²��Բ����޷�ִ��(�쳣��ֹ)";

        // "CA��������ʵ���" -> "���Խ��������׼"
        public const string Evaluation = "������������ȫ�����Բ��趼ͨ������־������Ϊ\"ͨ��\"";

        #endregion ���ݿ��ֶ�ȱʡֵ


        #region ��������"ִ��״̬"��"ִ�н��"����

        // ĳ�������������²����
        public static void UCAfterAddNewStep(string uctid, ref string execsta, ref string execrlt, TreeNode node)
        {
            // ���²���������ִ��״̬��ִ�н��
            // ("����ִ��"��"ͨ��") -> ("����ִ��"��"")
            if((execsta.Equals(ConstDef.execsta[2])) &
                (execrlt.Equals(ConstDef.execrlt[1])))
            {
                execsta = ConstDef.execsta[1];

                UIFunc.SetNodeImageKey(node, MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecute],
                    MainTestFrmCommon.ExecStatusImageKeys[ExecuteStatus.PartExecute]);

                // ͬ���������д������Ŀ�ݷ�ʽ
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

        // ɾ��ĳ���Բ��������ò��"ִ��״̬"��"ִ�н��"(tbl֮ǰ�Ѿ�AcceptChanges)
        public static void UpdateAfterDelStep(string uctid, ref string execsta, ref string execrlt, DataTable tbl, TreeNode node)
        {
            execsta = CheckExecSta(tbl);
            execrlt = CheckExecRlt(execsta, tbl);

            UpdateUCIcon(uctid, execsta, execrlt, node);
        }

        // ���"ִ��״̬"(tbl֮ǰ�Ѿ�AcceptChanges)
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
                if((DBNull.Value.Equals(row["ʵ����"])) ||
                    (string.Empty.Equals((string)row["ʵ����"])))
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

        // ���"ִ��״̬"(newtextΪ�µ�"ʵ����"�ı�)(tbl֮ǰ��û��AcceptChanges)
        public static string CheckExecSta(DataTable tbl, string steptid, string newtext)
        {
            DataTable tblcopy = tbl.Copy();
            tblcopy.AcceptChanges();
            string filter = string.Format("ID=\'{0}\'", steptid);
            DataRow[] rows = tblcopy.Select(filter);
            if((rows == null) || (rows.Length == 0))
                throw (new ArgumentException("���ݲ�ѯ�޼�¼����!!"));

            rows[0]["ʵ����"] = newtext;

            return CheckExecSta(tblcopy);
        }

        // ���"ִ�н��"(tbl֮ǰ�Ѿ�AcceptChanges)
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
                if((DBNull.Value.Equals(row["���ⱨ�浥ID"])) ||
                    (string.Empty.Equals((string)row["���ⱨ�浥ID"])))
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

        // ����������"ִ��״̬"��"ִ�н��"�����������ڵ��ͼ�꼰���п�ݷ�ʽͼ��
        public static void UpdateUCIcon(string uctid, string execsta, string execrlt, TreeNode node)
        {
            // ��������ͼ����ʾ״̬
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

        #endregion ��������"ִ��״̬"��"ִ�н��"����

        #region ����������Ӧ���ڵ㶨λ

        private static string _findcaseid;
        private static bool _expanednode;
        private static TreeView _tree;
        /// <summary>
        /// ��λ��ĳ�������������ڵ�
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

        // ������ڵ��Ƿ�Ϊ����������ݷ�ʽ(�޷��ж�ʱ����true)
        public static bool NodeIsShortcut(TreeNode node)
        {
            NodeTagInfo tag = node.Tag as NodeTagInfo;
            if(tag == null)
                return true;

            return tag.IsShortcut;
        }

        #endregion ����������Ӧ���ڵ㶨λ


        #region ���������ڵ㶨λ

        // ����������ѡ��ĳ�����ڵ�(����url��λ��ʽ, �˷�������)
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

                // �����ӽڵ�
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


        #endregion ���������ڵ㶨λ

        #region �ļ���Ŀ¼

        // ���������ļ���
        public static bool CreateAccFolder(DBAccess db, string pid, string vid)
        {
            if(ExecStatus.g_AccFolder.Equals(string.Empty)) // �޸����ļ�����Ϣ
            {
                FolderBrowserDialog odlgFolder = new FolderBrowserDialog();
                try
                {
                    odlgFolder.RootFolder = Environment.SpecialFolder.MyComputer;
                    odlgFolder.Description = "ѡ���Ÿ������ļ���";

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
                    MessageBox.Show("�����ļ��д���ʧ��!\n" + ex.Message, "����ʧ��", MessageBoxButtons.OK,
                         MessageBoxIcon.Error);
                    ExecStatus.g_AccFolder = string.Empty;
                    ProjectInfo.SetTextContent(db, pid, vid, "��Ŀ��Ϣ", "�����ļ���", string.Empty);
                    return false;
                }
            }

            ProjectInfo.SetTextContent(db, pid, vid, "��Ŀ��Ϣ", "�����ļ���", ExecStatus.g_AccFolder);
            return true;
        }

        // �����ļ�
        public static bool CopyFile(string srcfile, string desfile)
        {
            if(!File.Exists(srcfile))
            {
                MessageBox.Show("Դ�ļ�������!", "�����ļ�ʧ��", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            try
            {
                if(File.Exists(desfile))
                {
                    DialogResult usersel = MessageBox.Show("Ŀ���ļ��Ѿ�����!\n�Ƿ��滻?(Y/N)", "Ŀ���ļ�����", MessageBoxButtons.OKCancel,
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
                MessageBox.Show("�����ļ�ʱ��������: " + ex.Message, "����ʧ��", MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        /// <summary>
        /// ��ֻ����ʽ���ļ�, �����ļ����ֽ�����
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static byte[] GetBytesFromFile(string filename)
        {
            // ����ļ��Ƿ����
            FileInfo file = new FileInfo(filename);
            if(!file.Exists)
                return null;

            // ����ļ������Ƿ�Ϊ��
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
        /// ���ֽ�����д���ļ���
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

        #endregion �ļ���Ŀ¼

        #region ����/ճ��

        // ע: !!���ڲ�����ĸ���/ճ���漰�����ݿ����
        // ��������ش���ŵ����ݿ����������

        #region ���������ݼ��

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
            else if (Clipboard.ContainsData("CPUsecaseOld")) // ����ճ���ɹ��ߵ�����
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

        #endregion ���������ݼ��

        #region ����

        // ���Ʋ��Բ���
        public static void CopyStepsToClipboard(string connstr, List<TestStep> datalist, DBType dbtype)
        {
            Clipboard.Clear();
            CPStep cps = new CPStep(connstr, datalist, dbtype);
            Clipboard.SetData("CPStep", cps);
        }

        // ���Ʋ�������
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

        #endregion ����

        #region ճ��

        /// <summary>
        /// ճ�����Բ���(ճ����DataTable��)
        /// </summary>
        /// <param name="uceid">Ŀ���������ʵ��ID</param>
        /// <param name="index">����λ��</param>
        /// <param name="bCopyDesign">ֻճ�������Ϣ</param>
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

                // �����ļ�¼�������
                foreach (DataRow row in tbl.Rows)
                {
                    if ((int)row["���"] > index)
                        row["���"] = (int)row["���"] + count;
                }

                foreach (TestStep ts in datalist)
                {
                    DataRow row = tbl.NewRow();

                    object id = FunctionClass.NewGuid;
                    object entityid = FunctionClass.NewGuid;

                    row["ID"] = id;
                    row["��ĿID"] = MyBaseForm.pid;
                    row["����ID"] = entityid;
                    row["���԰汾"] = MyBaseForm.currentvid;
                    row["ʵ��ID"] = entityid;
                    row["��������ID"] = uceid;
                    row["���뼰����"] = ts.input;
                    row["�������"] = ts.expection;

                    if (!bCopyDesign)
                    {
                        row["ʵ����"] = ts.result;
                        row["����?"] = false;
                        row["�����ʶ"] = string.Empty;
                    }

                    row["���"] = index + 1;
                    row["�����汾ID"] = MyBaseForm.currentvid;
                    tbl.Rows.InsertAt(row, index++);
                }

                return datalist.Count;
            }
            else
                return 0;
        }

        // ճ�� "��������"
        public static void PasteUC(CPUsecase cpu, string itemtid, DataTable tbl, TreeNode parent)
        {
            using(PasteUsecaseSelForm dlg = new PasteUsecaseSelForm())
            {
                dlg.Text = "ѡ��ճ�����������ķ�ʽ";
                dlg.PasteSel = 1;

                // ��⸴��ʱ�޷�������ݷ�ʽ
                if(!cpu.ConnStr.Equals(MyBaseForm.dbProject.dbConnection.ConnectionString))
                    dlg.DisableShortcut();

                // ���Ŀ��������Ƿ��Ѿ������������Ŀ�ݷ�ʽ
                if(TestItem.ItemHasShortcut(tbl, itemtid, cpu.UCInfo.id))
                    dlg.DisableShortcut();

                // ���Ŀ��������Ƿ���Ǵ������ĸ�������
                if(TestItem.PasteUCOnSelfItem(tbl, itemtid, cpu.UCInfo.id))
                    dlg.DisableShortcut();

                if(DialogResult.OK == dlg.ShowDialog())
                {
                    if(dlg.PasteSel == 0)   // ճ����ݷ�ʽ
                    {
                        AddShortcut(tbl, cpu.UCInfo.id, itemtid, cpu.UCInfo);
                        FrmCommonFunc.AddUCNode(cpu.UCInfo, parent, tbl.Rows.Count, true);
                    }
                    else if(dlg.PasteSel == 1)  // ճ��ʵ��
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

        // ճ��"��������"(���ݾɰ汾���Թ��̹���)
        public static void PasteUCOld(CPUsecaseOld cpu, string itemtid, DataTable tbl, TreeNode parent)
        {
            MessageBox.Show("���ڴӾɰ汾���Թ��̹���ճ������!\n�������ݸ�ʽ�ĸ���, ֻ��ճ����������!!\n",
                "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            using (DBAccess dbold = DBAccessFactory.FromAccessFile(cpu.DatabaseName).CreateInst())
            {
                DataRow r = CPOldUC.GetUCInfo(dbold, cpu.ID);
                if (r != null)
                {
                    TestUC ucinfo = new TestUC();
                    ucinfo.execsta      = GetStringFromDB(r["ִ�����"]);
                    ucinfo.name         = GetStringFromDB(r["������������"]);
                    ucinfo.term         = GetStringFromDB(r["���Թ�����ֹ����"]);
                    ucinfo.cert         = GetStringFromDB(r["���Խ��������׼"]);
                    ucinfo.testtime     = GetDateTimeFromDB(r["����ʱ��"]);
                    ucinfo.execrlt      = GetStringFromDB(r["ͨ�����"]);
                    ucinfo.desc         = GetStringFromDB(r["��������"]);
                    ucinfo.init         = GetStringFromDB(r["�����ĳ�ʼ��"]);
                    ucinfo.constraint   = GetStringFromDB(r["ǰ���Լ��"]);
                    ucinfo.trace        = GetStringFromDB(r["׷�ٹ�ϵ"]);
                    ucinfo.unexec       = GetStringFromDB(r["δִ��ԭ��"]);

                    TestUC newinfo = new TestUC();
                    string uceid = AddUC(tbl, itemtid, ucinfo, parent.TreeView, newinfo);
                    PasteStepsForUCOld(uceid, CPOldUC.GetSteps(dbold, cpu.ID));
                    FrmCommonFunc.AddUCNode(newinfo, parent, tbl.Rows.Count, false);
                }
            }
        }

        /// <summary>
        /// ��Stpms��ճ��������������
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

        #endregion ճ��

        #region �ڲ�����

        // ճ������������ݷ�ʽ(CA����������������ϵ����¼���뵽DataTable)
        private static void AddShortcut(DataTable tbl, string uctid, string itemtid, TestUC ucinfo)
        {
            DataRow row = tbl.NewRow();

            row["ID"] = uctid;
            row["��ϵ��ID"] = FunctionClass.NewGuid;
            row["��ĿID"] = MyBaseForm.pid;
            row["ʵ��ID"] = uctid;
            row["������ID"] = itemtid;
            row["ֱ��������־"] = false;
            row["������������"] = ucinfo.name;
            row["���"] = tbl.Rows.Count + 1;
            row["������������"] = FrmCommonFunc.GetUCType(ucinfo, true);

            tbl.Rows.Add(row);
        }

        // ճ����������(����ʵ���, ����ʵ���, ��ϵ����¼���뵽DataTable��)(���������ⱨ�浥�͸���)
        // �������ɵ�����ʵ��ID
        private static string AddUC(DataTable tbl, string itemtid, TestUC ucinfo, TreeView tree, TestUC newinfo)
        {
            DataRow row = tbl.NewRow();

            object relid = FunctionClass.NewGuid;   // ��ϵ��ID
            object tid = FunctionClass.NewGuid;     // ����ʵ��ID
            object eid = FunctionClass.NewGuid;     // ����ʵ��ID

            TestUsecase.BeforeGenUniName();

            row["ֱ��������־"] = true;
            row["ִ��״̬"] = ucinfo.execsta;
            row["���԰汾"] = MyBaseForm.currentvid;
            row["ID"] = tid;
            row["��������ID"] = eid;
            row["ʵ��ID"] = eid;
            //row["������������"] = TestUsecase.GenUniqueUsecaseName(ucinfo.name + "�ĸ���", tree);
            row["������������"] = TestUsecase.GenUniqueUsecaseName(ucinfo.name, tree);
            row["��ϵ��ID"] = relid;
            row["��ĿID"] = MyBaseForm.pid;
            row["ʵ��ID"] = tid;
            row["������ID"] = itemtid;
            row["���"] = tbl.Rows.Count + 1;
            row["���Թ�����ֹ����"] = ucinfo.term;
            row["���Խ��������׼"] = ucinfo.cert;
            row["����ʱ��"] = ucinfo.testtime;
            if(ucinfo.execrlt.Equals(ConstDef.execrlt2))
            {
                row["ִ�н��"] = ConstDef.execrlt1;
                ucinfo.execrlt = ConstDef.execrlt1;
            }
            else
                row["ִ�н��"] = ucinfo.execrlt;

            row["��������"] = ucinfo.desc;
            row["�����ĳ�ʼ��"] = ucinfo.init;
            row["ǰ���Լ��"] = ucinfo.constraint;
            row["׷�ٹ�ϵ"] = ucinfo.trace;
            row["��ʹ�õ���Ʒ���"] = ucinfo.method;
            row["�����Ա"] = ucinfo.designperson;
            row["������Ա"] = ucinfo.testperson;
            row["δִ��ԭ��"] = ucinfo.unexec;
            row["������������"] = FrmCommonFunc.GetUCType(ucinfo, false);

            newinfo.id = (string)tid;
            newinfo.name = (string)row["������������"];
            newinfo.execsta = (string)row["ִ��״̬"];
            newinfo.execrlt = (string)row["ִ�н��"];

            tbl.Rows.Add(row);

            return (string)eid;
        }

        // ճ����������ʱ��������Բ���
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

                row["���"]         = count++;
                row["ID"]           = tid;
                row["��ĿID"]       = MyBaseForm.pid;
                row["����ID"]       = eid;
                row["���԰汾"]     = MyBaseForm.currentvid;
                row["ʵ����"]     = ts.result;
                row["ʵ��ID"]       = eid;
                row["��������ID"]   = uceid;
                row["���뼰����"]   = ts.input;
                row["�������"]     = ts.expection;
                row["�����汾ID"]   = MyBaseForm.currentvid;

                tbl.Rows.Add(row);

                PasteAccs(tid, eid, "���뼰����", ts.input_accs);
                PasteAccs(tid, eid, "�������", ts.expect_accs);
                PasteAccs(tid, eid, "ʵ����", ts.result_accs);
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

                string sql = "INSERT INTO DC���Թ��̸����� (ID, ��ĿID, ���Թ���ID, " +
                    "���, ����ʵ��ID, ��������, ���԰汾) VALUES (?,?,?,?,?,?,?)";
                MyBaseForm.dbProject.ExecuteNoQuery(sql, id1, MyBaseForm.pid, belong.Equals("ʵ����") ? tid : eid, sequence++, id2, belong, MyBaseForm.currentvid);

                sql = "INSERT INTO DC����ʵ��� (ID, ��ĿID, ��������, ��������, " +
                      "��������, ��ע, ��Ӧԭ�ļ�·��, ������, ������) VALUES (?,?,?,?,?,?,?,?,?)";
                MyBaseForm.dbProject.ExecuteNoQuery(sql, id2, MyBaseForm.pid, acc.Name, acc.Content,
                                                    acc.Type, acc.Memo, acc.Path, 1, acc.Output);
            }
        }

        // ճ����������ʱ��������Բ���(��ɰ汾����)
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

                r["���"]       = count++;
                r["ID"]         = tid;
                r["��ĿID"]     = MyBaseForm.pid;
                r["����ID"]     = eid;
                r["���԰汾"]   = MyBaseForm.currentvid;
                r["ʵ����"]   = GetStringFromDB(row["ʵ����"]);
                r["ʵ��ID"]     = eid;
                r["��������ID"] = uceid;
                r["���뼰����"] = GetStringFromDB(row["���뼰����"]);
                r["�������"]   = GetStringFromDB(row["�������"]);
                r["�����汾ID"] = MyBaseForm.currentvid;

                dt.Rows.Add(r);
            }

            TestUsecase.UpdateStep(MyBaseForm.dbProject, (string)MyBaseForm.pid,
                (string)MyBaseForm.currentvid, dt);
        }

        #endregion �ڲ�����

        #endregion ����/ճ��

        #region �����

        // ����obj���÷ǿ�
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

        #endregion �����

        #region ׷�ٹ�ϵ

        // ����������׷�ٹ�ϵ
        public static string InitTrace(TreeNode tn)
        {
            TreeNode itemnode = tn.Parent;
            if(itemnode == null)
                return string.Empty;

            string labeltext = itemnode.Text;

            int reqstart = MyProjectInfo.GetProjectContent<int>("������ʼ�½ں�");
            int prostart = MyProjectInfo.GetProjectContent<int>("�ƻ���ʼ�½ں�");

            var global = GlobalData.globalData;
            string reqver = ProjectInfo.GetTextContent(global.dbProject, (string)global.projectID, 
                (string)global.currentvid, "�������", "�ĵ��汾");
            string prover = ProjectInfo.GetTextContent(global.dbProject, (string)global.projectID,
                (string)global.currentvid, "���Լƻ�", "�ĵ��汾");
            if (reqver.Equals(string.Empty))
                reqver = "Non-Version";
            if (prover.Equals(string.Empty))
                prover = "Non-Version";

            string strreq = string.Empty;
            string strpro = string.Empty;

            string req = ZString.Split1(labeltext, reqstart.ToString(), out strreq);
            string pro = ZString.Split1(labeltext, prostart.ToString(), out strpro);

            string newreq = "�����������" + "(" + reqver + ")" + " " + req + " " + strreq;
            string newpro = "���Լƻ�" + "(" + prover + ")" + " " + pro + " " + strpro;

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

        #endregion ׷�ٹ�ϵ

        #region �ع����

        /// <summary>
        /// ��⵱ǰ����״̬, �Ƿ��ڻع���Ե�ִ�н׶�
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
        /// ���ĳ�����ڵ��Ƿ����ӽڵ�
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

        #endregion �ع����

        #region ��STPMS��������

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
                case NodeType.TestType: // ճ����ĳ������������

                    sql = "INSERT INTO CA������ʵ��� (ID, ������ID, ���, ��ĿID, ���԰汾) " +
                        "VALUES (?, ?, ?, ?, ?)";
                    dbdes.ExecuteNoQuery(sql, newitemtid, newitemeid, newseq, MyBaseForm.pid, MyBaseForm.currentvid);

                    sql = "INSERT INTO CA������ʵ��� (ID, ��ĿID, ����������, ��д��, ������˵��������Ҫ��, ���Է���˵��, " +
                        "��ֹ����, ������������ID, ���ڵ�ID) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    dbdes.ExecuteNoQuery(sql, newitemeid, MyBaseForm.pid, itemAction.ItemEntity.Name, itemAction.ItemEntity.Abbr,
                        itemAction.ItemEntity.Requirement, itemAction.ItemEntity.Method, 
                        itemAction.ItemEntity.Terminate, parenteid, DBNull.Value);

                    node = GenItemNodeForType(target, newitemtid, newseq, itemAction); // �������ڵ�

                    CopyItemSubNode(itemAction, dbdes, newitemtid, node);

                    AddNewToTbl(tbl, newitemtid, newitemeid, newseq, parenteid, true, itemAction);

                    return newitemtid;

                case NodeType.TestItem: // ճ����ĳ����������

                    sql = "INSERT INTO CA������ʵ��� (ID, ������ID, ���, ��ĿID, ���԰汾) " +
                        "VALUES (?, ?, ?, ?, ?)";
                    dbdes.ExecuteNoQuery(sql, newitemtid, newitemeid, newseq, MyBaseForm.pid, MyBaseForm.currentvid);

                    sql = "INSERT INTO CA������ʵ��� (ID, ��ĿID, ����������, ��д��, ������˵��������Ҫ��, ���Է���˵��, " +
                        "��ֹ����, ������������ID, ���ڵ�ID) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
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
        /// ճ����������ӽڵ�(��������������, �������Ӳ�����)
        /// </summary>
        /// <param name="itemAction"></param>
        /// <param name="dbdes"></param>
        /// <param name="desparenttid"></param>
        /// <param name="parent"></param>
        private static void CopyItemSubNode(TestItemAction itemAction, DBAccess dbdes, string desparenttid, TreeNode parent)
        {
            // ������������������
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
            // ��������ʵ�����ʵ��������¼�¼
            string uceid;
            string newname;
            string uctid = CopyUsecase(ucAction, dbdes, parent, out uceid, out newname);

            // ���Ӽ�¼��"����������������ϵ��"
            NewRecordRela(dbdes, uctid, desitemtid, seq);

            // ���Ӵ˲������������в��Թ��̵��½���������
            PasteTestStep(ucAction, dbdes,  uceid);

            GenUCNodeForItem(parent, uctid, seq, ucAction, newname);
        }

        #region �ڲ�����


        // "CA��������ʵ���"��"CA��������ʵ���"�����¼�¼
        private static string CopyUsecase(UsecaseAction ucAction, DBAccess dbdes, TreeNode parent, out string neweid,
            out string newname)
        {
            CheckNullVale(ucAction);
            string newtid = (string)FunctionClass.NewGuid;
            neweid = (string)FunctionClass.NewGuid;

            TestUsecase.BeforeGenUniName();

            newname = TestUsecase.GenUniqueUsecaseName((string)ucAction.UcEntity.Name, parent.TreeView);

            string sql = "INSERT INTO CA��������ʵ��� (ID, ��ĿID, ��������ID, ִ��״̬, ִ�н��, ����ʱ��, " +
                         "δִ��ԭ��, ���԰汾) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            dbdes.ExecuteNoQuery(sql, newtid, MyBaseForm.pid, neweid, ucAction.ExecuteState, ucAction.ExecuteResult,
                ucAction.ExecuteDate, ucAction.UnexecuteReason, MyBaseForm.currentvid);
            sql = "INSERT INTO CA��������ʵ��� (ID, ��ĿID, ������������, ��������, �����ĳ�ʼ��, ���Թ�����ֹ����, " +
                "���Խ��������׼, ǰ���Լ��) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
            dbdes.ExecuteNoQuery(sql, neweid, MyBaseForm.pid, newname, ucAction.UcEntity.Description,
                ucAction.UcEntity.Initial, ucAction.UcEntity.Terminate, ucAction.UcEntity.Evalution, ucAction.UcEntity.Constrain);

            return newtid;
        }

        private static void CheckNullVale(UsecaseAction ucAction)
        {
            if (string.IsNullOrEmpty(ucAction.UnexecuteReason))
                ucAction.UnexecuteReason = string.Empty;
        }

        // "CA����������������ϵ��"�����¼�¼
        private static void NewRecordRela(DBAccess dbdes, string uctid, string itemtid, int seq)
        {
            string sql = "INSERT INTO CA����������������ϵ�� (ID, ��ĿID, ��������ID, ������ID, ���, ֱ��������־) " +
                "VALUES (?, ?, ?, ?, ?, ?)";
            dbdes.ExecuteNoQuery(sql, FunctionClass.NewGuid, MyBaseForm.pid, uctid, itemtid, seq, true);
        }

        // ����ĳ���������������в��Բ���
        private static void PasteTestStep(UsecaseAction ucAction, DBAccess dbdes, string desuceid)
        {
            List<TestStep> steps = GetSteps(ucAction);
            foreach (TestStep step in steps)
            {
                var tid = FunctionClass.NewGuid;
                var eid = FunctionClass.NewGuid;

                string sql = "INSERT INTO CA���Թ���ʵ��� (ID, ��ĿID, ����ID, ���԰汾, ���, ʵ����) " +
                             "VALUES (?, ?, ?, ?, ?, ?)";
                dbdes.ExecuteNoQuery(sql, tid, MyBaseForm.pid, eid, MyBaseForm.currentvid, step.sequence, step.result);


                sql = "INSERT INTO CA���Թ���ʵ��� (ID, ��������ID, ��ĿID, ���뼰����, �������) " +
                      "VALUES (?, ?, ?, ?, ?)";
                dbdes.ExecuteNoQuery(sql, eid, desuceid, MyBaseForm.pid, step.input, step.expection);
            }
        }

        // Ϊ���������¼Ӳ��������ڵ�
        private static TreeNode GenItemNodeForType(TreeNode parent, string tid, int newseq, TestItemAction itemAction)
        {
            // �����������½ڵ�
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

        // Ϊ�������¼Ӳ��������ڵ�
        private static TreeNode GenItemNodeForItem(TreeNode parent, string tid, int newseq, TestItemAction itemAction)
        {
            //�����������½ڵ�
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

        // Ϊ�������¼Ӳ����������ڵ�
        private static void GenUCNodeForItem(TreeNode parent, string tid, int newseq, UsecaseAction ucAction, string newname)
        {
            // �����������½ڵ�
            TreeNode node = new TreeNode();

            NodeTagInfo taginfo = new NodeTagInfo();

            taginfo.id = tid;
            taginfo.nodeType = NodeType.TestCase;
            taginfo.keySign = newseq.ToString(); // ���ڱ�ʶ��

            if (Equals(null, parent.LastNode)) //�����Ӽ�¼��Ψһ�ڵ�
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

        // ����¼���ӵ�DataTable��
        private static void AddNewToTbl(DataTable tbl, string tid, string eid, int seq,
            string parentid, bool belongType, TestItemAction itemAction)
        {
            DataRow nr = tbl.NewRow();
            nr["ID"] = tid;
            nr["������ID"] = eid;
            nr["����������"] = itemAction.ItemEntity.Name;
            nr["��д��"] = itemAction.ItemEntity.Abbr;
            nr["���"] = seq;
            nr["������˵��������Ҫ��"] = itemAction.ItemEntity.Requirement;
            nr["���Է���˵��"] = itemAction.ItemEntity.Method;
            nr["��ֹ����"] = itemAction.ItemEntity.Terminate;
            if (belongType)
            {
                nr["������������ID"] = parentid;
                nr["���ڵ�ID"] = DBNull.Value;
            }
            else
            {
                nr["������������ID"] = DBNull.Value;
                nr["���ڵ�ID"] = parentid;
            }

            nr["ʵ��ID"] = eid;
            nr["��ĿID"] = MyBaseForm.pid;
            nr["���԰汾"] = MyBaseForm.currentvid;

            tbl.Rows.Add(nr);
            tbl.AcceptChanges();
        }

        #endregion �ڲ�����


        #endregion testitem

        #endregion ��STPMS��������


    }


    public enum PblNodeType
    {
        Unknown = 0,

        TestObj = 1,

        PblSigns = 2,
    }
}