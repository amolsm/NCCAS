﻿using System;
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
using ASPSnippets.SmsAPI;
using System.Net;

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

        public FileContentResult getImg1(int id)
        {
            byte[] byteArray = db.tbl_employee.Where(m => m.Empid == id).Select(m => m.emppic).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
        }

        public FileContentResult getImg2(int id)
        {
            byte[] byteArray = db.tbl_employee.Where(m => m.Empid == id).Select(m => m.parentspic).FirstOrDefault();
            return byteArray != null ? new FileContentResult(byteArray, "image/jpeg") : null;
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
     
        public ActionResult EmpAdmission(int? Empid)
        {
            employeeviewmodel _bgv = new employeeviewmodel();
            FillPermission(54);
         
                _bgv.statelist = db.tbl_state.Where(m => m.status == true).ToList();
            _bgv.departmentlistdetails = db.tblDepartment.Where(m => m.Dept_id != 0).ToList();
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
      
        [HttpGet]
        public JsonResult FillEmpDetails(int empid)
        {
         
            //var empexperience = db.tbl_Empexperience.Where(m => m.empId == empid).ToString();
           
            var data = db.tbl_employee.Where(m => m.Empid == empid).FirstOrDefault();
            return Json( data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EmpAdmission_DML(employeeviewmodel _bgv, string action, HttpPostedFileBase files1, HttpPostedFileBase files2)
        {
            if (files1 != null)
            {
                if (files1.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        WebImage img = new WebImage(files1.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(600, 600);
                        }
                        byte[] array = img.GetBytes();
                        _bgv.emppic = array;
                    }
                }
            }
            else {
                if (_bgv.Empid != 0)
                {
                    _bgv.emppic = db.tbl_employee.Where(m => m.Empid == _bgv.Empid).Select(m => m.emppic).FirstOrDefault();
                }
            }

            if (files2 != null)
            {
                if (files2.InputStream.Length < 31000000)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        WebImage img = new WebImage(files2.InputStream);
                        if (img.Width > 700)
                        {
                            img.Resize(600, 600);
                        }
                        byte[] array = img.GetBytes();
                        _bgv.ppic = array;
                    }
                }
            }
            else
            {
                if (_bgv.Empid != 0)
                {
                    _bgv.ppic = db.tbl_employee.Where(m => m.Empid == _bgv.Empid).Select(m => m.parentspic).FirstOrDefault();
                }
            }
            if (action != "delete")
            {
                //db.sp_bloodgroup_DML(_bgv.bloodgroupid, _bgv.bloodgroupnm, _bgv.status, _bgv.academicyear, "").ToString();
                try
                {
                    db.sp_employee_DML(_bgv.Empid, _bgv.FirstName, _bgv.MiddleName, _bgv.LastName, _bgv.Cityid, _bgv.Stateid, _bgv.Zipcode, _bgv.Emailid, _bgv.PhoneNo, _bgv.MobileNo, _bgv.Address, _bgv.DOB, _bgv.Gender, _bgv.Quallification, _bgv.DateOfJoin, _bgv.Typeid, Convert.ToInt32(_bgv.Code), _bgv.OQualification, "NA", _bgv.edepartment, _bgv.eblood, _bgv.equalification1, _bgv.euniversity1, _bgv.eprecentage1, _bgv.eYearofpassing1, _bgv.equalification2, _bgv.euniversity2, _bgv.eprecentage2, _bgv.eYearofpassing2, _bgv.equalification3, _bgv.euniversity3, _bgv.eprecentage3, _bgv.eYearofpassing3, _bgv.equalification4, _bgv.euniversity4, _bgv.eprecentage4, _bgv.eYearofpassing4, _bgv.eweight, _bgv.eheight, _bgv.emppic, _bgv.eidtype, _bgv.dname, _bgv.DCode, _bgv.dcontact, _bgv.daddress, _bgv.relationship, _bgv.pname, _bgv.poccupation, _bgv.pqualification, _bgv.pemail, _bgv.pofficeaddress, _bgv.PCODE, _bgv.pcontact, _bgv.ppic, _bgv.pastreet, _bgv.pacity, _bgv.pastate, _bgv.pacountry, _bgv.papin, _bgv.cstreet, _bgv.ccity, _bgv.cstate, _bgv.ccountry, _bgv.cpin, _bgv.PHCODE, _bgv.phome, _bgv.MCODE, _bgv.hmobile, _bgv.bankname, _bgv.branch, _bgv.ifsccode, _bgv.accountno, _bgv.acname, _bgv.collagename, _bgv.university, _bgv.joiningdate, _bgv.lastdate, _bgv.total, _bgv.ldesignation, _bgv.companyname, _bgv.clastdesignation, _bgv.cjoiningdate, _bgv.clastdate, _bgv.ctotal, _bgv.employeeemail, _bgv.empreligion, _bgv.empcategory, _bgv.empcast, _bgv.mtongue, _bgv.collagename1, _bgv.university1, _bgv.joiningdate1, _bgv.lastdate1,
                         _bgv.total1, _bgv.ldesignation1, _bgv.companyname1, _bgv.clastdesignation1, _bgv.cjoiningdate1, _bgv.clastdate1, _bgv.ctotal1, _bgv.Documenttypename, _bgv.DocumentIDNo, _bgv.conttactrelation, _bgv.ParishName, _bgv.DioceseName,_bgv.collagename2,_bgv.university2,_bgv.joiningdate2, _bgv.lastdate2,
                         _bgv.total2, _bgv.ldesignation2, _bgv.companyname2, _bgv.clastdesignation2, _bgv.cjoiningdate2, _bgv.clastdate2, _bgv.ctotal2, _bgv.collagename3, _bgv.university3, _bgv.joiningdate3, _bgv.lastdate3,
                         _bgv.total3, _bgv.ldesignation3, _bgv.companyname3, _bgv.clastdesignation3, _bgv.cjoiningdate3, _bgv.clastdate3, _bgv.ctotal3, _bgv.collagename4, _bgv.university4, _bgv.joiningdate4, _bgv.lastdate4,
                         _bgv.total4, _bgv.ldesignation4, _bgv.companyname4, _bgv.clastdesignation4, _bgv.cjoiningdate4, _bgv.clastdate4, _bgv.ctotal4, _bgv.collagename5, _bgv.university5, _bgv.joiningdate5, _bgv.lastdate5,
                         _bgv.total5, _bgv.ldesignation5, _bgv.companyname5, _bgv.clastdesignation5, _bgv.cjoiningdate5, _bgv.clastdate5, _bgv.ctotal5, _bgv.collagename6, _bgv.university6, _bgv.joiningdate6, _bgv.lastdate6,
                         _bgv.total6, _bgv.ldesignation6, _bgv.companyname6, _bgv.clastdesignation6, _bgv.cjoiningdate6, _bgv.clastdate6, _bgv.ctotal6, _bgv.collagename7, _bgv.university7, _bgv.joiningdate7, _bgv.lastdate7,
                         _bgv.total7, _bgv.ldesignation7, _bgv.companyname7, _bgv.clastdesignation7, _bgv.cjoiningdate7, _bgv.clastdate7, _bgv.ctotal7, _bgv.collagename8, _bgv.university8, _bgv.joiningdate8, _bgv.lastdate8,
                         _bgv.total8, _bgv.ldesignation8, _bgv.companyname8, _bgv.clastdesignation8, _bgv.cjoiningdate8, _bgv.clastdate8, _bgv.ctotal8, _bgv.collagename9, _bgv.university9, _bgv.joiningdate9, _bgv.lastdate9,
                         _bgv.total9, _bgv.ldesignation9, _bgv.companyname9, _bgv.clastdesignation9, _bgv.cjoiningdate9, _bgv.clastdate9, _bgv.ctotal9
                         ).ToString();
                    //if (_bgv.Empid == 0)
                    //SendSMS();

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
            else if (action == "Delete")
            {
                //db.sp_bloodgroup_DML(id, _bgv.bloodgroupnm, _bgv.status, _bgv.academicyear, "del").ToString();

                //   db.sp_employee_DML(id, _bgv.FirstName, _bgv.MiddleName, _bgv.LastName, _bgv.Cityid, _bgv.Stateid, _bgv.Zipcode, _bgv.Emailid, _bgv.PhoneNo, _bgv.MobileNo, _bgv.Address, _bgv.DOB, _bgv.Gender, _bgv.Quallification, _bgv.DateOfJoin, _bgv.Typeid, Convert.ToInt32(_bgv.Code), _bgv.OQualification, "del").ToString();

                db.sp_employee_DML(_bgv.Empid, _bgv.FirstName, _bgv.MiddleName, _bgv.LastName, _bgv.Cityid, _bgv.Stateid, _bgv.Zipcode, _bgv.Emailid, _bgv.PhoneNo, _bgv.MobileNo, _bgv.Address, _bgv.DOB, _bgv.Gender, _bgv.Quallification, _bgv.DateOfJoin, _bgv.Typeid, Convert.ToInt32(_bgv.Code), _bgv.OQualification, "NA", _bgv.edepartment, _bgv.eblood, _bgv.equalification1, _bgv.euniversity1, _bgv.eprecentage1, _bgv.eYearofpassing1, _bgv.equalification2, _bgv.euniversity2, _bgv.eprecentage2, _bgv.eYearofpassing2, _bgv.equalification3, _bgv.euniversity3, _bgv.eprecentage3, _bgv.eYearofpassing3, _bgv.equalification4, _bgv.euniversity4, _bgv.eprecentage4, _bgv.eYearofpassing4, _bgv.eweight, _bgv.eheight, _bgv.emppic, _bgv.eidtype, _bgv.dname, _bgv.DCode, _bgv.dcontact, _bgv.daddress, _bgv.relationship, _bgv.pname, _bgv.poccupation, _bgv.pqualification, _bgv.pemail, _bgv.pofficeaddress, _bgv.PCODE, _bgv.pcontact, _bgv.ppic, _bgv.pastreet, _bgv.pacity, _bgv.pastate, _bgv.pacountry, _bgv.papin, _bgv.cstreet, _bgv.ccity, _bgv.cstate, _bgv.ccountry, _bgv.cpin, _bgv.PHCODE, _bgv.phome, _bgv.MCODE, _bgv.hmobile, _bgv.bankname, _bgv.branch, _bgv.ifsccode, _bgv.accountno, _bgv.acname, _bgv.collagename, _bgv.university, _bgv.joiningdate, _bgv.lastdate, _bgv.total, _bgv.ldesignation, _bgv.companyname, _bgv.clastdesignation, _bgv.cjoiningdate, _bgv.clastdate, _bgv.ctotal, _bgv.employeeemail, _bgv.empreligion, _bgv.empcategory, _bgv.empcast, _bgv.mtongue, _bgv.collagename1, _bgv.university1, _bgv.joiningdate1, _bgv.lastdate1,
                         _bgv.total1, _bgv.ldesignation1, _bgv.companyname1, _bgv.clastdesignation1, _bgv.cjoiningdate1, _bgv.clastdate1, _bgv.ctotal1, _bgv.Documenttypename, _bgv.DocumentIDNo, _bgv.conttactrelation, _bgv.ParishName, _bgv.DioceseName, _bgv.collagename2, _bgv.university2, _bgv.joiningdate2, _bgv.lastdate2,
                         _bgv.total2, _bgv.ldesignation2, _bgv.companyname2, _bgv.clastdesignation2, _bgv.cjoiningdate2, _bgv.clastdate2, _bgv.ctotal2, _bgv.collagename3, _bgv.university3, _bgv.joiningdate3, _bgv.lastdate3,
                         _bgv.total3, _bgv.ldesignation3, _bgv.companyname3, _bgv.clastdesignation3, _bgv.cjoiningdate3, _bgv.clastdate3, _bgv.ctotal3, _bgv.collagename4, _bgv.university4, _bgv.joiningdate4, _bgv.lastdate4,
                         _bgv.total4, _bgv.ldesignation4, _bgv.companyname4, _bgv.clastdesignation4, _bgv.cjoiningdate4, _bgv.clastdate4, _bgv.ctotal4, _bgv.collagename5, _bgv.university5, _bgv.joiningdate5, _bgv.lastdate5,
                         _bgv.total5, _bgv.ldesignation5, _bgv.companyname5, _bgv.clastdesignation5, _bgv.cjoiningdate5, _bgv.clastdate5, _bgv.ctotal5, _bgv.collagename6, _bgv.university6, _bgv.joiningdate6, _bgv.lastdate6,
                         _bgv.total6, _bgv.ldesignation6, _bgv.companyname6, _bgv.clastdesignation6, _bgv.cjoiningdate6, _bgv.clastdate6, _bgv.ctotal6, _bgv.collagename7, _bgv.university7, _bgv.joiningdate7, _bgv.lastdate7,
                         _bgv.total7, _bgv.ldesignation7, _bgv.companyname7, _bgv.clastdesignation7, _bgv.cjoiningdate7, _bgv.clastdate7, _bgv.ctotal7, _bgv.collagename8, _bgv.university8, _bgv.joiningdate8, _bgv.lastdate8,
                         _bgv.total8, _bgv.ldesignation8, _bgv.companyname8, _bgv.clastdesignation8, _bgv.cjoiningdate8, _bgv.clastdate8, _bgv.ctotal8, _bgv.collagename9, _bgv.university9, _bgv.joiningdate9, _bgv.lastdate9,
                         _bgv.total9, _bgv.ldesignation9, _bgv.companyname9, _bgv.clastdesignation9, _bgv.cjoiningdate9, _bgv.clastdate9, _bgv.ctotal9
                         ).ToString();
            }
            //_bgv._emplist = db.sp_getemp().ToList();
            return RedirectToAction("Index");
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

        public JsonResult check_duplicate_Emp(string Emailid)
        {
            var data = db.tbl_employee.Where(m => m.Emailid == Emailid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetExperience(string EmpId)
        {
            int employeeid = 0;
            if (EmpId != null && EmpId != "")
            {
                employeeid = Convert.ToInt32(EmpId);
            }
            var exp = db.tbl_Empexperience.Where(m => m.empId == employeeid).FirstOrDefault();
            return Json(exp, JsonRequestBehavior.AllowGet);
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

        //private void SendSMS()
        //{
        //    //string senderMobileNo = "9699026421";
        //    //string senderPassword = "password";
        //    //string MshapeKey = "ajW5MqLWtAmshow3gkBQzvSWW8g2p1d5RI3jsnzhpldUSvQanM";
        //    //bool isSent = true;
        //    //try
        //    //{

        //    //    // Calling SMS Class to use Send Method.
        //    //    // Passing MobileNo, Password, MshapreKey, ReceiverMobileNo and Message as parameter of Send Method.
        //    //    isSent = Send(senderMobileNo, senderPassword, MshapeKey, "8898284281", "teshvhv");

        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //}
        //    try
        //    {
        //        SMS.APIType = SMSGateway.Site2SMS;
        //        SMS.MashapeKey = "ajW5MqLWtAmshow3gkBQzvSWW8g2p1d5RI3jsnzhpldUSvQanM";
        //        SMS.Username = "9699026421";
        //        SMS.Password = "password";
        //        SMS.SendSms("8898284281", "teghfghf");

        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.ToString();
        //    }

        //}

        //public static bool Send(string senderMobileNo, string senderPassword, string MshapeKey, string receiverMobileNo, string Message)
        //{
        //    bool isSent = true;
        //    try
        //    {
        //        WebRequest request = WebRequest.Create("https://site2sms.p.mashape.com/index.php?msg="
        //            + Message + "&phone=" + receiverMobileNo + "&pwd=" + senderPassword + "&uid=" + senderMobileNo);
        //        request.Headers.Add("X-Mashape-Key", MshapeKey);
        //        WebResponse response = request.GetResponse();
        //        return isSent;
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.ToString();
        //        return false;
        //    }
        //}
    }
}
