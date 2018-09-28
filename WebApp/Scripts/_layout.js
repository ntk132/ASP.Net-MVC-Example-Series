/*
	javascript for dropdown list in menu of admin area
	- click on dropdown-btn -> show/hide the dropdown-content is nextSibling
 */

var dropdownbtns = document.getElementsByClassName("dropdown-btn");
var i;

for (i = 0; i < dropdownbtns.length; i++) {
	dropdownbtns[i].addEventListener("click", dropdown_click);
}

/**/
function dropdown_click() {
	var j;
	/* Reset all dropdown button actived
	for (j = 0; j < dropdownbtns.length; j++) {
		dropdownbtns[j].classList.remove("active");
		dropdownbtns[j].nextSibling.style.maxHeight = null;
	}*/

	// Active the current button
	this.classList.toggle("selected");

	var dropdowcontent = this.nextElementSibling;
	if (dropdowcontent.style.maxHeight) {
		dropdowcontent.style.maxHeight = null;
	} else {
		dropdowcontent.style.maxHeight = dropdowcontent.scrollHeight + "px";
	}
}