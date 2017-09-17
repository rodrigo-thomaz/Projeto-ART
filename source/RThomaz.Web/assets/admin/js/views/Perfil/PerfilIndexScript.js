var PerfilIndexScript = function () {

    var handleInit = function () {

        //Setando default menu
        $("#mnuOverview").addClass("active");

        //Setando os titulos

        var title = 'Perfil';
        $(document).attr("title", title);
    }

    return {
        init: function () {

            handleInit();

        },
    };

}();

