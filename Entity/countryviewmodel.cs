using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class countryviewmodel
    {
        [DisplayName("Country ID ")]
        public int Countryid { get; set; }

        [DisplayName("Country Name ")]
        public string CountryName { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        public List<sp_getCountry_Result> _Countrylist { get; set; }    
    }
}
