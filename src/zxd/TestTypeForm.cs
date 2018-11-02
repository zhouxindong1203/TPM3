using System;
using System.Data;
using C1.Win.C1TrueDBGrid;
using Common;
using Common.TrueDBGrid;
using TPM3.Sys;
using TPM3.zxd.Helper;
using Z1.tpm;
using Z1.tpm.DB;
using System.Windows.Forms;
using TPM3.zxd.clu;
using System.Drawing;

namespace TPM3.zxd
{
    public partial class TestTypeForm : MyBaseForm
    {
        #region ����

        TrueDBGridAssist gridAssist1;

        public static ColumnPropList columnList2 = GridAssist.GetColumnPropList<TestTypeForm>(3); // ����"������"

        private DataTable _tbl;
        public DataTable Tbl
        {
            get
            {
                return this._tbl;
            }
        }

        private NodeTagInfo _taginfo;
        private string _typeeid;
        private TestTypeSub _subnodetype;
        public TestTypeSub SubNodeType
        {
            get
            {
                return _subnodetype;
            }
        }

        public string TypeName
        {
            set
            {
                this.txtbxTypeName.Text = value;

                this.expandablePanel1.TitleText = "�������� [" + value + "]";
                this.grid1.Caption = "���������ͼ� [" + value + "]";
                this.grid2.Caption = "����� [" + value + "]";

                _datachanged = true;
            }
        }

        public TestTypeForm()
        {
            InitializeComponent();
        }

        static TestTypeForm()
        {
            columnList2.Add("���", 50);
            columnList2.Add("����������", 120);
            columnList2.Add("��д��", 60);
            columnList2.Add("������˵��������Ҫ��", 160);
            columnList2.Add("���Է���˵��", 160, "���Բ����뷽��");
            columnList2.Add("�����Ҫ��", 160, "�����Ҫ��");
            columnList2.Add("׷�ٹ�ϵ", 120);
            columnList2.Add("���б�׼", 120);
            columnList2.Add("Լ������", 120);
            columnList2.Add("��ֹ����", 120);
            columnList2.Add("���ȼ�", 60);

            columnList2.AddValidator(new NotNullValidator("����������"));
            columnList2.AddValidator(new NotNullValidator("��д��"));
        }

        public override bool OnPageCreate()
        {
            // ���˲������͵��ӽڵ�����
            _taginfo = tnForm.Tag as NodeTagInfo;
            if(_taginfo == null)
                throw (new InvalidOperationException("�޷���ȡ���ڵ㸽����Ϣ!"));

            _typeeid = TestType.GetEntityID(dbProject, _taginfo.id);
            if(string.Empty.Equals(_typeeid))
                throw (new InvalidOperationException("�޷���ȡ��������ʵ��ID!"));

            _subnodetype = TestType.GetSubType(dbProject, (string)pid, _typeeid);
            if(_subnodetype == TestTypeSub.NonDefinition)
                throw (new InvalidOperationException("�������͵��ӽڵ�����δ����!"));

            // ����
            if(_subnodetype == TestTypeSub.SubType)        // �ӽڵ�����: ����������
            {
                gridAssist1 = new TrueDBGridAssist(grid1, "ID", "���");
                gridAssist1.columnList = TestObjForm.columnList1;
                this.grid2.Visible = false;

                //"������������"����༭��
                FrmCommonFunc.TestTypeEditor(gridAssist1, mapList_afterRowSelectEvent);
            }
            else if(_subnodetype == TestTypeSub.TestItem)  // �ӽڵ�����: ������
            {
                gridAssist1 = new TrueDBGridAssist(grid2, "ID", "���");
                gridAssist1.columnList = columnList2;
                this.grid1.Visible = false;

                // "���ȼ�"��"׷�ٹ�ϵ"����༭��
                FrmCommonFunc.PriorLevelEditor(gridAssist1);
                FrmCommonFunc.TraceEditor(gridAssist1, currentvid);
            }

            gridAssist1.rowPosition.GetTreeNodeKeyEvent += FrmCommonFunc.GetTreeNodeKey;
            gridAssist1.rowPosition.AfterRowMoveDown += RowMoveEventHandler;
            gridAssist1.rowPosition.AfterRowMoveUp += RowMoveEventHandler;
            gridAssist1.rowPosition.BeforeRowMoveDown += BeforeRowMove;
            gridAssist1.rowPosition.BeforeRowMoveUp += BeforeRowMove;

            // ���ݰ�
            if(_subnodetype == TestTypeSub.SubType)
            {
                this._tbl = TestType.GetTypesFromType(dbProject, (string)pid, (string)currentvid, _typeeid);
                if(this._tbl == null)
                    return false;

                gridAssist1.DataSource = this._tbl;

                // ��"�ӽڵ�����"��ComboBox���뼰��ʾ�ı����Ӧֵ������
                ValueItems items = grid1.Columns["�ӽڵ�����"].ValueItems;
                FrmCommonFunc.TransSubnodeType(items);

                // "�ӽڵ�����"��ֹ�û�����
                C1DataColumn col = this.grid1.Columns["�ӽڵ�����"];
                this.grid1.Splits[0].DisplayColumns[col].DropDownList = true;
            }
            else if(_subnodetype == TestTypeSub.TestItem)
            {
                this._tbl = TestItem.GetItemsFromType(dbProject, (string)pid, (string)currentvid, _typeeid);
                if(this._tbl == null)
                    return false;

                gridAssist1.DataSource = this._tbl;

                // ������ĸ���/ճ��
                gridAssist1.gridClipboard.CopyEvent += grid_CopyEvent;
                gridAssist1.gridClipboard.PasteEvent += grid_PasteEvent;
            }

            gridAssist1.rowPosition.tnc = tnForm.Nodes;
            gridAssist1.OnPageCreate();

            this.grid1.AllowRowSizing = (RowSizingEnum)us.rowSizeingList[1];
            this.grid2.AllowRowSizing = (RowSizingEnum)us.rowSizeingList[2];

            this.cmbSubNodeType.Enabled = false;

            return true;
        }

