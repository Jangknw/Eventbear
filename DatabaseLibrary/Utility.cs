using DatabaseLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary
{
    public static class Utility
    {
        private static CultureInfo _cultureEN = new CultureInfo("en-US");
        private static CultureInfo _cultureTH = new CultureInfo("th-TH");

        public static string ToSafeString(this object obj)
        {
            return obj + "";
        }

        public static string ToNumericString(this int obj)
        {
            return obj.ToString("#,##0.00");
        }
        public static string ToNumericString(this float obj)
        {
            return obj.ToString("#,##0.00");
        }
        public static string ToNumericString(this double obj)
        {
            return obj.ToString("#,##0.00");
        }
        public static string ToNumericString(this decimal obj)
        {
            return obj.ToString("#,##0.00");
        }

        public static int ToInteger(this string obj)
        {
            int returnValue = 0;
            int.TryParse(obj, out returnValue);
            return returnValue;
        }

        public static int ToInteger(this object obj)
        {
            return obj.ToSafeString().ToInteger();
        }

        public static decimal ToDecimal(this string obj)
        {
            decimal returnValue = 0;
            decimal.TryParse(obj, out returnValue);
            return returnValue;
        }

        public static decimal ToDecimal(this object obj)
        {
            return obj.ToSafeString().ToDecimal();
        }

        public static double ToDouble(this string obj)
        {
            double returnValue = 0;
            double.TryParse(obj, out returnValue);
            return returnValue;
        }

        public static double ToDouble(this object obj)
        {
            return obj.ToSafeString().ToDouble();
        }

        public static string ToDateString(this DateTime obj)
        {
            return obj.ToString("dd-MM-yyyy");
        }

        public static DateTime ToDateTimeTH(this string obj)
        {
            DateTime returnValue;
            DateTime.TryParseExact(obj, "dd-MM-yyyy", _cultureTH, DateTimeStyles.None, out returnValue);
            return returnValue;
        }

        public static DateTime ToDateTimeERP(this object obj)
        {
            return obj.ToSafeString().ToDateTimeERP();
        }

        public static DateTime ToDateTimeERP(this string obj)
        {
            DateTime returnValue;
            DateTime.TryParseExact(obj, "dd.MM.yyyy", _cultureEN, DateTimeStyles.None, out returnValue);
            return returnValue;
        }

        public static DateTime ToDateTimeTH(this object obj)
        {
            return obj.ToSafeString().ToDateTimeTH();
        }

        public static DateTime ToDateTimeEN(this string obj)
        {
            DateTime returnValue;
            DateTime.TryParseExact(obj, "dd-MM-yyyy", _cultureEN, DateTimeStyles.None, out returnValue);
            return returnValue;
        }

        public static DateTime ToDateTimeEN(this object obj)
        {
            return obj.ToSafeString().ToDateTimeEN();
        }

        public static DateTime ToDateTimeYYYYMMDD(this string obj)
        {
            DateTime returnValue;
            DateTime.TryParseExact(obj, "yyyyMMdd", _cultureEN, DateTimeStyles.None, out returnValue);
            return returnValue;
        }

        public static DateTime ToDateTimeYYYYMMDD(this object obj)
        {
            return obj.ToSafeString().ToDateTimeYYYYMMDD();
        }

        //StringBuilder Extension naja
        public static StringBuilder AppendLineFormat(this StringBuilder strBuilder, string format, params object[] args)
        {
            return strBuilder.AppendLine(string.Format(format, args));
        }
        public static StringBuilder AppendLineFormat(this StringBuilder strBuilder, string format, object arg0)
        {
            return strBuilder.AppendLine(string.Format(format, arg0));
        }
        public static StringBuilder AppendLineFormat(this StringBuilder strBuilder, IFormatProvider provider, string format, params object[] args)
        {
            return strBuilder.AppendLine(string.Format(provider, format, args));
        }
        public static StringBuilder AppendLineFormat(this StringBuilder strBuilder, string format, object arg0, object arg1)
        {
            return strBuilder.AppendLine(string.Format(format, arg0, arg1));
        }
        public static StringBuilder AppendLineFormat(this StringBuilder strBuilder, string format, object arg0, object arg1, object arg2)
        {
            return strBuilder.AppendLine(string.Format(format, arg0, arg1, arg2));
        }

        public static bool IsNumeric(this string s)
        {
            double output;
            return double.TryParse(s, out output);
        }

        //public static string ToJSON(this object obj, int recursionDepth = 100)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();
        //    return serializer.Serialize(obj);
        //}

        public static string StripHtmlTags(this string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }

            return new string(array, 0, arrayIndex);
        }


        public static T ToEnum<T>(this object enumValue) where T : struct, IConvertible
        {
            if (enumValue == null) return default(T);

            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            if (string.IsNullOrEmpty(enumValue.ToString()))
            {
                throw new InvalidCastException(string.Format("Invalid enum value: '{0}'", enumValue));
            }

            T returnEnum = default(T);
            Enum.TryParse<T>(enumValue.ToString(), true, out returnEnum);

            return returnEnum;
        }

        public static List<DBParameter> Add(this List<DBParameter> obj, string parameterName, object parameterValue)
        {
            obj.Add(new DatabaseLibrary.DBParameter(parameterName, parameterValue));
            return obj;
        }

    }
}
