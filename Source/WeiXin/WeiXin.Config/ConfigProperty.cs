﻿using System;
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
        private static string _WeiXin_GetUserListApi;
        public static string WeiXin_GetUserListApi
        {
            get
            {
                return _WeiXin_GetUserListApi;
            }
        }
        private static string _WeiXin_GetUserInfoApi;
        public static string WeiXin_GetUserInfoApi
        {
            get
            {
                return _WeiXin_GetUserInfoApi;
            }
        }
        private static string _WeiXin_AdvancedMassApi;
        public static string WeiXin_AdvancedMassApi
        {
            get
            {
                return _WeiXin_AdvancedMassApi;
            }
        }
        private static string _WeiXin_OAuth2AccessTokenApi;
        public static string WeiXin_OAuth2AccessTokenApi
        {
            get
            {
                return _WeiXin_OAuth2AccessTokenApi;
            }
        }
        private static string _WeiXin_OAuth2UserInfoApi;
        public static string WeiXin_OAuth2UserInfoApi
        {
            get
            {
                return _WeiXin_OAuth2UserInfoApi;
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
                        if (weiXinElement != null)
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

                            var getUserListApiElement = weiXinElement.Element("WeiXinGetUserListApi");
                            if (getUserListApiElement == null)
                            {
                                _IsConfigurationOk = false;
                                LogHelper.Log("缺少 WeiXin>WeiXinGetUserListApi 配置");
                            }
                            else
                            {
                                _WeiXin_GetUserListApi = getUserListApiElement.Value.Trim();
                                if (string.IsNullOrEmpty(_WeiXin_GetUserListApi))
                                {
                                    _IsConfigurationOk = false;
                                    LogHelper.Log("缺少 WeiXin>WeiXinGetUserListApi 配置");
                                }
                            }

                            var getUserInfoApiElement = weiXinElement.Element("WeiXinGetUserInfoApi");
                            if (getUserInfoApiElement == null)
                            {
                                _IsConfigurationOk = false;
                                LogHelper.Log("缺少 WeiXin>WeiXinGetUserInfoApi 配置");
                            }
                            else
                            {
                                _WeiXin_GetUserInfoApi = getUserInfoApiElement.Value.Trim();
                                if (string.IsNullOrEmpty(_WeiXin_GetUserInfoApi))
                                {
                                    _IsConfigurationOk = false;
                                    LogHelper.Log("缺少 WeiXin>WeiXinGetUserInfoApi 配置");
                                }
                            }

                            var advancedMassApiElement = weiXinElement.Element("WeiXinAdvancedMassApi");
                            if (advancedMassApiElement == null)
                            {
                                _IsConfigurationOk = false;
                                LogHelper.Log("缺少 WeiXin>WeiXinAdvancedMassApi 配置");
                            }
                            else
                            {
                                _WeiXin_AdvancedMassApi = advancedMassApiElement.Value.Trim();
                                if (string.IsNullOrEmpty(_WeiXin_AdvancedMassApi))
                                {
                                    _IsConfigurationOk = false;
                                    LogHelper.Log("缺少 WeiXin>WeiXinAdvancedMassApi 配置");
                                }
                            }

                            var oAuth2AccessTokenApiElement = weiXinElement.Element("WeiXinOAuth2AccessTokenApi");
                            if (oAuth2AccessTokenApiElement == null)
                            {
                                _IsConfigurationOk = false;
                                LogHelper.Log("缺少 WeiXin>WeiXinOAuth2AccessTokenApi 配置");
                            }
                            else
                            {
                                _WeiXin_OAuth2AccessTokenApi = oAuth2AccessTokenApiElement.Value.Trim();
                                if (string.IsNullOrEmpty(_WeiXin_OAuth2AccessTokenApi))
                                {
                                    _IsConfigurationOk = false;
                                    LogHelper.Log("缺少 WeiXin>WeiXinOAuth2AccessTokenApi 配置");
                                }
                            }

                            var oAuth2UserInfoApiElement = weiXinElement.Element("WeiXinOAuth2UserInfoApi");
                            if (oAuth2UserInfoApiElement == null)
                            {
                                _IsConfigurationOk = false;
                                LogHelper.Log("缺少 WeiXin>WeiXinOAuth2UserInfoApi 配置");
                            }
                            else
                            {
                                _WeiXin_OAuth2UserInfoApi = oAuth2UserInfoApiElement.Value.Trim();
                                if (string.IsNullOrEmpty(_WeiXin_OAuth2UserInfoApi))
                                {
                                    _IsConfigurationOk = false;
                                    LogHelper.Log("缺少 WeiXin>WeiXinOAuth2UserInfoApi 配置");
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
