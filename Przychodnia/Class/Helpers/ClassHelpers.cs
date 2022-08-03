using System;
using System.Collections.Generic;
using System.Text;

namespace Przychodnia.Class.Login
{
    public static class ClassHelpers
    {
        //Check if email ii valid
        public static bool ValidateEmail(string email)
        {
            //Check if email don't contains "@"
            if (!email.Contains("@")) 
            { 
                return false;
            }
            string[] pom = email.Split("@");
            //Check if there's more than two "@"
            if (pom.Length > 2) 
            { 
                return false; 
            }
            
            //Check if length before '@' is less than 3
            if (pom[0].Length < 3) 
            { 
                return false; 
            }
            //Check if after '@' is '.'
            if (!pom[1].Contains('.')) 
            { 
                return false; 
            }
            //Check if after and before '.' is at least one character
            string[] x = pom[1].Split('.');
            foreach (string y in x)
            {
                if (y.Length == 0) 
                { 
                    return false; 
                }
            }
            return true;
        }
        //Check if password is valid have at least one uppercase letter, lowercase letter, one number and one special chararacter
        public static bool IsValidPassword(string password)
        {
            int numberOfDigits = 0;
            int numberOfLowers = 0;
            int numberOfUppers = 0;
            int numberOfSpecial = 0;
            char[] charArray = password.ToCharArray();
            //Check if password lenght is more than 7 and less then 16
            if (password.Length > 7 && password.Length < 16)
            {
                //Count valid characters
                foreach (char ch in charArray)
                {
                    if (char.IsDigit(ch))
                    {
                        numberOfDigits += 1;
                    }
                    if (char.IsLower(ch))
                    {
                        numberOfLowers += 1;
                    }
                    if (char.IsUpper(ch))
                    {
                        numberOfUppers += 1;
                    }
                    if (!char.IsLetterOrDigit(ch))
                    {
                        numberOfSpecial += 1;
                    }
                }
                //Check if password have at least one uppercase letter, lowercase letter, one number and one special chararacter.
                if (numberOfDigits >= 1 && numberOfLowers >= 1 && numberOfSpecial >= 1 && numberOfUppers >= 1)
                {
                    return true;
                }
            }
            return false;
        }

        //Method that check if string contains only numbers and has specified lenght
        public static bool IsValidNumberLenght(string number, int lenghtOfnumber)
        {
            char[] numberdigits = number.ToCharArray();
            int numberofletters = 0;
            if (number.Length == lenghtOfnumber)
            {
                foreach (char digit in numberdigits)
                {
                    if (!char.IsDigit(digit))
                    {
                        numberofletters += 1;
                    }
                }
                if (numberofletters == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static string RemoveLastCharOfString(string text)
        {
            if (text.Length == 0) return text;
            int i = text.Length - 1;
            string modifiedText = text.Remove(i);
            return modifiedText;
        }

    }
}
