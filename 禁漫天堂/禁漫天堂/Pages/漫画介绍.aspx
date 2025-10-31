<%@ Page Title="漫画详情" Language="C#" MasterPageFile="~/MasterPages/ComicMaster.master" AutoEventWireup="true" CodeFile="漫画介绍.aspx.cs" Inherits="ComicDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    <%= ComicTitle %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="comic-info-container">
            <!-- 封面区域 -->
            <div class="comic-cover">
                <img src="<%= ComicCoverUrl %>" alt="封面" class="cover-img" />
            </div>

            <!-- 漫画详情 -->
            <div class="comic-details">
                <h1 class="comic-title"><%= ComicTitle %></h1>
                <p><strong>作者：</strong><%= ComicAuthor %></p>
                <h3>标签:</h3>
                <div class="tags-container">
                    <% if (ComicTags != null && ComicTags.Count > 0) { %>
                        <% Random rand = new Random(); %>
                        <% foreach (var tag in ComicTags) { 
                               string color = "#" + rand.Next(0x1000000).ToString("X6"); // 动态生成颜色
                        %>
                            <span class="tag-item" style="background-color: <%= color %>; color: white;">
                                <%= tag %>
                            </span>
                        <% } %>
                    <% } else { %>
                        <span class="no-tags">暂无标签</span>
                    <% } %>
                </div>


                <p><strong>评分：</strong><span class="comic-rating"><%= ComicRating %></span></p>
                <p><strong>简介：</strong><%= ComicDescription %></p>
                <p><strong>发布日期：</strong><%= ComicPublishDate %></p>


                <!-- 开始阅读按钮和收藏按钮 -->
                <div class="action-buttons">
                    <a href="<%= ReadingLink %>" class="btn start-reading" target="_blank">开始阅读</a>
                    <asp:Button ID="btnAddToFavorites" runat="server" Text="收藏" class="btn favorite" OnClick="AddToFavorites_Click"/>
                </div>
            </div>
        </div>

        <!-- 社交分享按钮 -->
        <div class="social-share">
            <a href="https://twitter.com/share?url=<%= Request.Url.AbsoluteUri %>" target="_blank" class="social-btn">分享到 Twitter</a>
            <a href="https://www.facebook.com/sharer/sharer.php?u=<%= Request.Url.AbsoluteUri %>" target="_blank" class="social-btn">分享到 Facebook</a>
        </div>
    </div>
</asp:Content>
