using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using System.Data.Entity;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Web.Helpers;

namespace SchoolManagementSystems.Controllers
{
    [HandleError]
    [SchoolManagementSystems.MvcApplication.SessionExpire]
    public class MastersController : Controller
    {
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
        #region Position master
        public ActionResult Position(string Search_Data)
        {
            positionviewmodel _pvm = new positionviewmodel();
            FillPermission(32);
            if (String.IsNullOrEmpty(Search_Data))
                _pvm._positionlist = db.sp_getposition().ToList();
            else
                _pvm._positionlist = db.sp_getposition().Where(x => x.posname.ToUpper().Contains(Search_Data.ToUpper())
                                                            || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            return View(_pvm);
        }
        public JsonResult FillPositionDetails(int posid)
        {
            var data = db.tbl_position.Where(m => m.posid == posid).FirstOrDefault();
         
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_position(string posname)
        {
            var data = db.tbl_position.Where(m => m.posname == posname).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLPosition(positionviewmodel _pvm, string evt, int id)
        {
            if (evt == "submit")
            {
                //db.sp_position_DML(_pvm.posid, _pvm.posname, _pvm.status, _pvm.academicyear, "").ToString();
                db.sp_position_DML(_pvm.posid, _pvm.posname, _pvm.status, "", "").ToString();
            }
            else if (evt == "Delete")
            {
                //db.sp_position_DML(id, _pvm.posname, _pvm.status, _pvm.academicyear, "del").ToString();
                db.sp_position_DML(id, _pvm.posname, _pvm.status, "", "del").ToString();
            }
            _pvm._positionlist = db.sp_getposition().ToList();
            return PartialView("_positionList", _pvm);
        }
        #endregion

        #region Leave master
        public ActionResult Leave(string Search_Data)
        {
            leaveviewmodel _lvm = new leaveviewmodel();
            FillPermission(33);
            if (String.IsNullOrEmpty(Search_Data))
                _lvm._Leavelist = db.sp_getLeave().ToList();
            else
                _lvm._Leavelist = db.sp_getLeave().Where(x => x.leavename.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                            x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            return View(_lvm);
        }
        public JsonResult FillLeaveDetails(int leaveid)
        {
            var data = db.tbl_leave.Where(m => m.leaveid == leaveid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Leave(string leavename)
        {
            var data = db.tbl_leave.Where(m => m.leavename == leavename).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLLeave(leaveviewmodel _lvm, string evt, int id)
        {
            if (evt == "submit")
            {
                //db.sp_leave_DML(_lvm.leaveid, _lvm.leavename, _lvm.status, _lvm.academicyear, "").ToString();
                db.sp_leave_DML(_lvm.leaveid, _lvm.leavename, _lvm.status, "", "", _lvm.leaveBalance).ToString();
            }
            else if (evt == "Delete")
            {
                //db.sp_leave_DML(id, _lvm.leavename, _lvm.status, _lvm.academicyear, "del").ToString();
                db.sp_leave_DML(id, _lvm.leavename, _lvm.status, "", "del", _lvm.leaveBalance).ToString();
            }
            _lvm._Leavelist = db.sp_getLeave().ToList();
            return PartialView("_leaveList", _lvm);
        }
        #endregion

        #region Payterm master
        public ActionResult Payterm(string Search_Data)
        {
            paytermviewmodel _pvm = new paytermviewmodel();
            FillPermission(34);
            if (String.IsNullOrEmpty(Search_Data))
                _pvm._Paytermlist = db.sp_getPayterm().ToList();
            else
                _pvm._Paytermlist = db.sp_getPayterm().Where(x => x.paytermname.ToUpper().Contains(Search_Data.ToUpper())
                                                        || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            return View(_pvm);
        }
        public JsonResult FillPaytermDetails(int paytermid)
        {
            var data = db.tbl_payterm.Where(m => m.paytermid == paytermid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Payterm(string paytermname)
        {
            var data = db.tbl_payterm.Where(m => m.paytermname == paytermname).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLPayterm(paytermviewmodel _pvm, string evt, int id)
        {
            if (evt == "submit")
            {
                //db.sp_payterm_DML(_pvm.paytermid, _pvm.paytermname, _pvm.status, _pvm.academicyear, "").ToString();
                db.sp_payterm_DML(_pvm.paytermid, _pvm.paytermname, _pvm.status, "", "").ToString();
            }
            else if (evt == "Delete")
            {
                //db.sp_payterm_DML(id, _pvm.paytermname, _pvm.status, _pvm.academicyear, "del").ToString();
                db.sp_payterm_DML(id, _pvm.paytermname, _pvm.status, "", "del").ToString();
            }
            _pvm._Paytermlist = db.sp_getPayterm().ToList();
            return PartialView("_paytermList", _pvm);
        }
        #endregion

        #region Class master
        public ActionResult Class(string Search_Data)
        {
            classviewmodel _cvm = new classviewmodel();
            _cvm.academicyear = GetYear();
            FillPermission(38);
            if (String.IsNullOrEmpty(Search_Data))
                _cvm._Classlist = db.sp_getClass().ToList();
            else
                _cvm._Classlist = db.sp_getClass().Where(x => x.Classnm.ToUpper().Contains(Search_Data.ToUpper())
                        || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            _cvm.departmentlistdetails = db.tblDepartment.ToList();

       
            return View(_cvm);
        }
        public JsonResult FillClassDetails(int Classid)
        {
            var data = db.tbl_class.Where(m => m.Classid == Classid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Class(string Classnm)
        {
            var data = db.tbl_class.Where(m => m.Classnm == Classnm).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLClass(classviewmodel _cvm, string evt, int id)
        {
            string yr = _cvm.academicyear[0].ToString();
            if (evt == "submit")
            {
                db.sp_class_DML(_cvm.Classid, _cvm.Classnm,_cvm.Dept_Id, _cvm.status, yr, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_class_DML(id, _cvm.Classnm,_cvm.Dept_Id, _cvm.status, yr, "del").ToString();
            }
            _cvm._Classlist = db.sp_getClass().ToList();
            return PartialView("_ClassList", _cvm);
        }
        #endregion

        #region Religion master
        public ActionResult Religion(string Search_Data)
        {
            religionviewmodel _rvm = new religionviewmodel();
            FillPermission(40);
            if (String.IsNullOrEmpty(Search_Data))
                _rvm._Religionlist = db.sp_getReligion().ToList();
            else
                _rvm._Religionlist = db.sp_getReligion().Where(x => x.Religionnm.ToUpper().Contains(Search_Data.ToUpper())
                                                                || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            return View(_rvm);
        }
        public JsonResult FillReligionDetails(int Religionid)
        {
            var data = db.tbl_religion.Where(m => m.Religionid == Religionid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Religion(string Religionnm)
        {
            var data = db.tbl_religion.Where(m => m.Religionnm == Religionnm).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLReligion(religionviewmodel _rvm, string evt, int id)
        {
            if (evt == "submit")
            {
                //db.sp_religion_DML(_rvm.Religionid, _rvm.Religionnm, _rvm.status, _rvm.academicyear, "").ToString();
                db.sp_religion_DML(_rvm.Religionid, _rvm.Religionnm, _rvm.status, "", "").ToString();
            }
            else if (evt == "Delete")
            {
                //db.sp_religion_DML(id, _rvm.Religionnm, _rvm.status, _rvm.academicyear, "del").ToString();
                db.sp_religion_DML(id, _rvm.Religionnm, _rvm.status, "", "del").ToString();
            }
            _rvm._Religionlist = db.sp_getReligion().ToList();
            return PartialView("_ReligionList", _rvm);
        }
        #endregion

        #region Qualification master
        public ActionResult Qualification(string Search_Data)
        {
            qualificationviewmodel _qvm = new qualificationviewmodel();
            FillPermission(44);
            if (String.IsNullOrEmpty(Search_Data))
                _qvm._Qualificationlist = db.sp_getQualification().ToList();
            else
                _qvm._Qualificationlist = db.sp_getQualification().Where(x => x.Qualificationnm.ToUpper().Contains(Search_Data.ToUpper())
                                                                        || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            return View(_qvm);
        }
        public JsonResult FillQualificationDetails(int qualificationid)
        {
            var data = db.tbl_qualification.Where(m => m.qualificationid == qualificationid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Qualification(string qualificationnm)
        {
            var data = db.tbl_qualification.Where(m => m.qualificationnm == qualificationnm).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLQualification(qualificationviewmodel _qvm, string evt, int id)
        {
            if (evt == "submit")
            {
                db.sp_qualification_DML(_qvm.qualificationid, _qvm.qualificationnm, _qvm.status, "", "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_qualification_DML(id, _qvm.qualificationnm, _qvm.status, "", "del").ToString();
            }
            _qvm._Qualificationlist = db.sp_getQualification().ToList();
            return PartialView("_QualificationList", _qvm);
        }
        #endregion

        #region Country master
        public ActionResult Country(string Search_Data)
        {
            countryviewmodel _cvm = new countryviewmodel();
            FillPermission(35);
            if (String.IsNullOrEmpty(Search_Data))
                _cvm._Countrylist = db.sp_getCountry().ToList();
            else
                _cvm._Countrylist = db.sp_getCountry().Where(x => x.CountryName.ToUpper().Contains(Search_Data.ToUpper())
                                                            || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            return View(_cvm);
        }
        public JsonResult FillCountryDetails(int Countryid)
        {
            var data = db.tbl_country.Where(m => m.Countryid == Countryid).FirstOrDefault();
         
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Country(string CountryName)
        {
            var data = db.tbl_country.Where(m => m.CountryName == CountryName).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLCountry(countryviewmodel _cvm, string evt, int id)
        {
            if (evt == "submit")
            {
                db.sp_Country_DML(_cvm.Countryid, _cvm.CountryName, _cvm.status, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_Country_DML(id, _cvm.CountryName, _cvm.status, "del").ToString();
            }
            _cvm._Countrylist = db.sp_getCountry().ToList();
            return PartialView("_CountryList", _cvm);
        }
        #endregion

        #region State master
        public ActionResult State(string Search_Data)
        {
            stateviewmodel _svm = new stateviewmodel();
            FillPermission(36);
            if (String.IsNullOrEmpty(Search_Data))
            {
                _svm.countrylist = db.tbl_country.Where(c => c.status == true).ToList();
                _svm._Statelist = db.sp_getState().ToList();
            }
            else
            {
                _svm.countrylist = db.tbl_country.Where(c => c.status == true).ToList();
                _svm._Statelist = db.sp_getState().Where(x => x.StateName.ToUpper().Contains(Search_Data.ToUpper())
                                                        || x.CountryName.ToUpper().Contains(Search_Data.ToUpper())
                                                        || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            }

            return View(_svm);
        }
        public JsonResult FillStateDetails(int Stateid)
        {
            var data = db.tbl_state.Where(m => m.Stateid == Stateid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_State(string StateName, int Countryid)
        {
            var data = db.tbl_state.Where(m => m.StateName == StateName && m.Countryid == Countryid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLState(stateviewmodel _svm, string evt, int id)
        {
            if (evt == "submit")
            {
                db.sp_State_DML(_svm.Stateid, _svm.StateName, _svm.Countryid, _svm.status, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_State_DML(id, _svm.StateName, _svm.Countryid, _svm.status, "del").ToString();
            }
            _svm._Statelist = db.sp_getState().ToList();
            return PartialView("_StateList", _svm);
        }
        #endregion

        #region City master
        public ActionResult City(string Search_Data)
        {
            cityviewmodel _cvm = new cityviewmodel();
            FillPermission(37);
            if (String.IsNullOrEmpty(Search_Data))
            {
                _cvm.countrylist = db.tbl_country.Where(c => c.status == true).ToList();
                _cvm._Citylist = db.sp_getCity().ToList();
                _cvm.statelist = new List<tbl_state>();
            }
            else
            {
                _cvm.countrylist = db.tbl_country.Where(c => c.status == true).ToList();
                _cvm._Citylist = db.sp_getCity().Where(x => x.CityName.ToUpper().Contains(Search_Data.ToUpper())
                                                       || x.CountryName.ToUpper().Contains(Search_Data.ToUpper())
                                                       || x.StateName.ToUpper().Contains(Search_Data.ToUpper())
                                                       || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
                _cvm.statelist = new List<tbl_state>();
            }

            return View(_cvm);
        }
        public JsonResult GetStates(string id)
        {
            int countryid = 0;
            if (id != null && id != "")
            {
                countryid = Convert.ToInt32(id);
            }
            var states = db.tbl_state.Where(m => m.Countryid == countryid && m.status == true).ToList();
            return Json(new SelectList(states, "Stateid", "StateName"));
        }
        public JsonResult FillCityDetails(int Cityid)
        {
            var data = db.tbl_city.Where(m => m.Cityid == Cityid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_City(string CityName, int Countryid, int Stateid)
        {
            var data = db.tbl_city.Where(m => m.CityName == CityName && m.Countryid == Countryid && m.Stateid == Stateid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLCity(cityviewmodel _cvm, string evt, int id)
        {
            if (evt == "submit")
            {
                db.sp_City_DML(_cvm.Cityid, _cvm.CityName, _cvm.Countryid, _cvm.Stateid, _cvm.status, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_City_DML(id, _cvm.CityName, _cvm.Countryid, _cvm.Stateid, _cvm.status, "del").ToString();
            }
            _cvm._Citylist = db.sp_getCity().ToList();
            return PartialView("_CityList", _cvm);
        }
        #endregion

        #region Caste master
        public ActionResult Caste(string Search_Data)
        {
            casteviewmodel _cvm = new casteviewmodel();
            FillPermission(42);
            if (String.IsNullOrEmpty(Search_Data))
            {
                _cvm.Religionlist = db.tbl_religion.Where(c => c.status == true).ToList();
                _cvm._Castelist = db.sp_getCaste().ToList();
            }
            else
            {
                _cvm.Religionlist = db.tbl_religion.Where(c => c.status == true).ToList();
                _cvm._Castelist = db.sp_getCaste().Where(x => x.CasteName.ToUpper().Contains(Search_Data.ToUpper())
                                                         || x.Religionnm.ToUpper().Contains(Search_Data.ToUpper())
                                                         || x.status.Contains(Search_Data.ToUpper())).ToList();
            }

            return View(_cvm);
        }
        public JsonResult FillCasteDetails(int Casteid)
        {
            var data = db.tbl_caste.Where(m => m.Casteid == Casteid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Caste(string CasteName, int Religionid)
        {
            var data = db.tbl_caste.Where(m => m.CasteName == CasteName && m.Religionid == Religionid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLCaste(casteviewmodel _cvm, string evt, int id)
        {
            if (evt == "submit")
            {
                //db.sp_Caste_DML(_cvm.Casteid, _cvm.CasteName, _cvm.Religionid, _cvm.status, _cvm.academicyear, "").ToString();
                db.sp_Caste_DML(_cvm.Casteid, _cvm.CasteName, _cvm.Religionid, _cvm.status, "", "").ToString();
            }
            else if (evt == "Delete")
            {
                //db.sp_Caste_DML(id, _cvm.CasteName, _cvm.Religionid, _cvm.status, _cvm.academicyear, "del").ToString();
                db.sp_Caste_DML(id, _cvm.CasteName, _cvm.Religionid, _cvm.status, "", "del").ToString();
            }
            _cvm._Castelist = db.sp_getCaste().ToList();
            return PartialView("_CasteList", _cvm);
        }
        #endregion

        #region subject master
        public ActionResult Subject(string Search_Data)
        {
            Subjectviewmodel _svm = new Subjectviewmodel();
            FillPermission(43);
            _svm.academicyear = GetYear();
            if (String.IsNullOrEmpty(Search_Data))
            {
                _svm.Courselist = db.tbl_CourseMaster.Where(c => c.Status == true).ToList();
                _svm._Subjectlist = db.sp_getSubject().ToList();
            }
            else
            {
                _svm.Courselist = db.tbl_CourseMaster.Where(c => c.Status == true).ToList();
                _svm._Subjectlist = db.sp_getSubject().Where(c => c.Classnm.ToString().ToUpper().Contains(Search_Data.ToUpper())
                                                             || c.SubjectNm.ToUpper().Contains(Search_Data.ToUpper())
                                                             || c.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            }
            _svm._courselist = db.sp_GetCourseforsubject().ToList();
            _svm.yearlist = db.tbl_YearMaster.ToList();
            _svm.departmentlistdetails=db.tblDepartment.Where(c => c.Status == true).ToList();
            return View(_svm);
        }
        public JsonResult FillSubjectDetails(int Subjectid)
        {
            var data = db.tbl_subject.Where(m => m.Subjectid == Subjectid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Subject(string SubjectNm, int Courseid,int Dept_id,int academicyear)
        {
            var data = db.tbl_subject.Where(m => m.SubjectNm == (SubjectNm).Trim() && m.Courseid == Courseid &&   m.DeptId == Dept_id &&   m.yearid == academicyear).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLSubject(Subjectviewmodel svm, string evt, int id)
        {
            svm.CreatedBy = Convert.ToInt32(Session["Userid"].ToString());
            string yr = svm.academicyear[0].ToString();
            if (evt == "submit")
            {
                db.sp_Subject_DML(svm.Subjectid, svm.Courseid, svm.SubjectNm, svm.Marks, svm.Pass_Marks, svm.status, yr, "", svm.Dept_Id, svm.CreatedBy, svm.subjectcode).ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_Subject_DML(id, svm.Courseid, svm.SubjectNm, svm.Marks, svm.Pass_Marks, svm.status, yr, "del", svm.Dept_Id, svm.CreatedBy, svm.subjectcode).ToString();
            }
            svm._Subjectlist = db.sp_getSubject().ToList();
            return PartialView("_SubjectList", svm);
        }

       
        #endregion

        #region User master
        public ActionResult User(string Search_Data)
        {
            Userviewmodel _uvm = new Userviewmodel();
            FillPermission(39);
            if (String.IsNullOrEmpty(Search_Data))
                _uvm._Userlist = db.sp_getUser().ToList();
            else
                _uvm._Userlist = db.sp_getUser().Where(x => x.UserName.ToUpper().Contains(Search_Data.ToUpper())
                                                        || x.Password.ToUpper().Contains(Search_Data.ToUpper())
                                                        || x.Type.ToUpper().Contains(Search_Data.ToUpper())
                                                        || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            return View(_uvm);
        }
        public JsonResult GetStudent(int Type)
        {
            var Peoples = db.tbl_student.Select(m => new { Genid = m.Studid, GenName = m.Studnm }).ToList();
            return Json(new SelectList(Peoples, "Genid", "GenName"));
        }
        public JsonResult GetParent(int Type)
        {
            var Peoples = db.tbl_student.Select(m => new { Genid = m.Studid, GenName = m.Studfathernm }).ToList();
            return Json(new SelectList(Peoples, "Genid", "GenName"));
        }
        public JsonResult GetAdmin(int Type)
        {
            var Peoples = db.tbl_employee.Where(m => m.Typeid == Type).Select(m => new { Genid = m.Empid, GenName = m.FirstName }).ToList();
            return Json(new SelectList(Peoples, "Genid", "GenName"));
        }
        public JsonResult GetTeacherorEmployee(int Type)
        {
            var Peoples = db.tbl_employee.Where(m => m.Typeid == Type).Select(m => new { Genid = m.Empid, GenName = m.FirstName }).ToList();
            return Json(new SelectList(Peoples, "Genid", "GenName"));
        }

        public JsonResult FillUserDetails(int Userid)
        {
            var data = db.tbl_user.Where(m => m.Userid == Userid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_User(string UserName)
        {
            var data = db.tbl_user.Where(m => m.UserName == UserName).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLUser(Userviewmodel _uvm, string evt, int id)
        {
            if (evt == "submit")
            {
                db.sp_User_DML(_uvm.Userid, _uvm.UserName, _uvm.Password, _uvm.Type, _uvm.Genid, _uvm.Status, _uvm.academicyear, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_User_DML(id, _uvm.UserName, _uvm.Password, _uvm.Type, _uvm.Genid, _uvm.Status, _uvm.academicyear, "del").ToString();
            }
            _uvm._Userlist = db.sp_getUser().ToList();
            return PartialView("_UserList", _uvm);
        }
        #endregion

        #region Occupation master
        public ActionResult Occupation(string Search_Data)
        {
            occupationviewmodel _ovm = new occupationviewmodel();
            FillPermission(45);
            if (String.IsNullOrEmpty(Search_Data))
                _ovm._occupationlist = db.sp_getOccupation().ToList();
            else
                _ovm._occupationlist = db.sp_getOccupation().Where(x => x.Occupationnm.ToUpper().Contains(Search_Data.ToUpper())
                                                                    || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            return View(_ovm);
        }
        public JsonResult FillOccupationDetails(int occupationid)
        {
            var data = db.tbl_occupation.Where(m => m.occupationid == occupationid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Occupation(string occupationnm)
        {
            var data = db.tbl_occupation.Where(m => m.occupationnm == occupationnm).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLOccupation(occupationviewmodel _ovm, string evt, int id)
        {
            if (evt == "submit")
            {
                //db.sp_occupation_DML(_ovm.occupationid, _ovm.occupationnm, _ovm.status, _ovm.academicyear, "").ToString();
                db.sp_occupation_DML(_ovm.occupationid, _ovm.occupationnm, _ovm.status, "", "").ToString();
            }
            else if (evt == "Delete")
            {
                //db.sp_occupation_DML(id, _ovm.occupationnm, _ovm.status, _ovm.academicyear, "del").ToString();
                db.sp_occupation_DML(id, _ovm.occupationnm, _ovm.status, "", "del").ToString();
            }
            _ovm._occupationlist = db.sp_getOccupation().ToList();
            return PartialView("_OccupationList", _ovm);
        }
        #endregion

        #region Blood Group master
        public ActionResult BloodGroup(string Search_Data)
        {
            bloodgroupviewmodel _bgv = new bloodgroupviewmodel();
            FillPermission(46);
            if (String.IsNullOrEmpty(Search_Data))
                _bgv._bloodgrouplist = db.sp_getBloodgroup().ToList();
            else
                _bgv._bloodgrouplist = db.sp_getBloodgroup().Where(x => x.Bloodgroupnm.ToUpper().Contains(Search_Data.ToUpper())
                                                                    || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            return View(_bgv);
        }
        public JsonResult FillBloodGroupDetails(int bloodgroupid)
        {
            var data = db.tbl_bloodgroup.Where(m => m.bloodgroupid == bloodgroupid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_BloodGroup(string bloodgroupnm)
        {
            var data = db.tbl_bloodgroup.Where(m => m.bloodgroupnm == bloodgroupnm).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLBloodGroup(bloodgroupviewmodel _bgv, string evt, int id)
        {
            if (evt == "submit")
            {
                //db.sp_bloodgroup_DML(_bgv.bloodgroupid, _bgv.bloodgroupnm, _bgv.status, _bgv.academicyear, "").ToString();
                db.sp_bloodgroup_DML(_bgv.bloodgroupid, _bgv.bloodgroupnm, _bgv.status, "", "").ToString();
            }
            else if (evt == "Delete")
            {
                //db.sp_bloodgroup_DML(id, _bgv.bloodgroupnm, _bgv.status, _bgv.academicyear, "del").ToString();
                db.sp_bloodgroup_DML(id, _bgv.bloodgroupnm, _bgv.status, "", "del").ToString();
            }
            _bgv._bloodgrouplist = db.sp_getBloodgroup().ToList();
            return PartialView("_BloodgroupList", _bgv);
        }
        #endregion

        #region Employee master
        //public ActionResult EmployeeList()
        //{
        //    var emps = db.tbl_employee.ToList();

        //    var emplist = new List<EmployeeListViewModel>();

        //    foreach (var m in emps)
        //    {
        //        if(m.Gender ==1)
        //        emplist.Add(new EmployeeListViewModel { EmployeeId = m.Empid, EmployeeName = m.FirstName +" "+ m.LastName, Gender = "Male", EmailId = m.Emailid, MobileNo = m.MobileNo });
        //        if (m.Gender == 2)
        //            emplist.Add(new EmployeeListViewModel { EmployeeId = m.Empid, EmployeeName = m.FirstName + " " + m.LastName, Gender = "Female", EmailId = m.Emailid, MobileNo = m.MobileNo });
        //    }

        //    return View(");
        //}
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
       
        public ActionResult Employee(string Search_Data)
        {
            employeeviewmodel _bgv = new employeeviewmodel();

            FillPermission(47);


            var data = db.sp_getemp().ToList();
            ViewBag.userdetails = data;
            //return View(); 
            if (String.IsNullOrEmpty(Search_Data))
            {

                _bgv.statelist = db.tbl_state.Where(m => m.status == true).ToList();
                _bgv.citylist = new List<tbl_city>();

                _bgv.qualificationlist = db.tbl_qualification.Where(m => m.status == true).ToList();
                _bgv.departmentlistdetails = db.tblDepartment.ToList();
                _bgv.bloodgrouplist = db.tbl_bloodgroup.ToList();
            }
            else
            {
                _bgv.statelist = db.tbl_state.Where(m => m.status == true).ToList();
                _bgv.citylist = new List<tbl_city>();

              //  _bgv._emplist = db.sp_getemp().Where(x => x.FirstName.ToUpper().Contains(Search_Data.ToUpper())
                                                    //|| x.LastName.ToUpper().Contains(Search_Data.ToUpper())
                                                    //|| x.MiddleName.ToUpper().Contains(Search_Data.ToUpper())
                                                    //|| x.Status.ToString().ToUpper().Contains(Search_Data.ToUpper())
                                                    //|| x.Stateid.ToUpper().Contains(Search_Data.ToUpper())
                                                    //|| x.Cityid.ToUpper().Contains(Search_Data.ToUpper())
                                                    //|| x.Emailid.ToUpper().Contains(Search_Data.ToUpper())
                                                    //|| x.MobileNo.ToUpper().Contains(Search_Data.ToUpper())
                                                    //|| x.Gender.ToUpper().Contains(Search_Data.ToUpper())
                                                    //|| x.Quallification.ToUpper().Contains(Search_Data.ToUpper())
                                                    //|| x.Typeid.ToUpper().Contains(Search_Data.ToUpper())).ToList();
                //_bgv.qualificationlist = db.tbl_qualification.Where(m => m.status == true).ToList();

            }
           
              _bgv.countrylist = db.tbl_country.ToList();
            return View(_bgv);
        }
        public JsonResult FillEmpDetails(int Empid)
        {
            var data = db.tbl_employee.Where(m => m.Empid == Empid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Emp(string Emailid)
        {
            var data = db.tbl_employee.Where(m => m.Emailid == Emailid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }          
        public ActionResult DMLEmp(employeeviewmodel _bgv, string evt, int id, HttpPostedFileBase empfile, HttpPostedFileBase empfile1)
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
            var data = db.sp_getemp().ToList();
            ViewBag.userdetails = data;
            //_bgv._emplist = db.sp_getemp().ToList();

            return PartialView("_EmpList", _bgv);
        }
        private void CreateUsers(string UserName, int Type, int Genid, string academicyear)
        {
            string Password = Guid.NewGuid().ToString().Substring(0, 8);
            var x = (from y in db.tbl_user where y.UserName == UserName select y).FirstOrDefault();
            if (x == null)
            {
                db.sp_User_DML(0, UserName, Password, Type, Genid, 0, academicyear, "");
            }
        }
        private void SendEmails(string Email)
        {
            string pass = db.tbl_user.Where(m => m.UserName == Email).Select(m => m.Password).FirstOrDefault();
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"].ToString(), ConfigurationManager.AppSettings["SenderName"].ToString());

            message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
            message.SubjectEncoding = System.Text.Encoding.GetEncoding("utf-8");
            message.From = fromAddress;
            message.To.Add(Email);
            message.Subject = "A-one School Management";
            message.Priority = MailPriority.High;
            message.IsBodyHtml = true;

            string msg = "<b>“Welcome to A-One School Management Systems”</b><br/><br/>";
            msg = msg + "Your UserName : " + Email + ".<br/>";
            msg = msg + "Your Password : " + pass + ".<br/>";
            msg = msg + "Hope we would be going long term relationship with feature with good Support<br/><br/>Best Regards<br/>A-One School Team";
            message.Body = msg;
            smtpClient.Send(message);
        }
        #endregion

        #region Category master
        public ActionResult Category(string Search_Data)
        {
            Entity.StudentCategoryviewmodel _svm = new StudentCategoryviewmodel();
            FillPermission(41);
            if (String.IsNullOrEmpty(Search_Data))
                _svm._categorylist = db.sp_getStudentCategory().ToList();
            else
                _svm._categorylist = db.sp_getStudentCategory().Where(x => x.StudCategoryname.ToUpper().Contains(Search_Data.ToUpper())
                                                                    || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            return View(_svm);
        }
        public JsonResult FillCategoryDetails(int catid)
        {
            var data = db.tbl_StudentCategory.Where(m => m.StudCategoryid == catid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_category(string Categoryname)
        {
            var data = db.tbl_StudentCategory.Where(m => m.StudCategoryname == Categoryname).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLCategory(StudentCategoryviewmodel _svm, string evt, int id)
        {
            if (evt == "submit")
            {
                //db.sp_position_DML(_pvm.posid, _pvm.posname, _pvm.status, _pvm.academicyear, "").ToString();
                db.sp_StudentCategory_DML(_svm.categoryid, _svm.categoryname, _svm.status, "").ToString();
            }
            else if (evt == "Delete")
            {
                //db.sp_position_DML(id, _pvm.posname, _pvm.status, _pvm.academicyear, "del").ToString();
                db.sp_StudentCategory_DML(id, _svm.categoryname, _svm.status, "del").ToString();
            }
            _svm._categorylist = db.sp_getStudentCategory().ToList();
            return PartialView("_categoryList", _svm);
        }
        #endregion

        #region Fees Label master
        public ActionResult FeesLabels()
        {
            feeslabelviewmodel _pvm = new feeslabelviewmodel();
            FillPermission(48);
            _pvm._CourseList = db.sp_GetCoursefordevision().ToList();
            //  _pvm.Classlist = db.tbl_class.Where(c => c.status == true).ToList();
            _pvm._feeslabellist = db.sp_getfeeslabels().ToList();
            return View(_pvm);
        }
        public JsonResult FillfeeslabelsDetails(int feeslblid)
        {
            var data = db.tbl_feeslabel.Where(m => m.feeslblid == feeslblid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FillfeeslabelchildDetails(int feeslblid)
        {
            var data = db.tbl_feeslabelchild.Where(m => m.feeslblid == feeslblid).Select(m => m.ctrlnm).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_feeslabels(int classid)
        {
            var data = db.tbl_feeslabel.Where(m => m.classid == classid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLFeesLabels(feeslabelviewmodel _pvm, string evt, int id, string FeesLabels)
        {
            int Lastfeeslblid = 0;
            if (evt == "submit")
            {
                db.sp_feeslabel_DML(_pvm.feeslblid, _pvm.ctrlcnt, _pvm.courseyearid, "").ToString();
            }
            if (_pvm.feeslblid == 0)
            {
                Lastfeeslblid = db.tbl_feeslabel.OrderByDescending(m => m.feeslblid).Select(m => m.feeslblid).FirstOrDefault();
            }
            else
            {
                Lastfeeslblid = _pvm.feeslblid;
            }
            db.sp_feeslabelchild_DML(Lastfeeslblid, "", "del").ToString();
            string[] labels = FeesLabels.ToString().Replace("undefined", "").Split('|');
            string lbl;
            for (int i = 0; i < labels.Count() - 1; i++)
            {
                lbl = labels[i].ToString();
                db.sp_feeslabelchild_DML(Lastfeeslblid, lbl, "").ToString();
            }
            _pvm._feeslabellist = db.sp_getfeeslabels().ToList();
            return PartialView("_FeesLabelsList", _pvm);
        }
        #endregion

        #region Chapter master
        public ActionResult Chapter(string Search_Data)
        {
            
            chapterviewmodel _chapter = new chapterviewmodel();
        
            _chapter.YearList = db.tbl_YearMaster.ToList();//new List<tbl_YearMaster>();
            _chapter.DepartmentList = db.tblDepartment.ToList(); // new List<tblDepartment>();
            _chapter.Classlist = db.tbl_CourseMaster.ToList();
            _chapter._subjectlist = db.tbl_subject.ToList();
            
            FillPermission(49);
            if (String.IsNullOrEmpty(Search_Data))
            {
                _chapter.Classlist = db.tbl_CourseMaster.ToList();
                _chapter._chapterlists = db.sp_getchapter().ToList();
            }
            else
            {
                _chapter._chapterlists = db.sp_getchapter().Where(x => x.ChapterName.ToUpper().Contains(Search_Data.ToUpper())
                                                            || x.Status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            }
            return View(_chapter);
        }
        public ActionResult DMLchapter(chapterviewmodel _chapter, string evt, int id)
        {
            _chapter.teacherid = Convert.ToInt32(Session["Userid"].ToString());// HttpContext.Current.Session["Userid"]; 
            if (evt == "submit")
            {
                db.sp_chapter_DML(_chapter.chapterid, _chapter.chaptername, _chapter.description, _chapter.status, _chapter.subjectid, _chapter.year, _chapter.department, _chapter.teacherid, _chapter.Classid, _chapter.teacherid, "act").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_chapter_DML(_chapter.chapterid, _chapter.chaptername, _chapter.description, _chapter.status, _chapter.subjectid,  _chapter.year, _chapter.department,_chapter.teacherid, _chapter.Classid, _chapter.teacherid,"del").ToString();
            }
            _chapter._chapterlists = db.sp_getchapter().ToList();
            return PartialView("_ChapterList", _chapter);
        }
        public JsonResult FillChapterDetails(int Chapterid)
        {
            var data = db.tbl_Chapter.Where(m => m.Chapter_id == Chapterid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_chapter(string ChapterName)
        {
            var data = db.tbl_Chapter.Where(m => m.ChapterName == ChapterName).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetSubjects(string id, string year,string department)
        {
            int subid = Convert.ToInt32(Session["Genid"].ToString());
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
        #endregion

        #region Content master
        public ActionResult Contents(string Search_Data)
        {
            contentviewmodel _content = new contentviewmodel();
            _content.YearList = db.tbl_YearMaster.ToList();//new List<tbl_YearMaster>();
            _content.DepartmentList = db.tblDepartment.ToList(); // new List<tblDepartment>();
            _content.Classlist = db.tbl_CourseMaster.ToList();
            _content._subjectlist = db.tbl_subject.ToList();//new List<tbl_subject>()
            FillPermission(49);
            if (String.IsNullOrEmpty(Search_Data))
            {
                _content.Classlist = db.tbl_CourseMaster.ToList();
                _content._chaptername = db.tbl_Chapter.ToList();
                
            }
            else
            {
                _content.Classlist = db.tbl_CourseMaster.ToList();
                _content._chaptername = db.tbl_Chapter.ToList();
            }
            
            var data = db.sp_getcontent().ToList();
            ViewBag.contentdetails = data; 
            return View(_content);
        }
        public ActionResult DMLContent(contentviewmodel _content, string evt, int id)
        {
            _content.createdby = Convert.ToInt32(Session["Userid"].ToString());
            _content.teacherid = Convert.ToInt32(Session["Userid"].ToString());
            if (evt == "submit")
            {
                db.sp_Content_DML(_content.contentid, _content.contentname, _content.chapterid, _content.cdescription, _content.status, _content.teacherid, _content.Classid, _content.department, _content.subjectid, _content.year,_content.createdby, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_Content_DML(_content.contentid, _content.contentname, _content.chapterid, _content.cdescription, _content.status, _content.teacherid, _content.Classid, _content.department, _content.subjectid, _content.year,_content.createdby, "del").ToString();
            }
            var data = db.sp_getcontent().ToList();
            ViewBag.contentdetails = data; 
            return PartialView("_ContentList", _content);
        }
        public JsonResult FillContentDetails(int contentid)
        {  
                var data = db.tbl_Content.Where(m => m.Content_id == contentid).FirstOrDefault();
                return Json(data, JsonRequestBehavior.AllowGet);          
        }
        public JsonResult check_duplicate_content(string ContentName)
        {
            var data = db.tbl_Content.Where(m => m.Content_Name == ContentName).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetChapter(string year,string dept,string subject,string courseid)
        {
            contentviewmodel _content = new contentviewmodel();
            //int teacherid = Convert.ToInt32(Session["Userid"].ToString());
            int subjectid = 0;
            int yeari = 0;
            int depti = 0;
            int coursei = 0;
            if (subject != null && subject != "" && year != null && year != "" && dept != null && dept != "" && courseid != null && courseid != "")
            {
                subjectid = Convert.ToInt32(subject);
                yeari = Convert.ToInt32(year);
                depti = Convert.ToInt32(dept);
                coursei = Convert.ToInt32(courseid); 

            }
            var chapter = db.tbl_Chapter.Where(m => m.Subjectid == subjectid && m.yearid == yeari && m.dept_id == depti && m.Courseid == coursei).ToList();
            return Json(new SelectList(chapter, "Chapter_id", "ChapterName"));
        }
        #endregion

        #region Route master
        public ActionResult Route(string Search_Data)
        {
            routeviewmodel _rvm = new routeviewmodel();
            FillPermission(49);
            if (String.IsNullOrEmpty(Search_Data))
                _rvm._routelist = db.sp_getroute().ToList();
            else
                _rvm._routelist = db.sp_getroute().Where(x => x.routename.ToUpper().Contains(Search_Data.ToUpper())
                                                            || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            return View(_rvm);
        }
        public JsonResult FillRouteDetails(int routeid)
        {
            var data = db.tbl_route.Where(m => m.routeid == routeid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_route(string routename)
        {
            var data = db.tbl_route.Where(m => m.routename == routename).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLroute(routeviewmodel _rvm, string evt, int id)
        {
            if (evt == "submit")
            {
                //db.sp_position_DML(_pvm.posid, _pvm.posname, _pvm.status, _pvm.academicyear, "").ToString();
                db.sp_route_DML(_rvm.routeid, _rvm.routename, _rvm.status, "", "").ToString();
            }
            else if (evt == "Delete")
            {
                //db.sp_position_DML(id, _pvm.posname, _pvm.status, _pvm.academicyear, "del").ToString();
                db.sp_route_DML(id, _rvm.routename, _rvm.status, "", "del").ToString();
            }
            _rvm._routelist = db.sp_getroute().ToList();
            return PartialView("_routeList", _rvm);
        }
        #endregion

        #region Permission master
        public ActionResult Permission()
        {
            UserPermissionviewmodel _upv = new UserPermissionviewmodel();
            FillPermission(50);
            _upv._roleslist = db.sys_UserRole.ToList();
            return View(_upv);
        }
        public JsonResult Fill_Modules_With_Permission(int roleid)
        {
            var data = db.sys_Module.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public void DeleteAllPermission(int UserRoleid, int Permissionid, int moduleid)
        {
            db.sp_Permission_DML(moduleid, UserRoleid, Permissionid, "del");
        }

        public void DMLPermission(string[] str)
        {
            int moduleid; int UserRoleid; int Permissionid; int cnt;
            string[] splt = str[0].ToString().Split('|');
            string s;
            for (int i = 0; i < splt.Count()-1; i++)
            {
                s = splt[i].ToString();
                string[] s1 = s.ToString().Split(',');
                moduleid = Convert.ToInt32(s1[1].ToString());
                UserRoleid = Convert.ToInt32(s1[0].ToString());
                Permissionid = Convert.ToInt32(s1[2].ToString());
                db.sp_Permission_DML(moduleid, UserRoleid, Permissionid, "");
            }            
        }

        public JsonResult GetPermission(int UserRoleid, int moduleid)
        {
            var data = db.sp_get_permission(UserRoleid, moduleid).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
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
        #endregion

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

        #region  Division master
        public ActionResult Division(string Search_Data)
        {
            divisionviewmodel _svm = new divisionviewmodel();

            FillPermission(51);
            if (String.IsNullOrEmpty(Search_Data))
            {
                _svm.classlistdetails = db.tbl_CourseMaster.Where(c => c.Status == true).ToList();
                _svm._DivisionList = db.sp_getDivision().ToList();
                
            }
            else
            {
                _svm.classlistdetails = db.tbl_CourseMaster.Where(c => c.Status == true).ToList();
                _svm._DivisionList = db.sp_getDivision().Where(x => x.DivisionName.ToUpper().Contains(Search_Data.ToUpper())
                                                        || x.Classnm.ToUpper().Contains(Search_Data.ToUpper())
                                                        || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            }

            _svm._CourseList = db.sp_GetCoursefordevision().ToList();
            return View(_svm);
        }
        public JsonResult FillDivisionDetails(int DivisionId)
        {
            var data = db.tbl_division.Where(m => m.DivisionId == DivisionId).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult check_duplicate_Division(string DivisionName, int Classid)
        {
            var data = db.tbl_division.Where(m => m.DivisionName == DivisionName && m.Classid == Classid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLDivision(divisionviewmodel _svm, string evt, int id)
        {
            if (evt == "submit")
            {
                db.sp_Division_DML(_svm.DivisionId, _svm.DivisionName, _svm.Classid, _svm.status, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_Division_DML(id, _svm.DivisionName, _svm.Classid, _svm.status, "del").ToString();
            }
            _svm._DivisionList = db.sp_getDivision().ToList();
            return PartialView("_DivisionList", _svm);
        }
        #endregion


        #region Department master
        public ActionResult Department(string Search_Data)
        {
            departmentviewmodel _dvm = new departmentviewmodel();
           
            FillPermission(52);
            if (String.IsNullOrEmpty(Search_Data))
                _dvm._Departmentlist = db.sp_getDepartment().ToList();
            else
                _dvm._Departmentlist = db.sp_getDepartment().Where(x => x.Dept_name.ToUpper().Contains(Search_Data.ToUpper())
                                                        || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            return View(_dvm);
        }
        public JsonResult FillDepartmentDetails(int DeptId)
        {
            var data = db.tblDepartment.Where(m => m.Dept_id == DeptId).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Department(string Dept_name)
        {
            var data = db.tblDepartment.Where(m => m.Dept_name == Dept_name).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLDepartment(departmentviewmodel _dvm, string evt, int id)
        {

            if (evt == "submit")
            {
                db.sp_department_DML(_dvm.Dept_id, _dvm.Dept_name, _dvm.status, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_department_DML(_dvm.Dept_id, _dvm.Dept_name, _dvm.status, "del").ToString();
            }
            _dvm._Departmentlist = db.sp_getDepartment().ToList();
            return PartialView("_departmentlist", _dvm);
        }
        #endregion


        #region CourseYear master
        public ActionResult CourseYear(string Search_Data)
        {
            CourseYearviewmodel _cym = new CourseYearviewmodel();
            //_cym.academicyear = GetYear();
           FillPermission(53);
            if (String.IsNullOrEmpty(Search_Data))
                _cym._courseyear = db.sp_getCourseYear().ToList();
            else
                _cym._courseyear = db.sp_getCourseYear().Where(x => x.Dept_name.ToUpper().Contains(Search_Data.ToUpper())
                        || x.Classnm.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            _cym.deptlist = db.tblDepartment.ToList();
            _cym.yearlist = db.tbl_YearMaster.ToList();
            _cym.courselist = db.tbl_CourseMaster.ToList();
            _cym._courseyear = db.sp_getCourseYear().ToList();
            return View(_cym);
        }
        public JsonResult FillCourseYearDetails(int courseyearid)
        {
            
            var data = db.tbl_CourseYearMaster.Where(m => m.id == courseyearid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Class(int dept_id,int courseid, int academicyear)
        {
            var data = db.tbl_CourseYearMaster.Where(m => (m.dept_id == dept_id)&& (m.courseid == courseid)&&(m.academicyear == academicyear)).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
           
        }
        public JsonResult check_duplicate_CourseYear(int deptid, int courseid, int accadmicyear)
        {
            var data = db.tbl_CourseYearMaster.Where(m => (m.dept_id == deptid) && (m.courseid == courseid) && (m.academicyear == accadmicyear)).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult DMLCourseYear(CourseYearviewmodel _cym, string evt, int id)
        {
          
            if (evt == "submit")
            {
               db.sp_CourseYearMaster_DML(_cym.Id,_cym.DeptId, _cym.CourseId, _cym.academicyear, _cym.status,  "").ToString();
            }
            else if (evt == "Delete")
            {
              db.sp_CourseYearMaster_DML(id, _cym.DeptId, _cym.CourseId, _cym.academicyear, _cym.status, "del").ToString();
            }
            _cym._courseyear = db.sp_getCourseYear().ToList();
            return PartialView("_CourseYearList", _cym);
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
                         where post.Course_id == coureid && post.status==true 
                         select new { meta.Dept_id,meta.Dept_name};

         
            return Json(new SelectList(course, "Dept_id", "Dept_name"));
        }

    
        #endregion

        #region CourseMaster master
        public ActionResult CourseMaster(string Search_Data)
        {
            coursemasterviewmodel _cmv = new coursemasterviewmodel();

            FillPermission(55);
            if (String.IsNullOrEmpty(Search_Data))
                _cmv._coursemasterlist = db.sp_getCourseMaster().ToList();
            else
             _cmv._coursemasterlist = db.sp_getCourseMaster().Where(x => x.CourseName.ToUpper().Contains(Search_Data.ToUpper())
                         || x.Status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            
            return View(_cmv);
        }
        public JsonResult FillCourseDetails(int courseid)
        {

            var data = db.tbl_CourseMaster.Where(m => m.Courseid == courseid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Course(string coursename)
        {
            var data = db.tbl_CourseMaster.Where(m => (m.CourseName == coursename)).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult DML_CourseMaster(coursemasterviewmodel _cmv, string evt, int id)
        {

            if (evt == "submit")
            {
              db.sp_coursemaster_DML(_cmv.CourseId, _cmv.CourseName, _cmv.CourseType,_cmv.Status, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_coursemaster_DML(_cmv.CourseId, _cmv.CourseName, _cmv.CourseType, _cmv.Status, "del").ToString();
            }
            _cmv._coursemasterlist = db.sp_getCourseMaster().ToList();
            return PartialView("_CoursemasterList", _cmv);
        }


        #endregion

        #region Class master
        public ActionResult Course(string Search_Data)
        {
            courseassigndeptmodel _cam = new courseassigndeptmodel();
          
           FillPermission(38);
            if (String.IsNullOrEmpty(Search_Data))
                _cam._CoursedetailsList = db.sp_getCourseDetails().ToList();
            else
                _cam._CoursedetailsList = db.sp_getCourseDetails().Where(x => x.CourseName.ToUpper().Contains(Search_Data.ToUpper())
                        || x.status.ToUpper().Contains(Search_Data.ToUpper())).ToList();
            _cam.departmentlistdetails = db.tblDepartment.ToList();
            _cam.courselist = db.tbl_CourseMaster.ToList();


            return View(_cam);
        }
        public JsonResult FillAssignCourseDetails(int Cid)
        {
            var data = db.tbl_Course.Where(m => m.Cid == Cid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_assigncourse(int CourseId, int Dept_Id)
        {
            var data = db.tbl_Course.Where(m => (m.Course_id == CourseId) && (m.Dept_id == Dept_Id) ).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult DMLAssignCourseDept(courseassigndeptmodel _cam, string evt, int id)
        {

            if (evt == "submit")
            {
                db.sp_course_DML(_cam.Cid,_cam.CourseId, _cam.Dept_Id,_cam.status, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_course_DML(_cam.Cid, _cam.CourseId, _cam.Dept_Id, _cam.status, "del").ToString();
            }
            _cam._CoursedetailsList = db.sp_getCourseDetails().ToList();
            return PartialView("_courselistdetails", _cam);
        }
        #endregion

        #region SessionMaster
        public ActionResult SessionMaster()
        {
            sessionviewmodel _cvm = new sessionviewmodel();
            FillPermission(35);
            _cvm._Sessionlist = db.sp_getSession().ToList();
            return View(_cvm);
        }
        public JsonResult Fillsession(int id)
        {
            var data = db.tbl_SessionMaster.Where(m => m.ID == id).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_session(string SessionName)
        {
            var data = db.tbl_SessionMaster.Where(m => m.SessionName == SessionName).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLSession(sessionviewmodel _cvm)
        {

            db.sp_Session(_cvm.ID,_cvm.SessionName, _cvm.status).ToString();

            _cvm._Sessionlist = db.sp_getSession().ToList();
            return PartialView("_SessionList", _cvm);
        }
        #endregion

        #region AssignedDevisionMaster
        public ActionResult AssignedDivisionMaster()
        {
            divisionmodel division = new divisionmodel();

            division.YearList = db.tbl_YearMaster.ToList();//new List<tbl_YearMaster>();
            division.DepartmentList = db.tblDepartment.ToList(); // new List<tblDepartment>();
            division.Courselist = db.tbl_CourseMaster.ToList();
            return View(division);
        }
        //public JsonResult Filldivision(int id)
        //{
        //    var data = db.tbl_AssignDivision.Where(m => m.ID == id).FirstOrDefault();

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult check_duplicate_divisiond(string divisionid,int studid,int courseid)
        {
            var data = db.tbl_AssignDivision.Where(m => m.Studid == studid && m.Classid == courseid && m.divisionId == divisionid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        
        }

        public JsonResult GettDivision(string id, string year,string department, divisionmodel avm)
        {

            int yearid = 0;
            int Courseid = 0;
            int deptid = 0;
            if (id != null && id != "" && year != null && year != "" && department != null && department != "")
            {
                deptid = Convert.ToInt32(department);
                Courseid = Convert.ToInt32(id);
                yearid = Convert.ToInt32(year);
            }
            var student = db.tbl_student
                .Where(m => (m.Classid == Courseid && m.Dept_Id == deptid && m.courseyearid == yearid))
                .Select(m => new { m.Studid, m.Studnm}).ToList();
            var division = db.tbl_division.Where(m => m.Classid == Courseid).Select(m => new { m.DivisionId, m.DivisionName }).ToList();
            return Json(new { sub = student, teach = division }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult assignDMLDivision(string[] presentdetails)
        {

            //db.sp_divisionDML(_cvm.ID, _cvm.SessionName, _cvm.status).ToString();

            tbl_AssignDivision sa = new tbl_AssignDivision();
            string s;
            int created = Convert.ToInt32(Session["Userid"].ToString());
            for (int i = 0; i < presentdetails.Count(); i++)
            {
                //sa.CreatedBy = Convert.ToInt32(Session["Userid"].ToString());
                s = presentdetails[i].ToString();
                string[] s1 = s.ToString().Split(',');
                sa.Studid = Convert.ToInt32(s1[0].ToString());
                sa.Classid = Convert.ToInt32(s1[1].ToString());
                sa.divisionId = s1[2].ToString();
                sa.department = Convert.ToInt32(s1[3].ToString());
                sa.year = Convert.ToInt32(s1[4].ToString());
                //sring session =  System.Web.HttpContext.Current.Session["sessionString"]; 
                try
                {
                    db.sp_divisionDML(sa.id, sa.Studid, sa.Classid, sa.divisionId, sa.department, sa.year);
                }
                catch (Exception ex)
                { string msg = ex.ToString(); }
                db.SaveChanges();

            }
            return Json(presentdetails);
        }
        #endregion

        #region Lib_VenderMaster
        public ActionResult VendorMaster()
        {
            lib_vendormodel _cvm = new lib_vendormodel();
            FillPermission(56);
            _cvm._Vendorlist = db.sp_getVendor().ToList();
            return View(_cvm);
        }
        public JsonResult check_duplicate_vender(string Vendorname)
        {
            var data = db.tbl_lib_Vendor.Where(m => m.Vendorname == Vendorname).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Fillvendor(int id)
        {
            var data = db.tbl_lib_Vendor.Where(m => m.Vendorid == id).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLvender(lib_vendormodel _lvm)
        {

            db.sp_Vendor(_lvm.venderId, _lvm.Vendername, _lvm.status).ToString();

            _lvm._Vendorlist = db.sp_getVendor().ToList();
            return PartialView("_VenderList", _lvm);
        }
        #endregion

        #region Lib_PublisherMaster
        public ActionResult PublisherMaster()
        {
            lib_publishermodel _cvm = new lib_publishermodel();
            FillPermission(57);
            _cvm._Publisherlist = db.sp_getPublisher().ToList();
            return View(_cvm);
        }
        public JsonResult check_duplicate_publisher(string Publishername)
        {
            var data = db.tbl_lib_Publisher.Where(m => m.Publishername == Publishername).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Fillpublisher(int id)
        {
            var data = db.tbl_lib_Publisher.Where(m => m.PublisherId == id).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLpublisher(lib_publishermodel _lvm)
        {

            db.sp_Publisher(_lvm.publisherid, _lvm.PublisherName, _lvm.status).ToString();

            _lvm._Publisherlist = db.sp_getPublisher().ToList();
            return PartialView("_PublisherList", _lvm);
        }
        #endregion

        #region Lib_AuthorMaster
        public ActionResult AuthorMaster()
        {
            lib_authormodel _cvm = new lib_authormodel();
            FillPermission(58);
            _cvm._Authorlist = db.sp_getAuthor().ToList();
            return View(_cvm);
        }
        public JsonResult check_duplicate_author(string Authorname)
        {
            var data = db.tbl_lib_Author.Where(m => m.Authorname == Authorname).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Fillauthor(int id)
        {
            var data = db.tbl_lib_Author.Where(m => m.Authorid == id).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLauthor(lib_authormodel _lvm)
        {

            db.sp_Author(_lvm.authorid, _lvm.AuthorName, _lvm.status).ToString();

            _lvm._Authorlist = db.sp_getAuthor().ToList();
            return PartialView("_AuthorList", _lvm);
        }
        #endregion


        #region Lib_JournalTypeMaster
        public ActionResult JournalTypeMaster()
        {
            lib_JTypemodule _cvm = new lib_JTypemodule();
            FillPermission(59);
            _cvm._JTypelist = db.sp_getJType().ToList();
            return View(_cvm);
        }
        public JsonResult check_duplicate_journaltype(string JTypeName)
        {
            var data = db.tbl_lib_JournalType.Where(m => m.JTypeName == JTypeName).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FillJType(int id)
        {
            var data = db.tbl_lib_JournalType.Where(m => m.JTypeid == id).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLJType(lib_JTypemodule _lvm)
        {

            db.sp_JType(_lvm.jTypeId, _lvm.JTypeName, _lvm.status).ToString();

            _lvm._JTypelist = db.sp_getJType().ToList();
            return PartialView("_JTypeList", _lvm);
        }
        #endregion

        #region Lib_BookCategoryMaster
        public ActionResult BookCategoryMaster()
        {
            bookCategoryviewmodel _cvm = new bookCategoryviewmodel();
            FillPermission(59);
            _cvm._bookcategorylist = db.sp_getBookCategory().ToList();
            return View(_cvm);
        }
        public JsonResult check_duplicate_bookcategory(string CategoryName)
        {
            var data = db.tbl_lib_BookCategory.Where(m => m.CategoryName == CategoryName).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FillBookCategory(int id)
        {
            var data = db.tbl_lib_BookCategory.Where(m => m.BookCategoryId == id).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLBookCategory(bookCategoryviewmodel _lvm)
        {

            db.sp_BookCategory(_lvm.BookCategoryId, _lvm.CategoryName, _lvm.status).ToString();

            _lvm._bookcategorylist = db.sp_getBookCategory().ToList();
            return PartialView("_BookCategoryList", _lvm);
        }
        #endregion

        #region CONFIGMASTER
        public ActionResult ConfigMaster()
        {
            Configurationtblmodelview cmv = new Configurationtblmodelview();
            cmv._configlist = db.sp_getConfigInfo().ToList();
            return View(cmv);
        }
        public JsonResult check_duplicate_configuration(string ConfigName, string ConfigKey, string configvalue)
        {
            var data = db.CONFIGMASTERs.Where(m => m.CONFIGNAME == ConfigName.Trim() && m.CONFIGKEY==ConfigKey.Trim() && m.CONFIGVALUE==configvalue.Trim()).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FillConfig(int id)
        {
            var data = db.CONFIGMASTERs.Where(m => m.ID == id).FirstOrDefault();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLConfiguration(Configurationtblmodelview _lvm)
        {

            db.sp_DMLConfig(_lvm.CID, _lvm.CONFIGNAME.Trim(), _lvm.CONFIGKEY.Trim(), _lvm.CONFIGVALUE.Trim(), _lvm.CREATEDBY,_lvm.ISACTIVE).ToString();

            _lvm._configlist = db.sp_getConfigInfo().ToList();
            return PartialView("_ConfigurationtblList", _lvm);
        }
        #endregion

    }
}
