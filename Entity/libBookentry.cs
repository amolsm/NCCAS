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

        [DisplayName("Book/Journal Title")]
        public string Title { get; set; }

        [DisplayName("Call No.")]
        public string CallNo { get; set; }

        [DisplayName("Volume")]
        public string Volume { get; set; }

        [DisplayName("Serial Number")]
        public string SerielNumber { get; set; }

        [DisplayName("Author")]
        public int Authorid { get; set; }


        [DisplayName("Author Name")]
        public string Authorname { get; set; }

        [DisplayName("Publisher")]
        public int PublishedByid { get; set; }

        [DisplayName("Publisher Name")]
        public string PublishedByName { get; set; }

        [DisplayName("Edition")]
        public string Edition { get; set; }

        [DisplayName("Vendor")]
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

        [DisplayName("Course")]
        public int LibType { get; set; }

        [DisplayName("Department")]
        public int Department { get; set; }

        [DisplayName("Journal Title")]
        public string JournalTitle { get; set; }

        [DisplayName("Volume")]
        public string LibJVolume { get; set; }

        [DisplayName("Accesson No.")]
        public string Number { get; set; }

        [DisplayName("Issue Date")]
        public DateTime IssueDate { get; set; }

        [DisplayName("Issue")]
        public int IssueType { get; set; }

        [DisplayName("Vendor")]
        public int JVendorId { get; set; }

        [DisplayName("Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [DisplayName("Bill No.")]
        public string JBillNo { get; set; }

        [DisplayName("Select Option")]
        public int Option { get; set; }

        [DisplayName("Cost Amt.")]
        public decimal JCost { get; set; }




        [DisplayName("No Of Days")]
        public int NoDays { get; set; }


        [DisplayName("Accesson No.")]
        public string AccessorNo { get; set; }

        [DisplayName("Shelf No")]
        public string ShelfNo { get; set; }

        [DisplayName("No Of Copy")]
        public int NoOfCopy { get; set; }

        [DisplayName("Student Name")]
        public string StudentName { get; set; }

        [DisplayName("Student ID")]
        public int StudentId { get; set; }

        [DisplayName("Book Status")]
        public int IsActive { get; set; }


        [DisplayName("Department")]
        public int DeptId { get; set; }



        [DisplayName("Course")]
        public int CourseId { get; set; }

        [DisplayName("Category")]
        public int BookCategoryid { get; set; }

        [DisplayName("Publisher")]
        public string JPublisher { get; set; }

        [DisplayName("Shelf No")]
        public string JShelfNo { get; set; }

        public List<lib_Bookentry> Bookentrycollection { get; set; }

        public List<tblDepartment> _departmentlist { get; set; }
        public List<tbl_lib_Author> _authorlist { get; set; }

        public List<tbl_lib_Publisher> _publisherlist { get; set; }


        public List<tbl_lib_Vendor> _vendorlist { get; set; }

        public List<tbl_lib_JournalType> _jtypelist { get; set; }

        public List<tbl_lib_BookIssue> _bookissuelist { get; set; }

        public List<lib_Journal> Journalentrydetails { get; set; }
        public List<sp_getBookentry_Result> _bookentrylist { get; set; }
        public List<sp_GetSearchBookAndJournal_Result> _bookandJournallist { get; set; }

        public List<sp_GetSearchJournal_Result> _journalList { get; set; }
        public List<tbl_CourseMaster> courselist { get; set; }

        public List<tbl_lib_BookCategory> _bookcategorylist { get; set; }
    }
}


