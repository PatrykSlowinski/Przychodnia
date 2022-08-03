using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Data.Common;


namespace Przychodnia.Class.Login
{
    public class ClassSQLConnection:ISqlLogowanie
    {
        private const string ConString = @"Data Source = 46.41.150.105; Initial Catalog = db_Clinic; Persist Security Info = True; User ID = sa; Password = bk&We43$yefw#%";

        //Check if login and password is correct if you want to login
        public bool CheckLoginDetails(string login, string password)
        {
            string querry = "SELECT [Login],[Password] FROM tbl_User WHERE Login=@Login AND Password=@Password";
            List<ClassSQLParameters> listOfParameters = new List<ClassSQLParameters>();
            ClassSQLParameters p1 = new ClassSQLParameters("@Login", login) ;
            ClassSQLParameters p2 = new ClassSQLParameters("@Password", password);
            listOfParameters.Add(p1);
            listOfParameters.Add(p2);
            List<int> retrievedColumns = new List<int>();
            retrievedColumns.Add(0);

            List<string> answer = Querry(querry, listOfParameters, retrievedColumns);
            if(answer.Count<1)
            {
                return false;
            }
            
            return true;
        }
        //Check if login and password is correct if you want to login
        public bool CheckLoginAndEmail(string login, string email)
        {
            bool find = false;
            string querry = "SELECT [Login],[Email]  FROM tbl_User WHERE login=@login AND Email=@Email ";
            List<ClassSQLParameters> listOfParameters = new List<ClassSQLParameters>();
            ClassSQLParameters p1 = new ClassSQLParameters("@Login", login);
            ClassSQLParameters p2 = new ClassSQLParameters("@Email", email);
            listOfParameters.Add(p1);
            listOfParameters.Add(p2);
            List<int> retrievedColumns = new List<int>();
            retrievedColumns.Add(0);
            List<string> answer = Querry(querry, listOfParameters, retrievedColumns);
            if (answer.Count >= 1)
            {
                find = true;
            }
            return find;
        }
        //Update new password to database
        public void ChangePassword(string login, string email, string newPassword)
        {
            //UPDATE [dbo].[pracownik] SET pr_st_id = 9, pr_szef_id = 9 WHERE pr_id BETWEEN 12 AND 14;
            string querry = "USE db_Clinic UPDATE tbl_User SET Password=@Password WHERE Login=@Login AND Email=@Email ";
            List<ClassSQLParameters> listOfParameters = new List<ClassSQLParameters>();
            ClassSQLParameters p1 = new ClassSQLParameters("@Login", login);
            ClassSQLParameters p2 = new ClassSQLParameters("@Email", email);
            ClassSQLParameters p3 = new ClassSQLParameters("@Password", newPassword);
            listOfParameters.Add(p1);
            listOfParameters.Add(p2);
            listOfParameters.Add(p3);
            List<int> retrievedColumns = new List<int>();
            Querry(querry, listOfParameters, retrievedColumns);
        }

        //Check if parameters equals with data from DB
        public static List<String> Querry(string querry, List<ClassSQLParameters> list, List<int> retrievedColumns)
        {
            List<string> readList = new List<string>(); 
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            foreach(ClassSQLParameters x in list)
            {
                sqlCommand.Parameters.AddWithValue(x.ParameterName,x.Parameter);
            }
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while (dr.Read())
            {
                foreach (int item in retrievedColumns)
                {
                    readList.Add(dr.GetString(item));
                }
            }
            sqlCon.Close();
            return readList;
        }

        public Class.DictionariesHanding.ClassPermission GetUserType(string login, string password)
        {
            string querry = "USE db_Clinic " +
            "SELECT tbl_User.Permission_id, Type_of_permission FROM tbl_User, tbl_Permission " +
            "WHERE tbl_Permission.Permission_id = tbl_User.Permission_id " +
            "AND login = @login AND Password = @Password ";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@login", login);
            sqlCommand.Parameters.AddWithValue("@password", password);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            Class.DictionariesHanding.ClassPermission permission = new Class.DictionariesHanding.ClassPermission();
            while (dr.Read())
            {
                permission.PermissionId = dr.GetInt32("Permission_id");
                permission.Permission = dr.GetString("Type_of_permission");
            }
            sqlCon.Close();
            return permission;
        }
    }
}
