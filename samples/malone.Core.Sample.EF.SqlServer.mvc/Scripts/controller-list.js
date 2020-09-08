(function () {
    let self = this;

    self.Index = function () {
    }

    let init = function () {
        var url = Helper.getMvcUrl();
        if (url == null || url.Action() == '')
            self.Index();
        else
            self[url.Action()]();
    }
    init();

})();