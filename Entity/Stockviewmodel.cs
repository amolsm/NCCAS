using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class Stockviewmodel
    {
        [DisplayName("Category")]
        public string catname { get; set; }

        [DisplayName("Product")]
        public string Product { get; set; }

        [DisplayName("Total Stock")]
        public int totalstock { get; set; }

        [DisplayName("Used Stock")]
        public int usedstock { get; set; }

        [DisplayName("Remain Stock")]
        public int remainingstock { get; set; }
        
        public List<sp_getCaste_Result> _stocklist { get; set; }
    }
}
