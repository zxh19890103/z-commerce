/// <reference path="jquery-1.7.2.min.js" />
/// <reference path="common.js" />

//翻页
function goto(page) {
    if (page == 0)
        return;
    var thisForm = document.forms.namedItem("PagerForm");
    thisForm.page.value = page;
    thisForm.submit();
}

/*清除搜索
searchForm必须存在
*/
function clearSeach() {
    var path = window.location.pathname;
    if (searchForm === undefined)
        error('searchForm Not Found!');
    var p = searchForm.page.value;
    window.location.href = path + '?page=' + p;
}

/*
获取选中的项
返回数组
*/
function getSelectedIds() {
    var ids = [];
    $("input.key-item").each(function (i, n) {
        if ($(n).attr("checked")) {
            ids.push($(n).val());
        }
    })
    return ids;
}

//全选
function selectAll(sender) {
    var b = $(sender).attr('checked');
    if (b)
        $("input.key-item").attr("checked", true);
    else
        $("input.key-item").attr("checked", false);
}

/*
设置bit型字段值的ajax方法
@sender:事件发送者[必选]
@field:字段名[必选]
@id:id[必选]
@c:回调函数[可选]function(true|false)
*/
function setBoolean(sender, field, id, c) {
    var data = {};
    data.ids = id;
    data.field = field;
    nt.ajax({
        data: data,
        action: 'setBoolean',
        success: function (json) {
            if (json.error) {
                error(json.message);
            }
            else {
                var y = true;
                if (sender) {
                    var s = $(sender);
                    var y = s.hasClass('boolean-yes');
                    if (y) s.removeClass('boolean-yes')
                    else s.addClass('boolean-yes');
                }
                if (typeof c === 'function') c(y);
            }
        }
    });
}

/*
删除行
@id：形如1,2,3的数字组[必选]
@tree：是否是树形表[可选]
@c:回调函数[可选]
*/
function delRow(id, tree, detetable, c) {
    confirm('您确定删除该项吗？',
        function () {
            var data = {};
            data.ids = id;
            var callback = nt.reload;
            if (typeof c === 'function')
                callback = c;
            if (typeof tree === 'boolean')
                data.tree = tree;
            if (typeof detetable === 'boolean')
                data.detetable = detetable;
            nt.ajax({
                data: data,
                action: 'DeleteOnDB',
                success: function (json) {
                    if (json.error) {
                        alert(json.message);
                    } else {
                        callback();
                    }
                }
            });
        }, null)
}

//重新排序
function reOrder(field, c) {
    var orders = [];
    var ids = [];
    $('input.order-item').each(function (i, n) {
        var J = $(n);
        if (J.data('data-changed')) {
            orders.push(J.val());
            ids.push(J.attr('data-item-id'));
            J.data('data-changed', false);
        }
    });
    if (ids.length == 0) {
        error('没有修改!');
        return;
    }
    var data = {};
    data.field = field || 'displayorder';
    data.ids = ids.join();
    data.orders = orders.join();
    nt.ajax({
        data: data,
        action: 'reorder',
        success: function (json) {
            if (json.error) { error(json.message); }
            else {
                if (typeof c === 'function') {
                    c();
                } else {
                    nt.reload();
                }
            }
        }
    })
}

//删除所选
function delSelected(detetable, c) {
    var ids = getSelectedIds();
    if (ids.length == 0) {
        alert('没有任何选项!');
        return;
    }

    confirm('您确定删除所选的项吗？', function () {
        var data = {};
        if (typeof detetable === 'boolean')
            data.detetable = detetable;
        data.ids = ids.join();
        nt.ajax({
            data: data,
            action: 'DeleteOnDB',
            success: function (json) {
                if (json.error) {
                    alert(json.message);
                }
                else {
                    if (typeof c === 'function') {
                        c();
                    } else {
                        nt.reload();
                    }
                }
            }
        });
    }, null)
}

/*
类别迁移
type:
1:文章 
2:产品
3:商品
*/
function treeMigrate(sender, current, type) {
    nt.ajax({
        url: '/netin/handlers/ajaxHandler.aspx',
        action: 'OutSelections',
        data: { from: current, type: type },
        success: function (json) {
            nt.openSelectionWindow(
                    '类别迁移',
                    json.ul, current,
                    function (v, t) {
                        nt.ajax({
                            url: '/netin/handlers/ajaxHandler.aspx',
                            action: 'TreeMigrate',
                            data: { from: current, to: v, type: type },
                            success: function (json) {
                                if (json.error) error(json.message);
                                else {
                                    nt.reload();
                                }
                            }
                        });
                    });
        }
    });
}

/*
将新闻或产品批量转移至指定分类
type:
1:文章 
2:产品
3:商品
*/
function batchMigrate(type) {
    var ids = getSelectedIds();
    var to = "";
    if ($("select[name='ToCategoryId']").length > 0)
        to = $("select[name='ToCategoryId']").val();
    else {
        alert("转移失败");
        return;
    }
    if (ids.length == 0) {
        alert("没有选中任何项");
        return;
    }
    nt.ajax({
        url: '/netin/handlers/ajaxHandler.aspx',
        action: 'BatchMigrate',
        data: { ids: ids.join(','), to: to, type: type },
        success: function (json) {
            alert(json.message, function () {
                nt.reload();
            });
        }
    })
}

/*
复制商品、产品、文章
id:id
type:0表示商品、1表示产品、2表示文章
c:回调函数
*/
function copy(id, type) {
    nt.ajax({
        url: '/netin/handlers/ajaxhandler.aspx',
        action: 'copy',
        data: { id: id, type: type },
        success: function (json) {
            if (json.error) {
                error(json.message);
            }
            else {
                var n = "";
                switch (type) {
                    case 0: n = '商品'; break;
                    case 1: n = '产品'; break;
                    case 2: n = '文章'; break;
                    default: n = '未知'; break;
                }
                success('复制' + n + '成功!', undefined, '<a href=\"javascript:;\" onclick=\"nt.reload();\">刷新</a>');
            }
        }
    })
}

/*恢复*/
function recovery(ids) {
    var ids = ids || getSelectedIds().join();
    if (!ids) { error('没有任何选择项!'); return; }
    setBoolean(null, 'Deleted', ids, nt.reload);
}

/*列表选择*/
function setKeyStatus(sender) {
    alert(sender);
    $('input.key-item', sender).click();
}