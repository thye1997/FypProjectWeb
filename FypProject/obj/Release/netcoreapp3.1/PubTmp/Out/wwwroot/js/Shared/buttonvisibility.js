
function EditBtnVisibility(editId, cancelId, saveId) {
    if (!isAble) {
        $(cancelId).closest("div").addClass("hide-btn");
        $(saveId).closest("div").addClass("hide-btn");
        //addMedHistory.addClass("hide-btn");
        $(editId).closest("div").removeClass("hide-btn");
    }
    else {
        $(cancelId).closest("div").removeClass("hide-btn");
        $(saveId).closest("div").removeClass("hide-btn");
        //addMedHistory.removeClass("hide-btn");
        $(editId).closest("div").addClass("hide-btn");
    }

}