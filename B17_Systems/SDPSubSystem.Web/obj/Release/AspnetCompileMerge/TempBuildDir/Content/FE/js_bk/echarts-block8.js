function loadBlock8() {
    const block8 = echarts.init(document.getElementById("block8"), "shine");

    var colorArray = [{
            top: '#ffa800', //黄
            bottom: 'rgba(11,42,84,.3)'
        }, {
            top: '#1ace4a', //绿
            bottom: 'rgba(11,42,84, 0.3)'
        },
        {
            top: '#4bf3ff', //蓝
            bottom: 'rgba(11,42,84,.3)'
        }, {
            top: '#4f9aff', //深蓝
            bottom: 'rgba(11,42,84,.3)'
        },
        {
            top: '#b250ff', //粉
            bottom: 'rgba(11,42,84,.3)'
        }
    ];

    const block8Opt = {
        tooltip: {
            show: true,
            formatter: "{b}:{c}"
        },
        grid: {
            left: '6%',
            top: '0',
            right: '4%',
            bottom: '8%',
            containLabel: true
        },
        xAxis: {
            type: 'value',
            show: false,
            position: 'top',
            axisTick: {
                show: false
            },
            axisLine: {
                show: false,
                lineStyle: {
                    color: '#fff',
                }
            },
            splitLine: {
                show: false
            },
        },
        yAxis: [{
                type: 'category',
                axisTick: {
                    show: false,
                    alignWithLabel: false,
                    length: 5,

                },
                "splitLine": { //网格线
                    "show": false
                },
                inverse: 'true', //排序
                axisLine: {
                    show: false,
                    lineStyle: {
                        color: '#fff',
                    }
                },
                data: []
            }

        ],
        series: [{
                name: '重点单位场所',
                type: 'bar',
                label: {
                    normal: {
                        show: true,
                        position: 'right',
                        formatter: '{c}',
                        textStyle: {
                            color: 'white' //color of value
                        }
                    }
                },
                itemStyle: {
                    normal: {
                        show: true,
                        color: function (params) {
                            let num = colorArray.length;
                            return {
                                type: 'linear',
                                colorStops: [{
                                    offset: 0,
                                    color: colorArray[params.dataIndex % num].bottom
                                }, {
                                    offset: 1,
                                    color: colorArray[params.dataIndex % num].top
                                }, {
                                    offset: 0,
                                    color: colorArray[params.dataIndex % num].bottom
                                }, {
                                    offset: 1,
                                    color: colorArray[params.dataIndex % num].top
                                }, {
                                    offset: 0,
                                    color: colorArray[params.dataIndex % num].bottom
                                }, {
                                    offset: 1,
                                    color: colorArray[params.dataIndex % num].top
                                }, {
                                    offset: 0,
                                    color: colorArray[params.dataIndex % num].bottom
                                }, {
                                    offset: 1,
                                    color: colorArray[params.dataIndex % num].top
                                }, {
                                    offset: 0,
                                    color: colorArray[params.dataIndex % num].bottom
                                }, {
                                    offset: 1,
                                    color: colorArray[params.dataIndex % num].top
                                }, {
                                    offset: 0,
                                    color: colorArray[params.dataIndex % num].bottom
                                }, {
                                    offset: 1,
                                    color: colorArray[params.dataIndex % num].top
                                }],
                                //globalCoord: false
                            }
                        },
                        barBorderRadius: 70,
                        borderWidth: 0,
                        borderColor: '#333',
                    }
                },
                barGap: '0%',
                barCategoryGap: '50%',
                data: []
            }
        ]
    };
    block8.setOption(block8Opt);
    $.ajax({
        url: ajaxBaseUrl + "data/public-security.json",
        dataType: "json"
    }).done(function () {
        $("#block8").addClass("chart-done");
    }).done(function (data) {
        const yData = [];
        const seriesData = [];
        for (let i in data) {
            yData.push(data[i].name);
            seriesData.push(data[i].value);
        }
        block8.setOption({
            yAxis: {
                data: yData
            },
            series: [{
                data: seriesData
            }]
        });
    }).fail(function (jqXHR) {
        console.log("Ajax Fail: ", jqXHR.status, jqXHR.statusText);
    });
}