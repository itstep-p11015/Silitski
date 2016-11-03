using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Phonebook
{
    [Serializable]
    public class Person
    {
        public Image img;
        public string firstName;
        public string lastName;
        public List<object> phoneNumber;
        public List<object> address;
        public List<object> workPlace;
        public List<object> workPosition;

        public Person()
        {

        }

        public Person(
            string imgPath,
            string firstName,
            string lastName,
            List<object> phoneNumber,
            List<object> address,
            List<object> workPlace,
            List<object> workPosition)
        {
            img = Image.FromFile(imgPath);
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.workPlace = workPlace;
            this.workPosition = workPosition;
        }

        public void InitFields(ComboBox cb, List<object> l)
        {
            if (cb.Items.Count > 0)
                cb.Items.Clear();
            cb.Items.AddRange(l.ToArray());
            cb.Items.Add("Add...");
            cb.Text = (string)l[0];
        }

        public void InitFields(TextBox tb, string s)
        {
            tb.Text = s;
        }

        public void InitFields(PictureBox pb, Image i)
        {
            pb.Image = i;
        }

        public void SavePerson(XmlDocument doc)
        {
            XmlElement p = doc.CreateElement("Person");
            p.Attributes.Append(doc.CreateAttribute("img"));

        }
    }
}
