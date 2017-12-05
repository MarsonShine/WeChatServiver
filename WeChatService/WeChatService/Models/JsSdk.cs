using System;

namespace WeChatService.Models
{
    public class JsSdk : IAuthenticationToken, IAuthenticationTokenDateTime,ITokenDateTime
    {
        public string JsApiTicket { get; set; }
        public string AccessToken { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime RefreshDate { get; set; }

        public DateTime AuthenticationTokenCreateDate { get; set; }
        public DateTime AuthenticationTokenRefreshDate { get; set; }
    }
}
