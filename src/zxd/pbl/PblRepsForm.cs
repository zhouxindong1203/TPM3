using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Aspose.Cells;
using Aspose.Words;
using Common;
using Common.Aspose;
using Common.RichTextBox;
using TPM3.Sys;
using Z1.tpm;
using Z1.tpm.DB;

namespace TPM3.zxd.pbl
{
    [TypeNameMap("zxd.pbl.PblRepsForm")]
    public partial class PblRepsForm : LeftTreeUserControl
    {
        // ���ڴ�����ⱨ�浥���и��ֱ�ʶ�ֶε����
        private Dictionary<string, List<PblSignsComb>> _pblsigns = new Dictionary<string, List<PblSignsComb>>();
        private Dictionary<string, string> _objsname = new Dictionary<string, string>(); // ID -> Name(obj)
        private Dictionary<string, string> _pblsname = new Dictionary<string, string>(); // ID -> Name(pbl)

        public TestTreeForm ttf = null;
        public string PblSpl;
        public DataTable tblPblSigns;

        #region ����

        private string _usevid = (string)currentvid;    // ��ǰ�����İ汾ID
        public string UseVid
        {
            get
            {
                return _usevid;
            }
        }

        private bool _preregress = false;   // �Ƿ�ع����׼���׶�
        public bool PreRegress
        {
            get
            {
                return _preregress;
            }
        }

        public PblRepsForm()
        {
            InitializeComponent();

            PblSignsComb.g_pblspl = ConstDef.PblSplitter();
            PblSignsComb.g_objsname = _objsname;
            PblSignsComb.g_pblsname = _pblsname;
        }

        public void SimulateFormLoad()
        {
            // �ع���Բ�ͬ�׶μ��(��ǰ�汾����֮ǰ�汾�����ⱨ�浥)
            DocTreeNode node = tnForm as DocTreeNode;
            if((node != null) && (node.nodeElement.HasAttribute("mode")))
            {
                if(node.nodeElement.GetAttribute("mode").Equals(ConstDef.RegressSign))
                {
                    if(Z1.tpm.DB.ProjectInfo.HasPreviousVer(dbProject, (string)pid, (string)currentvid))
                        _usevid = Z1.tpm.DB.ProjectInfo.GetPreviousVer(dbProject,
                            (string)pid, (string)currentvid);
                    _preregress = true;
                }
            }

            CommonDB.FillPblSignDic(dbProject, (string)pid, /*(string)currentvid*/_usevid,
                ref _pblsname);
        }

        private void PblRepsForm_Load(object sender, EventArgs e)
        {
            // �ع���Բ�ͬ�׶μ��(��ǰ�汾����֮ǰ�汾�����ⱨ�浥)
            DocTreeNode node = tnForm as DocTreeNode;
            if((node != null) && (node.nodeElement.HasAttribute("mode")))
            {
                if(node.nodeElement.GetAttribute("mode").Equals(ConstDef.RegressSign))
                {
                    if(Z1.tpm.DB.ProjectInfo.HasPreviousVer(dbProject, (string)pid, (string)currentvid))
                        _usevid = Z1.tpm.DB.ProjectInfo.GetPreviousVer(dbProject,
                            (string)pid, (string)currentvid);
                    _preregress = true;
                }
            }

            CommonDB.FillPblSignDic(dbProject, (string)pid, /*(string)currentvid*/_usevid,
                ref _pblsname);

            treeView1.BeforeSelect += treeView1_BeforeSelect;

            FillPblSigns();

            BuildTree();
            treeView1.HideSelection = false;

            ttf = new TestTreeForm();
            ttf.BuildTestTree(_usevid);

            PblSpl = ConstDef.PblSplitter();
            tblPblSigns = CommonDB.GetPblSigns(dbProject, (string)pid, /*(string)currentvid*/_usevid);

            //if (ExecStatus.g_PblRepsFormALink != null)
            //{
            //    ExecStatus.g_PblRepsFormALink.Close();
            //    ExecStatus.g_PblRepsFormALink = null;
            //}

            if(paramList.ContainsKey("id"))
            {
                string urlid = paramList["id"];
                LocatePbl(urlid);
            }
        }

        /// <summary>
        /// ��"���ⱨ�浥"���еĸ����ֶε������䵽������
        /// </summary>
        private void FillPblSigns()
        {
            DataTable tbl = TestObj.GetObjsSPV(dbProject, (string)pid, /*(string)currentvid*/_usevid);
            foreach(DataRow row in tbl.Rows)
            {
                if(!_objsname.ContainsKey((string)row["ID"]))
                    _objsname.Add((string)row["ID"], (string)row["�����������"]);
                List<PblSignsComb> li = new List<PblSignsComb>();

                // ��ȡÿ����������µ��������ⱨ�浥���
                using(DataTable tbl1 = CommonDB.GetPblRepsForObj(dbProject, (string)row["ID"]))
                {
                    foreach(DataRow row1 in tbl1.Rows)
                    {
                        PblSignsComb psc = new PblSignsComb(row1);
                        if(!li.Contains(psc))
                            li.Add(psc);
                    }
                }

                if(!_pblsigns.ContainsKey((string)row["ID"]))
                    _pblsigns.Add((string)row["ID"], li);
            }
        }


