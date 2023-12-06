<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ForB17Dashboard.adminDashboard" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>伟创力B17厂区能耗</title>
    <link rel="stylesheet" href="css/bootstrap_4.2.1.css">
    <link rel="stylesheet" href="css/base.css">
    <link rel="stylesheet" href="css/index.css">

    <style>
        .t_title{
            width: 100%;
            height: 100%;
            text-align: center;
            font-size: 2.5em;
            line-height: 80px;
            color: #fff;
        }
        #chart_map{
            cursor: pointer;
        }
        .t_show{
            position: absolute;
            top: 0;
            right: 0;
            border-radius: 2px;
            background: #2C58A6;
            padding: 2px 5px;
            color: #fff;
            cursor: pointer;
        }
    </style>
</head>
<body>

<!--header-->
<div class="header">
    <div class="bg_header">
        <div class="header_nav fl t_title">
            伟创力B17厂区能耗
        </div> 
    </div>
</div>

<!--main-->
<div class="data_content">
    <div id="showtime" class="data_time">
       <!-- 温馨提示: 点击模块后跳转至详情页面。     -->   
    </div>

    <div class="data_main">
        <div class="main_left fl">
            <div class="left_1 t_btn6" style="cursor: pointer;">
                <!--左上边框-->
                <div class="t_line_box">
                    <i class="t_l_line"></i> 
                    <i class="l_t_line"></i> 
                </div> 
                <!--右上边框-->
                <div class="t_line_box">
                    <i class="t_r_line"></i> 
                    <i class="r_t_line"></i> 
                </div> 
                <!--左下边框-->
                <div class="t_line_box">
                    <i class="l_b_line"></i> 
                    <i class="b_l_line"></i> 
                </div> 
                    <!--右下边框-->
                    <div class="t_line_box">
                    <i class="r_b_line"></i> 
                    <i class="b_r_line"></i> 
                </div> 
                <div class="main_title">
                    <img src="img/t_1.png" alt="">
                  电
                </div>
                <div id="chart_1" class="chart" style="width:100%;height: 280px;"></div>
            </div>
            <div class="left_2" style="cursor: pointer;">
                <!--左上边框-->
                <div class="t_line_box">
                    <i class="t_l_line"></i> 
                    <i class="l_t_line"></i> 
                </div> 
                <!--右上边框-->
                <div class="t_line_box">
                    <i class="t_r_line"></i> 
                    <i class="r_t_line"></i> 
                </div> 
                <!--左下边框-->
                <div class="t_line_box">
                    <i class="l_b_line"></i> 
                    <i class="b_l_line"></i> 
                </div> 
                    <!--右下边框-->
                    <div class="t_line_box">
                    <i class="r_b_line"></i> 
                    <i class="b_r_line"></i> 
                </div> 
                <div class="main_title">
                    <img src="img/t_2.png" alt="">
                    水
                </div>
                <div id="chart_2" class="chart t_btn9" style="width:100%;height: 280px;"></div>
            </div>
        </div>
        <div class="main_center fl">
            <div class="center_text">
                <!--左上边框-->
                <div class="t_line_box">
                    <i class="t_l_line"></i> 
                    <i class="l_t_line"></i> 
                </div> 
                <!--右上边框-->
                <div class="t_line_box">
                    <i class="t_r_line"></i> 
                    <i class="r_t_line"></i> 
                </div> 
                <!--左下边框-->
                <div class="t_line_box">
                    <i class="l_b_line"></i> 
                    <i class="b_l_line"></i> 
                </div> 
                 <!--右下边框-->
                 <div class="t_line_box">
                    <i class="r_b_line"></i> 
                    <i class="b_r_line"></i> 
                </div> 
                <div class="main_title">
                    <img src="img/t_3.png" alt="">
                    伟创力
                </div>
                <div id="chart_map" style="width:100%;height:610px;"></div>
            </div>
        </div>
        <div class="main_right fr">
            <div class="right_1">
                <!--左上边框-->
                <div class="t_line_box">
                    <i class="t_l_line"></i> 
                    <i class="l_t_line"></i> 
                </div> 
                <!--右上边框-->
                <div class="t_line_box">
                    <i class="t_r_line"></i> 
                    <i class="r_t_line"></i> 
                </div> 
                <!--左下边框-->
                <div class="t_line_box">
                    <i class="l_b_line"></i> 
                    <i class="b_l_line"></i> 
                </div> 
                    <!--右下边框-->
                    <div class="t_line_box">
                    <i class="r_b_line"></i> 
                    <i class="b_r_line"></i> 
                </div> 
                <div class="main_title">
                    <img src="img/t_4.png" alt="">
                    空气流量计
                </div>
                <div id="chart_3" class="echart t_btn7" style="width:100%;height: 280px;"></div>
            </div>
            <div class="right_2">
                <!--左上边框-->
                <div class="t_line_box">
                    <i class="t_l_line"></i> 
                    <i class="l_t_line"></i> 
                </div> 
                <!--右上边框-->
                <div class="t_line_box">
                    <i class="t_r_line"></i> 
                    <i class="r_t_line"></i> 
                </div> 
                <!--左下边框-->
                <div class="t_line_box">
                    <i class="l_b_line"></i> 
                    <i class="b_l_line"></i> 
                </div> 
                    <!--右下边框-->
                    <div class="t_line_box">
                    <i class="r_b_line"></i> 
                    <i class="b_r_line"></i> 
                </div> 
                <div class="main_title">
                    <img src="img/t_5.png" alt="">
                    压缩空气
                </div>
                <div id="chart_4" class="echart fl t_btn4" style="width:100%;height: 280px;cursor: pointer;"></div>
            </div>    
        </div>
    </div>
