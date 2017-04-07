using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using System.Globalization;
using System.Net.Mail;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Text;

namespace SchoolManagementSystems.Controllers
{
    //[HandleError]
    [SchoolManagementSystems.MvcApplication.SessionExpire]
    public class ExaminationController : Controller
    {
        //
        // GET: /Examination/
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExamTimeTable(int? Examid)
        {
            //List<string> yr = new List<string>();            
            Examinationviewmodel _evm = new Examinationviewmodel();
            FillPermission(13);
            if (Examid != null)
            {
                var data = db.tbl_ExamTImeTable.Where(x=>x.ExamId== Examid).FirstOrDefault();
                _evm.ExaminationTypeList = db.tbl_ExaminationType.Where(x => x.Status == true).ToList();
                _evm.SubjectList = db.tbl_subject.Where(x => x.Status == true).ToList();
                _evm._Courselist = db.tbl_CourseMaster.Where(x => x.Status == true).ToList();
                _evm._Deptlist = db.tblDepartment.Where(x => x.Status == true).ToList();
                _evm._yearlist = db.tbl_YearMaster.Where(x => x.Status == true).ToList();
                _evm.Examid = data.ExamId;
                _evm.CourseId = Convert.ToInt32(data.courseid);
                _evm.ExaminationName =data.ExamName;
                _evm.DeptId = Convert.ToInt32(data.deptid);
                _evm.yearid = Convert.ToInt32(data.yearid);
                _evm.IsActive = data.IsActive??false;
                _evm.ExaminationTypeid = data.ExamTypeId;
                var subjectdata = db.sp_GetSubjectlistEditbyExamId(Examid);
                ViewData["subjectlist"] = subjectdata;
               
                
            }
            else
            {
                _evm.AccadmicYear = GetYear();
                _evm.ExaminationTypeList = db.tbl_ExaminationType.Where(x => x.Status == true).ToList();
                _evm.SubjectList = db.tbl_subject.Where(x => x.Status == true).ToList();
                _evm._Courselist = db.tbl_CourseMaster.Where(x => x.Status == true).ToList();
                _evm._Deptlist = db.tblDepartment.Where(x => x.Status == true).ToList();
                _evm._yearlist = db.tbl_YearMaster.Where(x => x.Status == true).ToList();
            }
            return View(_evm);
        }

        public JsonResult GetSubject(string id)
        {
            int examid = 0;
            int subid = Convert.ToInt32(Session["Genid"].ToString());
            if (id != null && id != "")
            {
                examid = Convert.ToInt32(id);
            }
            var subjects = from post in db.tbl_ExamTimetableSubject
                         join meta in db.tbl_subject on post.SubjectId equals meta.Subjectid
                         join theta in db.tbl_teachersubject on post.SubjectId equals theta.subjectid
                         where post.ExamId == examid && theta.teacherid== subid
                           select new { meta.Subjectid, meta.SubjectNm };
            return Json(new SelectList(subjects, "Subjectid", "SubjectNm"));
        }

