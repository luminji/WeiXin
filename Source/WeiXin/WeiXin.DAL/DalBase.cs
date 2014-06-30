using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using WeiXin.Attributes;
using WeiXin.Models;
using WeiXin.Utilitys;

namespace WeiXin.DAL
{
    public abstract class DalBase<T> where T : class, IConvert, new()
    {
        public virtual bool Create(T model)
        {
            bool result = default(bool);
            var sql = "insert into {0}({1}) values({2});";
            var sqlTableName = string.Empty;
            var sqlColumnName = string.Empty;
            var sqlValues = string.Empty;
            var values = new List<SqlParameter>();
            var tmp = GetColumns(model);
            sqlTableName = tmp.Item1;
            foreach (var item in tmp.Item2)
            {
                sqlColumnName += string.Format("{0},", item.Item1);
                sqlValues += string.Format("@{0},", ReplaceTab(item.Item1));
                values.Add(new SqlParameter(string.Format("@{0}", ReplaceTab(item.Item1)), item.Item2));
            }
            if (!string.IsNullOrEmpty(sqlColumnName))
            {
                sqlColumnName = sqlColumnName.Substring(0, sqlColumnName.Length - 1);
            }
            if (!string.IsNullOrEmpty(sqlValues))
            {
                sqlValues = sqlValues.Substring(0, sqlValues.Length - 1);
            }
            sql = string.Format(sql, sqlTableName, sqlColumnName, sqlValues);
            try
            {
                result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, values.ToArray()) > 0;
            }
            catch (Exception e)
            {
                LogHelper.LogError(e);
            }
            return result;
        }

        public virtual T Read(T model)
        {
            var tmp = GetUniqueColumn(model);
            var sql = string.Format("select * from {0} where {1}=@{2};", tmp.Item1, tmp.Item2, ReplaceTab(tmp.Item2));
            try
            {
                var dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql, new SqlParameter(string.Format("@{0}", ReplaceTab(tmp.Item2)), tmp.Item3));
                return ConvertModel.Convert<T>(dt);
            }
            catch (Exception e)
            {
                LogHelper.LogError(e);
            }
            return null;
        }

        public virtual List<T> Read()
        {
            T obj = new T();
            var tableName = GetTableName(obj, obj.GetType());
            var sql = string.Format("select * from {0};", tableName);
            try
            {
                var dt = SqlHelper.ExecuteDataTable(CommandType.Text, sql);
                return ConvertModel.ConvertToList<T>(dt);
            }
            catch (Exception e)
            {
                LogHelper.LogError(e);
            }
            return new List<T>();
        }

        public virtual bool Update(T model)
        {
            bool result = default(bool);
            var sql = "update {0} set {1} where {2}=@{3};";
            var sqlTableName = string.Empty;
            var sqlSetColumn = string.Empty;
            var sqlWhereColumn = string.Empty;
            var values = new List<SqlParameter>();
            var tmp = GetColumns(model);
            sqlTableName = tmp.Item1;
            var uniqueTmp = GetUniqueColumn(model);
            sqlWhereColumn = uniqueTmp.Item2;
            foreach (var item in tmp.Item2)
            {
                if (!sqlWhereColumn.Equals(item.Item1))
                {
                    sqlSetColumn += string.Format("{0}=@{1},", item.Item1, ReplaceTab(item.Item1));
                    values.Add(new SqlParameter(string.Format("@{0}", ReplaceTab(item.Item1)), item.Item2));
                }
                else
                {
                    values.Add(new SqlParameter(string.Format("@{0}", ReplaceTab(item.Item1)), item.Item2));
                }
            }
            if (!string.IsNullOrEmpty(sqlSetColumn))
            {
                sqlSetColumn = sqlSetColumn.Substring(0, sqlSetColumn.Length - 1);
            }

            sql = string.Format(sql, sqlTableName, sqlSetColumn, sqlWhereColumn, ReplaceTab(sqlWhereColumn));
            try
            {
                result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, values.ToArray()) > 0;
            }
            catch (Exception e)
            {
                LogHelper.LogError(e);
            }
            return result;
        }

        public virtual bool Delete(T model)
        {
            var result = default(bool);
            try
            {
                var tmp = GetUniqueColumn(model);
                var sql = string.Format("delete from {0} where {1}=@{2};", tmp.Item1, tmp.Item2, ReplaceTab(tmp.Item2));
                result = SqlHelper.ExecuteNonQuery(CommandType.Text, sql, new SqlParameter(string.Format("@{0}", ReplaceTab(tmp.Item2)), tmp.Item3)) > 0;
            }
            catch (Exception e)
            {
                LogHelper.LogError(e);
            }

            return result;
        }

        private Tuple<string, List<Tuple<string, object>>> GetColumns(object obj)
        {
            var objType = obj.GetType();
            var tableName = GetTableName(obj, objType);
            if (tableName != null)
            {
                var columns = new List<Tuple<string, object>>();
                var properties = objType.GetProperties();
                foreach (var p in properties)
                {
                    var attribute = p.GetCustomAttribute<DataFieldAttribute>();
                    if (attribute != null)
                    {
                        if (attribute.IsDataField)
                        {
                            var tempValue = p.GetValue(obj);
                            if (tempValue != null)
                            {
                                columns.Add(new Tuple<string, object>(attribute.DataFieldName, tempValue));
                            }
                        }
                    }
                }
                return new Tuple<string, List<Tuple<string, object>>>(tableName, columns);
            }
            return null;
        }

        private string GetTableName(object obj, Type objType)
        {
            var attributes = objType.GetCustomAttributes(false);
            string tableName = null;
            foreach (var attribute in attributes)
            {
                if (attribute is DataFieldAttribute)
                {
                    var tmp = attribute as DataFieldAttribute;
                    if (tmp.IsDataField)
                    {
                        tableName = tmp.DataFieldName;
                        break;
                    }
                }
            }
            if (tableName == null)
            {
                LogHelper.Log(string.Format("类[{0}]没有设置数据库表名", obj.ToString()));
            }
            return tableName;
        }

        private Tuple<string, string, object> GetUniqueColumn(object obj)
        {
            var objType = obj.GetType();
            var tableName = GetTableName(obj, objType);
            if (tableName != null)
            {
                var properties = objType.GetProperties();
                foreach (var p in properties)
                {
                    var attribute = p.GetCustomAttribute<UniqueDataFieldAttribute>();
                    if (attribute != null)
                    {
                        if (attribute.IsUnique)
                        {
                            return new Tuple<string, string, object>(tableName, attribute.DataFieldName, p.GetValue(obj));
                        }
                    }
                }
            }
            LogHelper.Log(string.Format("类[{0}]没有设置表唯一字段", obj.ToString()));
            return null;
        }

        private string ReplaceTab(string columnName)
        {
            return columnName.Replace("[", string.Empty).Replace("]", string.Empty);
        }
    }
}
