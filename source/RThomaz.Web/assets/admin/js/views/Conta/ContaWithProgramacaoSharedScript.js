var ContaWithProgramacaoSharedScript = function () {

    var handleInit = function (controlId) {

        $.ajax({
            type: "GET",
            url: ApplicationScript.getAppWebApiUrl() + '/api/conta/selectViewListWithProgramacao',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
        }).success(function (data, textStatus, jqXHR) {

            var dataSource = [];

            var contaEspecieGroup = {
                tipoConta: 0,
                text: 'Conta espécie',
                children: [],
            };
            var contaCorrenteGroup = {
                tipoConta: 1,
                text: 'Conta corrente',
                children: [],
            };
            var contaPoupancaGroup = {
                tipoConta: 2,
                text: 'Conta poupança',
                children: [],
            };
            var contaCartaoCreditoGroup = {
                tipoConta: 3,
                text: 'Conta cartão de crédito',
                children: [],
            };            

            for (var i = 0; i < data.length; i++) {
                switch (data[i].model.tipoConta) {
                    case 0:
                        contaEspecieGroup.children.push(convertToSelect2Model(data[i]));
                        break;
                    case 1:
                        contaCorrenteGroup.children.push(convertToSelect2Model(data[i]));
                        break;
                    case 2:
                        contaPoupancaGroup.children.push(convertToSelect2Model(data[i]));
                        break;
                    case 3:
                        contaCartaoCreditoGroup.children.push(convertToSelect2Model(data[i]));
                        break;
                    default:
                        break;
                }
            } 

            if (contaEspecieGroup && contaEspecieGroup.children.length > 0)
                dataSource.push(contaEspecieGroup);
            if (contaCorrenteGroup && contaCorrenteGroup.children.length > 0)
                dataSource.push(contaCorrenteGroup);
            if (contaPoupancaGroup && contaPoupancaGroup.children.length > 0)
                dataSource.push(contaPoupancaGroup);
            if (contaCartaoCreditoGroup &&contaCartaoCreditoGroup.children.length > 0)
                dataSource.push(contaCartaoCreditoGroup);
            
            var cmbContaVal = $('#' + controlId).val();
            var id = cmbContaVal.split(',')[0];

            $('#' + controlId).select2(
            {
                placeholder: 'Selecione',
                minimumInputLength: 0,
                allowClear: true,
                formatSelection: formatSelection,
                formatResult: formatResult,                
                data: dataSource,
            }).select2('val', id);

            $('#' + controlId).on("change", function () {
                var formDetail = $("#formDetail");
                if ($("#formDetail").length) {
                    var validator = formDetail.validate();
                    validator.element($(this));
                }
            });

        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });

    }

    var convertToSelect2Model = function (item) {
        return {
            id: item.model.contaId,
            tipoConta: item.model.tipoConta,
            model: item.model,
            count: item.count,
        };
    }    

    var formatSelection = function (item) {

        if (!item.hasOwnProperty('id')) {//Group
            return item.text;
        }
        else {//Conta

            var itemTemplate = "";

            switch (item.model.tipoConta) {
                case 0:
                    itemTemplate = formatContaEspecieResult(item);
                    break;
                case 1:
                    itemTemplate = formatContaCorrenteResult(item);
                    break;
                case 2:
                    itemTemplate = formatContaPoupancaResult(item);
                    break;
                case 3:
                    itemTemplate = formatContaCartaoCreditoResult(item);
                    break;
                default:
                    break;
            }

            return itemTemplate;
        }
    }

    var formatResult = function (item) {

        if (!item.hasOwnProperty('id')) {//Group
            return item.text;
        }
        else {//Conta

            var itemTemplate = "";

            switch (item.model.tipoConta) {
                case 0:
                    itemTemplate = formatContaEspecieResult(item);
                    break;
                case 1:
                    itemTemplate = formatContaCorrenteResult(item);
                    break;
                case 2:
                    itemTemplate = formatContaPoupancaResult(item);
                    break;
                case 3:
                    itemTemplate = formatContaCartaoCreditoResult(item);
                    break;
                default:
                    break;
            }

            return itemTemplate;
        }
    }

    var formatContaEspecieResult = function (item) {

        var text = item.model.nome;
        var src = "/assets/admin/img/bank.svg";

        var itemTemplate = "";

        itemTemplate += "<div class='row'>";
        itemTemplate += "	<div class='col-md-12 user-info'>";
        itemTemplate += "		<img alt='' src='" + src + "' class='img-responsive' style='width:45px; height:45px;'>";
        itemTemplate += "		<div>";
        itemTemplate += "			<div>";
        itemTemplate += "				<span>";
        itemTemplate += text + "&nbsp;</span>";
        itemTemplate += "				<a href='#' class='badge badge-success pull-right'>";
        itemTemplate += item.count + "</a>";
        itemTemplate += "			</div>";
        itemTemplate += "			<div>";
        itemTemplate += '<span class="badge badge-success pull-right">Conta espécie</span>';
        itemTemplate += "			</div>";
        itemTemplate += "		</div>";
        itemTemplate += "	</div>";
        itemTemplate += "</div>";

        return itemTemplate;
    }

    var formatContaCorrenteResult = function (item) {

        text = 'Banco: ' + item.model.banco.nome + ' Ag: ' + item.model.dadoBancario.numeroAgencia + ' C/C: ' + item.model.dadoBancario.numeroConta;
        if (item.model.banco.logoStorageObject) {
            src = "/api/banco/logo/" + item.model.banco.logoStorageObject;
        }
        else {
            src = "/assets/admin/img/bank.svg";
        }

        var itemTemplate = "";

        itemTemplate += "<div class='row'>";
        itemTemplate += "	<div class='col-md-12 user-info'>";
        itemTemplate += "		<img alt='' src='" + src + "' class='img-responsive' style='width:45px; height:45px;'>";
        itemTemplate += "		<div>";
        itemTemplate += "			<div>";
        itemTemplate += "				<span>";
        itemTemplate += item.model.banco.nome + "&nbsp;</span>";
        itemTemplate += "				<a href='#' class='badge badge-success pull-right'>";
        itemTemplate += item.count + "</a>";
        itemTemplate += "			</div>";
        itemTemplate += "			<div>";
        itemTemplate += '<span>Ag: ' + item.model.dadoBancario.numeroAgencia + ' C/C: ' + item.model.dadoBancario.numeroConta + '</span><span class="badge badge-success pull-right">Conta corrente</span>';
        itemTemplate += "			</div>";
        itemTemplate += "		</div>";
        itemTemplate += "	</div>";
        itemTemplate += "</div>";

        return itemTemplate;
    }

    var formatContaPoupancaResult = function (item) {

        text = 'Banco: ' + item.model.banco.nome + ' Ag: ' + item.model.dadoBancario.numeroAgencia + ' C/C: ' + item.model.dadoBancario.numeroConta;
        if (item.model.banco.logoStorageObject) {
            src = "/api/banco/GetLogoStorage/" + item.model.banco.logoStorageObject;
        }
        else {
            src = "/assets/admin/img/bank.svg";
        }

        var itemTemplate = "";

        itemTemplate += "<div class='row'>";
        itemTemplate += "	<div class='col-md-12 user-info'>";
        itemTemplate += "		<img alt='' src='" + src + "' class='img-responsive' style='width:45px; height:45px;'>";
        itemTemplate += "		<div>";
        itemTemplate += "			<div>";
        itemTemplate += "				<span>";
        itemTemplate += item.model.banco.nome + "&nbsp;</span>";
        itemTemplate += "				<a href='#' class='badge badge-success pull-right'>";
        itemTemplate += item.count + "</a>";
        itemTemplate += "			</div>";
        itemTemplate += "			<div>";
        itemTemplate += '<span>Ag: ' + item.model.dadoBancario.numeroAgencia + ' C/C: ' + item.model.dadoBancario.numeroConta + '</span><span class="badge badge-success pull-right">Conta poupaça</span>';
        itemTemplate += "			</div>";
        itemTemplate += "		</div>";
        itemTemplate += "	</div>";
        itemTemplate += "</div>";

        return itemTemplate;
    }

    var formatContaCartaoCreditoResult = function (item) {

        text = 'Bandeira: ' + item.model.bandeiraCartao.nome + ' Nome: ' + item.model.nome;
        if (item.model.bandeiraCartao.logoStorageObject) {
            src = "/api/bandeiracartao/logo/" + item.model.bandeiraCartao.logoStorageObject;
        }
        else {
            src = "/assets/admin/img/bank.svg";
        }

        var itemTemplate = "";

        itemTemplate += "<div class='row'>";
        itemTemplate += "	<div class='col-md-12 user-info'>";
        itemTemplate += "		<img alt='' src='" + src + "' class='img-responsive' style='width:45px; height:45px;'>";
        itemTemplate += "		<div>";
        itemTemplate += "			<div>";
        itemTemplate += "				<span>";
        itemTemplate += item.model.bandeiraCartao.nome + "&nbsp;</span>";
        itemTemplate += "				<a href='#' class='badge badge-success pull-right'>";
        itemTemplate += item.count + "</a>";
        itemTemplate += "			</div>";
        itemTemplate += "			<div>";
        itemTemplate += ' Nome: ' + item.model.nome + '</span><span class="badge badge-success pull-right">Cartão de crédito</span>';
        itemTemplate += "			</div>";
        itemTemplate += "		</div>";
        itemTemplate += "	</div>";
        itemTemplate += "</div>";

        return itemTemplate;
    }

    return {
        init: function (controlId) {
            handleInit(controlId);
        },
    };

}();