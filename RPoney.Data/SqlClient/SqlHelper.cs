using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using RPoney.Data.Contract;

namespace RPoney.Data.SqlClient
{
    public class SqlHelper : IDbHelper
    {
        private bool _autoClose;
        private readonly bool _canSetHandClose;
        private TimeSpan _commandTimeout;
        private SqlConnection _conn;
        private readonly string _connString;
        private SqlTransaction _dbTran;
        private readonly bool _denyStar;
        private bool _isErr;
        private bool _openTranWhenHandClose;
        private const string DefaultoutputParName = "@returnvalue";
        private const string ReturnvalueParName = "@__returnvalue";

        public SqlHelper(string connString, bool canSetHandClose)
        {
            _commandTimeout = TimeSpan.Zero;
            _canSetHandClose = true;
            _connString = connString;
           _autoClose = true;
            _canSetHandClose = canSetHandClose;
        }

        public SqlHelper(string connString, bool canSetHandClose, bool denyStar)
        {
            _commandTimeout = TimeSpan.Zero;
            _canSetHandClose = true;
            _connString = connString;
            _autoClose = true;
            _canSetHandClose = canSetHandClose;
           _denyStar = denyStar;
        }

        private void CloseTransaction(ECloseTransactionType closeTransactionType)
        {
            if (_dbTran != null)
            {
                if (_dbTran.Connection == null)
                {
                    _dbTran = null;
                }
                else
                {
                    var flag = !_isErr;
                    if (closeTransactionType == ECloseTransactionType.RollBack)
                    {
                        flag = false;
                    }
                    else if (closeTransactionType == ECloseTransactionType.Commit)
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        _dbTran.Commit();
                    }
                    else
                    {
                        _dbTran.Rollback();
                    }
                    _dbTran.Dispose();
                    _dbTran = null;
                }
            }
        }

