var PerfilAccountScript = function () {
       
    var handleInit = function () {

        $("#mnuAccount").addClass("active");        
        
        $(".cancelAccount").on("click", function () {
            window.location.href = "/Perfil/Account";
        });        

        //Setando os titulos

        var title = 'Conta do usu\u00e1rio';
        $(document).attr("title", title);

        $('#spanTitle').text(title);
    }    

    return {
        init: function () {

            handleInit();

        },
    };

}();

