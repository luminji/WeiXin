using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WeiXin.Utilitys
{
    public sealed class XmlHelper
    {
        public static Dictionary<string, string> Read(string xml)
        {
            var result = new Dictionary<string, string>();
            XElement xmlElement = XElement.Parse(xml);
            var elements = xmlElement.Elements().ToList();
            foreach (var elemet in elements)
            {
                result.Add(elemet.Name.LocalName, elemet.Value);
            }
            return result;
        }

        /// <summary>
        /// 返回数组索引对应 params
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="elementNames"></param>
        /// <returns></returns>
        public static string[] GetElementValue(string xml, params string[] elementNames)
        {
            var result = new string[elementNames.Length];
            XElement xmlElement = XElement.Parse(xml);
            var elements = xmlElement.Elements().ToList();
            for (int i = 0; i < elementNames.Length; i++)
            {
                var tmps = elements.Where(e => e.Name.LocalName.Equals(elementNames[i])).ToList();
                if (tmps.Count > 0)
                {
                    result[i] = tmps[0].Value;
                }
            }
            return result;
        }
    }
}
