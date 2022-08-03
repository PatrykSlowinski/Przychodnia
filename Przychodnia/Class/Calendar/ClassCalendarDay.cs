using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.Calendar
{
    public class ClassCalendarDay
    {
        #region Variables
        private int calendarDayId;

        public int CalendarDayId
        {
            get { return calendarDayId; }
            set { calendarDayId = value; }
        }

        private int day;

        public int Day
        {
            get { return day; }
            set { day = value; }
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

        private ClassCalendar calendar;

        public ClassCalendar Calendar
        {
            get { return calendar; }
            set { calendar = value; }
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
        public string Date
        {
            get
            {
                return new DateTime(Calendar.Year, Calendar.Month, Day).ToShortDateString();
            }
        }

        #endregion
    }
}