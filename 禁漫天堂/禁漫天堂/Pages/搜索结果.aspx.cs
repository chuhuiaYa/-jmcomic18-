using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace 禁漫天堂.Pages
{
    public partial class 搜索结果 : Page
    {
        protected string searchTerm;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 获取搜索词
                searchTerm = Request.QueryString["query"];

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showLoading", "showLoading();", true);


                    // 执行搜索逻辑


                    DB db = new DB();
                    string sqlQuery = "SELECT ComicID, 标题, 封面URL, 简介 FROM Comics WHERE 标题 LIKE @SearchTerm";
                    SqlParameter[] parameters = {
                        new SqlParameter("@SearchTerm", "%" + searchTerm + "%")
                    };

                    DataSet ds = db.GetDataTableBySql(sqlQuery, parameters);


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideLoading", "hideLoading();", true);


                    // 将结果绑定到Repeater控件
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        RepeaterResults.DataSource = ds.Tables[0];
                        RepeaterResults.DataBind();
                    }
                    else
                    {
                        RepeaterResults.DataSource = null;
                        RepeaterResults.DataBind();
                        LiteralMessage.Text = "<p>没有找到相关漫画。</p>";

                    }
                }
                else
                {
                    // 如果没有查询内容，跳转到首页
                    Response.Redirect("/Pages/首页.aspx");
                }
            }
        }
    }
}
