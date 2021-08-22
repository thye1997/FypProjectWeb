$(document).ready(function () {

    var spHolidayDate = $('#HolidayDate');
    var spHolidayDescription = $('#HolidayDesc');
    var addSpHolidaybtn = $('#add-sp-holiday-btn');
    var table;
    var alertmsg = $('#sp-holiday-alert-message');

  table=  $('#specialHoliday-list-table').DataTable({
        "lengthChange": false,
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "ordering": false,
        "pageLength": 5,
        "draw": 1,
        
        "ajax": {
            "url": "/Appointment/RetrieveSpecialHoliday",
            "type": "POST",
            "datatype": "json",
            data: {
                dataRequest: "SpecialHoliday",
            },
        },
        "drawCallback": function (settings) {
            // Here the response
            var response = settings.json;
            console.log(response);
        },
        columns: [
            { "data": "id" },
            { "data": "description" },
            { "data": "date" },
        ],
        "language": {
            "emptyTable": "No Special Holiday Found."
        }
    });
           spHolidayDate.datepicker({
            format: 'dd/mm/yyyy',
            startDate: '+0d',
           });

    function AddSpecialHoliday() {
        var val1 = spHolidayDate.val();
        var val2 = spHolidayDescription.val();
        $.ajax({
            type: "POST",
            url: "/Appointment/AddSpecialHoliday",
            data: { "Date": val1, "Description": val2 },
            success: function (response) {
                var res = response["res"];
                console.log("res value =>"+res);
                var message = response["msg"];
                
                alertMessageFunc(res, message, alertmsg);
                table.ajax.reload();
            }
        });
        $('.sp-holiday-modal').modal('hide');
    }
    addSpHolidaybtn.click(function (e) {
        e.preventDefault();
        if ($('#spHolidayForm').valid()) {
            AddSpecialHoliday();
        }
    });

});
$("#spHolidayForm").validate({
    // Rules for form validation
    rules: {
        spHolidayDate: {
            required: true,
        },
        Description: {
            required: true,
        }
    },
    // Messages for form validation
    messages: {
        spHolidayDate: {
            required: "Date is required."
        },
        Description: {
            required: 'Description is required.'
        },
    },
    errorPlacement: function (error, element) {
        error.insertAfter(element);
    }
});