<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="first_index.aspx.cs" Inherits="禁漫天堂.first_index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>访客计数页面</title>
    <script type="text/javascript">
        var countdown = 5; // 倒计时秒数
        function startCountdown() {
            var timer = document.getElementById("timer");
            var interval = setInterval(function () {
                if (countdown <= 0) {
                    clearInterval(interval);
                } else {
                    timer.innerText = countdown + " 秒后跳转...";
                    countdown--;
                }
            }, 1000);
        }
    </script>
</head>
<body onload="startCountdown()">
    <form id="form1" runat="server">
        <div>
            您是第 
            <asp:Label ID="Labinfo" runat="server" Font-Names="楷体" ForeColor="Red"></asp:Label>
            位浏览者<br />
            <span id="timer" style="color: blue; font-size: larger;"></span>
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Pages/首页.aspx">首页</asp:HyperLink>
            |<a href="会员登陆.aspx">登陆</a>
        </div>
    </form>
</body>
</html>
