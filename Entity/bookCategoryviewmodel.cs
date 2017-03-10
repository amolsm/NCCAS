using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    public  class bookCategoryviewmodel
    {
        [DisplayName("Category Id")]
        public int BookCategoryId { get; set; }

        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [DisplayName("Active")]
        public bool status { get; set; }

        public List<sp_getBookCategory_Result> _bookcategorylist { get; set; }
    }
}
