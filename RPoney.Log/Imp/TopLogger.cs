using System;
using log4net.Core;

namespace RPoney.Log.Imp
{
    internal class TopLogger : ITopLogger
    {
        // Fields
        private Type _type;

        // Methods
        public TopLogger(Type type)
        {
            this._type = type;
            this._LoggerName = "Default";
        }

        public TopLogger(Type type, string loggerName)
        {
            this._type = type;
            this._LoggerName = string.IsNullOrEmpty(loggerName) ? "Default" : loggerName;
        }

        public void Debug(string bizType, string description)
        {
            LoggerManager.Debug(bizType, description, this._type, this._LoggerName);
        }

        public void Debug(string bizType, string description, Exception exception)
        {
            LoggerManager.Debug(bizType, description, exception, this._type, this._LoggerName);
        }

        public void Error(string bizType, string description)
        {
            LoggerManager.Error(bizType, description, this._type, this._LoggerName);
        }

        public void Error(string bizType, string description, Exception exception)
        {
            LoggerManager.Error(bizType, description, exception, this._type, this._LoggerName);
        }

        public void Fatal(string bizType, string description)
        {
            LoggerManager.Fatal(bizType, description, this._type, this._LoggerName);
        }

        public void Fatal(string bizType, string description, Exception exception)
        {
            LoggerManager.Fatal(bizType, description, exception, this._type, this._LoggerName);
        }

        public void Info(string bizType, string description)
        {
            LoggerManager.Info(bizType, description, this._type, this._LoggerName);
        }

        public void Info(string bizType, string description, Exception exception)
        {
            LoggerManager.Info(bizType, description, exception, this._type, this._LoggerName);
        }

        public void Warn(string bizType, string description)
        {
            LoggerManager.Warn(bizType, description, this._type, this._LoggerName);
        }

        public void Warn(string bizType, string description, Exception exception)
        {
            LoggerManager.Warn(bizType, description, exception, this._type, this._LoggerName);
        }

        // Properties
        private string _LoggerName { get; set; }
    }



}
