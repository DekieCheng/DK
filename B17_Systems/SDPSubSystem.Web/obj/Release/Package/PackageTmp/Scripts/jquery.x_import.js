﻿;(function ($, window, document, undefined) {
    var obj_import = function (ele, opt) {
        this.$element = ele;
        this.defaltus = {
            dialog: '_Dialog',
            deleteUrl: '/Base/DeleteFile',
            saveUrl: '/Base/SaveFile',
            template: '#',
            callback: '',
            X_DB_MATRIX:null,
            LangID:1
        };
        this.options = $.extend({}, this.defaltus, opt);
        this.modal = '<div class="modal-dialog">'
                    +'  <div class="modal-content">'
                    +'          <div class="modal-header" >'
                    +'              <button type="button" class="close" style="color:White" data-dismiss="modal" aria-hidden="true">&times;</button>'
                    +'              <h4 class="modal-title" >'+(this.options.langID==1?"Upload File":"上传文件")+'</h4>'
                    +'          </div>'
                    +'          <div class="modal-body">'
                    +'             <div class="form-horizontal">'
                    +'                  <div class="form-group">'
                    +'                      <div class="col-md-12">'
                    + '                          <a href="' + this.options.template + '   " target="_blank">'+(this.options.langID==1?"Template":"模板")+'</a>'
                    +'                      </div>'
                    +'                      <div class="col-md-12">'
                    +'                          <input id="up_input-upload" name="up_input-upload" type="file" class="file-loading" />'
                    +'                      </div>'
                    +'                  </div>'
                    +'            </div>'
                    +'          </div>'
                    +'          <div class="modal-footer">'
                    + '              <button type="button" class="btn btn-primary model-upload"><span class="glyphicon glyphicon-ok"></span>' + (this.options.langID == 1 ? "Upload" : "上传") + '</button>'
                    + '              <button type="button" class="btn btn-default" data-dismiss="modal"><span class="glyphicon glyphicon-remove"></span>' + (this.options.langID == 1 ? "Close" : "关闭") + '</button>'
                    +'          </div>'
                    +'  </div><!-- /.modal-content -->'
                    +'</div><!-- /.modal-dialog -->'
    }
    obj_import.prototype = {
        init:function(){
            var e = this.$element;
            var options = this.options
            var modal = this.modal;
            e.click(function () {
                $('#' + options.dialog).empty();
                $('#' + options.dialog).append(modal);
                $('#' + options.dialog).modal('show');
                $('#' + options.dialog + ' #up_input-upload').fileinput({
                    uploadUrl: options.saveUrl ,
                    showUpload: false, //是否显示上传按钮
                    showCaption: true, //是否显示标题
                    browseClass: "btn btn-primary", //按钮样式   
                    showPreview: false
                })
                $('#' + options.dialog).find('.model-upload').click(function () {
                    $('#' + options.dialog + ' #up_input-upload').fileinput('upload');
                });
                $('#' + options.dialog + ' #up_input-upload').on("fileuploaded", function (event, data, previewId, index) {
                    if (data.response.IsSuccess) {
                        if (options.callback != "") {
                            //eval(options.callback);
                            options.callback();
                            //$("#tab_data").DataTable().ajax.reload();
                        }
                        $('#' + options.dialog).modal('hide');
                        $.globalMessenger().post({
                            message: "操作成功",//提示信息
                            type: 'info',//消息类型。error、info、success
                            hideAfter: 2,//多长时间消失
                            showCloseButton: true,//是否显示关闭按钮
                            hideOnNavigate: true //是否隐藏导航
                        });
                    }
                    else {
                        $('#' + options.dialog).modal('hide');
                        bootbox.alert({ message: data.response.Msg, title: (options.LangID==1?"Error":"错误") });
                    }
                })
            })
        }
    }

    $.fn.x_import = function (option) {
        var langID = $('body').attr("LangID");
        option.LangID = (typeof (langID) == "undefined" || langID == null) ? 1 : langID;
        var obj = new obj_import(this, option);
        obj.init();
    }
})(jQuery, window, document);