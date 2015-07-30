/// <reference path="jquery-1.7.2.min.js" />
/// <reference path="common.list.js" />
/// <reference path="datetime.js" />

var nt = nt || {};

nt.config = {};
nt.config.loginUrl = '/netin/login.aspx';
nt.config.notAuthorizedUrl = '/netin/notauthorized.aspx';
nt.config.currentMovableObject = {};

nt.mask = null;
nt.loadingBox = null;
nt.maskCreated = false;
nt.loadingBoxCreated = false;
nt.loadingImgSrc = '/netin/content/images/loading.dark.GIF';
nt.fn = {};
/*
绑定多个onload处理程序
*/
function addLoadEventHandler(func) {
    var oldonload = window.onload;
    if (typeof window.onload != 'function') {
        window.onload = func;
    } else {
        window.onload = function () {
            oldonload();
            func();
        }
    }
}

/*
绑定多个onresize处理程序
*/
function addResizeEventHandler(func) {
    var oldonresize = window.onresize;
    if (typeof window.onresize != 'function') {
        window.onresize = func;
    } else {
        window.onresize = function () {
            oldonresize();
            func();
        }
    }
}


/*$ plugin*/

$.fn.isUndefined = function () {
    return this.get(0) === undefined;
}

/*
将一个元素移到指定的元素位置左下角
@selector需要被覆盖的元素的Jquery选择器
*/
$.fn.follow = function (selector, zIndex) {
    var o = $(selector).get(0);
    var x = 0, y = 0, w = 0, h = 0;
    x = getAbsoluteLeft(o);
    y = getAbsoluteTop(o);
    w = getElementWidth(o);
    h = getElementHeight(o);
    this.css({ position: 'absolute', 'z-index': zIndex || 2014, left: x, top: y + h });
    return this;
}

/*
注册元素的拖拽函数
@param:dragBar[Jquery selector or Jquery Object]
*/
$.fn.movable = function () {
    if (this.data('movable')) return;
    var bar = $(this).find('.nt-drag-bar');
    if (bar.isUndefined()) return;
    bar.mousedown(function (e) {
        var o = nt.config.currentMovableObject;
        var t = $(this);
        o.box = t.parent(); //父元素为移动元素
        var box = o.box;
        o.w = box.outerWidth();
        o.h = box.outerHeight();
        o.beginMove = true;
        o.boundWidth = document.documentElement.scrollWidth - 10;
        o.boundHeight = document.documentElement.scrollHeight - 10;
        var p = box.position();
        o.offX = p.left - e.pageX;
        o.offY = p.top - e.pageY;
        t.addClass('nt-drag-bar-press');
    }).mouseup(function (e) {
        var t = $(this);
        var o = nt.config.currentMovableObject;
        o.beginMove = false;
        t.removeClass('nt-drag-bar-press');
    }).mousemove(function (e) {
        var o = nt.config.currentMovableObject;
        if (o.beginMove) {
            var box = o.box;
            var tx = e.pageX + o.offX;
            var ty = e.pageY + o.offY;
            if (tx < 10 || tx + o.w > o.boundWidth)
                return;
            if (ty < 10 || ty + o.h > o.boundHeight)
                return;
            box.css({
                left: tx,
                top: ty
            });
        }
    });
    this.data('movable', true);
    return this;
}

/*
input value must be an int32
*/
$.fn.mustBeInt32 = function () {
    this.each(function (i, n) {
        this.oldValue = this.value;
    }).change(function () {
        if (!/^\d{1,9}$/.test(this.value)) {
            error('这里必须填写正整数!');
            this.value = this.oldValue;
            this.focus();
        }
        this.oldValue = this.value;
    });
}

/*
input value must be a Decimal
*/
$.fn.mustBeDecimal = function () {
    this.each(function (i, n) {
        this.oldValue = this.value;
    }).change(function () {
        if (!/^-?\d*\.?\d+$/.test(this.value)) {
            error('这里必须填写浮点数!');
            this.value = this.oldValue;
            this.focus();
        }
        this.oldValue = this.value;
    });
}

/*nt tabs*/
$.fn.tabs = function () {
    var self = this;
    self.data('cur', 0);
    $('.nt-tabs>li', this).click(function () {
        var i = $(this).index(),
            cur = self.data('cur');
        if (i == cur) return; //current
        $('.nt-tabs>li:eq(' + cur + ')').removeClass('active');
        $(this).addClass('active');
        var items = $('.nt-tabs-content-wrap>.tab-item', self);
        items.eq(cur).removeClass('tab-item-selected');
        items.eq(i).addClass('tab-item-selected');
        self.data('cur', i);
    }).first().addClass('active');
    $('.nt-tabs-content-wrap>.tab-item:eq(0)', self).addClass('tab-item-selected');
}

/*
nt tag
*/
$.fn.tag = function () {
    var self = this;
    self.tagEditor = $('.input-tag', self);
    self.tags = $('.tags', self);
    self.resource = $('.tag-resource', self);
    self.tagCount = $('.tag-item', self).size();

    /*聚焦*/
    self.click(function () {
        self.tagEditor.focus();
    });

    /*点击标签资源中的某项时发生*/
    $('li', self.resource).click(function () {
        self.tagEditor.val($(this).text());
        self.submitTag();
        self.hideTagRes();
    });

    /*提交文本输入框中新的标签*/
    self.submitTag = function () {
        var e = self.tagEditor;
        var tag = e.val();
        if (tag.trim() === '')
            return;
        var liHtml = '<li class="tag-item" id="tag-item-' + self.tagCount + '"><span>' + tag + '</span>';
        liHtml += '<a href="javascript:;" onclick="$(\'li#tag-item-' + self.tagCount + '\').remove();" class="x-tag">x</a>';
        liHtml += '<input type="hidden" name="Tags" value="' + tag + '"/></li>';
        $(liHtml).appendTo(self.tags);
        self.tagCount++;
        e.val('');
    }

    /*显示标签资源*/
    self.showTagRes = function () {
        self.resource.show().follow(self.tagEditor);
    }

    /*隐藏标签资源*/
    self.hideTagRes = function () {
        self.resource.hide();
    }

    /*监听按下回车键事件，
    keyCode=13=>Enter 
    iKeyCode=32=>Space   
    iKeyCode=188=>,*/
    self.tagEditor.keydown(function (evt) {
        var iKeyCode = window.event ? event.keyCode : evt.which;
        if (iKeyCode == 188 || iKeyCode == 13 || iKeyCode == 32) {
            if (window.event) //IE
                event.returnValue = false;
            else //Firefox
                evt.preventDefault();
            self.submitTag();
        }
        //ajax添加新的标签
    }).keyup(function () {
        var e = $(this);
        var tag = e.val();
        self.search(tag);
    });

    /*搜索近似的标签*/
    self.search = function (part) {
        var flag = false;
        $('li', self.resource).each(function (i, n) {
            $(n).removeClass('tag-selected');
            if (part === '')
                return;
            if ($(n).text().indexOf(part) > -1) {
                $(n).addClass('tag-selected');
                flag = true;
            }
        });
        if (flag)
            self.showTagRes();
        else
            self.hideTagRes();
    }
}

