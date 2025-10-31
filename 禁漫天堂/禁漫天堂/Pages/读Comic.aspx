<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="读Comic.aspx.cs" Inherits="禁漫天堂.ReadComic" MasterPageFile="~/MasterPages/ComicMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    开始阅读 - <%= ComicTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="comic-read-container">
        <h2><%= ComicTitle %></h2>
        <div class="comic-content">
            <img src="<%= ComicImageURL %>" alt="漫画封面" class="comic-img" />
            <div class="comic-pages">
                <!-- 输出漫画页面内容 -->
                <%= ComicContent %> <!-- 直接输出 HTML 内容 -->
            </div>
        </div>
    </div>
</asp:Content>
