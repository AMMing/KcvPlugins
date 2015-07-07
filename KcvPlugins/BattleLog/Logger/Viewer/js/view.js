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

var AdmiralData = function (data) {
    var obj = this;
    obj.source_data = data;//数据源
    obj.admiral_list = [];//提督列表
    obj.start_date = null;//起始时间
    obj.end_date = null;//结束时间

    //提督信息是否已经记录
    obj.adminral_exist = function (number) {
        for (var i = 0; i < obj.admiral_list.length; i++) {
            var item = obj.admiral_list[i];
            if (item.MemberId == number)
                return true;
        }

        return false;
    };
    //添加提督信息
    obj.add_admiral = function (data) {
        if (!obj.adminral_exist(data.MemberId)) {
            obj.admiral_list.push({
                MemberId: data.MemberId,
                Nickname: data.Nickname
            });
        }
    };
    //更新起始结束时间
    obj.update_date = function (date) {
        var newdate = new Date(date);
        if (!obj.start_date || obj.start_date > newdate) {
            obj.start_date = newdate;
        }
        if (!obj.end_date || obj.end_date < newdate) {
            obj.end_date = newdate;
        }
    }

    //获取列表
    obj.getList = function (admiral, s_date, e_date) {
        return $.map(obj.source_data.List, function (item, index) {
            var item_date = new Date(item.CreateDate);
            var admiral_id = parseInt(admiral);
            if ((parseInt(item.MemberId) == admiral_id || !admiral_id) &&
                (s_date <= item_date || !s_date) &&
                (item_date <= e_date || !e_date)) {
                return item;
            }
        });
    };

    //初始化
    obj.init = function () {
        $.each(obj.source_data.List, function () {
            obj.add_admiral(this);
            obj.update_date(this.CreateDate);
        });
    };
};

var ToUI = function () {
    var obj = this;
    obj.plot = null;
    obj.ui = {};

    obj.addItem = function (list, key, val) {
        list.push([key, val]);
    };

    obj.showChart = function (list) {
        obj.ui.plot.empty();

        var Fuel = [];//燃料
        var Ammunition = [];//弹药
        var Steel = [];//钢
        var Bauxite = [];//铝
        var DevelopmentMaterials = [];//开发资材
        var InstantRepairMaterials = [];//高速修复
        var InstantBuildMaterials = [];//高速建造

        for (var i = 0; i < list.length; i++) {
            var item = list[i];
            obj.addItem(Fuel, item.CreateDate, item.Fuel);
            obj.addItem(Ammunition, item.CreateDate, item.Ammunition);
            obj.addItem(Steel, item.CreateDate, item.Steel);
            obj.addItem(Bauxite, item.CreateDate, item.Bauxite);

            obj.addItem(DevelopmentMaterials, item.CreateDate, item.DevelopmentMaterials);
            obj.addItem(InstantRepairMaterials, item.CreateDate, item.InstantRepairMaterials);
            obj.addItem(InstantBuildMaterials, item.CreateDate, item.InstantBuildMaterials);
        }

        obj.plot = $.jqplot('chart',
            [Fuel, Ammunition, Steel, Bauxite, DevelopmentMaterials, InstantRepairMaterials, InstantBuildMaterials],
            {
                resetAxes: true,
                seriesColors: ["#12AE4F", "#BA8F08", "#9B9B9B", "#EAF496", "#C98EE5", "#96EFB9", "#EF5E5E"],
                title: '资源记录',
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
                series: [
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
                ],
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
                        }
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
    }

    obj.write_select = function (list) {
        obj.ui.select.empty();
        obj.ui.select.append('<option id="0" selected="selected">全部</option>');
        $.each(list, function (index, item) {
            var option = $('<option></option>');
            option.attr('id', item.MemberId);
            option.text(item.Nickname);
            obj.ui.select.append(option);
        });
        obj.ui.select.selectmenu();
    }

    obj.reset_plot = function () {
        var w = obj.ui.win.width() - 200;
        var h = obj.ui.win.height() - 200;
        obj.ui.body.width(w);
        obj.ui.plot.height(h);
        if (!!obj.plot)
            obj.plot.replot({ resetAxes: true });
    }

    //初始化
    obj.init = function () {
        $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
        obj.ui.win = $(window);
        obj.ui.body = $('body');
        obj.ui.plot = $('#chart');
        obj.ui.select = $('#select_admiral_list');
        obj.ui.datepicker_start = $('#datepicker_start');
        obj.ui.datepicker_end = $('#datepicker_end');
        obj.ui.btn_sumbit = $('#btn_sumbit');
        obj.ui.btn_resetzoom = $('#btn_resetzoom');

        $('.btn').button();
        obj.ui.datepicker_start.datepicker();
        obj.ui.datepicker_end.datepicker();
    };
};


var Admiral = function () {
    var obj = this;
    obj.admiral_data = new AdmiralData(admiralinfo);
    obj.admiral_toui = new ToUI();

    obj.bind_event = function () {
        obj.admiral_toui.ui.btn_resetzoom.click(function () {
            obj.admiral_toui.plot.resetZoom();
        });
        obj.admiral_toui.ui.btn_sumbit.click(function () {
            var admiral_id = admiral.admiral_toui.ui.select.find('option:selected').attr('id');
            var s_date = new Date(admiral.admiral_toui.ui.datepicker_start.val() + ' 00:00');
            var e_date = new Date(admiral.admiral_toui.ui.datepicker_end.val() + ' 23:59');
            var list = obj.admiral_data.getList(admiral_id, s_date, e_date);
            obj.admiral_toui.showChart(list);
        });
        obj.admiral_toui.ui.win.resize(function () {
            obj.admiral_toui.reset_plot();
        })
    };
    obj.set_init_data = function () {
        obj.admiral_toui.write_select(obj.admiral_data.admiral_list);
        obj.admiral_toui.ui.datepicker_start.val(obj.admiral_data.start_date.Format('yyyy-MM-dd'));
        obj.admiral_toui.ui.datepicker_end.val(obj.admiral_data.end_date.Format('yyyy-MM-dd'));
    };

    obj.init = function () {
        obj.admiral_data.init();
        obj.admiral_toui.init();
        obj.set_init_data();
        obj.bind_event();

        obj.admiral_toui.ui.win.trigger('resize');
        obj.admiral_toui.ui.btn_sumbit.trigger('click');
    };
}

var admiral = new Admiral();
$(document).ready(function () {
    admiral.init();
});