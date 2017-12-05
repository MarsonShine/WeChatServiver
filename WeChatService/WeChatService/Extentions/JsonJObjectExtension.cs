using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatService.Extentions
{
    public static class JsonJObjectExtension
    {
        public static bool HasError(this JProperty property)
        {
            return !(property == null
                || string.IsNullOrEmpty(property.ToString())
                || property.Value.ToString() == "0");
        }
    }
}
