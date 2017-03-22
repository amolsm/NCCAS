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
    public class TransportController : Controller
    {
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
        public ActionResult Index(string Search_Data)
        {
            transportviewmodel _tvm = new transportviewmodel();
            FillPermission(27);
            if (Search_Data == null || Search_Data == "")
                _tvm._transportlist = db.sp_gettransport().OrderBy(m => m.busid).ToList();
            else
                _tvm._transportlist = db.sp_gettransport().Where(x => x.busNo.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.busRoute.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.busDriverNm.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.busDrivercontact.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.busRTONo.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.busDateTime.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.BusTime.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.status.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.busRoute.ToUpper().Contains(Search_Data.ToUpper())).OrderBy(m => m.busid).ToList();

            return View(_tvm);
        }

        public ActionResult NewBusInfo(int? busid)
        {
            transportviewmodel _tvm = new transportviewmodel();
            FillPermission(26);
            _tvm.Routelist = db.tbl_route.Where(c => c.status == true).ToList();
            if (busid != null)
            {
                var data = db.tbl_transport.Where(m => m.busid == busid).FirstOrDefault();
                if (data != null)
                {
                    _tvm.busid = data.busid;
                    _tvm.busNo = data.busNo;
                    _tvm.busRoute = data.busRoute;
                    _tvm.busDriverNm = data.busDriverNm;
                    _tvm.busDrivercontact = data.busDrivercontact;
                    _tvm.busRTONo = data.busRTONo;
                    _tvm.busDateTime = Convert.ToDateTime(data.busDateTime);
                    _tvm.busTime = data.BusTime;
                    _tvm.status = data.status;
                }
            }
            return View(_tvm);
        }

        public JsonResult check_duplicate_busno(string busNo)
        {
            var data = db.tbl_transport.Where(m => m.busNo == busNo).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLTransport(transportviewmodel _pvm, string evt, int id)
        {
            if (evt == "submit")
            {
                if (_pvm.busid == null) { _pvm.busid = 0; }
                db.sp_transport_DML(_pvm.busid, _pvm.busNo, _pvm.busRoute, _pvm.busDriverNm, _pvm.busDrivercontact, _pvm.busRTONo, _pvm.busDateTime, _pvm.status, "", _pvm.busTime, "").ToString();
            }
            else if (evt == "Delete")
            {
                db.sp_transport_DML(id, _pvm.busNo, _pvm.busRoute, _pvm.busDriverNm, _pvm.busDrivercontact, _pvm.busRTONo, _pvm.busDateTime, _pvm.status, "", _pvm.busTime, "del").ToString();
            }
            return RedirectToAction("Index");
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
