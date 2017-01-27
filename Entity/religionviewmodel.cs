using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class religionviewmodel
    {
        [DisplayName("Religion ID ")]
        public int Religionid { get; set; }

        [DisplayName("Religion Name ")]
        public string Religionnm { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        [DisplayName("Academic Year ")]
        public string academicyear { get; set; }

        public List<sp_getReligion_Result> _Religionlist { get; set; }    
    }
}
