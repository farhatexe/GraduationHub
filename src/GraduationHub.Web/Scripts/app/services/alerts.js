(function () {
    'use strict';

    var serviceId = 'alerts';

    angular.module('graduateHubApp').factory(serviceId, function () {
        return window.alerts;
    });

})();
