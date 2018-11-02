using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TPM3.zxd;
using Z1.tpm;
using Z1.tpm.DB;
using TPM3.Sys;
using TMP3.zxd.clu;
using Common;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace DDEditor
{
    [Obfuscation(Exclude = true, ApplyToMembers = false)]
    public partial class AccEditor : C1.Win.C1Input.DropDownForm
    {
        public AccEditor()
        {
            InitializeComponent();
        }

        private TestUsecaseForm _tuf;
        private List<string>[] _li;
        private List<Accessory> _la;
        private string _stepid;

        private int col = -1;
        private void AccEditor_Open(object sender, EventArgs e)
        {
            _tuf = OwnerControl.Parent as TestUsecaseForm;
            if( _tuf == null )
                return;

            _li = _tuf.p_AccDic[_tuf.p_steptid];
            switch( _tuf.p_belong )
            {
            case "输入及操作":
                col = 0;
                _stepid = _tuf.p_stepeid;
                break;

            case "期望结果":
                col = 1;
                _stepid = _tuf.p_stepeid;
                break;

            case "实测结果":
                col = 2;
                _stepid = _tuf.p_steptid;
                break;
            }

            if( col == -1 )
                return;

            _la = TestUsecase.GetAccInfo(MyBaseForm.dbProject, _li[col]);

            this.checkedListBox1.Items.Clear();
            this.checkedListBox1.SuspendLayout();

            foreach( Accessory a in _la )
            {
                this.checkedListBox1.Items.Add(a);
            }

            this.checkedListBox1.ResumeLayout();

            this.btnDel.Enabled = false;
            this.btnViewContent.Enabled = false;
        }

        private void AccEditor_PostChanges(object sender, EventArgs e)
        {
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if( e.NewValue == CheckState.Checked )
                this.btnDel.Enabled = true;
            else
            {
                if( this.checkedListBox1.CheckedItems.Count <= 1 )
                    this.btnDel.Enabled = false;
            }
        }

        private void chbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool b = this.chbSelectAll.Checked;

            for( int i = 0; i < this.checkedListBox1.Items.Count; i++ )
                this.checkedListBox1.SetItemChecked(i, b);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1.检测附件数是否已经达到最大值
            int num = _li[0].Count + _li[1].Count + _li[2].Count;
            if( num >= ConstDef.MaxAccNum )
            {
                MessageBox.Show(this, "同一测试步骤最多只能添加 " + ConstDef.MaxAccNum.ToString() + " 个附件!", "操作提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            // 2.计算新附件的序号
            int newseq = 1;
            if( _la.Count != 0 )
                newseq = _la[_la.Count - 1].seq + 1;

            // 3.显示添加附件窗体
            using( AddAccessoryForm dlg1 = new AddAccessoryForm() )
            {
                dlg1.Text = "添加附件";
                dlg1.AccSym = GenAccSym(newseq);
                dlg1.AddStyle = 0;
                dlg1.AccListsSelIndex = -1;

                if( DialogResult.OK == dlg1.ShowDialog() )
                {
                    if( !BusiLogic.CreateAccFolder(MyBaseForm.dbProject, (string)MyBaseForm.pid, (string)MyBaseForm.currentvid) )
                    {
                        MessageBox.Show("无法创建附件文件夹!添加附件失败!!", "操作失败", MessageBoxButtons.OK,
                             MessageBoxIcon.Error);
                        return;
                    }

                    switch( dlg1.AddStyle )
                    {
                    case 0: // 添加新附件

                        Accessory a = new Accessory();

                        a.id = (string)FunctionClass.NewGuid;
                        a.eid = (string)FunctionClass.NewGuid;
                        a.name = dlg1.p_filename;
                        a.type = dlg1.p_ext;
                        a.memo = dlg1.Memo;
                        a.srcpath = dlg1.p_fullpath;
                        a.usenum = 1;
                        a.stepid = _stepid;
                        a.seq = newseq;
                        a.belong = _tuf.p_belong;

                        if( dlg1.Output ) // 打印输出时数据存入数据库中
                        {
                            a.content = BusiLogic.GetBytesFromFile(dlg1.p_fullpath);
                            a.output = true;
                        }
                        else // 不打印输出时把文件存入到附件目录中
                        {
                            a.output = false;

                            string desfile = Path.Combine(ExecStatus.g_AccFolder, dlg1.p_filename);
                            if( !BusiLogic.CopyFile(dlg1.p_fullpath, desfile) )
                            {
                                MessageBox.Show("将附件复制到附件文件夹时发生错误!", "操作失败", MessageBoxButtons.OK,
                                     MessageBoxIcon.Error);
                                return;
                            }
                        }

                        TestUsecase.AddNewAcc(MyBaseForm.dbProject, a, (string)MyBaseForm.pid,
                            (string)MyBaseForm.currentvid);

                        // 添加到当前附件列表中
                        _li[col].Add(a.id);

                        break;

                    case 1: // 从已有附件中选取
                        int selectedindex = dlg1.AccListsSelIndex;
                        if( selectedindex == -1 )
                            return;

                        Accessory a1 = new Accessory();
                        a1.id = (string)FunctionClass.NewGuid;
                        a1.stepid = _stepid;
                        a1.seq = newseq;
                        a1.eid = dlg1.SelExistAcc.eid;
                        a1.belong = _tuf.p_belong;
                        a1.usenum = dlg1.SelExistAcc.usenum + 1;

                        TestUsecase.AddExistAcc(MyBaseForm.dbProject, a1, (string)MyBaseForm.pid, (string)MyBaseForm.currentvid);

                        _li[col].Add(a1.id);

                        break;
                    } //switch(dlg1.AddStyle)

                    // 更新用测状态
                    if( (col == 2) && (_li[col].Count == 1) )
                    {
                        if( _tuf.p_rlttext.Equals(string.Empty) )
                        {
                            _tuf.ExecStatus = BusiLogic.CheckExecSta(_tuf.Tbl, _tuf.p_steptid, "anytext");
                            _tuf.ExecResult = BusiLogic.CheckExecRlt(_tuf.ExecStatus, _tuf.Tbl);
                            BusiLogic.UpdateUCIcon(_tuf.UCTid, _tuf.ExecStatus, _tuf.ExecResult, _tuf.tnForm);
                        }
                    }
                }
                else
                    return;
            }
        }

        // 生成附件标识(用例标识_StepX_I/E/R_AX)
        private string GenAccSym(int seq)
        {
            string belong = string.Empty;
            switch( _tuf.p_belong )
            {
            case "输入及操作":
                belong = "I";
                break;

            case "期望结果":
                belong = "E";
                break;

            case "实测结果":
                belong = "R";
                break;
            }

            if( belong.Equals(string.Empty) )
                return string.Empty;

            string ret = _tuf.UCSign + ConstDef.UCSignSpl + "Step" + (++_tuf.p_row).ToString() +
                ConstDef.UCSignSpl + belong + ConstDef.UCSignSpl + "A" + seq.ToString();

            return ret;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if( DialogResult.OK == MessageBox.Show("确定要删除所有选中的附件吗?(Y/N)", "操作确定", MessageBoxButtons.OKCancel,
                 MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) )
            {
                foreach( object o in checkedListBox1.CheckedItems )
                {
                    Accessory a = o as Accessory;
                    if( a != null )
                    {
                        TestUsecase.DelAcc(MyBaseForm.dbProject, a.id, a.eid);
                        _li[col].Remove(a.id);
                    }
                }

                // 更新用测状态
                if( (col == 2) && (_li[col].Count == 0) )
                {
                    if( _tuf.p_rlttext.Equals(string.Empty) )
                    {
                        _tuf.ExecStatus = BusiLogic.CheckExecSta(_tuf.Tbl, _tuf.p_steptid, string.Empty);
                        _tuf.ExecResult = BusiLogic.CheckExecRlt(_tuf.ExecStatus, _tuf.Tbl);
                        BusiLogic.UpdateUCIcon(_tuf.UCTid, _tuf.ExecStatus, _tuf.ExecResult, _tuf.tnForm);
                    }
                }

            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Accessory a = this.checkedListBox1.SelectedItem as Accessory;
            if( a == null )
                return;

            if( a.output )
                this.btnViewContent.Enabled = true;
            else
                this.btnViewContent.Enabled = false;
        }

        private void btnViewContent_Click(object sender, EventArgs e)
        {
            Accessory a = this.checkedListBox1.SelectedItem as Accessory;
            if( a == null )
                return;

            byte[] bdata = TestUsecase.GetAccContent(MyBaseForm.dbProject, a.eid);
            if( bdata == null )
                return;

            string filename = Path.GetTempFileName();
            filename = Path.ChangeExtension(filename, a.type);

            // 将数据写入临时文件
            BusiLogic.SetBytesToFile(filename, bdata);

            try
            {
                Process.Start(filename);
            }
            catch( Exception /*ex*/)
            {
                MessageBox.Show("无与此文件类型相关联的应用程序!无法打开文件!", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}