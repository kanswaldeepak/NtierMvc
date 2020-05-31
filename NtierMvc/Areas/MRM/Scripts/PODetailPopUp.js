
function FetchtPRDetailsFromPRSetNo() {
    var PRSetno = $('#PRNoPODetails').val();

    $.ajax({
        url: window.GetPRDetailsFromPRSetNo,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ PRSetno: PRSetno }),
        success: function (data) {

            if (data.length > 0) {
                $("#HiddenPRSetno").val(data[0].PRSetno);
                $('#RadioList' + data[0].PRcat).prop("checked", true);
                $('.RadioList').attr("disabled", true);
                $('#tableSelected').val(data[0].PRcat);
                $('#table' + data[0].PRcat + ' tbody tr:first').remove();

                switch (data[0].PRcat) {
                    case 'RM':
                        $('#tableRM').show();
                        $.each(data, function (i, item) {
                            $('#tableRM > tbody:last-child').append('<tr><td><span>' + item.SN + '</span></td><td><span>' + item.RMdescription + '</span></td><td><span>' + item.QtyReqd + '</span></td><td><span>' + item.QtyStock + '</span></td><td><span>' + item.PRqty + '</span></td><td><span>' + item.UnitPrice + '</span></td><td><span>' + item.TotalPrice + '</span></td><td><a href="#" class="removePO">Remove</a></td></tr>');

                            $('#imgRequestedBy').attr("src", "/Images/Sign/" + item.EntryPersonSign);
                            $('#imgStoreEx').attr("src", "/Images/Sign/" + item.ApprovePerson1Sign);
                            $('#imgApproverSign').attr("src", "/Images/Sign/" + item.ApprovePerson2Sign);

                        })


                        $('.removePO').on("click", function (e) {
                            e.preventDefault();
                            $(this).parent().parent().remove();
                        })

                        break;
                    case 'BOI':
                        $('.tableBOI').show();
                        break;
                    case 'JW':
                        $('.tableJW').show();
                        break;
                    case 'GI':
                        $('.tableGI').show();
                        break;
                    case 'C':
                        $('.tableC').show();
                        break;
                    case 'O':
                        $('.tableO').show();
                        break;
                    default:
                        break;
                }


            }
        },
        error: function () {
            alert(res);
        }
    })
}

function funcSavePurchaseDetails() {

    var PRSetNo = $('#HiddenPRSetno').val();
    var Communicate = $('#CommunicatePODetails').val();
    var PONo = $('#PONoPODetails').val();
    var PRRequestedOn = $('#PODetailsPRRequestedOn').val();
    var ExpectedDeliveryDate = $('#ExpectedDeliveryDatePODetails').val();

    $.ajax({
        url: window.SavePurchaseDetails,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ PRSetNo: PRSetNo, Communicate: Communicate, PONo: PONo, PRRequestedOn: PRRequestedOn, ExpectedDeliveryDate: ExpectedDeliveryDate }),
        success: function (res) {
            alert(res);
        },
        error: function () {
            alert(res);
        }
    })
}

function RMCatChange() {

    var RMCat = $('#RMCatPODetails option:selected').text();

    $('#tableRM tr:last').each(function () {
        $('td', this).each(function (index, val) {
            if (RMCat == 'Bar') {
                $(this).find(".RMdescription").val('Seamless Solid ' + RMCat);
            }
            else if (RMCat == 'Pipe' || RMCat == 'Tube') {
                $(this).find(".RMdescription").val('Seamless Casing ' + RMCat);
            }
            else
                $(this).find(".RMdescription").val('');
        });
    });

}

//After Click Save Button Pass All Data View To Controller For Save Database
function saveButton(data) {
    return $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: window.SavePODetailsList,
        data: data,
        success: function (result) {
            alert(result);
        },
        error: function () {
            alert(result)
        }
    });
}

function showRequestedSign(id, level) {

    if (id == 'ApproverSign') {
        $('#btnApprove').show();
        $('#btnReject').show();

        //$('#HiddenStatusPODetails').val(level);
    }

    if (id == 'StoreEx') {
        var PRSetNo = $('#HiddenPRSetno').val();
        var PRStatus = $('#HiddenPRStatusPODetails').val();
        var SignStatus = 'Approved' + level;

        $.ajax({
            url: window.UpdateApproveReject,
            type: 'POST',
            data: JSON.stringify({ PRSetNo: PRSetNo, SignStatus: SignStatus, PRStatus: PRStatus }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                alert(data);
            },
            error: function (x, e) {
                alert('Some error is occurred, Please try after some time.');
            }

        })
    }

    if ($('#' + id).is(':checked')) {
        $('#img' + id).show();
    }
    else {
        $('#img' + id).hide();

        $('#btnApprove').hide();
        $('#btnReject').hide();
    }

}

