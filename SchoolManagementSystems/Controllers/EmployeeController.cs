using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using BAL;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Web.Helpers;
using System.Data;
using System.Text;
using Ionic.Zip;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Net;
using System.Collections.Specialized;

namespace SchoolManagementSystems.Controllers
{
    [HandleError]
    public class EmployeeController : Controller
    {
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
        // GET: /Employee/

        public ActionResult Index(string Search_Data)
        {
            employeeviewmodel _bgv = new employeeviewmodel();
            FillPermission(47);
            if (Search_Data == null || Search_Data == "")
                _bgv.EmployeeDataCollection = db.tbl_employee.OrderBy(m => m.Empid).ToList();
            else
                _bgv.EmployeeDataCollection = db.tbl_employee.Where(x => x.FirstName.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.empemail.ToUpper().Contains(Search_Data.ToUpper()) ||
               x.MobileNo.ToUpper().Contains(Search_Data.ToUpper())).OrderBy(m => m.Empid).ToList();
            _bgv._emplist = db.sp_getemp().ToList();
            return View(_bgv);


        }
        public FileContentResult getImg1(int id)
        {
            byte[] byteArray = db.tbl_employee.Where(m => m.Empid == id).Select(m => m.emppic).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }
        public FileContentResult getImg2(int id)
        {
            byte[] byteArray = db.tbl_employee.Where(m => m.Empid == id).Select(m => m.parentspic).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
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
        public ActionResult EmpAdmission(int? Empid)
        {
            employeeviewmodel _bgv = new employeeviewmodel();
            FillPermission(54);
            _bgv.courselist = db.tbl_CourseMaster.Where(m => m.Status == true).ToList();
            _bgv.statelist = db.tbl_state.Where(m => m.status == true).ToList();
            _bgv.departmentlistdetails = db.tblDepartment.Where(m => m.Dept_id != 0).ToList();
            _bgv._emplist = db.sp_getemp().ToList();
            _bgv.citylist = db.tbl_city.Where(m => m.Status == true).ToList();
            _bgv.qualificationlist = db.tbl_qualification.Where(m => m.status == true).ToList();
            _bgv.countrylist = db.tbl_country.Where(m => m.status == true).ToList();
            _bgv.bloodgrouplist = db.tbl_bloodgroup.Where(m => m.status == true).ToList();
            _bgv.castelist = new List<tbl_caste>();
            _bgv.religionlist = db.tbl_religion.Where(m => m.status == true).ToList();
            _bgv.categorylist = db.tbl_StudentCategory.Where(m => m.status == true).ToList();
            _bgv.occupationlist = db.tbl_occupation.Where(m => m.status == true).ToList();
            _bgv.qualificationlist = db.tbl_qualification.Where(m => m.status == true).ToList();
    
            return View("EmpAdmission", _bgv);
        }
        [HttpGet]
        public JsonResult FillEmpDetails(int empid)
        {
          
            //var empexperience = db.tbl_Empexperience.Where(m => m.empId == empid).ToString();
            
            var data = db.tbl_employee.Where(m => m.Empid == empid).FirstOrDefault();
         
            return Json( data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EmpAdmission_DML(employeeviewmodel _bgv, string action, HttpPostedFileBase files1, HttpPostedFileBase files2)
        {
            
            if (files1 != null)
            {
                if (files1.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        WebImage img = new WebImage(files1.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(600, 600);
                        }
                        byte[] array = img.GetBytes();
                        _bgv.emppic = array;
                    }
                }
            }
            else {
                if (_bgv.Empid != 0)
                {
                    _bgv.emppic = db.tbl_employee.Where(m => m.Empid == _bgv.Empid).Select(m => m.emppic).FirstOrDefault();
                }
            }

            if (files2 != null)
            {
                if (files2.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        WebImage img = new WebImage(files2.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(600, 600);
                        }
                        byte[] array = img.GetBytes();
                        _bgv.ppic = array;
                    }
                }
            }
            else
            {
                if (_bgv.Empid != 0)
                {
                    _bgv.ppic = db.tbl_employee.Where(m => m.Empid == _bgv.Empid).Select(m => m.parentspic).FirstOrDefault();
                }
            }
                 try
                 {
                db.sp_employee_DML(_bgv.Empid, _bgv.FirstName, _bgv.MiddleName, _bgv.LastName, _bgv.Cityid, _bgv.Stateid, _bgv.Zipcode, _bgv.Emailid, _bgv.PhoneNo, _bgv.MobileNo, _bgv.Address, _bgv.DOB, _bgv.Gender, _bgv.Quallification, _bgv.DateOfJoin, _bgv.Typeid, _bgv.Code, _bgv.OQualification, "NA", _bgv.edepartment, _bgv.eblood, _bgv.equalification1, _bgv.euniversity1, _bgv.eprecentage1, _bgv.eYearofpassing1, _bgv.equalification2, _bgv.euniversity2, _bgv.eprecentage2, _bgv.eYearofpassing2, _bgv.equalification3, _bgv.euniversity3, _bgv.eprecentage3, _bgv.eYearofpassing3, _bgv.equalification4, _bgv.euniversity4, _bgv.eprecentage4, _bgv.eYearofpassing4, _bgv.eweight, _bgv.eheight, _bgv.emppic, _bgv.eidtype, _bgv.dname, _bgv.DCode, _bgv.dcontact, _bgv.daddress, _bgv.relationship, _bgv.pname, _bgv.poccupation, _bgv.pqualification, _bgv.pemail, _bgv.pofficeaddress, _bgv.PCODE, _bgv.pcontact, _bgv.ppic, _bgv.pastreet, _bgv.pacity, _bgv.pastate, _bgv.pacountry, _bgv.papin, _bgv.cstreet, _bgv.ccity, _bgv.cstate, _bgv.ccountry, _bgv.cpin, _bgv.PHCODE, _bgv.phome, _bgv.MCODE, _bgv.hmobile, _bgv.bankname, _bgv.branch, _bgv.ifsccode, _bgv.accountno, _bgv.acname, _bgv.collagename, _bgv.university, _bgv.joiningdate, _bgv.lastdate, _bgv.total, _bgv.ldesignation, _bgv.companyname, _bgv.clastdesignation, _bgv.cjoiningdate, _bgv.clastdate, _bgv.ctotal, _bgv.employeeemail, _bgv.empreligion, _bgv.empcategory, _bgv.empcast, _bgv.mtongue, _bgv.collagename1, _bgv.university1, _bgv.joiningdate1, _bgv.lastdate1,
                     _bgv.total1, _bgv.ldesignation1, _bgv.companyname1, _bgv.clastdesignation1, _bgv.cjoiningdate1, _bgv.clastdate1, _bgv.ctotal1, _bgv.Documenttypename, _bgv.DocumentIDNo, _bgv.conttactrelation, _bgv.ParishName, _bgv.DioceseName, _bgv.collagename2, _bgv.university2, _bgv.joiningdate2, _bgv.lastdate2,
                     _bgv.total2, _bgv.ldesignation2, _bgv.companyname2, _bgv.clastdesignation2, _bgv.cjoiningdate2, _bgv.clastdate2, _bgv.ctotal2, _bgv.collagename3, _bgv.university3, _bgv.joiningdate3, _bgv.lastdate3,
                     _bgv.total3, _bgv.ldesignation3, _bgv.companyname3, _bgv.clastdesignation3, _bgv.cjoiningdate3, _bgv.clastdate3, _bgv.ctotal3, _bgv.collagename4, _bgv.university4, _bgv.joiningdate4, _bgv.lastdate4,
                     _bgv.total4, _bgv.ldesignation4, _bgv.companyname4, _bgv.clastdesignation4, _bgv.cjoiningdate4, _bgv.clastdate4, _bgv.ctotal4, _bgv.collagename5, _bgv.university5, _bgv.joiningdate5, _bgv.lastdate5,
                     _bgv.total5, _bgv.ldesignation5, _bgv.companyname5, _bgv.clastdesignation5, _bgv.cjoiningdate5, _bgv.clastdate5, _bgv.ctotal5, _bgv.collagename6, _bgv.university6, _bgv.joiningdate6, _bgv.lastdate6,
                     _bgv.total6, _bgv.ldesignation6, _bgv.companyname6, _bgv.clastdesignation6, _bgv.cjoiningdate6, _bgv.clastdate6, _bgv.ctotal6, _bgv.collagename7, _bgv.university7, _bgv.joiningdate7, _bgv.lastdate7,
                     _bgv.total7, _bgv.ldesignation7, _bgv.companyname7, _bgv.clastdesignation7, _bgv.cjoiningdate7, _bgv.clastdate7, _bgv.ctotal7, _bgv.collagename8, _bgv.university8, _bgv.joiningdate8, _bgv.lastdate8,
                     _bgv.total8, _bgv.ldesignation8, _bgv.companyname8, _bgv.clastdesignation8, _bgv.cjoiningdate8, _bgv.clastdate8, _bgv.ctotal8, _bgv.collagename9, _bgv.university9, _bgv.joiningdate9, _bgv.lastdate9,
                     _bgv.total9, _bgv.ldesignation9, _bgv.companyname9, _bgv.clastdesignation9, _bgv.cjoiningdate9, _bgv.clastdate9, _bgv.ctotal9, _bgv.q_department1, _bgv.q_department2, _bgv.q_department3, _bgv.q_department4, _bgv.q_Set, _bgv.q_Net, _bgv.q_SetPassingYear, _bgv.q_NetPassingYear
                     ).ToString();

                       }
                           catch { }
                        try
                        {
                            DateTime dt = new DateTime();
                            string yr = dt.Year.ToString();
                            int empid = db.tbl_employee.Where(m => m.Emailid == _bgv.Emailid).Select(m => m.Empid).FirstOrDefault();
                            int typeid = db.tbl_employee.Where(m => m.Emailid == _bgv.Emailid).Select(m => m.Typeid).FirstOrDefault();
                            CreateUsers(_bgv.Emailid, typeid, empid, yr);
                           

                        }
                        catch { }
        
                   
                      return RedirectToAction("Index");

               }
        public JsonResult GetCities(string id)
        {
            int stateid = 0;
            if (id != null && id != "")
            {
                stateid = Convert.ToInt32(id);
            }
            var cities = db.tbl_city.Where(m => m.Stateid == stateid).ToList();
            return Json(new SelectList(cities, "Cityid", "CityName"));
        }
        public JsonResult check_duplicate_Emp(string Emailid)
        {
            var data = db.tbl_employee.Where(m => m.Emailid == Emailid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetExperience(string EmpId)
        {
            int employeeid = 0;
            if (EmpId != null && EmpId != "")
            {
                employeeid = Convert.ToInt32(EmpId);
            }
            var exp = db.tbl_Empexperience.Where(m => m.empId == employeeid).FirstOrDefault();
            return Json(exp, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCastes(string id)
        {
            int Religionid = 0;
            if (id != null && id != "")
            {
                Religionid = Convert.ToInt32(id);
            }
            var castes = db.tbl_caste.Where(m => m.Religionid == Religionid).ToList();
            return Json(new SelectList(castes, "Casteid", "CasteName"));
        }
        private void CreateUsers(string UserName, int Type, int Genid, string academicyear)
        {
            string Password = Guid.NewGuid().ToString().Substring(0, 8);
            var x = (from y in db.tbl_user where y.UserName == UserName select y).FirstOrDefault();
            if (x == null)
            {
                db.sp_User_DML(0, UserName, Password, Type, Genid, 0, academicyear, "");
                SendUserEmails(UserName);
                SendSMS(Genid);
            }
            else
            {
                SendEmails(UserName);
                int empid = db.tbl_employee.Where(m => m.Emailid == UserName).Select(m => m.Empid).FirstOrDefault();
                SendUpdateSMS(empid);
            }
        }
        private void SendUserEmails(string Email)
        {

            SmtpClient smtpClient = new SmtpClient();

            MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"].ToString(), ConfigurationManager.AppSettings["SenderName"].ToString());
            MailAddress to = new MailAddress(Email);
            MailMessage message = new System.Net.Mail.MailMessage(fromAddress, to);
            message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
            message.SubjectEncoding = System.Text.Encoding.GetEncoding("utf-8");
            message.From = fromAddress;
            message.To.Add(Email);
            message.Subject = "Nanjil Catholic College of Arts & Science";
            message.Priority = MailPriority.High;
            message.IsBodyHtml = true;
            string pass = db.tbl_user.Where(m => m.UserName == Email).Select(m => m.Password).FirstOrDefault();
            string msg = "<b>“Welcome to Nanjil Catholic College of Arts & Science”</b><br/><br/>";
            msg = msg + "Your UserName : " + Email + ".<br/>";
            msg = msg + "Your Password : " + pass + ".<br/>";
            msg = msg + "Hope we would be going long term relationship with feature with good Support<br/><br/>Best Regards<br/>NACCAS Management";
            message.Body = msg;
            smtpClient.Send(message);
        }
        private string SendSMS(int EmpId)
        {
            string stemailid;
            string mobileno;

            tbl_employee st = db.tbl_employee.Where(x => x.Empid == EmpId).FirstOrDefault();
            stemailid = st.Emailid;
            mobileno = st.MobileNo;

            string pass = db.tbl_user.Where(m => m.UserName == stemailid).Select(m => m.Password).FirstOrDefault();
            string msg = "Welcome to Nanjil Catholic College of Arts & Science" + Environment.NewLine;
            msg = msg + "USER NAME: " + stemailid + Environment.NewLine;
            msg = msg + "PASSWORD: " + pass + Environment.NewLine;

            String message = HttpUtility.UrlEncode(msg);
            using (var wb = new WebClient())
            {
                
                byte[] response = wb.UploadValues("http://api.textlocal.in/send/", new NameValueCollection()
                {
                {"username" , "ranjithkumar01@gmail.com"},
                {"hash" , "e399e1b41bbe615c57453488771c9ac83e102d9b87f3b7ae41654d2a8e3c4cb1"},
                {"numbers" , mobileno},
                {"message" , message},
                {"sender" , "TXTLCL"}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }
        private void SendEmails(string Email)
        {
            SmtpClient smtpClient = new SmtpClient();

            MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"].ToString(), ConfigurationManager.AppSettings["SenderName"].ToString());
            MailAddress to = new MailAddress(Email);
            MailMessage message = new System.Net.Mail.MailMessage(fromAddress, to);
            message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
            message.SubjectEncoding = System.Text.Encoding.GetEncoding("utf-8");

            message.Subject = "Nanjil Catholic College of Arts & Science";
            message.Priority = MailPriority.High;
            message.IsBodyHtml = true;

            string msg = "<b>“Nanjil Catholic College of Arts & Science”</b><br/><br/>";
            msg = msg +  db.tbl_employee.OrderByDescending(m => m.Empid).Select(m => m.Emailid).FirstOrDefault() + " .<br/>";
            msg = msg + "Here We would like to inform you that Your Profile is Updated Successfully.<br/>";
            msg = msg + "Hope we would be going long term relationship with feature with good Support<br/><br/>Best Regards<br/>NACCAS Management";
            message.Body = msg;
            smtpClient.Send(message);
        }
        private string SendUpdateSMS(int EmpId)
        {
            string stemailid;
            string mobileno;

            tbl_employee st = db.tbl_employee.Where(x => x.Empid == EmpId).FirstOrDefault();
            stemailid = st.Emailid;
            mobileno = st.MobileNo;

            
            string msg = "Nanjil Catholic College of Arts & Science" + Environment.NewLine;
            msg = msg + "Your Profile Updated Successfully";
           

            String message = HttpUtility.UrlEncode(msg);
            using (var wb = new WebClient())
            {
                //changes..
                byte[] response = wb.UploadValues("http://api.textlocal.in/send/", new NameValueCollection()
                {
                {"username" , "ranjithkumar01@gmail.com"},
                {"hash" , "e399e1b41bbe615c57453488771c9ac83e102d9b87f3b7ae41654d2a8e3c4cb1"},
                {"numbers" , mobileno},
                {"message" , message},
                {"sender" , "TXTLCL"}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }
        public JsonResult GetCourse(string id)
        {
            int coureid = 0;
            if (id != null && id != "")
            {
                coureid = Convert.ToInt32(id);
            }
            var course = from post in db.tbl_Course
                         join meta in db.tblDepartment on post.Dept_id equals meta.Dept_id
                         where post.Course_id == coureid && post.status == true
                         select new { meta.Dept_id, meta.Dept_name };
            var courselist = new SelectList(course, "Dept_id", "Dept_name");
            var coursetypelist = db.tbl_CourseMaster.Where(x => x.Courseid == coureid).FirstOrDefault().CourseType;
            return Json(new { courselist = courselist, coursetypelist = coursetypelist }, JsonRequestBehavior.AllowGet);
        }
    }
}
