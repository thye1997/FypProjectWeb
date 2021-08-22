var cancelId;
var attendId;
var dateSelected;
var userId = $('#userid').val();
var apptStatus = $('#apptStatusInt').val();
var appointment = new Object();
var apptResult = new Object();
var apptResultArray = new Array();
var apptAlertMsg = $('#appt-alert-message');
$(document).ready(function () {
    LoadMedHistory();
    CheckApptStatus();
    CheckDropdownContainer();    
})

$('#reschedule-form').submit(function (e) {
    e.preventDefault();
    $(this).validate();
    AddRescheduleValidation();
    if ($(this).valid()) {
        RescheduleAppointment();
        $('.reschedule-appt-modal').modal("hide");
    }
})
$('#medPrescriptionForm').submit(function (e) {
    e.preventDefault();
    $(this).validate();
    AddMedPrescriptionValidation();
    if ($(this).valid()) {
        console.log("this is valid");
        AddMedPrescriptionList();
        $("#medPrescriptionForm").trigger('reset');
        $('.med-presc-modal').modal("hide");    
    }
    //console.log(apptResultArray);
})
$("#medPrescriptionForm").validate({
    // Rules for form validation
    rules: {
        medDesc: {
            required: true,
        }
    },
    // Messages for form validation
    messages: {
        medDesc: {
            required: 'Description is required.'
        },
    },
    errorPlacement: function (error, element) {
        error.insertAfter(element);
    }
});
$('#appt-result-form').submit(function (e) {
    e.preventDefault();
    AddAppointmentResult();
})

$('#edit-appt-btn').click(function (e) {
    e.preventDefault();
    $('.appt-result').removeClass('hide-btn');
    $('#appt-result-txtarea').removeAttr('disabled', 'disabled');
    $('#medPrescriptdropdownMenuButton').removeClass('hide-btn');
    $('#dropdownMenuButton').addClass('hide-btn');
    $('.appt-edit-btn-section').removeClass('hide-btn');
})

$('#medTypeSelect').change(function (e) {
    e.preventDefault();
    var typeSelected = $(this).val();
    if (typeSelected != "") {
       // $("#medNameSelect :selected").remove();
        GetMedicineList(typeSelected);
    }
    else {
        $('#medNameSelect').attr('disabled', 'disabled');
    }
})

$('#cancel-edit-appt').click(function (e){
    e.preventDefault();
    $('.appt-result').addClass('hide-btn');
    $('#dropdownMenuButton').removeClass('hide-btn');
    $('.med-prescription-list-div div[id]').each(function () {
        $(this).remove();
    });
    $('#appt-result-txtarea').val('');
})

$('#cancel-appt-btn').click(function (e) {
    e.preventDefault();
})

function RescheduleAppointment() {

    appointment.userId = userId;
    appointment.Id = $('#apptId').val();
    console.log("this one runned");
    $.ajax({
        type: "POST",
        url: "/Appointment/RescheduleAppointment",
        dataType: "json",
        data: appointment,
        async: false,
        success: function (response) {
            var msg = response['msg'];
            var res = response['res'];
            var obj = response['obj'];
            $('#apptDate').val(obj.date);
            $('#apptSlot').val(obj.slot);
            alertMessageFunc(res, msg, apptAlertMsg);
            window.onload();
        }
    });
}

function CancelAppointment(id) {
    var apptAlertMsg = $('#appt-alert-message');
    if (id > 0) {
        cancelId = id;
    }
    if (id <= 0) {
        $.ajax({
            type: "POST",
            url: "/Appointment/CancelAppointment",
            data: { "Id": cancelId },
            success: function (response) {
                var msg = response['msg'];
                var res = response['res'];
                var apptStatus = response['apptStatus'];
                $('#apptStatus').val(apptStatus);
                alertMessageFunc(res, msg, apptAlertMsg);
                $('#dropdownMenuButton').addClass("hide-btn");
            }
        });

    }
}

function NextOngoingAppointment(id) {
    var apptAlertMsg = $('#appt-alert-message');
    if (id > 0) {
        attendId = id;
    }
    if (id <= 0) {
        $.ajax({
            type: "POST",
            url: "/Appointment/ChangeApptStatus",
            data: { "Id": attendId, "Status": 3 },
            success: function (response) {
                var msg = response['msg'];
                var res = response['res'];
                var apptStatus = response['apptStatus'];
                $('#apptStatus').val(apptStatus);
                alertMessageFunc(res, msg, apptAlertMsg);
                $('#dropdownMenuButton').addClass("hide-btn");
            }
        });

    }
}

