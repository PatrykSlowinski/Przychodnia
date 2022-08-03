using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.Calendar
{
    public class ClassAppointment
    {
        #region Variables
        private int appointmendtId;

        public int AppointmendtId
        {
            get { return appointmendtId; }
            set { appointmendtId = value; }
        }

        private TimeSpan startTime;

        public TimeSpan StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        private ClassTerm term;

        public ClassTerm Term
        {
            get { return term; }
            set { term = value; }
        }
        #endregion
    }
}
