// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//Spectrum color changer
function changeColor(color) {
    $('#Background').data(color.toRgbString());
    // Temporary set colors to allow for WYSIWYG
    if ($("#MainContent").length) {
        $('body').css('background-color', color.toRgbString());
    }
    if ($(".note-editable").length) {
        $('.note-editable').css('background-color', color.toRgbString());
    }
}


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
            showInput: true,
            showPalette: true,
            showPaletteOnly: true,
            togglePaletteOnly: true,
            showSelectionPalette: false,
            preferredFormat: "hsl",
            move: function (color) {
                changeColor(color);
            },
            change: function (color) {
                changeColor(color);
            },
            palette: [
                ['transparent', '#FF0000', '#FF8000', '#FFFF00', '#00FF00', '#00FFFF', '#0080FF', '#0000FF', '#8000FF', '#FF00FF'],
                ['#FFFFFF', '#FBEFEF', '#FBF5EF', '#FBFBEF', '#EFFBEF', '#EFFBFB', '#EFF5FB', '#EFEFFB', '#F5EFFB', '#FBEFFB'],
                ['#F2F2F2', '#F6CECE', '#F6E3CE', '#F5F6CE', '#CEF6CE', '#CEF6F5', '#CEE3F6', '#CECEF6', '#E3CEF6', '#F6CEF5'],
                ['#D8D8D8', '#F78181', '#F7BE81', '#F3F781', '#81F781', '#81F7F3', '#81BEF7', '#8181F7', '#BE81F7', '#F781F3'],
                ['#A4A4A4', '#FE2E2E', '#FE9A2E', '#F7FE2E', '#2EFE2E', '#2EFEF7', '#2E9AFE', '#2E2EFE', '#9A2EFE', '#FE2EF7'],
                ['#6E6E6E', '#DF0101', '#DF7401', '#D7DF01', '#01DF01', '#01DFD7', '#0174DF', '#0101DF', '#7401DF', '#DF01D7'],
                ['#424242', '#8A0808', '#8A4B08', '#868A08', '#088A08', '#088A85', '#084B8A', '#08088A', '#4B088A', '#8A0886'],
                ['#1C1C1C', '#3B0B0B', '#3B240B', '#393B0B', '#0B3B0B', '#0B3B39', '#0B243B', '#0B0B3B', '#240B3B', '#3B0B39'],
                ['#000000', '#190707', '#191007', '#181907', '#071907', '#071918', '#070B19', '#070719', '#100719', '#190718'],
                [$("#Background").val()]
            ]
        });
    }

    //Body and MainContent background color will interfere with each other due to alpha,
    //only show body - not MainContent
    if ($("#MainContent").length) {
        $('body').css('background-color', $("#MainContent").css('background-color'));
        $('#MainContent').css('background-color', '');
    }
});
