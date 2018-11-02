using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Common;
using Common.Database;
using Crownwood.Magic.Controls;
using Crownwood.Magic.Forms;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// �ڵ�ǰ���Եİ汾֮�ϴ����ع�汾
    /// </summary>
    public partial class NewVersionForm : WizardDialog
    {
        static ColumnPropList columnList1 = GridAssist.GetColumnPropList<NewVersionForm>(11);
        FlexGridAssist flexAssist1;
        DataTable dtVersionList;

        public NewVersionForm()
        {
            InitializeComponent();
            FlexGridAssist.SetFlexGridWithFocus(flex1);
            flexAssist1 = new FlexGridAssist(flex1, "ID", "���");
            flexAssist1.columnList = columnList1;
            flex1.AllowDragging = AllowDraggingEnum.Columns;
            flex1.SelectionMode = SelectionModeEnum.Row;
        }

        static NewVersionForm()
        {
            columnList1.Add("���", 50);
            columnList1.Add("�汾����", 100);
            columnList1.Add("�汾˵��", 250);
            columnList1.Add("�汾����ʱ��", 100);
            columnList1.Add("ǰ��汾����", 100);
        }

        public static DBAccess dbProject
        {
            get { return globalData.dbProject; }
        }

        public static GlobalData globalData
        {
            get { return GlobalData.globalData; }
        }

        public object pid
        {
            get { return globalData.projectID; }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        void NewVersionForm_Load(object sender, EventArgs e)
        {
            dtVersionList = DBLayer1.GetProjectVersionList(dbProject, pid, true);
            flexAssist1.DataSource = dtVersionList;
            flexAssist1.OnPageCreate();
            flex1.Row = flex1.Rows.Count - 1;

            FlexGridAssist.AutoSizeRows(flex1, 6);
            wizardControl.EnableFinishButton = WizardControl.Status.No;

            // ����ؼ���ʾ��ȫ������
            foreach(WizardPage pages in wizardControl.WizardPages)
                pages.Dock = DockStyle.Fill;
        }

        protected override void OnNextClick(object sender, CancelEventArgs e)
        {
            base.OnNextClick(sender, e);
            string msg = OnNextClick();
            if(msg != null)
            {
                if(msg != "") MessageBox.Show(msg);
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// ����null��ʾ�ɹ�������""��ʾ�д��󣬵�����Ҫ��ʾ
        /// </summary>
        protected string OnNextClick()
        {
            int selectedIndex = wizardControl.SelectedIndex;   // �ڽ�����һ��֮ǰ��ҳ���

            //ѡ��ģ��ҳ
            if(selectedIndex == 1)
            {
                if(GridAssist.IsNull(tbVersionName.Text)) return "�ع�汾���Ʋ���Ϊ��";
                foreach(DataRow dr in dtVersionList.Rows)
                {
                    if(Equals(dr["�汾����"], tbVersionName.Text))
                        return "�µĻع�汾���Ʋ��������еĻع�汾��������";
                }
            }

            if(selectedIndex == wizardControl.WizardPages.Count - 2)
                wizardControl.EnableFinishButton = WizardControl.Status.Yes;
            return null;
        }

        protected override void OnBackClick(object sender, CancelEventArgs e)
        {
            wizardControl.EnableFinishButton = WizardControl.Status.No;
            wizardControl.EnableNextButton = WizardControl.Status.Default;
        }

        /// <summary>
        /// �����ع�汾
        /// </summary>
        protected override void OnFinishClick(object sender, EventArgs e)
        {
            object vid = DBLayer1.CreateNewVersion(dbProject, pid, flex1.Rows[flex1.Row]["ID"], tbVersionName.Text, tbVersionMemo.Text);
            globalData.currentvid = vid;
            MainForm.mainFrm.VersionChanged = true;
            MainForm.mainFrm.InitFormByVersion();

            base.OnFinishClick(sender, e);
        }
    }
}