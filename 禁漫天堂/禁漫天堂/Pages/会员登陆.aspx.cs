using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace 禁漫天堂
{
    public partial class 会员登陆 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 如果已经登录，跳转到个人中心
            if (Session["UserName"] != null)
            {
                Response.Redirect("UserCenter.aspx");
            }
        }

        protected void Btn_login_Click(object sender, EventArgs e)
        {
            string userName = Txt_user_name.Text.Trim();  // 获取用户名
            string userPwd = Txt_user_pwd.Text.Trim();    // 获取密码

            // 使用 SHA-256 加密用户密码
            string sha256UserPwd = HashPasswordSHA256(userPwd);  // 使用SHA-256加密密码

            // 创建数据库工具类实例
            DB db = new DB();

            // 防止 SQL 注入，使用参数化查询
            string sqlQuery = "SELECT * FROM 会员表 WHERE 用户名 = @UserName AND 密码 = @Password";
            SqlParameter[] parameters = {
            new SqlParameter("@UserName", userName),
            new SqlParameter("@Password", sha256UserPwd) // 使用 SHA-256 加密后的密码进行验证
            };

            // 获取查询结果
            try
            {
                DataSet ds = db.GetDataTableBySql(sqlQuery, parameters);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    Labinfo.Text = "用户名或密码错误，请重试";
                }
                else
                {
                    // 登录成功，保存登录状态
                    Labinfo.Text = "用户 " + userName + " 登录成功！";

                    // 将用户信息保存到 Session
                    Session["UserName"] = userName;
                    Session["UserID"] = ds.Tables[0].Rows[0]["用户名"].ToString(); // 假设数据库中有UserID字段

                    // 跳转到首页
                    Response.Redirect("首页.aspx");
                }
            }
            catch (Exception ex)
            {
                Labinfo.Text = "没有得到数据，请检查SQL语句。错误信息：" + ex.Message;
            }
        }

        // SHA-256 加密方法
        private string HashPasswordSHA256(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // 将密码转化为字节数组
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // 将字节数组转换为十六进制字符串
                StringBuilder builder = new StringBuilder();
                foreach (byte byteValue in bytes)
                {
                    builder.Append(byteValue.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        protected void Btn_register_Click(object sender, EventArgs e)
        {
            // 跳转到注册页面
            Response.Redirect("会员注册.aspx");
        }
    }
}
