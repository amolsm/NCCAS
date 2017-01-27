using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class casteviewmodel
    {
        [DisplayName("Caste ID ")]
        public int Casteid { get; set; }

        [DisplayName("Caste Name ")]
        public string CasteName { get; set; }

        [DisplayName("Religion Name ")]
        public int Religionid { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        [DisplayName("Academic Year ")]
        public string academicyear { get; set; }

        public List<tbl_religion> Religionlist { get; set; }
        public List<sp_getCaste_Result> _Castelist { get; set; }
    }
}
