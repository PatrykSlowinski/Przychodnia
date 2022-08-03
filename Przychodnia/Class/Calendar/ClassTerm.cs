using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.Calendar
{
    public class ClassTerm
    {
        #region Variables
        private int termId;

        public int TermId
        {
            get { return termId; }
            set { termId = value; }
        }

        private TimeSpan startTime;

        public TimeSpan StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        private TimeSpan endTime;

        public TimeSpan EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private ClassCalendarDoctor calendarDoctor;

        public ClassCalendarDoctor CalendarDoctor
        {
            get { return calendarDoctor; }
            set { calendarDoctor = value; }
        }

        private ClassCalendarDay calendarDay;

        public ClassCalendarDay CalendarDay
        {
            get { return calendarDay; }
            set { calendarDay = value; }
        }

        private DictionariesHanding.ClassOffice office;

        public DictionariesHanding.ClassOffice Office
        {
            get { return office; }
            set { office = value; }
        }

        private DictionariesHanding.ClassDoctor doctor;

        public DictionariesHanding.ClassDoctor Doctor
        {
            get { return doctor; }
            set { doctor = value; }
        }


        public string StartTimeString
        {
            get
            {
                string text = startTime.Hours + ":";
                if (startTime.Minutes >= 10)
                {
                    text += startTime.Minutes;
                }
                else
                {
                    text += "0" + startTime.Minutes;
                }
                return text;
            }
        }
        public string EndTimeString
        {
            get
            {
                string text = EndTime.Hours + ":";
                if (EndTime.Minutes >= 10)
                {
                    text += EndTime.Minutes;
                }
                else
                {
                    text += "0" + EndTime.Minutes;
                }
                return text;
            }
        }
        public string DateString
        {
            get
            {
                return Date.ToShortDateString();
            }
        }

        #region Geters for data grid 
        public int GetOficeNumber
        {
            get { return Office.OfficeNumber; }
        }
        public string GetName
        {
            get { return Doctor.Name; }
        }
        public string GetSurname
        {
            get { return Doctor.Surname; }
        }
        public string GetTime
        {
            get { return StartTimeString + "-" + EndTimeString; }
        }
        public override string ToString()
        {
            return Date.ToShortDateString();
        }
        #endregion
        #endregion
    }
}