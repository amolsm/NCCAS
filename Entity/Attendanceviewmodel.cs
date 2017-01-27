using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class Attendanceviewmodel
    {
        [DisplayName("Class Name ")]
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
        public int LeaveTypeid { get; set; }

        [DisplayName("Leave Start Date")]
        public DateTime LeaveStartDate { get; set; }

        [DisplayName("Leave End Date")]
        public DateTime LeaveEndDate { get; set; }

        public List<tbl_employee> Employeelist { get; set; }

        public List<tbl_class> Classlist { get; set; }

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
