using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class routeviewmodel
    {
        [DisplayName("Route ID ")]  
        public int routeid { get; set; }

        [DisplayName("Route Name (From-To) ")]
        public string routename { get; set; }

        [DisplayName("Active ")]  
        public bool status { get; set; }

        [DisplayName("Academic Year ")]  
        public string academicyear { get; set; }

        public List<sp_getroute_Result> _routelist { get; set; }     
        
    }
}
