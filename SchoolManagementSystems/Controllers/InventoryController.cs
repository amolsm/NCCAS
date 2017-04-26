using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using System.Data.Entity;

namespace SchoolManagementSystems.Controllers
{
    [HandleError]
    [SchoolManagementSystems.MvcApplication.SessionExpire]
    public class InventoryController : Controller
    {
        SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();
        #region Category master
        public ActionResult Category(string Search_Data)
        {
            Categoryviewmodel _cvm = new Categoryviewmodel();
            FillPermission(21);
            if (Search_Data == null || Search_Data == "")
                _cvm._categorylist = db.sp_getcategory().OrderBy(m => m.catid).ToList();
            else
                _cvm._categorylist = db.sp_getcategory().Where(x => x.catname.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.status.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.catid.ToString().Contains(Search_Data.ToUpper())).OrderBy(m => m.catid).ToList();
            
            return View(_cvm);
        }
        public JsonResult FillCategoryDetails(int catid)
        {
            var data = db.tbl_category.Where(m => m.catid == catid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_category(string catname)
        {
            var data = db.tbl_category.Where(m => m.catname == catname).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLCategory(int catid, string catname, bool status, string evt, int id)
        {
            Categoryviewmodel _cvm = new Categoryviewmodel();
            if (evt == "submit")
            {
                //db.sp_position_DML(_cvm.catid, _cvm.catname, _cvm.status, _cvm.academicyear, "").ToString();
                db.sp_category_DML(catid, catname, status, "").ToString();
            }
            else if (evt == "Delete")
            {
                //db.sp_position_DML(id, _cvm.catname, _cvm.status, _cvm.academicyear, "del").ToString();
                db.sp_category_DML(id, _cvm.catname, _cvm.status, "del").ToString();
            }
            _cvm._categorylist = db.sp_getcategory().ToList();
            return PartialView("_categoryList", _cvm);
        }
        #endregion

        public ActionResult Product(string Search_Data)
        {
            Categoryviewmodel _cvm = new Categoryviewmodel();
            FillPermission(23);
            _cvm.Categorylist = db.tbl_category.Where(x => x.status == true).ToList();
            _cvm._subCategorylist = db.tbl_SubCategory.Where(x => x.status == true).ToList();
            _cvm._vendorlist = db.tbl_InventoryVendor.Where(x => x.IsActive == true).ToList();
            if (Search_Data == null || Search_Data == "")
                _cvm._Productlist = db.sp_getProduct().OrderBy(m => m.Productid).ToList();
            else
                _cvm._Productlist = db.sp_getProduct().Where(x => x.Categoryid.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.status.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.Product.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.Productid.ToString().Contains(Search_Data.ToUpper())).OrderBy(m => m.Productid).ToList();
            
            return View(_cvm);
        }

        public ActionResult DMLProduct(int Productid, int catid, int subcatid, string Product, bool status,int vendorid, string evt, int id)
        {
            Categoryviewmodel _cvm = new Categoryviewmodel();
            if (evt == "submit")
            {
                //db.sp_position_DML(_cvm.catid, _cvm.catname, _cvm.status, _cvm.academicyear, "").ToString();
                db.sp_Product_DML(Productid, catid, subcatid, Product, status, "",vendorid).ToString();
            }
            else if (evt == "Delete")
            {
                //db.sp_position_DML(id, _cvm.catname, _cvm.status, _cvm.academicyear, "del").ToString();
                db.sp_Product_DML(id, catid, subcatid, Product, status, "del", vendorid).ToString();
            }
            _cvm._Productlist = db.sp_getProduct().ToList();
            _cvm._vendorlist = db.tbl_InventoryVendor.Where(x => x.IsActive == true).ToList();
            return PartialView("_Productlist", _cvm);
        }

        public JsonResult check_duplicate_Product(string Product, int Categoryid)
        {
            string pro = Product;
            int catid = Categoryid;
            var data = db.tbl_Product.Where(x => x.Product == pro && x.Categoryid == catid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillProductDetails(int productid)
        {
            var data = db.tbl_Product.Where(m => m.Productid == productid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Inventory(string Search_Data)
        {
            Categoryviewmodel _cvm = new Categoryviewmodel();
            FillPermission(24);
            _cvm.Categorylist = db.tbl_category.Where(x => x.status == true).ToList();
            _cvm._subCategorylist = new List<tbl_SubCategory>();
            _cvm.productlist = new List<tbl_Product>();
            if (Search_Data == null || Search_Data == "")
                _cvm._Inventorylist = db.sp_getInventory().OrderBy(m => m.Inventoryid).ToList();
            else
                _cvm._Inventorylist = db.sp_getInventory().Where(x => x.catname.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.Price.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.Product.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.Inventoryid.ToString().Contains(Search_Data.ToUpper()) ||
                                                        x.Quantity.ToString().Contains(Search_Data.ToUpper())).OrderBy(m => m.Inventoryid).ToList();
            
            return View(_cvm);
        }

        public JsonResult BindProduct(int categoryid, int subcategoryid)
        {
            var data = db.tbl_Product.Where(m => m.Categoryid == categoryid && m.SubCategoryid == (subcategoryid == 0 ? m.SubCategoryid : subcategoryid) && m.Status == true);
            return Json(new SelectList(data, "Productid", "Product"));
        }

        public JsonResult BindSubCategory(int categoryid)
        {
            var data = db.tbl_SubCategory.Where(m => m.Categoryid == categoryid && m.status == true);
            return Json(new SelectList(data, "SubCategoryid", "SubCategoryname"));
        }

        public ActionResult DMLInventory(string evt, int id, string price, string Qty, int Prodid)
        {
            Categoryviewmodel _cvm = new Categoryviewmodel();
            if (evt == "submit")
            {
                //db.sp_position_DML(_cvm.catid, _cvm.catname, _cvm.status, _cvm.academicyear, "").ToString();
                db.sp_Inventory_DML(Prodid, Convert.ToInt32(Qty), Convert.ToDecimal(price), id, "").ToString();
            }
            else if (evt == "Delete")
            {
                //db.sp_position_DML(id, _cvm.catname, _cvm.status, _cvm.academicyear, "del").ToString();
                db.sp_Inventory_DML(Prodid, Convert.ToInt32(Qty), Convert.ToDecimal(price), id, "del").ToString();
            }
            _cvm._Inventorylist = db.sp_getInventory().ToList();
            return PartialView("_Inventorylist", _cvm);
        }

        public JsonResult check_duplicate_Inventory(int Productid)
        {
            int pro = Productid;
            var data = db.tbl_Inventory.Where(x => x.Productid == pro).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillInventoryDetails(int inventoryid)
        {
            var data = (from a in db.tbl_Inventory
                        join e in db.tbl_Product on a.Productid equals e.Productid
                        from d in db.tbl_SubCategory.Where(dt => dt.Categoryid == e.Categoryid).DefaultIfEmpty()
                        select new { a.Inventoryid, a.Productid, a.Quantity, a.Price, e.Categoryid, SubCategoryid = d.SubCategoryid == null ? 0 : d.SubCategoryid }).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #region SubCategory master
        public ActionResult SubCategory(string Search_Data)
        {
            Categoryviewmodel _cvm = new Categoryviewmodel();
            FillPermission(22);
            _cvm._categorylist = db.sp_getcategory().ToList();
            if (Search_Data == null || Search_Data == "")
                _cvm._SubCategorylist = db.sp_getSubCategory().OrderBy(m => m.SubCategoryid).ToList();
            else
                _cvm._SubCategorylist = db.sp_getSubCategory().Where(x => x.catname.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.status.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.SubCategoryname.ToUpper().Contains(Search_Data.ToUpper()) ||
                                                        x.SubCategoryid.ToString().Contains(Search_Data.ToUpper())).OrderBy(m => m.SubCategoryid).ToList();
            
            return View(_cvm);
        }
        public JsonResult FillSubCategoryDetails(int catid)
        {
            var data = db.tbl_SubCategory.Where(m => m.SubCategoryid == catid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult check_duplicate_subcategory(string Categoryname, int catid)
        {
            var data = db.tbl_SubCategory.Where(m => m.SubCategoryname == Categoryname && m.Categoryid == catid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DMLSubCategory(int subcatid, string subcatname, bool status, int catid, string evt, int id)
        {
            Categoryviewmodel _svm = new Categoryviewmodel();            
            if (evt == "submit")
            {
                //db.sp_position_DML(_pvm.posid, _pvm.posname, _pvm.status, _pvm.academicyear, "").ToString();
                db.sp_SubCategory_DML(subcatid, subcatname, status, catid, "").ToString();
            }
            else if (evt == "Delete")
            {
                //db.sp_position_DML(id, _pvm.posname, _pvm.status, _pvm.academicyear, "del").ToString();
                db.sp_SubCategory_DML(id, subcatname, status, catid, "del").ToString();
            }
            _svm._SubCategorylist = db.sp_getSubCategory().ToList();
            return PartialView("_subcategoryList", _svm);
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
