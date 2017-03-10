using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class timetableviewmodel
    {
        [DisplayName("Timetable ID ")]
        public int Tid { get; set; }

        [DisplayName("Course Name ")]
        public int Classid { get; set; }

        [DisplayName("Day Order")]
        public int Day { get; set; }

        [DisplayName("Lecture No ")]
        public int LecNo { get; set; }

        [DisplayName("year")]
        public int yearid { get; set; }

        [DisplayName("Department")]
        public int deptid { get; set; }

        [DisplayName("Subject Name ")]
        public int Subjectid { get; set; }

        [DisplayName("Department")]
        public int department { get; set; }

        [DisplayName("Year")]
        public int year { get; set; }

        [DisplayName("Lecture Start Time ")]
        public string LecTime { get; set; }

        [DisplayName("Lecture End Time ")]
        public string LecETime { get; set; }

        [DisplayName("Teacher Name ")]
        public int Empid { get; set; }

        [DisplayName("Semester")]
        public string semester { get; set; }

        public List<sp_gettimetable_Result> _Timetablelist { get; set; }
        public List<tblDepartment> DepartmentList { get; set; }
        public List<tbl_YearMaster> YearList { get; set; }
        public List<sp_gettimetable_pivot_Result> _Timetablelistpivot { get; set; }
        public List<sp_gettimetableclass_Result> timetableclass { get; set; }
        public List<tbl_CourseMaster> Classlist { get; set; }

        public List<tbl_subject> subjectlist { get; set; }

        public List<sp_getteachers_Result> Emplist { get; set; }

        public List<tbl_days> Daylist { get; set; }

        public List<string> parentemails { get; set; }

        public List<sp_getteacheremail_Result> teacheremails { get; set; }        
    }
}
