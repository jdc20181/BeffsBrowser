var imageAddr = "https://jdc20181.github.io/testpics/SpeedTestImage.jpg"; 
var downloadSize = 8793622; //bytes
function ShowProgressMessage(msg) {
    if (console) {
        if (typeof msg == "string") {
            console.log(msg);
        } else {
            for (var i = 0; i < msg.length; i++) {
                console.log(msg[i]);
            }
        }
    }
    
    var oProgress = document.getElementById("progress");
    if (oProgress) {
        var actualHTML = (typeof msg == "string") ? msg : msg.join("<br />");
        oProgress.innerHTML = actualHTML;
    }
}
function InitiateSpeedDetection() {
    ShowProgressMessage("Loading the image, please wait...");
    window.setTimeout(MeasureConnectionSpeed, 1);
};    
if (window.addEventListener) {
    window.addEventListener('load', InitiateSpeedDetection, false);
} else if (window.attachEvent) {
    window.attachEvent('onload', InitiateSpeedDetection);
}
function MeasureConnectionSpeed() {
    var startTime, endTime;
    var download = new Image();
    download.onload = function () {
        endTime = (new Date()).getTime();
        showResults();
    }
    
    download.onerror = function (err, msg) {
        ShowProgressMessage("Invalid image, or error downloading");
    }
    
    startTime = (new Date()).getTime();
    var cacheBuster = "?nnn=" + startTime;
    download.src = imageAddr + cacheBuster;
    
 function showResults() {
        var duration = (endTime - startTime) / 1000;
        var bitsLoaded = downloadSize * 12 ; /* Experment: Change Var from 8 to 12 */
        var speedBps = (bitsLoaded / duration);
        var speedKbps = (speedBps / 1024);
        var speedMbps = (speedKbps / 1024);
        ShowProgressMessage([
            speedMbps.toFixed(2) + " Mbps",
        ]);
}
}
