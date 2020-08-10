(function () {

    $(function () {
        //Disable some elements of the default Swagger UI that we don't need
        $("#input_apiKey").hide();
        $("#explore").hide();
        $("input_baseUrl").prop("disabled", true);

        // Create new elements needed to collect user info for oauth2 authentication.
        var AuthUI =
            '<div class="input"><input placeholder="username" id="input_username" name="username" type="text" size="14"></div>' +
            '<div class="input"><input placeholder="password" id="input_password" name="password" type="password" size="14"></div>' +
            '<div class="input"><button id="getToken" href="#" data-sw-translate="" style="color: black; height: 27px">Login</button></div>' +
            '<div class="input"><a id="showToken" href="#" data-sw-translate="" stype="color: white; text-decoration: none;">?</a></div>';

        var tokenDisplay = '<div id="tokenDisplayArea" class="swagger-ui-wrap" style=""><h2>Token:</h2><textarea id="tokenWindow" disabled rows="12" cols="130"></textarea></div>';

        $(AuthUI).insertBefore('#api_selector div.input:last-child');
        $(tokenDisplay).insertBefore('#message-bar');

        $('#getToken').on('click', getToken);

        $('#showToken').on('click', toggleTokenArea);
        toggleTokenArea();
    });

    function toggleTokenArea() {
        $("#tokenDisplayArea").toggle();
    }

    function getToken() {
        var issueUrl = "tuUrlParaObtenerToken"; //REEMPLAZA AQUI
        var scopeUrl = "laUrlDeTuProyecto"; // O cualquier otro scope para usar con OAuth v2

        var username = $('#input_username').val();
        var password = $('#input_password').val();

        if (username && username.trim() != "" && password && password.trim() != "") {
            $.post(issueUrl, "grant_type=password&username=" + username + "&password=" + password + "&scope=" + scopeUrl)
                .done(function (data) {
                    var token = data.access_token;

                    swaggerUi.api.clientAuthorizations.add("token", new SwaggerClient.ApiKeyAuthorization("Authorization", ("Bearer " + token), "header"));

                    alert("Authentication request succeded! Click the '?' button to view your current token.");
                    $('#tokenWindow').val(token);
                    console.log("Authorization added for: " + username);
                })
                .fail(function (data) {
                    alert("Authentication failed. Details: " + JSON.stringify(data));

                    console.log("Authorization FAILED for: " + username);
                })
        }
    }
})();