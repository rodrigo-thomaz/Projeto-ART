var PessoaFisicaDetailScript = function () {
    
    var pessoaId;

    var handleInit = function () {

        //Obtendo parametros
        pessoaId = parseInt(getUrlRouteParameter(3));

        var title = 'Pessoa f\u00edsica';
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
                    txtPrimeiroNome: {
                        required: true,
                        maxlength: 250,
                    },
                    txtNomeDoMeio: {
                        required: false,
                        maxlength: 250,
                    },
                    txtSobrenome: {
                        required: false,
                        maxlength: 250,
                    },
                    optSexo: {
                        required: true,
                    },
                    cmbEstadoCivil: {
                        required: false,
                    },
                    txtCPF: {
                        maxlength: 14,
                        required: false,
                        cpf: true,
                    },
                    txtRG: {
                        maxlength: 12,
                        required: false,
                    },
                    txtOrgaoEmissor: {
                        maxlength: 100,
                        required: false,
                    },
                    txtDataNascimento: {                        
                        required: false,
                    },
                    txtNaturalidade: {
                        maxlength: 100,
                        required: false,
                    },
                    txtNacionalidade: {
                        maxlength: 100,
                        required: false,
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

        $('#txtDataNascimento').datepicker({
            rtl: Metronic.isRTL(),
            autoclose: true,
            'language': 'pt-BR',
            'format': 'dd/mm/yyyy',
        });
        $('body').removeClass("modal-open"); // fix bug when inline picker is used in modal            

        $("#txtCPF").inputmask("mask", {
            "mask": "999.999.999-99"
        }); //specifying fn & options

        $("#txtRG").inputmask("mask", {
            "mask": "99.999.999[-]*",
        }); //specifying fn & options

        $("#cmbCBOOcupacao").on("change", function () {
            $("#cmbCBOSinonimo").select2("val", null);
            cmbCBOOcupacaoChange();
        });        
    }    

    var handleLoadForm = function () {

        if (pessoaId > 0) {//Edit

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/pessoa/fisica/' + pessoaId,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {

                $("#txtPrimeiroNome").val(data.primeiroNome);
                $("#txtNomeDoMeio").val(data.nomeDoMeio);
                $("#txtSobrenome").val(data.sobrenome);

                $("input[name=optSexo][value=" + data.sexo + "]").attr('checked', 'checked');
                $("input[name=optSexo][value=" + data.sexo + "]").parent().addClass("checked");

                $("#cmbEstadoCivil").val(data.estadoCivil);

                $("#txtCPF").val(data.cpf);
                $("#txtRG").val(data.rg);
                $("#txtOrgaoEmissor").val(data.orgaoEmissor);
                $("#txtDataNascimento").datepicker('update', data.dataNascimento);
                $("#txtNaturalidade").val(data.naturalidade);
                $("#txtNacionalidade").val(data.nacionalidade);

                $("#cmbCBOOcupacao").val(data.cboOcupacaoId);
                CBOOcupacaoSharedScript.init('cmbCBOOcupacao', data.cboOcupacao);

                $("#cmbCBOSinonimo").val(data.cboSinonimoId);
                CBOSinonimoSharedScript.init('cmbCBOSinonimo', data.cboSinonimo);

                cmbCBOOcupacaoChange();

                if (data.ativo) {
                    $('input[name=chkAtivo][value=true]').parent().addClass('checked');
                    $('input[name=chkAtivo][value=false]').parent().removeClass('checked');
                }
                else {
                    $('input[name=chkAtivo][value=true]').parent().removeClass('checked');
                    $('input[name=chkAtivo][value=false]').parent().addClass('checked');
                }

                $('#txtDescricao').code(data.descricao);

                PessoaEmailDetailScript.loadEmails(data.emails);
                PessoaEnderecoDetailScript.loadEnderecos(data.enderecos);
                PessoaHomePageDetailScript.loadHomePages(data.homePages);
                PessoaTelefoneDetailScript.loadTelefones(data.telefones);

            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {//New
            CBOOcupacaoSharedScript.init('cmbCBOOcupacao', null);
            CBOSinonimoSharedScript.init('cmbCBOSinonimo', null);
            cmbCBOOcupacaoChange();

            $('input[name=chkAtivo][value=true]').parent().addClass('checked');
            $('input[name=chkAtivo][value=false]').parent().removeClass('checked');            
        }        
    }   

    var saveForm = function () {

        var primeiroNome = $("#txtPrimeiroNome").val();
        var nomeDoMeio = $("#txtNomeDoMeio").val();
        var sobrenome = $("#txtSobrenome").val();
        var sexo = $("input:radio[name=optSexo]:checked").val();
        var estadoCivil = $("#cmbEstadoCivil").val();
        var cpf = $("#txtCPF").val().replace('.', "").replace('.', "").replace('-', '');
        var rg = $("#txtRG").val().replace('.','').replace('.','').replace('-','');
        var orgaoEmissor = $("#txtOrgaoEmissor").val();

        var dataNascimentoDateTime = new Date($("#txtDataNascimento").datepicker("getDate"));
        var dataNascimento = (dataNascimentoDateTime.getMonth() + 1) + '/' + dataNascimentoDateTime.getDate() + "/" + dataNascimentoDateTime.getFullYear();
        
        var naturalidade = $("#txtNaturalidade").val();
        var nacionalidade = $("#txtNacionalidade").val();
        var cboOcupacaoId = $("#cmbCBOOcupacao").select2("val");
        var cboSinonimoId = $("#cmbCBOSinonimo").select2("val");

        var ativo = $('input[name=chkAtivo][value=true]').parent().hasClass('checked');
        var descricao = $('#txtDescricao').code();

        var emails = PessoaEmailDetailScript.getEmails();
        var enderecos = PessoaEnderecoDetailScript.getEnderecos();
        var homePages = PessoaHomePageDetailScript.getHomePages();
        var telefones = PessoaTelefoneDetailScript.getTelefones();

        var data = {
            'pessoaId': pessoaId,
            'primeiroNome': primeiroNome,
            'nomeDoMeio': nomeDoMeio,
            'sobrenome': sobrenome,
            'sexo': sexo,
            'estadoCivil': estadoCivil,
            'cpf': cpf,
            'rg': rg,
            'orgaoEmissor': orgaoEmissor,
            'dataNascimento': dataNascimento,
            'naturalidade': naturalidade,
            'nacionalidade': nacionalidade,
            'cboOcupacaoId': cboOcupacaoId,
            'cboSinonimoId': cboSinonimoId,
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
                url: ApplicationScript.getAppWebApiUrl() + '/api/pessoa/fisica',
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
                url: ApplicationScript.getAppWebApiUrl() + '/api/pessoa/fisica',
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

    var cmbCBOOcupacaoChange = function () {
        var cboOcupacaoId = $("#cmbCBOOcupacao").select2("val");        
        if (cboOcupacaoId == '') {
            $("#cmbCBOSinonimo").prop("disabled", true);
        }
        else {
            $("#cmbCBOSinonimo").prop("disabled", false);
        }
        CBOSinonimoSharedScript.setCBOOcupacaoId(cboOcupacaoId);
    };

    return {
        init: function () {

            handleInit();
            handleLoadForm();

        },        
    };
}();

jQuery.validator.addMethod("cpf", function (value, element) {
    value = jQuery.trim(value);

    value = value.replace('.', '');
    value = value.replace('.', '');
    cpf = value.replace('-', '');
    while (cpf.length < 11) cpf = "0" + cpf;
    var expReg = /^0+$|^1+$|^2+$|^3+$|^4+$|^5+$|^6+$|^7+$|^8+$|^9+$/;
    var a = [];
    var b = new Number;
    var c = 11;
    for (i = 0; i < 11; i++) {
        a[i] = cpf.charAt(i);
        if (i < 9) b += (a[i] * --c);
    }
    if ((x = b % 11) < 2) { a[9] = 0 } else { a[9] = 11 - x }
    b = 0;
    c = 11;
    for (y = 0; y < 10; y++) b += (a[y] * c--);
    if ((x = b % 11) < 2) { a[10] = 0; } else { a[10] = 11 - x; }

    var retorno = true;
    if ((cpf.charAt(9) != a[9]) || (cpf.charAt(10) != a[10]) || cpf.match(expReg)) retorno = false;

    return this.optional(element) || retorno;

}, "Informe um CPF v&aacute;lido");

