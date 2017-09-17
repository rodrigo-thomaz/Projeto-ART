var UsuarioDetailScript = function () {

    var usuarioId;

    var handleInit = function () {

        //Obtendo parametros
        usuarioId = parseInt(getUrlRouteParameter(3));

        //Setando default menu
        $("#menuUsuario").addClass("active");

        //Setando os titulos

        var title = 'Usu\u00e1rios';
        var subTitle = 'gerencie os usu\u00e1rios do sistema';

        setAppTitleSubTitle(title, subTitle);


        $("#formDetail").submit(function (e) {
            e.preventDefault(); //prevent default form submit            
        });

        // Validation
        if ($.validator) {
            $("#formDetail").validate({
                errorElement: 'span',
                errorClass: 'help-block',
                focusInvalid: false,
                //ignore: "",  // validate all fields including form hidden input
                rules: {
                    txtEmail: {
                        required: true,
                        maxlength: 250,
                        email: true,
                        emailExistente: true,
                    },
                    txtNomeExibicao: {
                        required: true,
                        maxlength: 250,
                    },
                    txtSenha: {
                        validaSenha: true,
                        maxlength: 50,
                    },
                    txtConfirmaSenha: {
                        equalTo: "#txtSenha"
                    },
                    txtDescricao: {
                        maxlength: 4000,
                    },
                },
                highlight: function (element) {
                    $(element).closest('.form-group').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.form-group').removeClass('has-error');
                },
                success: function (label) {
                    label.closest('.form-group').removeClass('has-error');
                },
                errorPlacement: function (error, element) { // render error placement for each input type
                    if (element.parent(".input-group").size() > 0) {
                        error.insertAfter(element.parent(".input-group"));
                    } else if (element.attr("data-error-container")) {
                        error.appendTo(element.attr("data-error-container"));
                    } else if (element.parents('.radio-list').size() > 0) {
                        error.appendTo(element.parents('.radio-list').attr("data-error-container"));
                    } else if (element.parents('.radio-inline').size() > 0) {
                        error.appendTo(element.parents('.radio-inline').attr("data-error-container"));
                    } else if (element.parents('.checkbox-list').size() > 0) {
                        error.appendTo(element.parents('.checkbox-list').attr("data-error-container"));
                    } else if (element.parents('.checkbox-inline').size() > 0) {
                        error.appendTo(element.parents('.checkbox-inline').attr("data-error-container"));
                    } else {
                        error.insertAfter(element); // for other inputs, just perform default behavior
                    }
                },
                invalidHandler: function (form, validator) {
                    formInvalidDefaultHandler(validator, "custom-validate-error");
                    Metronic.scrollTo($("#custom-validate-error"), 1);
                    $("#divForm").effect("shake", { distance: 6, times: 2 }, 35);
                },
                submitHandler: function (form) {
                    saveForm();
                }
            });
        }

        $('#txtDescricao').summernote({ height: 300 });

        $("#btnCancelar").on("click", function () {
            window.location.href = "/Usuario/Index";
        });

        $("#txtEmail").keyup(function () {
            $(this).val($(this).val().toLowerCase());
        });

    }

    var handleLoadForm = function () {

        if (usuarioId > 0) {//Edit

            $("#divAvatar").show();
            $(".senha-group").hide();
            $("#txtEmail").prop("readonly", true);
            $("#txtNomeExibicao").prop("readonly", true);

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/usuario/' + usuarioId,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {

                $("#txtEmail").val(data.email);
                $("#txtNomeExibicao").val(data.nomeExibicao);
                $('#txtDescricao').code(data.descricao);

                if (data.avatarStorageObject) {
                    $('#imgAvatar').attr('src', "/api/perfil/avatar/" + data.avatarStorageObject);
                }
                else {
                    $('#imgAvatar').attr('src', "../assets/admin/img/avatar1.png");
                }

                if (data.ativo) {
                    $('input[name=chkAtivo][value=true]').parent().addClass('checked');
                    $('input[name=chkAtivo][value=false]').parent().removeClass('checked');
                }
                else {
                    $('input[name=chkAtivo][value=true]').parent().removeClass('checked');
                    $('input[name=chkAtivo][value=false]').parent().addClass('checked');
                }

            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {//New

            $('#txtEmail').removeAttr('readonly');
            $(".senha-group").show();

            $('input[name=chkAtivo][value=true]').parent().addClass('checked');
            $('input[name=chkAtivo][value=false]').parent().removeClass('checked');

            $("#txtEmail").val();
            $("#txtNomeExibicao").val();
            $("#txtSenha").val();
            $("#txtConfirmaSenha").val("");

        }
    }

    var saveForm = function () {

        var email = $("#txtEmail").val();
        var nomeExibicao = $("#txtNomeExibicao").val();
        var ativo = $('input[name=chkAtivo][value=true]').parent().hasClass('checked');
        var senha = $("#txtSenha").val();
        var descricao = $('#txtDescricao').code();

        if (usuarioId == 0) {

            var data = {
                'email': email,
                'nomeExibicao': nomeExibicao,
                'senha': senha,
                'ativo': ativo,
                'descricao': descricao,
            };

            $.ajax({
                type: "POST",
                url: ApplicationScript.getAppWebApiUrl() + '/api/usuario',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Usuario/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });

        }
        else {

            var data = {
                'usuarioId': usuarioId,
                'ativo': ativo,
                'descricao': descricao,
            };

            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/usuario',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Usuario/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });

        }
    }

    return {
        init: function () {

            handleInit();
            handleLoadForm();

        },
    };
}();

 jQuery.validator.addMethod("emailExistente", function (value, element) {

     var usuarioId = parseInt(getUrlRouteParameter(3));
     if (usuarioId > 0) {//Edit (Não valida)
         return true;
     }

     var result = false;

     $.ajax({
         type: "GET",
         url: "/api/usuario/uniqueEmail/" + value,
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

 }, "J\u00e1 existe um usu\u00e1rio cadastrado com este email.");

 jQuery.validator.addMethod("validaSenha", function (value, element) {

     var usuarioId = parseInt(getUrlRouteParameter(3));
     if (usuarioId > 0) {//Edit (Não valida)
         return true;
     }

     var senha = $("#txtSenha").val();

     if (senha) {
         return true;
     }
     else {
         return false;
     }
 }, "Este campo \u00e9 requerido.");