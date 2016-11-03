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
    public partial class NewContactForm : Form
    {
        public Person newPerson;
        public NewContactForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.Cancel)
                return;
            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            pictureBox1.Image.Tag = openFileDialog1.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newPerson = new Person(
                (pictureBox1.Image.Tag==null)?"No Image.png":(string)pictureBox1.Image.Tag,
                textBox1.Text,
                textBox2.Text,
                new List<object> { (textBox3.Text == "") ? "none" : textBox3.Text },
                new List<object> { (textBox4.Text == "") ? "none" : textBox4.Text },
                new List<object> { (textBox5.Text == "") ? "none" : textBox5.Text },
                new List<object> { (textBox6.Text == "") ? "none" : textBox6.Text });
            this.Dispose(false);
        }
    }
}
