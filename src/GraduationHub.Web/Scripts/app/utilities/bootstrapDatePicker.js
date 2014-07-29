// Note: this will initialize the date picker for browsers that do not support the HTML5 Date Input.
(function() {
    'use strict';

    if (!Modernizr.inputtypes.date) {
        $('.datepicker').datepicker({
            format: 'yyyy-mm-dd',
            autoclose:'true'
        });


    }

})();