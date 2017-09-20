using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SchoolManagementSystems.Models
{
    public class BarCodeModel
    {
        public int Id { get; set; }
        public string CallNo  { get; set; }
        public string AccessorNo { get; set; }
        public byte[] BarcodeImage { get; set; }
        public string Barcode { get; set; }
        public string ImageUrl { get; set; }

        public string ColA { get; set; }
        public string ColB { get; set; }
        public string ColC { get; set; }
        public string ColD { get; set; }

        public string ColE { get; set; }


        public string BookName { get; set; }
       
        public List<Entity.lib_Bookentry> _bookentry { get; set; }

    
    }
}