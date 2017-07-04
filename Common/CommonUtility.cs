using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
  public  class CommonUtility
    {
        /// <summary>
        /// Constant for sorting of date time format.
        /// </summary>
        public const string DateFormatCommaSeperator = @"dd/MM/yyyy";
        public const string DateFormatCommaSeperatoryyyy = @"yyyy/mm/dd";

        /// <summary>
        /// Constant for sorting of date time format in Employee Claim.
        /// </summary>
        public const string DateFormatYearAndMonth = @"MMM,yyyy";

        /// <summary>
        /// Constant for sorting of date time format in Employee Claim.
        /// </summary>
        public const string DateFormatMonthAndYear = @"MMMM, yyyy";

        /// <summary>
        /// Constant for date time format.
        /// </summary>
        public const string DateTimeFormatForAppendToFile = "dd-MM-yyyy hh:mm:ss:tt";

        /// <summary>
        /// Constant for date time format.
        /// </summary>
        public const string DateTimeFormat = "dd/MM/yyyy HH:mm";

        /// <summary>
        /// Purchase Requisition Prefix

        public static string FormatedDateString(DateTime dateTime)
        {
            if (dateTime == SqlDateTime.MinValue.Value)
                return string.Empty;
            else
                return dateTime.ToString("dd-MM-yyyy");
        }

        public static DateTime ConvertToDateApplicationFormat(string dateInDDMMYYYYFormat)
        {
            return DateTime.ParseExact(dateInDDMMYYYYFormat, "dd-MM-yyyy hh:mm:ss:tt", CultureInfo.InvariantCulture);
        }





    }
}
