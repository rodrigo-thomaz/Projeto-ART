var ConciliacaoSharedScript = function () {

    var handleInit = function () {
      

    }

    var handleGetLancamentos = function (movimentoId, tipoTransacao) {

        var result = null;

        $.ajax({
            type: "GET",
            async: false,
            url: ApplicationScript.getAppWebApiUrl() + '/api/conciliacao/getlancamentosconciliados/' + movimentoId + '/' + tipoTransacao,
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
        }).success(function (data, textStatus, jqXHR) {
            result = data;
        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });
       
        return result;
    }

    var handleGetMovimentos = function (lancamentoId, tipoTransacao) {

        var result = null;

        $.ajax({
            type: "GET",
            async: false,
            url: ApplicationScript.getAppWebApiUrl() + '/api/conciliacao/getmovimentosconciliados/' + lancamentoId + '/' + tipoTransacao,
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
        }).success(function (data, textStatus, jqXHR) {
            result = data;
        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });

        return result;
    }

    return {
        init: function () {
            handleInit();
        },
        getLancamentos: function (movimentoId, tipoTransacao) {
            return handleGetLancamentos(movimentoId, tipoTransacao);
        },
        getMovimentos: function (lancamentoId, tipoTransacao) {
            return handleGetMovimentos(lancamentoId, tipoTransacao);
        },
    };

}();