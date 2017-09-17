var SegurancaLoginScript = function () {

    var returnUrl = null;

    var handleLogin = function () {

        $(".login-form").submit(function (e) {
            e.preventDefault(); //prevent default form submit            
        });

        $('.login-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            rules: {
                txtLoginEmail: {
                    required: true
                },
                txtLoginSenha: {
                    required: true
                },
                chkLoginLembrarMe: {
                    required: false
                }
            },
            highlight: function (element) {
                $(element).closest('.form-group').addClass('has-error');
            },
            unhighlight: function (element) {
                $(element).closest('.form-group').removeClass('has-error');
            },
            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
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
            submitHandler: function (form) {
                validateCredentialsToken();
            }
        });

        $('.login-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.login-form').validate().form()) {
                    $('.login-form').submit(); //form validation success, call ajax form submit
                }
                return false;
            }
        });
    }

    var validateCredentialsToken = function () {

        var email = $("#txtLoginEmail").val();
        var senha = $("#txtLoginSenha").val();
        var lembrarMe = $("#chkLoginLembrarMe").prop("checked");

        var identityWebApiUrl = ApplicationScript.getIdentityWebApiUrl();

        $.ajax({
            type: "POST",
            url: identityWebApiUrl + '/token',
            contentType: "application/x-www-form-urlencoded",
            dataType: "json",
            data: {
                'grant_type': 'password',
                'username': email,
                'password': senha,
                'client_id': 'AppMCV',
            },
            success: function (data) {
                ApplicationScript.setToken(data.access_token);                
                $("#login-form-alert-danger").hide();
                window.location.href = returnUrl == '' ? '/' : returnUrl;                
            },
            error: function (request, status, error) {
                ApplicationScript.setToken('');
                if (JSON.parse(request.responseText).error == 'invalid_grant') {
                    var message = 'Oops, usu\u00e1rio ou senha incorretos.';
                    $("#login-form-alert-danger").html(message).show();                    
                }
                else {
                    alert("Erro login Token");
                }
            }
        });
    }

    var handleForgetPassword = function () {
        $('.forget-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",
            rules: {
                txtForgetEmail: {
                    required: true,
                    email: true
                }
            },

            messages: {
                txtForgetEmail: {
                    required: "Email is required."
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   

            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function (form) {
                sendCredentials();
            }
        });

        $('.forget-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.forget-form').validate().form()) {
                    $('.forget-form').submit();
                }
                return false;
            }
        });

        jQuery('#forget-password').click(function () {
            jQuery('.login-form').hide();
            jQuery('.forget-form').show();
        });

        jQuery('#back-btn').click(function () {
            jQuery('.login-form').show();
            jQuery('.forget-form').hide();
        });

    }

    var sendCredentials = function () {

        var email = $("#txtForgetEmail").val();

        $.ajax({
            type: "POST",
            url: '/api/account/forgotYourPassword?email=' + email,
            content: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data) {
                    $("#txtEmail").val('');
                    //$("#mws-validate-error").hide();
                    var message = 'A senha foi enviada com sucesso.';
                    alert(message);
                    //$("#mws-validate-success").html(message).show();

                } else {
                    //$("#mws-validate-success").hide();
                    var message = 'Oops, email informado n\u00e3o est\u00e1 cadastrado.';
                    alert(message);
                    //$("#mws-validate-error").html(message).show();
                    //$("#mws-login").effect("shake", { distance: 6, times: 2 }, 35);
                }
            },
            error: function (request, status, error) {
                alert("Erro ... /Seguranca/ForgotYourPassword/");
            }
        });

    }

    var handleRegister = function () {

        function format(state) {
            if (!state.id) return state.text; // optgroup
            return "<img class='flag' src='../../assets/global/img/flags/" + state.id.toLowerCase() + ".png'/>&nbsp;&nbsp;" + state.text;
        }

        $('.register-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "",
            rules: {
                
                txtRegisterEmail: {
                    required: true,
                    email: true
                },
                txtRegisterPassword: {
                    required: true
                },
                rpassword: {
                    equalTo: "#txtRegisterPassword"
                },

                tnc: {
                    required: true
                }
            },

            messages: { // custom messages for radio buttons and checkboxes
                tnc: {
                    required: "Por favor aceite os Termos de Servi&ccedil;o e Pol&iacute;tica de Privacidade."
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   

            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                if (element.attr("name") == "tnc") { // insert checkbox errors after the container                  
                    error.insertAfter($('#register_tnc_error'));
                } else if (element.closest('.input-icon').size() === 1) {
                    error.insertAfter(element.closest('.input-icon'));
                } else {
                    error.insertAfter(element);
                }
            },

            submitHandler: function (form) {
                //form.submit();
                registerCredentials();
            }
        });

        $('.register-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.register-form').validate().form()) {
                    $('.register-form').submit();
                }
                return false;
            }
        });

        jQuery('#register-btn').click(function () {
            jQuery('.login-form').hide();
            jQuery('.register-form').show();
        });

        jQuery('#register-back-btn').click(function () {
            jQuery('.login-form').show();
            jQuery('.register-form').hide();
        });
    }

    var registerCredentials = function () {

        var email = $("#txtRegisterEmail").val();
        var senha = $("#txtRegisterPassword").val();
        
        $.ajax({
            type: "POST",
            url: '/api/account/register',
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: {
                'email': email,
                'senha': senha,
            },
            success: function (data) {

                if (data.succeeded) {
                    $("#login-form-alert-danger").hide();
                    window.location.href = returnUrl == '' ? '/' : returnUrl;
                } else {
                    var message = '';

                    for (var i = 0; i < data.errors.length; i++) {
                        message += data.errors[i];
                    }
                    $("#login-form-alert-danger").html(message).show();
                }
            },
            error: function (request, status, error) {
                alert("Erro ... /Seguranca/Register/");
            }
        });

    }

    return {
        //main function to initiate the module
        init: function () {

            returnUrl = getUrlParameter('ReturnUrl');

            handleLogin();
            handleForgetPassword();
            handleRegister();
        }

    };

}();