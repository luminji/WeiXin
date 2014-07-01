using System.Collections.Generic;

namespace WeiXin.GlobalReturnCode
{
    internal class GlobalReturnCodeDictionary
    {
        internal readonly static Dictionary<int, string> RETURNCIDEDICTIONARY;
        static GlobalReturnCodeDictionary()
        {
            RETURNCIDEDICTIONARY = new Dictionary<int, string>();
            RETURNCIDEDICTIONARY.Add(-1, "系统繁忙");
            RETURNCIDEDICTIONARY.Add(0, "请求成功");
            RETURNCIDEDICTIONARY.Add(40001, " 获取access_token时AppSecret错误，或者access_token无效");
            RETURNCIDEDICTIONARY.Add(40002, " 不合法的凭证类型");
            RETURNCIDEDICTIONARY.Add(40003, " 不合法的OpenID");
            RETURNCIDEDICTIONARY.Add(40004, "	 不合法的媒体文件类型");
            RETURNCIDEDICTIONARY.Add(40005, " 不合法的文件类型");
            RETURNCIDEDICTIONARY.Add(40006, " 不合法的文件大小");
            RETURNCIDEDICTIONARY.Add(40007, "不合法的媒体文件id");
            RETURNCIDEDICTIONARY.Add(40008, "不合法的消息类型");
            RETURNCIDEDICTIONARY.Add(40009, "不合法的图片文件大小");
            RETURNCIDEDICTIONARY.Add(40010, "不合法的语音文件大小");
            RETURNCIDEDICTIONARY.Add(40011, "不合法的视频文件大小");
            RETURNCIDEDICTIONARY.Add(40012, "不合法的缩略图文件大小");
            RETURNCIDEDICTIONARY.Add(40013, "不合法的APPID");
            RETURNCIDEDICTIONARY.Add(40014, "不合法的access_token");
            RETURNCIDEDICTIONARY.Add(40015, "不合法的菜单类型");
            RETURNCIDEDICTIONARY.Add(40016, "不合法的按钮个数");
            RETURNCIDEDICTIONARY.Add(40017, "不合法的按钮个数");
            RETURNCIDEDICTIONARY.Add(40018, "不合法的按钮名字长度");
            RETURNCIDEDICTIONARY.Add(40019, "不合法的按钮KEY长度");
            RETURNCIDEDICTIONARY.Add(40020, "不合法的按钮URL长度");
            RETURNCIDEDICTIONARY.Add(40021, "不合法的菜单版本号");
            RETURNCIDEDICTIONARY.Add(40022, "不合法的子菜单级数");
            RETURNCIDEDICTIONARY.Add(40023, "不合法的子菜单按钮个数");
            RETURNCIDEDICTIONARY.Add(40024, "不合法的子菜单按钮类型");
            RETURNCIDEDICTIONARY.Add(40025, "不合法的子菜单按钮名字长度");
            RETURNCIDEDICTIONARY.Add(40026, "不合法的子菜单按钮KEY长度");
            RETURNCIDEDICTIONARY.Add(40027, "不合法的子菜单按钮URL长度");
            RETURNCIDEDICTIONARY.Add(40028, "不合法的自定义菜单使用用户");
            RETURNCIDEDICTIONARY.Add(40029, "不合法的oauth_code");
            RETURNCIDEDICTIONARY.Add(40030, "不合法的refresh_token");
            RETURNCIDEDICTIONARY.Add(40031, "不合法的openid列表");
            RETURNCIDEDICTIONARY.Add(40032, "不合法的openid列表长度");
            RETURNCIDEDICTIONARY.Add(40033, "不合法的请求字符，不能包含\\uxxxx格式的字符");
            RETURNCIDEDICTIONARY.Add(40035, "不合法的参数");
            RETURNCIDEDICTIONARY.Add(40038, "不合法的请求格式");
            RETURNCIDEDICTIONARY.Add(40039, "不合法的URL长度");
            RETURNCIDEDICTIONARY.Add(40050, "不合法的分组id");
            RETURNCIDEDICTIONARY.Add(40051, "分组名字不合法");
            RETURNCIDEDICTIONARY.Add(41001, "缺少access_token参数");
            RETURNCIDEDICTIONARY.Add(41002, "缺少appid参数");
            RETURNCIDEDICTIONARY.Add(41003, "缺少refresh_token参数");
            RETURNCIDEDICTIONARY.Add(41004, "缺少secret参数");
            RETURNCIDEDICTIONARY.Add(41005, "缺少多媒体文件数据");
            RETURNCIDEDICTIONARY.Add(41006, "缺少media_id参数");
            RETURNCIDEDICTIONARY.Add(41007, "缺少子菜单数据");
            RETURNCIDEDICTIONARY.Add(41008, "缺少oauth code");
            RETURNCIDEDICTIONARY.Add(41009, "缺少openid");
            RETURNCIDEDICTIONARY.Add(42001, "access_token超时");
            RETURNCIDEDICTIONARY.Add(42002, "refresh_token超时");
            RETURNCIDEDICTIONARY.Add(42003, "oauth_code超时");
            RETURNCIDEDICTIONARY.Add(43001, "需要GET请求");
            RETURNCIDEDICTIONARY.Add(43002, "需要POST请求");
            RETURNCIDEDICTIONARY.Add(43003, "需要HTTPS请求");
            RETURNCIDEDICTIONARY.Add(43004, "需要接收者关注");
            RETURNCIDEDICTIONARY.Add(43005, "需要好友关系");
            RETURNCIDEDICTIONARY.Add(44001, "多媒体文件为空");
            RETURNCIDEDICTIONARY.Add(44002, "POST的数据包为空");
            RETURNCIDEDICTIONARY.Add(44003, "图文消息内容为空");
            RETURNCIDEDICTIONARY.Add(44004, "文本消息内容为空");
            RETURNCIDEDICTIONARY.Add(45001, "多媒体文件大小超过限制");
            RETURNCIDEDICTIONARY.Add(45002, "消息内容超过限制");
            RETURNCIDEDICTIONARY.Add(45003, "标题字段超过限制");
            RETURNCIDEDICTIONARY.Add(45004, "描述字段超过限制");
            RETURNCIDEDICTIONARY.Add(45005, "链接字段超过限制");
            RETURNCIDEDICTIONARY.Add(45006, "图片链接字段超过限制");
            RETURNCIDEDICTIONARY.Add(45007, "语音播放时间超过限制");
            RETURNCIDEDICTIONARY.Add(45008, "图文消息超过限制");
            RETURNCIDEDICTIONARY.Add(45009, "接口调用超过限制");
            RETURNCIDEDICTIONARY.Add(45010, "创建菜单个数超过限制");
            RETURNCIDEDICTIONARY.Add(45015, "回复时间超过限制");
            RETURNCIDEDICTIONARY.Add(45016, "系统分组，不允许修改");
            RETURNCIDEDICTIONARY.Add(45017, "分组名字过长");
            RETURNCIDEDICTIONARY.Add(45018, "分组数量超过上限");
            RETURNCIDEDICTIONARY.Add(46001, "不存在媒体数据");
            RETURNCIDEDICTIONARY.Add(46002, "不存在的菜单版本");
            RETURNCIDEDICTIONARY.Add(46003, "不存在的菜单数据");
            RETURNCIDEDICTIONARY.Add(46004, "不存在的用户");
            RETURNCIDEDICTIONARY.Add(47001, "解析JSON/XML内容错误");
            RETURNCIDEDICTIONARY.Add(48001, "api功能未授权");
            RETURNCIDEDICTIONARY.Add(50001, "用户未授权该api");
        }
    }
}
