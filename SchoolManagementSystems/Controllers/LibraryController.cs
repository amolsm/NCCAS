using Entity;
using SchoolManagementSystems.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Common;
using System.Data.SqlClient;

namespace SchoolManagementSystems.Controllers
{
    public class LibraryController : Controller
    {
        //
        // GET: /Library/
            SchoolMgmtSysEntities db = new SchoolMgmtSysEntities();


       
        public ActionResult Index(libBookentry lb)
        {
            //FillPermission(47);
            lb._authorlist = db.tbl_lib_Author.Where(m => m.status == true).ToList();
            lb._bookentrylist = db.sp_getBookentry().ToList();
            lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
            lb._vendorlist = db.tbl_lib_Vendor.Where(m => m.status == true).ToList();
            lb.courselist = db.tbl_CourseMaster.Where(m => m.Status == true).ToList();
           if (lb.Option == 2)
            {
                int? deptid;
                int? courseid;
                int? vendor;
                if (lb.DeptId == 0) { deptid = null; } else { deptid = lb.DeptId; }
                if (lb.LibType == 0) { courseid = null; } else { courseid = lb.LibType; }
                if (lb.JVendorId == 0) { vendor = null; } else { vendor = lb.JVendorId; }
                string jtitle = (lb.JournalTitle == string.Empty) ? null : lb.JournalTitle;
                string jpublisher= (lb.JPublisher == string.Empty) ? null : lb.JPublisher;
                string accessonno = (lb.Number == string.Empty) ? null : lb.Number;
                string jshelfno = (lb.JShelfNo == string.Empty) ? null : lb.JShelfNo;
                lb._journalList = db.sp_GetSearchJournal(lb.Option, deptid, jtitle, courseid, jpublisher, accessonno, vendor, jshelfno).ToList();
                lb.Option = 2;
            }

            else { lb._journalList = db.sp_GetSearchJournal(2, null, null, null, null, null, null, null).ToList(); }
            if (lb.Option == 1)
            {
                int? bdeptid;
                int? bcourseid;
              
                if (lb.Department == 0) { bdeptid = null; } else { bdeptid = lb.Department; }
                if (lb.CourseId == 0) { bcourseid = null; } else { bcourseid = lb.CourseId; }
                string btitle = (lb.booktitle == string.Empty) ? null : lb.booktitle;
                string bpublisher = (lb.PublishedByName == string.Empty) ? null : lb.PublishedByName;
                string baccessonno = (lb.AccessorNo == string.Empty) ? null : lb.AccessorNo;
                string bcallno = (lb.CallNo == string.Empty) ? null : lb.CallNo;
                string authorname = (lb.Authorname == string.Empty) ? null : lb.Authorname;
                string vendorname = (lb.Vendorname == string.Empty) ? null : lb.Vendorname;
                lb._bookandJournallist = db.sp_GetSearchBookAndJournal(lb.Option, btitle, authorname, bcallno, baccessonno, bpublisher, vendorname, bdeptid, bcourseid).ToList();
                lb.Option = 1;
            }
            else { lb._bookandJournallist = db.sp_GetSearchBookAndJournal(1, null, null, null, null, null, null,null,null).Take(10).ToList(); }
            return View(lb);
          
        }
       
   

        public ActionResult BookEntry()
         {
            libBookentry lb = new libBookentry();
           
            lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
            lb._authorlist = db.tbl_lib_Author.Where(m => m.status == true).ToList();
            lb._jtypelist = db.tbl_lib_JournalType.Where(m => m.status == true).ToList();
            lb._publisherlist = db.tbl_lib_Publisher.Where(m => m.status == true).ToList();
            lb._vendorlist = db.tbl_lib_Vendor.Where(m => m.status == true).ToList();
            lb.courselist = db.tbl_CourseMaster.Where(m => m.Status == true).ToList();
            lb._bookcategorylist = db.tbl_lib_BookCategory.Where(m => m.status == true).ToList();
            int count = (from p in db.lib_Bookentry
                         select p).Count();
            count = count + 1;
            if (count != 1)
            {
                var lastaccessorno = (from t in db.lib_Bookentry
                                      orderby t.AccessorNo descending
                                      select t.AccessorNo).First();
                int accessorno = 0;
                accessorno = Convert.ToInt32(lastaccessorno) + 1;
                lb.AccessorNo = accessorno.ToString();
            }
            else { lb.AccessorNo = count.ToString(); }
            lb.SerielNumber = count.ToString();
            int count1 = (from q in db.lib_Journal
                         select q).Count();
            count1 = count1 + 1;
           
            lb.Number = count1.ToString();
            return View(lb);

        }
      
