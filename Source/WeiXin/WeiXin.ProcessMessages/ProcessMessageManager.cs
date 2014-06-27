using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeiXin.Attributes.Messages;
using WeiXin.Config;
using WeiXin.DAL;
using WeiXin.Messages;
using WeiXin.Models;
using WeiXin.SendMessage;
using WeiXin.Utilitys;

namespace WeiXin.ProcessMessages
{
    public static class ProcessMessageManager
    {
        public const string NotProcessMsg = "呃，不明白，您的问题难倒我了。";
        public const string NotImplementedMsg = "研发小弟正在疯狂的开发中，精彩即将呈现，敬请期待[鼓掌][鼓掌][鼓掌]";
        public const string ConfigurationErrorMsg = "唉，粗心大意啊，服务器配置出错了。放心，我们会马上修复的。";
        public const string SystemUpdateingMsg = "为了给您提供更好的服务体验，我们会不定期进行系统升级，给您造成的不便我们深表歉意。";
        readonly static Dictionary<MessageType, ProcessMessage> Processs;
        readonly static Dictionary<string, Func<Message, string>> MessageFuncs;

        static ProcessMessageManager()
        {
            MessageFuncs = new Dictionary<string, Func<Message, string>>();
            MessageFuncs.Add("1", Action1);
            MessageFuncs.Add("2", Action2);
            MessageFuncs.Add("3", Action3);
            MessageFuncs.Add("4", Action4);
            MessageFuncs.Add("5", Action5);
            MessageFuncs.Add("6", Action6);
            MessageFuncs.Add("7", Action7);
            MessageFuncs.Add("8", Action8);
            MessageFuncs.Add("9", Action9);
            MessageFuncs.Add("10", Action10);
            MessageFuncs.Add("11", Action11);
            MessageFuncs.Add("12", Action12);
            MessageFuncs.Add("13", Action13);
            MessageFuncs.Add("14", Action14);
            MessageFuncs.Add("15", Action15);

            Processs = new Dictionary<MessageType, ProcessMessage>();
            Processs.Add(MessageType.Event, new ProcessEventMessage());
            Processs.Add(MessageType.Image, new ProcessImageMessage());
            Processs.Add(MessageType.Link, new ProcessLinkMessage());
            Processs.Add(MessageType.Location, new ProcessLocationMessage());
            Processs.Add(MessageType.Text, new ProcessTextMessage());
            Processs.Add(MessageType.Video, new ProcessVideoMessage());
            Processs.Add(MessageType.Voice, new ProcessVoiceMessage());
        }

        public static string InvokMessageFunc(Message receiveMsg, string funcKey)
        {
            if (MessageFuncs.ContainsKey(funcKey))
            {
                return MessageFuncs[funcKey](receiveMsg);
            }
            else
            {
                return NotProcess(receiveMsg.Xml);
            }
        }

        public static string Process(Message receiveMsg, MessageType msgType)
        {
            string replyMsg = string.Empty;
            // TODO:这里需要根据 receiveMsg.MsgId 去重
            if (ConfigProperty.IsConfigurationOk)
            {
                if (ConfigProperty.WeiXin_UpdateEnable)
                {
                    replyMsg = MessageManager.CreateTextMessageXml(receiveMsg.Xml, SystemUpdateingMsg);
                }
                else
                {
                    var process = Processs[msgType];
                    if (process != null)
                    {
                        replyMsg = process.Process(receiveMsg);
                    }
                    else
                    {
                        LogHelper.Log("方法[WeiXin.ProcessMessages.ProcessMessageManager.Process]创建消息处理接口失败，原因是没有找到对应的消息类型接口。");
                    }
                }
            }
            else
            {
                replyMsg = MessageManager.CreateTextMessageXml(receiveMsg.Xml, ConfigurationErrorMsg);
            }
            return replyMsg;
        }

        public static string NotProcess(string xml)
        {
            return MessageManager.CreateTextMessageXml(xml, NotProcessMsg);
        }

