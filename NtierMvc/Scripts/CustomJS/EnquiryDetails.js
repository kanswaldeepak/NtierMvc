//var EnquiryDetails = function () {

//    var SmallFuntion = function (e) {
//        var fn = $('#' + e.id).data('inneraction');
//        var EnquiryDetails = "EnquiryDetails";
//        window[EnquiryDetails][fn](e);
//    }
//    var validation = function (e) {
//        if (($('#' + e.id).val() === null) || ($('#' + e.id).val() === "-1") || ($('#' + e.id).val() === "") || ($('#' + e.id).val().length <= 0)) {
//            $(e).next("span").removeClass('HideValidMsg');
//            $(e).next("span").addClass('CustomRequiredCSS');
//            $(e).closest('.form-group').find('.ErrorMessages').find('.HideValidMsg').addClass('CustomRequiredCSS');
//            $(e).closest('.form-group').find('.ErrorMessages').find('.CustomRequiredCSS').removeClass('HideValidMsg');
//        } else {
//            $(e).next("span").removeClass('CustomRequiredCSS');
//            $(e).next("span").addClass('HideValidMsg');
//            $(e).closest('.form-group').find('.ErrorMessages').find('.CustomRequiredCSS').addClass('HideValidMsg');
//            $(e).closest('.form-group').find('.ErrorMessages').find('.HideValidMsg').removeClass('CustomRequiredCSS');
//        }
//    }
//    var BindPopup = function () {
//        //$(".btn-Add-EnquiryDetails").on("click", function (e) {
//        var _actionType = "ADD"
//        //var _EnquiryDetailsId = $(this).parents("tr:first").find("#EnquiryDetailsId").val();
//        //var _staffProfileName = $(this).parents("tr:first").find("#StaffFirstName").val();
//        $.ajax({
//            type: "POST",
//            data: { actionType: _actionType },
//            datatype: "JSON",
//            url: window.OtherDetailsPopup,
//            success: function (html) {
//                SetModalTitle("Add Enquiry Details")
//                SetModalBody(html);
//                HideLoadder();
//                ShowModal();
//            },
//            error: function (r) {
//                HideLoadder();
//                alert(window.ErrorMsg);
//            }
//        })
//        //});
//    }
//    var onButtonRelease = function (e) {
//        alert(e);
//    }

//    var SaveData = function () {