        private void BuildTree()
        {
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            // �������ĸ��ڵ�
            TreeNode root = new TreeNode("���ⱨ�浥");
            root.SelectedImageKey = "question.bmp";
            root.ImageKey = "question.bmp";
            treeView1.Nodes.Add(root);

            foreach(KeyValuePair<string, List<PblSignsComb>> entry in _pblsigns)
            {
                TreeNode nodeobj = new TreeNode(_objsname[entry.Key]);
                nodeobj.Name = entry.Key;
                nodeobj.ImageKey = "object.bmp";
                nodeobj.SelectedImageKey = "object.bmp";
                root.Nodes.Add(nodeobj);

                foreach(PblSignsComb psc in entry.Value)
                {
                    TreeNode node1;
                    if(psc.ToString().Equals(string.Empty))
                        node1 = new TreeNode("{δ����}");
                    else
                        node1 = new TreeNode(psc.ToString());
                    node1.ImageKey = "symbol.bmp";
                    node1.SelectedImageKey = "symbol.bmp";
                    nodeobj.Nodes.Add(node1);

                    using(DataTable tbl = CommonDB.GetSameCombPbls(dbProject, psc.firstid, psc.secondid,
                        psc.thirdid, psc.fouthid, psc.objid))
                    {
                        foreach(DataRow row in tbl.Rows)
                        {
                            TreeNode node2 = new TreeNode(row["ͬ��ʶ���"] + "[" + row["����"] + "]");
                            node2.ImageKey = "sheet.bmp";
                            node2.SelectedImageKey = "sheet.bmp";
                            node2.Tag = (string)row["ID"] + ";" + TestObj.GetObjAbbr(dbProject, psc.objid);
                            node1.Nodes.Add(node2);
                        }
                    }
                }
            }

            treeView1.EndUpdate();
            treeView1.ExpandAll();
        }

        public override bool OnPageCreate()
        {
            MainForm.mainFrm.VisibleReorder(true);
            return base.OnPageCreate();
        }

        public override Control GetSubForm(TreeNode tn)
        {
            string pbltag = tn.Tag as string;

            if(pbltag == null)
                return new Form();

            string[] tags = pbltag.Split(';');
            string pblid = tags[0];
            string objabbr = tags[1];

            return new PblRepsRightForm(pblid, objabbr);
        }

        public override void OnShowForm(TreeNode tn, Form f)
        {
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(f);
            f.Dock = DockStyle.Fill;
            f.Visible = true;
        }

        public override bool OnPageClose(bool bClose)
        {
            MainForm.mainFrm.VisibleReorder(false);
            //if (currentSelectedForm != null)
            //{
            //    IBaseTreeForm bf = currentSelectedForm as IBaseTreeForm;
            //    if (bf != null)
            //        bf.OnPageClose(false);
            //}
            return base.CloseCurrentForm(bClose, false);
        }

        private void PblRepsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //OnPageClose(false);
        }

        #endregion ����


        private void Reorder()
        {
            CommonDB.Reorder(dbProject, _pblsigns);
        }