function LoadMedHistory() {
    $('#medHistory-list-table').DataTable({
        "lengthChange": false,
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "ordering": false,
        "pageLength": 5,
        "draw": 1,
        "ajax": {
            "url": "/User/getMedHistory",
            "data": { "Id": userId },
            "type": "POST",
            "datatype": "json",
        },
        "drawCallback": function (setting) {
            // Here the response
            var response = setting.json;
            console.log(response);
        },
        columns: [
            { "data": "id" },
            { "data": "date" },
            { "data": "slot" },
            { "data": "service" },
            { "data": "result" },
            {
                "data": null,
                render: function (obj) {
                    var formatted = obj.formattedMedicalPrescription;
                    if (formatted != "-") {
                        return formatted.replaceAll("/n", '<br>');
                    }
                    else {
                        return "No Medical Prescription Available.";
                    }
                }

            },
            { "data": "doctorName" },
        ],
        "language": {
            "emptyTable": "No medical history found.",
        }
    });
}

function CompleteAppointment(id) {
    var apptAlertMsg = $('#appt-alert-message');
    if (id > 0) {
        attendId = id;
    }
    if (id <= 0) {
        $.ajax({
            type: "POST",
            url: "/Appointment/ChangeApptStatus",
            data: { "Id": attendId, "Status": 4 },
            success: function (response) {
                var msg = response['msg'];
                var res = response['res'];
                var apptStatus = response['apptStatus'];
                $('#apptStatus').val(apptStatus);
                alertMessageFunc(res, msg, apptAlertMsg);
                $('#dropdownMenuButton').addClass("hide-btn");
            }
        });

    }
}

function CheckApptStatus() {
    console.log("checkapptstatus called.");
    console.log("appt status value:=>" + apptStatus);
    if (apptStatus == 4) {
        $(".appt-result").removeClass("hide-btn");
        $('#appt-result-txtarea').attr('disabled', 'disabled');
    }
}

function CheckDropdownContainer() {
    if ($('.dropdown-container').children().length <= 0) {
        $('#dropdownMenuButton').addClass('hide-btn');
    }
}

function LoadRescheduleModal() {
    var dateHoliday = new Array();
    var offDayList = new Array();
    var tomorrow = new Date();
    tomorrow.setDate(new Date().getDate() + 1);
    $.ajax({
        type: "POST",
        url: "/Appointment/GetApptRequestConfigData",
        dataType: "json",
        async: false,
        success: function (response) {
            for (i = 0; i < response.spHoliday.length; i++) {
                dateHoliday.push(response.spHoliday[i].date);
            }
            for (q = 0; q < response.offDay.length; q++) {
                offDayList.push(response.offDay[q]);
            }
        }
    });

    $('.reschedule-appt-modal').find('#ApptDate').datepicker({
        format: 'dd/mm/yyyy',
        startDate: tomorrow,
        datesDisabled: dateHoliday,
        daysOfWeekDisabled: offDayList,
    });
}

$('.reschedule-appt-modal').find('#ApptDate').change(function (e) {
    var selected= $(this).val();
    console.log(selected);
    dateSelected = selected;
    appointment.Date = selected;
    $('.reschedule-appt-modal').find('#timeSlotSelect').removeAttr('disabled');
})

$('.reschedule-appt-modal').find('#timeSlotSelect').change(function (e) {
    e.preventDefault();
    var val = $(this).val();
    console.log("slot picked=>", val);
    if (val == "") {
        $('.reschedule-appt-modal').find('#ApptTimeSlot').attr('disabled', 'disabled');
    }
    else {
        PopulateSpecificDateSlot(val);
    }
})

$('.reschedule-appt-modal').find('#ApptTimeSlot').change(function (e) {
    var startTime = $(this).val();
    appointment.StartTime = startTime;
    console.log("appointment type =>" + appointment.ApptType);
})

function PopulateSpecificDateSlot(slot) {
    $.ajax({
        type: "POST",
        url: "/Appointment/LoadSpecificTimeSlot",
        dataType: "json",
        data: { slot: slot, date: dateSelected },
        async: false,
        success: function (response) {
            console.log(response.timeslots);
            spSlotList = response.timeslots;
            PopulateApptSlot(response.timeslots);
            $('.reschedule-appt-modal').find('#ApptTimeSlot').removeAttr('disabled');
        }
    });
}