</div>
</body>
<script src="js/jquery-1.10.2.js"></script>
<script src="js/bootstrap.min.js"></script>
<script src="js/common.js"></script>
<script src="js/echarts-4.2.1.min.js"></script>
<script src="js/dataTool.js"></script>
<script src="js/index.js"></script>
<script src="js/china.js"></script>
<script src="js/hunan.js"></script>
</html>
<script>
    realSysTime(showtime);
    function realSysTime(showtime) {
        var now = new Date();            //创建Date对象
        var year = now.getFullYear();    //获取年份
        var month = now.getMonth();    //获取月份
        var date = now.getDate();        //获取日期
        var day = now.getDay();        //获取星期
        var hour = now.getHours();    //获取小时
        var minu = now.getMinutes();    //获取分钟
        var sec = now.getSeconds();    //获取秒钟
        month = month + 1;
        var arr_week = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
        var week = arr_week[day];  //获取中文的星期
        var time = year + "-" + month + "-" + date + "-" + " " + ((hour < 10) ? ('0' + hour) : hour) + ":" + ((minu < 10) ? ('0' + minu) : minu) + ":" + ((sec < 10) ? ('0' + sec) : sec);    //组合系统时间
        showtime.innerHTML = time;    //显示系统时间
    }
</script>

<script>
 

    var dom = document.getElementById("chart_1");
    var myChart = echarts.init(dom);

                var option = {
                    tooltip: {
                        trigger: 'axis'
                    },
                    legend: {
                        x: '35%',
                        y: '0%',
                        data: ["电"],
                        textStyle: {
                            color: "#fff",
                            fontSize: 8
                        },
                        itemWidth: 10,
                        itemHeight: 10,
                    },
                    calculable: true,
                    xAxis: [
                        {

                            type: 'category',
                            data: [<%=HTTimeLine["WaterDay"]%>],
                            axisLabel: {
                                interval: 0,
                                rotate: 40,
                                textStyle: {
                                    fontSize: 8,
                                    color: 'rgba(255,255,255,.7)',
                                }
                            },
                            "axisTick": {       //y轴刻度线
                                "show": false
                            },
                            "axisLine": {       //y轴
                                "show": false,
                            },
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            scale: true,
                            name: 'Unit：KW',
                            nameTextStyle: {
                                color: 'rgba(255,255,255,.7)',
                                fontSize: 8
                            },
                            //max: 100,
                            min: 0,
                            boundaryGap: [0.2, 0.2],
                            "axisTick": {       //y轴刻度线
                                "show": false
                            },
                            "axisLine": {       //y轴
                                "show": false,
                            },
                            axisLabel: {
                                textStyle: {
                                    color: 'rgba(255,255,255,.8)',
                                    fontSize: 8,
                                }
                            },
                            splitLine: {  //决定是否显示坐标中网格
                                show: true,
                                lineStyle: {
                                    color: ['#fff'],
                                    opacity: 0.2
                                }
                            },
                        },
                        {
                            type: 'value',
                            scale: true,
                            show: false,

                            nameTextStyle: {
                                color: 'rgba(255,255,255,.2)',
                            },
                            max: 1,
                            min: 0,
                            boundaryGap: [0.2, 0.2],
                            "axisTick": {       //y轴刻度线
                                "show": false
                            },
                            "axisLine": {       //y轴
                                "show": false,
                            },
                            axisLabel: {
                                textStyle: {
                                    color: 'rgba(255,255,255,.2)',
                                    // opacity: 0.1,
                                }
                            },
                            splitLine: {  //决定是否显示坐标中网格
                                show: true,
                                lineStyle: {
                                    color: ['#fff'],
                                    opacity: 0.2
                                }
                            },

                        }
                    ],
                    color: ['#0EFCFF', '#FD1133', '#f55', '#1F2EF'],
                    grid: {
                        left: '14%',
                        right: '10%',
                        top: '25%',
                        bottom: '15%'
                        // containLabel: true
                    },
                    series: [

                        {
                            name: '',//'每日用电量',
                            type: 'line',
                            smooth: true,
                            //stack: '总量',
                            //barWidth: '100',
                            //data: ['58','50','63','104','117','67','105','120','56','56','129'],
                            data: [<%=HTSource["Water"] %>],

                            label: {
                                color: 'rgba(255, 255, 255, 0.3)', fontFamily: 'sans-serif',
                                //字体大小
                                fontSize: 18
                            },
                            itemStyle: {
                                normal: {

                                    /*color: 'green',*/
                                    color: new echarts.graphic.LinearGradient(
                                        0, 0, 0, 1,
                                        [

                                            { offset: 1, color: 'rgba(100,188,1211,1)' }

                                        ]),
                                    shadowBlur: 15,

                                    shadowColor: 'rgba(0, 37, 155, 0.7)',
                                    label: {
                                        show: true, position: 'top',
                                        rotate: 0,
                                        formatter: '{c}',
                                        textStyle: {
                                            color: 'white',
                                            fontSize: 12
                                        }
                                    },

                                    labelLine: {
                                        show: false
                                    }
                                }
                            }
                        },
                    ],
                };
    if (option && typeof option === "object") {
        myChart.setOption(option, true);
    }
    setTimeout(function () {
        window.onresize = function () {
            myChart.resize();
            myChart2.resize();
        }
    }, 200)
