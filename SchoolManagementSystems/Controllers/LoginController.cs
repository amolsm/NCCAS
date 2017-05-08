using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using Common;

namespace SchoolManagementSystems.Controllers
{
    [HandleError]
    public class LoginController : Controller
    {
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string Username, string Password)
        {
            ViewData["Error"] = "";
            Userviewmodel uvm = new Userviewmodel();
            Dashboardviewmodel dvm = new Dashboardviewmodel();
            string currentdate = CommonUtility.FormatedDateString(DateTime.Now);
            string datemonth = currentdate.Substring(0, 5);
            if (Session["Userid"] != null && Session["Userid"].ToString() != "")
            {
                dvm.Newsdatacollection = db.tbl_news.Where(m => m.Status == true).ToList();
                dvm.BirthdaylistDetails = db.sp_GetBirthdayListDetails().Where(m => m.DOB.ToUpper().Contains(datemonth.ToString())).OrderBy(m => m.Studnm).ToList();

                return View("DashBoard", dvm);
            }
            else
            {
                try
                {
                    uvm.Userdatacollection = db.tbl_user.Where(m => m.UserName == Username && m.Password == Password && m.Status == 1).ToList();
                }
                catch { }
                if (uvm.Userdatacollection != null)
                {
                    if (uvm.Userdatacollection.Count() > 0 && uvm.Userdatacollection[0].Status == 1)
                    {
                        Session["Userid"] = uvm.Userdatacollection[0].Userid.ToString();
                        Session["User"] = uvm.Userdatacollection[0].UserName.ToString();
                        Session["Role"] = uvm.Userdatacollection[0].Type.ToString();
                        Session["Genid"] = uvm.Userdatacollection[0].Genid.ToString();
                        dvm.Newsdatacollection = db.tbl_news.Where(m => m.Status == true).ToList();
                        dvm.BirthdaylistDetails = db.sp_GetBirthdayListDetails().Where(m => m.DOB.ToUpper().Contains(datemonth.ToString())).OrderBy(m => m.Studnm).ToList();
                        return View("DashBoard", dvm);
                    }
                    else
                    {
                        ViewData["Error"] = "Either UserName or Password is incorrect / You are not active User!..";
                        return View("Index");
                    }
                }
               
                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Index");
        }

        public ActionResult ChangePassword()
        {
            Userviewmodel uvm = new Userviewmodel();
            return View(uvm);
        }

        [HttpGet]
        public JsonResult UpdatePassword(int Userid, string OldPassword, string NewPassword)
        {
            Userviewmodel uvm = new Userviewmodel(); string msg = null;
            uvm.checkpassword = db.sp_changepassword(Userid, NewPassword, OldPassword, "check").ToList();
            if (uvm.checkpassword.Count > 0)
            {
                db.sp_changepassword(Userid, NewPassword, OldPassword, "change");
                msg = "Password changed successfully!..";
            }
            else
            {
                msg = "Old password entered is incorrect!..";
            }
            //ViewData["msg"] = msg;
            //return View(ViewData["msg"]);
            return Json(uvm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            Userviewmodel uvm = new Userviewmodel();
            return View(uvm);
        }

        public ActionResult GetPassword()
        {
            Userviewmodel uvm = new Userviewmodel();
            return View(uvm);
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

        public ActionResult GetNews(Dashboardviewmodel dash)
        {

            return View("Dashboard");
        }


    }
}
