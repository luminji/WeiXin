using System.Collections.Generic;
using System.Web;
using WeiXin.Attributes.SendMessage;

namespace WeiXin.SendMessage
{
    public class CustomerServiceNewsMessage : CustomerServiceMessage
    {
        public CustomerServiceNewsMessage()
        {
            this.MsgType = CustomerServiceMessageType.News;
        }

        [JsonProperty("articles")]
        public List<Article> Articles { get; set; }

        public override string GetJson()
        {
            var newsFormat = "\"articles\":[{0}]";
            var articles = string.Empty;
            foreach (var article in Articles)
            {
                articles += "{" + article.GetJson() + "},";
            }
            if (articles.Length > 0)
            {
                articles = articles.Substring(0, articles.Length - 1);
            }
            var news = string.Format(newsFormat, articles);
            return base.Json(news);
        }
    }

    public class Article
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("picurl")]
        public string PicUrl { get; set; }

        public string GetJson()
        {
            var result = string.Empty;
            if (!string.IsNullOrEmpty(Title))
            {
                result += "\"title\":\"" + HttpUtility.UrlDecode(Title) + "\",";
            }
            if (!string.IsNullOrEmpty(Description))
            {
                result += "\"description\":\"" + HttpUtility.UrlDecode(Description) + "\",";
            }
            if (!string.IsNullOrEmpty(Url))
            {
                result += "\"url\":\"" + HttpUtility.UrlDecode(Url) + "\",";
            }
            if (!string.IsNullOrEmpty(PicUrl))
            {
                result += "\"picurl\":\"" + HttpUtility.UrlDecode(PicUrl) + "\",";
            }
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
    }
}
