var ContaCartaoCreditoDetailScript = function () {

    var contaId;

    var handleInit = function () {

        //Obtendo parametros
        contaId = parseInt(getUrlRouteParameter(3));

        //Setando os titulos

        var title = 'Conta - Cart\u00e3o de cr\u00e9dito';
        var subTitle = 'gerencie seus cart\u00f5es de cr\u00e9dito';

        setAppTitleSubTitle(title, subTitle);

        //Setando default menu
        $("#menuConta").addClass("active");

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
                    cmbBandeiraCartao: {
                        required: true,
                    },
                    txtNome: {
                        required: true,
                        maxlength: 100,
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
            window.location.href = "/Conta/Index";
        });
    }

    var handleLoadForm = function () {

        if (contaId > 0) {//Edit

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/conta/cartaocredito/' + contaId,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {

                $("#cmbBandeiraCartao").val(data.bandeiraCartaoId);
                BandeiraCartaoSharedScript.init('cmbBandeiraCartao', false);
                
                if (data.contaCorrenteId != null) {
                    var cmbContaCorrenteVal = data.contaCorrenteId + ',1';
                    $("#cmbContaCorrente").val(cmbContaCorrenteVal);
                }
                ContaSharedScript.init('cmbContaCorrente');

                $("#txtNome").val(data.nome);

                if (data.ativo) {
                    $('input[name=chkAtivo][value=true]').parent().addClass('checked');
                    $('input[name=chkAtivo][value=false]').parent().removeClass('checked');
                }
                else {
                    $('input[name=chkAtivo][value=true]').parent().removeClass('checked');
                    $('input[name=chkAtivo][value=false]').parent().addClass('checked');
                }
                $('#txtDescricao').code(data.descricao);

            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {//New

            BandeiraCartaoSharedScript.init('cmbBandeiraCartao');
            ContaSharedScript.init('cmbContaCorrente');

            $('input[name=chkAtivo][value=true]').parent().addClass('checked');
            $('input[name=chkAtivo][value=false]').parent().removeClass('checked');
        }
    }

    var saveForm = function () {

        var bandeiraCartaoId = $("#cmbBandeiraCartao").select2("val");
        var nome = $("#txtNome").val();
        var ativo = $('input[name=chkAtivo][value=true]').parent().hasClass('checked');
        var descricao = $('#txtDescricao').code();
        var contaCorrenteId = $("#cmbContaCorrente").select2("val").split(',')[0];
        
        var data = {
            'contaId': contaId,
            'bandeiraCartaoId': bandeiraCartaoId,
            'contaCorrenteId': contaCorrenteId,
            'nome': nome,
            'ativo': ativo,
            'descricao': descricao,
        };

        if (contaId == 0) {
            $.ajax({
                type: "POST",
                url: ApplicationScript.getAppWebApiUrl() + '/api/conta/cartaocredito',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Conta/Index";
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {
            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/conta/cartaocredito',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/Conta/Index";
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