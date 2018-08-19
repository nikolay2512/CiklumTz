this.Token;
this.SetInitialDataToHeader = function ()
{
    href = '/admin/Authorization';
    var isAuthorizationPage = window.location.pathname == href;
    if(isAuthorizationPage)
    {
        $('#generalHeader').css('display', 'none');
    }
    this.Token = this.GetCookie("Authorization");
    if(this.Token == null)
    {
        if(!isAuthorizationPage)
        {
            this.Token = 'not authorized';
            window.location.href = href;
        }
    }
    else
    {
        var aLogout = $('<a>',{
        'href': '#',
        'id': 'aLogout',
        'onclick': 'Logout()',
        });
        var iLogout = $('<i>',          {             'class':'fa fa-sign-out',
            'aria-hidden' : 'true'         });         iLogout.appendTo(aLogout);
        $("#Logout").html(aLogout);
    }
}

this.Logout = function()
{
    let confirmLogout = window.confirm('Are you sure you want to logout?');
    if(confirmLogout)
    {   
        this.CreateOrDeleteCookie("Authorization", "", -1);
        href = '/admin/Authorization';
        window.location.href = href;
    }
}

this.GetCookie = function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for(var i=0;i < ca.length;i++) {
        var c = ca[i];
        while (c.charAt(0)==' ') c = c.substring(1,c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
    }
    return null;
}

this.CreateOrDeleteCookie = function createOrDeleteCookie(name,value,days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime()+(days*24*60*60*1000));
        var expires = "; expires="+date.toGMTString();
    }
    else var expires = "";
    document.cookie = name+"="+value+expires+"; path=/";
}

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
    var header = GetCookie("Authorization");
    $.ajax({
        url: '/admin/' + controllerName +'/' + methodName,
        type: type,
        async: true,
        cache: false,
        dataType: "json",
        data: request,
        contentType: "application/json; charset=utf-8",
        beforeSend : function(xhr) {  
            xhr.setRequestHeader('Authorization', 'Bearer ' + header);
        },
        success: function (result) {
            onsucces(result);
        },
        error: function (e) {
            onerror(e);
        }
    });
}