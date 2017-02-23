using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    public class BookAllocation:libBookentry
    {
        [DisplayName("Student Name")]
        public string StudentName { get; set; }

        [DisplayName("Student Id")]
        public int StudentId { get; set; }

        public List<tbl_lib_BookIssue> _BookIssueList { get; set; }
    }
}