        [HttpPost]
        public ActionResult BookEntry(libBookentry _lb,string command)
        {
            
            if (command.Equals("BookSubmit"))
            {
                
                try {
                    int subid = Convert.ToInt32(Session["Userid"].ToString());
                    db.sp_AddBookEntry(_lb.bookid,_lb.booktitle,_lb.CallNo, _lb.Volume, _lb.SerielNumber, _lb.Authorid,
                    _lb.Authorname, _lb.PublishedByid, _lb.PublishedByName, _lb.Edition, _lb.Vendorid, _lb.Vendorname,

                     _lb.Dateofpurchase, _lb.BillNo, _lb.Cost,_lb.AccessorNo,_lb.ShelfNo, true, subid,_lb.CourseId,_lb.DeptId,_lb.BookCategoryid).ToString();



              TempData["Error"] = "Success";
                }
                catch { TempData["Error"] = "Failed"; }
                 }
            if (command.Equals("JournalSubmit"))
            {
               
                try
                {
                    db.sp_AddLibraryJournal(_lb.lib_Jid,_lb.LibType,_lb.Department,_lb.JournalTitle,_lb.LibJVolume,
                        _lb.Number,_lb.IssueDate, _lb.JVendorId, _lb.PurchaseDate,_lb.JBillNo,_lb.IssueType,_lb.JPublisher,_lb.JShelfNo,_lb.JCost).ToString();
                    TempData["Error"] = "Success";

                }
                catch { TempData["Error"] = "Failed"; }





            }
          
           
            _lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
         
            return RedirectToAction("BookEntry");
           
        }

        public ActionResult BookEdit(int? bookid)
        {
            libBookentry lb = new libBookentry();
            TempData["Error"] = "";
            lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
            lb._authorlist = db.tbl_lib_Author.Where(m => m.status == true).ToList();
            lb._jtypelist = db.tbl_lib_JournalType.Where(m => m.status == true).ToList();
            lb._publisherlist = db.tbl_lib_Publisher.Where(m => m.status == true).ToList();
            lb._vendorlist = db.tbl_lib_Vendor.Where(m => m.status == true).ToList();
            lb.courselist = db.tbl_CourseMaster.Where(m => m.Status == true).ToList();
            lb._bookcategorylist = db.tbl_lib_BookCategory.Where(m => m.status == true).ToList();
            return View("BookEntry", lb);
        }

