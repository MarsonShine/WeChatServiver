using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatService.Service;

namespace WeChatService
{
    class Program
    {
        static void Main(string[] args)
        {
            WxHandle wxHandle = new WxHandle();
            var access_token = wxHandle.GetAccessToken().GetAwaiter().GetResult();
            Console.WriteLine(access_token);

            var ticket = wxHandle.GetTicket(access_token).GetAwaiter().GetResult();
            Console.WriteLine(ticket);

            var tuple = wxHandle.GetWxWebAuthenticationAccessToken("",openid=> {
                //业务逻辑代码
            }).GetAwaiter().GetResult();
            //wxHandle.GetWxCode(access_token, "http://localhost:56956/FillBugsForBillDoc.aspx").GetAwaiter().GetResult();
            Console.ReadLine();
        }
    }
}