/*
将$元素居中于窗口
@param:zindex[Number]css属性z-index
*/
$.fn.center = function (zindex) {
    var top = 0;
    var left = 0;
    var width = $(this).width();
    var height = $(this).height();
    top = (document.documentElement.clientHeight - height) / 2 + document.documentElement.scrollTop + document.body.scrollTop;
    left = (document.documentElement.clientWidth - width) / 2;
    zindex = zindex || 1990;
    $(this).css({ 'position': 'absolute', 'top': top, 'left': left, 'z-index': zindex });
    $(this).addClass('centerable-dialog').show(50, "easeInOutElastic");
    return this;
}

/*
将指定的Jquery对象内包含的所有input和textarea子元素的name/value提交到指定的url
url,
action,
mutiform_data,
success[Function(json|text)],
type
*/
$.fn.ajaxSubmit2 = function (o) {
    if (!this.get(0)) return;
    if (!o || typeof o != 'object') return;
    if (!o.action) return;
    var form = this;
    o.url = o.url || (form.attr('action') || window.location.pathname);
    o.type = o.type || 'json';
    o.mutiform_data = o.mutiform_data || {};
    $('select[name],textarea[name],input[name]', this).each(function (i, n) {
        var name = $(n).attr('name');
        if (o.mutiform_data[name])
            o.mutiform_data[name] += ',' + $(n).val();
        else
            o.mutiform_data[name] = $(n).val();
    });

    o.success = o.success || function (d) { };

    nt.ajax({
        method: 'post',
        url: o.url,
        action: o.action,
        data: o.mutiform_data,
        success: o.success,
        type: o.type
    });
}

/*
将json对象中的数据拷贝到form表单的字段上
*/
$.fn.fillForm = function (json) {
    $('select[name],textarea[name],input[name]', this).each(function (i, n) {
        var name = $(n).attr('name');
        var ntType = $(n).attr('nt-type');
        var type = $(n).attr('type');
        if (json[name] != undefined) {
            if (type === 'checkbox') {
                $(n).attr('checked', json[name]);
            }
            if (ntType === 'checkbox') {
                $('#' + name).setOnOff(json[name]);
            }
            $(n).val(json[name]);
        }
    });
}

/*
表单提交（ajax）
param:
@formId  the id of a form control which must modified by  insert char '#' in front
o:
@url:   the url that will be requested
@action:the action(a function or a method) that will be called on server
@data: send to server
*/
nt.ajaxSubmit = function (formId, o) {
    if (typeof formId != 'string' || formId.charAt(0) != '#')
        return;
    if (typeof o != 'object' ||
        o.action === undefined ||
        o.action === '') return;

    var options = {};
    $.extend(options, { url: '/handlers/ajaxhandler.aspx', data: {}, action: '', ok: function (j) { } }, o);

    $(formId).ajaxSubmit({
        url: options.url + '?action=' + options.action + '&' + Math.random(),
        forceSync: true,
        dataType: 'json',
        data: options.data,
        beforeSubmit: function () {
            nt.showLoading();
        },
        success: function (json, statusText) {
            if (json.error) {
                //not login
                if (json.error == 2) {
                    alert(json.message, function () {
                        window.location.href = nt.config.loginUrl;
                    });
                } else if (json.error == 3) {
                    //not authorized
                    alert(json.message, function () {
                        window.location.href = nt.config.notAuthorizedUrl;
                    });
                }
            } else {
                if (typeof options.ok === 'function') options.ok(json);
            }
            nt.removeLoading();
        },
        error: function () {
            error('操作错误!');
            nt.removeLoading();
        }
    });
}

/*
获取Model
params:
@id:int model的id
@url:String  请求的路径，没有时默认为window.location.pathname
@action:String 请求的方法，默认为Ajax_Get
*/
nt.fetchModel = function (id, success, action, url) {
    if (id === undefined) {
        return;
    }
    var model = {};
    var url = url || window.location.pathname;
    var action = action || 'Ajax_Get';
    nt.ajax({
        url: url,
        action: action,
        data: { id: id },
        success: function (json) {
            if (json.error) {
                alert(json.message);
            } else {
                if (typeof success === 'function')
                    success(json);
            }
        }
    });
};

/*
url
data,
action,
success(json or text),
type[json|text]
method[String post|get]
*/
nt.ajax = function (o) {
    if (!o || typeof o != 'object') return;
    if (!o.action) return;
    o.url = o.url || window.location.pathname;
    o.success = o.success || function (json) { };
    o.type = (o.type) || 'json';
    o.data = o.data || {};
    o.method = o.method || 'post';
    $.ajax({
        type: o.method,
        url: o.url + '?action=' + o.action + '&' + Math.random(),
        data: {},
        beforeSend: function (xhr) {
            nt.showLoading();
        },
        data: o.data,
        dataType: o.type,
        error: function (xhr, msg) {
            error(msg);
            nt.removeLoading();
        },
        success: function (data, textStatus, xhr) {
            var error = 0;
            if (o.type != 'json') {
                try {
                    var json = $.parseJSON(data);
                    error = json.error;
                } catch (e) {
                    error = 0;
                }
            } else {
                error = data.error;
            }
            //not login
            if (error == 2) {
                alert(data.message, function () {
                    var redirectUrl = escape(getRawUrl());
                    window.location.href = nt.config.loginUrl + '?redirectUrl=' + redirectUrl;
                });
                return;
            }
            //not authorized
            if (error == 3) {
                alert(data.message, function () {
                    window.location.href = nt.config.notAuthorizedUrl;
                });
                return;
            }
            nt.removeLoading();
            o.success(data);
        }
    });
};

