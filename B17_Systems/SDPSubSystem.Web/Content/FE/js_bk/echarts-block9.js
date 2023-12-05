function loadBlock9() {
    const block9 = echarts.init(document.getElementById("block9"), "macarons");

    let block9Opt = {
        color: ['#3398DB'],
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'line',
                lineStyle: {
                    opacity: 0
                }
            },
            formatter: function (params) {
                const marker = '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:10px;height:10px;background-color:#e6b600;"></span>';
                if (params[0].name !== '') {
                    return params[0].name + "<br />" + marker + "数量：" + params[0].value + " 人";
                } else {
                    return '';
                }
            }
        },
        legend: {
            show: false
        },
        grid: {
            left: '5%',
            right: '5%',
            bottom: '5%',
            top: '7%',
            height: '85%',
            containLabel: true,
            z: 22
        },
        xAxis: [{
            type: 'category',
            gridIndex: 0,
            data: [],
            axisTick: {
                alignWithLabel: true
            },
            axisLine: {
                lineStyle: {
                    color: '#0c3b71'
                }
            },
            axisLabel: {
                show: true,
                color: 'rgb(170,170,170)',
                fontSize: 12
            }
        }],
        yAxis: [{
                type: 'value',
                gridIndex: 0,
                splitLine: {
                    show: false
                },
                axisTick: {
                    show: false
                },
                axisLine: {
                    lineStyle: {
                        color: '#0c3b71'
                    }
                },
                axisLabel: {
                    color: 'rgb(170,170,170)',
                    formatter: '{value}'
                }
            },
            {
                type: 'value',
                gridIndex: 0,
                splitNumber: 12,
                splitLine: {
                    show: false
                },
                axisLine: {
                    show: false
                },
                axisTick: {
                    show: false
                },
                axisLabel: {
                    show: false
                },
                splitArea: {
                    show: true,
                    areaStyle: {
                        color: ['rgba(250,250,250,0.0)', 'rgba(250,250,250,0.05)']
                    }
                }
            }
        ],
        series: [{
            name: '实用人口',
            type: 'bar',
            barWidth: '30%',
            xAxisIndex: 0,
            yAxisIndex: 0,
            itemStyle: {
                normal: {
                    barBorderRadius: 30,
                    color: new echarts.graphic.LinearGradient(
                        0, 0, 0, 1, [{
                                offset: 0,
                                color: '#00feff'
                            },
                            {
                                offset: 0.5,
                                color: '#027eff'
                            },
                            {
                                offset: 1,
                                color: '#0286ff'
                            }
                        ]
                    )
                }
            },
            data: [],
            zlevel: 11
        }]
    };

    block9.setOption(block9Opt);

    $.ajax({
        url: ajaxBaseUrl + "data/practical-person.json",
        dataType: "json"
    }).done(function () {
        $("#block9").addClass("chart-done");
    }).done(function (data) {
        var yData = [];
        var seriesData = [];
        for (var i = 0; i < data.length; i++) {
            yData.push(data[i].name);
            seriesData.push(data[i].value);
        }
        block9.setOption({
            xAxis: [{
                data: yData
            }],
            series: [{
                data: seriesData
            }]
        });
    }).fail(function (jqXHR) {
        console.log("Ajax Fail: ", jqXHR.status, jqXHR.statusText);
    });
}