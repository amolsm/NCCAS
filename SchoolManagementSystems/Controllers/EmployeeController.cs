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
        [SchoolManagementSystems.MvcApplication.SessionExpire]
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
        [SchoolManagementSystems.MvcApplication.SessionExpire]
        public ActionResult EmpAdmission(int? Empid)
        {
            employeeviewmodel _bgv = new employeeviewmodel();
            //FillPermission(54);
         
                _bgv.statelist = db.tbl_state.Where(m => m.status == true).ToList();
                
               _bgv._emplist = db.sp_getemp().ToList();
            _bgv.citylist = new List<tbl_city>();
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
        [SchoolManagementSystems.MvcApplication.SessionExpire]
        [HttpGet]
        public JsonResult FillEmpDetails(int Empid)
        {
            var data = db.tbl_employee.Where(m => m.Empid == Empid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EmpAdmission_DML(employeeviewmodel _bgv, string evt, int id, HttpPostedFileBase empfile, HttpPostedFileBase empfile1)
        {
            if (empfile != null)
            {
                if (empfile.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        WebImage img = new WebImage(empfile.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(600, 600);
                        }
                        byte[] array = img.GetBytes();
                        _bgv.emppic = array;
                    }
                }
            }

            if (empfile1 != null)
            {
                if (empfile1.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        WebImage img = new WebImage(empfile1.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(600, 600);
                        }
                        byte[] array = img.GetBytes();
                        _bgv.ppic = array;
                    }
                }
            }
            if (evt == "submit")
            {
                //db.sp_bloodgroup_DML(_bgv.bloodgroupid, _bgv.bloodgroupnm, _bgv.status, _bgv.academicyear, "").ToString();
                try
                {
                    db.sp_employee_DML(_bgv.Empid, _bgv.FirstName, _bgv.MiddleName, _bgv.LastName, _bgv.Cityid, _bgv.Stateid, _bgv.Zipcode, _bgv.Emailid, _bgv.PhoneNo, _bgv.MobileNo, _bgv.Address, _bgv.DOB, _bgv.Gender, _bgv.Quallification, _bgv.DateOfJoin, _bgv.Typeid, Convert.ToInt32(_bgv.Code), _bgv.OQualification, "NA", _bgv.edepartment, _bgv.eblood, _bgv.equalification1, _bgv.euniversity1, _bgv.eprecentage1, _bgv.eYearofpassing1, _bgv.equalification2, _bgv.euniversity2, _bgv.eprecentage2, _bgv.eYearofpassing2, _bgv.equalification3, _bgv.euniversity3, _bgv.eprecentage3, _bgv.eYearofpassing3, _bgv.equalification4, _bgv.euniversity4, _bgv.eprecentage4, _bgv.eYearofpassing4, _bgv.eweight, _bgv.eheight, _bgv.emppic, _bgv.eidtype, _bgv.dname, _bgv.DCode, _bgv.dcontact, _bgv.daddress, _bgv.relationship, _bgv.pname, _bgv.poccupation, _bgv.pqualification, _bgv.pemail, _bgv.pofficeaddress, _bgv.PCODE, _bgv.pcontact, _bgv.ppic, _bgv.pastreet, _bgv.pacity, _bgv.pastate, _bgv.pacountry, _bgv.papin, _bgv.cstreet, _bgv.ccity, _bgv.cstate, _bgv.ccountry, _bgv.cpin, _bgv.PHCODE, _bgv.phome, _bgv.MCODE, _bgv.hmobile, _bgv.bankname, _bgv.branch, _bgv.ifsccode, _bgv.accountno, _bgv.acname, _bgv.collagename, _bgv.university, _bgv.joiningdate, _bgv.lastdate, _bgv.total, _bgv.ldesignation, _bgv.companyname, _bgv.clastdesignation, _bgv.cjoiningdate, _bgv.clastdate, _bgv.ctotal, _bgv.employeeemail, _bgv.empreligion, _bgv.empcategory, _bgv.empcast, _bgv.mtongue).ToString();
                    //if (_bgv.Empid == 0)

                }
                catch (Exception ex)
                {
                    string msg = ex.ToString();
                }

                //{
                //    int Empid = db.tbl_employee.Where(m => m.Emailid == _bgv.Emailid).Select(m => m.Empid).FirstOrDefault();
                //    CreateUsers(_bgv.Emailid, _bgv.Typeid, Empid, "NA");
                //    SendEmails(_bgv.Emailid);
                //}
            }
            else if (evt == "Delete")
            {
                //db.sp_bloodgroup_DML(id, _bgv.bloodgroupnm, _bgv.status, _bgv.academicyear, "del").ToString();

                //   db.sp_employee_DML(id, _bgv.FirstName, _bgv.MiddleName, _bgv.LastName, _bgv.Cityid, _bgv.Stateid, _bgv.Zipcode, _bgv.Emailid, _bgv.PhoneNo, _bgv.MobileNo, _bgv.Address, _bgv.DOB, _bgv.Gender, _bgv.Quallification, _bgv.DateOfJoin, _bgv.Typeid, Convert.ToInt32(_bgv.Code), _bgv.OQualification, "del").ToString();

                db.sp_employee_DML(_bgv.Empid, _bgv.FirstName, _bgv.MiddleName, _bgv.LastName, _bgv.Cityid, _bgv.Stateid, _bgv.Zipcode, _bgv.Emailid, _bgv.PhoneNo, _bgv.MobileNo, _bgv.Address, _bgv.DOB, _bgv.Gender, _bgv.Quallification, _bgv.DateOfJoin, _bgv.Typeid, Convert.ToInt32(_bgv.Code), _bgv.OQualification, "Del", _bgv.edepartment, _bgv.eblood, _bgv.equalification1, _bgv.euniversity1, _bgv.eprecentage1, _bgv.eYearofpassing1, _bgv.equalification2, _bgv.euniversity2, _bgv.eprecentage2, _bgv.eYearofpassing2, _bgv.equalification3, _bgv.euniversity3, _bgv.eprecentage3, _bgv.eYearofpassing3, _bgv.equalification4, _bgv.euniversity4, _bgv.eprecentage4, _bgv.eYearofpassing4, _bgv.eweight, _bgv.eheight, _bgv.emppic, _bgv.eidtype, _bgv.dname, _bgv.DCode, _bgv.dcontact, _bgv.daddress, _bgv.relationship, _bgv.pname, _bgv.poccupation, _bgv.pqualification, _bgv.pemail, _bgv.pofficeaddress, _bgv.PCODE, _bgv.pcontact, _bgv.ppic, _bgv.pastreet, _bgv.pacity, _bgv.pastate, _bgv.pacountry, _bgv.papin, _bgv.cstreet, _bgv.ccity, _bgv.cstate, _bgv.ccountry, _bgv.cpin, _bgv.PHCODE, _bgv.phome, _bgv.MCODE, _bgv.hmobile, _bgv.bankname, _bgv.branch, _bgv.ifsccode, _bgv.accountno, _bgv.acname, _bgv.collagename, _bgv.university, _bgv.joiningdate, _bgv.lastdate, _bgv.total, _bgv.ldesignation, _bgv.companyname, _bgv.clastdesignation, _bgv.cjoiningdate, _bgv.clastdate, _bgv.ctotal, _bgv.employeeemail, _bgv.empreligion, _bgv.empcategory, _bgv.empcast, _bgv.mtongue).ToString();

            }
            //_bgv._emplist = db.sp_getemp().ToList();
            return RedirectToAction("Index");
        }
    }
}
