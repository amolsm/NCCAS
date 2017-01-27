using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class occupationviewmodel
    {
        [DisplayName("Occupation ID ")]
        public int occupationid { get; set; }

        [DisplayName("Occupation Name ")]
        public string occupationnm { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        [DisplayName("Academic Year ")]
        public string academicyear { get; set; }

        public List<sp_getOccupation_Result> _occupationlist { get; set; }    
    }
}
