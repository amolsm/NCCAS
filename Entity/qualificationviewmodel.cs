using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class qualificationviewmodel
    {
        [DisplayName("Qualification ID ")]
        public int qualificationid { get; set; }

        [DisplayName("Qualification Name ")]
        public string qualificationnm { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        [DisplayName("Academic Year ")]
        public string academicyear { get; set; }

        public List<sp_getQualification_Result> _Qualificationlist { get; set; }    
    }
}
