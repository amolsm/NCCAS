using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using System.Text;
using System.Globalization;
using Common;

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
            _fvm._receipttype = db.tblReceiptTypes.Where(m => m.Status == true).ToList();
            return View(_fvm);
        }
        public JsonResult FillFeessetupDetails(int Feesid)
        {
            var data = db.tbl_fees.Where(m => m.Feesid == Feesid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_Class(int Classid,int receipttypeid)
        {
            var data = db.tbl_fees.Where(m => m.Courseid == Classid && m.receipttypeid == receipttypeid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FillFeesLabels(int Classid, int receipttypeid)
        {
            var duplicateclass = db.tbl_fees.Where(m => m.Courseid == Classid && m.receipttypeid == receipttypeid).FirstOrDefault();
            var lblid = db.tbl_feeslabel.Where(m => m.receipttypeid == receipttypeid).FirstOrDefault();
            var  feeslabels = db.tbl_feeslabelchild.Where(m => m.feeslblid == lblid.feeslblid).Select(m => m.ctrlnm).ToList();
            
            return Json(new { duplicateclass = duplicateclass, feeslabels = feeslabels }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLFeessetup(feesviewmodel _fvm, string evt, int id, string FeesLabels)
        {
            int Lastfeeslblid = 0;
            if (evt == "submit")
            {
                db.sp_feessetup_DML(_fvm.Feesid, _fvm.CourseId, _fvm.receipttypeid, Convert.ToDecimal(_fvm.LFees), Convert.ToDecimal(_fvm.TFees), Convert.ToDecimal(_fvm.UFees), Convert.ToDecimal(_fvm.HFees), Convert.ToDecimal(_fvm.OFees), Convert.ToDecimal(_fvm.TotalFees),_fvm.PTypeid, "").ToString();
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

            var lblid = db.tbl_feeslabel.Where(m => m.classid == _fvm.CourseId && m.receipttypeid==_fvm.receipttypeid).FirstOrDefault();
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
            _pvm._receipttype = db.tblReceiptTypes.Where(m => m.Status == true).ToList();
            _pvm.academicyear = GetYear();
            string currentdate=CommonUtility.FormatedDateString(DateTime.Now);
            if (Search_Data == null || Search_Data == "")
                _pvm._Feespaymentlist = db.sp_getFeespaymentlist().Where(x=>  x.P_Date.ToUpper().Contains(currentdate.ToString())).OrderBy(m => m.Pid).ToList();
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
        public JsonResult check_duplicate_Payment(int Studid, int CourseId,int receipttypeid,string academicyear)
        {
            var data = db.sp_checkPreviousFeesPaymentAmt(Studid,CourseId,receipttypeid, academicyear).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLFeespayment(paymentviewmodel _pvm, string evt, int id)
        {
            string yr = _pvm.academicyear[0].ToString();
            if (evt == "submit")
            {
                db.sp_feespayment_DML(_pvm.Pid, _pvm.CourseId,_pvm.receipttypeid, _pvm.Studid, _pvm.Scholarship, Convert.ToDecimal(_pvm.ScholarshipFees), Convert.ToDecimal(_pvm.TotalFees), Convert.ToDecimal(_pvm.PaidFees), Convert.ToDecimal(_pvm.PendingFees), _pvm.PTypeid, _pvm.Remarks, _pvm.Status, "", yr).ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_feespayment_DML(id, _pvm.CourseId, _pvm.receipttypeid, _pvm.Studid, _pvm.Scholarship, Convert.ToDecimal(_pvm.ScholarshipFees), Convert.ToDecimal(_pvm.TotalFees), Convert.ToDecimal(_pvm.PaidFees), Convert.ToDecimal(_pvm.PendingFees), _pvm.PTypeid, _pvm.Remarks, _pvm.Status, "del", yr).ToString();
            }
            _pvm._Feespaymentlist = db.sp_getFeespaymentlist().ToList();
        
            //_pvm.paymentid=db.tbl_feespayment.Where(m=>(m.Courseid==_pvm.CourseId&&m.PTypeid).Select()
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
        public JsonResult GetFeesDetails(string id,string receipttypeid)
        {
            int Courseid = 0;
            int r_typeid = 0;
            if (id != null && id != "" && receipttypeid!=null && receipttypeid!="")
            {
                Courseid = Convert.ToInt32(id);
                r_typeid = Convert.ToInt32(receipttypeid);
            }
            var feesdetails = db.tbl_fees.Where(m => m.Courseid == Courseid && m.receipttypeid== r_typeid).FirstOrDefault();
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

        #region Print Receipt
        public ActionResult PrintReceipt(int paymentid)
        {
            string result = string.Empty;
            StringBuilder sb = new StringBuilder();
            var studentfeesdetails = db.sp_GetFeeReceiptbyFeeId(paymentid).ToList();
            var feesdetails = db.sp_GetFeeReceiptdetailsbyFeeId(paymentid).ToList();
            ViewData["acadmicyear"] = studentfeesdetails[0].academicyear;
            sb.Append("<table class='table table-bordered student_detail'>");
                sb.Append("<tbody>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:center'>");
                sb.Append("<strong>"+ studentfeesdetails[0].R_Name + "</strong>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("<strong> Receipt Number </strong>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(studentfeesdetails[0].No);
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("<strong> Date  </strong>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(studentfeesdetails[0].P_Date);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("<strong> Name </strong>");
                sb.Append("</td>");
                sb.Append("<td colspan='3'>");
                sb.Append(studentfeesdetails[0].StudentName);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("<strong> Course </strong>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(studentfeesdetails[0].Course);
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append("<strong> RollNo </strong>");
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(studentfeesdetails[0].RollNo);
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</tbody>");
                sb.Append("</table>");

            sb.Append("<table class='table table-bordered'>");

            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th>Sr.No</th>");
            sb.Append("<th>Fee Perticular</th>");
            sb.Append("<th>Fees</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            double totalfees = 0;
            int srno = 0;
            foreach (var stddetail in feesdetails)
            {
                srno++;
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append(srno);
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(stddetail.FeesNm);
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(stddetail.Fees);
                sb.Append("</td>");
                sb.Append("</tr>");
                totalfees += Convert.ToDouble(stddetail.Fees);

            }
            sb.Append("<tr>");
            sb.Append("<td class='foot'></td>");
            sb.Append("<td class='foot'><h5 style='text - align:center;'><b><font color='red'></font>Total Amount </b></h5></td>");
            sb.Append("<td class='foot'>");
            sb.Append("<h5><b>" + Convert.ToDecimal(totalfees).ToString("0.00") + "</b></h5></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='foot'></td>");
            sb.Append("<td class='foot'><h5 style='text - align:center;'><b><font color='red'></font>Scholarship Amount </b></h5></td>");
            sb.Append("<td class='foot'>");
            sb.Append("<h5><b>" + Convert.ToDecimal(studentfeesdetails[0].ScholarshipFees).ToString("0.00") + "</b></h5></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='foot'></td>");
            sb.Append("<td class='foot'><h5 style='text - align:center;'><b><font color='red'></font>Total </b></h5></td>");
            sb.Append("<td class='foot'>");
            sb.Append("<h5><b>" + Convert.ToDecimal(studentfeesdetails[0].TotalFees).ToString("0.00") + "</b></h5></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='foot'></td>");
            sb.Append("<td class='foot'><h5 style='text - align:center;'><b><font color='green'>Paid Amount</font></b></h5></td>");
            sb.Append("<td class='foot'>");
            sb.Append("<h5><b>" + Convert.ToDecimal(studentfeesdetails[0].PaidFees).ToString("0.00") + "</b></h5></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td class='foot'></td>");
            sb.Append("<td class='foot'><h5 style='text - align:center;'><b><font color='red'>Pending Amount</font></b></h5></td>");
            sb.Append("<td class='foot'>");
            sb.Append("<h5><b>" + Convert.ToDecimal(studentfeesdetails[0].PendingFees).ToString("0.00") + "</b></h5></td>");
            sb.Append("</tr>");
            sb.Append("</tbody>");
            sb.Append("</table>");

            result = sb.ToString();
            ViewBag.HtmlStr = result;
            return PartialView("_printreceipt", ViewBag.HtmlStr);
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
    }
}
