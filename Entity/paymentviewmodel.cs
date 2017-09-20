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

        [DisplayName("Course")]
        public string Course { get; set; }

        [DisplayName("Receipt Type")]
        public int receipttypeid { get; set; }

        [DisplayName("Student ID")]
        public string Studid { get; set; }

       
        [DisplayName("Scholarship(Yes/No) ")]
        public bool Scholarship { get; set; }

        [DisplayName("Scholarship Fees ")]
        public double ScholarshipFees { get; set; }

        [DisplayName("Total Amount ")]
        public double TotalFees { get; set; }

        [DisplayName("Previous Payment Amount ")]
        public double Pr_PaymentAmt { get; set; }

        [DisplayName("Fees Amount ")]
        public double FeesAmt { get; set; }

        [DisplayName("Payment Amount ")]
        public double PaidFees { get; set; }
        
        [DisplayName("Pending Fees ")]
        public double PendingFees { get; set; }

        [DisplayName("Payment Type ")]
        public int PTypeid { get; set; }

        public int PTypeid1 { get; set; }

        [DisplayName("Academic Year : ")]
        public List<string> academicyear { get; set; }

        [DisplayName("Remarks ")]
        public string Remarks { get; set; }

        [DisplayName("Student Name ")]
        public string StudentName { get; set; }


        [DisplayName("Status(Paid/Unpaid) ")]
        public bool Status { get; set; }

        public string FessStatus { get; set; }

        public int paymentid { get; set; }
        public List<tbl_class> Classlist { get; set; }

        public List<tblReceiptType> _receipttype { get; set; }
        public List<sp_GetCoursefordevision_Result> _CourseList { get; set; }
        public List<tbl_student> studlist { get; set; }
        public List<tbl_payterm> ptypelist { get; set; }
        public List<sp_getPaymentHistory_Result> _Feespaymenthistory { get; set; }
        public List<sp_getFeespaymentlist_Result> _Feespaymentlist { get; set; }
    }
}
