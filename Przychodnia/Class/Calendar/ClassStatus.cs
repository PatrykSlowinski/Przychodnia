using System;
using System.Collections.Generic;
using System.Text;



namespace Przychodnia.Class.Calendar
{
    public enum EnumStatus
    {
        New,
        SharedForDoctors,
        DuringVerification,
        Verified,
        WaitingForAdministratorAcceptance,
        DuringVerificationByTheDoctor,
        AcceptedByTheDoctor
    }
    public class ClassStatus
    {
        #region Variables
        private int statusId;



        public int StatusId
        {
            get { return statusId; }
            set { statusId = value; }
        }
        private string status;



        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion
        public static string StatusString(EnumStatus status)
        {
            string text = "";
            if (status == EnumStatus.New) text = "New";
            if (status == EnumStatus.SharedForDoctors) text = "Shared for doctors";
            if (status == EnumStatus.DuringVerification) text = "During verification";
            if (status == EnumStatus.Verified) text = "Verified";
            if (status == EnumStatus.WaitingForAdministratorAcceptance) text = "Waiting for administrator acceptance";
            if (status == EnumStatus.DuringVerificationByTheDoctor) text = "During verification by the doctor";
            if (status == EnumStatus.AcceptedByTheDoctor) text = "Accepted by the doctor";
            return text;
        }
        public override string ToString()
        {
            return Status;
        }
    }
}