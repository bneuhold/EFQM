/// <reference path="../gasper/jqueryui-theme/js/jquery-1.5.1.min.js" />
/// <reference path="../gasper/jqueryui-theme/js/jquery-ui-1.8.13.custom.min.js" />
/// <reference path="extender.js" />
/// <reference path="api-generated.js" />

function MyHtmlDecode2(input) {
    return input;
};

function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}

Object.keys = Object.keys || (function () {
    var hasOwnProperty = Object.prototype.hasOwnProperty,
        hasDontEnumBug = !{ toString: null}.propertyIsEnumerable("toString"),
        DontEnums = [
            'toString', 'toLocaleString', 'valueOf', 'hasOwnProperty',
            'isPrototypeOf', 'propertyIsEnumerable', 'constructor'
        ],
        DontEnumsLength = DontEnums.length;

    return function (o) {
        if (typeof o != "object" && typeof o != "function" || o === null)
            throw new TypeError("Object.keys called on a non-object");

        var result = [];
        for (var name in o) {
            if (hasOwnProperty.call(o, name))
                result.push(name);
        }

        if (hasDontEnumBug) {
            for (var i = 0; i < DontEnumsLength; i++) {
                if (hasOwnProperty.call(o, DontEnums[i]))
                    result.push(DontEnums[i]);
            }
        }

        return result;
    };
})();

Object.isDefined = function (input) {
    if ((input == null) || (input === undefined)) return false;
    else return true;
};

var API = {};

API.Monitor = {
    _save: false,
    _load: false,
    lockSave: function () {
        if (API.Monitor._save) {
            API.Console.log("saving locked!");
            return false;
        }
        API.Monitor._save = true;
        return API.Monitor._save;
    },
    unlockSave: function () {
        API.Monitor._save = false;
    },
    lockLoad: function () {
        if (API.Monitor._load) return false;
        API.Monitor._load = true;
        return API.Monitor._load;
    },
    unlockLoad: function () {
        API.Monitor._load = false;
    }
};

API.Settings = {};
API.Actions = {};

API.Console = {
    log: function (text) {
        if (API.Settings.isTestEnvironment && window.console && Object.isDefined(console) && Object.isDefined(console.log)) {
            console.log(text);
        }
    },
    error: function (text) {
        if (API.Settings.isTestEnvironment && window.console && Object.isDefined(console) && Object.isDefined(console.error)) {
            console.error(text);
        } else {
            API.Console.log(text);
        }
    }
};

API.Communication = {
    url: "http://localhost/WebsiteAPI/",
    signature: null,
    basic: null
};

API.Handlers = {};

API.Handlers.page = {
    initComplete: false,
    init: function () { },
    isBackForwardOrInit: true,
    filterToHash: function (param) {
        var hash = "";
        for (var key in param) {
            if (param.hasOwnProperty(key)) {
                if (key != "signature")
                    hash += key + "=" + param[key] + "&";
            }
        }
        if (hash.length > 0) hash = hash.substring(0, hash.length - 1);
        return hash;
    },
    hashToFilter: function (hash) {
        if (!Object.isDefined(hash) || (hash == "")) return {};
        var result = {};
        var params = hash.split('&');
        for (var i = 0; i < params.length; i++) {
            var keyval = params[i].split("=");
            result[keyval[0]] = keyval[1];
        }
        return result;
    },
    setupFilter: function (param) {
        for (var key in param) {
            if (param.hasOwnProperty(key)) {
                $("#" + key).val(param[key]);
            }
        }
    }
}

API.Handlers.filter = {
    submitFunction: null,
    afterSubmit: null,
    timer: null
}

API.Handlers.paging = {
    listFunction: null
}

API.Handlers.cache = {
    filter: null,
    post: null
};

API.Handlers.actions = {
    cancel: null,
    save: null,
    reload: null,
    unload: null,
    details: null,
    card: null
};

