using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
   public class libBookentry
    {
        public int bookid { get; set; }
        public string booktitle { get; set; }
        public string CallNo { get; set; }
        public string Volume { get; set; }

        public string SerielNumber { get; set; }
        public int Authorid { get; set; }

        public string Authorname { get; set; }


        public int PublishedByid { get; set; }
        public string PublishedByName { get; set; }

        public string Edition { get; set; }

        public int Vendorid { get; set; }

        public string Vendorname { get; set; }

        public DateTime Dateofpurchase { get; set; }

        public string BillNo { get; set; }

        public double Cost { get; set; }

        public int lib_Jid { get; set; }

        public int LibType { get; set; }

        public int Department { get; set; }

        public string JournalTitle { get; set; }

        public string LibJVolume { get; set; }

        public string Number { get; set; }

        public DateTime IssueDate { get; set; }

        public int VendorId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string JBillNo { get; set; }

        public List<lib_Bookentry> Bookentrycollection { get; set; }

        public List<lib_Journal> Journalentrydetails { get; set; }
        public List<sp_getBookentry_Result> _bookentrylist { get; set; }
    }
}


