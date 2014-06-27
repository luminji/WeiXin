using WeiXin.Messages;

namespace WeiXin.ProcessMessages
{
    public class ProcessEventClickMessage : ProcessMessage
    {
        public override string Process(Message receiveMsg)
        {
            EventMessage eventMsg = receiveMsg as EventMessage;
            return ProcessMessageManager.InvokMessageFunc(receiveMsg, eventMsg.EventKey);
        }
    }
}