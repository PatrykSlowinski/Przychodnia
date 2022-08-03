using System;
using System.Collections.Generic;
using System.Text;
using Przychodnia.Class.Calendar;
using Przychodnia.Class.DictionariesHanding;

namespace ClinicUnitTests.Administration
{
    public class SqlCalendar : ISqlCalendar
    {
        public SqlCalendar()
        {
            #region Office
            ClassOffice o1 = new ClassOffice();
            o1.OfficeId = 1;
            o1.OfficeNumber = 100;

            ClassOffice o2 = new ClassOffice();
            o1.OfficeId = 2;
            o1.OfficeNumber = 200;
            ListOffice = new List<ClassOffice>() { o1, o2 };
            #endregion

            #region Status
            ClassStatus s1 = new ClassStatus();
            s1.StatusId = 1;
            s1.Status = "New";
            ClassStatus s2 = new ClassStatus();
            s2.StatusId = 2;
            s2.Status = "Shared for doctors";
            ClassStatus s3 = new ClassStatus();
            s3.StatusId = 3;
            s3.Status = "During verification";
            ClassStatus s4 = new ClassStatus();
            s4.StatusId = 4;
            s4.Status = "Verified";
            ClassStatus s5 = new ClassStatus();
            s5.StatusId = 5;
            s5.Status = "Waiting for administrator acceptance";
            ClassStatus s6 = new ClassStatus();
            s6.StatusId = 6;
            s6.Status = "During verification by the doctor";
            ClassStatus s7 = new ClassStatus();
            s7.StatusId = 7;
            s7.Status = "Accepted by the doctor";
            ListStatus =  new List<ClassStatus>() { s1, s2, s3, s4, s5, s6, s7 };
            #endregion

            #region Doctor
            ClassDoctor dct1 = new ClassDoctor();
            dct1.Active = true;
            dct1.Doctor_id = 1;
            dct1.Name = "Jan";
            dct1.Surname = "Nowak";
            dct1.OfficeNumber = 100;
            ClassDoctor dct2 = new ClassDoctor();
            dct2.Active = true;
            dct2.Doctor_id = 2;
            dct2.Name = "Tomasz";
            dct2.Surname = "Kowalski";
            dct2.OfficeNumber = 200;
            ListDoctor = new List<ClassDoctor>() { dct1, dct2 };
            #endregion

            #region FixedTerms
            ClassFixedTerms ft1 = new ClassFixedTerms();
            ft1.Day = 2;
            ft1.Start = new TimeSpan(8, 0, 0);
            ft1.End = new TimeSpan(12, 0, 0);
            ft1.DoctorId = 1;

            ClassFixedTerms ft2 = new ClassFixedTerms();
            ft2.Day = 3;
            ft2.Start = new TimeSpan(13, 0, 0);
            ft2.End = new TimeSpan(15, 0, 0);
            ft2.DoctorId = 2;
            ListFixedTerms = new List<ClassFixedTerms>() { ft1, ft2 };
            #endregion

            #region Calendar
            ClassCalendar c1 = new ClassCalendar();
            c1.Year = 2000;
            c1.Month = 2;
            c1.CalendarId = 1;
            c1.Status = new ClassStatus();
            c1.Status.StatusId = 1;
            ListCalendar.Add(c1);

            ClassCalendar c2 = new ClassCalendar();
            c2.Year = 2000;
            c2.Month = 3;
            c2.CalendarId = 2;
            c2.Status = new ClassStatus();
            c2.Status.StatusId = 3;
            ListCalendar.Add(c2);

            ClassCalendar c3 = new ClassCalendar();
            c3.Year = 2000;
            c3.Month = 4;
            c3.CalendarId = 3;
            c3.Status = new ClassStatus();
            c3.Status.StatusId = 1;
            ListCalendar.Add(c3);

            #endregion

            #region Calendar Doctor
            ClassCalendarDoctor cd1 = new ClassCalendarDoctor();
            cd1.Calendar = new ClassCalendar();
            cd1.Calendar.CalendarId = 2;
            cd1.CalendarDoctorId = 1;
            cd1.Doctor = new ClassDoctor();
            cd1.Doctor.Doctor_id = 1;
            cd1.Status = new ClassStatus();
            cd1.Status.StatusId = 7;
            ListCalendarDoctor.Add(cd1);

            ClassCalendarDoctor cd2 = new ClassCalendarDoctor();
            cd2.Calendar = new ClassCalendar();
            cd2.Calendar.CalendarId = 2;
            cd2.CalendarDoctorId = 2;
            cd2.Doctor = new ClassDoctor();
            cd2.Doctor.Doctor_id = 2;
            cd2.Status = new ClassStatus();
            cd2.Status.StatusId = 7;
            ListCalendarDoctor.Add(cd2);
            #endregion

            #region Calendar Day
            ClassCalendarDay d1 = new ClassCalendarDay();
            d1.Calendar = new ClassCalendar();
            d1.Calendar.CalendarId = 1;
            d1.CalendarDayId = 1;
            d1.Day = 1;
            d1.StartTime = new TimeSpan(7, 0, 0);
            d1.EndTime = new TimeSpan(20, 0, 0);
            ListCalendarDay.Add(d1);

            ClassCalendarDay d2 = new ClassCalendarDay();
            d2.Calendar = new ClassCalendar();
            d2.Calendar.CalendarId = 1;
            d2.CalendarDayId = 2;
            d2.Day = 2;
            d2.StartTime = new TimeSpan(7, 0, 0);
            d2.EndTime = new TimeSpan(20, 0, 0);
            ListCalendarDay.Add(d2);

            ClassCalendarDay d3 = new ClassCalendarDay();
            d3.Calendar = new ClassCalendar();
            d3.Calendar.CalendarId = 2;
            d3.CalendarDayId = 3;
            d3.Day = 1;
            d3.StartTime = new TimeSpan(7, 0, 0);
            d3.EndTime = new TimeSpan(20, 0, 0);
            ListCalendarDay.Add(d3);

            ClassCalendarDay d4 = new ClassCalendarDay();
            d4.Calendar = new ClassCalendar();
            d4.Calendar.CalendarId = 2;
            d4.CalendarDayId = 4;
            d4.Day = 7;
            d4.StartTime = new TimeSpan(7, 0, 0);
            d4.EndTime = new TimeSpan(20, 0, 0);
            ListCalendarDay.Add(d4);

            #endregion

            #region Term
            ClassTerm t1 = new ClassTerm();
            t1.TermId = 1;
            t1.CalendarDay = new ClassCalendarDay();
            t1.CalendarDay = d3;
            t1.CalendarDoctor = cd1;
            t1.Date = new DateTime(2000, 3, 1);
            t1.Doctor = dct1;
            t1.StartTime = new TimeSpan(8, 0, 0);
            t1.EndTime = new TimeSpan(12, 0, 0);
            t1.Office = new ClassOffice();
            t1.Office.OfficeNumber = 100;
            ListTerm.Add(t1);

            ClassTerm t2 = new ClassTerm();
            t2.TermId = 2;
            t2.CalendarDay = new ClassCalendarDay();
            t2.CalendarDay = d4;
            t2.CalendarDoctor = cd2;
            t2.Date = new DateTime(2000, 3, 7);
            t2.Doctor = dct2;
            t2.StartTime = new TimeSpan(13, 0, 0);
            t2.EndTime = new TimeSpan(15, 0, 0);
            t2.Office = new ClassOffice();
            t2.Office.OfficeNumber = 200;
            ListTerm.Add(t2);
            #endregion
        }

