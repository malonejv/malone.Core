//#region MvcUrl Class

function MvcUrl(url, controller, action, id) {
    let self = this;

    self.Url = url.href;

    self.Origin = url.origin;

    self.Path = url.pathname;

    self.Controller = controller;

    self.Action = action;

    self.Id = id;
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
            let id = null;
            let splitUrl = null;

            if (path.substring(0, 1) == "/" && path.length > 1) {
                splitPath = path.substring(1).split("/")

                for (var i = 0; i < splitPath.length; i++) {
                    if (i == 0) {
                        controller = splitPath[i];
                    } else if (i == 1) {
                        action = splitPath[i];
                    } else if (i == 2) {
                        id = splitPath[i];
                    }
                    else {
                        break;
                    }
                }
                mvcUrl = new MvcUrl(url, controller, action, id);
            }
        }
    }
    return mvcUrl;
}


//#endregion