var ChangeAvatarScript = function () {

    var _usuarioId;
    var _avatarStorageObject;

    var handleInit = function (usuarioId, avatarStorageObject) {

        _usuarioId = usuarioId;
        _avatarStorageObject = avatarStorageObject;

        $("#formChangeAvatar").submit(function (e) {
            e.preventDefault(); //prevent default form submit            
            saveForm();
        });

        $("#btnRemoverImagem").on("click", function () {
            $('#imgAvatar').attr('src', "../assets/admin/img/avatar1.png");
            $('#btnChangeAvatar').removeAttr('disabled');
        });

        $("#fileAvatar").on("change", function () {

            _avatarStorageObject = null;

            $('#btnChangeAvatar').removeAttr('disabled');
            var fileAvatar = $("#fileAvatar")[0].files[0];
            if (fileAvatar) {
                $("#btnRemoverImagem").show();
            }
            else {
                $("#btnRemoverImagem").hide();
            }
        });

        if (_avatarStorageObject) {
            $('#imgAvatar').attr('src', "/api/perfil/avatar/" + _avatarStorageObject);
            $("#btnRemoverImagem").show();
        }
        else {
            $('#imgAvatar').attr('src', "../assets/admin/img/avatar1.png");
            $("#btnRemoverImagem").hide();
        }
    }

    var saveForm = function () {

        var fileAvatar = $("#fileAvatar")[0].files[0];

        if (fileAvatar) {
            var reader = new FileReader();
            reader.onload = function (e) {
                saveAvatar(reader.result);
            };
            reader.readAsDataURL(fileAvatar);
        }
        else {
            saveAvatar(null);
        }
    }

    var saveAvatar = function (avatarAsDataURL) {

        var usuarioId = $("#txtUsuarioId").val();

        var data = {
            'usuarioId': usuarioId,
            'storageUpload': storageUpload,
        };

        var storageUpload = null;

        if (avatarAsDataURL != null) {
            var dataURLSplit = avatarAsDataURL.toString().split([',']);
            var bufferBase64String = dataURLSplit[1];
            var contentType = dataURLSplit[0].split(';')[0].split(':')[1];
            storageUpload = {
                'contentType': contentType,
                'bufferBase64String': bufferBase64String,
            };
        }

        if (storageUpload != null) {
            data.storageUpload = storageUpload;
        }

        $.ajax({
            type: "PUT",
            url: ApplicationScript.getAppWebApiUrl() + '/api/perfil/avatar',
            headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
            content: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            data: data,
        }).success(function (data, textStatus, jqXHR) {
            window.location.href = "/Perfil/Account";
        }).error(function (jqXHR, textStatus, errorThrown) {
            ApplicationScript.error(jqXHR, textStatus, errorThrown);
        });
    }

    return {
        init: function (usuarioId, avatarStorageObject) {

            handleInit(usuarioId, avatarStorageObject);

        },
    };

}();