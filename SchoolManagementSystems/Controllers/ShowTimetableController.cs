using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystems.Controllers
{
    [HandleError]
    public class ShowTimetableController : Controller
    {
        //
        // GET: /ShowTimetable/

        public ActionResult Index()
        {
            return View();
        }

    }
}
