using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class feeslabelviewmodel
    {
        [DisplayName("Fees Label ID ")]
        public int feeslblid { get; set; }

        [DisplayName("Control count ")]
        public int ctrlcnt { get; set; }

        [DisplayName("Course")]
        public int courseyearid { get; set; }

        [DisplayName("Course")]
        public int CourseId { get; set; }

        [DisplayName("Department")]
        public int DeptId { get; set; }

        [DisplayName("Control Name ")]  
        public string ctrlnm { get; set; }

        [DisplayName("Receipt Type")]
        public int receipttypeid { get; set; }

        public List<tbl_class> Classlist { get; set; }

        public List<tblReceiptType> _receipttype { get; set; }

        public List<tbl_CourseMaster> _CourseMasterList { get; set; }

        public List<tblDepartment> _DepartmentList { get; set; }

        public List<sp_getfeeslabels_Result> _feeslabellist { get; set; }
        public List<sp_GetCoursefordevision_Result> _CourseList { get; set; }

    }
}
