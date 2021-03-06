﻿using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystems.Controllers
{
    public class ChapterCompleteController : Controller
    {
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
        public ActionResult ChapterComplete(string Search_Data)
        {
            Chaptercompleteviewmodel svm = new Chaptercompleteviewmodel();
            svm.Teacherlist = new List<tbl_employee>();
            svm.Yearlist = new List<tbl_YearMaster>();
            svm.Departmentlist = new List<tblDepartment>();
            svm.Subjectlist = new List<tbl_subject>();
            svm.Courselist = db.tbl_CourseMaster.ToList();
            svm.classlistdetails = new List<tbl_class>();
            return View(svm);
        }
        
        public JsonResult GetSubject(string id, string year)
        {
            int yearid = 0;
            int Courseid = 0;
            if (id != null && id != "" && year != null && year != "")
            {
                Courseid = Convert.ToInt32(id);
                yearid = Convert.ToInt32(year);
            }
            var subject = db.tbl_subject.Where(m => m.Courseid == Courseid && m.yearid == yearid).ToList();
            return Json(new SelectList(subject, "Subjectid", "SubjectNm"));
        }

        public JsonResult GetSubjects(string id, string year, string department)
        {
            int subid = Convert.ToInt32(Session["Genid"].ToString());
            //int subid = 3018;
            int yearid = 0;
            int Courseid = 0;
            int dept = 0;
            if (id != null && id != "" && year != null && year != "" && department != null && department != "")
            {
                dept = Convert.ToInt32(department);
                Courseid = Convert.ToInt32(id);
                yearid = Convert.ToInt32(year);
            }

            var subject = from t in db.tbl_teachersubject
                          join s in db.tbl_subject on t.subjectid equals s.Subjectid
                          where t.courseid == Courseid && t.departmentid == dept && t.yearid == yearid && t.teacherid == subid
                          select new { s.Subjectid, s.SubjectNm };

            //var course =  db.tbl_class.Where(m => m.Dept_id == depid && m.status == true).ToList();
            return Json(new SelectList(subject, "Subjectid", "SubjectNm"));
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

        public JsonResult GetChapterlist(string id)
        {
            //int chapid = 0;
            int subid = 0;//////////////////
            if (id != null && id != "")
            {
                //chapid = Convert.ToInt32(id1);
                subid = Convert.ToInt32(id);
            }
            var chaplist = db.Sp_GetChapterDetails(subid).ToList();
            return Json(chaplist, JsonRequestBehavior.AllowGet);
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

        public JsonResult DMLCompletion(string[] completiondetails)
        {
           

            string s;
            int createdby = Convert.ToInt32(Session["Genid"].ToString());
            for (int i = 0; i < completiondetails.Count(); i++)
            {
                //sa.CreatedBy = Convert.ToInt32(Session["Userid"].ToString());
                s = completiondetails[i].ToString();
                string[] s1 = s.ToString().Split(',');
                int Teacherid = Convert.ToInt32(s1[0].ToString());
                bool complete = Convert.ToBoolean(s1[2].ToString());
                string Remark = s1[3].ToString();
                DateTime CompleteDate = Convert.ToDateTime(s1[1].ToString());
                int Subjectid = Convert.ToInt32(s1[6].ToString());
                int Chapterid = Convert.ToInt32(s1[5].ToString());
                int Departmentid = Convert.ToInt32(s1[4].ToString());
                int Yearid = Convert.ToInt32(s1[8].ToString());
                int Courseid = Convert.ToInt32(s1[7].ToString());
                int createdsby = createdby;
                int contentid = Convert.ToInt32(s1[9].ToString());
                try
                {
                    db.sp_ChapterComplete_DML(Teacherid, complete, Remark, CompleteDate, Subjectid, Chapterid, Departmentid, Yearid, Courseid,createdsby,contentid);
                }
                catch (Exception ex)
                { string msg = ex.ToString(); }
                db.SaveChanges();

            }
            return Json(completiondetails);

        }

        public ActionResult Index(string Search_Data)
        {

            return View();
        }

        public JsonResult FillChapterContentDetails(int courseid, int deptid,int yearid,int subjectid,int ChapterId)
        {
            var data = db.sp_GetChapterContentDetails(courseid, deptid, yearid, subjectid, ChapterId).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}


