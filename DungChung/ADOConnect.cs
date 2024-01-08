using System;using QLBV_Database;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;

namespace QLBV.DungChung
{
    public class ADOConnect
    {
        public static string _connectionString = "server=" + DungChung.Bien.TenServer + ";database=" + DungChung.Bien.TenCSDL + ";uid=" + DungChung.Bien.accountsql + ";password=" + DungChung.Bien.passql + ";";
        public static bool ExecuteNonQuery(string strQuery, CommandType commandType, string[] strParameters, object[] oValues, SqlDbType[] sqlDbType, bool usingTransaction = false)
        {
            bool rs = false;
            TransactionScope scope = null;
            if (usingTransaction) scope = new TransactionScope();
            try
            {
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionString))
                {
                    if (_sqlConnection.State == ConnectionState.Open)
                        _sqlConnection.Close();
                    _sqlConnection.Open();
                    SqlTransaction trans = null;
                    SqlCommand _sqlCommand = new SqlCommand();
                    _sqlCommand.CommandText = strQuery;
                    _sqlCommand.CommandType = commandType;
                    _sqlCommand.Connection = _sqlConnection;
                    _sqlCommand.Transaction = trans;
                    DefineSqlParameter(_sqlCommand, strParameters, oValues, sqlDbType);
                    _sqlCommand.ExecuteNonQuery();
                    if (trans != null)
                        trans.Commit();
                    _sqlConnection.Close();
                }
                rs = true;
                if (scope != null)
                    scope.Complete();
            }
            catch (Exception ex)
            {
                rs = false;
                if (scope != null)
                    scope.Dispose();
                throw ex;
            }
            if (scope != null)
                scope.Dispose();
            return rs;
        }

        private static void DefineSqlParameter(SqlCommand sqlCommand, string[] strParameters, object[] oValues, SqlDbType[] sqlDbType)
        {
            SqlParameter sqlParameter;
            for (int i = 0; i < strParameters.Length; i++)
            {
                sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = strParameters[i];
                sqlParameter.SqlValue = oValues[i];
                sqlParameter.SqlDbType = sqlDbType[i];
                sqlCommand.Parameters.Add(sqlParameter);
            }
        }
    }
}
