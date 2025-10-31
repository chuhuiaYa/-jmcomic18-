document.addEventListener('DOMContentLoaded', function () {
    const prevButton = document.querySelector('.carousel-prev');
    const nextButton = document.querySelector('.carousel-next');
    const items = document.querySelectorAll('.carousel-item');
    let currentIndex = 0;

    // 从 localStorage 获取当前索引，如果没有则使用 0（默认第一张）
    if (localStorage.getItem('carouselIndex')) {
        currentIndex = parseInt(localStorage.getItem('carouselIndex'));
    }

    // 更新显示的图片
    function showItem(index) {
        // 隐藏所有图片
        items.forEach(item => item.classList.remove('active'));

        // 显示当前图片
        items[index].classList.add('active');
    }

    // 上一张按钮事件
    prevButton.addEventListener('click', function () {
        // 当前索引减一，如果是第一个则回到最后一项
        currentIndex = (currentIndex - 1 + items.length) % items.length;
        showItem(currentIndex);

        // 保存当前索引到 localStorage
        localStorage.setItem('carouselIndex', currentIndex);
    });

    // 下一张按钮事件
    nextButton.addEventListener('click', function () {
        // 当前索引加一，如果是最后一个则回到第一项
        currentIndex = (currentIndex + 1) % items.length;
        showItem(currentIndex);

        // 保存当前索引到 localStorage
        localStorage.setItem('carouselIndex', currentIndex);
    });

    // 自动切换图片的定时器，每隔 3 秒切换一次图片
    function autoPlay() {
        setInterval(function () {
            currentIndex = (currentIndex + 1) % items.length; // 切换到下一张
            showItem(currentIndex);
            // 保存当前索引到 localStorage
            localStorage.setItem('carouselIndex', currentIndex);
        }, 3000); // 每隔 3 秒切换
    }

    // 初始化，显示当前索引的图片
    showItem(currentIndex);

    // 启动自动切换
    autoPlay();
});
// 显示 Loading 层
function showLoading() {
    document.getElementById('loading').style.display = 'flex';
}

// 隐藏 Loading 层
function hideLoading() {
    document.getElementById('loading').style.display = 'none';
}

// 模拟数据加载（例如，你可以在数据加载前调用 showLoading，在数据加载完成后调用 hideLoading）
function fetchData() {
    showLoading(); // 显示加载动画

    // 假设进行异步请求加载数据
    setTimeout(function () {
        // 数据加载完成后隐藏加载动画
        hideLoading();
    }, 2000); // 模拟 2 秒钟的数据加载过程
}

// 调用 fetchData 函数以模拟加载过程
fetchData();

// 获取音频元素和控制按钮
var audio = document.getElementById('backgroundMusic');
var playPauseButton = document.getElementById('playPauseButton');
var muteButton = document.getElementById('muteBtn');
var volumeControl = document.getElementById('volumeControl');

// 播放/暂停音频的函数
function toggleAudio() {
    if (audio.paused) {
        audio.play();  // 播放音频
        playPauseButton.innerText = "暂停";  // 修改按钮文字为“暂停”
    } else {
        audio.pause();  // 暂停音频
        playPauseButton.innerText = "播放";  // 修改按钮文字为“播放”
    }
}

// 静音/取消静音的函数
function toggleMute() {
    if (audio.muted) {
        audio.muted = false;  // 取消静音
        muteButton.innerText = "静音";  // 修改按钮文字为“静音”
    } else {
        audio.muted = true;  // 设置为静音
        muteButton.innerText = "取消静音";  // 修改按钮文字为“取消静音”
    }
}

// 更改音量的函数
function changeVolume() {
    audio.volume = volumeControl.value;  // 设置音量
}



