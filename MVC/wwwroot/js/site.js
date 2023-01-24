$(document).ready(function () {
    $("#countryName").keypress(function (e) {
        var _val = $("#countryName").val();
        var _txt = _val.charAt(0).toUpperCase() + _val.slice(1);
        $("#countryName").val(_txt);

        //var keyCode = e.keyCode || e.which;
        //var regex = /^[a-zA-Z ]*$/;

        ////Validate TextBox value against the Regex.
        //var isValid = regex.test(String.fromCharCode(keyCode));
        //if (!isValid) {
        //    $("span").html("Only Alphabets allowed.");
        //}

        //return isValid;
    })

    $("#aeroportName").keypress(function (e) {
        var _val = $("#aeroportName").val();
        var _txt = _val.charAt(0).toUpperCase() + _val.slice(1);
        $("#aeroportName").val(_txt);
    })

    $("#motorType").keypress(function (e) {
        var _val = $("#motorType").val();
        var _txt = _val.charAt(0).toUpperCase() + _val.slice(1);
        $("#motorType").val(_txt);
    })
})




