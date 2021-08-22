
$(document).ready(function () {
    var sdEditBtn = $('#sd-edit-btn');
    var sdCancelBtn = $('#sd-cancel-btn');
    var sdSaveBtn = $('#sd-save-btn');
    var slotDurAlertMsg = $('#slot-duration-alert-message');
    $('#slotDurationSelect').attr('disabled', 'disabled');
    $.ajax({
        type: "POST",
        url: "/Appointment/SlotDuration",
        data: { "Id": 0 },
        success: function (response) {
            var res = response["res"];
            var duration = response["duration"];

            console.log("res value =>" + res);
            var message = response["msg"];
            var duration = response["duration"];
            $('#slotDurationSelect option[value=' + duration + ']').attr('selected', 'selected');
        }
    });


    sdEditBtn.click(function (e) {
        isAble = true;
        console.log("edit button clicked");
        e.preventDefault();
        $('#slotDurationSelect').removeAttr('disabled');
        EditBtnVisibility(sdEditBtn, sdCancelBtn, sdSaveBtn);

    });
    sdCancelBtn.click(function (e) {
        isAble = false;
        e.preventDefault();
        $('#slotDurationSelect').attr('disabled', 'disabled');
        EditBtnVisibility(sdEditBtn, sdCancelBtn, sdSaveBtn);
    });

    sdSaveBtn.click(function (e) {
       var val =  $('#slotDurationSelect').val();
        isAble = false;
        e.preventDefault();
        $('#slotDurationSelect').attr('disabled', 'disabled');
        EditBtnVisibility(sdEditBtn, sdCancelBtn, sdSaveBtn);

        $.ajax({
            type: "POST",
            url: "/Appointment/SlotDuration",
            data: { "Id": val },
            success: function (response) {
                var res = response["res"];
                var duration = response["duration"];

                console.log("res value =>" + res);
                var message = response["msg"];
                var duration = response["duration"];
                updateSelectDuration(duration);
                alertMessageFunc(res, message, slotDurAlertMsg);
            }
        });
    });
});


function updateSelectDuration(duration) {
    $('#slotDurationSelect option[value=' + duration + ']').attr('selected', 'selected');   
}

