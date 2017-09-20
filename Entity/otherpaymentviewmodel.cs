using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
   public class otherpaymentviewmodel
    {
        [DisplayName("Payment ID ")]
        public int OPid { get; set; }

        [DisplayName("Receipt Type")]
        public int receipttypeid { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Place")]
        public string Place { get; set; }

        [DisplayName("Total Amount ")]
        public double TotalFees { get; set; }

        [DisplayName("Payment Amount ")]
        public double PaymentAmt { get; set; }

     
        [DisplayName("Academic Year : ")]
        public List<string> academicyear { get; set; }

        [DisplayName("Remarks ")]
        public string Remarks { get; set; }

        public int opaymentid { get; set; }
      
        public List<sp_getOtherpaymentlist_Result> _otherpaymentList { get; set; }
        public List<tblReceiptType> _receipttype { get; set; }
      
    }
}