</script>

<script>


    var dom2 = document.getElementById("chart_2");
    var myChart2 = echarts.init(dom2);

    var option2 = {
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            x: '35%',
            y: '0%',
            data: ["水"],
            textStyle: {
                color: "#fff",
                fontSize: 8
            },
            itemWidth: 10,
            itemHeight: 10,
        },
        calculable: true,
        xAxis: [
            {

                type: 'category',
                data: [<%=HTTimeLine["WaterDay"]%>],
                            axisLabel: {
                                interval: 0,
                                rotate: 40,
                                textStyle: {
                                    fontSize: 8,
                                    color: 'rgba(255,255,255,.7)',
                                }
                            },
                            "axisTick": {       //y轴刻度线
                                "show": false
                            },
                            "axisLine": {       //y轴
                                "show": false,
                            },
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            scale: true,
                            name: 'Unit：m³',
                            nameTextStyle: {
                                color: 'rgba(255,255,255,.7)',
                                fontSize: 8
                            },
                            //max: 100,
                            min: 0,
                            boundaryGap: [0.2, 0.2],
                            "axisTick": {       //y轴刻度线
                                "show": false
                            },
                            "axisLine": {       //y轴
                                "show": false,
                            },
                            axisLabel: {
                                textStyle: {
                                    color: 'rgba(255,255,255,.8)',
                                    fontSize: 8,
                                }
                            },
                            splitLine: {  //决定是否显示坐标中网格
                                show: true,
                                lineStyle: {
                                    color: ['#fff'],
                                    opacity: 0.2
                                }
                            },
                        },
                        {
                            type: 'value',
                            scale: true,
                            show: false,

                            nameTextStyle: {
                                color: 'rgba(255,255,255,.2)',
                            },
                            max: 1,
                            min: 0,
                            boundaryGap: [0.2, 0.2],
                            "axisTick": {       //y轴刻度线
                                "show": false
                            },
                            "axisLine": {       //y轴
                                "show": false,
                            },
                            axisLabel: {
                                textStyle: {
                                    color: 'rgba(255,255,255,.2)',
                                    // opacity: 0.1,
                                }
                            },
                            splitLine: {  //决定是否显示坐标中网格
                                show: true,
                                lineStyle: {
                                    color: ['#fff'],
                                    opacity: 0.2
                                }
                            },

                        }
                    ],
                    color: ['#0EFCFF', '#FD1133', '#f55', '#1F2EF'],
                    grid: {
                        left: '14%',
                        right: '10%',
                        top: '25%',
                        bottom: '15%'
                        // containLabel: true
                    },
                    series: [

                        {
                            name: '',//'每日用电量',
                            type: 'line',
                            smooth: true,
                            //stack: '总量',
                            //barWidth: '100',
                            //data: ['58','50','63','104','117','67','105','120','56','56','129'],
                            data: [<%=HTSource["Water"] %>],

                label: {
                    color: 'rgba(255, 255, 255, 0.3)', fontFamily: 'sans-serif',
                    //字体大小
                    fontSize: 18
                },
                itemStyle: {
                    normal: {

                        /*color: 'green',*/
                        color: new echarts.graphic.LinearGradient(
                            0, 0, 0, 1,
                            [

                                { offset: 1, color: 'rgba(100,188,1211,1)' }

                            ]),
                        shadowBlur: 15,

                        shadowColor: 'rgba(0, 37, 155, 0.7)',
                        label: {
                            show: true, position: 'top',
                            rotate: 0,
                            formatter: '{c}',
                            textStyle: {
                                color: 'white',
                                fontSize: 12
                            }
                        },

                        labelLine: {
                            show: false
                        }
                    }
                }
            },
        ],
    };
    if (option2 && typeof option2 === "object") {
        myChart2.setOption(option2, true);
    }
