using System.Collections.Generic;
using System.Web.Script.Serialization;

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
    }
}