API.Ajax = {
    GetJson: function (url, param, context, beforeSend, successHandler, errorHandler, completeHandler) {
        API.Ajax._ajaxCall({ url: url, type: "GET", dataType: "json" }, param, context, beforeSend, successHandler, errorHandler, completeHandler);
    },
    PostJson: function (url, param, context, beforeSend, successHandler, errorHandler, completeHandler) {
        API.Ajax._ajaxCall({ url: url, type: "POST", dataType: "json" }, param, context, beforeSend, successHandler, errorHandler, completeHandler);
    },
    GetHtml: function (url, param, context, beforeSend, successHandler, errorHandler, completeHandler) {
        API.Ajax._ajaxCall({ url: url, type: "GET", dataType: "html" }, param, context, beforeSend, successHandler, errorHandler, completeHandler);
    },
    PostHtml: function (url, param, context, beforeSend, successHandler, errorHandler, completeHandler) {
        API.Ajax._ajaxCall({ url: url, type: "POST", dataType: "html" }, param, context, beforeSend, successHandler, errorHandler, completeHandler);
    },
    _ajaxCall: function (call, param, context, beforeSend, successHandler, errorHandler, completeHandler) {
        var contentType = "application/x-www-form-urlencoded";
        if (context.useJsonPost) {
            //contentType = "application/json; charset=utf-8;";
            //param = param.postJsonData;
        }
        if (!Object.isDefined(param.signature)) {
            param.signature = API.Communication.signature;
        }
        if (!Object.isDefined(context)) {
            context = {};
        }
        var showLoader = true;
        if (context.ignoreLoader) {
            showLoader = false;
        }

        param.__RequestVerificationToken = $("input[name='__RequestVerificationToken']").val();
        $.ajax({
            type: call.type,
            url: call.url,
            data: param,
            dataType: call.dataType,
            contentType: contentType,
            timeout: (API.Settings.ajaxTimeout * 1000),
            beforeSend: function (jqXHR) { jqXHR.setRequestHeader("Authorization", "Basic " + API.Communication.basic); if (showLoader) API.Design.loader.start(); if (beforeSend) beforeSend(); if (context.before) context.before(); },
            success: function (response, textStatus, jqXHR) {
                if (call.dataType == "json") {
                    if (response.Status == 0) {
                        if (successHandler) successHandler(context, response, textStatus, jqXHR);
                    } else if (response.Status == 403) {
                        //need to login
                        if (showLoader) API.Design.loader.stop();
                        window.location = API.Communication.url;
                    } else if (response.Status == 1) {
                        API.Functions.displayError(textStatus);
                        if (errorHandler) errorHandler(context, jqXHR, textStatus, response);
                    } else if (response.Status == -1) {
                        API.Ajax._ajaxError(context, jqXHR, response.Message + "<br/>" + response.StackTrace);
                        if (errorHandler) errorHandler(context, jqXHR, textStatus, response);
                    } else if (response.Status == 104) {
                        API.Functions.displayError(textStatus);
                        if (errorHandler) errorHandler(context, jqXHR, textStatus, response);
                    } else if (response.Status == 110) {
                        API.Functions.displayError(textStatus);
                        if (errorHandler) errorHandler(context, jqXHR, textStatus, response);
                    } else if (response.Status == 2) {
                        if (errorHandler) errorHandler(context, jqXHR, textStatus, response);
                    }
                } else {
                    if ((call.dataType == "html") && (jqXHR.responseText.indexOf("<unauthorized></unauthorized>") > 0)) {
                        window.location = API.Communication.url + "account/index";
                        return;
                    }
                    if (successHandler) successHandler(context, response, textStatus, jqXHR);
                }
                if (context.success) context.success();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (textStatus == "timeout") {
                    if (showLoader) API.Design.loader.stop();
                    API.Design.fadeMessage.open(API.Resources.AjaxTimeoutMessage, 3);
                } else {
                    if (jqXHR.responseText.indexOf("<unauthorized></unauthorized>") > 0) {
                        window.location = API.Communication.url + "account/logon";
                        return;
                    }
                    if (jqXHR.status == "403") {
                        //do something for authentification
                    }
                    API.Ajax._ajaxError(context, jqXHR, textStatus);
                    if (errorHandler) errorHandler(context, jqXHR, textStatus);
                }
            },
            complete: function () {
                if (showLoader) API.Design.loader.stop();
                if (completeHandler) completeHandler();
                if (context.after) context.after();
            }
        });
    },
    _ajaxError: function (context, jqXHR, textStatus, handler) {
        API.Functions.displayError(textStatus);
        if (handler) handler(context, jqXHR, textStatus);
    }
};

