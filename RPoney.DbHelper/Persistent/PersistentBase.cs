﻿using System.Data;
using RPoney.DbHelper.Transaction;
using RPoney.Data.Contract;

namespace RPoney.DbHelper.Persistent
{
    public abstract class PersistentBase
    {
        private ITransaction _transaction;

        protected PersistentBase()
        {
            this._transaction = null;
        }

        protected PersistentBase(ITransaction transaction)
        {
            this._transaction = null;
            this.Transaction = transaction;
        }

        public string GetDBCFileName { get; private set; }

        public ITransaction Transaction
        {
            get { return this._transaction; }

            private set
            {
                this._transaction = value;
            }
        }

        public virtual int ExecuteNonQuery(string commandText) =>
                            this._transaction.GetTransactionContext<IDbHelper>().ExecuteNonQuery(commandText, new IDataParameter[0]);

        public virtual int ExecuteNonQuery(string commandText, CommandType cmdType, params IDataParameter[] dataParameters) =>
            this._transaction.GetTransactionContext<IDbHelper>().ExecuteNonQuery(commandText, cmdType, dataParameters);

        public virtual object ExecuteScalar(string commandText, CommandType cmdType, params IDataParameter[] dataParameters) =>
            this._transaction?.GetTransactionContext<IDbHelper>().ExecuteScalar(commandText, cmdType, dataParameters);
    }
}
