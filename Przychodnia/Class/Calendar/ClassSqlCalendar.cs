using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Windows.Controls;
using Przychodnia.Class.DictionariesHanding;

namespace Przychodnia.Class.Calendar
{
    public class ClassSqlCalendar:ISqlCalendar
    {
        #region Lists
        public List<ClassStatus> StatusList()
        {
            string querry = "Use[db_Clinic] SELECT[Status_id],[Status] FROM[dbo].[tbl_Status]";
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            List<ClassStatus> StatusList = new List<ClassStatus>();
            while (dr.Read())
            {
                ClassStatus Status = new ClassStatus();
                Status.StatusId = dr.GetInt32("Status_id");
                Status.Status = dr.GetString("Status");
                StatusList.Add(Status);
            }
            q.CloseConnection();
            return StatusList;
        }
        public List<ClassTerm> TermsForSpecifiedYearAndMonth(int year, int month)
        {
            string querry = "Use [db_Clinic] SELECT [Term_id],[Start_time],[End_time],[Date],[Office_id],[Doctor_id] " +
            "FROM [dbo].[tbl_Term] WHERE YEAR(Date)= " + year + " AND MONTH(Date) = " + month;
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
        public List<ClassCalendar> CalendarList()
        {
            string querry = "Use [db_Clinic] SELECT [Calendar_id],[Year],[Month],[Status_id] FROM [db_Clinic].[dbo].[tbl_Calendar]";
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            List<ClassCalendar> CalendarList = new List<ClassCalendar>();
            while (dr.Read())
            {
                ClassCalendar calendar = new ClassCalendar();
                calendar.CalendarId = dr.GetInt32("Calendar_id");
                calendar.Year = dr.GetInt32("Year");
                calendar.Month = dr.GetInt32("Month");
                calendar.Status = new ClassStatus();
                calendar.Status.StatusId = dr.GetInt32("Status_id");
                CalendarList.Add(calendar);
            }
            q.CloseConnection();
            return CalendarList;
        }
        /*public List<ClassDoctor> ListOfDoctorsForSpecifiedCalendar(int calendarId)
        {
            string querry = "USE [db_Clinic] SELECT [tbl_Doctor].[Doctor_id],[Name],[Surname],[Phone_number],[Active],[Degree],[Type_of_specialization],[Office_id],[Specialization], " +
            "[tbl_Degree_of_doctor].[Degree_of_doctor_id],[tbl_Type_of_specialization].[Type_of_specialization_id],[tbl_Specialization].[Specialization_id], [tbl_Calendar_doctor].[Status_id], " +
            "[tbl_Calendar].[Calendar_id], [tbl_Status].[Status] " +
            "FROM[dbo].[tbl_Doctor], [dbo].[tbl_Degree_of_doctor], [dbo].[tbl_Specialization],[dbo].[tbl_Type_of_specialization], [dbo].[tbl_Calendar_doctor], [dbo].[tbl_Calendar], [dbo].[tbl_Status], [dbo].[tbl_Employee] " +
            "WHERE[tbl_Doctor].[Degree_of_doctor_id] =[tbl_Degree_of_doctor].[Degree_of_doctor_id] " +
            "AND[tbl_Doctor].[Type_of_specialization_id] =[tbl_Type_of_specialization].[Type_of_specialization_id] " +
            "AND[tbl_Type_of_specialization].[Specialization_id] =[tbl_Specialization].[Specialization_id] " +
            "AND[Active] = 'Yes' " +
            "AND[tbl_Calendar_doctor].[Doctor_id] =[tbl_Doctor].[Doctor_id] " +
            "AND[tbl_Calendar].[Calendar_id] = " + calendarId +
            " AND [tbl_Status].[Status_id]=[tbl_Calendar_doctor].[Status_id] " +
            "AND [tbl_Calendar].[Calendar_id]=[tbl_Calendar_doctor].[Calendar_id] " +
            "AND [tbl_Employee].[Employee_id]=[tbl_doctor].[Employee_id]";
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            List<ClassDoctor> DoctorList = new List<ClassDoctor>();
            while (dr.Read())
            {
                ClassDoctor dct = new ClassDoctor();
                dct.Doctor_id = dr.GetInt32("Doctor_id");
                dct.Name = dr.GetString("Name");
                dct.Surname = dr.GetString("Surname");
                dct.PhoneNumber = dr.GetString("Phone_number");
                dct.OfficeNumber = dr.GetInt32("Office_id");
                dct.Active = false;
                if (dr.GetString("Active").Equals("Yes")) dct.Active = true;

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
                dct.Status = new ClassStatus();
                dct.Status.StatusId = dr.GetInt32("Status_id");
                dct.Status.Status = dr.GetString("Status");
                DoctorList.Add(dct);
            }
            q.CloseConnection();
            return DoctorList;
        }*/
        public List<ClassTerm> TermLisTSelectedDoctor(int Calendar_id, int Doctor_id)
        {
            string querry = "" +
            "USE[db_Clinic] " +
            "Select Start_time, End_time, Date, Office_number, Name, Surname, tbl_Doctor.Doctor_id " +
            "FROM tbl_Term, tbl_Office, tbl_Doctor, tbl_Employee, tbl_Calendar_doctor, tbl_Calendar " +
            "WHERE tbl_Office.Office_id = tbl_Term.Office_id " +
            "AND tbl_Doctor.Doctor_id = tbl_Term.Doctor_id " +
            "AND tbl_Employee.Employee_id = tbl_Doctor.Employee_id " +
            "AND tbl_Calendar_doctor.Doctor_id = tbl_Doctor.Doctor_id " +
            "AND tbl_Calendar.Calendar_id = tbl_Calendar_doctor.Calendar_id " +
            "AND tbl_Doctor.Doctor_id = "+Doctor_id+
            " AND tbl_Calendar.Calendar_id = "+Calendar_id;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            List<ClassTerm> TermList = new List<ClassTerm>();
            while (dr.Read())
            {
                ClassTerm term = new ClassTerm();
                term.StartTime = dr.GetTimeSpan(0);
                term.EndTime = dr.GetTimeSpan(1);
                term.Date = dr.GetDateTime("Date");
                term.Doctor = new ClassDoctor();
                term.Doctor.Name = dr.GetString("Name");
                term.Doctor.Surname = dr.GetString("Surname");
                TermList.Add(term);
            }
            q.CloseConnection();
            return TermList;
        }
        public List<ClassCalendarDay> ListOfCalendarDays(int calendarId)
        {
            string querry = "USE[db_Clinic] SELECT[Calendar_day_id],[Day],[Start_time],[End_time],[tbl_Calendar].[Calendar_id], [Year],[Month] " +
            "FROM[dbo].[tbl_Calendar_Day], [dbo].[tbl_Calendar] " +
            "WHERE[tbl_Calendar].[Calendar_id] =[tbl_Calendar_Day].[Calendar_id] " +
            "AND[tbl_Calendar].Calendar_id = " + calendarId;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            List<ClassCalendarDay> dayList = new List<ClassCalendarDay>();
            while (dr.Read())
            {
                ClassCalendarDay day = new ClassCalendarDay();
                day.CalendarDayId = dr.GetInt32("Calendar_day_id");
                day.Day = dr.GetInt32("Day");
                day.StartTime = dr.GetTimeSpan(2);
                day.EndTime = dr.GetTimeSpan(3);
                day.Calendar = new ClassCalendar();
                day.Calendar.CalendarId = dr.GetInt32("Calendar_id");
                day.Calendar.Year = dr.GetInt32("Year");
                day.Calendar.Month = dr.GetInt32("Month");
                dayList.Add(day);
            }
            q.CloseConnection();
            return dayList;
        }
        public List<ClassCalendarDoctor> ListOfCalendarDoctor(int calendarId)
        {
            string querry = "USE[db_Clinic] " +
            "SELECT[Calendar_doctor_id],[Name],[Surname],[Status],[Calendar_id], [tbl_Status].[Status_id],[tbl_Calendar_doctor].[Doctor_id] " +
            "FROM[dbo].[tbl_Calendar_doctor], [tbl_Status], [tbl_Doctor], [tbl_Employee] " +
            "WHERE[tbl_Status].[Status_id] =[tbl_Calendar_doctor].[Status_id] " +
            "AND[tbl_Doctor].[Doctor_id] =[tbl_Calendar_doctor].[Doctor_id] " +
            "AND[tbl_Employee].[Employee_id]=[tbl_Doctor].[Employee_id] " +
            "AND[Calendar_id] = " + calendarId;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            List<ClassCalendarDoctor> calendarDoctorList = new List<ClassCalendarDoctor>();
            while (dr.Read())
            {
                ClassCalendarDoctor cd = new ClassCalendarDoctor();
                cd.CalendarDoctorId = dr.GetInt32("Calendar_doctor_id");
                cd.Doctor = new ClassDoctor();
                cd.Doctor.Name = dr.GetString("Name");
                cd.Doctor.Surname = dr.GetString("Surname");
                cd.Doctor.Doctor_id = dr.GetInt32("Doctor_id");
                cd.Status = new ClassStatus();
                cd.Status.Status = dr.GetString("Status");
                cd.Status.StatusId = dr.GetInt32("Status_id");
                calendarDoctorList.Add(cd);
            }
            q.CloseConnection();
            return calendarDoctorList;
        }
        public List<ClassTerm> ListOfTerms()
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
        public List<ClassOverlappingCalendar> OverlappingTerms(int callendarId)
        {
            string querry = "" +
             "USE[db_Clinic] " +
             "SELECT DISTINCT t1.Term_id,t1.Doctor_id,tbl_Employee.Name,tbl_Employee.Surname, t1.Office_id,Office_number,t1.Date, T1.Calendar_day_id  " +
             "FROM[tbl_Term] AS t1, [tbl_Term] AS t2, tbl_Office, tbl_Doctor as d1, tbl_doctor as d2, tbl_Calendar_doctor, tbl_Employee  " +
             "WHERE t2.Date = t1.Date " +
             "AND t1.Office_id = t2.Office_id " +
             "AND t1.Term_id > t2.Term_id " +
             "AND(t1.Start_time < t2.End_time " +
             "AND t2.Start_time < t1.End_time) " +
             "AND(t2.Start_time < t1.End_time " +
             "AND t2.Start_time < t1.End_time) " +
             "AND tbl_Office.Office_id = t1.Office_id " +
             "AND t1.Doctor_id = d1.Doctor_id " +
             "AND t2.Doctor_id = d2.Doctor_id " +
            "AND tbl_Calendar_doctor.Calendar_doctor_id = t1.Calendar_doctor_id " +
            "AND tbl_Employee.Employee_id = d1.Employee_id " +
            "AND tbl_Calendar_doctor.Calendar_id = " + callendarId;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            List<ClassOverlappingCalendar> list = new List<ClassOverlappingCalendar>();
            while (dr.Read())
            {
                ClassOverlappingCalendar Term = new ClassOverlappingCalendar();
                Term.TermId = dr.GetInt32("Term_id");
                Term.DoctorID = dr.GetInt32("Doctor_id");
                Term.Name = dr.GetString("Name");
                Term.Surname = dr.GetString("Surname");
                Term.OfficeId = dr.GetInt32("Office_id");
                Term.OfficeNumber = dr.GetInt16("Office_number");
                Term.Date = dr.GetDateTime("Date");
                Term.CalendarDayId = dr.GetInt32("Calendar_day_id");
                list.Add(Term);
            }
            q.CloseConnection();
            return list;
        }
        public List<ClassOffice> ListOfAvailableOfficeForSelectedDay(int Calendar_day_id)
        {
            string querry = "" +
             "SELECT * FROM tbl_Office " +
             "WHERE Office_id NOT IN " +
             "( " +
             "SELECT tbl_Office.Office_id FROM tbl_Calendar_Day, tbl_Term, tbl_Office " +
             "WHERE tbl_Calendar_Day.Calendar_day_id = tbl_Term.Calendar_day_id " +
             "AND tbl_Calendar_Day.Calendar_day_id = " + Calendar_day_id +
             " AND tbl_Office.Office_id = tbl_Term.Office_id " +
             ")";
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            List<ClassOffice> list = new List<ClassOffice>();
            while (dr.Read())
            {
                ClassOffice office = new ClassOffice();
                office.OfficeId = dr.GetInt32("Office_id");
                office.OfficeNumber = dr.GetInt16("Office_number");
                list.Add(office);
            }
            q.CloseConnection();
            return list;
        }
        public List<ClassFixedTerms> ListFixedTermsForSpecifiedDoctor(int doctorId)
        {
            string querry = "USE [db_Clinic] SELECT [Day],[Start],[End] FROM tbl_Fixed_terms WHERE Doctor_id=" + doctorId;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            List<ClassFixedTerms> FixedTermsList = new List<ClassFixedTerms>();
            while (dr.Read())
            {
                ClassFixedTerms term = new ClassFixedTerms();
                term.Day = dr.GetInt32("Day");
                term.Start = dr.GetTimeSpan(1);
                term.End = dr.GetTimeSpan(2);
                FixedTermsList.Add(term);
            }
            q.CloseConnection();
            return FixedTermsList;
        }
        public List<ClassDoctor> DoctorList()
        {
            return ClassSQLConnections.DoctorList();
        }
        #endregion

        #region Select
        public int SelectCalendarDayId(int Day, int Calendar_id)
        {
            string querry = "Use [db_Clinic] SELECT Calendar_day_id FROM [tbl_Calendar_Day] WHERE DAY = "+Day+" AND Calendar_id = "+Calendar_id;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            int id = 0;
            while (dr.Read())
            {
                id = dr.GetInt32("Calendar_day_id");
            }
            q.CloseConnection();
            return id;
        }
        public int SelectCalendarId(int year, int month)
        {
            string querry = "USE [db_Clinic] SELECT [Calendar_id] FROM [dbo].[tbl_Calendar] WHERE [Year] = "+year+" AND [Month]="+month;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            int id = 0;
            while (dr.Read())
            {
                id = dr.GetInt32("Calendar_id");
            }
            q.CloseConnection();
            return id;
        }
        public int SelectStatusId(EnumStatus enumstatus)
        {
            string status = ClassStatus.StatusString(enumstatus);
            string querry = "USe db_Clinic SELECT Status_id FROM tbl_Status WHERE Status='"+status+"'";
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            int id = 0;
            while (dr.Read())
            {
                id = dr.GetInt32("Status_id");
            }
            q.CloseConnection();
            return id;
        }
        #endregion

        #region Create
        public int CreateCalendar(int Year, int Month)
        {
            string querry = "USE [db_Clinic] INSERT INTO [dbo].[tbl_Calendar](Year,Month,Status_id) "+ 
            String.Format("VALUES({0}, {1}, {2})", Year, Month, new ClassSqlCalendar().SelectStatusId(EnumStatus.New));
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            q.CloseConnection();
            return SelectCalendarId(Year, Month);
        }
        public int CreateCalendarDoctor(int Doctor_id, int Calendar_id)
        {
            string querry = " USE[db_Clinic] INSERT INTO[dbo].[tbl_Calendar_doctor] (Status_id, Doctor_id, Calendar_id) "+
            String.Format("VALUES({0},{1},{2})", SelectStatusId(EnumStatus.DuringVerificationByTheDoctor), Doctor_id, Calendar_id);
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            q.CloseConnection();

            querry = "USE [db_Clinic] Select [Calendar_doctor_id] FROM [dbo].[tbl_Calendar_doctor] WHERE Doctor_id="+Doctor_id+" AND Calendar_id="+Calendar_id;
            dr = q.ExecuteQuerry(querry);
            int index = 0;
            while (dr.Read())
            {
                index = dr.GetInt32("Calendar_doctor_id");
            }
            q.CloseConnection();
            return index;
        }
        public void CreateCalendarDays(string days)
        {
            string querry = "USE [db_Clinic] INSERT INTO [dbo].[tbl_Calendar_Day](Day, Start_time, End_time, Calendar_id) VALUES " + days;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            q.CloseConnection();
        }
        public void CreateTerms(string text)
        {
            if (text.Length == 0) return;
            string querry = "Use [db_Clinic] INSERT INTO [dbo].[tbl_Term] (Start_time,End_time,Date,Calendar_doctor_id,Calendar_day_id,Office_id,Doctor_id)" +
            "Values " + text;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            q.CloseConnection();
        }
        public void CreateCalendarDay(int day, int calendar_id, TimeSpan start, TimeSpan end)
        {
            string querry = "USE [db_Clinic] INSERT INTO [dbo].[tbl_Calendar_Day](Day, Start_time, End_time, Calendar_id) " +
            String.Format("Values({0}, '{1}', '{2}', {3})", day, start, end, calendar_id);
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            q.CloseConnection();
        }
        #endregion

        #region Update
        public void UpdateCalendarStatus(int Status_id, int Calendar_id)
        {
            string querry = "USE [db_Clinic] UPDATE [dbo].[tbl_Calendar] SET [Status_id]="+Status_id+" WHERE [Calendar_id]="+Calendar_id;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            q.CloseConnection();
        }
        public void UpdateStatus(int Calendar_id, int Status_id)
        {
            string querry = "USE[db_Clinic] UPDATE[dbo].[tbl_Calendar] SET[Status_id] = "+Status_id+" WHERE Calendar_id = "+Calendar_id;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            q.CloseConnection();
        }
        public void UpdateCalendar(int Calendar_id)
        {
            string querry = "USE[db_Clinic] " +
            "BEGIN " +
            "DECLARE @nubmerOfGeneralCalendar int " +
            "SELECT @nubmerOfGeneralCalendar =  COUNT(*) FROM[dbo].[tbl_Calendar], [dbo].[tbl_Calendar_Doctor], [dbo].[tbl_Status] " +
                   " WHERE[tbl_Calendar].[Calendar_id] =[tbl_Calendar_Doctor].[Calendar_id] " +
                   " AND[tbl_Status].Status_id =[tbl_Calendar_Doctor].[Status_id] " +
                   " AND[tbl_Calendar].[Calendar_id] = "+Calendar_id+
            " PRINT @nubmerOfGeneralCalendar; " +
                    "DECLARE @nubmerOfAcceptedCalendar int " +
                    "SELECT @nubmerOfAcceptedCalendar = COUNT(*) FROM[dbo].[tbl_Calendar], [dbo].[tbl_Calendar_Doctor], [dbo].[tbl_Status] " +
                    "WHERE[tbl_Calendar].[Calendar_id] =[tbl_Calendar_Doctor].[Calendar_id] " +
                    "AND[tbl_Status].Status_id =[tbl_Calendar_Doctor].[Status_id] " +
                   " AND[tbl_Calendar].[Calendar_id] = " + Calendar_id +
            " AND[tbl_Status].Status = 'Accepted by the doctor' " +
            "PRINT @nubmerOfAcceptedCalendar; " +
                    "PRINT @nubmerOfAcceptedCalendar " +
            "PRINT @nubmerOfGeneralCalendar " +
            "IF(@nubmerOfAcceptedCalendar = @nubmerOfGeneralCalendar) " +
            "UPDATE[dbo].[tbl_Calendar] SET[Status_id] = 3 WHERE[Calendar_id] = " + Calendar_id +
            " END ";
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            q.CloseConnection();
        }
        public void UpdateOffice(int termid, int officeId)
        {
            string querry = "UPDATE tbl_Term " +
            "SET Office_id = "+officeId+
            " WHERE Term_id="+termid;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            q.CloseConnection();
        }
        public void UpdateCalendarDay(int day, int Calendar_day_id, TimeSpan start, TimeSpan end)
        {
            string querry = "USE [db_Clinic] UPDATE [dbo].[tbl_Calendar_Day] SET Day=" + day + ", Start_time='" + start + "', End_time='" + end + "' WHERE Calendar_day_id=" + Calendar_day_id;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            q.CloseConnection();
        }
        #endregion

        #region Delete
        public void DeleteTerm(int TermId)
        {
            string querry = "Use [db_Clinic] DELETE [dbo].[tbl_Term] WHERE Term_id = "+TermId;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            q.CloseConnection();
        }
        public void DeleteCalendarDay(int calendarId)
        {
            string querry = "USE [db_Clinic] DELETE [dbo].[tbl_Calendar_Day] WHERE Calendar_day_id="+calendarId;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            q.CloseConnection();
        }
        public void DeleteCalendarAndApropriateCalendarDays(int Calendar_id)
        {
            string querry = "" +
            "DELETE FROM tbl_Calendar_Day WHERE Calendar_id = "+ Calendar_id +
            " DELETE FROM tbl_Calendar WHERE Calendar_id = "+ Calendar_id;
            ClassQuerry q = new ClassQuerry();
            SqlDataReader dr = q.ExecuteQuerry(querry);
            q.CloseConnection();
        }
        #endregion
    }
}
