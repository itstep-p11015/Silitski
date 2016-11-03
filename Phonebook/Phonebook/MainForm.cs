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
        List<Person> persons = new List<Person>();
        public MainForm()
        {
            InitializeComponent();
            persons.Add(new Person(
                "Adam.jpg",
                "Adam",
                "Smith",
                new List<object> { "+1152374585" },
                new List<object> { "2652 Virginia Park st., Detroit, MI, USA" },
                new List<object> { "Factory" },
                new List<object> { "Worker" }));
            persons.Add(new Person(
                "Carl.jpg",
                "Carl",
                "Johnson",
                new List<object> { "+1864198461" },
                new List<object> { "1234 Grove st., Los Angels, CA, USA" },
                new List<object> { "Four Dragons Casino" },
                new List<object> { "Owner" }));
            persons.Add(new Person(
                "Amy.jpg",
               "Amy",
               "Fowler",
               new List<object> { "+16489613478" },
               new List<object> { "1421 San Pasqual st., Pasadena, CA, USA" },
               new List<object> { "CalTech" },
               new List<object> { "Scientist" }));
            foreach(Person pers in persons)
            {
                listBox1.Items.Add(pers.firstName + ", " + pers.lastName);
            }
            listBox1.Items.Add("Add New Contact...");
        }

        private void Save()
        {
            XmlDocument doc = new XmlDocument();
            XmlSerializer ser = new XmlSerializer(typeof(Person));
            TextWriter writer = new StreamWriter("Save.txt");
            ser.Serialize(writer, persons);
            writer.Close();
        }

        Person curPerson;
        
        private void listBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedItem == null)
                return;
            string a = (((ListBox)sender).SelectedItem).ToString();
            if (a == "Add New Contact...")
                AddNewContact();
            string[] b = a.Split(',', ' ');
            foreach (Person p in persons)
            {
                if (p.firstName == b[0] && p.lastName == b[2])
                {
                    curPerson = p;

                    p.InitFields(pictureBox1, p.img);

                    p.InitFields(textBox1, p.firstName);

                    p.InitFields(textBox2, p.lastName);

                    p.InitFields(comboBox1, p.phoneNumber);

                    p.InitFields(comboBox2, p.address);

                    p.InitFields(comboBox3, p.workPlace);

                    p.InitFields(comboBox4, p.workPosition);
                }
            }
        }

        private void AddNewContact()
        {
            NewContactForm frm = new NewContactForm();
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            persons.Add(frm.newPerson);
            listBox1.Items.Clear();
            foreach (Person pers in persons)
            {
                listBox1.Items.Add(pers.firstName + ", " + pers.lastName);
            }
            listBox1.Items.Add("Add New Contact...");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == (comboBox1.Items.Count - 1))
            {
                object temp = CreateNewElementForm(1);
                if (temp == null)
                    return;
                curPerson.phoneNumber.Add(temp);
                curPerson.InitFields(comboBox1, curPerson.phoneNumber);
                comboBox1.Refresh();
                return;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == (comboBox2.Items.Count - 1))
            {
                object temp = CreateNewElementForm(2);
                if (temp == null)
                    return;
                curPerson.address.Add(temp);
                curPerson.InitFields(comboBox2, curPerson.address);
                comboBox2.Refresh();
                return;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == (comboBox3.Items.Count - 1))
            {
                object temp = CreateNewElementForm(3);
                if (temp == null)
                    return;
                curPerson.workPlace.Add(temp);
                curPerson.InitFields(comboBox3, curPerson.workPlace);
                comboBox3.Refresh();
                return;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex == (comboBox4.Items.Count - 1))
            {
                object temp = CreateNewElementForm(4);
                if (temp == null)
                    return;
                curPerson.workPosition.Add(temp);
                curPerson.InitFields(comboBox4, curPerson.workPosition);
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
            return form.ReturnNewElement();
        }
    }
}
