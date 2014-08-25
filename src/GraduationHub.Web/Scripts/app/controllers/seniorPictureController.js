(function() {
    'use strict';

    var controllerId = 'seniorPictureController';

    angular.module('graduateHubApp').controller(controllerId,
        ['$scope', '$http', 'alerts', seniorPictureController]);

    function seniorPictureController($scope, $http, alerts) {
        $scope.editing = false;
        $scope.cancel = cancel;
        $scope.edit = edit;

        function edit() {
            $scope.editing = true;
        }

        function cancel() {
            $scope.editing = false;
        }

    }
})();