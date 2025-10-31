using System;
using System.Web;
using 禁漫天堂;
using System.Data.SqlClient;

public partial class AddToFavorites : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // 获取传入的 comicId 和用户 ID（假设是存储在 Session 中的）
        string comicId = Request.Form["comicId"]; // 从 Ajax 请求中获取 ComicID
        int userId = 1;  // 假设用户 ID 是 1，你应该从 Session 或认证系统中获取用户 ID

        if (!string.IsNullOrEmpty(comicId))
        {
            DB db = new DB();  // 创建 DB 类实例
            bool isSuccess = db.AddComicToFavorites(userId, comicId);  // 调用 DB 类方法插入收藏记录

            // 返回结果
            if (isSuccess)
            {
                Response.Write("Success");  // 如果成功，返回成功信息
            }
            else
            {
                Response.Write("Failure");  // 如果失败，返回失败信息
            }
        }
        else
        {
            Response.Write("Failure");  // 如果 comicId 为空，返回失败信息
        }
    }
}
