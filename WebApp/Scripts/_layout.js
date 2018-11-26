/*
	javascript for dropdown list in menu of admin area
	- click on dropdown-btn -> show/hide the dropdown-content is nextSibling
 */
$(document).ready(function () {
	var param = window.location.pathname.replace("/Admin/", "").split('/');
	var menu = $('#' + param[0].toLocaleLowerCase());

	var btn = menu.find('button[class="dropdown-btn"]');
	var cnt = menu.find('li');

	btn.addClass("selected");

	switch (param.length) {
		case 1:
			cnt.first().children().addClass('entered');
			break;
		case 2:
			cnt.last().children().addClass('entered');
			break;
		default: break;
	}
});

$('.dropdown-btn').click(function () {
	var hasClass = $(this).hasClass("selected");

	$('.dropdown-btn').removeClass("selected");

	if (!hasClass) {
		$(this).addClass("selected");
	} else {
		$(this).removeClass("selected");
	}
});

/* Circle - Donut chart - dynamic chart */

// percent: int -> 0-100%
$('#btn-run-circle').click(function () { doClick(); });

doClick();

function doClick() {
	j = 0;
	var id = setInterval(runCircle, 10);

	function runCircle() {
		if (j > 100) {
			clearInterval(id);
		} else {
			percent = j;

			var activeColor = "#ccc";
			var deg1 = 90;
			var deg2 = (percent / 100 * 360);

			if (percent < 50) {
				activeColor = "#eee";
				deg1 = (percent / 100 * 360 - 90);
				deg2 = 0;
			}

			var one = document.getElementsByClassName("one");
			var two = document.getElementsByClassName("two");
			var cnt = document.getElementsByClassName("percent");

			for (i = 0; i < one.length; i++) {
				one[i].style.transform = "rotate(" + deg1.toString() + "deg)";
				one[i].style.webkitTransform = "rotate(" + deg1.toString() + "deg)";
				two[i].style.backgroundColor = activeColor;
				two[i].style.transform = "rotate(" + deg2.toString() + "deg)";
				two[i].style.webkitTransform = "rotate(" + deg2.toString() + "deg)";
				cnt[i].innerHTML = j + "%";
			}

			j++;
		}
	}
}