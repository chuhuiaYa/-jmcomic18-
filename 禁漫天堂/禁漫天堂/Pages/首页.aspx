
<%@ Page Title="首页" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeFile="首页.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageTitle" runat="server">
    最多关注的comic - 禁漫天堂
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- 背景音乐播放器 -->
    <audio id="backgroundMusic" autoplay loop>
        <source src="/App_Themes/主题one/音频/99%25.mp3" type="audio/mpeg" />
        您的浏览器不支持音频标签。
    </audio>

     <!-- 音频控制按钮 -->
    <div id="audioControls">
    <button type="button" id="playPauseButton" onclick="toggleAudio()">播放</button>
    <button type="button" id="muteBtn" onclick="toggleMute()">静音</button>
    <label for="volumeControl">音量</label>
    <input type="range" id="volumeControl" min="0" max="1" step="0.1" value="1" oninput="changeVolume()">
</div>

    <!-- 主容器 -->
    <div class="main-container">
        <!-- 轮播图容器 -->
        <div class="carousel-container">
            <div class="carousel">
                <div class="carousel-item active">
                    <a href="/Pages/漫画介绍.aspx?id=JM264883">
                        <img src="/Content/images/comic1.jpg" alt="图片 1">
                    </a>
                </div>
                <div class="carousel-item">
                    <a href="/Pages/漫画介绍.aspx?id=JM255485">
                        <img src="/Content/images/comic2.jpg" alt="图片 2">
                    </a>
                </div>
                <div class="carousel-item">
                    <a href="/Pages/漫画介绍.aspx?id=JM485786">
                        <img src="/Content/images/comic3.jpg" alt="图片 3">
                    </a>
                </div>
            </div>
            <!-- 左右导航箭头 -->
            <button class="carousel-prev">&lt;</button>
            <button class="carousel-next">&gt;</button>
        </div>

        <!-- 排行榜部分，放在轮播图容器外面，但紧接在轮播图下方 -->
        <div class="ranking-container">
            <h2>全站排行榜</h2>
            <div class="ranking-list">
                <div class="ranking-item">
                    <a href="/Pages/漫画介绍.aspx?id=JM146417">
                        <img src="/Content/images/comic9.jpg" alt="排行榜 1">
                        <p>秘密教学 / 秘密の授業</p>
                    </a>
                </div>
                <div class="ranking-item">
                    <a href="/Pages/漫画介绍.aspx?id=JM180459">
                        <img src="/Content/images/comic10.jpg" alt="排行榜 2">
                        <p>继母的朋友们/ハーレム×ハーレム</p>
                    </a>
                </div>
                <div class="ranking-item">
                    <a href="/Pages/漫画介绍.aspx?id=JM145504">
                        <img src="/Content/images/comic11.jpg" alt="排行榜 3">
                        <p>健身教练 / もしも、幼馴染を抱いたなら / Fitness</p>
                    </a>
                </div>
                <!-- 更多的排行榜项目... -->
            </div>
        </div>

        <!-- 右侧图片区域 -->
        <div class="right-images">
            <!-- 第一排两张图片 -->
            <div class="row">
                <div class="image-item">
                    <a href="/Pages/漫画介绍.aspx?id=JM404137"><img src="/Content/images/comicr1.jpg" alt="图片 4"></a>
                </div>
                <div class="image-item">
                    <a href="/Pages/漫画介绍.aspx?id=JM236959"><img src="/Content/images/comicr2.jpg" alt="图片 5"></a>
                </div>
            </div>
            <!-- 第二排三张图片 -->
            <div class="row">
                <div class="image-item">
                    <a href="/Pages/漫画介绍.aspx?id=JM196438"><img src="/Content/images/comicr3.jpg" alt="图片 6"></a>
                </div>
                <div class="image-item">
                    <a href="/Pages/漫画介绍.aspx?id=JM254931"><img src="/Content/images/comicr4.png" alt="图片 7"></a>
                </div>
                <div class="image-item">
                    <a href="/Pages/漫画介绍.aspx?id=JM180491"><img src="/Content/images/comicr5.jpg" alt="图片 8"></a>
                </div>
            </div>
        </div>
    </div>
     
      <!-- 页脚部分 -->
    <footer class="footer">
        <div class="footer-content">
            <div class="links">
                <h4>友情链接</h4>
                <ul>
                    <li><a href="https://www.dm5.com/">动漫屋</a></li>
                    <li><a href="https://www.1kkk.com/">极速漫画</a></li>
                    <li><a href="https://www.fmdaxiang.com/">大象FM</a></li>
                    <li><a href="https://www.zymk.cn/">知音漫客</a></li>
                    <li><a href="https://www.manmankan.com/">漫漫看</a></li>
                </ul>
            </div>
            <div class="copyright">
                <p>&copy; 2024 王佳浩的网站. All Rights Reserved.</p>
            </div>
        </div>
    </footer>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsContent" runat="server">
    <script src="/Scripts/scripts.js"></script>
    <script src="/Scripts/音乐.js"></script>
</asp:Content>


