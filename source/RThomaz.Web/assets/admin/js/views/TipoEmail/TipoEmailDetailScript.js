var TipoEmailDetailScript = function () {

    var tipoEmailId;
    var tipoPessoa;

    var handleInit = function () {

        //Obtendo parametros
        tipoEmailId = getUrlParameter('id');
        tipoPessoa = parseInt(getUrlParameter('tipo'));
        
        //Setando default menu
        $("#menuTipoEmail").addClass("active");

        //Setando os titulos

        var title = '';
        var subTitle = 'gerencie seus tipos de email';

        if (tipoPessoa == 0) {
            title = "Tipo de email - pessoa f\u00edsica";
            $("#menuTipoEmailPessoaFisica").addClass("active");
        }
        else if (tipoPessoa == 1) {
            title = "Tipo de email - pessoa jur\u00eddica";
            $("#menuTipoEmailPessoaJuridica").addClass("active");
        }
                
        setAppTitleSubTitle(title, subTitle);
        
        $('#divCaption').html('<i class="fa fa-reorder"></i> ' + title);


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
                    txtNome: {
                        required: true,
                        maxlength: 255,
                        tipoEmailNomeExistente: true,
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

        $("#btnCancelar").on("click", function () {
            window.location.href = "/TipoEmail?tipoPessoa=" + tipoPessoa;
        });
    }

    var handleLoadForm = function () {

        if (tipoEmailId != null) {//Edit

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/tipoemail/' + tipoEmailId + '/' + tipoPessoa,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {
                $("#txtNome").val(data.nome);

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
            $('input[name=chkAtivo][value=true]').parent().addClass('checked');
            $('input[name=chkAtivo][value=false]').parent().removeClass('checked');
        }
    }

    var saveForm = function () {

        var nome = $("#txtNome").val();
        var ativo = $('input[name=chkAtivo][value=true]').parent().hasClass('checked');

        if (tipoEmailId == null) {

            var data = {
                'nome': nome,
                'ativo': ativo,
                'tipoPessoa': tipoPessoa,
            };

            $.ajax({
                type: "POST",
                url: ApplicationScript.getAppWebApiUrl() + '/api/tipoemail',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/TipoEmail?tipoPessoa=" + tipoPessoa;
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });

        }
        else {

            var data = {
                'tipoEmailId': tipoEmailId,
                'nome': nome,
                'ativo': ativo,
                'tipoPessoa': tipoPessoa,
            };

            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/tipoemail',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/TipoEmail?tipoPessoa=" + tipoPessoa;
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


jQuery.validator.addMethod("tipoEmailNomeExistente", function (value, element) {

    var tipoEmailId = getUrlParameter('id');
    var tipoPessoa = parseInt(getUrlParameter('tipo'));

    var url = ApplicationScript.getAppWebApiUrl();

    if (tipoEmailId == null) {
        url = url + "/api/tipoemail/tipoPessoa/" + tipoPessoa + "/uniqueNome/" + value;
    }
    else {
        url = url + "/api/tipoemail/" + tipoEmailId + "/tipoPessoa/" + tipoPessoa + "/uniqueNome/" + value;
    }

    var result = false;

    $.ajax({
        type: "GET",
        url: url,
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

}, "J\u00e1 existe um tipo de email cadastrado com este nome.");