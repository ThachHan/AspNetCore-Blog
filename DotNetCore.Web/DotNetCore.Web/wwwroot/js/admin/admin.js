$(document).ready(function () {
    $(".collapse-link").on("click", function () {
        var e = $(this).closest(".x_panel")
            , t = $(this).find("i")
            , n = e.find(".x_content");
        e.attr("style") ? n.slideToggle(200, function () {
            e.removeAttr("style")
        }) : (n.slideToggle(200),
            e.css("height", "auto")),
            t.toggleClass("fa-chevron-up fa-chevron-down")
    }),
        $(".close-link").click(function () {
            var e = $(this).closest(".x_panel");
            e.remove()
        })
}),
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip({
            container: "body"
        })
    }),
    $(".progress .progress-bar")[0] && $(".progress .progress-bar").progressbar(),
    $(document).ready(function () {
        if ($(".js-switch")[0]) {
            var e = Array.prototype.slice.call(document.querySelectorAll(".js-switch"));
            e.forEach(function (e) {
                new Switchery(e, {
                    color: "#26B99A"
                })
            })
        }
    }),
    $(document).ready(function () {
        $("input.flat")[0] && $(document).ready(function () {
            $("input.flat").iCheck({
                checkboxClass: "icheckbox_flat-green",
                radioClass: "iradio_flat-green"
            })
        })
    }),
    $("table input").on("ifChecked", function () {
        checkState = "",
            $(this).parent().parent().parent().addClass("selected"),
            countChecked()
    }),
    $("table input").on("ifUnchecked", function () {
        checkState = "",
            $(this).parent().parent().parent().removeClass("selected"),
            countChecked()
    });
var checkState = "";
$(".bulk_action input").on("ifChecked", function () {
    checkState = "",
        $(this).parent().parent().parent().addClass("selected"),
        countChecked()
}),
    $(".bulk_action input").on("ifUnchecked", function () {
        checkState = "",
            $(this).parent().parent().parent().removeClass("selected"),
            countChecked()
    }),
    $(".bulk_action input#check-all").on("ifChecked", function () {
        checkState = "all",
            countChecked()
    }),
    $(".bulk_action input#check-all").on("ifUnchecked", function () {
        checkState = "none",
            countChecked()
    }),
    $(document).ready(function () {
        $(".expand").on("click", function () {
            $(this).next().slideToggle(200),
                $expand = $(this).find(">:first-child"),
                "+" == $expand.text() ? $expand.text("-") : $expand.text("+")
        })
    }),
    "undefined" != typeof NProgress && ($(document).ready(function () {
        NProgress.start()
    }),
        $(window).load(function () {
            NProgress.done()
        })),
    function (e, t) {
        var n = function (e, t, n) {
            var i;
            return function () {
                function c() {
                    n || e.apply(a, o),
                        i = null
                }
                var a = this
                    , o = arguments;
                i ? clearTimeout(i) : n && e.apply(a, o),
                    i = setTimeout(c, t || 100)
            }
        }
            ;
        jQuery.fn[t] = function (e) {
            return e ? this.bind("resize", n(e)) : this.trigger(t)
        }
    }(jQuery, "smartresize");
function InitSelect2(values, selector, url, language) {
    var pageSize = 20;
    $(selector).select2({
        ajax: {
            url: url,
            type: "GET",
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    username: params.term,
                    // search term
                    pageSize: pageSize,
                    pageNum: params.page
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;
                var more = (params.page * pageSize) < data.Total;
                return {
                    results: data.Results,
                    more: more
                };
            },
            cache: true
        },
        minimumInputLength: 1,
        language: language
    });
    $.each($.parseJSON(values), function () {
        $(selector).append('<option value="' + this.id + '" selected="selected">' + this.text + '</option>');
    })
};
function ShowAlertDialog(message) {
    $("#alertMessage").text(message);
    $('#alertModal').modal('show');
}

