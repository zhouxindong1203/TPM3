using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common.TrueDBGrid;
using Common;
using TPM3.Sys;
using Z1.tpm.DB;
using C1.Win.C1TrueDBGrid;
using System.Collections;
using Z1Utils.Controls;
using Z1.tpm;

namespace TPM3.zxd
{
    public partial class TestProjectForm : MyBaseForm
    {
        #region ����

        TrueDBGridAssist gridAssist1;
        public static ColumnPropList columnList1 = GridAssist.GetColumnPropList<TestProjectForm>();

        private DataTable _tbl;

        static TestProjectForm()
        {
            columnList1.Add("���", 50);
            columnList1.Add("�����������", 200);
            columnList1.Add("��д��", 80);
            columnList1.Add("�������汾", 120);
            columnList1.Add("���Լ���", 120);
            columnList1.AddValidator(new NotNullValidator("�����������"));
            columnList1.AddValidator(new NotNullValidator("��д��"));
            columnList1.AddValidator(new NotNullValidator("�������汾"));
        }

        public TestProjectForm()
        {
            InitializeComponent();

            gridAssist1 = new TrueDBGridAssist(grid1, "ID", "���");
            gridAssist1.columnList = columnList1;

            gridAssist1.rowPosition.GetTreeNodeKeyEvent += FrmCommonFunc.GetTreeNodeKey;

            gridAssist1.rowPosition.BeforeRowMoveDown += BeforeRowMove;
            gridAssist1.rowPosition.BeforeRowMoveUp += BeforeRowMove;

            gridAssist1.rowPosition.AfterRowMoveDown += RowMoveEventHandler;
            gridAssist1.rowPosition.AfterRowMoveUp += RowMoveEventHandler;
        }

        public override bool OnPageCreate()
        {
            this._tbl = TestObj.GetObjectsSPV(dbProject, (string)pid, (string)currentvid);
            if (this._tbl == null)
                return false;

            gridAssist1.DataSource = this._tbl;
            gridAssist1.rowPosition.tnc = tnForm.Nodes;
            gridAssist1.OnPageCreate();

            // ���Լ����ComboBox����
            ValueItems items = grid1.Columns["���Լ���"].ValueItems;
            FrmCommonFunc.TransTestLevel(items);
            grid1.Splits[0].DisplayColumns["���Լ���"].DropDownList = true;

            ArrayList li = Z1.tpm.DB.CommonDB.GetSecretLevel(dbProject);
            foreach (string s in li)
                this.cmbDocSec.Items.Add(s);

            this.grid1.AllowRowSizing = (RowSizingEnum)us.rowSizeingList[0];

            return true;
        }

        // ��ȡ"��Ŀ������Ϣ"
        private void LoadData()
        {
            this.txtbxProjName.Text = Z1.tpm.DB.ProjectInfo.GetProjName((string)pid, dbProject);
            this.txtbxProjSign.Text = Z1.tpm.DB.ProjectInfo.GetProjSign((string)pid, dbProject);
            this.txtbxTester.Text = Z1.tpm.DB.ProjectInfo.GetTester((string)pid, dbProject);
            this.cmbDocSec.Text = Z1.tpm.DB.ProjectInfo.GetDocSecret((string)pid, dbProject);

            this._datachanged = false;
        }

        private void TestProjectForm_Load(object sender, EventArgs e)
        {
            FrmCommonFunc.UniformGrid(this.grid1);

            LoadData();
        }

        public override bool OnPageClose(bool bClose)
        {
            SaveData();
            gridAssist1.OnPageClose();

            return SaveToDB();
        }

        private bool SaveToDB()
        {
            // ����grid�����ݿ�
            this.grid1.UpdateData();
            if (!TestObj.UpdateObjects(dbProject, (string)pid, (string)currentvid, this._tbl))
                return false;
            else
                return true;
        }

        // ����"��Ŀ������Ϣ"
        private void SaveData()
        {
            if (this._datachanged)
                Z1.tpm.DB.ProjectInfo.SetTextContent((string)pid, dbProject, "��Ŀ��Ϣ", "�ܼ�", this.cmbDocSec.Text);
        }

        #endregion ����

        #region Grid����

        bool BeforeRowMove(DataRow drCur, DataRow drPre)
        {
            return FrmCommonFunc.BeforeRowMove(this.grid1, drCur, drPre);
        }

        bool RowMoveEventHandler(DataRow drCur, DataRow drPre)
        {
            return FrmCommonFunc.AfterRowMove(tnForm, NodeType.TestObject, drCur, drPre);
        }

        private void grid1_OnAddNew(object sender, EventArgs e)
        {
            FrmCommonFunc.OnAddNew_TestObj(this.grid1);
        }

