(function() {
    'use strict';

    var controllerId = 'studentBiographyController';
    
    angular.module('graduateHubApp').controller(controllerId,
    ['$scope', '$http', 'alerts', studentBiographyController]);

    function studentBiographyController($scope, $http, alerts) {

        $scope.editing = false;
        $scope.init = init;
        $scope.save = save;
        $scope.cancel = cancel;
        $scope.edit = edit;

        function init(biography) {
            $scope.originalBiography = biography;
            $scope.biography = biography;
        }

        function edit() {
            $scope.editing = true;
        }

        function cancel() {
            angular.extend($scope.biography, $scope.originalBiography);
            $scope.editing = false;
        }

        function save() {

            $http.post("/StudentExpressions/MyBiography", $scope.biography)
                .success(function(data) {
                    $scope.originalIssue = angular.extend({}, data);

                    $scope.editing = false;

                    alerts.success("Your Biography has been saved!");
                })
                .error(function(data) {
                    if (data.errorMessage) {
                        alerts.error("There was a problem saving your Biography: \n" + data.errorMessage + "\nPlease try again.");
                    } else {
                        alerts.error("There was a problem saving your Biography.  Please try again.");
                    }
                });
        }

    }

})();