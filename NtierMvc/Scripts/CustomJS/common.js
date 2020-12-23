
function DisplayNewItemInDdl(selectedId) {
    debugger;
    var item = $(selectedId).find("option:selected");
    var value = item.val(); //get the selected option value
    var text = item.html(); //get the selected option text
    if (text == "Others") {
        $('#SelectedDdlId').val(selectedId.id);
        $('#TblName').val(selectedId.id);
        if (!($('.modal.in').length)) {
            $('.modalAddNewItemContent').css({
                top: '35%',
                left: '30%'
            });
        }
        $('#AddNewItemContent').modal({
            backdrop: true,
            show: true
        });

        $('.modal-dialog').draggable({
            handle: ".modal-body"
        });
    }
};

function AddNewItemInDdl(txtNewOption, SelectedDdlId) {
    var newitem = $("#" + txtNewOption.id).val(); //get the new value
    //alert(newitem);
    var newOption = "<option value='" + newitem + "'>" + newitem + "</option>";
    $(newOption).insertBefore($("#" + SelectedDdlId.value + " option:last")); //add new option 
    $("#" + SelectedDdlId.value).val(newitem);
    $("#" + txtNewOption.id).val('');
    $("#AddNewItemContent").modal('hide');
    return false;
};


var regexExp = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}/
function AllowNumbers(evt) {
    var keyCode = (evt.which) ? evt.which : event.keyCode;
    if (keyCode < 48 || keyCode >= 58 && keyCode != 8) {
        return false;
    }
    return true;
}

function formatDate(dateObject, convertTo) {
    //var d = new Date(dateObject);
    var d = new Date(dateObject.split("/").reverse().join("-"));

    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    if (day < 10) {
        day = "0" + day;
    }
    if (month < 10) {
        month = "0" + month;
    }

    var date = '';

    switch (convertTo) {
        case 'dd/MM/yyyy':
            date = day + "/" + month + "/" + year;
            break;
        case 'dd-MM-yyyy':
            date = day + "-" + month + "-" + year;
            break;
        case 'yyyy-MM-dd':
            date = year + "-" + month + "-" + day;
            break;
        default:
            date = day + "/" + month + "/" + year;
            break;
    }


    return date;
};

function formatAMPM(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}


function validateDecimalNumbers(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (number.length == 2 && number[1].length == 2) {
        return false;
    }
    //((charCode >= 48 && charCode <= 57) || charCode == 190)
    if (charCode >= 48 && charCode <= 57) {
        return true;
    } else if (charCode == 46) {
        // Allow only 1 decimal point ('.')...  
        if ((el.value) && (el.value.indexOf('.') >= 0))
            return false;
        else
            return true;
    }
    return false;
}


function AllowNumbersLatLong(evt) {
    var keyCode = (evt.which) ? evt.which : event.keyCode;
    if (keyCode < 48 || keyCode >= 58 && keyCode != 8) {
        if (keyCode == 43 || keyCode == 45 || keyCode == 46)
            return true;
        else
            return false;
    }
    return true;
}

function validatePassword(password) {
    var pattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,}/;

    return $.trim(password).match(pattern) ? true : false;
}


function validateEmail(Email) {
    var pattern = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

    return $.trim(Email).match(pattern) ? true : false;
}