API.Entities = {};


API.Functions = {};
API.Functions.isEventTrigger = function (e, testedTriger) {
    var toCompare;
    if (Object.isDefined(e.target)) toCompare = e.target;
    else if (Object.isDefined(e.srcElement)) toCompare = e.srcElement;

    if ($(testedTriger).get(0) == toCompare) {
        return true;
    } else {
        return false;
    }
};
API.Functions.validateEMail = function (sEmail) {
    var emailRegex = new RegExp(/^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$/i);
    var valid = emailRegex.test(sEmail);
    if (!valid) {
        return false;
    } else
        return true;
};

API.Functions.displayError = function (text) {
    var bar = $("#errorStatus");
    if (bar.length > 0) {
        $(".message", bar).html(text);
        bar.show();
    }
};

API.Functions.displayInfo = function (text) {
    var bar = $("#infoStatus");
    if (bar.length > 0) {
        $(".message", bar).html(text);
        bar.show();
    }
};

API.Functions.leadingZeros = function (num, size) {
    var s = "000000000" + num;
    return s.substr(s.length - size);
}

API.Functions.displayTrialExpired = function () {
    var bar = $("#trialOverStatus");
    if (bar.length > 0) {
        bar.show();
    }
};

API.Functions.displayInTrial = function () {
    var bar = $("#trialStatus");
    if (bar.length > 0) {
        bar.show();
    }
};

API.Functions.redirect = function (url) {
    window.location = url;
};

API.Functions.redirectWithPost = function (url, params, target) {
    var form = $("<form>").attr("action", url).attr("method", "POST").addClass("hidden");
    for (var key in params) {
        form.append($("<input>").attr("name", key).val(params[key]));
    }
    if (Object.isDefined(target))
        form.attr("target", target);
    form.appendTo("body").submit();
};

API.Functions.delayCall = function (handler, timeInSeconds) {
    setTimeout(handler, timeInSeconds * 1000);
};

API.Functions.fixJqueryDateFormat = function (uiCulture) {
    if (API.Settings.jqueryDateFormat == null) return;
    if (Object.isDefined($.datepicker.regional[uiCulture])) {
        if ($.datepicker.regional[uiCulture].dateFormat != API.Settings.jqueryDateFormat)
            $.datepicker.regional[uiCulture].dateFormat = API.Settings.jqueryDateFormat;
    }
};

API.Functions.markRequired = function (ctx) {
    var list;
    if (ctx) {
        list = $("input.required, textarea.required, select.required", ctx);
    } else {
        list = $("input.required, textarea.required, select.required");
    }
    $(list).each(function () {
        var p = $(this).parent();
        API.Functions.processRequiredField($(this), p);
        $(this).blur(function (e) { API.Functions.processRequiredField($(this), p); });
    });
};

API.Functions.requiredOnBlur = function (ctx, bHandler) {
    $(".required", ctx).blur(function () {
        var p = $(this).parent();
        if (($(this).val() != null) && ($(this).val() != "")) {
            if ($(p).hasClass("required-parent")) $(p).removeClass("required-parent");
            if ($(this).hasClass("warning")) $(this).removeClass("warning");
        } else {
            if (!$(p).hasClass("required-parent")) $(p).addClass("required-parent");
            if (!$(this).hasClass("warning")) $(this).addClass("warning");
        }
        if (bHandler) bHandler();
    });
};

