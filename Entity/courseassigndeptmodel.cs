using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
   public class courseassigndeptmodel
    {
        [DisplayName("Course Assign ID")]
        public int Cid { get; set; }

        [DisplayName("Course")]
        public int CourseId { get; set; }

        [DisplayName("Department Name ")]
        public int Dept_Id { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        public List<tblDepartment> departmentlistdetails { get; set; }

        public List<tbl_CourseMaster> courselist { get; set; }

        public List<sp_getCourseDetails_Result> _CoursedetailsList { get; set; }

    }
}
