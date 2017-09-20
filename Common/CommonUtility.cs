using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Reflection;
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



        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

       

    }
    public class FeesChildList
    {
        public int Fchildid { set; get; }
        public string FeesNm { set; get; }
        public decimal Fees { set; get; }
    }
}
