using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class Subjectviewmodel
    {
        [DisplayName("subject ID ")]
        public int Subjectid { get; set; }

        [DisplayName("Department Name ")]
        public int Dept_Id { get; set; }
        [DisplayName("subjectcode")]
        public string subjectcode { get; set; }
        
        [DisplayName("Course Name")]
        public int Courseid { get; set; }

        [DisplayName("CreatedBy")]
        public int CreatedBy { get; set; }

        [DisplayName("Subject Name ")]
        public string SubjectNm { get; set; }

        [DisplayName("Total Marks ")]
        public float Marks { get; set; }

        [DisplayName("Passing Marks ")]
        public float Pass_Marks { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        [DisplayName("Academic Year ")]
        public List<string> academicyear { get; set; }

        public List<tbl_YearMaster> yearlist { get; set; }
        public List<tblDepartment> departmentlistdetails { get; set; }
        public List<tbl_CourseMaster> Courselist { get; set; }
        public List<sp_getSubject_Result> _Subjectlist { get; set; }
        public List<sp_GetCourseforsubject_Result> _courselist { get; set; }
        public List<sp_GetCourseforsubjectbydeptid_Result> _courselistbydeptid { get; set; }
    }
}
