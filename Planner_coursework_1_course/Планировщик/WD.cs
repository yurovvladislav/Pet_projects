using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Планировщик
{
    internal class WD
    {
        public int x;
        public int y;
        public int width;
        public int height;
        Color color;
        Graphics g;
        public WD(int x, int y, int w, int h, Color col)
        {
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;
            this.color = col;
        }
        public void draw(Graphics g)
        {
            SolidBrush b = new SolidBrush(color);
            g.FillRectangle(b, x, y, width, height);          
        }
        public bool Click(int x1, int y1)
        {
            return x1 >= x && y1 >= y && x1 <= x + width && y1 <= y + height;
        }

    }
}
