using Newtonsoft.Json;
using System.IO;
using System.Text;
using WeChatService.Configuration;
using System;

namespace WeChatService.Service
{
    public class WeChatConfigurationProvider
    {
        private readonly string _configJsonUrl = "../../wx.config.json";
        private WeChatConfiguration _configuration;

        public WeChatConfigurationProvider(string configJsonUri = null)
        {
            _configJsonUrl = configJsonUri ?? _configJsonUrl;
        }

        public WeChatConfiguration CreateConfiguration()
        {
            using (var fileStream = new FileStream(_configJsonUrl, FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
            {
                using (var streamReader = new StreamReader(fileStream,Encoding.UTF8))
                {
                    string json = streamReader.ReadToEnd();
                    _configuration = JsonConvert.DeserializeObject<WeChatConfiguration>(json);
                    return _configuration;
                }
            }
        }

        public void SaveAccessToken(string access_token)
        {
            _configuration.WeChat.AccessToken = access_token;
            _configuration.WeChat.CreateDate = DateTime.Now;
            _configuration.WeChat.RefreshDate = DateTime.Now.AddSeconds(7000);
            SaveJsonConfigFile(_configuration);
        }

        public void SaveTicket(string ticket)
        {
            _configuration.JsSdk.JsApiTicket = ticket;
            _configuration.JsSdk.CreateDate = DateTime.Now;
            _configuration.JsSdk.RefreshDate = DateTime.Now.AddSeconds(7000);
            SaveJsonConfigFile(_configuration);
        }

        public void SaveAuthenticationAccessToken(string accessToken)
        {
            _configuration.JsSdk.AccessToken = accessToken;
            _configuration.JsSdk.AuthenticationTokenCreateDate = DateTime.Now;
            _configuration.JsSdk.AuthenticationTokenRefreshDate = DateTime.Now.AddSeconds(7000);
            SaveJsonConfigFile(_configuration);

        }

        public WeChatConfiguration Instance
        {
            set
            {
                _configuration = value;
            }
            get
            {
                if(_configuration == null)
                {
                    _configuration = CreateConfiguration();
                }
                return _configuration;
            }
        }


        private void SaveJsonConfigFile(WeChatConfiguration configuration)
        {
            //写入json文件
            File.WriteAllText(_configJsonUrl, JsonConvert.SerializeObject(configuration,Formatting.Indented,new JsonSerializerSettings()));
        }     
    }
}
