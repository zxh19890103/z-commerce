/// <reference path="jquery-1.7.2-vsdoc.js" />
/// <reference path="jquery-1.7.2.min.js" />
/// <reference path="common.js" />
/*
Function Name:showDaysWin
params:
@dt:(dateTime)当前日期
*/

var nt = nt || {};

//当前正在使用的日期控件
var activeDateTimer = null;
//年份的起始位置
var startYear = 1753;
var endYear = 2999;

function parseDate(str) {
    if (typeof str == 'string') {
        var results = str.match(/^ *(\d{4})[-,/](\d{1,2})[-,/](\d{1,2}) *$/);
        if (results && results.length > 3)
            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]));
        results = str.match(/^ *(\d{4})[-,/](\d{1,2})[-,/](\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2}) *$/);
        if (results && results.length > 6)
            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]), parseInt(results[4]), parseInt(results[5]), parseInt(results[6]));
        results = str.match(/^ *(\d{4})[-,/](\d{1,2})[-,/](\d{1,2}) +(\d{1,2}):(\d{1,2}):(\d{1,2})\.(\d{1,9}) *$/);
        if (results && results.length > 7)
            return new Date(parseInt(results[1]), parseInt(results[2]) - 1, parseInt(results[3]), parseInt(results[4]), parseInt(results[5]), parseInt(results[6]), parseInt(results[7]));
    }
    return new Date();
}

/*9=>09*/
function Fix(n) {
    return (n < 10 ? '0' + n : n);
};

/*日期对象，输入参数为String，格式必须为yyyy-MM-dd hh:mm:ss*/
function DateTime(object) {
    var self = this;
    var d = parseDate(object);
    self.y = d.getFullYear();
    self.M = d.getMonth() + 1;
    self.d = d.getDate();
    self.h = d.getHours();
    self.m = d.getMinutes();
    self.s = d.getSeconds();

    self.toString = function () {
        var part0 = this.y + '-' + Fix(this.M) + '-' + Fix(this.d);
        var part1 = Fix(this.h) + ':' + Fix(this.m) + ':' + Fix(this.s);
        if (this.IsShort)
            return part0;
        else
            return part0 + ' ' + part1;
    };
}

