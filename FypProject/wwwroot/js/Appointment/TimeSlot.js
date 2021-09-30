var slotss;
var startFirst = false;
var endFirst = false;
var timeSlotTable = $('#timeSlot-list-table');
$(document).ready(function () {
    var updateBtn = $('#update-btn');
    var confirmUpdateBtn = $('#confirm-update-btn');
    updateBtn.click(function (e) {
        e.preventDefault();
        $('select[id=timeSlotSelect]').on('change', function () {
            if (this.value == "") {
                if ($("#timeslot-time").hasClass("hide-btn") == false) {
                    $("#timeslot-time").addClass("hide-btn");
                }           
            }
            else if (this.value == 1) {
                loadTimeSlot(this.value);
            }
            else if (this.value ==2)
            {
                loadTimeSlot(this.value);
            }
            else if (this.value == 3) {
                loadTimeSlot(this.value);
            }
        });
    });


    confirmUpdateBtn.click(function (e) {
        e.preventDefault();
        if ($('#timeSlotForm').valid()) {
            AddTimeSlot();
        }
    });
})


function populateStartOption(slots, index = 0) {
    var indexVal = 1;
    if (index > 0) {
        indexVal = index;
        for (i = 0; i < indexVal; i++) {
            $('select[id=timeSlotStart]').append('<option value="' + slots[i].slotKey + '">' + slots[i].slot + '</option>');
        }
    }
    else {
        for (i = 0; i < slots.length -1; i++) {
            $('select[id=timeSlotStart]').append('<option value="' + slots[i].slotKey + '">' + slots[i].slot + '</option>');
        }
    }
    
}

function populateEndOption(slots, index = 0) {
    var indexVal=1;
    if (index > 0) {
        console.log("position if index=>" + index);
        for (i = index+1; i < slots.length; i++) {
            $('select[id=timeSlotEnd]').append('<option value="' + slots[i].slotKey + '">' + slots[i].slot + '</option>');
        }
    }
    else {
        for (i = indexVal; i < slots.length; i++) {
            $('select[id=timeSlotEnd]').append('<option value="' + slots[i].slotKey + '">' + slots[i].slot + '</option>');
        }
    }
    console.log("value position=>" + indexVal);
    startFirst = false;
    $("#timeslot-time").removeClass("hide-btn");
}

$('select[id=timeSlotStart]').on('change', function (e) {
    e.preventDefault();
    startFirst = true;
    var text = $(this).find('option:selected').text();
    var pos = findValPos(text);
    $('select[id=timeSlotEnd] option').not(":selected").remove();
    $('select[id=timeSlotEnd] option:selected').hide();
        $('select[id=timeSlotEnd]').append('<option value="' + "" + '">' + "Select Time" + '</option>');
        populateEndOption(slotss, pos); 
});

$('select[id=timeSlotEnd]').on('change', function (e) {
    e.preventDefault();
    endFirst = true;
    var text = $(this).find('option:selected').text();
    var pos = findValPos(text);
    if (startFirst == false) {
        $('select[id=timeSlotStart] option').not(":selected").remove();
        $('select[id=timeSlotStart] option:selected').hide();
        $('select[id=timeSlotStart]').append('<option value="' + "" + '">' + "Select Time" + '</option>');
        populateStartOption(slotss, pos);
    }


     
});

function loadTimeSlot(slot) {
    $.ajax({
        type: "GET",
        url: "/Appointment/LoadSlotTime",
        data: { slot: slot },
        dataType: "json",
        async:false,
        success: function (response) {
            resetTimeSlot();
            var res = response.data.timeslots;
            slotss = response.data.timeslots;
            populateStartOption(res);
            populateEndOption(res);
        }
    });
}

function resetTimeSlot() {
    $('select[id=timeSlotStart] option').remove();
    $('select[id=timeSlotEnd] option').remove();

    $('select[id=timeSlotStart]').append('<option value="' + "" + '">' + "Select Time" + '</option>');
    $('select[id=timeSlotEnd]').append('<option value="' + "" + '">' + "Select Time" + '</option>');

}

function findValPos(text) {
    for (i = 0; i < slotss.length; i++) {
        if (slotss[i].slot == text)
            return i;
    }
}

function AddTimeSlot() {
    var id = $('#timeSlotSelect').val();
    var val1 = $('#timeSlotSelect option:selected').text();
    var val2 = $('#timeSlotStart').val();
    var val3 = $('#timeSlotEnd').val();

    var startTxt = $('#timeSlotStart  option:selected').text();
    var endTxt = $('#timeSlotEnd  option:selected').text();

    $.ajax({
        type: "PUT",
        url: "/Appointment/UpdateTimeSlot",
        data: { "Id": id, "Slot": val1, "Start": val2, "End": val3 },
        success: function (response) {
            var res = response["res"];
            console.log("res value =>" + res);
            var message = response["msg"];
            var alertmsg = $('#timeslot-alert-message');
            alertMessageFunc(res, message, alertmsg);
            $('#timeSlotTR-'+id+' > #'+id+'').each(function () {
                $(this).find('#slotStartTD').text(startTxt);
                $(this).find('#slotEndTD').text(endTxt);
            });             
        }
    });
    $('.timeslot-modal').modal('hide');
}





$("#timeSlotForm").validate({
    // Rules for form validation
    rules: {
        timeSlot: {
            required: true,
        },
        timeSlotStart: {
            required: true,
        },
        timeSlotEnd: {
            required: true,
        },
    },
    // Messages for form validation
    messages: {
        timeSlot: {
            required: 'Slot is required.',
        },
        timeSlotStart: {
            required: 'Start time is required.',
        },
        timeSlotEnd: {
            required: 'End time is required.',
        },
    },
    errorPlacement: function (error, element) {
        error.insertAfter(element);
    }
});