/*重载*/
nt.reload = function () {
    window.location.href = window.location.href;
};

/*显示蒙板*/
nt.showMask = function (recreate) {
    if (!recreate && nt.maskCreated) {
        return;
    }
    if (nt.maskCreated) nt.mask.remove();
    var css = 'position: absolute; left: 0; top: 0; width: 100%; height: 100%; z-index:1988; ';
    css += 'background-color: #000; filter: alpha(opacity=50);-moz-opacity: 0.5; opacity:0.5;'
    var html = '<div id="admin-mask" style="' + css + '"></div>';
    $(document.body).append(html);
    nt.mask = $('#admin-mask');
    nt.mask.css({
        width: document.documentElement.scrollWidth,
        height: document.documentElement.scrollHeight
    });
    nt.maskCreated = true;
};

/*移除蒙板*/
nt.removeMask = function () {
    if (nt.maskCreated && !nt.hasDialogOn()) {
        nt.mask.remove();
        nt.maskCreated = false;
    }
};

/*是否存在打开的窗口*/
nt.hasDialogOn = function () {
    var c = 0;
    $('.centerable-dialog').each(function (i, n) {
        if ($(n).is('#loadingBox>img')) return true;
        if (n.style.display != 'none') c++;
    });
    return c > 0;
};

/*显示正在加载状态*/
nt.showLoading = function () {
    if (nt.loadingBoxCreated)
        return;
    var css = 'position: absolute; left: 0; top: 0; width: 100%; height: 100%; z-index:1988;background-color: #fff; filter: alpha(opacity=50);-moz-opacity: 0.5; opacity:0.5;';
    var loadBox = '<div  id="loadingBox" ' +
        'style="' + css + '"' +
        '><img alt="正在加载..." src="' + nt.loadingImgSrc + '"/></div>';
    $(document.body).append(loadBox);
    nt.loadingBox = $('#loadingBox');
    nt.loadingBox.css({
        width: document.documentElement.scrollWidth,
        height: document.documentElement.scrollHeight
    });
    nt.loadingBox.find('img').center(1989);
    nt.loadingBoxCreated = true;
}

/*移除正在加载状态*/
nt.removeLoading = function () {
    if (nt.loadingBoxCreated) {
        nt.loadingBox.remove();
        nt.loadingBoxCreated = false;
    }
}

nt.fn.alertOK = function () { $('#alertBox').remove(); nt.removeMask(); }
nt.fn.confirmOK = function () { $('#confirmBox').remove(); nt.removeMask(); }
nt.fn.confirmCancel = function () { $('#confirmBox').remove(); nt.removeMask(); }

/*
警告框
msg[String]消息
fn[Function]回调函数
*/
window.alert = function (msg, fn) {
    var alertBoxHtml = '';
    alertBoxHtml += '<div id="alertBox" class="nt-dialog" style="width:300px;">';
    alertBoxHtml += '<div class="nt-dialog-header nt-drag-bar">';
    alertBoxHtml += '<img src="/netin/content/images/alert.png" style="margin:8px;"/>';
    alertBoxHtml += '<span style="width:255px;float:right;">警告<a title="关闭" href="javascript:;" onclick="$(\'#alertBox\').remove(); nt.removeMask(); ">x</a></span></div>';
    alertBoxHtml += '<div class="nt-dialog-body"><p>' + msg + '</p>';
    alertBoxHtml += '</div>';
    alertBoxHtml += '<div class="nt-dialog-footer">';
    alertBoxHtml += '<a href="javascript:;" class="a-button a-button-small" onclick="nt.fn.alertOK();">确定</a>';
    alertBoxHtml += '</div>';
    alertBoxHtml += '</div>';
    nt.showMask();
    $(document.body).append(alertBoxHtml);
    $('#alertBox').center().movable();
    (typeof (fn) != 'function') || (nt.fn.alertOK = function () { $('#alertBox').remove(); nt.removeMask(); fn(); });
}

/*
确认框
msg[String]消息
fn0[Function]回调函数
fn1[Function]回调函数
*/
window.confirm = function (msg, fn0, fn1) {
    var confirmBoxHtml = '';
    confirmBoxHtml += '<div id="confirmBox"  class="nt-dialog"  style="width:300px;">';
    confirmBoxHtml += '<div   class="nt-dialog-header nt-drag-bar">';
    confirmBoxHtml += '<img src="/netin/content/images/confirm.png" style="margin:8px;"/>';
    confirmBoxHtml += '<span style="width:255px;float:right;">确认<a title="关闭" href="javascript:;" onclick="$(\'#confirmBox\').remove(); nt.removeMask();">x</a></span>';
    confirmBoxHtml += '</div>';
    confirmBoxHtml += '<div  class="nt-dialog-body"><p>' + msg + '</p></div>';
    confirmBoxHtml += '<div  class="nt-dialog-footer">';
    confirmBoxHtml += '<a href="javascript:;" class="a-button a-button-small" onclick="nt.fn.confirmCancel();">取消</a>';
    confirmBoxHtml += '&nbsp;&nbsp;&nbsp;';
    confirmBoxHtml += '<a href="javascript:;" class="a-button a-button-small" onclick="nt.fn.confirmOK();">确定</a>';
    confirmBoxHtml += '</div>';
    confirmBoxHtml += '</div>';
    $(document.body).append(confirmBoxHtml);
    nt.showMask();
    $('#confirmBox').center().movable();
    (typeof (fn0) != 'function') || (nt.fn.confirmOK = function () { $('#confirmBox').remove(); nt.removeMask(); fn0(); });
    (typeof (fn1) != 'function') || (nt.fn.confirmCancel = function () { $('#confirmBox').remove(); nt.removeMask(); fn1(); });
}