        /// <summary>
        /// �û�ѡ�� ������������ ���,���¼�д��
        /// </summary>
        void mapList_afterRowSelectEvent(ColumnRefMap cm, object key)
        {
            FrmCommonFunc.AfterChangeTestType(cm.dt, this.grid1, tnForm, key);
        }

        bool BeforeRowMove(DataRow drCur, DataRow drPre)
        {
            C1TrueDBGrid grid = null;
            if(_subnodetype == TestTypeSub.SubType)
                grid = this.grid1;
            else if(_subnodetype == TestTypeSub.TestItem)
                grid = this.grid2;

            if(grid == null)
                return false;

            return FrmCommonFunc.BeforeRowMove(grid, drCur, drPre);
        }

        bool RowMoveEventHandler(DataRow drCur, DataRow drPre)
        {
            //���������������ڵ��order�Լ��������½ں�
            return FrmCommonFunc.AfterRowMove(tnForm, drCur, drPre);
        }

        void grid_CopyEvent(object sender, EventArgs e)
        {
            // ����"������"
            //CommonTestData ctd = _nodeCur.Tag as CommonTestData;
            //if (ctd == null)
            //    return;
            //if (ctd.NodeType != NodeType.TestItem)
            //    return;

            //GlobalTempData.tnSrc = _nodeCur;

            //CommonTestData.CopyTestItemToClipboard(ctd.ID);
            //_nodeforcopy = _nodeCur;
        }

        void grid_PasteEvent(object sender, EventArgs e)
        {
            //MessageBox.Show("Paste!!");
        }

        private bool _datachanged = false;
        // ��ȡ"�������"�����Ϣ
        private void LoadData()
        {
            DataRow row = TestType.GetTestTypeInfo(dbProject, _taginfo.id);
            if(row == null)
                return;

            this.txtbxTypeName.Text = row["������������"].Equals(DBNull.Value) ? string.Empty : (string)row["������������"];
            this.cmbSubNodeType.SelectedIndex = (int)row["�ӽڵ�����"] - 1;
            this.txtbxAbbr.Text = row["��д��"].Equals(DBNull.Value) ? string.Empty : (string)row["��д��"];
            this.txtbxWorkTime.Text = row["Ԥ�ƹ���ʱ��"].ToString();
            if(!DBNull.Value.Equals(row["����Ҫ��"]))
                this.rich1.SetRichData((byte[])row["����Ҫ��"]);

            this._datachanged = false;

            this.expandablePanel1.TitleText = "�������� [" + this.txtbxTypeName.Text + "]";
            this.grid1.Caption = "���������ͼ� [" + this.txtbxTypeName.Text + "]";
            this.grid2.Caption = "����� [" + this.txtbxTypeName.Text + "]";
        }

