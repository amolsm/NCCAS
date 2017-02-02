using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    public class studentdivisionmodel
    {
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Student Name")]
        public int studentid { get; set; }

        [DisplayName("Division Name")]
        public int divisionid { get; set; }

        [DisplayName("Course Name")]
        public int courseid { get; set; }

        [DisplayName("Department Name")]
        public int departmentid { get; set; }

        [DisplayName("Year Name")]
        public int yearid { get; set; }

        [DisplayName("Created By")]
        public int createdby { get; set; }


        [DisplayName("Active")]
        public bool status { get; set; }

        public List<tbl_CourseYearMaster> Courselist { get; set; }
        public List<tbl_division> DivisionList { get; set; }
        public List<tbl_student> StudentList { get; set; }
        public List<tblDepartment> DepartmentList { get; set; }
        public List<tbl_YearMaster> YearList { get; set; }
        public List<tbl_class> Classlist { get; set; }
    }
}
