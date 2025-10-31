<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="会员注册.aspx.cs" Inherits="禁漫天堂.会员注册" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            width: 357px;
        }
        .auto-style4 {
            height: 21px;
            text-align: center;
        }
        .auto-style6 {
            width: 357px;
            height: 28px;
        }
        .auto-style7 {
            height: 28px;
        }
        .auto-style9 {
            width: 357px;
            height: 30px;
        }
        .auto-style10 {
            height: 30px;
        }
        .auto-style11 {
            width: 560px;
            height: 28px;
            text-align: right;
        }
        .auto-style12 {
            width: 560px;
            height: 30px;
            text-align: right;
        }
        .auto-style13 {
            width: 560px;
            text-align: right;
        }
    </style>
        <script type="text/javascript">
            // 页面加载完成后执行
            window.onload = function () {
                // 通过 API 获取背景图 URL
                fetch('http://localhost:8500/api/proxy/getImage')
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('网络响应失败');
                        }
                        return response.json(); // 解析 JSON 数据
                    })
                    .then(data => {
                        const imageUrl = data.imageUrl;  // 获取图片 URL

                        console.log('获取到的图片 URL:', imageUrl); // 调试输出

                        // 如果 imageUrl 无效或为空，检查是否正确
                        if (!imageUrl) {
                            throw new Error('图片 URL 无效');
                        }

                        // 将获取到的图片 URL 设置为背景图
                        document.body.style.backgroundImage = 'url(' + imageUrl + ')';  // 使用 Base64 编码的图片
                        document.body.style.backgroundSize = 'cover';
                        document.body.style.backgroundPosition = 'center';
                    })
                    .catch(error => {
                        console.error('获取背景图失败:', error);
                    });
            };
        </script>
</head>
<body>
    <form id="form1" runat="server">
        
    <div style="display: flex; justify-content: center; align-items: center; height: 100vh; width: 100%;">
    
        <table class="auto-style1" style="margin: 0 auto;">
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="Label7" runat="server" Text="用户注册" Font-Names="楷体" Font-Size="XX-Large" ForeColor="#FF99FF" style="font-weight: 700; text-align: center"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style11">用户名：</td>
                            <td class="auto-style6">
                                <asp:TextBox ID="Txt_user_name" runat="server" TabIndex="1" Width="350px" OnTextChanged="Txt_user_name_TextChanged" ValidationGroup="CheckUserName"></asp:TextBox>
                            </td>
                            <td class="auto-style7">*<asp:Button ID="btn_User_Cheak" runat="server" OnClick="btn_User_Cheak_Click" TabIndex="2" Text="检测用户名" ValidationGroup="CheckUserName" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txt_user_name" ErrorMessage="此项必填" ValidationGroup="Register"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style12">密码：</td>
                            <td class="auto-style9">
                                <asp:TextBox ID="Txt_user_pwd" runat="server" TabIndex="3" TextMode="Password" Width="350px"></asp:TextBox>
                            </td>
                            <td class="auto-style10">*<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Txt_user_pwd" ErrorMessage="此项必填" ValidationGroup="Register"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style13">确认密码：</td>
                            <td class="auto-style3">
                                <asp:TextBox ID="Txt_Repwd" runat="server" TabIndex="4" TextMode="Password" Width="350px"></asp:TextBox>
                            </td>
                            <td>*<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Txt_Repwd" ErrorMessage="此项必填" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Txt_user_pwd" ControlToValidate="Txt_Repwd" ErrorMessage="两次密码输入不一样"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">姓名：</td>
                            <td class="auto-style6">
                                <asp:TextBox ID="Txt_Name" runat="server" TabIndex="5" Width="350px"></asp:TextBox>
                            </td>
                            <td class="auto-style7">*<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Txt_Name" Display="Dynamic" ErrorMessage="此项必填" ValidationGroup="Register"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">性别：</td>
                            <td class="auto-style6">
                                <asp:DropDownList ID="DDL_Sex" runat="server" TabIndex="6">
                                    <asp:ListItem>男</asp:ListItem>
                                    <asp:ListItem>女</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style7"></td>
                        </tr>
                        <tr>
                            <td class="auto-style13">出生日期：</td>
                            <td class="auto-style3">
                                <asp:DropDownList ID="DDL_Year" runat="server" TabIndex="7">
                                </asp:DropDownList>
                                年<asp:DropDownList ID="DDL_Month" runat="server" TabIndex="8">
                                </asp:DropDownList>
                                月<asp:DropDownList ID="DDL_Day" runat="server" TabIndex="9">
                                </asp:DropDownList>
                                日</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style13">联系地址：</td>
                            <td class="auto-style3">
                                <asp:TextBox ID="Txt_Address" runat="server" TabIndex="10" Width="350px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style13">邮政编码：</td>
                            <td class="auto-style3">
                                <asp:TextBox ID="Txt_Post" runat="server" TabIndex="11" Width="350px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Txt_Post" ErrorMessage="邮政编码格式错误" ValidationExpression="\d{6}"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style13">联系电话：</td>
                            <td class="auto-style3">
                                <asp:TextBox ID="Txt_Tel" runat="server" TabIndex="12" Width="350px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style13">手机：</td>
                            <td class="auto-style3">
                                <asp:TextBox ID="Txt_Mobile" runat="server" TabIndex="13" Width="350px"></asp:TextBox>
                            </td>
                            <td>*<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="Txt_Mobile" ErrorMessage="此项必填" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Txt_Mobile" ErrorMessage="手机号格式错误" ValidationExpression="(\(\d{3}\)|\d{3}-)?\d{8}"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">身份证号：</td>
                            <td class="auto-style6">
                                <asp:TextBox ID="Txt_ID" runat="server" TabIndex="14" Width="350px"></asp:TextBox>
                            </td>
                            <td class="auto-style7">*<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Txt_ID" ErrorMessage="此项必填" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Txt_ID" ErrorMessage="身份证号格式错误" ValidationExpression="\d{17}[\d|X]|\d{15}"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style13">&nbsp;</td>
                            <td class="auto-style3">
                                <asp:Button ID="btn_register" runat="server" OnClick="btn_register_Click" TabIndex="15" Text="注册" ValidationGroup="Register" />
                                <asp:Button ID="btn_catch" runat="server" TabIndex="16" Text="重填" OnClick="btn_catch_Click" />
                                <asp:Label ID="Label8" runat="server" Font-Size="Smaller" ForeColor="Red" Text="带*为必填项"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Labinfo" runat="server" Font-Size="Smaller" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
        

    </form>
</body>
</html>
