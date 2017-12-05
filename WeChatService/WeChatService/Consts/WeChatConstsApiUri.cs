namespace WeChatService.Consts
{
    public class WeChatConstsApiUri
    {
        public const string AccessTokenUri = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        public const string WxWebDevCodeUri = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect";

        public const string WxWebTicketUri = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";

        public const string WxWebAuthenticationAccessTokenUri = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={1}&secret={2}&code={0}&grant_type=authorization_code";

        public const string ValidateOpenIdUri = "https://api.weixin.qq.com/sns/auth?access_token={1}&openid={0}";
    }
}
