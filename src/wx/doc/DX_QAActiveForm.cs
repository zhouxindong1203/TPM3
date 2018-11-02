using System;
using System.Collections.Generic;
using Common;
using Common.RichTextBox;
using TPM3.Sys;

namespace TPM3.wx
{
    /// <summary>
    /// 测试环境概述(定型测评大纲用)
    /// </summary>
    [TypeNameMap("wx.DX_QAActiveForm")]
    public partial class DX_QAActiveForm : MyBaseForm
    {
        Dictionary<RichTextBoxOle, string> map = new Dictionary<RichTextBoxOle, string>();
        Dictionary<RichTextBoxOle, string> mapDefault = new Dictionary<RichTextBoxOle, string>();

        public DX_QAActiveForm()
        {
            InitializeComponent();
            map[rich1] = "产品审核";
            map[rich2] = "过程审核";
            map[rich3] = "质量评审";
            map[rich4] = "不符合项跟踪和验证";

            mapDefault[rich1] = "对软件鉴定测评大纲、测试说明、测试记录、软件问题报告、回归测试方案、回归测试记录、测试报告等产品进行审核。产品审核应在文档编制基本完成之后、提交评审之前进行。";
            mapDefault[rich2] = "对测试需求分析与策划、测试设计与实现、测试执行（包含回归测试）、测试总结等阶段进行过程审核。过程审核可与内部评审同时开展，或在外部评审之前进行。";
            mapDefault[rich3] = "按照要求，对测试需求分析与策划、测试设计与实现、测试总结三个阶段进行评审。质量保证员参加各评审活动，对评审发现的问题进行跟踪直至问题解决。";
            mapDefault[rich4] = "质量保证员依据《不符合项控制程序》对过程审核、产品审核、评审等质量保证活动中发现的不符合项进行记录、跟踪和验证。";
        }

        void DX_QAActiveForm_Load(object sender, EventArgs e)
        {
            foreach(var rich in map.Keys)
            {
                byte[] v = ProjectInfo.GetDocContent(dbProject, pid, currentvid, null, map[rich]);
                if(IOleObjectAssist.IsOleBufferEmpty(v)) v = IOleObjectAssist.GetByteFromString(mapDefault[rich]);
                rich.SetRichData(v);
            }
        }

        public override bool OnPageClose(bool bClose)
        {
            foreach(var de in map)
                ProjectInfo.SetDocContent(dbProject, pid, currentvid, null, de.Value, de.Key.GetRichData());
            return true;
        }
    }
}