//        var IsValid = true;
//        //if ($('#ddlposttype').val() === "" || $('#ddlposttype').val() === null || $('#ddlposttype').val() === "-1") {
//        //    IsValid = false;
//        //    $('#ddlposttype').next("span").removeClass('HideValidMsg');
//        //    $('#ddlposttype').next("span").addClass('CustomRequiredCSS');
//        //} else {
//        //    $('#ddlposttype').next("span").removeClass('CustomRequiredCSS');
//        //    $('#ddlposttype').next("span").addClass('HideValidMsg');
//        //}
//        //if ($('#ddlrole').val() === "" || $('#ddlrole').val() === null || $('#ddlrole').val() === "-1") {
//        //    IsValid = false;
//        //    $('#ddlrole').next("span").removeClass('HideValidMsg');
//        //    $('#ddlrole').next("span").addClass('CustomRequiredCSS');
//        //} else {
//        //    $('#ddlrole').next("span").removeClass('CustomRequiredCSS');
//        //    $('#ddlrole').next("span").addClass('HideValidMsg');
//        //}
//        //if ($('#ddlcourse').val() === "" || $('#ddlcourse').val() === null || $('#ddlcourse').val() === "-1") {
//        //    IsValid = false;
//        //    $('#ddlcourse').next("span").removeClass('HideValidMsg');
//        //    $('#ddlcourse').next("span").addClass('CustomRequiredCSS');
//        //} else {
//        //    $('#ddlcourse').next("span").removeClass('CustomRequiredCSS');
//        //    $('#ddlcourse').next("span").addClass('HideValidMsg');
//        //}
//        //if ($('#ddldesignation').val() === "" || $('#ddldesignation').val() === null || $('#ddldesignation').val() === "-1") {
//        //    IsValid = false;
//        //    $('#ddldesignation').next("span").removeClass('HideValidMsg');
//        //    $('#ddldesignation').next("span").addClass('CustomRequiredCSS');
//        //} else {
//        //    $('#ddldesignation').next("span").removeClass('CustomRequiredCSS');
//        //    $('#ddldesignation').next("span").addClass('HideValidMsg');
//        //}
//        //if ($('#ddlAppointment').val() === "" || $('#ddlAppointment').val() === null || $('#ddlAppointment').val() === "-1") {
//        //    IsValid = false;
//        //    $('#ddlAppointment').next("span").removeClass('HideValidMsg');
//        //    $('#ddlAppointment').next("span").addClass('CustomRequiredCSS');
//        //} else {
//        //    $('#ddlAppointment').next("span").removeClass('CustomRequiredCSS');
//        //    $('#ddlAppointment').next("span").addClass('HideValidMsg');
//        //}
//        //if ($('#IsApprovedUniversity').val().length <= 0) {
//        //    IsValid = false;
//        //    $('#IsApprovedUniversity').next("span").removeClass('CustomRequiredCSS');
//        //    $('#IsApprovedUniversity').next("span").addClass('HideValidMsg');
//        //} else {
//        //    if ($('input[name=IsApprovedUniversity]:checked').val() == "False") {
//        //        $('#ApporvalLetterNo').val("0");
//        //        $('#ApporvalLetterDate').val("");
//        //        $('#ApporvalLetterNo').next("span").removeClass('CustomRequiredCSS');
//        //        $('#ApporvalLetterNo').next("span").addClass('HideValidMsg');
//        //        $('#ApporvalLetterDate').next("span").removeClass('CustomRequiredCSS');
//        //        $('#ApporvalLetterDate').next("span").addClass('HideValidMsg');
//        //    }
//        //    else if ($('#ApporvalLetterNo').val() <= 0 || $('#ApporvalLetterDate').val() == '') {
//        //        IsValid = false;

//        //        $('#ApporvalLetterNo').next("span").addClass('CustomRequiredCSS');
//        //        $('#ApporvalLetterNo').next("span").removeClass('HideValidMsg');
//        //        $('#ApporvalLetterDate').next("span").addClass('CustomRequiredCSS');
//        //        $('#ApporvalLetterDate').next("span").removeClass('HideValidMsg');
//        //    }

//        //}
//        //if ($('#NoSectionPosts').val() <= 0) {
//        //    IsValid = false;
//        //    $('#NoSectionPosts').next("span").addClass('CustomRequiredCSS');
//        //    $('#NoSectionPosts').next("span").removeClass('HideValidMsg');
//        //}

//        if (IsValid) {
//            $('#formSaveEnquiryDetail').submit();
//        } else {
//            alertNotification("Please Select all the Details.");
//            return false;
//        }
//    }
//    var LoadViewPopup = function (_EnquiryDetailsId) {
//        var _actionType = "VIEW"
//        //var ID = e.target.id;
//        $.ajax({
//            type: "POST",
//            data: { actionType: _actionType, enquiryId: _EnquiryDetailsId },
//            datatype: "JSON",
//            url: window.EnquiryDetailsPopup,
//            success: function (html) {
//                SetModalTitle("View Enquiry Details Detail")
//                SetModalBody(html);
//                HideLoadder();
//                $('#formsaveEnquiryDetailsdetail input[type=radio],input[type=text], select').prop("disabled", true);
//                $('#save_results').css('display', 'none');
//                $('#cancel_results').css('display', 'none');
//                $('.bs-tooltip-top').css('display', 'none');
//                ShowModal();
//            },
//            error: function () {
//                HideLoadder();
//                alert(window.ErrorMsg);
//            }
//        })
//    }
//    var LoadEditPopup = function (_EnquiryDetailsId) {
//        var _actionType = "EDIT"
//        //var ID = e.target.id;
//        $.ajax({
//            type: "POST",
//            data: { actionType: _actionType, EnquiryDetailsId: _EnquiryDetailsId },
//            datatype: "JSON",
//            url: window.OtherDetailsPopup,
//            success: function (html) {
//                SetModalTitle("Edit Enquiry Details Detail")
//                SetModalBody(html);
//                HideLoadder();
//                $('.bs-tooltip-top').css('display', 'none');
//                ShowModal();
//            },
//            error: function () {
//                HideLoadder();
//                alert(window.ErrorMsg);
//            }
//        })
//    }
//    return {
//        Callfuntion: function (e) {
//            SmallFuntion(e);
//        },
//        validationCall: function (e) {
//            validation(e);
//        },
//        OnRelease: function (e) {
//            onButtonRelease(Element);
//        },
//        FinalSave: function () {
//            SaveData();
//        },
//        Load: function () {

