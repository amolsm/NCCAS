using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class transportviewmodel
    {
        [DisplayName("Bus ID ")]
        public int? busid { get; set; }

        [DisplayName("Bus Number")]
        public string busNo { get; set; }

        //[DisplayName("Select Bus Route")]
        //public string busRoute { get; set; }

        [DisplayName("Select Bus Route")]
        public int busRoute { get; set; }

        [DisplayName("Driver Name ")]
        public string busDriverNm { get; set; }

        [DisplayName("Driver Contact No ")]
        public string busDrivercontact { get; set; }

        [DisplayName("Bus RTO No. ")]
        public string busRTONo { get; set; }

        [DisplayName("Bus Time ")]
        public DateTime busDateTime { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        [DisplayName("Academic Year ")]
        public string academicyear { get; set; }

        public List<sp_gettransport_Result> _transportlist { get; set; }

        public List<tbl_route> Routelist { get; set; }
    }
}
