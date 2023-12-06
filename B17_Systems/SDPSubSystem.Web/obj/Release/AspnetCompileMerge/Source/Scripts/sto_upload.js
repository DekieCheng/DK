;(function ($, window, document, undefined) {
    var obj_upload = function (ele,opt) {
        this.$element = ele;
        this.defaltus = {
            dialog: '_UploadDialog',
            saveUrl: '/STOStandardTemplate/SaveUpload',
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
    obj_upload.prototype = {
        init: function () {
            var e = this.$element;
            if (e.attr("isInit") == "0") {
                e.attr("isInit", "1");
                if (e.attr('fileUrl') == "") {
                    this.button();
                }
                else {
                    this.link();
                }
            }
        },
        link: function () {
            var t = this;
            var e = this.$element;
            e.empty();
            var options = this.options
            var a = $('<a target="_blank" href="' + e.attr('fileUrl') + '"><span class="glyphicon glyphicon-file"></span></a><span class="glyphicon glyphicon-remove" style="color:red;cursor:pointer"></span>');
            a.next('span').click(function () {
                e.attr('fileUrl', '');
                e.attr('fileName', '');
                t.button();
                //bootbox.confirm({
                //    message: 'Are you sure you want to remove the attachment?', title: 'Confirm',
                //    buttons: {
                //        confirm: {
                //            label: 'Remove',
                //            className: 'btn-danger'
                //        },
                //        cancel: {
                //            label: 'Cancel',
                //            className: 'btn-default'
                //        }
                //    },
                //    callback: function (result) {
                //        if (result) {
                //            //$.post(options.deleteUrl,{fileName:e.attr('fileUrl')})
                //            e.attr('fileUrl', '');
                //            e.attr('fileName', '');
                //            t.button();
                //        }
                //    }
                //});
            });
            e.append(a);
        },
        button: function () {
            var t = this;
            var e = this.$element;
            e.empty();
            var b = $('<button class="btn btn-primary btn-sm"><span class="glyphicon glyphicon-upload"></span>' + (this.options.langID == 1 ? "Upload File" : "上传文件") + '</button>');
            var options = this.options
            var modal = this.modal;
            b.click(function () {
                $('#' + options.dialog).empty();
                $('#' + options.dialog).append(modal);
                $('#' + options.dialog).modal('show');
                $('#' + options.dialog + ' #up_input-upload').fileinput({
                    uploadUrl: options.saveUrl + "?NPI=" + options.NPI,
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
                        e.attr('fileUrl', data.response.Data.path);
                        e.attr('fileName', data.response.Data.name);
                        t.link();
                        $('#' + options.dialog).modal('hide');
                    }
                })
            });
            e.append(b);
        }
    }

    $.fn.sto_upload = function (option) {
        var langID = $('body').attr("LangID");
        if (typeof (option) == 'undefined') {
            option = { LangID: 1 };
        }
        option.LangID = (typeof (langID) == "undefined" || langID == null) ? 1 : langID;
        var obj = new obj_upload(this, option);
        obj.init();
    }
})(jQuery, window, document);