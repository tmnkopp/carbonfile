using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace CarbonDash.Core 
{
    public static class DateUtils
    {
        public static DateTime dbmindate = Convert.ToDateTime("1/1/1900");
        public static string GetMonthName(int month, bool abbreviate, IFormatProvider provider)
        {
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(provider);
            if (abbreviate) return info.GetAbbreviatedMonthName(month);
            return info.GetMonthName(month);
        }

        public static string GetMonthName(int month, bool abbreviate)
        {
            return GetMonthName(month, abbreviate, null);
        }

        public static string GetMonthName(int month, IFormatProvider provider)
        {
            return GetMonthName(month, false, provider);
        }

        public static string GetMonthName(int month)
        {
            return GetMonthName(month, false, null);
        }
        public static bool IsDate(string date)
        { 
            try
            { 
                DateTime dt = DateTime.Parse(date); 
                return true;
            } 
            catch
            { 
                return false; 
            } 
        }
        public static bool IsDate(DateTime date)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
