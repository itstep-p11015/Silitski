using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChildFormsMDI
{
    public partial class Form1 : Form
    {
        static int number = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm child = new ChildForm(this);
            child.Text=("Untitled "+(number++).ToString());
            child.Show();
            ToolStripMenuItem cMI = new ToolStripMenuItem(child.Text);
            cMI.Tag = child;
            cMI.DropDownItems.Add(new ToolStripMenuItem(child.menuStrip1.Name));
            windowToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(child.Text));
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void tilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string formNames = "";
            foreach (ChildForm f in this.MdiChildren)
            {
                formNames += f.Text + "\n";
            }
            MessageBox.Show(formNames);
        }

    }
}
