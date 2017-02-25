using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    public class lib_vendormodel
    {
        [DisplayName("Vendor Id ")]
        public int venderId { get; set; }

        [DisplayName("Vender Name")]
        public string Vendername { get; set; }

        [DisplayName("Active")]
        public bool status { get; set; }
  
        public List<sp_getVendor_Result> _Vendorlist { get; set; }
    }
}