</script>

<script>


    var dom3 = document.getElementById("chart_3");
    var myChart3 = echarts.init(dom3);

    var option3 = {
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            x: '35%',
            y: '0%',
            data: ["空调流量计"],
            textStyle: {
                color: "#fff",
                fontSize: 8
            },
            itemWidth: 10,
            itemHeight: 10,
        },
        calculable: true,
        xAxis: [
            {

                type: 'category',
                data: [<%=HTTimeLine["KongtiaoDay"]%>],
                            axisLabel: {
                                interval: 0,
                                rotate: 40,
                                textStyle: {
                                    fontSize: 8,
                                    color: 'rgba(255,255,255,.7)',
                                }
                            },
                            "axisTick": {       //y轴刻度线
                                "show": false
                            },
                            "axisLine": {       //y轴
                                "show": false,
                            },
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            scale: true,
                            name: 'Unit：m³',
                            nameTextStyle: {
                                color: 'rgba(255,255,255,.7)',
                                fontSize: 8
                            },
                            //max: 100,
                            min: 0,
                            boundaryGap: [0.2, 0.2],
                            "axisTick": {       //y轴刻度线
                                "show": false
                            },
                            "axisLine": {       //y轴
                                "show": false,
                            },
                            axisLabel: {
                                textStyle: {
                                    color: 'rgba(255,255,255,.8)',
                                    fontSize: 8,
                                }
                            },
                            splitLine: {  //决定是否显示坐标中网格
                                show: true,
                                lineStyle: {
                                    color: ['#fff'],
                                    opacity: 0.2
                                }
                            },
                        },
                        {
                            type: 'value',
                            scale: true,
                            show: false,

                            nameTextStyle: {
                                color: 'rgba(255,255,255,.2)',
                            },
                            max: 1,
                            min: 0,
                            boundaryGap: [0.2, 0.2],
                            "axisTick": {       //y轴刻度线
                                "show": false
                            },
                            "axisLine": {       //y轴
                                "show": false,
                            },
                            axisLabel: {
                                textStyle: {
                                    color: 'rgba(255,255,255,.2)',
                                    // opacity: 0.1,
                                }
                            },
                            splitLine: {  //决定是否显示坐标中网格
                                show: true,
                                lineStyle: {
                                    color: ['#fff'],
                                    opacity: 0.2
                                }
                            },

                        }
                    ],
                    color: ['#0EFCFF', '#FD1133', '#f55', '#1F2EF'],
                    grid: {
                        left: '14%',
                        right: '10%',
                        top: '25%',
                        bottom: '15%'
                        // containLabel: true
                    },
                    series: [

                        {
                            name: '',//'每日空调流量计',
                            type: 'line',
                            smooth: true,
                            data: [<%=HTSource["Kongtiao"] %>],

                label: {
                    color: 'rgba(255, 255, 255, 0.3)', fontFamily: 'sans-serif',
                    //字体大小
                    fontSize: 18
                },
                itemStyle: {
                    normal: {

                        /*color: 'green',*/
                        color: new echarts.graphic.LinearGradient(
                            0, 0, 0, 1,
                            [

                                { offset: 1, color: 'rgba(100,188,1211,1)' }

                            ]),
                        shadowBlur: 15,

                        shadowColor: 'rgba(0, 37, 155, 0.7)',
                        label: {
                            show: true, position: 'top',
                            rotate: 0,
                            formatter: '{c}',
                            textStyle: {
                                color: 'white',
                                fontSize: 12
                            }
                        },

                        labelLine: {
                            show: false
                        }
                    }
                }
            },
        ],
    };
    if (option3 && typeof option3 === "object") {
        myChart3.setOption(option3, true);
    }
