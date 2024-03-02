using System.Globalization;

namespace cars_webapp.Utils
{
    public static class DateConvertor
    {
        public static DateTime? DateStringToDateTime(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
            {
                return null;
            }
            return DateTime.ParseExact(dateString, "dd MMMM yyyy", CultureInfo.GetCultureInfo("fr-FR"));
        }

        public static string DateTimeToString(DateTime? date)
        {
            if (!date.HasValue)
            {
                return string.Empty;
            }

            return date.Value.ToString("dd MMMM yyyy", CultureInfo.GetCultureInfo("fr-FR"));
        }

        public static string DateTimeToStringHtml(DateTime? date)
        {
            if (!date.HasValue)
            {
                return string.Empty;
            }
            return date.Value.ToString("yyyy-MM-yy", CultureInfo.GetCultureInfo("fr-FR"));
        }
    }
}