        private SqlCommand CreateCommand(string cmdText, CommandType cmdType, bool addReturnValuePar, bool addDefalutOutPar, params IDataParameter[] sqlpars)
        {
            if ((cmdType == CommandType.Text) && !ZSQLCStatic.CheckText(cmdText, this._denyStar))
            {
                throw new SqlException("非法语句！", cmdText, new IDataParameter[0]);
            }
            var command = new SqlCommand(cmdText, _conn);
            if (_commandTimeout != TimeSpan.Zero)
            {
                command.CommandTimeout = (int)_commandTimeout.TotalSeconds;
            }
            if ((_dbTran != null) && (_dbTran.Connection != null))
            {
                command.Transaction = _dbTran;
            }
            command.CommandType = cmdType;
            if (addReturnValuePar)
            {
                var parameter = new SqlParameter("@__returnvalue", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                command.Parameters.Add(parameter);
            }
            if (addDefalutOutPar)
            {
                var parameter2 = new SqlParameter("@returnvalue", SqlDbType.VarChar, 0x100)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(parameter2);
            }
            if (sqlpars != null)
            {
                foreach (var parameter3 in sqlpars)
                {
                    if (parameter3 != null)
                    {
                        if (parameter3.Value == null)
                        {
                            parameter3.Value = DBNull.Value;
                        }
                        if (!command.Parameters.Contains(parameter3.ParameterName))
                        {
                            command.Parameters.Add(parameter3);
                        }
                    }
                }
            }
            return command;
        }

        public void EndConnection()
        {
            EndConnection(ECloseTransactionType.Auto);
        }

        public void EndConnection(ECloseTransactionType closeTransactionType)
        {
            if (_conn != null)
            {
                CloseTransaction(closeTransactionType);
                if (_conn.State != ConnectionState.Closed)
                {
                   _conn.Close();
                }
                _conn.Dispose();
                _conn = null;
                _autoClose = true;
            }
        }

        public DataSet ExecuteFillDataSet(string commandText, params IDataParameter[] dataParameters)
        {
            return ExecuteFillDataSet(commandText, CommandType.Text, false, dataParameters);
        }

        public DataSet ExecuteFillDataSet(string commandText, CommandType cmdType, params IDataParameter[] dataParameters)
        {
            return ExecuteFillDataSet(commandText, cmdType, false, dataParameters);
        }

        public DataSet ExecuteFillDataSet(string commandText, CommandType cmdType, bool withTransaction, params IDataParameter[] dataParameters)
        {
            DataSet set2;
            TryStarConnection(withTransaction);
            var selectCommand = this.CreateCommand(commandText, cmdType, false, false, dataParameters);
            try
            {
                var dataSet = new DataSet();
                new SqlDataAdapter(selectCommand).Fill(dataSet);
                selectCommand.Parameters.Clear();
                set2 = dataSet;
            }
            catch (Exception exception)
            {
                _isErr = true;
                throw new SqlException(exception, commandText, dataParameters);
            }
            finally
            {
                TryEndConnection();
            }
            return set2;
        }

        public DataTable ExecuteFillDataTable(string commandText, params IDataParameter[] dataParameters)
        {
            return ExecuteFillDataTable(commandText, CommandType.Text, false, dataParameters);
        }

        public DataTable ExecuteFillDataTable(string commandText, CommandType cmdType, params IDataParameter[] dataParameters)
        {
            return ExecuteFillDataTable(commandText, cmdType, false, dataParameters);
        }

        public DataTable ExecuteFillDataTable(string commandText, CommandType cmdType, bool withTransaction, params IDataParameter[] dataParameters)
        {
            DataTable table2;
            TryStarConnection(withTransaction);
            var selectCommand = this.CreateCommand(commandText, cmdType, false, false, dataParameters);
            try
            {
                var dataTable = new DataTable();
                new SqlDataAdapter(selectCommand).Fill(dataTable);
                selectCommand.Parameters.Clear();
                table2 = dataTable;
            }
            catch (Exception exception)
            {
                _isErr = true;
                throw exception;
            }
            finally
            {
               TryEndConnection();
            }
            return table2;
        }

        public int ExecuteNonQuery(string commandText, params IDataParameter[] dataParameters)
        {
            return ExecuteNonQuery(commandText, CommandType.Text, false, dataParameters);
        }

        public int ExecuteNonQuery(string commandText, CommandType cmdType, params IDataParameter[] dataParameters)
        {
            return ExecuteNonQuery(commandText, cmdType, false, dataParameters);
        }

        public int ExecuteNonQuery(string commandText, CommandType cmdType, bool withTransaction, params IDataParameter[] dataParameters)
        {
            int num2;
            TryStarConnection(withTransaction);
            var command = this.CreateCommand(commandText, cmdType, false, false, dataParameters);
            try
            {
                var num = command.ExecuteNonQuery();
                command.Parameters.Clear();
                num2 = num;
            }
            catch (Exception exception)
            {
                _isErr = true;
                throw new SqlException(exception, commandText, dataParameters);
            }
            finally
            {
                TryEndConnection();
            }
            return num2;
        }

        public CommandResult ExecuteProc(string commandText, bool useDefalutOutPar, params IDataParameter[] dataParameters)
        {
            return ExecuteProc(commandText, useDefalutOutPar, false, false, dataParameters);
        }

        public CommandResult ExecuteProc(string commandText, bool useDefalutOutPar, bool isProcContainTran, bool withTransaction, params IDataParameter[] dataParameters)
        {
            CommandResult result2;
            var withSqlTransaction = withTransaction && !isProcContainTran;
            TryStarConnection(withSqlTransaction);
            var command = this.CreateCommand(commandText, CommandType.StoredProcedure, true, useDefalutOutPar, dataParameters);
            try
            {
                var result = new CommandResult
                {
                    NonQuery = command.ExecuteNonQuery(),
                    OutPutValue = new Dictionary<string, object>()
                };
                foreach (SqlParameter parameter in command.Parameters)
                {
                    if (parameter.Direction == ParameterDirection.Output)
                    {
                        result.OutPutValue[parameter.ParameterName.Substring(1)] = parameter.Value.ToString();
                    }
                }
                if (command.Parameters.Contains("@__returnvalue"))
                {
                    result.ReturnValueObj = Convert.ToInt32(command.Parameters["@__returnvalue"].Value);
                }
                command.Parameters.Clear();
                result2 = result;
            }
            catch (Exception exception)
            {
                _isErr = true;
                throw new SqlException(exception, commandText, dataParameters);
            }
            finally
            {
                TryEndConnection();
            }
            return result2;
        }

        public IDataReader ExecuteReader(string commandText, params IDataParameter[] dataParameters)
        {
            return ExecuteReader(commandText, CommandType.Text, false, dataParameters);
        }

        public IDataReader ExecuteReader(string commandText, CommandType cmdType, params IDataParameter[] dataParameters)
        {
            return ExecuteReader(commandText, cmdType, false, dataParameters);
        }

        public IDataReader ExecuteReader(string commandText, CommandType cmdType, bool withTransaction, params IDataParameter[] dataParameters)
        {
            IDataReader reader;
            TryStarConnection(withTransaction);
            var command = this.CreateCommand(commandText, cmdType, false, false, dataParameters);
            try
            {
                if (_autoClose)
                {
                    return command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                reader = command.ExecuteReader();
            }
            catch
            {
                _isErr = true;
                TryEndConnection();
                throw;
            }
            finally
            {
                command.Parameters.Clear();
            }
            return reader;
        }

        public object ExecuteScalar(string commandText, params IDataParameter[] dataParameters)
        {
            return ExecuteScalar(commandText, CommandType.Text, false, dataParameters);
        }

        public object ExecuteScalar(string commandText, CommandType cmdType, params IDataParameter[] dataParameters)
        {
            return ExecuteScalar(commandText, cmdType, false, dataParameters);
        }

        public object ExecuteScalar(string commandText, CommandType cmdType, bool withTransaction, params IDataParameter[] dataParameters)
        {
            object obj3;
            TryStarConnection(withTransaction);
            var command = this.CreateCommand(commandText, cmdType, false, false, dataParameters);
            try
            {
                var obj2 = command.ExecuteScalar();
                command.Parameters.Clear();
                obj3 = obj2;
            }
            catch (Exception exception)
            {
                _isErr = true;
                throw new SqlException(exception, commandText, dataParameters);
            }
            finally
            {
                TryEndConnection();
            }
            return obj3;
        }

        public bool Exists(string commandText, params IDataParameter[] dataParameters)
        {
            bool flag;
            var reader = this.ExecuteReader(commandText, CommandType.Text, true, dataParameters);
            try
            {
                flag = reader.Read();
            }
            catch (Exception exception)
            {
                throw new SqlException(exception, commandText, dataParameters);
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
            return flag;
        }

        public static SqlHelper FromConfig(bool canSetHandClose)
        {
            return FromConfigKey("dbCfgFileName", canSetHandClose);
        }

        public static SqlHelper FromConfigKey(string appSettingsKey, bool canSetHandClose)
        {
            var str = ConfigurationManager.AppSettings[appSettingsKey];
            if (string.IsNullOrEmpty(str))
            {
                throw new SqlException(string.Format("找不到配置项目【{0}】", appSettingsKey));
            }
            return FromFile(str, canSetHandClose);
        }

        public static SqlHelper FromConfigKey(string appSettingsKey, bool canSetHandClose, bool denyStar)
        {
            var str = ConfigurationManager.AppSettings[appSettingsKey];
            if (string.IsNullOrEmpty(str))
            {
                throw new SqlException($"找不到配置项目【{appSettingsKey}】");
            }
            return FromFile(str, canSetHandClose);
        }

        public static SqlHelper FromFile(string fileFullName, bool canSetHandClose)
        {
            return new SqlHelper(Tools.ReadConnString(fileFullName), canSetHandClose);
        }

        public static SqlHelper FromFile(string fileFullName, bool canSetHandClose, bool denyStar)
        {
            return new SqlHelper(Tools.ReadConnString(fileFullName), canSetHandClose, denyStar);
        }

        public string GetConnectionString()
        {
            return _connString;
        }

        public string GetDataBaseName()
        {
            using (SqlConnection connection = new SqlConnection(_connString))
            {
                return connection.Database;
            }
        }

        public int GetMaxId(string tableName, string fieldName)
        {
            int num2;
            var commandText = "select max([" + fieldName + "]) from [" + tableName + "]";
            try
            {
                num2 = Convert.ToInt32(ExecuteScalar(commandText, CommandType.Text, true, null) ?? 0);
            }
            catch (Exception exception)
            {
                throw new SqlException(exception, commandText, new IDataParameter[0]);
            }
            return num2;
        }

        public DateTime GetNow()
        {
            return DateTime.Parse(ExecuteScalar("select getdate()").ToString());
        }

        public bool IsExistsColumn(string tableName, string columnName)
        {
            var parameterArray2 = new IDataParameter[2];
            var parameter = new SqlParameter("tbname", SqlDbType.NVarChar, 50)
            {
                Value = tableName
            };
            parameterArray2[0] = parameter;
            var parameter2 = new SqlParameter("columnname", SqlDbType.NVarChar, 50)
            {
                Value = columnName
            };
            parameterArray2[1] = parameter2;
            var dataParameters = parameterArray2;
            return Exists("select top 1 1 from sysobjects where id = object_id(@tbname) and [name]=@columnname", dataParameters);
        }

        public bool IsExistsTable(string tableName)
        {
            var parameter = new SqlParameter("tbname", SqlDbType.NVarChar, 50)
            {
                Value = tableName
            };
            return Exists("select top 1 1 from sysobjects where id = object_id(@tbname) and OBJECTPROPERTY(id, N'IsUserTable') = 1", new IDataParameter[] { parameter });
        }

        public void SetCommandTimeOut(TimeSpan timeOutSpan)
        {
            _commandTimeout = timeOutSpan;
        }

        public void SetHandClose(bool withTransaction)
        {
            if (!_canSetHandClose)
            {
                throw new SqlException("该实例不允许设置手动关闭连接！请重新实例一个对象！");
            }
            _autoClose = false;
            _openTranWhenHandClose = withTransaction;
        }

        private void StartConnection(bool withSqlTransaction)
        {
            if (_conn == null)
            {
                _conn = new SqlConnection(_connString);
            }
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }
            if (!_autoClose)
            {
                withSqlTransaction = _openTranWhenHandClose;
            }
            if (((_dbTran == null) || (_dbTran.Connection == null)) && withSqlTransaction)
            {
                _isErr = false;
                _dbTran = _conn.BeginTransaction();
            }
        }

        public bool TryConnection()
        {
            return TryConnection(TimeSpan.FromSeconds(3.0));
        }

        public bool TryConnection(TimeSpan timeOutSpan)
        {
            var strArray = _connString.Split(';');
            var builder = new StringBuilder();
            var flag = false;
            foreach (var str in strArray)
            {
                var str2 = str;
                var index = str2.IndexOf("=", StringComparison.Ordinal);
                if ((index > -1) && str2.Substring(0, index).ToLower().Equals("connect timeout"))
                {
                    flag = true;
                    str2 = "Connect Timeout=" + timeOutSpan.Seconds;
                }
                builder.Append(str2 + ";");
            }
            if (!flag)
            {
                builder.Append("Connect Timeout=" + timeOutSpan.Seconds + ";");
            }
            var connection = new SqlConnection(builder.ToString());
            var flag2 = connection.State == ConnectionState.Open;
            try
            {
                connection.Open();
                connection.Close();
                return flag2;
            }
            catch
            {
                return false;
            }
        }

        private void TryEndConnection()
        {
            if (_autoClose)
            {
                EndConnection(ECloseTransactionType.Auto);
            }
        }

        private void TryStarConnection(bool withSqlTransaction)
        {
            StartConnection(withSqlTransaction);
        }
    }
}