/*
提示框
msg[String]消息
delay[Number]生命周期
@type:0=error,1=warning,2=success
*/
window.tips = function (msg, delay, type, html) {
    var delay = delay || 6000;
    if (document.getElementById('tipsBox')) {
        $('#tipsBox').stop().remove();
    }
    var typeClass = '', title = '注意';
    switch (type) {
        case 0:
            typeClass = 'nt-dialog-error';
            title = '错误';
            break;
        case 1:
            typeClass = 'nt-dialog-warning';
            title = '警告';
            break;
        case 2:
            typeClass = 'nt-dialog-success';
            title = '成功';
            break;
        default:
            break;
    }
    var tipsHtml = '';
    tipsHtml += '<div id="tipsBox" class="nt-dialog-tips';
    if (typeClass) tipsHtml += ' ' + typeClass;
    tipsHtml += '">';
    tipsHtml += '<div class="tips-image"></div>';
    tipsHtml += '<div class="tips-content">';
    tipsHtml += '<h4>' + title + '</h4>';
    tipsHtml += '<p>' + msg + ((typeof html === 'string') ? html : '') + '</p>';
    tipsHtml += '</div><div class=\"tips-close\"><a href="javascript:;" onclick="$(\'#tipsBox\').remove();"></a></div></div>';
    $(document.body).append(tipsHtml);
    $('#tipsBox').animate({ top: 0 }, 800, 'easeInOutElastic', function () {
        setTimeout(function () {
            $('#tipsBox').fadeOut(200, null, function () {
                $('#tipsBox').remove();
            });
        }, delay);
    });
}

/*错误框*/
window.error = function (msg, delay, html) {
    window.tips(msg, delay, 0, html);
}

/*警告框*/
window.warning = function (msg, delay, html) {
    window.tips(msg, delay, 1, html);
}

/*成功*/
window.success = function (msg, delay, html) {
    window.tips(msg, delay, 2, html);
}
/*通知*/
window.notice = function (msg, delay, html) {
    window.tips(msg, delay, 3, html);
}

/*
是否为函数
*/
function isFunc(obj) {
    return typeof obj === 'function';
}

/*
title:标题
data:like [{text:'xx',value:78},{text:'vv',value:120},...]
current is a value,for example 78.
ok is a function(value, text) which will be called after one item selected
width:宽度
height:高度
*/
nt.openSelectionWindow = function (title, data, current, ok, width, height) {
    var w = width || 250;
    var h = height || 100;
    var html = '';
    if (typeof data === 'object') {
        html += '<ul>';
        for (var i in data) {
            if (current == data[i].value)
                html += '<li class="nt-selector-current" ';
            else
                html += '<li ';
            //here we cannot set value as the attribute name,we use val in short.
            html += ' val="' + data[i].value + '">';
            html += data[i].text;
            html += '</li>';
        }
        html += '</ul>';
    } else {
        html = data;
    }

    nt.createHtmlWindow({
        title: title, html: html, width: width, height: height, id: 'selectorBox', created: function () {
            var that = this;
            $('li', this).click(function () {
                var jqObj = $(this);
                if (jqObj.hasClass('nt-selector-current')) {
                } else {
                    var text = jqObj.text();
                    var value = jqObj.attr('val');
                    if (isFunc(ok)) {
                        ok.call(that, value, text);
                    }
                }
                that.close();
            });
        }
    });
}

/*
title:标题
data:like [{text:'xx',value:78},{text:'vv',value:120},...]
current String is a value,for example '78,120'.
ok is a function(value, text) which will be called after one item selected
width:宽度
height:高度
*/
nt.openMutiSelectorWin = function (title, data, current, ok, width, height) {
    var html = '';
    var x = 0;
    var current = current || '';
    current = ',' + current + ',';
    for (var i in data) {
        html += '<input type="checkbox"';
        if (current.indexOf(data[i].value) > -1)
            html += ' checked="checked"';
        html += ' id="nt-muti-selector-' + x + '" value="' + data[i].value + '"/>';
        html += '<label for="nt-muti-selector-' + x + '">' + data[i].text + '</label>&nbsp;&nbsp;';
        x++;
    }
    nt.createHtmlWindow({
        title: title,
        html: html,
        width: width,
        height: height,
        id: 'mutiSelectorBox',
        created: function () {
        },
        ok: function () {
            var that = this;
            if (isFunc(ok)) ok.call(this);
        }
    });
}

/*
title:标题
html:html代码
ok is a function
cancel  is a function
created:function
width:宽度
height:高度
*/
nt.createHtmlWindow = function (o) {
    if (!window.htmlEditorId)
        window.htmlEditorId = 0;
    else
        window.htmlEditorId++;
    var options = { title: 'untitled', html: '', width: 250, height: 100, ok: function () { }, cancel: function () { }, id: 'htmlEditor-' + window.htmlEditorId, created: function () { } };
    $.extend(options, o);
    var html = [
        '<div class="html-content" id="' + options.id + '" style="width:' + options.width + 'px;height:' + options.height + 'px;">',
            '<div class="html-content-top nt-drag-bar">',
                '<span class="html-content-title">',
                options.title,
                '</span><a href="javascript:;" role="close">x</a>',
            '</div>',
            '<div class="html-content-body">',
            options.html,
            '</div>',
            '<div class="html-content-footer">',
                '<a href="javascript:;" class="a-button" role="ok">确定</a><a href="javascript:;" class="a-button" role="cancel">取消</a>',
            '</div>',
        '</div>',
        '</div>'
    ].join('');

    $(document.body).append(html);

    var tar = $('#' + options.id);
    tar.css('max-width', '2000px');

    tar.close = function () {
        tar.remove();
        nt.removeMask();
    }

    if (isFunc(options.created))
        options.created.call(tar);

    //close
    $('a[role="close"]', tar).click(function () {
        tar.close();
    });
    //cancel
    $('a[role="cancel"]', tar).click(function () {
        if (isFunc(options.cancel)) options.cancel.call(tar);
        tar.close();
    });
    //ok
    $('a[role="ok"]', tar).click(function () {
        if (isFunc(options.ok)) options.ok.call(tar);
        tar.close();
    });

    nt.showMask();
    tar.center().movable();
}