function PinCodeSearchByTaluka(pinCode, districtDrpCtrlId, subDistrictcode, hdnStateCode, ddlCityVillage) {
    $.ajax({
        type: 'GET',
        url: window.SearchPincodeTalukaURL,
        async: false,
        cache: false,
        data: { pinCode: pinCode, subDistrictCode: subDistrictcode },
        success: function (data) {
            if (data.length > 0 && data[0].length > 0) {
                $(districtDrpCtrlId).find('option')
                    .remove()
                    .end()
                    .append('<option value="' + data[0][0].districtId + '">' + data[0][0].districtName + '</option>')
                    .val(data[0][0].districtId);

                //BINDED CITY/VILLAGE TO DROPDOWN BY --NARENDRA--08-05-2018
                if (data.length > 0 && data[1].length > 0) {
                    var options = "";
                    var cvCode = $('#hdnCityVillageCode').val() == undefined ? 0 : $('#hdnCityVillageCode').val(); //ADDED GENERIC CODE PLEASE UTILISE THIS
                    for (var index = 0; index < data[1].length; index++) {
                        options += '<option value="' + data[1][index].cityID + '">' + data[1][index].cityName + '</option>';
                        $(ddlCityVillage).find('option')
                            .remove()
                            .end()
                            .append(options)
                            .val(cvCode == 0 ? data[1][index].cityID : cvCode);
                    }
                }
                //   $(ddlCityVillage);
                if ($(ddlCityVillage).val() != -1 || $(ddlCityVillage).val() != null || $(ddlCityVillage).val() != "")
                    $('#hdnCityVillageCode').val($(ddlCityVillage).val());

                $(ddlCityVillage).prop("selectedIndex", 0);

                //$(subDistrictDrpCtrlId).find('option')
                //    .remove()
                //    .end()
                //    .append('<option value="' + data[0][0].subDistrictId + '">' + data[0][0].subDistrictName + '</option>')
                //    .val(data[0][0].subDistrictId);


                $(hdnStateCode).val(data[0][0].stateId);
                $('#_stateName').val(data[0][0].stateName); //ADDING STATE NAME TO TEXT FIELD - BY SHESHENDRA 14-03-2018
                if ($('#_stdCode').val() == null && $('#_stdCode').val() == 0 && $('#_stdCode').val() == undefined && $('#_stdCode').val() == "")
                    $('#_stdCode').val(data[0][0].stdCode); //ADDING STATE CODE TO TEXT FIELD - BY SHESHENDRA 14-03-2018
                $('#_countryCode').text(data[0][0].countryCode); //ADDING COUNTRY CODE TO TEXT FIELD - BY SHESHENDRA 14-03-2018
            }
            else {
                $(districtDrpCtrlId).find('option')
                    .remove()
                    .end()
                    .append('<option value="">Select</option>')
                    .val("");
                $(subDistrictDrpCtrlId).find('option')
                    .remove()
                    .end()
                    .append('<option value="">Select</option>')
                    .val("");
                $('#_stateName').val(""); //EMPTY STATE NAME TO TEXT FIELD - BY SHESHENDRA 14-03-2018
                $('#_stdCode').val(""); //EMPTY STATE NAME TO TEXT FIELD - BY SHESHENDRA 14-03-2018
                $('#_countryCode').text(""); //ADDING COUNTRY CODE TO TEXT FIELD - BY SHESHENDRA 14-03-2018
                $('#txtPinCode').val("");
                alertNotification("Please enter valid  Pincode");
            }
        },
        error: function (e, f, d) {
            console.log(e);
        }
    });
}

function validatePassword(pswd) {

    var newPassword = pswd;
    var minNumberofChars = 8;
    var maxNumberofChars = 16;
    var regularExpression = /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{6,16}$/;
    if (newPassword.length < minNumberofChars || newPassword.length > maxNumberofChars) {
        return false;
    }
    else {
        if (!regularExpression.test(newPassword)) {
            return false;
        }
        else {
            return true;
        }
    }
}
function PinCodeSearch(pinCode, districtDrpCtrlId, subDistrictDrpCtrlId, hdnStateCode, ddlCityVillage) {
    $.ajax({
        type: 'GET',
        url: window.SearchPincodeURL,
        async: false,
        cache: false,
        data: { pinCode: pinCode },
        success: function (data) {
            if (data.length > 0 && data[0].length > 0) {
                $(districtDrpCtrlId).find('option')
                    .remove()
                    .end()
                    .append('<option value="' + data[0][0].districtId + '">' + data[0][0].districtName + '</option>')
                    .val(data[0][0].districtId);

                //BINDED CITY/VILLAGE TO DROPDOWN BY --NARENDRA--08-05-2018
                if (data.length > 0 && data[1].length > 0) {
                    var options = "";
                    var cvCode = $('#hdnCityVillageCode').val() == undefined ? 0 : $('#hdnCityVillageCode').val(); //ADDED GENERIC CODE PLEASE UTILISE THIS
                    for (var index = 0; index < data[1].length; index++) {
                        options += '<option value="' + data[1][index].cityID + '">' + data[1][index].cityName + '</option>';
                        $(ddlCityVillage).find('option')
                            .remove()
                            .end()
                            .append(options)
                            .val(cvCode == 0 ? data[1][index].cityID : cvCode);
                    }
                }
                $(ddlCityVillage).append(options);
                if ($(ddlCityVillage).val() != -1 || $(ddlCityVillage).val() != null || $(ddlCityVillage).val() != "")
                    $('#hdnCityVillageCode').val($(ddlCityVillage).val());

                var subdistoptions = "";
                for (var index = 0; index < data[0].length; index++) {
                    subdistoptions += '<option value="' + data[0][index].subDistrictId + '">' + data[0][index].subDistrictName + '</option>';
                    $(subDistrictDrpCtrlId).find('option')
                        .remove()
                        .end()
                        .append(subdistoptions)
                        .val(data[0][0].subDistrictId);
                }

                //$(subDistrictDrpCtrlId).find('option')
                //    .remove()
                //    .end()
                //    .append('<option value="' + data[0][0].subDistrictId + '">' + data[0][0].subDistrictName + '</option>')
                //    .val(data[0][0].subDistrictId);


                $(hdnStateCode).val(data[0][0].stateId);
                $('#_stateName').val(data[0][0].stateName); //ADDING STATE NAME TO TEXT FIELD - BY SHESHENDRA 14-03-2018
                if ($('#_stdCode').val() == null && $('#_stdCode').val() == 0 && $('#_stdCode').val() == undefined && $('#_stdCode').val() == "")
                    $('#_stdCode').val(data[0][0].stdCode); //ADDING STATE CODE TO TEXT FIELD - BY SHESHENDRA 14-03-2018
                $('#_countryCode').text(data[0][0].countryCode); //ADDING COUNTRY CODE TO TEXT FIELD - BY SHESHENDRA 14-03-2018
            }
            else {
                $(districtDrpCtrlId).find('option')
                    .remove()
                    .end()
                    .append('<option value="">Select</option>')
                    .val("");
                $(subDistrictDrpCtrlId).find('option')
                    .remove()
                    .end()
                    .append('<option value="">Select</option>')
                    .val("");
                $('#_stateName').val(""); //EMPTY STATE NAME TO TEXT FIELD - BY SHESHENDRA 14-03-2018
                $('#_stdCode').val(""); //EMPTY STATE NAME TO TEXT FIELD - BY SHESHENDRA 14-03-2018
                $('#_countryCode').text(""); //ADDING COUNTRY CODE TO TEXT FIELD - BY SHESHENDRA 14-03-2018
                $('#txtPinCode').val("");
                alert("Please enter valid  Pincode");
            }
        },
        error: function (e, f, d) {
            console.log(e);
        }
    });
}

