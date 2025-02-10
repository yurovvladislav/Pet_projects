using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Планировщик
{
    public abstract class Furniture
    {
        public int x;
        public int y;
        public int w;
        public int h;
        public PictureBox pb;
        public Furniture(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            //pb = new PictureBox();
            //pb.Width = w;
            //pb.Height = h;
            //pb.Location = new Point(x, y);
            //pb.Image = Image.FromFile("1.jpg");
            //pb.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
