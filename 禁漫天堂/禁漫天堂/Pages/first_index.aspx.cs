using System;
using System.IO;
using System.Web.UI;

namespace 禁漫天堂
{
    public partial class first_index : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("系统当前时间是 " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "<br>");

            if (!IsPostBack)
            {
                string countFilePath = Server.MapPath("count.txt");
                if (!File.Exists(countFilePath))
                {
                    File.WriteAllText(countFilePath, "0");
                }

                int count;
                using (StreamReader reader = File.OpenText(countFilePath))
                {
                    string countStr = reader.ReadLine();
                    count = int.TryParse(countStr, out count) ? count : 0;
                }

                Application.Lock();
                Application["count"] = count + 1;
                Application.UnLock();

                Labinfo.Text = Application["count"].ToString();
            }

            if (DateTime.Now.Hour < 8 || DateTime.Now.Hour > 22)
            {
                Response.Write("本网站此时段停止开放<br>");
                Response.Write("开放时间为上午 8:00 ~ 下午 22:00");
                Response.End();
            }
            else
            {
                // 延迟重定向 5 秒
                Response.Write("<script>setTimeout(function() { window.location.href = '会员登陆.aspx'; }, 5000);</script>");
                Response.Write("您将在 5 秒后跳转到登陆页面...");
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            string countFilePath = Server.MapPath("count.txt");
            if (Application["count"] != null)
            {
                File.WriteAllText(countFilePath, Application["count"].ToString());
            }
        }
    }
}
