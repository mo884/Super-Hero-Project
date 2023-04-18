var videoPlayer = document.getElementById("videoPlayer");
var myVideo = document.getElementById("myVideo");

function playVideo(file){
    myVideo.src = file;
    videoPlayer.style.display="block";
    myVideo.style.display="block";
}
function stopVideo() {
    myVideo.pause();
    videoPlayer.style.display="none";
    myVideo.style.display = "none";
    myVideo.disablePopup(); 
}