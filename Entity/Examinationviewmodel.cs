using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Examinationviewmodel
    {
        public int Examid { get; set; }

        [DisplayName("Class Name ")]
        public int Classid { get; set; }

        [DisplayName("Subject Name ")]
        public int Subjectid { get; set; }

        [DisplayName("Examination Type ")]
        public int ExaminationTypeid { get; set; }

        [DisplayName("Examination Name ")]
        public string ExaminationName { get; set; }

        [DisplayName("Select Exam Date ")]
        //[DataType(DataType.Date)]
        public DateTime ExamDate { get; set; }

        [DisplayName("Exam Start Time ")]
        public string ExamStartTime { get; set; }

        [DisplayName("Exam End Time ")]
        public string ExamEndTime { get; set; }

        [DisplayName("Exam Year ")]
        public List<string> ExamYear { get; set; }

        public List<tbl_class> Classlist { get; set; }

        public List<tbl_ExaminationType> ExaminationTypeList { get; set; }

        public List<tbl_subject> SubjectList { get; set; }

        public List<ExaminationCollection> ExamCollection { get; set; }


    }

    public class MarkAllocation
    {
        public int markid { get; set; }

        [DisplayName("Student Name ")]
        public int Studentid { get; set; }

        public int Examid { get; set;}

        [DisplayName("Exam Name ")]
        public string Examname { get; set; }

        [DisplayName("Exam Year ")]
        public List<string> ExamYear { get; set; }

        [DisplayName("Marks ")]
        public decimal Marks { get; set; }

        [DisplayName("Class Name ")]
        public int Classid { get; set; }
        
        [DisplayName("Class Name : ")]
        public string Classname { get; set; }

        [DisplayName("Subject Name ")]
        public int Subjectid { get; set; }

        [DisplayName("Examination Type ")]
        public int ExaminationTypeid { get; set; }

        [DisplayName("Subject : ")]
        public string Subject { get; set; }

        [DisplayName("Examination Type : ")]
        public string ExamType { get; set; }

        [DisplayName("Total Marks ")]
        public decimal TotalMarks { get; set; }

        [DisplayName("Academic Year ")]
        public string AcademicYear { get; set; }

        [DisplayName("Remark ")]
        public bool Remark { get; set; }

        //public List<String> ExamTable { get; set; }

        public List<tbl_class> Classlist { get; set; }

        public List<tbl_ExaminationType> ExaminationTypeList { get; set; }

        public List<tbl_subject> SubjectList { get; set; }

        public List<tbl_ExamTImeTable> ExamTable { get; set; }

        public List<tbl_student> StudentDetail { get; set; }
    }

    public class ExaminationCollection
    {
        public string ExamName { get; set; }

        public DateTime ExamStartDate { get; set; }

        public DateTime ExamEndDate { get; set; }

        public string ExamType { get; set; }

        public string ExamClass { get; set; }

        public string ExamSubject { get; set; }

        public List<string> ExamYear { get; set; }
    }
}
