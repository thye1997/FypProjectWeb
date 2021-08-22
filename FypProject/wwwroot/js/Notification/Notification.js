var table;
$(document).ready(function () {

    LoadNotificationList();
    $('#notificationForm').submit(function (e) {
        e.preventDefault();
        if ($(this).valid()) {
            SendNotification();
            $('.notification-modal').modal("hide");
            $('#notificationForm').trigger("reset");
        }
    })
})
$('#notificationForm').validate({
    // Rules for form validation
    rules: {
        notiTitle: {
            required: true,
        },
        notiContent: {
            required: true,
        },

    },
    // Messages for form validation
    messages: {
        notiTitle: {
            required: 'NRIC is required.',
        },
        notiContent: {
            required: 'Content is required.',
        }
    },
    errorPlacement: function (error, element) {
        error.insertAfter(element);
    }
});
$('#close-notification-modal').click(function (e) {
    e.preventDefault();
    $('#notificationForm').trigger("reset");
})

function LoadNotificationList() {
   table= $('#notification-list-table').DataTable({
        "lengthChange": false,
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "ordering": false,
        "pageLength": 10,
        "draw": 1,

        "ajax": {
            "url": "/Notification/NotificationList",
            "type": "POST",
            "datatype": "json",
        },
        "drawCallback": function (settings) {
            // Here the response
            var response = settings.json;
            console.log(response);
       },
       'columnDefs': [
           {
               'targets': 1,
               'createdCell': function (td, cellData, rowData, row, col) {
                   $(td).attr('id', 'contentTH');
               }
           }
       ],
        columns: [
            { "data": "title" },
            {
              "data": "content"
            },
            { "data": "createdOn" },
            { "data": "createdBy" },      
        ],
        "language": {
            "emptyTable": "No Notification Found."
        }
    });
}
function SendNotification() {
    var alertMsg = $('#notification-list-alert-message');
    var title = $('#title').val();
    var content = $('#content').val();
    $.ajax({
        type: "POST",
        url: "/Notification/AddNotification",
        dataType: "json",
        data: { "title": title, "content": content },
        async: false,
        success: function (response) {
            var msg = response['msg'];
            var res = response['res'];
            alertMessageFunc(res, msg, alertMsg);
            table.ajax.reload();
        }
    });
}