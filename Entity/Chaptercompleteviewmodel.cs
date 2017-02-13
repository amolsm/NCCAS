using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Mvc;

namespace Entity
{
    public class Chaptercompleteviewmodel
    {
        [DisplayName("ID ")]
        public int ID { get; set; }

        [DisplayName("Teacher Name ")]
        public int Teacherid { get; set; }

        [DisplayName("Subject ")]
        public int Subjectid { get; set; }

        [DisplayName("Department ")]
        public int Departmentid { get; set; }

        [DisplayName("Course ")]
        public int CourseId { get; set; }

        [DisplayName("Year ")]
        public int YearId { get; set; }

        [DisplayName("Chapter ")]
        public int Chapter_id { get; set; }

        [DisplayName("Complete ")]
        public bool complete { get; set; }

        [DisplayName("CompleteDate")]
        public DateTime CompleteDate { get; set; }
        public List<tbl_class> classlistdetails { get; set; }

        public List<tbl_employee> Teacherlist { get; set; }
        public List<tblDepartment> Departmentlist { get; set; }
        public List<tbl_subject> Subjectlist { get; set; }
        public List<tbl_Chapter> Chapterlist { get; set; }
        public List<tbl_YearMaster> Yearlist { get; set; }
        public List<tbl_CourseMaster> Courselist { get; set; }
    }
}
