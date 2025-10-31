// 收藏按钮点击事件
function addToFavorites(comicId) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "AddToFavorites.aspx", true);
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            if (xhr.responseText == "Success") {
                alert("已成功添加到收藏夹！");
            } else {
                alert("添加失败，请稍后再试！");
            }
        }
    };
    xhr.send("comicId=" + comicId);  // 发送漫画的 ID 到后台
}
