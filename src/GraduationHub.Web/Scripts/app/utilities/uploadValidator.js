(function() {
    'use strict';

    $('input:file').each(function() {
        $(this).on('change', function() {

            var fileTypes = $(this).data('types').toLowerCase().split(',');
            var maxsize = $(this).data('maxsize');
            
            // Get the file upload control file extension
            var ext = $(this).val().split('.').pop().toLowerCase();

            //Check the file extension is in the array.
            var isValidFile = $.inArray(ext, fileTypes);

            // isValidFile gets the value -1 if the file extension is not in the list.
            if (isValidFile == -1) {
                alerts.error('Please select a valid file of type jpg.');
                $(this).replaceWith($(this).val('').clone(true));
            } else {


                if ($(this).get(0).files[0].size > (1024 * 1024 * maxsize)) {
                    alerts.error('File size should not exceed ' + maxsize + ' MB.');
                    $(this).replaceWith($(this).val('').clone(true));
                } else {
                    alerts.success('Thank you for selecting a valid file.');
                }
            }

        });
    });
})()