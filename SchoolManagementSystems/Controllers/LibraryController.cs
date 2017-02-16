using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystems.Controllers
{
    public class LibraryController : Controller
    {
        //
        // GET: /Library/
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();

    

        public ActionResult Index(string Search_Data)
        {
            libBookentry lb = new libBookentry();
            //FillPermission(47);
            if (Search_Data == null || Search_Data == "")
                lb.Bookentrycollection = db.lib_Bookentry.OrderBy(m => m.bookid).ToList();
            else
                lb.Bookentrycollection = db.lib_Bookentry.Where(x => x.booktitle.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.CallNo.ToUpper().Contains(Search_Data.ToUpper()) ||
               x.SerielNumber.ToUpper().Contains(Search_Data.ToUpper())).OrderBy(m => m.bookid).ToList();
            lb._bookentrylist = db.sp_getBookentry().ToList();
            lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
            return View(lb);
          
        }

        public ActionResult BookEntry()
         {
            libBookentry lb = new libBookentry();
            TempData["Error"] = "";
            lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
            return View(lb);

        }
        [HttpPost]
        public ActionResult BookEntry(libBookentry _lb,string command)
        {
          
            if (command.Equals("BookSubmit"))
            {
                lib_Bookentry tbl_Book = new lib_Bookentry();
                tbl_Book.booktitle = _lb.booktitle;
                tbl_Book.CallNo = _lb.CallNo;
                tbl_Book.Volume = _lb.Volume;
                tbl_Book.SerielNumber = _lb.SerielNumber;
                tbl_Book.Authorid = _lb.Authorid;
                tbl_Book.Authorname = _lb.Authorname;
                tbl_Book.PublishedByid = _lb.PublishedByid;
                tbl_Book.PublishedByName = _lb.PublishedByName;
                tbl_Book.Edition = _lb.Edition;
                tbl_Book.Vendorid = _lb.Vendorid;
                tbl_Book.Vendorname = _lb.Vendorname;
                tbl_Book.Dateofpurchase = _lb.Dateofpurchase;
                tbl_Book.BillNo = _lb.BillNo;
                tbl_Book.Cost = _lb.Cost;
                try
                {
                    db.lib_Bookentry.AddObject(tbl_Book);
                    db.SaveChanges();
                    TempData["Error"] = "Success";
                 

                }
                catch { TempData["Error"] = "Failed"; }
                



            }
            if (command.Equals("JournalSubmit"))
            {
                lib_Journal tbl_journal = new lib_Journal();
                tbl_journal.LibType = _lb.LibType;
                tbl_journal.Department = _lb.Department;
                tbl_journal.JournalTitle = _lb.JournalTitle;
                tbl_journal.Volume = _lb.LibJVolume;
                tbl_journal.Number = _lb.Number;
                tbl_journal.IssueDate = _lb.IssueDate;
                tbl_journal.Vendorid = _lb.JVendorId;
                tbl_journal.PurchaseDate = _lb.PurchaseDate;
                tbl_journal.JBillNo = _lb.JBillNo;
                try
                {
                    db.lib_Journal.AddObject(tbl_journal);
                    db.SaveChanges();
                    TempData["Error"] = "Success";

                }
                catch { TempData["Error"] = "Failed"; }





            }
           
           
            _lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();

            return View(_lb);
           
        }

        }
}
