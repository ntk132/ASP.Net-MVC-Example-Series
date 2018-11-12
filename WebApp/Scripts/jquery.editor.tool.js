var colorPalette = ['FFFFFF', 'FF6F00', 'F57F17', 'E65100', 'BF360C', 'B71C1C', '880E4F', '827717', '4A148C', '3E2723', '33691E', '311B92', '263238', '212121', '1A237E', '0D47A1', '1B5E20', '01579B', '006064', '004D40', '', '000000'];
var forePalette = $('.fore-palette');
var backPalette = $('.back-palette');

for (var i = 0; i < colorPalette.length; i++) {
	forePalette.append('<a href="#" data-command="forecolor" data-value="' + '#' + colorPalette[i] + '" style="background-color:' + '#' + colorPalette[i] + ';" class="palette-item"></a>');
	backPalette.append('<a href="#" data-command="backcolor" data-value="' + '#' + colorPalette[i] + '" style="background-color:' + '#' + colorPalette[i] + ';" class="palette-item"></a>');
}

$('.wysiwyg-toolbar a').click(function (e) {
	var command = $(this).data('command');

	if (command == 'h1' || command == 'h2' || command == 'p') {
		document.execCommand('formatBlock', false, command);
	}
	if (command == 'forecolor' || command == 'backcolor') {
		document.execCommand($(this).data('command'), false, $(this).data('value'));
	}
	if (command == 'createlink' || command == 'insertimage') {
		url = prompt('Enter the link here: ', 'http:\/\/');
		document.execCommand($(this).data('command'), false, url);
	} else document.execCommand($(this).data('command'), false, null);
});

$('.wysiwyg-raw').blur(function () {
	$('.wysiwyg-upload').val($('.wysiwyg-raw').html());
});