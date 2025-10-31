<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="个人中心.aspx.cs" Inherits="禁漫天堂.Pages.UserCenter" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="utf-8" />
    <title>个人中心</title>
    <!-- 引入样式表 -->
    <link href="/App_Themes/主题one/个人styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
    <div class="container">
        <div class="header">
            <img id="UserAvatar" src='<%= ResolveUrl(AvatarUrl) %>' alt="User Avatar" class="avatar" />
            <h1>欢迎回来，<%= UserName %>！</h1>
            <p>个人中心</p>
        </div>
          <asp:FileUpload ID="FileUploadAvatar" runat="server" CssClass="file-upload" />
          <asp:Button ID="ButtonUpload" runat="server" Text="上传头像" CssClass="btn-upload" OnClick="ButtonUpload_Click" />
        
        <div class="nav">
            <a href="#user-info">用户信息</a>
            <a href="#settings">账户设置</a>
            <a href="#favorites">我的收藏</a>
        </div>

        <div class="content">
            <!-- 用户信息 -->
            <div class="section" id="user-info">
                <h3>个人信息</h3>
                <p><strong>用户名：</strong><%= UserName %></p>
                <p><strong>邮政编号：</strong><%= UserEmail %></p>
                <p><strong>注册时间：</strong><%= RegistrationDate %></p>
            </div>

            <!-- 账户设置 -->
            <div class="section" id="settings">
                <h3>账户设置</h3>
                 
    <!-- 修改密码 -->
    <div>
        <label for="txtNewPassword">修改密码：</label>
        <input type="password" id="txtNewPassword" runat="server" placeholder="请输入新密码" class="input-field" />
        <asp:Button ID="btnChangePassword" runat="server" Text="修改密码" CssClass="btn-change" OnClick="ChangePassword_Click" />
    </div>
    
    <!-- 修改邮政编码 -->
    <div>
        <label for="txtNewEmail">修改邮政编码：</label>
        <input type="email" id="txtNewEmail" runat="server" placeholder="请输入新邮政编码" class="input-field" />
        <asp:Button ID="btnChangeEmail" runat="server" Text="修改邮政编码" CssClass="btn-change" OnClick="ChangeEmail_Click" />
    </div>
    
    <!-- 修改绑定手机号 -->
    <div>
        <label for="txtNewPhone">修改绑定手机号：</label>
        <input type="tel" id="txtNewPhone" runat="server" placeholder="请输入新手机号" class="input-field" />
        <asp:Button ID="btnChangePhone" runat="server" Text="修改手机号" CssClass="btn-change" OnClick="ChangePhone_Click" />
    </div>
            </div>

            <!-- 我的收藏 -->
            <div class="section" id="favorites">
                <h3>我的收藏</h3>
                <asp:Repeater ID="RepeaterFavorites" runat="server">
                    <ItemTemplate>
                        <div class="favorite-item">
                            <img src="<%# Eval("封面URL") %>" alt="<%# Eval("标题") %>" class="comic-cover" />
                            <p class="comic-title"><%# Eval("标题") %></p>
                            <p class="comic-author"><%# Eval("作者") %></p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="footer">
            <p><a href="Logout.aspx">退出登录</a></p>
        </div>
    </div>
  </form>
</body>
</html>
