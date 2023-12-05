var tooltipoptions = {
    delay: { show: 500, hide: 100 },
    title: '',
    placement: 'bottom'
};
var _editUrl = '';


function showTooltip(obj, message) {
    tooltipoptions.title = message;
    obj.tooltip(tooltipoptions);
    obj.tooltip("show");
    obj.on('hidden.bs.tooltip', function () {
        $(this).tooltip("destroy");
    })
}

function arrToStr(value) {
    // 选中的文本值集合
    var checkText = [];
    if (value.length > 0) {
        for (var i = 0; i < value.length; i++) {
            checkText.push(value[i].textContent);
        }
    }
    return checkText.join(',');
}

//新增或修改数据通用方法(用于弹出层ID="_Dialog"的弹出层)
function postSave(url, json_str, tableName) {
    $.post(url, { postjson: json_str }, function (result) {
        if (result.IsSuccess) {
            $('#_Dialog').modal("hide");
            $("#" + tableName).DataTable().ajax.reload();
        }
        else {
            if (result.ControlMessage) {

                $.each(result.ControlMessage, function (key, value) {
                    tooltipoptions.title = value;
                    $("#_Dialog #" + key).tooltip(tooltipoptions);
                    $("#_Dialog #" + key).tooltip("show");
                    $("#_Dialog #" + key).on('hidden.bs.tooltip', function () {
                        $(this).tooltip("destroy");
                    })
                });
                
            }
            if (result.Msg) {
                var m_box = $("#_Dialog #message-box");
                var m_ul = m_box.find("ul");
                m_ul.empty();
                var flag = false;
                $.each(result.Msg, function (idx, msg) {
                    var li = $("<li/>");
                    li.text(msg);
                    m_ul.append(li);
                    flag = true;
                });
                if (flag) {
                    m_box.fadeIn();
                    setTimeout(function () { m_box.fadeOut(); }, 5000);
                }
            }
        }
    })
}
function batchPostDeleteByProject(url, tableName) {
    var ids = "";

    var message_lang = "Are you sure you want to delete the selected data?";
    var title_lang = "Confirm";
    var delete_lang = "Delete";
    var cancel_lang = "Cancel";
    var msg_info = "Please choose the data which you want to delete!";
    var info_lang = "Information"
    if ($('body').attr("LangID") != "1") {
        message_lang = "是否确认删除所选中的数据?";
        title_lang = " 确认";
        delete_lang = "删除";
        cancel_lang = "取消";
        msg_info = "请选择要删除的数据!";
        info_lang = "提示";
    }
    $.each($("#" + tableName + " tbody input[name='selectid']:checked"), function (idx, item) {
        ids += $(item).val() + ",";
    });

    if (ids != "") {
        ids = ids.substring(0, ids.length - 1);
        bootbox.confirm({
            message: message_lang, title: title_lang,
            buttons: {
                confirm: {
                    label: delete_lang,
                    className: 'btn-danger'
                },
                cancel: {
                    label: cancel_lang,
                    className: 'btn-default'
                }
            },
            callback: function (result) {
                if (result) {
                    $.each($("#" + tableName + " tbody input[name='selectid']:checked"), function (idx, item) {
                        ids = $(item).val();
                        var tr = $(this).closest('tr');
                        var proj = $(tr).find('td:eq(2)').text();
                        //debugger

                        $.post(url, { ids: ids, Project: proj }, function (result) {
                            if (result.IsSuccess) {
                                $("#" + tableName).DataTable().ajax.reload();
                            }
                            else {
                                if (result.Msg) {
                                    var m_ul = "<ul>";
                                    var flag = false;
                                    $.each(result.Msg, function (idx, msg) {
                                        m_ul += "<li>" + msg + "</li>";
                                        flag = true;
                                    });
                                    m_ul += "</ul>";
                                    if (flag) { bootbox.alert({ message: m_ul, title: "Error" }); }
                                }
                            }
                        });

                    });
                    
                }
            }
        });
    }
    else {
        bootbox.alert({ message: msg_info, title: info_lang })
    }
}

