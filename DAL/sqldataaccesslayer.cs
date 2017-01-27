using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class sqldataaccesslayer : sqldataprocessinglayer
    {
        //public int Insert_or_Update(Studentviewmodel svm, string act)
        //{
        //    int result = 0;
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "sp_student_admission";
        //    AddParamToCmd(cmd, "@Studid", SqlDbType.Int, 50, ParameterDirection.Input, svm.Studid);
        //    AddParamToCmd(cmd, "@Studsession", SqlDbType.VarChar, 50, ParameterDirection.Input, svm.Studsession = Convert.ToString(System.DateTime.Now.Year + "-" + System.DateTime.Now.AddYears(1).Year));
        //    AddParamToCmd(cmd, "@Studnm", SqlDbType.VarChar, 50, ParameterDirection.Input, svm.Studnm);
        //    AddParamToCmd(cmd, "@Gender", SqlDbType.Bit, 20, ParameterDirection.Input, svm.Gender);
        //    AddParamToCmd(cmd, "@Classid", SqlDbType.Int, 50, ParameterDirection.Input, svm.Classid);
        //    AddParamToCmd(cmd, "@Studfathernm", SqlDbType.VarChar, 50, ParameterDirection.Input, svm.Studfathernm);
        //    AddParamToCmd(cmd, "@FatherEdu", SqlDbType.VarChar, 50, ParameterDirection.Input, svm.FatherEdu);
        //    AddParamToCmd(cmd, "@Studmothernm", SqlDbType.VarChar, 50, ParameterDirection.Input, svm.Studmothernm);
        //    AddParamToCmd(cmd, "@MotherEdu", SqlDbType.VarChar, 50, ParameterDirection.Input, svm.MotherEdu);
        //    AddParamToCmd(cmd, "@Emailid", SqlDbType.VarChar, 50, ParameterDirection.Input, svm.Emailid);
        //    AddParamToCmd(cmd, "@MobileNo", SqlDbType.VarChar, 20, ParameterDirection.Input, svm.MobileNo);
        //    AddParamToCmd(cmd, "@Countryid", SqlDbType.Int, 20, ParameterDirection.Input, svm.Countryid);
        //    AddParamToCmd(cmd, "@Stateid", SqlDbType.Int, 20, ParameterDirection.Input, svm.Stateid);
        //    AddParamToCmd(cmd, "@Cityid", SqlDbType.Int, 20, ParameterDirection.Input, svm.Cityid);
        //    AddParamToCmd(cmd, "@Address", SqlDbType.VarChar, 100000, ParameterDirection.Input, svm.Address);
        //    AddParamToCmd(cmd, "@PreviousEdulDesc", SqlDbType.VarChar, 100000, ParameterDirection.Input, svm.PreviousEdulDesc);
        //    AddParamToCmd(cmd, "@FeeStatus", SqlDbType.Bit, 20, ParameterDirection.Input, svm.FeeStatus);
        //    AddParamToCmd(cmd, "@TotalFees", SqlDbType.Money, 10000, ParameterDirection.Input, svm.TotalFees);
        //    AddParamToCmd(cmd, "@FeeDesc", SqlDbType.VarChar, 100000, ParameterDirection.Input, svm.FeeDesc);
        //    AddParamToCmd(cmd, "@SysDt", SqlDbType.DateTime, 100000, ParameterDirection.Input, svm.SysDt = System.DateTime.Now);
        //    AddParamToCmd(cmd, "@DOB", SqlDbType.DateTime, 100, ParameterDirection.Input, svm.DOB);
        //    AddParamToCmd(cmd, "@act", SqlDbType.VarChar, 20, ParameterDirection.Input, act);
        //    result = executebatch(cmd);
        //    return result;
        //}
    }
}
