using System;
using System.IO;

namespace WeiXin.Utilitys
{
    public sealed class LogHelper
    {
        static readonly string errorLogFileName = @"error.txt";
        static readonly string logFileName = @"log.txt";
        static readonly string WeiXinMessageFileName = @"wx_msg.txt";

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
            WeiXinMessageFileName = Path.Combine(logPath, WeiXinMessageFileName);
        }

        public static void LogError(Exception e)
        {
            File.AppendAllText(errorLogFileName, string.Format("时间：{0}\r\n异常信息：{1}\r\n源：{2}\r\n堆栈：{3}\r\n引发异常的方法：{4}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), e.Message, e.Source, e.StackTrace, e.TargetSite.Name));
        }

        public static void Log(string msg)
        {
            File.AppendAllText(logFileName, string.Format("时间：{0}\r\n消息：{1}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg));
        }

        public static void LogWeiXinMessage(string msg)
        {
            File.AppendAllText(WeiXinMessageFileName, string.Format("时间：{0}\r\n消息：\r\n{1}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg));
        }
    }
}
