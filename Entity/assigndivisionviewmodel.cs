using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    class assigndivisionviewmodel
    {
        [DisplayName("Select Role")]
        public int studentid { get; set; }

        [DisplayName("Class Name")]
        public int Classid { get; set; }

        [DisplayName("Division Name")]
        public int DivisionId { get; set; }

        [DisplayName("Enter Message")]
        public string msg { get; set; }

        public List<tbl_student> _Studlist { get; set; }
        public List<tbl_class> _Classlist { get; set; }
        public List<tbl_AssignDivision> _AssignDivisonList { get; set; }
        public List<tbl_division> _Divisionlist { get; set; }
    }
}
