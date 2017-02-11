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
    public class StudentController : Controller
    {
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
        public ActionResult Index(string Search_Data)
        {
            Studentviewmodel svm = new Studentviewmodel();
            FillPermission(4);
            if (Search_Data == null || Search_Data == "")
                svm.StudentDataCollection = db.tbl_student.OrderBy(m => m.Studid).ToList();
            else
                svm.StudentDataCollection = db.tbl_student.Where(x => x.Studnm.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.StudEmail.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.FatherContact.ToUpper().Contains(Search_Data.ToUpper())).OrderBy(m => m.Studid).ToList();
            return View(svm);
        }

       
        public ActionResult Admission(int? Studid)
        {
            Studentviewmodel svm = new Studentviewmodel();            
            FillPermission(3);            
            svm.courselist = db.tbl_CourseMaster.Where(m => m.Status == true).ToList();
            svm.Translist = db.tbl_transport.Where(m => m.status == true).ToList();
            svm.catlist = db.tbl_category.Where(m => m.status == true).ToList();
            svm.departmentlistdetails = db.tblDepartment.Where(m => m.Dept_id != 0).ToList();
            svm.academicyear = GetYear();
            svm.yearlist=db.tbl_YearMaster.Where(m => m.Status == true).ToList();
            List<SelectListItem> test = new List<SelectListItem>();
            foreach (var r in svm.catlist)
            {
                test.Add(new SelectListItem { Text = r.catname, Value = r.catid.ToString() });
            }
            ViewData["appList"] = test;
            List<SelectListItem> subcat = new List<SelectListItem>();
            ViewData["subcatList"] = subcat;
            List<SelectListItem> prd = new List<SelectListItem>();
            ViewData["prdList"] = prd;
            svm.countrylist = db.tbl_country.Where(m => m.status == true).ToList();
            svm.bloodgrouplist = db.tbl_bloodgroup.Where(m => m.status == true).ToList();
            svm.castelist = new List<tbl_caste>();
            svm.religionlist = db.tbl_religion.Where(m => m.status == true).ToList();
            svm.categorylist = db.tbl_StudentCategory.Where(m => m.status == true).ToList();
            svm.occupationlist = db.tbl_occupation.Where(m => m.status == true).ToList();
            svm.qualificationlist = db.tbl_qualification.Where(m => m.status == true).ToList();
            svm.statelist = new List<tbl_state>();
            svm.citylist = new List<tbl_city>();
            return View("Admission", svm);
        }
   
        public JsonResult FillStudentDetails(int studid)
        {
            var data = db.tbl_student.Where(m => m.Studid == studid).FirstOrDefault();
            TempData["Docs"] = data.Docs;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
    
        public JsonResult FillOnlineStudentDetails(int studid)
        {
            var data = db.tbl_online_student.Where(m => m.Studid == studid).FirstOrDefault();
            TempData["Docs"] = data.Docs;
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult CheckStudEmail(string Email)
        {
            var data = db.tbl_student.Where(m => m.StudEmail == Email).Select(m => m.StudEmail).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


     
        public JsonResult GetProductsexclude(string subcats)
        {
            string[] s = subcats.Split(',');
            List<SelectListItem> prd = new List<SelectListItem>();
            for (int i = 0; i < s.Count(); i++)
            {
                var data = db.sp_getproductsbycatexclude(s[i].ToString()).ToList();
                foreach (var r in data.ToList())
                {
                    prd.Add(new SelectListItem { Text = r.Product, Value = r.Productid.ToString() });
                }
            }
            var prds = prd;
            return Json(prds, JsonRequestBehavior.AllowGet);
        }

    
        public JsonResult GetProducts(string subcats)
        {
            string[] s = subcats.Split(',');
            List<SelectListItem> prd = new List<SelectListItem>();
            for (int i = 0; i < s.Count(); i++)
            {
                if (s[i] != "")
                {
                    var data = db.sp_getproductsbycat(s[i].ToString()).ToList();
                    foreach (var r in data.ToList())
                    {
                        prd.Add(new SelectListItem { Text = r.Product, Value = r.Productid.ToString() });
                    }
                }
            }
            var prds = prd;
            return Json(prds, JsonRequestBehavior.AllowGet);
        }

   
        public JsonResult GetSubcats(string cats)
        {
            string[] s = cats.Split(',');
            StringBuilder exclude = new StringBuilder();
            List<SelectListItem> subcat = new List<SelectListItem>();
            for (int i = 0; i < s.Count(); i++)
            {
                var data = db.sp_getsubcatbycat(s[i].ToString()).ToList();
                if (data.Count() != 0)
                {
                    foreach (var r in data.ToList())
                    {
                        subcat.Add(new SelectListItem { Text = r.SubCategoryname, Value = r.SubCategoryid.ToString() });
                    }
                }
                else
                {
                    exclude.Append(s[i].ToString() + ",");
                }
            }
            var result = new { subcats = subcat, prds = exclude.ToString() };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

      
        public JsonResult GetTransportDetails(int busid)
        {
            var data = db.tbl_transport.Where(m => m.busid == busid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStates(string id)
        {
            int countryid = 0;
            if (id != null && id != "")
            {
                countryid = Convert.ToInt32(id);
            }
            var states = db.tbl_state.Where(m => m.Countryid == countryid).ToList();
            return Json(new SelectList(states, "Stateid", "StateName"));
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
    //    [SchoolManagementSystems.MvcApplication.SessionExpire]
        public JsonResult GetBusInfo(string busid)
        {
            var data = db.tbl_transport.Where(m => m.busid == Convert.ToInt32(busid)).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
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

        public FileContentResult getImg1(int id)
        {
            byte[] byteArray = db.tbl_student.Where(m => m.Studid == id).Select(m => m.StudPic).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }

        public FileContentResult getImg2(int id)
        {
            byte[] byteArray = db.tbl_student.Where(m => m.Studid == id).Select(m => m.FatherPic).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }

      

        public FileContentResult getImg3(int id)
        {
            byte[] byteArray = db.tbl_student.Where(m => m.Studid == id).Select(m => m.GuardianPic).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }

        public FileContentResult getImg4(int id)
        {
            byte[] byteArray = db.tbl_student.Where(m => m.Studid == id).Select(m => m.MotherPic).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }

        public FileContentResult getImg5(int id)
        {
            byte[] byteArray = db.tbl_student.Where(m => m.Studid == id).Select(m => m.sc_refletter).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }
        public FileContentResult getImg6(int id)
        {
            byte[] byteArray = db.tbl_student.Where(m => m.Studid == id).Select(m => m.SecondaryTCScanCopy).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }
        public FileContentResult getImg7(int id)
        {
            byte[] byteArray = db.tbl_student.Where(m => m.Studid == id).Select(m => m.SecondaryMarksheetCopy).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }
        public FileContentResult getImg8(int id)
        {
            byte[] byteArray = db.tbl_student.Where(m => m.Studid == id).Select(m => m.UGMarksheet).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }
        public FileContentResult getImg9(int id)
        {
            byte[] byteArray = db.tbl_student.Where(m => m.Studid == id).Select(m => m.PGMarksheet).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }


        [HttpPost]
        public ActionResult Admission_DML(Studentviewmodel svm, string action, HttpPostedFileBase files1, HttpPostedFileBase files2, HttpPostedFileBase files3, HttpPostedFileBase filemotherpic, HttpPostedFileBase files5, HttpPostedFileBase files6, HttpPostedFileBase files7, HttpPostedFileBase files8, HttpPostedFileBase files9, int? id, HttpPostedFileBase FileUpload1, string group1)
        {
            string yr = svm.academicyear[0].ToString();
           
            if (files1 != null)
            {
                if (System.IO.File.Exists(files1.ToString()))
                {

                    System.IO.File.Delete(files1.ToString());
                    files1 = null;

                }
                if (files1.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        files1.InputStream.CopyTo(ms);
                        WebImage img = new WebImage(files1.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(450, 450);
                        }
                        byte[] array = img.GetBytes();
                        svm.StudPic = array;
                    }
                }
                else
                {
                    if (svm.Studid != 0)
                    {
                        svm.StudPic = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.StudPic).FirstOrDefault();
                    }
                }
            }
            else
            {
                if (svm.Studid != 0)
                {
                    svm.StudPic = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.StudPic).FirstOrDefault();
                }
            }
            //father pic
            if (files2 != null)
            {
                if (System.IO.File.Exists(files2.ToString()))
                {

                    System.IO.File.Delete(files2.ToString());
                    files2 = null;

                }
                if (files2.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        files2.InputStream.CopyTo(ms);
                        WebImage img = new WebImage(files2.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(450, 450);
                        }
                        byte[] array = img.GetBytes();
                        svm.FatherPic = array;
                    }
                }
                else
                {
                    if (svm.Studid != 0)
                    {
                        svm.FatherPic = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.FatherPic).FirstOrDefault();
                    }
                }
            }
            else
            {
                if (svm.Studid != 0)
                {
                    svm.FatherPic = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.FatherPic).FirstOrDefault();
                }
            }
            //gaurdian pic
            if (files3 != null)
            {
                if (System.IO.File.Exists(files3.ToString()))
                {

                    System.IO.File.Delete(files3.ToString());
                    files3 = null;

                }
                if (files3.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        files3.InputStream.CopyTo(ms);
                        WebImage img = new WebImage(files3.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(450, 450);
                        }
                        byte[] array = img.GetBytes();
                        svm.GuardianPic = array;
                    }
                }
                else
                {
                    if (svm.Studid != 0)
                    {
                        svm.GuardianPic = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.GuardianPic).FirstOrDefault();
                    }
                }
            }
            else
            {
                if (svm.Studid != 0)
                {
                    svm.GuardianPic = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.GuardianPic).FirstOrDefault();
                }
            }
           // mother pic

            if (filemotherpic != null)
            {
                if (System.IO.File.Exists(filemotherpic.ToString()))
                {

                    System.IO.File.Delete(filemotherpic.ToString());
                    filemotherpic = null;

                }
                if (filemotherpic.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        filemotherpic.InputStream.CopyTo(ms);
                        WebImage img = new WebImage(filemotherpic.InputStream);

                        if (img.Width > 700)
                        {
                            img.Resize(450, 450);
                        }
                        byte[] array = img.GetBytes();
                        svm.MPic = array;
                    }
                }
                else
                {
                    if (svm.Studid != 0)
                    {
                        svm.MPic = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.MotherPic).FirstOrDefault();
                    }
                }
            }
            else
            {
                if (svm.Studid != 0)
                {
                    svm.MPic = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.MotherPic).FirstOrDefault();
                }
            }
            //sc reference letter pic

            if (files5 != null)
            {
                if (System.IO.File.Exists(files5.ToString()))
                {

                    System.IO.File.Delete(files5.ToString());
                    files5 = null;

                }
                if (files5.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        WebImage img = new WebImage(files5.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(600, 600);
                        }
                        byte[] array = img.GetBytes();
                        svm.sc_refletter = array;
                    }
                }
                else
                {
                    if (svm.Studid != 0)
                    {
                        svm.sc_refletter = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.sc_refletter).FirstOrDefault();
                    }
                }
            }
            else
            {
                if (svm.Studid != 0)
                {
                    svm.sc_refletter = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.sc_refletter).FirstOrDefault();
                }
            }
            //tc scan copy
            if (files6 != null)
            {
                if (System.IO.File.Exists(files6.ToString()))
                {

                    System.IO.File.Delete(files6.ToString());
                    files6 = null;

                }
                if (files6.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        WebImage img = new WebImage(files6.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(600, 600);
                        }
                        byte[] array = img.GetBytes();
                        svm.SCTCPic = array;
                    }
                }
                else
                {
                    if (svm.Studid != 0)
                    {
                        svm.SCTCPic = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.SecondaryTCScanCopy).FirstOrDefault();
                    }
                }
            }
            else
            {
                if (svm.Studid != 0)
                {
                    svm.SCTCPic = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.SecondaryTCScanCopy).FirstOrDefault();
                }
            }
            //sc marksheet
            if (files7 != null)
            {
                if (System.IO.File.Exists(files7.ToString()))
                {

                    System.IO.File.Delete(files7.ToString());
                    files7 = null;

                }
                if (files7.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        WebImage img = new WebImage(files7.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(600, 600);
                        }
                        byte[] array = img.GetBytes();
                        svm.SCMarksheet = array;
                    }
                }
                else
                {
                    if (svm.Studid != 0)
                    {
                        svm.SCMarksheet = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.SecondaryMarksheetCopy).FirstOrDefault();
                    }
                }
            }
            else
            {
                if (svm.Studid != 0)
                {
                    svm.SCMarksheet = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.SecondaryMarksheetCopy).FirstOrDefault();
                }
            }
            //ug marksheet
            if (files8 != null)
            {
                if (System.IO.File.Exists(files8.ToString()))
                {

                    System.IO.File.Delete(files8.ToString());
                    files8 = null;

                }
                if (files8.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        WebImage img = new WebImage(files8.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(600, 600);
                        }
                        byte[] array = img.GetBytes();
                        svm.UGMarksheet = array;
                    }
                }
                else
                {
                    if (svm.Studid != 0)
                    {
                        svm.UGMarksheet = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.UGMarksheet).FirstOrDefault();
                    }
                }
            }
            else
            {
                if (svm.Studid != 0)
                {
                    svm.UGMarksheet = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.UGMarksheet).FirstOrDefault();
                }
            }
            //pg marksheet
            if (files9 != null)
            {
                if (System.IO.File.Exists(files9.ToString()))
                {

                    System.IO.File.Delete(files9.ToString());
                    files9 = null;

                }
                if (files9.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        WebImage img = new WebImage(files9.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(600, 600);
                        }
                        byte[] array = img.GetBytes();
                        svm.PGMarksheet = array;
                    }
                }
                else
                {
                    if (svm.Studid != 0)
                    {
                        svm.PGMarksheet = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.PGMarksheet).FirstOrDefault();
                    }
                }
            }
            else
            {
                if (svm.Studid != 0)
                {
                    svm.PGMarksheet = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.PGMarksheet).FirstOrDefault();
                }
            }
            //======upload multiple attachments start============
            if (FileUpload1 != null)
            {
                DeleteAttachment();
                StringBuilder s = new StringBuilder();
                string Random = Guid.NewGuid().ToString().Substring(0, 8);
                HttpFileCollectionBase files = Request.Files;
                DataTable dt = new DataTable { Columns = { new DataColumn("Path") } };
                for (int i = 3; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    if (files[i].InputStream.Length < 31000000)
                    {
                        string path = Server.MapPath("~") + "\\upload\\" + Random + "_" + file.FileName;
                        dt.Rows.Add(file.FileName);
                        file.SaveAs(path);
                    }
                    s.Append(Random + "_" + file.FileName.Trim()).Append('|');
                }
                svm.Docs = s.ToString();
            }
            else
            {
                if (svm.Studid != 0)
                {
                    svm.Docs = db.tbl_student.Where(m => m.Studid == svm.Studid).Select(m => m.Docs).FirstOrDefault();
                }
            }
            //=====upload multiple attachement end==========


            if (action != "delete")
            {
                string cats = "";
                string subcats = "";
                string prds = "";
                if (Convert.ToString(Session["online_Admmission"]) != "Yes")
                {
                    if (svm.categories != null)
                    {
                        foreach (var c in svm.categories)
                        {
                            cats += c + ",";
                        }
                    }
                    if (svm.subcategories != null)
                    {
                        foreach (var p in svm.subcategories)
                        {
                            subcats += p + ",";
                        }
                    }
                    if (svm.products != null)
                    {
                        foreach (var p in svm.products)
                        {
                            prds += p + ",";
                        }
                    }
                }
                if (svm.FormType == "O_type")
                {
                   // db.sp_student_online_admission(svm.Studid, svm.Studnm, svm.Studfathernm, svm.Studmothernm, svm.DOB, svm.Weight, svm.Height, svm.StudBldGrp, svm.StudEmail, svm.Disease, svm.Religionid, svm.Casteid, svm.Classid, svm.RollNo, svm.Gender, svm.MotherTongue, svm.PreviousSchool, svm.SchoolAddress, svm.LastClass, svm.Grade, svm.LeaveYear, svm.LeaveReason, svm.PrincipalNm, svm.ReferenceNm, svm.ReferenceContact, svm.BusFacility, svm.BusNo, svm.BusRTONo, svm.EmergencyPhysicianNm, svm.EmergencyPhysicianContact, svm.EmergencyAddress, svm.StudPic, svm.FatherOccpationid, svm.FatherQualificationid, svm.FatherEmail, svm.FatherOfficeAddress, svm.FatherContact, svm.FatherBldGrpid, svm.FatherPic, svm.MotherOccpationid, svm.MotherQualificationid, svm.MotherEmail, svm.MotherOfficeAddress, svm.MotherContact, svm.MotherBldGrpid, svm.MotherPic, svm.Countryid, svm.Stateid, svm.Cityid, svm.CurrentAddress, svm.PermanentAddress, yr, svm.busid, cats, prds, svm.Docs, subcats, svm.StudCategoryid, svm.GuardianOccpationid, svm.GuardianQualificationid, svm.GuardianEmail, svm.GuardianOfficeAddress, svm.GuardianContact, svm.GuardianName, svm.FCode, svm.MCode, svm.GCode, svm.ECode, svm.RCode, "").ToString();
                }
                else
                {
                    //====If Approve=======//
                    if (group1 != "0" && group1!=null)
                    {
                        //db.sp_student_online_admission(svm.Studid, svm.Studnm, svm.Studfathernm, svm.Studmothernm, svm.DOB, svm.Weight, svm.Height, svm.StudBldGrp, svm.StudEmail, svm.Disease, svm.Religionid, svm.Casteid, svm.Classid, svm.RollNo, svm.Gender, svm.MotherTongue, svm.PreviousSchool, svm.SchoolAddress, svm.LastClass, svm.Grade, svm.LeaveYear, svm.LeaveReason, svm.PrincipalNm, svm.ReferenceNm, svm.ReferenceContact, svm.BusFacility, svm.BusNo, svm.BusRTONo, svm.EmergencyPhysicianNm, svm.EmergencyPhysicianContact, svm.EmergencyAddress, svm.StudPic, svm.FatherOccpationid, svm.FatherQualificationid, svm.FatherEmail, svm.FatherOfficeAddress, svm.FatherContact, svm.FatherBldGrpid, svm.FatherPic, svm.MotherOccpationid, svm.MotherQualificationid, svm.MotherEmail, svm.MotherOfficeAddress, svm.MotherContact, svm.MotherBldGrpid, svm.MotherPic, svm.Countryid, svm.Stateid, svm.Cityid, svm.CurrentAddress, svm.PermanentAddress, yr, svm.busid, cats, prds, svm.Docs, subcats, svm.StudCategoryid, svm.GuardianOccpationid, svm.GuardianQualificationid, svm.GuardianEmail, svm.GuardianOfficeAddress, svm.GuardianContact, svm.GuardianName, svm.FCode, svm.MCode, svm.GCode, svm.ECode, svm.RCode, "del").ToString();
                        try
                        {
                            db.sp_student_admission(0, svm.Studnm, svm.Studfathernm, svm.Studmothernm, svm.DOB, svm.Weight, svm.Height, svm.StudBldGrp, svm.StudEmail, svm.Disease, svm.Religionid, svm.Casteid, svm.Classid, svm.RollNo, svm.Gender, svm.MotherTongue, svm.PreviousSchool, svm.SchoolAddress, svm.LastClass, svm.Grade, svm.LeaveYear, svm.LeaveReason, svm.PrincipalNm, svm.ReferenceNm, svm.ReferenceContact, svm.BusFacility, svm.BusNo, svm.BusRTONo, svm.EmergencyPhysicianNm, svm.EmergencyPhysicianContact, svm.EmergencyAddress, svm.StudPic, svm.FatherOccpationid, svm.FatherQualificationid, svm.FatherEmail, svm.FatherOfficeAddress, svm.FatherContact, svm.FatherBldGrpid, svm.FatherPic, svm.MotherOccpationid, svm.MotherQualificationid, svm.MotherEmail, svm.MotherOfficeAddress, svm.MotherContact, svm.MotherBldGrpid, svm.MPic, svm.Countryid, svm.Stateid, svm.Cityid, svm.CurrentAddress, svm.PermanentAddress, yr, svm.busid, cats, prds, svm.Docs, subcats, svm.StudCategoryid, svm.GuardianOccpationid, svm.GuardianQualificationid, svm.GuardianEmail, svm.GuardianOfficeAddress, svm.GuardianContact, svm.GuardianName, svm.FCode, svm.MCode, svm.GCode, svm.ECode, svm.RCode, "", svm.GuardianPic, svm.PrScTotalMarks, svm.PrScObtainMark, svm.PrScPercentage, svm.SCTCPic, svm.SCMarksheet, svm.PrUgCollegeName, svm.PrUgCollegeAddress,
                         svm.PrUgAffilatedUniversity, svm.PrUgRefContactNo, svm.PrUgTotalMark, svm.PrUgObtainMark, svm.PrUgPercentage, svm.PrUgGradeLeaving, svm.PrUgYearLeaving, svm.PrUgReasonofLeaving, svm.PrUgPrincipalName, svm.PrUgRefContactName, svm.UGMarksheet,
                         svm.PrPgCollegeName, svm.PrPgCollegeAddress, svm.PrPgAffilatedUniversity, svm.PrPgRefContactNo, svm.PrPgTotalMark, svm.PrPgObtainMark, svm.PrPgPercentage, svm.PrPgGradeLeaving, svm.PrPgYearLeaving, svm.PrPgReasonofLeaving, svm.PrPgPrincipalName, svm.PrPgRefContactName, svm.PGMarksheet,
                         svm.Sibling1Name, svm.Sibling1Rel, svm.Sibling1DOB, svm.Sibling1Ql, svm.Sibling2Name, svm.Sibling2Rel, svm.Sibling2DOB, svm.Sibling2Ql,
                         svm.Sibling3Name, svm.Sibling3Rel, svm.Sibling3DOB, svm.Sibling3Ql, svm.Sibling4Name, svm.Sibling4Rel, svm.Sibling4DOB, svm.Sibling4Ql, svm.ParishName, svm.DioceseName, svm.DocumentType, svm.DocumentIDNo, svm.DepartmentId, svm.ApplicationID, svm.UniversityRegId, svm.Pincode, svm.PrScRegisterNumber, svm.sc_refletter, svm.PrScTCNumber, svm.PrUgRegisterNumber, svm.PrPgRegisterNumber, svm.Documenttypename, svm.courseyear,
                         svm.FatherOccumationName,svm.FatherQualificationName,svm.GuardianOccpationName,svm.GuardianQualificationName,svm.MotherOccpationName,svm.MotherQualificationName,svm.StdRegMob, svm.StdRegNo, svm.emcontactrel).ToString();
                            
                        }
                        catch(Exception ex) { string msg = ex.ToString();
                          //  TempData["StudentError"] = msg;
                        }

                        try
                        {
                            int studid = db.tbl_student.Where(m => m.StudEmail == svm.StudEmail).Select(m => m.Studid).FirstOrDefault();
                            CreateUsers(svm.StudEmail, 1, studid, yr);
                            CreateUsers(svm.FatherEmail, 2, studid, yr);
                            SendSMS(studid);
                        }
                        catch (Exception ex) { string msg = ex.ToString(); }
                    }
                    else if (group1 == "0" && group1!=null)
                    {
                        try
                        {
                           // db.sp_student_online_admission(svm.Studid, svm.Studnm, svm.Studfathernm, svm.Studmothernm, svm.DOB, svm.Weight, svm.Height, svm.StudBldGrp, svm.StudEmail, svm.Disease, svm.Religionid, svm.Casteid, svm.Classid, svm.RollNo, svm.Gender, svm.MotherTongue, svm.PreviousSchool, svm.SchoolAddress, svm.LastClass, svm.Grade, svm.LeaveYear, svm.LeaveReason, svm.PrincipalNm, svm.ReferenceNm, svm.ReferenceContact, svm.BusFacility, svm.BusNo, svm.BusRTONo, svm.EmergencyPhysicianNm, svm.EmergencyPhysicianContact, svm.EmergencyAddress, svm.StudPic, svm.FatherOccpationid, svm.FatherQualificationid, svm.FatherEmail, svm.FatherOfficeAddress, svm.FatherContact, svm.FatherBldGrpid, svm.FatherPic, svm.MotherOccpationid, svm.MotherQualificationid, svm.MotherEmail, svm.MotherOfficeAddress, svm.MotherContact, svm.MotherBldGrpid, svm.MotherPic, svm.Countryid, svm.Stateid, svm.Cityid, svm.CurrentAddress, svm.PermanentAddress, yr, svm.busid, cats, prds, svm.Docs, subcats, svm.StudCategoryid, svm.GuardianOccpationid, svm.GuardianQualificationid, svm.GuardianEmail, svm.GuardianOfficeAddress, svm.GuardianContact, svm.GuardianName, svm.FCode, svm.MCode, svm.GCode, svm.ECode, svm.RCode, "del").ToString();
                        }
                        catch (Exception ex) { string msg = ex.ToString(); }
                    }
                    else
                    {
                        try
                        {
                            db.sp_student_admission(svm.Studid, svm.Studnm, svm.Studfathernm, svm.Studmothernm, svm.DOB, svm.Weight, svm.Height, svm.StudBldGrp, svm.StudEmail, svm.Disease, svm.Religionid, svm.Casteid, svm.Classid, svm.RollNo, svm.Gender, svm.MotherTongue, svm.PreviousSchool, svm.SchoolAddress, svm.LastClass, svm.Grade, svm.LeaveYear, svm.LeaveReason, svm.PrincipalNm, svm.ReferenceNm, svm.ReferenceContact, svm.BusFacility, svm.BusNo, svm.BusRTONo, svm.EmergencyPhysicianNm, svm.EmergencyPhysicianContact, svm.EmergencyAddress, svm.StudPic, svm.FatherOccpationid, svm.FatherQualificationid, svm.FatherEmail, svm.FatherOfficeAddress, svm.FatherContact, svm.FatherBldGrpid, svm.FatherPic, svm.MotherOccpationid, svm.MotherQualificationid, svm.MotherEmail, svm.MotherOfficeAddress, svm.MotherContact, svm.MotherBldGrpid, svm.MPic, svm.Countryid, svm.Stateid, svm.Cityid, svm.CurrentAddress, svm.PermanentAddress, yr, svm.busid, cats, prds, svm.Docs, subcats, svm.StudCategoryid, svm.GuardianOccpationid, svm.GuardianQualificationid, svm.GuardianEmail, svm.GuardianOfficeAddress, svm.GuardianContact, svm.GuardianName, svm.FCode, svm.MCode, svm.GCode, svm.ECode, svm.RCode, "", svm.GuardianPic, svm.PrScTotalMarks, svm.PrScObtainMark, svm.PrScPercentage, svm.SCTCPic, svm.SCMarksheet, svm.PrUgCollegeName, svm.PrUgCollegeAddress,
                          svm.PrUgAffilatedUniversity, svm.PrUgRefContactNo, svm.PrUgTotalMark, svm.PrUgObtainMark, svm.PrUgPercentage, svm.PrUgGradeLeaving, svm.PrUgYearLeaving, svm.PrUgReasonofLeaving, svm.PrUgPrincipalName, svm.PrUgRefContactName, svm.UGMarksheet,
                          svm.PrPgCollegeName, svm.PrPgCollegeAddress, svm.PrPgAffilatedUniversity, svm.PrPgRefContactNo, svm.PrPgTotalMark, svm.PrPgObtainMark, svm.PrPgPercentage, svm.PrPgGradeLeaving, svm.PrPgYearLeaving, svm.PrPgReasonofLeaving, svm.PrPgPrincipalName, svm.PrPgRefContactName, svm.PGMarksheet,
                          svm.Sibling1Name, svm.Sibling1Rel, svm.Sibling1DOB, svm.Sibling1Ql, svm.Sibling2Name, svm.Sibling2Rel, svm.Sibling2DOB, svm.Sibling2Ql,
                          svm.Sibling3Name, svm.Sibling3Rel, svm.Sibling3DOB, svm.Sibling3Ql, svm.Sibling4Name, svm.Sibling4Rel, svm.Sibling4DOB, svm.Sibling4Ql, svm.ParishName, svm.DioceseName, svm.DocumentType, svm.DocumentIDNo, svm.DepartmentId, svm.ApplicationID, svm.UniversityRegId, svm.Pincode, svm.PrScRegisterNumber, svm.sc_refletter, svm.PrScTCNumber, svm.PrUgRegisterNumber, svm.PrPgRegisterNumber, svm.Documenttypename, svm.courseyear,
                          svm.FatherOccumationName, svm.FatherQualificationName, svm.GuardianOccpationName, svm.GuardianQualificationName, svm.MotherOccpationName, svm.MotherQualificationName, svm.StdRegMob, svm.StdRegNo, svm.emcontactrel).ToString();

                        }
                        catch (Exception ex) { string msg = ex.ToString();
                            //TempData["StudentError"] = msg;
                        }
                        try
                        {
                            SendEmails(svm.StudEmail);
                            SendUpdateSMS(svm.Studid);
                        }
                        catch (Exception ex)
                        {
                            string msg = ex.ToString();
                        }


                        }
                }
            }
            else
            {
                try
                {
                    db.sp_student_admission(id, "", "", "", Convert.ToDateTime("01/01/1753"), 0, 0, 0, "", "", 0, 0, 0, 0, 0, "", "", "", "", "", "", "", "", "", "", false, "", "", "", "", "", null, 0, 0, "", "", "", 0, null, 0, 0, "", "", "", 0, null, 0, 0, 0, "", "", "", 0, "", "", "", "", 0, 0, 0, "", "", "", "", svm.FCode, svm.MCode, svm.GCode, svm.ECode, svm.RCode, "del",
                     svm.GuardianPic, svm.PrScTotalMarks, svm.PrScObtainMark, svm.PrScPercentage, svm.SCTCPic, svm.SCMarksheet, svm.PrUgCollegeName, svm.PrUgCollegeAddress,
                           svm.PrUgAffilatedUniversity, svm.PrUgRefContactNo, svm.PrUgTotalMark, svm.PrUgObtainMark, svm.PrUgPercentage, svm.PrUgGradeLeaving, svm.PrUgYearLeaving, svm.PrUgReasonofLeaving, svm.PrUgPrincipalName, svm.PrUgRefContactName, svm.UGMarksheet,
                           svm.PrPgCollegeName, svm.PrPgCollegeAddress, svm.PrPgAffilatedUniversity, svm.PrPgRefContactNo, svm.PrPgTotalMark, svm.PrPgObtainMark, svm.PrPgPercentage, svm.PrPgGradeLeaving, svm.PrPgYearLeaving, svm.PrPgReasonofLeaving, svm.PrPgPrincipalName, svm.PrPgRefContactName, svm.PGMarksheet,
                           svm.Sibling1Name, svm.Sibling1Rel, svm.Sibling1DOB, svm.Sibling1Ql, svm.Sibling2Name, svm.Sibling2Rel, svm.Sibling2DOB, svm.Sibling2Ql,
                           svm.Sibling3Name, svm.Sibling3Rel, svm.Sibling3DOB, svm.Sibling3Ql, svm.Sibling4Name, svm.Sibling4Rel, svm.Sibling4DOB, svm.Sibling4Ql,svm.ParishName,svm.DioceseName,svm.DocumentType,svm.DocumentIDNo, svm.DepartmentId, svm.ApplicationID, svm.UniversityRegId, svm.Pincode, svm.PrScRegisterNumber, svm.sc_refletter, svm.PrScTCNumber, svm.PrUgRegisterNumber, svm.PrPgRegisterNumber, svm.Documenttypename,svm.courseyear,
                         svm.FatherOccumationName, svm.FatherQualificationName, svm.GuardianOccpationName, svm.GuardianQualificationName, svm.MotherOccpationName, svm.MotherQualificationName,svm.StdRegMob,svm.StdRegNo,svm.emcontactrel).ToString();
                }
                catch (Exception ex) { string msg = ex.ToString();
                    //TempData["StudentError"] = msg;

                }
            }
            if (Convert.ToString(Session["online_Admmission"])!= "Yes")
            {
                return RedirectToAction("Index");
            }
            else
            {
                Session["online_Admmission"] = "No";
                return RedirectToAction("Index");
            }
        }
        private void CreateUsers(string UserName, int Type, int Genid, string academicyear)
        {
            string Password = Guid.NewGuid().ToString().Substring(0, 8);
            var x = (from y in db.tbl_user where y.UserName == UserName select y).FirstOrDefault();
            if (x == null)
            {
                db.sp_User_DML(0, UserName, Password, Type, Genid, 1, academicyear, "");
                SendUserEmails(UserName);

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
            //message.From = fromAddress;
            //message.To.Add(Email);
            message.Subject = "Nanjil Catholic College of Arts & Science";
            message.Priority = MailPriority.High;
            message.IsBodyHtml = true;
            string pass = db.tbl_user.Where(m => m.UserName == Email).Select(m => m.Password).FirstOrDefault();
            string msg = "<b>“Welcome to Nanjil Catholic College of Arts & Science”</b><br/><br/>";
            msg = msg + "Your UserName : " + Email + ".<br/>";
            msg = msg + "Your Password : " + pass + ".<br/>";
            msg = msg + "Hope we would be going long term relationship with feature with good Support<br/><br/>Best Regards<br/>NCCAS Management";
            message.Body = msg;
            smtpClient.Send(message);
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
            msg = msg + "Dear Student: "  + db.tbl_student.OrderByDescending(m => m.Studid).Select(m => m.Studnm).FirstOrDefault() + " .<br/>";
            msg = msg + "Here We would like to inform you that Your Profile is Updated Successfully.<br/>";
            msg = msg + "Hope we would be going long term relationship with feature with good Support<br/><br/>Best Regards<br/>NCCAS Management";
            message.Body = msg;
            smtpClient.Send(message);
        }
    
        public ActionResult GuardianList(string Search_Data)
        {
            Studentviewmodel svm = new Studentviewmodel();
            FillPermission(6);
            if (Search_Data == null || Search_Data == "")
                svm.StudentDataCollection = db.tbl_student.OrderBy(m => m.Studid).ToList();
            else
                svm.StudentDataCollection = db.tbl_student.OrderBy(m => m.Studid).Where(x => x.GuardianName.ToUpper().Contains(Search_Data.ToUpper())
                                                                                        || x.Studnm.ToUpper().Contains(Search_Data.ToUpper())
                                                                                        || x.GuardianEmail.ToUpper().Contains(Search_Data.ToUpper())
                                                                                        || x.GuardianContact.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            return View(svm);
        }

        public ActionResult Online_Admission(int? Studid, HttpPostedFileBase FileUpload1)
        {            
            Session["online_Admmission"] = "Yes";
            Studentviewmodel svm = new Studentviewmodel();
            svm.classlist = db.tbl_class.Where(m => m.status == true).ToList();
            svm.Translist = db.tbl_transport.Where(m => m.status == true).ToList();
            svm.catlist = db.tbl_category.Where(m => m.status == true).ToList();
            List<SelectListItem> test = new List<SelectListItem>();
            foreach (var r in svm.catlist)
            {
                test.Add(new SelectListItem { Text = r.catname, Value = r.catid.ToString() });
            }
            ViewData["appList"] = test;
            List<SelectListItem> subcat = new List<SelectListItem>();
            ViewData["subcatList"] = subcat;
            List<SelectListItem> prd = new List<SelectListItem>();
            ViewData["prdList"] = prd;
            svm.countrylist = db.tbl_country.Where(m => m.status == true).ToList();
            svm.bloodgrouplist = db.tbl_bloodgroup.Where(m => m.status == true).ToList();
            svm.castelist = new List<tbl_caste>();
            svm.religionlist = db.tbl_religion.Where(m => m.status == true).ToList();
            svm.categorylist = db.tbl_StudentCategory.Where(m => m.status == true).ToList();
            svm.occupationlist = db.tbl_occupation.Where(m => m.status == true).ToList();
            svm.qualificationlist = db.tbl_qualification.Where(m => m.status == true).ToList();
            svm.statelist = new List<tbl_state>();
            svm.citylist = new List<tbl_city>();
            if (FileUpload1 != null)
            {
                DeleteAttachment();
                StringBuilder s = new StringBuilder();
                string Random = Guid.NewGuid().ToString().Substring(0, 8);
                HttpFileCollectionBase files = Request.Files;
                DataTable dt = new DataTable { Columns = { new DataColumn("Path") } };
                for (int i = 3; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    if (files[i].InputStream.Length < 31000000)
                    {
                        string path = Server.MapPath("~") + "\\upload\\" + Random + "_" + file.FileName;
                        dt.Rows.Add(file.FileName);
                        file.SaveAs(path);
                    }
                    s.Append(Random + "_" + file.FileName.Trim()).Append('|');
                }
                svm.Docs = s.ToString();
            }
            var stud_details = db.tbl_online_student.Where(m => m.Studid == Studid).FirstOrDefault();
            return View("Online_Admission", svm);
        }

        public ActionResult PromoteStudent()
        {
            Studentviewmodel svm = new Studentviewmodel();
            FillPermission(5);
            svm.classlist = db.tbl_class.Where(m => m.status == true).ToList();
            return View(svm);
        }

        public JsonResult ClassStudent(int Class)
        {
            var data = db.tbl_student.Where(m => m.Classid == Class).Select(x => new { x.Studid, x.Studnm });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MoveStudent(int Studentid, int ToClass, int FromClassid, int act)
        {
            tbl_student st = db.tbl_student.Where(x => x.Studid == Studentid).FirstOrDefault();
            if (act == 1)
            {
                st.Classid = ToClass;
            }
            else
            {
                st.Classid = FromClassid;
            }
            db.SaveChanges();
            return View();
        }

        public void DownloadAttachment()
        {
            if (TempData["Docs"] != null)
            {
                ZipFile multipleFiles = new ZipFile();
                Response.AddHeader("Content-Disposition", "attachment; filename=DownloadedFile.zip");
                Response.ContentType = "application/zip";
                string[] s = TempData["Docs"].ToString().Split('|');
                for (int i = 0; i < s.Length - 1; i++)
                {
                    string filePath = Server.MapPath("~") + "\\upload\\" + s[i].ToString();
                    multipleFiles.AddFile(filePath, string.Empty);
                }
                multipleFiles.Save(Response.OutputStream);
            }
        }

        public void DeleteAttachment()
        {
            if (TempData["Docs"] != null)
            {
                string[] s = TempData["Docs"].ToString().Split('|');
                for (int i = 0; i < s.Length - 1; i++)
                {
                    string completePath = Server.MapPath("~") + "\\upload\\" + s[i].ToString();
                    if (System.IO.File.Exists(completePath))
                    {

                        System.IO.File.Delete(completePath);

                    }
                }
                TempData["Docs"] = null;
            }
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
        public ActionResult Online_student_Mgmt(string Search_Data)
        {
            TempData["FormType"] = "O_type";
            Studentviewmodel svm = new Studentviewmodel();
            FillPermission(4);
            if (Search_Data == null || Search_Data == "")
                svm.online_StudentDataCollection = db.tbl_online_student.OrderBy(m => m.Studid).ToList();
            else
                svm.online_StudentDataCollection = db.tbl_online_student.Where(x => x.Studnm.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.StudEmail.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.FatherContact.ToUpper().Contains(Search_Data.ToUpper())).OrderBy(m => m.Studid).ToList();
            return View(svm);
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

        public JsonResult GetCourseYear(string courseid,string deptid)
        {
            int coureid = 0;
            int detid = 0;
            if (courseid != null && courseid != "" && deptid!=null && deptid!="")
            {
                coureid = Convert.ToInt32(courseid);
                detid = Convert.ToInt32(deptid);
            }
            var yeardata = from post in db.tbl_CourseYearMaster
                         join meta in db.tbl_YearMaster on post.academicyear equals meta.yearid
                         where post.courseid == coureid && post.dept_id==detid && post.status == true
                         select new { meta.yearid, meta.YearName };

            return Json(new SelectList(yeardata, "yearid", "YearName"));
        }


        private string SendSMS(int studentid)
        {
            string stemailid;
            string mobileno;
          
            tbl_student st = db.tbl_student.Where(x => x.Studid == studentid).FirstOrDefault();
            stemailid = st.StudEmail;
            mobileno = st.StdMobNo;
         
            string pass = db.tbl_user.Where(m => m.UserName == stemailid).Select(m => m.Password).FirstOrDefault();
            string msg = "Welcome to Nanjil Catholic College of Arts & Science"+ Environment.NewLine;
            msg = msg + "U/N: " + stemailid + Environment.NewLine;
            msg = msg + "P/W: " + pass + Environment.NewLine;
           
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

        public JsonResult GetRegisterationNo(string acadmicyear, string deptname)
        {
            string acyear = acadmicyear.Substring(0, 4);
            var data = db.sp_GetStdCount();
          
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private string SendUpdateSMS(int studentId)
        {
            string stemailid;
            string mobileno;

            tbl_student st = db.tbl_student.Where(x => x.Studid == studentId).FirstOrDefault();
            stemailid = st.StudEmail;
            mobileno = st.StdMobNo;


            string msg = "Nanjil Catholic College of Arts & Science" + Environment.NewLine;
            msg = msg + "Your Profile Updated Successfully";


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

    }
}
