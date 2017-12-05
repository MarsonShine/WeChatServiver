using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeChatService.Consts;
using WeChatService.Models;

namespace WeChatService.Service
{
    public class WxHandle
    {
        private HttpClient _httpClient;
        private WeChatConfigurationProvider _provider;

        public WxHandle()
        {
            _provider = new WeChatConfigurationProvider();
        }

        public WeChat WeChat
        {
            get { return _provider.Instance.WeChat; }
        }

        public async Task<string> GetAccessToken()
        {
            //取json文件
            if(_provider.Instance.WeChat.RefreshDate > DateTime.Now && !string.IsNullOrEmpty(_provider.Instance.WeChat.AccessToken))
            {
                return _provider.Instance.WeChat.AccessToken;
            }
            var responseStr = await HttpClient.GetStringAsync(UriFormat.GetAccessTokenUri(WeChat.AppID, WeChat.Secret));
            var jObject = JsonConvert.DeserializeObject<JObject>(responseStr);

            if (jObject.Property("errcode") == null || string.IsNullOrEmpty(jObject.Property("errcode").ToString()))
            {
                _provider.SaveAccessToken(jObject["access_token"].ToString());
                return jObject["access_token"].ToString();
            }
            return responseStr;

        }

        public async Task GetWxCode(string accessToken,string redirectUrl)
        {
            var t = await HttpClient.GetStringAsync(UriFormat.GetWxWebCodeUri(_provider.Instance.WeChat.AppID, redirectUrl));
        }

        public HttpClient HttpClient
        {
            get
            {
                if(_httpClient == null)
                {
                    _httpClient = new HttpClient();
                }
                return _httpClient;
            }
        }

        internal class UriFormat
        {
            internal static string GetAccessTokenUri(string appid,string secret)
            {
                return string.Format(WeChatConstsApiUri.AccessTokenUri, appid, secret);
            }

            internal static string GetWxWebCodeUri(string appid,string redirectUrl,string scope= "snsapi_base",string state = "")
            {
                return string.Format(WeChatConstsApiUri.WxWebDevCodeUri, appid, redirectUrl, scope, state);
            }
        }
    }
}
