using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChildFormsMDI
{
    class Rectangle : Figure
    {
        public float x;
        public float y;
        public float width;
        public float height;
        public Rectangle(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public override void Draw(Graphics drawer, Pen pen)
        {
            drawer.DrawRectangle(pen, x, y, width, height);
        }
    }
}
