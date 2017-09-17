$('#input_apiKey').change(function () {
    var key = $('#input_apiKey')[0].value;
    var credentials = key.split(':'); //username:password expected
    $.ajax({
        url: "http://localhost:27264/token",
        type: "post",
        contenttype: 'x-www-form-urlencoded',
        data: {
            'grant_type': 'password',
            'username': credentials[0],
            'password': credentials[1],
            'client_id': 'AppWebApi',
        },
        success: function (response) {
            var bearerToken = 'bearer ' + response.access_token;
            swaggerApi.clientAuthorizations.add("key", new SwaggerClient.ApiKeyAuthorization('Authorization', bearerToken, 'header'));
        },
        error: function (xhr, ajaxoptions, thrownerror) {
            alert("Erro no Login!");
        }
    });
});