        private void TestTypeForm_Load(object sender, EventArgs e)
        {
            foreach(string s in ConstDef.subtype)
                this.cmbSubNodeType.Items.Add(s);

            FrmCommonFunc.UniformGrid(this.grid1);
            FrmCommonFunc.UniformGrid(this.grid2, 36);

            LoadData();

            if(_subnodetype == TestTypeSub.TestItem)
            {
                // �������
                int level = BusiLogic.GetTestTypeLevel(tnForm);
                if(level >= (ConstDef.MaxLevel - 1))
                {
                    this.cmbSubNodeType.Enabled = false;
                }

                this.grid2.MarqueeStyle = MarqueeEnum.SolidCellBorder;

                this.grid2.Splits[0].DisplayColumns["������˵��������Ҫ��"].Button = true;
                this.grid2.Splits[0].DisplayColumns["���Է���˵��"].Button = true;
                this.grid2.Splits[0].DisplayColumns["��ֹ����"].Button = true;
                this.grid2.Splits[0].DisplayColumns["�����Ҫ��"].Button = true;

                this.grid2.EditDropDown = true;
            }

            SetInfoEditable();
        }

        private void SetInfoEditable()
        {
            LeftTreeUserControl ltuc = FrmCommonFunc.GetParentFrm(this);
            if ((ltuc != null) && (ltuc is TestTreeForm))
            {
                if ((ltuc as TestTreeForm).IsRegressExec)   // �ع����ִ�н׶�
                {
                    NodeTagInfo tag = tnForm.Tag as NodeTagInfo;
                    if (tag != null)
                    {
                        if (tag.IsRegressCreate)            // �̳ж���
                        {
                            this.txtbxTypeName.ReadOnly = true;
                            this.txtbxAbbr.ReadOnly = true;
                            this.rich1.ReadOnly = true;
                            this.cmbSubNodeType.Enabled = true;

                            this.txtbxTypeName.BackColor = Color.WhiteSmoke;
                            this.txtbxAbbr.BackColor = Color.WhiteSmoke;
                            this.rich1.BackColor = Color.WhiteSmoke;
                            this.cmbSubNodeType.BackColor = Color.WhiteSmoke;
                        }
                    }
                }
            }
        }

        private void grid2_ButtonClick(object sender, ColEventArgs e)
        {
            this.grid2.EditActive = true;
        }

        public override bool OnPageClose(bool bClose)
        {
            SaveData();
            gridAssist1.OnPageClose();

            return SaveToDB();
        }

