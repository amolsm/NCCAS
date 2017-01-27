using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class feeslabelviewmodel
    {
        [DisplayName("Fees Label ID ")]
        public int feeslblid { get; set; }

        [DisplayName("Select Control count ")]
        public int ctrlcnt { get; set; }

        [DisplayName("Select Class : ")]
        public int classid { get; set; }

        [DisplayName("Control Name ")]  
        public string ctrlnm { get; set; }

        public List<tbl_class> Classlist { get; set; }
        public List<sp_getfeeslabels_Result> _feeslabellist { get; set; }     
        
    }
}
