using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.DictionariesHanding
{
    public class ClassProfession
    {
        private static List<ClassProfession> listOfProfession; //List of profession

        public static List<ClassProfession> ListOfProfession { get => listOfProfession; set => listOfProfession = value; }

        private int professionId;

        public int ProfessionId
        {
            get { return professionId; }
            set { professionId = value; }
        }
        private string profession;

        public string Profession
        {
            get { return  profession; }
            set {  profession = value; }
        }

        public override string ToString()
        {
            return Profession;
        }

    }
}
