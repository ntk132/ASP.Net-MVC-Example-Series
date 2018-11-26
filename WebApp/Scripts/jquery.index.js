$('#btn-th-list').click(function () {
	var card = $('.card');
	card.parent().removeClass("col-lg-3");

	card.find('img').addClass("th-list");
	card.find('.card-th').addClass("th-list");	
});

$('#btn-th-large').click(function () {
	var card = $('.card');
	card.parent().addClass("col-lg-3");

	card.find('img').removeClass("th-list");
	card.find('.card-th').removeClass("th-list");	
});