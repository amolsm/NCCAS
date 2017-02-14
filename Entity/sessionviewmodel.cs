using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    public class sessionviewmodel
    {
         [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Session Name ")]
        public string SessionName { get; set; }

        [DisplayName("Active")]
        public bool status { get; set; }

        public List<tbl_SessionMaster> _tblsession { get; set; }    

        public List<sp_getSession_Result> _Sessionlist { get; set; }    
    }
    
}
