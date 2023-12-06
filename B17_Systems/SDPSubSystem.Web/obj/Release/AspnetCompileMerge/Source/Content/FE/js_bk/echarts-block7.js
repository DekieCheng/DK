function loadBlock7() {
    const block7 = echarts.init(document.getElementById("block7"), "macarons");

    $.ajax({
        url: ajaxBaseUrl + "data/event.json",
        dataType: "json"
    }).done(function () {
        $("#block7").addClass("chart-done");
    }).done(function (data) {
        let total = data.total;
        var seriesData = [];
        for (var i = 0; i < data.num.length; i++) {
            seriesData.push({
                value: data.num[i].value,
                name: data.num[i].name,
                itemStyle: {
                    normal: {
                        borderWidth: 5,
                        shadowBlur: 30,
                        borderColor: new echarts.graphic.LinearGradient(0, 0, 1, 1, [{
                            offset: 0,
                            color: '#7777eb'
                        }, {
                            offset: 1,
                            color: '#70ffac'
                        }]),
                        shadowColor: 'rgba(142, 152, 241, 0.6)'
                    }
                }
            }, {
                value: 4,
                name: '',
                itemStyle: {
                    normal: {
                        label: {
                            show: false
                        },
                        labelLine: {
                            show: false
                        },
                        color: 'rgba(0, 0, 0, 0)',
                        borderColor: 'rgba(0, 0, 0, 0)',
                        borderWidth: 0
                    }
                }
            });
        }
        var seriesObj = [{
            name: '',
            type: 'pie',
            clockWise: false,
            radius: [70, 73],
            hoverAnimation: false,
            itemStyle: {
                normal: {
                    label: {
                        show: true,
                        position: 'outside',
                        color: '#ddd',
                        formatter: '{b}',
                        rich: {
                            white: {
                                color: '#ddd',
                                align: 'center',
                                padding: [0, 0]
                            }
                        }
                    },
                    labelLine: {
                        show: true,
                        linstyle: {
                            color: '#fff'
                        }
                    }
                }
            },
            data: seriesData
        }];
        block7Opt = {
            tooltip: {
                trigger: "item",
                formatter: function (params) {
                    var percent = 0;
                    percent = ((params.value / total) * 100).toFixed(0);
                    const marker = '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:10px;height:10px;background-color:#e6b600;"></span>';
                    if (params.name !== '') {
                        return params.name + "<br />" + marker + "任务：" + params.value + " 个<br />" + marker + "占比：" + percent + "%";
                    } else {
                        return '';
                    }
                }
            },
            title: {
                text: '任务总数',
                subtext: total,
                textStyle: {
                    color: '#f2f2f2',
                    fontSize: 24,
                    align: 'center'
                },
                subtextStyle: {
                    fontSize: 20,
                    color: ['#ff9d19']
                },
                x: 'center',
                y: 'center',
            },
            legend: {
                show: false
            },
            toolbox: {
                show: false
            },
            series: seriesObj
        }

        block7.setOption(block7Opt);

        rollNum("event-rate1", 0, data.rate['办理率']);
        rollNum("event-rate2", 0, data.rate['办结率']);

    }).fail(function (jqXHR) {
        console.log("Ajax Fail: ", jqXHR.status, jqXHR.statusText);
    });
}