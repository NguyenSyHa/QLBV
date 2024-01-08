using System;using QLBV_Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace QLBV.DungChung
{
    public class ProcessCommandSQL
    {
        public string connectString = "";
        public void set_connectString(string server, string db, string user, string pass)
        {
            connectString = "Data Source=" + server + ";Initial Catalog=" + db + ";User ID=" + user + ";Password=" + pass + "";
        }
        public string ExecuteCommand(string command)
        {
            string returns = "";
            SqlConnection myconnect = new SqlConnection(connectString);
            try
            {
                myconnect.Open();
                SqlCommand commandSQL = new SqlCommand(command, myconnect);
                commandSQL.ExecuteNonQuery();
                commandSQL.Dispose();
                myconnect.Close();
                return returns;
            }
            catch (Exception ex)
            {
                returns = "Lỗi! " + ex.Message;
                return returns;
            }
        }

        public void ExecuteCommand_NotReturnError(string command)
        {
            SqlConnection myconnect = new SqlConnection(connectString);
            myconnect.Open();
            SqlCommand commandSQL = new SqlCommand(command, myconnect);
            commandSQL.ExecuteNonQuery();
            commandSQL.Dispose();
            myconnect.Close();
        }

        public void SaveDataInTables(DataTable dataTable, string tablename, List<SqlBulkCopyColumnMapping> listColumnMappings = null)
        {
            if (dataTable.Rows.Count > 0)
            {
                using (SqlConnection con = new SqlConnection(connectString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        if (listColumnMappings != null && listColumnMappings.Count > 0)
                        {
                            foreach (var item in listColumnMappings)
                            {
                                sqlBulkCopy.ColumnMappings.Add(item);
                            }
                        }
                        sqlBulkCopy.DestinationTableName = tablename;
                        con.Open();
                        sqlBulkCopy.WriteToServer(dataTable);
                        con.Close();
                    }
                }
            }
        }
    }
}
