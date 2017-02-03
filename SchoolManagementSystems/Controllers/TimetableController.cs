using System;
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
        public ActionResult Index(string Search_Data)
        {
            timetableviewmodel _tvm = new timetableviewmodel();
            FillPermission(18);
            if (Search_Data == null || Search_Data == "")
                _tvm._Timetablelist = db.sp_gettimetable().OrderBy(m => m.Tid).ToList();
            else
                _tvm._Timetablelist = db.sp_gettimetable().Where(x => x.Classid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.Day.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.Tid.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.LecNo.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.Subjectid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.LecTime.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.LecETime.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.Empid.ToUpper().Contains(Search_Data.ToUpper())).OrderBy(m => m.Tid).ToList();

            return View(_tvm);
        }

        public ActionResult NewTimetableInfo(int? Tid, string Search_Data)
        {
            timetableviewmodel _tvm = new timetableviewmodel();
            FillPermission(17);
            _tvm.Classlist = db.tbl_class.Where(c => c.status == true).ToList();
            _tvm.subjectlist = new List<tbl_subject>();
            _tvm.Emplist = db.sp_getteachers().ToList();
            _tvm.Daylist = db.tbl_days.ToList();
            if (Tid != null)
            {
                var data = db.tbl_timetable.Where(m => m.Tid == Tid).FirstOrDefault();
                if (data != null)
                {
                    _tvm.Tid = data.Tid;
                    _tvm.Classid = data.Classid;
                    _tvm.Day = data.Day;
                    _tvm.LecNo = data.LecNo;
                    _tvm.Subjectid = data.Subjectid;
                    _tvm.LecTime = data.LecTime;
                    _tvm.Empid = data.Empid;
                }
            }
            if (Search_Data == null || Search_Data == "")
                _tvm._Timetablelist = db.sp_gettimetable().OrderBy(m => m.Tid).ToList();
            else
                _tvm._Timetablelist = db.sp_gettimetable().Where(x => x.Classid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.Day.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.Tid.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.LecNo.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.Subjectid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.LecTime.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.LecETime.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.Empid.ToUpper().Contains(Search_Data.ToUpper())).OrderBy(m => m.Tid).ToList();

            return View(_tvm);
        }

        public JsonResult GetSubjects(string id)
        {
            int Courseid = 0;
            if (id != null && id != "")
            {
                Courseid = Convert.ToInt32(id);
            }
            var subjects = db.tbl_subject.Where(m => m.Courseid == Courseid && m.Status == true).ToList();
            return Json(new SelectList(subjects, "Subjectid", "SubjectNm"));
        }
        public JsonResult FillTimetableInfo(int Tid)
        {
            var data = db.tbl_timetable.Where(m => m.Tid == Tid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_LecNo(int LecNo, int Day, int Classid)
        {
            var data = db.tbl_timetable.Where(m => m.LecNo == LecNo && m.Classid == Classid && m.Day == Day).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLTimetable(timetableviewmodel _tvm, string evt, int id)
        {
            if (evt == "submit")
            {
                if (_tvm.Tid == null) { _tvm.Tid = 0; }
                db.sp_timetable_DML(_tvm.Tid, _tvm.Classid, _tvm.Day, _tvm.LecNo, _tvm.Subjectid, _tvm.LecTime, _tvm.LecETime, _tvm.Empid, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_timetable_DML(id, _tvm.Classid, _tvm.Day, _tvm.LecNo, _tvm.Subjectid, _tvm.LecTime, _tvm.LecETime, _tvm.Empid, "del").ToString();
            }
            _tvm._Timetablelist = db.sp_gettimetable().ToList();
            return PartialView("_Timetablelist", _tvm);
        }

        public ActionResult publish()
        {
            timetableviewmodel _tvm = new timetableviewmodel();
            FillPermission(19);
            _tvm._Timetablelistpivot = new List<sp_gettimetable_pivot_Result>();
            _tvm.Classlist = db.tbl_class.Where(c => c.status == true).ToList();
            return View(_tvm);
        }

        public ActionResult FillTimetable_classwise(int Classid)
        {
            string ClassNm = db.tbl_class.Where(m => m.Classid == Classid).Select(m => m.Classnm).FirstOrDefault();
            timetableviewmodel _tvm = new timetableviewmodel();
            //_tvm._Timetablelist = db.sp_gettimetable().Where(m => m.Classid == ClassNm).OrderBy(m => m.Day).ThenBy(m => m.LecNo).ToList();
            _tvm._Timetablelistpivot = db.sp_gettimetable_pivot().Where(m => m.Classnm == ClassNm).ToList();
            return PartialView("_publishedtimetable", _tvm);
        }

        public void SendTimetable(int Classid)
        {
            timetableviewmodel tvm = new timetableviewmodel();
            tvm.parentemails = db.tbl_student.Where(m => m.Classid == Classid).Select(m => m.FatherEmail).ToList();
            tvm.teacheremails = db.sp_getteacheremail(Classid).ToList();
            SendEmails(Classid, tvm);
        }

        private void SendEmails(int Classid, timetableviewmodel tvm)
        {
            foreach (var r in tvm.parentemails)
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"].ToString(), ConfigurationManager.AppSettings["SenderName"].ToString());

                message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                message.SubjectEncoding = System.Text.Encoding.GetEncoding("utf-8");
                message.From = fromAddress;

                message.To.Add(r);
                message.Subject = "A-one School Management";
                message.Priority = MailPriority.High;
                message.IsBodyHtml = true;

                string msg = "<b>“Welcome to A-One School Management Systems”</b><br/><br/>";
                msg = msg + "Here We would like to inform you, Class timetable is published. Please see more details on below url.<br/>";
                msg = msg + "http://design.makeeasyconference.in/ShowTimetable/Index?Classid=" + Classid + " ";
                msg = msg + "Hope we would be going long term relationship with feature with good Support<br/><br/>Best Regards<br/>A-One School Team";
                message.Body = msg;
                smtpClient.Send(message);
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
