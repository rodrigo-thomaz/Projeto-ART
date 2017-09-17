function zeroPad(num, places) {
    var zero = places - num.toString().length + 1;
    return Array(+(zero > 0 && zero)).join("0") + num;
}

function formatToLocalMoney(value) {
    var num = new NumberFormat();
    num.setInputDecimal(',');
    num.setNumber(value); // value is '-1000.247'
    num.setPlaces('2', false);
    num.setCurrencyValue('R$');
    num.setCurrency(true);
    num.setCurrencyPosition(num.LEFT_OUTSIDE);
    num.setNegativeFormat(num.LEFT_DASH);
    num.setNegativeRed(false);
    num.setSeparators(true, '.', '.');
    return num.toFormatted();
}

function formatToLocalMoney2(value) {
    value = value.toString().replace(',', '').replace('.', ',');
    if (value == 0) {
        return 'R$ 0,00';
    }
    else {
        return 'R$ ' + value;
    }    
}

function formatToLocalMoneyWithOutCurrency(value) {
    var num = new NumberFormat();
    num.setInputDecimal(',');
    num.setNumber(value); // value is '-1000.247'
    num.setPlaces('2', false);
    num.setCurrencyValue('R$');
    num.setCurrency(false);
    num.setCurrencyPosition(num.LEFT_OUTSIDE);
    num.setNegativeFormat(num.LEFT_DASH);
    num.setNegativeRed(false);
    num.setSeparators(true, '.', '.');
    return num.toFormatted();
}

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
    return null;
};

var getUrlRouteParameter = function (index) {
    return window.location.pathname.split('/')[index];
};

var setAppTitleSubTitle = function (title, subTitle) {
    $(document).attr("title", title);
    $('#appTitle').html(title + ' <small>' + subTitle + '</small>');
};

var getListOfDias = function () {    
    var listOfDias = [];
    for (var i = 1; i <= 31; i++) {
        var display = '';
        if (i >= 29) {
            listOfDias.push({
                id: i,
                text: i + ' (ou último dia)',
            });
        }
        else {
            listOfDias.push({
                id: i,
                text: i.toString(),
            });
        }
    }
    return listOfDias;
};

var getListOfFrequencia = function () {
    var result = [];
    result.push({
        id: 0,
        text: 'Diariamente',
    });
    result.push({
        id: 1,
        text: 'Semanal',
    });
    result.push({
        id: 2,
        text: 'Mensal',
    });
    result.push({
        id: 3,
        text: 'Bimestral',
    });
    result.push({
        id: 4,
        text: 'Trimestral',
    });
    result.push({
        id: 5,
        text: 'Quadrimestral',
    });
    result.push({
        id: 6,
        text: 'Semestral',
    });
    result.push({
        id: 7,
        text: 'Anual',
    });
    return result;
};