        /// <summary>
        /// 验证此消息类型是否需要回复
        /// </summary>
        /// <param name="msgType"></param>
        /// <returns></returns>
        public static bool IsReply(MessageType msgType)
        {
            ReplyMessageAttribute replyMessageAttribute = ReflectionHelper.GetCustomAttribute<ReplyMessageAttribute>(msgType);
            return replyMessageAttribute.IsReply;
        }
        private static string Action1(Message receiveMsg)
        {
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, "我是被动文本消息");
        }
        private static string Action2(Message receiveMsg)
        {
            var article = new List<WeiXin.Messages.Article>();
            article.Add(new WeiXin.Messages.Article { Title = string.Format(MessageManager.MsgTag, "图文消息1"), Description = string.Format(MessageManager.MsgTag, "大图的格式为JPG、PNG，360*200，小图200*200。这里我随便指定的图片"), Picurl = string.Format(MessageManager.MsgTag, "http://a.hiphotos.baidu.com/image/pic/item/37d3d539b6003af352021be5372ac65c1138b6ff.jpg"), Url = string.Format(MessageManager.MsgTag, "http://www.wangwenzhuang.com/") });
            article.Add(new WeiXin.Messages.Article { Title = string.Format(MessageManager.MsgTag, "图文消息2"), Description = string.Format(MessageManager.MsgTag, "大图的格式为JPG、PNG，360*200，小图200*200。这里我随便指定的图片"), Picurl = string.Format(MessageManager.MsgTag, "http://g.hiphotos.baidu.com/image/pic/item/55e736d12f2eb93895023c7fd7628535e4dd6fcb.jpg"), Url = string.Format(MessageManager.MsgTag, "http://www.wangwenzhuang.com/") });
            return MessageManager.CreateNewsMessageXml(receiveMsg.Xml, article);
        }
        private static string Action3(Message receiveMsg)
        {
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, NotImplementedMsg);
        }
        private static string Action4(Message receiveMsg)
        {
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, NotImplementedMsg);
        }
        private static string Action5(Message receiveMsg)
        {
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, NotImplementedMsg);
        }
        private static string Action6(Message receiveMsg)
        {
            //Task t = new Task(() =>
            //{
            //    var discription = "图文消息";
            //    var url = "http://www.wangwenzhuang.com/";
            //    var articles = new List<WeiXin.SendMessage.Article>();
            //    articles.Add(new WeiXin.SendMessage.Article { Title = "已申请的学分", Description = discription, Url = url });
            //    var msg = new CustomerServiceNewsMessage { Touser = receiveMsg.FromUserName, Articles = articles };
            //    SendCustomerServiceMessageManager.SendTextAndImageMessage(msg);
            //});
            //t.Start();
            //return string.Empty;
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, NotImplementedMsg);
        }
        private static string Action7(Message receiveMsg)
        {
            //Task t = new Task(() =>
            //{
            //    var msg = new CustomerServiceTextMessage { Touser = receiveMsg.FromUserName, Content = string.Empty };
            //    SendCustomerServiceMessageManager.SendTextMessage(msg);
            //});
            //t.Start();
            //return string.Empty;
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, NotImplementedMsg);
        }
        private static string Action8(Message receiveMsg)
        {
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, NotImplementedMsg);
        }
        private static string Action9(Message receiveMsg)
        {
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, NotImplementedMsg);
        }
        private static string Action10(Message receiveMsg)
        {
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, NotImplementedMsg);
        }
        private static string Action11(Message receiveMsg)
        {
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, NotImplementedMsg);
        }
        private static string Action12(Message receiveMsg)
        {
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, NotImplementedMsg);
        }
        private static string Action13(Message receiveMsg)
        {
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, NotImplementedMsg);
        }
        private static string Action14(Message receiveMsg)
        {
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, NotImplementedMsg);
        }
        private static string Action15(Message receiveMsg)
        {
            return MessageManager.CreateTextMessageXml(receiveMsg.Xml, NotImplementedMsg);
        }
    }
}