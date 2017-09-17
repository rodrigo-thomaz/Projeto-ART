var TipoEnderecoDetailScript = function () {

    var tipoEnderecoId;
    var tipoPessoa;

    var handleInit = function () {

        //Obtendo parametros
        tipoEnderecoId = getUrlParameter('id');
        tipoPessoa = parseInt(getUrlParameter('tipo'));

        //Setando default menu
        $("#menuTipoEndereco").addClass("active");

        //Setando os titulos

        var title = '';
        var subTitle = 'gerencie seus tipos de endere\u00e7o';

        if (tipoPessoa == 0) {
            title = "Tipo de endere\u00e7o - pessoa f\u00edsica";
            $("#menuTipoEnderecoPessoaFisica").addClass("active");
        }
        else if (tipoPessoa == 1) {
            title = "Tipo de endere\u00e7o - pessoa jur\u00eddica";
            $("#menuTipoEnderecoPessoaJuridica").addClass("active");
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
                        tipoEnderecoNomeExistente: true,
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
            window.location.href = "/TipoEndereco?tipoPessoa=" + tipoPessoa;
        });
    }

    var handleLoadForm = function () {

        if (tipoEnderecoId != null) {//Edit

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/tipoendereco/' + tipoEnderecoId + '/' + tipoPessoa,
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

        if (tipoEnderecoId == null) {

            var data = {
                'nome': nome,
                'ativo': ativo,
                'tipoPessoa': tipoPessoa,
            };

            $.ajax({
                type: "POST",
                url: ApplicationScript.getAppWebApiUrl() + '/api/tipoendereco',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/TipoEndereco?tipoPessoa=" + tipoPessoa;
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });

        }
        else {

            var data = {
                'tipoEnderecoId': tipoEnderecoId,
                'nome': nome,
                'ativo': ativo,
                'tipoPessoa': tipoPessoa,
            };

            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/tipoendereco',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/TipoEndereco?tipoPessoa=" + tipoPessoa;
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


jQuery.validator.addMethod("tipoEnderecoNomeExistente", function (value, element) {

    var tipoEnderecoId = getUrlParameter('id');
    var tipoPessoa = parseInt(getUrlParameter('tipo'));

    var url = ApplicationScript.getAppWebApiUrl();

    if (tipoEnderecoId == null) {
        url = url + "/api/tipoendereco/tipoPessoa/" + tipoPessoa + "/uniqueNome/" + value;
    }
    else {
        url = url + "/api/tipoendereco/" + tipoEnderecoId + "/tipoPessoa/" + tipoPessoa + "/uniqueNome/" + value;
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

}, "J\u00e1 existe um tipo de endere&ccedil;o cadastrado com este nome.");
