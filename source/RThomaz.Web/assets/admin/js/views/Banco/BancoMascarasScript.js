var BancoMascarasScript = function () {

    var handleInit = function () {

       
    }    

    var handleGet = function (bancoId) {

        var result;

        $.ajax({
            type: "GET",
            url: ApplicationScript.getAppWebApiUrl() + '/api/banco/' + bancoId + '/mascaras',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: false,
        }).success(function (data, textStatus, jqXHR) {
            result = data;
        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });

        return result;
    };

    return {
        init: function () {
            handleInit();
        },
        get: function (bancoId) {
            return handleGet(bancoId);
        },
    };

}();