function PopulateApptSlot(response) {
    var arrMaxlength = response.length;
    console.log("value of maxlength=>" + arrMaxlength);
    var select = $('.reschedule-appt-modal').find('#ApptTimeSlot');
    select.find('option').not(":selected").remove();
    select.find('option:selected').hide();
    select.append('<option value="' + "" + '">' + "Select Time" + '</option>');
    for (i = 0; i < response.length - 1; i++) {
        select.append('<option value="' + response[i].startTime + '">' + response[i].slot + '</option>');
    }
}

function AddRescheduleValidation() {

    $("#reschedule-form").find('#ApptDate').rules("add", {
        required: true,
        messages: {
            required: 'Date is required.',
        }
    });
    $("#reschedule-form").find('#timeSlotSelect').rules("add", {
        required: true,
        messages: {
            required: 'Time slot is required.',
        }
    });
    $("#reschedule-form").find('#ApptTimeSlot').rules("add", {
        required: true,
        messages: {
            required: 'Appointment slot is required.',
        }
    });
 
}

function AddMedPrescriptionValidation() {
    $('#medPrescriptionForm').find('#medTypeSelect').rules("add", {
        required: true,
        messages: {
            required: 'Medicine type is required.',
        }
    });
    $('#medPrescriptionForm').find('#medNameSelect').rules("add", {
        required: true,
        messages: {
            required: 'Meidicine name is required.',
        }
    });
    $('#medPrescriptionForm').find('#medDesc').rules("add", {
        required: true,
        messages: {
            required: 'Description is required.',
        }
    });
}

function AddMedPrescriptionList() {
    var count = $('.med-prescription-list-div').children().length;
    var medType = $('#medTypeSelect').val();
    var medName = $('#medNameSelect option:selected').text();
    var medicineId = $('#medNameSelect option:selected').val();
    var desc = $('#medDesc').val();
    var apptresult = {
        Id: medicineId,
        description: desc
    }
    apptResultArray.push(apptresult);
    $('.med-prescription-list-div').append(
        '<div class="row" id=' + count + '>(' + medType + ')-' + medName + ' - ' + desc + '<a id=' + count + ' style="cursor:pointer;" onclick="DeleteMedPrescription(' + count + ')" class="ml-2"><i class="bi bi-x-circle" style="color:blue;"></i></a></div>'
    )
}

function GetMedicineList(Type) {
    $.ajax({
        type: "POST",
        url: "/Medicine/GetSpecificTypeMedicine",
        dataType: "json",
        data: { Type: Type },
        async: false,
        success: function (response) {
            PopulateMedicineList(response.data.list)
        }
    });
}

function PopulateMedicineList(list) {
    var select = $('#medNameSelect');
    select.find('option').not(":selected").remove();
    select.find('option:selected').hide();
    select.append('<option value="' + "" + '">' + "Select Medicine" + '</option>');
    for (i = 0; i < list.length; i++) {
        select.append('<option value="' + list[i].id + '">' + list[i].medName + '</option>');
    }
    select.removeAttr('disabled');
}

function DeleteMedPrescription(id) {
    console.log("id of prescription clicked: " + id);
    $('.med-prescription-list-div').find('#' + id).remove();
    for (var i = 0; i < apptResultArray.length; i++) {
        if (i=== id) {
            apptResultArray.splice(i, 1);
        }
    }
    console.log(apptResultArray);
}

function AddAppointmentResult() {
    var apptId = $('#apptId').val();
    var result = $('#appt-result-txtarea').val();


    $.ajax({
        type: "POST",
        url: "/Appointment/AddAppointmentResult",
        dataType: "json",
        data: { userId: userId, appointmentId: apptId, result: result, PrescriptionList: apptResultArray },
        async: false,
        success: function (response) {
            var msg = response['msg'];
            var res = response['res'];

            $('#medPrescriptdropdownMenuButton').addClass('hide-btn');
            $('.appt-edit-btn-section').addClass('hide-btn');
            alertMessageFunc(res, msg, apptAlertMsg);
            $('#appt-result-txtarea').attr('disabled', 'disabled');
            $('#dropdownMenuButton').removeClass('hide-btn');
            $('.med-prescription-list-div div[id]').find('a[id]').each(function () {
                $(this).remove();
            });

        }
    });
}