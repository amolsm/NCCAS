using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class cityviewmodel
    {
        [DisplayName("City ID ")]
        public int Cityid { get; set; }

        [DisplayName("City Name ")]
        public string CityName { get; set; }

        [DisplayName("Country Name ")]
        public int Countryid { get; set; }

        [DisplayName("State Name ")]
        public int Stateid { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        public List<tbl_country> countrylist { get; set; }
        public List<tbl_state> statelist { get; set; }
        public List<sp_getCity_Result> _Citylist { get; set; }        
    }
}
