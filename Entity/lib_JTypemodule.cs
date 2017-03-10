using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
  public  class lib_JTypemodule
    {
        [DisplayName("Journal Type Id ")]
        public int jTypeId { get; set; }

        [DisplayName("Journal Type Name")]
        public string JTypeName { get; set; }

        [DisplayName("Active")]
        public bool status { get; set; }

        public List<sp_getJType_Result> _JTypelist { get; set; }
    }
}
