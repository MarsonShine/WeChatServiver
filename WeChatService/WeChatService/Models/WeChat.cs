using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatService.Models
{
    public class WeChat : IToken, ITokenDateTime
    {
        public string AppID { get; set; }
        public string Secret { get; set; }

        public string AccessToken { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime RefreshDate { get; set; }

    }
}
