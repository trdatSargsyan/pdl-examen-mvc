$(document).ready(function () {
    var urlConfig = $('#getResForUserUi').val();
    $.ajax({
        type: 'GET',
        url: urlConfig
    }).done(function (data) {
        console.log(data)
        var cancelBtnStat;
        for (i = 0; i < data.length; i++) {

            var sDate = data[i].start_Date
            var sDate_format = new Date(Date.parse(sDate));
            var startDate  = (sDate_format.getDate()) + "/" + (sDate_format.getMonth() + 1) + "/" + sDate_format.getFullYear();

            var eDate = data[i].end_Date;
            var eDate_format = new Date(Date.parse(eDate));
            var endDate = (eDate_format.getDate()) + "/" + (eDate_format.getMonth() + 1) + "/" + eDate_format.getFullYear();

            if (sDate_format > moment(Date.now()).toDate()) {
                cancelBtnStat = '>Cancel</button></div>';
            } else {
                cancelBtnStat = ' disabled >Cancel</button></div>';
            }
            $('#resTable').append(
                '<tr>' +
                '<td>' + data[i].brand +'</td>' +
                '<td>' + data[i].model + '</td>' +
                '<td>' + startDate + '</td>' +
                '<td>' + endDate + '</td>' +
                '<td>$ ' + data[i].solde.toFixed(2) + '</td>' +
                '<td class="text-center">' +
                '<button onclick="CancelReservation(' + data[i].reservationId + ')" class="btn btn-danger btn-sm" id="btnCancel"'  + cancelBtnStat +
                '</td>' +
                '</tr>'

            );
        }
    })
});

function CancelReservation(Id) {
    var url = $('#cancelReservation').val();
    console.log(Id)

    $.ajax({
        type: 'POST',
        url: url,
        data: { Id : Id },
        success: function () {
            console.log("OK")
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    });
}
