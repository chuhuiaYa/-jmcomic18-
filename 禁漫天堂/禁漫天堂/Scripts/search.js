window.search = function () {
    var searchInput = document.getElementById('search-input');
    var searchTerm = searchInput.value.trim();

    if (searchTerm) {
        var url = '/Pages/搜索结果.aspx?query=' + encodeURIComponent(searchTerm);
        console.log('搜索词:', searchTerm);
        console.log('跳转 URL:', url);
        window.location.href = url;
    } else {
        alert('请输入搜索内容');
    }
};

