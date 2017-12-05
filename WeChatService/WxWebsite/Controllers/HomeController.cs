using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChatService.Service;

namespace WxWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly WxHandle _wx;
        private readonly WeChatConfigurationProvider _provider;
        public HomeController()
        {
            _wx = new WxHandle();
            _provider = new WeChatConfigurationProvider(System.Web.Hosting.HostingEnvironment.MapPath("~/wx.config.json"));
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Authentication()
        {
            //获取token
            var accessToken = _wx.GetAccessToken().GetAwaiter().GetResult();
            _wx.GetWxWebAuthenticationCode(accessToken, "http://localhost:8081/Home/AuthenticationCode").GetAwaiter().GetResult();
            return View();
        }

        public ActionResult AuthenticationCode()
        {
            return new EmptyResult();
        }
    }
}