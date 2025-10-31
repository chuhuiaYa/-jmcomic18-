using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 禁漫天堂.Pages
{
    public partial class _404 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = Request.QueryString["errorMessage"];
                Response.Write("Error Message: " + errorMessage);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = "没有提供错误信息";
                }
                else
                {
                    errorMessage = Server.UrlDecode(errorMessage); // 解码错误信息
                }

                Labinfo.Text = errorMessage;  // 将错误信息显示在 Label 控件中
            }
            catch (Exception ex)
            {
                // 如果 404 页面本身发生错误，输出错误信息
                Response.Write($"Error loading 404 page: {ex.Message}");
            }
        }
    }
}