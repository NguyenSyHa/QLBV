using System;
using QLBV_Database;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;


namespace QLBV.DungChung
{
    class ConnectData
    {
        #region Biến

        private bool _isConnect;
        private SqlConnection _sqlConnection;
        private SqlCommand _sqlCommand;
        private string _connectionString;

        public bool isConnect
        {
            get { return _isConnect; }
            set { _isConnect = value; }
        }
        public SqlConnection sqlConnection
        {
            get { return _sqlConnection; }
            set { _sqlConnection = value; }
        }
        public SqlCommand sqlCommand
        {
            get { return _sqlCommand; }
            set { _sqlCommand = value; }
        }
        public string connectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        #endregion

        #region connect
        public void Connect()
        {
            try
            {
                if (_sqlConnection == null || _sqlConnection.State == ConnectionState.Closed)
                {
                    _connectionString = "server=" + DungChung.Bien.TenServer + ";database=" + DungChung.Bien.TenCSDL + ";uid=" + DungChung.Bien.accountsql + ";password=" + DungChung.Bien.passql + ";";
                    _sqlConnection = new SqlConnection(_connectionString);
                    _sqlConnection.Open();
                    _isConnect = true;
                }
                
            //    if (_isConnect)
            //    {
            //        _sqlConnection.Close();
            //        _isConnect = false;
            //    }
            //    // _connectionString = ConfigurationManager.ConnectionStrings["myConnect"].ConnectionString;
            //    _connectionString = "server=" + DungChung.Bien.TenServer + ";database=" + DungChung.Bien.TenCSDL + ";uid=" + DungChung.Bien.accountsql + ";password=" + DungChung.Bien.passql + ";";
            //    _sqlConnection = new SqlConnection(_connectionString);

            //    _sqlConnection.Open();
            //    _isConnect = true;


            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
        public void Close()
        {
            try
            {
                _sqlConnection?.Close();
                _isConnect = false;
            }
            catch
            {
            }
        }
        #endregion

        #region add mảng tham số
        public void DefineSqlParameter(SqlCommand sqlCommand, string[] strParameters, object[] oValues, SqlDbType[] sqlDbType)
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
        #endregion

        public void LoadDataInToLookup(string strQuery, CommandType commandType, LookUpEdit lup, string displayText, string value)
        {

            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandText = strQuery;
            _sqlCommand.CommandType = commandType;
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.ExecuteNonQuery();

            DataTable bangdl = new DataTable("NewTable");
            SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);
            da.Fill(bangdl);

            //Hiển thị
            lup.Properties.DataSource = bangdl;
            lup.Properties.DisplayMember = displayText;
            lup.Properties.ValueMember = value;

        }

        public void LoadDataInToLookupAll(string strQuery, CommandType commandType, LookUpEdit lup, string displayText, string value, string addText, object addValue)
        {

            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandText = strQuery;
            _sqlCommand.CommandType = commandType;
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.ExecuteNonQuery();

            DataTable bangdl = new DataTable("NewTable");
            SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);
            da.Fill(bangdl);
            DataRow dr = bangdl.NewRow();
            dr[value] = addValue;
            dr[displayText] = addText;
            bangdl.Rows.InsertAt(dr, 0);
            //Hiển thị
            lup.Properties.DataSource = bangdl;
            lup.Properties.DisplayMember = displayText;
            lup.Properties.ValueMember = value;
        }

        public void LoadDataInToResLookup(string strQuery, CommandType commandType, RepositoryItemLookUpEdit lup, string displayText, string value)
        {

            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandText = strQuery;
            _sqlCommand.CommandType = commandType;
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.ExecuteNonQuery();

            DataTable bangdl = new DataTable("NewTable");
            SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);
            da.Fill(bangdl);

            //Hiển thị
            lup.DataSource = bangdl;
            lup.DisplayMember = displayText;
            lup.ValueMember = value;

        }

