var PessoaEmailDetailScript = function () {    

    var _tipoPessoa;
    var _newPessoaEmailId = -1;

    var handleInit = function (tipoPessoa) {

        _tipoPessoa = tipoPessoa;

        $("#btnNewEmail").on("click", function () {
            handleInsertEmail();
        });

    }

    var handleLoadEmails = function (emails) {

        $("#divEmails").append('');

        $.each(emails, function (index, value) {

            createEmailRow(value.pessoaEmailId, value.tipoEmailId, value.email);

        });

    };

    var handleInsertEmail = function () {

        var tipoEmailId = null;
        var email = '';

        createEmailRow(_newPessoaEmailId, tipoEmailId, email);

        _newPessoaEmailId--;
    };

    var handleRemoveEmail = function (pessoaEmailId) {
        var control = $("#divEmails [data-pessoaemailid='" + pessoaEmailId + "']");
        $(control).remove();
    };

    var handleGetEmails = function () {

        var emailRows = $("#divEmails .row");

        var result = [];

        $.each(emailRows, function (index, value) {

            var pessoaEmailId = $(value).data().pessoaemailid;

            var tipoEmailId = $('#cmbTipoEmail' + pessoaEmailId).val();
            var email = $('#txtEmail' + pessoaEmailId).val();

            if (pessoaEmailId < 0) {
                pessoaEmailId = 0;
            }

            result.push({
                'PessoaEmailId': pessoaEmailId,
                'TipoEmailId': tipoEmailId,
                'Email': email,
            });
        });

        return result;
    };

    var createEmailRow = function (pessoaEmailId, tipoEmailId, email) {

        var emailControl = '';

        var cmbTipoEmailNome = 'cmbTipoEmail' + pessoaEmailId;
        var txtEmailNome = 'txtEmail' + pessoaEmailId;

        emailControl += '<div class="row" data-pessoaemailid="' + pessoaEmailId + '">';
        emailControl += '    <div class="col-md-6">';
        emailControl += '        <div class="form-group">';
        emailControl += '            <select id="' + cmbTipoEmailNome + '" name="' + cmbTipoEmailNome + '" class="form-control"></select>';
        emailControl += '        </div>';
        emailControl += '    </div>';
        emailControl += '    <div class="col-md-6">';
        emailControl += '        <div class="form-group">';
        emailControl += '            <div class="input-group">';
        emailControl += '                <span class="input-group-addon">';
        emailControl += '                    <i class="fa fa-envelope"></i>';
        emailControl += '                </span>';
        emailControl += '                <input id="' + txtEmailNome + '" name="' + txtEmailNome + '" class="form-control" type="text" placeholder="email" value="' + email + '">';
        emailControl += '                <span class="input-group-btn">';
        emailControl += '                    <button class="btn btn-danger" type="button" onclick="PessoaEmailDetailScript.removeEmail(' + pessoaEmailId + ');"><i class="fa fa-trash-o fa-fw"></i></button>';
        emailControl += '                </span>';
        emailControl += '            </div>';
        emailControl += '        </div>';
        emailControl += '    </div>';
        emailControl += '</div>';

        $("#divEmails").append(emailControl);

        $("#" + cmbTipoEmailNome).rules("add", {
            required: true,
        });

        $("#" + txtEmailNome).rules("add", {
            required: true,
        });

        $.ajax({
            type: "GET",
            url: ApplicationScript.getAppWebApiUrl() + '/api/tipoemail/selectViewList/' + _tipoPessoa,
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
        }).success(function (data, textStatus, jqXHR) {
            var options = $("#" + cmbTipoEmailNome);
            options.append($("<option />"));
            for (var i = 0; i < data.length; i++) {
                options.append($("<option />").val(data[i].tipoEmailId).text(data[i].nome));
            }
            if (tipoEmailId > 0) {
                options.val(tipoEmailId);
            }
        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });

    };
    

    return {
        init: function (tipoPessoa) {
            handleInit(tipoPessoa);
        },
        loadEmails: function (emails) {
            handleLoadEmails(emails);
        },
        removeEmail: function (pessoaEmailId) {
            handleRemoveEmail(pessoaEmailId);
        },
        getEmails: function () {
            return handleGetEmails();
        },
    };
}();



