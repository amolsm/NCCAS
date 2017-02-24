using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Entity
{
   public class libBookentry
    {
        [DisplayName("Book Id")]
        public int bookid { get; set; }

        [DisplayName("Book Title")]
        public string booktitle { get; set; }

        [DisplayName("Call No.")]
        public string CallNo { get; set; }

        [DisplayName("Volume")]
        public string Volume { get; set; }

        [DisplayName("Seriel Number")]
        public string SerielNumber { get; set; }

        [DisplayName("Select Author")]
        public int Authorid { get; set; }


        [DisplayName("Author Name")]
        public string Authorname { get; set; }

        [DisplayName("Select Publisher")]
        public int PublishedByid { get; set; }

        [DisplayName("Publisher Name")]
        public string PublishedByName { get; set; }

        [DisplayName("Edition")]
        public string Edition { get; set; }

        [DisplayName("Select Vendor")]
        public int Vendorid { get; set; }

        [DisplayName("Vendor Name")]
        public string Vendorname { get; set; }

        [DisplayName("Purchase Date")]
        public DateTime Dateofpurchase { get; set; }

        [DisplayName("Bill No.")]
        public string BillNo { get; set; }

        [DisplayName("Cost Amt.")]
        public decimal Cost { get; set; }

        [DisplayName("Journal Id.")]
        public int lib_Jid { get; set; }

        [DisplayName("Journal Type")]
        public int LibType { get; set; }

        [DisplayName("Select Department")]
        public int Department { get; set; }

        [DisplayName("Journal Title")]
        public string JournalTitle { get; set; }

        [DisplayName("Journal Volume")]
        public string LibJVolume { get; set; }

        [DisplayName("Serial Number")]
        public string Number { get; set; }

        [DisplayName("Issue Date")]
        public DateTime IssueDate { get; set; }

        [DisplayName("Select Vendor")]
        public int JVendorId { get; set; }

        [DisplayName("Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [DisplayName("Bill No.")]
        public string JBillNo { get; set; }

        [DisplayName("Select Option")]
        public int Option { get; set; }


      


        [DisplayName("Accessor No")]
        public string AccessorNo { get; set; }

        [DisplayName("Shelf No")]
        public string ShelfNo { get; set; }


        public List<lib_Bookentry> Bookentrycollection { get; set; }

        public List<tblDepartment> _departmentlist { get; set; }

        public List<lib_Journal> Journalentrydetails { get; set; }
        public List<sp_getBookentry_Result> _bookentrylist { get; set; }
        public List<sp_GetSearchBookAndJournal_Result> _bookandJournallist { get; set; }

        public List<sp_GetSearchJournal_Result> _journalList { get; set; }
    }
}


