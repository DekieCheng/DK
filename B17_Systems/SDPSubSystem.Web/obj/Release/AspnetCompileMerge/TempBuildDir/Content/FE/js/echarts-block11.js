function loadBlock11(data) {
    const block1 = echarts.init(document.getElementById("block11"), "shine");
    const block1Opt = {
        tooltip: {
            trigger: "axis",
            formatter: function (params) {
                const param = params[0];
                const marker = '<span style="display:inline-block;margin-right:5px;border-radius:10px;width:10px;height:10px;background-color:#e6b600;"></span>';
                return param.name + "<br />" +
                    marker + "用电量：" + param.value + " ";
            }
        },
        grid: {
            top: '20%',
            left: '10%',
            containLabel: true,
            width: "80%",
            height: "75%"
        },
        xAxis: [{
            type: 'category',
            boundaryGap: false,
            axisLine: { //坐标轴轴线相关设置。数学上的x轴
                show: true,
                lineStyle: {
                    color: '#233e64'
                },
            },
            axisLabel: { //坐标轴刻度标签的相关设置
                textStyle: {
                    color: '#6a9cd5',
                    margin: 15,
                },
                rotate: 45,
            },
            axisTick: {
                show: false,
            }
        }],
        yAxis: [{
            type: 'value',
            splitNumber: 2,
            name: 'Unit: kwh',
            nameTextStyle: {
                color: '#6a9cd5',
                fontSize: 12
            },
            splitLine: {
                show: true,
                lineStyle: {
                    color: '#233e64'
                }
            },
            axisLine: {
                show: false,
            },
            axisLabel: {
                margin: 5,
                textStyle: {
                    color: '#6a9cd5',

                },
            },
            axisTick: {
                show: false,
            }
        }],
        series: [{
            name: '用电量',
            type: 'line',
            smooth: true, //是否平滑曲线显示
            // 			symbol:'circle',  // 默认是空心圆（中间是白色的），改成实心圆
            symbolSize: 0,

            lineStyle: {
                normal: {
                    color: "#3deaff" // 线条颜色
                }
            },
            areaStyle: { //区域填充样式
                normal: {
                    //线性渐变，前4个参数分别是x0,y0,x2,y2(范围0~1);相当于图形包围盒中的百分比。如果最后一个参数是‘true’，则该四个值是绝对像素位置。
                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                        offset: 0,
                        color: 'rgba(61,234,255, 0.9)'
                    },
                    {
                        offset: 1,
                        color: 'rgba(61,234,255, 0)'
                    }
                    ], false),

                    shadowColor: 'rgba(53,142,215, 0.9)', //阴影颜色
                    shadowBlur: 20 //shadowBlur设图形阴影的模糊大小。配合shadowColor,shadowOffsetX/Y, 设置图形的阴影效果。
                }
            },
        }]
    };
    block1.setOption(block1Opt);
    //ajaxBaseUrl;
    //debugger;


    const xData = [];
    const yData = [];
    for (let i in data) {
        xData.push(data[i].CurrDay);
        yData.push(data[i].TPAEConsumption);
    }
    block1.setOption({
        xAxis: {
            data: xData
        },
        series: [{
            name: "用电量",
            data: yData
        }]
    });
}