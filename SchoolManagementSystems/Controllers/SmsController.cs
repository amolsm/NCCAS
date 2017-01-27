using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using System.Net;

namespace SchoolManagementSystems.Controllers
{
    public class SmsController : Controller
    {
        //
        // GET: /Sms/
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sms()
        {
            smsviewmodel _svm = new smsviewmodel();
            _svm._Classlist = db.tbl_class.Where(m => m.status == true).ToList();
            return View(_svm);
        }

        public ActionResult Test()
        {
            smsviewmodel _svm = new smsviewmodel();
            _svm._Classlist = db.tbl_class.Where(m => m.status == true).ToList();
            return View(_svm);
        }

        public ActionResult Student_sms()
        {
            smsviewmodel _svm = new smsviewmodel();
            _svm._Classlist = db.tbl_class.Where(m => m.status == true).ToList();
            _svm._Divisionlist = db.tbl_division.Where(m => m.status == true).ToList();
            return View(_svm);
        }

        public ActionResult Divisionwise_sms()
        {
            smsviewmodel _svm = new smsviewmodel();
            _svm._Classlist = db.tbl_class.Where(m => m.status == true).ToList();
            return View(_svm);
        }

        public ActionResult Classwise_sms()
        {
            smsviewmodel _svm = new smsviewmodel();
            _svm._Classlist = db.tbl_class.Where(m => m.status == true).ToList();
            return View(_svm);
        }
        public JsonResult ClassStudent(int Class)
        {
            var data = db.tbl_student.Where(m => m.Classid == Class).Select(m => new { m.Studid, m.Studnm, m.FatherContact, m.Studfathernm }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClassStudentDivision(int Classid,int Divisionid)
        {
            var data =
   from m in db.tbl_student
   join n in db.tbl_AssignDivision on m.Studid equals n.Studid
   where m.Classid == Classid && n.DivisionId == Divisionid
   select new { m.Studid, m.Studnm, m.FatherContact, m.Studfathernm };
          
            //var data = db.tbl_student.Where(m => m.Classid == Classid).Select(m => new { m.Studid, m.Studnm, m.FatherContact, m.Studfathernm }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployee()
        {
            var data = db.tbl_employee.Select(m => new { m.Empid, fullname = m.FirstName + " " + m.LastName, m.MobileNo }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public void SendSms(string[] nos, string msg)
        {
            string s;
            string[] splt = nos[0].ToString().Split(',');
            for (int i = 0; i < splt.Count()-1; i++)
            {
                s = splt[i].ToString();
                MsgDelivery(s, msg);
            }
        }

        public JsonResult BindSubClass(int ClassId)
        {
            var data = db.tbl_division.Where(m => m.Classid == ClassId && m.status == true);
            return Json(new SelectList(data, "DivisionId", "DivisionName"));
        }

        public void MsgDelivery(string mb, string msg)
        {
            WebClient client = new WebClient();
            string s = client.DownloadString("http://smsc.biz/httpapi/send?username=lalit.dudani@gmail.com&password=Welcome123&sender_id=SMSIND&route=T&phonenumber=" + mb.ToString() + "&message=" + msg.ToString() + "");
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


        public JsonResult ClassDivision(int Class)
        {
            var data =
   from m in db.tbl_AssignDivision
   join n in db.tbl_division on m.DivisionId equals n.DivisionId
   where m.Classid == Class && m.status == true
   select new { m.id, n.DivisionName };
   return Json(data, JsonRequestBehavior.AllowGet);
        }

         [HttpPost]
        public ActionResult Test(string[] ids)
        {
            smsviewmodel _svm = new smsviewmodel();
            _svm._Classlist = db.tbl_class.Where(m => m.status == true).ToList();
         

            // In the real application you can ids 

            if (ids != null)
            {
                ViewBag.Message = "You have selected following Customer ID(s):" + string.Join(", ", ids);
            }
            else
            {
                ViewBag.Message = "No record selected";
            }

            return View(_svm);
        }
    }
}
  
