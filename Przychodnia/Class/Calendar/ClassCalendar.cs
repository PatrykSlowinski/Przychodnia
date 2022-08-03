using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.Calendar
{
    public class ClassCalendar
    {
        #region Variables
        private int calendarId;

        public int CalendarId
        {
            get { return calendarId; }
            set { calendarId = value; }
        }
        private int year;

        public int Year
        {
            get { return year; }
            set { year = value; }
        }
        private int month;

        public int Month
        {
            get { return month; }
            set { month = value; }
        }
        private ClassStatus status;

        public ClassStatus Status
        {
            get { return status; }
            set { status = value; }
        }


        #endregion
        public override string ToString()
        {
            return Month.ToString() + "-" + Year.ToString();
        }
    }
}