        public ActionResult JournalEdit(int? lib_Jid)
        {
            libBookentry lb = new libBookentry();
            TempData["Error"] = "";
            lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
            lb._authorlist = db.tbl_lib_Author.Where(m => m.status == true).ToList();
            lb._jtypelist = db.tbl_lib_JournalType.Where(m => m.status == true).ToList();
            lb._publisherlist = db.tbl_lib_Publisher.Where(m => m.status == true).ToList();
            lb._vendorlist = db.tbl_lib_Vendor.Where(m => m.status == true).ToList();
            lb.courselist = db.tbl_CourseMaster.Where(m => m.Status == true).ToList();
            lb._bookcategorylist = db.tbl_lib_BookCategory.Where(m => m.status == true).ToList();
            return View("BookEntry", lb);
        }
        public JsonResult FillBookDetails(int bookid)
        {
            var data = db.Sp_GetLibBookDetailsbyId(bookid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FillJournalDetails(int jid)
        {
          
            var data = db.lib_Journal.Where(m => m.lib_Jid == jid).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BookIssue()
        {
            BookAllocation b = new BookAllocation();

            b._BookIssueList = db.tbl_lib_BookIssue.ToList();
            b._bookandJournallist = db.sp_GetSearchBookAndJournal(1, null, null, null, null, null, null,null,null).ToList();
            return View(b);
        }
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var StudentName = (from bi in db.tbl_student
                           where bi.Studnm.StartsWith(prefix)
                           select new {label=bi.Studnm ,
                           val=bi.StudentID}).ToList();
            return Json(StudentName, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BookAutoComplete(string prefix)
        {
            var BookName = (from bi in db.tbl_BookDetails
                               where bi.BookName.StartsWith(prefix)
                               select new
                               {
                                   label = bi.BookName,
                                   val = bi.CallNo
                               }).ToList();
            return Json(BookName, JsonRequestBehavior.AllowGet);
        }
       

        public JsonResult GetBookIssueDetails(string StudentName, string Studentid,string Stdid)
        {
            int stid;
            int Stdids;
           
            try { stid =db.tbl_student.Where(m=>m.StudentID ==Studentid).Select(m=>m.Studid).FirstOrDefault();
           
            } catch { stid = 0; }
            try { Stdids = db.tbl_student.Where(m => m.StudentID == Stdid).Select(m => m.Studid).FirstOrDefault();
            }
            catch { Stdids = 0; }
           
            if (Stdids != 0)
            {
                var data = db.sp_GetBookIssueDetailsbyStudentid(Stdids);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {

                var data = db.sp_GetBookIssueDetailsbyStudentid(stid);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
           
           
            

        }


        public JsonResult GetBookDetails(string BookName, string callno,string accessonno,string authorname,string barcodeno)
        {
             string bookn;
             bookn=(BookName == string.Empty)?null: BookName;
             string cno;
             cno = (callno == string.Empty) ? null : callno;
             string asno;
             asno = (accessonno == string.Empty) ? null : accessonno;
             string authname;
             authname = (authorname == string.Empty) ? null : authorname;
             string barcode;
             barcode = (barcodeno == string.Empty) ? null : barcodeno;


            var data = db.sp_GetBookDetailsbyBookidorBookname(bookn, cno, asno, authname, barcode).ToList();
             return Json(data);
           
        }
        

          
           
    
       

        public JsonResult StudentBookAllotment(string[] bookallotments)
        {
            string s;

            for (int i = 0; i < bookallotments.Count(); i++)
            {
                s = bookallotments[i].ToString();
                string[] s1 = s.ToString().Split(',');
                string studids = string.Empty;
                studids = s1[0].ToString();
                int Studentid = db.tbl_student.Where(m => m.StudentID == studids).Select(m => m.Studid).FirstOrDefault();
                int bookid = Convert.ToInt32(s1[1].ToString());
                int noofdays = Convert.ToInt32(s1[2].ToString());
                int returnflag = Convert.ToInt32(s1[3].ToString());
                string hcallid = s1[4].ToString();
                int subid = Convert.ToInt32(Session["Userid"].ToString());
                int result= AddBookAllotment(Studentid, bookid, noofdays, returnflag, hcallid, subid);
                if (result < 0)
                { }



            }
            return Json(bookallotments);


        }

        public int AddBookAllotment(int studentid, int bookid, int noofdays, int returnflag,string hcallid, int CreatedBy)
        {
            int result;
            result = db.AddBookAllotment(studentid, bookid, noofdays, returnflag, hcallid,CreatedBy);
            return result;

        }

        public ActionResult BookReturn()
        {
            //libBookentry lb = new libBookentry();
            //lb._bookissuelist = db.tbl_lib_BookIssue.ToList();
            return View();
        }
        public JsonResult GetBookreturn(string studid)
        {
            int studids = db.tbl_student.Where(m => m.StudentID == studid).Select(m => m.Studid).FirstOrDefault();
            var bookreturn = db.sp_getbookissue(studids).ToList();
            var bookstatus = db.tbl_libBookReturnStatus.Select(m => new { m.BookStatusId, m.StatusName }).ToList();
            return Json(new { bookreturn = bookreturn, bookstatus = bookstatus }, JsonRequestBehavior.AllowGet);
           
        }

        public ActionResult BookUpdate(string[] presentdetails)
        {
            tbl_lib_BookIssue sa = new tbl_lib_BookIssue();
            string s;
            int createdby = Convert.ToInt32(Session["Userid"].ToString());
            for (int i = 0; i < presentdetails.Count(); i++)
            {

                s = presentdetails[i].ToString();
                string[] s1 = s.ToString().Split(',');
                sa.BookIssueId = Convert.ToInt32(s1[0].ToString());
                sa.BookStatus = Convert.ToInt32(s1[1].ToString());
                sa.Return_CreatedBy = createdby;
                db.sp_updatereturnbook(sa.BookIssueId, sa.Return_CreatedBy, sa.BookStatus);

  

                db.SaveChanges();

            }
            return Json(presentdetails);


        }

        public ActionResult BookCallStock(BookAllocation b)
        {
            b._BookStockList = db.Sp_GetBookStockList().ToList();
            
            return View(b);
        }


        
       public JsonResult check_duplicate_CallNo(string Call_number)
        {
            var data = db.tbl_BookDetails.Where(m => m.CallNo == Call_number).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
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


        public ActionResult BarCode(barcodeviewmodel lb)
        {
            lb._departmentlist = db.tblDepartment.Where(m => m.Status == true).ToList();
             lb.courselist = db.tbl_CourseMaster.Where(m => m.Status == true).ToList();
            lb._bookcategorylist = db.tbl_lib_BookCategory.Where(m => m.status == true).ToList();

            return View(lb);

            
        }


        public JsonResult GenerateBarCode(string[] Generatebarcode)
        {
            string s;
            for (int i = 0; i < Generatebarcode.Count(); i++)
            {
                s = Generatebarcode[i].ToString();
                string[] s2 = s.ToString().Split(',');

                int s3;
                for (int j = 0; j < s2.Count(); j++)
                {
                    s3 = Convert.ToInt32(s2[j]);
                    barcodecs objbar = new barcodecs();
                    Entity.lib_Bookentry tbl = db.lib_Bookentry.Where(x => (x.bookid == s3) && (x.IsGenerated==null || x.IsGenerated==false) ).FirstOrDefault();
                    db.ObjectStateManager.ChangeObjectState(tbl, System.Data.EntityState.Modified);
                    tbl.IsGenerated = true;
                    tbl.Barcode = objbar.generateBarcode(s3);
                    db.SaveChanges();
                }
            }

            return Json(Generatebarcode);


        }

        [HttpGet]
        public ActionResult GetGeneratePrintBarCode(string CourseId, string DeptId, string BookCategoryId)
        {
            int CourseIds = 0;
            int DeptIds = 0;
            int BookCategoryIds = 0;
            if (CourseId != "")
            { CourseIds = Convert.ToInt32(CourseId); }
            if (DeptId != "")
            { DeptIds = Convert.ToInt32(DeptId); }
            if (BookCategoryId != "")
            { BookCategoryIds = Convert.ToInt32(BookCategoryId); }
            IList<BarCodeModel> model = new List<BarCodeModel>();
            barcodecs objbar = new barcodecs();
            var list = db.sp_GetBarCodePrint(CourseIds, DeptIds, BookCategoryIds).ToList();
            foreach (var n in list)
            {
                model.Add(new BarCodeModel()
                {
                    ColA =n.C0 !=null? "data:image/png;base64," + Convert.ToBase64String((byte[])objbar.getBarcodeImage(n.C0.ToString(), string.Empty)) : "",
                    ColB = n.C1!= null ? "data:image/png;base64," + Convert.ToBase64String((byte[])objbar.getBarcodeImage(n.C1.ToString(), string.Empty)) : "",
                    ColC = n.C2!= null ? "data:image/png;base64," + Convert.ToBase64String((byte[])objbar.getBarcodeImage(n.C2.ToString(), string.Empty)) : "",
                    ColD = n.C3 != null ? "data:image/png;base64," + Convert.ToBase64String((byte[])objbar.getBarcodeImage(n.C3.ToString(), string.Empty)) : "",
                    ColE = n.C4 != null ? "data:image/png;base64," + Convert.ToBase64String((byte[])objbar.getBarcodeImage(n.C4.ToString(), string.Empty)) : ""


                });
            }
            return PartialView("_GeneratePrintBarCode", model);
        }


        [HttpGet]
        public ActionResult GetGenerateBarCode(string CourseId,string DeptId,string BookCategoryId)
        {   int CourseIds=0;
            int DeptIds=0;
            int BookCategoryIds=0;
            if (CourseId!="")
            { CourseIds = Convert.ToInt32(CourseId); }
            if (DeptId != "")
            { DeptIds = Convert.ToInt32(DeptId); }
            if (BookCategoryId != "")
            { BookCategoryIds = Convert.ToInt32(BookCategoryId); }
            IList<BarCodeModel> model = new List<BarCodeModel>();
            var list = db.Sp_GetBookForBarCode(CourseIds, DeptIds, BookCategoryIds).ToList();
            foreach (var n in list)
            {
                model.Add(new BarCodeModel()
                {
                    Id = n.bookid,
                    CallNo = n.CallNo,
                    AccessorNo = n.AccessorNo,
                    BookName = n.BookName,
                    Barcode = n.Barcode

                });
            }
            return PartialView("_GenerateBarCode", model);
        }
    }
}
