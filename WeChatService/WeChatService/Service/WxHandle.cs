using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeChatService.Consts;
using WeChatService.Extentions;
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

        public JsSdk JsSdk
        {
            get { return _provider.Instance.JsSdk; }
        }

        public async Task<string> GetAccessToken()
        {
            //取json文件
            if (_provider.Instance.WeChat.RefreshDate > DateTime.Now && !string.IsNullOrEmpty(_provider.Instance.WeChat.AccessToken))
            {
                return _provider.Instance.WeChat.AccessToken;
            }
            var responseStr = await HttpClient.GetStringAsync(UriFormat.GetAccessTokenUri(WeChat.AppID, WeChat.Secret));
            var jObject = JsonConvert.DeserializeObject<JObject>(responseStr);

            if (!jObject.Property("errcode").HasError())
            {
                _provider.SaveAccessToken(jObject["access_token"].ToString());
                return jObject["access_token"].ToString();
            }
            return responseStr;

        }

        public async Task GetWxWebAuthenticationCode(string accessToken, string redirectUrl)
        {
            var t = await HttpClient.GetStringAsync(UriFormat.GetWxWebAuthenticationCodeUri(_provider.Instance.WeChat.AppID, redirectUrl));
        }

        public async Task<string> GetWxWebAuthenticationAccessToken(string code, Action<string> whenFirstCall = null)
        {
            //取json文件
            if (_provider.Instance.JsSdk.AuthenticationTokenRefreshDate > DateTime.Now && !string.IsNullOrEmpty(_provider.Instance.JsSdk.AccessToken))
            {
                return _provider.Instance.JsSdk.AccessToken;
            }
            var responseStr = await HttpClient.GetStringAsync(UriFormat.GetWxWebAuthenticationAccessTokenUri(code, WeChat.AppID, WeChat.Secret));
            var response = JsonConvert.DeserializeObject<JObject>(responseStr);

            if (!response.Property("errcode").HasError())
            {
                _provider.SaveAuthenticationAccessToken(response["access_token"].ToString());
                if (string.IsNullOrEmpty(_provider.Instance.JsSdk.AccessToken))
                    whenFirstCall.Invoke(response["openid"].ToString());
                return response["access_token"].ToString();
            }
            return responseStr;
        }

        public async Task<string> GetTicket(string accessToken)
        {
            //取json文件
            if (_provider.Instance.JsSdk.RefreshDate > DateTime.Now && !string.IsNullOrEmpty(_provider.Instance.JsSdk.JsApiTicket))
            {
                return _provider.Instance.JsSdk.JsApiTicket;
            }
            var responseStr = await HttpClient.GetStringAsync(UriFormat.GetTicketUri(accessToken));
            var response = JsonConvert.DeserializeObject<JObject>(responseStr);

            if (!response.Property("errcode").HasError())
            {
                _provider.SaveTicket(response["ticket"].ToString());
                return response["ticket"].ToString();
            }
            return responseStr;
        }

        public async Task<bool> ValidateOpenid(string openid)
        {
            var responseStr = await HttpClient.GetStringAsync(UriFormat.GetValidOpenid(openid, JsSdk.AccessToken));
            var response = JsonConvert.DeserializeObject<JObject>(responseStr);

            return !response.Property("errcode").HasError();
        }



        public HttpClient HttpClient
        {
            get
            {
                if (_httpClient == null)
                {
                    _httpClient = new HttpClient();
                }
                return _httpClient;
            }
        }

        internal class UriFormat
        {
            internal static string GetAccessTokenUri(string appid, string secret)
            {
                return string.Format(WeChatConstsApiUri.AccessTokenUri, appid, secret);
            }

            internal static string GetWxWebAuthenticationCodeUri(string appid, string redirectUrl, string scope = "snsapi_base", string state = "")
            {
                return string.Format(WeChatConstsApiUri.WxWebDevCodeUri, appid, redirectUrl, scope, state);
            }

            internal static string GetTicketUri(string accessToken)
            {
                return string.Format(WeChatConstsApiUri.WxWebTicketUri, accessToken);
            }

            internal static string GetWxWebAuthenticationAccessTokenUri(string code, string appid, string secret)
            {
                return string.Format(WeChatConstsApiUri.WxWebAuthenticationAccessTokenUri, appid, secret);
            }

            internal static string GetValidOpenid(string openid,string accessToken)
            {
                return string.Format(WeChatConstsApiUri.ValidateOpenIdUri, openid, accessToken);
            }
        }
    }
}
