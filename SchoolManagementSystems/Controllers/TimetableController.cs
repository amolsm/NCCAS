﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using System.Net.Mail;
using System.Configuration;

namespace SchoolManagementSystems.Controllers
{
    [HandleError]
    [SchoolManagementSystems.MvcApplication.SessionExpire]
    public class TimetableController : Controller
    {
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
        [HttpGet]
        public ActionResult Index(timetableviewmodel _tvm)
        {
            FillPermission(18);
          
            _tvm.timetableclass = db.sp_gettimetableclass(_tvm.Classid, _tvm.department, _tvm.year, _tvm.Day, _tvm.Subjectid, _tvm.Empid).ToList();
            _tvm.YearList = db.tbl_YearMaster.OrderBy(m => m.YearName).ToList();
            _tvm.DepartmentList = db.tblDepartment.OrderBy(m => m.Dept_name).ToList();
            _tvm.Classlist = db.tbl_CourseMaster.OrderBy(m => m.CourseName).ToList();
            _tvm.Emplist = db.sp_getteachers().ToList();
            _tvm.Daylist = db.tbl_days.OrderBy(m => m.dayid).ToList();
            _tvm.subjectlist = db.Sp_GetSubjectName().ToList();
            return View(_tvm);
        }

        public ActionResult NewTimetableInfo(string Search_Data)
        {
            timetableviewmodel _tvm = new timetableviewmodel();
            FillPermission(17);
            _tvm.YearList = db.tbl_YearMaster.OrderBy(m => m.YearName).ToList();
            _tvm.DepartmentList = db.tblDepartment.OrderBy(m => m.Dept_name).ToList();
            _tvm.subjectlist = db.Sp_GetSubjectName().ToList();
            _tvm.Classlist = db.tbl_CourseMaster.OrderBy(m => m.CourseName).ToList();
            _tvm.Emplist = db.sp_getteachers().ToList();
            _tvm.Daylist = db.tbl_days.OrderBy(m => m.dayid).ToList();
            _tvm._Lecturelist = db.tbl_LectTimeSetUp.OrderBy(m => m.LecNo).ToList();
            if (Search_Data == null || Search_Data == "")
                _tvm._Timetablelist = db.sp_gettimetable().OrderBy(m => m.Tid).ToList();
            else
                _tvm._Timetablelist = db.sp_gettimetable().Where(x => x.Classid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.Day.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.Tid.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.LecNo.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.Subjectid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.LecTime.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.LectETime.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.EmpId.ToUpper().Contains(Search_Data.ToUpper())).OrderBy(m => m.Tid).ToList();

            return View(_tvm);
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
            var yeardata = from post in db.tbl_CourseYearMaster
                           join meta in db.tbl_YearMaster on post.academicyear equals meta.yearid
                           where post.courseid == coureid && post.dept_id == detid && post.status == true
                           select new { meta.yearid, meta.YearName };

            return Json(new SelectList(yeardata, "yearid", "YearName"));

        }
        public JsonResult Getdepartment(string id)
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
        public JsonResult GetSubjects(string id,string year,string dept)
        {
            int yearid = 0;
            int Courseid = 0;
            int department = 0;
            if (id != null && id != "" && year != null && year != "")
            {
                Courseid = Convert.ToInt32(id);
                yearid = Convert.ToInt32(year);
                department = Convert.ToInt32(dept);
            }
            var subjects = db.sp_getsubjectcode(Courseid, yearid, department).ToList();
            return Json(new SelectList(subjects, "Subjectid", "SubjectNms"));
        }
      