/*
@params:
url                  ''
editor             'tmEditor' 
form              'tmForm' 
action2post       'tmPost' 
action2del         'tmDel' 
action2get      'tmGet' 
action2list       'list' 
listDivId           'list'
editButtons      '.edit'
delButtons       '.del'
addButtons      'a[role="add"]'
ajax2page         false
--------Function-----
-  posted                
-  got                     
-  deleted                
-  listed
-  onPost
-  formFilled
-  onAdd
----------Style--------------
width
*/
nt.tableMgr = function (o) {
    var me = this;
    //参数选项
    me.o = {
        width: 0,
        url: '',
        title: 'unTitled',
        editor: '#tmEditor', form: '#tmForm',
        action2post: 'tmPost', action2del: 'tmDel', action2get: 'tmGet', action2list: 'tmList',
        listDivId: 'list',
        editButtons: '.edit',
        delButtons: '.del',
        addButtons: 'a[role="add"]',
        delSelectedButtons: 'a[role="del-selected-button"]',
        reOrderButtons: '.nt-btn-reorder',
        onPost: function () { },
        posted: function (json) { },
        formFilled: function () { },
        onAdd: function () { },
        got: function (json) { },
        deleted: function (json) { },
        listed: function () { },
        ajax2page: false
    };

    if (typeof o === 'object')
        $.extend(me.o, o);

    //如果o是function类型则调用
    me._tryCallFunc = function (o, args) {
        if (typeof o === 'function') {
            o.call(me, args);
        }
    };

    //打开编辑器窗口
    me.open = function (id) {
        nt.showMask();
        $(me.o.form).trigger('reset');
        me._tryCallFunc(me.o.onAdd);
        if (id === undefined) {
            $('input[name="Id"]', me.o.form).val(0);
            var e = $(me.o.editor);
            e.find('.html-content-title').html('添加' + me.o.title);
            e.center().movable();
        }
        else {
            nt.fetchModel(id, function (json) {
                me._tryCallFunc(me.o.got, json);
                $(me.o.form).fillForm(json);
                me._tryCallFunc(me.o.formFilled, json);
                var e = $(me.o.editor);
                e.find('.html-content-title').html('编辑' + me.o.title);
                e.center().movable();
            }, me.o.action2get);
        }
    };

    //关闭编辑器窗口
    me.close = function () {
        $(me.o.editor).hide();
        nt.removeMask();
    };

    //提交表单数据
    me.post = function () {
        me._tryCallFunc(me.o.onPost);
        $(me.o.form).ajaxSubmit({
            url: $(me.o.form).attr('action'),
            forceSync: true,
            dataType: 'json',
            data: { action: me.o.action2post },
            beforeSubmit: function () {
                nt.showLoading();
            },
            success: function (json, statusText) {
                me._tryCallFunc(me.o.posted, json);
                me.list();
                me.close();
            },
            error: function () {
                error('操作错误!');
                nt.removeLoading();
                me.close();
            }
        });
    };

    //删除一个记录
    me.del = function (id) {
        confirm('您确定删除？', function () {
            nt.ajax({
                action: me.o.action2del,
                data: { id: id },
                success: function (json) {
                    if (json.error) {
                        error(json.message);
                    }
                    else {
                        success('删除成功！');
                        me._tryCallFunc(me.o.deleted, json);
                        me.list();
                    }
                }
            });
        });
    };

    //重新列表
    me.list = function () {
        refreshList(
                me.o.listDivId,
                me.o.action2list,
                false,
                function () {
                    me._tryCallFunc(me.o.listed);
                    me.bind();
                }, me.currentPage);
    };

    //绑定事件处理程序
    me.bind = function () {
        $(me.o.editButtons).click(function () {
            var id = $(this).attr('data-id');
            me.open(id);
        });

        $(me.o.delButtons).click(function () {
            var id = $(this).attr('data-id');
            me.del(id);
        });

        $(me.o.delSelectedButtons).click(function () {
            delSelected(false, me.list);
        });

        $(me.o.reOrderButtons).each(function (i, n) {
            n.onclick = function () { reOrder(undefined, me.list); };
        });

        $(me.o.addButtons).click(function () {
            me.open();
        });
    };

    //初始化
    me.init = function () {
        $(document).ready(function () {

            if (me.o.width != 0)
                $(me.o.editor).css({ width: me.o.width, 'max-width': '2000px' });

            $('a[role="post"]', me.o.editor).click(function () {
                me.post();
            });

            $('a[role="close"]', me.o.editor).click(function () {
                me.close();
            });

            me.bind();

            if (me.o.ajax2page) {
                window.goto = function (page) {
                    me.currentPage = page;
                    me.list();
                }
            }
        });
    };

};

/*
刷新验证码
*/
function refreshCode(imgId) {
    var checkCode = null;
    var rand = Math.random();
    var _imgId = imgId || "checkCode";
    if (typeof _imgId == 'string') {
        checkCode = document.getElementById(_imgId);
    }
    else {
        checkCode = _imgId;
    }
    var src = checkCode.src.split('?');
    checkCode.src = src[0] + "?" + rand;
}

/*
刷新列表数据（如果有）
list:[String]需要刷新的div的id
auto:[True|False]是否在没有找到指定的列表div元素的情况下自动刷新整个页面
*/
function refreshList(listId, action, auto, c, page) {
    var list = document.getElementById(listId);
    if (!list) {
        if (!auto)
            return;
        nt.reload();
    }
    var pair = getQuery();
    if (page) pair.page = page;
    nt.ajax({
        action: action || 'list',
        data: pair,
        success: function (text) {
            list.innerHTML = text;
            $('input.input-number').mustBeInt32(); //number
            $('input.order-item').change(function () {
                $(this).data('data-changed', true);
            });
            if (typeof c === 'function') c();
        },
        type: 'text'
    });
}

