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

        [DisplayName("Date Of Birth : ")]

      
        public string DOB { get; set; }

        [DisplayName("Weight in KG : ")]
        public int Weight { get; set; }

        [DisplayName("Height in Centimeter : ")]
        public int Height { get; set; }

        [DisplayName("Blood Group : ")]
        public int StudBldGrp { get; set; }

        [DisplayName("Email Address : ")]
        public string StudEmail { get; set; }

        [DisplayName("Medical Info : ")]
        public string Disease { get; set; }

        [DisplayName("Religion : ")]
        public int Religionid { get; set; }

        [DisplayName("Sub Category : ")]
        public int Casteid { get; set; }

        [DisplayName("Course : ")]
        public int Classid { get; set; }

        [DisplayName("RollNo : ")]
        public int RollNo { get; set; }

        [DisplayName("Gender : ")]
        public int Gender { get; set; }

        [DisplayName("MotherTongue : ")]
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

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [DisplayName("Percentage : ")]
        public double PrScPercentage { get; set; }

        [DisplayName("Grade: ")]
        public string Grade { get; set; }

        [DisplayName("Year of leaving : ")]
        public DateTime LeaveYear { get; set; }

        [DisplayName("Reason for Leaving : ")]
        public string LeaveReason { get; set; }

        [DisplayName("Principal Name : ")]
        public string PrincipalNm { get; set; }

        [DisplayName("Reference Name : ")]
        public string ReferenceNm { get; set; }

        [DisplayName("Reference Number : ")]
        public string ReferenceContact { get; set; }

        [DisplayName("Bus Facility? Yes/No : ")]
        public bool BusFacility { get; set; }

        [DisplayName("Bus Number : ")]
        public string BusNo { get; set; }

        [DisplayName("Bus RTO Number : ")]
        public string BusRTONo { get; set; }

        [DisplayName("Name : ")]
        public string EmergencyPhysicianNm { get; set; }

        [DisplayName("Contact : ")]
        public string EmergencyPhysicianContact { get; set; }

        [DisplayName("Address : ")]
        public string EmergencyAddress { get; set; }

        [DisplayName("Student's Pic : ")]
        public byte[] StudPic { get; set; }

        [DisplayName("Father's Occupation: ")]
        public int FatherOccpationid { get; set; }

        [DisplayName("Father's Occupation: ")]
        public string FatherOccumationName { get; set; }

        [DisplayName("Father's Qualification : ")]
        public int FatherQualificationid { get; set; }

        [DisplayName("Father's Qualification : ")]
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

        [DisplayName("Guardian's Occupation: ")]
        public string GuardianOccpationName { get; set; }

        [DisplayName("Guardian's Qualification : ")]
        public int GuardianQualificationid { get; set; }

        [DisplayName("Guardian's Qualification: ")]
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

        [DisplayName("Mother's Occupation: ")]
        public string MotherOccpationName { get; set; }


        [DisplayName("Mother's Qualification : ")]
        public int MotherQualificationid { get; set; }

        [DisplayName("Mother's Qualification : ")]
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

        [DisplayName("Country : ")]
        public int Countryid { get; set; }

        [DisplayName("State : ")]
        public int Stateid { get; set; }

        [DisplayName("City : ")]
        public int Cityid { get; set; }

        [DisplayName("Current Address : ")]
        public string CurrentAddress { get; set; }

        [DisplayName("Permanent Address : ")]
        public string PermanentAddress { get; set; }

        [DisplayName("Academic Year : ")]
        public List<string> academicyear { get; set; }

        [DisplayName("Bus No. : ")]
        public int busid { get; set; }

        [DisplayName("Category. : ")]
        public string cats { get; set; }

        [DisplayName("Products. : ")]
        public string prds { get; set; }

        [DisplayName("From Class ")]
        public int FromClass { get; set; }

        [DisplayName("To Class ")]
        public int ToClass { get; set; }

        [DisplayName("Upload Multiple Documents ")]
        public string Docs { get; set; }

        [DisplayName("SubCategories. : ")]
        public string subcats { get; set; }

        [DisplayName("Category ")]
        public int StudCategoryid { get; set; }

        [DisplayName("Father Code")]
        public string FCode { get; set; } = "+91";

        [DisplayName("Mother Code")]
        public string MCode { get; set; } = "+91";

        [DisplayName("Gaurdiam Code")]
        public string GCode { get; set; } = "+91";

        [DisplayName("Reference Code")]
        public string RCode { get; set; } = "+91";

        [DisplayName("Reference Code")]
        public string ECode { get; set; } = "+91";

        public string FormType { get; set; }

        public int sta { get; set; }

        public int SelectGender { get; set; }

        [DisplayName("Department : ")]
        public int DepartmentId { get; set; }

        [DisplayName("Application ID : ")]

        public string ApplicationID { get; set; }

        [DisplayName("University Reg. : ")]
        public string UniversityRegId { get; set; }

        [DisplayName("Guardian's Pic : ")]
        public byte[] GuardianPic { get; set; }

        [DisplayName("Pincode: ")]
        public string Pincode { get; set; }


        [DisplayName("Registered Number:")]
        public string PrScRegisterNumber { get; set; }

        [DisplayName("TC No.")]
        public string PrScTCNumber { get; set; }

        [DisplayName("Reference Letter:")]
        public byte[] PrScReferenceLetter { get; set; }

        [DisplayName("Previous College Name:")]
        public string PrUgCollegeName { get; set; }

        [DisplayName("College Address:")]
        public string PrUgCollegeAddress { get; set; }

        [DisplayName("University - Affliated University:")]
        public string PrUgAffilatedUniversity { get; set; }

        [DisplayName("Grade while leaving:")]
        public string PrUgGradeLeaving { get; set; }

        [DisplayName("Total Marks:")]
        public int PrUgTotalMark { get; set; }

        [DisplayName("Obtain Marks:")]
        public int PrUgObtainMark { get; set; }

        [DisplayName("Year of leaving:")]
        public string PrUgYearLeaving { get; set; }

        [DisplayName("Registered Number:")]
        public string PrUgRegisterNumber { get; set; }

        [DisplayName("Reason for Leaving:")]
        public string PrUgReasonofLeaving { get; set; }

        [DisplayName("Principal Name:")]
        public string PrUgPrincipalName { get; set; }

        [DisplayName("Reference Name:")]
        public string PrUgRefContactName { get; set; }

        [DisplayName("Reference Number:")]
        public string PrUgRefContactNo { get; set; }

        [DisplayName("Percentage:")]
        public float PrUgPercentage { get; set; }

        [DisplayName("College Name:")]
        public string PrPgCollegeName { get; set; }

        [DisplayName("Previous College Address:")]
        public string PrPgCollegeAddress { get; set; }

        [DisplayName("University - Affliated University:")]
        public string PrPgAffilatedUniversity { get; set; }

        [DisplayName("Grade while leaving:")]
        public string PrPgGradeLeaving { get; set; }

        [DisplayName("Total Marks:")]
        public int PrPgTotalMark { get; set; }

        [DisplayName("Obtain Marks:")]
        public int PrPgObtainMark { get; set; }

        [DisplayName("Year of leaving:")]
        public DateTime PrPgYearLeaving { get; set; }

        [DisplayName("Registered Number:")]
        public string PrPgRegisterNumber { get; set; }

        [DisplayName("Reason for Leaving:")]
        public string PrPgReasonofLeaving { get; set; }

        [DisplayName("Principal Name:")]
        public string PrPgPrincipalName { get; set; }

        [DisplayName("Reference Name:")]
        public string PrPgRefContactName { get; set; }

        [DisplayName("Reference Number:")]
        public string PrPgRefContactNo { get; set; }

        [DisplayName("Percentage:")]
        public float PrPgPercentage { get; set; }



        public string Sibling1Name { get; set; }


        public string Sibling2Name { get; set; }

        public string Sibling3Name { get; set; }

        public string Sibling4Name { get; set; }

        public string Sibling1Rel { get; set; }


        public string Sibling2Rel { get; set; }

        public string Sibling3Rel { get; set; }

        public string Sibling4Rel { get; set; }

        public string Sibling1DOB { get; set; }

        public string Sibling2DOB { get; set; }

        public string Sibling3DOB { get; set; }

        public string Sibling4DOB { get; set; }
        public string Sibling1Ql { get; set; }


        public string Sibling2Ql { get; set; }


        public string Sibling3Ql { get; set; }


        public string Sibling4Ql { get; set; }

        [DisplayName("MarksSheet Scan Copy : ")]
        public byte[] SCMarksheet { get; set; }

        [DisplayName("College UG Scan Copy : ")]
        public byte[] UGMarksheet { get; set; }

        [DisplayName("College PG Scan Copy : ")]
        public byte[] PGMarksheet { get; set; }

        [DisplayName("TC Scan Copy : ")]
        public byte[] SCTCPic { get; set; }

        [DisplayName("ID Type : ")]
        public string DocumentType { get; set; }

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

        [DisplayName("Registration.No.: ")]
        public string StdRegNo { get; set; }

        [DisplayName("Register Mobile No.: ")]
        public string StdRegMob { get; set; }

        [DisplayName("RelationShip:")]
        public string emcontactrel { get; set; }

        [DisplayName("Status:")]
        public int St_Status { get; set; }

        [DisplayName("Subject")]
        public string subject1 { get; set; }

        [DisplayName("Subject")]
        public string subject2 { get; set; }

        [DisplayName("Subject")]
        public string subject3 { get; set; }

        [DisplayName("Subject")]
        public string subject4 { get; set; }

        [DisplayName("Subject")]
        public string subject5 { get; set; }

        [DisplayName("Subject")]
        public string subject6 { get; set; }

        [DisplayName("Marks")]
        public int marks1 { get; set; }

        [DisplayName("Marks")]
        public int marks2 { get; set; }

        [DisplayName("Marks")]
        public int marks3 { get; set; }

        [DisplayName("Marks")]
        public int marks4 { get; set; }

        [DisplayName("Marks")]
        public int marks5 { get; set; }

        [DisplayName("Marks")]
        public int marks6 { get; set; }

        [DisplayName("Maximum")]
        public int maximum1 { get; set; }

        [DisplayName("Maximum")]
        public int maximum2 { get; set; }

        [DisplayName("Maximum")]
        public int maximum3 { get; set; }

        [DisplayName("Maximum")]
        public int maximum4 { get; set; }

        [DisplayName("Maximum")]
        public int maximum5 { get; set; }

        [DisplayName("Maximum")]
        public int maximum6 { get; set; }

        public bool d1 { get; set; }
        public int d2 { get; set; }

        public int test { get; set; }
        [DisplayName("Are You Son/Daughter Of Ex-Serviceman Of Tamilnadu Origin:")]
        public string c1 { get; set; }

        [DisplayName("Are You Of Tamilnadu Origin From Andaman Nicobar Islands ?:")]
        public string c2 { get; set; }

        [DisplayName("Distinction In Sports & Games/NCC/NSS:")]
        public string c3 { get; set; }

        [DisplayName("Proficiency In Extra-Curricular Activities:")]
        public string c4 { get; set; }

        [DisplayName("If Physically Challenged Specify:")]
        public string c5 { get; set; }

        [DisplayName("Name & Location (District) Of School Last Studied:")]
        public string namelocation { get; set; }

        [DisplayName("Qualifying Examination Passed")]
        public string examinationpassed { get; set; }

        [DisplayName("Year Of Passing:")]
        public DateTime pyear1 { get; set; }

        [DisplayName("Register No:")]
        public string rnumber1 { get; set; }

        [DisplayName("No. Of Attempts:")]
        public int attempts1 { get; set; }


        [DisplayName("Year Of Leaving")]
        public DateTime sleaving { get; set; }

        [DisplayName("Subject")]
        public string psubject1 { get; set; }

        [DisplayName("Subject")]
        public string psubject2 { get; set; }

        [DisplayName("Subject")]
        public string psubject3 { get; set; }

        [DisplayName("Subject")]
        public string psubject4 { get; set; }

        [DisplayName("Subject")]
        public string psubject5 { get; set; }

        [DisplayName("Subject")]
        public string psubject6 { get; set; }

        [DisplayName("Marks")]
        public int pmarks1 { get; set; }

        [DisplayName("Marks")]
        public int pmarks2 { get; set; }

        [DisplayName("Marks")]
        public int pmarks3 { get; set; }

        [DisplayName("Marks")]
        public int pmarks4 { get; set; }

        [DisplayName("Marks")]
        public int pmarks5 { get; set; }

        [DisplayName("Marks")]
        public int pmarks6 { get; set; }

        [DisplayName("Maximum")]
        public int pmaximum1 { get; set; }

        [DisplayName("Maximum")]
        public int pmaximum2 { get; set; }

        [DisplayName("Maximum")]
        public int pmaximum3 { get; set; }

        [DisplayName("Maximum")]
        public int pmaximum4 { get; set; }

        [DisplayName("Maximum")]
        public int pmaximum5 { get; set; }

        [DisplayName("Maximum")]
        public int pmaximum6 { get; set; }

        [DisplayName("Year Of Passing:")]
        public DateTime ppyear { get; set; }

        [DisplayName("Year Of Passing:")]
        public DateTime pyear2 { get; set; }

        [DisplayName("Year Of Passing:")]
        public DateTime pyear3 { get; set; }

        [DisplayName("Year Of Passing:")]
        public DateTime pyear4 { get; set; }

        [DisplayName("Register No:")]
        public string prnumber1 { get; set; }

        [DisplayName("No. Of Attempts:")]
        public int pattempts1 { get; set; }



        [DisplayName("Are You Son/Daughter Of Ex-Serviceman Of Tamilnadu Origin:")]
        public string pc1 { get; set; }

        [DisplayName("Are You Of Tamilnadu Origin From Andaman Nicobar Islands ?:")]
        public string pc2 { get; set; }

        [DisplayName("Distinction In Sports & Games/NCC/NSS:")]
        public string pc3 { get; set; }

        [DisplayName("Proficiency In Extra-Curricular Activities:")]
        public string pc4 { get; set; }

        [DisplayName("If Physically Challenged Specify:")]
        public string pc5 { get; set; }

        [DisplayName("Name & Location (District) Of School Last Studied:")]
        public string pnamelocation { get; set; }

        [DisplayName("Qualifying Examination Passed: HSC (OR) Equivalent:")]
        public string pexaminationpassed { get; set; }

        [DisplayName("Class")]
        public int uclass1 { get; set; }

        [DisplayName("Class")]
        public int uclass2 { get; set; }

        [DisplayName("Class")]
        public int uclass3 { get; set; }

        [DisplayName("Class")]
        public int uclass4 { get; set; }

        [DisplayName("Class")]
        public int uclass5 { get; set; }

        [DisplayName("Class")]
        public int uclass6 { get; set; }

        [DisplayName("Select Option")]
        public int ugoldm { get; set; }

        [DisplayName("University Rank")]
        public string uUniversityRank { get; set; }


        [DisplayName("Subject")]
        public string hsubject1 { get; set; }

        [DisplayName("Subject")]
        public string hsubject2 { get; set; }

        [DisplayName("Subject")]
        public string hsubject3 { get; set; }

        [DisplayName("Subject")]
        public string hsubject4 { get; set; }

        [DisplayName("Subject")]
        public string hsubject5 { get; set; }

        [DisplayName("Subject")]
        public string hsubject6 { get; set; }

        [DisplayName("Marks")]
        public int hmarks1 { get; set; }

        [DisplayName("Marks")]
        public int hmarks2 { get; set; }

        [DisplayName("Marks")]
        public int hmarks3 { get; set; }

        [DisplayName("Marks")]
        public int hmarks4 { get; set; }

        [DisplayName("Marks")]
        public int hmarks5 { get; set; }

        [DisplayName("Marks")]
        public int hmarks6 { get; set; }

        [DisplayName("Maximum")]
        public int hmaximum1 { get; set; }

        [DisplayName("Maximum")]
        public int hmaximum2 { get; set; }

        [DisplayName("Maximum")]
        public int hmaximum3 { get; set; }

        [DisplayName("Maximum")]
        public int hmaximum4 { get; set; }

        [DisplayName("Maximum")]
        public int hmaximum5 { get; set; }

        [DisplayName("Maximum")]
        public int hmaximum6 { get; set; }

        [DisplayName("Year Of Passing:")]
        public DateTime hpyear { get; set; }

        [DisplayName("Year Of Leaving")]
        public DateTime hleaving { get; set; }

        [DisplayName("Register No:")]
        public string hrnumber1 { get; set; }

        [DisplayName("No. Of Attempts:")]
        public int hattempts1 { get; set; }

        [DisplayName("Obtain Mark")]
        public int hobtain { get; set; }

        [DisplayName("Total Mark")]
        public int htotalmark { get; set; }

        [DisplayName("Percentage")]
        public double hpercentage { get; set; }

        [DisplayName("Grade")]
        public string hgrade { get; set; }

        [DisplayName("School Name")]
        public string hschool { get; set; }

        [DisplayName("Are You Son/Daughter Of Ex-Serviceman Of Tamilnadu Origin:")]
        public string hc1 { get; set; }

        [DisplayName("Are You Of Tamilnadu Origin From Andaman Nicobar Islands ?:")]
        public string hc2 { get; set; }

        [DisplayName("Distinction In Sports & Games/NCC/NSS:")]
        public string hc3 { get; set; }

        [DisplayName("Proficiency In Extra-Curricular Activities:")]
        public string hc4 { get; set; }

        [DisplayName("If Physically Challenged Specify:")]
        public string hc5 { get; set; }

        [DisplayName("Name & Location (District) Of School Last Studied:")]
        public string hnamelocation { get; set; }

        [DisplayName("Qualifying Examination Passed: HSC (OR) Equivalent:")]
        public string hexaminationpassed { get; set; }

        [DisplayName("Reference Name")]
        public string hrefname { get; set; }

        [DisplayName("Reference Number")]
        public string hrefno { get; set; }

        [DisplayName("Code")]
        public int hrefcode { get; set; }

        [DisplayName("TC No.")]
        public string htcno { get; set; }

        [DisplayName("Reference Letter")]
        public byte[] hrefscan { get; set; }

        [DisplayName("TC Scan Copy")]
        public byte[] htcscan { get; set; }

        [DisplayName("MarksSheet Scan Copy")]
        public byte[] hmarksheetscan { get; set; }









        [DisplayName("Subject")]
        public string pgsubject1 { get; set; }

        [DisplayName("Subject")]
        public string pgsubject2 { get; set; }

        [DisplayName("Subject")]
        public string pgsubject3 { get; set; }

        [DisplayName("Subject")]
        public string pgsubject4 { get; set; }

        [DisplayName("Subject")]
        public string pgsubject5 { get; set; }

        [DisplayName("Subject")]
        public string pgsubject6 { get; set; }

        [DisplayName("Marks")]
        public int pgmarks1 { get; set; }

        [DisplayName("Marks")]
        public int pgmarks2 { get; set; }

        [DisplayName("Marks")]
        public int pgmarks3 { get; set; }

        [DisplayName("Marks")]
        public int pgmarks4 { get; set; }

        [DisplayName("Marks")]
        public int pgmarks5 { get; set; }

        [DisplayName("Marks")]
        public int pgmarks6 { get; set; }

        [DisplayName("Maximum")]
        public int pgmaximum1 { get; set; }

        [DisplayName("Maximum")]
        public int pgmaximum2 { get; set; }

        [DisplayName("Maximum")]
        public int pgmaximum3 { get; set; }

        [DisplayName("Maximum")]
        public int pgmaximum4 { get; set; }

        [DisplayName("Maximum")]
        public int pgmaximum5 { get; set; }

        [DisplayName("Maximum")]
        public int pgmaximum6 { get; set; }

        [DisplayName("Year Of Passing:")]
        public DateTime pgpyear { get; set; }

        [DisplayName("No. Of Attempts:")]
        public int pgattempts1 { get; set; }

        [DisplayName("Are You Son/Daughter Of Ex-Serviceman Of Tamilnadu Origin:")]
        public string pgc1 { get; set; }

        [DisplayName("Are You Of Tamilnadu Origin From Andaman Nicobar Islands ?:")]
        public string pgc2 { get; set; }

        [DisplayName("Distinction In Sports & Games/NCC/NSS:")]
        public string pgc3 { get; set; }

        [DisplayName("Proficiency In Extra-Curricular Activities:")]
        public string pgc4 { get; set; }

        [DisplayName("If Physically Challenged Specify:")]
        public string pgc5 { get; set; }

        [DisplayName("Answer")]
        public string answer1 { get; set; }

        [DisplayName("Answer")]
        public string answer2 { get; set; }

        [DisplayName("Answer")]
        public string answer3 { get; set; }

        [DisplayName("Answer")]
        public string answer4 { get; set; }

        [DisplayName("Answer")]
        public string answer5 { get; set; }

        [DisplayName("Answer")]
        public string answer6 { get; set; }

        [DisplayName("Answer")]
        public string answer7 { get; set; }

        [DisplayName("Answer")]
        public string answer8 { get; set; }

        [DisplayName("Class")]
        public int pclass1 { get; set; }

        [DisplayName("Class")]
        public int pclass2 { get; set; }

        [DisplayName("Class")]
        public int pclass3 { get; set; }

        [DisplayName("Class")]
        public int pclass4 { get; set; }

        [DisplayName("Class")]
        public int pclass5 { get; set; }

        [DisplayName("Class")]
        public int pclass6 { get; set; }

        [DisplayName("Select Option")]
        public int goldm { get; set; }

        [DisplayName("University Rank")]
        public string UniversityRank { get; set; }

        [DisplayName("Department : ")]
        public int deptpg { get; set; }

        [DisplayName("Course : ")]
        public int coursepg { get; set; }

        [DisplayName("Department : ")]
        public int deptug { get; set; }

        [DisplayName("Course : ")]
        public int courseug { get; set; }

        [DisplayName("Name & Location (District) Of School Last Studied:")]
        public string pgnamelocation { get; set; }

        [DisplayName("Qualifying Examination Passed: HSC (OR) Equivalent:")]
        public string pgexaminationpassed { get; set; }

        [DisplayName("Year Of Leaving:")]
        public DateTime uglyear { get; set; }

        [DisplayName("Code")]
        public int prefcode { get; set; }

        [DisplayName("Code")]
        public int urefcode { get; set; }
        //[DisplayName("Total")]
        //public int ssctotal { get; set; }

        //[DisplayName("Precentage")]
        //public int sscprecentage { get; set; }

        //[DisplayName("Passing")]
        //public int sscpassing { get; set; }

        //[DisplayName("Total")]
        //public int hsctotal { get; set; }

        //[DisplayName("Precentage")]
        //public int hscprecentage { get; set; }

        //[DisplayName("Passing")]
        //public int hscpassing { get; set; }

        //[DisplayName("Total")]
        //public int ugtotal { get; set; }

        //[DisplayName("Precentage")]
        //public int ugprecentage { get; set; }

        //[DisplayName("Passing")]
        //public int ugpassing { get; set; }

        //[DisplayName("Total")]
        //public int pgtotal { get; set; }

        //[DisplayName("Precentage")]
        //public int pgprecentage { get; set; }

        //[DisplayName("Passing")]
        //public int pgpassing { get; set; }

        public List<tbl_YearMaster> yearlist { get; set; }

        public List<tblDepartment> departmentlistdetails { get; set; }
        public List<sp_GetStudentDataCollection_Result> StudentDataCollection { get; set; }
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
        public List<tbl_collageeducationdetails> collage { get; set; }
        public List<secondaryeducationdetail> secondry { get; set; }
        public List<tbl_city> citylist { get; set; }
        public List<CONFIGMASTER> select { get; set; }


        public List<tbl_CourseMaster> courselist { get; set; }
        public string[] categories { get; set; }
        public string[] subcategories { get; set; }
        public string[] products { get; set; }
        public byte[] sc_refletter { get; set; }

        public List<sp_getTeacherCourse_Result> _courselist { get; set; }

        public List<tbl_YearMaster> YearList { get; set; }

        public List<tblDepartment> DepartmentList { get; set; }
    }
}
