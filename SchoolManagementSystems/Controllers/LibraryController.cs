using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

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
           
            lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
            int count = (from p in db.lib_Bookentry
                         select p).Count();
            count=count + 1;
            lb.AccessorNo = count.ToString();
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
<<<<<<< HEAD
                   _lb.Dateofpurchase, _lb.BillNo, _lb.Cost,_lb.AccessorNo,_lb.ShelfNo).ToString();
=======

                     _lb.Dateofpurchase, _lb.BillNo, _lb.Cost,_lb.AccessorNo,_lb.ShelfNo,1).ToString();

>>>>>>> origin/master
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
         
            return RedirectToAction("BookEntry");
           
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
            var data = db.lib_Bookentry.Where(m => m.bookid == bookid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillJournalDetails(int jid)
        {
          
            var data = db.lib_Journal.Where(m => m.lib_Jid == jid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BookIssue()
        {
            BookAllocation b = new BookAllocation();

            b._BookIssueList = db.tbl_lib_BookIssue.ToList();
            b._bookandJournallist = db.sp_GetSearchBookAndJournal(3, 0, null, null, DateTime.Now).ToList();
            return View(b);
        }
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var StudentName = (from bi in db.tbl_student
                           where bi.Studnm.StartsWith(prefix)
                           select new {label=bi.Studnm ,
                           val=bi.Studid}).ToList();
            return Json(StudentName, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BookAutoComplete(string prefix)
        {
            var BookName = (from bi in db.lib_Bookentry
                               where bi.booktitle.StartsWith(prefix)
                               select new
                               {
                                   label = bi.booktitle,
                                   val = bi.CallNo
                               }).ToList();
            return Json(BookName, JsonRequestBehavior.AllowGet);
        }
       

        public JsonResult GetBookIssueDetails(string StudentName, int Studentid,int Stdid)
        {
            int stid;
            int Stdids;
           
            try { stid = Convert.ToInt32(Studentid);
           
            } catch { stid = 0; }
            try { Stdids = Convert.ToInt32(Stdid); }
            catch { Stdids = 0; }
           
            if (Stdids != 0)
            {
                var data = db.sp_GetBookIssueDetailsbyStudentid(Stdids);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var data = db.sp_GetBookIssueDetailsbyStudentid(stid);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
           
           
            

        }


        public JsonResult GetBookDetails(string BookName, string hcallid, string callno)
        {
          
            
            if (callno == "" || callno==null)
            { var data = db.sp_GetBookDetailsbyBookidorBookname(BookName, hcallid, null).ToList();
              return Json(data);
            }
            else
            { var data = db.sp_GetBookDetailsbyBookidorBookname(null, null, callno).ToList();
              return Json(data);
            }
           
           
        }

        public JsonResult SaveAllotment(int Stdid, int bookid,int NoOfDays)
        {
            BookAllocation b = new BookAllocation();
            //b._BookIssueList = db.tbl_lib_BookIssue.ToList();
            var data = db.sp_GetBookDetailsbyBookidorBookname(null, null, null).ToList();
            return Json(data);
        }
    }
}
