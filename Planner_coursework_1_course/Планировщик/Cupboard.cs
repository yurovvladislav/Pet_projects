using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Планировщик
{
    internal class Cupboard : Furniture
    {
        public Cupboard(int x, int y, int w, int h) : base(x, y, w, h)
        {
            pb = new PictureBox();
            pb.Width = w;
            pb.Height = h;
            pb.Location = new Point(x, y);
            pb.Image = Image.FromFile("6.jpg");
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
