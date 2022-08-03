using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.Calendar
{
    public class ClassCalendarDoctor
    {
        #region Variables
        private int calendarDoctorId;

        public int CalendarDoctorId
        {
            get { return calendarDoctorId; }
            set { calendarDoctorId = value; }
        }

        private ClassStatus status;

        public ClassStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        private DictionariesHanding.ClassDoctor doctor;

        public DictionariesHanding.ClassDoctor Doctor
        {
            get { return doctor; }
            set { doctor = value; }
        }

        private ClassCalendar calendar;

        public ClassCalendar Calendar
        {
            get { return calendar; }
            set { calendar = value; }
        }
        public string Name
        {
            get { return Doctor.Name; }
        }
        public string Surname
        {
            get { return Doctor.Surname; }
        }
        public string StatusString
        {
            get { return Status.Status; }
        }
        #endregion
    }
}
