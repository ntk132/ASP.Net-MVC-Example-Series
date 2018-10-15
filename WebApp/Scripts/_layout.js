/*
	javascript for dropdown list in menu of admin area
	- click on dropdown-btn -> show/hide the dropdown-content is nextSibling
 */

/*
	Using for reload page
	if: location.hash != null that actived/selected element is available
	else: new page load
 */
var i, j;

if (localStorage.getItem("selected")) {
	var frtChld = document.getElementById(localStorage.getItem("selected").toString()).firstElementChild;
	frtChld.classList.toggle("selected");

	var nxtChld = frtChld.nextElementSibling;
	nxtChld.style.maxHeight = nxtChld.scrollHeight + "px";
}

var dropdownbtns = document.getElementsByClassName("dropdown-btn");

for (i = 0; i < dropdownbtns.length; i++) {
	dropdownbtns[i].addEventListener("click", dropdown_click);
}

/**/
function dropdown_click(e) {
	e.preventDefault();
	var j;
	/* Reset all dropdown button actived
	for (j = 0; j < dropdownbtns.length; j++) {
		dropdownbtns[j].classList.remove("active");
		dropdownbtns[j].nextSibling.style.maxHeight = null;
	}*/

	// Active the current button
	this.classList.toggle("selected");

	/*for (j = 0; j < dropdownbtns.length; j++) {

		dropdownbtns[j].nextElementSibling.style.maxHeight = null;
	}

	var dropdowcontent = this.nextElementSibling;
	if (dropdowcontent.style.maxHeight) {
		dropdowcontent.style.maxHeight = null;
	} else {
		dropdowcontent.style.maxHeight = dropdowcontent.scrollHeight + "px";
	}*/


	if (localStorage.getItem("selected") == this.parentNode.id) {
		localStorage.removeItem("selected");
	} else {
		localStorage.setItem("selected", this.parentNode.id);
	}
}

/* Circle - Donut chart - dynamic chart */

// percent: int -> 0-100%
var btnRun = document.getElementById("btn-run-circle");

btnRun.addEventListener("click", doClick);

doClick();

function doClick() {
	j = 0;
	var id = setInterval(runCircle, 10);

	function runCircle() {
		if (j > 100) {
			clearInterval(id);
		} else {
			percent = j;

			var activeColor = "#50c690";
			var deg1 = 90;
			var deg2 = (percent / 100 * 360);

			if (percent < 50) {
				activeColor = "#ccc";
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