        public JsonResult GetSubjectForExamtimetable(int CourseId, int deptid , int yearid)
        {
            var data = db.sp_GetSubjectForExamtimetable(CourseId,deptid,yearid).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


       [HttpPost]
        public ActionResult SubmitExam(Examinationviewmodel evm, string[] subjectdata)
        {
            int? createdby = Convert.ToInt32(Session["Userid"].ToString());
            if (evm.Examid.ToString() != null && evm.Examid != 0)
            {
               
                tbl_ExamTImeTable tbl = db.tbl_ExamTImeTable.Where(x => x.ExamId == evm.Examid).FirstOrDefault();
                db.ObjectStateManager.ChangeObjectState(tbl, System.Data.EntityState.Modified);
                //db.Entry(tbl).State = System.Data.Entity.EntityState.Modified;   
                tbl.courseid = evm.CourseId;
                tbl.deptid = evm.DeptId;
                tbl.yearid = evm.yearid;
                tbl.ExamName = evm.ExaminationName.Trim();
                tbl.ExamTypeId = evm.ExaminationTypeid;
                tbl.ExamId = evm.Examid;
                tbl.IsPublish = false;
                tbl.Createdby = createdby;
                tbl.IsActive = evm.IsActive;
                db.SaveChanges();

                var recordsToDelete = (from c in db.tbl_ExamTimetableSubject where c.ExamId == evm.Examid select c).ToList<tbl_ExamTimetableSubject>();
                if (recordsToDelete.Count > 0)
                {
                    foreach (var record in recordsToDelete)
                    {
                        db.tbl_ExamTimetableSubject.DeleteObject(record);
                        db.SaveChanges();
                    }
                }

               
                string s;


                for (int i = 0; i < subjectdata.Count(); i++)
                {
                    s = subjectdata[i].ToString();
                    string[] s2 = s.ToString().Split(',');
                    string s3;

                    for (int j = 0; j < s2.Count(); j++)
                    {
                        tbl_ExamTimetableSubject _sub = new tbl_ExamTimetableSubject();
                        s3 = s2[j].ToString();
                        string[] s4 = s3.ToString().Split('|');
                        _sub.ExamId = evm.Examid;
                        _sub.SubjectId = Convert.ToInt32(s4[0].ToString());
                        _sub.ExamDate = Convert.ToDateTime(s4[1].ToString());
                        _sub.ExamStartTime = DateTime.ParseExact(s4[2].ToString(), "HH:mm", CultureInfo.InvariantCulture).TimeOfDay;
                        _sub.ExamEndTime = DateTime.ParseExact(s4[3].ToString(), "HH:mm", CultureInfo.InvariantCulture).TimeOfDay;
                        db.tbl_ExamTimetableSubject.AddObject(_sub);
                        db.SaveChanges();
                    }

                }
                TempData["ErrorMessage"] = "Update";
                return RedirectToAction("ExamTimeTable");
            }
            else
            {
                var a = db.tbl_ExamTImeTable.Where(x => x.courseid == evm.CourseId && x.deptid == evm.DeptId &&
                        x.yearid == evm.yearid && x.ExamTypeId == evm.ExaminationTypeid && x.ExamName==evm.ExaminationName.Trim());
                if (a == null || a.Count() == 0)
                {
                    tbl_ExamTImeTable _evm = new tbl_ExamTImeTable();
                    _evm.courseid = evm.CourseId;
                    _evm.deptid = evm.DeptId;
                    _evm.yearid = evm.yearid;
                    _evm.ExamTypeId = evm.ExaminationTypeid;
                    _evm.ExamName = evm.ExaminationName;
                    _evm.IsPublish = false;
                    _evm.IsActive = evm.IsActive;
                    _evm.Createdby = createdby;
                    db.tbl_ExamTImeTable.AddObject(_evm);
                    db.SaveChanges();

                    int examids=db.tbl_ExamTImeTable.Where(x => x.courseid == evm.CourseId && x.deptid == evm.DeptId &&
                     x.yearid == evm.yearid && x.ExamTypeId == evm.ExaminationTypeid && x.ExamName == evm.ExaminationName.Trim()).Select(x=>x.ExamId).SingleOrDefault();
                    string s;
                 
                    
                    for (int i = 0; i < subjectdata.Count(); i++)
                    {
                        s = subjectdata[i].ToString();
                        string[] s2 = s.ToString().Split(',');
                        string s3;
                      
                        for (int j = 0; j < s2.Count(); j++)
                        {
                            tbl_ExamTimetableSubject _sub = new tbl_ExamTimetableSubject();
                            s3 = s2[j].ToString();
                            string[] s4 = s3.ToString().Split('|');
                            _sub.ExamId = examids;
                            _sub.SubjectId = Convert.ToInt32(s4[0].ToString());
                            _sub.ExamDate = Convert.ToDateTime(s4[1].ToString());
                            _sub.ExamStartTime = DateTime.ParseExact(s4[2].ToString(), "HH:mm", CultureInfo.InvariantCulture).TimeOfDay;
                            _sub.ExamEndTime = DateTime.ParseExact(s4[3].ToString(), "HH:mm", CultureInfo.InvariantCulture).TimeOfDay;
                            db.tbl_ExamTimetableSubject.AddObject(_sub);
                            db.SaveChanges();
                        }
                       
                    }

                    TempData["ErrorMessage"] = "Success";
                    return RedirectToAction("ExamTimeTable");

                  
                }
                else
                {
                    TempData["ErrorMessage"] = "Exists";
                    return RedirectToAction("ExamTimeTable");
                }
            }
         

        }
        public JsonResult GetExamSubjectList(int ExamId)
        {
            var data = db.sp_GetSubjectlistbyExamId(ExamId);
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowExamTimeTable(int? Classid, int? ExaminationTypeid, string ExamYear)
        {
            Examinationviewmodel _evm = new Examinationviewmodel();
            FillPermission(14);
            int PClassid = 0, PExaminationTypeid = 0;
            string PExamYear = "";
            if (Classid != null && Classid != 0)
                PClassid = Convert.ToInt32(Classid);
            if (ExaminationTypeid != null && ExaminationTypeid != 0)
                PExaminationTypeid = Convert.ToInt32(ExaminationTypeid);
            if (ExamYear != null)
                PExamYear = Convert.ToString(ExamYear);

            if (PClassid != 0 && PExaminationTypeid != 0 && PExamYear != "")
            {
                _evm.ExaminationTypeList = db.tbl_ExaminationType.Where(x => x.Status == true).ToList();
                _evm.SubjectList = db.tbl_subject.Where(x => x.Status == true).ToList();
                _evm._Courselist = db.tbl_CourseMaster.Where(x => x.Status == true).OrderBy(x=>x.CourseName).ToList();
                _evm._Deptlist = db.tblDepartment.Where(x => x.Status == true).ToList();
                _evm._yearlist = db.tbl_YearMaster.Where(x => x.Status == true).ToList();
                _evm.ExaminationTypeid = PExaminationTypeid;
                _evm.yearid = 0;
            }
            else
            {
                _evm.AccadmicYear = GetYear();
                _evm.ExaminationTypeList = db.tbl_ExaminationType.Where(x => x.Status == true).ToList();
                _evm.SubjectList = db.tbl_subject.Where(x => x.Status == true).ToList();
                _evm._Courselist = db.tbl_CourseMaster.Where(x => x.Status == true).OrderBy(x => x.CourseName).ToList();
                _evm._Deptlist = db.tblDepartment.Where(x => x.Status == true).ToList();
                _evm._yearlist = db.tbl_YearMaster.Where(x => x.Status == true).ToList();
            }
            return View(_evm);
        }

        public JsonResult DisplayExamTimeTable(int? ExamTypeid, int? courseid,int? departmentid,int? yearid)
        {
            var ExaminationDetail = db.tbl_ExamTImeTable.Where(x=>(x.ExamTypeId== ExamTypeid && x.courseid==courseid&&x.deptid==departmentid&&x.yearid==yearid)).ToList();
            return Json(ExaminationDetail, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveExamination(string evt, int id)
        {
            if (id != null && id != 0)
            {
                db.tbl_ExamTImeTable
    .Where(x => x.ExamId == id).ToList()
    .ForEach(ft => db.tbl_ExamTImeTable.DeleteObject(ft));
                //db.tbl_ExamTImeTable.RemoveRange(db.tbl_ExamTImeTable.Where(x => x.ExamId == id));
                db.SaveChanges();
                return RedirectToAction("ShowExamTimeTable");
            }
            else
                return RedirectToAction("ShowExamTimeTable");
        }

        public void SubD(int Student)
        {
            int s = Student;
        }

        public ActionResult SubmitMark(int Studentid, int Subjectid, int ExamId, string Marks, string TotalMarks, string ExamYear, string Remark)
        {
            int PExamid = db.tbl_ExamTImeTable.Where(x => x.ExamId == ExamId).FirstOrDefault().ExamId;
            int subid = Convert.ToInt32(Session["Genid"].ToString());
            var Markcheck = db.tbl_StudentMark.Where(x => x.Examid == PExamid && x.Studentid == Studentid && x.Subjectid == Subjectid);
            if (Markcheck.Count() == 0 || Markcheck == null)
            {
                
                tbl_StudentMark sm = new tbl_StudentMark();
                sm.Studentid = Studentid;
                sm.Subjectid = Subjectid;
                sm.Examid = PExamid;
                sm.AcadamicYear = ExamYear;
                sm.Marks = Convert.ToDecimal(Marks);
                sm.TotalMarks = Convert.ToDecimal(TotalMarks);
                sm.Createdby = subid;
                db.tbl_StudentMark.AddObject(sm);
                db.SaveChanges();
            }
            else
            {
                tbl_StudentMark sm = db.tbl_StudentMark.Where(x => x.Marksid == Markcheck.FirstOrDefault().Marksid).FirstOrDefault();
                sm.Marksid = Markcheck.FirstOrDefault().Marksid;
                sm.Marks = Convert.ToDecimal(Marks);
                sm.TotalMarks = Convert.ToDecimal(TotalMarks);
                if (Remark == "true")
                    sm.Remark = true;
                else
                    sm.Remark = false;
                sm.Createdby = subid;
                db.SaveChanges();
            }
            return RedirectToAction("ShowExamTimeTable");
        }

        public ActionResult ShowMarks(int Classid, int ExamTypeid, string ExamYear)
        {
            List<string> yr = new List<string>();
            yr.Add(ExamYear);
            MarkAllocation _ma = new MarkAllocation();
            _ma.StudentDetail = db.tbl_student.Where(x => x.Classid == Classid && x.academicyear == ExamYear).ToList();
            _ma.Classname = db.tbl_class.Where(x => x.Classid == Classid).FirstOrDefault().Classnm;
            _ma.ExamYear = yr;
            _ma.ExaminationTypeid = ExamTypeid;
            _ma.ExamType = db.tbl_ExaminationType.Where(x => x.ExaminationTypeId == ExamTypeid).FirstOrDefault().ExaminationType;
            return View(_ma);
        }

        public JsonResult ShowStudentMarks(int CourseId, int DeptId, int yearid, int subjectid, int Examid  ,string ExamYear)
        {
            var stud = db.sp_ExamMarksAllotment(CourseId, DeptId, yearid, Examid, subjectid, ExamYear);
            return Json(stud, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult PublishExam(Examinationviewmodel evm)
        //{
        //    string yr = evm.AccadmicYear[0].ToString();
        //    var tb = db.tbl_ExamTImeTable.Where(x => x.courseid == evm.Classid && x.ExamTypeId == evm.ExaminationTypeid && x.yearid == 0 && (x.IsPublish == false || x.IsPublish == null));
        //    if (tb != null && tb.Count() > 0)
        //    {
        //        string host = Request.Url.AbsoluteUri;
        //        string[] url = host.Split('/');
        //        string Url = "http://" + url[2] + "/Examination/ShowExamTimeTable?Classid=" + evm.Classid + "&ExaminationTypeid=" + evm.ExaminationTypeid + "&ExamYear=" + yr;
        //        string ExamType = db.tbl_ExaminationType.Where(x => x.ExaminationTypeId == evm.ExaminationTypeid).FirstOrDefault().ExaminationType;
        //        string ClassName = db.tbl_class.Where(x => x.Classid == evm.Classid).FirstOrDefault().Classnm;
        //        string Description = "Time table for " + ExamType + " of Class " + ClassName + " for Year " + evm.AccadmicYear;

        //        tbl_Event ev = new tbl_Event();
        //        ev.EventDescription = Description;
        //        ev.EventUrl = Url;
        //        db.tbl_Event.AddObject(ev);
        //        db.SaveChanges();
        //        db.sp_PublishExamination(evm.ExaminationTypeid, evm.Classid, yr);
        //        var data = from a in db.tbl_student.Where(x => x.Classid == evm.Classid) select a;
        //        foreach (var d in data)
        //        {
        //            SendEmails(d.FatherEmail, Url);
        //        }

        //        return RedirectToAction("ShowExamTimeTable");
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = "Exam time table is already published!!";
        //        return RedirectToAction("ShowExamTimeTable");
        //    }

        //}

        public ActionResult PublishMarks(MarkAllocation ma)
        {

            string yr = ma.ExamYear[0].ToString();
            string host = Request.Url.AbsoluteUri;
            string[] url = host.Split('/');
            string Url = "http://" + url[2] + "/Examination/ShowMarks?CourseId=" + ma.CourseId + "&DeptId="+ma.DeptId+"&yearid="+ma.yearid+"&Examid"+ma.Examid+"&Examtypeid" + ma.ExaminationTypeid + "&ExamYear=" + yr;
            string ExamType = db.tbl_ExaminationType.Where(x => x.ExaminationTypeId == ma.ExaminationTypeid).FirstOrDefault().ExaminationType;
            string Course = db.tbl_CourseMaster.Where(x => x.Courseid == ma.CourseId).FirstOrDefault().CourseName;
            string Department = db.tblDepartment.Where(x => x.Dept_id == ma.DeptId).FirstOrDefault().Dept_name;
            string year = db.tbl_YearMaster.Where(x => x.yearid == ma.yearid).FirstOrDefault().YearName;
            string Description = "Marks declaration for " + ExamType + " of Course " + Course + " for Department of " +Department+""+ year+"for accadmic year " + yr;
            var tb = db.tbl_Event.Where(x => x.EventUrl == Url && x.EventDescription == Description);
            if (tb == null || tb.Count() == 0)
            {
                tbl_Event ev = new tbl_Event();
                ev.EventDescription = Description;
                ev.EventUrl = Url;
                db.tbl_Event.AddObject(ev);
                db.SaveChanges();

                var data = from a in db.tbl_student.Where(x => (x.Classid == ma.CourseId && x.Dept_Id==ma.DeptId&&x.courseyearid==ma.yearid&&x.academicyear==yr)) select a;
                foreach (var d in data)
                {
                    SendEmails(d.FatherEmail, Url);
                }

                return RedirectToAction("ExamTimeTable");
            }
            else
            {
                TempData["ErrorMessage"] = "Marks is already published!!";
                return RedirectToAction("ExamMarksAllotment");
            }

        }

        public JsonResult StudentMarkDetail(int Classid, int ExaminationTypeid, int Subjectid, string ExamYear)
        {
            int PExamid = db.tbl_ExamTImeTable.Where(x => x.yearid == 0 && x.ExamTypeId == ExaminationTypeid && x.courseid == Classid).Select(m => m.ExamId).FirstOrDefault();
            var ExaminationDetail = db.sp_StudentMarks(PExamid).ToList();
            return Json(ExaminationDetail, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExamMarksAllotment(int? id)
        {
            MarkAllocation ma = new MarkAllocation();
            FillPermission(15);
            ma.AccadmicYear = GetYear();
            ma.ExaminationTypeList = db.tbl_ExaminationType.Where(x => x.Status == true).ToList();
            ma.SubjectList = db.tbl_subject.Where(x => x.Status == true).ToList();
            ma._Courselist = db.tbl_CourseMaster.Where(x => x.Status == true).ToList();
            ma._Deptlist = db.tblDepartment.Where(x => x.Status == true).ToList();
            ma._yearlist = db.tbl_YearMaster.Where(x => x.Status == true).ToList();
            ma.ExamYear = GetYear();
            ma.ExamTable = db.tbl_ExamTImeTable.Where(x => (x.IsPublish == true && x.IsActive == true)).ToList();
        
            return View(ma);
        }


        private void SendEmails(string Email, string reason)
        {
            SmtpClient smtpClient = new SmtpClient();
        
            MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"].ToString(), ConfigurationManager.AppSettings["SenderName"].ToString());
            MailAddress to = new MailAddress(Email);
            MailMessage message = new System.Net.Mail.MailMessage(fromAddress, to);
            message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
            message.SubjectEncoding = System.Text.Encoding.GetEncoding("utf-8");
            //message.From = fromAddress;
            //message.To.Add(Email);
            message.Subject = "A-One School Management";
            message.Priority = MailPriority.High;
            message.IsBodyHtml = true;

            string msg = "<b>“Welcome to A-One School Management Systems”</b><br/><br/>";
            msg = msg + "Here We would like to inform you, Examination timetable is published. Please see more details on below url.<br/>";
            msg = msg + reason + "<br/>";
            msg = msg + "Hope we would be going long term relationship with feature with good Support<br/><br/>Best Regards<br/>A-One School Team";
            message.Body = msg;
            smtpClient.Send(message);
        }
        public List<string> GetYear()
        {
            string[] Years = new string[10];
            int curYear = DateTime.Now.Year;
            int j = 0;
            for (int i = 4; i >= 0; i--)
            {
                Years[j] = (curYear - i - 1) + "-" + (curYear - i);
                j = j + 1;
            }
            for (int i = 0; i < 5; i++)
            {
                Years[j] = (curYear + i) + "-" + (curYear + i + 1);
                j = j + 1;
            }
            return Years.ToList();
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
            return Json(new SelectList(course, "Dept_id", "Dept_name"));
        }
        public JsonResult GetCourseYear(string courseid, string deptid)
        {
            int coureid = 0;
            int detid = 0;
            if (courseid != null && courseid != "" && deptid != null && deptid != "")
            {
                coureid = Convert.ToInt32(courseid);
                detid = Convert.ToInt32(deptid);
            }
            var yeardata = from post in db.tbl_CourseYearMaster
                           join meta in db.tbl_YearMaster on post.academicyear equals meta.yearid
                           where post.courseid == coureid && post.dept_id == detid && post.status == true
                           select new { meta.yearid, meta.YearName };

            return Json(new SelectList(yeardata, "yearid", "YearName"));
        }

        public JsonResult PublishExam(int Examid)
          {
            var examdetails = db.sp_GetExamDetailsByExamId(Examid).FirstOrDefault();
            var Subjectdetails  = db.sp_GetSubjectlistbyExamId(Examid);
            string result = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head></head>");
            sb.Append("<table style='font-family: verdana,arial,sans-serif;font-size: 11px;color: #333333;border-width: 1px;border-color: #666666;border-collapse: collapse;'>");
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th align='center' colspan='5' style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>");
            sb.Append("<label style ='font -weight: bold;' id = 'lbllogo'> Nanjil Catholic College Of Arts &amp; Science </label>");
            sb.Append("<br/>");
            sb.Append("<label style = 'font-weight: bold;' id = 'lblheaddata'>" + examdetails.CourseName.ToString()+ " Department Of "+ examdetails.Dept_name.ToString()+ " "+ examdetails.ExamName.ToString()+" "+examdetails.YearName.ToString()+ " Examination Time Table</label>");
            sb.Append("<br/>");
            sb.Append("</th>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<th align = 'left' style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Sub.Code</th>");
            sb.Append("<th align = 'left' style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Subject</th>");
            sb.Append("<th align = 'left' style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>Date</th>");
            sb.Append("<th align = 'left' style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>StartTime</th>");
            sb.Append("<th align = 'left' style='border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #dedede;'>EndTime</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");

            foreach (var item in Subjectdetails)
            {
                sb.Append("<tr id=" + item.ExSubId + ">");
                sb.Append("<td  style='width:10%; border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>" + item.subjectcode + "</td>");
                sb.Append("<td  style='width:50%;border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>" + item.SubjectNm + "</td>");
                sb.Append("<td  style='width:20%;border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>" + item.ExamDate + "</td>");
                sb.Append("<td style='width:10%;border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>" + item.ExamStartTime + "</td>");
                sb.Append("<td  style='width:10%;border-width: 1px;padding: 8px;border-style: solid;border-color: #666666;background-color: #ffffff;'>" + item.ExamEndTime + "</td>");
             }
            sb.Append("</tbody>");
            sb.Append("</table>");
            result = sb.ToString();
            var exdetails = db.tbl_ExamTImeTable.Where(x=>x.ExamId==Examid).FirstOrDefault();
            int resultcount =SendUserEmails(exdetails.courseid, exdetails.deptid,exdetails.yearid,result);
            tbl_ExamTImeTable tbl = db.tbl_ExamTImeTable.Where(x => x.ExamId == Examid).FirstOrDefault();
            db.ObjectStateManager.ChangeObjectState(tbl, System.Data.EntityState.Modified);
            tbl.IsPublish = true;
            db.SaveChanges();
            return Json(resultcount, JsonRequestBehavior.AllowGet);
        }
        public int SendUserEmails(int? courseid,int? deptid,int? yearid,string messageresult)
        {
            int resultcount;
            var emails = db.tbl_student.Where(x => (x.Classid ==courseid && x.Dept_Id==deptid && x.courseyearid==yearid)).Select(x=>x.StudEmail).ToList();
            foreach (var r in emails)
            {
                SmtpClient smtpClient = new SmtpClient();

                MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"].ToString(), ConfigurationManager.AppSettings["SenderName"].ToString());
                MailAddress to = new MailAddress(r);
                MailMessage message = new System.Net.Mail.MailMessage(fromAddress, to);
                message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                message.SubjectEncoding = System.Text.Encoding.GetEncoding("utf-8");
                message.From = fromAddress;
                message.To.Add(r);
                message.Subject = "Nanjil Catholic College of Arts & Science";
                message.Priority = MailPriority.High;
                message.IsBodyHtml = true;
                string msg = messageresult;
                msg = msg + "<br/><br/>Hope we would be going long term relationship with feature with good Support<br/><br/>Best Regards<br/>NACCAS Management";
                message.Body = msg;
                smtpClient.Send(message);
            }
            resultcount= emails.Count();
            return resultcount;
        }
    }
}
