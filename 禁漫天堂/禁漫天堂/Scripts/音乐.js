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
