using System;
using System.IO;
using System.Xml.Linq;
using WeiXin.Utilitys;

namespace WeiXin.Config
{
    public sealed class ConfigProperty
    {
        static bool _IsConfigurationOk;
        public static bool IsConfigurationOk
        {
            get
            {
                return _IsConfigurationOk;
            }
        }
        private static string _WeiXin_Token;
        public static string WeiXin_Token
        {
            get
            {
                return _WeiXin_Token;
            }
        }
        private static string _WeiXin_AppId;
        public static string WeiXin_AppId
        {
            get
            {
                return _WeiXin_AppId;
            }
        }
        private static string _WeiXin_AppSecret;
        public static string WeiXin_AppSecret
        {
            get
            {
                return _WeiXin_AppSecret;
            }
        }
        private static string _WeiXin_CustomerServiceApi;
        public static string WeiXin_CustomerServiceApi
        {
            get
            {
                return _WeiXin_CustomerServiceApi;
            }
        }
        private static string _WeiXin_CreateQRApi;
        public static string WeiXin_CreateQRApi
        {
            get
            {
                return _WeiXin_CreateQRApi;
            }
        }
        private static string _WeiXin_AccessTokenApi;
        public static string WeiXin_AccessTokenApi
        {
            get
            {
                return _WeiXin_AccessTokenApi;
            }
        }
        private static bool _WeiXin_UpdateEnable;
        public static bool WeiXin_UpdateEnable
        {
            get
            {
                return _WeiXin_UpdateEnable;
            }
        }
        private static string _DataBase_ConnectionString;
        public static string DataBase_ConnectionString
        {
            get
            {
                return _DataBase_ConnectionString;
            }
        }
        static ConfigProperty()
        {
            ReadConfig();
        }
        private static void ReadConfig()
        {
            _IsConfigurationOk = true;
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
                        XElement dataBaseElement = xmlElement.Element("DataBase");
                        if (weiXinElement != null && dataBaseElement != null)
                        {
                            #region 读取微信接口配置
                            var tokenElement = weiXinElement.Element("Token");
                            if (tokenElement == null)
                            {
                                _IsConfigurationOk = false;
                                LogHelper.Log("缺少 WeiXin>Token 配置");
                            }
                            else
                            {
                                _WeiXin_Token = tokenElement.Value.Trim();
                                if (string.IsNullOrEmpty(_WeiXin_Token))
                                {
                                    _IsConfigurationOk = false;
                                    LogHelper.Log("缺少 WeiXin>Token 配置");
                                }
                            }

                            var appIdElement = weiXinElement.Element("AppId");
                            if (appIdElement == null)
                            {
                                _IsConfigurationOk = false;
                                LogHelper.Log("缺少 WeiXin>AppId 配置");
                            }
                            else
                            {
                                _WeiXin_AppId = appIdElement.Value.Trim();
                                if (string.IsNullOrEmpty(_WeiXin_AppId))
                                {
                                    _IsConfigurationOk = false;
                                    LogHelper.Log("缺少 WeiXin>AppId 配置");
                                }
                            }

                            var appSecretElement = weiXinElement.Element("AppSecret");
                            if (appSecretElement == null)
                            {
                                _IsConfigurationOk = false;
                                LogHelper.Log("缺少 WeiXin>AppSecret 配置");
                            }
                            else
                            {
                                _WeiXin_AppSecret = appSecretElement.Value.Trim();
                                if (string.IsNullOrEmpty(_WeiXin_AppSecret))
                                {
                                    _IsConfigurationOk = false;
                                    LogHelper.Log("缺少 WeiXin>AppSecret 配置");
                                }
                            }

                            var customerServiceApiElement = weiXinElement.Element("WeiXinCustomerServiceApi");
                            if (customerServiceApiElement == null)
                            {
                                _IsConfigurationOk = false;
                                LogHelper.Log("缺少 WeiXin>WeiXinCustomerServiceApi 配置");
                            }
                            else
                            {
                                _WeiXin_CustomerServiceApi = customerServiceApiElement.Value.Trim();
                                if (string.IsNullOrEmpty(_WeiXin_CustomerServiceApi))
                                {
                                    _IsConfigurationOk = false;
                                    LogHelper.Log("缺少 WeiXin>WeiXinCustomerServiceApi 配置");
                                }
                            }

                            var createQRApiElement = weiXinElement.Element("WeiXinCreateQRApi");
                            if (createQRApiElement == null)
                            {
                                _IsConfigurationOk = false;
                                LogHelper.Log("缺少 WeiXin>WeiXinCreateQRApi 配置");
                            }
                            else
                            {
                                _WeiXin_CreateQRApi = createQRApiElement.Value.Trim();
                                if (string.IsNullOrEmpty(_WeiXin_CreateQRApi))
                                {
                                    _IsConfigurationOk = false;
                                    LogHelper.Log("缺少 WeiXin>WeiXinCreateQRApi 配置");
                                }
                            }

                            var accessTokenApiElement = weiXinElement.Element("WeiXinAccessTokenApi");
                            if (accessTokenApiElement == null)
                            {
                                _IsConfigurationOk = false;
                                LogHelper.Log("缺少 WeiXin>WeiXinAccessTokenApi 配置");
                            }
                            else
                            {
                                _WeiXin_AccessTokenApi = accessTokenApiElement.Value.Trim();
                                if (string.IsNullOrEmpty(_WeiXin_AccessTokenApi))
                                {
                                    _IsConfigurationOk = false;
                                    LogHelper.Log("缺少 WeiXin>WeiXinAccessTokenApi 配置");
                                }
                            }

                            var updateEnableElement = weiXinElement.Element("UpdateEnable");
                            if (updateEnableElement == null)
                            {
                                _IsConfigurationOk = false;
                                LogHelper.Log("缺少 WeiXin>UpdateEnable 配置");
                            }
                            else
                            {
                                var tmp = updateEnableElement.Value.Trim();
                                if (string.IsNullOrEmpty(tmp))
                                {
                                    _IsConfigurationOk = false;
                                    LogHelper.Log("缺少 WeiXin>UpdateEnable 配置");
                                }
                                else
                                {
                                    if (!bool.TryParse(tmp, out _WeiXin_UpdateEnable))
                                    {
                                        _IsConfigurationOk = false;
                                        LogHelper.Log("WeiXin>UpdateEnable 配置错误");
                                    }
                                }
                            }
                            #endregion
                            #region 读取数据库配置
                            var connectionStringElement = dataBaseElement.Element("ConnectionString");
                            if (connectionStringElement == null)
                            {
                                _IsConfigurationOk = false;
                                LogHelper.Log("缺少 DataBase>ConnectionString 配置");
                            }
                            else
                            {
                                _DataBase_ConnectionString = connectionStringElement.Value.Trim();
                                if (string.IsNullOrEmpty(_DataBase_ConnectionString))
                                {
                                    _IsConfigurationOk = false;
                                    LogHelper.Log("缺少 DataBase>ConnectionString 配置");
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            _IsConfigurationOk = false;
                            LogHelper.Log("配置文件错误");
                        }
                    }
                }
                catch (Exception e)
                {
                    _IsConfigurationOk = false;
                    LogHelper.LogError(e);
                    LogHelper.Log("读取配置文件失败");
                }
            }
        }
        public static void ReReadConfig()
        {
            ReadConfig();
        }
    }
}
