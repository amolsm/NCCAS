using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Userviewmodel
    {
        [DisplayName("User ID ")]
        public int Userid { get; set; }

        [DisplayName("UserName ")]
        public string UserName { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DisplayName("Password ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Type ")]
        public int Type { get; set; }

        [DisplayName("ID ")]
        public int Genid { get; set; }

        [DisplayName("Status ")]
        public int Status { get; set; }

        [DisplayName("Remark ")]
        public string academicyear { get; set;}  

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DisplayName("Old Password ")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DisplayName("New Password ")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DisplayName("Confirm New Password ")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

        public List<tbl_user> Userdatacollection { get; set; }
        public List<tbl_user> Newsdatacollection { get; set; }
        public List<sp_changepassword_Result> checkpassword { get; set; }
        public List<sp_getUser_Result> _Userlist { get; set; }  

    }
}
