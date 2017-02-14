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
            return View();

        }
        [HttpPost]
        public ActionResult AddBookAndJournal(libBookentry _lb, string action)
        {
            return View();
        }
        }
}
