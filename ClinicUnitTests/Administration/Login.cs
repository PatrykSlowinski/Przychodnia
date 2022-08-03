using System;
using Xunit;
using Przychodnia.Class.Login;
using Przychodnia.Class.DictionariesHanding;
using System.Collections.Generic;
using System.Linq;

namespace ClinicUnitTests.Administration
{
    public class Login
    {
        [Theory]
        //Less than 8
        [InlineData("Pas1d!#", false)]
        //More than 15
        [InlineData("Dfh1*fhtushtajsdk", false)]
        //Without number
        [InlineData("aDJNjfg!g", false)]
        //Without special
        [InlineData("Adfghd112", false)]
        //without lower
        [InlineData("ADJFHFG1!", false)]
        //Without upper
        [InlineData("dgfjfhdg1!", false)]
        //Correct Data
        [InlineData("Asdfgh1!", true)]
        public void ValidatePassword(string input, bool expected)
        {
            bool actual = ClassHelpers.IsValidPassword(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        //Without @
        [InlineData("asdfgmail.com", false)]
        //More than one @
        [InlineData("asdf@gmail@com", false)]
        //Before @ less than 3 characters
        [InlineData("as@gmail.com", false)]
        //Without . after @
        [InlineData("asdf@gmailcom", false)]
        //No charatker before dot (dot after @)
        [InlineData("asdf@.com", false)]
        //No charatker after dot (dot after @)
        [InlineData("asdf@gmail.", false)]
        //Correct data
        [InlineData("asdf@gmail.com", true)]
        public void ValidateEmail(string input, bool expected)
        {
            bool actual = ClassHelpers.ValidateEmail(input);
            Assert.Equal(expected, actual);
        }

        [Theory]
        //Not only numbers
        [InlineData("1234a5343", 9,false)]
        //Invalid number length
        [InlineData("12347543", 9, false)]
        //Correct data
        [InlineData("123475434", 9, true)]
        public void IsValidNumberLenght(string input, int lenght, bool expected)
        {
            bool actual = ClassHelpers.IsValidNumberLenght(input, lenght);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestChangePassword()
        {
            SqlLogin sql = new SqlLogin();
            string login = "user2";
            string email = "user2@gmail.com";
            string newPassword = "NewPassword1!";
            sql.ChangePassword(login, email, newPassword);
            IEnumerable<ClassUser> query =
                from elem in sql.ListUser
                where elem.Login == login && elem.Email == email
                select elem;
            Assert.Equal(newPassword, query.First().Password);
        }

        [Theory]
        [InlineData("user1", "user1@gmail.com", true)]
        [InlineData("user2", "user1@gmail.com", false)]
        [InlineData("user", "user@gmail.com", false)]
        public void TestCheckLoginAndEmail(string login, string email, bool expected)
        {
            SqlLogin sql = new SqlLogin();
            Assert.Equal(expected, sql.CheckLoginAndEmail(login,email));
        }


        [Theory]
        [InlineData("user3", "3Passw3!", true)]
        [InlineData("user3", "3Passw3", false)]
        [InlineData("user", "3Passw3", false)]
        public void TsetCheckLoginDetails(string login, string password, bool expected)
        {
            SqlLogin sql = new SqlLogin();
            Assert.Equal(expected, sql.CheckLoginDetails(login, password));
        }

        [Theory]
        [InlineData("user1", "1Passw1!", "Administrator")]
        [InlineData("user2", "2Passw2!", "Office staff")]
        [InlineData("user3", "3Passw3!", "Doctor")]
        [InlineData("user3", "3Passw!", "Exception")]
        public void TestGetUserType(string login, string password, string expected)
        {
            SqlLogin sql = new SqlLogin();
            if (expected == "Exception")
            {
                Assert.Throws<ArgumentException>(() => sql.GetUserType(login, password));
                return;
            }
            Assert.Equal(expected, sql.GetUserType(login,password).Permission);
        }
    }
}
