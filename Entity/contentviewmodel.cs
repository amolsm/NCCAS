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

        [DisplayName("Description")]
        public string cdescription { get; set; }

        [DisplayName("Active")]
        public bool status { get; set; }

        public List<tbl_Chapter> _chaptername { get; set; }
        public List<tbl_Content> _content { get; set; }
        public List<sp_getcontent_Result> _contentlist { get; set; }
    }
}
