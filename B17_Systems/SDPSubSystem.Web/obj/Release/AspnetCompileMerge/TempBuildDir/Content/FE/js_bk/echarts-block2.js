function loadBlock2() {
    var color = ['#00ffff', '#00cfff', '#006ced', '#ffe000', '#ffa800', '#ff5b00', '#ff3000']

    var seriesOption = [{
        name: '',
        type: 'pie',
        clockWise: false,
        radius: [72, 75],
        center: ['35%', 'center'],
        hoverAnimation: true,
        hoverOffset: 5,
        label: {
            show: false,
        }
    }];

    const block2 = echarts.init(document.getElementById("block2"), "shine");

    $.ajax({
        url: ajaxBaseUrl + "data/grid.json",
        dataType: "json"
    }).done(function () {
        $("#block2").addClass("chart-done");
    }).done(function (data) {
        var percent = 0;
        var total = 0;
        for (let i = 0; i < data.length; i++) {
            total += data[i].value;
        }
        const block2Opt = {
            tooltip: {
                trigger: "item",
                formatter: function (params) {
                    percent = ((params.value / total) * 100).toFixed(0);
                    const marker = '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:10px;height:10px;background-color:#e6b600;"></span>';
                    if (params.name !== '') {
                        return params.name + "<br />" + marker + "数量：" + params.value + " 支<br />" + marker + "占比：" + percent + "%";
                    } else {
                        return '';
                    }
                }
            },
            color: color,
            // graphic: {
            //     elements: [{
            //         type: "image",
            //         z: 3,
            //         style: {
            //             image: "./img/circle.png",
            //             width: 120,
            //             height: 120
            //         },
            //         left: '19%',
            //         top: '16.5%',
            //         position: [100, 100]
            //     }]
            // },
            legend: {
                show: true,
                icon: "roundRect",
                orient: 'vertical',
                // x: 'left',
                right: 20,
                top: 45,
                align: 'right',
                itemWidth: 20,
                textStyle: {
                    color: "#fff"
                },
                itemGap: 15
            },
            toolbox: {
                show: false
            },
            series: seriesOption
        };

        block2.setOption(block2Opt);

        var seriesData = [];
        for (var i = 0; i < data.length; i++) {
            seriesData.push({
                value: data[i].value,
                name: data[i].name,
                total: data[i].total,
                itemStyle: {
                    normal: {
                        borderWidth: 5,
                        shadowBlur: 20,
                        borderColor: color[i],
                        shadowColor: color[i]
                    }
                }
            }, {
                value: 3,
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
        const legendData = [];
        for (let i in data) {
            legendData.push(data[i].name);
        }
        block2.setOption({
            legend: {
                data: legendData
            },
            series: [{
                data: seriesData
            }]
        });

        var teamTable = '<tr>';
        for (let i = 0; i < data.length; i++) {
            teamTable += '<td><p>' + data[i].name + '</p><p><span id="team' + (i + 1) + '">0</span></p></td>';
        }
        teamTable += '</tr>';
        document.getElementById("team-box").innerHTML = teamTable;

        rollNum("team1", 0, data[0].value);
        rollNum("team2", 0, data[1].value);
        rollNum("team3", 0, data[2].value);
        rollNum("team4", 0, data[3].value);
        rollNum("team5", 0, data[4].value);

    }).fail(function (jqXHR) {
        console.log("Ajax Fail: ", jqXHR.status, jqXHR.statusText);
    });
}