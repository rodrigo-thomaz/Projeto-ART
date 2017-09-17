var PerfilHelpScript = function () {

    var handleInit = function () {

        //Setando default menu
        $("#mnuHelp").addClass("active");

        //Setando os titulos

        var title = 'Ajuda';
        $(document).attr("title", title);
    }    

    return {
        init: function () {

            handleInit();            

        },
    };

}();

