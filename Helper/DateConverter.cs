using System.Globalization;

namespace HamedProject02.Helper
{
    public static class DateConverter
    {
        public static string miladiToShamsi(DateTime dateTime) {
            PersianCalendar pc = new PersianCalendar();
            
            return (pc.GetYear(dateTime)+"/"+pc.GetMonth(dateTime).ToString("00")+"/"+pc.GetDayOfMonth(dateTime).ToString("00")).ToString();
        }
        public static DateTime shamsiToMiladi(string dateTime) {
            PersianCalendar pc = new PersianCalendar();
            var year= Convert.ToInt32(dateTime.Substring(0, 4));
            var month = Convert.ToInt32(dateTime.Substring(5, 2));
            var day = Convert.ToInt32(dateTime.Substring(8, 2));
            return new DateTime(year, month, day, pc);
        }

    }
}
