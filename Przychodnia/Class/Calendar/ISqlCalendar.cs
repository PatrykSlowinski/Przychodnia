using System;
using System.Collections.Generic;
using System.Text;
using Przychodnia.Class.DictionariesHanding;
namespace Przychodnia.Class.Calendar
{
    public interface ISqlCalendar
    {
        #region Lists
        public List<ClassStatus> StatusList();
        public List<ClassTerm> TermsForSpecifiedYearAndMonth(int year, int month);
        public List<ClassCalendar> CalendarList();
        public List<ClassTerm> TermLisTSelectedDoctor(int Calendar_id, int Doctor_id);
        public List<ClassCalendarDay> ListOfCalendarDays(int calendarId);
        public List<ClassCalendarDoctor> ListOfCalendarDoctor(int calendarId);
        public List<ClassTerm> ListOfTerms();
        public List<ClassFixedTerms> ListFixedTermsForSpecifiedDoctor(int doctorId);
        public List<ClassDoctor> DoctorList();
        #endregion

        #region Select
        public int SelectCalendarDayId(int Day, int Calendar_id);
        public int SelectCalendarId(int year, int month);
        public int SelectStatusId(EnumStatus enumstatus);
        #endregion

        #region Create
        public int CreateCalendar(int Year, int Month);
        public int CreateCalendarDoctor(int Doctor_id, int Calendar_id);
        public void CreateCalendarDays(string days);
        public void CreateTerms(string text);
        #endregion

        #region Update
        public void UpdateCalendarStatus(int Status_id, int Calendar_id);
        public void UpdateOffice(int termid, int officeId);
        #endregion

        #region Delete
        public void DeleteTerm(int TermId);
        public void DeleteCalendarDay(int calendarId);
        public void DeleteCalendarAndApropriateCalendarDays(int Calendar_id);
        #endregion
    }
}