function AdharNumberCheck(aadhaarNumber, errorDisCtrlId, $hdnCtrl) {
    if (errorDisCtrlId != undefined || errorDisCtrlId != '') {
        $(errorDisCtrlId).hide();
    }
    $hdnCtrl.val('');
    if (aadhaarNumber != '') {
        if (parseInt(aadhaarNumber.length) === 12) {
            $.ajax({
                type: 'POST',
                url: window.AadhaarNumberValidCheckUrl,
                async: false,
                cache: false,
                data: { AadhaarNumber: aadhaarNumber },
                success: function (html) {
                    if (html.IsAadharNumberValid == true) {
                        $hdnCtrl.val(html.IsAadharNumberValid);
                        $(errorDisCtrlId).hide();
                    }
                    else {
                        $hdnCtrl.val(html.IsAadharNumberValid);
                        $(errorDisCtrlId).show();
                    }
                },
                error: function (e, f, d) {
                    console.log(e);
                }
            });
        }
        else {
            $(errorDisCtrlId).show();
            $hdnCtrl.val('false');
            return false;

        }
    }
}

function CheckEmail(c) {

    if (c != null && c != undefined) {
        if (!validateEmail($(c).val())) {
            alertNotification('Invalid Email ID');
            $(c).val('');
        }
    }
}

function validateFloatKeyPress(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot (thanks ddlab)
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}


function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}

function onlyAlphabets(evt) {
    var keyCode = (evt.which) ? evt.which : event.keyCode;
    if ((keyCode > 64 && keyCode < 91) || (keyCode > 96 && keyCode < 123) || keyCode == 127 || keyCode == 32 || keyCode == 8)
        return true;
    return false;
}

//GENERIC ALERT NOTIFICATION FUNCTION
function alertNotification(msg) {
    $("#spn-Sucess-Failure").text(msg)
    $("#Sucess-Failure").modal('show');
}

//Added By Akshay
function AlertMsg(msg) {
    //$("#ModalConatiner").prop("style","min-width:300px;")
    $("#ModalBody").html(msg)
    $("#ModalHeading").html('Alert');
    $("#ModalPopup").modal('show');
}

function ShowModal(ModalType) {
    $("#ModalPopup").modal('show');
}

function HideModal() {
    $("#ModalBody").html('');
    $("#ModalPopup").modal('hide');
}

function SetModalWidth(width) {
    $("#ModalConatiner").prop("style", "max-width:" + width);
}

function SetModalTitle(Title) {
    $("#ModalHeading").text(Title);
}

function SetModalBody(Data) {
    $("#ModalBody").html(Data);
}

function SetModalBody(Data, ResetValidation) {
    $("#ModalBody").html(Data);
    if (ResetValidation) {
        ResetUnobtrusiveValidation();
    }
}

//For Quote Panel Model

function ShowPanelModal() {
    $("#Panel").modal('show');
}

function HidePanelModal() {
    $("#PanelBody").html('');
    $("#Panel").modal('hide');
}

