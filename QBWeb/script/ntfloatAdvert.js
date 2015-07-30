/// <reference path="jquery-1.7.2.min.js" />

var nt = nt || {};
/*
初始化
   href:
   src:
   width:
   height:
   top:
   */
nt.floatAdvertMgr = function (options) {
    var self = this;
    self.on = false;
    self.src = options.src;
    self.href = options.href || 'http://www.naite.com.cn';
    self.width = options.width || 1000;
    self.height = options.height || 400;
    self.top = options.top || 105;
    self.arrowSize = 50;
    self.hideLeft = getHideLeft();
    self.newID = 'div' + Math.random().toString().substr(2, 5);
    html = [
        '<div id="' + self.newID + '" style="z-index:20001;position:fixed;width:' + self.width + 'px;height:' + self.height + 'px;top:' + self.top + 'px;left:' + self.hideLeft + 'px;">',
        '<a href="' + self.href + '" title="advert"><img width="' + self.width + '" height="' + self.height + '" src="' + self.src + '"/></a>',
        '<a class="advert-arrow" href="javascript:closeAdvertWin();" title="advert" style="display:block;width:' + self.arrowSize + 'px;height:' + self.arrowSize + 'px;font-size:12px;line-height:20px;text-align:center;background:#000;color:#fff;position:absolute;top:' + (self.height - self.arrowSize) / 2 + 'px;right:-' + (self.arrowSize) + 'px;text-decoration:none;background:url(/content/images/arrow.png) no-repeat;"></a>',
        '</div>'
    ].join('');
    $(document.body).append(html);
    self.newID = '#' + self.newID;

    self.onOrOffFloater = function () {
        if (self.on)
            self.close();
        else
            self.display();
    }

    /*关闭漂浮广告*/
    self.close = function () {
        self.on = false;
        clearTimeout(self.timer);
        $(self.newID).stop().animate({ left: getHideLeft() }, 1000, null, function () {
            $("a.advert-arrow").css('background', 'url(/content/images/arrow.png) no-repeat');
        });
    }

    window.closeAdvertWin = self.onOrOffFloater;

    function getHideLeft() {
        var w = ($(window).width() - 1062) / 2 - self.width - self.arrowSize;
        if (w + self.width < 0)
            w = self.arrowSize + 20 - self.width;
        return w;
    }

    self.init = function () {
        $(self.newID).find('img').load(function () {
            self.timer = setTimeout(self.display, 2000);
        })
    };

    self.display = function () {
        self.on = true;
        $(self.newID).stop().animate({ left: ($(window).width() - self.width) / 2 }, 1000, null,
            function () {
                $("a.advert-arrow").css('background', 'url(/content/images/arrowf.png) no-repeat');
                self.timer = setTimeout(self.close, 3000);
            });
    }
}