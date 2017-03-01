using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace RPoney.Data
{
    public static class ModelConvertHelper<T> where T : new()
    {
        // Methods
        private static Type GetPropertyType(Type pType)
        {
            Type[] genericArguments = pType.GetGenericArguments();
            if (genericArguments.Length > 0)
            {
                return genericArguments[0];
            }
            return pType;
        }

        public static T ToModel(DataRow dr)
        {
            var name = "";
            T local = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
            PropertyInfo[] properties = local.GetType().GetProperties();
            DataTable table = dr.Table;
            foreach (PropertyInfo info in properties)
            {
                name = info.Name;
                var propertyType = GetPropertyType(info.PropertyType);
                if (table.Columns.Contains(name) && info.CanWrite)
                {
                    var obj2 = dr[name];
                    if ((obj2 == null) || ((obj2 == DBNull.Value) && !propertyType.Equals(typeof (string)))) continue;
                    if ((obj2 == DBNull.Value) && propertyType.Equals(typeof(string)))
                    {
                        obj2 = string.Empty;
                    }
                    if (propertyType.IsEnum)
                    {
                        info.SetValue(local, Enum.Parse(propertyType, obj2.ToString().Trim(), true), null);
                    }
                    else
                    {
                        info.SetValue(local, Convert.ChangeType(obj2, propertyType), null);
                    }
                }
            }
            return local;
        }

        public static T ToModel(DataTable dt)
        {
            if ((dt != null) && (dt.Rows.Count > 0))
            {
                return ToModel(dt.Rows[0]);
            }
            return default(T);
        }

        public static T ToModel(IDataRecord dr)
        {
            T local = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
            var properties = local.GetType().GetProperties();
            var fieldCount = dr.FieldCount;
            var dictionary = new Dictionary<string, object>();
            for (int i = 0; i < fieldCount; i++)
            {
                var str = dr.GetName(i).ToLower();
                dictionary[str] = dr[i];
            }
            foreach (PropertyInfo info in properties)
            {
                var key = info.Name.ToLower();
                if (dictionary.ContainsKey(key) && info.CanWrite)
                {
                    object obj2 = dictionary[key];
                    if (obj2 != DBNull.Value)
                    {
                        Type propertyType = GetPropertyType(info.PropertyType);
                        if (propertyType.IsEnum)
                        {
                            info.SetValue(local, Enum.Parse(propertyType, obj2.ToString().Trim(), true), null);
                        }
                        else
                        {
                            info.SetValue(local, Convert.ChangeType(obj2, propertyType), null);
                        }
                    }
                }
            }
            return local;
        }

        public static IList<T> ToModels(DataTable dt)
        {
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                return new List<T>();
            }
            IList<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T local2 = default(T);
                T local = (local2 == null) ? Activator.CreateInstance<T>() : default(T);
                foreach (var info in local.GetType().GetProperties())
                {
                    Type propertyType = GetPropertyType(info.PropertyType);
                    string name = info.Name;
                    if (dt.Columns.Contains(name) && info.CanWrite)
                    {
                        object obj2 = row[name];
                        if ((obj2 != null) && ((obj2 != DBNull.Value) || propertyType.Equals(typeof(string))))
                        {
                            if ((obj2 == DBNull.Value) && propertyType.Equals(typeof(string)))
                            {
                                obj2 = string.Empty;
                            }
                            if (propertyType.IsEnum)
                            {
                                info.SetValue(local, Enum.Parse(propertyType, obj2.ToString().Trim(), true), null);
                            }
                            else
                            {
                                info.SetValue(local, Convert.ChangeType(obj2, propertyType), null);
                            }
                        }
                    }
                }
                list.Add(local);
            }
            return list;
        }

        public static IList<T> ToModels(IDataReader dr)
        {
            IList<T> list = new List<T>();
            while (dr.Read())
            {
                list.Add(ToModel(dr));
            }
            return list;
        }
    }


}