function SetModalPanelWidth(width) {
    $("#PanelConatiner").prop("style", "max-width:" + width);
}

function SetModalPanelTitle(Title) {
    $("#PanelHeading").text(Title);
}

function SetModalPanelBody(Data) {
    $("#PanelBody").html(Data);
}

function SetModalPanelBody(Data, ResetValidation) {
    $("#PanelBody").html(Data);
    if (ResetValidation) {
        ResetUnobtrusiveValidation();
    }
}
//Panel Model End

//Parametrized Panel
function SetParamModalPanelWidth(ParamPanelId, width) {
    $("#" + ParamPanelId).prop("style", "max-width:" + width);
}

function SetParamModalPanelTitle(ParamPanelHeadingId, Title) {
    $("#" + ParamPanelHeadingId).text(Title);
}

function SetParamModalPanelBody(ParamPanelBodyId, Data) {
    $("#" + ParamPanelBodyId).html(Data);
}

function SetParamModalPanelBody(ParamPanelBodyId, Data, ResetValidation) {
    $("#" + ParamPanelBodyId).html(Data);
    if (ResetValidation) {
        ResetUnobtrusiveValidation();
    }
}
//Parametrized Panel

function ShowLoadder() {
    $(".overlap-container").show();
}

function HideLoadder() {
    $(".overlap-container").hide();
}

// endDate is added for getting only past date.
function SetDateTimePicker() {
    $('.datetime-picker').datepicker({
        format: 'dd-mm-yyyy',
        changeMonth: true,
        autoclose: true,
        changeYear: true,
        showMonthAfterYear: true,
        endDate: 'today'
    });
}

function SetDateTimePicker1() {
    $('.datetime-picker').datepicker({
        format: 'dd-mm-yyyy',
        changeMonth: true,
        autoclose: true,
        changeYear: true,
        showMonthAfterYear: true,
        endDate: new Date(2090, 10, 30)
    });
}

function ResetUnobtrusiveValidation() {
    $("form").removeData("validator");
    $("form").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse("form");

}

//--------------------------------------------------

function ValidateAadharNo(AadharNo) {
    try {
        if (AadharNo != '') {
            if (AadharNo.length == 12) {
                ShowLoadder();
                $.ajax({
                    type: 'POST',
                    url: window.AadhaarNumberValidCheckUrl,
                    async: false,
                    cache: false,
                    data: { AadhaarNumber: AadharNo },
                    success: function (html) {
                        HideLoadder();
                        return html.IsAadharNumberValid;
                    },
                    error: function () {
                        HideLoadder();
                        return false;
                    }
                });
            }
            else {
                return false;
            }
        }
    } catch (e) {

        return false;
    }

}


//End


//GENERIC UPLOAD FILE VALIDATION ACCORDING TO EXTENSION
function validateUploadFile(file) {
    var ext = file.value.split(".");
    ext = ext[ext.length - 1].toLowerCase();
    var arrayExtensions = ["jpg", "jpeg", "png", "xls", "xlsx", "txt"];
    //var arrayExtensions = ["jpg", "jpeg"];

    if (arrayExtensions.lastIndexOf(ext) == -1) {
        alertNotification("Please upload valid file");
        $(file).val("");
    }
}

//GENERIC UPLOAD FILE VALIDATION ACCORDING TO EXTENSION - Only Jpg
function validateOnlyJpg(file) {

    var ext = file.value.split(".");
    ext = ext[ext.length - 1].toLowerCase();
    var arrayExtensions = ["jpg", "jpeg", "png", "bmp", "tif"];
    //var arrayExtensions = ["jpg", "jpeg"];
    if ($(file).val() != '') {
        if (arrayExtensions.lastIndexOf(ext) == -1) {
            alertNotification("Invalid file format. Supporting formats are JPG, JPEG, BMP, PNG, TIF.");
            $(file).val("");
        }
    }
    else {

    }
}


//GENERIC FILE SIZE
function validateFileSize(f) {
    var maxSizeMB = 2; //MB
    var fSizeMB = f.files[0].size / 1024 / 1024;

    if (fSizeMB > maxSizeMB) {
        alertNotification(" File Size should not exceeds 2Mb.");
        f[0].value = '';
    }
}

