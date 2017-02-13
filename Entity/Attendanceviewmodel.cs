using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.Mvc;

namespace Entity
{
    public class Attendanceviewmodel
    {
        [DisplayName("Course Name ")]
        public int Classid { get; set; }

        [DisplayName("Select Date ")]
        public DateTime AttendanceDate { get; set; }

        [DisplayName("Time In ")]
        public string AttendanceTimeIn { get; set; }

        [DisplayName("Time Out ")]
        public int AttendanceTimeOut { get; set; }

        [DisplayName("Reason ")]
        public string StaffReason { get; set; }

        [DisplayName("Absent ")]
        public bool StaffAbsent { get; set; }

        [DisplayName("Employee Name ")]
        public int Empid { get; set; }

        [DisplayName("Student Name ")]
        public string StudentName { get; set; }
        public int Studentid { get; set; }

        public string errormessage { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Leave Type")]
        public string LeaveTypeid { get; set; }

        [DisplayName("Year")]
        public int year { get; set; }

        [DisplayName("Department")]
        public int department { get; set; }

        [DisplayName("Division")]
        public int division { get; set; }

        [DisplayName("Subject Name")]
        public int subjectid { get; set; }

        [DisplayName("Session Name")]
        public int session { get; set; }


        [DisplayName("Leave Start Date")]
        public DateTime LeaveStartDate { get; set; }

        [DisplayName("Leave End Date")]
        public DateTime LeaveEndDate { get; set; }

        public List<tbl_subject> SubjectList { get; set; }
        public SelectList ylist { get; set; }
        public List<tbl_CourseYearMaster> tblcourse { get; set; }
        public List<tbl_employee> Employeelist { get; set; }
        public List<tbl_division> DivisionList { get; set; }
        public List<tblDepartment> DepartmentList { get; set; }
        public List<tbl_YearMaster> YearList { get; set; }
        public List<tbl_CourseMaster> Classlist { get; set; }
        public List<sp_getyear_Result> yearlist1 { get; set; }
        public List<tbl_leave> Leavelist { get; set; }
        public class presentdetails
        {
            public int StudentID { get; set; }
            public bool Present { get; set; }
            public string Reason { get; set; }
        
            public int Classid { get; set; }
            public DateTime AttendanceDate { get; set; }
            public List<tbl_class> Classlist { get; set; }
        }
        public List<presentdetails> presentrecords { get; set; }

        public List<Attendanceviewmodel> Attendance { get; set; }
     

        public List<sp_GetEmployeeLeave_Result> EmployeeLeavelist { get; set; }

        public List<sp_GetAvailableLeave_Result> AvailableLeaveList { get; set; }

        public List<sp_GetEmpLeaveDetails_Result> EmpLeaveDetail { get; set; }
    }
  
        
     
   
}