//删除数据通用方法
function postDelete(url, tableName) {
    var ids = "";
    
    var message_lang = "Are you sure you want to delete the selected data?";
    var title_lang = "Confirm";
    var delete_lang = "Delete";
    var cancel_lang = "Cancel";
    var msg_info = "Please choose the data which you want to delete!";
    var info_lang = "Information"
    if ($('body').attr("LangID") != "1") {
        message_lang = "是否确认删除所选中的数据?";
        title_lang = " 确认";
        delete_lang = "删除";
        cancel_lang = "取消";
        msg_info = "请选择要删除的数据!";
        info_lang = "提示";
    }
    $.each($("#"+tableName+" tbody input[name='selectid']:checked"), function (idx, item) {
        ids += $(item).val() + ",";
    });
    if (ids != "") {
        ids = ids.substring(0, ids.length - 1);
        bootbox.confirm({ message: message_lang, title: title_lang,
            buttons: {
                confirm: {
                    label: delete_lang,
                    className: 'btn-danger'
                },
                cancel: {
                    label: cancel_lang,
                    className: 'btn-default'
                }
            },  
            callback:function(result){
            if(result){
                $.post(url, { ids: ids }, function (result) {
                if (result.IsSuccess) {
                    $("#" + tableName).DataTable().ajax.reload();
                }
                else {
                    if (result.Msg) {
                        var m_ul = "<ul>";
                        var flag = false;
                        $.each(result.Msg, function (idx, msg) {
                            m_ul += "<li>" + msg + "</li>";
                            flag = true;
                        });
                        m_ul += "</ul>";
                        if (flag) { bootbox.alert({message: m_ul,title:"Error"}); }
                    }
                }
            });
            }
        }});
    }
    else {
        bootbox.alert({ message: msg_info, title: info_lang })
    }
}

function postDelete2(url, tableName) {
    var ids = "";

    var message_lang = "Are you sure you want to delete the selected data?";
    var title_lang = "Confirm";
    var delete_lang = "Delete";
    var cancel_lang = "Cancel";
    var msg_info = "Please choose the data which you want to delete!";
    var info_lang = "Information"
    if ($('body').attr("LangID") != "1") {
        message_lang = "是否确认删除所选中的数据?";
        title_lang = " 确认";
        delete_lang = "删除";
        cancel_lang = "取消";
        msg_info = "请选择要删除的数据!";
        info_lang = "提示";
    }
    $.each($("#" + tableName + " tbody input[name='selectid']:checked"), function (idx, item) {
        ids += $(item).val() + ",";
    });
    if (ids != "") {
        ids = ids.substring(0, ids.length - 1);
        bootbox.confirm({
            message: message_lang, title: title_lang,
            buttons: {
                confirm: {
                    label: delete_lang,
                    className: 'btn-danger'
                },
                cancel: {
                    label: cancel_lang,
                    className: 'btn-default'
                }
            },
            callback: function (result) {
                if (result) {
                    $.post(url, { ids: ids }, function (result) {
                        if (result.IsSuccess) {

                            $.globalMessenger().post({
                                message: "Delete Success",//提示信息
                                type: 'info',//消息类型。error、info、success
                                hideAfter: 1,//多长时间消失
                                showCloseButton: true,//是否显示关闭按钮
                                hideOnNavigate: true //是否隐藏导航
                            });
                            load();

                        }
                        else {
                            if (result.Msg) {
                                var m_ul = "<ul>";
                                var flag = false;
                                $.each(result.Msg, function (idx, msg) {
                                    m_ul += "<li>" + msg + "</li>";
                                    flag = true;
                                });
                                m_ul += "</ul>";
                                if (flag) { bootbox.alert({ message: m_ul, title: "Error" }); }
                            }
                        }
                    });
                }
            }
        });
    }
    else {
        bootbox.alert({ message: msg_info, title: info_lang })
    }
}

function isNull(data) {
    return (data == "" || data == undefined || data == null) ? true : false;
}



