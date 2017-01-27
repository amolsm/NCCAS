using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class StudentCategoryviewmodel
    {
        [DisplayName("Category ID ")]
        public int categoryid { get; set; }

        [DisplayName("Category Name ")]
        public string categoryname { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        public List<sp_getStudentCategory_Result> _categorylist { get; set; }   
    }
}
