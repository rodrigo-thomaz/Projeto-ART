var PessoaHomePageDetailScript = function () {

    var _tipoPessoa;
    var _newPessoaHomePageId = -1;

    var handleInit = function (tipoPessoa) {

        _tipoPessoa = tipoPessoa;

        $("#btnNewHomePage").on("click", function () {
            handleInsertHomePage();
        });

    }

    var handleLoadHomePages = function (homePages) {

        $("#divHomePages").append('');

        $.each(homePages, function (index, value) {

            createHomePageRow(value.pessoaHomePageId, value.tipoHomePageId, value.homePage);

        });

    };

    var handleInsertHomePage = function () {

        var tipoHomePageId = null;
        var homePage = '';

        createHomePageRow(_newPessoaHomePageId, tipoHomePageId, homePage);

        _newPessoaHomePageId--;
    };

    var handleRemoveHomePage = function (pessoaHomePageId) {
        var control = $("#divHomePages [data-pessoahomepageid='" + pessoaHomePageId + "']");
        $(control).remove();
    };

    var handleGetHomePages = function () {

        var homePageRows = $("#divHomePages .row");

        var result = [];

        $.each(homePageRows, function (index, value) {

            var pessoaHomePageId = $(value).data().pessoahomepageid;

            var tipoHomePageId = $('#cmbTipoHomePage' + pessoaHomePageId).val();
            var homePage = $('#txtHomePage' + pessoaHomePageId).val();

            if (pessoaHomePageId < 0) {
                pessoaHomePageId = 0;
            }

            result.push({
                'PessoaHomePageId': pessoaHomePageId,
                'TipoHomePageId': tipoHomePageId,
                'HomePage': homePage,
            });
        });

        return result;
    };

    var createHomePageRow = function (pessoaHomePageId, tipoHomePageId, homePage) {

        var homePageControl = '';

        var cmbTipoHomePageNome = 'cmbTipoHomePage' + pessoaHomePageId;
        var txtHomePageNome = 'txtHomePage' + pessoaHomePageId;

        homePageControl += '<div class="row" data-pessoahomePageid="' + pessoaHomePageId + '">';
        homePageControl += '    <div class="col-md-6">';
        homePageControl += '        <div class="form-group">';
        homePageControl += '            <select id="' + cmbTipoHomePageNome + '" name="' + cmbTipoHomePageNome + '" class="form-control"></select>';
        homePageControl += '        </div>';
        homePageControl += '    </div>';
        homePageControl += '    <div class="col-md-6">';
        homePageControl += '        <div class="form-group">';
        homePageControl += '            <div class="input-group">';
        homePageControl += '                <span class="input-group-addon">';
        homePageControl += '                    <i class="fa fa-envelope"></i>';
        homePageControl += '                </span>';
        homePageControl += '                <input id="' + txtHomePageNome + '" name="' + txtHomePageNome + '" class="form-control" type="text" placeholder="homePage" value="' + homePage + '">';
        homePageControl += '                <span class="input-group-btn">';
        homePageControl += '                    <button class="btn btn-danger" type="button" onclick="PessoaHomePageDetailScript.removeHomePage(' + pessoaHomePageId + ');"><i class="fa fa-trash-o fa-fw"></i></button>';
        homePageControl += '                </span>';
        homePageControl += '            </div>';
        homePageControl += '        </div>';
        homePageControl += '    </div>';
        homePageControl += '</div>';

        $("#divHomePages").append(homePageControl);

        $("#" + cmbTipoHomePageNome).rules("add", {
            required: true,
        });

        $("#" + txtHomePageNome).rules("add", {
            required: true,
        });

        $.ajax({
            type: "GET",
            url: ApplicationScript.getAppWebApiUrl() + '/api/tipohomepage/selectViewList/' + _tipoPessoa,
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
        }).success(function (data, textStatus, jqXHR) {
            var options = $("#" + cmbTipoHomePageNome);
            options.append($("<option />"));
            for (var i = 0; i < data.length; i++) {
                options.append($("<option />").val(data[i].tipoHomePageId).text(data[i].nome));
            }
            if (tipoHomePageId > 0) {
                options.val(tipoHomePageId);
            }
        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });

    };


    return {
        init: function (tipoPessoa) {
            handleInit(tipoPessoa);
        },
        loadHomePages: function (homePages) {
            handleLoadHomePages(homePages);
        },
        removeHomePage: function (pessoaHomePageId) {
            handleRemoveHomePage(pessoaHomePageId);
        },
        getHomePages: function () {
            return handleGetHomePages();
        },
    };
}();



