/// <reference path="jquery-1.7.2.min.js" />

var nt = nt || {};
nt.attributeMgr = function (context) {
    var self = this;
    /*start ddl*/
    self.currentSelectBar = null;
    self._SelectionSlideUpOrDown = function () {
        var b = self.currentSelectBar;
        var s = b.next();
        if (b.data('extend')) {
            s.slideUp('fast');
            b.data('extend', false);
        } else {
            s.slideDown('fast');
            b.data('extend', true);
        }
    }

    $('.attr-dropdown-list .select-bar', context).click(function () {
        if (self.currentSelectBar && !self.currentSelectBar.eq(this)) {
            self._SelectionSlideUpOrDown();
        }
        self.currentSelectBar = $(this);
        self._SelectionSlideUpOrDown();
    });

    $('.attr-dropdown-list  li', context).click(function (e) {
        var li = $(this);
        var v = li.attr('data-value');
        var t = li.text();
        $('input[name="' + li.attr('for') + '"]', context).val(v);
        self.currentSelectBar.text(t);
        li.siblings('.selected').removeClass('selected');
        li.addClass('selected');
        self._SelectionSlideUpOrDown();
    });
    /*end ddl*/

    /*start color*/

    $('.attr-colors  li', context).click(function () {
        var li = $(this);
        var v = li.attr('data-value');
        $('input[name="' + li.attr('for') + '"]', context).val(v);
        li.siblings('.selected').removeClass('selected');
        li.addClass('selected');
    });

    /*end color*/


    /*start checkbox list*/
    $('.attr-checkboxes   li', context).click(function () {
        var li = $(this);
        var v = [];
        if (li.hasClass('selected'))
            li.removeClass('selected');
        else {
            li.addClass('selected');
            v.push(li.attr('data-value'));
        }

        $(li.siblings('.selected')).each(function (i, n) {
            v.push($(n).attr('data-value'));
        });
        
        var vBox = $('input[name="' + li.attr('for') + '"]', context);

        vBox.val(v.join('|||'));
    });
    /*end checkbox list*/

    /*start radio list*/

    $('.attr-radio-list  li', context).click(function () {
        var li = $(this);
        var v = li.attr('data-value');
        $('input[name="' + li.attr('for') + '"]', context).val(v);
        li.siblings('.selected').removeClass('selected');
        li.addClass('selected');
    });

    /*end radio list*/

}