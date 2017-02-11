using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    public class teachersubjectmodel
    {
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Teacher Name")]
        public int teacherid { get; set; }
       
        [DisplayName("Course Name")]
        public int Classid { get; set; }

        [DisplayName("Department Name")]
        public int department { get; set; }

        [DisplayName("Year")]
        public int year { get; set; }

        [DisplayName("Subject Name")]
        public int subjectid { get; set; }

        [DisplayName("Active")]
        public bool status { get; set; }

        public List<tbl_employee> TeacherList { get; set; }
        public List<tbl_CourseMaster> Courselist { get; set; }
        public List<tbl_subject> SubjectList { get; set; }
        public List<tblDepartment> DepartmentList { get; set; }
        public List<tbl_YearMaster> YearList { get; set; }
        public List<tbl_class> Classlist { get; set; }
    }
}
