var isAble = false;
var cancelBtn = $('#cancel-btn');
var saveBtn = $('#save-btn');
var editBtn = $('#edit-btn');
var addMedHistory = $('#add-med-history');
var onEditBtnClick = false;

$(document).ready(function () {
    //console.log(`value is isable=> ${isAble}`);
    btnClick();
    btnVisibility();
    if (!isAble) {
        disableAll();
    }
});

function enableAll() {
    $('#DOB').prop("disabled", false);
    //$('#nric').prop("disabled", false);
    $('#genderSelect').prop("disabled", false);
    $('#name').prop("disabled", false);
    $('#phoneNumber').prop("disabled", false);
}
function disableAll() {
    $('#DOB').prop("disabled", true);
    //$('#nric').prop("disabled", true);
    $('#genderSelect').prop("disabled", true);
    $('#name').prop("disabled", true);
    $('#phoneNumber').prop("disabled", true);

}
function btnVisibility() {
    if (!isAble) {
        cancelBtn.closest("div").addClass("hide-btn");
        saveBtn.closest("div").addClass("hide-btn");
        //addMedHistory.addClass("hide-btn");
        editBtn.closest("div").removeClass("hide-btn");
    }
    else {
        cancelBtn.closest("div").removeClass("hide-btn");
        saveBtn.closest("div").removeClass("hide-btn");
        //addMedHistory.removeClass("hide-btn");
        editBtn.closest("div").addClass("hide-btn");
    }

}

function btnClick() {
    //edit btn click
    editBtn.click(function (e) {
        isAble = true;
        btnVisibility();
        e.preventDefault();
        if (isAble == true) {
            enableAll();
        }
        else {
            disableAll();
        }

    });

    //cancel btn click
    cancelBtn.click(function (e) {
        e.preventDefault();
        if (isAble == true) {
            isAble = false;
            btnVisibility();
            disableAll();
        }
    });
}