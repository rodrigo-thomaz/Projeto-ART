var BancoDetailScript = function () {

    var bancoId;

    var handleInit = function () {

        //Obtendo parametros
        bancoId = parseInt(getUrlRouteParameter(3));

        //Setando os titulos

        var title = 'Bancos';
        var subTitle = 'gerencie seus bancos';

        setAppTitleSubTitle(title, subTitle);
        

        $("#menuBanco").addClass("active");

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
                        maxlength: 250,
                        bancoNomeExistente: true,
                    },
                    txtNomeAbreviado: {
                        required: true,
                        maxlength: 250,
                        bancoNomeAbreviadoExistente: true,
                    },
                    txtNumero: {
                        required: true,
                        maxlength: 15,
                        bancoNumeroExistente: true,
                    },
                    txtCodigoImportacaoOfx: {
                        required: false,
                        maxlength: 15,                        
                    },
                    txtSite: {
                        required: false,
                        maxlength: 500,
                    },
                    txtMascaraNumeroAgencia: {
                        maxlength: 20,
                    },
                    txtMascaraNumeroContaCorrente: {
                        maxlength: 20,
                    },
                    txtMascaraNumeroContaPoupanca: {
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
            window.location.href = "/Banco";
        });

        $("#fileLogo").on("change", function () {

            $("#txtLogoId").val(null);

            var fileLogo = $("#fileLogo")[0].files[0];
            if (fileLogo) {
                $("#btnRemoverImagem").show();
                $("#spnLogo").width("98px");
            }
            else {
                $("#btnRemoverImagem").hide();
                $("#spnLogo").width("220px");                
            }
        });        

    }
      
    var handleLoadForm = function () {

        if (bancoId > 0) {//Edit

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/banco/' + bancoId,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {
                
                $("#txtNome").val(data.nome);
                $("#txtNomeAbreviado").val(data.nomeAbreviado);
                $("#txtNumero").val(data.numero);
                $("#txtCodigoImportacaoOfx").val(data.codigoImportacaoOfx);
                $("#txtSite").val(data.site);
                $("#txtMascaraNumeroAgencia").val(data.mascaraNumeroAgencia);
                $("#txtMascaraNumeroContaCorrente").val(data.mascaraNumeroContaCorrente);
                $("#txtMascaraNumeroContaPoupanca").val(data.mascaraNumeroContaPoupanca);
                $('#txtDescricao').code(data.descricao);

                if (data.logoStorageObject) {
                    $('#imgLogo').attr('src', "/api/banco/logo/" + data.logoStorageObject);
                    $("#btnRemoverImagem").show();
                    $("#txtLogoId").val(data.logoStorageObject);
                }
                else {
                    $('#imgLogo').attr('src', "../../assets/admin/img/bank.svg");
                    $("#btnRemoverImagem").hide();
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
            $('input[name=chkAtivo][value=true]').parent().addClass('checked');
            $('input[name=chkAtivo][value=false]').parent().removeClass('checked');
        }
    }

    var saveForm = function () {

        var fileLogo = $("#fileLogo")[0].files[0];

        if (fileLogo) {
            var reader = new FileReader();
            reader.onload = function (e) {
                saveBanco(reader.result);
            };
            reader.readAsDataURL(fileLogo);
        }
        else {
            saveBanco(null);
        }

    }

    var saveBanco = function (logoAsDataURL) {

        var nome = $("#txtNome").val();
        var nomeAbreviado = $("#txtNomeAbreviado").val();
        var numero = $("#txtNumero").val();
        var mascaraNumeroAgencia = $("#txtMascaraNumeroAgencia").val();
        var mascaraNumeroContaCorrente = $("#txtMascaraNumeroContaCorrente").val();
        var mascaraNumeroContaPoupanca = $("#txtMascaraNumeroContaPoupanca").val();
        var codigoImportacaoOfx = $("#txtCodigoImportacaoOfx").val();
        var site = $("#txtSite").val();
        var ativo = $('input[name=chkAtivo][value=true]').parent().hasClass('checked');
        var descricao = $('#txtDescricao').code();
        var logoStorageObject = $("#txtLogoId").val();        

        var storageUpload = null;

        if (logoAsDataURL != null) {
            var dataURLSplit = logoAsDataURL.toString().split([',']);
            var bufferBase64String = dataURLSplit[1];
            var contentType = dataURLSplit[0].split(';')[0].split(':')[1];
            storageUpload = {
                'contentType': contentType,
                'bufferBase64String': bufferBase64String,
            };
        }

        if (bancoId == 0) {           

            var data = {
                'nome': nome,
                'nomeAbreviado': nomeAbreviado,
                'numero': numero,
                'codigoImportacaoOfx': codigoImportacaoOfx,
                'site': site,
                'mascaraNumeroAgencia': mascaraNumeroAgencia,
                'mascaraNumeroContaCorrente': mascaraNumeroContaCorrente,
                'mascaraNumeroContaPoupanca': mascaraNumeroContaPoupanca,
                'ativo': ativo,
                'descricao': descricao,                
            };

            if(storageUpload != null){
                data.storageUpload = storageUpload;
            }

            $.ajax({
                type: "POST",
                url: ApplicationScript.getAppWebApiUrl() + '/api/banco',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Banco/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });

        }
        else {

            var data = {
                'bancoId': bancoId,
                'nome': nome,
                'nomeAbreviado': nomeAbreviado,
                'numero': numero,
                'codigoImportacaoOfx': codigoImportacaoOfx,
                'site': site,
                'mascaraNumeroAgencia': mascaraNumeroAgencia,
                'mascaraNumeroContaCorrente': mascaraNumeroContaCorrente,
                'mascaraNumeroContaPoupanca': mascaraNumeroContaPoupanca,
                'ativo': ativo,
                'descricao': descricao,
                'logoStorageObject': logoStorageObject,
            };

            if (storageUpload != null) {
                data.storageUpload = storageUpload;
            }

            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/banco',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Banco/Index";
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

jQuery.validator.addMethod("bancoNomeExistente", function (value, element) {

    var bancoId = parseInt(getUrlRouteParameter(3));

    var result = false;

    $.ajax({
        type: "GET",
        url: ApplicationScript.getAppWebApiUrl() + '/api/banco/' + bancoId + '/uniqueNome/' + value,
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

}, "J\u00e1 existe um banco cadastrado com este nome.");

jQuery.validator.addMethod("bancoNomeAbreviadoExistente", function (value, element) {

    var bancoId = parseInt(getUrlRouteParameter(3));

    var result = false;

    $.ajax({
        type: "GET",
        url: ApplicationScript.getAppWebApiUrl() + '/api/banco/' + bancoId + '/uniqueNomeAbreviado/' + value,
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

}, "J\u00e1 existe um banco cadastrado com este nome abreviado.");

jQuery.validator.addMethod("bancoNumeroExistente", function (value, element) {

    var bancoId = parseInt(getUrlRouteParameter(3));

    var result = false;

    $.ajax({
        type: "GET",
        url: ApplicationScript.getAppWebApiUrl() + '/api/banco/' + bancoId + '/uniqueNumero/' + value,
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

}, "J\u00e1 existe um banco cadastrado com este n\u00famero.");