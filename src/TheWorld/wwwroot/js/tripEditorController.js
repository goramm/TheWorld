(function () {
    "use strict";

    angular.module("app-trips")
        .controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        var vm = this;

        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        vm.newStop = {};

        var url = "/api/trips/" + vm.tripName + "/stops";

        $http.get(url)
            .then(function (response) {
                angular.copy(response.data, vm.stops);
                _showMap(vm.stops)
            }, function () {
                vm.errorMessage = "Faild to load data: " + error.statusText;
            })
            .finally(function () {
                vm.isBusy = false;
            });


        vm.addStop = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post(url, vm.newStop)
                .then(function (response) {
                    vm.stops.push(response.data);
                    _showMap(vm.stops);
                    vm.newStop = {};
                }, function (error) {
                    vm.errorMessage = "Faild to load data: " + error.statusText;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };

       
    }

    function _showMap(stops) {
        if (stops && stops.length) {

            var mapStops = stops.map(function (item) {
                return {
                    lat: item.lat,
                    long: item.long,
                    info: item.name
                };
            });

            console.log(mapStops)

            travelMap.createMap({
                stops: mapStops,
                selector: "#map",
                currentStop: 0,
                initialZoom: 6,
            });
        }
    }
})();