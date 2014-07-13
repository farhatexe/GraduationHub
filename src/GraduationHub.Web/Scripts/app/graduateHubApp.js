(function () {
    'use strict';

    window.onerror = function (msg) {
        if (window.alerts) {
            window.alerts.error("There was a problem with your last action.  Please reload the page, then try again.");
        } else {
            alert("Something serious went wrong.  Please close out of Fail Tracker, then try again.");
        }
    };

    var id = 'graduateHubApp';

    var graduateHubApp = angular.module(id, []);

    graduateHubApp.run([
		function () {
		    //Startup code goes here!
		}
    ]);
})();