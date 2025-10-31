 using System;
using 禁漫天堂.Models;
using 禁漫天堂;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

public partial class ComicDetail : System.Web.UI.Page
{
    public string ComicTitle { get; set; }
    public string ComicCoverUrl { get; set; }
    public string ComicAuthor { get; set; }
    public List<string> ComicTags { get; set; }
    public string ComicRating { get; set; }
    public string ComicDescription { get; set; }
    public string ComicPublishDate { get; set; }
    public string ReadingLink { get; set; } // 添加 ReadingLink 属性

    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (Request.QueryString["id"] != null)
        {
            string comicId = Request.QueryString["id"]; // 获取查询字符串中的 comicId

            if (string.IsNullOrEmpty(comicId))
            {
                Response.Redirect("404.aspx", false);
            }
            else
            {
                LoadComicDetails(comicId); // 传递字符串类型的 comicId
            }
        }
        else
        {
            Response.Redirect("404.aspx", false); // 如果没有提供ID，重定向到404页面
        }
    }

    private void LoadComicDetails(string Id)
    {
        DB db = new DB();
        Comic comic = db.GetComicDetails(Id);

        if (comic != null)
        {
            ComicTitle = comic.标题;
            ComicCoverUrl = comic.封面URL;
            ComicAuthor = comic.作者;

            // 调用 GetComicTags 方法获取标签
            ComicTags = db.GetComicTags(Id);
            if (ComicTags == null || ComicTags.Count == 0)
            {
                ComicTags = new List<string> { "暂无标签" };
            }

            ComicRating = comic.评分.ToString("0.0");
            ComicDescription = comic.简介;
            ComicPublishDate = comic.发布日期.ToString("yyyy-MM-dd");
            ReadingLink = comic.ReadingLink;
            Random rand = new Random();
            string randomColor = string.Format("#{0:X6}", rand.Next(0x1000000));


        }
        else
        {
            Response.Redirect("404.aspx", false);
        }
    }
    protected void AddToFavorites_Click(object sender, EventArgs e)
    {
            // 判断用户是否已登录
            if (IsUserLoggedIn())
        {
            
            string userID = Session["UserID"].ToString();  // 获取登录用户的ID
            string comicID = Request.QueryString["id"];  // 获取当前漫画的ID

            if (!IsComicAlreadyFavorited(userID, comicID))
            {
                // 添加到收藏表
                AddComicToFavorites(userID, comicID);
                // 显示提示或更新界面
                Response.Write("<script>alert('收藏成功！');</script>");
            }
            else
            {
                Response.Write("<script>alert('您已经收藏过该漫画！');</script>");
            }
        }
        else
        {
            Response.Redirect("会员登陆.aspx", false);  // 未登录，跳转到登录页面
        }
        

    }

    // 判断当前漫画是否已经被收藏
    private bool IsComicAlreadyFavorited(string userID, string comicID)
    {
        DB db = new DB(); // 创建 DB 类实例
        string sql = "SELECT COUNT(*) FROM Comic收藏 WHERE 用户名 = @UserID AND ComicID = @ComicID";

        SqlParameter[] parameters = {
        new SqlParameter("@UserID", SqlDbType.Char) { Value = userID },
        new SqlParameter("@ComicID", SqlDbType.NVarChar) { Value = comicID }
    };

        // 获取返回的 DataSet
        DataSet ds = db.GetDataTableBySql(sql, parameters);

        // 检查 DataSet 是否包含数据表，并获取第一张表
        if (ds.Tables.Count > 0)
        {
            // 获取 DataTable 的第一张表
            DataTable dt = ds.Tables[0];

            // 检查 DataTable 是否有数据行
            if (dt.Rows.Count > 0)
            {
                // 获取 COUNT 的值
                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count > 0; // 如果 count 大于 0，表示漫画已被收藏
            }
        }

        return false; // 如果没有找到数据，表示漫画未被收藏
    }


    // 添加漫画到收藏
    private void AddComicToFavorites(string userID, string comicID)
    {
       
        DB db = new DB();
        string sql = "INSERT INTO Comic收藏 (用户名, ComicID) VALUES (@UserID, @ComicID)";
        SqlParameter[] parameters = {
        new SqlParameter("@UserID", SqlDbType.NVarChar) { Value = userID },
        new SqlParameter("@ComicID", SqlDbType.NVarChar) { Value = comicID }
    };

        db.UpdateDataBySql(sql, parameters);  // 假设你有一个执行更新操作的方法
    
    }
    // 判断用户是否登录
    private bool IsUserLoggedIn()
    {
        return Session["UserID"] != null;
    }


}
