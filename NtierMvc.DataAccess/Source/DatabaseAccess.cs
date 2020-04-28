using NtierMvc.Common;
using NtierMvc.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace NtierMvc.DataAccess.Source
{
    public class DatabaseAccess : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private SqlConnection _conn;
        private DataTable dt;
        //private DataHandler _dataHandler;
        //private ConfigurationHandler _configurationHandler;
        //private DbProviderFactory _dbProviderFactory;
        //private string _connectionString;
        //private string _connectionProvider;
        //private int _errorCode, _rowsAffected;
        private bool _bDisposed;

        #endregion

        #region Class Methods

        public DatabaseAccess()
        {
            _loggingHandler = new LoggingHandler();
            _conn = new SqlConnection();
            dt = new DataTable();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool bDisposing)
        {
            // Check to see if Dispose has already been called.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Dispose managed resources.
                    _loggingHandler = null;
                    //_configurationHandler = null;
                    //_dataHandler = null;
                    //_dbProviderFactory = null;
                    _conn = null;
                }
            }
            _bDisposed = true;
        }
        //No more using 

        string _outErr = string.Empty;

        public DataTable GetDataTable(string spName)
        {
            // Connection.Singleton.SqlConnetionFactory;// GetDbConnection();
            using (_conn = Connection.Singleton.SqlConnetionFactory)
            {
                try
                {
                    using (var cmd = _conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 600;
                        _conn.Open();

                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dt.Load(dr);
                        //SqlDataAdapter da = new SqlDataAdapter(cmd);
                        //da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    //Log exception error
                    _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                    //Bubble error to caller and encapsulate Exception object
                    throw new Exception("GetDataTable::Error occured.", ex);
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                }
            }
            return dt;
        }
        public DataTable GetDataTable(string spName, Dictionary<string, object> parms)
        {
            // Connection.Singleton.SqlConnetionFactory;// GetDbConnection();
            using (_conn = Connection.Singleton.SqlConnetionFactory)
            {
                try
                {

                    using (var cmd = _conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 10000;

                        foreach (var o in parms)
                        {
                            if (o.Value != null)
                            {
                                cmd.Parameters.AddWithValue(o.Key, o.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(o.Key, DBNull.Value);
                            }
                        }
                        _conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dt.Load(dr);
                    }
                }
                catch (Exception ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();

                }
            }
            return dt;
        }
        public object ExecuteScalar(string spName, Dictionary<string, object> parms)
        {
            object obj = new object();
            // Connection.Singleton.SqlConnetionFactory;// GetDbConnection();
            using (_conn = Connection.Singleton.SqlConnetionFactory)
            {
                try
                {

                    using (var cmd = _conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 10000;

                        foreach (var o in parms)
                        {
                            if (o.Value != null)
                            {
                                cmd.Parameters.AddWithValue(o.Key, o.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(o.Key, DBNull.Value);
                            }
                        }
                        _conn.Open();
                        obj = cmd.ExecuteScalar();

                    }
                }
                catch (Exception ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();

                }
            }
            return obj;
        }

        public object ExecuteScalar(string spName)
        {
            object obj = new object();
            // Connection.Singleton.SqlConnetionFactory;// GetDbConnection();
            using (_conn = Connection.Singleton.SqlConnetionFactory)
            {
                try
                {

                    using (var cmd = _conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 10000;
                        _conn.Open();
                        obj = cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();

                }
            }
            return obj;
        }
        public DataTable GetDataTable2(string spName, Dictionary<string, object> parms)
        {
            // Connection.Singleton.SqlConnetionFactory;// GetDbConnection();
            using (_conn = Connection2.Singleton2.SqlConnetionFactory2)
            {
                try
                {

                    using (var cmd = _conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 10000;

                        foreach (var o in parms)
                        {
                            if (o.Value != null)
                            {
                                cmd.Parameters.AddWithValue(o.Key, o.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(o.Key, DBNull.Value);
                            }
                        }
                        _conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dt.Load(dr);
                    }
                }
                catch (Exception ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();

                }
            }
            return dt;
        }
        public DataTable GetDataTable(string spName, string parms, string parmValues)
        {
            // Connection.Singleton.SqlConnetionFactory;// GetDbConnection();
            using (_conn = Connection.Singleton.SqlConnetionFactory)
            {


                try
                {

                    using (var cmd = _conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 10000;
                        if (!string.IsNullOrEmpty(parms) && !string.IsNullOrEmpty(parmValues))
                        {
                            if (parms.Contains("$"))
                            {
                                string[] prams = parms.Split('$');
                                string[] parmvalues = parmValues.Split('$');
                                for (int i = 0; i < prams.Length; i++)
                                {
                                    var parameter = prams[i];
                                    for (int j = 0; j < parmvalues.Length; j++)
                                    {
                                        var parametervalue = parmvalues[i];
                                        if (!string.IsNullOrEmpty(parametervalue))
                                            cmd.Parameters.AddWithValue(parameter, parametervalue.Trim());
                                        else
                                            cmd.Parameters.AddWithValue(parameter, DBNull.Value);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 1000;
                                cmd.Parameters.AddWithValue(parms, parmValues.Trim());
                                // cmd.ExecuteNonQuery();
                            }
                        }

                        //SqlDataAdapter da = new SqlDataAdapter(cmd);
                        //da.Fill(dt);
                        _conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        dt.Load(dr);

                    }
                }
                catch (Exception ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();

                }
            }
            return dt;
        }
        public int ExecuteNonQuery(string spName, Dictionary<string, object> parms)
        {
            // Connection.Singleton.SqlConnetionFactory;// GetDbConnection();
            int a = 0;
            using (_conn = Connection.Singleton.SqlConnetionFactory)
            {
                try
                {

                    using (var cmd = _conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 10000;
                        foreach (var o in parms)
                        {
                            if (o.Value != null)
                            {
                                cmd.Parameters.AddWithValue(o.Key, o.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(o.Key, DBNull.Value);
                            }
                        }

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1000;

                        _conn.Open();
                        a = (int)cmd.ExecuteNonQuery();
                        //return a;
                    }
                }
                catch (Exception ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDBInnerException(ex);
                    a = -10;
                    throw ex;
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();
                }
            }
            return a;
        }
        public int ExecuteNonQuery(string spName, Dictionary<string, object> parms, string outParm, out string outputValue)
        {
            // Connection.Singleton.SqlConnetionFactory;// GetDbConnection();
            int a = 0;
            string outputResultValue = "";
            using (_conn = Connection.Singleton.SqlConnetionFactory)
            {
                try
                {

                    using (var cmd = _conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 10000;
                        foreach (var o in parms)
                        {
                            if (o.Value != null)
                            {
                                cmd.Parameters.AddWithValue(o.Key, o.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(o.Key, DBNull.Value);
                            }
                        }

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1000;

                        var outputIdParam = new SqlParameter(outParm, SqlDbType.NVarChar)
                        {
                            Direction = ParameterDirection.Output,
                            Size = 150
                        };

                        cmd.Parameters.Add(outputIdParam);

                        _conn.Open();
                        a = (int)cmd.ExecuteNonQuery();

                        outputResultValue = $"{outputIdParam.Value}";
                        //return a;

                    }
                }
                catch (Exception ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                    a = -10;
                    throw ex;
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();
                }
            }
            outputValue = outputResultValue;
            return a;
        }
        public int ExecuteNonQuery2(string spName, Dictionary<string, object> parms, string outParm, out string outputValue)
        {
            // Connection.Singleton.SqlConnetionFactory;// GetDbConnection();
            int a = 0;
            string outputResultValue = "";
            using (_conn = Connection2.Singleton2.SqlConnetionFactory2)
            //using ( _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ERPUIDDBConnection1"].ConnectionString))
            {
                try
                {

                    using (var cmd = _conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 10000;
                        foreach (var o in parms)
                        {
                            if (o.Value != null)
                            {
                                cmd.Parameters.AddWithValue(o.Key, o.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(o.Key, DBNull.Value);
                            }
                        }

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1000;

                        var outputIdParam = new SqlParameter(outParm, SqlDbType.NVarChar)
                        {
                            Direction = ParameterDirection.Output,
                            Size = 150
                        };

                        cmd.Parameters.Add(outputIdParam);

                        _conn.Open();
                        a = (int)cmd.ExecuteNonQuery();

                        outputResultValue = $"{outputIdParam.Value}";
                        //return a;
                    }
                }
                catch (Exception ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                    a = -10;
                    throw ex;
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();
                }
            }
            outputValue = outputResultValue;
            return a;
        }

        public int ExecuteNonQuery3(string spName, Dictionary<string, object> parms, string outParm, out int outputValue)
        {
            // Connection.Singleton.SqlConnetionFactory;// GetDbConnection();
            int a = 0;
            int outputResultValue = 0;
            using (_conn = Connection.Singleton.SqlConnetionFactory)
            //using (SqlConnection _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ERPUIDDBConnection1"].ConnectionString))
            {
                try
                {
                    using (var cmd = _conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 10000;
                        foreach (var o in parms)
                        {
                            if (o.Value != null)
                            {
                                cmd.Parameters.AddWithValue(o.Key, o.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(o.Key, DBNull.Value);
                            }
                        }

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1000;

                        var outputIdParam = new SqlParameter(outParm, SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output,
                            //Size = 150
                        };

                        cmd.Parameters.Add(outputIdParam);

                        _conn.Open();
                        a = (int)cmd.ExecuteNonQuery();

                        outputResultValue = Convert.ToInt32(outputIdParam.Value);
                        //return a;
                    }
                }
                catch (Exception ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                    a = -10;
                    throw ex;
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();
                }
            }
            outputValue = outputResultValue;
            return a;
        }
        public int ExecuteNonQuery(string spName, Dictionary<string, object> parms, List<string> outParmValueList, out List<string> outputResultList)
        {
            // Connection.Singleton.SqlConnetionFactory;// GetDbConnection();
            int a = 0;
            outputResultList = new List<string>();
            using (_conn = Connection.Singleton.SqlConnetionFactory)
            {
                try
                {

                    using (var cmd = _conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 10000;
                        foreach (var o in parms)
                        {
                            if (o.Value != null)
                            {
                                cmd.Parameters.AddWithValue(o.Key, o.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(o.Key, DBNull.Value);
                            }
                        }

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1000;

                        foreach (var outParm in outParmValueList)
                        {
                            if (!string.IsNullOrEmpty(outParm))
                            {
                                var outputIdParam = new SqlParameter(outParm, SqlDbType.NVarChar, 200)
                                {
                                    Direction = ParameterDirection.Output,
                                };
                                cmd.Parameters.Add(outputIdParam);
                            }
                        }

                        _conn.Open();
                        a = (int)cmd.ExecuteNonQuery();

                        foreach (var outParm in outParmValueList)
                        {
                            if (!string.IsNullOrEmpty(outParm))
                            {
                                outputResultList.Add($"{outParm}");
                            }
                        }

                        //outputResultValue = $"{outputIdParam.Value}";
                        //return a;
                    }
                }
                catch (Exception ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                    a = -10;
                    throw ex;

                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();
                }
            }

            return a;
        }
        public DataSet GetDataSet(string spName, Dictionary<string, object> parms)
        {
            // Connection.Singleton.SqlConnetionFactory;// GetDbConnection();
            DataSet ds = new DataSet();
            using (_conn = Connection.Singleton.SqlConnetionFactory)
            {
                try
                {
                    using (var cmd = _conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 10000;

                        foreach (var o in parms)
                        {
                            if (o.Value != null)
                            {
                                cmd.Parameters.AddWithValue(o.Key, o.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(o.Key, DBNull.Value);
                            }
                        }
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        //_conn.Open();
                        //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        //ds.Load(dr);
                    }
                }
                catch (Exception ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                    throw ex;
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();
                }
            }
            return ds;
        }
        public string InsertBulkData(string sprocName, DataTable CandiadteMarkDetails, string p1)
        {
            string ValResult = "Failed";
            using (_conn = Connection.Singleton.SqlConnetionFactory)
            {
                using (var cmd = new SqlCommand(sprocName, _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(p1, CandiadteMarkDetails);
                    cmd.Parameters[0].SqlDbType = SqlDbType.Structured;
                    try
                    {
                        var da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (SqlException ex)
                    {
                        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                    }
                    catch (Exception ex)
                    {
                        NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                        throw ex;
                    }
                }
            }
            if (dt.Rows.Count > 0)
            {
                ValResult = string.IsNullOrEmpty(dt.Rows[0]["success"].ToString()) ? "Failed" : (string)dt.Rows[0]["success"];
            }
            return ValResult;

        }

        public int ExecuteNonQuery(string spName, Dictionary<string, object> parms, DataTable dt, string dtParameterName)
        {
            // Connection.Singleton.SqlConnetionFactory;// GetDbConnection();
            int a = 0;
            using (_conn = Connection.Singleton.SqlConnetionFactory)
            {
                try
                {

                    using (var cmd = _conn.CreateCommand())
                    {
                        cmd.CommandText = spName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 10000;
                        foreach (var o in parms)
                        {
                            if (o.Value != null)
                            {
                                cmd.Parameters.AddWithValue(o.Key, o.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue(o.Key, DBNull.Value);
                            }
                        }

                        SqlParameter tableParameter = new SqlParameter(dtParameterName, dt);
                        tableParameter.SqlDbType = SqlDbType.Structured;
                        cmd.Parameters.Add(tableParameter);

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1000;

                        _conn.Open();
                        a = (int)cmd.ExecuteNonQuery();
                        //return a;
                    }
                }
                catch (Exception ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                    a = -10;
                    throw ex;
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                        _conn.Close();
                }
            }
            return a;
        }

        public string BulkUpload(DataTable source, string destination)
        {
            string ValResult = "Failed";
            using (_conn = Connection.Singleton.SqlConnetionFactory)
            {
                try
                {
                    SqlBulkCopy oSqlBulkCopy = new SqlBulkCopy(_conn, SqlBulkCopyOptions.TableLock, null);
                    oSqlBulkCopy.DestinationTableName = destination;
                    foreach (DataColumn column in source.Columns)
                    {
                        oSqlBulkCopy.ColumnMappings.Add(column.ToString(), column.ToString());
                    }
                    _conn.Open();
                    oSqlBulkCopy.BulkCopyTimeout = 300;
                    oSqlBulkCopy.BatchSize = source.Rows.Count;
                    oSqlBulkCopy.WriteToServer(source);
                    oSqlBulkCopy.Close();
                    _conn.Close();

                    ValResult = "Inserted Successfully";
                }
                catch (SqlException ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                }
                catch (Exception ex)
                {
                    NtierMvc.DataAccess.ExceptionLogging.SendExcepToDB(ex);
                    throw ex;
                }
                //}
            }
            //if (dt.Rows.Count > 0)
            //{
            //    ValResult = string.IsNullOrEmpty(dt.Rows[0]["success"].ToString()) ? "Failed" : (string)dt.Rows[0]["success"];
            //}

            return ValResult;

        }
        #endregion Class Methods

    }
}
