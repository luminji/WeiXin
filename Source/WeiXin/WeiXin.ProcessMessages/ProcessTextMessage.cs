using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeiXin.Config;
using WeiXin.DAL;
using WeiXin.Messages;
using WeiXin.SendMessage;

namespace WeiXin.ProcessMessages
{
    public class ProcessTextMessage : ProcessMessage
    {
        public override string Process(Message receiveMsg)
        {
            TextMessage msg = receiveMsg as TextMessage;
            msg.Content = msg.Content.Trim();
            return ProcessMessageManager.InvokMessageFunc(receiveMsg, msg.Content);
        }
    }
}