/*
open window
options说明
url:[String]return if empty
name:[String]_blank
width:[Number]800
height:[Number]600
replace:[False|True]False
*/
function openWindow(options) {
    var n = '_blank', w = 600, h = 600, r = false, u = '';

    if (typeof options === 'string') {
        u = options;
    } else {
        u = options.url;
        n = options.name || n;
        w = options.width || w;
        h = options.height || h;
        r = options.replace || r;
    }
    if (!u) return;
    var l = (screen.availWidth - w) / 2,
    t = (screen.availHeight - h) / 2;

    var browserInfo = getBrowserInfo();

    if (window.showModalDialog) {
        window.showModalDialog(u, window, 'dialogWidth:' + w + 'px;dialogHeight:' + h + 'px;dialogTop: ' + t + 'px; dialogLeft: ' + l + 'px;center:yes;scroll:no;status:no;resizable:0;location:no');
    } else {
        window.open(u, n, 'height=' + h + ', width=' + w + ', top=' + t + ', left=' + l + ', toolbar=no, menubar=no, scrollbars=yes, resizable=yes,location=no, status=no,modal=yes', r);
    }

}

/*
function:获取参数键值对
params
url(String)：指定的url,如果未指定则使用window.location.href
return(Object)
参数键值对
*/
function getQuery(url) {
    var tUrl = '';
    if (typeof url != 'string' || url.length == 0)
        tUrl = window.location.href;
    else
        tUrl = url;
    //modify query string
    var p = tUrl.indexOf('?');
    if (p < 0)
        return {};
    var query = {};
    tUrl = tUrl.substring(p + 1, tUrl.length);
    p = tUrl[0];
    if (p != '&')
        tUrl = '&' + tUrl;
    p = tUrl[tUrl.length - 1];
    if (p != '&')
        tUrl = tUrl + '&';

    var lastAnd = 0, and = 0, eq = 0;
    var name = '', value = '';
    var limit = tUrl.length;
    while (limit > 0) {
        limit--;
        and = tUrl.indexOf('&', lastAnd + 1);
        eq = tUrl.indexOf('=', lastAnd + 1);
        if (and - lastAnd < 2) {
            lastAnd = and;
            continue;
        }
        if (eq > and) {
            name = tUrl.substring(lastAnd + 1, and);
            value = '';
        } else {
            if (eq == -1) {
                name = tUrl.substring(lastAnd + 1, and);
                value = '';
            } else {
                name = tUrl.substring(lastAnd + 1, eq);
                value = tUrl.substring(eq + 1, and);
            }
        }
        if (name && name.length > 0)
            query[name] = value;
        if (and == tUrl.length - 1)
            break;
        lastAnd = and;
    }
    return query;
}

