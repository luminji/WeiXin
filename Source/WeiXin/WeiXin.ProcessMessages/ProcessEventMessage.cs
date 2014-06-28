using WeiXin.Messages;

namespace WeiXin.ProcessMessages
{
    public class ProcessEventMessage : ProcessMessage
    {
        public override string Process(Message receiveMsg)
        {
            string result = string.Empty;
            EventMessage eventMsg = receiveMsg as EventMessage;
            if (!string.IsNullOrEmpty(eventMsg.Ticket))              // 扫描二维码事件
            {
                var content = string.Empty;
                if (eventMsg.Event.Equals("subscribe"))             // 用户未关注时扫描的事件
                {
                    content += string.Format("二维码参数值：{0}\r\n", eventMsg.EventKey.Replace("qrscene_", string.Empty));
                }
                else if (eventMsg.Event.Equals("SCAN"))             // 用户已关注时扫描的事件
                {
                    content += string.Format("二维码参数值：{0}\r\n", eventMsg.EventKey);
                }
                content += string.Format("二维码 Ticket：{0}", eventMsg.Ticket);
                result = MessageManager.CreateTextMessageXml(receiveMsg.Xml, content);
            }
            else if (eventMsg.Event.Equals("VIEW"))                 // 点击菜单跳转链接时的事件
            {

            }
            else if (eventMsg.Event.Equals("CLICK"))                // 点击菜单拉取消息时的事件
            {
                result = new ProcessEventClickMessage().Process(receiveMsg);
            }
            else if (eventMsg.Event.Equals("LOCATION"))             // 上报地理位置时的事件
            {
                result = MessageManager.CreateTextMessageXml(receiveMsg.Xml, string.Format("您的地理位置纬度：{0}，经度：{1}，精度：{2}", eventMsg.Latitude, eventMsg.Longitude, eventMsg.Precision));
            }
            else if (eventMsg.Event.Equals("subscribe"))            // 进行关注的事件
            {
                result = MessageManager.CreateTextMessageXml(receiveMsg.Xml, "感谢您关注。");
            }
            else if (eventMsg.Event.Equals("unsubscribe"))          // 取消关注的事件
            {

            }
            else
            {
                result = ProcessMessageManager.NotProcess(receiveMsg.Xml);
            }
            return result;
        }
    }
}