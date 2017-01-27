using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class employeeviewmodel
    {
        [DisplayName("Employee ID ")]
        public int Empid { get; set; }

        [DisplayName("First Name ")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name ")]
        public string MiddleName { get; set; }

        [DisplayName("Last Name ")]
        public string LastName { get; set; }

        [DisplayName("Select State ")]
        public int Stateid { get; set; }

        [DisplayName("Select City ")]
        public int Cityid { get; set; }

        [DisplayName("Zipcode ")]
        public string Zipcode { get; set; }

        [DisplayName("Emailid ")]
        public string Emailid { get; set; }

        [DisplayName("Phone No ")]
        public string PhoneNo { get; set; }

        [DisplayName("Mobile No ")]
        public string MobileNo { get; set; }

        [DisplayName("Code ")]
        public string Code { get; set; }

        [DisplayName("Address ")]
        public string Address { get; set; }

        [DisplayName("DOB ")]
        public DateTime DOB { get; set; }

        [DisplayName("Gender ")]
        public int Gender { get; set; }

        [DisplayName("Quallification ")]
        public int Quallification { get; set; }

        [DisplayName("DateOfJoin ")]
        public DateTime DateOfJoin { get; set; }

        [DisplayName("Typeid ")]
        public int Typeid { get; set; }

        [DisplayName("Other Qualification")]
        public string OQualification { get; set; }

        public string SelectGender { get; set; }
        public List<sp_getemp_Result> _emplist { get; set; }
        public List<tbl_state> statelist { get; set; }
        public List<tbl_city> citylist { get; set; }
        public List<tbl_qualification> qualificationlist { get; set; }
    }
}
