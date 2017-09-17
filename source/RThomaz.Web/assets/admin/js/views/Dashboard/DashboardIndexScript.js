var DashboardIndexScript = function () {

    var handleInit = function () {

        //Setando default menu
        $("#menuDashboard").addClass("active");

        //Setando os titulos

        var title = 'Dashboard';
        var subTitle = 'gerencie suas finan\u00e7as';

        setAppTitleSubTitle(title, subTitle);
    }

    return {
        init: function () {          

            handleInit();
        },
    };
}();


function clickGET() {

    $.ajax({
        type: "GET",
        async: false,
        url: 'api/DashboardApi/18',
        headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
        content: "application/json; charset=utf-8",
        dataType: "json",
        //data: {
        //    'id': 18,
        //},
        success: function (data) {
            alert("GET OK");
        },
        error: function (request, status, error) {
            errorLog(request, status, error);
        }
    });

}

function clickPOST() {

    var model = {
        Id: "9",
        Nome: "Rodrigo Thomaz",
    };

    //var data = jQuery.param({ model: model, var1: "hello2" });

    $.ajax({
        type: "POST",
        url: 'api/DashboardApi/Post',
        headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: model,
        success: function (data) {
            alert("POST OK");
        },
        error: function (request, status, error) {
            errorLog(request, status, error);
        }
    });

}

function clickSave() {

    $.ajax({
        data: JSON.stringify({
            model: {
                Id: "9",
                Nome: "Rodrigo",
            },
            param3: "param3"
        }),
        url: "/api/DashboardApi/DoSomething",
        headers: { 'Authorization': 'bearer ' + ApplicationScript.getToken() },
        contentType: "application/json",
        method: "POST",
        processData: false,
        success: function (data) {
            alert("Save OK");
        },
        error: function (request, status, error) {
            errorLog(request, status, error);
        }
    });

}

function errorLog(request, status, error) {
    if (request.status == 403) {//Forbidden            
        window.location.href = "/Seguranca/Login";
    }
    else {
        var exceptionMessage = request.responseJSON.ExceptionMessage;
        var exceptionType = request.responseJSON.ExceptionType;
        var message = request.responseJSON.Message;
        var stackTrace = request.responseJSON.StackTrace;
        alert(exceptionMessage);
    }
}