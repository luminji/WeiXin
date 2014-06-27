using WeiXin.Messages;

namespace WeiXin.ProcessMessages
{
    public class ProcessVoiceMessage : ProcessMessage
    {
        public override string Process(Message receiveMsg)
        {
            return ProcessMessageManager.NotProcess(receiveMsg.Xml);
        }
    }
}