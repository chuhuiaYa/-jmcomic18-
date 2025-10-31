using System.Data.SqlClient;
using System;

namespace 禁漫天堂.MasterPages
{
    public partial class ComicMaster : System.Web.UI.MasterPage
    {
        public string AvatarUrl = "/Content/images/default-avatar.jpg"; // 默认头像路径

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserAvatar();
            }
        }

        private void LoadUserAvatar()
        {
            // 假设用户 ID 存储在 Session 中
            string userId = Session["UserID"] != null ? Session["UserID"].ToString() : null;

            if (string.IsNullOrEmpty(userId))
            {
                return; // 未登录用户，保持默认头像
            }

            // 数据库查询获取头像路径
            DB db = new DB();
            string sqlQuery = "SELECT 头像路径 FROM 会员表 WHERE 用户名 = @UserID";
            SqlParameter[] parameters = {
                new SqlParameter("@UserID", userId)
            };

            var result = db.GetDataTableBySql(sqlQuery, parameters);
            if (result != null && result.Tables[0].Rows.Count > 0)
            {
                string avatarPath = result.Tables[0].Rows[0]["头像路径"].ToString();
                if (!string.IsNullOrEmpty(avatarPath))
                {
                    AvatarUrl = avatarPath; // 使用数据库中的头像路径
                }
            }
        }
    }
}
