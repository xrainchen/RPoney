using System;

namespace RPoney.Data.Pager
{
    /// <summary>
    /// 简单分页
    /// </summary>
    public class SimplePager
    {
        // Methods
        public SimplePager(string tableName)
        {
            TableName = tableName;
        }

        private string _FormatWhere(string where)
        {
            var str = where;
            if (string.IsNullOrEmpty(str))
            {
                return " where 1=1 ";
            }
            str = where.Trim();
            if (string.IsNullOrEmpty(str))
            {
                return " where 1=1 ";
            }
            if (str.StartsWith("and ", StringComparison.CurrentCultureIgnoreCase))
            {
                return (" where 1=1 " + where);
            }
            if (!str.StartsWith("where ", StringComparison.CurrentCultureIgnoreCase))
            {
                return (" where " + where);
            }
            return str;
        }

        public string GetCountSQL(string where)
        {
            var str = _FormatWhere(where);
            return string.Format("select count(1) from {0} {1}", new object[] { this.TableName, str });
        }

        public string GetPagerSQL(int pageindex, int pagesize, string fields, string where, string orderBy)
        {
            var str = _FormatWhere(where);
            if (pagesize <= 0)
            {
                return $"SELECT {fields} FROM {TableName} {where} Order By {orderBy}";
            }
            if (pagesize == 1)
            {
                return $"SELECT top {pagesize} {fields} FROM {TableName} {where} Order By {orderBy}";
            }
            var strArray = new string[] { (((pageindex - 1) * pagesize) + 1).ToString(), (pageindex * pagesize).ToString(), this.TableName, str, orderBy, fields };
            var format = "\r\n                With tmp_tb as(select top {1} ROW_NUMBER() Over(Order By {4} ) as wsj_rb, {5} from {2} {3}  )\r\n                select * from tmp_tb where wsj_rb between {0} and {1}\r\n            ";
            return string.Format(format,strArray);
        }

        public string TableName { get; set; }
    }
}
