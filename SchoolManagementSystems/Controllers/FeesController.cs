using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;

namespace SchoolManagementSystems.Controllers
{
    [HandleError]
    [SchoolManagementSystems.MvcApplication.SessionExpire]
    public class FeesController : Controller
    {
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();

        #region Fees Paid/Unpaid History
        public ActionResult Index(string Search_Data)
        {
            paymentviewmodel _pvm = new paymentviewmodel();
            FillPermission(31);
            if (Search_Data == null || Search_Data == "")
                _pvm._Feespaymenthistory = db.sp_getPaymentHistory().OrderBy(m => m.Pid).ToList();
            else
                _pvm._Feespaymenthistory = db.sp_getPaymentHistory().Where(x => x.Classid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.PTypeid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.scholarship.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.Studid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.TotalFees.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.PaidFees.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.PendingFees.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.PTypeid.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.ScholarshipFees.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.TotalFees.ToString().Contains(Search_Data.ToUpper())).OrderBy(m => m.Pid).ToList();
            
            return View(_pvm);
        }
        #endregion

        #region Fees setup
        public ActionResult FeesManagement(string Search_Data)
        {
            feesviewmodel _fvm = new feesviewmodel();
            FillPermission(29);
            if (Search_Data == null || Search_Data == "")
                _fvm._Feessetuplist = db.sp_getFeessetup().OrderBy(m => m.Feesid).ToList();
            else
                _fvm._Feessetuplist = db.sp_getFeessetup().Where(x => x.Classid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.PTypeid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.TotalFees.ToString().Contains(Search_Data.ToUpper())).OrderBy(m => m.Feesid).ToList();
            _fvm.ptypelist = db.tbl_payterm.Where(c => c.status == true).ToList();
            _fvm._CourseList = db.sp_GetCoursefordevision().ToList();
            return View(_fvm);
        }
        public JsonResult FillFeessetupDetails(int Feesid)
        {
            var data = db.tbl_fees.Where(m => m.Feesid == Feesid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Class(int Classid)
        {
            var data = db.tbl_fees.Where(m => m.Courseid == Classid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FillFeesLabels(int Classid)
        {
            var duplicateclass = db.tbl_fees.Where(m => m.Courseid == Classid).FirstOrDefault();
            var lblid = db.tbl_feeslabel.Where(m => m.classid == Classid).FirstOrDefault();
            int feeslabelid = 0;
            try
            {
                feeslabelid = lblid.feeslblid;
            }
            catch { }
            var  feeslabels = db.tbl_feeslabelchild.Where(m => m.feeslblid == feeslabelid).Select(m => m.ctrlnm).ToList();
            
            return Json(new { duplicateclass = duplicateclass, feeslabels = feeslabels }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLFeessetup(feesviewmodel _fvm, string evt, int id, string FeesLabels)
        {
            int Lastfeeslblid = 0;
            if (evt == "submit")
            {
                db.sp_feessetup_DML(_fvm.Feesid, _fvm.CourseId, Convert.ToDecimal(_fvm.LFees), Convert.ToDecimal(_fvm.TFees), Convert.ToDecimal(_fvm.UFees), Convert.ToDecimal(_fvm.HFees), Convert.ToDecimal(_fvm.OFees), Convert.ToDecimal(_fvm.TotalFees),_fvm.PTypeid, "").ToString();
            }
            //else if (evt == "Delete")
            //{
            //    db.sp_feessetup_DML(id, _fvm.Classid, Convert.ToDecimal(_fvm.LFees), Convert.ToDecimal(_fvm.TFees), Convert.ToDecimal(_fvm.UFees), Convert.ToDecimal(_fvm.HFees), Convert.ToDecimal(_fvm.OFees), Convert.ToDecimal(_fvm.TotalFees), "del").ToString();
            //}
            if (_fvm.Feesid == 0)
            {
                Lastfeeslblid = db.tbl_fees.OrderByDescending(m => m.Feesid).Select(m => m.Feesid).FirstOrDefault();
            }
            else
            {
                Lastfeeslblid = _fvm.Feesid;
            }
            db.sp_feeschild_DML(Lastfeeslblid,"", 0, "del").ToString();

            var lblid = db.tbl_feeslabel.Where(m => m.classid == _fvm.CourseId).FirstOrDefault();
            var data = db.tbl_feeslabelchild.Where(m => m.feeslblid == lblid.feeslblid).Select(m => m.ctrlnm).ToList();
            string[] labels = FeesLabels.ToString().Replace("undefined", "").Split('|');
            string lbl,ctrl;
            for (int i = 0; i < labels.Count() - 1; i++)
            {
                lbl = labels[i].ToString();
                ctrl = data[i].ToString();
                db.sp_feeschild_DML(Lastfeeslblid,ctrl, Convert.ToDecimal(lbl), "").ToString();
            }
            _fvm._Feessetuplist = db.sp_getFeessetup().ToList();
            return PartialView("_FeesSetupList", _fvm);
        }
        public JsonResult FillFeesAmount(int feesid)
        {
            var data = db.tbl_feeschild.Where(m => m.Feesid == feesid).Select(m => m.Fees).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        #endregion

        #region Fees Payment
        public ActionResult FeesPayment(string Search_Data)
        {
            paymentviewmodel _pvm = new paymentviewmodel();
            FillPermission(30);
            _pvm._CourseList = db.sp_GetCoursefordevision().ToList();
            _pvm.studlist = new List<tbl_student>();
            _pvm.ptypelist = db.tbl_payterm.Where(c => c.status == true).ToList();
            if (Search_Data == null || Search_Data == "")
                _pvm._Feespaymentlist = db.sp_getFeespaymentlist().OrderBy(m => m.Pid).ToList();
            else
                _pvm._Feespaymentlist = db.sp_getFeespaymentlist().Where(x => x.Classid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.PTypeid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.scholarship.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.Studid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.TotalFees.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.PaidFees.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.PendingFees.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.PTypeid.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.ScholarshipFees.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.TotalFees.ToString().Contains(Search_Data.ToUpper())).OrderBy(m => m.Pid).ToList();
            
            return View(_pvm);
        }
        public JsonResult check_duplicate_Payment(int Studid, int CourseId)
        {
            var data = db.tbl_feespayment.Where(m => m.Studid == Studid && m.Courseid == CourseId).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLFeespayment(paymentviewmodel _pvm, string evt, int id)
        {
            if (evt == "submit")
            {
                db.sp_feespayment_DML(_pvm.Pid, _pvm.CourseId, _pvm.Studid, _pvm.Scholarship, Convert.ToDecimal(_pvm.ScholarshipFees), Convert.ToDecimal(_pvm.TotalFees), Convert.ToDecimal(_pvm.PaidFees), Convert.ToDecimal(_pvm.PendingFees), _pvm.PTypeid, _pvm.Remarks, _pvm.Status, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_feespayment_DML(id, _pvm.CourseId, _pvm.Studid, _pvm.Scholarship, Convert.ToDecimal(_pvm.ScholarshipFees), Convert.ToDecimal(_pvm.TotalFees), Convert.ToDecimal(_pvm.PaidFees), Convert.ToDecimal(_pvm.PendingFees), _pvm.PTypeid, _pvm.Remarks, _pvm.Status, "del").ToString();
            }
            _pvm._Feespaymentlist = db.sp_getFeespaymentlist().ToList();
            return PartialView("_Feespaymentlist", _pvm);
        }
        public JsonResult GetStudents(string id)
        {
            int courseyearid = 0;
            if (id != null && id != "")
            {
                courseyearid = Convert.ToInt32(id);
            }
            int? courseid = 0;
            int? deptid = 0;
            int? yearid = 0;
            var courseyear = db.tbl_CourseYearMaster.Where(m => m.id == courseyearid).FirstOrDefault();
            courseid = courseyear.courseid;
            deptid = courseyear.dept_id;
            yearid = courseyear.academicyear;   
            var students = db.tbl_student.Where(m => m.Classid == courseid &&  m.Dept_Id == deptid &&  m.courseyearid==yearid).ToList();
            return Json(new SelectList(students, "Studid", "Studnm"));
        }
        public JsonResult GetFeesDetails(string id)
        {
            int Courseid = 0;
            if (id != null && id != "")
            {
                Courseid = Convert.ToInt32(id);
            }
            var feesdetails = db.tbl_fees.Where(m => m.Courseid == Courseid).FirstOrDefault();
            return Json(feesdetails, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FillPaymentDetails(int Pid)
        {
            var data = db.tbl_feespayment.Where(m => m.Pid == Pid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFeeschildDetails(int feesid)
        {
            var data = db.tbl_feeschild.Where(m => m.Feesid == feesid).Select(m => new { m.Fees,m.FeesNm }).ToList();
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
    }
}