API.Functions.getRequiredFailList = function (ctx) {
    var list;
    if (ctx) {
        list = $("input.required, textarea.required, select.required", ctx);
    } else {
        list = $("input.required, textarea.required, select.required");
    }
    return list;
};

API.Functions.checkRequired = function (ctx) {
    var failCount = 0;
    $(API.Functions.getRequiredFailList(ctx)).each(function () {
        if (($(this).val() == null) || ($(this).val() == "")) {
            failCount++;
        }
    });
    if (failCount > 0) {
        return false;
    } else {
        return true;
    }
};

API.Functions.checkRequiredBeforeSave = function () {
    $(".save").click(function (e) {
        if (!API.Functions.checkRequired()) {
            e.preventDefault();
            return false;
        } else {
            return true;
        }
    });
};

API.Functions.getFilter = function (filter) {
    var result = {};
    var elements;
    if (filter) {
        elements = $(".filter-data", filter);
    } else {
        elements = $("#filter .filter-data,.filter .filter-data");
    }
    $(elements).each(function () {
        if ($(this).val() != "") {
            result[$(this).attr("id")] = $(this).val();
        }
    });
    return result;
};

API.Functions.setFilter = function (d) {
    var elements = $("#filter .filter-data");
    $(elements).each(function () {
        if (Object.isDefined(d[key])) {
            $(this).val(d[key]);
        }
    });
};

API.Functions.gatherData = function (ctx) {
    var result = {};
    $("input, select, textarea", ctx).each(function () {
        if ($(this).is(":checkbox")) {
            if ($(this).is(":checked"))

                result[$(this).attr("id")] = true;
            else
                result[$(this).attr("id")] = false;
        } else {
            result[$(this).attr("id")] = $(this).val();
        }
    });
    result.signature = API.Communication.signature;
    return result;
};

API.Functions.displaySheet = function (sheet, ctx) {
    if ((ctx) && (ctx.container)) {
        $(".display-sheet", ctx.container).hide();
    } else
        $(".display-sheet").hide();
    var direction = "right";
    if ((ctx) && (ctx.ShowAnimation)) {
        if ($(sheet).attr("id").contains("list")) direction = "left"
        $(sheet).show("drop", { direction: direction }, 200);
    } else {
        $(sheet).show();
    }
};

API.Functions.renderEditData = function (editor, data) {
    for (var key in data) {
        if (data.hasOwnProperty(key)) {
            var input = $("#" + key + ":first", editor);
            if ($(input).length == 1) {
                if ($(input).attr("type") == "checkbox") {
                    if (data[key]) $(input).attr("checked", data[key]);
                    else $(input).removeAttr("checked");
                } else {
                    if ($(input).is("input") || $(input).is("textarea") || $(input).is("select")) {
                        $(input).val(data[key]);
                    }
                    else {
                        $(input).html(data[key]);
                    }
                }
            }
        }
    }
};

API.Functions.userAccess = function (ctx) {
    if (API.Settings.userType == "master") return;
    var save, cancel, edit, editControls;
    var masterActions;
    if (Object.isDefined(ctx)) {
        masterActions = $(".master-action", ctx.container);
        save = $(".button_save", ctx.container);
        cancel = $(".lnkCancel", ctx.container);
        edit = $(".button_edit", ctx.container);
        editControls = $(".border-edit", ctx.container);
    } else {
        masterActions = $(".master-action");
        save = $(".button_save");
        cancel = $(".lnkCancel");
        edit = $(".button_edit");
        editControls = $(".border-edit");
    }
    masterActions.addClass("disabled").addClass("hidden").attr("disabled", "disabled");
    $(".details_edit input, .details_edit select, .details_edit textarea").attr("readonly", "readonly");
    edit.addClass("view_only").val(API.Actions.ShowDetails);
    editControls.addClass("border-readonly").removeClass("border-edit");
    cancel.html(API.Actions.HideDetails);
};