        public bool SaveToDB()
        {
            if(_subnodetype == TestTypeSub.SubType)
            {
                // ����grid1�����ݿ�
                this.grid1.UpdateData();
                if(!TestType.UpdateType(dbProject, (string)pid, (string)currentvid, this._tbl))
                    return false;
                else
                    return true;

            }
            else if(_subnodetype == TestTypeSub.TestItem)
            {
                // ����grid2�����ݿ�
                this.grid2.UpdateData();
                if(!TestItem.UpdateItem(dbProject, (string)pid, (string)currentvid, this._tbl))
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        // ����"��������"��Ϣ
        private void SaveData()
        {
            if(_datachanged)
            {
                TestType.UpdateTypeInfo(dbProject, _typeeid, this.txtbxTypeName.Text, this.txtbxAbbr.Text,
                    this.cmbSubNodeType.SelectedIndex + 1, this.rich1.GetRichData());
            }
        }

        #endregion ����

        #region grid1

        private void txtbxTypeName_TextChanged(object sender, EventArgs e)
        {
            this._datachanged = true;
        }

        private void grid1_OnAddNew(object sender, EventArgs e)
        {
            object id = FunctionClass.NewGuid;
            object entityid = FunctionClass.NewGuid;

            this.grid1.Columns["ID"].Value = id;
            this.grid1.Columns["ʵ��ID"].Value = entityid;
            this.grid1.Columns["��������ID"].Value = entityid;
            this.grid1.Columns["��ĿID"].Value = pid;
            this.grid1.Columns["���԰汾"].Value = currentvid;

            this.grid1.Columns["����������ID"].Value = _typeeid;
            this.grid1.Columns["�����������ID"].Value = DBNull.Value;
            this.grid1.Columns["�����汾ID"].Value = currentvid;

            if(BusiLogic.GetTestTypeLevel(tnForm) >= (ConstDef.MaxLevel - 1))
            {
                this.grid1.Columns["�ӽڵ�����"].Value = 2;
                this.grid1.Splits[0].DisplayColumns["�ӽڵ�����"].Locked = true;
            }
            else
            {
                this.grid1.Columns["�ӽڵ�����"].Value = 1;
            }
        }

        private void grid1_AfterColUpdate(object sender, ColEventArgs e)
        {
            string col = e.Column.Name;
            if(col.Equals("������������"))
                FrmCommonFunc.GridAfterColUpdate(this.grid1, "ID", "������������", string.Empty, tnForm);
            else if(col.Equals("��д��"))
                FrmCommonFunc.GridAfterColUpdate(this.grid1, "ID", string.Empty, "��д��", tnForm);
        }

        private string _deltypeeid = null;
        private string _deltypetid = null;
        private void grid1_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            if(this.grid1.AddNewMode != AddNewModeEnum.NoAddNew) // ɾ��������¼
                return;

            LeftTreeUserControl ltuc = FrmCommonFunc.GetParentFrm(this);
            if ((ltuc != null) && (ltuc is TestTreeForm))
            {
                if ((ltuc as TestTreeForm).IsRegressExec)
                {
                    TreeNode node = FrmCommonFunc.GetTreeNode(tnForm, (string)grid1.Columns["ID"].Value);
                    if (node != null)
                        if (node.Nodes.Count != 0)
                        {
                            MessageBox.Show("�˲��������·ǿ�!!ɾ��ǰ����ɾ���������ӽڵ�!!", "����ʧ��", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            e.Cancel = true;
                            return;
                        }
                }
            }

            FrmCommonFunc.ProcBeforeDelete proc = new FrmCommonFunc.ProcBeforeDelete(SaveToDB);

            bool ret = FrmCommonFunc.OnBeforeDelete_TestType(this.grid1, proc, ref _deltypeeid, ref _deltypetid);
            if(ret)
                e.Cancel = true;
        }

        private void grid1_AfterDelete(object sender, EventArgs e)
        {
            /* ֱ��ɾ���������ӵ��¼�¼�����ᴥ�����¼� */

            FrmCommonFunc.OnAfterDelete_TestType(this.grid1, tnForm, _deltypetid, _deltypeeid, this._tbl);
        }

        private void grid1_AfterInsert(object sender, EventArgs e)
        {
            // �����������½ڵ�
            FrmCommonFunc.OnAfterInsert_TestType(this._tbl, tnForm);
        }

        private void grid1_ComboSelect(object sender, ColEventArgs e)
        {
            if(this.grid1.AddNewMode != AddNewModeEnum.NoAddNew)
                return;

            // ֻ���ڲ����������ӽڵ�ʱ�������޸�"�ӽڵ�����"
            FrmCommonFunc.BeforeChangeSubtype(this.grid1, tnForm);
        }

        #endregion grid1

        #region grid2

        private void grid2_OnAddNew(object sender, EventArgs e)
        {
            object id = FunctionClass.NewGuid;
            object entityid = FunctionClass.NewGuid;

            this.grid2.Columns["ID"].Value = id;
            this.grid2.Columns["ʵ��ID"].Value = entityid;
            this.grid2.Columns["������ID"].Value = entityid;
            this.grid2.Columns["��ĿID"].Value = pid;
            this.grid2.Columns["���԰汾"].Value = currentvid;
            this.grid2.Columns["���"].Value = Tbl.Rows.Count + 1;

            this.grid2.Columns["������������ID"].Value = _typeeid;
            this.grid2.Columns["���ڵ�ID"].Value = DBNull.Value;

            grid2.Columns["�����Ҫ��"].Value = DefaultText.GetItemSufficientDefaultText();
            this.grid2.Columns["��ֹ����"].Value = DefaultText.GetItemTerminateDefaultText();
            grid2.Columns["���б�׼"].Value = DefaultText.GetItemEvaluateDefaultText();
            grid2.Columns["Լ������"].Value = DefaultText.GetItemConstrainDefaultText();
            this.grid2.Columns["�����汾ID"].Value = currentvid;
        }

        private void grid2_AfterColUpdate(object sender, ColEventArgs e)
        {
            string col = e.Column.Name;
            if(col.Equals("����������"))
                FrmCommonFunc.GridAfterColUpdate(this.grid2, "ID", "����������", string.Empty, tnForm);
            else if(col.Equals("��д��"))
                FrmCommonFunc.GridAfterColUpdate(this.grid2, "ID", string.Empty, "��д��", tnForm);
        }

        private string _delitemeid = null;
        private string _delitemtid = null;
        private void grid2_BeforeDelete(object sender, C1.Win.C1TrueDBGrid.CancelEventArgs e)
        {
            if(this.grid2.AddNewMode != AddNewModeEnum.NoAddNew) // ɾ��������¼
                return;

            if (!SaveToDB())
            {
                e.Cancel = true;
                return;
            }

            LeftTreeUserControl ltuc = FrmCommonFunc.GetParentFrm(this);
            if ((ltuc != null) && (ltuc is TestTreeForm))
            {
                if ((ltuc as TestTreeForm).IsRegressExec)
                {
                    string itemeid = this.grid2.Columns["ʵ��ID"].Value as string;
                    if (!TestItem.IsSameVer(dbProject, itemeid, currentvid as string))
                    {
                        InputText input = new InputText();
                        if (input.ShowDialog() == DialogResult.OK)
                        {
                            object trace = TestItem.GetItemTrace(dbProject, this.grid2.Columns["ID"].Value as string);
                            string objtid = FrmCommonFunc.GetBelongObjID(tnForm);
                            Regress.InsertUntest(dbProject, pid, itemeid, 2, input.InputReason, trace, currentvid, objtid);

                            FrmCommonFunc.OnBeforeDelete_TestItem(this.grid2, null, ref _delitemeid, ref _delitemtid, false);
                        }
                        else
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                    else
                    {
                        if (FrmCommonFunc.OnBeforeDelete_TestItem(this.grid2, null, ref _delitemeid, ref _delitemtid, true))
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }
                else
                {
                    if (FrmCommonFunc.OnBeforeDelete_TestItem(this.grid2, null, ref _delitemeid, ref _delitemtid, true))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void grid2_AfterDelete(object sender, EventArgs e)
        {
            /* ֱ��ɾ���������ӵ��¼�¼�����ᴥ�����¼� */
            FrmCommonFunc.OnAfterDelete_TestItem(this.grid2, tnForm, _delitemtid, _delitemeid, this._tbl);
        }

        private void grid2_AfterInsert(object sender, EventArgs e)
        {
            FrmCommonFunc.OnAfterInsert_TestItem(this._tbl, tnForm);
        }

        #endregion grid2

        #region typeinfo

        private void txtbxTypeName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = this.txtbxTypeName.Text;

            //�������б����������
            NodeTagInfo taginfo = tnForm.Tag as NodeTagInfo;
            if(taginfo == null)
                return;

            taginfo.text = input;
            tnForm.Text = UIFunc.GenSections(tnForm.Parent, taginfo.order, ConstDef.SectionSep) +
                taginfo.text;

            this.expandablePanel1.TitleText = "�������� [" + input + "]";
            this.grid1.Caption = "���������ͼ� [" + input + "]";
            this.grid2.Caption = "����� [" + input + "]";
        }

        private void txtbxAbbr_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string input = this.txtbxAbbr.Text;

            NodeTagInfo taginfo = tnForm.Tag as NodeTagInfo;
            if(taginfo == null)
                return;

            taginfo.keySign = input;
        }

        #endregion typeinfo
    }
}