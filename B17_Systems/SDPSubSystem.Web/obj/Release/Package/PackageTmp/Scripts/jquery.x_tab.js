; (function ($, window, document, undefined) {
    var tabObj = function(ele,opts){
        this.el = ele;
        this.defaults = {
            contentCtl: $(".layout-content"),
        }
        this.options = $.extend({}, this.defaults, opts);
    }
    tabObj.prototype = {
        init: function () {//初始化对象
            var e = this.el;
            var option = this.options;
            if (e.find("tab-left").length == 0) {
                e.attr("class", (typeof (e.attr("class")) == "undefined" ? "tab-out-container" : e.attr("class") + ' tab-out-container'));
                e.append('<div class="tab-left"><span class="glyphicon glyphicon-chevron-left"></span></div>');
                e.append('<div class="tab-inner-contailner"><ul class="tab-menu"></ul></div>');
                e.append('<div class="tab-right"><span class="glyphicon glyphicon-chevron-right"></span></div>')
                e.append('<div class="tab-list"><span class="glyphicon glyphicon-list"></span></div>')
                e.append('<div class="tab-list-content"><ul></ul></div>');
                option.contentCtl.append('<div class="tab-content"></div>');
                e.find('.tab-right').click(function () {
                    $(".tab-inner-contailner").animate({ "scrollLeft": e.find(".tab-inner-contailner").width() / 2 + e.find(".tab-inner-contailner").scrollLeft() }, 500);
                })
                e.find('.tab-left').click(function () {
                    $(".tab-inner-contailner").animate({ "scrollLeft": e.find(".tab-inner-contailner").scrollLeft() - e.find(".tab-inner-contailner").width() / 2 }, 500);
                })

                e.find('.tab-list').click(function () {
                    $('.tab-list-content').fadeIn(1000);
                })
                e.find(".tab-list-content").mouseover(function () { $(this).show(); }).mouseout(function () { $(this).hide(); })
            }
        },
        Add: function (code, text, url, ishome) {//新增一个标签页
            var obj = this;
            var e = this.el;
            var option = this.options;
            var iframe = $('<iframe id="' + code + '" tag="' + code + '" style="height:100%; width:100%; border:0px; overflow-y:auto; " src="' + url + '"></iframe>');
            if (!this.IsExist(code)) {
                //if (true) {
                ishome = typeof (ishome) == 'undefined' ? false : ishome;

                var li = $('<li tag="' + code + '">' + text + (ishome ? "" : '<span class="glyphicon glyphicon-remove-sign"></span>') + '</li>');
                var li_list = $('<li tag="' + code + '">' + text + ' </li>');


                e.find('.tab-menu').append(li);
                e.find('.tab-list-content').append(li_list);
                if (url.indexOf("?") > 0) {
                    url += "&MCode=" + code;
                }
                else {
                    url += "?MCode=" + code;
                }
                option.contentCtl.find('.tab-content').append(iframe);

                document.getElementById(code).onload = function () {
                    $('.layer').hide();
                }
                li.click(function () {
                    obj.Active(code);
                });
                li_list.click(function () {
                    obj.Active(code);
                })
                li.find('span.glyphicon-remove-sign').click(function () {
                    obj.Remove(code);
                })
            } else {
                //location.reload();
                //$('.tab-menu').tabs('getTab', title).panel('refresh');
                //parent.$('#nav li ul li').eq(1).children('a').reload();
                var _iframe1 = document.getElementById(code);
                _iframe1.contentWindow.location.reload(true);
            }
            this.Active(code);
        },
        GetActiveCode: function () {
            var e = this.el;
            return e.find('.tab-menu li.active').attr('tag');
        },
        IsExist:function(code){
            var e = this.el;
            if (e.find('.tab-menu li[tag="' + code + '"]').length == 0) {
                return false;
            }
            else {
                return true;
            }
        },
        Active: function (code) {//根据标签页的Code激活标签页
            var e = this.el;
            var option = this.options;
            var otag = e.find('.tab-menu li.active').attr("tag");
            option.contentCtl.find('.tab-content iframe[tag="' + otag + '"]').hide();
            e.find('.tab-menu li.active').removeAttr("class");
            e.find('.tab-menu li[tag="' + code + '"]').attr("class", "active");
            option.contentCtl.find('.tab-content iframe[tag="' + code + '"]').fadeIn(1000);
            var left = e.find('.tab-menu li[tag="' + code + '"]').position().left;
            var width = e.find('.tab-menu li[tag="' + code + '"]').width();
            if (left < 0) {//如果被隐藏了，滚动滚动条到该对象的位置让它显示出来
                e.find(".tab-inner-contailner").animate({ "scrollLeft": e.find(".tab-inner-contailner").scrollLeft() + left }, 500);
            }
            else if (left + width > $('.tab-inner-contailner').width()) {
                e.find(".tab-inner-contailner").animate({ "scrollLeft": e.find(".tab-inner-contailner").scrollLeft() + left + width * 2 - e.find('.tab-inner-contailner').width() }, 500);
            }
        },
        Remove: function (code) {//移除一个标签页
            var e = this.el;
            var option = this.options;
            var li = e.find('.tab-menu li[tag="' + code + '"]');
            if (li.attr("class") == "active") {
                if (li.next().length == 0) {
                    this.Active(li.prev().attr("tag"));
                }
                else {
                    this.Active(li.next().attr("tag"));
                }
            }
            li.remove();
            option.contentCtl.find('.tab-content iframe[tag="' + code + '"]').remove();
            e.find('.tab-list-content li[tag="' + code + '"]').remove();
        }
    }

    $.fn.x_tab = function (option) {
        var _tab = new tabObj(this, option);
        _tab.init();
        return _tab;
    }
})(jQuery, window, document);