API.Functions.deleteSampleData = function (ctx) {
    API.Design.confirm.open($("#tmpl-DeleteSampleData").tmpl({}),
    function () {
        var url = API.Communication.url + "home/deletesampledata";
        API.Ajax.PostJson(url, {}, ctx, null, function (ctx, response) {
            var handler = ctx.handler;
            delete ctx.handler;
            API.Settings.hasSampleData = false;
            if (handler) handler(ctx);
        });
    },
    function () {
    },
    400);
};

API.Functions.formatNumber = function (number) {
    return $.formatNumber(number, { format: API.Settings.numberFormat, locale: API.Settings.culture });
};

API.Functions.formatDateFromString = function (stringDate, f) {
    if (f == "d") {
        return Globalize.format(new Date(stringDate.split("T")[0]), f);
    } else if (f == "t") {
        var hhmmss = stringDate.split("T")[1].split(":");
        return hhmmss[0] + ":" + hhmmss[1];
    }
};

//API.Functions.formatDate = function (jsonDate, f) {
//    if (!jsonDate || jsonDate == null)
//        return "";

//    var date = new Date(parseInt(jsonDate.replace("/Date(", "").replace(")/", ""), 10));
//    var n = Globalize.format(date, f);
//    return n;
//};

API.Functions.formatDate = function (jsonDate, f) {
    if (!jsonDate || jsonDate == null)
        return "";

    var d = new Date();
    d.setYear(parseInt(jsonDate.substring(0, 4)));
    d.setMonth(parseInt(jsonDate.substring(5, 7)));
    d.setDate(parseInt(jsonDate.substring(8, 10)));
    d.setHours(parseInt(jsonDate.substring(11, 13)));
    d.setMinutes(parseInt(jsonDate.substring(14, 16)));
    d.setSeconds(parseInt(jsonDate.substring(17, 19)));

    var n = Globalize.format(d, f);

    return n;
};


API.Design = { };

API.Design.common = {
    options: {
    },
    init: function (ctx, options) {
        var o = $.extend(API.Design.common.options, options);
        $("input, select, textarea, .button, .sexy-drop-down-container", ctx).each(function () {
            if (!$(this).hasClass("rounded") && ($(this).attr("type") != "checkbox")) $(this).addClass("rounded").addClass("border");
        });

        $(".button").button();

        $("a[href='#']").click(function (e) { e.preventDefault(); });

        $(".initial-hidden").hide().removeClass("initial-hidden");
        // $(".date_picker").datepicker($.datepicker.regional[API.Settings.culture], { onClose: function (dateText) { /* * */ } });
        $(".date_picker").datepicker($.datepicker.regional[API.Settings.culture]);
    }
};

API.Design.loader = {
    options: {
        defaultText: "Please wait...",
        effect: "fade",
        duration: 5
    },
    init: function (options) {
        $.extend(API.Design.loader.options, options);
        $("#loader-box").html(API.Design.loader.options.defaultText);
        $("#loader-box").dialog({
            autoOpen: false,
            modal: false,
            show: { effect: API.Design.loader.options.effect, duration: API.Design.loader.options.duration },
            hide: { effect: API.Design.loader.options.effect, duration: API.Design.loader.options.duration },
            minHeight: 50,
            resizable: false,
            open: function (event, ui) {
                if (!$(this).parent().hasClass("no-title-bar"))
                    $(this).parent().addClass("no-title-bar");
                if ($(this).parent().hasClass("ui-corner-all"))
                    $(this).parent().removeClass("ui-corner-all");
                /*var overlay= $(".ui-widget-overlay");
                if(overlay.length > 0) overlay.addClass("dark-shadow");*/
            }
        }).removeClass("hidden");
    },
    start: function (customText) {
        if (Object.isDefined(customText)) {
            $("#loader-box").html(customText);
        }
        $("#loader-box").dialog("open");
    },
    stop: function (revertToDefaultText) {
        $("#loader-box").dialog("close");
        if (revertToDefaultText) {
            $("#loader-box").html(API.Design.loader.options.defaultText);
        }
    }
};

