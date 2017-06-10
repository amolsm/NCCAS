using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystems.Controllers
{
    public class MyProfileController : Controller
    {
        //
        // GET: /MyProfile/
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewProfile()
        {
            return View();
        }

        public JsonResult Get_ProfileDetails()
        {
            using (SchoolMgmtSysEntities Obj = new SchoolMgmtSysEntities())
            {
                int studentid = Convert.ToInt32(Session["Genid"]);
                List<Sp_GetStudentProfile_Result> Std = db.Sp_GetStudentProfile().Where(m=>m.Studid== studentid).ToList();
                return Json(Std, JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in db.tbl_student where temp.Studid == Id select temp.StudPic;
            byte[] cover = q.First();
            return cover;
        }
        public FileContentResult getProfileimg(int id)
        {
            byte[] byteArray = db.tbl_student.Where(m => m.Studid == id).Select(m => m.StudPic).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }
    }
}
