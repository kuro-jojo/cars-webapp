using System.Globalization;

namespace cars_webapp.Utils
{
    public static class MoneyConvertor
    {
        public static float EuroStringToFloat(string euroString)
        {
            // Remove non-numeric characters except comma and dot
            euroString = euroString.Replace("€", "").Replace(" ", "")
                .Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)
                .Replace("\"", "");


            // Parse as float
            if (float.TryParse(euroString, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
            {
                return result;
            }
            else
            {
                throw new FormatException("Invalid Euro currency format.");
            }
        }

        public static string FloatToEuroString(float amount)
        {
            // Convert float to string with currency format
            return amount.ToString("C", CultureInfo.GetCultureInfo("fr-FR"));
        }
    }
}
