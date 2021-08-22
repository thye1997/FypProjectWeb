
$(function () {
    $("#userForm").validate({
        // Rules for form validation
        rules: {
            nric: {
                required: true,
                regex: /^\d{6}-\d{2}-\d{4}$/

            },
            fullName: {
                required: true,
            },
            phoneNumber: {
                required: true,
                regex: /^\d{3}-\d{7,8}$/

            },
            DOB: {
                required: true,
            },
            Gender: {
                required: true,
            }
        },
        // Messages for form validation
        messages: {
            nric: {
                required: 'NRIC is required.',
                regex: 'Invalid NRIC format.'
            },
            fullName: {
                required: 'Full Name is required.'
            },
            phoneNumber: {
                required: 'Phone Number is required.',
                regex: "Invalid phone number format."
            },
            DOB: {
                required: 'Date of birth is required.'
            },
        },

        errorPlacement: function (error, element) {
            if (element.attr("name") == "Gender") {
               $("#genderErr").text("Please select gender.");
            }
            else {
                error.insertAfter(element);
            }
        }
       
    });

});




$("#medHistoryForm").validate({
    // Rules for form validation
    rules: {
        Description: {
            required: true,
        }
    },
    // Messages for form validation
    messages: {
        Description: {
            required: 'Description is required.'
        },
    },
    errorPlacement: function (error, element) {
        error.insertAfter(element);
    }
});