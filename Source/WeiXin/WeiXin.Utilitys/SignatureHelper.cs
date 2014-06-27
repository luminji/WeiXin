using System;
using System.Security.Cryptography;
using System.Text;

namespace WeiXin.Utilitys
{
    public sealed class SignatureHelper
    {
        /*
         * 必备参数：token、时间戳、随机数（9位）
         * 生成规则：对参数进行排序生成sha1哈希（不带符号）
         * 
         * 
         * 王文壮
         */
        /// <summary>
        /// 生成签名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string CreateSignature(Array array)
        {
            Array.Sort(array);
            string tmpStr = string.Empty;
            foreach (var tmp in array)
            {
                tmpStr += tmp;
            }
            return BitConverter.ToString(new SHA1CryptoServiceProvider().ComputeHash(new ASCIIEncoding().GetBytes(tmpStr))).Replace("-", string.Empty).ToLower();
        }

        /// <summary>
        /// 生成时间戳
        /// 规则：1970年1月1日至今的间隔秒数
        /// </summary>
        /// <returns></returns>
        public static string CreateTimestamp()
        {
            return ((int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        /// <summary>
        /// 创建9位随机数
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomNumber()
        {
            return new Random().Next(100000000, 999999999).ToString();
        }

        public static bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] tempArr = { token, timestamp, nonce };
            var tmpStr = SignatureHelper.CreateSignature(tempArr);
            return tmpStr.Equals(signature);
        }
    }
}