//通用加载table的方法，查询条件控件ID必须为code
//url:后台的加载数据方法，必须包含分页查询
//被加载的table的名称（必须已经写了表头）
//cols 加载数据的列-字符串数组，顺序与table的表头一致
//editCol 该列会被加载成链接，点击后弹出层修改数据
//editUrl 修改时的Url连接
function postLoadTable(url, tableName, cols, editCol, editUrl) {
    debugger;
    //console.log($('body').attr("LangID"));
    //console.log('/Content/Datatables/Lang/Lang_' + $('body').attr("LangID") + '.txt');
    //debugger;
    var domCustomized = "<'row'<'col-sm-3'l><'col-sm-3'><'col-sm-6'>>" +
        "<'row'<'col-sm-12'tr>>" +
        "<'row'<'col-sm-3'i><'col-sm-3'><'col-sm-6'p>>";
    var height = parseInt($("body").css("height").replace("px", "")) - 140;
    var defs = [];
    for (var i = 0; i < cols.length; i++) {
        if (cols[i] == editCol) {
            defs.push({
                "className": "text-left", "targets": i, "orderable": false, "data": cols[i], "render": function (data, type, full, meta) {
                    if (data) {
                        var str = "javascript" + ":postEdit(" + full.ID + ",'" + encodeURIComponent(editUrl) + "')";
                        return '<a href="#" onclick="'+str+'">' + data + '</a>';
                    } else {
                        return '';
                    }
                }
            });
        }
        else if (cols[i] == "ID") {
            defs.push({ "className": "text-center", "targets": i, "orderable": false, "data": "ID", "render": function (data, type, full, meta) {
                return '<input type="checkbox" name="selectid" value="' + data + '" />';
            }
            });
        }
        else {
            defs.push({
                "className": "text-left", "targets": i, "orderable": false, "data": cols[i], "render": function (data, type, full, meta) {
                    //debugger;
                return data;
            }
            });
        }
    }
    //debugger;
    var t = $("#" + tableName).DataTable({
        "dom": domCustomized,
        //autoWidth: false,
        deferRender: true,
        filter: false,
        processing: true,
        serverSide: true,
        order: [],
        scrollY: height,
        scrollX: true,
        scrollCollapse: true,
        scroller: true,
        searching: true,
        info: true,
        paging: true,
        paginationType: "full_numbers",
        lengthMenu: [[10, 20, 50, 100, 99999999], [10, 20, 50, 100, "All"]],
        pageLength: 10,
        lengthChange: true, //fixedColumns: { leftColumns: 2 },
        serverMethod: "POST",
        ajax: {
            url: url
        },
        language: {
            url: '/Content/Datatables/Lang/Lang_' + $('body').attr("LangID") + '.txt'
        },
        columnDefs: defs,
        fnServerParams: function (data) {
            data.code = $("#code").val();
        }

    });
}

function postLoadTableWithoutEditCol(url, tableName, cols) {
    var exportTitle = $("#dateFrom").val() + '-' + $("#dateTo").val() + ' ' + $("#Line").find("option:selected").text() + ' SMT Kitting Plan Data';
    var domCustomized = "<'row'<'col-sm-3'l><'col-sm-3'i><'col-sm-5'p><'col-sm-1'B>>" +
        "<'row'<'col-sm-12'tr>>";
    var height = parseInt($("body").css("height").replace("px", "")) - 140;
    var defs = [];
    for (var i = 0; i < cols.length; i++) {
        if (cols[i] == "ID") {
            defs.push({
                "className": "text-center", "targets": i, "orderable": false, "data": "ID", "render": function (data, type, full, meta) {
                    return '<input type="checkbox" name="selectid" value="' + data + '" />';
                }
            });
        }
        else {
            defs.push({
                "className": "text-left", "targets": i, "orderable": false, "data": cols[i], "render": function (data, type, full, meta) {
                    return data || "";
                }
            });
        }
    }
    
    var t = $("#" + tableName).dataTable({
        dom: domCustomized,
        "buttons": {
            buttons: [
                //{ extend: 'copy', className: 'copyButton' },
                {
                    extend: 'excel',
                    text: '导出excel',
                    className: 'excelButton btn btn-info',
                    title: exportTitle,
                }
            ]
        },
        autoWidth: true,
        filter: false,
        processing: true,
        serverSide: true,
        order: [],
        scrollY: height,
        scrollX: true,
        scrollCollapse: false,
        paging: true,
        paginationType: "full_numbers",
        lengthMenu: [[10,20, 50, 100,99999999], [10,20, 50, 100,"All"]],
        pageLength:10,
        lengthChange: true,
        //fixedColumns: { leftColumns: 2 },
        serverMethod: "POST",
        ajax: {
            url: url
        },
        language: {
            url: '/Content/Datatables/Lang/Lang_' + $('body').attr("LangID") + ".txt"
        },
        columnDefs: defs,
        fnServerParams: function (data) {
            data.dateFrom = $("#dateFrom").val();
            data.dateTo = $("#dateTo").val();
            data.Line = $("#Line").val();
            data.Project = $("#Project").val();
        }

    });
}

