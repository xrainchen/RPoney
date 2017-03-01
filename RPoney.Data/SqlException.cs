using System;
using System.Data;
using System.Text;

namespace RPoney.Data
{
    public class SqlException : Exception
    {
        // Fields
        private const string DefaultMessage = "RPoney.Data运行时出现错误";

        // Methods
        public SqlException(string message) : base(message)
        {
            this.CommandText = string.Empty;
        }

        public SqlException(Exception ex, string cmdText, params IDataParameter[] dataParameters) : base("FzCyjh.Data运行时出现错误", ex)
        {
            this.CommandText = cmdText;
            this.DataParameters = dataParameters;
        }

        public SqlException(string message, string cmdText, params IDataParameter[] dataParameters) : base(message)
        {
            this.CommandText = cmdText;
            this.DataParameters = dataParameters;
        }

        public SqlException(string message, Exception ex, string cmdText, params IDataParameter[] dataParameters) : base(message, ex)
        {
            this.CommandText = cmdText;
            this.DataParameters = dataParameters;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(this.CommandText))
            {
                builder.AppendLine("查询语句：");
                builder.AppendLine("\t" + this.CommandText);
            }
            if ((this.DataParameters != null) && (this.DataParameters.Length > 0))
            {
                builder.AppendLine("参数：");
                foreach (IDataParameter parameter in this.DataParameters)
                {
                    builder.AppendLine($"\t{parameter.ParameterName}:{parameter.Value}");
                }
            }
            builder.AppendLine(base.ToString());
            return builder.ToString();
        }

        // Properties
        public string CommandText { get; private set; }

        public IDataParameter[] DataParameters { get; private set; }
    }

}
