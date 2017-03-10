using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
    public class lib_publishermodel
    {
        [DisplayName("Publisher Id ")]
        public int publisherid { get; set; }

        [DisplayName("Publisher Name")]
        public string PublisherName { get; set; }

        [DisplayName("Active")]
        public bool status { get; set; }

        public List<sp_getPublisher_Result> _Publisherlist { get; set; }
    }
}
