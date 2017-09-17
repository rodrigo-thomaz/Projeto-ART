var PersonalInfoScript = function () {

    var handleInit = function () {

        $("#formPersonalInfo").submit(function (e) {
            e.preventDefault(); //prevent default form submit            
        });

        // Validation
        if ($.validator) {
            $("#formPersonalInfo").validate({
                errorElement: 'span',
                errorClass: 'help-block',
                focusInvalid: false,
                //ignore: "",  // validate all fields including form hidden input
                rules: {
                    txtNomeExibicao: {
                        required: true,
                        maxlength: 250,
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
                    formInvalidDefaultHandler(validator, "formPersonalInfo_custom-validate-error");
                    Metronic.scrollTo($("#formPersonalInfo_custom-validate-error"), 1);
                },
                submitHandler: function (form) {
                    saveForm();
                }
            });
        }

    }

    var handleLoadForm = function () {

        $.ajax({
            type: "GET",
            url: ApplicationScript.getAppWebApiUrl() + '/api/perfil/personalinfo',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
        }).success(function (data, textStatus, jqXHR) {

            $("#txtNomeExibicao").val(data.nomeExibicao);
            $("#txtEmail").val(data.email);

        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });
    }

    var saveForm = function () {

        var usuarioId = $("#txtUsuarioId").val();
        var nomeExibicao = $("#txtNomeExibicao").val();

        var data = {
            'usuarioId': usuarioId,
            'nomeExibicao': nomeExibicao,
        };

        $.ajax({
            type: "PUT",
            url: ApplicationScript.getAppWebApiUrl() + '/api/perfil/personalinfo',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            data: data,
        }).success(function (data, textStatus, jqXHR) {
            window.location.href = "/Perfil/Account";
        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });
    }

    return {
        init: function () {

            handleInit();
            handleLoadForm();

        },
    };

}();