//FILE TYPE RESTRICTION AND SIZE 
function validateFileSizeFormat(f) {

    var maxSizeMB = 2; //MB
    var extension = $(f).val().split('.').pop().toLowerCase();
    if (extension == 'png' || extension == 'jpg' || extension == 'pdf') {
        var fSizeMB = f.files[0].size / 1024 / 1024;
        if (fSizeMB > maxSizeMB) {
            alertNotification(" File Size should not exceeds 2MB.");
            f[0].value = '';
        }
    }
    else if ($(f).val() != '') {
        alertNotification("Please upload file type of only pdf or jpg or jpeg");
        $(f).val('');
    }
    else {

    }
}
//FILE TYPE RESTRICTION AND SIZE 
function validateFileFormatpdf(f) {
    var maxSizeMB = 2; //MB
    var extension = $(f).val().split('.').pop().toLowerCase();
    if (extension == 'pdf') {
        var fSizeMB = f.files[0].size / 1024 / 1024;
        if (fSizeMB > maxSizeMB) {
            alertNotification("File Size should not exceeds 2MB.");
            $(f).val('');
        }
    }
    else if ($(f).val() != '') {
        alertNotification("Please upload file type of only pdf");
        $(f).val('');
    }
    else {
    }
    $(f).closest('div').removeClass('errorClass');
}

//CHECK VALIDATION
function mandatoryFile(f) {
    var fsize = f.files.length;

    if (fsize == 0) {
        $('.sp-required').show();
        return false;
    }
    else {
        $('.sp-required').hide();
        return true;
    }
}

//  CANCEL CONFIRMATION
$(document).on("click", ".cancelcnfrm", function () {
    //var _result = confirm("Are you sure? You might lose data in the form if not saved.");
    _result = true;
    return _result;
});


function onlyAlphaNumeric(evt) {
    var keyCode = (evt.which) ? evt.which : event.keyCode;
    if ((keyCode > 64 && keyCode < 91) || (keyCode > 96 && keyCode < 123) || (keyCode > 47 && keyCode < 58) || keyCode == 127 || keyCode == 32 || keyCode == 8)
        return true;
    return false;
}

function onlyAlphaNumSpecial(evt) {
    var keyCode = (evt.which) ? evt.which : event.keyCode;
    if ((keyCode > 64 && keyCode < 91) || (keyCode > 96 && keyCode < 123) || (keyCode > 47 && keyCode < 58) || keyCode == 127 || keyCode == 32 || keyCode == 8 || (keyCode > 37 && keyCode < 42) || keyCode == 46 || keyCode == 44)
        return true;
    return false;
}
function onlyAlphaSpecial(evt) {
    var keyCode = (evt.which) ? evt.which : event.keyCode;
    if ((keyCode > 64 && keyCode < 91) || (keyCode > 96 && keyCode < 123) || keyCode == 127 || keyCode == 32 || keyCode == 8 || (keyCode > 37 && keyCode < 42) || keyCode == 46 || keyCode == 44)
        return true;
    return false;
}

function validateMobileNumberForZero(e) {
    var mobileNumber = $(e).val();
    var one1 = String(mobileNumber).charAt(0);
    var one_as_number1 = Number(one1);
    if (one1 != "") {
        if (one_as_number1 < 6) {
            alertNotification('Enter Valid Mobile Number');
            //alert("Mobile Number should not start with " + one_as_number1);
            $(e).val('');
            $(e).focus();
        }
    }
}

//CONVERTING TO TITLECASE
function titleCase(str) {
    str = str.toLowerCase().split(' ');
    for (var i = 0; i < str.length; i++) {
        str[i] = str[i].charAt(0).toUpperCase() + str[i].slice(1);
    }
    return str.join(' ');
}

function allowalphanumericOnly(e) {
    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (regex.test(str)) {
        return true;
    }
    e.preventDefault();
    return false;
}
function AllowOnlyPositive(str) {
    var keyCode = (str.which) ? str.which : event.keyCode;
    if (keyCode == 45) {
        return true;
    }
    else {
        alertNotification("negative values are not allowed.");
        $(this).val('');
        return false;
    }
    //var charat = str.val().charAt(0);
    //if (str.val().charAt(0) != '-') {
    //    return true;
    //}
    //else {
    //    alertNotification("negative values are not allowed.");
    //    $(this).val('');
    //    return false;
    //}
}


