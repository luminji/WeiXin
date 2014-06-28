using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WeiXin.Attributes;
using WeiXin.Attributes.Messages;
using WeiXin.Utilitys;

namespace WeiXin.Messages
{
    public sealed class MessageManager
    {
        /// <summary>
        /// 此标签在xml中可以在{0}中输入特殊字符，转为属性时会自动过滤为{0}中的值，所以在xml转成实体属性时不需要做任何处理
        /// </summary>
        public const string MsgTag = "<![CDATA[{0}]]>";

        public static Dictionary<string, object> ConvertToMessage(string xml)
        {
            var obj = XmlHelper.Read(xml);
            var msgType = obj["MsgType"];
            var messageType = GetMsgType(msgType);
            if (messageType != MessageType.Undefined)
            {
                var result = new Dictionary<string, object>();
                var msg = ConvertMsg(obj, messageType);
                msg.Xml = xml;
                result.Add("MsgType", messageType);
                result.Add("Msg", msg);
                return result;
            }
            LogHelper.Log("方法[WeiXin.Messages.MessageManager.ConvertToMessage]转换消息失败，原因是没有找到对应消息类型。");
            return null;
        }

        public static MessageType GetMsgTypeByXml(string xml)
        {
            var result = MessageType.Undefined;
            var values = XmlHelper.GetElementValue(xml, "MsgType");
            if (values.Length > 0)
            {
                var msgType = values[0];
                result = GetMsgType(msgType);
            }
            else
            {
                LogHelper.Log("方法[WeiXin.Messages.MessageManager.GetMsgTypeByXml]获取消息MsgType失败，消息没有此节点，这是必要属性。");
            }
            return result;
        }

        public static string CreateTextMessageXml(string xml, string content)
        {
            var obj = XmlHelper.Read(xml);
            var receiveMsg = MessageManager.ConvertMsg(obj, MessageType.Text);
            var sendMsg = new TextMessage();
            sendMsg.Xml = xml;
            sendMsg.ToUserName = receiveMsg.FromUserName;
            sendMsg.FromUserName = receiveMsg.ToUserName;
            sendMsg.CreateTime = SignatureHelper.CreateTimestamp();
            sendMsg.MsgType = string.Format(MessageManager.MsgTag,ReflectionHelper.GetCustomAttribute<WXMsgTypeAttribute>(MessageType.Text).MsgType);
            sendMsg.Content = string.Format(MessageManager.MsgTag, content);
            return MessageManager.ConvertToXml(sendMsg);
        }

        public static string CreateNewsMessageXml(string xml, List<Article> articles)
        {
            var obj = XmlHelper.Read(xml);
            var receiveMsg = MessageManager.ConvertMsg(obj, MessageType.Text);
            var sendMsg = new NewsMessage();
            sendMsg.Xml = xml;
            sendMsg.ToUserName = receiveMsg.FromUserName;
            sendMsg.FromUserName = receiveMsg.ToUserName;
            sendMsg.CreateTime = SignatureHelper.CreateTimestamp();
            sendMsg.MsgType = string.Format(MessageManager.MsgTag,ReflectionHelper.GetCustomAttribute<WXMsgTypeAttribute>(MessageType.News).MsgType);
            sendMsg.Articles = articles;
            sendMsg.ArticleCount = sendMsg.Articles.Count;
            return MessageManager.ConvertToXml(sendMsg);
        }

        private static Message CreateMessage(MessageType msgType)
        {
            var reflectionAttribute = ReflectionHelper.GetCustomAttribute<MessageReflectionAttribute>(msgType);
            return ReflectionHelper.CreateObject(reflectionAttribute.AssemblyString, string.Format("{0}.{1}", reflectionAttribute.AssemblyString, reflectionAttribute.ClassName)) as Message;
        }

        private static Message ConvertMsg(Dictionary<string, string> dic, MessageType msgType)
        {
            var result = CreateMessage(msgType);
            var objType = result.GetType();
            var pros = objType.GetProperties();
            foreach (var key in dic.Keys)
            {
                var tmps = pros.Where(p => p.Name.Equals(key)).ToList();
                if (tmps.Count > 0)
                {
                    var pInfo = tmps[0];
                    var value = dic[key];
                    pInfo.SetValue(result, value);
                }
            }
            return result as Message;
        }

        private static string ConvertToXml(Message msg)
        {
            var xml = "<xml>";
            var objType = msg.GetType();
            var pors = objType.GetProperties();
            foreach (var p in pors)
            {
                var sendMessagePropertyAttribute = p.GetCustomAttribute<SendMessagePropertyAttribute>();
                if (sendMessagePropertyAttribute != null)
                {
                    if (sendMessagePropertyAttribute.IsRequired)
                    {
                        var sendMessagePropertyIsChildAttribute = p.GetCustomAttribute<SendMessagePropertyIsChildAttribute>();
                        if (sendMessagePropertyIsChildAttribute != null)
                        {
                            if (sendMessagePropertyIsChildAttribute.IsChild)
                            {
                                var value = ConvertChildToXml("item", p.GetValue(msg));
                                xml = string.Format("{0}<{1}>{2}</{1}>", xml, p.Name, value);
                            }
                        }
                        else
                        {
                            xml = string.Format("{0}<{1}>{2}</{1}>", xml, p.Name, p.GetValue(msg));
                        }
                    }
                }
            }
            xml = xml + "</xml>";
            return xml;
        }

        private static string ConvertChildToXml(string tagName, object obj)
        {
            var result = string.Empty;
            if (obj is IEnumerable)
            {
                var enumerable = obj as IEnumerable;
                foreach (var item in enumerable)
                {
                    result = string.Format("{0}<{1}>", result, tagName);
                    var objType = item.GetType();
                    var pors = objType.GetProperties();
                    foreach (var p in pors)
                    {
                        var sendMessagePropertyAttribute = p.GetCustomAttribute<SendMessagePropertyAttribute>();
                        if (sendMessagePropertyAttribute != null)
                        {
                            if (sendMessagePropertyAttribute.IsRequired)
                            {
                                result = string.Format("{0}<{1}>{2}</{1}>", result, p.Name, p.GetValue(item));
                            }
                        }
                    }
                    result = string.Format("{0}</{1}>", result, tagName);
                }
            }
            return result;
        }

        private static MessageType GetMsgType(string msgType)
        {
            if (msgType.Equals("text"))
            {
                return MessageType.Text;
            }
            else if (msgType.Equals("image"))
            {
                return MessageType.Image;
            }
            else if (msgType.Equals("voice"))
            {
                return MessageType.Voice;
            }
            else if (msgType.Equals("video"))
            {
                return MessageType.Video;
            }
            else if (msgType.Equals("location"))
            {
                return MessageType.Location;
            }
            else if (msgType.Equals("link"))
            {
                return MessageType.Link;
            }
            else if (msgType.Equals("event"))
            {
                return MessageType.Event;
            }
            else
            {
                return MessageType.Undefined;
            }
        }
    }
}
