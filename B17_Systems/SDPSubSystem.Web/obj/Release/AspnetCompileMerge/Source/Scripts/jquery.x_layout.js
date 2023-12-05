; (function ($, window, document, undefined) {
    var accordion = function(ele,opts){
        this.el = ele;
        this.defaults = {
            //url: undefined
        }
        this.options = $.extend({}, this.defaults, opts);
    }
    accordion.prototype = {
        init:function(){
            var links = this.el.find('.layout-link');
            links.off('click').on('click',{ el: this.el, multiple: this.multiple },this.dropdown);
        },
        dropdown:function(e){
            var $el = e.data.el;
            $this = $(this),
            $next = $this.next();

            $next.slideToggle();
            $this.parent().toggleClass('open');

            if (!e.data.multiple) {
                $el.find('.layout-submenu').not($next).slideUp().parent().removeClass('open');
            };
        }
    }
    $.fn.x_layout = function (options) {
        var _accordion = new accordion(this, options);
        _accordion.init();
        var _menuheight = $(".layout-left").height() - $(".layout-accordion>li").length * $(".layout-accordion>li .layout-link:first").outerHeight() - $(".layout-left-top").outerHeight();
        if (_menuheight > 0) {
            $(".layout-submenu").height(_menuheight);
        }
        //var _width = $(window).width();
        //if (_width <= 768) {
        //    $(".layout-left").width("100%");
        //}
        //else {
        //    $(".layout-left").width("200px");
        //}
        //隐藏、显示左边菜单事件
        //$(".layout-header li").click(function () {
        //    var skin = $(this).attr("class");
        //    $.cookie("css_skin", skin, { expires: 365 });
        //    var url = $("#css-skin").attr('href');
        //    var oldskin = url.substring(url.lastIndexOf('-') + 1, url.lastIndexOf('.'));
        //    url = url.replace(oldskin, skin);
        //    $("#css-skin").attr('href', url);
        //});
        //最后切换样式
//        var cssskin = $.cookie("css_skin");
//        if (typeof (cssskin) != 'undefined') {
//            var url = $("#css-skin").attr('href');
//            var skin = url.substring(url.lastIndexOf('-') + 1, url.lastIndexOf('.'));
//            if (cssskin != skin) {
//                url = url.replace(skin, cssskin);
//                $("#css-skin").attr('href', url);
//                //setTimeout(_accordion.init(),2000);
//            }
        //        }
        if (options && options.isFirst) {
            $("#accordion .layout-submenu a").click(function () {
                if ($(window).width() <= 768) {
                    $(".layout-splits").trigger("click");
                }
            });
            $(".layout-splits").click(function () {
                if ($(this).find("span:eq(0)").attr("class") == "glyphicon glyphicon-triangle-left") {
                    $(".layout-left").hide();
                    $(this).find("span:eq(0)").attr("class", "glyphicon glyphicon-triangle-right")
                }
                else {
                    $(".layout-left").show();
                    $(this).find("span:eq(0)").attr("class", "glyphicon glyphicon-triangle-left")
                }
            });
            $(".layout-splits").trigger("click");
            //设置菜单上下两端固定
            
            $(window).resize(function () {
                _menuheight = $(".layout-left").height() - $(".layout-accordion>li").length * $(".layout-accordion>li .layout-link:first").outerHeight() - $(".layout-left-top").outerHeight();
                if (_menuheight > 0) {
                    $(".layout-submenu").height(_menuheight);
                }
            });
        }
    };
})(jQuery, window, document);