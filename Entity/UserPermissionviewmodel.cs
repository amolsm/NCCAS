using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class UserPermissionviewmodel
    {
        [DisplayName("Module Name ")]
        public int moduleid { get; set; }

        [DisplayName("Select Role ")]
        public int UserRoleid { get; set; }

        [DisplayName("Permission ")]
        public int Permissionid { get; set; }

        public List<sys_UserRole> _roleslist { get; set; }
    }
}
