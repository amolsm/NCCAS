using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL
{
    public class sqldataprocessinglayer
    {
        private string _conn;
        public string conn
        {
            get
            {
                return _conn = Convert.ToString(ConfigurationManager.ConnectionStrings["SchoolMgmtSysConnectionString"]);
            }
            set
            {
                _conn = value;
            }
        }
        public void AddParamToCmd(SqlCommand cmd, string paramnm, SqlDbType sqlDbType,int size, ParameterDirection parameterDirection, object val)
        {
            try
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = paramnm;
                param.SqlDbType = sqlDbType;
                param.Direction = parameterDirection;
                param.Value = val;
                cmd.Parameters.Add(param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int executebatch(SqlCommand cmd)
        {
            int result = 0;
            SqlTransaction trans = null;
            using (SqlConnection con = new SqlConnection(conn))
            {
                cmd.Connection = con;
                try
                {
                    con.Open();
                    trans = con.BeginTransaction();
                    cmd.Transaction = trans;
                    result = cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch (SqlException ex)
                {
                }
                finally
                {
                }
            }
            return result;
        }
    }
}