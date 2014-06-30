using WeiXin.Messages;

namespace WeiXin.ProcessMessages
{
    public class ProcessVoiceMessage : ProcessMessage
    {
        public override string Process(Message receiveMsg)
        {
            string result = string.Empty;
            VoiceMessage voiceMessage = receiveMsg as VoiceMessage;
            if (!string.IsNullOrEmpty(voiceMessage.Recognition))              // 语音识别结果
            {
                result = MessageManager.CreateTextMessageXml(receiveMsg.Xml, string.Format("语音识别结果为：{0}", voiceMessage.Recognition));
            }
            else
            {
                result = ProcessMessageManager.NotProcess(receiveMsg.Xml);
            }
            return result;
        }
    }
}