var IMSS = (function (scope) {
    scope.url = `/home`;

    scope.ajaxCall = function (method, url, data, dataType, f, headers = null, asyncHit = true, showWaiting = true) {
        if (showWaiting) {
            tpc.waitToggle();
        }
        $.ajax({
            type: method,
            url: url,
            data: data,
            headers: headers,
            dataType: dataType,
            async: asyncHit,
            success: function (d) {
                f(d);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                f("");
            }
        });
    };
    scope.login = function () {
    //    $("#responsemsg", "#loginform").html("");
    //    var url = "/home/authenticate";
    //    var data = $("#loginform").serialize();
    //    IMSC.ajaxCall("POST", url, data, "text", function (d) {
    //        var jsonResponse = JSON.parse(d);
    //        if (jsonResponse == "Fail") {
    //            $("#loginForm").val("");
    //            $("#msg").text("username and password is wrong..!");
    //        }
    //        else {
    //            if (jsonResponse.returnUrl != undefined) {
    //                window.location.href = jsonResponse.returnUrl;
    //            } else {
    //                window.location.href = "Admin/Dashboard";
    //            }
    //            $("#msg").hide();
    //        }
    //    });
    //   // return true;
    };
    return scope;

})(IMSS || {});

$(function () {
    $("#loginform").validate({
        submitHandler: function () {
            return true // IMSS.login()
        },
        rules: {
            loginid: { required: true },
            password: { required: true },
        },
        messages: {
            loginid: "please enter loginid",
            password: "please enter password"
        }
    });
});