        public JsonResult FillTimetableInfo(int Tid)
        {
            var data = db.tbl_timetable.Where(m => m.Tid == Tid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_LecNo( int Classid,int yearid,int deptid, int Day,int LecNo,int Subjectid,string semester)
        {
            var data = db.tbl_timetable.Where(m => m.Classid == Classid && m.yearid == yearid && m.deptid == deptid && m.Day == Day && m.LecNo == LecNo && m.Subjectid == Subjectid && m.Semester== semester).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLTimetable(timetableviewmodel _tvm, string evt, int id)
        {
            if (evt == "submit")
            {
                if (_tvm.Tid.ToString() == null) { _tvm.Tid = 0; }

                db.sp_timetable_DML(_tvm.Tid, _tvm.Classid, _tvm.Day, _tvm.LecNo, _tvm.Subjectid, _tvm.semester, _tvm.Empid, _tvm.year, _tvm.department, "",_tvm.SubjectCode).ToString();
            }
        
            else if (evt == "Delete")
            {
                db.sp_timetable_DML(id, _tvm.Classid, _tvm.Day, _tvm.LecNo, _tvm.Subjectid, _tvm.semester, _tvm.Empid, _tvm.year, _tvm.department, "del", _tvm.SubjectCode).ToString();

            }
            _tvm._Timetablelist = db.sp_gettimetable().ToList();
            return PartialView("_Timetablelist", _tvm);
        }

        public ActionResult publish()
        {
            timetableviewmodel _tvm = new timetableviewmodel();
            FillPermission(19);
            _tvm._Timetablelistpivot = new List<sp_gettimetable_pivot_Result>();
            _tvm.Classlist = db.tbl_CourseMaster.ToList();
            _tvm._CourseList = db.sp_GetCoursefordevision().ToList();

            return View(_tvm);
        }

        public ActionResult FillTimetable_classwise(string CourseName)
        {
           
            timetableviewmodel _tvm = new timetableviewmodel();

            _tvm.CourseName = CourseName;
            _tvm._Timetablelistpivot = db.sp_gettimetable_pivot().Where(m => m.CourseName.Trim() == CourseName.Trim()).ToList();
            
            return PartialView("_publishedtimetable", _tvm);
        }

        public void SendTimetable(timetableviewmodel tvm)
        {
            var coursedetails= db.tbl_CourseYearMaster.Where(m => m.id == tvm.Classid).FirstOrDefault();
            tvm.parentemails = db.tbl_student.Where(m => m.Classid == coursedetails.courseid && m.Dept_Id==coursedetails.dept_id && m.courseyearid==coursedetails.academicyear).Select(m => m.FatherEmail).ToList();
            tvm.teacheremails = db.sp_getteacheremail(coursedetails.courseid, coursedetails.dept_id,coursedetails.academicyear).ToList();
            SendEmails(tvm);
        }

        private void SendEmails(timetableviewmodel tvm)
        {
            foreach (var r in tvm.parentemails)
            {

                try
                {

                    SmtpClient smtpClient = new SmtpClient();

                    MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"].ToString(), ConfigurationManager.AppSettings["SenderName"].ToString());
                    MailAddress to = new MailAddress(r);
                    MailMessage message = new System.Net.Mail.MailMessage(fromAddress, to);
                    message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    message.SubjectEncoding = System.Text.Encoding.GetEncoding("utf-8");

                    message.Subject = "Welcome Email from Nanjil college ***** TESTING ONLY *****";
                    message.Priority = MailPriority.High;
                    message.IsBodyHtml = true;

                    string msg = "<b>“Welcome to Nanjil Catholic College of Arts & Science”</b><br/><br/>";
                    msg = msg + "Here We would like to inform you," + tvm.CourseName + " timetable is published. Please see more details on below url.<br/>";
                    msg = msg + "http://ims.24hourdesignstudio.net";
                    msg = msg + "Hope we would be going long term relationship with feature with good Support<br/><br/>Best Regards<br/>NACCAS Team";
                    message.Body = msg;
                    smtpClient.Send(message);
                }
                catch { }
            }

            foreach (var t in tvm.teacheremails)
                {
                    try
                    {
                        SmtpClient smtpClient = new SmtpClient();

                        MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"].ToString(), ConfigurationManager.AppSettings["SenderName"].ToString());
                        MailAddress to = new MailAddress(t.Emailid);
                        MailMessage message = new System.Net.Mail.MailMessage(fromAddress, to);
                        message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                        message.SubjectEncoding = System.Text.Encoding.GetEncoding("utf-8");

                        message.Subject = "Welcome Email from Nanjil college ***** TESTING ONLY *****";
                        message.Priority = MailPriority.High;
                        message.IsBodyHtml = true;
                        string msg = "<b>“Welcome to Nanjil Catholic College of Arts & Science”</b><br/><br/>";
                        msg = msg + "Here We would like to inform you, " + tvm.CourseName + " timetable is published. Please see more details on below url.<br/>";
                        msg = msg + "http://ims.24hourdesignstudio.net";
                        msg = msg + "Hope we would be going long term relationship with feature with good Support<br/><br/>Best Regards<br/>NACCAS Team";
                        message.Body = msg;
                        smtpClient.Send(message);
                    }
                    catch { }
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
    }
}
