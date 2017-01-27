using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class stateviewmodel
    {
        [DisplayName("State ID ")]
        public int Stateid { get; set; }

        [DisplayName("State Name ")]
        public string StateName { get; set; }

        [DisplayName("Country Name ")]
        public int Countryid { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        public List<tbl_country> countrylist { get; set; }
        public List<sp_getState_Result> _Statelist { get; set; }        
    }
}
