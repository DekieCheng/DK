function loadBlock4() {
    const block4 = echarts.init(document.getElementById("block4"), "shine");
    const block4Opt = {
        legend: {
            show: true,
            top: "center",
            left: '70%',
            itemWidth: 30,
            itemHeight: 20,
            width: 50,
            padding: [0, 5],
            itemGap: 25,
            formatter: function (name) {
                return "{title|" + name + "}\n{value|" + (objData[name].value) + "}"
            },
            textStyle: {
                rich: {
                    title: {
                        fontSize: 10,
                        lineHeight: 10,
                        color: "rgba(255,255,255,.85)"
                    },
                    value: {
                        fontSize: 14,
                        lineHeight: 18,
                        color: "rgba(255,255,255,1)"
                    }
                }
            },
        },
        tooltip: {
            show: true,
            trigger: "item",
            formatter: "{a}<br>{b}:{c}"
        },
        color: ['#FF8700', '#ffc300', '#00e473', '#009DFF'],
        grid: {
            top: '20%',
            bottom: '48%',
            left: "30%",
            containLabel: false
        },
        yAxis: [{
            type: 'category',
            inverse: true,
            axisLine: {
                show: false
            },
            axisTick: {
                show: false
            },
            axisLabel: {
                interval: 0,
                inside: true,
                textStyle: {
                    color: "#fff",
                    fontSize: 12,
                },
                show: true
            }
        }],
        xAxis: [{
            show: false
        }]
    };
    $.ajax({
        url: ajaxBaseUrl + "data/house.json",
        dataType: "json"
    }).done(function () {
        $("#block4").addClass("chart-done");
    }).done(function (data) {
        arrName = getArrayValue(data, "name");
        arrValue = getArrayValue(data, "value");
        sumValue = eval(arrValue.join('+'));
        objData = array2obj(data, "name");
        optionData = getData(data);
        block4.setOption(block4Opt);
        block4.setOption({
            yAxis: {
                data: optionData.yAxis
            },
            series: optionData.series,
            legend: {
                data: arrName
            }
        });
    }).fail(function (jqXHR) {
        console.log("Ajax Fail: ", jqXHR.status, jqXHR.statusText);
    });

    function getArrayValue(array, key) {
        var key = key || "value";
        var res = [];
        if (array) {
            array.forEach(function (t) {
                res.push(t[key]);
            });
        }
        return res;
    }

    function array2obj(array, key) {
        var resObj = {};
        for (var i = 0; i < array.length; i++) {
            resObj[array[i][key]] = array[i];
        }
        return resObj;
    }

    function getData(data) {
        var res = {
            series: [],
            yAxis: []
        };
        for (let i = 0; i < data.length; i++) {
            res.series.push({
                name: '房屋管理',
                type: 'pie',
                clockWise: false, //顺时加载
                hoverAnimation: false, //鼠标移入变大
                radius: [65 - i * 15 + '%', 57 - i * 15 + '%'],
                center: ["30%", "55%"],
                label: {
                    show: false
                },
                itemStyle: {
                    label: {
                        show: false,
                    },
                    labelLine: {
                        show: false
                    },
                    borderWidth: 5,
                },
                data: [{
                    value: data[i].value,
                    name: data[i].name
                }, {
                    value: sumValue - data[i].value,
                    name: '',
                    itemStyle: {
                        color: "rgba(0,0,0,0)",
                        borderWidth: 0
                    },
                    tooltip: {
                        show: false
                    },
                    hoverAnimation: false
                }]
            });
            res.series.push({
                name: '',
                type: 'pie',
                silent: true,
                z: 1,
                clockWise: false, //顺时加载
                hoverAnimation: false, //鼠标移入变大
                radius: [65 - i * 15 + '%', 57 - i * 15 + '%'],
                center: ["30%", "55%"],
                label: {
                    show: false
                },
                itemStyle: {
                    label: {
                        show: false,
                    },
                    labelLine: {
                        show: false
                    },
                    borderWidth: 5,
                },
                data: [{
                    value: 7.5,
                    itemStyle: {
                        color: "#E3F0FF",
                        borderWidth: 0
                    },
                    tooltip: {
                        show: false
                    },
                    hoverAnimation: false
                }, {
                    value: 2.5,
                    name: '',
                    itemStyle: {
                        color: "rgba(0,0,0,0)",
                        borderWidth: 0
                    },
                    tooltip: {
                        show: false
                    },
                    hoverAnimation: false
                }]
            });
            res.yAxis.push(data[i].value);
        }
        return res;
    }
}