using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using 禁漫天堂.Models;

namespace 禁漫天堂.Pages
{
    public partial class 漫画List : Page
    {
        public string TagName { get; set; }  // 存储标签名称

        protected void Page_Load(object sender, EventArgs e)
        {
            // 获取标签名称（从 URL 查询字符串获取）
            TagName = Request.QueryString["tag"];

            // 如果标签名称有效且不是页面回发时，绑定数据
            if (!string.IsNullOrEmpty(TagName) && !IsPostBack)
            {
                // 根据标签名称查询符合条件的漫画
                List<Comic> comics = GetComicsByTag(TagName);

                // 将查询结果绑定到 Repeater 控件
                Repeater1.DataSource = comics;
                Repeater1.DataBind();
            }
        }

        public List<Comic> GetComicsByTag(string tagName)
        {
            List<Comic> comics = new List<Comic>();
            DB db = new DB(); // 创建 DB 类实例

            string sqlQuery = @"
                SELECT c.ComicID, c.标题, c.封面URL, c.作者
                FROM Comics c
                JOIN Comic标签 ct ON c.ComicID = ct.ComicID
                JOIN 标签 t ON ct.标签ID = t.标签ID
                WHERE t.标签Name = @TagName";

            SqlParameter[] parameters = {
                new SqlParameter("@TagName", SqlDbType.NVarChar) { Value = tagName }
            };

            DataSet ds = db.GetDataTableBySql(sqlQuery, parameters); // 使用 DB 实例调用方法


            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    comics.Add(new Comic
                    {
                        ComicID = row["ComicID"].ToString(),
                        标题 = row["标题"].ToString(),
                        封面URL = row["封面URL"].ToString(),
                        作者 = row["作者"].ToString()
                    });
                }
            }

            return comics;
        }
    }
}
