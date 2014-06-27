using System;
using System.IO;
using System.Net;
using System.Text;

namespace WeiXin.Utilitys
{
    public class PostHttpErrorException : Exception
    {
        public int Code { get; set; }
        public int HttpStatusCode { get; set; }
        new public string Message { get; set; }
    }

    public class HttpRequestHelper
    {
        private static string Http_ForamtByJson(string url, string method = "GET", string json = null)
        {
            var request = WebRequest.Create(url);
            request.Method = method;
            request.Timeout = 600000;
            request.ContentType = "application/json";
            if (!string.IsNullOrEmpty(json))
            {
                var buffer = Encoding.UTF8.GetBytes(json);
                request.ContentLength = buffer.Length;
                using (Stream writer = request.GetRequestStream())
                {
                    writer.Write(buffer, 0, buffer.Length);
                    writer.Flush();
                }
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new PostHttpErrorException { Message = string.Format("请求失败，HTTP 状态码{0}", (int)response.StatusCode), HttpStatusCode = (int)response.StatusCode, Code = 9000 };
                }
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
        }

        public static string GetHttp_ForamtByJson(string url)
        {
            return Http_ForamtByJson(url);
        }

        public static string PostHttp_ForamtByJson(string url, string json)
        {
            return Http_ForamtByJson(url, "POST", json);
        }

        public static string HttpRequest(string url, string method = "GET", string data = null)
        {
            var request = WebRequest.Create(url);
            request.Method = method;
            request.Timeout = 600000;

            if (!string.IsNullOrEmpty(data))
            {
                var buffer = Encoding.ASCII.GetBytes(data);
                request.ContentLength = data.Length;
                using (Stream writer = request.GetRequestStream())
                {
                    writer.Write(buffer, 0, buffer.Length);
                    writer.Flush();
                }
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new PostHttpErrorException { Message = string.Format("请求失败，HTTP 状态码{0}", (int)response.StatusCode), HttpStatusCode = (int)response.StatusCode, Code = 9000 };
                }
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
        }
    }
}