API.Design.validator = {
    getDefaultContext: function () {
        return {
            container: $("document"),
            selector: "required",
            validatorType: "required",
            warningClass: "warning",
            validatorMessage: null
        };
    },
    message: "Fill in mandatory fields (marked with red border)",
    validateRequired: function (ctx) {
        ctx = $.extend(API.Design.validator.getDefaultContext(), ctx);
        var result = true;
        $(ctx.selector, ctx.container).each(function () {
            if (!API.Design.validator.validator(ctx, $(this))) {
                result = false;
            }
        });
        if (Object.isDefined(ctx.validatorMessage) && result == false) {
            API.Design.fadeMessage.open(ctx.validatorMessage);
        }
        return result;
    },
    validator: function (ctx, target) {
        var validatorType = "required";
        if (target.attr("validatorType") != null && target.attr("validatorType") != "") {
            validatorType = target.attr("validatorType");
        } else if (Object.isDefined(ctx.validatorType)) {
            validatorType = ctx.validatorType;
        }
        if (target.val() == "" && validatorType == "required") {
            target.addClass(ctx.warningClass);
            return false;
        }
        //in case it's valid for all cases
        target.removeClass(ctx.warningClass);
        return true;
    }
};

/**
* API.Design.fadeMessage - fade message box for displaying ajax action result
*/
API.Design.fadeMessage = {
    duration: 2,
    init: function () {
        $("#fade-message-box").dialog({
            autoOpen: false,
            modal: false,
            show: "fade",
            hide: "fade",
            minHeight: 50,
            resizable: false,
            open: function (event, ui) {
                if (!$(this).parent().hasClass("no-title-bar"))
                    $(this).parent().addClass("no-title-bar");
                if ($(this).parent().hasClass("ui-corner-all"))
                    $(this).parent().removeClass("ui-corner-all");
                /*var overlay= $(".ui-widget-overlay");
                if(overlay.length > 0) overlay.addClass("dark-shadow");*/
            }
        }).removeClass("hidden");
    },
    open: function (message, duration, handler) {
        if (duration == null) duration = API.Design.fadeMessage.duration;
        var content = $("#fade-message-box");
        content.html(message);
        $(content).dialog("open");
        API.Functions.delayCall(
        function () {
            $(content).dialog("close");
            if (Object.isDefined(handler)) handler();
        },
        duration);
    }
};

API.Design.dialog = {
    noTitleOpen: function (event, ui) {
        var dialogContent = $(this);
        if (!$(dialogContent).parent().hasClass("no-title-bar")) {
            $(dialogContent).parent().addClass("no-title-bar").addClass("floated-close");
            $(dialogContent).before("<span class='close-icon-button'></span>");
            $(dialogContent.prev()).click(function () {
                $(dialogContent).dialog("close");
            });
        }
        if ($(dialogContent).parent().hasClass("ui-corner-all"))
            $(dialogContent).parent().removeClass("ui-corner-all");
    },

    init: function (ctx) {
        var dialogs;
        var resizable = false;
        if (Object.isDefined(ctx)) {
            dialogs = $("div.modal", ctx.container);
        } else {
            dialogs = $("div.modal");
        }

        $(dialogs).each(function () {
            var options = {
                autoOpen: false,
                modal: true,
                minHeight: 200,
                maxHeight: $(window).height() - 50,
                resizable: false,
                open: function (event, ui) {
                    if ($(event.target).height() > ($(window).height() - 50)) {
                        $(event.target).dialog("option", "height", $(window).height() - 50);
                    } else {
                        $(event.target).dialog("option", "height", "auto");
                    }
                }
            };

            var isResizable = false;
            if ($(this).attr("resizable") == "true") {
                options.resizable = true;
            }
            if ($(this).attr("useWidth") == "true") {
                options.width = $(this).width();
            }
            $(this).dialog(options).removeClass("hidden");
        });
    }
};

