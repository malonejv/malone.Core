(function () {
    let self = this;


    //#region Index

    self.Index = function () {
        $("div#lists a").bind("contextmenu", function (event) {
            event.preventDefault();

            var url = Helper.getMvcUrl();
            let id = $(event.target).data("id");

            const Edit = "/List/Edit/" + id;
            const Delete = "/List/Delete/" + id;
            const Details = "/List/Details/" + id;

            if (url == null) {
                $("#editAction").attr("href", Edit);
                $("#deleteAction").attr("href", Delete);
                $("#detailsAction").attr("href", Details);
            } else {
                $("#editAction").attr("href", url.Origin + Edit);
                $("#deleteAction").attr("href", url.Origin + Delete);
                $("#detailsAction").attr("href", url.Origin + Details);
            }

            $("ul.contextMenu")
                .show()
                .css({ top: event.pageY + 15, left: event.pageX + 10 });
        });
        $(document).click(function () {
            isHovered = $("ul.contextMenu").is(":hover");
            if (isHovered == false) {
                $("ul.contextMenu").fadeOut("fast");
            }
        });

        let $modalConfirmarEliminar = $("#modalConfirmarEliminar");
        if ($modalConfirmarEliminar.length > 0) {
            $modalConfirmarEliminar.modal('show');
        }
    }
    //#endregion

    //#region Details

    self.Details = function () {
        var url = Helper.getMvcUrl();

        $("ul#tasksPending li").bind("contextmenu", function (event) {
            event.preventDefault();

            let id = $(event.target).data("id");

            const Edit = "/List/EditTask/" + url.Id + '/' + id;
            const Delete = "/List/DeleteTask/" + url.Id + '/' + id;

            if (url == null) {
                $("#editAction").attr("href", Edit);
                $("#deleteAction").attr("href", Delete);
            } else {
                $("#editAction").attr("href", url.Origin + Edit);
                $("#deleteAction").attr("href", url.Origin + Delete);
            }

            $("ul.contextMenu")
                .show()
                .css({ top: event.pageY + 15, left: event.pageX + 10 });
        });
        $(document).click(function () {
            isHovered = $("ul.contextMenu").is(":hover");
            if (isHovered == false) {
                $("ul.contextMenu").fadeOut("fast");
            }
        });

        $("input[type='checkbox']").change(function () {
            var $inputCheck = $(this);
            var headers = {};
            var token = $(".body-content>.container>[name=__RequestVerificationToken]").val();
            headers['__RequestVerificationToken'] = token;
            var data = {
                listId: url.Id,
                taskId: $inputCheck.data("id")
            };

            let checkAction = url.Origin + '/List/CheckTask';
            $.ajax({
                url: checkAction,
                type: 'POST',
                headers: headers,
                dataType: 'json',
                data: JSON.stringify(data),
                contentType: 'application/json',
                success: function (response) {
                    window.location.href = response.Url;
                },
                error: function (e) {
                    var errorMsg = e;

                    var checked = $inputCheck.prop("checked", true);
                    $inputCheck.prop("checked", !checked);

                }
            });
        });
    }

    //#endregion

    //#region General functions

    self.ShowLoading = function () {
        //show a progress modal of your choosing
        var $wrapper = $("#loadingWrapper");
        $wrapper.removeClass("d-none");
    };

    self.HideLoading = function () {
        //hide it
        var $wrapper = $("#loadingWrapper");
        $wrapper.addClass("d-none");
    };

    self.ShowMessage = function (message) {
        var $wrapper = $("#messageWrapper");

        $wrapper.ShowMessage(message,
            {
                popupTime: 0,
                dismissable: true
            });
    };

    //#endregion

    let init = function () {
        self.Index();

        var url = Helper.getMvcUrl();
        if (url == null || url.Action == '' || self[url.Action] == undefined)
            self.Index();
        else {
            if (self[url.Action] != undefined)
                self[url.Action]();
        }
    }
    init();

    $(document).ajaxStart(function () {
        if (self != null)
            self.ShowLoading();
    });
    $(document).ajaxStop(function () {
        if (self != null)
            self.HideLoading();
    });
    $(document).ajaxError(function (e, jqxhr, settings, exception) {
        e.stopPropagation();

        if (jqxhr != null) {
            try {
                var content = $.parseJSON(jqxhr.responseText == "" ? null : jqxhr.responseText);
                if (self != null && content != null)
                    self.ShowMessage(content);
            } catch (e) {
                alert('Ocurrió un error inesperado.');
            }
        }
    });
})();