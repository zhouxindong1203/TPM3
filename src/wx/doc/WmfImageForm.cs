using System.Xml;
using System.Reflection;
using System.Drawing;
using System.IO;
using Common;

namespace TPM3.Sys
{
    [TypeNameMap("wx.WmfImageForm")]
    public partial class WmfImageForm : MyBaseForm
    {
        const string baseDirectory = "TPM3.Config.res.";

        public WmfImageForm()
        {
            InitializeComponent();
        }

        public override bool OnPageCreate()
        {
            ShowImage(docTN.nodeElement.GetAttribute("Image"));
            return true;
        }

        public void ShowImage(string imagePath)
        {
            Assembly a = Assembly.GetEntryAssembly();
            using( Stream stream = a.GetManifestResourceStream(baseDirectory + imagePath) )
            {
                Image i = Image.FromStream(stream);
                pb1.Image = i;
                this.AutoScrollMinSize = new Size(i.Width, i.Height);
            }
        }
    }

    [TypeNameMap("wx.WmfImageFormSelect")]
    public class WmfImageFormSelect : WmfImageForm
    {
        public override bool OnPageCreate()
        {
            XmlElement ele = docTN.nodeElement;
            string image = ele.GetAttribute("Image1");
            if( docTN.Nodes.Count > 0 )
            {     
                DocTreeNode child = docTN.Nodes[0] as DocTreeNode;
                if( child.documentName != "需求分析" )  // 文档名
                    image = ele.GetAttribute("Image2");
            }
            ShowImage(image);
            return true;
        }
    }
}