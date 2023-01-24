var path = 0;
var pathAero = 0;

$(document).ready(function () {
    var urlConfig = $('#GetConfigValue').val();
    $.ajax({
        type: 'GET',
        url: urlConfig      
    }).done(function (data) {
        var settings = {
            "async": true,
            "crossDomain": true,
            "url": "https://" + data.audience + "/oauth/token",
            "method": "POST",
            "headers": {
                "content-type": "application/json"
            },
            "data": "{\"client_id\":\"" + data.clientId + "\",\"client_secret\":\"" + data.clientSecret
                + "\",\"audience\":\"https://"+ data.audience +  "/api/v2/\",\"grant_type\":\"client_credentials\"}"
        }
        AssignRoleToUser(settings);
        
    })
});

$(document).ready(function () {
    DisplayCars();
    $('#CountryId').change(function () {     
        path = $("#CountryId").val();
        $('#listCar').empty();
        if (path != 0) {
            DisplayCarsByCountry(path);
        } else {
            DisplayCars();
        }
        pathAero = path;
        GetAeroportsByIdCountry(path);     
    });
    DisplayCarsByAeroport();
    path = 0;
});


function AssignRoleToUser(settings) {
    var access_token = '';
    $.ajax(settings).done(function (response) {
        access_token = response.access_token;
        var url = $('#AssignRoleToUser').val();
        $.ajax({
            url: url,
            data: { accessToken: access_token },
            type: 'GET',
            success: (function () {
                console.log("OK");
            })
        })
    });
}

function DisplayCars() {
    var getCars = $("#GetCars").val();
    $.ajax({
        type: 'GET', url: getCars
    }).done(function (data) {
        DisplayAllCars(data);
    });
}

function DisplayAllCars(data) {
    $('#test').empty();
    for (i = 0; i < data.length; i++) {
        var price = data[i].prices;
        var date = data[i].productionDate;
        var aeroport = data[i].aeroportDto;
        var date2 = new Date(Date.parse(date));
        var dateDeProduction = (date2.getDate()) + "/" + (date2.getMonth() + 1) + "/" + date2.getFullYear();
        for (j = 0; j < 1; j++) {

            $("#test").append(
                '<div class="column"><div class="card">' +
                '<h4>' + data[i].brand + ' ' + data[i].model + '</h4> ' +
                '<p>' + aeroport.name + '</p>' +
                '<p>Prix : ' + price.priceDay.toFixed(2) + ' $/day' + '</p>' +
                '<p>Prix : ' + price.priceWeek.toFixed(2) + ' $/week' + ' ' + '</p>' +
                '<p>' + ' ' + ' Date de production :' + ' ' + dateDeProduction + '</p>' +
                '<img src="' + data[i].picture + '" alt="voiture" />' +
                '<div id="resButton"><button onclick="GetCarForResById(' + data[i].id + ')" class="btn btn-success btn-sm">Details</button></div>' +
                '</div> </div>'
            );
        }
    }
}

function DisplayCarsByAeroport() {
    $('#AeroportId').change(function () {
        var url = $('#CarsURL').val();
        var path = $("#AeroportId").val();
        if (typeof path === 'string' && path.length === 0) {
            console.log(pathAero)
            DisplayCarsByCountry(pathAero); 
        }

        $.getJSON(url, { IdAeroport: path }, function (data) {
            $('#test').empty();
            for (i = 0; i < data.length; i++) {
                var price = data[i].priceDto;
                var date = data[i].productionDate;
                var date2 = new Date(Date.parse(date));
                var date3 = date2.getFullYear() + "/" + (date2.getMonth() + 1) + "/" + (date2.getUTCDate());

                for (j = 0; j < 1; j++) {

                    $("#test").append(
                        '<div class="column"><div class="card">' +
                        '<h4>' + data[i].brand + ' ' + data[i].model + '</h4> ' +
                        '<p>Prix : ' + price.priceDay.toFixed(2) + ' $/day' +
                        '</p><p>Prix : ' + price.priceWeek.toFixed(2) + ' $/week' + ' ' +
                        '</p><p>Date de production : ' + date3 +
                        '<img src="' + data[i].picture + '" alt="voiture" />' +
                        '<div id="resButton"><button onclick="GetCarForResById(' + data[i].id + ')"class="btn btn-success btn-sm">Details</button></div>' +
                        '</div> </div>'
                    );
                }
            }
        });
    });
}

function DisplayCarsByCountry(path) {
    var urlCarsByCountry = $("#GetCarsByIdCountry").val();
    $.ajax({
        type: 'GET', url: urlCarsByCountry, data: { Id: path }
    }).done(function (data) {
        DisplayAllCars(data);
    });
    $('#listCar').empty();
}

function GetCarForResById(id) {
    var url = $('#CarInfoURL').val();
    $.ajax({
        url: url,
        data: { Id: id },
        type: 'GET',
        success: function () {
            window.location.href = url + id;
            console.log(url + id);

        }
    });
}

function GetAeroportsByIdCountry(path) {
    var url = $('#CountryURL').val();
    $.getJSON(url, { IdCountry: path }, function (data) {
        var items = '<option value="">Choisir un aéroport</option>';
        $.each(data, function (i, subcategory) {
            items += "<option value='" + subcategory.value + "'>" + subcategory.text + "</option>";
        });
        $('#AeroportId').html(items);
    });
    $('#test').empty();
}