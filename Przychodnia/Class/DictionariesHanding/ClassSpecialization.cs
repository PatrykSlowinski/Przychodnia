using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.DictionariesHanding
{
    public class ClassSpecialization
    {
        private static List<ClassSpecialization> listOfSpecializations; //List of specializations

        public static List<ClassSpecialization> ListOfSpecializations { get => listOfSpecializations; set => listOfSpecializations = value; }

        private int specializationId;
        public int SpecializationId { get => specializationId; set => specializationId = value; }
        
        private string specialization; 
        public string Specialization { get => specialization; set => specialization = value; }

        public override string ToString()
        {
            return Specialization;
        }
    }
}
