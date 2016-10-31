using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ChildFormsMDI
{
    public partial class ChildForm : Form
    {
        List<Circle> circles = new List<Circle>();
        List<Figure> figures = new List<Figure>();
        bool clicked = false;
        int delta;
        double alpha = 0;
        Color penColor = Color.Red;
        int penWidth = 5;
        Pen graphPen;
        public ChildForm(Form1 parentForm)
        {
            InitializeComponent();
            this.MdiParent = parentForm;
            graphPen = new Pen(penColor, penWidth);
            circles.Add(new Circle(300, 300, 20));
            circles.Add(new Circle(200, 300, 35));
            circles.Add(new Circle(300, 200, 50));
            circles.Add(new Circle(400, 100, 80));
            circles.Add(new Circle(50, 50, 10));

            //figures.Add(new Circle(300, 300, 20));
            figures.Add(new Circle(200, 300, 35));
            figures.Add(new Circle(300, 200, 50));
            //figures.Add(new Circle(400, 100, 80));
            figures.Add(new Circle(50, 50, 10));

            figures.Add(new Rectangle(400, 300, 120, 80));
            figures.Add(new Line(0, 0, 684, 500));
            figures.Clear();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // взяли рисовалку
            Graphics drawer = e.Graphics;

            //// создали точку
            //PointF pt1D = new PointF();
            //pt1D.X = 0;
            //pt1D.Y = 10;

            //// ещё точку
            //PointF pt2D = new PointF();
            //pt2D.X = 10;
            //pt2D.Y = 10;

            //float clockX = 200f, clockY = 100f, clockSizeX = 150, clockSizeY = 50;

            //// нарисовали линию между ними
            //drawer.DrawLine(graphPen, pt1D, pt2D);

            //// ещё одну линию
            //drawer.DrawLine(graphPen, clockX, clockY,
            //    (float)(clockX + clockSizeX * Math.Cos(alpha)),
            //    (float)(clockY + clockSizeY * Math.Sin(alpha)));

            //// нарисовали эллипс (окружность в данном случае)
            //drawer.DrawEllipse(graphPen, clockX - clockSizeX, clockY - clockSizeY, clockSizeX * 2, clockSizeY * 2);

            // рисуем кружочки из массива
            foreach (Figure fig in figures)
            {
                fig.Draw(drawer, graphPen);
            }
            //foreach (var cir in circles)
            //{
            //    drawer.DrawEllipse(graphPen, cir.x - cir.r, cir.y - cir.r, cir.r * 2, cir.r * 2);
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            alpha += 0.003141596;
            Refresh();
        }

        static float Hypot(float x, float y)
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            delta = e.Delta;
            this.Text = ("Graphic " + String.Format("({0}, {1})", e.X, e.Y));
            try
            {
                foreach (Circle circle in figures)
                {
                    figures[0].GetType();
                    if (Hypot(e.X - circle.x, e.Y - circle.y) < circle.r)
                    {
                        clicked = (clicked) ? false : true;
                        numericUpDown1.Tag = circle;
                        numericUpDown1.Value = (decimal)circle.r;
                        this.Refresh();
                        return;
                    }
                }
            }
            catch (Exception)
            {
                return;
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value != 0 && numericUpDown1.Tag != null)
                ((Circle)numericUpDown1.Tag).r = (float)numericUpDown1.Value;
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            if (numericUpDown1.Value != 0 && numericUpDown1.Tag != null)
            {
                numericUpDown1.Value = 0;
                numericUpDown1.Tag = null;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (numericUpDown1.Tag != null && clicked)
            {
                ((Circle)numericUpDown1.Tag).x = e.X;
                ((Circle)numericUpDown1.Tag).y = e.Y;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = saveFileDialog1.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.Cancel)
                return;
            XmlDocument doc = new XmlDocument();
            XmlElement svg = doc.CreateElement("svg");
            XmlNode body = doc.CreateElement("body");
            XmlNode html = doc.CreateElement("html");
            svg.SetAttribute("width", pictureBox1.Size.Width.ToString());
            svg.SetAttribute("height", pictureBox1.Size.Height.ToString());
            foreach (Figure fig in figures)
            {
                XmlElement figure;
                string type = fig.GetType().ToString();
                switch (type)
                {
                    case "ChildFormsMDI.Circle":
                        Circle circ = (Circle)fig;
                        figure = doc.CreateElement("circle");
                        figure.SetAttribute("cx", circ.x.ToString());
                        figure.SetAttribute("cy", circ.y.ToString());
                        figure.SetAttribute("r", circ.r.ToString());
                        figure.SetAttribute("stroke", penColor.Name);
                        figure.SetAttribute("stroke-width", penWidth.ToString());
                        figure.SetAttribute("fill", "white");
                        svg.AppendChild(figure);
                        break;
                    case "ChildFormsMDI.Rectangle":
                        Rectangle rec = (Rectangle)fig;
                        figure = doc.CreateElement("rect");
                        figure.SetAttribute("x", rec.x.ToString());
                        figure.SetAttribute("y", rec.y.ToString());
                        figure.SetAttribute("width", rec.width.ToString());
                        figure.SetAttribute("height", rec.height.ToString());
                        figure.SetAttribute("style", ("fill:white;stroke:" + penColor.Name + ";stroke-width:" + penWidth.ToString()));
                        svg.AppendChild(figure);
                        break;
                    case "ChildFormsMDI.Line":
                        Line line = (Line)fig;
                        figure = doc.CreateElement("line");
                        figure.SetAttribute("x1", line.x1.ToString());
                        figure.SetAttribute("y1", line.y1.ToString());
                        figure.SetAttribute("x2", line.x2.ToString());
                        figure.SetAttribute("y2", line.y2.ToString());
                        figure.SetAttribute("style", ("stroke:" + penColor.Name + ";stroke-width:" + penWidth.ToString()));
                        svg.AppendChild(figure);
                        break;
                }

            }
            body.AppendChild(svg);
            html.AppendChild(body);
            doc.AppendChild(html);
            doc.Save(saveFileDialog1.FileName);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.Cancel)
                return;
            XmlDocument doc = new XmlDocument();
            try { doc.Load(openFileDialog1.FileName); }
            catch (Exception)
            {
                MessageBox.Show("File cannot be open!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            XmlNode root = doc.SelectSingleNode("/html/body/svg");
            figures.Clear();
            foreach (XmlNode child in root)
            {
                string type = child.Name;
                switch (type)
                {
                    case "circle":
                        {
                            float x = 0, y = 0, r = 0;
                            foreach (XmlAttribute a in child.Attributes)
                            {
                                switch (a.Name)
                                {
                                    case "cx": x = float.Parse(a.Value); break;
                                    case "cy": y = float.Parse(a.Value); break;
                                    case "r": r = float.Parse(a.Value); break;
                                    case "stroke": penColor = Color.FromName(a.Value); break;
                                    case "stroke-width": penWidth = Int32.Parse(a.Value); break;
                                }
                            }
                            figures.Add(new Circle(x, y, r));
                        }
                        break;
                    case "rect":
                        {
                            float x = 0, y = 0, width = 0, height = 0;
                            foreach (XmlAttribute a in child.Attributes)
                            {
                                switch (a.Name)
                                {
                                    case "x": x = float.Parse(a.Value); break;
                                    case "y": y = float.Parse(a.Value); break;
                                    case "width": width = float.Parse(a.Value); break;
                                    case "height": height = float.Parse(a.Value); break;
                                    case "style":
                                        string[] attr = a.Value.Split(';', ':');
                                        for (int i = 0; i < attr.Length; i++)
                                        {
                                            if (attr[i] == "stroke")
                                                penColor = Color.FromName(attr[i + 1]);
                                            if (attr[i] == "stroke-width")
                                                penWidth = Int32.Parse(attr[i + 1]);
                                        }
                                        break;
                                }
                            }
                            figures.Add(new Rectangle(x, y, width, height));
                        }
                        break;
                    case "line":
                        {
                            float x1 = 0, y1 = 0, x2 = 0, y2 = 0;
                            foreach (XmlAttribute a in child.Attributes)
                            {
                                switch (a.Name)
                                {
                                    case "x1": x1 = float.Parse(a.Value); break;
                                    case "y1": y1 = float.Parse(a.Value); break;
                                    case "x2": x2 = float.Parse(a.Value); break;
                                    case "y2": y2 = float.Parse(a.Value); break;
                                    case "style":
                                        string[] attr = a.Value.Split(';', ':');
                                        for (int i = 0; i < attr.Length; i++)
                                        {
                                            if (attr[i] == "stroke")
                                                penColor = Color.FromName(attr[i + 1]);
                                            if (attr[i] == "stroke-width")
                                                penWidth = Int32.Parse(attr[i + 1]);
                                        }
                                        break;
                                }
                            }
                            figures.Add(new Line(x1, y1, x2, y2));
                        }
                        break;
                }

            }
        }
    }
}
