using System;
using System.Collections.Generic;
using System.Text;
using Przychodnia.Class.Calendar;

namespace Przychodnia.Class.DictionariesHanding
{
    public class ClassDoctor : ClassEmployee
    {
        private static List<ClassDoctor> listOfDoctors; //List of doctors

        public static List<ClassDoctor> ListOfDoctors { get => listOfDoctors; set => listOfDoctors = value; }
        private int doctor_id;

        private Class.Calendar.ClassStatus status;

        public Class.Calendar.ClassStatus Status
        {
            get { return status; }
            set { status = value; }
        }


        public bool Active { get => active; set => active = value; }


        private bool active;
        public int Doctor_id
        {
            get { return doctor_id; }
            set { doctor_id = value; }
        }
        private int officeNumber;
        public int OfficeNumber
        {
            get { return officeNumber; }
            set { officeNumber = value; }
        }

        private ClassDegreeOfDoctor degree;

        public ClassDegreeOfDoctor Degree
        {
            get => degree;
            set 
                {
                    if (value == null) throw new Exception("Select degree");
                    degree = value;
                }
        }


        private ClassSpecialization specialization;

        public ClassSpecialization Specialization
        {
            get => specialization;
            set => specialization = value;
        }

        private ClassTypeOfSpecialization typeOfSpecialization;
        public ClassTypeOfSpecialization TypeOfSpecialization
        {
            get => typeOfSpecialization;
            set => typeOfSpecialization = value;
        }
        public ClassEmployee Employee { get => employee; set => employee = value; }

        private ClassEmployee employee;
    }
}
