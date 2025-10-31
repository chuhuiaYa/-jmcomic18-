<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.Master" CodeBehind="搜索结果.aspx.cs" Inherits="禁漫天堂.Pages.搜索结果" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    搜索结果
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Loading 层 -->
<div id="loading" class="loading">
    <div class="spinner"></div>
</div>

    <div class="search-results">
        <h2>搜索结果</h2>
        <asp:Repeater ID="RepeaterResults" runat="server">
            <ItemTemplate>
                <div class="comic-card">
                    <img src='<%# Eval("封面URL") %>' alt='<%# Eval("标题") %>' class="comic-thumbnail" />
                    <div class="comic-info">
                        <h3><%# Eval("标题") %></h3>
                        <p class="comic-description"><%# Eval("简介") %></p>
                        <a href="漫画介绍.aspx?id=<%# Eval("ComicID") %>" class="btn">查看详情</a>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Literal ID="LiteralMessage" runat="server" EnableViewState="false" />
    </div>
</asp:Content>
