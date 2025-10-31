using System;
using System.Web;
using System.Web.Http;

namespace 禁漫天堂
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            // 应用程序启动时的逻辑
        }
    }
}
