using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class bloodgroupviewmodel
    {
        [DisplayName("Blood Group ID ")]
        public int bloodgroupid { get; set; }

        [DisplayName("Blood Group Name ")]
        public string bloodgroupnm { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        [DisplayName("Academic Year ")]
        public string academicyear { get; set; }

        public List<sp_getBloodgroup_Result> _bloodgrouplist { get; set; }    
    }
}
