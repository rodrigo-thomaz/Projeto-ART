var PessoaEnderecoDetailScript = function () {

    var _tipoPessoa;
    var _newPessoaEnderecoId = -1;

    var _buscaEnderecoAutocomplete;

    var handleInit = function (tipoPessoa) {

        _tipoPessoa = tipoPessoa;

        $("#btnNewEndereco").on("click", function () {

            var place = _buscaEnderecoAutocomplete.getPlace();

            var addressComponents = convertAddressComponents(place);

            handleInsertEndereco(addressComponents.cep, addressComponents.logradouro, addressComponents.numero, addressComponents.bairroNome, addressComponents.bairroNomeAbreviado, addressComponents.cidadeNome, addressComponents.cidadeNomeAbreviado, addressComponents.estadoNome, addressComponents.estadoSigla, addressComponents.paisNome, addressComponents.paisISO2, addressComponents.latitude, addressComponents.longitude);

            $("#txtBuscaEndereco").val("");
        });        
    }

    var convertAddressComponents = function (place) {

        var cep = '';
        var numero = '';

        var logradouro = '';
        var logradouroAbreviado = '';

        var complemento = '';

        var bairroNome = '';
        var bairroNomeAbreviado = '';

        var cidadeNome = '';
        var cidadeNomeAbreviado = '';

        var estadoNome = '';
        var estadoSigla = '';

        var paisNome = '';
        var paisISO2 = '';

        // Get each component of the address from the place details
        // and fill the corresponding field on the form.
        for (var i = 0; i < place.address_components.length; i++) {
            var addressType = place.address_components[i].types[0];
            if (addressType == 'street_number') {
                numero = place.address_components[i]['short_name'];
            }
            else if (addressType == 'postal_code') {
                cep = place.address_components[i]['short_name'];
            }
            else if (addressType == 'route') {
                logradouro = place.address_components[i]['long_name'];
                logradouroAbreviado = place.address_components[i]['short_name'];
            }
            else if (addressType == 'sublocality_level_1') {
                bairroNome = place.address_components[i]['long_name'];
                bairroNomeAbreviado = place.address_components[i]['short_name'];
            }
            else if (addressType == 'locality') {
                cidadeNome = place.address_components[i]['long_name'];
                cidadeNomeAbreviado = place.address_components[i]['short_name'];
            }
            else if (addressType == 'administrative_area_level_1') {
                estadoNome = place.address_components[i]['long_name'];
                estadoSigla = place.address_components[i]['short_name'];
            }
            else if (addressType == 'country') {
                var paisNome = place.address_components[i]['long_name'];
                var paisISO2 = place.address_components[i]['short_name'];
            }
        }

        var location = place.geometry.location;

        return {
            cep: cep,
            numero: numero,
            logradouro: logradouro,
            logradouroAbreviado: logradouroAbreviado,
            complemento: complemento,
            bairroNome: bairroNome,
            bairroNomeAbreviado: bairroNomeAbreviado,
            cidadeNome: cidadeNome,
            cidadeNomeAbreviado: cidadeNomeAbreviado,
            estadoNome: estadoNome,
            estadoSigla: estadoSigla,
            paisNome: paisNome,
            paisISO2: paisISO2,
            latitude: location.lat(),
            longitude: location.lng(),
        };
    };

    var handleInitMap = function () {

        var map = new google.maps.Map(document.getElementById('map'), {
            center: { lat: -23.5505199, lng: -46.63330940000003 },
            zoom: 13
        });

        _buscaEnderecoAutocomplete = new google.maps.places.Autocomplete(
        /** @type {!HTMLInputElement} */(document.getElementById('txtBuscaEndereco')),
        { types: ['geocode'] });

        _buscaEnderecoAutocomplete.bindTo('bounds', map);

        var infowindow = new google.maps.InfoWindow();
        var marker = new google.maps.Marker({
            map: map,
            anchorPoint: new google.maps.Point(0, -29)
        });

        _buscaEnderecoAutocomplete.addListener('place_changed', function () {

            infowindow.close();
            marker.setVisible(false);
            var place = _buscaEnderecoAutocomplete.getPlace();
            if (!place.geometry) {
                window.alert("Autocomplete's returned place contains no geometry");
                return;
            }

            // If the place has a geometry, then present it on a map.
            if (place.geometry.viewport) {
                map.fitBounds(place.geometry.viewport);
            } else {
                map.setCenter(place.geometry.location);
                map.setZoom(17);  // Why 17? Because it looks good.
            }
            marker.setIcon(/** @type {google.maps.Icon} */({
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(35, 35)
            }));
            marker.setPosition(place.geometry.location);
            marker.setVisible(true);            

            var addressComponents = convertAddressComponents(place);

            var contentMap = formatMapAddress(addressComponents.cep, addressComponents.logradouroAbreviado, addressComponents.numero, addressComponents.bairroNomeAbreviado, addressComponents.cidadeNomeAbreviado, addressComponents.estadoSigla, addressComponents.paisISO2);

            infowindow.setContent(contentMap);
            infowindow.open(map, marker);

        });

        geolocate();
    };

    function geolocate() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                var geolocation = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                };
                var circle = new google.maps.Circle({
                    center: geolocation,
                    radius: position.coords.accuracy
                });
                _buscaEnderecoAutocomplete.setBounds(circle.getBounds());
            });
        }
    }

    var handleLoadEnderecos = function (enderecos) {

        $("#divEnderecos").append('');

        $.each(enderecos, function (index, value) {

            createEnderecoRow(value.pessoaEnderecoId, value.tipoEnderecoId, value.cep, value.logradouro, value.numero, value.complemento, value.bairroId, value.bairroNome, value.bairroNomeAbreviado, value.cidadeNome, value.cidadeNomeAbreviado, value.estadoNome, value.estadoSigla, value.paisNome, value.paisISO2, value.latitude, value.longitude);

        });

    };

    var handleInsertEndereco = function (cep, logradouro, numero, bairroNome, bairroNomeAbreviado, cidadeNome, cidadeNomeAbreviado, estadoNome, estadoSigla, paisNome, paisISO2, latitude, longitude) {

        var tipoEnderecoId = null;
        var bairroId = null;
        var complemento = '';

        if (bairroId == null) {
            $.ajax({
                type: "PUT",
                url: ApplicationScript.getAppWebApiUrl() + '/api/localidade',
                headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
                content: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                data: {
                    bairroNome: bairroNome,
                    bairroNomeAbreviado: bairroNomeAbreviado,
                    cidadeNome: cidadeNome,
                    cidadeNomeAbreviado: cidadeNomeAbreviado,
                    estadoNome: estadoNome,
                    estadoSigla: estadoSigla,
                    paisNome: paisNome,
                    paisISO2: paisISO2,
                },
            }).success(function (data, textStatus, jqXHR) {
                
                createEnderecoRow(_newPessoaEnderecoId, tipoEnderecoId, cep, logradouro, numero, complemento, data.bairroId, bairroNome, bairroNomeAbreviado, cidadeNome, cidadeNomeAbreviado, estadoNome, estadoSigla, paisNome, paisISO2, latitude, longitude);

                _newPessoaEnderecoId--;

            }).error(function (jqXHR, textStatus, errorThrown) {
                ApplicationScript.error(jqXHR, textStatus, errorThrown);
            });
        }        
    };

    var handleRemoveEndereco = function (pessoaEnderecoId) {
        var control = $("#divEnderecos [data-pessoaenderecoid='" + pessoaEnderecoId + "']");
        $(control).remove();
    };

    var handleGetEnderecos = function () {

        var enderecoRows = $("#divEnderecos .endereco");

        var result = [];

        $.each(enderecoRows, function (index, value) {

            var pessoaEnderecoId = $(value).data().pessoaenderecoid;

            var tipoEnderecoId = $('#cmbTipoEndereco' + pessoaEnderecoId).val();
            var cep = $('#txtCep' + pessoaEnderecoId).val();
            var logradouro = $('#txtLogradouro' + pessoaEnderecoId).val();
            var numero = $('#txtNumero' + pessoaEnderecoId).val();
            var complemento = $('#txtComplemento' + pessoaEnderecoId).val();
            var bairroData = $('#txtBairro' + pessoaEnderecoId).data();
            var cidadeData = $('#txtCidade' + pessoaEnderecoId).data();
            var estadoData = $('#txtEstado' + pessoaEnderecoId).data();
            var paisData = $('#txtPais' + pessoaEnderecoId).data();

            var mapEnderecoData = $('#mapEndereco' + pessoaEnderecoId).data();
            
            if (pessoaEnderecoId < 0) {
                pessoaEnderecoId = 0;
            }

            result.push({
                'PessoaEnderecoId': pessoaEnderecoId,
                'TipoEnderecoId': tipoEnderecoId,
                'Cep': cep,
                'Logradouro': logradouro,
                'Numero': numero,
                'Complemento': complemento,
                'BairroId': bairroData.bairroid,                                                      
                'Latitude': mapEnderecoData.latitude,
                'Longitude': mapEnderecoData.longitude,
            });
        });

        return result;
    };

    var createEnderecoRow = function (pessoaEnderecoId, tipoEnderecoId, cep, logradouro, numero, complemento, bairroId, bairroNome, bairroNomeAbreviado, cidadeNome, cidadeNomeAbreviado, estadoNome, estadoSigla, paisNome, paisISO2, latitude, longitude) {       
    
        if (logradouro == null) {
            logradouro = '';
        }
        if (numero == null) {
            numero = '';
        }
        if (cep == null) {
            cep = '';
        }
        if (complemento == null) {
            complemento = '';
        }
        if (bairroNome == null) {
            bairroNome = '';
        }
        if (bairroNomeAbreviado == null) {
            bairroNomeAbreviado = '';
        }

        var enderecoControl = '';

        var cmbTipoEnderecoNome = 'cmbTipoEndereco' + pessoaEnderecoId;
        var txtCepNome = 'txtCep' + pessoaEnderecoId;
        var txtLogradouroNome = 'txtLogradouro' + pessoaEnderecoId;
        var txtNumeroNome = 'txtNumero' + pessoaEnderecoId;
        var txtComplementoNome = 'txtComplemento' + pessoaEnderecoId;
        var txtBairroNome = 'txtBairro' + pessoaEnderecoId;
        var txtCidadeNome = 'txtCidade' + pessoaEnderecoId;
        var txtEstadoNome = 'txtEstado' + pessoaEnderecoId;
        var txtPaisNome = 'txtPais' + pessoaEnderecoId;
        var mapEnderecoNome = 'mapEndereco' + pessoaEnderecoId;

        var txtBuscaEnderecoNome = 'txtBuscaEndereco' + pessoaEnderecoId;
        
        enderecoControl += '<div class="endereco" data-pessoaenderecoid="' + pessoaEnderecoId + '">';        

        enderecoControl += '    <div class="row">';
        enderecoControl += '        <div class="col-md-3">';
        enderecoControl += '           <button class="btn btn-danger" type="button" onclick="PessoaEnderecoDetailScript.removeEndereco(' + pessoaEnderecoId + ');"><i class="fa fa-trash-o fa-fw"></i></button>';
        enderecoControl += '        </div>';
        enderecoControl += '        <!--/span-->';
        enderecoControl += '    </div>';

        enderecoControl += '    <div class="row">';
        enderecoControl += '        <div class="col-md-6 ">';
        enderecoControl += '            <div class="form-group">';
        enderecoControl += '                <label>Tipo de endere&ccedilo</label>';
        enderecoControl += '            <select id="' + cmbTipoEnderecoNome + '" name="' + cmbTipoEnderecoNome + '" class="form-control"></select>';
        enderecoControl += '            </div>';
        enderecoControl += '        </div>';
        enderecoControl += '        <!--/span-->';
        enderecoControl += '        <div class="col-md-3">';
        enderecoControl += '            <div class="form-group">';
        enderecoControl += '                <label>Cep</label>';
        enderecoControl += '                <input id="' + txtCepNome + '" name="' + txtCepNome + '" type="text" class="form-control" readonly value="' + cep + '">';
        enderecoControl += '            </div>';
        enderecoControl += '        </div>';
        enderecoControl += '        <!--/span-->';
        enderecoControl += '    </div>';

        enderecoControl += '    <div class="row">';
        enderecoControl += '        <div class="col-md-9">';
        enderecoControl += '            <div class="form-group">';
        enderecoControl += '                <label>Logradouro</label>';
        enderecoControl += '                <input id="' + txtLogradouroNome + '" name="' + txtLogradouroNome + '" type="text" class="form-control" readonly value="' + logradouro + '">';
        enderecoControl += '            </div>';
        enderecoControl += '        </div>';
        enderecoControl += '        <!--/span-->';
        enderecoControl += '        <div class="col-md-3">';
        enderecoControl += '            <div class="form-group">';
        enderecoControl += '                <label>N&uacutemero</label>';
        enderecoControl += '                <input id="' + txtNumeroNome + '" name="' + txtNumeroNome + '" type="text" class="form-control" readonly value="' + numero + '">';
        enderecoControl += '            </div>';
        enderecoControl += '        </div>';
        enderecoControl += '        <!--/span-->';
        enderecoControl += '    </div>';

        enderecoControl += '    <div class="row">';
        enderecoControl += '        <div class="col-md-6">';
        enderecoControl += '            <div class="form-group">';
        enderecoControl += '                <label>Complemento</label>';
        enderecoControl += '                <input id="' + txtComplementoNome + '" name="' + txtComplementoNome + '" type="text" class="form-control" value="' + complemento + '">';
        enderecoControl += '            </div>';
        enderecoControl += '        </div>';
        enderecoControl += '        <!--/span-->';
        enderecoControl += '        <div class="col-md-6">';
        enderecoControl += '            <div class="form-group">';
        enderecoControl += '                <label>Bairro</label>';
        enderecoControl += '                <input id="' + txtBairroNome + '" name="' + txtBairroNome + '" type="text" class="form-control" readonly value="' + bairroNome + '" data-bairroid="' + bairroId + '" data-bairronome="' + bairroNome + '" data-bairronomeabreviado="' + bairroNomeAbreviado + '">';
        enderecoControl += '            </div>';
        enderecoControl += '        </div>';
        enderecoControl += '        <!--/span-->';
        enderecoControl += '    </div>';

        enderecoControl += '    <div class="row">';
        enderecoControl += '        <div class="col-md-6">';
        enderecoControl += '            <div class="form-group">';
        enderecoControl += '                <label>Cidade</label>';
        enderecoControl += '                <input id="' + txtCidadeNome + '" name="' + txtCidadeNome + '" type="text" class="form-control" readonly value="' + cidadeNome + '" data-cidadenome="' + cidadeNome + '" data-cidadenomeabreviado="' + cidadeNomeAbreviado + '">';
        enderecoControl += '            </div>';
        enderecoControl += '        </div>';
        enderecoControl += '        <!--/span-->';
        enderecoControl += '        <div class="col-md-3">';
        enderecoControl += '            <div class="form-group">';
        enderecoControl += '                <label>Estado</label>';
        enderecoControl += '                <input id="' + txtEstadoNome + '" name="' + txtEstadoNome + '" type="text" class="form-control" readonly value="' + estadoSigla + '" data-estadonome="' + estadoNome + '" data-estadosigla="' + estadoSigla + '">';
        enderecoControl += '            </div>';
        enderecoControl += '        </div>';
        enderecoControl += '        <!--/span-->';

        enderecoControl += '        <div class="col-md-3">';
        enderecoControl += '            <div class="form-group">';
        enderecoControl += '                <label>Pais</label>';
        enderecoControl += '                <input id="' + txtPaisNome + '" name="' + txtPaisNome + '" type="text" class="form-control" readonly value="' + paisNome + '" data-paisnome="' + paisNome + '" data-paisiso2="' + paisISO2 + '">';
        enderecoControl += '            </div>';
        enderecoControl += '        </div>';
        enderecoControl += '        <!--/span-->';

        enderecoControl += '    </div>';

        enderecoControl += '    <div class="row">';
        enderecoControl += '        <div class="col-md-12">';
        enderecoControl += '            <div class="form-group">';
        enderecoControl += '                <div id="' + mapEnderecoNome + '" name="' + mapEnderecoNome + '" data-latitude="' + latitude + '" data-longitude="' + longitude + '" style="height:400px;"></div>';
        enderecoControl += '            </div>';
        enderecoControl += '        </div>';
        enderecoControl += '    </div>                        ';

        enderecoControl += '</div>';


        $("#divEnderecos").append(enderecoControl);
        
        $.ajax({
            type: "GET",
            url: ApplicationScript.getAppWebApiUrl() + '/api/tipoendereco/selectViewList/' + _tipoPessoa,
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
        }).success(function (data, textStatus, jqXHR) {
            var options = $("#" + cmbTipoEnderecoNome);
            options.append($("<option />"));
            for (var i = 0; i < data.length; i++) {
                options.append($("<option />").val(data[i].tipoEnderecoId).text(data[i].nome));
            }
            if (tipoEnderecoId > 0) {
                options.val(tipoEnderecoId);
            }
        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });

        $("#" + cmbTipoEnderecoNome).rules("add", {
            required: true,
        });
        
        $("#" + txtCepNome).rules("add", {
            required: false,
        });

        $("#" + txtLogradouroNome).rules("add", {
            required: false,
            maxlength: 255,
        });

        $("#" + txtNumeroNome).rules("add", {
            required: false,
            maxlength: 10,
        });

        $("#" + txtComplementoNome).rules("add", {
            required: false,
            maxlength: 255,
        });
        
        $("#" + txtBairroNome).rules("add", {
            required: false,
            maxlength: 255,
        });

        $("#" + txtEstadoNome).rules("add", {
            required: true,
            maxlength: 255,
        });

        $("#" + txtCidadeNome).rules("add", {
            required: true,
            maxlength: 255,
        });

        $("#" + txtPaisNome).rules("add", {
            required: true,
            maxlength: 255,
        });

        var map = new google.maps.Map(document.getElementById(mapEnderecoNome), {
            zoom: 17,
            center: { lat: latitude, lng: longitude }
        });

        var geocoder = new google.maps.Geocoder;
        var infowindow = new google.maps.InfoWindow;

        var latlng = { lat: latitude, lng: longitude };

        var marker = new google.maps.Marker({
            position: latlng,
            map: map
        });

        var contentMap = formatMapAddress(cep, logradouro, numero, bairroNomeAbreviado, cidadeNome, estadoSigla, paisISO2);
        infowindow.setContent(contentMap);
        infowindow.open(map, marker);

    };    

    var formatMapAddress = function (cep, logradouro, numero, bairro, cidade, estado, pais) {

        var result = '';

        if (logradouro != '') {
            result += '<div><strong>' + logradouro;
            if(numero != ''){
                result += ', ' + numero;
            }            
            result += '</strong><br>';            
        }
        else if (cep != '' && logradouro == '') {
            result += '<div><strong>' + cep + '</strong><br>';
        }

        result += cep + ' ' + bairro + '<br>';
        result += cidade + ' - ' + estado + ' - ' + pais + '<br>';

        return result;
    };

    return {
        init: function (tipoPessoa) {
            handleInit(tipoPessoa);
        },
        initMap: function () {
            handleInitMap();
        },
        loadEnderecos: function (enderecos) {
            handleLoadEnderecos(enderecos);
        },
        removeEndereco: function (pessoaEnderecoId) {
            handleRemoveEndereco(pessoaEnderecoId);
        },
        getEnderecos: function () {
            return handleGetEnderecos();
        },
    };
}();




