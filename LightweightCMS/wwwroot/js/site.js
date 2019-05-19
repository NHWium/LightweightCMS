// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
            //### start summernote ###
            //only start if an id with the value 'summernote' appears
            if ($("#summernote").length) {
        $('#summernote').summernote({
            codeviewFilter: false,
            codeviewIfreameFilter: false,
            maximumImageFileSize: 524288
        });
    if ($("#Background").length) {
        $('.note-editable').css('background-color', $("#Background").val());
    }
}

//### start spectrum ###
//only start if an id with the value 'Background' appears
            if ($("#Background").length) {
        $('#Background').spectrum({
            showAlpha: true,
            preferredFormat: "hsl",
            move: function (color) {
                $('#Background').data(color.toRgbString());
                // Temporary set colors to allow for WYSIWYG
                if ($("#MainContent").length) {
                    $('body').css('background-color', color.toRgbString());
                }
                if ($(".note-editable").length) {
                    $('.note-editable').css('background-color', color.toRgbString());
                }
            }
        });
    }

    //Body and MainContent background color will interfere with each other due to alpha,
    //only show body - not MainContent
            if ($("#MainContent").length) {
        $('body').css('background-color', $("#MainContent").css('background-color'));
    $('#MainContent').css('background-color', '');
}
});
