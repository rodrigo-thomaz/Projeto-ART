var HeaderUserLoginScript = function () {

    var handleInit = function () {

        $("#btnLogout").on("click", function () {
            logout();
        });
    }

    var handleLoadForm = function () {
        
        $.ajax({
            type: "GET",
            url: ApplicationScript.getIdentityWebApiUrl() + '/api/account/headeruserinfo',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
        }).success(function (data, textStatus, jqXHR) {

            var avatarStorageObject = data.key;
            var nomeExibicao = data.value;

            if (avatarStorageObject) {
                $('#headerImageAvatar').attr('src', "/api/perfil/avatar/" + avatarStorageObject);
            }
            else {
                $('#headerImageAvatar').attr('src', "../../../assets/admin/img/avatar.png");
            }

            $('#headerNomeExibicao').text('Ola, ' + nomeExibicao);
            $('#sideBarNomeExibicao').html(nomeExibicao);
            $('#aboutNomeExibicao').text('Sobre ' + nomeExibicao);

        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });

    }

    var logout = function () {

        var message = "Deseja realmente sair da aplica&ccedil&atildeo??";

        bootbox.confirm(message, function (result) {
            if (result) {                
                ApplicationScript.setToken('');
                window.location.href = '/Seguranca/Login?ReturnUrl=';
            }
        });

    }

    return {
        init: function () {            

            handleInit();
            handleLoadForm();

        },
    };
}();