        private void miReorder_Click(object sender, EventArgs e)
        {
            if(DialogResult.Cancel == MessageBox.Show("ȷ������ͬ�����ʶ�µ����ⱨ�浥�����?\n(ʹ���ڱ��浥��ɾ�����µ���Ų�����������)",
                "������ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
                return;

            string selid = null;
            if(this.treeView1.SelectedNode != null)
                selid = this.treeView1.SelectedNode.Tag as string;

            Reorder();
            BuildTree();

            if(selid != null)
            {
                TreeNode selnode = FrmCommonFunc.GetTreeNode1(treeView1, selid);
                if(selnode != null)
                    treeView1.SelectedNode = selnode;
            }
        }

        public void PerformRecorder()
        {
            miReorder_Click(null, EventArgs.Empty);
        }

        public void LocatePbl(object id)
        {
            foreach(TreeNode tnobj in treeView1.Nodes[0].Nodes)
                foreach(TreeNode tnSign in tnobj.Nodes)
                    foreach(TreeNode tnFall in tnSign.Nodes)
                        //if (Equals(id, tnFall.Tag))
                        if((tnFall.Tag as string).Contains(id as string))
                        {
                            treeView1.SelectedNode = tnFall;
                            tnFall.EnsureVisible();
                            return;
                        }
            MessageBox.Show("�޷���λ�������ʶ!", "��λʧ��", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        #region ����Ĺ�������

        public Dictionary<string, List<string>> GetPblsForObj(string objtid)
        {
            FillPblSigns();

            BuildTree();

            //PblSpl = ConstDef.PblSplitter();
            //tblPblSigns = CommonDB.GetPblSigns(dbProject, (string)pid, (string)currentvid);

            TreeNode nd = null;
            TreeNode beginnode = this.treeView1.Nodes[0];
            foreach(TreeNode node in beginnode.Nodes)
            {
                if(node.Name.Equals(objtid))
                {
                    nd = node;
                    break;
                }
            }

            if(nd == null)
                return null;

            Dictionary<string, List<string>> retli = new Dictionary<string, List<string>>();
            foreach(TreeNode tn in nd.Nodes)
            {
                List<string> li = new List<string>();
                foreach(TreeNode tn1 in tn.Nodes)
                {
                    string s1 = tn1.Tag as string;
                    string s2 = s1.Split(';')[0];
                    li.Add(s2);
                }
                retli.Add(tn.Text.Equals("{δ����}") ? string.Empty : tn.Text, li);
            }

            return retli;
        }

        #endregion ����Ĺ�������

        private void testOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetPblsForObj("7GKJPCJEBEC5XO");
        }

        private Workbook _workbook;
        private Worksheet _worksheet;
        private List<string> _titls = new List<string>
        {
            "������", "��ϵͳ", "��������", "�������", "���⼶��", "��������", "�޸Ľ���", "����"
        };

        private void InitExcel()
        {
            _workbook = new Workbook();
            if (_workbook.Worksheets.Count == 0) _workbook.Worksheets.Add();

            _worksheet = _workbook.Worksheets[0];
            _worksheet.Name = "����һ����";
            var col = 0;
            foreach (var item in _titls)
            {
                _worksheet.Cells[0, col++].PutValue(item);
            }
        }

        private void _pbl_excel_Click(object sender, EventArgs e)
        {
            InitExcel();

            var count = 1;
            var row = 1;
            var objs = TestObj.GetObjectsSPV(dbProject, (string) pid, (string) currentvid);
            for (int i = 0; i < objs.Rows.Count; i++)
            {
                var obj_tid = (string)objs.Rows[i]["ID"];
                var obj_abbr = TestObj.GetObjAbbr(dbProject, obj_tid);
                var obj_name = TestObj.GetObjName(dbProject, obj_tid);
                var pbls = CommonDB.GetPblRepsForObj(dbProject, obj_tid, obj_name, obj_abbr, (string) pid,
                    (string) currentvid);
                foreach (var pbl in pbls)
                {
                    _worksheet.Cells[row, 0].PutValue(count++);
                    _worksheet.Cells[row, 1].PutValue(obj_abbr);
                    _worksheet.Cells[row, 2].PutValue(pbl.pblname);
                    _worksheet.Cells[row, 3].PutValue(GetPblInof(pbl.pblcat));
                    _worksheet.Cells[row, 4].PutValue(GetPblInof(pbl.pbllel));

                    _worksheet.Cells[row, 5].PutValue(GetPblDesc(pbl.pbldes));
                    _worksheet.Cells[row++, 6].PutValue(pbl.pblmemo);
                }
            }

            WriteToSheet();
        }

        private string GetPblInof(string id)
        {
            string sql = "select ���� from DC���⼶��� where ��ĿID=? and ID=?";
            var row = dbProject.ExecuteDataRow(sql, (string)pid, id);
            if (row == null)
                return string.Empty;
            return (string) row["����"];
        }

        private string GetPblDesc(byte[] desc_bytes)
        {
            // �ڡ������������ֶΣ���������Ϊole����
            // �����ı���ʹ��word�༭��Ϊbyte[]���ͣ�����ȡ��ʽ��һ��
            string str_desc = null;
            str_desc = IOleObjectAssist.GetStringFromByte(desc_bytes); // ��ȡֱ��������ı�
            if (str_desc == null)
                str_desc = ((Document)AsposeFactory.GetDocumentFromRich(desc_bytes)).GetText(); // ����Word������ı�
            return str_desc;
        }

        private void WriteToSheet()
        {
            var filename = Path.GetFileNameWithoutExtension(us.LastDatabaseName) + "_����һ����.xls";
            if (File.Exists(filename))
                File.Delete(filename);

            // write to file
            _workbook.Save(filename);
            MessageBox.Show(string.Format("Excel�ļ������ɣ�{0}", filename));
        }
    }
}

