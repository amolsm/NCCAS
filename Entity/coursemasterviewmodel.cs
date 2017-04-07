using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
  public  class coursemasterviewmodel
    {
        [DisplayName("Course ID ")]
        public int CourseId { get; set; }

        [DisplayName("Course Name ")]
        public string CourseName { get; set; }

        [DisplayName("Course Type ")]
        public int CourseType { get; set; }

        [DisplayName("Active ")]
        public bool Status { get; set; }

       public List<sp_getCourseMaster_Result> _coursemasterlist { get; set; }
    }
}
