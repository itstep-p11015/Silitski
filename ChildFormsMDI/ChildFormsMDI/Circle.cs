using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChildFormsMDI
{
    class Circle : Figure
    {
        public float x;
        public float y;
        public float r;
        public Circle(float x, float y, float r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
        }
        public override void Draw(Graphics drawer, Pen pen)
        {
            drawer.DrawEllipse(pen, x - r, y - r, r * 2, r * 2);
        }
    }
}
