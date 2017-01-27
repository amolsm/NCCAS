using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class classviewmodel
    {
        [DisplayName("Class ID ")]
        public int Classid { get; set; }

        [DisplayName("Class Name ")]
        public string Classnm { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        [DisplayName("Academic Year ")]
        public List<string> academicyear { get; set; }

        public List<sp_getClass_Result> _Classlist { get; set; }    
    }
}
