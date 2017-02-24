using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    public class divisionmodel
    {
        [DisplayName("Course")]
        public int courseid { get; set; }

        [DisplayName("Year")]
        public int yearid { get; set; }

        [DisplayName("Department")]
        public int department { get; set; }

        [DisplayName("Division Name")]
        public int DivisionId { get; set; }

        [DisplayName("Division Name ")]
        public string DivisionName { get; set; }

        [DisplayName("Enter Message")]
        public string msg { get; set; }

        public List<tblDepartment> DepartmentList { get; set; }
        public List<tbl_CourseMaster> Courselist { get; set; }
        public List<tbl_division> _Divisionlist { get; set; }
        public List<tbl_YearMaster> YearList { get; set; }
    }
}
