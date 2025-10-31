using System;
using System.Collections.Generic;
using System.Web;
using 禁漫天堂.Models;
using 禁漫天堂;

namespace 禁漫天堂
{
    public partial class ReadComic : System.Web.UI.Page
    {
        public string ComicID { get; set; }
        public string ComicTitle { get; set; }
        public string ComicImageURL { get; set; }
        public List<string> ComicPages { get; set; }
        public string ComicContent { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                string comicId = Request.QueryString["Id"]; // 获取查询字符串中的 ComicID

                if (string.IsNullOrEmpty(comicId))
                {
                    // 错误信息：查询字符串中的 ComicID 为空
                    string errorMessage = "ComicID is empty.";
                    string encodedErrorMessage = Server.UrlEncode(errorMessage);  // URL 编码
                    string redirectUrl = Server.MapPath("~/404.aspx") + "?errorMessage=" + encodedErrorMessage;
                    Response.Redirect(redirectUrl);

                }
                else
                {
                    LoadComicDetails(comicId); // 获取漫画详情
                }
            }
            else
            {
                // 错误信息：没有提供 ComicID 参数
                string errorMessage = "ID parameter is missing in URL.";
                string encodedErrorMessage = Server.UrlEncode(errorMessage);  // URL 编码
                string redirectUrl = Server.MapPath("~/404.aspx") + "?errorMessage=" + encodedErrorMessage;
                Response.Redirect(redirectUrl);

            }
        }

        private void LoadComicDetails(string comicId)
        {
            DB db = new DB();
            Comic comic = db.GetComicDetails(comicId); // 获取漫画的基本信息

            if (comic != null)
            {
                // 获取漫画页面 URL 列表
                ComicPages = db.GetComicPages(comicId);

                // 设置页面数据
                ComicTitle = comic.标题;
                ComicImageURL = comic.封面URL;
                ComicContent = GetComicPagesHtml(); // 获取 HTML 格式的漫画页面内容
            }
            else
            {
                // 错误信息：没有找到漫画
                string errorMessage = "Comic not found in database: ComicID = " + comicId;
                string encodedErrorMessage = Server.UrlEncode(errorMessage);  // URL 编码
                string redirectUrl = Server.MapPath("~/404.aspx") + "?errorMessage=" + encodedErrorMessage;
                Response.Redirect(redirectUrl);

            }
        }

        private string GetComicPagesHtml()
        {
            string comicPagesHtml = "";

            // 遍历所有漫画页面，生成 HTML 内容
            foreach (string pageUrl in ComicPages)
            {
                comicPagesHtml += $"<img src='{pageUrl}' alt='Comic Page' class='comic-page-img' />"; // 生成页面图片
            }

            return comicPagesHtml;
        }
    }
}