function postLoadTableByBuildPlan(url, tableName, cols, editCol, editUrl) {
    var height = parseInt($("body").css("height").replace("px", "")) - 240;
    var defs = [];
    for (var i = 0; i < cols.length; i++) {
        if (cols[i] == editCol) {
            defs.push({
                "className": "text-left", "targets": i, "orderable": false, "data": cols[i], "render": function (data, type, full, meta) {
                    if (data) {
                        var str = "javascript" + ":postEdit(" + full.ID + ",'" + encodeURIComponent(editUrl) + "')";
                        return '<a href="#" onclick="' + str + '">' + data + '</a>';
                    } else {
                        return '';
                    }
                }
            });
        }
        else if (cols[i] == "ID") {
            defs.push({
                "className": "text-center", "targets": i, "orderable": false, "data": "ID", "render": function (data, type, full, meta) {
                    return '<input type="checkbox" name="selectid" value="' + data + '" />';
                }
            });
        }
        else {
            defs.push({
                "className": "text-left", "targets": i, "orderable": false, "data": cols[i], "render": function (data, type, full, meta) {
                    return data || "";
                }
            });
        }
    }
    var t = $("#" + tableName).dataTable({
        autoWidth: true, filter: false, processing: true, serverSide: true, order: [], scrollY: height, scrollX: true, scrollCollapse: false,
        paging: true, paginationType: "full_numbers", lengthMenu: [[20, 50, 100], [20, 50, 100]], lengthChange: false, //fixedColumns: { leftColumns: 2 },
        serverMethod: "POST",
        retrieve: true,
        ajax: {
            url: url
        },
        language: {
            url: '/Content/Datatables/Lang/Lang_' + $('body').attr("LangID") + ".txt"
        },
        columnDefs: defs,
        fnServerParams: function (data) {
            data.Code1_PlanDate = $("#date").val();
            data.Code2_ShiftCode = $('#Shift option:selected').text();
            data.Code3_LineID = $("#Line").val();
        }

    });
}



function postLoadTableByBuildPlanAdd(url, tableName, cols, editCol, editUrl) {
    //debugger;
    var height = parseInt($("body").css("height").replace("px", "")) - 240;
    var defs = [];
    for (var i = 0; i < cols.length; i++) {
        if (cols[i] == editCol) {
            defs.push({
                "className": "text-left", "targets": i, "orderable": false, "data": cols[i], "render": function (data, type, full, meta) {
                    if (data) {
                        var str = "javascript" + ":postEdit(" + full.ID + ",'" + encodeURIComponent(editUrl) + "')";
                        return '<a href="#" onclick="' + str + '">' + data + '</a>';
                    } else {
                        return '';
                    }
                }
            });
        }
        else if (cols[i] == "ID") {
            defs.push({
                "className": "text-center", "targets": i, "orderable": false, "data": "ID", "render": function (data, type, full, meta) {
                    return '<input type="checkbox" name="selectid" value="' + data + '" />';
                }
            });
        }
        else {
            defs.push({
                "className": "text-left", "targets": i, "orderable": false, "data": cols[i], "render": function (data, type, full, meta) {
                    return data || "";
                }
            });
        }
    }
    var t = $("#" + tableName).dataTable({
        autoWidth: true, filter: false, processing: true, serverSide: true, order: [], scrollY: height, scrollX: true, scrollCollapse: false,
        paging: true, paginationType: "full_numbers", lengthMenu: [[20, 50, 100], [20, 50, 100]], lengthChange: false, //fixedColumns: { leftColumns: 2 },
        serverMethod: "POST",
        retrieve: true,
        ajax: {
            url: url
        },
        language: {
            url: '/Content/Datatables/Lang/Lang_' + $('body').attr("LangID") + ".txt"
        },
        columnDefs: defs,
        fnServerParams: function (data) {
            //debugger;
            data.Code1_PlanDate = $("#date").val();
            data.Code2_ShiftCode = $('#Shift option:selected').text();
            data.Code3_LineID = $("#Line").val();
        }

    });
}

