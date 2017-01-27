﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class smsviewmodel
    {
        [DisplayName("Select Role")]
        public int Roleid { get; set; }

        [DisplayName("Class Name")]
        public int Classid { get; set; }

        [DisplayName("Division Name")]
        public int DivisionId { get; set; }

        public bool Checked { get; set; }

        [DisplayName("Division Name ")]
        public string DivisionName { get; set; }

        [DisplayName("Class Name ")]
        public string Classnm { get; set; }


        [DisplayName("Enter Message")]
        public string msg { get; set; }

        public List<tbl_student> _Studlist { get; set; }
        public List<tbl_class> _Classlist { get; set; }
        public List<tbl_employee> _Emplist { get; set; }
        public List<tbl_division> _Divisionlist { get; set; }
    }

}