        #region Variables
        int NextCalendarId = 4;
        int NextCalendarDayId = 5;
        int NextCalendarDoctorId = 3;
        int NextTermId = 3;
        private List<ClassCalendar> ListCalendar = new List<ClassCalendar>();
        private List<ClassCalendarDay> ListCalendarDay = new List<ClassCalendarDay>();
        private List<ClassCalendarDoctor> ListCalendarDoctor = new List<ClassCalendarDoctor>();
        private List<ClassStatus> ListStatus;
        private List<ClassTerm> ListTerm = new List<ClassTerm>();
        private List<ClassDoctor> ListDoctor;
        private List<ClassFixedTerms> ListFixedTerms;
        private List<ClassOffice> ListOffice;
        #endregion

        public List<ClassCalendar> CalendarList()
        {
            return ListCalendar;
        }

        public int CreateCalendar(int Year, int Month)
        {
            ClassCalendar calendar = new ClassCalendar();
            calendar.CalendarId = NextCalendarId;
            NextCalendarId++;
            calendar.Month = Month;
            calendar.Year = Year;
            calendar.Status = new ClassStatus();
            calendar.Status.Status = "New";
            calendar.Status.StatusId = 1;
            ListCalendar.Add(calendar);
            return calendar.CalendarId;
        }