function RadioListChange() {

    $('table tr').each(function () {
        $('td', this).each(function (index, val) {
            $(this).find(".form-control").val('');
        });
    });

    $('.tableRadio').hide();
    var radioVal = $("input[type=radio][name=multiRL]:checked").val();
    $('#table' + radioVal).show();
    $('#tableSelected').val(radioVal);

    $.ajax({
        type: 'POST',
        url: window.ChangeRMCat,
        data: JSON.stringify({ CatType: radioVal }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#RMCatPODetails').empty();
            $.each(data, function (i, item) {
                $("#RMCatPODetails").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
            })

        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })

    //else if (radioVal == 'JW') {
    //    $('#tableJW').show();
    //}
    //else if (radioVal == 'GI') {
    //    $('#tableGI').show();
    //}
    //else if (radioVal == 'C') {
    //    $('#tableC').show();
    //}
    //else if (radioVal == 'O') {
    //    $('#tableO').show();
    //}
    //var label = $(this).closest("td").find("label").eq(0);
    //alert("SelectedText: " + label.html()
    //    + "\nSelectedValue: " + $(this).val());
}


function addNewRM(e) {
    e.preventDefault();
    var $tableBody = $("#tableRM");
    var $trLast = $tableBody.find("tr:last");

    if ($trLast.find("#RMdescription").val() == '') {
        alert('Please fill the required fields.');
        return;
    }
    var $trNew = $trLast.clone();
    var size = $('#tableRM >tbody >tr').length + 1;

    $trNew.find('.RMSN').html(size);
    var suffix = $trNew.find(':input:first').attr('name').match(/\d+/);

    //$trNew.find("#SN").attr('class', 'requiredValidation form-control');
    //$trNew.find("#RMcode").attr('class', 'form-control requiredValidation');
    $trNew.find("#RMdescription").attr('class', 'requiredValidation form-control RMdescription');
    $trNew.find("#RMgrade").attr('class', 'form-control');
    $trNew.find("#RMPSLlevel").attr('class', 'form-control');
    $trNew.find("#RMOD").attr('class', 'form-control');
    $trNew.find("#RMWT").attr('class', 'form-control');
    $trNew.find("#RMLen").attr('class', 'form-control');
    $trNew.find("#RMQtyReqd").attr('class', 'form-control');
    $trNew.find("#RMQtyStock").attr('class', 'form-control');
    $trNew.find("#RMPRqty").attr('class', 'form-control RMPRqty');
    $trNew.find("#RMPRqty").attr('onkeyup', 'CalcTotal(this)');
    $trNew.find("#RMUnitPrice").attr('class', 'form-control RMUnitPrice');
    $trNew.find("#RMUnitPrice").attr('onkeyup', 'CalcTotal(this)');
    $trNew.find("#RMTotalPrice").attr('class', 'form-control RMTotalPrice');
    $trNew.find("#RMHardness").attr('class', 'form-control');

    $trNew.find("td:last").html('<a href="#" class="remove">Remove</a>');
    $.each($trNew.find(':input'), function (i, val) {
        // Replaced Name
        var oldN = $(this).attr('name');
        var newN = oldN.replace('[' + suffix + ']', '[' + (parseInt(suffix) + 1) + ']');
        $(this).attr('name', newN);
        //Replaced value

        $(this).val('');

        //var type = $(this).attr('type');
        //debugger;
        //if (type.toLowerCase() == "text") {
        //    $(this).attr('value', '');
        //}
        //if (type.toLowerCase() == "select") {
        //    $(this).attr('value', '');
        //}

        // If you have another Type then replace with default value
        $(this).removeClass("input-validation-error");
        //$(this).addClass("requiredValidation");

    });
    $trLast.after($trNew);


    // Re-assign Validation
    //var form = $("form")
    //    .removeData("validator")
    //    .removeData("unobtrusiveValidation");
    //$.validator.unobtrusive.parse(form);


    // 2. Remove
    $('.remove').on("click", function (e) {
        e.preventDefault();
        $(this).parent().parent().remove();
    });

    $('.NoEndDate').datepicker({
        format: 'dd/mm/yyyy',
        autoclose: true,
        changeMonth: true,
        changeYear: true,
        endDate: '',
    });

};

function SaveDetailsForPO(e) {
    e.preventDefault();

    var arr = [];
    arr.length = 0;

    var frm = $("#formPODetails");
    var formData = new FormData(frm[0]);

    var Status = false;
    Status = GetFormValidationStatus("#formPODetails");

    let tableSelected = '#table' + $('#tableSelected').val();
    let TotalPRSetPrice = 0;

    $.each($(tableSelected + " tbody tr"), function () {

        TotalPRSetPrice = TotalPRSetPrice + Math.round($(this).find('td:eq(6) input').val());
    });


    if (!Status) {
        alert("Kindly Fill all mandatory fields");
        return;
    }
    else {

        $.each($(tableSelected + " tbody tr"), function () {
            arr.push({
                //Id: $("#Id").val(),
                PRno: $("#PRNoPODetails").val(),
                PRSetno: $('#PRNoPODetails').val(),
                PONo: $("#PONoPODetails").val(),
                POdate: $("#PODatePODetails").val(),
                
                SN: $(this).find('td:eq(0) span').text(),
                RMdescription: $(this).find('td:eq(1) span').text(),
                QtyReqd: $(this).find('td:eq(2) span').text(),
                QtyStock: $(this).find('td:eq(3) span').text(),
                PRqty: $(this).find('td:eq(4) span').text(),
                UnitPrice: $(this).find('td:eq(5) span').text(),
                TotalPrice: $(this).find('td:eq(6) span').text(),

                WorkNo: $('#PODetailsWorkNo').val(),
                DeliveryTime: $('#PODetailsDeliveryTime').val(),
                POValidity: $('#PODetailsPOValidity').val(),
                TotalPRSetPrice: TotalPRSetPrice

            });
        });

        var data = JSON.stringify({
            PODetails: arr
        });

        $.when(saveButton(data)).then(function (response) {
            console.log(response);
        }).fail(function (err) {
            console.log(err);
        });
    }
};