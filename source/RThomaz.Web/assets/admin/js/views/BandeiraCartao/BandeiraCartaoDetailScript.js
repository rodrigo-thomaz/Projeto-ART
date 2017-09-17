var BandeiraCartaoDetailScript = function () {

    var bandeiraCartaoId;

    var handleInit = function () {

        //Obtendo parametros
        bandeiraCartaoId = parseInt(getUrlRouteParameter(3));

        var title = 'Bandeira cart\u00e3o';
        var subTitle = 'gerencie sua bandeira';

        setAppTitleSubTitle(title, subTitle);

        $("#menuBandeiraCartao").addClass("active");

        $("#formDetail").submit(function (e) {
            e.preventDefault(); //prevent default form submit            
        });

        $("#btnRemoverImagem").on("click", function () {
            $('#imgLogo').attr('src', "../../assets/admin/img/bank.svg");
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
                        bandeiraCartaoNomeExistente: true,
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
            window.location.href = "/BandeiraCartao";
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

        if (bandeiraCartaoId > 0) {//Edit

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/bandeiracartao/' + bandeiraCartaoId,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {

                $("#txtNome").val(data.nome);

                if (data.logoStorageObject) {
                    $('#imgLogo').attr('src', "/api/bandeiracartao/logo/" + data.logoStorageObject);
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
                saveBandeiraCartao(reader.result);
            };
            reader.readAsDataURL(fileLogo);
        }
        else {
            saveBandeiraCartao(null);
        }

    }

    var saveBandeiraCartao = function (logoAsDataURL) {

        var nome = $("#txtNome").val();
        var ativo = $('input[name=chkAtivo][value=true]').parent().hasClass('checked');
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

        if (bandeiraCartaoId == 0) {

            var data = {
                'nome': nome,
                'ativo': ativo,
            };

            if (storageUpload != null) {
                data.storageUpload = storageUpload;
            }

            $.ajax({
                type: "POST",
                url: ApplicationScript.getAppWebApiUrl() + '/api/bandeiracartao',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/BandeiraCartao/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });

        }
        else {

            var data = {
                'bandeiraCartaoId': bandeiraCartaoId,
                'nome': nome,
                'ativo': ativo,
                'logoStorageObject': logoStorageObject,
            };

            if (storageUpload != null) {
                data.storageUpload = storageUpload;
            }

            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/bandeiracartao',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/BandeiraCartao/Index";
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

jQuery.validator.addMethod("bandeiraCartaoNomeExistente", function (value, element) {

    var bandeiraCartaoId = parseInt(getUrlRouteParameter(3));

    var result = false;

    $.ajax({
        type: "GET",
        url: "/api/bandeiracartao/" + bandeiraCartaoId + "/uniqueNome/" + value,
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

}, "J\u00e1 existe uma bandeira de cart&atilde;o de cr&eacute;dito cadastrada com este nome.");