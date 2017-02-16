using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    public class contentviewmodel
    {
        [DisplayName("Content ID")]
        public int contentid { get; set; }

        [DisplayName("Content Name")]
        public string contentname{ get; set; }

        [DisplayName("Chapter Name")]
        public string chaptername { get; set; }

        [DisplayName("Chapter ID")]
        public int chapterid { get; set; }

        [DisplayName("Teacher Name")]
        public int teacherid { get; set; }

        [DisplayName("Subject Name")]
        public int subjectid { get; set; }

        [DisplayName("Description")]
        public string cdescription { get; set; }

        [DisplayName("Active")]
        public bool status { get; set; }

        [DisplayName("Department")]
        public int department { get; set; }

        [DisplayName("Year")]
        public int year { get; set; }

        [DisplayName("Createdt By")]
        public int createdby { get; set; }

        [DisplayName("Course Name")]
        public int Classid { get; set; }


        public List<tblDepartment> DepartmentList { get; set; }
        public List<tbl_YearMaster> YearList { get; set; }
        public List<tbl_CourseMaster> Classlist { get; set; }
        public List<tbl_subject> _subjectlist { get; set; }
        public List<sp_getchapter_Result> _chapterlists { get; set; }
        public List<tbl_Chapter> _chaptername { get; set; }
        public List<tbl_Content> _content { get; set; }
        public List<sp_getcontent_Result> _contentlist { get; set; }
    }
}
