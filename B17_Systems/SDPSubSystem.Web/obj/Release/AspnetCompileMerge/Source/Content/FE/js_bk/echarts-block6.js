function loadBlock6() {
    const block6 = echarts.init(document.getElementById("block6"), "macarons");
    let block6Opt = {
        tooltip: {
            trigger: "axis",
            axisPointer: {
                type: "shadow",
                textStyle: {
                    color: "#fff"
                }

            },
        },
        grid: {
            borderWidth: 0,
            top: 10,
            bottom: 70,
            textStyle: {
                color: "#fff"
            }
        },
        calculable: true,
        xAxis: [{
            type: "category",
            axisLine: {
                lineStyle: {
                    color: '#fff'
                }
            },
            splitLine: {
                show: false
            },
            axisTick: {
                show: false
            },
            splitArea: {
                show: false
            },
            axisLabel: {
                interval: 0,
                formatter: function (value) {
                    //x轴的文字改为竖版显示
                    var str = value.split("");
                    return str.join("\n");
                }
            },
            data: [],
        }],
        yAxis: [{
            type: "value",
            splitLine: {
                show: false
            },
            axisLine: {
                lineStyle: {
                    color: '#90979c'
                }
            },
            axisTick: {
                show: false
            },
            axisLabel: {
                interval: 0,
            },
            splitArea: {
                show: false
            },
        }],
        series: [{
                name: "已办结",
                type: "bar",
                stack: "总量",
                barMaxWidth: 35,
                barGap: "10%",
                itemStyle: {
                    normal: {
                        color: "rgba(255,144,128,1)",
                        // label: {
                        //     show: true,
                        //     textStyle: {
                        //         color: "#fff"
                        //     },
                        //     position: "inside",
                        //     formatter: function (p) {
                        //         return p.value > 0 ? (p.value) : '';
                        //     }
                        // }
                    }
                },
                data: [],
            }, {
                name: "办理中",
                type: "bar",
                stack: "总量",
                itemStyle: {
                    normal: {
                        color: "rgba(0,191,183,1)",
                        barBorderRadius: 0,
                        // label: {
                        //     show: true,
                        //     position: "inside",
                        //     formatter: function (p) {
                        //         return p.value > 0 ? (p.value) : '';
                        //     }
                        // }
                    }
                },
                data: []
            }, {
                name: "未办理",
                type: "bar",
                stack: "总量",
                itemStyle: {
                    normal: {
                        color: "rgba(252, 230, 48, 1)",
                        barBorderRadius: 0,
                        // label: {
                        //     show: true,
                        //     position: "inside",
                        //     formatter: function (p) {
                        //         return p.value > 0 ? (p.value) : '';
                        //     }
                        // }
                    }
                },
                data: []
            }
        ]
    };

    block6.setOption(block6Opt);

    $.ajax({
        url: ajaxBaseUrl + "data/event-type.json",
        dataType: "json"
    }).done(function () {
        $("#block6").addClass("chart-done");
    }).done(function (data) {
        var dealtData = [], undealData = [], dealingData = [];
        var xData = [];
        for (var i = 0; i < data.length; i++) {
            dealtData.push(data[i].dealt);
            undealData.push(data[i].undeal);
            dealingData.push(data[i].dealing);
            xData.push(data[i].name);
        }
        block6.setOption({
            series: [{
                data: dealtData
            }, {
                data: undealData
            }, {
                data: dealingData
            }],
            xAxis: {
                data: xData
            }
        });
    }).fail(function (jqXHR) {
        console.log("Ajax Fail: ", jqXHR.status, jqXHR.statusText);
    });
}