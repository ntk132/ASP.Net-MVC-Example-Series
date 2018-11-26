var variable = "";

alert(variable);

$(".btn-comment-reply").click(function () {
	var frm = $('form[class="comment-form"]');
	var tmp = "<li>" + frm.html() + "</li>";
	var block = $(this).parent("comment-block");

	if (block.has('ul[class="sub-comment"]')) {
		block.find('.sub-comment').append(tmp);
	} else {
		block.append('<ul class="sub-comment">' + tmp + '</ul>');
	}
});

$('').click();