using WeiXin.Messages;

namespace WeiXin.ProcessMessages
{
    public class ProcessImageMessage : ProcessMessage
    {
        public override string Process(Message receiveMsg)
        {
            return ProcessMessageManager.NotProcess(receiveMsg.Xml);
        }
    }
}