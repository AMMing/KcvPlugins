Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

var Admiral = function (data) {
    var obj = this;
    obj.admiral_list = [];
    obj.source_data = data;

    obj.getAllAdmiral = function () {
        var adminral_exist = function (number) {
            for (var i = 0; i < obj.admiral_list.length; i++) {
                var item = obj.admiral_list[i];
                if (item.MemberId == number)
                    return true;
            }

            return false;
        };

        var add_admiral = function (data) {
            if (!adminral_exist(data.MemberId)) {
                obj.admiral_list.push({
                    MemberId: data.MemberId,
                    Nickname: data.Nickname
                });
            }
        };
        $.each(obj.source_data.List, function () {
            add_admiral(this);
        });
    };

    obj.getListByAdmiral = function (number) {
        return $.map(obj.source_data.List, function (item, index) {
            if (number == 0 || item.MemberId == number) {//number等于0返回全部
                return item;
            }
        });
    };

    obj.init = function () {
        obj.getAllAdmiral();
    };
};

var ToChart = function () {
    var obj = this;
    obj.plot1 = null;
    obj.plot2 = null;

    obj.addItem = function (list, key, val) {
        var date = new Date(key);
        var newkey = date.Format("yyyy-MM-dd hh:mm");;
        list.push([newkey, val]);
    };

    obj.show = function (list) {
        var Fuel = [];//燃料
        var Ammunition = [];//弹药
        var Steel = [];//钢
        var Bauxite = [];//铝
        var DevelopmentMaterials = [];//开发资材
        var InstantRepairMaterials = [];//高速修复
        var InstantBuildMaterials = [];//高速建造

        for (var i = 0; i < list.length; i++) {
            //if (i > 10)
            //    break;
            var item = list[i];
            obj.addItem(Fuel, item.CreateDate, item.Fuel);
            obj.addItem(Ammunition, item.CreateDate, item.Ammunition);
            obj.addItem(Steel, item.CreateDate, item.Steel);
            obj.addItem(Bauxite, item.CreateDate, item.Bauxite);

            obj.addItem(DevelopmentMaterials, item.CreateDate, item.DevelopmentMaterials);
            obj.addItem(InstantRepairMaterials, item.CreateDate, item.InstantRepairMaterials);
            obj.addItem(InstantBuildMaterials, item.CreateDate, item.InstantBuildMaterials);
        }

        obj.plot1 = obj.jqplot('chart1',
            '资源记录',
            [Fuel, Ammunition, Steel, Bauxite, DevelopmentMaterials, InstantRepairMaterials, InstantBuildMaterials],
            ["#12AE4F", "#BA8F08", "#9B9B9B", "#EAF496", "#C98EE5", "#96EFB9", "#EF5E5E"],
            [
                {
                    label: '燃料'
                },
                {
                    label: '弹药'
                },
                {
                    label: '钢铁'
                },
                {
                    label: '铝'
                },
                {
                    linePattern: 'dashed',
                    label: '开发资材',
                    yaxis: 'y2axis'
                },
                {
                    linePattern: 'dashed',
                    label: '高速修复',
                    yaxis: 'y2axis'
                },
                {
                    linePattern: 'dashed',
                    label: '高速建造',
                    yaxis: 'y2axis'
                }
            ]);
    }

    obj.jqplot = function (select, title, data, seriesColors, series) {
        $.jqplot._noToImageButton = true;

        return $.jqplot(select, data, {
            resetAxes: true,
            seriesColors: seriesColors,
            title: title,
            highlighter: {
                show: true,
                tooltipLocation: 'n',
                tooltipAxes: 'xy',
                yvalues: 4,
                formatString: '<table class="jqplot-highlighter"> \
                              <tr><td>时间:</td><td>%s</td></tr> \
                              <tr><td>数值:</td><td>%s</td></tr> \</table>'
            },
            seriesDefaults: {
                rendererOptions: {
                    //smooth: true,//true为曲线
                    animation: {
                        show: true
                    }
                },
                showMarker: false,
            },
            series: series,
            axesDefaults: {
                rendererOptions: {
                    baselineWidth: 1.5,
                    drawBaseline: false
                },
                //pad: 0//y轴以0为起点
            },
            legend: {
                renderer: $.jqplot.EnhancedLegendRenderer,
                show: true
            },
            axes: {
                xaxis: {
                    renderer: $.jqplot.DateAxisRenderer,
                    tickRenderer: $.jqplot.CanvasAxisTickRenderer,
                    tickOptions: {
                        formatString: "%Y-%m-%d %H:%M",
                        angle: -30,
                        textColor: '#333'
                    },
                    label: '记录时间'
                },
                yaxis: {
                    tickOptions: {
                        textColor: '#333',
                        labelPosition: 'middle',
                        angle: -30
                    },
                    label: '实线刻度',
                    //drawMajorGridlines: false,
                },
                y2axis: {
                    rendererOptions: { forceTickAt0: true, forceTickAt100: true },
                    tickOptions: {
                        textColor: '#333'
                    },
                    label: '虚线刻度',
                    drawMajorGridlines: false,
                }
            },
            cursor: {
                show: true,
                zoom: true
            }
        });
    };
};

var admiral = null;
var toChart = new ToChart();
$(document).ready(function () {
    $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
    admiral = new Admiral(admiralinfo);
    admiral.init();

    var list = admiral.getListByAdmiral(admiral.admiral_list[0].MemberId);
    toChart.show(list);

    $('.btn').button();

    $('#btn_resetzoom').click(function () {
        toChart.plot1.resetZoom();
    });
    $("#datepicker_start,#datepicker_end").datepicker();

    $("#speed").selectmenu();
});