/*日期选择器*/
nt.calendar = function () {
    var self = this;
    $.extend(self, {
        currDate: new DateTime(),
        oldDate: new DateTime(),
        toDay: new DateTime(),
        _data: [
         '00', '01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12',
         '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24',
         '25', '26', '27', '28', '29', '30', '31', '32', '33', '34', '35', '36',
         '37', '38', '39', '40', '41', '42', '43', '44', '45', '46', '47', '48',
         '49', '50', '51', '52', '53', '54', '55', '56', '57', '58', '59'
        ],
        _weeks: ['日', '一', '二', '三', '四', '五', '六']
    });

    //获取下拉框的html，也就是select
    self._getSelection = function (start, end, id) {
        var txt = '<select id="' + id + '">';
        if (end === Infinity) {
            for (var y = startYear; y <= endYear; y++) {
                txt += '<option value="' + y + '">' + y + '</option>';
            }
        } else {
            for (var y = start; y < end; y++) {
                txt += '<option value="' + self._data[y] + '">' + self._data[y] + '</option>';
            }
        }
        txt += '</select>';
        return txt;
    };

    //获取日
    self._getDayItem = function (i) {
        var val = self._data[i + 1];
        var isCurrent = (self.currDate.y === self.oldDate.y)
        && (self.currDate.M === self.oldDate.M)
        && (parseInt(val) === self.oldDate.d);

        var isToday = (self.currDate.y === self.toDay.y)
        && (self.currDate.M === self.toDay.M)
        && (parseInt(val) === self.toDay.d);

        return '<li  class="day-item' + (isCurrent ? ' day-current' : '') + (isToday ? ' today-day-item' : '') + '">' + val + '</li>';
    };

    //获取日期Html
    self._getDays = function () {
        var txt = '';
        for (var i = 0; i < self.w; i++)
            txt += '<li></li>';
        for (var i = 0; i < 7 - self.w; i++)
            txt += self._getDayItem(i);

        var r = 0;
        for (; r < self.rows; r++) {
            for (var i = 0; i < 7; i++)
                txt += self._getDayItem(r * 7 + i + 7 - self.w);
        }

        if (self.append > 0) {
            for (var i = 0; i < self.append; i++)
                txt += self._getDayItem(r * 7 + i + 7 - self.w);
            for (var i = 0; i < 7 - self.append; i++)
                txt += '<li></li>';
        }
        return txt;
    };

    //渲染主体html
    self._render = function () {
        var html = [];
        html.push('<div class="nt-calendar"  id="ntCalendar">');
        html.push('<div class="nt-calendar-top">');
        html.push('<div class="calendar-item" style="width:23%">');
        html.push(self._getSelection(0, Infinity, 'ntYear'));
        html.push('</div>');
        html.push('<div class="calendar-item">');
        html.push(self._getSelection(1, 13, 'ntMonth'));
        html.push('</div>');
        html.push('<div class="calendar-item a-button a-button-small" role="close">');
        html.push('关闭');
        html.push('</div>');
        html.push('<div class="calendar-item a-button a-button-small" role="ok">');
        html.push('确定');
        html.push('</div>');
        html.push('</div>');

        html.push('<div class="nt-calendar-body"><ul class="weeks">');
        for (var item in self._weeks) {
            html.push('<li>' + self._weeks[item] + '</li>');
        }
        html.push('</ul><ul id="ntDay">');
        html.push(self._getDays());
        html.push('</ul></div>');

        html.push('<div class="nt-calendar-foot">');
        html.push('<div class="calendar-item">');
        html.push(self._getSelection(0, 24, 'ntHour'));
        html.push('</div>');

        html.push('<div class="calendar-item">');
        html.push(self._getSelection(0, 60, 'ntMinute'));
        html.push('</div>');

        html.push('<div class="calendar-item">');
        html.push(self._getSelection(0, 60, 'ntSecond'));
        html.push('</div>');

        html.push('<div class="calendar-item  a-button a-button-small" role="today">');
        html.push('今天');
        html.push('</div>');

        html.push('</div>');
        html.push('</div>');

        $(document.body).append(html.join(''));
    };

    //绑定控件的事件处理方法
    self._bindEvents = function () {
        $('#ntYear').bind('change', function () {
            self._select(this, 'y');
            self._reset();
        });

        $('#ntMonth').bind('change', function () {
            self._select(this, 'M');
            self._reset();
        });

        $('#ntHour').bind('change', function () {
            self._select(this, 'h');
        });

        $('#ntMinute').bind('change', function () {
            self._select(this, 'm');
        });

        $('#ntSecond').bind('change', function () {
            self._select(this, 's');
        });

        $('div[role="close"]', '#ntCalendar').bind('click', function () {
            self._closeCalendar();
        });

        $('div[role="today"]', '#ntCalendar').bind('click', function () {
            self.currDate = new DateTime();
            self._reset();
        });

        $('div[role="ok"]', '#ntCalendar').bind('click', function () {
            if (self.activeDateTimer)
                self.activeDateTimer.value = self.currDate.toString();
            self.oldDate = new DateTime(self.currDate.toString());
            self._closeCalendar();
        });
    };

    //设置日期
    self._setDate = function () {
        $('#ntYear').val(self.currDate.y);
        $('#ntMonth').val(Fix(self.currDate.M));
        $('#ntHour').val(Fix(self.currDate.h));
        $('#ntMinute').val(Fix(self.currDate.m));
        $('#ntSecond').val(Fix(self.currDate.s));
    };

    //刷新日期列表
    self._refresh = function () {
        $('#ntDay').html(self._getDays());
        $('li.day-item').click(function () {
            self._select(this, 'd');
            $('li.selected-day-item').removeClass('selected-day-item');
            $(this).addClass('selected-day-item');
        });
    };

    //选择年月日时分秒
    self._select = function (sender, code) {
        if (!self.activeDateTimer) return;
        self.currDate[code] = parseInt(sender.value || sender.innerHTML);
        var d = self.currDate.d;
        var maxD = getMaxDay(self.currDate.y, self.currDate.M);
        if (d > maxD)
            self.currDate.d = maxD;
    };

    //重新设置当前日期
    self._reset = function () {
        //计算星期
        var y, M, d = 1;
        y = self.currDate.y;
        M = self.currDate.M;
        self.w = new Date(y, M - 1, d).getDay();
        var maxDays = getMaxDay(y, M);
        self.rows = parseInt((maxDays - (7 - self.w)) / 7);
        self.append = (maxDays - (7 - self.w)) % 7;
        self._setDate();
        self._refresh();
    };

    //显示日期选择器
    self._showCalendar = function () {
        self._reset();
        $('#ntCalendar').follow(self.activeDateTimer, 2014).show();
    };

    //关闭日期选择器
    self._closeCalendar = function () {
        $('#ntCalendar').hide();
    };

    //初始化，包括html的渲染，事件绑定
    self.init = function () {
        self._render();
        self._bindEvents();
        $('input.input-datetime').each(function (i, n) {
            this.setAttribute('readonly', 'readonly');
            $(this).click(function () {
                self.activeDateTimer = this;
                self.currDate = new DateTime(this.value);
                self.oldDate = new DateTime(this.value);
                self._showCalendar();
            });
        });
    };
};

$(function () {
    window.calendar = new nt.calendar();
    window.calendar.init();
})
