$('#menuResizer').mousedown(function (e) {
    $(this).addClass('menuResizer-press ');
    this.mousedownOnThis = true;
    this.mX = e.pageX;
});

$(document.body).mouseup(function (e) {
    if (menuResizer.mousedownOnThis) {
        $(menuResizer).removeClass('menuResizer-press ');
        menuResizer.mousedownOnThis = false;
    }
}).mousemove(function (e) {
    if (menuResizer.mousedownOnThis) {
        var x = e.pageX;
        var wAdjustment = x - menuResizer.mX;
        var w = $(menu_container).width();
        $(menu_container).width(0);
    }
});