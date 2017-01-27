using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
   public class departmentviewmodel
    {
        [DisplayName("Department ID ")]
        public int Dept_id { get; set; }

        [DisplayName("Department Name ")]
        public string Dept_name { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        public List<sp_getDepartment_Result> _Departmentlist { get; set; }

    }
}
