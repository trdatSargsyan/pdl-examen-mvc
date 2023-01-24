
$(document).ready(function () {
    var ccType = 0; //default value
    var ccNumber;
    var ccvLength;
    var creditcardLength;
    $("#ccv").on("keydown keyup change", function () {
        var value = $(this).val();
        if (value.length < ccvLength)
            $("#ccvError").text("Text is short");
        else if (value.length > ccvLength)
            $("#ccvError").text("Text is long");
        else {
            $("#ccvError").text("");
             validateCVV(ccNumber, value);
        }
    });

    $(function () {
        ccNumber = document.getElementById('ccNumber').value;
        console.log(ccNumber)
        ccType = document.getElementById('sType').value;
        //console.log(ccType)

    })

    $("#ccNumber").on("keydown keyup change", function () {

        //if (ccNumber == undefined) {
            ccNumber = $(this).val();
        //}
        ccNumber = ccNumber.replace(/\s/g, "");
        var ccLength = ccNumber.length;
        //console.log(ccLength)
        if (ccLength < creditcardLength)
            $("#ccError").text("Text is short");
        else if (ccLength > creditcardLength)
            $("#ccError").text("Text is long");
        else {
            $("#ccError").text("");
            if (ccType >= 0 && ccType <= 4) {
                ValidationCC(ccType, ccNumber);
             }
        }
    });

    $("select").on('change', function () {
        ccType = $(this).val();
        console.log(ccType)
        $("#ccNumber").val('');
        $("#ccv").val('');
        if (ccType == '2') {
            creditcardLength = 15;
            ccvLength = 4;
        } else {
            creditcardLength = 16;
            ccvLength = 3;
        }
    })
});

//CreditCardValidation
function ValidationCC(ccType, ccNumber) {
    let x = parseInt(ccType);
    switch (x) {
        case 0:
            return MasterCard(ccNumber);
            //break;
        case 1:
            return Visa(ccNumber);
            //break;
        case 2:
            return AmericanExpress(ccNumber);
            //break;
        case 3:
            return Discover(ccNumber);
            //break;
        case 4:
            return JCB(ccNumber);
            //break;
        default:
            console.log("Error");
            return false;
    }
}

//AmericanExpress
function AmericanExpress(inputtxt) {
    var cardno = /^(?:3[47][0-9]{13})$/;
    if (inputtxt.match(cardno)) {
        return true;
    }
    else {
        $("#ccError").text("Not a valid Amercican Express credit card number!");
        return false;
    }
}

//Visa 
function Visa(inputtxt) {
    var cardno = /^(?:4[0-9]{12}(?:[0-9]{3})?)$/;
    if (inputtxt.match(cardno)) {
        return true;
    }
    else {
        $("#ccError").text("Not a valid Visa credit card number!");
        return false;
    }
}

//MasterCard
function MasterCard(inputtxt) {
    var cardno = /^(?:5[1-5][0-9]{14})$/;
    if (inputtxt.match(cardno)) {
        return true;
    }
    else {
        $("#ccError").text("Not a valid Mastercard number!");
        return false;
    }
}

//Discover 
function Discover(inputtxt) {
    var cardno = /^(?:6(?:011|5[0-9][0-9])[0-9]{12})$/;
    if (inputtxt.match(cardno)) {
        return true;
    }
    else {
        $("#ccError").text("Not a valid Discover card number!");
        return false;
    }
}

//JCB 
function JCB(inputtxt) {
    var cardno = /^(?:(?:2131|1800|35\d{3})\d{11})$/;
    if (inputtxt.match(cardno)) {
        return true;
    }
    else {
        $("#ccError").text("Not a valid JCB card number!");
        return false;
    }
}


function validateCVV(creditCard, cvv) {
    // remove all non digit characters
    var creditCard = creditCard.replace(/\D/g, '');
    var cvv = cvv.replace(/\D/g, '');
    // american express and cvv is 4 digits
    if ((acceptedCreditCards.amex).test(creditCard)) {
        if ((/^\d{4}$/).test(cvv))
            return true;
    } else if ((/^\d{3}$/).test(cvv)) { // other card & cvv is 3 digits
        return true;
    }
    return false;
}



var acceptedCreditCards = {
    amex: /^3[47][0-9]{13}$/,
    visa: /^4[0-9]{12}(?:[0-9]{3})?$/,
    mastercard: /^5[1-5][0-9]{14}$|^2(?:2(?:2[1-9]|[3-9][0-9])|[3-6][0-9][0-9]|7(?:[01][0-9]|20))[0-9]{12}$/,  
    discover: /^65[4-9][0-9]{13}|64[4-9][0-9]{13}|6011[0-9]{12}|(622(?:12[6-9]|1[3-9][0-9]|[2-8][0-9][0-9]|9[01][0-9]|92[0-5])[0-9]{10})$/,
    //diners_club: /^3(?:0[0-5]|[68][0-9])[0-9]{11}$/,
    jcb: /^(?:2131|1800|35[0-9]{3})[0-9]{11}$/,
};