function postLoadTableByComputerOwner(url, tableName, cols, editCol, editUrl) {
    var height = parseInt($("body").css("height").replace("px", "")) - 140;
    var defs = [];
    for (var i = 0; i < cols.length; i++) {
        if (cols[i] == editCol) {
            defs.push({
                "className": "text-center", "targets": i, "orderable": false, "data": cols[i], "searchable": false,"render": function (data, type, full, meta) {
                    if (data) {
                        var str = "javascript" + ":postEdit(" + full.ID + ",'" + encodeURIComponent(editUrl) + "')";
                        return '<a href="#" onclick="' + str + '">' + data + '</a>';
                    } else {
                        return '';
                    }
                }
            });
        }
        else if (cols[i] == "ID") {
            defs.push({
                "className": "text-center", "targets": i, "orderable": false, "data": "ID", "searchable": false,"render": function (data, type, full, meta) {
                    return '<input type="checkbox" name="selectid" value="' + data + '" />';
                }
            });
        }
        else if (cols[i] == "RowNum") {
            defs.push({
                "className": "text-center", "targets": i, "orderable": false, "data": null, "searchable": false,"width": "50px",
                //"render": function (data, type, full, meta) {
                //    return '<input type="checkbox" name="selectid" value="' + data + '" />';
                //}
            });
        }
        else {
            defs.push({
                "className": "text-center", "targets": i, "orderable": false, "data": cols[i], "searchable": true,"render": function (data, type, full, meta) {
                    return data || "";
                }
            });
        }
    }
    var domCustomized = "<'row'<'col-md-2'l><'col-md-2'i><'col-md-8'p>>" +
        "<'row'<'col-sm-12'tr>>";
    var page_dt = $("#" + tableName).DataTable({
        "dom": domCustomized,
        "autoWidth": true,
        "filter": true,
        "processing": true,
        "serverSide": true,
        "order": [],
        "scrollY": height,
        "scrollX": true,
        "scrollCollapse": true,
        "paging": true,
        "paginationType": "full_numbers",
        "lengthMenu": [[5, 10, 20, 50, 100, 99999999], [5, 10, 20, 50, 100, "All"]], 
        "lengthChange": true,
        "pageLength": 10,//默认显示所有条数
        //fixedColumns: { leftColumns: 2 },
        "serverMethod": "POST",
        ajax: {
            url: url
        },
        language: {
            url: '/Content/Datatables/Lang/Lang_' + $('body').attr("LangID") + ".txt"
            //searchPlaceholder: 'Computer Name...',
        },
        columnDefs: defs,
        fnServerParams: function (data) {
            data.code = $("#code").val();
        }

    });

    // .draw.dt 是 DataTables 的绘制事件，监听绘制事件，绘制的时候给 datatales 编号。
    page_dt.on('draw.dt', function () {
        //给第一列编号
        page_dt.column(1, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    });
}

//通用加载table的方法，查询条件控件ID必须为code
//url:后台的加载数据方法，必须包含分页查询
//被加载的table的名称（必须已经写了表头）
//cols 加载数据的列-字符串数组，顺序与table的表头一致
//editCol 该列会被加载成链接，点击后弹出层修改数据
//editUrl 修改时的Url连接
// poststr 传一个对象数据到后台
function postLoadTableByStr(url, tableName, cols, editCol, editUrl) {
    var height = parseInt($("body").css("height").replace("px", "")) - 140;
    var defs = [];
    for (var i = 0; i < cols.length; i++) {
        if (cols[i] == editCol) {
            defs.push({
                "className": "text-left", "targets": i, "orderable": false, "data": cols[i], "render": function (data, type, full, meta) {
                    if (data) {
                        var str = "javascript" + ":postEdit(" + full.ID + ",'" + encodeURIComponent(editUrl) + "')";
                        return '<a href="#" onclick="' + str + '">' + data + '</a>';
                    } else {
                        return '';
                    }
                }
            });
        }
        else if (cols[i] == "ID") {
            defs.push({
                "className": "text-center", "targets": i, "orderable": false, "data": "ID", "render": function (data, type, full, meta) {
                    return '<input type="checkbox" name="selectid" value="' + data + '" />';
                }
            });
        }
        else {
            defs.push({
                "className": "text-left", "targets": i, "orderable": false, "data": cols[i], "render": function (data, type, full, meta) {
                    return data || "";
                }
            });
        }
    }
    var t = $("#" + tableName).dataTable({
        autoWidth: true, filter: false, processing: true, serverSide: true, order: [], scrollY: height, scrollX: true, scrollCollapse: false,
        paging: true, paginationType: "full_numbers", lengthMenu: [[20, 50, 100], [20, 50, 100]], lengthChange: false, //fixedColumns: { leftColumns: 2 },
        serverMethod: "POST",
        ajax: {
            url: url
        },
        language: {
            url: '/Content/Datatables/Lang/Lang_' + $('body').attr("LangID") + ".txt"
        },
        columnDefs: defs,
        fnServerParams: function (data) {
            data.code = $("#PostStr").val();
        }

    });
}



//弹出修改对话框
function postEdit(id,newUrl) {
    newUrl = decodeURIComponent(newUrl);
    $("#_Dialog").load(newUrl, { ID: id }, function () {
        $(this).modal("show");
    });
}

function isNumber(value) {
    var patrn = /^(-)?\d+(\.\d+)?$/;
    if (patrn.exec(value) == null || value == "") {
        return false
    } else {
        return true
    }
}

// 根据周数获取日期
// w 周
// y 年
function getDateOfISOWeek(w, y) {
    var simple = new Date(y, 0, 1 + (w - 2) * 7);
    var dow = simple.getDay();
    var ISOweekStart = simple;
    if (dow <= 4)
        ISOweekStart.setDate(simple.getDate() - simple.getDay() + 1);
    else
        ISOweekStart.setDate(simple.getDate() + 8 - simple.getDay());
    //console.log(ISOweekStart.getFullYear())
    //console.log(ISOweekStart.getMonth() + 1)
    //console.log(ISOweekStart.getDate())
    return ISOweekStart;
}

//getWeek(new Date('2019-11-26'))
function getWeek(str) {
    let day = Date.parse(str);
    //如果不是当年的第一天不是星期一，则该日所属周数为上一年的最后一周
    day = new Date(day);

    if (day.getDay() !== 1) {
        day = day.getTime() - 24 * 60 * 60 * 1000
        day = new Date(day);
    }
    day.setMonth(0);
    day.setDate(1);
    day.setHours(0);
    day.setMinutes(0);
    day.setSeconds(0);//到这里就得到该年的一月一日

    let today = Date.parse(str);
    today = new Date(today);
    let todayWeek = today.getDay()

    //计算日期是一年中的第几天
    let rankDay = Math.ceil((today.getTime() - day.getTime()) / (1000 * 24 * 60 * 60))
    let rankWeek = Math.ceil(rankDay / 7)
    rankWeek = rankWeek < 10 ? ("0" + rankWeek) : rankWeek
    let year = day.getFullYear().toString()
    console.log(year)
    console.log(rankWeek)
    return year + rankWeek
}

// 根据日期计算 周一和周日  日期
function getWeekBeginAndEndDate(date) {
    let nowTime = date.getTime()
    let day = date.getDay()
    let oneDayTime = 24 * 60 * 60 * 1000
    let MonDayTime = nowTime - (day - 1) * oneDayTime
    let sunDayTime = nowTime + (7 - day) * oneDayTime
    let monDay = new Date(MonDayTime)
    let sunDay = new Date(sunDayTime)
    let startTime = this.formatDate(monDay)
    let endTime = this.formatDate(sunDay)
    var WeekBeginAndEnd = { startTime, endTime};

    return WeekBeginAndEnd;
}


// 格式化日期 转成字符串：年月日
function formatDate(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    m = m < 10 ? ('0' + m) : m;
    var d = date.getDate();
    d = d < 10 ? ('0' + d) : d;

    return y + '-' + m + '-' + d;

}

//获取本月最后一天
function getLastDateOfNowMonth() {
    var monthDate = {};
    var now = new Date(); //当前日期
    var nowMonth = now.getMonth(); //当前月
    var nowYear = now.getFullYear(); //当前年
    //本月的开始时间
    var monthStartDate = new Date(nowYear, nowMonth, 1);
    var yy = monthStartDate.getFullYear() + "-";
    var mm =
        (monthStartDate.getMonth() + 1 < 10
            ? "0" + (monthStartDate.getMonth() + 1)
            : monthStartDate.getMonth() + 1) + "-";
    var dd =
        monthStartDate.getDate() < 10
            ? "0" + monthStartDate.getDate()
            : monthStartDate.getDate();
    //本月的结束时间
    var monthEndDate = new Date(nowYear, nowMonth + 1, 0);
    var YY = monthEndDate.getFullYear() + "-";
    var MM =
        (monthEndDate.getMonth() + 1 < 10
            ? "0" + (monthEndDate.getMonth() + 1)
            : monthEndDate.getMonth() + 1) + "-";
    var DD =
        monthEndDate.getDate() < 10
            ? "0" + monthEndDate.getDate()
            : monthEndDate.getDate();
    monthDate.start_day = yy + mm + dd;
    monthDate.end_day = YY + MM + DD;

}

//获取某月最后一天
function getLastDateOfMonth(nowMonth,nowYear) {
    var monthDate = {};
    
    //本月的开始时间
    nowMonth = parseInt(nowMonth);
    var monthStartDate = new Date(nowYear, nowMonth-1, 1);
    var yy = monthStartDate.getFullYear() + "-";
    var mm =
        (monthStartDate.getMonth() + 1 < 10
            ? "0" + (monthStartDate.getMonth() + 1)
            : monthStartDate.getMonth() + 1) + "-";
    var dd =
        monthStartDate.getDate() < 10
            ? "0" + monthStartDate.getDate()
            : monthStartDate.getDate();
    //本月的结束时间
    var monthEndDate = new Date(nowYear, nowMonth, 0);
    var YY = monthEndDate.getFullYear() + "-";
    var MM =
        (monthEndDate.getMonth() + 1 < 10
            ? "0" + (monthEndDate.getMonth() + 1)
            : monthEndDate.getMonth() + 1) + "-";
    var DD =
        monthEndDate.getDate() < 10
            ? "0" + monthEndDate.getDate()
            : monthEndDate.getDate();
    monthDate.start_day = yy + mm + dd;
    monthDate.end_day = YY + MM + DD;
    return monthDate.end_day;
}

//时间转成字符串：年月日 时分秒
function formatDateTime(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    m = m < 10 ? ('0' + m) : m;
    var d = date.getDate();
    d = d < 10 ? ('0' + d) : d;
    var h = date.getHours();
    h = h < 10 ? ('0' + h) : h;
    var minute = date.getMinutes();
    minute = minute < 10 ? ('0' + minute) : minute;
    var second = date.getSeconds();
    second = second < 10 ? ('0' + second) : second;
    return y + '-' + m + '-' + d + ' ' + h + ':' + minute + ':' + second; 
}


//使用Javascript语言，将时间戳转换为类似新浪微博的时间的表示方法。 要求转换规则： 1分钟以内显示为：刚刚 1小时以内显示为：N分钟前 当天以内显示为：今天 N点N分（如：今天 22:33） 昨天时间显示为：昨天 N点N分（如：昨天 10:15） 当年以内显示为：N月N日 N点N分（如：02月03日 09:33） 今年以前显示为：N年N月N日 N点N分（如：2000年09月18日 15:59）
function timestampFormat(timestamp) {
    function zeroize(num) {
        return (String(num).length == 1 ? '0' : '') + num;
    }

    var curTimestamp = parseInt(new Date().getTime() / 1000); //当前时间戳
    var timestampDiff = curTimestamp - timestamp; // 参数时间戳与当前时间戳相差秒数

    var curDate = new Date(curTimestamp * 1000); // 当前时间日期对象
    var tmDate = new Date(timestamp * 1000);  // 参数时间戳转换成的日期对象

    var Y = tmDate.getFullYear(), m = tmDate.getMonth() + 1, d = tmDate.getDate();
    var H = tmDate.getHours(), i = tmDate.getMinutes(), s = tmDate.getSeconds();

    if (timestampDiff < 60) { // 一分钟以内
        return "刚刚";
    } else if (timestampDiff < 3600) { // 一小时前之内
        return Math.floor(timestampDiff / 60) + "分钟前";
    } else if (curDate.getFullYear() == Y && curDate.getMonth() + 1 == m && curDate.getDate() == d) {
        return '今天' + zeroize(H) + ':' + zeroize(i);
    } else {
        var newDate = new Date((curTimestamp - 86400) * 1000); // 参数中的时间戳加一天转换成的日期对象
        if (newDate.getFullYear() == Y && newDate.getMonth() + 1 == m && newDate.getDate() == d) {
            return '昨天' + zeroize(H) + ':' + zeroize(i);
        } else if (curDate.getFullYear() == Y) {
            return zeroize(m) + '月' + zeroize(d) + '日 ' + zeroize(H) + ':' + zeroize(i);
        } else {
            return Y + '年' + zeroize(m) + '月' + zeroize(d) + '日 ' + zeroize(H) + ':' + zeroize(i);
        }
    }
}

//timestampFormat2(Date.parse('2012-10-10 10:10:10')/1000); //2012年10月10日 10:10
function timestampFormat2(timestamp) {
    function zeroize(num) {
        return (String(num).length == 1 ? '0' : '') + num;
    }

    var curTimestamp = parseInt(new Date().getTime() / 1000); //当前时间戳
    var timestampDiff = curTimestamp - timestamp; // 参数时间戳与当前时间戳相差秒数

    var curDate = new Date(curTimestamp * 1000); // 当前时间日期对象
    var tmDate = new Date(timestamp * 1000);  // 参数时间戳转换成的日期对象

    var Y = tmDate.getFullYear(), m = tmDate.getMonth() + 1, d = tmDate.getDate();
    var H = tmDate.getHours(), i = tmDate.getMinutes(), s = tmDate.getSeconds();

    return Y + '-' + zeroize(m) + '-' + zeroize(d) + ' ' + zeroize(H) + ':' + zeroize(i) + ':' + zeroize(s);
}

function nowTimestampFormat(){
    function zeroize(num) {
        return (String(num).length == 1 ? '0' : '') + num;
    }
    var curTimestamp = parseInt(new Date().getTime() / 1000); //当前时间戳
    var curDate = new Date(curTimestamp * 1000); // 当前时间日期对象
    var Y = curDate.getFullYear(), m = curDate.getMonth() + 1, d = curDate.getDate();
    var H = curDate.getHours(), i = curDate.getMinutes(), s = curDate.getSeconds();
    return Y + '-' + zeroize(m) + '-' + zeroize(d) + ' ' + zeroize(H) + ':' + zeroize(i) + ':' + zeroize(s);
}

function nowDateFormat() {
    function zeroize(num) {
        return (String(num).length == 1 ? '0' : '') + num;
    }
    var curTimestamp = parseInt(new Date().getTime() / 1000); //当前时间戳
    var curDate = new Date(curTimestamp * 1000); // 当前时间日期对象
    var Y = curDate.getFullYear(), m = curDate.getMonth() + 1, d = curDate.getDate();
    return Y + '-' + zeroize(m) + '-' + zeroize(d);
}





