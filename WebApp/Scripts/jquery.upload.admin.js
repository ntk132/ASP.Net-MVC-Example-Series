var selectStr = "";

$('.clickMe').click(function () {
	var url = "/Admin/Upload/LoadMedia";
	$.get(url, null, function (data) {
		var tmp = "";

		$.each($.parseJSON(data), function (index, value) {
			//tmp += "<li><a class=\"media-box\"><img src=\"" + value + "\" /></a></li>";
			tmp += "<li><img class=\"media-box\" src=\"" + value + "\" /></li>";
		});

		$('.media-gallery').html(tmp);
		$('#myModal').modal('show');
	});
});
//$("#closebtn").on('click',function(){
//    $('#myModal').modal('hide');

$("#closbtn").click(function () {
	$('.media-gallery').html("");
	$('#myModal').modal('hide');
});

$("#btn-cancel").click(function () {
	alert($('.media-box').length);
	$('.media-gallery').html("");
	$('#myModal').modal('hide');
});

//
$("#btn-select").click(function () {
	$('.media-gallery').html("");
	$('#myModal').modal('hide');

	$('#test').attr('src', selectStr);
});

var DELAY = 700, clicks = 0, timer = null;
$(document).on('click', '.media-box', function () {
	clicks++;

	if (clicks === 1) {
		timer = setTimeout(function () {
			// CODE FOR SINGLE CLICK EVENT in here.
			selectStr = $(this).attr('src');
			clicks = 0;
		}, DELAY);
	} else {
		// CODE FOR SINGLE CLICK EVENT in here.
		clearTimeout(timer);

		$('.media-gallery').html("");
		$('#myModal').modal('hide');
		$('#img-thumbnail').attr('src', $(this).attr('src'));

		clicks = 0;
	}
}).on('dblclick', function(e) {
	e.preventDefault();
});

$('input[name="files"]').change(function () {
	var files = $(this).get(0).files;
	var txt = "";

	for (var i = 0; i < files.length; i++) {
		txt += "<li><p>" + files[i].name + "</p></li>";
	}

	$('#lst-img').html(txt);
});

$('#btn-upload').click(function () {
	var ifrm = $('#ifrm');
	var src = ifrm.attr('src');
	var input = $('input[name="files"]');
	var url = "/Admin/Upload/LoadMedia";

	ifrm.contents().find('input[name="files"]').get(0).files = input.get(0).files;
	ifrm.contents().find('#frm-media-upload').submit();

	$('#lst-img').html("");
	$('.media-gallery').html("");
		
	$.get(url, null, function (data) {
		var tmp = "";

		$.each($.parseJSON(data), function (index, value) {
			tmp += "<li><img class=\"media-box\" src=\"" + value + "\" /></li>";
		});

		$('.media-gallery').html(tmp);
	});

	// Clear data in input
	input.val("");
	ifrm.attr('src', src);
});