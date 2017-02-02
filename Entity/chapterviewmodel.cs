﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    public class chapterviewmodel
    {
        [DisplayName("Chapter ID")]
        public int chapterid { get; set; }

        [DisplayName("Subject")]
        public int subjectid { get; set; }
        
        [DisplayName("Chapter Name")]
        public string chaptername { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }

        [DisplayName("Subject")]
        public string subject { get; set; }

        [DisplayName("Active")]
        public bool status { get; set; }

        public List<tbl_subject> _subjectlist { get; set; }
        public List<sp_getchapter_Result> _chapterlists { get; set; }

       
    }
}