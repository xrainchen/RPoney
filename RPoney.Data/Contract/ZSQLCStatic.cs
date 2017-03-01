using System;

namespace RPoney.Data.Contract
{
    internal static class ZSQLCStatic
    {
        // Fields
        private static readonly char[] spliter = new char[] { ' ', '\r', '\n', '\t' };
        private const string Star = "*";

        // Methods
        private static bool CheckStart(string[] arr, int index)
        {
            if (arr[index].Equals("select", StringComparison.OrdinalIgnoreCase) && arr[index + 1].Equals("*", StringComparison.Ordinal))
            {
                throw new SqlException("不允许使用*进行查询");
            }
            return true;
        }

        public static bool CheckText(string sqltext, bool checkStar)
        {
            string[] strArray = sqltext.ToLower().Split(spliter, StringSplitOptions.RemoveEmptyEntries);
            Func<string[], int, bool> func = (arr, i) => true;
            if (checkStar)
            {
                func = new Func<string[], int, bool>(ZSQLCStatic.CheckStart);
            }
            for (int j = 0; j < (strArray.Length - 1); j++)
            {
                if (!func(strArray, j))
                {
                    return false;
                }
                if (strArray[j].Equals("delete", StringComparison.Ordinal) || strArray[j].Equals("update", StringComparison.Ordinal))
                {
                    for (int k = j + 1; k < strArray.Length; k++)
                    {
                        if (strArray[k].Equals("where", StringComparison.Ordinal) || strArray[k].Equals("on", StringComparison.Ordinal))
                        {
                            return true;
                        }
                    }
                    throw new SqlException("Update 和 Delete 语句必须包含where 或 on！");
                }
            }
            return true;
        }
    }
}
