using System;
using System.Collections.Generic;
using System.Text;
using Przychodnia.Class.DictionariesHanding;
using Przychodnia.Class.Login;
using System.Linq;
namespace ClinicUnitTests.Administration
{
    public class SqlLogin : ISqlLogowanie
    {
        public SqlLogin()
        {
            ClassPermission p1 = new ClassPermission();
            p1.Permission = "Administrator";
            p1.PermissionId = 1;

            ClassPermission p2 = new ClassPermission();
            p2.Permission = "Office staff";
            p2.PermissionId = 2;

            ClassPermission p3 = new ClassPermission();
            p3.Permission = "Doctor";
            p3.PermissionId = 3;
            ListPermission = new List<ClassPermission>() { p1, p2, p3 };

            ClassUser u1 = new ClassUser();
            u1.User_id = 1;
            u1.Login = "user1";
            u1.Password = "1Passw1!";
            u1.Email = "user1@gmail.com";
            u1.Permission = p1;

            ClassUser u2 = new ClassUser();
            u2.User_id = 2;
            u2.Login = "user2";
            u2.Password = "2Passw2!";
            u2.Email = "user2@gmail.com";
            u2.Permission = p2;

            ClassUser u3 = new ClassUser();
            u3.User_id = 3;
            u3.Login = "user3";
            u3.Password = "3Passw3!";
            u3.Email = "user3@gmail.com";
            u3.Permission = p3;
            ListUser = new List<ClassUser>() { u1, u2, u3 };
        }
        public List<ClassUser> ListUser;
        List<ClassPermission> ListPermission;

        public void ChangePassword(string login, string email, string newPassword)
        {
            IEnumerable<ClassUser> query =
                from elem in ListUser
                where elem.Login == login && elem.Email == email
                select elem;
            query.First().Password = newPassword;
        }

        public bool CheckLoginAndEmail(string login, string email)
        {
            IEnumerable<ClassUser> query =
                from elem in ListUser
                where elem.Login == login && elem.Email == email
                select elem;
            if (query.Count() != 1) return false;
            return true;
        }

        public bool CheckLoginDetails(string login, string password)
        {
            IEnumerable<ClassUser> query =
                from elem in ListUser
                where elem.Login == login && elem.Password == password
                select elem;
            if (query.Count() != 1) return false;
            return true;
        }

        public ClassPermission GetUserType(string login, string password)
        {
            IEnumerable<ClassUser> query =
                from elem in ListUser
                where elem.Login == login && elem.Password == password
                select elem;
            if (query.Count() != 1) throw new ArgumentException();
            return query.First().Permission;
        }
    }
}
