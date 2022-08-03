using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Windows.Controls;
namespace Przychodnia.Class.Calendar
{
    class ClassQuerry
    {
        #region Config
        private const string ConString = @"Data Source = 46.41.150.105; Initial Catalog = db_Clinic; Persist Security Info = True; User ID = sa; Password = bk&We43$yefw#%";
        #endregion
        private SqlConnection sqlCon = new SqlConnection(ConString);
        
        public  SqlDataReader ExecuteQuerry(string querry)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            return dr;
        }
        public void CloseConnection()
        {
            sqlCon.Close();
        }
        
    }
}
