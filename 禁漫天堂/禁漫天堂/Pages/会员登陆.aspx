<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="会员登陆.aspx.cs" Inherits="禁漫天堂.会员登陆" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>会员登录</title>
    <!-- 引入外部 CSS 文件 -->
    <link href="/App_Themes/主题one/登陆styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="title">会员登陆</h2>
            
            <div class="form-group">
                <label for="Txt_user_name">用户名：</label>
                <asp:TextBox ID="Txt_user_name" runat="server" class="form-control" />
            </div>
            
            <div class="form-group">
                <label for="Txt_user_pwd">密码：</label>
                <asp:TextBox ID="Txt_user_pwd" runat="server" TextMode="Password" class="form-control" />
            </div>
            
            <div class="button-container">
                <asp:Button ID="Btn_login" runat="server" OnClick="Btn_login_Click" Text="登陆" class="button" />
                <asp:Button ID="Btn_register" runat="server" OnClick="Btn_register_Click" Text="注册" class="button" />
            </div>
            
            <asp:Label ID="Labinfo" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
