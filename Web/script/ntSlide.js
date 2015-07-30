/// <reference path="jquery-1.7.2.min.js" />

/*
auto
speed
onMouse
hasFloatBox
*/
$.fn.ntSlide = function (o) {
    var ntS = $(this);
    o = $.extend({ auto: 700, speed: 500, onMouse: false, hasFloatBox: false, next: '#next', pre: '#pre' }, o || {});

    if (o.auto < o.speed)
        o.auto = o.speed;
    ntS.find('#nt-slides').css(
        { position: "relative", overflow: 'hidden', width: '100%', height: '100%' }
        );
    ntS.x = ntS.offset().left;
    ntS.y = ntS.offset().top;
    ntS.w = ntS.outerWidth();
    ntS.count = ntS.find('li').size();
    if (ntS.count < 2) return;
    ntS.movableBox = ntS.find('ul');
    ntS.movableBox.append(ntS.movableBox.html());
    ntS.movableBox.css({ display: 'block', position: 'absolute', width: ntS.count * ntS.w * 2, height: '100%', margin: 0, padding: 0 });
    ntS.movableBox.find('li').css(
        { float: 'left', display: 'block', width: ntS.w, height: '100%', position: 'relative' }
        );
    ntS.find('ul>li>img').css({ width: '100%', height: '100%' });
    ntS.currentIndex = 0;

    //next
    ntS.nextSlide = function () {
        ntS.currentIndex++;
        if (ntS.currentIndex > ntS.count) {
            ntS.currentIndex = 1;
            ntS.movableBox.css('left', 0);
        }
        ntS.movableBox.stop().animate({ left: -ntS.currentIndex * ntS.w }, o.speed, null, function () {
        });
        if (!o.hasFloatBox) return;
        var currentFB = ntS.movableBox.find('li:eq(' + (ntS.currentIndex) + ')').find('.nt-float-box');
        var fbleft = currentFB.position().left;
        currentFB.css({ left: ntS.w + currentFB.width() });
        currentFB.stop().animate({ left: fbleft }, o.speed > 500 ? o.speed - 500 : o.speed, null, function () {
        });
    }

    //pre
    ntS.preSlide = function () {
        ntS.currentIndex--;
        if (ntS.currentIndex < 0) {
            ntS.currentIndex = ntS.count - 1;
            ntS.movableBox.css('left', -ntS.w * (ntS.count));
        }

        ntS.movableBox.stop().animate({ left: -ntS.currentIndex * ntS.w }, o.speed, null, function () { });
        if (!o.hasFloatBox) return;
        var currentFB = ntS.movableBox.find('li:eq(' + (ntS.currentIndex) + ')').find('.nt-float-box');
        var fbleft = currentFB.position().left;
        currentFB.css({ left: ntS.w + currentFB.width() });
        currentFB.stop().animate({ left: fbleft }, o.speed > 500 ? o.speed - 500 : o.speed, null, function () {
        });
    }

    ntS.start = function () {
        ntS.int = setInterval(function () {
            ntS.nextSlide();
        },
       o.auto);
    }

    if (o.onMouse) {
        ntS.mouseover(function () {
            clearInterval(ntS.int);
        }).mouseout(function () {
            ntS.start();
        })
    }

    $(o.next).click(function () {
        clearInterval(ntS.int);
        ntS.nextSlide();
    });

    $(o.pre).click(function () {
        clearInterval(ntS.int);
        ntS.preSlide();
    });

    ntS.start();
}