(function() {
    'use strict';

    var controllerId = 'addInvitationController';

    angular.module('graduateHubApp').controller(controllerId,
    ['$scope', '$http', 'alerts', addInvitationController]);

    function addInvitationController($scope, $http, alerts) {

        $scope.adding = false;
        $scope.init = init;
        $scope.save = save;
        $scope.cancel = cancel;
        $scope.add = add;

        function init(invitation) {
            
        }

        function add() {
            $scope.adding = true;
        }

        function save() {
            
        }

        function cancel() {
            
        }
    }
})