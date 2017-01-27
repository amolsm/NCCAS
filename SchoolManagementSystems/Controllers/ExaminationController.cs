using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using System.Globalization;
using System.Net.Mail;
using System.Configuration;

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

        //public ActionResult ExamTimeTable()
        //{
        //    Examinationviewmodel _evm = new Examinationviewmodel();
        //    _evm.ExaminationTypeList = db.tbl_ExaminationType.Where(x => x.Status == true).ToList();
        //    _evm.SubjectList = db.tbl_subject.Where(x => x.Status == true).ToList();
        //    _evm.Classlist = db.tbl_class.Where(x => x.status == true).ToList();
        //    return View(_evm);
        //}

        public ActionResult ExamTimeTable(int? Examid)
        {
            //List<string> yr = new List<string>();            
            Examinationviewmodel _evm = new Examinationviewmodel();
            FillPermission(13);
            if (Examid != null)
            {
                var data = db.sp_ExaminationDetailbyId(Examid).FirstOrDefault();
                _evm.ExaminationTypeList = db.tbl_ExaminationType.Where(x => x.Status == true).ToList();
                _evm.SubjectList = db.tbl_subject.Where(x => x.Status == true).ToList();
                _evm.Classlist = db.tbl_class.Where(x => x.status == true).ToList();
                _evm.Examid = data.ExamId;
                _evm.Classid = data.Classid;
                _evm.ExamDate = Convert.ToDateTime(data.ExamDate);
                _evm.ExamEndTime = data.ExamEndDate;
                _evm.ExamStartTime = data.ExamStartDate;
                _evm.ExaminationName = data.ExamName;
                _evm.ExaminationTypeid = data.ExamTypeId;
                _evm.ExamYear = GetYear();
                ViewData["yr"] = data.ExamYear;
                //yr.Add(data.ExamYear);
                _evm.Subjectid = data.Subjectid;
            }
            else
            {
                _evm.ExamYear = GetYear();
                _evm.ExaminationTypeList = db.tbl_ExaminationType.Where(x => x.Status == true).ToList();
                _evm.SubjectList = db.tbl_subject.Where(x => x.Status == true).ToList();
                _evm.Classlist = db.tbl_class.Where(x => x.status == true).ToList();
            }
            return View(_evm);
        }

        public JsonResult GetSubject(string id)
        {
            int subjectid = 0;
            if (id != null && id != "")
            {
                subjectid = Convert.ToInt32(id);
            }
            var subjects = db.tbl_subject.Where(m => m.Classid == subjectid).ToList();
            return Json(new SelectList(subjects, "Subjectid", "SubjectNm"));
        }

        [HttpPost]
        public ActionResult SubmitExam(Examinationviewmodel evm)
        {
            string yr = evm.ExamYear[0].ToString();
            if (evm.Examid != null && evm.Examid != 0)
            {
                tbl_ExamTImeTable tbl = db.tbl_ExamTImeTable.Where(x => x.ExamId == evm.Examid).FirstOrDefault();
                db.ObjectStateManager.ChangeObjectState(tbl, System.Data.EntityState.Modified);
                //db.Entry(tbl).State = System.Data.Entity.EntityState.Modified;   
                tbl.ExamClassId = evm.Classid;
                string[] date = evm.ExamDate.ToString("MM/dd/yyyy").Replace("-", "/").Split(' ');
                DateTime ExamStartDate = DateTime.ParseExact(date[0].ToString() + " " + evm.ExamStartTime, "MM/dd/yyyy hh:mmtt", CultureInfo.InvariantCulture);
                DateTime ExamEndDate = DateTime.ParseExact(date[0].ToString() + " " + evm.ExamEndTime, "MM/dd/yyyy hh:mmtt", CultureInfo.InvariantCulture);
                tbl.ExamStartTime = ExamStartDate;
                tbl.ExamEndTime = ExamEndDate;
                tbl.ExamName = evm.ExaminationName;
                tbl.ExamTypeId = evm.ExaminationTypeid;
                tbl.ExamYear = yr;
                tbl.ExamSubjectId = evm.Subjectid;
                tbl.ExamId = evm.Examid;
                db.SaveChanges();
                return RedirectToAction("ExamTimeTable");
            }
            else
            {
                var a = db.tbl_ExamTImeTable.Where(x => x.ExamClassId == evm.Classid && x.ExamSubjectId == evm.Subjectid &&
                        x.ExamTypeId == evm.ExaminationTypeid && x.ExamYear == yr);
                if (a == null || a.Count() == 0)
                {
                    tbl_ExamTImeTable _evm = new tbl_ExamTImeTable();
                    _evm.ExamClassId = evm.Classid;
                    string[] date = evm.ExamDate.ToString("MM/dd/yyyy").Replace("-", "/").Split(' ');
                    DateTime ExamStartDate = DateTime.ParseExact(date[0].ToString() + " " + evm.ExamStartTime, "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture);
                    DateTime ExamEndDate = DateTime.ParseExact(date[0].ToString() + " " + evm.ExamEndTime, "MM/dd/yyyy hh:mm", CultureInfo.InvariantCulture);
                    _evm.ExamStartTime = ExamStartDate;
                    _evm.ExamEndTime = ExamEndDate;
                    _evm.ExamName = evm.ExaminationName;
                    _evm.ExamTypeId = evm.ExaminationTypeid;
                    _evm.ExamYear = yr;
                    _evm.ExamSubjectId = evm.Subjectid;
                    db.tbl_ExamTImeTable.AddObject(_evm);
                    db.SaveChanges();
                    return RedirectToAction("ExamTimeTable");
                }
                else
                {
                    TempData["ErrorMessage"] = "Exam is already exists for the Subject, Class and Academic year";
                    return RedirectToAction("ExamTimeTable");
                }
            }

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
                _evm.Classlist = db.tbl_class.Where(x => x.status == true).ToList();
                _evm.Classid = PClassid;
                _evm.ExaminationTypeid = PExaminationTypeid;
                List<string> yr = new List<string>();
                yr.Add(PExamYear);
                _evm.ExamYear = yr;
            }
            else
            {
                _evm.ExamYear = GetYear();
                _evm.ExaminationTypeList = db.tbl_ExaminationType.Where(x => x.Status == true).ToList();
                _evm.SubjectList = db.tbl_subject.Where(x => x.Status == true).ToList();
                _evm.Classlist = db.tbl_class.Where(x => x.status == true).ToList();
            }
            return View(_evm);
        }

        public JsonResult DisplayExamTimeTable(int Classid, int ExamTypeid, string ExamYear)
        {
            int ExamClassid = 0, PExamTypeid = 0;
            string PExamYear = "";
            if (Classid != null && Classid != 0)
                ExamClassid = Convert.ToInt32(Classid);
            if (ExamTypeid != null && ExamTypeid != 0)
                PExamTypeid = Convert.ToInt32(ExamTypeid);
            if (ExamYear != null && ExamYear != "")
                PExamYear = ExamYear;
            var ExaminationDetail = db.sp_ExaminationDetail(PExamTypeid, ExamClassid, PExamYear).ToList();
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

        public ActionResult SubmitMark(int Studentid, int Subjectid, int ExaminationTypeid, int Classid, string Marks, string TotalMarks, string ExamYear, string Remark)
        {
            int PExamid = db.tbl_ExamTImeTable.Where(x => x.ExamYear == ExamYear && x.ExamTypeId == ExaminationTypeid && x.ExamClassId == Classid && x.ExamSubjectId == Subjectid).FirstOrDefault().ExamId;

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

        public JsonResult ShowStudentMarks(int Classid, int ExamTypeid, string ExamYear, int Studentid)
        {
            var stud = db.sp_GetStudentMarks(Classid, ExamTypeid, ExamYear, Studentid);
            return Json(stud, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PublishExam(Examinationviewmodel evm)
        {
            string yr = evm.ExamYear[0].ToString();
            var tb = db.tbl_ExamTImeTable.Where(x => x.ExamClassId == evm.Classid && x.ExamTypeId == evm.ExaminationTypeid && x.ExamYear == yr && (x.IsPublish == false || x.IsPublish == null));
            if (tb != null && tb.Count() > 0)
            {
                string host = Request.Url.AbsoluteUri;
                string[] url = host.Split('/');
                string Url = "http://" + url[2] + "/Examination/ShowExamTimeTable?Classid=" + evm.Classid + "&ExaminationTypeid=" + evm.ExaminationTypeid + "&ExamYear=" + yr;
                string ExamType = db.tbl_ExaminationType.Where(x => x.ExaminationTypeId == evm.ExaminationTypeid).FirstOrDefault().ExaminationType;
                string ClassName = db.tbl_class.Where(x => x.Classid == evm.Classid).FirstOrDefault().Classnm;
                string Description = "Time table for " + ExamType + " of Class " + ClassName + " for Year " + evm.ExamYear;

                tbl_Event ev = new tbl_Event();
                ev.EventDescription = Description;
                ev.EventUrl = Url;
                db.tbl_Event.AddObject(ev);
                db.SaveChanges();
                db.sp_PublishExamination(evm.ExaminationTypeid, evm.Classid, yr);
                var data = from a in db.tbl_student.Where(x => x.Classid == evm.Classid) select a;
                foreach (var d in data)
                {
                    SendEmails(d.FatherEmail, Url);
                }

                return RedirectToAction("ShowExamTimeTable");
            }
            else
            {
                TempData["ErrorMessage"] = "Exam time table is already published!!";
                return RedirectToAction("ShowExamTimeTable");
            }

        }

        public ActionResult PublishMarks(MarkAllocation ma)
        {

            string yr = ma.ExamYear[0].ToString();
            string host = Request.Url.AbsoluteUri;
            string[] url = host.Split('/');
            string Url = "http://" + url[2] + "/Examination/ShowMarks?Classid=" + ma.Classid + "&ExamTypeid=" + ma.ExaminationTypeid + "&ExamYear=" + yr;
            string ExamType = db.tbl_ExaminationType.Where(x => x.ExaminationTypeId == ma.ExaminationTypeid).FirstOrDefault().ExaminationType;
            string ClassName = db.tbl_class.Where(x => x.Classid == ma.Classid).FirstOrDefault().Classnm;
            string Description = "Marks declaration for " + ExamType + " of Class " + ClassName + " for Year " + ma.ExamYear;
            var tb = db.tbl_Event.Where(x => x.EventUrl == Url && x.EventDescription == Description);
            if (tb == null || tb.Count() == 0)
            {
                tbl_Event ev = new tbl_Event();
                ev.EventDescription = Description;
                ev.EventUrl = Url;
                db.tbl_Event.AddObject(ev);
                db.SaveChanges();

                var data = from a in db.tbl_student.Where(x => x.Classid == ma.Classid) select a;
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
            int PExamid = db.tbl_ExamTImeTable.Where(x => x.ExamYear == ExamYear && x.ExamTypeId == ExaminationTypeid && x.ExamClassId == Classid && x.ExamSubjectId == Subjectid).Select(m => m.ExamId).FirstOrDefault();
            var ExaminationDetail = db.sp_StudentMarks(PExamid).ToList();
            return Json(ExaminationDetail, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExamMarksAllotment(int? id)
        {
            MarkAllocation ma = new MarkAllocation();
            FillPermission(15);
            ma.ExamYear = GetYear();
            ma.ExamTable = db.tbl_ExamTImeTable.Where(x => x.IsPublish == true).Distinct().ToList();
            ma.ExaminationTypeList = db.tbl_ExaminationType.Where(x => x.Status == true).ToList();
            ma.SubjectList = db.tbl_subject.Where(x => x.Status == true).ToList();
            ma.Classlist = db.tbl_class.Where(x => x.status == true).ToList();
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
    }
}
