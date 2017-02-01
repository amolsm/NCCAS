using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class divisionviewmodel
    {
        [DisplayName("Division ID ")]
        public int DivisionId { get; set; }

          [DisplayName("Division Name ")]
        public string DivisionName { get; set; }

       [DisplayName("Class Name ")]
       public int Classid { get; set; }

          [DisplayName("Active ")]
       public bool status { get; set; }

          public List<tbl_class> classlistdetails { get; set; }
          public List<sp_getDivision_Result> _DivisionList { get; set; }
        public List<sp_GetCoursefordevision_Result> _CourseList { get; set; }



    }
}
