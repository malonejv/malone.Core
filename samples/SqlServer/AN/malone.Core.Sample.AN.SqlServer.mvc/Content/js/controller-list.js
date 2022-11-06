(function () {
    let self = this;

    const ui = {
        listForm: '#ListForm',
        btnCreateList: '#btnCreateList',
        btnEditList: '#btnEditList',
        inputListName: "input#Name",
        lists: 'div#lists a',
        taskForm: '#TaskForm',
        btnCreateTask: '#btnCreateTask',
        btnEditTask: '#btnEditTask',
        inputTaskDescription: "input#Description",
        btnClear: '#btnClear',
        formWrapper: '#formWrapper',
        modalWrapper: '#modalWrapper',
        editAction: '#editAction',
        deleteAction: '#deleteAction',
        detailsAction: '#detailsAction',
        contextMenu: 'ul.contextMenu',
        confirmDeleteModal: '#modalConfirmarEliminar',
        taskPending: 'ul#tasksPending li',
        checkBox: "input[type='checkbox']",
        loading: '#loadingWrapper',
        message: '#messageWrapper',
        defaultControl: 'input.default-control'
    }

    const url = {
        List: {
            Create: '/List/Create/',
            Edit: '/List/Edit/',
            Delete: '/List/Delete/',
            Detail: '/List/Detail/',
        },
        Task: {
            Create: '/Task/Create/',
            Edit: '/Task/Edit',
            Delete: '/Task/Delete',
            ToggleDone: '/Task/ToggleDone',
        }
    }

    const messages = {
        unexpectedError: 'Ocurrió un error inesperado.'
    }

    let urlPath = Helper.getMvcUrl();

    //#region Index

    $(document).on("click", ui.btnCreateList, (e) => {
        e.stopImmediatePropagation();
        e.preventDefault();

        const $form = $(ui.listForm);
        let headers = {};
        headers['__RequestVerificationToken'] = $("input[name=__RequestVerificationToken]", $form).val();

        $.ajax({
            url: url.List.Create,
            type: 'POST',
            headers: headers,
            data: $form.serializeArray(),
            success: function (response) {
                if (response && response.Url)
                    window.location.href = response.Url;
                else {
                    $(ui.formWrapper).html(response)
                }
            }
        });

    });

    $(document).on("click", ui.btnEditList, (e) => {
        e.stopImmediatePropagation();
        e.preventDefault();

        const $form = $(ui.listForm);
        let headers = {};
        headers['__RequestVerificationToken'] = $("input[name=__RequestVerificationToken]", $form).val();

        $.ajax({
            url: url.List.Edit,
            type: 'POST',
            headers: headers,
            data: $form.serializeArray(),
            success: function (response) {
                if (response && response.Url)
                    window.location.href = response.Url;
                else {
                    $(ui.formWrapper).html(response)
                }
            }
        });

    });

    $(document).on("input", `${ui.inputListName}, ${ui.inputTaskDescription}`, (e) => {
        const $input = $(e.currentTarget);
        let $btn = $(e.currentTarget).parent().find(ui.btnClear);
        const calcLeft = $input.width() - 10;
        $btn.css('left', `${calcLeft}px`);

        if ($input.val() == '')
            $btn.fadeOut('fast');
        else
            $btn.fadeIn();
    })

    $(document).on("click", ui.btnClear, (e) => {
        e.stopImmediatePropagation();
        e.preventDefault();

        const $btn = $(e.currentTarget);
        let input = $btn.parent().find("input[type=text]");
        $(input).val('');
        $btn.fadeOut('fast');
    });

    //#endregion

    //#region Detail

    $(document).on("click", ui.btnCreateTask, (e) => {
        e.stopImmediatePropagation();
        e.preventDefault();

        const $form = $(ui.taskForm);
        let headers = {};
        headers['__RequestVerificationToken'] = $("input[name=__RequestVerificationToken]", $form).val();

        $.ajax({
            url: url.Task.Create,
            type: 'POST',
            headers: headers,
            data: $form.serializeArray(),
            success: function (response) {
                if (response && response.Url)
                    window.location.href = response.Url;
                else {
                    $(ui.formWrapper).html(response)
                }
            }
        });

    });

    $(document).on("click", ui.btnEditTask, (e) => {
        e.stopImmediatePropagation();
        e.preventDefault();

        const $form = $(ui.taskForm);
        let headers = {};
        headers['__RequestVerificationToken'] = $("input[name=__RequestVerificationToken]", $form).val();

        $.ajax({
            url: url.Task.Edit,
            type: 'POST',
            headers: headers,
            data: $form.serializeArray(),
            success: function (response) {
                if (response && response.Url)
                    window.location.href = response.Url;
                else {
                    $(ui.formWrapper).html(response)
                }
            }
        });

    });

    $(ui.checkBox).change(function () {
        let $inputCheck = $(this);
        let headers = {};
        let token = $(".body-content>.container>[name=__RequestVerificationToken]").val();
        headers['__RequestVerificationToken'] = token;

        let data = {
            listId: urlPath.Id,
            taskId: $inputCheck.data("id")
        };
        let checkAction = `${url.Task.ToggleDone}`;

        if (urlPath != null) {
            checkAction = `${urlPath.Origin}/${url.Task.ToggleDone}`;
        }

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
                let errorMsg = e;

                let checked = $inputCheck.prop("checked", true);
                $inputCheck.prop("checked", !checked);

            }
        });
    });

    //#endregion

    //#region Context menu

    $(document).on("contextmenu", ui.lists, function (e) {
        e.stopImmediatePropagation();
        e.preventDefault();

        let id = $(e.currentTarget).data("id");

        const Edit = url.List.Edit + id;
        const Delete = url.List.Delete + id;
        const Detail = url.List.Detail + id;

        if (urlPath == null) {
            $(ui.editAction).attr("href", Edit);
            $(ui.deleteAction).attr("href", Delete);
            $(ui.detailsAction).attr("href", Detail);
        } else {
            $(ui.editAction).attr("href", urlPath.Origin + Edit);
            $(ui.deleteAction).attr("href", urlPath.Origin + Delete);
            $(ui.detailsAction).attr("href", urlPath.Origin + Detail);
        }
        self.ShowContextMenu(event.pageX, event.pageY)
    });

    $(document).on("click", ui.editAction, (e) => {
        e.stopImmediatePropagation();
        e.preventDefault();

        const url = $(e.currentTarget).attr("href");

        $.ajax({
            url: url,
            type: 'GET',
            contentType: 'application/json',
            success: function (response) {
                $(ui.formWrapper).html(response);
                self.CalculateButtonClearLocation();
                self.HideContextMenu();
            }
        });
    });

    $(document).on("click", ui.deleteAction, (e) => {
        e.stopImmediatePropagation();
        e.preventDefault();

        const url = $(e.currentTarget).attr("href");

        $.ajax({
            url: url,
            type: 'GET',
            contentType: 'application/json',
            success: function (response) {
                self.HideContextMenu();

                if (response && response.Url)
                    window.location.href = response.Url;
                else {
                    $(ui.modalWrapper).html(response);
                    $(ui.modalWrapper).children().modal('show');
                }
            }
        });
    });

    $(document).on("contextmenu", ui.taskPending, function (event) {
        event.preventDefault();

        let id = $(event.currentTarget).data("id");

        let Edit = `/${urlPath.Controller}/${urlPath.Id}${url.Task.Edit}/${id}`;
        let Delete = `/${urlPath.Controller}/${urlPath.Id}${url.Task.Delete}/${id}`;

        if (urlPath.Origin != null) {
            Edit = `${urlPath.Origin}${Edit}`;
            Delete = `${urlPath.Origin}${Delete}`;
        }

        $(ui.editAction).attr("href", Edit);
        $(ui.deleteAction).attr("href", Delete);

        self.ShowContextMenu(event.pageX, event.pageY);
    });

    $(document).on("click", function () {
        self.HideContextMenu();
    });

    self.ShowContextMenu = function (x, y) {
        $(ui.contextMenu)
            .show()
            .css({ top: y + 15, left: x + 10 });
    }

    self.HideContextMenu = function () {
        $(ui.contextMenu).fadeOut("fast");
    }

    //#endregion

    //#region General functions

    self.ShowLoading = function () {
        //show a progress modal of your choosing
        let $wrapper = $(ui.loading);
        $wrapper.removeClass("d-none");
    }

    self.HideLoading = function () {
        //hide it
        let $wrapper = $(ui.loading);
        $wrapper.addClass("d-none");
    }

    self.ShowMessage = function (message) {
        let $wrapper = $(ui.message);

        $wrapper.ShowMessage(message,
            {
                popupTime: 0,
                dismissable: true
            });
    }

    self.CalculateButtonClearLocation = function () {
        let $defaultControl = $(ui.defaultControl);
        let $btn = $defaultControl.parent().find(ui.btnClear);
        const calcLeft = $defaultControl.width() - 10;
        $btn.css('left', `${calcLeft}px`);
    }
    //#endregion

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
                let content = $.parseJSON(jqxhr.responseText == "" ? null : jqxhr.responseText);
                if (self != null && content != null)
                    self.ShowMessage(content);
            } catch (e) {
                alert(messages.unexpectedError);
            }
        }
    });

    let $defaultControl = $(ui.defaultControl);
    if ($defaultControl)
        $defaultControl.focus();

})();