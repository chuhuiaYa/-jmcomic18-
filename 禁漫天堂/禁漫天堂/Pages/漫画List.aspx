<%@ Page Title="标签漫画" Language="C#" MasterPageFile="~/MasterPages/ComicMaster.master" AutoEventWireup="true" CodeFile="漫画List.aspx.cs" Inherits="禁漫天堂.Pages.漫画List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <!-- 标签下的漫画列表 -->
        <h2 class="page-title"><%= TagName %> 标签下的所有漫画</h2>
        <div class="comic-list">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="comic-card">
                        <img src="<%# Eval("封面URL") %>" alt="<%# Eval("标题") %>" class="comic-cover" />
                        <div class="comic-info">
                            <h3 class="comic-title"><%# Eval("标题") %></h3>
                            <p class="comic-author">作者：<%# Eval("作者") %></p>
                            <a href="漫画介绍.aspx?id=<%# Eval("ComicID") %>" class="btn">查看详情</a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>

<asp:Content ID="CustomCSS" ContentPlaceHolderID="CustomCSS" runat="server">
    <!-- 引用漫画List页面独有的CSS -->
    <link href="/App_Themes/主题one/标签styles.css" rel="stylesheet" />
</asp:Content>
