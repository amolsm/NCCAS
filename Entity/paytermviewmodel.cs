using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class paytermviewmodel
    {
        [DisplayName("Payterm ID ")]
        public int paytermid { get; set; }

        [DisplayName("PaytermType Name ")]
        public string paytermname { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        [DisplayName("Academic Year ")]
        public string academicyear { get; set; }

        public List<sp_getPayterm_Result> _Paytermlist { get; set; }
    }
    public class receipttypeviewmodel

    {
        [DisplayName("ReceiptType ID ")]
        public int R_Id { get; set; }

        [DisplayName("ReceiptType Name ")]
        public string receiptname { get; set; }

        [DisplayName("PaytermType")]
        public int paytermid { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        public List<tbl_payterm> _PaytermList { get; set; }

        public List<sp_ReceiptType_Result> _ReceiptTypeList { get; set; }
    }
}