//            $(document).on("click", ".btn-Add-EnquiryDetails", function () {
//                var _EnquiryDetailsId = $(this).parents("tr:first").find("#EnqId").val();
//                BindPopup();
//            });
//            $(document).on("click", ".btn-Edit-EnquiryDetails", function () {
//                var _EnquiryDetailsId = $(this).parents("tr:first").find("#EnqId").val();
//                LoadEditPopup(_EnquiryDetailsId);
//            });
//            $(document).on("click", ".btn-view-EnquiryDetails", function () {
//                //var _EnquiryDetailsId = $(this).parents("tr:first").find("#EnqId").val();
//                var _EnquiryDetailsId = $(this).closest('tr').find('#EnqId').text();
//                LoadViewPopup(_EnquiryDetailsId);
//            });
//        }
//    }
//}();


function GetVendorDetailsForEnquiry() {
    var CustomerId = $("#EDCustomerId").val();
    $.ajax({
        type: 'POST',
        url: window.VendorDetailForEnquiry,
        data: JSON.stringify({ CustomerId: CustomerId }),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#City").val(data.City);
            $("#State").val(data.State);
            $("#Country").val(data.Country);
            $("#EnqType").val(data.EnqType);
            $("#StateId").val(data.StateId);
            $("#CountryId").val(data.CountryId);
            $("#EnqTypeId").val(data.EnqTypeId);
            $("#EDCustomerName").val(data.CustomerName);
        },
        error: function (x, e) {
            $('#alertmsg').text('Some error is occurred, Please try after some time.');
            $('#alertmsg').addClass("important red");
            $('#hostelModal').modal('show');
        }
    });

}

function GetQuoteNumbers() {
    var QuoteType = $("#QuoteEnquiryFormType").val();
    $.ajax({
        type: 'POST',
        url: window.GetDeliveryItems,
        data: JSON.stringify({ quotetypeId: QuoteType }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {
            //$('#QuoteFormNo').val(res.QuoteNo);

            $("#EDCustomerId").empty();
            if (res.lstVendors.length > 0) {
                $.each(res.lstVendors, function (i, item) {
                    $("#EDCustomerId").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }

        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').addClass("important red");
            //$('#Sucess-Failure').modal('show');
        }
    })
}

function GetSubProductGrps() {
    var MainProdGrp = $("#MainProdGrp").val();
    $.ajax({
        type: 'POST',
        url: window.GetSubProdGrp,
        data: JSON.stringify({ MainProdGrpId: MainProdGrp }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {

            $("#SubProdGrp").empty();
            if (res.length > 0) {
                $.each(res, function (i, item) {
                    $("#SubProdGrp").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }

        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').addClass("important red");
            //$('#Sucess-Failure').modal('show');
        }
    })
}

function GetProdName() {
    var MainProdGrp = $("#MainProdGrp").val();
    var SubProdGrp = $("#SubProdGrp").val();

    $.ajax({
        type: 'POST',
        url: window.GetProdNames,
        data: JSON.stringify({ MainProdGrpId: MainProdGrp, SubProdGrpId: SubProdGrp }),
        contentType: "application/json; charset=utf-8",
        success: function (res) {

            $("#ProdName").empty();
            if (res.length > 0) {
                $.each(res, function (i, item) {
                    $("#ProdName").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })
            }

        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').text('Some error is occurred, Please try after some time.');
            //$('#spn-Sucess-Failure').addClass("important red");
            //$('#Sucess-Failure').modal('show');
        }
    })
}

function AgentShowHide() {

    var EnqThruVal = $('#EnqThru option:selected').text();

    if (EnqThruVal == 'Agent')
        $('#divAgentName').show();
    else
        $('#divAgentName').hide();
}