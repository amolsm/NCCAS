using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class feesviewmodel
    {
        [DisplayName("Fees ID ")]
        public int Feesid { get; set; }

        [DisplayName("Course")]
        public int CourseId { get; set; }

        [DisplayName("Department")]
        public int DeptId { get; set; }

        [DisplayName("Receipt Type")]
        public int receipttypeid { get; set; }


        [DisplayName("Library Fees ")]
        public double LFees { get; set; }

        [DisplayName("Transport Fees ")]
        public double TFees { get; set; }

        [DisplayName("Uniform Fees ")]
        public double UFees { get; set; }

        [DisplayName("Hostel Fees ")]
        public double HFees { get; set; }

        [DisplayName("Other Fees ")]
        public double OFees { get; set; }

        [DisplayName("Total Fees ")]
        public double TotalFees { get; set; }

        [DisplayName("Payment Type ")]
        public int PTypeid { get; set; }

        public List<tblReceiptType> _receipttype { get; set; }

        public List<tbl_class> Classlist { get; set; }

        public List<tbl_CourseMaster> _CourseMasterList { get; set; }

        public List<tblDepartment> _DepartmentList { get; set; }
        public List<tbl_payterm> ptypelist { get; set; }
        public List<sp_getFeessetup_Result> _Feessetuplist { get; set; }

        public List<sp_GetCoursefordevision_Result> _CourseList { get; set; }
    }
}
