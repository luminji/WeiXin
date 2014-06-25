using System;
using System.IO;
using System.Xml.Linq;
using WeiXin.Utilitys;

namespace WeiXin.Config
{
    /// <summary>
    /// 全局配置
    /// </summary>
    public class GlobalConfig
    {
        private static bool _IsConfigOk;
        /// <summary>
        /// 获取网站配置是否正确
        /// </summary>
        public static bool IsConfigOk
        {
            get
            {
                return _IsConfigOk;
            }
        }

        private static string _WeiXin_Token;
        /// <summary>
        /// Token
        /// </summary>
        public static string WeiXin_Token
        {
            get
            {
                return _WeiXin_Token;
            }
        }

        private static string _WeiXin_AppId;
        /// <summary>
        /// appID
        /// </summary>
        public static string WeiXin_AppId
        {
            get
            {
                return _WeiXin_AppId;
            }
        }

        private static string _WeiXin_AppSecret;
        /// <summary>
        /// appsecret
        /// </summary>
        public static string WeiXin_AppSecret
        {
            get
            {
                return _WeiXin_AppSecret;
            }
        }
        private static string _WeiXin_CustomerServiceApi;
        /// <summary>
        /// 客服消息接口
        /// </summary>
        public static string WeiXin_CustomerServiceApi
        {
            get
            {
                return _WeiXin_CustomerServiceApi;
            }
        }

        private static string _WeiXin_AccessTokenApi;
        /// <summary>
        /// 获取 accessToken 接口
        /// </summary>
        public static string WeiXin_AccessTokenApi
        {
            get
            {
                return _WeiXin_AccessTokenApi;
            }
        }

        private static string _WeiXin_UpdateEnable;
        /// <summary>
        /// 系统维护升级状态
        /// </summary>
        public static string WeiXin_UpdateEnable
        {
            get
            {
                return _WeiXin_UpdateEnable;
            }
        }

        static GlobalConfig()
        {
            ReadConfig();
        }

        private static void ReadConfig()
        {
            _IsConfigOk = true;
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var webConfigFileName = Path.Combine(baseDir, @"bin\Config.xml");
            var appConfigFileName = Path.Combine(baseDir, "Config.xml");
            var configFileName = string.Empty;
            if (File.Exists(webConfigFileName))
            {
                configFileName = webConfigFileName;
            }
            else if (File.Exists(appConfigFileName))
            {
                configFileName = appConfigFileName;
            }
            else
            {
                LogHelper.Log("找不到配置文件");
            }
            if (!string.IsNullOrEmpty(configFileName))
            {
                try
                {
                    using (FileStream fs = new FileStream(configFileName, FileMode.Open, FileAccess.Read))
                    {
                        XElement xmlElement = XElement.Load(fs);
                        XElement weiXinElement = xmlElement.Element("WeiXin");
                        if (weiXinElement != null)
                        {
                            #region 读取微信接口配置
                            var tokenElement = weiXinElement.Element("Token");
                            if (tokenElement == null)
                            {
                                _IsConfigOk = false;
                                LogHelper.Log("缺少 WeiXin>Token 配置");
                            }
                            else
                            {
                                _WeiXin_Token = tokenElement.Value.Trim();
                                if (string.IsNullOrEmpty(WeiXin_Token))
                                {
                                    _IsConfigOk = false;
                                    LogHelper.Log("缺少 WeiXin>Token 配置");
                                }
                            }

                            var appIdElement = weiXinElement.Element("AppId");
                            if (appIdElement == null)
                            {
                                _IsConfigOk = false;
                                LogHelper.Log("缺少 WeiXin>AppId 配置");
                            }
                            else
                            {
                                _WeiXin_AppId = appIdElement.Value.Trim();
                                if (string.IsNullOrEmpty(WeiXin_AppId))
                                {
                                    _IsConfigOk = false;
                                    LogHelper.Log("缺少 WeiXin>AppId 配置");
                                }
                            }

                            var appSecretElement = weiXinElement.Element("AppSecret");
                            if (appSecretElement == null)
                            {
                                _IsConfigOk = false;
                                LogHelper.Log("缺少 WeiXin>AppSecret 配置");
                            }
                            else
                            {

                                _WeiXin_AppSecret = appSecretElement.Value.Trim();
                                if (string.IsNullOrEmpty(WeiXin_AppSecret))
                                {
                                    _IsConfigOk = false;
                                    LogHelper.Log("缺少 WeiXin>AppSecret 配置");
                                }
                            }

                            var customerServiceApiElement = weiXinElement.Element("WeiXinCustomerServiceApi");
                            if (customerServiceApiElement == null)
                            {
                                _IsConfigOk = false;
                                LogHelper.Log("缺少 WeiXin>WeiXinCustomerServiceApi 配置");
                            }
                            else
                            {
                                _WeiXin_CustomerServiceApi = customerServiceApiElement.Value.Trim();
                                if (string.IsNullOrEmpty(WeiXin_CustomerServiceApi))
                                {
                                    _IsConfigOk = false;
                                    LogHelper.Log("缺少 WeiXin>WeiXinCustomerServiceApi 配置");
                                }
                            }

                            var accessTokenApiElement = weiXinElement.Element("WeiXinAccessTokenApi");
                            if (accessTokenApiElement == null)
                            {
                                _IsConfigOk = false;
                                LogHelper.Log("缺少 WeiXin>WeiXinAccessTokenApi 配置");
                            }
                            else
                            {
                                _WeiXin_AccessTokenApi = accessTokenApiElement.Value.Trim();
                                if (string.IsNullOrEmpty(WeiXin_AccessTokenApi))
                                {
                                    _IsConfigOk = false;
                                    LogHelper.Log("缺少 WeiXin>WeiXinAccessTokenApi 配置");
                                }
                            }

                            var updateEnableElement = weiXinElement.Element("UpdateEnable");
                            if (updateEnableElement == null)
                            {
                                _IsConfigOk = false;
                                LogHelper.Log("缺少 WeiXin>UpdateEnable 配置");
                            }
                            else
                            {
                                _WeiXin_UpdateEnable = updateEnableElement.Value.Trim();
                                if (string.IsNullOrEmpty(_WeiXin_UpdateEnable))
                                {
                                    _IsConfigOk = false;
                                    LogHelper.Log("缺少 WeiXin>UpdateEnable 配置");
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            _IsConfigOk = false;
                            LogHelper.Log("配置文件错误");
                        }
                    }
                }
                catch (Exception e)
                {
                    _IsConfigOk = false;
                    LogHelper.LogError(e);
                    LogHelper.Log("读取配置文件失败");
                }
            }
        }

        /// <summary>
        /// 重新加载配置
        /// </summary>
        public static void ReReadConfig()
        {
            ReadConfig();
        }
    }
}
