var dateChoose = false;
var days = 0;
var startDate = Date.now();
var startDateRes = Date.now();
var endDateRes = Date.now();
var total = 0;
var resDates;

//postReservation
$(document).ready(function () {
    $('#saveReservation').prop('disabled', true);  
    var postReservationUrl = $('#postReservationUrl').val();
    var postCC = $('#postCreditCard').val();
    //Post Reservation
    $('#saveReservation').click(function () {
        $.ajax({
            type: 'POST',
            url: postReservationUrl,
            data: {
                carId: $('#carId').val(),
                sDate: startDateRes,
                eDate: endDateRes,
                Total: total
            },
            success: function () {
                window.location.href = postCC;
            }
        });
    });

    getDatesList();
});


var sd = Date.now();
var ed ;
function getDatesList() {
    var path = $('#GetCarsResDates').val();
    $.ajax({
        type: 'GET', url: path
    }).done(function (data) {
        resDates = data;
    });
}
$(function () {
    var diff_Time = endDateRes - startDateRes;
    statCheckbox(diff_Time, dateChoose);
})

var testMaxDate;
$(function () {

    $('#Startdate').daterangepicker({
        "autoApply": true,
        singleDatePicker: true,
        minDate: new Date(), //disable previus date
        "locale": {
            format: ('MM-DD-YYYY')
        },
        isInvalidDate: function (date) {
            for (i = 0; i < resDates.length; i++) {
                if (date.format('MM/DD/YYYY') == resDates[i]) {
                    return true;
                }
            }
            return false
        },
    }, function (start) {
        sd = start;
        ed = start;
        Details();
        startDate = new Date(start.format('YYYY-MM-DD')).getTime();
        startDateRes = start.format('YYYY-MM-DD');
        endDate(start);       
    });
});

function endDate(start_Date) {
    $('#Enddate').daterangepicker({
        "autoApply": true,
        singleDatePicker: true,
        minDate: start_Date.toDate(),
        maxDate: MaxDateForRes(start_Date.toDate()),
        locale: {
            format: ('MM-DD-YYYY'),
        },
    }, function (end) {
        ed = end;
        Details();
        Amount(end);
    });   
}

function Details() {  
    if (ed != undefined) {
        if (sd.toDate() <= ed.toDate()) {
            $('#day').prop('checked', false)
            $('#week').prop('checked', false)
            $('input[name=amount]').val("0");
            $('#selectPrice').text("Choisir un forfait")
            $('#day').attr('disabled', 'disabled')
            $('#week').attr('disabled', 'disabled')
            return true;
        }
    }
    return false;
}

function MaxDateForRes(Start_Date) {
    if (Start_Date < new Date(resDates[0])) {
         return setPreviousDay(new Date(resDates[0]));             
    }
    return setPreviousDay(new Date('1/1/2100'));
}

function setPreviousDay(date) {
    const previous = new Date(date.getTime());
    previous.setDate(date.getDate() - 1);
    return previous;
}

function Amount(end) {
    var endDate = new Date(end.format('YYYY-MM-DD')).getTime();
    var diff_Time = endDate - startDate;
   
    var diff_Days = 1 + diff_Time / (1000 * 3600 * 24);
    days = diff_Days;

    endDateRes = end.format('YYYY-MM-DD');

    dateChoose = true;
    statCheckbox(diff_Time, dateChoose);
    if (diff_Days % 7 !== 0) {
        $('#week').attr('disabled', 'disabled')
    } else {
        $('#week').attr('disabled', false)
    }
}



//Day
$(document).on('change','#day',function () {
    if (this.checked) {
        var dayPrice = this.value;
        if (days > 0) {          
            total = days * dayPrice;
            $('input[name=amount]').val(total);
            $('#saveReservation').prop('disabled', false);
            $('#selectPrice').text("")
        }
    } else {
        $('#saveReservation').prop('disabled', true);
        $('input[name=amount]').val("0");
        $('#selectPrice').text("Choisir un forfait")
    }
});
//Week
$(document).on('change', '#week', function () {
    if (this.checked) {
        const inWeek = days / 7;
        total = this.value * inWeek;
        $('input[name=amount]').val(total);
        $('#saveReservation').prop('disabled', false);        
        $('#selectPrice').hide();
    } else {
        $('#saveReservation').prop('disabled', true);
        $('input[name=amount]').val("0");
        $('#selectPrice').show();
    }
});
//checkbox
$(document).on('click', 'input[type="checkbox"]', function () {
    $('input[type="checkbox"]').not(this).prop('checked', false);
}); 



function statCheckbox(dayDiff,dateChoose) {
    if (dayDiff < 1 && dateChoose == false) {
        $('#day').attr('disabled', true)
        $('#week').attr('disabled', true)
    } else {
        $('#day').attr('disabled', false)
        $('#week').attr('disabled', false)
    }
}

function toDate(dateStr) {
    var parts = dateStr.split("-");
    return new Date(parts[2], parts[1] - 1, parts[0]);
}

