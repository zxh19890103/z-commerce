var nt = nt || {};

/*
创建地图
默认为奈特有限公司的地图
o:
b String[dituContent]
x Number[121.656251]
y Number[38.928606]
t String[奈特原动力]
c html[中山区&nbsp;&nbsp;人民路&nbsp;锦联大厦1505]
*/
nt.map = function (o) {
    var me = this;
    me.o = {};
    $.extend(me.o, {
        b: 'dituContent',
        x: 121.655600,
        y: 38.928100,
        t: '奈特原动力',
        c: '中山区&nbsp;&nbsp;人民路&nbsp;锦联大厦1505'
    }, o);
    //创建和初始化地图函数：
    me.initMap = function () {
        me.createMap();//创建地图
        me.setMapEvent();//设置地图事件
        me.addMapControl();//向地图添加控件
        me.addMarker();//向地图中添加marker
    }

    //创建地图函数：
    me.createMap = function () {
        me.map = new BMap.Map(me.o.b);//在百度地图容器中创建一个地图
        var point = new BMap.Point(me.o.x, me.o.y);//定义一个中心点坐标
        me.map.centerAndZoom(point, 18);//设定地图的中心点和坐标并将地图显示在地图容器中
    }

    //地图事件设置函数：
    me.setMapEvent = function () {
        me.map.enableDragging();//启用地图拖拽事件，默认启用(可不写)
        me.map.enableScrollWheelZoom();//启用地图滚轮放大缩小
        me.map.enableDoubleClickZoom();//启用鼠标双击放大，默认启用(可不写)
        me.map.enableKeyboard();//启用键盘上下左右键移动地图
    }

    //地图控件添加函数：
    me.addMapControl = function () {
    }

    //标注点数组
    me.markerArr = [{ title: me.o.t, content: me.o.c, point: me.o.x + '|' + me.o.y, isOpen: 0, icon: { w: 21, h: 21, l: 0, t: 0, x: 6, lb: 5 } }
    ];
    //创建marker
    me.addMarker = function () {
        for (var i = 0; i < me.markerArr.length; i++) {
            var json = me.markerArr[i];
            var p0 = json.point.split("|")[0];
            var p1 = json.point.split("|")[1];
            var point = new BMap.Point(p0, p1);
            var iconImg = me.createIcon(json.icon);
            var marker = new BMap.Marker(point, { icon: iconImg });
            var iw = me.createInfoWindow(i);
            var label = new BMap.Label(json.title, { "offset": new BMap.Size(json.icon.lb - json.icon.x + 10, -20) });
            marker.setLabel(label);
            me.map.addOverlay(marker);
            label.setStyle({
                borderColor: "#808080",
                color: "#333",
                cursor: "pointer"
            });

            (function () {
                var index = i;
                var _iw = me.createInfoWindow(i);
                var _marker = marker;
                _marker.addEventListener("click", function () {
                    this.openInfoWindow(_iw);
                });
                _iw.addEventListener("open", function () {
                    _marker.getLabel().hide();
                })
                _iw.addEventListener("close", function () {
                    _marker.getLabel().show();
                })
                label.addEventListener("click", function () {
                    _marker.openInfoWindow(_iw);
                })
                if (!!json.isOpen) {
                    label.hide();
                    _marker.openInfoWindow(_iw);
                }
            })()
        }
    }
    //创建InfoWindow
    me.createInfoWindow = function (i) {
        var json = me.markerArr[i];
        var iw = new BMap.InfoWindow("<b class='iw_poi_title' title='" + json.title + "'>" + json.title + "</b><div class='iw_poi_content'>" + json.content + "</div>");
        return iw;
    }
    //创建一个Icon
    me.createIcon = function (json) {
        var icon = new BMap.Icon("/netin/favicon.gif", new BMap.Size(json.w, json.h), { imageOffset: new BMap.Size(-json.l, -json.t), infoWindowOffset: new BMap.Size(json.lb + 5, 1), offset: new BMap.Size(json.x, json.h) })
        return icon;
    }

    me.initMap();//创建和初始化地图
}