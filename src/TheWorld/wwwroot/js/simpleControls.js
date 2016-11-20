(function () {
    "use strict";

    angular.module("simpleControls", [])
        .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            scope: {
                show: "=showCursor"
            },
            restrict: "E",
            templateUrl: "/views/waitCursor.html"
        };
    }
})();