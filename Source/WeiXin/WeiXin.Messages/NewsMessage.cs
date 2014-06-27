using System.Collections.Generic;
using WeiXin.Attributes.Messages;

namespace WeiXin.Messages
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class NewsMessage : Message
    {
        /// <summary>
        /// 图文消息个数，限制为10条以内
        /// </summary>
        [SendMessageProperty(true)]
        public int ArticleCount { get; set; }
        [SendMessageProperty(true)]
        [SendMessagePropertyIsChild(true)]
        public List<Article> Articles { get; set; }
    }
    public class Article
    {
        [SendMessageProperty(true)]
        public string Title { get; set; }
        [SendMessageProperty(true)]
        public string Description { get; set; }
        [SendMessageProperty(true)]
        public string Url { get; set; }
        [SendMessageProperty(true)]
        public string Picurl { get; set; }
    }
}
