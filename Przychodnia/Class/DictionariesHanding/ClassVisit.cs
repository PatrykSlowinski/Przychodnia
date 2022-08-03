using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.DictionariesHanding
{
    public class ClassVisit
    {
        private static List<ClassVisit> listOfVisits; //List of visits
        private int visitId;
        
        public static List<ClassVisit> ListOfVisits
        {
            get => listOfVisits;
            set => listOfVisits = value;
        }

        
        public int VisitId
        {
            get { return visitId; }
            set { visitId = value; }
        }
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
        private string topic;
        public string Topic
        { 
            get => topic; 
            set => topic = value; 
        }

        private int patientId;
        public int PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }
        private DateTime dateVisit;
        public DateTime DateVisit
        {
            get { return dateVisit; }
            set { dateVisit = value; }
        }
        private string toPay;

        public string ToPay
        {
            get => toPay;
            set => toPay = value;
        }
        private string patientName;
        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

        private string patientSurname;
        public string PatientSurname
        {
            get { return patientSurname; }
            set { patientSurname = value; }
        }
        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        private string personalIdentityNumber;

        public string PersonalIdentityNumber
        {
            get { return personalIdentityNumber; }
            set { personalIdentityNumber = value; }
        }
        private string doctorName;
        public string DoctorName
        {
            get { return doctorName; }
            set { doctorName = value; }
        }

        private string doctorSurname;
        public string DoctorSurname
        {
            get { return doctorSurname; }
            set { doctorSurname = value; }
        }

        private string spceialization;
        public string Specialization
        {
            get { return spceialization; }
            set { spceialization = value; }
        }
        private string degree;
        public string Degree
        {
            get { return degree; }
            set { degree = value; }
        }
        private string typeOfSpecializtion;
        public string TypeOfSpecialization
        {
            get { return typeOfSpecializtion; }
            set { typeOfSpecializtion = value; }
        }
        private int doctorId;
        public int DoctorId
        {
            get { return doctorId; }
            set { doctorId = value; }
        }
        private int officeNumber;
        public int OfficeNumber
        {
            get { return officeNumber; }
            set { officeNumber = value; }
        }

    }
}