        public void CreateCalendarDays(string days)
        {
            //String.Format("({0},'{1}','{2}',{3}),", day, new TimeSpan(7, 0, 0), new TimeSpan(20, 0, 0), calendarId)
            string[] pom = days.Split(")");
            foreach (var item in pom)
            {
                if (item.Length < 5) break;
                string[] table = item.Split(",");
                int x = 0;
                if (table.Length > 4) x++;

                ClassCalendarDay day = new ClassCalendarDay();
                day.Day = int.Parse((table[x].Split("("))[1]);
                day.StartTime = TimeSpan.Parse(table[x+1].Split("'")[1]);
                day.EndTime = TimeSpan.Parse(table[x+2].Split("'")[1]);
                day.Calendar = new ClassCalendar();
                day.Calendar.CalendarId = int.Parse(table[x+3]);
                day.CalendarDayId = NextCalendarDayId;
                NextCalendarDayId++;
                ListCalendarDay.Add(day);
            }
        }

        public List<ClassCalendarDay> ListOfCalendarDays(int calendarId)
        {
            List<ClassCalendarDay> list = new List<ClassCalendarDay>();
            foreach (var item in ListCalendarDay)
            {
                if (item.Calendar.CalendarId == calendarId) list.Add(item);
            }
            return list;
        }

        public List<ClassStatus> StatusList()
        {
            return ListStatus;
        }

        public int CreateCalendarDoctor(int Doctor_id, int Calendar_id)
        {
            ClassCalendarDoctor calendarDoctor = new ClassCalendarDoctor();
            calendarDoctor.Calendar = new ClassCalendar();
            calendarDoctor.Calendar.CalendarId = Calendar_id;
            calendarDoctor.CalendarDoctorId = NextCalendarDoctorId;
            NextCalendarDoctorId++;
            calendarDoctor.Doctor = new ClassDoctor();
            calendarDoctor.Doctor.Doctor_id = Doctor_id;
            calendarDoctor.Status = new ClassStatus();
            calendarDoctor.Status.StatusId = 6;
            ListCalendarDoctor.Add(calendarDoctor);
            return calendarDoctor.CalendarDoctorId;
        }

        public void CreateTerms(string text)
        {
            string[] pom = text.Split(")");
            foreach (var item in pom)
            {
                if (item.Length < 5) break;
                string[] table = item.Split(",");
                int x = 0;
                if (table.Length > 7) x++;
                ClassTerm term = new ClassTerm();
                term.StartTime = TimeSpan.Parse(table[x].Split("'")[1]);
                term.EndTime = TimeSpan.Parse(table[x + 1].Split("'")[1]);
                string[] date = table[x + 2].Split("'")[1].Split("-");
                term.Date = new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                term.CalendarDoctor = new ClassCalendarDoctor();
                term.CalendarDoctor.CalendarDoctorId = int.Parse(table[x + 3]);
                term.CalendarDay = new ClassCalendarDay();
                term.CalendarDay.CalendarDayId = int.Parse(table[x + 4]);
                term.Office = new ClassOffice();
                term.Office.OfficeNumber = int.Parse(table[x + 5]);
                term.Doctor = new Przychodnia.Class.DictionariesHanding.ClassDoctor();
                term.Doctor.Doctor_id = int.Parse(table[x + 6]);
                term.TermId = NextTermId;
                NextTermId++;
                ListTerm.Add(term);
            }
        }

