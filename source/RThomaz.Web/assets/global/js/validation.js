//jQuery.extend(jQuery.validator.messages, {
//    required: "Este campo &eacute; requerido.",
//    remote: "Por favor, corrija este campo.",
//    email: "Por favor, forne&ccedil;a um endere&ccedil;o eletr&ocirc;nico v&aacute;lido.",
//    url: "Por favor, forne&ccedil;a uma URL v&aacute;lida.",
//    date: "Por favor, forne&ccedil;a uma data v&aacute;lida.",
//    dateISO: "Por favor, forne&ccedil;a uma data v&aacute;lida (ISO).",
//    dateDE: "Bitte geben Sie ein gültiges Datum ein.",
//    number: "Por favor, forne&ccedil;a um n&uacute;mero v&aacute;lida.",
//    numberDE: "Bitte geben Sie eine Nummer ein.",
//    digits: "Por favor, forne&ccedil;a somente d&iacute;gitos.",
//    creditcard: "Por favor, forne&ccedil;a um cart&atilde;o de cr&eacute;dito v&aacute;lido.",
//    equalTo: "Por favor, forne&ccedil;a o mesmo valor novamente.",
//    accept: "Por favor, forne&ccedil;a um valor com uma extens&atilde;o v&aacute;lida.",
//    maxlength: jQuery.validator.format("Por favor, forne&ccedil;a n&atilde;o mais que {0} caracteres."),
//    minlength: jQuery.validator.format("Por favor, forne&ccedil;a ao menos {0} caracteres."),
//    rangelength: jQuery.validator.format("Por favor, forne&ccedil;a um valor entre {0} e {1} caracteres de comprimento."),
//    range: jQuery.validator.format("Por favor, forne&ccedil;a um valor entre {0} e {1}."),
//    max: jQuery.validator.format("Por favor, forne&ccedil;a um valor menor ou igual a {0}."),
//    min: jQuery.validator.format("Por favor, forne&ccedil;a um valor maior ou igual a {0}.")
//});

jQuery.validator.addMethod("datePTBR", function (value) {
    return this.optional(element) || /^\d\d?\/\d\d?\/\d\d\d?\d?$/.test(value);
}, "Por favor, forne&ccedil;a uma data v&aacute;lida.");


function formInvalidDefaultHandler(validator, controlName) {
    var errors = validator.numberOfInvalids();
    if (errors) {
        var message = errors == 1 ? 'Oops, voc\u00ea esqueceu um campo que est\u00e1 destacado abaixo' : 'Oops, voc\u00ea esqueceu ' + errors + ' campos que est\u00e3o destacados abaixo';
        $("#" + controlName).children("span").html(message);
        $("#" + controlName).show();
    } else {
        $("#" + controlName).hide();
    }
}

//Fazendo um override no rule 'date' do jquery validator para alterar o formato
$(function () {
    $.validator.addMethod('date',
    function (value, element) {
        if (this.optional(element)) {
            return true;
        }
        var ok = true;
        try {
            $.datepicker.parseDate('dd/mm/yy', value);
        }
        catch (err) {
            ok = false;
        }
        return ok;
    });
    if ($(".datefield").length > 0) {
        $(".datefield").datepicker({ dateFormat: 'dd/mm/yy', changeYear: true });
    }
});