using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.Login
{
    public class ClassSQLParameters
    {
        
        private string parameterName;
        private string parameter;
        //Parameters that are used in query in SQL
        public ClassSQLParameters(string p1, string p2)
        {
            #region Example
            //querry = "SELECT ... FROM ... WHERE login=@login"
            //ParameterName = "login"
            //Parameter = string text
            #endregion
            ParameterName = p1;
            Parameter = p2;
        }

        public string ParameterName { get => parameterName; set => parameterName = value; }
        public string Parameter { get => parameter; set => parameter = value; }
    }
}
