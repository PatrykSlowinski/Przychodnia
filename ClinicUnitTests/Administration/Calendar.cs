using System;
using Xunit;
using Przychodnia.Class.Calendar;
using Przychodnia.Class.Login;
using Przychodnia.Class.DictionariesHanding;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;
using System.Linq;
using System.Threading;

namespace ClinicUnitTests.Administration
{
    public class Calendar : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases.OrderBy(testCase => testCase.TestMethod.Method.Name);
        }
        [Fact]
        public void GetWorkingDayForClinic()
        {
            List<int> expected = new List<int> { 1, 3, 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15, 17, 18, 19, 20, 21, 22, 24, 25, 26, 27, 28, 29, 31 };
            List<int> actual = ClassGenerateCalendar.WorkingDaysInMonth(2021, 5);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ListOfActiveDoctors()
        {
            ClassDoctor doctor1 = new ClassDoctor() { Active = true, Doctor_id = 1 };
            ClassDoctor doctor2 = new ClassDoctor() { Active = false, Doctor_id = 2 };
            ClassDoctor doctor3 = new ClassDoctor() { Active = true, Doctor_id = 3 };
            ClassDoctor doctor4 = new ClassDoctor() { Active = true, Doctor_id = 4 };
            ClassDoctor doctor5 = new ClassDoctor() { Active = false, Doctor_id = 5 };
            List<ClassDoctor> fullList = new List<ClassDoctor> { doctor1, doctor2, doctor3, doctor4, doctor5 };
            List<ClassDoctor> expected = new List<ClassDoctor> { doctor1, doctor3, doctor4 };
            List<ClassDoctor> actual = ClassGenerateCalendar.ListOfActiveDoctors(fullList);
            Assert.Equal(expected, actual);
        }

        [Theory]
        //Sting length = 0
        [InlineData("","")]
        [InlineData("asdf","asd")]
        public void RemoveLastCharOfString(string input, string expected)
        {
            string actual = ClassHelpers.RemoveLastCharOfString(input);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestGenerationOfCalendar()
        {
            SqlCalendar sql = new SqlCalendar();
            List<int> expected = ClassGenerateCalendar.WorkingDaysInMonth(2000, 1);
            ClassGenerateCalendar.GenerateCalendar(2000, 1, sql);

            List<ClassCalendar> list = sql.CalendarList(); 
            IEnumerable<ClassCalendar> query =
                from elem in list
                where elem.Year == 2000 && elem.Month == 1
                select elem;
            Assert.Single(query);  
            int calendarId = (query.First()).CalendarId;
            List<ClassCalendarDay> actual = sql.ListOfCalendarDays(calendarId);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], actual[i].Day);
            }
        }

        [Fact]
        public void TestShareCalendar()
        {
            SqlCalendar sql = new SqlCalendar();
            IEnumerable<ClassCalendar> querycal =
                from elem in sql.CalendarList()
                where elem.Month==2
                select elem;
            ClassCalendar calendar = querycal.First();

            ClassGenerateCalendar.ShareCalendar(calendar, sql);

            List<ClassTerm> list = sql.ListOfTerms();
            IEnumerable<ClassTerm> query =
                from elem in list
                where elem.TermId==3 && elem.CalendarDay.CalendarDayId == 1 && elem.Doctor.Doctor_id == 1 && elem.StartTime.CompareTo(new TimeSpan(8, 0, 0))==0 && elem.EndTime.CompareTo(new TimeSpan(12, 0, 0)) == 0
                select elem;
            Assert.Single(query);

            query =
                from elem in list
                where elem.TermId == 4 && elem.CalendarDay.CalendarDayId == 2 && elem.Doctor.Doctor_id == 2 && elem.StartTime.CompareTo(new TimeSpan(13, 0, 0)) == 0 && elem.EndTime.CompareTo(new TimeSpan(15, 0, 0)) == 0
                select elem;
            Assert.Single(query);

            querycal =
                from elem in sql.CalendarList()
                where elem.Month == 2
                select elem;
            
            calendar = querycal.First();
            Assert.Equal(2, calendar.Status.StatusId);
        }

        [Fact]
        public void TestCalendarList()
        {
            SqlCalendar sql = new SqlCalendar();

            Assert.Equal(3, sql.CalendarList().Count);

            IEnumerable<ClassCalendar> query =
                from elem in sql.CalendarList()
                where elem.Year == 2000 && elem.Month==2 && elem.CalendarId==1 && elem.Status.StatusId==1
                select elem;
            Assert.Single(query);

            query =
                from elem in sql.CalendarList()
                where elem.Year == 2000 && elem.Month == 3 && elem.CalendarId == 2 && elem.Status.StatusId == 3
                select elem;
            Assert.Single(query);

            query =
                from elem in sql.CalendarList()
                where elem.Year == 2000 && elem.Month == 4 && elem.CalendarId == 3 && elem.Status.StatusId == 1
                select elem;
            Assert.Single(query);
        }

        [Fact]
        public void TestCreateCalendar()
        {
            SqlCalendar sql = new SqlCalendar();

            int expected = sql.CalendarList().Count + 1;
            sql.CreateCalendar(2000, 1);
            int actual = sql.CalendarList().Count;
            Assert.Equal(expected, actual);

            IEnumerable<ClassCalendar> query =
                from elem in sql.CalendarList()
                where elem.Year == 2000 && elem.Month == 1 && elem.Status.StatusId == 1
                select elem;
            Assert.Single(query);
        }

        [Fact]
        public void TestCreateCalendarDays()
        {
            SqlCalendar sql = new SqlCalendar();

            IEnumerable<ClassCalendar> querycal =
               from elem in sql.CalendarList()
               where elem.Month == 4
               select elem;
            ClassCalendar calendar = querycal.First();
            int id = calendar.CalendarId;

            sql.CreateCalendarDays("(1,'07:00:00','20:00:00',"+id+"),(2,'07:00:00','20:00:00',"+id+"),(3,'07:00:00','20:00:00',"+id+")");

            IEnumerable<ClassCalendarDay> query =
                from elem in sql.ListOfCalendarDays(id)
                where elem.Day==1 && elem.StartTime.CompareTo(new TimeSpan(7,0,0))==0 && elem.EndTime.CompareTo(new TimeSpan(20, 0, 0)) == 0
                select elem;
            Assert.Single(query);

            query =
                from elem in sql.ListOfCalendarDays(id)
                where elem.Day == 2 && elem.StartTime.CompareTo(new TimeSpan(7, 0, 0)) == 0 && elem.EndTime.CompareTo(new TimeSpan(20, 0, 0)) == 0
                select elem;
            Assert.Single(query);

            query =
                from elem in sql.ListOfCalendarDays(id)
                where elem.Day == 3 && elem.StartTime.CompareTo(new TimeSpan(7, 0, 0)) == 0 && elem.EndTime.CompareTo(new TimeSpan(20, 0, 0)) == 0
                select elem;
            Assert.Single(query);
        }

        [Fact]
        public void TestListOfCalendarDays()
        {
            SqlCalendar sql = new SqlCalendar();
            
            IEnumerable<ClassCalendarDay> query =
               from elem in sql.ListOfCalendarDays(1)
               where elem.CalendarDayId == 1 && elem.Day == 1 && elem.StartTime.CompareTo(new TimeSpan(7, 0, 0)) == 0 && elem.EndTime.CompareTo(new TimeSpan(20, 0, 0)) == 0
               select elem;
            Assert.Single(query);

            query =
               from elem in sql.ListOfCalendarDays(1)
               where elem.CalendarDayId == 2 && elem.Day == 2 && elem.StartTime.CompareTo(new TimeSpan(7, 0, 0)) == 0 && elem.EndTime.CompareTo(new TimeSpan(20, 0, 0)) == 0
               select elem;
            Assert.Single(query);
        }

        [Theory]
        [InlineData(1,"New")]
        [InlineData(2,"Shared for doctors")]
        [InlineData(3, "During verification")]
        [InlineData(4, "Verified")]
        [InlineData(5, "Waiting for administrator acceptance")]
        [InlineData(6, "During verification by the doctor")]
        [InlineData(7, "Accepted by the doctor")]
        public void TestStatusList(int id, string status)
        {
            SqlCalendar sql = new SqlCalendar();

            IEnumerable<ClassStatus> query =
               from elem in sql.StatusList()
               where elem.StatusId == id && elem.Status == status
               select elem;
            Assert.Single(query);
        }

        [Theory]
        [InlineData(EnumStatus.New, "New")]
        [InlineData(EnumStatus.SharedForDoctors, "Shared for doctors")]
        [InlineData(EnumStatus.DuringVerification, "During verification")]
        [InlineData(EnumStatus.Verified, "Verified")]
        [InlineData(EnumStatus.WaitingForAdministratorAcceptance, "Waiting for administrator acceptance")]
        [InlineData(EnumStatus.DuringVerificationByTheDoctor, "During verification by the doctor")]
        [InlineData(EnumStatus.AcceptedByTheDoctor, "Accepted by the doctor")]
        public void TestClassStatusString(EnumStatus status, string expected)
        {
            Assert.Equal(expected ,ClassStatus.StatusString(status));
        }

        [Fact]
        public void TestCreateCalendarDoctor()
        {
            int id = 3;
            SqlCalendar sql = new SqlCalendar();
            int expected = sql.ListOfCalendarDoctor(id).Count + 1;
            sql.CreateCalendarDoctor(1,3);
            int actual = sql.ListOfCalendarDoctor(id).Count;

            IEnumerable<ClassCalendarDoctor> query =
               from elem in sql.ListOfCalendarDoctor(id)
               where elem.Doctor.Doctor_id == 1 && elem.Status.StatusId==6
               select elem;
            Assert.Single(query);
        }

        [Fact]
        public void TestCreateTerms()
        {
            SqlCalendar sql = new SqlCalendar();
            IEnumerable<ClassCalendar> querycal =
                from elem in sql.CalendarList()
                where elem.Month == 2
                select elem;
            ClassCalendar calendar = querycal.First();

            sql.CreateTerms("('08:00:00','12:00:00','2000-2-1',3,1,100,1),('13:00:00','15:00:00','2000-2-2',4,2,200,2)");
            ClassGenerateCalendar.ShareCalendar(calendar, sql);

            List<ClassTerm> list = sql.ListOfTerms();
            IEnumerable<ClassTerm> query =
                from elem in list
                where elem.TermId == 3 && elem.CalendarDay.CalendarDayId == 1 && elem.Doctor.Doctor_id == 1 && elem.StartTime.CompareTo(new TimeSpan(8, 0, 0)) == 0 && elem.EndTime.CompareTo(new TimeSpan(12, 0, 0)) == 0
                select elem;
            Assert.Single(query);

            query =
                from elem in list
                where elem.TermId == 4 && elem.CalendarDay.CalendarDayId == 2 && elem.Doctor.Doctor_id == 2 && elem.StartTime.CompareTo(new TimeSpan(13, 0, 0)) == 0 && elem.EndTime.CompareTo(new TimeSpan(15, 0, 0)) == 0
                select elem;
            Assert.Single(query);

            querycal =
                from elem in sql.CalendarList()
                where elem.Month == 2
                select elem;

            calendar = querycal.First();
            Assert.Equal(2, calendar.Status.StatusId);
        }

        [Theory]
        [InlineData(true, 1, "Jan", "Nowak", 100)]
        [InlineData(true, 2, "Tomasz", "Kowalski", 200)]
        public void TestDoctorList(bool active, int doctorId, string name, string surname, int officeNumber)
        {
            SqlCalendar sql = new SqlCalendar();
            IEnumerable<ClassDoctor> query =
                from elem in sql.DoctorList()
                where elem.Active == active && elem.Doctor_id == doctorId && elem.Name == name && elem.Surname == surname && elem.OfficeNumber==officeNumber
                select elem;
            Assert.Single(query);
        }
        
        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        public void TestListFixedTermsForSpecifiedDoctor(int doctorId, int day)
        {
            SqlCalendar sql = new SqlCalendar();

            IEnumerable<ClassFixedTerms> query =
                from elem in sql.ListFixedTermsForSpecifiedDoctor(doctorId)
                where elem.Day == day
                select elem;
            Assert.Single(query);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, 1, 2)]
        [InlineData(1, 2, 3)]
        [InlineData(7, 2, 4)]
        public void TestSelectCalendarDayId(int Day, int Calendar_id, int expected)
        {
            SqlCalendar sql = new SqlCalendar();
            Assert.Equal(expected, sql.SelectCalendarDayId(Day, Calendar_id));
        }

        [Theory]
        [InlineData(EnumStatus.New, 1)]
        [InlineData(EnumStatus.SharedForDoctors, 2)]
        [InlineData(EnumStatus.DuringVerification, 3)]
        [InlineData(EnumStatus.Verified, 4)]
        [InlineData(EnumStatus.WaitingForAdministratorAcceptance, 5)]
        [InlineData(EnumStatus.DuringVerificationByTheDoctor, 6)]
        [InlineData(EnumStatus.AcceptedByTheDoctor, 7)]
        public void TestSelectStatusId(EnumStatus enumstatus, int expected)
        {
            SqlCalendar sql = new SqlCalendar();
            Assert.Equal(expected, sql.SelectStatusId(enumstatus));
        }

        [Fact]
        public void TestUpdateCalendarStatus()
        {
            SqlCalendar sql = new SqlCalendar();
            sql.UpdateCalendarStatus(3, 3);

            IEnumerable<ClassCalendar> query =
                from elem in sql.CalendarList()
                where elem.Status.StatusId == 3 && elem.CalendarId == 3
                select elem;
            Assert.Single(query);
        }

        [Theory]
        [InlineData(1, 3, 1, 100, 2000, 3)]
        [InlineData(2, 4, 2, 200, 2000, 3)]
        public void TestTermsForSpecifiedYearAndMonth(int termId, int calendarDayId, int calendarDoctorId, int officeNumber, int year, int month)
        {
            SqlCalendar sql = new SqlCalendar();

            IEnumerable<ClassTerm> query =
                from elem in sql.TermsForSpecifiedYearAndMonth(year,month)
                where elem.TermId == termId && elem.CalendarDay.CalendarDayId == calendarDayId && elem.CalendarDoctor.CalendarDoctorId == calendarDoctorId && elem.Office.OfficeNumber == officeNumber
                select elem;
            Assert.Single(query);
        }

        [Fact]
        public void TestTermLisTSelectedDoctor()
        {
            SqlCalendar sql = new SqlCalendar();

            IEnumerable<ClassTerm> query =
               from elem in sql.TermLisTSelectedDoctor(1,1)
               select elem;
            Assert.Single(query);
        }

        [Theory]
        [InlineData(1, 2, 1, 7)]
        [InlineData(2, 2, 2, 7)]
        public void TestListOfCalendarDoctor(int calendarDoctorId, int calendarId, int doctorId, int statusId)
        {
            SqlCalendar sql = new SqlCalendar();

            IEnumerable<ClassCalendarDoctor> query =
               from elem in sql.ListOfCalendarDoctor(calendarId)
               where elem.CalendarDoctorId == calendarDoctorId && elem.Doctor.Doctor_id == doctorId && elem.Status.StatusId == statusId
               select elem;
            Assert.Single(query);
        }

        [Theory]
        [InlineData(1,3,1,1,100)]
        [InlineData(2,4,2,2,200)]
        public void TestListOfTerms(int termId, int calendarDayId, int doctorId, int calendarDoctorId, int officeNumber)
        {
            SqlCalendar sql = new SqlCalendar();

            IEnumerable<ClassTerm> query =
               from elem in sql.ListOfTerms()
               where elem.TermId == termId && elem.CalendarDay.CalendarDayId == calendarDayId && elem.Doctor.Doctor_id == doctorId && elem.CalendarDoctor.CalendarDoctorId == calendarDoctorId && elem.Office.OfficeNumber == officeNumber
               select elem;
            Assert.Single(query);
        }

        [Theory]
        [InlineData(2000,2,1)]
        [InlineData(2000,3,2)]
        [InlineData(2000,4,3)]
        public void TestSelectCalendarId(int year, int month, int expected)
        {
            SqlCalendar sql = new SqlCalendar();
            Assert.Equal(expected, sql.SelectCalendarId(year,month));
        }

        [Fact]
        public void TestUpdateOffice()
        {
            SqlCalendar sql = new SqlCalendar();
            sql.UpdateOffice(1,200);

            IEnumerable<ClassTerm> query =
                from elem in sql.ListOfTerms()
                where elem.TermId == 1 && elem.Office.OfficeNumber==200
                select elem;
            Assert.Single(query);
        }

        [Fact]
        public void TestDeleteTerm()
        {
            SqlCalendar sql = new SqlCalendar();
            sql.DeleteTerm(1);

            IEnumerable<ClassTerm> query =
                from elem in sql.ListOfTerms()
                where elem.TermId == 1
                select elem;
            Assert.Empty(query);
        }

        [Fact]
        public void TestDeleteCalendarDay()
        {
            SqlCalendar sql = new SqlCalendar();
            sql.DeleteCalendarDay(1);

            IEnumerable<ClassCalendarDay> query =
                from elem in sql.ListOfCalendarDays(1)
                where elem.CalendarDayId == 1
                select elem;
            Assert.Empty(query);
        }

        [Fact]
        public void TestDeleteCalendarAndApropriateCalendarDays()
        {
            SqlCalendar sql = new SqlCalendar();
            sql.DeleteCalendarAndApropriateCalendarDays(1);

            IEnumerable<ClassCalendarDay> query =
                from elem in sql.ListOfCalendarDays(1)
                select elem;
            Assert.Empty(query);

            IEnumerable<ClassCalendar> query2 =
                from elem in sql.CalendarList()
                where elem.CalendarId == 1
                select elem;
            Assert.Empty(query2);
        }
    }
}
