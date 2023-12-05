function loadBlock3() {
    const block3 = echarts.init(document.getElementById("block3"), "shine");
    const block3Opt = {
        tooltip: {
            trigger: "axis",
            // axisPointer: {
            //     type: "cross"
            // },
            formatter: function (params) {
                const param = params[0];
                const marker = '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:10px;height:10px;background-color:#e6b600;"></span>';
                // const suffix = '<span style="margin-left:5px;font-size:12px;">亿元</span>';
                return param.name + "<br />" +
                    // marker + "排名：" + (param.dataIndex + 1) + "<br />" +
                    marker + "人员数量：" + param.value + " 人";
            }
        },
        grid: {
            width: '80%',
            height: '80%',
            left: '10%',
            top: '5%',
        },
        xAxis: {
            data: [],
            axisTick: {
                show: false
            },
            axisLine: {
                lineStyle: {
                    color: 'rgba(255, 129, 109, 0.1)',
                    width: 1 //这里是为了突出显示加上的
                }
            },
            axisLabel: {
                textStyle: {
                    color: '#999',
                    fontSize: 12
                }
            }
        },
        yAxis: [{
            show: false,
            splitNumber: 2,
            axisTick: {
                show: false
            },
            axisLine: {
                lineStyle: {
                    color: 'rgba(255, 129, 109, 0.1)',
                    width: 1 //这里是为了突出显示加上的
                }
            },
            axisLabel: {
                textStyle: {
                    color: '#999'
                }
            },
            splitArea: {
                areaStyle: {
                    color: 'rgba(255,255,255,.5)'
                }
            },
            splitLine: {
                show: true,
                lineStyle: {
                    color: 'rgba(255, 129, 109, 0.1)',
                    width: 0.5,
                    type: 'dashed'
                }
            }
        }],
        series: [{
            type: 'pictorialBar',
            barCategoryGap: '0%',
            symbol: 'path://M0,10 L10,10 C5.5,10 5.5,5 5,0 C4.5,5 4.5,10 0,10 z',
            label: {
                show: true,
                position: 'top',
                distance: 15,
                color: '#DB5E6A',
                fontWeight: 'bolder',
                fontSize: 16,
            },
            itemStyle: {
                normal: {
                    color: {
                        type: 'linear',
                        x: 0,
                        y: 0,
                        x2: 0,
                        y2: 1,
                        colorStops: [{
                                offset: 0,
                                color: 'rgba(232, 94, 106, .8)' //  0%  处的颜色
                            },
                            {
                                offset: 1,
                                color: 'rgba(232, 94, 106, .1)' //  100%  处的颜色
                            }
                        ],
                        global: false //  缺省为  false
                    }
                },
                emphasis: {
                    opacity: 1
                }
            },
            data: [],
            z: 10
        }]
    };
    block3.setOption(block3Opt);
    $.ajax({
        url: ajaxBaseUrl + "data/important-person.json",
        dataType: "json"
    }).done(function () {
        $("#block3").addClass("chart-done");
    }).done(function (data) {
        const xData = [];
        const yData = [];
        for (let i in data) {
            xData.push(data[i].name);
            yData.push(data[i].value);
        }
        block3.setOption({
            xAxis: {
                data: xData
            },
            series: [{
                name: "人员数量",
                data: yData
            }]
        });
    }).fail(function (jqXHR) {
        console.log("Ajax Fail: ", jqXHR.status, jqXHR.statusText);
    });
}