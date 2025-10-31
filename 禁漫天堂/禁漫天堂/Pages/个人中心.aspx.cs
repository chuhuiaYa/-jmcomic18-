using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using 禁漫天堂.Models;
using System.Security.Cryptography;
using System.Text;

namespace 禁漫天堂.Pages
{
    public partial class UserCenter : Page
    {
        // 用户信息
        public string UserName;
        public string UserEmail;
        public string RegistrationDate;
        public string AvatarUrl = "/Content/images/default-avatar.jpg"; // 默认头像路径

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserInfo();
                BindFavorites();
            }
        }

        // 加载用户信息
        private void LoadUserInfo()
        {
            // 假设用户 ID 已经存储在 Session 中
            string userId = Session["UserID"]?.ToString();

            // 如果没有用户 ID，跳转到登录页
            if (string.IsNullOrEmpty(userId))
            {
                Response.Redirect("会员登陆.aspx");
            }

            // 使用 DB 工具类获取用户信息
            DB db = new DB();
            string sqlQuery = "SELECT 姓名 AS UserName, 邮政编码 AS UserEmail, 出生日期 AS RegistrationDate, 头像路径 AS AvatarUrl FROM 会员表 WHERE 用户名 = @UserID";
            SqlParameter[] parameters = {
                new SqlParameter("@UserID", userId)
            };

            DataSet ds = db.GetDataTableBySql(sqlQuery, parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow user = ds.Tables[0].Rows[0];

                // 绑定用户数据
                UserName = user["UserName"].ToString();
                UserEmail = user["UserEmail"].ToString();
                RegistrationDate = Convert.ToDateTime(user["RegistrationDate"]).ToString("yyyy-MM-dd");

                // 获取头像路径
                AvatarUrl = string.IsNullOrEmpty(user["AvatarUrl"].ToString()) ? "/Content/images/default-avatar.jpg" : user["AvatarUrl"].ToString();
            }
            else
            {
                // 如果没有找到用户信息，显示错误或跳转
                Response.Redirect("404.aspx");
            }
        }

        // 上传头像
        protected void ButtonUpload_Click(object sender, EventArgs e)
        {
            if (FileUploadAvatar.HasFile)
            {
                try
                {
                    // 验证文件扩展名
                    string extension = System.IO.Path.GetExtension(FileUploadAvatar.FileName).ToLower();
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (!Array.Exists(allowedExtensions, ext => ext == extension))
                    {
                        Response.Write("<script>alert('仅支持 JPG、PNG 和 GIF 格式的图片！');</script>");
                        return;
                    }

                    // 保存文件到服务器
                    string userId = Session["UserID"].ToString();
                    string fileName = "avatar_" + userId + extension;
                    string savePath = Server.MapPath("/Content/UserAvatars/") + fileName;

                    FileUploadAvatar.SaveAs(savePath);

                    // 更新数据库中的头像路径
                    DB db = new DB();
                    string sqlUpdate = "UPDATE 会员表 SET 头像路径 = @AvatarPath WHERE 用户名 = @UserID";
                    SqlParameter[] parameters = {
                        new SqlParameter("@AvatarPath", "/Content/UserAvatars/" + fileName),
                        new SqlParameter("@UserID", userId)
                    };

                    if (db.UpdateDataBySql(sqlUpdate, parameters))
                    {
                        // 更新成功后重新加载页面
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        Response.Write("<script>alert('头像更新失败，请稍后重试！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"<script>alert('上传失败：{ex.Message}');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('请选择一张图片！');</script>");
            }
        }
        private void BindFavorites()
        {
            // 从 Session 获取当前登录用户的 ID
            string userID = Session["UserID"]?.ToString(); // 使用实际的 Session 值

            if (string.IsNullOrEmpty(userID))
            {
                Response.Redirect("会员登陆.aspx"); // 如果未登录，重定向到登录页
            }

            // 获取当前用户的收藏漫画
            List<Comic> favorites = GetUserFavorites(userID);

            // 绑定收藏的漫画到 Repeater 控件
            RepeaterFavorites.DataSource = favorites;
            RepeaterFavorites.DataBind();
        }
        private List<Comic> GetUserFavorites(string userID)
        {
            List<Comic> favorites = new List<Comic>();
            DB db = new DB();
            string sql = @"
            SELECT c.ComicID, c.标题, c.封面URL, c.作者
            FROM Comic收藏 uf
            JOIN Comics c ON uf.ComicID = c.ComicID
            WHERE uf.用户名 = @UserID";

            SqlParameter[] parameters = {
            new SqlParameter("@UserID", SqlDbType.NVarChar) { Value = userID }
            };

            DataSet ds = db.GetDataTableBySql(sql, parameters);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    favorites.Add(new Comic
                    {
                        ComicID = row["ComicID"].ToString(),
                        标题 = row["标题"].ToString(),
                        封面URL = row["封面URL"].ToString(),
                        作者 = row["作者"].ToString()
                    });
                }
            }

            return favorites;
        }
        // 修改密码
        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Value.Trim();
            string userId = Session["UserID"]?.ToString();

            if (string.IsNullOrEmpty(newPassword))
            {
                Response.Write("<script>alert('密码不能为空！');</script>");
                return;
            }

            // 对密码进行加密
            string encryptedPassword = SecurityHelper.EncryptPassword(newPassword);

            // 数据库操作：更新密码
            DB db = new DB();
            string sqlUpdatePassword = "UPDATE 会员表 SET 密码 = @NewPassword WHERE 用户名 = @UserID";
            SqlParameter[] parameters = {
            new SqlParameter("@NewPassword", encryptedPassword),
            new SqlParameter("@UserID", userId)
            };

            bool success = db.UpdateDataBySql(sqlUpdatePassword, parameters);

            if (success)
            {
                Response.Write("<script>alert('密码修改成功！');</script>");
            }
            else
            {
                Response.Write("<script>alert('密码修改失败，请稍后重试！');</script>");
            }
        }

        public static class SecurityHelper
        {
            // 使用SHA256加密
            public static string EncryptPassword(string password)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // 将密码转换为字节数组
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                    // 将字节数组转换为十六进制字符串
                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in bytes)
                    {
                        builder.Append(b.ToString("x2"));
                    }

                    return builder.ToString(); // 返回加密后的密码
                }
            }
        }
        // 修改邮政编码
        protected void ChangeEmail_Click(object sender, EventArgs e)
        {
            string newEmail = txtNewEmail.Value.Trim();
            string userId = Session["UserID"]?.ToString();

            if (string.IsNullOrEmpty(newEmail))
            {
                Response.Write("<script>alert('邮政编码不能为空！');</script>");
                return;
            }

            // 数据库操作：更新邮政编码
            DB db = new DB();
            string sqlUpdateEmail = "UPDATE 会员表 SET 邮政编码 = @NewEmail WHERE 用户名 = @UserID";
            SqlParameter[] parameters = {
            new SqlParameter("@NewEmail", newEmail),
            new SqlParameter("@UserID", userId)
            };

            bool success = db.UpdateDataBySql(sqlUpdateEmail, parameters);

            if (success)
            {
                Response.Write("<script>alert('邮政编码修改成功！');</script>");
            }
            else
            {
                Response.Write("<script>alert('邮政编码修改失败，请稍后重试！');</script>");
            }
        }
        // 修改绑定手机号
        protected void ChangePhone_Click(object sender, EventArgs e)
        {
            string newPhone = txtNewPhone.Value.Trim();
            string userId = Session["UserID"]?.ToString();

            if (string.IsNullOrEmpty(newPhone))
            {
                Response.Write("<script>alert('手机号不能为空！');</script>");
                return;
            }

            // 数据库操作：更新手机号
            DB db = new DB();
            string sqlUpdatePhone = "UPDATE 会员表 SET 联系电话 = @NewPhone WHERE 用户名 = @UserID";
            SqlParameter[] parameters = {
            new SqlParameter("@NewPhone", newPhone),
            new SqlParameter("@UserID", userId)
            };

            bool success = db.UpdateDataBySql(sqlUpdatePhone, parameters);

            if (success)
            {
                Response.Write("<script>alert('手机号修改成功！');</script>");
            }
            else
            {
                Response.Write("<script>alert('手机号修改失败，请稍后重试！');</script>");
            }
        }
    }
}
