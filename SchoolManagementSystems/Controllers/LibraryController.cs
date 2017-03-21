using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

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
            else { lb._bookandJournallist = db.sp_GetSearchBookAndJournal(1, null, null, null, null, null, null,null,null).ToList(); }
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
            count=count + 1;
            lb.AccessorNo = count.ToString();
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
                           val=bi.Studid}).ToList();
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
       

        public JsonResult GetBookIssueDetails(string StudentName, int Studentid,int Stdid)
        {
            int stid;
            int Stdids;
           
            try { stid = Convert.ToInt32(Studentid);
           
            } catch { stid = 0; }
            try { Stdids = Convert.ToInt32(Stdid); }
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


        public JsonResult GetBookDetails(string BookName, string callno,string accessonno,string authorname)
        {
             string bookn;
             bookn=(BookName == string.Empty)?null: BookName;
             string cno;
             cno = (callno == string.Empty) ? null : callno;
             string asno;
             asno = (accessonno == string.Empty) ? null : accessonno;
             string authname;
             authname = (authorname == string.Empty) ? null : authorname;


            var data = db.sp_GetBookDetailsbyBookidorBookname(bookn, cno, asno, authname).ToList();
             return Json(data);
           
        }
        

          
           
    
       

        public JsonResult StudentBookAllotment(string[] bookallotments)
        {
            string s;

            for (int i = 0; i < bookallotments.Count(); i++)
            {
                s = bookallotments[i].ToString();
                string[] s1 = s.ToString().Split(',');
                int Studentid = Convert.ToInt32(s1[0].ToString());
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
        public JsonResult GetBookreturn(int studid)
        {
         
            var bookreturn = db.sp_getbookissue(studid).ToList();
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

    }
}
