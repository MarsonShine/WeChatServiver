namespace WeChatService.Consts
{
    public class WeChatConstsApiUri
    {
        public const string AccessTokenUri = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        public const string WxWebDevCodeUri = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect";
    }
}
