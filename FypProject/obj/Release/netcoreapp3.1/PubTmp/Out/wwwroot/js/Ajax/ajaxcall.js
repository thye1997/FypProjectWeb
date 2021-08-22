function ajaxPOST(obj, path,tableId,alertMessage,res) {
    $.ajax({
        type: "POST",
        url: path,
        data: obj,
        async: false,
        success: function (response) {
            res.response = response["exist"];
        }
    });
}

function datatablesAjax(table, path, userId, column, emptyLabel) {
    var tables = "#" + table;
    console.log("table name:" + tables);
    $("tables").DataTable({
        "lengthChange": false,
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "ordering": false,

        "ajax": {
            "url": path,
            "data": { "Id": userId },
            "type": "POST",
            "datatype": "json"
        },
        "drawCallback": function (settings) {
            // Here the response
            var response = settings.json;
            console.log(response);
        },
        columns: column,
        "language": {
            "emptyTable": emptyLabel,
        }
    });
}