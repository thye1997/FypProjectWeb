var activeTab;
var submitAppt = $('#submit-appt-btn');
var currentForm;
var userLists;
var dateSelected;
var apptType;
var userId;
var spSlotList;
var appointment = new Object();
var user = new Object();
var apptListTable;
var pastListTable;
var noShowListTable;
var birthdayPicker = $('#Inputbirthday');
var checkInId;
var apptAlertMsg = $('#appt-list-alert-message');
$(document).ready(function () {
    LoadNoShowApptList();
    LoadApptList();
    LoadPastApptList();
    LoadUserList();
    CheckActiveTab();
    ApptDetails();
    birthdayPicker.datepicker({
        format: 'dd/mm/yyyy',
        startDate: '-100y',
        endDate: '+0d',
    });
    $('#PatientForm').submit(function (e) {
        e.preventDefault();
        $(this).validate();
        AddValidation();
        AddNewPatientValidation();
        if ($(this).valid()){
            console.log("it is valid");
            if (activeTab == 2) {
                AddAppointmentExistPatient();
            }
            if (activeTab == 1) {
                AddAppointmentNewPatient();

            }
            $('.request-appt-modal').modal("hide");
            $('#PatientForm').trigger("reset");

        }
    }
    );
});
$(document).on('input', '#userData', function () {
    var inputVal = $(this).val();
    var selected = $("#userDataList option[value='" + $("#userData").val() + "']").attr('data-val');
    userId = selected;
    console.log("value of userId=>" + selected);
    if (inputVal == "") {
        $('.additional-info').addClass('hide-btn');
    }
    else {
        $('.additional-info').removeClass('hide-btn');
        InitAdditionalInfo(selected);
    }
});
$('#close-reqAppt-modal').click(function (e) {
    e.preventDefault();
    $('#PatientForm').trigger("reset");
})
$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    CheckActiveTab();    
});
$('#PatientForm').validate({
    // Rules for form validation
    rules: {
        nric: {
            required: true,
        },
    },
    // Messages for form validation
    messages: {
        nric: {
            required: 'NRIC is required.',
        }
    },
    errorPlacement: function (error, element) {
        error.insertAfter(element);
    }

});
$('input[name=apptType]').change(function (e) {
    e.preventDefault();
    var val = $(this).val();
    ApptDetails(val);
    apptType = val;
    console.log("val of radio=>" + val);
});
$('#existPatientApptDate').change(function (e) {
    e.preventDefault();
    dateSelected = $(this).val();
    console.log("date selected=> " + dateSelected);
    $('.general-form').find('#timeSlotSelect').removeAttr('disabled');
});
$('.general-form').find('#timeSlotSelect').change(function (e) {
    e.preventDefault();
    var val = $(this).val();
    console.log("slot picked=>", val);
    if (val == "") {
        $('.exist-patient').find('#ApptTimeSlot').attr('disabled', 'disabled');
    }
    else {
        PopulateSpecificDateSlot(val);
    }
});
$('.general-form').find('#serviceSelect').change(function (e) {
    appointment.serviceId = $(this).val();
    console.log("service ID =>", appointment.serviceId);
});
$('.general-form').find('#ApptTimeSlot').change(function (e) {
    var startTime = $(this).val();
    appointment.StartTime = startTime;
    //console.log("start time =>" + appointment.StartTime + " end time =>" + appointment.EndTime + " Date =>" + appointment.Date);
    console.log("appointment type =>" + appointment.ApptType);

});
function LoadUserList() {
    $.ajax({
        type: "POST",
        url: "/User/GetUserList",
        dataType: "json",
        async: false,
        success: function (response) {
            var res = response["res"];
            userLists = response.data.userList;
            InitUserList(response.data.userList);
        }
    });
}
function ApptDetails() {
    var choice = 1;
    var dateHoliday = new Array();
    var offDayList = new Array();
    var tomorrow = new Date();
    tomorrow.setDate(new Date().getDate()+1);
    $.ajax({
        type: "POST",
        url: "/Appointment/GetApptRequestConfigData",
        dataType: "json",
        async: false,
        success: function (response) {
            for (i = 0; i < response.data.spHoliday.length; i++) {
                dateHoliday.push(response.data.spHoliday[i].date);
            }
            for (q = 0; q < response.data.offDay.length; q++) {
                offDayList.push(response.data.offDay[q]);
            }
            InitServiceList(response.data.service);
        }
    });
    if (choice != 0) {
        $('.general-form').find('.appt-details').removeClass('hide-btn');
        $('.general-form').find('.appt-common-details').removeClass('hide-btn');
        $('#existPatientApptDate').datepicker({
            format: 'dd/mm/yyyy',
            startDate: tomorrow,
            datesDisabled: dateHoliday,
            daysOfWeekDisabled: offDayList,
        });
    }
    else {
        $('.general-form').find('.appt-details').addClass('hide-btn');
        $('.general-form').find('.appt-common-details').removeClass('hide-btn');
    }
}
function CheckActiveTab() {
    if ($('.upcoming-search-input').hasClass('active')) {
        $('.upcoming-search-input').removeClass('hide-btn');
        $('.upcoming-table').removeClass('hide-btn');
        $('.past-table').addClass('hide-btn');
        $('.past-search-input').val("");
        $('.past-search-input').addClass('hide-btn');
        $('.noShow-table').addClass('hide-btn');
        $('.noShow-search-input').val("");
        $('.noShow-search-input').addClass('hide-btn');
    }
    else if ($('.past-search-input').hasClass('active')) {
        $('.upcoming-search-input').val("");
        $('.upcoming-search-input').addClass('hide-btn');
        $('.upcoming-table').addClass('hide-btn');
        $('.past-table').removeClass('hide-btn');
        $('.past-search-input').removeClass('hide-btn');
        $('.noShow-table').addClass('hide-btn');
        $('.noShow-search-input').val("");
        $('.noShow-search-input').addClass('hide-btn');
    }
    else if ($('.noShow-search-input').hasClass('active')) {
        $('.past-search-input').val("");
        $('.past-search-input').addClass('hide-btn');
        $('.past-table').addClass('hide-btn');
        $('.noShow-table').removeClass('hide-btn');
        $('.noShow-search-input').removeClass('hide-btn');    
        $('.upcoming-search-input').val("");
        $('.upcoming-search-input').addClass('hide-btn');
        $('.upcoming-table').addClass('hide-btn');     
    }


    if ($('.new-patient').hasClass('active')) {
        activeTab = 1;
        console.log("current active is new patient");
    }
    else {
        activeTab = 2;
        console.log("active tab value=>" + activeTab);
        console.log("current active is existing patient");
    }
}
function InitUserList(userList) {
    for (i = 0; i < userList.length; i++) {
        $('#userDataList').append('<option value="' + userList[i].nric + '" data-val="' + userList[i].id + '">' + userList[i].fullName + "/" + userList[i].nric + '</option>');
    }
}
function InitServiceList(serviceList) {
    var select = $('.general-form').find('#serviceSelect');
    select.find('option').not(":selected").remove();
    select.find('option:selected').hide();
    select.append('<option value="' + "" + '">' + "Select Service" + '</option>');
    for (i = 0; i < serviceList.length; i++) {
        select.append('<option value="' + serviceList[i].id + '">' + serviceList[i].serviceName + '</option>');
    }
    select.removeAttr('disabled');
}
function InitAdditionalInfo(Id) {
    for (i = 0; i < userLists.length; i++) {
        if (userLists[i].id == Id) {
            $('#existPatientNRIC').val(userLists[i].nric);
            $('#existPatientName').val(userLists[i].fullName);
            $('#existPatientPhone').val(userLists[i].phoneNumber);
            $('#existPatientGender').val(userLists[i].gender);
            $('#existPatientDOB').val(userLists[i].dob);
        }
    }
}
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
            $('.general-form').find('#ApptTimeSlot').removeAttr('disabled');
        }
    });
}
function PopulateApptSlot(response) {
    var arrMaxlength = response.length;
    console.log("value of maxlength=>" + arrMaxlength);
    var select = $('.general-form').find('#ApptTimeSlot');
    select.find('option').not(":selected").remove();
    select.find('option:selected').hide();
    select.append('<option value="' + "" + '">' + "Select Time" + '</option>');
    for (i = 0; i < response.length-1; i++) {
    select.append('<option value="' + response[i].startTime + '">' + response[i].slot + '</option>'); 
    }
}
function AddValidation() {
    if (apptType != 0 || apptType != 1) {
        $('#apptTypeRad').rules("add", {
            required: true,
        });
    }
    if (activeTab == 1 || activeTab == 2) //common input that must have value
    {
        $('#apptTypeRad').rules("add", {
            required: true,
        });
        if (apptType == 1) {
            $(".general-form").find('#existPatientApptDate').rules("add", {
                required: true,
                messages: {
                    required: 'Appointment date is required.',
                }
            });
            $(".general-form").find('#timeSlotSelect').rules("add", {
                required: true,
                messages: {
                    required: 'Time slot is required.',
                }
            });
            $(".general-form").find('#ApptTimeSlot').rules("add", {
                required: true,
                messages: {
                    required: 'Appointment slot is required.',
                }
            });
        }  
    }

    if (activeTab == 2) {
        $(".general-form").find("#userData").rules("add", {
            required: true,
            messages: {
                required: 'Patient name is required.',
            }
        });
    }           
         $(".general-form").find('#serviceSelect').rules("add", {
            required: true,
            messages: {
                required: 'Service is required.',
            }
        });

    
    
}
function AddAppointmentExistPatient() {
    appointment.Date = $('.general-form').find('#existPatientApptDate').val();
    appointment.userId = userId;
    appointment.ApptType = 1;
    appointment.PatientType = 1;
    appointment.Note = $('.general-form').find('#noteArea').val();
    $.ajax({
        type: "POST",
        url: "/Appointment/AddAppointment",
        dataType: "json",
        data: appointment,
        async: false,
        success: function (response) {
            var msg = response['msg'];
            var res = response['res'];
            alertMessageFunc(res, msg, apptAlertMsg);
            apptListTable.ajax.reload();
        }
    });
}
function AddAppointmentNewPatient() {
    appointment.fullName = $('#InputFullName').val();
    appointment.NRIC = $('#InputNRIC').val();
    appointment.Gender = $('#genderSelect').val();;
    appointment.PhoneNumber = $('#InputPhoneNumber').val();;
    appointment.DOB = $('#Inputbirthday').val();;
    appointment.Date = $('.general-form').find('#existPatientApptDate').val();
    appointment.ApptType = 1;
    appointment.PatientType = 0;
    appointment.Note = $('.general-form').find('#noteArea').val();
    $.ajax({
        type: "POST",
        url: "/Appointment/AddAppointment",
        dataType: "json",
        data: appointment,
        async: false,
        success: function (response) {
            var msg = response['msg'];
            var res = response['res'];
            alertMessageFunc(res, msg, apptAlertMsg);
            apptListTable.ajax.reload();
        }
    });
}
function LoadApptList() {
    var apptStatus = new Array();
    apptStatus.push(0);
    apptListTable= $('#appt-list-table').DataTable({
        "lengthChange": false,
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "ordering": false,
        "pageLength": 10,
        "draw": 1,

        "ajax": {
            "url": "/Appointment/RetriveAppointmentList",
            "type": "POST",
            "datatype": "json",
            data: {
                apptStatus: apptStatus,
            },
        },
        "drawCallback": function (settings) {
            // Here the response
            var response = settings.json;
            console.log(response);
        },
        columns: [
            { "data": "fullName" },
            { "data": "nric" },
            { "data": "phoneNumber" },
            { "data": "date" },
            { "data": "slot" },
            {"data": "service"},
            { "data": "status" },
            {
               "data":null,
                render: function (obj) {
                    obj.checkIn = true
                    if (obj.checkIn ==true) {
                        return '<a class="view-appt-details btn btn-primary btn-sm" href="/Appointment/AppointmentDetail/' + obj.id + '">View</a>' +
                            '<a class="check-in-details btn btn-success btn-sm" data-toggle="modal" data-target=".checkIn-appt-modal" style="margin-left:5px;" href=# onclick="CheckInAppt(' + obj.id + ')">Check-In</a>';
                    } else {
                        return '<a class="view-appt-details btn btn-primary btn-sm" href="/Appointment/AppointmentDetail/' + obj.id + '">View</a>';
                    }
                }
            },
        ],
        "language": {
            "emptyTable": "No Appointment Found."
        }
    });
    $('#upcomingsearchUserInfo').keyup(function () {
        apptListTable.search($(this).val()).draw();
    })
}
function LoadPastApptList() {
    var apptStatus = new Array();
    apptStatus.push(4);
    apptStatus.push(201);
    //apptStatus.push(301);

    pastListTable = $('#past-appt-list-table').DataTable({
        "lengthChange": false,
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "ordering": false,
        "pageLength": 10,
        "draw": 1,

        "ajax": {
            "url": "/Appointment/RetriveAppointmentList",
            "type": "POST",
            "datatype": "json",
            data: {
                apptStatus: apptStatus,
            },
        },
        "drawCallback": function (settings) {
            // Here the response
            var response = settings.json;
            console.log(response);
        },
        columns: [
            { "data": "fullName" },
            { "data": "nric" },
            { "data": "phoneNumber" },
            { "data": "date" },
            { "data": "slot" },
            { "data": "service"},
            {
                "data": "status",
                render: function (status) {
                    if (status == "NoShow")
                        return "No-Show";
                    else return status;
                }
            },
            {
                "data": null,
                render: function (obj) {
                    return '<a class="view-appt-details btn btn-primary btn-sm" href="/Appointment/AppointmentDetail/' + obj.id + '">View</a>';
                }
            },
        ],
        "language": {
            "emptyTable": "No Appointment Found."
        }
    });
    $('#pastsearchUserInfo').keyup(function () {
        pastListTable.search($(this).val()).draw();
    })
}
function LoadNoShowApptList() {
    console.log("load no show");
    var apptStatus = new Array();
    apptStatus.push(301);

    noShowListTable = $('#no-show-appt-list-table').DataTable({
        "lengthChange": false,
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "ordering": false,
        "pageLength": 10,
        "draw": 1,

        "ajax": {
            "url": "/Appointment/RetriveAppointmentList",
            "type": "POST",
            "datatype": "json",
            data: {
                apptStatus: apptStatus,
            },
        },
        "drawCallback": function (settings) {
            // Here the response
            var response = settings.json;
            console.log(response);
        },
        columns: [
            { "data": "fullName" },
            { "data": "nric" },
            { "data": "phoneNumber" },
            { "data": "date" },
            { "data": "slot" },
            { "data": "service"},
            {
                "data": "status",
                render: function (status) {
                    if (status == "NoShow")
                        return "No-Show";
                    else return status;
                }
            },
            {
                "data": null,
                render: function (obj) {
                    return '<a class="view-appt-details btn btn-primary btn-sm" href="/Appointment/AppointmentDetail/' + obj.id + '">View</a>';
                }
            },
        ],
        "language": {
            "emptyTable": "No Appointment Found."
        }
    });
    $('#noShowsearchUserInfo').keyup(function () {
        noShowListTable.search($(this).val()).draw();
    })
}
function AddNewPatientValidation() {
    $.validator.addMethod(
        "regex",
        function (value, element, regexp) {
            return this.optional(element) || regexp.test(value);
        },
        "Please check your input."
    );

    $(".general-form").find('#InputNRIC').rules("add", {
        required: true,
        regex: /^\d{6}-\d{2}-\d{4}$/,
        messages: {
            required: 'NRIC is required.',
            regex: 'Incorrect NRIC format.',
        }
    });
    $(".general-form").find('#InputFullName').rules("add", {
        required: true,
        messages: {
            required: 'Full name is required.',
        }
    });
    $(".general-form").find('#InputPhoneNumber').rules("add", {
        required: true,
        regex: /^\d{3}-\d{7,8}$/,
        messages: {
            required: 'Phone number is required.',
            regex: 'Incorrect phone number format.',
        }
    });

}
function CheckInAppt(id) {
    if (id > 0) {
        checkInId = id;
    }
    if (id <= 0)
    {
        $.ajax({
            type: "POST",
            url: "/Appointment/CheckInAppt",
            data: { "Id": checkInId },
            success: function (response) {
                var msg = response['msg'];
                var res = response['res'];
                alertMessageFunc(res, msg, apptAlertMsg);
                apptListTable.ajax.reload();
            }
        });

    }
}
