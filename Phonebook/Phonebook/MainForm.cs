using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Phonebook
{
    public partial class MainForm : Form
    {
        Person[] start = new Person[3];
        public MainForm()
        {
            InitializeComponent();
            object[] a = new object[1] { "asd" };
            start[0] = new Person(
                "Smith", 
                "Brown",
                new List<object> { "+1152374585" },
                new List<object> { "2652 Virginia Park st., Detroit, MI, USA" },
                new List<object> { "Factory" },
                new List<object> { "Worker" });
            start[1] = new Person(
                "Carl",
                "Johnes",
                new List<object> { "+1864198461" },
                new List<object> { "1234 Grove st., Los Angels, CA, USA" },
                new List<object> { "Four Dragons Casino" },
                new List<object> { "Owner" });
            start[2] = new Person(
               "Amy",
               "Fowler",
               new List<object> { "+16489613478" },
               new List<object> { "1421 San Pasqual st., Pasadena, CA, USA" },
               new List<object> { "CalTech" },
               new List<object> { "Scientist" });
            foreach(Person pers in start)
            {
                listBox1.Items.Add(pers.firstName + ", " + pers.lastName);
            }
        }

        private void Save()
        {
            XmlDocument doc = new XmlDocument();
            XmlSerializer ser = new XmlSerializer(typeof(Person));
            TextWriter writer = new StreamWriter("Save.txt");
            ser.Serialize(writer, start);
            writer.Close();
        }


        Person curPerson;
        
        private void listBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedItem == null)
                return;
            string a = (((ListBox)sender).SelectedItem).ToString();
            string[] b = a.Split(',', ' ');
            if (comboBox1.Items.Count > 0)
                foreach (var cb in this.Controls)
                    if (cb.GetType() == typeof(ComboBox))
                        ((ComboBox)cb).Items.Clear();
            foreach (Person p in start)
            {
                if (p.firstName == b[0] && p.lastName == b[2])
                {
                    curPerson = p;
                    textBox1.Text = p.firstName;
                    textBox2.Text = p.lastName;

                    comboBox1.Items.AddRange(p.phoneNumber.ToArray());
                    comboBox1.Items.Add("Add...");
                    comboBox1.Text = (string)p.phoneNumber[0];

                    comboBox2.Items.AddRange(p.address.ToArray());
                    comboBox2.Items.Add("Add...");
                    comboBox2.Text = (string)p.address[0];

                    comboBox3.Items.AddRange(p.workPlace.ToArray());
                    comboBox3.Items.Add("Add...");
                    comboBox3.Text = (string)p.workPlace[0];

                    comboBox4.Items.AddRange(p.workPosition.ToArray());
                    comboBox4.Items.Add("Add...");
                    comboBox4.Text = (string)p.workPosition[0];
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == (comboBox1.Items.Count - 1))
            {
                curPerson.phoneNumber.Add(CreateNewElementForm(1));
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(curPerson.phoneNumber.ToArray());
                comboBox1.Items.Add("Add...");
                comboBox1.Text = (string)curPerson.phoneNumber[0];
                comboBox1.Refresh();
                return;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == (comboBox2.Items.Count - 1))
            {
                curPerson.address.Add(CreateNewElementForm(2));
                comboBox2.Items.Clear();
                comboBox2.Items.AddRange(curPerson.address.ToArray());
                comboBox2.Items.Add("Add...");
                comboBox2.Text = (string)curPerson.address[0];
                comboBox2.Refresh();
                return;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == (comboBox3.Items.Count - 1))
            {

                curPerson.workPlace.Add(CreateNewElementForm(3));
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(curPerson.workPlace.ToArray());
                comboBox3.Items.Add("Add...");
                comboBox3.Text = (string)curPerson.workPlace[0];
                comboBox3.Refresh();
                return;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex == (comboBox4.Items.Count - 1))
            {

                curPerson.workPosition.Add(CreateNewElementForm(4));
                comboBox4.Items.Clear();
                comboBox4.Items.AddRange(curPerson.workPosition.ToArray());
                comboBox4.Items.Add("Add...");
                comboBox4.Text = (string)curPerson.workPosition[0];
                comboBox4.Refresh();
                return;
            }
        }

        private object CreateNewElementForm(int num)
        {
            NewElementForm form = null;
            switch(num)
            {
                case 1:
                        form = new NewElementForm("Phone Number");
                        form.ShowDialog();
                        if (form.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                            return null;
                    break;
                case 2:
                        form = new NewElementForm("Address");
                        form.ShowDialog();
                        if (form.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                            return null;
                        break;
                case 3:
                        form = new NewElementForm("Place of Work");
                        form.ShowDialog();
                        if (form.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                            return null;
                        break;
                case 4:
                        form = new NewElementForm("Position at Work");
                        form.ShowDialog();
                        if (form.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                            return null;
                        break;
            }
            object res = null;
            switch (form.comboBox1.Text)
            {
                case "string": res = (string)form.ReturnNewElement(); break;
                case "int": res = Int32.Parse((string)form.ReturnNewElement()); break;
                case "float": res = float.Parse((string)form.ReturnNewElement()); break;
            }
            return res;
        }

        static void ProcessItems<T>(IList<T> coll)
        {
            // IsReadOnly returns True for the array and False for the List.
            System.Console.WriteLine
                ("IsReadOnly returns {0} for this collection.",
                coll.IsReadOnly);

            // The following statement causes a run-time exception for the 
            // array, but not for the List.
            //coll.RemoveAt(4);

            foreach (T item in coll)
            {
                System.Console.Write(item.ToString() + " ");
            }
            System.Console.WriteLine();
        }
    }
}
