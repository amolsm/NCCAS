using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
   public class CourseYearviewmodel
    {
        [DisplayName("Course Year Id ")]
        public int Id { get; set; }

        [DisplayName("Department")]
        public int DeptId { get; set; }

  

        [DisplayName("Course")]
        public int CourseId { get; set; }
        [DisplayName("Year")]
        public int academicyear { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        public List<tblDepartment> deptlist { get; set; }

        public List<tbl_CourseMaster> courselist { get; set; }

        public List<tbl_YearMaster> yearlist { get; set; }

       public List<sp_getCourseYear_Result> _courseyear { get; set; }

    }
}
