using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Windows.Controls;
using System.Windows;
using Przychodnia.Class.DictionariesHanding;
using Przychodnia.Class.Calendar;
namespace Przychodnia.Class.DictionariesHanding
{
    public class ClassSQLConnections
    {
        private const string ConString = @"Data Source = 46.41.150.105; Initial Catalog = db_Clinic; Persist Security Info = True; User ID = sa; Password = bk&We43$yefw#%";

        //A method that takes a list of doctors from the database and returns it
        public static List<ClassOffice> NotSpecifiedOffice()
        {
            string querry = "" +
            "select office.Office_id, office.Office_number " +
            "from tbl_Office as office " +
            "FULL OUTER JOIN tbl_Doctor as doc " +
            "ON office.Office_id = doc.Office_id " +
            "where office.Office_id is null " +
            "or doc.Office_id is null";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<ClassOffice> offlist = new List<ClassOffice>();
            while (dr.Read())
            {
                ClassOffice off = new ClassOffice();
                off.OfficeId = dr.GetInt32("Office_id");
                off.OfficeNumber = dr.GetInt16("Office_number");
                offlist.Add(off);
            }
            sqlCon.Close();
            return offlist;
        }
        public static List<ClassDoctor> DoctorList()
        {
            string querry = "" +
            "USE[db_Clinic] SELECT[Doctor_id],[Name],[Surname],[Phone_number],[Active],[Degree],[Type_of_specialization],[Office_id],[Specialization], " +
            "[tbl_Degree_of_doctor].[Degree_of_doctor_id],[tbl_Type_of_specialization].[Type_of_specialization_id],[tbl_Specialization].[Specialization_id] " +
            "FROM[dbo].[tbl_Doctor], [dbo].[tbl_Degree_of_doctor], [dbo].[tbl_Specialization],[dbo].[tbl_Type_of_specialization], [dbo].[tbl_Employee] " +
            "WHERE[tbl_Doctor].[Degree_of_doctor_id] =[tbl_Degree_of_doctor].[Degree_of_doctor_id] " +
            "AND[tbl_Doctor].[Type_of_specialization_id] =[tbl_Type_of_specialization].[Type_of_specialization_id] " +
            "AND[tbl_Type_of_specialization].[Specialization_id] =[tbl_Specialization].[Specialization_id] " +
            "AND[tbl_Doctor].[Employee_id] =[tbl_Employee].[Employee_id] ";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<ClassDoctor> dctlist = new List<ClassDoctor>();
            while (dr.Read())
            {
                ClassDoctor dct = new ClassDoctor();
                dct.Doctor_id = dr.GetInt32("Doctor_id");
                dct.Name = dr.GetString("Name");
                dct.Surname = dr.GetString("Surname");
                dct.PhoneNumber = dr.GetString("Phone_number");
                dct.OfficeNumber = dr.GetInt32("Office_id");
                if (dr.GetString("Active").Equals("Yes"))
                {
                    dct.Active = true;
                }
                else
                {
                    dct.Active = false;
                }
                ClassDegreeOfDoctor degree = new ClassDegreeOfDoctor();
                degree.DegreeOfDoctorId = dr.GetInt32("Degree_of_doctor_id");
                degree.Degree = dr.GetString("Degree");
                dct.Degree = degree;
                ClassTypeOfSpecialization typeOfSpecialization = new ClassTypeOfSpecialization();
                typeOfSpecialization.TypeOfSpecializationId = dr.GetInt32("Type_of_specialization_id");
                typeOfSpecialization.TypeOfSpecialization = dr.GetString("Type_of_specialization");
                dct.TypeOfSpecialization = typeOfSpecialization;
                ClassSpecialization specialization = new ClassSpecialization();
                specialization.SpecializationId = dr.GetInt32("Specialization_id");
                specialization.Specialization = dr.GetString("Specialization");
                dct.Specialization = specialization;

                dctlist.Add(dct);
            }
            sqlCon.Close();
            return dctlist;
        }

