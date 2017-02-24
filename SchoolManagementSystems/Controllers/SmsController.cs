using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using System.Net;
using System.Collections.Specialized;

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
            _svm._Classlist = new List<tbl_CourseMaster>();
            return View(_svm);
        }
        public ActionResult Test()
        {
            smsviewmodel _svm = new smsviewmodel();
            _svm._Classlist = db.tbl_CourseMaster.ToList();
            return View(_svm);
        }
        public ActionResult Student_sms()
        {
            smsviewmodel _svm = new smsviewmodel();
            _svm._Classlist = db.tbl_CourseMaster.ToList();
            _svm.yearlist = db.tbl_YearMaster.ToList();
            return View(_svm);
        }
        public JsonResult GetYearClass(string cid)
        {
            int coureid = 0;
            if ( cid != null && cid != "")
            {
                coureid = Convert.ToInt32(cid);
            }
            var yeardata = from post in db.tbl_CourseYearMaster
                           join meta in db.tbl_YearMaster on post.academicyear equals meta.yearid
                           where post.courseid == coureid && post.status == true
                           select new { meta.yearid, meta.YearName };

            return Json(new SelectList(yeardata, "yearid", "YearName"));

        }
        public ActionResult Divisionwise_sms()
        {
            smsviewmodel _svm = new smsviewmodel();
            _svm._Classlist = db.tbl_CourseMaster.ToList();
            return View(_svm);
        }
        public ActionResult Classwise_sms()
        {
            smsviewmodel _svm = new smsviewmodel();
            _svm._Classlist = db.tbl_CourseMaster.ToList();
            return View(_svm);
        }
        public JsonResult ClassStudent(int Class)
        {
            var data = db.tbl_student.Where(m => m.Classid == Class).Select(m => new { m.Studid, m.Studnm, m.FatherContact, m.Studfathernm }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ClassStudentDivision(int Classid,string Divisionid)
        {
            var data =
   from m in db.tbl_student
   join n in db.tbl_AssignDivision on m.Studid equals n.Studid
   where m.Classid == Classid && n.divisionId == Divisionid
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
            var data = db.tbl_division.Where(m =>  m.Classid == Class && m.status == true).Select(m => new { m.DivisionId, m.DivisionName }).ToList();
   //         var data =
   //from m in db.tbl_AssignDivision
   //join n in db.tbl_division on m.DivisionId equals n.DivisionId
   //where m.Classid == Class && m.status == true
   //select new { m.id, n.DivisionName };
   return Json(data, JsonRequestBehavior.AllowGet);
        }
         [HttpPost]
        public ActionResult Test(string[] ids)
        {
            smsviewmodel _svm = new smsviewmodel();
            _svm._Classlist = db.tbl_CourseMaster.ToList();
         

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
         private string SendUpdateSMS(int EmpId,string msgs)
         {
             string stemailid;
             string mobileno;

             tbl_employee st = db.tbl_employee.Where(x => x.Empid == EmpId).FirstOrDefault();
             //stemailid = st.Emailid;
             mobileno = st.MobileNo;

             string msg = msgs;
             //string msg = "Nanjil Catholic College of Arts & Science" + Environment.NewLine;
             //msg = msg + "Your Profile Updated Successfully";


             String message = HttpUtility.UrlEncode(msg);
             using (var wb = new WebClient())
             {
                 //changes..
                 byte[] response = wb.UploadValues("http://api.textlocal.in/send/", new NameValueCollection()
                {
                {"username" , "ranjithkumar01@gmail.com"},
                {"hash" , "7ddf766152c1e491ae183d793dd1e94bd93e3231"},
                {"numbers" , mobileno},
                {"message" , message},
                {"sender" , "24HDS"}
                });
                 string result = System.Text.Encoding.UTF8.GetString(response);
                 return result;
             }
         }

         public JsonResult msgsend(string[] presentdetails)
         {
             tbl_employee sa = new tbl_employee();
             string s;

             for (int i = 0; i < presentdetails.Count(); i++)
             {
                 s = presentdetails[i].ToString();
                 string[] s1 = s.ToString().Split(',');
                 string msg = s1[0].ToString();
                 int Empid = Convert.ToInt32(s1[2].ToString());
                 bool Present = Convert.ToBoolean(s1[3].ToString());
                 if (Present == true)
                 {
                    SendUpdateSMS(Empid, msg);
                 }
                 else { }
                 
             }
             return Json(presentdetails);


         }
         private string studentSMS(string mobileno,string msgs)
         {
             //string stemailid;
             //string mobileno;

             //tbl_employee st = db.tbl_employee.Where(x => x.Empid == EmpId).FirstOrDefault();
             ////stemailid = st.Emailid;
             //mobileno = st.MobileNo;

             string msg = msgs;
             //string msg = "Nanjil Catholic College of Arts & Science" + Environment.NewLine;
             //msg = msg + "Your Profile Updated Successfully";


             String message = HttpUtility.UrlEncode(msg);
             using (var wb = new WebClient())
             {
                 //changes..
                 byte[] response = wb.UploadValues("http://api.textlocal.in/send/", new NameValueCollection()
                {
                {"username" , "ranjithkumar01@gmail.com"},
                {"hash" , "7ddf766152c1e491ae183d793dd1e94bd93e3231"},
                {"numbers" , mobileno},
                {"message" , message},
                {"sender" , "24HDS"}
                });
                 string result = System.Text.Encoding.UTF8.GetString(response);
                 return result;
             }
         }
         public JsonResult studentmsg(string[] presentdetails)
         {
             tbl_student sa = new tbl_student();
             string s;

             for (int i = 0; i < presentdetails.Count(); i++)
             {
                 s = presentdetails[i].ToString();
                 string[] s1 = s.ToString().Split(',');
                 string msg = s1[0].ToString();
                 string student = s1[1].ToString();
                 bool Present = Convert.ToBoolean(s1[2].ToString());
                 if (Present == true)
                 {
                         studentSMS(student, msg);
                 }
                 else { }

             }
             return Json(presentdetails);


         }

         public JsonResult studentmsgclass(string[] presentdetails)
         {
             tbl_student sa = new tbl_student();
             //int classid;
             string s;
            
             for (int i = 0; i < presentdetails.Count(); i++)
             {
                 
                 s = presentdetails[i].ToString();
                 string[] s1 = s.ToString().Split(',');
                 string msg = s1[0].ToString();
                 int student = Convert.ToInt32(s1[1].ToString());
                 var studentlist = db.tbl_student.Where(x => x.Classid == student).Select(m=> m.FatherContact).ToList();

                //for (int j = 0; j < studentlist.Count(); j++)
                // {
                     foreach (string value in studentlist)
                     {
                         bool Present = Convert.ToBoolean(s1[2].ToString());
                         if (Present == true)
                         {

                             studentSMS(value, msg);
                         }
                         else { }
                     }
                 //}

             }
             return Json(presentdetails);


         }

         public JsonResult studentmsgdivision(string[] presentdetails)
         {
             tbl_AssignDivision sa = new tbl_AssignDivision();
             //int classid;
             string s;

             for (int i = 0; i < presentdetails.Count(); i++)
             {
                 

                 s = presentdetails[i].ToString();
                 string[] s1 = s.ToString().Split(',');
                 string msg = s1[0].ToString();
                 int classid = Convert.ToInt32(s1[1].ToString());
                 int division = Convert.ToInt32(s1[2].ToString());
                 var divisionname = db.tbl_division.Where(x => (x.DivisionId == division && x.Classid == classid)).Select(m => m.DivisionName).ToList();
                 foreach (string divisiionn in divisionname)
                 {
                     var divisionlist = db.tbl_AssignDivision.Where(x => (x.divisionId == divisiionn && x.Classid == classid)).Select(m => m.Studid).ToList();
                     foreach (int studentid in divisionlist)
                     {
                         var studentlist = db.tbl_student.Where(x => x.Studid == studentid).Select(m => m.FatherContact).ToList();


                         foreach (string value in studentlist)
                         {
                             bool Present = Convert.ToBoolean(s1[3].ToString());
                             if (Present == true)
                             {

                                 studentSMS(value, msg);
                             }
                             else { }
                         }
                     }
                 }

             }
             return Json(presentdetails);


         }      

    }
}
  
