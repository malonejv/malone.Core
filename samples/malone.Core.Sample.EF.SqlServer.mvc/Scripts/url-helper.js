//#region MvcUrl Class

function MvcUrl(controller, action) {

    var _controller = controller;
    this.Controller = function () {
        return _controller;
    }

    var _action = action;
    this.Action = function () {
        return _action;
    }

}

//#endregion

//#region Helper Class

function Helper() { }

Helper.getMvcUrl = function () {
    let mvcUrl = null;
    let url = window.location;
    if (url != null) {

        let path = url.pathname;
        if (path != null && path.trim() != '') {
            let controller = '';
            let action = '';
            let splitUrl = null;

            if (path.substring(0, 1) == "/" && path.length > 1) {
                splitPath = path.substring(1).split("/")

                for (var i = 0; i < splitPath.length; i++) {
                    if (i == 0) {
                        controller = splitPath[i];
                    } else if (i == 1) {
                        action = splitPath[i];
                    }
                    else {
                        break;
                    }
                }
                mvcUrl = new MvcUrl(controller, action);
            }
        }
    }
    return mvcUrl;
}


//#endregion