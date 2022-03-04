/// <reference path="jquery-1.10.2.js" />

(function ($) {
    var defaultSettings = {
        //Defaults.
        popupTime: "0000",
        dismissable: true
    };

    function CreateMessage(container, json, settings, type) {
        var messageContainer = $('<div class="alert alert-' + type + ' alert-dismissible fade in" role="alert"></div>');
        var closeButton = $('<button type="button" class="close" data-dismiss="alert" aria-label="Close" data-dismiss="alert"><span aria-hidden="true">&times;</span></button');

        var header;
        if (json.Header != undefined && json.Header != '') {
            header = $('<div class="text-' + type + ' text-bold alert-heading"></div>');
            var strong = $('<strong></strong>');
            json.Header = json.Header.indexOf(":") !== -1 ? json.Header : json.Header + ":";
            strong.text(json.Header);
            header.append(strong);
            header.append(" ");
        }

        var contentText;
        if (json.Paragraphs != undefined && json.Paragraphs != '') {
            $.each(json.Paragraphs, function (key, value) {
                if (json.Paragraphs.length > 1) {
                    contentText = $('<ul></ul>');
                    li = $("<li class='text-" + type + "' ></li>");
                    li.text(value);
                    contentText.append(li);
                } else {
                    if (!header) {
                        contentText = $("<li class='text-" + type + "' ></li>");
                        contentText.text(value);
                    }
                    else {
                        var small = $('<small></small>');
                        small.text(value);
                        header.append(small);
                    }
                }
            });
        }

        if (settings.dismissable || settings.popupTime == 0) {
            messageContainer.append(closeButton);
            messageContainer.append(header);
            messageContainer.append(contentText);
        }
        else {
            messageContainer.append(header);
            messageContainer.append(contentText);
        }
        container.append(messageContainer);
        container.removeClass('d-none');
        messageContainer.addClass('show');
        if (settings.popupTime > 0) {
            var interval = setInterval(function () {
                if (messageContainer != null) {
                    messageContainer.remove();

                    if (container.children('.alert').length == 0) {
                        container.addClass('d-none');
                    }

                    clearInterval(interval);
                }
            }, settings.popupTime);


            closeButton.bind('click', function () {
                clearInterval(interval);
            });
        }
    }

    $.fn.ShowMessage = function (data, options) {

        var settings = $.extend(defaultSettings, options);

        return this.each(function () {
            //var json = $.parseJSON(data);
            var json = data;

            if (json != null) {
                var $container = $(this);
                CreateMessage($container, json, settings, data.Status.toLowerCase());
            }
        });
    };

    $.fn.ShowError = function (data, options) {

        var settings = $.extend(defaultSettings, options);

        return this.each(function () {
            //var json = $.parseJSON(data);
            var json = data;

            if (json != null) {
                var $container = $(this);
                CreateMessage($container, json, settings, 'danger');
            }
        });
    };

    $.fn.ShowWarning = function (data, options) {

        var settings = $.extend(defaultSettings, options);

        return this.each(function () {
            //var json = $.parseJSON(data);
            var json = data;

            if (json != null) {
                var $container = $(this);
                CreateMessage($container, json, settings, 'warning');
            }
        });
    };

    $.fn.ShowInfo = function (data, options) {

        var settings = $.extend(defaultSettings, options);

        return this.each(function () {
            //var json = $.parseJSON(data);
            var json = data;

            if (json != null) {
                var $container = $(this);
                CreateMessage($container, json, settings, 'info');
            }
        });
    };

    $.fn.ShowSuccess = function (data, options) {

        var settings = $.extend(defaultSettings, options);

        return this.each(function () {
            //var json = $.parseJSON(data);
            var json = data;

            if (json != null) {
                var $container = $(this);
                CreateMessage($container, json, settings, 'success');
            }
        });
    };

} (jQuery));

