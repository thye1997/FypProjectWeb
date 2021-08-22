var alertMessage1;
var closeMsg;
var bool;
function alertMessageFunc(response, message, msgid) {
    res = response;
    alertMessage1 = $(msgid);
    $(msgid).find('#app-text').remove();
    removeAlert(msgid);
    $(msgid).append(`<span id="app-text">` + message + `</span>`);
    if (response === 0) {
        $(msgid).addClass("alert-success").removeClass("hide-btn");
    }
    else if (response === 1) {
        $(msgid).addClass("alert-warning").removeClass("hide-btn");
    }
    else {
        $(msgid).addClass("alert-danger").removeClass("hide-btn");
    }
}

function closeMessageFunc(msgid) {
    removeAlert(msgid);
}

function removeAlert(msgid) {
    $(msgid).addClass("hide-btn").removeClass("alert-success");
    $(msgid).addClass("hide-btn").removeClass("alert-warning");
    $(msgid).addClass("hide-btn").removeClass("alert-danger");
}
