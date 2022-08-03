using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.DictionariesHanding
{
    public class ClassTypeOfSpecialization
    {
        private static List<ClassTypeOfSpecialization> listOfTypesOfSpecializations; //List of types of specializations
        public static List<ClassTypeOfSpecialization> ListOfTypesOfSpecializations { get => listOfTypesOfSpecializations; set => listOfTypesOfSpecializations = value; }
        private int typeOfSpecializationId;
        public int TypeOfSpecializationId { get => typeOfSpecializationId; set => typeOfSpecializationId = value; }
        private string typeOfSpecialization;
        public string TypeOfSpecialization { get => typeOfSpecialization; set => typeOfSpecialization = value; }
        private int specializationId;
        public int SpecializationId { get => specializationId; set => specializationId = value; }
        public override string ToString()
        {
            return TypeOfSpecialization;
        }
        public override bool Equals(object obj)
        {
            var item = obj as ClassDoctor;

            if (item == null)
            {
                return false;
            }

            return this.TypeOfSpecialization.Equals(item.TypeOfSpecialization.TypeOfSpecialization);
        }

        public override int GetHashCode()
        {
            return this.TypeOfSpecialization.GetHashCode();
        }
    }
}