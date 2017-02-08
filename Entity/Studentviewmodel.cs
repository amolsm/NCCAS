using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Studentviewmodel
    {
        [DisplayName("Student ID : ")]
        public int Studid { get; set; }

        [DisplayName("Student Name : ")]
        public string Studnm { get; set; }

        [DisplayName("Father's Name : ")]
        public string Studfathernm { get; set; }

        [DisplayName("Mother's Name : ")]
        public string Studmothernm { get; set; }

        [DisplayName("Enter Date Of Birth : ")]
       
        public DateTime DOB { get; set; }

        [DisplayName("Enter Weight in KG : ")]
        public int Weight { get; set; }

        [DisplayName("Enter Height in Centimeter : ")]
        public int Height { get; set; }

        [DisplayName("Student Blood Group : ")]
        public int StudBldGrp { get; set; }

        [DisplayName("Student Email Address : ")]
        public string StudEmail { get; set; }

        [DisplayName("Medical Info : ")]
        public string Disease { get; set; }

        [DisplayName("Student Religion : ")]
        public int Religionid { get; set; }

        [DisplayName("Sub Category : ")]
        public int Casteid { get; set; }

        [DisplayName("Select Course : ")]
        public int Classid { get; set; }

        [DisplayName("Enter RollNo : ")]
        public int RollNo { get; set; }

        [DisplayName("Select Gender : ")]
        public int Gender { get; set; }

        [DisplayName("Enter MotherTongue : ")]
        public string MotherTongue { get; set; }

        [DisplayName("Previous School : ")]
        public string PreviousSchool { get; set; }

        [DisplayName("Address of School : ")]
        public string SchoolAddress { get; set; }

        //[DisplayName("Class during Leaving : ")]
        //public int LastClass { get; set; }

        [DisplayName("Year of Passing : ")]
        public string LastClass { get; set; }

        [DisplayName("Total Marks : ")]
        public int PrScTotalMarks { get; set; }

        [DisplayName("Obtain Marks : ")]
        public int PrScObtainMark { get; set; }

        [DisplayName("Percentage : ")]
        public float PrScPercentage { get; set; }

        [DisplayName("Grade while leaving : ")]
        public string Grade { get; set; }

        [DisplayName("Year of leaving : ")]
        public string LeaveYear { get; set; }

        [DisplayName("Reason for Leaving : ")]
        public string LeaveReason { get; set; }

        [DisplayName("Principal Name : ")]
        public string PrincipalNm { get; set; }

        [DisplayName("Reference Contact Name : ")]
        public string ReferenceNm { get; set; }

        [DisplayName("Reference Contact Number : ")]
        public string ReferenceContact { get; set; }

        [DisplayName("Bus Facility? Yes/No : ")]
        public bool BusFacility { get; set; }

        [DisplayName("Bus Number : ")]
        public string BusNo { get; set; }

        [DisplayName("Bus RTO Number : ")]
        public string BusRTONo { get; set; }

        [DisplayName("Doctor Name : ")]
        public string EmergencyPhysicianNm { get; set; }

        [DisplayName("Doctor Contact : ")]
        public string EmergencyPhysicianContact { get; set; }

        [DisplayName("Doctor Address : ")]
        public string EmergencyAddress { get; set; }

        [DisplayName("Student's Pic : ")]
        public byte[] StudPic { get; set; }

        [DisplayName("Father's Occupation: ")]
        public int FatherOccpationid { get; set; }

        [DisplayName("Enter Father's Occupation: ")]
        public string FatherOccumationName { get; set; }

        [DisplayName("Father's Qualification : ")]
        public int FatherQualificationid { get; set; }

        [DisplayName("Enter Father's Qualification : ")]
        public string FatherQualificationName { get; set; }

        [DisplayName("Father's Email Address : ")]
        public string FatherEmail { get; set; }

        [DisplayName("Father's Office Address : ")]
        public string FatherOfficeAddress { get; set; }

        [DisplayName("Father's Contact : ")]
        public string FatherContact { get; set; }

        [DisplayName("Father's Blood Group : ")]
        public int FatherBldGrpid { get; set; }

        [DisplayName("Father's Pic : ")]
        public byte[] FatherPic { get; set; }

        [DisplayName("Guardian's Occupation: ")]
        public int GuardianOccpationid { get; set; }

        [DisplayName("Enter Guardian's Occupation: ")]
        public string GuardianOccpationName { get; set; }

        [DisplayName("Guardian's Qualification : ")]
        public int GuardianQualificationid { get; set; }

        [DisplayName("Enter Guardian's Qualification: ")]
        public string GuardianQualificationName { get; set; }

        [DisplayName("Guardian's Email Address : ")]
        public string GuardianEmail { get; set; }

        [DisplayName("Guardian's Office Address : ")]
        public string GuardianOfficeAddress { get; set; }

        [DisplayName("Guardian's Contact : ")]
        public string GuardianContact { get; set; }

        [DisplayName("Guardian Name : ")]
        public string GuardianName { get; set; }

        [DisplayName("Mother's Occupation: ")]
        public int MotherOccpationid { get; set; }

        [DisplayName("Enter Mother's Occupation: ")]
        public string MotherOccpationName{ get; set; }


        [DisplayName("Mother's Qualification : ")]
        public int MotherQualificationid { get; set; }

        [DisplayName("Enter Mother's Qualification : ")]
        public string MotherQualificationName { get; set; }

        [DisplayName("Mother's Email Address : ")]
        public string MotherEmail { get; set; }

        [DisplayName("Mother's Office Address : ")]
        public string MotherOfficeAddress { get; set; }

        [DisplayName("Mother's Contact : ")]
        public string MotherContact { get; set; }

        [DisplayName("Mother's Blood Group : ")]
        public int MotherBldGrpid { get; set; }

        [DisplayName("Mother's Pic : ")]
        public byte[] MPic { get; set; }

        [DisplayName("Select Country : ")]
        public int Countryid { get; set; }

        [DisplayName("Select State : ")]
        public int Stateid { get; set; }

        [DisplayName("Select City : ")]
        public int Cityid { get; set; }

        [DisplayName("Current Address : ")]
        public string CurrentAddress { get; set; }

        [DisplayName("Permanent Address : ")]
        public string PermanentAddress { get; set; }

        [DisplayName("Academic Year : ")]
        public List<string> academicyear { get; set; }

        [DisplayName("Select Bus No. : ")]
        public int busid { get; set; }

        [DisplayName("Select Category. : ")]
        public string cats { get; set; }

        [DisplayName("Select Products. : ")]
        public string prds { get; set; }

        [DisplayName("From Class ")]
        public int FromClass { get; set; }

        [DisplayName("To Class ")]
        public int ToClass { get; set; }

        [DisplayName("Upload Multiple Documents ")]
        public string Docs { get; set; }

        [DisplayName("Select SubCategories. : ")]
        public string subcats { get; set; }

        [DisplayName("Select Category ")]
        public int StudCategoryid { get; set; }

        [DisplayName("Father Code")]
        public string FCode { get; set; }

        [DisplayName("Mother Code")]
        public string MCode { get; set; }

        [DisplayName("Gaurdiam Code")]
        public string GCode { get; set; }

        [DisplayName("Reference Code")]
        public string RCode { get; set; }

        [DisplayName("Reference Code")]
        public string ECode { get; set; }

        public string FormType { get; set; }

        public int sta { get; set; }

        public int SelectGender { get; set; }

        [DisplayName("Select Department : ")]
        public int DepartmentId { get; set; }

        [DisplayName("Application ID : ")]

        public string ApplicationID { get; set; }

        [DisplayName("University Reg. : ")]
        public string UniversityRegId { get; set; }

        [DisplayName("Guardian's Pic : ")]
        public byte[] GuardianPic { get; set; }

        [DisplayName("Pincode: ")]
        public string Pincode { get; set; }


        [DisplayName("Registered Number")]
        public string PrScRegisterNumber { get; set; }

        [DisplayName("TC Number")]
        public string PrScTCNumber { get; set; }

        [DisplayName("Reference Letter")]
        public byte[] PrScReferenceLetter { get; set; }

        [DisplayName("Previous College Name")]
        public string PrUgCollegeName { get; set; }

        [DisplayName("College Address")]
        public string PrUgCollegeAddress { get; set; }

        [DisplayName("University - Affliated University")]
        public string PrUgAffilatedUniversity { get; set; }

        [DisplayName("Grade while leaving")]
        public string PrUgGradeLeaving { get; set; }

        [DisplayName("Total Marks")]
        public int PrUgTotalMark { get; set; }

        [DisplayName("Obtain Marks")]
        public int PrUgObtainMark { get; set; }

        [DisplayName("Year of leaving")]
        public string PrUgYearLeaving { get; set; }

        [DisplayName("Registered Number")]
        public string PrUgRegisterNumber { get; set; }

        [DisplayName("Reason for Leaving")]
        public string PrUgReasonofLeaving { get; set; }

        [DisplayName("Principal Name")]
        public string PrUgPrincipalName { get; set; }

        [DisplayName("Reference Contact Name")]
        public string PrUgRefContactName { get; set; }

        [DisplayName("Reference Contact Number")]
        public string PrUgRefContactNo { get; set; }

        [DisplayName("Percentage")]
        public float PrUgPercentage { get; set; }

        [DisplayName("College Name")]
        public string PrPgCollegeName { get; set; }

        [DisplayName("Previous College Address")]
        public string PrPgCollegeAddress { get; set; }

        [DisplayName("University - Affliated University")]
        public string PrPgAffilatedUniversity { get; set; }

        [DisplayName("Grade while leaving")]
        public string PrPgGradeLeaving { get; set; }

        [DisplayName("Total Marks")]
        public int PrPgTotalMark { get; set; }

        [DisplayName("Obtain Marks")]
        public int PrPgObtainMark { get; set; }

        [DisplayName("Year of leaving")]
        public string PrPgYearLeaving { get; set; }

        [DisplayName("Registered Number")]
        public string PrPgRegisterNumber { get; set; }

        [DisplayName("Reason for Leaving")]
        public string PrPgReasonofLeaving { get; set; }

        [DisplayName("Principal Name")]
        public string PrPgPrincipalName { get; set; }

        [DisplayName("Reference Contact Name")]
        public string PrPgRefContactName { get; set; }

        [DisplayName("Reference Contact Number")]
        public string PrPgRefContactNo { get; set; }

        [DisplayName("Percentage")]
        public float PrPgPercentage { get; set; }

        

        public string Sibling1Name { get; set; }


        public string Sibling2Name { get; set; }

        public string Sibling3Name { get; set; }

        public string Sibling4Name { get; set; }

        public string Sibling1Rel { get; set; }


        public string Sibling2Rel { get; set; }

        public string Sibling3Rel { get; set; }

        public string Sibling4Rel { get; set; }
       
        public DateTime Sibling1DOB { get; set; }
       
        public DateTime Sibling2DOB { get; set; }
       
        public DateTime Sibling3DOB { get; set; }
       
        public DateTime Sibling4DOB { get; set; }
        public string Sibling1Ql { get; set; }


        public string Sibling2Ql { get; set; }


        public string Sibling3Ql { get; set; }


        public string Sibling4Ql { get; set; }

        [DisplayName("12MarksSheet Scan Copy : ")]
        public byte[] SCMarksheet { get; set; }

        [DisplayName("College UG Scan Copy : ")]
        public byte[] UGMarksheet { get; set; }

        [DisplayName("College PG Scan Copy : ")]
        public byte[] PGMarksheet { get; set; }

        [DisplayName("TC Scan Copy : ")]
        public byte[] SCTCPic { get; set; }

        [DisplayName("ID Type : ")]
        public string DocumentType{ get; set; }

        [DisplayName("ID Number : ")]
        public string DocumentIDNo { get; set; }

        [DisplayName("Parish name : ")]
        public string ParishName { get; set; }

       
        [DisplayName("Diocese name : ")]
        public string DioceseName { get; set; }

        [DisplayName("ID Type name : ")]
        public string Documenttypename { get; set; }

        [DisplayName("Course Year : ")]
        public int courseyear { get; set; }

        public List<tbl_YearMaster> yearlist { get; set; }

        public List<tblDepartment> departmentlistdetails { get; set; }
        public List<tbl_student> StudentDataCollection { get; set; }
        public List<tbl_online_student> online_StudentDataCollection { get; set; }
        public List<tbl_class> classlist { get; set; }
        public List<tbl_transport> Translist { get; set; }
        public List<tbl_bloodgroup> bloodgrouplist { get; set; }
        public IEnumerable<tbl_category> catlist { get; set; }
        public List<tbl_caste> castelist { get; set; }
        public List<tbl_occupation> occupationlist { get; set; }
        public List<tbl_qualification> qualificationlist { get; set; }
        public List<tbl_religion> religionlist { get; set; }
        public List<tbl_StudentCategory> categorylist { get; set; }
        public List<tbl_country> countrylist { get; set; }
        public List<tbl_state> statelist { get; set; }
        public List<tbl_city> citylist { get; set; }

        public List<tbl_CourseMaster> courselist { get; set; }
        public string[] categories { get; set; }
        public string[] subcategories { get; set; }
        public string[] products { get; set; }
        public byte[] sc_refletter { get; set; }
    }
}
