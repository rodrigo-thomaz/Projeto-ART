var PessoaTelefoneDetailScript = function () {

    var _tipoPessoa;
    var _newPessoaTelefoneId = -1;

    var handleInit = function (tipoPessoa) {

        _tipoPessoa = tipoPessoa;

        $("#btnNewTelefone").on("click", function () {
            handleInsertTelefone();
        });

    }

    var handleLoadTelefones = function (telefones) {

        $("#divTelefones").append('');

        $.each(telefones, function (index, value) {

            createTelefoneRow(value.pessoaTelefoneId, value.tipoTelefoneId, value.telefone);

        });

    };

    var handleInsertTelefone = function () {

        var tipoTelefoneId = null;
        var telefone = '';

        createTelefoneRow(_newPessoaTelefoneId, tipoTelefoneId, telefone);

        _newPessoaTelefoneId--;
    };

    var handleRemoveTelefone = function (pessoaTelefoneId) {
        var control = $("#divTelefones [data-pessoatelefoneid='" + pessoaTelefoneId + "']");
        $(control).remove();
    };

    var handleGetTelefones = function () {

        var telefoneRows = $("#divTelefones .row");

        var result = [];

        $.each(telefoneRows, function (index, value) {

            var pessoaTelefoneId = $(value).data().pessoatelefoneid;

            var tipoTelefoneId = $('#cmbTipoTelefone' + pessoaTelefoneId).val();
            var telefone = $('#txtTelefone' + pessoaTelefoneId).val();

            if (pessoaTelefoneId < 0) {
                pessoaTelefoneId = 0;
            }

            result.push({
                'PessoaTelefoneId': pessoaTelefoneId,
                'TipoTelefoneId': tipoTelefoneId,
                'Telefone': telefone,
            });
        });

        return result;
    };

    var createTelefoneRow = function (pessoaTelefoneId, tipoTelefoneId, telefone) {

        var telefoneControl = '';

        var cmbTipoTelefoneNome = 'cmbTipoTelefone' + pessoaTelefoneId;
        var txtTelefoneNome = 'txtTelefone' + pessoaTelefoneId;

        telefoneControl += '<div class="row" data-pessoatelefoneid="' + pessoaTelefoneId + '">';
        telefoneControl += '    <div class="col-md-6">';
        telefoneControl += '        <div class="form-group">';
        telefoneControl += '            <select id="' + cmbTipoTelefoneNome + '" name="' + cmbTipoTelefoneNome + '" class="form-control"></select>';
        telefoneControl += '        </div>';
        telefoneControl += '    </div>';
        telefoneControl += '    <div class="col-md-6">';
        telefoneControl += '        <div class="form-group">';
        telefoneControl += '            <div class="input-group">';
        telefoneControl += '                <span class="input-group-addon">';
        telefoneControl += '                    <i class="fa fa-envelope"></i>';
        telefoneControl += '                </span>';
        telefoneControl += '                <input id="' + txtTelefoneNome + '" name="' + txtTelefoneNome + '" class="form-control" type="text" placeholder="telefone" value="' + telefone + '">';
        telefoneControl += '                <span class="input-group-btn">';
        telefoneControl += '                    <button class="btn btn-danger" type="button" onclick="PessoaTelefoneDetailScript.removeTelefone(' + pessoaTelefoneId + ');"><i class="fa fa-trash-o fa-fw"></i></button>';
        telefoneControl += '                </span>';
        telefoneControl += '            </div>';
        telefoneControl += '        </div>';
        telefoneControl += '    </div>';
        telefoneControl += '</div>';

        $("#divTelefones").append(telefoneControl);

        $("#" + cmbTipoTelefoneNome).rules("add", {
            required: true,
        });

        $("#" + txtTelefoneNome).rules("add", {
            required: true,
        });

        $.ajax({
            type: "GET",
            url: ApplicationScript.getAppWebApiUrl() + '/api/tipotelefone/selectViewList/' + _tipoPessoa,
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
        }).success(function (data, textStatus, jqXHR) {
            var options = $("#" + cmbTipoTelefoneNome);
            options.append($("<option />"));
            for (var i = 0; i < data.length; i++) {
                options.append($("<option />").val(data[i].tipoTelefoneId).text(data[i].nome));
            }
            if (tipoTelefoneId > 0) {
                options.val(tipoTelefoneId);
            }
        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });

    };


    return {
        init: function (tipoPessoa) {
            handleInit(tipoPessoa);
        },
        loadTelefones: function (telefones) {
            handleLoadTelefones(telefones);
        },
        removeTelefone: function (pessoaTelefoneId) {
            handleRemoveTelefone(pessoaTelefoneId);
        },
        getTelefones: function () {
            return handleGetTelefones();
        },
    };
}();



