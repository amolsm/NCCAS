using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class paytermviewmodel
    {
        [DisplayName("Payterm ID ")]
        public int paytermid { get; set; }

        [DisplayName("Payment Type Name ")]
        public string paytermname { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        [DisplayName("Academic Year ")]
        public string academicyear { get; set; }

        public List<sp_getPayterm_Result> _Paytermlist { get; set; }    
    }
}
