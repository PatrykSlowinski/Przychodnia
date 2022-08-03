using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.DictionariesHanding
{
    public class ClassUser
    {
        
        private int user_id;
        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }

        private ClassPermission permission;

        private string login;
        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        private string password;



        public string Password
        {
            get { return password; }
            set { password = value; }
        }



        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public override string ToString()
        {
            return Login + " | " + Email;
        }



        public ClassPermission Permission { get => permission; set => permission = value; }
    }
}
