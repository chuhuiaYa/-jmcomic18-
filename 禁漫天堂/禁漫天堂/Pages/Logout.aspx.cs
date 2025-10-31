using System;
using System.Web;

namespace 禁漫天堂.Pages
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 清除会话信息
            Session.Clear();
            Session.Abandon();

            // 可选：清除认证票据（如果使用 FormsAuthentication）
            if (Request.Cookies[".ASPXAUTH"] != null)
            {
                HttpCookie authCookie = new HttpCookie(".ASPXAUTH")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Path = "/"
                };
                Response.Cookies.Add(authCookie);
            }

            // 重定向到登录页面或首页
            Response.Redirect("会员登陆.aspx");
        }
    }
}
