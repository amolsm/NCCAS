using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using System.Text;

namespace SchoolManagementSystems.Controllers
{
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
       
        public ActionResult StudentAttandanceReport()

        {
            ReportsViewModel rp = new ReportsViewModel();
            rp.Courselist = db.tbl_CourseMaster.ToList();
            rp.DepartmentList = db.tblDepartment.ToList();
            rp.YearList = db.tbl_YearMaster.ToList();
            return View(rp);
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

        public ActionResult ViewStudentAttandanceReport(ReportsViewModel rp)
        {
            rp.CourseName = db.tbl_CourseMaster.Where(m => m.Courseid == rp.CourseId).Select(m => m.CourseName).FirstOrDefault();
            rp.DepartmentName = db.tblDepartment.Where(m => m.Dept_id == rp.DepartmentId).Select(m => m.Dept_name).FirstOrDefault();
            rp.CourseYearName = db.tbl_YearMaster.Where(m => m.yearid == rp.CourseYearId).Select(m => m.YearName).FirstOrDefault();

            rp._studentreportlist = db.sp_getstudent_attandance_pivot(rp.Date, rp.CourseId, rp.DepartmentId, rp.CourseYearId).ToList();
          
          
            return PartialView("_printstudattandance",rp);







        }

    }
}
