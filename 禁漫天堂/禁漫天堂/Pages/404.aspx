<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="禁漫天堂.Pages._404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="utf-8" />
    <title>页面未找到 - 404</title>
    <link href="/App_Themes/主题one/styles.css" rel="stylesheet" type="text/css" />
    <style>
        body {
            font-family: Arial, sans-serif;
            text-align: center;
            padding: 50px;
            background-color: #f4f4f4;
        }
        .error-container {
            background-color: #fff;
            border-radius: 8px;
            padding: 30px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            max-width: 600px;
            margin: 0 auto;
        }
        .error-container h1 {
            font-size: 100px;
            color: #f44336;
            margin: 0;
        }
        .error-container p {
            font-size: 18px;
            color: #555;
        }
        .error-container a {
            color: #007bff;
            text-decoration: none;
            font-weight: bold;
        }
        .error-container a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="error-container">
        <h1>404</h1>
        <p>抱歉，您访问的页面未找到。</p>
        <p>您可以返回 <a href="/Pages/首页.aspx">首页</a> 或继续浏览其他内容。</p>

        <!-- 显示传递过来的错误信息 -->
        <p><strong>调试信息:</strong></p>
        <p>
            <asp:Label ID="Labinfo" runat="server" Text=""></asp:Label>
        </p>
    </div>
    </form>
</body>
</html>
