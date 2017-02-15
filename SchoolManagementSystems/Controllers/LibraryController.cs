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
          
            return View(lb);
          
        }

        public ActionResult BookEntry()
         {
            libBookentry lb = new libBookentry();
            lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
            return View(lb);

        }
        [HttpPost]
        public ActionResult BookEntry(libBookentry _lb, string action,string command)
        {
          
            if (command.Equals("BookSubmit"))
            {
                if (string.IsNullOrEmpty(_lb.booktitle))
                {
                    ModelState.AddModelError("booktitle", "Book Title is Required");
                }
                if (string.IsNullOrEmpty(_lb.CallNo))
                { ModelState.AddModelError("CallNo", "Call No is Required"); }
                if (string.IsNullOrEmpty(_lb.Volume))
                { ModelState.AddModelError("Volume", "Volume is Required"); }
                if (string.IsNullOrEmpty(_lb.SerielNumber))
                { ModelState.AddModelError("SerielNumber", "SerielNumber is Required"); }
                if (string.IsNullOrEmpty(Convert.ToString(_lb.Authorid)))
                { ModelState.AddModelError("Authorid", "Author is Required"); }
                if (string.IsNullOrEmpty(Convert.ToString(_lb.PublishedByid)))
                { ModelState.AddModelError("PublishedByid", "Publisher is Required"); }
                if (string.IsNullOrEmpty(_lb.Edition))
                { ModelState.AddModelError("Edition", "Edition is Required"); }
                if (string.IsNullOrEmpty(Convert.ToString(_lb.Vendorid)))
                { ModelState.AddModelError("Vendorid", "Vendor is Required"); }
                if (string.IsNullOrEmpty(Convert.ToString(_lb.PurchaseDate)))
                { ModelState.AddModelError("PurchaseDate", "Purchase Date is Required"); }
                if (string.IsNullOrEmpty(Convert.ToString(_lb.BillNo)))
                { ModelState.AddModelError("BillNo", "Bill No. is Required"); }
                if (string.IsNullOrEmpty(Convert.ToString(_lb.Cost)))
                { ModelState.AddModelError("Cost", "Cost Amt is Required"); }

            }
            else if (command.Equals("JournalSubmit"))
            {
                
              
            }
            else { }
            if (ModelState.IsValid)
            {
                string msg = "test";
            }
            _lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
            return View(_lb);
        }
        }
}
