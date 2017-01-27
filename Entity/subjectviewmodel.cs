using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class Subjectviewmodel
    {
        [DisplayName("subject ID ")]
        public int Subjectid { get; set; }

        [DisplayName("Class Name")]
        public int Classid { get; set; }

        [DisplayName("Subject Name ")]
        public string SubjectNm { get; set; }

        [DisplayName("Total Marks ")]
        public float Marks { get; set; }

        [DisplayName("Passing Marks ")]
        public float Pass_Marks { get; set; }

        [DisplayName("Active ")]
        public bool status { get; set; }

        [DisplayName("Academic Year ")]
        public List<string> academicyear { get; set; }

        public List<tbl_class> Classlist { get; set; }
        public List<sp_getSubject_Result> _Subjectlist { get; set; }
    }
}
