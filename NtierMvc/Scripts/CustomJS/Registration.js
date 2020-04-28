var InstituteRegistrationApp = function () {

    var validateRegistrationForm = function () {
        $.validator.addMethod(
            "regex",
            function (value, element, regexp) {
                if (regexp.constructor != RegExp)
                    regexp = new RegExp(regexp);
                else if (regexp.global)
                    regexp.lastIndex = 0;
                return this.optional(element) || regexp.test(value);
            },
            "Please check your input."
        );
        jQuery.validator.addMethod("notEqual", function (value, element, param) {
            return this.optional(element) || value != $(param).val();
        }, "This has to be different...");
        jQuery.validator.addMethod("lettersonly", function (value, element) {
            return this.optional(element) || /^[a-z\s]+$/i.test(value);
        });

        var validator = $('#formCreateAccount').validate({
            //errorElement: 'span', //default input error message container
            //errorClass: 'help-block', // default input error message class
            focusInvalid: true, // do not focus the last invalid input
            rules: {
                'RegistrationModel.ApplicationTypeID': {
                    required: function (element) { return $("#organizationType").val() == '-1' }
                },
                'PromotingOrganizationModel.OrganizationID': {
                    required: true,
                },
                'PromotingOrganizationModel.OrganizationSectorID': {
                    required: true,
                },
                'PromotingOrganizationModel.SocietyName': {
                    required: true,
                },
                'PromotingOrganizationModel.NoBatchesSantioned': {
                    required: true,
                },
                'PromotingOrganizationModel.DateofEstablishment': {
                    required: true,
                },
                'AuthorizedRepresentativeModel.Designation': {
                    required: true,
                },
                'AuthorizedRepresentativeModel.FirstName': {
                    required: true,
                    lettersonly: true,
                    minlength: 2
                },
                //'AuthorizedRepresentativeModel.MiddleName': {
                //    required: true,
                //    lettersonly: true,
                //    minlength: 2
                //},
                'AuthorizedRepresentativeModel.LastName': {
                    required: true,
                    lettersonly: true,
                    minlength: 1
                },
                'AuthorizedRepresentativeModel.DateofBirth': {
                    required: true,
                },
                'AuthorizedRepresentativeModel.AadharNumber': {
                    required: true,
                },

                'AuthorizedRepresentativeModel.MobileNumber': {
                    required: true,
                    minlength: 10,
                    maxlength: 10,
                },
                'AuthorizedRepresentativeModel.AlternativeMobileNumber': {
                    minlength: 10,
                    maxlength: 10,
                },
                'AuthorizedRepresentativeModel.EmailID':
                {
                    required: true,
                    regex: /^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i
                },
                'UserModel.Password': {
                    required: true,
                    regex: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}/,
                    minlength: 8
                },
                ConfirmPassword:
                {
                    required: true,
                    equalTo: "#Password"
                }
            },

            messages: {
                'RegistrationModel.ApplicationTypeID': {
                    required: "Application for is required.",
                },
                'PromotingOrganizationModel.OrganizationType': {
                    required: "Organization Type is required.",
                },
                'PromotingOrganizationModel.OrganizationSectorID': {
                    required: "Type of Promoting Organization is required."
                },
                'PromotingOrganizationModel.SocietyName': {
                    required: "Name is required.",
                },
                'PromotingOrganizationModel.NoBatchesSantioned': {
                    required: "No. of Batches Santioned is required.",
                },
                'PromotingOrganizationModel.DateofEstablishment': {
                    required: "Date of Establishment is required."
                },
                'AuthorizedRepresentativeModel.Designation': {
                    required: "Designation is required."
                },
                'AuthorizedRepresentativeModel.FirstName': {
                    required: "First Name is required.",
                    lettersonly: "Please enter characters only",
                    minlength: "You need to use at least 2 characters for your first name."
                },
                //'AuthorizedRepresentativeModel.MiddleName': {
                //    required: "Middle Name is required.",
                //    lettersonly: "Please enter characters only",
                //    minlength: "You need to use at least 2 characters for your middle name."
                //},
                'AuthorizedRepresentativeModel.LastName': {
                    required: "Last Name is required.",
                    lettersonly: "Please enter characters only",
                    minlength: "You need to use at least 2 characters for your last name."
                },
                'AuthorizedRepresentativeModel.DateofBirth': {
                    required: "Date of Birth is required."
                },
                'AuthorizedRepresentativeModel.AadharNumber': {
                    required: "Aadhaar Number is required."
                },
                'AuthorizedRepresentativeModel.MobileNumber': {
                    required: "Mobile No. is required.",
                    minlength: "Enter 10 digit Mobile Number."
                },
                'AuthorizedRepresentativeModel.AlternativeMobileNumber': {
                    minlength: "Enter 10 digit Mobile Number."
                },
                'AuthorizedRepresentativeModel.EmailID':
                {
                    required: "Email id is required.",
                    regex: "Please type your Email address in the format yourname@example.com "
                },
                'UserModel.Password': {
                    required: "Password is required.",
                    regex: "Password should have minimum 1 Capital Alphabet, 1 Number,1 Special Character and without space, e.g. Password@123"
                },
                ConfirmPassword:
                {
                    required: "Confirm Password is required.",
                    equalTo: "Password and Confirm password should be same"

                }
            },

            highlight: function (element) { // hightlight error inputs
                //$(element).closest('.form-group').addClass('has-error'); // set error class to the control group
                $(element).parents('.form-group').addClass('has-error has-feedback');
                //console.log("highlight " + $(element).prop("id"));
                $(element).closest('.form-group').find("[data-name='errorField']").show();
            },

            //unhighlight: function (element) {
            //    //  $(element).parents('.form-group').removeClass('has-error');
            //},

            //success: function (label) {
            //    $(label).closest('.form-group').removeClass('has-error');
            //    $(label).closest('.form-group').find("[data-name='errorField']").hide();
            //},

            //errorPlacement: function (error, element) {
            //    if (error.html().length > 0) {
            //        ($(element).closest('.form-group').find("[data-name='errorField']")).html(error.html());
            //        $(element).closest('.form-group').find("[data-name='errorField']").show();
            //    } else {
            //        $(element).closest('.form-group').find("[data-name='errorField']").hide();
            //    }
            //},

            submitHandler: function (form) {
                //$.ajax({
                //    type: 'Post',
                //    url: window.GetUserNameURL,
                //    data: {},
                //    success: function (data) {
                //        $("#lblUserName").text(data.UniqueKey);
                //        $("#hdnUserName").val(data.UniqueKey);
                //        $("#registor-confirm-pwd").modal('show');
                //    },
                //    error: function (xhr, status, error) {
                //    }
                //});
                //$(form).find("button[type=submit]").prop("disabled", true);
                //console.log($(form).find("button[type=submit]").prop("id"));
                //form.submit(); 
            }
        });

        return validator;
    }

    var validateForm = function () {
        $("#spnorganizationType").hide();
        var isValid = true;
        var org = $("#InstituteType").val();
        if ($("#InstituteType").val() != -1) {
            $("#formCreateAccount select.requiredValidation").each(function () {
                if ($(this).val() == '' || $(this).val() == '-1') {
                    $(this).closest('.form-group').find("label.validationError").show();
                    if (isValid) {
                        $(this).focus();
                    }
                    isValid = false;
                }
                else {
                    $(this).closest('.form-group').find("label.validationError").hide();
                }

                $(this).change(function () {
                    if ($(this).val() != '' && $(this).val() != "-1") {
                        $(this).closest('.form-group').find("label.validationError").hide();
                    }
                });
            });

            if (($("#passportFileValid").val() != "" && $("#passportFileValid").val() != null)) {
                isValid = true;
            }
            else {
                $("#spnAuthPersonPhoto").show();
                isValid = false;
            }
            return isValid;
        }
        else {
            //alertNotification("Please Select Type of Promoting Organization.")
            $("#spnorganizationType").show();
            isValid = false;
            return isValid;
        }

    }

    function adharNumberIsValid() {
        $('#IsvalidAadhar').hide();
        var aadhaarNumber = $('#txtAadhaarNumber').val();
        $('#IsAadharNumberValid').val('');
        //$("[data-input='InstructorAadharNumber']").closest('.form-group').removeClass('has-error');
        if (aadhaarNumber != '') {
            if (parseInt(aadhaarNumber.length) === 12) {
                $.ajax({
                    type: 'POST',
                    url: window.AadhaarNumberValidCheckUrl,
                    async: false,
                    cache: false,
                    data: { AadhaarNumber: aadhaarNumber },
                    //beforeSend: function (xhr) {
                    //    bootbox.dialog({
                    //        title: 'Please wait...', closeButton: false, backdrop: 'static', keyboard: false,
                    //        message: '<div class="text-center"><i class="fa fa-spin fa-spinner"></i> Loading...</div>'
                    //    })
                    //},
                    //complete: function () {
                    //    bootbox.hideAll();
                    //},
                    success: function (html) {
                        if (html.IsAadharNumberValid == true) {
                            $('#IsAadharNumberValid').val(html.IsAadharNumberValid);
                            //$("#IAadharKey").val('');
                            //$("[data-input='InstructorAadharNumber']").closest('.form-group').removeClass('has-error')
                            $('#IsvalidAadhar').hide();
                        }
                        else {
                            $('#IsAadharNumberValid').val(html.IsAadharNumberValid);
                            //$("[data-input='InstructorAadharNumber']").closest('.form-group').addClass('has-error')
                            $('#IsvalidAadhar').show();
                        }
                    },
                    error: function (e, f, d) {
                        console.log(e);
                    }
                });
            }
            else {
                $('#IsvalidAadhar').show();
                $('#IsAadharNumberValid').val('false');
                //bootbox.alert({ size: "small", title: "Warning", message: "Enter valid Aadhar number." });
                // $("[data-input='InstructorAadharNumber']").closest('.form-group').addClass('has-error');
                return false;

            }
        }
    }


    var aadharNumberCheck = function () {
        $('#txtAadhaarNumber').keypress(function (e) {
            var ret = false;
            var evt = (e) ? e : window.event;
            var charCode = (evt.keyCode) ? evt.keyCode : evt.which;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                ret = false;
            }
            else
                ret = true;
            if (ret) {
                // adharNumberIsValid();
            }
            else
                return false;
        }).blur(function (e) {
            var ret = false;
            var evt = (e) ? e : window.event;
            var charCode = (evt.keyCode) ? evt.keyCode : evt.which;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                ret = false;
            }
            else
                ret = true;
            if (ret)
                adharNumberIsValid();
            else
                return false;
        });
    }

    //**************************************************************************************************************
    //OTP GENERATION

    var generateOtp = function (chars) {
        $("#opterrormsg").text("");
        $("#otpNumber").text("");
        $('.otpvalid').attr("style", "display:none");
        if ($('#mobileNumber').val().length == 10 && $('#mobileNumber').val() != "") {
            $.ajax({
                type: "GET",
                url: window.GetMobileOtpURL,
                //url: window.GetMobileOTPWithAadhaarDataURL,
                contentType: "json",
                //data: {
                //    firstname: $("#AuthorizedRepresentativeModel_FirstName").val(), lastname: $("#AuthorizedRepresentativeModel_LastName").val(), isfathername: "n",
                //    fathername: "", aadhaarnumber: $("#txtAadhaarNumber").val(), mobilenumber: $('#mobileNumber').val(), dobday: "", dobmonth: "", dobyear: $("#AuthorizedRepresentativeModel_DateofBirth").datepicker('getDate').getFullYear(),
                //    gender: $(".ddgender").val() == 0 ? "male" : "female", isonlyyear: "y"
                //},
                data: { mobilenumber: $('#mobileNumber').val() },
                //var strFirstName = "T Venkatesh";
                //    var strLastName = "G"; var strIsFatherName = "n"; var strFatherName = "";
                //var strMobileNumber = 8147848620; var strDOBDay = ""; var strDOBMonth = "";
                //var strDOBYear = "1979"; var StrGender = "male"; var strIsOnlyYear = "y";
                success: function (data) {
                    JSON.stringify(data);
                    $("#optotpmsg").attr("style", "display:block;color:#20e10e;");
                    if (data == "Success") {
                        //bootbox.alert({ size: "small", title: "Warning", message: "OTP successfully sent to the mobile number" });

                    }
                    else {
                        //$("#optotpmsg").attr("style", "display:none");
                    }
                    //if (data == "Success") {
                    //    //bootbox.alert({ size: "small", title: "Warning", message: "OTP successfully sent to the mobile number" });
                    //    JSON.stringify(data);
                    //    if (chars == "Send")
                    //        $('#getotp').modal({ show: true, backdrop: 'static', keyboard: false })

                    //    $("#optotpmsg").attr("style", "display:block;color:#20e10e;");
                    //}
                    //else {
                    //    //$("#optotpmsg").attr("style", "display:none");
                    //    $("#getotp").modal("hide");
                    //    alertNotification(data);
                    //}
                }

            });
        }
        else {
            $("#getotp").modal("hide");
        }
    }

    var VerifyEnteredOtp = function () {
        $("#opterrormsg").text("");
        if ($("#otpNumber").val() != "") {
            $.ajax({
                type: "GET",
                url: window.GetOtpVerifyURL,
                contentType: "json",
                data: { enterOtp: $("#otpNumber").val() },
                success: function (res) {
                    if (res === true) {
                        $("#getotp").modal("hide");
                        $("#hdnOTPVerified").val(true);
                        $('#IdVerifyMbNo').hide();
                        $('#mobileNumber').attr("readonly", "readonly");
                        $('#hrfMobileVerify').attr("style", "display:none");
                        $('#hrfVerified').show();

                        $.ajax({
                            type: "GET",
                            url: window.GetOtpClearURL,
                            contentType: "json",
                            data: {},
                            success: function (data) {
                                if (res === true) {
                                }
                            }
                        });

                    } else {
                        $('.otpvalid').attr("style", "display:block");
                        $('#optotpmsg').attr("style", "display:none");
                        // $("#statusOTPMsg").removeClass("show-span").addClass("hide-span");
                        // $("#hdnOTPMsg").val(0);
                        $("#hdnOTPVerified").val(false);
                        //$("#MobileNumber").attr("readonly", false);
                    }
                }
            });
        }
    }

    // END OF OTP


    //MAIL OTP
    //**************************************************************************************************************
    //OTP GENERATION

    var generateMailOtp = function () {
        $("#mailopterrormsg").text("");
        $("#otpMailNumber").text("");
        var emailid = $('#emailId').val();
        $('.mailOtpvalid').attr("style", "display:none");
        if ($('#emailId').val() != "") {
            $.ajax({
                type: "GET",
                url: window.GetMailOtpURL,
                contentType: "json",
                data: { emailId: emailid },
                success: function (data) {
                    JSON.stringify(data);
                    $("#mailoptotpmsg").attr("style", "display:block;color:#20e10e;");
                    if (data == "Success") {
                        //bootbox.alert({ size: "small", title: "Warning", message: "OTP successfully sent to the mobile number" });

                    }
                    else {
                        //$("#optotpmsg").attr("style", "display:none");
                    }
                }

            });
        }
        else {
            $("#getMailotp").modal("hide");
        }
    }

    var BindProgramDropDown = function (e) {
        var facultyId = $(e).val();
        $("#ddlprogramId").html("");
        $.ajax({
            url: window.GetProgramListByFacultyId,
            type: "GET",
            data: { facultyId: facultyId },
            success: function (result) {
                var options = '';
                $(result).each(function (index, obj) {
                    options = options + '<option value="' + obj.DataValueField + '">' + obj.DataTextField + '</option>';
                });
                $("#ddlprogramId").html(options);
            },
            error: function (e) {
                var options = '<option value="-1">Select</option>';
                $("#ddlprogramId").html(options);
                alertNotification(e.statusText);
            }
        });
    }

    var BindCourseDropDown = function (e) {
        var facultyId = $(e).val();
        $.ajax({
            url: window.GetCourseListByFacultyId,
            type: "GET",
            data: { facultyId: facultyId },
            success: function (result) {
                var options = '';
                $(result).each(function (index, obj) {
                    options = options + '<option value="' + obj.DataValueField + '">' + obj.DataTextField + '</option>';
                });
                $("#ddlCourse").html(options);
            },
            error: function (e) {
                var options = '<option value="-1">Select</option>';
                $("#ddlCourse").html(options);
                alertNotification(e.statusText);
            }
        });

        var options = '<option value="-1">Select</option>';
        $("#ddlDegree").html(options);
        //$("#ddlSubject").html(options);
    }

    var BindDegreeDropDown = function (e) {
        var courseId = $(e).val();
        $.ajax({
            url: window.GetDegreeListByCourseId,
            type: "GET",
            data: { courseId: courseId },
            success: function (result) {
                var options = '';
                $(result).each(function (index, obj) {
                    options = options + '<option value="' + obj.DataValueField + '">' + obj.DataTextField + '</option>';
                });
                $("#ddlDegree").html(options);
            },
            error: function (e) {
                var options = '<option value="-1">Select</option>';
                $("#ddlDegree").html(options);
                alertNotification(e.statusText);
            }
        });

        var options = '<option value="-1">Select</option>';
        $("#ddlSubject").html(options);
    }

    var VerifyEnteredEmailOtp = function () {
        $("#mailopterrormsg").text("");
        if ($("#otpMailNumber").val() != "") {
            $.ajax({
                type: "GET",
                url: window.EnterMailOtpCheckURL,
                contentType: "json",
                data: { enterOtp: $("#otpMailNumber").val() },
                success: function (res) {
                    if (res === true) {
                        $("#getMailotp").modal("hide");
                        $("#hdnEmailOTPVerified").val(true);

                        $('#emailId').attr("readonly", "readonly");
                        $('#hrfMailVerify').attr("style", "display:none");
                        $('#hrfMailVerified').show();
                        $.ajax({
                            type: "GET",
                            url: window.MailOtpClearURL,
                            contentType: "json",
                            data: {},
                            success: function (data) {
                                if (res === true) {
                                }
                            }
                        });

                    } else {
                        $('.mailOtpvalid').attr("style", "display:block");
                        $('#mailoptotpmsg').attr("style", "display:none");
                        // $("#statusOTPMsg").removeClass("show-span").addClass("hide-span");
                        // $("#hdnOTPMsg").val(0);
                        $("#hdnEmailOTPVerified").val(false);
                        //$("#MobileNumber").attr("readonly", false);
                    }
                }
            });
        }
    }
    //check eduaction proof file //////
    var isBifocalValid = function () {
        var applicationForText = $("#applicationType option:selected").text();
        if (applicationForText == "Bi Focal" || applicationForText == "MSBV") {
            if (($("#resolutionAuthorizedPersonFile").val() != "" && $("#resolutionAuthorizedPersonFile").val() != null)) {
                return true;
            }
            else {
                alertNotification("Upload all the files in the form.");
                return false;
            }
        }
        else {
            return true;
        }
    }
    return {
        Load: function () {
            var validator = validateRegistrationForm();
            aadharNumberCheck();
            $(document).on("click", "#btnCreateAccount", function () {
                var isValid = validateForm();
                var isAdharNumberIsValid = $('#IsAadharNumberValid').val().toLowerCase();
                var isBifoaclValid = isBifocalValid();
                var isFormValid = $("#formCreateAccount").valid();
                if (!isFormValid && isValid) {
                    validator.focusInvalid();
                }
                isValid = true;
                if (isFormValid && isValid && isBifoaclValid && isAdharNumberIsValid != 'false') {
                    $("#hdnOTPVerified").val("true");
                    if ($("#hdnOTPVerified").val() == "true") {
                        //if ($("#hdnEmailOTPVerified").val() == "true") { //Mail verifiaction not mandatory for now
                        $('.overlap_container').show();
                        document.getElementById("formCreateAccount").submit();
                        //}
                        //else {
                        //    alertNotification('Please verify your Email Id.');
                        //}
                    }
                    else
                        alertNotification('Please verify your Mobile Number.');
                    //}
                }
            });

            //
            var applicant_type = $('.type_applicant').val();
            var applicationFor = $('#applicationType').val();
            if (applicant_type == 1) {
                $('.type_applicant').val(applicant_type);
                $('#applicationType').val(applicationFor);
                $('._pvt-vti-mssds-container').removeClass("show-model").addClass("hide-model");
                //$('.pviti_new_applicant_section').show();
                $('.pviti_new_applicant_section').removeClass("hide-model").addClass("show-model");
            }
            else {
                $('._pvt-vti-mssds-container').removeClass("show-model").addClass("hide-model");
                $('.pviti_new_applicant_section').removeClass("show-model").addClass("hide-model");
            }

            $(document).on('click', '#hrfMobileVerify', function () {
                //if ($('#mobileNumber').val().trim() != "" || $('#mobileNumber').val().length == 10) {
                //    generateOtp("Send");
                //}
                //else
                //    alertNotification('Please valid Mobile Number.');
                if ($('#mobileNumber').val().trim() != "" || $('#mobileNumber').val().length == 10) {
                    $('#getotp').modal({ show: true, backdrop: 'static', keyboard: false })
                    generateOtp();
                }
                else
                    alertNotification('Please enter valid Mobile Number.');
            });

            $(document).on("click", "#btnResetForOTP", function () {
                generateOtp("Resend");
            });
            $(document).on("click", "#btnVerifyOTP", function () {
                VerifyEnteredOtp();
            });

            $(document).on('click', '#hrfMailVerify', function () {
                if (validateEmail($("#emailId").val())) {
                    //if ($('#emailId').val().trim() != "") {
                    $('#getMailotp').modal({ show: true, backdrop: 'static', keyboard: false })
                    generateMailOtp();
                }
                else {
                    alertNotification('Please enter valid E-Mail Id.');
                }
            });
            $(document).on("click", "#btnVerifyEmailOTP", function () {
                VerifyEnteredEmailOtp();
            });
            $(document).on("click", "#btnResetForEmailOTP", function () {
                generateMailOtp();
            });
            $(document).on("change", "#ddlFaculty", function () {
                //BindCourseDropDown(this);
                BindProgramDropDown(this);
            });

            $(document).on("change", "#ddlCourse", function () {
                BindDegreeDropDown(this);
            });
        }
    }
}();