var PessoaJuridicaDetailScript = function () {
    
    var pessoaId;

    var handleInit = function () {

        //Obtendo parametros
        pessoaId = parseInt(getUrlRouteParameter(3));

        var title = 'Pessoa jur\u00eddica';
        var subTitle = 'gerencie seus contatos';

        setAppTitleSubTitle(title, subTitle);

        $("#menuPessoa").addClass("active");

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
                    txtNomeFantasia: {
                        required: true,
                        maxlength: 250,
                    },
                    txtRazaoSocial: {
                        required: true,
                        maxlength: 250,
                    },
                    txtCNPJ: {
                        required: false,
                        maxlength: 18,
                        cnpj: true,
                    },
                    txtInscricaoEstadual: {
                        required: false,
                        maxlength: 12,
                    },
                    txtInscricaoMunicipal: {
                        required: false,
                        maxlength: 20,
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
            window.location.href = "/Pessoa/Index";
        });       

        $("#txtCNPJ").inputmask("mask", {
            "mask": "99.999.999/9999-99"
        }); //specifying fn & options
    }

    var handleLoadForm = function () {

        if (pessoaId > 0) {//Edit

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/pessoa/juridica/' + pessoaId,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {

                $("#txtNomeFantasia").val(data.nomeFantasia);
                $("#txtRazaoSocial").val(data.razaoSocial);
                $("#txtCNPJ").val(data.cnpj);
                $("#txtInscricaoEstadual").val(data.inscricaoEstadual);
                $("#txtInscricaoMunicipal").val(data.inscricaoMunicipal);
                $('#txtDescricao').code(data.descricao);

                if (data.ativo) {
                    $('input[name=chkAtivo][value=true]').parent().addClass('checked');
                    $('input[name=chkAtivo][value=false]').parent().removeClass('checked');
                }
                else {
                    $('input[name=chkAtivo][value=true]').parent().removeClass('checked');
                    $('input[name=chkAtivo][value=false]').parent().addClass('checked');
                }

                PessoaEmailDetailScript.loadEmails(data.emails);
                PessoaEnderecoDetailScript.loadEnderecos(data.enderecos);
                PessoaHomePageDetailScript.loadHomePages(data.homePages);
                PessoaTelefoneDetailScript.loadTelefones(data.telefones);

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

        var nomeFantasia = $("#txtNomeFantasia").val();
        var razaoSocial = $("#txtRazaoSocial").val();
        var cnpj = $("#txtCNPJ").val();
        var cnpj = $("#txtCNPJ").val().replace('.', "").replace('.', "").replace('/', '').replace('-', '');
        var inscricaoEstadual = $("#txtInscricaoEstadual").val();
        var inscricaoMunicipal = $("#txtInscricaoMunicipal").val();
        var ativo = $('input[name=chkAtivo][value=true]').parent().hasClass('checked');
        var descricao = $('#txtDescricao').code();

        var emails = PessoaEmailDetailScript.getEmails();
        var enderecos = PessoaEnderecoDetailScript.getEnderecos();
        var homePages = PessoaHomePageDetailScript.getHomePages();
        var telefones = PessoaTelefoneDetailScript.getTelefones();

        var data = {
            'pessoaId': pessoaId,
            'nomeFantasia': nomeFantasia,
            'razaoSocial': razaoSocial,
            'cnpj': cnpj,
            'inscricaoEstadual': inscricaoEstadual,
            'inscricaoMunicipal': inscricaoMunicipal,
            'ativo': ativo,
            'descricao': descricao,
            'emails': emails,
            'enderecos': enderecos,
            'homePages': homePages,
            'telefones': telefones,
        };

        if (pessoaId == 0) {
            $.ajax({
                type: "POST",
                url: ApplicationScript.getAppWebApiUrl() + '/api/pessoa/juridica',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Pessoa/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {
            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/pessoa/juridica',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Pessoa/Index";
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

jQuery.validator.addMethod("cnpj", function (value, element) {
    value = jQuery.trim(value);

    // DEIXA APENAS OS NÚMEROS
    value = value.replace('/', '');
    value = value.replace('.', '');
    value = value.replace('.', '');
    value = value.replace('-', '');

    var numeros, digitos, soma, i, resultado, pos, tamanho, digitos_iguais;
    digitos_iguais = 1;

    if (value.length < 14 && value.length < 15) {
        return this.optional(element) || false;
    }
    for (i = 0; i < value.length - 1; i++) {
        if (value.charAt(i) != value.charAt(i + 1)) {
            digitos_iguais = 0;
            break;
        }
    }

    if (!digitos_iguais) {
        tamanho = value.length - 2
        numeros = value.substring(0, tamanho);
        digitos = value.substring(tamanho);
        soma = 0;
        pos = tamanho - 7;

        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2) {
                pos = 9;
            }
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0)) {
            return this.optional(element) || false;
        }
        tamanho = tamanho + 1;
        numeros = value.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2) {
                pos = 9;
            }
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1)) {
            return this.optional(element) || false;
        }
        return this.optional(element) || true;
    } else {
        return this.optional(element) || false;
    }

}, "Informe um CNPJ v&aacute;lido");

