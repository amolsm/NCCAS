using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class leaveviewmodel
    {
        [DisplayName("Leave ID ")]
        public int leaveid { get; set; }

        [DisplayName("Leave Name ")]
        public string leavename { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        [DisplayName("Academic Year ")]
        public string academicyear { get; set; }

        [DisplayName("Leave Balance ")]
        public int leaveBalance { get; set; }

        public List<sp_getLeave_Result> _Leavelist { get; set; }    
    }
}
