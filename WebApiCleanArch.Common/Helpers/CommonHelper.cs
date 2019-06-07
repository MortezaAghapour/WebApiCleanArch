using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApiCleanArch.Common.Helpers
{
    public class CommonHelper
    {
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        public static bool LessThanZero(int i)
        {
            return i <= 0;
        }




        public static bool IsAlphaNumeric(string strToCheck)
        {
            var rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
            return rg.IsMatch(strToCheck);
            /**
             *  ^ - means start of the string
             *  []* - could contain any number of characters between brackets
             *  a-zA-Z0-9 - any alphanumeric characters
            *   \s - any space characters (space/tab/etc.)
            *   , - commas
            *   $ - end of the string
            * */
        }
    }
}
