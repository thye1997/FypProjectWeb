$(document).ready(function () {

    LoadQueueList();
    LoadOnGoingList();
    LoadCompletedList();
});

function LoadQueueList() {
        $('#inqueue-appt-list-table').DataTable({
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
                    apptStatus: "2",
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
                { "data": "service" },
                { "data": "status" },
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
}

function LoadOnGoingList() {
    $('#ongoing-appt-list-table').DataTable({
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
                apptStatus: "3",
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
            { "data": "service" },
            { "data": "status" },
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
}

function LoadCompletedList() {
    $('#complete-appt-list-table').DataTable({
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
                apptStatus: 4,
                today: 0
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
            { "data": "service" },
            { "data": "status" },
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
}