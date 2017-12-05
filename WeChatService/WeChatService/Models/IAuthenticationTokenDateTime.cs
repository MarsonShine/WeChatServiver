using System;

namespace WeChatService.Models
{
    interface IAuthenticationTokenDateTime
    {
        DateTime AuthenticationTokenCreateDate { get; set; }
        DateTime AuthenticationTokenRefreshDate { get; set; }
    }
}
