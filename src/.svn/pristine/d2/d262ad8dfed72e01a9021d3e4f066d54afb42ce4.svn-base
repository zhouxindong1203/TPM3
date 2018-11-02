using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TPM3.Sys
{
    class MySplitter : Splitter
    {
        static readonly HatchBrush br = new HatchBrush(HatchStyle.Percent60, Color.DarkBlue, Color.LightBlue);

        public MySplitter()
        {
            this.Paint += splitter1_Paint;
        }

        private void splitter1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(br, e.ClipRectangle);
        }
    }
}