//full screen
function toggleFullScreen(elem) {
    // ## The below if statement seems to work better ## if ((document.fullScreenElement && document.fullScreenElement !== null) || (document.msfullscreenElement && document.msfullscreenElement !== null) || (!document.mozFullScreen && !document.webkitIsFullScreen)) {
    if ((document.fullScreenElement !== undefined && document.fullScreenElement === null) || (document.msFullscreenElement !== undefined && document.msFullscreenElement === null) || (document.mozFullScreen !== undefined && !document.mozFullScreen) || (document.webkitIsFullScreen !== undefined && !document.webkitIsFullScreen)) {
        if (elem.requestFullScreen) {
            elem.requestFullScreen();
        } else if (elem.mozRequestFullScreen) {
            elem.mozRequestFullScreen();
        } else if (elem.webkitRequestFullScreen) {
            elem.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
        } else if (elem.msRequestFullscreen) {
            elem.msRequestFullscreen();
        }
    } else {
        if (document.cancelFullScreen) {
            document.cancelFullScreen();
        } else if (document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if (document.webkitCancelFullScreen) {
            document.webkitCancelFullScreen();
        } else if (document.msExitFullscreen) {
            document.msExitFullscreen();
        }
    }
}

window.ElfInput = {
    elfNode: undefined, elfInsrance: '', dialogName: '', elfUrl: '', elfLang: '', currentId: '', elfDirHashMap: { // Dialog name / elFinder holder hash Map
        image: '', // example for 1st LocalFileVolume "/images"
        flash: '',
        files: '',
        link: ''
    },
}

function initCkEl(url, lang) {
    var elfNode, elfInsrance, dialogName,
        elfUrl = url, // Your connector's URL
        elfDirHashMap = { // Dialog name / elFinder holder hash Map
            image: '', // example for 1st LocalFileVolume "/images"
            flash: '',
            files: '',
            link: ''
        };
    var getLang = function () {
        return lang;
    }

    CKEDITOR.on('dialogDefinition', function (event) { // connection manager
        var editor = event.editor;
        var dialogDefinition = event.data.definition;
        var tabCount = dialogDefinition.contents.length;
        for (var i = 0; i < tabCount; i++) { // cycle to replace the click of button "View on the server"
            var content = dialogDefinition.contents[i];
            if (content != null) {
                var browseButton = content.get('browse');

                if (browseButton !== null) {
                    browseButton.hidden = false;
                    browseButton.onClick = function (dialog, i) {

                        dialogName = CKEDITOR.dialog.getCurrent()._.name;
                        if (elfNode) {
                            if (elfDirHashMap[dialogName] && elfDirHashMap[dialogName] != elfInsrance.cwd().hash) {
                                elfInsrance.request({
                                    data: { cmd: 'open', target: elfDirHashMap[dialogName] },
                                    notify: { type: 'open', cnt: 1, hideCnt: true },
                                    syncOnFail: true
                                });
                            }
                            elfNode.dialog('open');
                        } else {
                            elfNode = $('<div \>');
                            elfNode.dialog({
                                modal: true,
                                width: '80%',
                                title: 'Server File Manager',
                                create: function (event, ui) {
                                    var startPathHash = (elfDirHashMap[dialogName] && elfDirHashMap[dialogName]) ? elfDirHashMap[dialogName] : '';
                                    // elFinder configure
                                    elfInsrance = $(this).elfinder({
                                        startPathHash: startPathHash,
                                        useBrowserHistory: false,
                                        resizable: false,
                                        width: '100%',
                                        url: elfUrl,
                                        lang: getLang(),
                                        getFileCallback: function (file) {
                                            var url = file;
                                            var dialog = CKEDITOR.dialog.getCurrent();
                                            if (dialogName == 'image') {
                                                var urlObj = 'txtUrl'
                                            } else if (dialogName == 'flash') {
                                                var urlObj = 'src'
                                            } else if (dialogName == 'files' || dialogName == 'link') {
                                                var urlObj = 'url'
                                            } else {
                                                return;
                                            }
                                            dialog.setValueOf(dialog._.currentTabId, urlObj, url);
                                            elfNode.dialog('close');
                                            elfInsrance.disable();
                                        }
                                    }).elfinder('instance');
                                },
                                open: function () {
                                    elfNode.find('div.elfinder-toolbar input').blur();
                                    setTimeout(function () {
                                        elfInsrance.enable();
                                    }, 100);
                                },
                                resizeStop: function () {
                                    elfNode.trigger('resize');
                                }
                            }).parent().css({ 'zIndex': '11000' });
                        }

                    }
                }
            }
        }
    });
};

function InitElf(url, lang) {
    ElfInput.elfUrl = url;
    ElfInput.elfLang = lang;

};

function OpenElf(id, url) {
    ElfInput.currentId = id;
    if (url != undefined) {
        ElfInput.elfUrl = url;
    }
    if (ElfInput.elfNode == undefined) {
        ElfInput.elfNode = $('<div \>');
        ElfInput.elfNode.dialog({
            modal: true,
            width: '80%',
            title: 'Server File Manager',
            create: function (event, ui) {
                var startPathHash = (ElfInput.elfDirHashMap[ElfInput.dialogName] && ElfInput.elfDirHashMap[ElfInput.dialogName]) ? ElfInput.elfDirHashMap[ElfInput.dialogName] : '';
                // elFinder configure
                ElfInput.elfInsrance = $(this).elfinder({
                    startPathHash: startPathHash,
                    useBrowserHistory: false,
                    resizable: false,
                    width: '100%',
                    url: ElfInput.elfUrl,
                    lang: ElfInput.elfLang,
                    getFileCallback: function (file) {
                        var url = file;
                        $(ElfInput.currentId).val(url).change();
                        ElfInput.elfNode.dialog('close');
                        ElfInput.elfInsrance.disable();
                    }
                }).elfinder('instance');
            },
            open: function () {
                ElfInput.elfNode.find('div.elfinder-toolbar input').blur();
                setTimeout(function () {
                    ElfInput.elfInsrance.enable();
                }, 100);
            },
            resizeStop: function () {
                ElfInput.elfNode.trigger('resize');
            }
        }).parent().css({ 'zIndex': '11000' });
    }
    else {
        if (ElfInput.elfDirHashMap[ElfInput.dialogName] && ElfInput.elfDirHashMap[ElfInput.dialogName] != ElfInput.elfInsrance.cwd().hash) {
            ElfInput.elfInsrance.request({
                data: { cmd: 'open', target: ElfInput.elfDirHashMap[dialogName] },
                notify: { type: 'open', cnt: 1, hideCnt: true },
                syncOnFail: true
            });
        }
        ElfInput.elfNode.dialog('open');
    }
};

function ReplaceCKEditor(selector) {
    if (typeof selector === 'undefined') {
        $('.ck_editor').each(function () {
            CKEDITOR.replace($(this).attr('id'), { filebrowserBrowseUrl: '#' });
        });
    }
    else {
        var ckSelector = selector + ' .ck_editor';
        $(ckSelector).each(function () {
            CKEDITOR.replace($(this).attr('id'), { filebrowserBrowseUrl: '#' });
        });
    }

};

function ReplaceSelect2(lang, selector) {
    if (typeof selector === 'undefined') {
        $('.ddl_select2').select2({
            theme: "bootstrap",
            language: lang
        });
    }
    else {
        var select2Selector = selector + ' .ddl_select2';
        $(select2Selector).select2({
            theme: "bootstrap",
            language: lang
        });
    }

};


function InitAjaxForm(selector, lang) {
    var form = $(selector)
        .removeData("validator") /* added by the raw jquery.validate plugin */
        .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin */

    $.validator.unobtrusive.parse(form);

    ReplaceCKEditor(selector);
    if (typeof lang !== 'undefined') {
        ReplaceSelect2(lang, selector);
    }
};

function ShowPopup(selector, lang) {
    InitAjaxForm(selector, lang);
    $(selector).modal();
}

function HideModal(selector) {
    $(selector).modal('hide');
}

function ChangeMediaType(url, selector, lang) {
    var form = $(selector);
    for (instance in CKEDITOR.instances) {
        CKEDITOR.instances[instance].updateElement();
    }
    $.ajax({
        url: url,
        data: form.serialize(),
        type: 'GET',

        success: function (data) {
            $('#divModalMedia').html(data);
            InitAjaxForm(selector, lang);
        },
    });
}

function CloneTableRow(tableSelector) {
    var rowCount = $(tableSelector + " > tbody > tr").length;
    var idPlaceHolder = '_0_';
    var namePlaceHolder = '[0]';
    var newId = "_" + rowCount + "_";
    var newName = "[" + rowCount + "]";
    var childTableExist = $(tableSelector + " table").length > 0;
    var hasParentTable = $(tableSelector).parents('table').length > 0;

    var row = $(tableSelector + " > tbody > tr:first").clone().show().find("input,table,div").each(function () {
        $(this).attr({
            'id': function (_, id) {
                return ChangeRowAttribute(id, idPlaceHolder, newId, hasParentTable);
            },
            'name': function (_, name) {
                return ChangeRowAttribute(name, namePlaceHolder, newName, hasParentTable);
            },
            'onchange': function (_, onChange) {
                return ChangeRowAttribute(onChange, idPlaceHolder, newId, hasParentTable);
            },
        });
    }).val('').end();

    row = row.find("button,span").each(function () {
        $(this).attr({
            'onClick': function (_, onClick) {
                return ChangeRowAttribute(onClick, idPlaceHolder, newId, hasParentTable);
            },
            'data-target': function (_, dataTarget) {
                return ChangeRowAttribute(dataTarget, idPlaceHolder, newId, hasParentTable);
            },
            'data-valmsg-for': function (_, dataValmsgFor) {
                return ChangeRowAttribute(dataValmsgFor, namePlaceHolder, newName, hasParentTable);
            },
            'for': function (_, forAttr) {
                return ChangeRowAttribute(forAttr, idPlaceHolder, newId, hasParentTable);
            },
        });
    }).end();

    row = row.find("tbody tr:gt(0)").remove().end().appendTo(tableSelector);

    InitAjaxForm(tableSelector + '  > tbody > tr:eq(' + rowCount + ')');
}

function ChangeRowAttribute(currentValue, placeHolder, newReplaceValue, hasParentTable) {
    if (typeof currentValue != 'undefined') {
        var n = currentValue.lastIndexOf(placeHolder);
        if (n != -1) {
            if (hasParentTable) {
                return currentValue.slice(0, n) + currentValue.slice(n).replace(placeHolder, newReplaceValue);
            }
            return currentValue.replace(placeHolder, newReplaceValue);
        }
        return currentValue;
    }
    return '';
}

function DeleteTableRow(_this) {
    var row = $(_this).closest('tr');
    row.hide();
    row.find("input").each(function () {
        $(this).attr({
            'value': ''
        });
    });
}

function SetFileName(_this, inputNameSelector) {
    var path = decodeURI($(_this).val());
    $(_this).val(path);
    var fileName = path.substr(path.lastIndexOf("/") + 1);
    $(inputNameSelector).val(fileName)
}
$(document).ready(function () {

    $(document).on('click', '#close-preview', function () {
        $('.image-preview').popover('hide');
        // Hover befor close the preview
        $('.image-preview').hover(
            function () {
                $('.image-preview').popover('show');
            },
            function () {
                $('.image-preview').popover('hide');
            }
        );
    });

    $(function () {
        // Create the close button
        var closebtn = $('<button/>', {
            type: "button",
            text: 'x',
            id: 'close-preview',
            style: 'font-size: initial;',
        });
        closebtn.attr("class", "close pull-right");
        // Set the popover default content
        $('.image-preview').popover({
            trigger: 'manual',
            html: true,
            title: "<strong>Preview</strong>" + $(closebtn)[0].outerHTML,
            content: "There's no image",
            placement: 'bottom'
        });
        // Clear event
        $('.image-preview-clear').click(function () {
            $('.image-preview').attr("data-content", "").popover('hide');
            $('.image-preview-filename').val("");
            $('.image-preview-clear').hide();
            $('.image-preview-input input:file').val("");
            $(".image-preview-input-title").text("Browse");
        });
        // Create the preview image
        $(".image-preview-input input:file").change(function () {
            var img = $('<img/>', {
                id: 'dynamic',
                width: 250,
                height: 200
            });
            var file = this.files[0];
            var reader = new FileReader();
            // Set preview image into the popover data-content
            reader.onload = function (e) {
                $(".image-preview-input-title").text("Change");
                $(".image-preview-clear").show();
                $(".image-preview-filename").val(file.name);
                img.attr('src', e.target.result);
                $(".image-preview").attr("data-content", $(img)[0].outerHTML).popover("show");
            }
            reader.readAsDataURL(file);
        });
    });

    var triggerUpload = function () {
        var fileUpload = $("#form_upload_file").get(0);
        var files = fileUpload.files;

        // Create FormData object  
        var fileData = new FormData();

        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }
        $.ajax({
            url: '/File/UploadFiles',
            type: "POST",
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            data: fileData,
            success: function (result) {
                alert(result);
            },
            error: function (err) {
                alert(err.statusText);
            }
        });
    }

    $('#btnUpload').click(function (e) {
        e.preventDefault();
        triggerUpload();
    });
});

