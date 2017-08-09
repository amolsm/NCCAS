using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using System.Net.Mail;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Collections.Specialized;

namespace SchoolManagementSystems.Controllers
{
    [HandleError]
    [SchoolManagementSystems.MvcApplication.SessionExpire]
    public class AttendanceController : Controller
    {
        //
        // GET: /Attendance/
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Staff()
        {
            Attendanceviewmodel _avm = new Attendanceviewmodel();
            _avm.Employeelist = db.tbl_employee.ToList();
            return View(_avm);
            //ViewBag.errormessage = "";
            //Attendanceviewmodel _avm = new Attendanceviewmodel();
            //FillPermission(8);
            ////_avm.Classlist = db.tbl_class.Where(c => c.status == true).ToList();
            //if (Search_Data == null || Search_Data == "")
            //    _avm.Employeelist = db.tbl_employee.OrderBy(m => m.Empid).ToList();
            //else
            //    _avm.Employeelist = db.tbl_employee.Where(x => x.FirstName.ToUpper().Contains(Search_Data.ToUpper()) ||
            //                                            x.MiddleName.ToUpper().Contains(Search_Data.ToUpper()) ||
            //                                            x.LastName.ToUpper().Contains(Search_Data.ToUpper())).OrderBy(m => m.Empid).ToList();

            //_avm.Leavelist = db.tbl_leave.Where(x => x.status == true).ToList();

            //return View(_avm);
        }
        public JsonResult GetStudentsSearchData(string id, string Search_Data)
        {
            int Classid = 0;
            if (id != null && id != "")
            {
                Classid = Convert.ToInt32(id);
            }
            Studentviewmodel svm = new Studentviewmodel();
            //var students = db.tbl_student.Where(m => m.Classid == Classid).ToList();
            if (Search_Data == null || Search_Data == "")
                svm.StudentDataCollection = db.sp_GetStudentDataCollection().OrderBy(m => m.Studid).ToList();
            else
                svm.StudentDataCollection = db.sp_GetStudentDataCollection().OrderBy(m => m.Studid).Where(x => x.Studnm.ToUpper().Contains(Search_Data.ToUpper())
                                                                                        || x.StdMobNo.ToUpper().Contains(Search_Data.ToUpper())
                                                                                        || x.StudEmail.ToUpper().Contains(Search_Data.ToUpper())
                                                                                        || x.YearName.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            var students = svm.StudentDataCollection.Where(x => x.Classid == Classid).ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStudents(string id, string id2, string id3)
        {
            int Classid = 0;
            int DepartmentId = 0;
            int yearid = 0;
            if (id != null && id != "" && id2 != null && id2 != "" && id3 != null && id3 != "")
            {
                DepartmentId = Convert.ToInt32(id3);
                yearid = Convert.ToInt32(id2);
                Classid = Convert.ToInt32(id);
            }
            var students = db.tbl_student.Where(m => (m.Classid == Classid && m.Dept_Id == DepartmentId && m.courseyearid == yearid)).Select(m => new { m.Studid, m.Studnm, m.Studfathernm, m.Studmothernm, m.StudEmail, m.Classid, m.FatherEmail, m.MotherEmail, m.Countryid, m.Stateid, m.Cityid, m.RollNo }).ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StaffAttendance(Attendanceviewmodel avm, string evt, int id)
        {
            if (evt == "submit")
            {
                var employee = db.tbl_employee.Select(x => x.Empid).ToList();
                var CHECK = db.tbl_StaffAttendance.Where(x => x.AttendanceDate == (avm.StaffAttendanceDate) && employee.Contains(x.StaffId.Value) && x.Absent == null ? true : false != false).ToList();
                if (CHECK.Count > 0)
                {
                    TempData["ErrorMessage"] = "Attendance Already exists!!";
                }
                else
                {
                    db.sp_EmployeeAttendance(avm.StaffAttendanceDate);
                    TempData["ErrorMessage"] = "Attendance Added Successfully!!";
                }
            }
            return RedirectToAction("staff");
        }
        public ActionResult Student()
        {
            ViewBag.errormessage = "";
            Attendanceviewmodel _avm = new Attendanceviewmodel();
            FillPermission(9);
            _avm.YearList = new List<tbl_YearMaster>();
            _avm.DepartmentList = new List<tblDepartment>();
            _avm.SubjectList = new List<tbl_subject>();
            _avm.DivisionList = new List<tbl_division>();
            int Empid;
            int roleid;
            try { Empid = Convert.ToInt32(Session["Genid"].ToString()); } catch { Empid = 0; }
            try { roleid = Convert.ToInt32(Session["Role"].ToString()); } catch { roleid = 0; }
            _avm._courselist = db.sp_getTeacherCourse(Empid, roleid).ToList();
            _avm.SessionList = db.tbl_SessionMaster.OrderBy(m => m.SessionName).ToList();
            _avm._Lecturelist = db.tbl_LectTimeSetUp.OrderBy(m => m.LecNo).ToList();
            return View(_avm);
        }
        public JsonResult GetYearClass(string depid, string cid)
        {
            int coureid = 0;
            int detid = 0;
            if (depid != null && depid != "" && cid != null && cid != "")
            {
                coureid = Convert.ToInt32(cid);
                detid = Convert.ToInt32(depid);
            }
            int Empid;
            int roleid;
            try { Empid = Convert.ToInt32(Session["Genid"].ToString()); } catch { Empid = 0; }
            try { roleid = Convert.ToInt32(Session["Role"].ToString()); } catch { roleid = 0; }
            List<sp_getTeacherCourseYear_Result> yeardata = db.sp_getTeacherCourseYear(Empid, roleid, coureid, detid).ToList();

            return Json(new SelectList(yeardata, "yearid", "YearName"));

        }
        public JsonResult GetDepartment(string id)
        {
            int courseid = 0;
            if (id != null && id != "")
            {
                courseid = Convert.ToInt32(id);
            }

            var DClass = from post in db.tbl_Course
                         join meta in db.tblDepartment on post.Dept_id equals meta.Dept_id
                         where post.Course_id == courseid && post.status == true
                         select new { meta.Dept_id, meta.Dept_name };

            return Json(new SelectList(DClass, "Dept_id", "Dept_name"));
        }


        public JsonResult GetCourseTeacherDepartment(string id)
        {
            int courseid = 0;
            if (id != null && id != "")
            {
                courseid = Convert.ToInt32(id);
            }
            int Empid;
            int roleid;
            try { Empid = Convert.ToInt32(Session["Genid"].ToString()); } catch { Empid = 0; }
            try { roleid = Convert.ToInt32(Session["Role"].ToString()); } catch { roleid = 0; }

            List<sp_getTeacherCourseDepartment_Result> DClass = db.sp_getTeacherCourseDepartment(Empid, roleid, courseid).ToList();

            return Json(new SelectList(DClass, "Dept_id", "Dept_name"));
        }
        public JsonResult GetDivision(string id)
        {
            int classid = 0;
            if (id != null && id != "")
            {
                classid = Convert.ToInt32(id);
            }
            var division = db.tbl_division.Where(m => m.Classid == classid).ToList();
            return Json(new SelectList(division, "Divisionid", "DivisionName"));
        }
        public JsonResult GetSubject(string id, string dept, string year)
        {
            int yearid = 0;
            int Courseid = 0;
            int deptid = 0;
            if (id != null && id != "" && dept != null && dept != "" && year != null && year != "")
            {
                Courseid = Convert.ToInt32(id);
                yearid = Convert.ToInt32(year);
                deptid = Convert.ToInt32(dept);
            }
           int Empid = Convert.ToInt32(Session["Genid"].ToString());
           int roleid = Convert.ToInt32(Session["Role"].ToString());
           
            var subject = db.sp_GetTeacherSubject(Empid, roleid, Courseid, deptid, yearid).ToList();
            return Json(new SelectList(subject, "Subjectid", "SubjectNm"));
        }
        //public ActionResult GetYear(int dept_id, int courseid,int academicyear)
        //{ 
        //    //Attendanceviewmodel _avm = new Attendanceviewmodel();
        //    //_avm.tblcourse = new List<tbl_CourseYearMaster>();
        //    // _avm.YearList = new List<tbl_YearMaster>();
        //    // tbl_CourseYearMaster yClass = db.tbl_CourseYearMaster.Where(m => m.dept_id == deptid && m.courseid == courseid).FirstOrDefault();
        //    //var data1 = db.tbl_YearMaster.Where(m => m.yearid == academicyear).ToList();
        //    //return Json(new SelectList(data1, "yearid", "YearName"));
        //    //var query = from c in db.tbl_CourseYearMaster
        //    //            join o in db.tbl_YearMaster on c.academicyear equals o.yearid
        //    //            where c.dept_id == dept_id && c.courseid == courseid
        //    //            select new { o.yearid, o.YearName };
        //    //_avm.ylist = new SelectList(query, "yearid", "YearName");

        //    //_avm.year = "NY";
        //    //return View(_avm);
        //    //return Json(new SelectList(query, "yearid", "YearName"));
        //    //var data = db.sp_getyear(dept_id,courseid).FirstOrDefault();
        //    //var data1 = db.tbl_YearMaster.Where(m => m.yearid == academicyear).FirstOrDefault();
        //    //return Json(data1, JsonRequestBehavior.AllowGet);
        //    //_avm.yearlist1 = db.sp_getyear(_avm.department, _avm.Classid).ToList();
        //    //return View(_avm);

        //}
        public ActionResult StudentAttendance(int Studid)
        {
            Attendanceviewmodel _avm = new Attendanceviewmodel();

            ////ViewBag["StudentID"] = 
            _avm.Studentid = Studid;
            _avm.StudentName = db.tbl_student.Where(x => x.Studid == Studid).FirstOrDefault().Studnm;
            return View(_avm);
        }
        [HttpPost]
        public ActionResult StudentAttendance(Attendanceviewmodel avm)
        {
            var CHECK = from a in db.tbl_StudentAttendance.Where(x => x.AttendanceDate == avm.AttendanceDate && x.StudentID == avm.Studentid) select a;
            string Parentsemail = db.tbl_student.Where(x => x.Studid == avm.Studentid).FirstOrDefault().FatherEmail.ToString();
            if (CHECK == null || CHECK.Count() == 0)
            {
                tbl_StudentAttendance sa = new tbl_StudentAttendance();
                sa.StudentID = avm.Studentid;
                sa.AttendanceDate = avm.AttendanceDate;
                sa.Present = avm.StaffAbsent;
                sa.Reason = avm.StaffReason;
                db.tbl_StudentAttendance.AddObject(sa);
                db.SaveChanges();
                SendEmails(Parentsemail, avm.StaffReason);
                return RedirectToAction("Student");
            }
            else
            {
                TempData["ErrorMessage"] = "Attendance already exists";
                //return RedirectToAction("StudentAttendance");
                return RedirectToAction("Student");
            }
        }
        public JsonResult StudentAtt(int id, string r, string date, string Present)
        {
            var CHECK = from a in db.tbl_StudentAttendance.Where(x => x.AttendanceDate == (date) && x.StudentID == id) select a;
            string Parentsemail = db.tbl_student.Where(x => x.Studid == id).FirstOrDefault().FatherEmail.ToString();
            if (CHECK == null || CHECK.Count() == 0)
            {
                tbl_StudentAttendance sa = new tbl_StudentAttendance();
                sa.StudentID = id;
                sa.AttendanceDate = (date);
                sa.Present = Present == "true" ? true : false;
                sa.Reason = r;
                db.tbl_StudentAttendance.AddObject(sa);
                db.SaveChanges();
                SendEmails(Parentsemail, r);
                var students = db.tbl_StudentAttendance.Where(x => x.StudentID == id && x.AttendanceDate == (date)).FirstOrDefault();
                return Json(students, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["ErrorMessage"] = "Attendance already exists";
                //return RedirectToAction("StudentAttendance");
                var students = db.tbl_StudentAttendance.Where(x => x.StudentID == id && x.AttendanceDate == (date)).FirstOrDefault();
                return Json(students, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DMLAttendance(string[] presentdetails)
        {


            tbl_StudentAttendance sa = new tbl_StudentAttendance();
            string s;

            for (int i = 0; i < presentdetails.Count(); i++)
            {
                sa.CreatedBy = Convert.ToInt32(Session["Genid"].ToString());
                s = presentdetails[i].ToString();
                string[] s1 = s.ToString().Split(',');
                sa.StudentID = Convert.ToInt32(s1[0].ToString());
                sa.AttendanceDate = s1[1].ToString();
                sa.Present = Convert.ToBoolean(s1[2].ToString());
                sa.Reason = s1[3].ToString();
                sa.Subjectid = Convert.ToInt32(s1[4].ToString());
                sa.Session = Convert.ToInt32(s1[5].ToString());
                sa.LecttimeId = Convert.ToInt32(s1[6].ToString());
                var dayname = Convert.ToDateTime(s1[1].ToString()).DayOfWeek.ToString();
                var row = db.tbl_days.SingleOrDefault(m=>m.daynm==dayname);
                try { sa.DayId = Convert.ToInt32(row.dayid); } catch { sa.DayId = 0; }


                if (sa.Present == false)
                {
                    SendSMS(sa.StudentID, Convert.ToInt32(sa.Session), Convert.ToDateTime(sa.AttendanceDate));
                }

                try
                {
                    db.sp_Attandance_DML(sa.StudentID, sa.Present, sa.Reason, sa.CreatedBy, sa.AttendanceDate, sa.Subjectid, sa.Session, sa.DayId, sa.LecttimeId);
                }
                catch (Exception ex)
                { string msg = ex.ToString(); }

                db.SaveChanges();

            }
            return Json(presentdetails);


        }
        public JsonResult StudentPresent(int id, string date, string Present)
        {
            var CHECK = from a in db.tbl_StudentAttendance.Where(x => x.AttendanceDate == (date) && x.StudentID == id) select a;
            string Parentsemail = db.tbl_student.Where(x => x.Studid == id).FirstOrDefault().FatherEmail.ToString();
            if (CHECK == null || CHECK.Count() == 0)
            {
                tbl_StudentAttendance sa = new tbl_StudentAttendance();
                sa.StudentID = id;
                sa.AttendanceDate = (date);
                sa.Present = Present == "true" ? true : false;

                db.tbl_StudentAttendance.AddObject(sa);
                db.SaveChanges();

                var students = db.tbl_StudentAttendance.Where(x => x.StudentID == id && x.AttendanceDate == (date)).FirstOrDefault();
                return Json(students, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["ErrorMessage"] = "Attendance already exists";
                //return RedirectToAction("StudentAttendance");
                var students = db.tbl_StudentAttendance.Where(x => x.StudentID == id && x.AttendanceDate == (date)).FirstOrDefault();
                return Json(students, JsonRequestBehavior.AllowGet);
            }
        }
        private void SendEmails(string Email, string reason)
        {
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"].ToString(), ConfigurationManager.AppSettings["SenderName"].ToString());

            message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
            message.SubjectEncoding = System.Text.Encoding.GetEncoding("utf-8");
            message.From = fromAddress;
            message.To.Add(Email);
            message.Subject = "Nanjil Catholic College of Arts & Science";
            message.Priority = MailPriority.High;
            message.IsBodyHtml = true;

            string msg = "<b>“Welcome to Nanjil Catholic College of Arts & Science”</b><br/><br/>";
            msg = msg + "Here We would like to inform you, your child is Present today because of below reason.<br/>";
            msg = msg + reason + "<br/>";
            msg = msg + "Hope we would be going long term relationship with feature with good Support<br/><br/>Best Regards<br/>A-One School Team";
            message.Body = msg;
            smtpClient.Send(message);
        }
        [HttpPost]
        public ActionResult ClassStudentAttendance(Attendanceviewmodel avm)
        {
            var student = db.tbl_student.Where(x => x.Classid == avm.Classid).Select(x => x.Studid).ToList();
            var CHECK = db.tbl_StudentAttendance.Where(x => x.AttendanceDate == (avm.AttendanceDate) && student.Contains(x.StudentID) && x.Present == null ? true : false != false).ToList();
            if (CHECK.Count() > 0)
            {
                TempData["ErrorMessage"] = "Attendance already exists!!";
            }
            else
            {
                db.sp_StudentAttendance(avm.Classid, avm.AttendanceDate);
                TempData["ErrorMessage"] = "Attendance Successfully Added!!";
            }

            return RedirectToAction("Student");
        }
        public ActionResult Leave()
        {
            Attendanceviewmodel _avm = new Attendanceviewmodel();
            FillPermission(10);
            _avm.Employeelist = db.tbl_employee.ToList();
            _avm.Leavelist = db.tbl_leave.Where(x => x.status == true).ToList();
            _avm.AvailableLeaveList = new List<sp_GetAvailableLeave_Result>();
            //_avm.AvailableLeaveList = db.sp_GetAvailableLeave(12).ToList();
            _avm.EmpLeaveDetail = new List<sp_GetEmpLeaveDetails_Result>();
            return View(_avm);
        }
        public ActionResult GetAvailableLeaves(Attendanceviewmodel _avm)
        {
            _avm.AvailableLeaveList = db.sp_GetAvailableLeave(_avm.Empid).ToList();
            _avm.EmpLeaveDetail = db.sp_GetEmpLeaveDetails(_avm.Empid).ToList();
            return PartialView("_leaveList", _avm);
        }
        public JsonResult check_Leave(string Empid, string LeaveTypeid, string Days)
        {
            var data = from d in db.sp_GetAvailableEmpLeave(Convert.ToInt32(Empid)).Where(x => x.leaveid == Convert.ToInt32(LeaveTypeid))
                       select new { Leaves = d.AvailableLeave - Convert.ToInt32(Days) };
            return Json(data.FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ApproveLeave()
        {
            Attendanceviewmodel _avm = new Attendanceviewmodel();
            FillPermission(11);
            _avm.EmployeeLeavelist = db.sp_GetEmployeeLeave().Where(x => x.IsApprove == false).ToList();
            return View(_avm);
        }
        public ActionResult DeleteLeave(int id)
        {
            db.tbl_EmployeeLeave.DeleteObject(db.tbl_EmployeeLeave.Where(x => x.Leaveid == id).SingleOrDefault());
            db.SaveChanges();
            Attendanceviewmodel _avm = new Attendanceviewmodel();
            _avm.EmployeeLeavelist = db.sp_GetEmployeeLeave().Where(x => x.IsApprove == false).ToList();
            return View("ApproveLeave", _avm);
        }
        public ActionResult Approve(int id, int check)
        {
            tbl_EmployeeLeave tb = db.tbl_EmployeeLeave.Where(x => x.Leaveid == id).FirstOrDefault();
            tb.Leaveid = id;
            if (check == 0)
                tb.IsApprove = false;
            else
                tb.IsApprove = true;
            db.SaveChanges();
            return View();
        }
        public JsonResult DMLAttendancestaff(string[] presentdetails)
        {
            tbl_StaffAttendance sa = new tbl_StaffAttendance();
            tbl_EmployeeLeave sa1 = new tbl_EmployeeLeave();
            string s;

            for (int i = 0; i < presentdetails.Count(); i++)
            {
                //sa.CreatedBy = Convert.ToInt32(Session["Userid"].ToString());
                s = presentdetails[i].ToString();
                string[] s1 = s.ToString().Split(',');
                sa.StaffId = Convert.ToInt32(s1[0].ToString());
                sa.AttendanceDate = Convert.ToDateTime(s1[1].ToString());
                sa.InTime = Convert.ToDateTime(s1[2].ToString());
                sa.OutTime = Convert.ToDateTime(s1[3].ToString());
                sa.Reason = s1[4].ToString();
                sa.Absent = Convert.ToBoolean(s1[5].ToString());
                sa1.Empid = Convert.ToInt32(s1[0].ToString());
                sa1.LeaveStartDate = Convert.ToDateTime(s1[1].ToString());
                sa1.LeaveEndDate = Convert.ToDateTime(s1[1].ToString());
                sa1.LeaveType = s1[6].ToString();
                sa1.Description = s1[4].ToString();
                sa1.IsApprove = Convert.ToBoolean(s1[5].ToString());



                try
                {
                    db.sp_StaffAttandance_DML(sa.StaffId, sa.AttendanceDate, sa.InTime, sa.OutTime, sa.Absent, sa.Reason);
                    db.sp_empleave_DML(sa1.Leaveid, sa1.Empid, sa1.LeaveStartDate, sa1.LeaveEndDate, sa1.LeaveType, sa1.Description, sa1.IsApprove);
                }
                catch (Exception ex)
                { string msg = ex.ToString(); }

                db.SaveChanges();

            }
            return Json(presentdetails);


        }
        public JsonResult AbsentEmployee(int id, string r, DateTime date, string Present, string LeaveType)
        {
            var a = db.tbl_StaffAttendance.Where(x => x.StaffId == id && x.AttendanceDate == date);
            var data = from d in db.sp_GetAvailableEmpLeave(Convert.ToInt32(id)).Where(x => x.leaveid == Convert.ToInt32(LeaveType))
                       select new { Leaves = d.AvailableLeave - Convert.ToInt32(1) };
            int leave = Convert.ToInt32(data.FirstOrDefault().Leaves);
            if (leave > 0)
            {
                if (a.Count() > 0)
                {
                    tbl_StaffAttendance tbl = db.tbl_StaffAttendance.Where(x => x.StaffId == id && x.AttendanceDate == date).FirstOrDefault();
                    if (Present == "true")
                    {
                        tbl.Absent = true;
                    }
                    else
                    {
                        tbl.Absent = false;
                    }

                    tbl.Reason = r;
                    db.SaveChanges();
                }
                else
                {
                    tbl_StaffAttendance staff = new tbl_StaffAttendance();
                    staff.StaffId = id;
                    staff.Reason = r;
                    staff.Absent = true;
                    staff.AttendanceDate = Convert.ToDateTime(date);
                    db.tbl_StaffAttendance.AddObject(staff);
                    db.SaveChanges();

                    tbl_EmployeeLeave el = new tbl_EmployeeLeave();
                    el.Empid = id;
                    el.LeaveStartDate = date;
                    el.LeaveEndDate = date;
                    el.LeaveType = LeaveType;
                    el.Description = r;
                    el.IsApprove = true;
                    db.tbl_EmployeeLeave.AddObject(el);
                    db.SaveChanges();
                }
            }
            else
            {
                TempData["ErrorMessage"] = "There is no leave available for Employee";
            }
            var students = db.tbl_StaffAttendance.Where(x => x.StaffId == id && x.AttendanceDate == date).FirstOrDefault();
            return Json(students, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EmployeeLeave(Attendanceviewmodel avm)
        {
            var check = from a in db.tbl_EmployeeLeave.Where(x => x.Empid == avm.Empid && x.LeaveStartDate == avm.LeaveStartDate && x.LeaveEndDate == avm.LeaveEndDate) select a;
            var data = from d in db.sp_GetAvailableEmpLeave(Convert.ToInt32(avm.Empid)).Where(x => x.leaveid == Convert.ToInt32(avm.LeaveTypeid))
                       select new { Leaves = d.AvailableLeave - Convert.ToInt32((avm.LeaveEndDate - avm.LeaveStartDate).TotalDays + 1) };
            int leave = Convert.ToInt32(data.FirstOrDefault().Leaves);
            if (check == null || check.Count() == 0 && leave > 0)
            {
                tbl_EmployeeLeave el = new tbl_EmployeeLeave();
                el.Empid = avm.Empid;
                el.LeaveStartDate = avm.LeaveStartDate;
                el.LeaveEndDate = avm.LeaveEndDate;
                el.LeaveType = avm.LeaveTypeid;
                el.Description = avm.Description;
                db.tbl_EmployeeLeave.AddObject(el);
                db.SaveChanges();

                return RedirectToAction("Staff");
            }
            else
            {
                if (leave > 0)
                {
                    TempData["ErrorMessage"] = "Leave is already exists";
                }
                else
                {
                    TempData["ErrorMessage"] = "Employee has not available leaves!!!";
                }
                return RedirectToAction("Leave");
            }

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
        public ActionResult TeacherSubject()
        {
            ViewBag.errormessage = "";
            teachersubjectmodel _tsvm = new teachersubjectmodel();
            FillPermission(9);
            _tsvm.YearList = db.tbl_YearMaster.ToList();
            _tsvm.Courselist = db.tbl_CourseMaster.ToList();
            _tsvm.SubjectList = new List<tbl_subject>();
            _tsvm.DepartmentList = new List<tblDepartment>();
            _tsvm.Classlist = new List<tbl_class>();
            return View(_tsvm);
        }
        public JsonResult GettClass(string id)
        {
            int deptid = 0;
            if (id != null && id != "")
            {
                deptid = Convert.ToInt32(id);
            }
            var DClass = db.tbl_class.Where(m => m.Dept_id == deptid).ToList();
            return Json(new SelectList(DClass, "Classid", "Classnm"));
        }
        public JsonResult DMLTeachersubject(string[] presentdetails)
        {


            tbl_teachersubject sa = new tbl_teachersubject();
            string s;
            int created = Convert.ToInt32(Session["Userid"].ToString());
            for (int i = 0; i < presentdetails.Count(); i++)
            {
                //sa.CreatedBy = Convert.ToInt32(Session["Userid"].ToString());
                s = presentdetails[i].ToString();
                string[] s1 = s.ToString().Split(',');
                sa.departmentid = Convert.ToInt32(s1[0].ToString());
                sa.courseid = Convert.ToInt32(s1[1].ToString());
                sa.teacherid = Convert.ToInt32(s1[2].ToString());
                sa.yearid = Convert.ToInt32(s1[3].ToString());
                sa.subjectid = Convert.ToInt32(s1[4].ToString());
                sa.createdby = created;
                //sring session =  System.Web.HttpContext.Current.Session["sessionString"]; 
                try
                {
                    db.sp_teachersubject_DML(sa.id, sa.teacherid, sa.courseid, sa.departmentid, sa.yearid, sa.createdby, sa.subjectid);
                }
                catch (Exception ex)
                { string msg = ex.ToString(); }
                db.SaveChanges();

            }
            return Json(presentdetails);


        }
        public JsonResult GettSubject(string id, string dept, string year, teachersubjectmodel avm)
        {

            int yearid = 0;
            int Courseid = 0;
            int deptid = 0;
            if (id != null && id != "" && dept != null && dept != "" && year != null && year != "")
            {
                Courseid = Convert.ToInt32(id);
                yearid = Convert.ToInt32(year);
                deptid = Convert.ToInt32(dept);
            }
            var subject = db.tbl_subject
                .Where(m => (m.Courseid == Courseid && m.DeptId == deptid && m.yearid == yearid))
                .Select(m => new { m.Subjectid, m.SubjectNm, m.Status, m.Marks })
                .ToList();
            //var s = from sub in db.tbl_subject
            //        join p in db.tbl_employee
            //        on sub.
            var teacherl1 = db.tbl_employee.Where(m => m.Typeid == 4).Select(m => new { m.Empid, m.FirstName }).ToList();

            //var subject = new { subject1, teacherl1 };
            return Json(new { sub = subject, teach = teacherl1 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTeachers()
        {
            var teacher = db.tbl_employee.Where(m => m.Typeid == 4).ToList();
            return Json(new SelectList(teacher, "Empid", "FirstName"));
        }
        public ActionResult Test()
        {
            Attendanceviewmodel _avm = new Attendanceviewmodel();
            _avm.Employeelist = db.tbl_employee.ToList();
            return View(_avm);
        }

        public JsonResult GetEmp()
        {
            var students = db.tbl_employee.Select(m => new { m.Empid, m.FirstName }).ToList();
            var leave = db.tbl_leave.Select(m => new { m.leaveid, m.leavename }).ToList();
            return Json(new { sub = students, leaves = leave }, JsonRequestBehavior.AllowGet);
        }

        private string SendSMS(int studentid, int Sessionid, DateTime date)
        {
            string stemailid;
            string mobileno;
            string studentname;
            string sessionname;

            tbl_student st = db.tbl_student.Where(x => x.Studid == studentid).FirstOrDefault();
            stemailid = st.StudEmail;
            mobileno = st.StdMobNo;
            studentname = st.Studnm;
            tbl_SessionMaster s = db.tbl_SessionMaster.Where(x => x.ID == Sessionid).FirstOrDefault();
            sessionname = s.SessionName;


            string msg = "Dear Parent,Your Son/Daughter " + studentname + Environment.NewLine;
            msg = msg + "is absent on " + (Convert.ToDateTime(date)).ToString("dd-MM-yyyy") + " for " + sessionname + Environment.NewLine;
            msg = msg + "NACCAS";


            String message = HttpUtility.UrlEncode(msg);
            using (var wb = new WebClient())
            {
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

            public JsonResult GetLecture(string sessionid)
            {
                int s_id = 0;
                if (sessionid != null && sessionid != "")
                {
                    s_id = Convert.ToInt32(sessionid);
                }
                var lecturelist = db.tbl_LectTimeSetUp.Where(m => m.SessionId == s_id).ToList();
                return Json(new SelectList(lecturelist, "LecTimeid", "LecNo"));
            }
        }
    }
    

      

    

