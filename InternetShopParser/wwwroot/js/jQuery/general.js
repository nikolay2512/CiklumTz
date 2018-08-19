this.ShowToast = function(message)
{
    $.mobile.toast(
    {
        message: message,
        duration: 3000,
        position: 50,
        classOnOpen: "notification-error"
    });
}

this.GetParametrFromUrl = function (locationString, name) 
{
    name = name.replace(/[\[]/, '\\\[').replace(/[\]]/, '\\\]');
    var regexS = '[\\?&]' + name + '=([^&#]*)';
    var regex = new RegExp(regexS);
    var results = regex.exec(locationString);
    if (results == null)
        return '';
    else
        return decodeURIComponent(results[1].replace(/\+/g, ' '));
}

this.CallLoadData = function (controllerName, methodName, type, request, onsucces, onerror)
{
    $.ajax({
        url: '/v1/' + controllerName +'/' + methodName,
        type: type,
        async: true,
        cache: false,
        dataType: "json",
        data: request,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            onsucces(result);
        },
        error: function (e) {
            onerror(e);
        }
    });
}