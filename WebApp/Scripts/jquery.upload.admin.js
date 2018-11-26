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

$('.cata-delete').click(function () {
	$(this).parent().remove();
});