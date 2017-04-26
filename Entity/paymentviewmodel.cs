using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class paymentviewmodel
    {
        [DisplayName("Payment ID ")]
        public int Pid { get; set; }

        [DisplayName("Course Name ")]
        public int CourseId { get; set; }

        [DisplayName("Student Name ")]
        public int Studid { get; set; }

        [DisplayName("Scholarship(Yes/No) ")]
        public bool Scholarship { get; set; }

        [DisplayName("Scholarship Fees ")]
        public double ScholarshipFees { get; set; }

        [DisplayName("Total Fees ")]
        public double TotalFees { get; set; }

        [DisplayName("Paid Fees ")]
        public double PaidFees { get; set; }
        
        [DisplayName("Pending Fees ")]
        public double PendingFees { get; set; }

        [DisplayName("Payment Type ")]
        public int PTypeid { get; set; }

        public int PTypeid1 { get; set; }

        [DisplayName("Remarks ")]
        public string Remarks { get; set; }

        [DisplayName("Status(Paid/Unpaid) ")]
        public bool Status { get; set; }

        public List<tbl_class> Classlist { get; set; }

        public List<sp_GetCoursefordevision_Result> _CourseList { get; set; }
        public List<tbl_student> studlist { get; set; }
        public List<tbl_payterm> ptypelist { get; set; }
        public List<sp_getPaymentHistory_Result> _Feespaymenthistory { get; set; }
        public List<sp_getFeespaymentlist_Result> _Feespaymentlist { get; set; }
    }
}
