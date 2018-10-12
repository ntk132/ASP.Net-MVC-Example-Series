/*
	javascript for dropdown list in menu of admin area
	- click on dropdown-btn -> show/hide the dropdown-content is nextSibling
 */

/*
	Using for reload page
	if: location.hash != null that actived/selected element is available
	else: new page load
 */
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