        public void LoadDataInToResLookupAll(string strQuery, CommandType commandType, RepositoryItemLookUpEdit lup, string displayText, string value, string addText, object addValue)
        {

            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandText = strQuery;
            _sqlCommand.CommandType = commandType;
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.ExecuteNonQuery();

            DataTable bangdl = new DataTable("NewTable");
            SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);
            da.Fill(bangdl);
            DataRow dr = bangdl.NewRow();
            dr[value] = addValue;
            dr[displayText] = addText;
            bangdl.Rows.InsertAt(dr, 0);
            //Hiển thị
            lup.DataSource = bangdl;
            lup.DisplayMember = displayText;
            lup.ValueMember = value;
        }
        #region hàm thực thi Không có tham số truyền vào, không có giá trị trả về
        public void ExecuteNonQuery(string strQuery, CommandType commandType)
        {
            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandText = strQuery;
            _sqlCommand.CommandType = commandType;
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.ExecuteNonQuery();

        }
        #endregion

        #region Hàm thực thi Có tham số truyền vào, không có giá trị trả về
        public void ExecuteNonQuery(string strQuery, CommandType commandType, string[] strParameters, object[] oValues, SqlDbType[] sqlDbType)
        {
            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandText = strQuery;
            _sqlCommand.CommandType = commandType;
            _sqlCommand.Connection = _sqlConnection;
            DefineSqlParameter(_sqlCommand, strParameters, oValues, sqlDbType);
            _sqlCommand.ExecuteNonQuery();

        }
        #endregion

        #region Hàm không có tham số truyền vào, Có giá trị trả về
        public int ExecuteNonQuery(string strQuery, string stroutpar, CommandType commandType)
        {
            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandText = strQuery;
            _sqlCommand.CommandType = commandType;
            _sqlCommand.Connection = _sqlConnection;

            // add out paramet
            SqlParameter pOutpara = _sqlCommand.Parameters.Add(stroutpar, SqlDbType.Int, 4);
            pOutpara.Direction = ParameterDirection.Output;
            // add normal paramet           
            _sqlCommand.ExecuteNonQuery();

            //return value
            int n_outvalue = (int)pOutpara.Value;
            return n_outvalue;
        }
        #endregion

        #region Hàm thực thi có tham số truyền vào, có giá trị trả về
        //public int ExecuteNonQuery(string strQuery, string stroutpar, CommandType commandType, string[] strParameters, object[] oValues, SqlDbType[] sqlDbType)
        //{
        //    _sqlCommand = new SqlCommand();
        //    _sqlCommand.CommandText = strQuery;
        //    _sqlCommand.CommandType = commandType;
        //    _sqlCommand.Connection = _sqlConnection;

        //    // add out paramet
        //    SqlParameter pOutpara = _sqlCommand.Parameters.Add(stroutpar, SqlDbType.Int, 4);
        //    pOutpara.Direction = ParameterDirection.Output;

        //    // add normal paramet
        //    DefineSqlParameter(_sqlCommand, strParameters, oValues, sqlDbType);
        //    _sqlCommand.ExecuteNonQuery();

        //    //return value
        //    int n_outvalue = (int)pOutpara.Value;
        //    return n_outvalue;
        //}
        public double ExecuteNonQuery(string strQuery, string stroutpar, CommandType commandType, string[] strParameters, object[] oValues, SqlDbType[] sqlDbType)
        {
            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandText = strQuery;
            _sqlCommand.CommandType = commandType;
            _sqlCommand.Connection = _sqlConnection;

            // add out paramet
            SqlParameter pOutpara = _sqlCommand.Parameters.Add(stroutpar, SqlDbType.Float, 10);
            pOutpara.Direction = ParameterDirection.Output;

            // add normal paramet
            DefineSqlParameter(_sqlCommand, strParameters, oValues, sqlDbType);
            _sqlCommand.ExecuteNonQuery();

            //return value
            double n_outvalue = (double)pOutpara.Value;
            return n_outvalue;
        }
        #endregion
        public SqlDataReader ExecuteReader(string strQuery, CommandType commandType, string[] strParameters, object[] oValues, SqlDbType[] sqlDbType)
        {
            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandText = strQuery;
            _sqlCommand.CommandType = commandType;
            _sqlCommand.Connection = _sqlConnection;
            DefineSqlParameter(_sqlCommand, strParameters, oValues, sqlDbType);
            SqlDataReader drsql = _sqlCommand.ExecuteReader();
            return drsql;
        }

        #region View Không có para, truyền thẳng câu lệnh sql
        public DataTable FillDatatable(string strSQL, CommandType commandType)
        {
            DataTable dtTble = new DataTable();
            try
            {
                _sqlCommand = new SqlCommand("SET ARITHABORT ON", _sqlConnection);
                _sqlCommand.CommandText = strSQL;
                _sqlCommand.CommandType = commandType;
            //    _sqlCommand = new SqlCommand();
            //    _sqlCommand.CommandText = strSQL;
            //    _sqlCommand.CommandType = commandType;
            //    _sqlCommand.Connection = _sqlConnection;
                _sqlCommand.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);
                da.Fill(dtTble);
                da.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fill Dtb Error:" + ex.Message);
            }
            return dtTble;
        }
        #endregion

        #region View có para
        public DataTable FillDatatable(string strSQL, CommandType commandType, string[] strParameters, object[] oValues, SqlDbType[] sqlDbType)
        {
            DataTable dtTble = new DataTable();
            try
            {
                _sqlCommand = new SqlCommand("SET ARITHABORT ON", _sqlConnection);
                _sqlCommand.CommandText = strSQL;
                _sqlCommand.CommandType = commandType;
                //_sqlCommand.CommandTimeout = 120;

                //_sqlCommand = new SqlCommand();
                //_sqlCommand.CommandText = strSQL;
                //_sqlCommand.CommandType = commandType;
                //_sqlCommand.Connection = _sqlConnection;
                DefineSqlParameter(_sqlCommand, strParameters, oValues, sqlDbType);
                //_sqlCommand.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(_sqlCommand);
                da.Fill(dtTble);
                da.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fill Dtb Error:" + ex.Message);
            }
            return dtTble;
        }

        internal SqlDataReader ExecuteReader(string strQuery, CommandType commandType)
        {
            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandText = strQuery;
            _sqlCommand.CommandType = commandType;
            _sqlCommand.Connection = _sqlConnection;
            SqlDataReader drsql = _sqlCommand.ExecuteReader();
            return drsql;
        }
        #endregion

       
    }
}
