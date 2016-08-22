function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        center: {lat: -23.6815315, lng: -46.8754831},
        scrollwheel: true,
        zoomControl: true,
        scaleControl: true,       
        zoom: 12,
        mapTypeControl: true,
        mapTypeControlOptions: {
            style: google.maps.MapTypeControlStyle.DROPDOWN_MENU,
            mapTypeIds: [
                google.maps.MapTypeId.ROADMAP,
                google.maps.MapTypeId.TERRAIN
            ]
        }
    });

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function(position) {
       
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };

            infoWindow.setPosition(pos);
            infoWindow.setContent('Location found.');

            map.setCenter(pos);
        }, function() {
            handleLocationError(true, infoWindow, map.getCenter());
        });
    } else {
        handleLocationError(false, infoWindow, map.getCenter());
    }
}

function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ? 'Erro: Falha ao obter localização.' : 'Erro: Browser não suporta geolocalização.');
}