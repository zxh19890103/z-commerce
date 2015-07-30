var mobileAgent = new Array("iphone", "ipod", "ipad", "android", "mobile", "blackberry", "webos", "incognito", "webmate", "bada", "nokia", "lg", "ucweb", "skyfire");
var browser = navigator.userAgent.toLowerCase();
var isMobile = false;
var mobilesiteUrl = 'http://m.naite.com.cn';
for (var i = 0; i < mobileAgent.length; i++) {
    if (browser.indexOf(mobileAgent[i]) != -1) {
        isMobile = true; window.location.href = mobilesiteUrl;
        break;
    }
}
