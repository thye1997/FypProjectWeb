$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});




var appt_dropdown = document.getElementById("appt-dropdown");
var config_dropdown = document.getElementById("config-dropdown");

appt_dropdown.onclick = onApptDropdownClick;
config_dropdown.onclick = onConfigDropdownClick;

function onApptDropdownClick() {
    var dropdownMenu = document.getElementById("appt-dropdown-container");
    if (dropdownMenu.style.display === "block") {
        dropdownMenu.style.display = "none";
    }
    else {
        dropdownMenu.style.display = "block";
    }
    dropdown.classList.add("active");
}

function onConfigDropdownClick() {
    var dropdownMenu = document.getElementById("config-dropdown-container");
    if (dropdownMenu.style.display === "block") {
        dropdownMenu.style.display = "none";
    }
    else {
        dropdownMenu.style.display = "block";
    }
    dropdown.classList.add("active");
}