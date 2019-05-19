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
            showPaletteOnly: false,
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
                ['Transparent', 'HotPink', 'DarkSalmon', 'LightSteelBlue', 'Maroon', 'LimeGreen'],
                ['White', 'Tomato', 'Lavender', 'PaleTurquoise', 'Aquamarine', 'DarkSlateGrey'],
                ['Ivory', 'OrangeRed', 'LightCyan', 'GreenYellow', 'Chartreuse', 'DarkSlateGray'],
                ['LightYellow', 'DeepPink', 'BurlyWood', 'LightBlue', 'LawnGreen', 'SeaGreen'],
                ['Yellow', 'Magenta', 'Plum', 'DarkGrey', 'MediumSlateBlue', 'ForestGreen'],
                ['Snow', 'Fuchsia', 'Gainsboro', 'DarkGray', 'LightSlateGrey', 'LightSeaGreen'],
                ['FloralWhite', 'Red', 'Crimson', 'Brown', 'LightSlateGray', 'DodgerBlue'],
                ['LemonChiffon', 'OldLace', 'PaleVioletRed', 'Sienna', 'SlateGrey', 'MidnightBlue'],
                ['Cornsilk', 'LightGoldenRodYellow', 'GoldenRod', 'YellowGreen', 'SlateGray', 'Cyan'],
                ['SeaShell', 'Linen', 'Orchid', 'DarkOrchid', 'OliveDrab', 'Aqua'],
                ['LavenderBlush', 'AntiqueWhite', 'Thistle', 'PaleGreen', 'SlateBlue', 'SpringGreen'],
                ['PapayaWhip', 'Salmon', 'LightGrey', 'DarkViolet', 'DimGrey', 'Lime'],
                ['BlanchedAlmond', 'GhostWhite', 'LightGray', 'MediumPurple', 'DimGray', 'MediumSpringGreen'],
                ['MistyRose', 'MintCream', 'Tan', 'LightGreen', 'MediumAquaMarine', 'DarkTurquoise'],
                ['Bisque', 'WhiteSmoke', 'Chocolate', 'DarkSeaGreen', 'RebeccaPurple', 'DeepSkyBlue'],
                ['Moccasin', 'Beige', 'Peru', 'SaddleBrown', 'CornflowerBlue', 'DarkCyan'],
                ['NavajoWhite', 'Wheat', 'IndianRed', 'DarkMagenta', 'CadetBlue', 'Teal'],
                ['PeachPuff', 'SandyBrown', 'MediumVioletRed', 'DarkRed', 'DarkOliveGreen', 'Green'],
                ['Gold', 'Azure', 'Silver', 'BlueViolet', 'Indigo', 'DarkGreen'],
                ['Pink', 'HoneyDew', 'DarkKhaki', 'LightSkyBlue', 'MediumTurquoise', 'Blue'],
                ['LightPink', 'AliceBlue', 'RosyBrown', 'SkyBlue', 'DarkSlateBlue', 'MediumBlue'],
                ['Orange', 'Khaki', 'MediumOrchid', 'Grey', 'SteelBlue', 'DarkBlue'],
                ['LightSalmon', 'LightCoral', 'DarkGoldenRod', 'Gray', 'RoyalBlue', 'Navy'],
                ['DarkOrange', 'PaleGoldenRod', 'FireBrick', 'Olive', 'Turquoise', 'Black'],
                ['Coral', 'Violet', 'PowderBlue', 'Purple', 'MediumSeaGreen', 'Transparent']
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
