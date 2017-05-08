using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
  public  class CommonUtility
    {
        public static string FormatedDateString(DateTime dateTime)
        {
            if (dateTime == SqlDateTime.MinValue.Value)
                return string.Empty;
            else
                return dateTime.ToString("dd-MM-yyyy");
        }

      


    }
}
