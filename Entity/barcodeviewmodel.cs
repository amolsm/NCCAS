using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
  public  class barcodeviewmodel
    {
        public int Id { get; set; }
        public string CallNo { get; set; }
        public string AccessorNo { get; set; }
        public byte[] BarcodeImage { get; set; }
        public string Barcode { get; set; }
        public string ImageUrl { get; set; }

        [DisplayName("Department")]
        public int DeptId { get; set; }



        [DisplayName("Course")]
        public int CourseId { get; set; }

        [DisplayName("Category")]
        public int BookCategoryid { get; set; }

        public List<tblDepartment> _departmentlist { get; set; }
        public List<tbl_CourseMaster> courselist { get; set; }
        public List<tbl_lib_BookCategory> _bookcategorylist { get; set; }

        public List<Entity.lib_Bookentry> _bookentry { get; set; }

    }
}