        public List<ClassDoctor> DoctorList()
        {
            return ListDoctor;
        }

        public List<ClassFixedTerms> ListFixedTermsForSpecifiedDoctor(int doctorId)
        {
            List<ClassFixedTerms> list = new List<ClassFixedTerms>();
            foreach (var item in ListFixedTerms)
            {
                if (item.DoctorId == doctorId) list.Add(item);
            }
            return list;
        }

        public int SelectCalendarDayId(int Day, int Calendar_id)
        {
            foreach (var item in ListCalendarDay)
            {
                if (item.Day == Day && item.Calendar.CalendarId == Calendar_id) return item.CalendarDayId;
            }
            return 0;
        }

        public int SelectStatusId(EnumStatus enumstatus)
        {
            foreach (var item in ListStatus)
            {
                if (ClassStatus.StatusString(enumstatus) == item.Status) return item.StatusId;
            }
            return 0;
        }

        public void UpdateCalendarStatus(int Status_id, int Calendar_id)
        {
            foreach (var item in ListCalendar)
            {
                if (item.CalendarId == Calendar_id) item.Status.StatusId = Status_id;
            }
        }

        public List<ClassTerm> TermsForSpecifiedYearAndMonth(int year, int month)
        {
            List<ClassTerm> list = new List<ClassTerm>();
            foreach (var term in ListTerm)
            {
                if (term.Date.Year == year && term.Date.Month == month) list.Add(term);
            }
            return list;
        }

        public List<ClassTerm> TermLisTSelectedDoctor(int Calendar_id, int Doctor_id)
        {
            List<ClassTerm> list = new List<ClassTerm>();
            foreach (var term in ListTerm)
            {
                if (term.Doctor.Doctor_id == Doctor_id) list.Add(term);
            }
            return list;
        }

        public List<ClassCalendarDoctor> ListOfCalendarDoctor(int calendarId)
        {
            return ListCalendarDoctor;
        }

        public List<ClassTerm> ListOfTerms()
        {
            return ListTerm;
        }

        public int SelectCalendarId(int year, int month)
        {
            foreach (var item in ListCalendar)
            {
                if (item.Year == year && item.Month == month) return item.CalendarId;
            }
            return 0;
        }

        public void UpdateOffice(int termid, int officeId)
        {
            foreach (var item in ListTerm)
            {
                if(item.TermId == termid)
                {
                    item.Office.OfficeNumber = officeId;
                }
            }
        }

        public void DeleteTerm(int TermId)
        {
            List<ClassTerm> list = new List<ClassTerm>();
            foreach (var item in ListTerm)
            {
                if (item.TermId != TermId) list.Add(item);
            }
            ListTerm = list;
        }

        public void DeleteCalendarDay(int calendarId)
        {
            List<ClassCalendarDay> list = new List<ClassCalendarDay>();
            foreach (var item in ListCalendarDay)
            {
                if (item.CalendarDayId != calendarId) list.Add(item);
            }
            ListCalendarDay = list;
        }

        public void DeleteCalendarAndApropriateCalendarDays(int Calendar_id)
        {
            List<ClassCalendarDay> list = new List<ClassCalendarDay>();
            foreach (var item in ListCalendarDay)
            {
                if (item.Calendar.CalendarId != Calendar_id) list.Add(item);
            }
            ListCalendarDay = list;

            List<ClassCalendar> list2 = new List<ClassCalendar>();
            foreach (var item in ListCalendar)
            {
                if (item.CalendarId != Calendar_id) list2.Add(item);
            }
            ListCalendar = list2;
        }
    }
}