function GetTradesByAppTypeSectorTradeType() {
    var _applicationTypeId = $("#applicationType").val();
    var _tradeTypeId = $("#TradeType").val();
    var _tradeSectorId = $("#Sector").val();
    var url = "/ManageNorms/GetTradesByAppTypeSectorTradeType/";
    var dropDownItems = [];

    $.ajax({
        url: url,
        data: {
            applicationTypeId: _applicationTypeId,
            tradeTypeId: _tradeTypeId,
            tradeSectorId: _tradeSectorId
        },
        cache: false,
        type: "POST",
        success: function (data) {
            dropDownItems = data;
            var options = '';
            if (dropDownItems != null && dropDownItems != '') {
                $(dropDownItems).each(function (index, listItem) {
                    options += '<option value="' + listItem.DataValueField + '">' + listItem.DataTextField + '</option>'
                })
            }

            $("#Trade").html(options).show();
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });
}

function closeCommitteInspectionRemark() {
    $("#inspection-page-modal").modal('hide');
}

function ShowCommitteInspectionRemark(RegID, SectionId) {
    ShowLoadder();
    params = {
        RegistrationId: RegID,
        SectionId: SectionId
    };
    $.ajax({
        type: "GET",
        url: $("#getCommitteInspectionRemarkUrl").text().trim(),
        cache: false,
        data: params,
        success: function (data) {
            //alert(data);
            SetModalTitle("Committe Inspection Remarks");
            SetModalBody(data);
            ShowModal();
            // check for if table is empty start
            //if ($("#tblinspectionremark > tbody > tr").length == 0) {
            //    $("#tblinspectionremark > tbody").append('<tr><td colspan="5" style="text-align: center;border: 1px solid #d4cbcb;">There are no records</td></tr>')
            //}
            // check for if table is empty end
            HideLoadder();
        },
        error: function (data) {
            HideLoadder();
        }
    });
    return false;
}

function DisplayInstituteInspectionForm(RegID, UserID, SectionId) {
    ShowLoadder();
    params = {
        RegistrationId: RegID,
        UserId: UserID, SectionId: SectionId,
        IP: $(".GetCommonIP").val()
    };
    $.ajax({
        type: "GET",
        url: $("#getInstituteInspectionFormUrl").text().trim(),
        cache: false,
        contentType: "html",
        data: params,
        success: function (data) {
            //alert(data);
            SetModalTitle("Institute Inspection");
            SetModalBody(data, true);
            ShowModal();
            HideLoadder();
        },
        error: function (data) {
            HideLoadder();
        }
    });
}


// Common Method for Adding Success Failure Message
var ObjGetSuccessFailureMessageRegi = { Success: "Data is successfully submitted.", Error: "Data could not be submitted." };
var ObjGetSuccessFailureMessageInsp = { Success: "Inspection remarks is successfully submitted.", Error: "Inspection remarks could not be submitted." };

function DisplaySuccessorFailMessage(ServerResponse, MessageDisplay) {
    if (ServerResponse == "Success" || ServerResponse == "Error") {
        $("#spn-Sucess-Failure").text(MessageDisplay[ServerResponse]);
        $("#Sucess-Failure").modal('show');
    }
}
// Common Method for Adding Success Failure Message

function ValidateInspectionRequiredFields() {
    var Status = false;
    $(".requiredInspectValidation").each(function () {
        //alert($(this).val());
        if ($(this).val() == "" || $(this).val() == "-1") {
            $(this).addClass("fieldreq");
            $(this).next().addClass("CustomRequiredCSS");
            $(this).next().removeClass("HideValidMsg");
            Status = true;
        } else {
            $(this).next().removeClass("CustomRequiredCSS");
            $(this).next().addClass("HideValidMsg");
            $(this).removeClass("fieldreq");
        }
    })

    if (!Status) {
        if (confirm("Are your sure. Do you want to submit?")) {
            return true;
        } else {
            return false;
        }
    } else {
        return false;
    }
}

function GetFormValidationStatus(formName) {
    var Status = true;
    $(formName).find(".requiredValidation").each(function () {
        //alert($(this).val());
        if ($(this).val() == "" || $(this).val() == "-1" || $(this).val() == "0") {
            $(this).addClass("fieldreq");
            $(this).next().addClass("CustomRequiredCSS");
            $(this).next().removeClass("HideValidMsg");
            Status = false;
        } else {
            $(this).next().removeClass("CustomRequiredCSS");
            $(this).next().addClass("HideValidMsg");
            $(this).removeClass("fieldreq");
        }
    })
    return Status;
}

function setReadOnly(formName) {
    var Status = true;
    $(formName).find(".applyDisabled").each(function () {
        $(this).attr("disabled", true);
    })
    return Status;
}

function ClearAllFields(formName) {
    $(formName).find(".clearField").each(function () {
        //alert($(this).val());
        if ($(this).val() != "") {
            $(this).val("");
        }
    })
}

// Common Required Fields Validation
function ValidateRequiredFields() {
    var isInvalid = false;

    // Return Validation Status Start
    var isInvalid = GetFormValidationStatus();
    // Return Validation Status End

    if (!isInvalid) {
        if (confirm("Are your sure. Do you want to submit?")) {
            return true;
        } else {
            return false;
        }
    } else {
        return false;
    }
}
// below function is modified and re-written at the end.
function ValidateRequiredFieldsOnFocusOut(element) {
    if ($(element).val() == "" || $(element).val() == "-1") {
        $(element).addClass("fieldreq");
        $(element).next().addClass("CustomRequiredCSS");
        $(element).next().removeClass("HideValidMsg");
        // Added by vignesh - start
        $(element).closest('.form-group').find('.ErrorMessages').find('.HideValidMsg').addClass('CustomRequiredCSS');
        $(element).closest('.form-group').find('.ErrorMessages').find('.CustomRequiredCSS').removeClass('HideValidMsg')
        // End
        isInvalid = true;
    } else {
        $(element).next().removeClass("CustomRequiredCSS");
        $(element).next().addClass("HideValidMsg");
        // Added by vignesh - start
        $(element).closest('.form-group').find('.ErrorMessages').find('.CustomRequiredCSS').addClass('HideValidMsg');
        $(element).closest('.form-group').find('.ErrorMessages').find('.HideValidMsg').removeClass('CustomRequiredCSS');
        // End
        $(element).removeClass("fieldreq");
    }
}
// Common Required Fields Validation

function ValidateMultipleFields() {

    var StatusRes = true;
    $(".CustomRequiredFields").each(function () {
        //alert($(this).val());
        if ($(this).val() == "" || $(this).val() == "-1" || $(this).val() == "0") {
            $(this).addClass("fieldreq");
            $(this).next().addClass("CustomRequiredCSS");
            $(this).next().removeClass("HideValidMsg");
            StatusRes = false;
        } else {
            $(this).next().removeClass("CustomRequiredCSS");
            $(this).next().addClass("HideValidMsg");
            $(this).removeClass("fieldreq");
        }
    })
    if (StatusRes) {
        if (confirm("Are your sure. Do you want to submit?")) {
            return true;
        } else {
            return false;
        }
    } else {
        return false;
    }
}
function yearValidation(year, ev) {

    var text = /^[0-9]+$/;
    if (ev.type == "blur" || year.length == 4 && ev.keyCode != 8 && ev.keyCode != 46) {
        if (year != 0) {
            if ((year != "") && (!text.test(year))) {

                alert("Please Enter Numeric Values Only");
                return false;
            }

            if (year.length != 4) {
                alert("Year is not proper. Please check");
                return false;
            }
            var current_year = new Date().getFullYear();
            if ((year < 1920) || (year > current_year)) {
                alert("Year should be in range 1920 to current year");
                return false;
            }
            return true;
        }
    }
}
// for displaying page name start
// for getting Unique Path using Area, Controller and View.
function GetPageName(pagename, controllername, areaname) {
    var getUniqPath = areaname + "/" + controllername + "/" + pagename;
    $("a.m-menu__link").each(function () {
        if ($(this).attr("href").indexOf(getUniqPath) != "-1") {
            $(".commpageclass").text($(this).find(".m-menu__link-text").text());
        }
    })
}

function GetPageNameNew(pagename, controllername) {
    if (pagename.toLowerCase() != "index") {
        $("a.m-menu__link").each(function () {
            if ($(this).attr("href").indexOf(pagename) != "-1") {
                $(".commpageclass").text($(this).find(".m-menu__link-text").text());
            }
        })
    } else if (pagename.toLowerCase() == "index") {
        $("a.m-menu__link").each(function () {
            if ($(this).attr("href").indexOf(controllername) != "-1") {
                $(".commpageclass").text($(this).find(".m-menu__link-text").text());
            }
        })
    }
}
// for displaying page name end

// for Upload and View File Document start
function UploadFileDocument(UploadFileUrl) {
    if (UploadFileUrl.indexOf("False") != -1) {
        window.open(UploadFileUrl, '_blank'); // for new tab
        // window.location.href = UploadFileUrl; //for same window        
        return false;
    }
    ShowLoadder();
    $.ajax({
        type: "GET",
        url: UploadFileUrl.trim(),
        async: false,
        contentType: "html",
        success: function (data) {
            //alert(data);
            SetModalTitle("Upload Document");
            SetModalBody(data);
            HideLoadder();
            ShowModal();
        },
        error: function (data) {
            HideLoadder();
        }
    });
}
function displayFile(fileName) {

    window.open(fileName);
}
function ViewFileDocument(ViewDocumentUrl) {
    if (ViewDocumentUrl.indexOf("False") != -1) {
        window.open(ViewDocumentUrl, '_blank');// for new tab
        //window.location.href = ViewDocumentUrl; // for same window       
        return false;
    }
    ShowLoadder();
    $.ajax({
        type: "GET",
        url: ViewDocumentUrl.trim(),
        async: false,
        contentType: "html",
        success: function (data) {
            //alert(data);
            SetModalTitle("View Document");
            SetModalBody(data);
            HideLoadder();
            ShowModal();
        },
        error: function (data) {
            HideLoadder();
        }
    });
}
// for Upload and View File Document end


// Common and Consistent Validation Start
function ValidAndSubmit(formName) {
    var Status = false;
    $(".requiredValidation").each(function () {
        //alert($(this).val());
        if (!$(this).prop('disabled') && ($(this).val() == "" || $(this).val() == "-1" || $(this).text().toLowerCase().trim() == "select")) {
            $(this).next(".commonerror").remove();
            if ($(this).siblings(".control-label").text() != "") {
                $(this).after("<label class= 'commonerror' >" + $(this).siblings(".control-label").text().replace("*", "") + " is required.</label>");
            } else {
                $(this).after("<label class= 'commonerror' > Required Field. </label>");
            }
            Status = true;
        } else {
            $(this).next(".commonerror").remove();
        }
    })

    // for Registration Start
    //if (formName == "#formCreateNewAccount" || formName == "#formCreateResearcherAccount") {
    //    $("#hdnOTPVerified").val("true");
    //    if ($("#hdnOTPVerified").val() == "true") {
    //        $("#IdVerifyMbNo").hide();
    //    } else {
    //        $("#mobileNumber").focus();
    //        $("#IdVerifyMbNo").show();
    //        Status = true;
    //    }
    //}
    // for Registration End 

    if (!Status) {
        if (confirm("Are your sure. Do you want to submit?")) {
            if (formName == "#frmHostelFacilityDetails") {
                SaveHostelData(eve);
            } else if (formName == "#frmFurnitureAreaDetails") {
                Furniture.FurnitureAreaSaveData();
            } else if (formName == "#frmStaffStrengthDetail") {
                StaffQualificationSaveData();
            } else {
                $(formName).submit();
            }
            //return true;
        } else {
            return false;
        }
    } else {
        return false;
    }
    //return Status;
}

// Common and Consistent Validation End


$(document).ready(function () {
    //common menu opening code
    $(".commonprofile").hover(function () {
        $(this).toggleClass("m-dropdown--open");
    });
    //common menu opening code

    // datetime validation start
    // past date start
    // note: for custom date put date in 'dd/mm/yyyy' format in endDate
    $(".commonpastdate").datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        changeMonth: true,
        changeYear: true,
        endDate: 'today'
    });

    // past date end

    // DateOfBirth Start
    $(".commondateofbirth").datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        changeMonth: true,
        changeYear: true,
        endDate: '-18y'
    });
    // DateOfBirth End
    $(".commonpastdate, .commondateofbirth").removeAttr("data-val data-val-date");
    // datetime validation end

    $(".requiredValidation").focusout(function () {
        var element = $(this);
        if ($(element).val() == "" || $(element).val() == "-1" || $(element).text().toLowerCase().trim() == "select") {
            $(element).siblings(".commonerror").remove();
            if ($(element).siblings(".control-label").text() != "") {
                $(element).after("<label class= 'commonerror' >" + $(element).siblings(".control-label").text().replace("*", "") + " is required.</label>");
            } else {
                $(element).after("<label class= 'commonerror' > Required Field. </label>");
            }
        } else {
            $(element).siblings(".commonerror").remove();

            if ($(element).hasClass("password")) {
                if (element != null && element != undefined) {
                    if (!validatePassword($(element).val())) {
                        $(element).next(".commonerror").remove();
                        $(element).after("<label class= 'commonerror' > Password should have minimum 1 Capital Alphabet, 1 Number,1 Special Character and without space, e.g. Password@123. </label>");
                        $(element).val('');
                    }
                }
            } else if ($(element).hasClass("EmailID")) {
                if (element != null && element != undefined) {
                    if (!validateEmail($(element).val())) {
                        $(element).next(".commonerror").remove();
                        $(element).after("<label class= 'commonerror' > Please Enter Valid Email ID. </label>");
                        $(element).val('');
                    }
                }
            } else if ($(element).hasClass("confirmpassword")) {
                if (element != null && element != undefined) {
                    if ($(".password").val() != "" && $(".confirmpassword").val() != ""
                        && $(".password").val() != $(".confirmpassword").val() != "") {
                        $(".confirmpassword").next(".commonerror").remove();
                        $(".confirmpassword").after("<label class= 'commonerror' > Password and Confirm Password Should be Same. </label>");
                        $(".confirmpassword").val('');
                    }
                }
            } else if ($(element).hasClass("MobileNumber")) {
                if (element != null && element != undefined) {
                    if ($(element).val().trim().length != 10) {
                        $(element).next(".commonerror").remove();
                        $(element).after("<label class= 'commonerror' > Mobile Number Should Be 10 Digits. </label>");
                        $(element).val('');
                    }
                }
            }
        }
    });
});

