function loadBlock5() {
    const block5 = echarts.init(document.getElementById("block5"), "macarons");

    let color = ['#f845f1', '#ad46f3', '#5045f6', '#4777f5', '#44aff0', '#45dbf7', '#f6d54a', '#f69846', '#ff4343', '#f845f1'];
    $.ajax({
        url: ajaxBaseUrl + "data/event-distributed.json",
        dataType: "json"
    }).done(function () {
        $("#block5").addClass("chart-done");
    }).done(function (data) {
        var total = 0;
        for (let i = 0; i < data.length; i++) {
            total += data[i].value;
        }
        let block5Opt = {
            tooltip: {
                trigger: "item",
                formatter: function (params) {
                    var percent = 0;
                    percent = ((params.value / total) * 100).toFixed(0);
                    const marker = '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:10px;height:10px;background-color:#e6b600;"></span>';
                    if (params.name !== '') {
                        return params.name + "<br />" + marker + "数量：" + params.value + "</br>" + marker + "占比：" + percent + "%";
                    } else {
                        return '';
                    }
                }
            },
            calculable: true,
            series: [{
                type: "pie",
                radius: [8, 90],
                avoidLabelOverlap: false,
                startAngle: 0,
                center: ["50%", "50%"],
                roseType: "area",
                selectedMode: "single",
                label: {
                    normal: {
                        show: true,
                        formatter: "{b}"
                    },
                    emphasis: {
                        show: true
                    }
                },
                labelLine: {
                    normal: {
                        show: true,
                        smooth: false,
                        length: 5,
                        length2: 10
                    },
                    emphasis: {
                        "show": true
                    }
                },
                data: []
            }]
        };

        block5.setOption(block5Opt);

        var seriesData = [];
        for (var i = 0; i < data.length; i++) {
            seriesData.push({
                value: data[i].value,
                name: data[i].name,
                itemStyle: {
                    normal: {
                        color: color[i]
                    }
                }
            });
        }
        block5.setOption({
            series: [{
                data: seriesData
            }]
        });
    }).fail(function (jqXHR) {
        console.log("Ajax Fail: ", jqXHR.status, jqXHR.statusText);
    });
}