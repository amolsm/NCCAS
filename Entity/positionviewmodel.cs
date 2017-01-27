using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Entity
{
    public class positionviewmodel
    {
        [DisplayName("Designation ID ")]  
        public int posid { get; set; }

        [DisplayName("Designation Name ")]  
        public string posname { get; set; }

        [DisplayName("Active ")]  
        public bool status { get; set; }

        [DisplayName("Academic Year ")]  
        public string academicyear { get; set; }

        public List<sp_getposition_Result> _positionlist { get; set; }     
        
    }
}