API.Design.grid = {
    currentPage: null,
    init: function (ctx) {
        var heads = null;
        var toggleRows = null;
        if (Object.isDefined(ctx) && Object.isDefined(ctx.container)) {
            heads = $("thead td.sortable", ctx);
            toggleRows = $("thead .rows-toggle-cell input", ctx);
        } else {
            heads = $("thead td.sortable");
            toggleRows = $("thead .rows-toggle-cell input");
        }
        heads.append("<span class='arrow'></span>");
        heads.click(function (e) {
            $(this).siblings(".sortable").removeClass("asc").removeClass("desc");
            $(".filter #sortBy").val($(this).attr("sortBy"));
            if ($(this).hasClass("asc")) {
                $(this).toggleClass("asc").toggleClass("desc");
                $(".filter #ascending").val(false);
            } else if ($(this).hasClass("desc")) {
                $(this).toggleClass("asc").toggleClass("desc");
                $(".filter #ascending").val(true);
            } else {
                $(this).addClass("desc");
                $(".filter #ascending").val(false);
            }
            var param = {};
            if (API.Design.grid.currentPage == -1) {
                param.page = API.Design.grid.currentPage;
            }
            API.Handlers.filter.submitFunction(ctx, param);
        });

        toggleRows.change(function () {
            var val = $(this).attr("checked");
            if (!Object.isDefined(val)) val = null;
            $(this).parents("table:first").find(".row-checkbox input").attr("checked", val);
        });
    },
    pager: function (ctx, list) {
        var pagerContainer = ctx.table.next();
        $(pagerContainer).html("");
        if (((list.Page == null) || (list.Page == 0)) && (!list.HasMore)) return;
        if ((list.PageSize >= list.Count)) return;
        var pager = {};
        if (list.HasMore) {
            if (list.Page == null) pager.Next = 1;
            else pager.Next = list.Page + 1;
        }
        if (list.Page != null) {
            if (list.Page > 0) pager.Previous = list.Page - 1;
            else if (list.Page < 0) pager.Previous = 0;
        } else {
            list.Page = 0;
        }
        var from = (list.Page) * list.PageSize + 1;
        var to = (list.Page + 1) * list.PageSize;
        pager.ShowingInfo = API.Resources.PagerInfo.replace("$1", from + "-" + to).replace("$2", list.Count);
        if ((list.Page == -1) || (ctx.showAllScenarion)) pager.ShowingInfo = API.Resources.PagerInfoShowingAll.replace("$2", list.Count);
        var pagingControl = $("#tmpl-paging").tmpl(pager);

        $(".pager", pagingControl).click(function (e) {
            var param = API.Functions.getFilter();
            API.Design.grid.currentPage = $(this).attr("page");
            param.page = $(this).attr("page");
            param.initiator = "pager";
            API.Handlers.paging.listFunction({}, param);
        });
        $(pagerContainer).append(pagingControl);
    }
};

