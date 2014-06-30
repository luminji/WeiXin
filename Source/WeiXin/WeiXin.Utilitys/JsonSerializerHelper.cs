using System.Collections.Generic;
using System.Reflection;
using System.Web.Script.Serialization;
using WeiXin.Attributes;

namespace WeiXin.Utilitys
{
    public sealed class JsonSerializerHelper
    {
        public static Dictionary<string, object> Deserialize(string json)
        {
            var jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, object>>(json);
        }

        public static IList<Dictionary<string, object>> Deserializes(string json)
        {
            var jss = new JavaScriptSerializer();
            return jss.Deserialize<IList<Dictionary<string, object>>>(json);
        }

        public static string Serialize(object obj)
        {
            var jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }

        public static T ConvertJsonStringToObject<T>(string json)
        {
            var jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(json);
        }

        public static IList<T> ConvertJsonStringToObjects<T>(string jsonStr)
        {
            var jss = new JavaScriptSerializer();
            return jss.Deserialize<IList<T>>(jsonStr);
        }

        public static T ConvertJsonStringToObjectByJsonPropertyAttribute<T>(string json) where T : class,new()
        {
            T obj = new T();
            var objType = typeof(T);
            var tmpPros = objType.GetProperties();
            var jsonObj = Deserialize(json);
            foreach (var p in tmpPros)
            {
                var attribute = p.GetCustomAttribute<JsonPropertyAttribute>();
                if (attribute != null)
                {
                    if (jsonObj.ContainsKey(attribute.PropertyName))
                    {
                        p.SetValue(obj, jsonObj[attribute.PropertyName]);
                    }
                }
            }
            return obj;
        }
    }
}