/*
get the information of current browser
*/
function getBrowserInfo() {
    var Sys = {};
    var ua = navigator.userAgent.toLowerCase();
    var s;
    (s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
    (s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
    (s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
    (s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
    (s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;
    return Sys;
}

/*
获取RawUrl
*/
function getRawUrl() {
    var p0 = window.location.protocol; //http:
    var p1 = window.location.hostname; //localhost
    var port = window.location.port; //80
    var p2 = port == '80' ? '' : ':' + port;
    var leftLen = p0.length + 2 + p1.length + p2.length;
    return window.location.href.substr(leftLen);
}

/*
获取最大天数
*/
function getMaxDay(year, month) {
    if (month == 4 || month == 6 || month == 9 || month == 11)
        return "30";
    if (month == 2)
        if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
            return "29";
        else
            return "28";
    return "31";
}

/*
智能输入
*/
function registerSmartInput(selector) {
    selector = selector || '.smart-input';
    var aInp = $(selector);
    var i = 0;
    var sArray = [];
    for (i = 0; i < aInp.size() ; i++) {
        aInp.get(i).index = i;
        sArray.push(aInp[i].value);
        aInp.get(i).onfocus = function () {
            if (sArray[this.index] == aInp.get(this.index).value) {
                aInp.get(this.index).value = '';
            }
        };
        aInp.get(i).onblur = function () {
            if (aInp.get(this.index).value == '') {
                aInp.get(this.index).value = sArray[this.index];
            }
        };
    }
}

/*
展开下拉列表
@sender 事件发送者
*/
function ddlExtends(sender) {
    var d = $(sender).next();
    var flag = sender.extended;
    if (flag) {
        d.slideUp(50).css('z-index', '');
        sender.extended = false;
    }
    else {
        $('.drop-down-list-bar').each(function (i, n) {
            $(this).next().css({ 'z-index': '', display: 'none' });
            this.extended = false;
        });
        d.css('z-index', 2014).slideDown(150);
        sender.extended = true;
    }
}

/*
CheckBox
@sender 事件发送者
*/
function onOff(sender) {
    var a = $(sender);
    if (a.hasClass('on')) {
        a.removeClass('on');
        a.next().val('false');
    }
    else {
        a.addClass('on');
        a.next().val('true');
    }
}

/*
@v String Or Number e.g. '2',2
*/
$.fn.setDDL = function (v) {
    if (!this.hasClass('drop-down-list'))
        return;
    var lis = $('.ddl-item', this);
    var size = lis.size();
    for (var i = 0; i < size; i++) {
        var e = lis.get(i);
        if (e.getAttribute('data-value') == v) {
            $(e).click();
            break;
        }
    }
}

/*
@v Boolean e.g. true
@v String e.g. 'true'
@v Number e.g. 1
true 1*/
$.fn.setOnOff = function (v) {
    if (!this.hasClass('on-off')) return;
    var f = false;
    var t = typeof (v);
    if (t === 'string')
        f = v.toLowerCase() === 'true';
    else if (t == 'boolean')
        f = v;
    else if (t === 'number')
        f = v != 0;
    if (f) {
        $(this).find('a').addClass('on');
        $(this).find('input').val('true');
    } else {
        $(this).find('a').removeClass('on');
        $(this).find('input').val('false');
    }
}

/*
@v  String e.g. '1,2,3,4'
*/
$.fn.setCBL = function (v) {
    if (!this.hasClass('check-box-list')) return;
    if (v === '') return;
    var name = $(this).attr('data-field-name');
    $('.cbl-item', this).each(function (i, n) {
        var that = $(n), value = that.attr('data-value');
        that.removeClass('cbl-selected');
        that.find('input').remove();
        if (v.indexOf(value, 0) > -1) {
            that.click();
        }
    });
}

/*
@v String Or Number e.g. '2',2
*/
$.fn.setRBL = function (v) {
    if (!this.hasClass('radio-button-list')) return;
    var lis = $('.rbl-item', this);
    var size = lis.size();
    for (var i = 0; i < size; i++) {
        var e = lis.get(i);
        if (e.getAttribute('data-value') == v) {
            $(e).click();
            break;
        }
    }
}

/*登出*/
function logout() {
    nt.ajax({
        url: '/netin/handlers/ajaxHandler.aspx',
        action: 'logout',
        success: function (json) {
            if (json.error) error(json.message);
            else {
                window.location.href = '/netin/login.aspx';
            }
        }
    });
}

/*重新启动软件*/
function restartApp() {
    nt.ajax({
        url: '/netin/handlers/ajaxhandler.aspx',
        action: 'restartApp',
        success: function (j) {
            if (j.error) error(j.message);
            else {
                nt.reload();
            }
        }
    });
}

/*
rootDirPath:要浏览的根目录
width：窗口的宽度[800]
height：窗口的高度[600]
onfiledblclick：当文件项被双击时发生(arg:)
loadSubDirs：是否连子文件夹一起读取，默认为false
title:标题[untitled]
loadImg:单击时是否加载图片[false]
filter:文件格式过滤默认为[.aspx]
*/
nt.serverBrowser = function (options) {
    var self = this;
    self.rootDirPath = options.rootDirPath;
    self.width = options.width || 800;
    self.height = options.height || 400;
    self.onfiledblclick = options.onfiledblclick;
    self.loadSubDirs = options.loadSubDirs || false;
    self.title = options.title || 'untitled';
    self.loadImg = options.loadImg || false;
    self.filter = options.filter || '.aspx';
    self.dirQueue = [];
    self.htmlHolder = '';
    self.htmlBox = null;
    self.jsonData = null;
    self.dataLoaded = false;
    self.rootID = -100;
    self.currentDirPath = self.rootDirPath;

    self.init = function () {
        self.htmlHolder = 'server-dialog-' + Math.random().toString().substr(2, 5);
        $(document.body).append('<div class="nt-dialog" style="width:' + self.width + 'px;" id="' + self.htmlHolder + '"/>');
        self.htmlHolder = '#' + self.htmlHolder;
        self.htmlBox = $(self.htmlHolder);
        self.htmlBox.append('<div class="nt-dialog-header nt-drag-bar"><span>' + self.title + '</span><a href="javascript:;" class="nt-dialog-back">←</a></div>');
        self.htmlBox.append('<div class="nt-dialog-body" style="height:' + self.height + 'px;"><ul class="files-wrap"></ul></div>');
        self.htmlBox.append('<div class="nt-dialog-footer"><a href="javascript:;" class="a-button" role="nt-dialog-close">关闭</a></div>');
        self.htmlBox.movable();
        $('a.nt-dialog-back', self.htmlHolder).click(function () {
            if (self.dirQueue.length < 2) {
                tips('已是根目录.');
                return false;
            }
            self.dirQueue.pop();
            self.render(self.dirQueue.pop());
        });

        $('a[role="nt-dialog-close"]', self.htmlHolder).click(function () {
            self.close();
        });
    }

    self.__findTargetDirJson = function (dirId) {
        return this.__findTargetDirJson2(dirId, self.jsonData);
    }

    /*用于递归计算的*/
    self.__findTargetDirJson2 = function (dirId, json) {
        var target = null;
        for (var item in json) {
            var mapper = json[item];
            if (typeof mapper != 'object')
                continue;
            if (mapper.id == dirId) {
                target = mapper;
            }
            else {
                target = self.__findTargetDirJson2(dirId, mapper);
            }
            if (target != null) break;
        }
        return target;
    }

    /*载入*/
    self.load = function (ok) {
        if (!self.__validate()) return;
        var data = {};
        data['dir-path'] = escape(self.currentDirPath);
        data['load-sub-dir'] = self.loadSubDirs;
        data['filter'] = self.filter;
        nt.ajax({
            url: '/netin/handlers/ajaxHandler.aspx',
            action: 'ServerBrowse',
            data: data,
            success: function (json) {
                self.jsonData = json;
                if (typeof ok === 'function')
                    ok.call(self);
            },
            type: 'json',
            method: 'get'
        });
    }

    self.__renderByJson = function (json) {
        var ul = self.htmlBox.find("ul:eq(0)");
        ul.empty();
        for (var item in json) {
            var n = json[item];
            if (typeof n == 'object') {
                if (n.type == 0)
                    ul.append('<li data-item-id="' + n.id + '" class="folder-item" title="' + item + '" path="' + n.url + '"><div class="nt-folder"></div><div class="nt-folder-name"><span>' + item.substr(0, 25) + '</span></div></li>');
                else {
                    ul.append('<li data-item-id="' + n.id + '" ext="' + n.format + '" class="file-item ' + n.format.substr(1) + '" title="' + item + '" path="' + n.url + '"><div class="nt-file"><span>' + n.format.substr(1) + '</span></div><div class="nt-file-name"><span>' + item.substr(0, 25) + '</span></div></li>');
                }
                console.log(n.id);
            }
        }

        /*文件夹双击时打开*/
        $('li.folder-item', self.htmlHolder).dblclick(function () {
            if (self.loadSubDirs)
                self.render($(this).attr('data-item-id'));
            else
                self.render($(this).attr('path'));
        });

        /*文件双击时，传入参数为path*/
        $('li.file-item', self.htmlHolder).dblclick(function () {
            if (typeof self.onfiledblclick === 'function')
                self.onfiledblclick($(this).attr('path'));
        });

        /*单击图片时加载图片并显示原图*/
        if (self.loadImg) {
            $('li.file-item', self.htmlHolder).filter(function () {
                var formatofimg = '|.gif|.png|.jpg|.jpeg|.bmp';
                return formatofimg.indexOf($(this).attr('ext').toLocaleLowerCase()) > 0;
            }).click(function () {
                if (self.currentImg) {
                    self.currentImg.remove();
                }
                self.currentImg = $('<img style="max-width:' + (self.width) + 'px;max-height:' + (self.height - 10) + 'px;cursor:pointer;display:none;border:1px solid #524b4b;"/>');
                self.currentImg.click(function () {
                    $(this).hide(500, function () {
                        self.currentImg.remove();
                    });
                });
                $(document.body).append(self.currentImg);
                self.currentImg.load(function () {
                    self.currentImg.css({
                        opacity: 0.8,
                        position: 'absolute',
                        'z-index': 2015,
                        top: self.htmlBox.position().top + (self.height - self.currentImg.height()) / 2 + 10,
                        left: self.htmlBox.position().left + (self.width - self.currentImg.width()) / 2
                    });
                    self.currentImg.show(600);
                });
                self.currentImg.attr('src', $(this).attr('path'));
            });
        }

        $('li', self.htmlHolder).hover(function () {
            $(this).css({ 'background-color': '#819cf7' });
        }, function () {
            $(this).css({ 'background-color': '' });
        });
    }

    //while self.loadSubDirs = true;
    self.__renderById = function (id) {
        var json = null;
        if (id == self.rootID)
            json = self.jsonData.root;
        else
            json = this.__findTargetDirJson(id);
        var path = json.path;
        self.currentDirPath = path;
        self.dirQueue.push(id);
        this.__renderByJson(json);
    }

    //while self.loadSubDirs = false;
    self.__renderByPath = function (path) {
        self.currentDirPath = path;
        self.dirQueue.push(path);
        self.load(function () {
            this.__renderByJson(self.jsonData.root);
        })
    }

    /*渲染*/
    self.render = function (arg) {
        if (arg) {
            if (self.loadSubDirs) {
                this.__renderById(arg);
            } else {
                this.__renderByPath(arg);
            }
        } else {
            if (self.loadSubDirs)
                self.dirQueue.push(self.rootID);
            else
                self.dirQueue.push(self.currentDirPath);
            this.__renderByJson(self.jsonData.root);
        }
    }

    self.__validate = function () {
        return true;
    }

    /*关闭窗口*/
    self.close = function () {
        self.htmlBox.hide();
        nt.removeMask();
        if (self.currentImg)
            self.currentImg.remove();
    }

    /*打开窗口*/
    self.open = function (tag) {
        if (!self.dataLoaded) {
            self.load(function () {
                self.render();
                self.dataLoaded = true;
                nt.showMask();
                self.htmlBox.show().center();
            });
        } else {
            nt.showMask();
            self.htmlBox.center();
        }
        self.tag = tag;
    }
}

/*日期选择器*/
/*获取光标在input控件中的位置*/
function getPositionForInput(ctrl) {
    var cursurPosition = -1;
    if (ctrl.selectionStart) {//非IE浏览器
        cursurPosition = ctrl.selectionStart;
    } else {//IE
        var range = document.selection.createRange();
        range.moveStart("character", -ctrl.value.length);
        cursurPosition = range.text.length;
    }
    return (cursurPosition);
}

/*获取元素*/
function G(objectId) {
    return typeof (objectId) === 'string' ? document.getElementById(objectId) : objectId;
}

/*获取控件的绝对位置Left*/
function getAbsoluteLeft(objectId) {
    var o = G(objectId);
    if (o === undefined || o == null) return 0;
    var oLeft = o.offsetLeft;
    while (o.offsetParent != null) {
        var oParent = o.offsetParent;
        oLeft += oParent.offsetLeft;
        o = oParent;
    }
    return oLeft;
}

/*获取控件的绝对位置Top*/
function getAbsoluteTop(objectId) {
    var o = G(objectId);
    if (o === undefined || o == null) return 0;
    var oTop = o.offsetTop;
    while (o.offsetParent != null) {
        var oParent = o.offsetParent;
        oTop += oParent.offsetTop;   // Add parent top position
        o = oParent;
    }
    return oTop;
}

/*获取控件的Width*/
function getElementWidth(objectId) {
    var o = G(objectId);
    if (o === undefined || o == null) return 0;
    return o.offsetWidth;
}

/*获取控件的Height*/
function getElementHeight(objectId) {
    var o = G(objectId);
    if (o === undefined || o == null) return 0;
    return o.offsetHeight;
}

/*滚动到指定位置*/
function scrollTo(objectId) {
    var top = getAbsoluteTop(objectId);
    if (document.body.scrollTop)
        document.body.scrollTop = top;//ie
    else
        document.documentElement.scrollTop = top;//firefox
}

$(document).ready(function () {
    $('input.input-decimal').mustBeDecimal(); //decimal
    $('input.input-number').mustBeInt32(); //number
    $('input.order-item').change(function () {
        $(this).data('data-changed', true);
    });

    //dropdownlist 弃用
    $('.ddl-item').click(function () {
        var ddl = this.parentNode.parentNode;
        ddl.children.item(2).value = this.getAttribute('data-value');
        $('.ddl-current', ddl).removeClass('ddl-current');
        $(this).addClass('ddl-current');
        var a = ddl.children.item(0);
        $(this.parentNode).slideUp(50);
        a.extended = false;
        var span = a.children.item(0);
        span.innerHTML = this.innerHTML;
        if (typeof nt.onDDLClick === 'function') {
            nt.onDDLClick(this.getAttribute('data-value'));
        }
    });

    //checkbox list
    $('.cbl-item').click(function () {
        var x = $(this), p = this.parentNode.parentNode;
        if (x.hasClass('cbl-selected')) {
            x.removeClass('cbl-selected');
            x.children('input').remove();
        } else {
            x.addClass('cbl-selected');
            var fieldName = p.getAttribute('data-field-name');
            $('<input name="' + fieldName + '" type="hidden" value="' + x.attr('data-value') + '"/>').appendTo(x);
        }
    });

    //radio buttons list
    $('.rbl-item').click(function () {
        var x = $(this), p = this.parentNode.parentNode;
        x.siblings().removeClass('rbl-current');
        x.addClass('rbl-current');
        p.children.item(1).value = x.attr('data-value');
    });
});

/*
重新居中所有显示的对话框
*/
$(window).resize(function () {
    $('.centerable-dialog').each(function (i, n) {
        if ($(n).css('display') != 'none') {
            $(n).center();
        }
    });

    if (nt.maskCreated) {
        nt.showMask(true);
    }

});


