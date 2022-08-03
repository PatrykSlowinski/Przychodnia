using System;
using System.Collections.Generic;
using System.Text;
using Przychodnia.Class.DictionariesHanding;

namespace Przychodnia.Class.Calendar
{
    public class ClassOverlappingCalendar
    {
        private int termId;

        public int TermId
        {
            get { return termId; }
            set { termId = value; }
        }
        private int doctorID;

        public int DoctorID
        {
            get { return doctorID; }
            set { doctorID = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string surname;

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        private int officeId;

        public int OfficeId
        {
            get { return officeId; }
            set { officeId = value; }
        }
        private int officeNumber;

        public int OfficeNumber
        {
            get { return officeNumber; }
            set { officeNumber = value; }
        }
        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string DateString
        {
            get { return Date.ToShortDateString(); }
        }
        private int calendarDayId;

        public int CalendarDayId
        {
            get { return calendarDayId; }
            set { calendarDayId = value; }
        }

        private int newOfficeNumber;

        public int NewOfficeNumber
        {
            get { return newOfficeNumber; }
            set { newOfficeNumber = value; }
        }
        public string NewOfficeNumberString
        {
            get {
                if(NewOfficeNumber==0) return "Not selected";
                return NewOfficeNumber.ToString(); 
            }
        }
        private int newOfficeId;
        public int NewOfficeId
        {
            get { return newOfficeId; }
            set { newOfficeId = value; }
        }
    }
}
