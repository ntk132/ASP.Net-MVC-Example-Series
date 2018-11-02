(function (window, undefined) {
	var selectStr = "";

	$('.clickMe').click(function () {
		var url = "/Admin/User/LoadMedia";
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

	$(document).on('click', '.media-box', function () {
		alert($(this).attr('src'));
		selectStr = $(this).attr('src');
	});

	$('#frm-media-upload').submit(function (event) {
		// Stop form's submitting defaultly
		event.preventDefault();

		// Gte value to submit
		var $frm = $(this),
			files = $frm.find('input[name="files"]').val(),
			url = "/Admin/User/Upload"/*$frm.attr('action')*/;

		// Sending data using post
		var posting = $.post(url, { files: files });

		posting.done(function (data) {
			var tmp = "";

			$.each($.parseJSON(data), function (index, value) {
				tmp += "<li><img class=\"media-box\" src=\"" + value + "\" /></li>";
			});

			$('.media-gallery').html(tmp);
			$('#myModal').modal('show');
		});
	});
});