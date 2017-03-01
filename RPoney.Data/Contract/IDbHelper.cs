using System;
using System.Data;

namespace RPoney.Data.Contract
{
    /// <summary>
    /// 数据库帮助接口
    /// </summary>
    public interface IDbHelper
    {
        /// <summary>
        /// 关闭连接
        /// </summary>
        void EndConnection();
        /// <summary>
        /// 关闭连接，执行事务
        /// </summary>
        /// <param name="closeTransactionType"></param>
        void EndConnection(ECloseTransactionType closeTransactionType);
        DataSet ExecuteFillDataSet(string commandText, params IDataParameter[] dataParameters);
        DataSet ExecuteFillDataSet(string commandText, CommandType cmdType, params IDataParameter[] dataParameters);
        DataSet ExecuteFillDataSet(string commandText, CommandType cmdType, bool withTransaction, params IDataParameter[] dataParameters);
        DataTable ExecuteFillDataTable(string commandText, params IDataParameter[] dataParameters);
        DataTable ExecuteFillDataTable(string commandText, CommandType cmdType, params IDataParameter[] dataParameters);
        DataTable ExecuteFillDataTable(string commandText, CommandType cmdType, bool withTransaction, params IDataParameter[] dataParameters);
        int ExecuteNonQuery(string commandText, params IDataParameter[] dataParameters);
        int ExecuteNonQuery(string commandText, CommandType cmdType, params IDataParameter[] dataParameters);
        int ExecuteNonQuery(string commandText, CommandType cmdType, bool withTransaction, params IDataParameter[] dataParameters);
        CommandResult ExecuteProc(string commandText, bool useDefalutOutPar, params IDataParameter[] dataParameters);
        CommandResult ExecuteProc(string commandText, bool useDefalutOutPar, bool isProcContainTran, bool withTransaction, params IDataParameter[] dataParameters);
        IDataReader ExecuteReader(string commandText, params IDataParameter[] dataParameters);
        IDataReader ExecuteReader(string commandText, CommandType cmdType, params IDataParameter[] dataParameters);
        IDataReader ExecuteReader(string commandText, CommandType cmdType, bool withTransaction, params IDataParameter[] dataParameters);
        object ExecuteScalar(string commandText, params IDataParameter[] dataParameters);
        object ExecuteScalar(string commandText, CommandType cmdType, params IDataParameter[] dataParameters);
        object ExecuteScalar(string commandText, CommandType cmdType, bool withTransaction, params IDataParameter[] dataParameters);
        bool Exists(string commandText, params IDataParameter[] dataParameters);
        string GetConnectionString();
        string GetDataBaseName();
        int GetMaxId(string tableName, string fieldName);
        DateTime GetNow();
        bool IsExistsColumn(string tableName, string columnName);
        bool IsExistsTable(string tableName);
        void SetCommandTimeOut(TimeSpan timeOutSpan);
        void SetHandClose(bool withTransaction);
        bool TryConnection();
        bool TryConnection(TimeSpan timeOutSpan);
    }
}
