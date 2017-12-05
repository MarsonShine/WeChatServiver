using System;

namespace WeChatService.Models
{
    public class JsSdk : ITokenDateTime
    {
        public string JsApiTicket { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime RefreshDate { get; set; }
    }
}
