using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    public class lib_authormodel
    {
        [DisplayName("Author Id ")]
        public int authorid { get; set; }

        [DisplayName("Author Name")]
        public string AuthorName { get; set; }

        [DisplayName("Active")]
        public bool status { get; set; }

        public List<sp_getAuthor_Result> _Authorlist { get; set; }
    }
}
