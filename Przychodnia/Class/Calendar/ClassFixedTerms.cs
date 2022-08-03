using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.Calendar
{
    public class ClassFixedTerms
    {
        private int day;

        public int Day
        {
            get { return day; }
            set { day = value; }
        }
        private TimeSpan start;

        public TimeSpan Start
        {
            get { return start; }
            set { start = value; }
        }
        private TimeSpan end;

        public TimeSpan End
        {
            get { return end; }
            set { end = value; }
        }
        private int doctorId;

        public int DoctorId
        {
            get { return doctorId; }
            set { doctorId = value; }
        }

    }
}
