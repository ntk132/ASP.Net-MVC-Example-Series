var colorPalette = ['000000', '', '', 'ffffff'];
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

$('#btn-txt').click(function () {
	$('#txt-test').text($('#editor').html());
});