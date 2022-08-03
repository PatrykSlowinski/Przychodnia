using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.DictionariesHanding
{
    public class ClassDegreeOfDoctor
    {
       

        private int degreeOfDoctorId;

        public int DegreeOfDoctorId
        {
            get { return degreeOfDoctorId; }
            set { degreeOfDoctorId = value; }
        }
        private string degree;

        public  string Degree
        {
            get { return degree; }
            set { degree = value; }
        }
        public override string ToString()
        {
            return Degree;
        }

    }
}