        //A method that gets office staff from the database and returns list of office staff
        public static List<ClassEmployee> EmployeeList()
        {
            string querry = "SELECT [Employee_id],[Name],[Surname],[Phone_number],[Date_of_birth],[Personal_identity_number],[User_id] FROM [db_Clinic].[dbo].[tbl_Employee]";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<ClassEmployee> employeeList = new List<ClassEmployee>();
            while (dr.Read())
            {
                ClassEmployee employee = new ClassEmployee();
                employee.EmployeeId = dr.GetInt32("Employee_id");
                employee.Name = dr.GetString("Name");
                employee.Surname = dr.GetString("Surname");
                employee.PhoneNumber = dr.GetString("Phone_number");
                employee.DateOfBirth = dr.GetDateTime("Date_of_birth");
                employee.PersonalIdentityNumber = dr.GetString("Personal_identity_number");
                employee.User_id = dr.GetInt32("User_id");
                employeeList.Add(employee);
            }
            sqlCon.Close();
            return employeeList;
        }

       
        //Method that updates office staff in database table when you edit office staff in program
        public static void UpdateEmployee(ClassEmployee employee)
        {

            string querry = "USE [db_Clinic] UPDATE tbl_employee SET[Name] = @Name,[Surname]= @Surname,[Phone_number]= @Phone_number, Date_of_birth = @Date_of_birth WHERE Employee_id = @Employee_id";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@Employee_id", employee.EmployeeId);
            sqlCommand.Parameters.AddWithValue("@Name", employee.Name);
            sqlCommand.Parameters.AddWithValue("@Surname", employee.Surname);
            sqlCommand.Parameters.AddWithValue("@Phone_number", employee.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@Date_of_birth", employee.DateOfBirth);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }

        //Method that adds office staff to database table when you add office staff member in program
        public static void AddNewEmployee(ClassEmployee employee)
        {
            //Add office staff
            string querryp1 = "USE[db_Clinic] INSERT INTO [dbo].tbl_Employee ([Name],[Surname],[Phone_number],[Date_of_birth],[Personal_identity_number],[User_id]) ";
            string querryp2 = "VALUES(@Name,@Surname,@Phone_number,@Date_of_birth,@Personal_identity_number,@User_id)";
            string querry = querryp1 + querryp2;
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@Name", employee.Name);
            sqlCommand.Parameters.AddWithValue("@Surname", employee.Surname);
            sqlCommand.Parameters.AddWithValue("@Phone_number", employee.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@Date_of_birth", employee.DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@Personal_identity_number", employee.PersonalIdentityNumber);
            sqlCommand.Parameters.AddWithValue("@User_id", employee.User_id);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }

        //Method that gets and returns type of permission from logged user (from database)
        public static ClassPermission GetUserType(string login, string password)
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
            ClassPermission permission = new ClassPermission();
            while (dr.Read())
            {
                permission.PermissionId = dr.GetInt32("Permission_id");
                permission.Permission = dr.GetString("Type_of_permission");
            }
            sqlCon.Close();
            return permission;
        }

        //Method that deletes office staff from data base when you remove office staff in program
        public static void DeleteEmployee(int id)
        {
            string querry = "USE[db_Clinic] DELETE FROM [dbo].tbl_Employee WHERE [Employee_id] = @id";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }

        //Method that gets degrees of doctors from database and returns degrees of doctors in list
        public static List<ClassDegreeOfDoctor> DegreeOfDoctorList()
        {
            string querry = "USE[db_Clinic] SELECT[Degree_of_doctor_id],[Degree] From[dbo].[tbl_Degree_of_doctor]";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<ClassDegreeOfDoctor> DegreeList = new List<ClassDegreeOfDoctor>();
            while (dr.Read())
            {
                ClassDegreeOfDoctor degree = new ClassDegreeOfDoctor();
                degree.DegreeOfDoctorId = dr.GetInt32("Degree_of_doctor_id");
                degree.Degree = dr.GetString("Degree");
                DegreeList.Add(degree);
            }
            sqlCon.Close();
            return DegreeList;
        }
        //Method that updates doctor in database when you edit doctor in program
        public static void UpdateDoctor(ClassDoctor doctor)
        {
            string querry = "UPDATE [db_Clinic].[dbo].[tbl_Doctor] SET "+
            "[Active] = @active,[Type_of_specialization_id] = @TypeOfSpecializationId, " +
            "[Degree_of_doctor_id] = @degreeOfDoctorId" +
            " Where [Doctor_id] = @Doctor_id ";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand cmd = new SqlCommand(querry, sqlCon);
            if (doctor.Active == true)
            {
                cmd.Parameters.AddWithValue("@active", "Yes");
            }
            else
            {
                cmd.Parameters.AddWithValue("@active", "No");
            }
            cmd.Parameters.AddWithValue("@Doctor_id", doctor.Doctor_id);
            cmd.Parameters.AddWithValue("@degreeOfDoctorId", doctor.Degree.DegreeOfDoctorId);
            cmd.Parameters.AddWithValue("@TypeOfSpecializationId", doctor.TypeOfSpecialization.TypeOfSpecializationId);

            SqlDataReader dr = cmd.ExecuteReader();
            sqlCon.Close();
        }
        //Method that adds doctor to database when you add doctor in program
        public static void AddDoctor(ClassDoctor doctor)
        {
            string querry = "SET IDENTITY_INSERT [db_Clinic].[dbo].[tbl_Doctor]  ON " +
                "INSERT INTO [db_Clinic].[dbo].[tbl_Doctor] (Doctor_id,Active,Type_of_specialization_id,Office_id,Degree_of_doctor_id,Employee_id) " +
            "VALUES ((SELECT TOP 1 Doctor_id FROM tbl_Doctor ORDER BY Doctor_id DESC)+1,@active,@typeOfSpecializationId,(SELECT Office_id FROM tbl_Office WHERE Office_number=@officeId),@degreeOfDoctorId, @employee_id)" +
            "SET IDENTITY_INSERT [db_Clinic].[dbo].[tbl_Doctor]  OFF ";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand cmd = new SqlCommand(querry, sqlCon);
            if (doctor.Active == true)
            {
                cmd.Parameters.AddWithValue("@active", "Yes");
            }
            else
            {
                cmd.Parameters.AddWithValue("@active", "No");
            }
            cmd.Parameters.AddWithValue("@typeOfSpecializationId", doctor.TypeOfSpecialization.TypeOfSpecializationId);
            cmd.Parameters.AddWithValue("@officeId", doctor.OfficeNumber);
            cmd.Parameters.AddWithValue("@degreeOfDoctorId", doctor.Degree.DegreeOfDoctorId);
            cmd.Parameters.AddWithValue("@employee_id", doctor.EmployeeId);
            cmd.ExecuteNonQuery();
            sqlCon.Close();
        }
        //Method that gets types of specializations from database and returns list of types of specializations
        public static List<ClassTypeOfSpecialization> TypeOfSpecializationList()
        {

            string querryp1 = "USE db_Clinic SELECT Type_of_specialization_id, Type_of_specialization, Specialization_id ";
            string querryp2 = "FROM tbl_Type_of_specialization";



            string querry = querryp1 + querryp2;
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<ClassTypeOfSpecialization> TypeOfSpecializationList = new List<ClassTypeOfSpecialization>();
            while (dr.Read())
            {
                ClassTypeOfSpecialization TypeOfSpecialization = new ClassTypeOfSpecialization();
                TypeOfSpecialization.TypeOfSpecializationId = dr.GetInt32("Type_of_specialization_id");
                TypeOfSpecialization.TypeOfSpecialization = dr.GetString("Type_of_specialization");
                TypeOfSpecialization.SpecializationId = dr.GetInt32("Specialization_id");
                TypeOfSpecializationList.Add(TypeOfSpecialization);
            }
            sqlCon.Close();
            return TypeOfSpecializationList;


        }

        //Method that gets specializations from database and returns list of specializations
        public static List<ClassSpecialization> SpecializationList()
        {
            string querryp1 = "USE db_Clinic SELECT [Specialization_id],[Specialization] ";
            string querryp2 = "FROM [tbl_Specialization]";
            string querry = querryp1 + querryp2;
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<ClassSpecialization> SpecializationList = new List<ClassSpecialization>();
            while (dr.Read())
            {
                ClassSpecialization specialization = new ClassSpecialization();
                specialization.SpecializationId = dr.GetInt32("Specialization_id");
                specialization.Specialization = dr.GetString("Specialization");
                SpecializationList.Add(specialization);
            }
            sqlCon.Close();
            return SpecializationList;
        }

        //Method that gets Id of user which is not assigned to any employee or administrator and returns list of indexes
        public static List<int> NotSpecifiedEmployeeIndex()
        {
            List<int> indexList = new List<int>();
            string querry = "SELECT [tbl_Employee].Employee_id FROM [dbo].[tbl_Doctor] "+
                "RIGHT JOIN[dbo].tbl_Employee "+
               "ON[tbl_Doctor].Employee_id =[tbl_Employee].Employee_id "+
                "WHERE[tbl_Doctor].Employee_id IS NULL "+
                "INTERSECT "+
                "select[tbl_Employee].Employee_id "+
                "from[tbl_User], tbl_Employee "+
                "where[tbl_User].Permission_id = 3 and[tbl_Employee].User_id = tbl_User.User_id";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while (dr.Read())
            {
                indexList.Add(dr.GetInt32("Employee_id"));
            }
            sqlCon.Close();
            return indexList;
        }

        //Uses Method NotSpecifiedUserIndex and additionally gets logins, emails, and permission id of that users and returns everything in list
        public static List<ClassEmployee> NotSpecifiedEmployee()
        {
            List<int> indexList = NotSpecifiedEmployeeIndex();
            List<ClassEmployee> employeeList = new List<ClassEmployee>();
            if (indexList.Count == 0)
            {
                return employeeList;
            }
            string querry = "USE [db_Clinic] SELECT [Employee_id],[Name],[Surname],[Phone_number],[Date_of_birth],[Personal_identity_number],tbl_Employee.[User_id],tbl_User.Permission_id,tbl_Permission.Type_of_permission FROM [dbo].[tbl_Employee],tbl_User, dbo.tbl_Permission where dbo.tbl_Permission.Permission_id=tbl_User.Permission_id and tbl_Employee.User_id=tbl_User.User_id and [tbl_Employee].Employee_id in(";
            string lastItem = "";
            foreach (int item in indexList)
            {
                querry = querry + item.ToString() + ",";
                lastItem = item.ToString();
            }
            querry = querry + lastItem + ")";

            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while (dr.Read())
            {
                ClassEmployee employee = new ClassEmployee();
                employee.EmployeeId = dr.GetInt32("Employee_id");
                employee.Name = dr.GetString("Name");
                employee.Surname = dr.GetString("Surname");
                employee.PhoneNumber = dr.GetString("Phone_number");
                employee.DateOfBirth = dr.GetDateTime("Date_of_birth");
                employee.PersonalIdentityNumber = dr.GetString("Personal_identity_number");
                employee.User_id = dr.GetInt32("User_id");
                ClassPermission permission= new ClassPermission();
                permission.PermissionId = dr.GetInt32("Permission_id");
                permission.Permission = dr.GetString("Type_of_permission");
                employee.Permission = permission;
                employeeList.Add(employee);
            }
            sqlCon.Close();
            return employeeList;
        }
        public static List<int> NotSpecifiedUserIndex()
        {
            List<int> indexList = new List<int>();
            string querry = "SELECT tbl_User.User_id FROM tbl_User LEFT JOIN tbl_Employee ON tbl_Employee.User_id = tbl_User.User_id WHERE Employee_id IS NULL";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while (dr.Read())
            {
                indexList.Add(dr.GetInt32("User_id"));
            }
            sqlCon.Close();
            return indexList;
        }

        //Uses Method NotSpecifiedUserIndex and additionally gets logins, emails, and permission id of that users and returns everything in list
        public static List<ClassUser> NotSpecifiedUsers()
        {
            List<int> indexList = NotSpecifiedUserIndex();
            List<ClassUser> userList = new List<ClassUser>();
            if (indexList.Count == 0)
            {
                return userList;
            }
            string querry = "SELECT[User_id],[Login],[Password],[Email],[tbl_User].Permission_id,tbl_Permission.Type_of_permission FROM [db_Clinic].[dbo].[tbl_User], [db_Clinic].[dbo].tbl_Permission WHERE tbl_Permission.Permission_id = tbl_User.Permission_id and [User_id] IN(";
            string lastItem = "";
            foreach (int item in indexList)
            {
                querry = querry + item.ToString() + ",";
                lastItem = item.ToString();
            }
            querry = querry + lastItem + ")";

            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            while (dr.Read())
            {
                ClassUser user = new ClassUser();
                user.Login = dr.GetString("Login");
                user.Email = dr.GetString("Email");
                user.User_id = dr.GetInt32("User_id");
                ClassPermission permission = new ClassPermission();
                permission.Permission = dr.GetString("Type_of_permission");
                permission.PermissionId = dr.GetInt32("Permission_id");
                user.Permission = permission;
                userList.Add(user);
            }
            sqlCon.Close();
            return userList;
        }
        //Method that gets offices from database and returns list of offices
        public static List<ClassOffice> OfficeList()
        {
            string querry = "SELECT [Office_id],[Office_number],[Description] FROM [db_Clinic].[dbo].[tbl_Office]";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<ClassOffice> officeList = new List<ClassOffice>();
            while (dr.Read())
            {
                ClassOffice office = new ClassOffice();
                office.OfficeId = dr.GetInt32("Office_id");
                office.OfficeNumber = dr.GetInt16("Office_number");
                office.OfficeDescription = dr.GetString("Description");
                officeList.Add(office);
            }
            sqlCon.Close();
            return officeList;
        }
        public static List<ClassUser> UserList()
        {
            string querry = "SELECT[User_id],[Login],[Password],[Email],[tbl_User].Permission_id,tbl_Permission.Type_of_permission FROM [db_Clinic].[dbo].[tbl_User], [db_Clinic].[dbo].tbl_Permission WHERE tbl_Permission.Permission_id = tbl_User.Permission_id";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<ClassUser> userList = new List<ClassUser>();
            while (dr.Read())
            {
                ClassUser user = new ClassUser();
                user.User_id = dr.GetInt32("User_id");
                user.Login = dr.GetString("Login");
                user.Password = dr.GetString("Password");
                user.Email = dr.GetString("Email");
                ClassPermission permission = new ClassPermission();
                permission.Permission = dr.GetString("Type_of_permission");
                permission.PermissionId = dr.GetInt32("Permission_id");
                user.Permission = permission;
                userList.Add(user);
            }
            sqlCon.Close();
            return userList;
        }
        public static List<ClassPermission> PermissionList()
        {
            string querry = "SELECT [Permission_id], [Type_of_permission] FROM [db_Clinic].[dbo].[tbl_Permission]";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<ClassPermission> permissionList = new List<ClassPermission>();
            while (dr.Read())
            {
                ClassPermission permission = new ClassPermission();
                permission.PermissionId = dr.GetInt32("Permission_id");
                permission.Permission = dr.GetString("Type_of_permission");
                permissionList.Add(permission);
            }
            sqlCon.Close();
            return permissionList;
        }

        public static void AddPermission(ClassPermission permission)
        {
            string querry = "INSERT INTO [dbo].[tbl_Permission]([Type_of_permission]) VALUES (@typeOfPermission)";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand cmd = new SqlCommand(querry, sqlCon);
            cmd.Parameters.AddWithValue("@typeOfPermission", permission.Permission);
            cmd.ExecuteNonQuery();
            sqlCon.Close();
        }


        public static void UpdateUser(ClassUser user)
        {
            string querry = "UPDATE [dbo].[tbl_User] SET[Login] = @login ,[Password] = @password ,[Email] = @email,[Permission_id] = @Permission_id WHERE User_id = @userId";

            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@login", user.Login);
            sqlCommand.Parameters.AddWithValue("@password", user.Password);
            sqlCommand.Parameters.AddWithValue("@email", user.Email);
            sqlCommand.Parameters.AddWithValue("@userId", user.User_id);
            sqlCommand.Parameters.AddWithValue("@Permission_id", user.Permission.PermissionId);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }


        public static void AddUser(ClassUser user)
        {
            string querry = "INSERT INTO [dbo].[tbl_User] ([Login], [Password],[Permission_id],[Email]) VALUES (@login, @password,@permission_id, @email)";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand cmd = new SqlCommand(querry, sqlCon);
            cmd.Parameters.AddWithValue("@login", user.Login);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@permission_id", user.Permission.PermissionId);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.ExecuteNonQuery();
            sqlCon.Close();
        }


        public static void DeleteUser(int userId)
        {
            string querry = "DELETE FROM dbo.tbl_User WHERE User_id = @userId";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@userId", userId);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }


        public static List<int> NotAssignedPermission()
        {
            string querry = "SELECT perm.Permission_id FROM dbo.tbl_Permission perm LEFT JOIN tbl_User usr ON perm.Permission_id = usr.Permission_id WHERE usr.Permission_id IS NULL";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<int> indexList = new List<int>();
            while (dr.Read())
            {
                indexList.Add(dr.GetInt32("Permission_id"));
            }
            sqlCon.Close();
            return indexList;
        }

        public static void DeletePermission(int permissionId)
        {
            string querry = "DELETE FROM dbo.tbl_Permission WHERE Permission_id = @permissionId";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@permissionId", permissionId);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }
        public static void UpdateOffice(ClassOffice office)
        {
            string querry = "USE [db_Clinic] UPDATE tbl_Office SET [Office_number]=@OfficeNumber, [Description] = @OfficeDescription WHERE Office_id=@Office_id";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@Office_id", office.OfficeId);
            sqlCommand.Parameters.AddWithValue("@OfficeNumber", office.OfficeNumber);
            sqlCommand.Parameters.AddWithValue("@OfficeDescription", office.OfficeDescription);

            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }
        public static void AddNewOffice(ClassOffice office)
        {

            //Add office staff
            string querryp1 = "USE[db_Clinic] INSERT INTO[dbo].[tbl_Office]([Office_number],[Description]) ";
            string querryp2 = "VALUES(@Office_number,@OfficeDescription)";
            string querry = querryp1 + querryp2;
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@Office_number", office.OfficeNumber);
            sqlCommand.Parameters.AddWithValue("@OfficeDescription", office.OfficeDescription);

            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }
        public static void DeleteOffice(int id)
        {
            string querry = "USE [db_Clinic] DELETE FROM [dbo].[tbl_Office] WHERE [Office_id]=@id";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }


        public static List<int> EmployeesThatAreDoctors()
        {
            string querry = "select [tbl_Employee].Employee_id from [tbl_User], tbl_Employee where[tbl_User].Permission_id = 3 and [tbl_Employee].User_id = tbl_User.User_id";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<int> indexList = new List<int>();
            while (dr.Read())
            {
                indexList.Add(dr.GetInt32("Employee_id"));
            }
            sqlCon.Close();
            return indexList;
        }

        public static List<int> OfficeNumbersList()
        {
            string querry = "SELECT Office_number from tbl_Office";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<int> indexList = new List<int>();
            while (dr.Read())
            {
                indexList.Add(dr.GetInt16("office_number"));
            }
            sqlCon.Close();
            return indexList;
        }

        //A method that gets office staff from the database and returns list of office staff
        public static List<ClassPatient> PatientList()
        {
            string querry = "SELECT [Patient_id],[Name],[Surname],[Phone_number],[Date_of_birth],[Personal_identity_number],[City],[Street],[Street_number],[Active] FROM [db_Clinic].[dbo].[tbl_Patient]";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<ClassPatient> patientList = new List<ClassPatient>();
            while (dr.Read())
            {
                ClassPatient patient = new ClassPatient();
                patient.PatientId = dr.GetInt32("Patient_id");
                patient.Name = dr.GetString("Name");
                patient.Surname = dr.GetString("Surname");
                patient.PhoneNumber = dr.GetString("Phone_number");
                patient.DateOfBirth = dr.GetDateTime("Date_of_birth");
                patient.PersonalIdentityNumber = dr.GetString("Personal_identity_number");
                patient.IsActiv = (bool)dr.GetBoolean("Active");
                patient.City = dr.GetString("City");
                patient.Street = dr.GetString("Street");
                patient.Street_number = dr.GetString("Street_number");
                patientList.Add(patient);
            }
            sqlCon.Close();
            return patientList;
        }


        //Method that updates office staff in database table when you edit office staff in program
        public static void UpdatePatient(ClassPatient patient)
        {

            string querry = "USE [db_Clinic] UPDATE tbl_Patient SET[Name] = @Name,[Surname]= @Surname,[Phone_number]= @Phone_number, Date_of_birth = @Date_of_birth, Active = @Active,[City]=@City,[Street]=@Street,[Street_number]=@Street_number WHERE Patient_id = @Patient_id";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@Patient_id", patient.PatientId);
            sqlCommand.Parameters.AddWithValue("@Name", patient.Name);
            sqlCommand.Parameters.AddWithValue("@Surname", patient.Surname);
            sqlCommand.Parameters.AddWithValue("@Phone_number", patient.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@Date_of_birth", patient.DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@Active", patient.IsActiv);
            sqlCommand.Parameters.AddWithValue("@City", patient.City);
            sqlCommand.Parameters.AddWithValue("@Street", patient.Street);
            sqlCommand.Parameters.AddWithValue("@Street_number", patient.Street_number);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }

        //Method that adds office staff to database table when you add office staff member in program
        public static void AddNewPatient(ClassPatient patient)
        {
            //Add office staff
            string querryp1 = "USE[db_Clinic] INSERT INTO [dbo].tbl_Patient ([Name],[Surname],[Phone_number],[Date_of_birth],[Personal_identity_number],[Active],[City],[Street],[Street_number]) ";
            string querryp2 = "VALUES(@Name,@Surname,@Phone_number,@Date_of_birth,@Personal_identity_number,@Active,@City,@Street,@Street_number)";
            string querry = querryp1 + querryp2;
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@Name", patient.Name);
            sqlCommand.Parameters.AddWithValue("@Surname", patient.Surname);
            sqlCommand.Parameters.AddWithValue("@Phone_number", patient.PhoneNumber);
            sqlCommand.Parameters.AddWithValue("@Date_of_birth", patient.DateOfBirth);
            sqlCommand.Parameters.AddWithValue("@Personal_identity_number", patient.PersonalIdentityNumber);
            sqlCommand.Parameters.AddWithValue("@Active", patient.IsActiv);
            sqlCommand.Parameters.AddWithValue("@City", patient.City);
            sqlCommand.Parameters.AddWithValue("@Street", patient.Street);
            sqlCommand.Parameters.AddWithValue("@Street_number", patient.Street_number);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }

        //Method that deletes patient from data base when you remove
        public static void DeletePatient(int id)
        {
            string querry = "USE[db_Clinic] DELETE FROM [dbo].tbl_Patient WHERE [Patient_id] = @id";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }
        //List of visits
        
        public static void AddVisit(ClassVisit visit)
        {
            string querry = "USE[db_Clinic] INSERT INTO[dbo].[tbl_Appointment] ([Start_time],[Patient_id],[Term_id],[ToPay],[Topic])" +

            "VALUES(@StartTime, @PatientID, @TermID, @ToPay,@Topic)";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand cmd = new SqlCommand(querry, sqlCon);
            if (visit.ToPay == "Waiting")
            {
                cmd.Parameters.AddWithValue("@ToPay", "Doz");
            }
            else
            {
                cmd.Parameters.AddWithValue("@ToPay", "Zap");
            }
            cmd.Parameters.AddWithValue("@TermID", visit.TermId);
            cmd.Parameters.AddWithValue("@PatientID", visit.PatientId);
            cmd.Parameters.AddWithValue("@StartTime", visit.StartTime);
            cmd.Parameters.AddWithValue("@Topic", visit.Topic);
            cmd.ExecuteNonQuery();
            sqlCon.Close();
        }
        public static void UpdateVisit(ClassVisit visit)
        {
            string querry = " UPDATE[dbo].[tbl_Appointment]" +
                           "SET[Start_time] = @Start_time" +
                             " ,[Patient_id] = @PatientId" +
                             " ,[Term_id] = @TermId" +
                             ",[Topic] = @Topic" +
                              " WHERE [Appointment_id]= @VisitId";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand cmd = new SqlCommand(querry, sqlCon);
            cmd.Parameters.AddWithValue("@Start_time", visit.StartTime);
            cmd.Parameters.AddWithValue("@PatientId", visit.PatientId);
            cmd.Parameters.AddWithValue("@TermId", visit.TermId);
            cmd.Parameters.AddWithValue("@Topic", visit.Topic);
            cmd.Parameters.AddWithValue("@VisitId", visit.VisitId);
            
            SqlDataReader dr = cmd.ExecuteReader();
            sqlCon.Close();
        }
        public static List<ClassTerm> ListOfTerms()
        {
            string querry = "Use [db_Clinic] SELECT [Term_id],[Start_time],[End_time],[Date],[Office_id],[Doctor_id] " +
            "FROM [dbo].[tbl_Term]";
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            List<ClassTerm> TermList = new List<ClassTerm>();
            while (dr.Read())
            {
                ClassTerm Term = new ClassTerm();
                Term.TermId = dr.GetInt32("Term_id");
                Term.StartTime = dr.GetTimeSpan(1);
                Term.EndTime = dr.GetTimeSpan(2);
                Term.Date = dr.GetDateTime("Date");
                Term.Office = new DictionariesHanding.ClassOffice();
                Term.Office.OfficeId = dr.GetInt32("Office_id");
                Term.Doctor = new DictionariesHanding.ClassDoctor();
                Term.Doctor.Doctor_id = dr.GetInt32("Doctor_id");
                TermList.Add(Term);
            }
            q.CloseConnection();
            return TermList;
        }
        public static void DeleteVisit(int visitId)
        {
            string querry = "DELETE FROM dbo.tbl_Appointment WHERE Appointment_id = @VisitId";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@VisitId", visitId);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }
        public static void ConfirmVisit(int VisitId)
        {
            string querry = " UPDATE[dbo].[tbl_Appointment]" +
                           "SET [ToPay] = @ToPay WHERE [Appointment_id]= @VisitId";
            
            
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@ToPay", "Zap");
            sqlCommand.Parameters.AddWithValue("@VisitId", VisitId);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }
        public static List<ClassVisit> VisitListSearch(string patient,string doctor, string topic,DateTime startTime,DateTime endTime)
        {
            string querry = "" +
            "USE[db_Clinic]" +
            "SELECT" +
                "[Appointment_id],[tbl_Appointment].[Start_time],[Topic],[tbl_Patient].[Name] as patientName,[tbl_Patient].[Surname] as patientSurname,[tbl_Patient].[Phone_number]," +
                "[tbl_Patient].[Personal_identity_number],[tbl_Appointment].[Term_id],[tbl_Appointment].[Patient_id],[Date],[tbl_Appointment].[Start_time]," +
                "[tbl_Employee].[Name] as doctorName,[tbl_Employee].[Surname] as doctorSurname,[Degree],[Type_of_specialization],[Specialization]," +
                "[Office_number],[ToPay],[tbl_Term].[Doctor_id]" +
            "FROM" +
                "[dbo].[tbl_Appointment], [dbo].[tbl_Patient],[dbo].[tbl_Degree_of_doctor]," +
                "[dbo].[tbl_Specialization],[dbo].[tbl_Type_of_specialization]," +
                "[dbo].[tbl_Employee],[dbo].[tbl_Office],[dbo].[tbl_Term],[dbo].[tbl_Doctor]" +
            "WHERE" +
                "[tbl_Appointment].[Patient_id] =[tbl_Patient].[Patient_id]" +
                "AND[tbl_Appointment].[Term_id] =[tbl_Term].[Term_id]" +
                "AND[tbl_Term].[Office_id] =[tbl_Office].[Office_id]" +
                "AND[tbl_Term].[Doctor_id] =[tbl_Doctor].[Doctor_id]" +
                "AND[tbl_Doctor].[Employee_id] =[tbl_Employee].[Employee_id]" +
                "AND[tbl_Doctor].[Degree_of_doctor_id] =[tbl_Degree_of_doctor].[Degree_of_doctor_id]" +
                "AND[tbl_Doctor].[Type_of_specialization_id] =[tbl_Type_of_specialization].[Type_of_specialization_id]" +
                "AND ([tbl_Patient].[Name] like '%'+@Patient+'%' " +
                "OR [tbl_Patient].[Surname] like '%'+@Patient+'%') " +
                "AND ([tbl_Employee].[Name] like '%'+@Doctor+'%' " +
                "OR [tbl_Employee].[Surname] like '%'+@Doctor+'%') " +
                "AND [tbl_Appointment].[Topic] like '%'+@Topic+'%' " +
                "AND [tbl_Term].[Date] >= @StartTime " +
                "AND [tbl_Term].[Date] <= @EndTime " +
                "AND [tbl_Type_of_specialization].[Specialization_id] =[tbl_Specialization].[Specialization_id];";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@Patient", patient);
            sqlCommand.Parameters.AddWithValue("@Doctor", doctor);
            sqlCommand.Parameters.AddWithValue("@Topic", topic);
            sqlCommand.Parameters.AddWithValue("@StartTime", startTime);
            sqlCommand.Parameters.AddWithValue("@EndTime", endTime);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<ClassVisit> visitList = new List<ClassVisit>();
            while (dr.Read())
            {
                ClassVisit visit = new ClassVisit();
                visit.VisitId = dr.GetInt32("Appointment_id");
                visit.PatientName = dr.GetString("patientName");
                visit.PatientSurname = dr.GetString("patientSurname");
                visit.PhoneNumber = dr.GetString("Phone_number");
                visit.PersonalIdentityNumber = dr.GetString("Personal_identity_number");
                visit.OfficeNumber = dr.GetInt16("Office_number");
                visit.DateVisit = dr.GetDateTime("Date") + dr.GetTimeSpan(1);
                visit.DoctorName = dr.GetString("doctorName") + dr.GetString("doctorSurname");
                //visit.DoctorSurname = dr.GetString("Surname");
                visit.TypeOfSpecialization = dr.GetString("Type_of_specialization");
                visit.Degree = dr.GetString("Degree");
                visit.Specialization = dr.GetString("Specialization");
                visit.Topic = dr.GetString("Topic");
                if (dr.GetString("ToPay").Equals("Doz"))
                {
                    visit.ToPay = "Waiting";
                }
                else
                {
                    visit.ToPay = "Paid";
                }
                visit.PatientId = dr.GetInt32("Patient_id");
                visit.TermId = dr.GetInt32("Term_id");
                visit.StartTime = dr.GetTimeSpan(1);
                visit.DoctorId = dr.GetInt32("Doctor_id");
                
                visitList.Add(visit);
            }
            sqlCon.Close();
            return visitList;
        }
        public static void DeleteOldVisit(DateTime actualDate)
        {
            string querry = "DELETE [dbo].[tbl_Appointment] FROM [dbo].[tbl_Appointment] JOIN [dbo].[tbl_Term] ON [tbl_Appointment].[Term_id] = [tbl_Term].[Term_id]" +
                "WHERE  Date < @ActualDate AND ToPay = 'Doz'";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@ActualDate", actualDate);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            sqlCon.Close();
        }
        //not tested
        public static List<TimeSpan> NotFreeTime(int termId)
        {
            string querry = "USE[db_Clinic] SELECT [Appointment_id],[tbl_Appointment].[Start_time] FROM [dbo].[tbl_Appointment] " +
                "WHERE Term_id = @TermId";
            SqlConnection sqlCon = new SqlConnection(ConString);
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry, sqlCon);
            sqlCommand.Parameters.AddWithValue("@TermId", termId);
            SqlDataReader dr = sqlCommand.ExecuteReader();
            List<TimeSpan> timeList = new List<TimeSpan>();
            TimeSpan temp = new TimeSpan();
            while (dr.Read())
            {
                temp = dr.GetTimeSpan(1);
                timeList.Add(temp);
            }
            sqlCon.Close();
            return timeList;
        }

    }
}
