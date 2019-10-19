function LoadRegions() {
    //var countryid = selectedObject.value;
    var countryid = document.getElementById("SelectedCountry").value;
    if (countryid != "") {
        var regionUrl = `/City/RegionList?countryId=${countryid}`;
        $.ajax({
            url: regionUrl,

            success: function (data) {
                if (data != null) {
                    var regions = JSON.parse(data);

                    var regionList = document.getElementById("SelectedRegion");
                    regionList.innerHTML = "";

                    for (var i in regions) {
                        regionList.appendChild(new Option(regions[i].Name, regions[i].Id));
                    }
                    LoadCities();
                }
            }
        });
    }
}

function LoadCities() {
    //var regionid = selectedObject.value;
    var regionid = document.getElementById("SelectedRegion").value;
    if (regionid != "") {
        var regionUrl = `/City/CityList?regionId=${regionid}`;
        $.ajax({
            url: regionUrl,

            success: function (data) {
                if (data != null) {
                    var cities = JSON.parse(data);

                    var cityList = document.getElementById("SelectedCity");
                    cityList.innerHTML = "";
                    for (var i in cities) {
                        cityList.appendChild(new Option(cities[i].Name, cities[i].Id));
                    }
                    LoadStreets();
                }
            }
        });
    }
}

function LoadStreets() {
    //var cityid = selectedObject.value;
    var cityid = document.getElementById("SelectedCity").value;
    if (cityid != "") {
        var cityUrl = `/City/StreetList?cityId=${cityid}`;
        $.ajax({
            url: cityUrl,

            success: function (data) {
                if (data != null) {
                    var streets = JSON.parse(data);

                    var streetList = document.getElementById("SelectedStreet");
                    streetList.innerHTML = "";
                    for (var i in streets) {
                        streetList.appendChild(new Option(streets[i].Name, streets[i].Id));
                    }
                }
            }
        });
    }
}