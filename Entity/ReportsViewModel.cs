using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class ReportsViewModel
    {
        public string StartDate { get; set; }

        public string Date { get; set; }



        public string EndDate { get; set; }

        public int DepartmentId { get; set; }

        public int CourseYearId { get; set; }

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public string DepartmentName { get; set; }

        public string CourseYearName { get; set; }
        public List<tblDepartment> DepartmentList { get; set; }
        public List<tbl_YearMaster> YearList { get; set; }
        public List<tbl_CourseMaster> Courselist { get; set; }

        public string attancelistdata { get; set; }

        public List<sp_getstudent_attandance_pivot_Result> _studentreportlist { get; set; }

    }
}
