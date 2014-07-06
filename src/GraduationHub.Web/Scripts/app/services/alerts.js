(function () {
    'use strict';

    var serviceId = 'alerts';

    angular.module('graduationHubApp').factory(serviceId, function () {
        return window.alerts;
    });

})();
