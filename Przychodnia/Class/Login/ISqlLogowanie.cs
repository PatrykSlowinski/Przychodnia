using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.Login
{
    public interface ISqlLogowanie
    {
        public bool CheckLoginDetails(string login, string password);
        public bool CheckLoginAndEmail(string login, string email);
        public void ChangePassword(string login, string email, string newPassword);
        public Class.DictionariesHanding.ClassPermission GetUserType(string login, string password);
    }
}