API.Design.upitnik = {
    init: function (ctx) {

        $("input.input-data").blur(function (e) {
            if ($(this).val().trim() == "") {
                $(this).val("0");
            }
            var row = $(this).parents("tr:first");
            var sum = 0;
            var l = $("input.input-data", row).each(function () {
                sum += parseInt($(this).val());
            });
            var avg = Math.round(sum / l.length);
            $(".average", row).html(avg);
        }).forceInt100();

        
    },
    load: function (id) {
        API.Ajax.PostJson(API.Communication.url + "izvrsnost/load", { UpitnikId: id }, {}, null, API.Design.upitnik.loadSuccess, API.Design.upitnik.loadError);
    },
    loadSuccess: function (ctx, data) {
        $(data.Vrijednosti).each(function () {
            var d = this;
            $("#" + d.O + "-" + d.A).val(d.V);
        });
        
        $("td.average").each(function () {
            $("input.input-data:first", $(this).parent()).trigger("blur");
        });
    },
    loadError: function () {

    }

};

/**
*
*  Base64 encode / decode
*  http://www.webtoolkit.info/
*
**/

API.base64 = {
    // private property
    _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",

    // public method for encoding
    encode: function (input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;

        input = API.base64._utf8_encode(input);

        while (i < input.length) {
            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }

            output = output +
			this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) +
			this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);
        }

        return output;
    },

    // public method for decoding
    decode: function (input) {
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;

        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        while (i < input.length) {
            enc1 = this._keyStr.indexOf(input.charAt(i++));
            enc2 = this._keyStr.indexOf(input.charAt(i++));
            enc3 = this._keyStr.indexOf(input.charAt(i++));
            enc4 = this._keyStr.indexOf(input.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output = output + String.fromCharCode(chr1);

            if (enc3 != 64) {
                output = output + String.fromCharCode(chr2);
            }
            if (enc4 != 64) {
                output = output + String.fromCharCode(chr3);
            }
        }

        output = API.base64._utf8_decode(output);

        return output;
    },

    // private method for UTF-8 encoding
    _utf8_encode: function (string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";

        for (var n = 0; n < string.length; n++) {
            var c = string.charCodeAt(n);

            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }
        }

        return utftext;
    },

    // private method for UTF-8 decoding
    _utf8_decode: function (utftext) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;

        while (i < utftext.length) {
            c = utftext.charCodeAt(i);

            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            }
            else if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            }
            else {
                c2 = utftext.charCodeAt(i + 1);
                c3 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }
        }

        return string;
    }
}

API.Design.masterInit = function () {
    

    API.Design.common.init();
    API.Design.loader.init({ defaultText: API.Actions.Loading });
    API.Design.fadeMessage.init();
    
    API.Design.dialog.init();
    API.Design.grid.init();

    $(".display-sheet.hidden").removeClass("hidden").hide();
    $(".draggable-content").draggable({ handle: ".header" });
};

jQuery.fn.forceNumeric = function () {
    return this.each(function () {
        $(this).keydown(function (e) {
            var key = e.which || e.keyCode;

            if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
            // numbers
                         key >= 48 && key <= 57 ||
            // Numeric keypad
                         key >= 96 && key <= 105 ||
            // comma, period and minus, . on keypad
                        key == 190 || key == 188 || key == 109 || key == 110 ||
            // Backspace and Tab and Enter
                        key == 8 || key == 9 || key == 13 ||
            // Home and End
                        key == 35 || key == 36 ||
            // left and right arrows
                        key == 37 || key == 39 ||
            // Del and Ins
                        key == 46 || key == 45)
                return true;

            return false;
        });
    });
}

jQuery.fn.forceInt100 = function () {
    return this.each(function () {
        $(this).keydown(function (e) {
            var key = e.which || e.keyCode;

            if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
            // numbers
                         key >= 48 && key <= 57 ||
            // Numeric keypad
                         key >= 96 && key <= 105 ||
            // Backspace and Tab and Enter
                        key == 8 || key == 9 || key == 13 ||
            // Home and End
                        key == 35 || key == 36 ||
            // left and right arrows
                        key == 37 || key == 39 ||
            // Del and Ins
                        key == 46 || key == 45)
                return true;

            return false;
        }).keyup(function (e) {
            var i = parseInt($(this).val());
            if (i > 100) {
                $(this).val("100");
            }
        });
    });
}

