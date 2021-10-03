var table;
var alertMsg;
$(document).ready(function () {
    LoadQRCodeList();
})

function DownloadQRCode(id) {
    $.ajax({
        type: "GET",
        url: "/QRCode/DownloadSpecificQRCode",
        data: { "Id": id },
        success: function (response) {     
            table.ajax.reload();
        }
    });
}
function GenerateQRCode() {
    $.ajax({
        type: "GET",
        url: "/QRCode/GenerateQRCode",
        success: function (response) {
            alertMsg = $('#QRCode-list-alert-message');
            var message = "New QR Code generated successfully.";
            alertMessageFunc(0, message, alertMsg);
            DownloadGeneratedPDF();
            table.ajax.reload();
        }
    });
}
function LoadQRCodeList() {
    table = $('#QRCode-list-table').DataTable({
        "lengthChange": false,
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "ordering": false,
        "pageLength": 10,
        "draw": 1,

        "ajax": {
            "url": "/QRCode/GetQRCodeList",
            "type": "POST",
            "datatype": "json",
        },
        "drawCallback": function (settings) {
            // Here the response
            var response = settings.json;
            console.log(response);
        },

        columns: [
            { "data": "fileName" },
            {
                "data": "createdBy"
            },
            { "data": "createdOn" },
            {
                "data": "isActive",
                render: function (isActive) {
                    if (isActive) {
                        return "Active";
                    }
                    else return "Not Active";
                }         
            },
            {
                "data": null,
                render: function (obj) {
                    if (obj.isActive) {
                        return '<a class="view-appt-details btn btn-primary btn-sm" href="QRCode/DownloadSpecificQRCode?Id=' + obj.id + '"  target="_blank"">Download</a>'
                    }
                    else return "-";
                }
            }
        ],
        "language": {
            "emptyTable": "No QR Code Found."
        }
    });
}
function DownloadGeneratedPDF() {
    $.ajax({
        type: "GET",
        url: "/QRCode/GenerateTest",
        success: function (response) {
        }
    });
}
