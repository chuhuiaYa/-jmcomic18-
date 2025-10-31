using System;
using System.Data;//内存数据库的命名空间
using System.Data.SqlClient;//Sql sever的命名空间
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;//加密密码命名空间
using System.Web.UI;
using System.Web.UI.WebControls;
using 禁漫天堂;
using System.Security.Cryptography;
using System.Text;

namespace 禁漫天堂
{
    public partial class 会员注册 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (Page.IsPostBack == false)
            {
                for (int year = 1940; year < 2024; year++)
                {
                    DDL_Year.Items.Add(year.ToString());
                }
                for (int month = 1; month < 13; month++)
                {
                    DDL_Month.Items.Add(month.ToString());
                }
                for (int day = 1; day < 32; day++)
                {
                    DDL_Day.Items.Add(day.ToString());
                }
            }
            btn_register.CausesValidation = false;
            btn_catch.CausesValidation = false;
        }

        private bool IsUserNameAvailable(string userName)
        {
            string sql = "SELECT COUNT(*) FROM 会员表 WHERE 用户名 = @UserName";
            using (SqlConnection con = new SqlConnection("server=.;database=禁漫天堂;uid=sa;pwd=123"))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@UserName", userName);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count == 0; // 如果不存在，返回 true
            }
        }

        protected void btn_User_Cheak_Click(object sender, EventArgs e)
        {
            if (IsUserNameAvailable(Txt_user_name.Text))
                Labinfo.Text = "恭喜您，此用户名可以使用！";
            else
                Labinfo.Text = "对不起，此用户名已被注册，请输入其它用户名";
        }

        protected void Txt_user_name_TextChanged(object sender, EventArgs e)
        {
            if (IsUserNameAvailable(Txt_user_name.Text))
                Labinfo.Text = "恭喜！，此用户名可以使用！";
            else
                Labinfo.Text = "此用户名已经被占用！";
        }


        protected void btn_catch_Click(object sender, EventArgs e)
        {
            Txt_user_name.Focus();//用户名所在文本框获得输入焦点（光标）
            Txt_user_pwd.Text = "";
            Txt_Repwd.Text = "";
            Txt_Name.Text = "";
            Txt_Address.Text = "";
            Txt_Post.Text = "";
            Txt_Tel.Text = "";
            Txt_Mobile.Text = "";
            Txt_ID.Text = "";
            Txt_user_name.Text = "";
        }

        protected void btn_register_Click(object sender, EventArgs e)
        {
            string userPwd = Txt_user_pwd.Text.Trim();
            string hashedPassword = HashPasswordSHA256(userPwd); // 使用SHA-256加密密码

            DB db = new DB();//1.声明一个具体的工具

            this.Validate();

            if (Page.IsValid) // 验证通过
            {
                string SqlStr = "INSERT INTO 会员表(用户名, 密码, 姓名, 性别, 出生日期, 联系地址, 邮政编码, 联系电话, 手机, 身份证号) " +
                                "VALUES(@UserName, @Password, @Name, @Sex, @BirthDate, @Address, @PostalCode, @Tel, @Mobile, @ID)";

                SqlParameter[] parameters = {
                new SqlParameter("@UserName", Txt_user_name.Text),
                new SqlParameter("@Password", hashedPassword),
                new SqlParameter("@Name", Txt_Name.Text),
                new SqlParameter("@Sex", DDL_Sex.SelectedItem.Text),
                new SqlParameter("@BirthDate", DDL_Year.SelectedItem.Text + "-" + DDL_Month.SelectedItem.Text + "-" + DDL_Day.SelectedItem.Text),
                new SqlParameter("@Address", Txt_Address.Text),
                new SqlParameter("@PostalCode", Txt_Post.Text),
                new SqlParameter("@Tel", Txt_Tel.Text),
                new SqlParameter("@Mobile", Txt_Mobile.Text),
                new SqlParameter("@ID", Txt_ID.Text)
            };

                Boolean InsertResult = db.UpdateDataBySql(SqlStr, parameters);

                if (InsertResult)
                    Labinfo.Text = "注册成功！";
                else
                {
                    Labinfo.Text = "注册失败！";
                    Txt_user_name.Focus(); //光标定位到用户名框
                }
            }
            else
            {
                Labinfo.Text = "请填写完整信息！";
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


    }
}