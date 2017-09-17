var PlanoContaDetailScript = function () {

    var planoContaId;
    var tipoTransacao;
    var parentId;

    var handleInit = function () {

        //Obtendo parametros
        planoContaId = parseInt(getUrlRouteParameter(3));
        tipoTransacao = parseInt(getUrlRouteParameter(4));
        parentId = parseInt(getUrlRouteParameter(5));        
        parentId = parentId > 0 ? parentId : null;

        //Setando default menu
        $("#menuPlanoContas").addClass("active");

        //Setando os titulos

        var title = '';
        var subTitle = 'gerencie seu plano de conta';

        if (tipoTransacao == 0) {
            title = "Plano de contas - contas a receber";
            $("#menuPlanoContaContaReceber").addClass("active");
        }
        else if (tipoTransacao == 1) {
            title = "Plano de contas - contas a pagar";
            $("#menuPlanoContaContaPagar").addClass("active");
        }

        setAppTitleSubTitle(title, subTitle);
       
        
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
                        nomeExistente: true,
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
            window.location.href = "/Planoconta/?tipotransacao=" + tipoTransacao;
        });

    }

    var handleLoadForm = function () {

        if (planoContaId > 0) {//Edit

            $.ajax({
                type: "GET",
                url: ApplicationScript.getAppWebApiUrl() + '/api/planoconta/' + planoContaId + '/tipotransacao/' + tipoTransacao,
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
            }).success(function (data, textStatus, jqXHR) {

                $("#txtNome").val(data.nome);
                $('#txtNome').prop('readonly', true);

                $('#txtDescricao').code(data.descricao);

                if (data.ativo) {
                    $('input[name=chkAtivo][value=true]').parent().addClass('checked');
                    $('input[name=chkAtivo][value=false]').parent().removeClass('checked');
                }
                else {
                    $('input[name=chkAtivo][value=true]').parent().removeClass('checked');
                    $('input[name=chkAtivo][value=false]').parent().addClass('checked');
                }

                $("#cmbPlanoConta").val(data.parentId);
                PlanoContaSharedScript.init('cmbPlanoConta', tipoTransacao, data.parent);

            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }
        else {//New  

            $('#txtNome').prop('readonly', false);

            $('input[name=chkAtivo][value=true]').parent().addClass('checked');
            $('input[name=chkAtivo][value=false]').parent().removeClass('checked');

            $("#cmbPlanoConta").val(parentId);
            PlanoContaSharedScript.init('cmbPlanoConta', tipoTransacao);
        }
    }

    var saveForm = function () {

        var nome = $("#txtNome").val();
        var ativo = $('input[name=chkAtivo][value=true]').parent().hasClass('checked');
        var descricao = $('#txtDescricao').code();
        var parentId = $("#cmbPlanoConta").select2("val");

        if (planoContaId == 0) {

            var data = {
                'tipoTransacao': tipoTransacao,
                'parentId': parentId,                
                'nome': nome,
                'ativo': ativo,
                'descricao': descricao,
            };

            $.ajax({
                type: "POST",
                url: ApplicationScript.getAppWebApiUrl() + '/api/planoconta',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/PlanoConta/Index/?tipotransacao=" + tipoTransacao;
            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });

        }
        else {

            var data = {
                'planoContaId': planoContaId,
                'tipoTransacao': tipoTransacao,
                'parentId': parentId,                
                'nome': nome,
                'ativo': ativo,
                'descricao': descricao,
            };

            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/planoconta',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: data,
            }).success(function (data, textStatus, jqXHR) {
                window.location.href = "/PlanoConta/Index/?tipotransacao=" + tipoTransacao;
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

jQuery.validator.addMethod("nomeExistente", function (value, element) {

    var planoContaId = parseInt(getUrlRouteParameter(3));
    var tipoTransacao = parseInt(getUrlRouteParameter(4));

    if (planoContaId > 0) {//Edit (Não valida)
        return true;
    }

    var result = false;

    $.ajax({
        type: "GET",
        url: "/api/planoconta/" + tipoTransacao + "/uniqueNome/" + value,
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

}, "J\u00e1 existe um plano de conta cadastrado com este nome.");


