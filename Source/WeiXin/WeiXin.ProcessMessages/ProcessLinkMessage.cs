using WeiXin.Messages;

namespace WeiXin.ProcessMessages
{
    public class ProcessLinkMessage : ProcessMessage
    {
        public override string Process(Message receiveMsg)
        {
            return ProcessMessageManager.NotProcess(receiveMsg.Xml);
        }
    }
}