using System;
using System.IO;

namespace WeiXin.Utilitys
{
    public sealed class LogHelper
    {
        static readonly string errorLogFileName = @"error.txt";
        static readonly string logFileName = @"log.txt";
        static readonly string weiXinMessageFileName = @"wx_msg.txt";
        static readonly string weiXinApiReturnCodeFileName = @"wx_api.txt";

        static LogHelper()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var logPath = Path.Combine(baseDir, "Log");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            errorLogFileName = Path.Combine(logPath, errorLogFileName);
            logFileName = Path.Combine(logPath, logFileName);
            weiXinMessageFileName = Path.Combine(logPath, weiXinMessageFileName);
            weiXinApiReturnCodeFileName = Path.Combine(logPath, weiXinApiReturnCodeFileName);
        }

        public static void LogError(Exception e)
        {
            File.AppendAllText(errorLogFileName, string.Format("时间：{0}\r\n异常信息：{1}\r\n源：{2}\r\n堆栈：{3}\r\n引发异常的方法：{4}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), e.Message, e.Source, e.StackTrace, e.TargetSite.Name));
        }

        public static void Log(string msg)
        {
            File.AppendAllText(logFileName, string.Format("时间：{0}\r\n消息：{1}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg));
        }
        public static void Log(string msg, Exception e)
        {
            File.AppendAllText(logFileName, string.Format("时间：{0}\r\n消息：{1}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg));
            File.AppendAllText(logFileName, string.Format("时间：{0}\r\n消息：{1}\r\n异常信息：{2}\r\n源：{3}\r\n堆栈：{4}\r\n引发异常的方法：{5}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg, e.Message, e.Source, e.StackTrace, e.TargetSite.Name));
        }

        public static void LogWeiXinMessage(string msg)
        {
            File.AppendAllText(weiXinMessageFileName, string.Format("时间：{0}\r\n消息：\r\n{1}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg));
        }

        public static void LogWeiXinApiReturnCode(string msg, int errCode, string errMsg, string json)
        {
            File.AppendAllText(weiXinApiReturnCodeFileName, string.Format("时间：{0}\r\n消息：\r\n{1}\r\nerrCode：{2}\r\n返回值说明：{3}\r\njson：{4}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg, errCode, errMsg, json));
        }
    }
}
