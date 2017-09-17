var ChangePasswordScript = function () {

    var handleInit = function () {

        $("#formChangePassword").submit(function (e) {
            e.preventDefault(); //prevent default form submit            
        });       

        // Validation
        if ($.validator) {
            $("#formChangePassword").validate({
                errorElement: 'span',
                errorClass: 'help-block',
                focusInvalid: false,
                //ignore: "",  // validate all fields including form hidden input
                rules: {
                    txtSenhaAntiga: {
                        maxlength: 50,
                        required: true,
                    },
                    txtNovaSenha: {
                        maxlength: 50,
                        required: true,
                    },                    
                    txtConfirmaSenha: {
                        equalTo: "#txtNovaSenha"
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
                    formInvalidDefaultHandler(validator, "formChangePassword_custom-validate-error");
                    Metronic.scrollTo($("#formChangePassword_custom-validate-error"), 1);
                },
                submitHandler: function (form) {
                    changePassword();
                }
            });
        }

    }   

    var changePassword = function () {

        var senhaAntiga = $("#txtSenhaAntiga").val();
        var novaSenha = $("#txtNovaSenha").val();

        $.ajax({
            type: "POST",
            url: ApplicationScript.getAppWebApiUrl() + '/api/account/changePassword',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                'senhaAntiga': senhaAntiga,
                'novaSenha': novaSenha,
            },
            success: function (data) {

                if (data.succeeded)
                {
                    $("#formChangePassword_custom-validate-error").hide();
                    $("#formChangePassword_custom-validate-success").show();

                    Metronic.scrollTo($("#formChangePassword_custom-validate-success"), 1);

                    $("#txtSenhaAntiga").val('');
                    $("#txtNovaSenha").val('');
                    $("#txtConfirmaSenha").val('');
                }
                else
                {
                    $("#formChangePassword_custom-validate-success").hide();

                    var message = '';

                    for (var i = 0; i < data.errors.length; i++) {
                        message += data.errors[i];
                    }

                    $("#formChangePassword_custom-validate-error").children("span").html(message);
                    $("#formChangePassword_custom-validate-error").show();

                    Metronic.scrollTo($("#formChangePassword_custom-validate-error"), 1);
                }
            },
            error: function (request, status, error) {
                alert("Erro /Perfil/ChangePassword/");
            }
        });
    }

    return {
        init: function () {

            handleInit();

        },
    };

}();