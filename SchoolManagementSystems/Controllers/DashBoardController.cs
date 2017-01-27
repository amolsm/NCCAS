using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystems.Controllers
{
    [HandleError]
    [SchoolManagementSystems.MvcApplication.SessionExpire]
    public class DashBoardController : Controller
    {
        //
        // GET: /DashBoard/
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
        public ActionResult Index(string Search_Data)
        {
            Dashboardviewmodel _dashview = new Dashboardviewmodel();
            FillPermission(31);
            if (Search_Data == null || Search_Data == "")
                _dashview._Newslist = db.sp_getNewslist().OrderBy(m => m.Newsid).ToList();
           
        
            return View(_dashview);
        }

        public ActionResult AddNews(string Search_Data)
        {
            Dashboardviewmodel _dashview = new Dashboardviewmodel();
            FillPermission(31);
            if (Search_Data == null || Search_Data == "")
                _dashview._Newslist = db.sp_getNewslist().OrderBy(m => m.Newsid).ToList();


            return View(_dashview);
        }
        public JsonResult FillNewsDetails(int Newsid)
        {
            var data = db.tbl_news.Where(m => m.Newsid == Newsid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_news(string NewsTitle)
        {
            var data = db.tbl_news.Where(m => m.NewsTitle == NewsTitle).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLNews(Dashboardviewmodel _dbvm, string evt, int id)
        {
            if (evt == "submit")
            {

                db.sp_news_DML(_dbvm.Newsid, _dbvm.NewsDate, _dbvm.NewsTitle, _dbvm.NewsDesc, _dbvm.Status, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_news_DML(_dbvm.Newsid, _dbvm.NewsDate, _dbvm.NewsTitle, _dbvm.NewsDesc, _dbvm.Status, "del").ToString();
            }
            _dbvm._Newslist = db.sp_getNewslist().ToList();
            return PartialView("_NewsList", _dbvm);
        
        }

        public void FillPermission(int modid)
        {
            var per = db.sp_get_permission(Convert.ToInt32(Session["Role"]), modid).ToList();
            for (int i = 0; i < per.Count; i++)
            {
                if (per[i].Permissionid == 1) { ViewData["Add"] = "Allow"; }
                else if (per[i].Permissionid == 2) { ViewData["Modify"] = "Allow"; }
                else if (per[i].Permissionid == 3) { ViewData["View"] = "Allow"; }
                else if (per[i].Permissionid == 4) { ViewData["Delete"] = "Allow"; }
            }
        }

    }
}
