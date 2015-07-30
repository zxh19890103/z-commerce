/// <reference path="jquery-1.7.2.min.js" />
/// <reference path="jquery.form.js" />


var nt = nt || {};
nt.mask = null;
nt.loadingBox = null;
nt.maskCreated = false;
nt.loadingBoxCreated = false;
nt.loadingImgSrc = '/content/img/loading.gif';
nt.fn = {};
nt.config = {};
nt.config.loginUrl = '/customer/login.aspx';//login page url
nt.config.dialogHeaderBg = 'background:#F2262C';
nt.config.dialogBodyBg = 'background:#F2F2F2';
nt.config.dialogFooterBg = 'background:#444444';
nt.config.dialogWidth = 500;
nt.config.dialogBodyMinHeight = 90;
nt.config.sendQueryString = false;//调用nt.ajax的时候，是否发送当前网址的参数

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
url
data,
action,
success(json or text),
type[json|text],
method[String post|get]
*/
nt.ajax = function (o) {
    if (!o || typeof o != 'object') return;
    if (!o.action) return;
    o.url = o.url || '/handlers/ajaxhandler.aspx';
    o.success = o.success || function (json) { };
    o.type = (o.type) || 'json';
    o.data = o.data || {};
    if (nt.config.sendQueryString) {
        $.extend(o.data, getQuery());
    }
    o.method = o.method || 'post';
    $.ajax({
        type: o.method,
        url: o.url + '?action=' + o.action + '&' + Math.random(),
        beforeSend: function (xhr) {
            nt.showMask();
            nt.showLoading();
        },
        data: o.data,
        dataType: o.type,
        error: function (xhr, msg) {
            error(msg);
            nt.removeMask();
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
            //未登录
            if (error == 2) {
                alert(data.message, function () {
                    var redirectUrl = escape(getRawUrl());
                    window.location.href = nt.config.loginUrl + '?redirectUrl=' + redirectUrl;
                });
                return;
            }
            nt.removeMask();
            nt.removeLoading();
            o.success(data);
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

    if (typeof o != 'object') return;
    var form = $(formId);
    //validate
    var error_size = 0;
    $('input[name]', form).each(function (i, n) {
        //console.log(n.onchange && n.onchange());
        if (n.onchange && n.onchange()) {
            error_size++;
        }
    });

    if (error_size > 0) return false;

    var options = {};
    $.extend(options, { url: form.attr('action'), data: {}, action: form.attr('invoke'), ok: function (j) { } }, o);

    form.ajaxSubmit({
        url: options.url + '?action=' + options.action + '&' + Math.random(),
        forceSync: true,
        dataType: 'json',
        data: options.data,
        beforeSubmit: function () {
            nt.showMask();
            nt.showLoading();
        },
        success: function (json, statusText) {
            nt.removeMask();
            nt.removeLoading();

            if (json.error) {
                //not login
                if (json.error == 2) {
                    alert(json.message, function () {
                        window.location.href = nt.config.loginUrl;
                    });
                } else {
                    error(json.message);
                }
            } else {
                if (typeof options.ok === 'function') options.ok(json);
            }
        },
        error: function () {
            error('操作错误!');
            nt.removeMask();
            nt.removeLoading();
        }
    });
}

/*重载*/
nt.reload = function () {
    window.location.href = window.location.href;
}

/*显示蒙板*/
nt.showMask = function () {
    if (nt.maskCreated) {
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
}

/*移除蒙板*/
nt.removeMask = function () {
    if (nt.maskCreated) {
        nt.mask.remove();
        nt.maskCreated = false;
    }
}

/*显示正在加载状态*/
nt.showLoading = function () {
    if (nt.loadingBoxCreated)
        return;
    var loadBox = '<img id="loadingBox" alt="正在加载..." src="' + nt.loadingImgSrc + '"/>';
    $(document.body).append(loadBox);
    nt.loadingBox = $('#loadingBox');
    nt.loadingBox.show();
    nt.loadingBox.center(1990);
    nt.loadingBoxCreated = true;
}

/*移除正在加载状态*/
nt.removeLoading = function () {
    if (nt.loadingBoxCreated) {
        nt.loadingBox.remove();
        nt.loadingBoxCreated = false;
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
    $(this).addClass('centerable-dialog').show(200, "easeInOutElastic");
    return this;
}

/*
绑定多个处理程序
*/
nt.fn.bindHandler = function (func, handler) {
    var old = nt.fn[func];
    if (typeof old != 'function') {
        nt.fn[func] = handler;
    } else {
        nt.fn[func] = function () {
            old();
            handler();
        }
    }
}

//close alert dialog
nt.fn.alertClose = function () { $('#alertBox').remove(); nt.removeMask(); };
//be called if ok to alert
nt.fn.alertOK = function () { nt.fn.alertClose(); };
//close comfirm dialog
nt.fn.confirmClose = function () { $('#confirmBox').remove(); nt.removeMask(); };
//be called if ok to confirm
nt.fn.confirmOK = function () { nt.fn.confirmClose(); };
//be called if cancel to confirm
nt.fn.confirmCancel = function () { nt.fn.confirmClose(); };

/*
msg消息
title标题
fn回调函数
*/
window.alert = function (msg, fn) {
    var alertBoxHtml = '';
    alertBoxHtml += '<div id="alertBox" class=\"nt-dialog-base\" style="width:' + nt.config.dialogWidth + 'px;">';
    alertBoxHtml += '<div class=\"nt-dialog-base-top\" style="' + nt.config.dialogHeaderBg + ';">';
    alertBoxHtml += '<span class=\"nt-dialog-base-title\">警告</span><a class=\"nt-dialog-base-button nt-dialog-base-close-button\" href="javascript:;" onclick=\"nt.fn.alertClose();\"></a></div>';
    alertBoxHtml += '<div class=\"nt-dialog-base-content\" style="' + nt.config.dialogBodyBg + ';min-height:' + nt.config.dialogBodyMinHeight + 'px;"><span class=\"nt-dialog-base-inner\">' + msg + '</span>';
    alertBoxHtml += '</div>';
    alertBoxHtml += '<div class=\"nt-dialog-base-bottom\" style="' + nt.config.dialogFooterBg + ';">';
    alertBoxHtml += '<a class=\"nt-dialog-base-button nt-dialog-base-cancel-button\" href="javascript:;" onclick="nt.fn.alertOK();">确定</a>';
    alertBoxHtml += '</div>';
    alertBoxHtml += '</div>';
    nt.showMask();
    $(document.body).append(alertBoxHtml);
    $('#alertBox').center();
    (typeof (fn) != 'function') || (nt.fn.alertOK = function () { fn(); nt.fn.alertClose(); });
}

/*
msg消息
title标题
fn回调函数
*/
window.confirm = function (msg, fn0, fn1) {
    var confirmBoxHtml = '';
    confirmBoxHtml += '<div id="confirmBox" class=\"nt-dialog-base\" style="width:' + nt.config.dialogWidth + 'px;">';
    confirmBoxHtml += '<div class=\"nt-dialog-base-top\" style="' + nt.config.dialogHeaderBg + ';">';
    confirmBoxHtml += '<span class=\"nt-dialog-base-title\">确认</span>';
    confirmBoxHtml += '<a class=\"nt-dialog-base-button nt-dialog-base-close-button\" href="javascript:;" onclick=\"nt.fn.confirmClose();\"></a>';
    confirmBoxHtml += '</div>';
    confirmBoxHtml += '<div class=\"nt-dialog-base-content\" style="' + nt.config.dialogBodyBg + ';min-height:' + nt.config.dialogBodyMinHeight + 'px;"><span class=\"nt-dialog-base-inner\">' + msg + '</span></div>';
    confirmBoxHtml += '<div class=\"nt-dialog-base-bottom\" style="' + nt.config.dialogFooterBg + ';">';
    confirmBoxHtml += '<a class=\"nt-dialog-base-button nt-dialog-base-ok-button\" href="javascript:;" onclick="nt.fn.confirmOK();">确定</a>';
    confirmBoxHtml += '<a class=\"nt-dialog-base-button nt-dialog-base-cancel-button\" href="javascript:;" onclick="nt.fn.confirmCancel();">取消</a>';
    confirmBoxHtml += '</div>';
    confirmBoxHtml += '</div>';
    $(document.body).append(confirmBoxHtml);
    nt.showMask();
    $('#confirmBox').center();
    (typeof (fn0) != 'function') || (nt.fn.confirmOK = function () { fn0(); nt.fn.confirmClose(); });
    (typeof (fn1) != 'function') || (nt.fn.confirmCancel = function () { fn1(); nt.fn.confirmClose(); });
}

/*
消息框的基本函数
*/
window.msgBoxBase = function (msg, delay, bgColor, color) {
    if (window.msgbox != null) {
        window.msgbox.remove();
        window.msgbox = null;
    }
    var html = '';
    var css = 'background-color: ' + bgColor + '; color: ' + color + ';';
    html += '<div id="msgBox" class=\"nt-message-box-base\" style="' + css + '">';
    html += msg;
    html += '</div>';
    $(document.body).append(html);
    window.msgbox = $('#msgBox');
    window.msgbox.animate({ top: 0 }, 800, 'easeInOutElastic', function () {
        setTimeout(function () {
            window.msgbox.fadeOut(500, null, function () {
                window.msgbox.remove();
                window.msgbox = null;
            });
        }, delay);
    });
}

/*错误消息*/
window.error = function (msg, delay) {
    var delay = delay || 5000;
    msgBoxBase(msg, delay, 'red', '#fff');
}

/*成功消息*/
window.success = function (msg, delay) {
    var delay = delay || 5000;
    msgBoxBase(msg, delay, 'green', '#fff');
}

/*通知消息*/
window.notice = function (msg, delay) {
    var delay = delay || 5000;
    msgBoxBase(msg, delay, 'yellow', '#fff');
}
/*
智能输入,控件获得焦点时,内容消失，移开时出现
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
获取RawUrl
PathAndQuery
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
根据id获取元素
*/
function G(objId) {
    var o = null;
    if (typeof objId === 'string')
        o = document.getElementById(objId);
    return o;
}

/*
登录
*/
function login(formId) {
    var form = G(formId);

    //validate
    var data = {};
    data.loginname = form['LoginName'].value;
    data.password = form['Password'].value;
    data.checkcode = form['CheckCode'].value;

    nt.config.sendQueryString = true;

    nt.ajax({
        action: 'Login',
        data: data,
        success: function (json) {
            if (json.error) { error(json.message); }
            else {
                //login successfully
                alert(json.message, function () {
                    window.location.href = unescape(json.redirectUrl);
                });
            }
        }
    });

    nt.config.sendQueryString = false;

    return false;
}

/*
退出登录
*/
function logout() {
    nt.ajax({
        action: 'logout',
        success: function (j) {
            if (j.error) error(j.message);
            else
                nt.reload();
        }
    });
}

/*
会员注册
*/
function register(formId) {
    var form = G(formId);
    //validate
    var data = {};
    data.loginname = form['LoginName'].value;
    data.password = form['Password'].value;
    data['Password.Again'] = form['Password.Again'].value;
    data.email = form['Email'].value;
    data.checkcode = form['CheckCode'].value;
    nt.ajax({
        action: 'register',
        data: data,
        success: function (json) {
            if (json.error) { error(json.message); }
            else {
                window.location.href = '/index.aspx';
            }
        }
    });

    return false;
}

/*
页面跳转
*/
function go(url) {
    window.location.href = url;
}

/*判断日期格式
@s  控件
@type  验证类型
*/
function validateField(s, type) {
    var old = s.getAttribute('data-old-value');
    var currentValue = s.value;
    var errorMsg = '';
    var error = false;

    switch (type) {
        case 'date':
            var fmt = 'yyyy-MM-dd';
            errorMsg = '日期格式错误!必须为yyyy-MM-dd';
            error = (currentValue.length != fmt.length) || !isDate(date, fmt);
            break;
        case 'email':
            errorMsg = '邮箱格式错误!格式必须像:abc@163.com';
            error = !isEmail(currentValue);
            break;
        case 'phone':
            errorMsg = '固话号码格式错误!格式必须像:(0411-82527872)';
            error = !isPhone(currentValue);
            break;
        case 'mobile':
            errorMsg = '手机号码格式错误!格式必须像:18742538743';
            error = !isMobile(currentValue);
            break;
        case 'zip':
            errorMsg = '邮政编码格式错误!格式必须像:116622';
            error = !/^\d{6}$/.test(currentValue);
            break;
        case 'text':
            errorMsg = '此字段内容不能为空!';
            error = /^[ ]*$/.test(currentValue);
            break;
        default:
            return false;
    }

    if (error) {
        s.value = old;
        var target = $(s);
        if (s.nextElementSibling === undefined || s.nextElementSibling == null) {
            $('<span class="nt-validation-message"/>').insertAfter(s);
        }
        target.next().text(errorMsg).show(500, '', function () {
            setTimeout(function () {
                target.next().hide('slow');
            }, 4000);
        });
        return true;
    } else {
        s.setAttribute('data-old-value', currentValue);
        return false;
    }
}

/*validation func begin*/

/*
用途：判断是否是日期
输入：date：日期；fmt：日期格式
返回：如果通过验证返回true,否则返回false
*/
function isDate(date, fmt) {
    if (fmt == null || fmt == undefined) fmt = "yyyyMMdd";
    var yIndex = fmt.indexOf("yyyy");
    if (yIndex == -1) return false;
    var year = date.substring(yIndex, yIndex + 4);
    var mIndex = fmt.indexOf("MM");
    if (mIndex == -1) return false;
    var month = date.substring(mIndex, mIndex + 2);
    var dIndex = fmt.indexOf("dd");
    if (dIndex == -1) return false;
    var day = date.substring(dIndex, dIndex + 2);
    if (!isNumber(year) || year > "2100" || year < "1900") return false;
    if (!isNumber(month) || month > "12" || month < "01") return false;
    if (day > getMaxDay(year, month) || day < "01") return false;
    return true;
}

/*
获取当月的天数
@year 年份
@month 月份
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
用途：检查输入字符串是否符合正整数格式
输入：
s：字符串
返回：
如果通过验证返回true,否则返回false
*/
function isNumber(s) {
    var regu = "^[0-9]+$";
    var re = new RegExp(regu);
    if (s.search(re) != -1) {
        return true;
    } else {
        return false;
    }
}

/*
用途：检查输入的Email信箱格式是否正确
输入：
strEmail：字符串
返回：
如果通过验证返回true,否则返回false
*/
function isEmail(strEmail) {
    var emailReg = /^(\w)+(\.\w+)*@(\w)+((\.\w{2,3}){1,3})$/;
    if (emailReg.test(strEmail)) {
        return true;
    } else {
        return false;
    }
}

/*
用途：检查输入的电话号码格式是否正确
输入：
strPhone：字符串
返回：
如果通过验证返回true,否则返回false
*/
function isPhone(strPhone) {
    var phoneRegWithArea = /^[0][1-9]{2,3}-?[0-9]{5,10}$/;
    var phoneRegNoArea = /^[1-9]{1}[0-9]{5,8}$/;
    if (strPhone.length > 9) {
        if (phoneRegWithArea.test(strPhone)) {
            return true;
        } else {
            return false;
        }
    } else {
        if (phoneRegNoArea.test(strPhone)) {
            return true;
        } else {
            return false;
        }
    }
}

/*
用途：校验ip地址的格式
输入：strIP：ip地址
返回：如果通过验证返回true,否则返回false； 
*/
function isIP(strIP) {
    if (isNull(strIP)) return false;
    var re = /^(\d+)\.(\d+)\.(\d+)\.(\d+)$/g //匹配IP地址的正则表达式
    if (re.test(strIP)) {
        if (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256 && RegExp.$4 < 256) return true;
    }
    return false;
}

/*
用途：检查输入字符串是否为空或者全部都是空格
输入：str
返回：
如果全是空返回true,否则返回false
*/
function isNull(str) {
    if (str == "") return true;
    var regu = "^[ ]+$";
    var re = new RegExp(regu);
    return re.test(str);
}

/*
用途：检查输入对象的值是否符合整数格式
输入：str 输入的字符串
返回：如果通过验证返回true,否则返回false
*/
function isInteger(str) {
    var regu = /^[-]{0,1}[0-9]{1,}$/;
    return regu.test(str);
}

/*
用途：检查输入手机号码是否正确
输入：
s：字符串
返回：
如果通过验证返回true,否则返回false
*/
function isMobile(s) {
    var regu = /^[1][0-9]{10}$/;
    var re = new RegExp(regu);
    if (re.test(s)) {
        return true;
    } else {
        return false;
    }
}

/*
用途：检查输入字符串是否是带小数的数字格式,可以是负数
输入：
s：字符串
返回：
如果通过验证返回true,否则返回false
*/
function isDecimal(str) {
    if (isInteger(str)) return true;
    var re = /^[-]{0,1}(\d+)[\.]+(\d+)$/;
    if (re.test(str)) {
        if (RegExp.$1 == 0 && RegExp.$2 == 0) return false;
        return true;
    } else {
        return false;
    }
}

/*
用途：检查输入对象的值是否符合端口号格式
输入：str 输入的字符串
返回：如果通过验证返回true,否则返回false
*/
function isPort(str) {
    return (isNumber(str) && str < 65536);
}

/*
用途：检查输入字符串是否符合金额格式
格式定义为带小数的正数，小数点后最多三位
输入：
s：字符串
返回：
如果通过验证返回true,否则返回false
*/
function isMoney(s) {
    var regu = "^[0-9]+[\.][0-9]{0,3}$";
    var re = new RegExp(regu);
    if (re.test(s)) {
        return true;
    } else {
        return false;
    }
}

/*validation func end*/

/*
goods detail
*/

/*
########请将以下部分代码粘贴至aspx页面的script标签中##############

window.AttriData=<%=r.AttributeInfoJSArray%>;

var CT_DROPDOWNLIST = <%= Goods_Attribute_Mapping.CT_DROPDOWNLIST%>,
    CT_RADIOBUTTONLIST = <%= Goods_Attribute_Mapping.CT_RADIOBUTTONLIST%>,
    CT_CHECKBOXES = <%= Goods_Attribute_Mapping.CT_CHECKBOXES%>,
    CT_TEXTBOX = <%= Goods_Attribute_Mapping.CT_TEXTBOX%>,
    CT_MUTILINETEXTBOX = <%= Goods_Attribute_Mapping.CT_MUTILINETEXTBOX%>,
    CT_DATEPICKER = <%= Goods_Attribute_Mapping.CT_DATEPICKER%>,
    CT_FILEUPLOAD = <%= Goods_Attribute_Mapping.CT_FILEUPLOAD%>,
    CT_COLORSQUARES = <%= Goods_Attribute_Mapping.CT_COLORSQUARES%>;

 * <ol>
 *      <li  valueId="2" adjustment="0.25"><label>something:</label></li>
 *      <li  valueId="1" adjustment="0.65"><label>something2:</label></li>
 *      ...
 *  </ol>
 */
function getAttributeXml(){
    var xml='<ol>';
    for(var i=0;i<window.AttriData.length;){
        var id=window.AttriData[i++];
        var t=window.AttriData[i++];
        switch(t){
            //this three selection can lead to price ajustment
            case CT_DROPDOWNLIST:
            case CT_COLORSQUARES:
            case CT_RADIOBUTTONLIST:
                var ctrl=document.getElementById('attriControl_'+id);
                if(ctrl!=null&&ctrl!=undefined)
                    xml+='<li valueId="'+ctrl.value+'" adjustment="'+ctrl.getAttribute('data-attr-adjustment')+'"><label>'+ctrl.getAttribute('data-attr-name')+':</label>'+ctrl.getAttribute('data-attr-label')+'</li>';
                break;
            case CT_CHECKBOXES:
                var ctrls=document.getElementsByName('attriControl_'+id);
                if(ctrls!=null&&ctrls!=undefined){
                    for(var c in ctrls){
                        var ctrl=ctrls[c];
                        if(ctrl.checked)
                            xml+='<li valueId="'+ctrl.value+'" adjustment="0.00"><label>'+ctrl.getAttribute('data-attr-name')+':</label>'+ctrl.getAttribute('data-attr-label')+'</li>';
                    }
                }
                break;
            default:
                break;
        }
    }
    xml+='</ol>';
    return xml;
}

/*
添加至购物车
*/
function addtocart(id){
    //需要选择购买数量
    if(G('Quqntity').value==0){
        error('抱歉:没有货物!请联系商家!');
        return;
    };
    var xml=getAttributeXml();
    data={};
    data.attrXml=xml;
    data.goodsid=id;
    data.qty=G('Quqntity').value;
    nt.ajax({
        action:'AddToCart',
        data:data,
        success:function(j){
            if(j.error)
                error(j.message);
            else{
                success('恭喜:加入购物车成功!');
                var tar=G('shoppingcartItemTotal');
                tar.innerHTML=j.total;
            }
        }
    });
}

/*评论*/
function postReview(gid){
    var data={};
    data.goodsid=gid;
    data.body=G('ReviewBody').value;
    nt.ajax({
        action:'PostReview',
        data:data,
        success:function(j){
            if(j.error)error(j.message);
            else
                success(j.message);
        }
    });
    return false;
}

/*点赞*/
function rating(sender,id){
    if(sender.niced){
        error('您已经点过赞');
        return;
    }
    nt.ajax({
        action:'ReviewRating',
        data:{id:id},
        success:function(j){
            if(j.error)
                error(j.message);
            else{
                sender.children[0].innerHTML=j.message;
                sender.niced=true;
            }
        }
    });
}

