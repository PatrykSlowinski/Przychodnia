using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.DictionariesHanding
{
    public class ClassEmployee:ClassUser
    {       

        private int employeeId;

        public int EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string surname;
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        private DateTime dateOFBirth;
        public DateTime DateOfBirth
        {
            get { return dateOFBirth; }
            set { dateOFBirth = value; }
        }

        private string personalIdentityNumber;

        public string PersonalIdentityNumber
        {
            get { return personalIdentityNumber; }
            set { personalIdentityNumber = value; }
        }
        public override string ToString()
        {
            return Name + " | " + Surname;
        }

    }
}
