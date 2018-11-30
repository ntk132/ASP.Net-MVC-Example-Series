var selectStr = "";

$('.clickMe').click(function () {
	var url = "/Admin/Upload/LoadMedia";
	$.get(url, null, function (data) {
		var tmp = "";

		$.each($.parseJSON(data), function (index, value) {
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
		$('#img-upload').attr('src', $(this).attr('src'));
		$('#upload').attr("value", $(this).attr('src'));

		clicks = 0;
	}
}).on('dblclick', function(e) {
	e.preventDefault();
});

$('#ifrm').load(function () {
	$(this).contents().find('#frm-media-upload').submit(function () {

		$.get("/Admin/Upload/LoadMedia", null, function (data) {
			var tmp = "";

			$.each($.parseJSON(data), function (index, value) {
				tmp += "<li><img class=\"media-box\" src=\"" + value + "\" /></li>";
			});

			$('.media-gallery').html(tmp);
		});
	});
});

// Using for the case: adding new cata-items that's created after loaded page.
$(document).on('click', '.cata-delete', function () {
	$(this).parent().remove();
});

$('.cata-add').click(function () {
	var html = $(this).prevUntil().html() + '<li class="cata-item"><input id="food" name="food" value="" hidden /><span>NTK132</span><a class="cata-delete">x</a></li >';
	$(this).prevUntil().html(html);
});

$('.cata-add').bind('enterKey', function (e) {
	// TODO:
	$('form[]').submit(function (evt) { evt.preventDefault() });

	alert("NTK132");

	var html = $(this).nextUntil().html() + '<li class="cata-item"><input id="food" name="food" value="" hidden /><span>NTK132</span><a class="cata-delete">x</a></li >';
	$(this).nextUntil().html(html);
});

$('.cata-add').keyup(function (e) {
	if (e.keyCode == 13) {
		$(this).trigger('enterKey');
	}
});