$(document).ready(function () {
    $('.btn-default.btn-reject-comment').on('click', function (event) {
        var id = $(this).data('commentid');
        event.preventDefault();
        $.ajax({
            url: '/Admin/Comment/RejectAsync',
            type: "POST",
            data: { 'id': id },
            success: function (result) {
                if (result.result) {
                    window.location.href = "/Admin/Comment/LoadCommentAsync/" + result.contentId;
                }
            },
            error: function (err) {
                alert(err.statusText);
            }
        });

    });

});


$(document).ready(function () {
    $('.btn-default.btn-approve-comment').on('click', function (event) {
        var id = $(this).data('commentid');
        event.preventDefault();
        $.ajax({
            url: '/Admin/Comment/ApproveAsync',
            type: "POST",
            data: { 'id': id },
            success: function (result) {
                if (result.result) {
                    window.location.href = "/Admin/Comment/LoadCommentAsync/" + result.contentId;
                }
            },
            error: function (err) {
                alert(err.statusText);
            }
        });

    });

});

$(document).ready(function () {
    $('.btn-default.btn-reject').on('click', function (event) {
        var id = $(this).data('contentid');
        event.preventDefault();
        $.ajax({
            url: '/Admin/Content/RejectAsync',
            type: "POST",
            data: { 'id': id },
            success: function (result) {
                window.location.href = "/Admin/Content/WritedContents";
            },
            error: function (err) {
                alert(err.statusText);
            }
        });

    });

});

$(document).ready(function () {
    $('.btn-default.btn-approve').on('click', function (event) {
        var id = $(this).data('contentid');
        event.preventDefault();
        $.ajax({
            url: '/Admin/Content/ApproveAsync',
            type: "POST",
            data: { "id": id },
            success: function (result) {
                window.location.reload();
            },
            error: function (err) {
                alert(err.statusText);
            }
        });

    });

});