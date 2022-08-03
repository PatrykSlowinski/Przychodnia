using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.DictionariesHanding
{
    public class ClassPatient
    {       

        private int patientId;

        public int PatientId
        {
            get { return patientId; }
            set { patientId = value; }
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
        
        private string city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }
        private string street;

        public string Street
        {
            get { return street; }
            set { street = value; }
        }
        private string street_number;

        public string Street_number
        {
            get { return street_number; }
            set { street_number = value; }
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
        private bool isActiv;

        public bool IsActiv
        {
            get { return isActiv; }
            set { isActiv = value; }
        }
    }
}
