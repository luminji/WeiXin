using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using WeiXin.Attributes;

namespace WeiXin.Models
{
    public sealed class ConvertModel
    {
        public static List<T> ConvertToList<T>(DataTable table) where T : class, IConvert, new()
        {
            List<T> result = new List<T>();
            var objType = typeof(T);
            var TmpPros = objType.GetProperties();
            List<Tuple<PropertyInfo, string>> ps = new List<Tuple<PropertyInfo, string>>();
            foreach (var p in TmpPros)
            {
                var attribute = p.GetCustomAttribute<DataFieldAttribute>();
                if (attribute != null)
                {
                    if (attribute.IsDataField)
                    {
                        ps.Add(new Tuple<PropertyInfo, string>(p, attribute.DataFieldName));
                    }
                }
            }
            foreach (DataRow row in table.Rows)
            {
                T obj = new T();
                foreach (var p in ps)
                {
                    var colunmName = p.Item2.Replace("[", string.Empty).Replace("]", string.Empty);
                    if (table.Columns.Contains(colunmName))
                    {
                        if (row[colunmName] != DBNull.Value)
                        {
                            p.Item1.SetValue(obj, row[colunmName], null);
                        }
                    }
                }
                result.Add(obj);
            }
            return result;
        }

        public static T Convert<T>(DataTable table) where T : class, IConvert, new()
        {
            if (table.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                var objType = typeof(T);
                var TmpPros = objType.GetProperties();
                List<Tuple<PropertyInfo, string>> ps = new List<Tuple<PropertyInfo, string>>();
                foreach (var p in TmpPros)
                {
                    var attribute = p.GetCustomAttribute<DataFieldAttribute>();
                    if (attribute != null)
                    {
                        if (attribute.IsDataField)
                        {
                            ps.Add(new Tuple<PropertyInfo, string>(p, attribute.DataFieldName));
                        }
                    }
                }
                var row = table.Rows[0];
                T result = new T();
                foreach (var p in ps)
                {
                    var colunmName = p.Item2.Replace("[", string.Empty).Replace("]", string.Empty);
                    if (table.Columns.Contains(colunmName))
                    {
                        if (row[colunmName] != DBNull.Value)
                        {
                            p.Item1.SetValue(result, row[colunmName], null);
                        }
                    }
                }
                return result;
            }
        }
    }
}
