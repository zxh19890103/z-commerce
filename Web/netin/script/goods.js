/// <reference path="jquery-1.7.2.min.js" />
/// <reference path="jquery.form.js" />
/// <reference path="common.js" />
/// <reference path="common.list.js" />


var nt = nt || {};
nt.delSpecifiedRow = function (table, id, c) {
    confirm('您确定删除此项吗？', function () {
        nt.ajax({
            url: '../handlers/ajaxHandler.aspx',
            action: 'DeleteOnDB',
            data: { ids: id, table: table },
            success: function (json) {
                if (json.error) { error(json.message); }
                if (typeof c === 'function') c();
            }
        })
    }, null);
}


/*****************************Attribute Begin*********************************************/
/*添加参数*/
function attribute_add() {
    attributeEditForm.reset();
    attributeEditForm.Id.value = '0';
    nt.showMask();
    $('#edit_attribute').center().movable();
}

//保存参数
function attribute_save() {
    if (!attributeEditForm.GoodsAttributeId
        || attributeEditForm.GoodsAttributeId.value === '') {
        tips('未选择属性');
        return;
    }
    if (!attributeEditForm.ControlType
        || attributeEditForm.ControlType.value === '') {
        tips('未选择属性展现形式');
        return;
    }

    $(attributeEditForm).ajaxSubmit2({
        action: 'SaveAttribute',
        success: function (json) {
            if (json.error) alert(json.message);
            else {
                refreshList('list_attribute', 'Ajax_RenderAttribute', false);
            }
            $('#edit_attribute').hide();
            nt.removeMask();
        }
    });
}

//修改参数值
function attribute_edit(arrid) {
    nt.ajax({
        action: 'GetGoodsAttribute',
        data: { id: arrid },
        success: function (json) {
            $('#attributeEditForm').fillForm(json);
            nt.showMask();
            $('#edit_attribute').center().movable();
        }
    });
}

/*删除参数*/
function attribute_del(arrid) {
    nt.delSpecifiedRow(
        'Goods_Attribute_Mapping',
        arrid,
        function () {
            refreshList('list_attribute', 'Ajax_RenderAttribute', false);
        });
}

/*取消*/
function attribute_cancel() {
    edit_attribute.style.display = 'none';
    nt.removeMask();
}

/*属性值管理*/
function mgrAttributeValues(sender, attrid, type, goodsid, attrname) {
    var url = 'attributevalue.aspx?attrid=' + attrid + '&t=' + type + '&gid=' + goodsid + '&attrname=' + escape(attrname);
    openWindow({ url: url, name: 'attributevalue', width: 800, height: 600 });
}

/*****************************Attribute End*********************************************/


/*****************************Parameter Begin*********************************************/

/*添加参数*/
function param_add() {
    paramEditForm.reset();
    paramEditForm.Id.value = '0';
    nt.showMask();
    $('#edit_parameter').center().movable();
}

//保存参数
function param_save() {
    if (!paramEditForm.GoodsParameterGroupId
        || paramEditForm.GoodsParameterGroupId.value === '') {
        tips('没有参数组!');
        return;
    }
    if (paramEditForm.Name.value === '') {
        tips('参数名不能为空!');
        return;
    }

    $(paramEditForm).ajaxSubmit2({
        action: 'SaveParam',
        success: function (json) {
            if (json.error) alert(json.message);
            else {
                refreshList('list_parameter', 'Ajax_RenderParams', false);
            }
            $('#edit_parameter').hide();
            nt.removeMask();
        }
    });
}

//修改参数值
function param_edit(paramid) {
    nt.ajax({
        action: 'GetGoodsParameter',
        data: { id: paramid },
        success: function (json) {
            $('#paramEditForm').fillForm(json);
            nt.showMask();
            $('#edit_parameter').center().movable();
        }
    });
}

/*删除参数*/
function param_del(paramid) {
    nt.delSpecifiedRow(
        'Goods_Parameter',
        paramid,
        function () {
            refreshList('list_parameter', 'Ajax_RenderParams', false);
        });
}

/*取消*/
function param_cancel() {
    edit_parameter.style.display = 'none';
    nt.removeMask();
}

/*****************************Parameter End*********************************************/

/*****************************GoodsBinding Begin*********************************************/

/*
删除相关商品
*/
function delGoodsBinding(id) {
    nt.delSpecifiedRow(
        'Goods_Binding',
        id,
        function () {
            refreshList('goodsBinding', 'Ajax_RenderBindingGoods', false);
        });
}

/*
刷新相关商品列表
*/
function refreshGoodsBinding() {
    refreshList('goodsBinding', 'Ajax_RenderBindingGoods', false);
}

/*****************************GoodsBinding End*********************************************/

/*
上架或下架商品
*/
function putOnOrOffSale(ids) {
    var ids = ids || getSelectedIds().join();
    if (!ids) { error('没有任何选择项!'); return; }
    setBoolean(null, 'Removed', ids, nt.reload);
}
