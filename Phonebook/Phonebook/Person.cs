using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook
{
    [Serializable]
    public class Person
    {
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
            string firstName,
            string lastName,
            List<object> phoneNumber,
            List<object> address,
            List<object> workPlace,
            List<object> workPosition)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.workPlace = workPlace;
            this.workPosition = workPosition;
        }

    }
}
