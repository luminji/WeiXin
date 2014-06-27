using WeiXin.Messages;

namespace WeiXin.ProcessMessages
{
    public abstract class ProcessMessage
    {
        public virtual string Process(Message receiveMsg)
        {
            return string.Empty;
        }
    }
}