</script>

<script>


    var dom4 = document.getElementById("chart_4");
    var myChart4 = echarts.init(dom4);

    var option4 = {
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            x: '35%',
            y: '0%',
            data: ["压缩空气"],
            textStyle: {
                color: "#fff",
                fontSize: 8
            },
            itemWidth: 10,
            itemHeight: 10,
        },
        calculable: true,
        xAxis: [
            {

                type: 'category',
                data: [<%=HTTimeLine["AirDay"]%>],
                            axisLabel: {
                                interval: 0,
                                rotate: 40,
                                textStyle: {
                                    fontSize: 8,
                                    color: 'rgba(255,255,255,.7)',
                                }
                            },
                            "axisTick": {       //y轴刻度线
                                "show": false
                            },
                            "axisLine": {       //y轴
                                "show": false,
                            },
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            scale: true,
                            name: 'Unit：m³',
                            nameTextStyle: {
                                color: 'rgba(255,255,255,.7)',
                                fontSize: 8
                            },
                            //max: 100,
                            min: 0,
                            boundaryGap: [0.2, 0.2],
                            "axisTick": {       //y轴刻度线
                                "show": false
                            },
                            "axisLine": {       //y轴
                                "show": false,
                            },
                            axisLabel: {
                                textStyle: {
                                    color: 'rgba(255,255,255,.8)',
                                    fontSize: 8,
                                }
                            },
                            splitLine: {  //决定是否显示坐标中网格
                                show: true,
                                lineStyle: {
                                    color: ['#fff'],
                                    opacity: 0.2
                                }
                            },
                        },
                        {
                            type: 'value',
                            scale: true,
                            show: false,

                            nameTextStyle: {
                                color: 'rgba(255,255,255,.2)',
                            },
                            max: 1,
                            min: 0,
                            boundaryGap: [0.2, 0.2],
                            "axisTick": {       //y轴刻度线
                                "show": false
                            },
                            "axisLine": {       //y轴
                                "show": false,
                            },
                            axisLabel: {
                                textStyle: {
                                    color: 'rgba(255,255,255,.2)',
                                    // opacity: 0.1,
                                }
                            },
                            splitLine: {  //决定是否显示坐标中网格
                                show: true,
                                lineStyle: {
                                    color: ['#fff'],
                                    opacity: 0.2
                                }
                            },

                        }
                    ],
                    color: ['#0EFCFF', '#FD1133', '#f55', '#1F2EF'],
                    grid: {
                        left: '14%',
                        right: '10%',
                        top: '25%',
                        bottom: '15%'
                        // containLabel: true
                    },
                    series: [

                        {
                            name: '',//'每日空调流量计',
                            type: 'line',
                            smooth: true,
                            data: [<%=HTSource["Air"] %>],

                label: {
                    color: 'rgba(255, 255, 255, 0.3)', fontFamily: 'sans-serif',
                    //字体大小
                    fontSize: 18
                },
                itemStyle: {
                    normal: {

                        /*color: 'green',*/
                        color: new echarts.graphic.LinearGradient(
                            0, 0, 0, 1,
                            [

                                { offset: 1, color: 'rgba(100,188,1211,1)' }

                            ]),
                        shadowBlur: 15,

                        shadowColor: 'rgba(0, 37, 155, 0.7)',
                        label: {
                            show: true, position: 'top',
                            rotate: 0,
                            formatter: '{c}',
                            textStyle: {
                                color: 'white',
                                fontSize: 12
                            }
                        },

                        labelLine: {
                            show: false
                        }
                    }
                }
            },
        ],
    };
    if (option4 && typeof option4 === "object") {
        myChart4.setOption(option4, true);
    }
</script>