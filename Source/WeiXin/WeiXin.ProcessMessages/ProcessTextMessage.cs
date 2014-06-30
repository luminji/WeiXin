using WeiXin.Messages;

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