        // "�����������"��"��д��"��Ψһ�Լ��
        private void grid1_BeforeColUpdate(object sender, BeforeColUpdateEventArgs e)
        {
            string oldvalue = string.Empty;
            string newvalue = string.Empty;

            if (!DBNull.Value.Equals(e.OldValue))
                oldvalue = (string)e.OldValue;

            if (!DBNull.Value.Equals(e.Column.DataColumn.Value))
                newvalue = (string)e.Column.DataColumn.Value;

            if (oldvalue.Equals(newvalue)) // δ�Ķ�
                return;

            // "�����������"Ψһ�Լ��
            if (e.Column.DataColumn.Caption.Equals("�����������"))
            {
                if (TestObj.ExistObjNameFromTbl(this._tbl, (string)pid, newvalue))
                {
                    MessageBox.Show("�Ѿ����ڴ����Ƶı������, �뻻����������!", "������������ظ�", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    e.Column.DataColumn.Text = oldvalue;
                    return;
                }

                //�������б����������
                foreach (TreeNode nd in tnForm.Nodes)
                {
                    NodeTagInfo taginfo = nd.Tag as NodeTagInfo;
                    if ((taginfo.id.Equals((string)grid1.Columns["ID"].Value)) &
                        (taginfo.nodeType == NodeType.TestObject))
                    {
                        taginfo.text = newvalue;
                        nd.Text = UIFunc.GenSections(tnForm, taginfo.order, ConstDef.SectionSep) + taginfo.text;
                        break;
                    }
                }
            }

            // "��д��"Ψһ�Լ��
            if (e.Column.DataColumn.Caption.Equals("��д��"))
            {
                if (TestObj.ExistObjAbbrFromTbl(this._tbl, (string)pid, newvalue))
                {
                    MessageBox.Show("��������д���ظ�!", "�����������", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    e.Column.DataColumn.Text = oldvalue;
                    return;
                }

                //������Ӧ���ڵ�ĸ�����Ϣ
                foreach (TreeNode nd in tnForm.Nodes)
                {
                    NodeTagInfo taginfo = nd.Tag as NodeTagInfo;
                    if ((taginfo.id.Equals((string)grid1.Columns["ID"].Value)) &
                        (taginfo.nodeType == NodeType.TestObject))
                    {
                        taginfo.keySign = (string)grid1.Columns["��д��"].Value;
                        break;
                    }
                }
            }
        }

        private string _delobjid = null;
        private string _delobjtid = null;
        private void grid1_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            if (this.grid1.AddNewMode != AddNewModeEnum.NoAddNew) // ɾ��������¼
                return;

            LeftTreeUserControl ltuc = FrmCommonFunc.GetParentFrm(this);
            if ((ltuc != null) && (ltuc is TestTreeForm))
            {
                if ((ltuc as TestTreeForm).IsRegressExec)
                {
                    TreeNode node = FrmCommonFunc.GetTreeNode(tnForm, (string)grid1.Columns["ID"].Value);
                    if(node != null)
                        if (node.Nodes.Count != 0)
                        {
                            MessageBox.Show("�˱�������·ǿ�!!ɾ��ǰ����ɾ���������ӽڵ�!!", "����ʧ��", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            e.Cancel = true;
                            return;
                        }
                        else
                        {
                            if (!SaveToDB())    // �Ƚ��������grid���µ����ݿ�
                            {
                                e.Cancel = true;
                                return;
                            }

                            this._delobjid = (string)grid1.Columns["�������ID"].Value;
                            this._delobjtid = (string)grid1.Columns["ID"].Value;
                            return;
                        }
                }
            }

            if (MessageBox.Show("ȷʵҪɾ���˱��������?\n(�˱�����������нڵ㽫һ��ɾ��!)",
                "ɾ���������", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            if (!SaveToDB())    // �Ƚ��������grid���µ����ݿ�
            {
                e.Cancel = true;
                return;
            }

            this._delobjid = (string)grid1.Columns["�������ID"].Value;
            this._delobjtid = (string)grid1.Columns["ID"].Value;
        }

        private void grid1_AfterDelete(object sender, EventArgs e)
        {
            /* ֱ��ɾ��������ӵ��¼�¼�����ᴥ�����¼� */

            //�������ݿ�,��ɾ�����µ������ӽڵ�
            TestObj.DeleteObj(dbProject, (string)pid, (string)currentvid, this._delobjid);

            //ɾ����Ӧ���ڵ�
            TreeNode node = FrmCommonFunc.GetTreeNode(tnForm, _delobjtid);
            UIFunc.DeleteTreeNode(node);

            this._tbl.AcceptChanges();
        }

        private void grid1_AfterInsert(object sender, EventArgs e)
        {
            // ����������½ڵ�
            TreeNode node = new TreeNode();

            NodeTagInfo taginfo = new NodeTagInfo();
            int index = this._tbl.Rows.Count - 1;

            taginfo.id = (string)_tbl.Rows[index]["ID"];
            taginfo.nodeType = NodeType.TestObject;
            taginfo.keySign = (string)_tbl.Rows[index]["��д��"];
            taginfo.order = (int)_tbl.Rows[index]["���"];
            taginfo.text = (string)_tbl.Rows[index]["�����������"];
            taginfo.verId = (string)currentvid;

            node.Text = UIFunc.GenSections(tnForm, taginfo.order, ConstDef.SectionSep) +
                taginfo.text;
            node.Tag = taginfo;
            UIFunc.SetNodeImageKey(node, NodeType.TestObject);

            tnForm.Nodes.Add(node);

            if (!tnForm.IsExpanded)
                tnForm.Expand();
        }

        #endregion Grid����

        #region �¼�����

        private bool _datachanged = false;
        private void cmbDocSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            _datachanged = true;
        }

        #endregion �¼�����
    }
}