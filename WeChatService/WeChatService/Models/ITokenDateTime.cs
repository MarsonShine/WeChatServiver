using System;

namespace WeChatService
{
    public interface ITokenDateTime
    {
        DateTime CreateDate { get; set; }

        DateTime RefreshDate { get; set; }
    }
}
