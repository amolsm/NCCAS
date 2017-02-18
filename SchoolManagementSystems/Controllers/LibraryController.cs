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

    

        public ActionResult Index()
        {
            libBookentry lb = new libBookentry();
            //FillPermission(47);
          
            lb._bookentrylist = db.sp_getBookentry().ToList();
            lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
            lb._journalList = db.sp_GetSearchJournal(2, 0, null, DateTime.Now).ToList();
            lb._bookandJournallist = db.sp_GetSearchBookAndJournal(3,0,null, null, DateTime.Now).ToList();
            return View(lb);
          
        }
        [HttpPost]
        public ActionResult Index(libBookentry lb)
        {
           
            //FillPermission(47);

            lb._bookentrylist = db.sp_getBookentry().ToList();
            lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();

            if (lb.Option == 2)
            {
                lb._journalList = db.sp_GetSearchJournal(lb.Option, lb.Department, lb.booktitle, lb.Dateofpurchase).ToList();
            }
            else { lb._journalList = db.sp_GetSearchJournal(2, 0, null, DateTime.Now).ToList(); }
            if (lb.Option == 1 || lb.Option == 3)
            {
                lb._bookandJournallist = db.sp_GetSearchBookAndJournal(lb.Option, lb.Department, lb.booktitle,lb.Authorname, lb.Dateofpurchase).ToList();
            }
            else { lb._bookandJournallist = db.sp_GetSearchBookAndJournal(3, 0, "", "", DateTime.Now).ToList(); }
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
               try { 
                db.sp_AddLibraryBook(_lb.bookid,_lb.booktitle,_lb.CallNo, _lb.Volume, _lb.SerielNumber, _lb.Authorid,
                    _lb.Authorname, _lb.PublishedByid, _lb.PublishedByName, _lb.Edition, _lb.Vendorid, _lb.Vendorname,
                     _lb.Dateofpurchase, _lb.BillNo, _lb.Cost).ToString();
              TempData["Error"] = "Success";
                }
                catch { TempData["Error"] = "Failed"; }
                 }
            if (command.Equals("JournalSubmit"))
            {
               
                try
                {
                    db.sp_AddLibraryJournal(_lb.lib_Jid,_lb.LibType,_lb.Department,_lb.JournalTitle,_lb.LibJVolume,
                        _lb.Number,_lb.IssueDate,_lb.Vendorid,_lb.PurchaseDate,_lb.JBillNo).ToString();
                    TempData["Error"] = "Success";

                }
                catch { TempData["Error"] = "Failed"; }





            }
          
           
            _lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();

            return View(_lb);
           
        }

        public ActionResult BookEdit(int? bookid)
        {
            libBookentry lb = new libBookentry();
            TempData["Error"] = "";
            lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
            return View("BookEntry", lb);
        }

        public ActionResult JournalEdit(int? lib_Jid)
        {
            libBookentry lb = new libBookentry();
            TempData["Error"] = "";
            lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
            return View("BookEntry", lb);
        }
        public JsonResult FillBookDetails(int bookid)
        {
            TempData["Error"] = "";
            var data = db.lib_Bookentry.Where(m => m.bookid == bookid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillJournalDetails(int jid)
        {
            TempData["Error"] = "";
            var data = db.lib_Journal.Where(m => m.lib_Jid == jid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
