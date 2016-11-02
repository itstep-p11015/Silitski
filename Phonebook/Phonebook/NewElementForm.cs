using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phonebook
{
    public partial class NewElementForm : Form
    {

        public NewElementForm(string str)
        {
            InitializeComponent();
            this.Text += str;
            label1.Text += str;
            button1.Text += str;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose(false);
        }

        public object ReturnNewElement()
        {
            return textBox1.Text;
        }
    }
}
