using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cars_webapp.Utils
{
    public class VINGenerator
    {
        private static readonly Random random = new Random();

        public static string GenerateVIN()
        {
            // VIN format: 17 characters (alphanumeric)
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] vinArray = new char[17];

            // The first 3 characters are fixed according to ISO 3779
            vinArray[0] = '1'; // Country of Origin
            vinArray[1] = 'G'; // Manufacturer
            vinArray[2] = '0'; // Vehicle Type

            // The next 14 characters are randomly generated alphanumeric characters
            for (int i = 3; i < 17; i++)
            {
                vinArray[i] = chars[random.Next(chars.Length)];
            }

            return new string(vinArray);
        }
    }

}