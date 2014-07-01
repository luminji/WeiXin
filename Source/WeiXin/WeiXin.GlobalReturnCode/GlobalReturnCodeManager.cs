using System;
using WeiXin.Utilitys;

namespace WeiXin.GlobalReturnCode
{
    public sealed class GlobalReturnCodeManager
    {
        public static ReturnCode GetReturnCode(string json)
        {
            ReturnCode returnCode = new ReturnCode();
            returnCode.Json = json;
            if (!string.IsNullOrEmpty(json))
            {
                if (json.IndexOf("errcode") > -1)
                {
                    var jsonObj = JsonSerializerHelper.Deserialize(json);
                    returnCode.ErrCode = Convert.ToInt32(jsonObj["errcode"]);
                    returnCode.Msg = GlobalReturnCodeDictionary.RETURNCIDEDICTIONARY[returnCode.ErrCode];
                    returnCode.IsRequestSuccess = returnCode.ErrCode == 0;
                }
                else
                {
                    returnCode.IsRequestSuccess = true;
                }
            }
            return returnCode;
        }
    }
}
