using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Entity
{
   public class Configurationtblmodelview
    {
        [DisplayName("Config Id")]
        public int CID { get; set; }

        [DisplayName("Config Name")]
        public string CONFIGNAME { get; set; }

        [DisplayName("Config Key")]
        public string CONFIGKEY { get; set; }

        [DisplayName("Config Value")]
        public string CONFIGVALUE { get; set; }

        [DisplayName("IsActive")]
        public bool ISACTIVE { get; set; }

        [DisplayName("Created By")]
        public int CREATEDBY { get; set; }

        public List<CONFIGMASTER> _configlist { get; set; }


    }
}
