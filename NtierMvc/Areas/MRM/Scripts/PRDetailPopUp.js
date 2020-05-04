
$(document).ready(function () {



})

function ApproveReject(value) {

    //$('#ApproveRejectPRDetails').attr('options:selected', value);
    $('#ApproveRejectPRDetails option:selected').text(value);
}

function RMCatChange() {

    var RMCat = $('#RMCatPRDetails option:selected').text();

    $('#tableRM tr:last').each(function(){
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
        url: window.SavePRDetailsList,
        data: data,
        success: function (result) {
            alert(result);
        },
        error: function () {
            alert(result)
        }
    });
}


function CalcTotal(ob) {
    let trob = $(ob).closest('tr');
    let qty = parseFloat($.trim(trob.find(".RMPRqty").val()));
    if (!qty || isNaN(qty)) {
        qty = 0;
    }
    let unitprice = parseFloat($.trim(trob.find(".RMUnitPrice").val()));
    if (!unitprice || isNaN(unitprice)) {
        unitprice = 0;
    }
    trob.find(".RMTotalPrice").val(Math.round(qty * unitprice));
}

function showRequestedSign(id) {

    if ($('#' + id).is(':checked')) {
        $('#img' + id).show();
    }
    else
        $('#img' + id).hide();

    //if (type == 'RequestedBy') {
    //    if ($("#RequestedBy input[type=checkbox]").prop(":checked"))
    //        $('#imgRequestedSign').show();
    //    else
    //        $('#imgRequestedSign').hide();
    //}

    //if (type == 'StoreEx') {
    //    if ($("#StoreEx input[type=checkbox]").prop(":checked"))
    //        $('#imgStoreExSign').show();
    //    else
    //        $('#imgStoreExSign').hide();
    //}

    //if (type == 'ApproverSign') {
    //    if ($("#ApproverSign input[type=checkbox]").prop(":checked"))
    //        $('#imgApproverSign').show();
    //    else
    //        $('#imgApproverSign').hide();
    //}

}

function changeEndUseNo() {
    var EndUse = $('#EndUsePRDetails option:selected').text();

    if (EndUse == 'SONo' || EndUse == 'QuoteNo') {

        $('#QuoteTypeDiv').show();
        $('#QuoteTypePRDetails').val('');

    }
    else if (EndUse == 'Non-PO') {
        $('#QuoteTypeDiv').hide();

        $.ajax({
            type: 'POST',
            url: window.ChangeEndUseNoForNonPO,
            data: JSON.stringify({ EndUse: EndUse }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $('#EndUseNoPRDetails').empty();
                $.each(data, function (i, item) {
                    $("#EndUseNoPRDetails").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
                })

            },
            error: function (x, e) {
                alert('Some error is occurred, Please try after some time.');
            }
        })
    }
}

function changeQuoteType() {

    var EndUse = $('#EndUsePRDetails option:selected').text();
    var QuoteType = $('#QuoteTypePRDetails').val();

    //if (EndUse == "SONo")
    //    EndUse = 'SoNo';
    //else if (EndUse == "QuoteNo")
    //    EndUse = 'QuoteNo';

    $.ajax({
        type: 'POST',
        url: window.ChangeEndUseNoForQuoteOrSoNo,
        data: JSON.stringify({ EndUse: EndUse, quoteType: QuoteType }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#EndUseNoPRDetails').empty();
            $.each(data, function (i, item) {
                $("#EndUseNoPRDetails").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
            })
        },
        error: function (x, e) {
            alert('Some error is occurred, Please try after some time.');
        }
    })
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
            $('#RMCatPRDetails').empty();
            $.each(data, function (i, item) {
                $("#RMCatPRDetails").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
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

function SavePRDetails(e) {
    e.preventDefault();

    var arr = [];
    arr.length = 0;

    var frm = $("#formPRDetails");
    var formData = new FormData(frm[0]);

    var Status = false;
    Status = GetFormValidationStatus("#formPRDetails");

    let tableSelected = '#table' + $('#tableSelected').val();
    let TotalPRSetPrice = 0;

    $.each($(tableSelected + " tbody tr"), function () {

        TotalPRSetPrice = TotalPRSetPrice + Math.round($(this).find('td:eq(12) input').val());
    });


    if (!Status) {
        alert("Kindly Fill all mandatory fields");
        return;
    }
    else {
        $.each($(tableSelected + " tbody tr"), function () {
            arr.push({
                //Id: $("#Id").val(),
                PRSetNo: $("#HiddenPRSetno").val(),
                PRNO: $("#PRNoPRDetails").val(),
                ReqFrom: $("#RequestFromPRDetails").val(),
                ReqTo: $("#RequestToPRDetails").val(),
                DeptName: $("#HiddenDeptNamePRDetails").val(),
                PRCat: $("#tableSelected").val(),
                Currency: $("#CurrencyPRDetails").val(),
                Priority: $("#PriorityPRDetails").val(),
                EndUse: $("#EndUsePRDetails").val(),
                EndUseNo: $("#EndUseNoPRDetails").val(),
                CostCentre: $("#CostCentrePRDetails").val(),
                RMcat: $("#RMCatPRDetails").val(),
                UOM: $("#UOMPRDetails").val(),

                SN: $(this).find('td:eq(0) label').text(),
                RMdescription: $(this).find('td:eq(1) input').val(),
                RMgrade: $(this).find('td:eq(2) input').val(),
                RMHardness: $(this).find('td:eq(3) input').val(),
                PSLlevel: $(this).find('td:eq(4) select').val(),
                OD: $(this).find('td:eq(5) input').val(),
                WT: $(this).find('td:eq(6) input').val(),
                LEN: $(this).find('td:eq(7) input').val(),
                QtyReqd: $(this).find('td:eq(8) input').val(),
                QtyStock: $(this).find('td:eq(9) input').val(),
                PRqty: $(this).find('td:eq(10) input').val(),
                UnitPrice: $(this).find('td:eq(11) input').val(),
                TotalPrice: $(this).find('td:eq(12) input').val(),

                //SN: $('.RMSN').val(),
                //RMdescription: $('.RMdescription').val(),
                //RMgrade: $('.RMgrade').val(),
                //RMHardness: $('.RMHardness').val(),
                //PSLlevel: $('.RMPSLlevel').val(),
                //OD: $('.RMOD').val(),
                //WT: $('.RMWT').val(),
                //LEN: $('.RMLen').val(),
                //QtyReqd: $('.RMQtyReqd').val(),
                //QtyStock: $('.RMQtyStock').val(),
                //PRqty: $('.RMPRqty').val(),
                //UnitPrice: $('.RMUnitPrice').val(),
                //TotalPrice: $('.RMTotalPrice').val(),

                DeliveryDate: $('#DeliveryDatePRDetails').val(),
                SupplyTerms: $('#SupplyTermsPRDetails').val(),
                DeliveryTerms: $('#PRDetailsDeliveryTerms').val(),
                PaymentTerms: $('#PRDetailsPaymentTerms').val(),
                Certificates: $('#PRDetailsCertificates').val(),
                ApprovedSupplier1: $('#PRApprovedSupplier1').val(),
                ApprovedSupplier2: $('#PRApprovedSupplier2').val(),
                ApprovedReject: $('#AcceptRejectPRDetails').val(),
                Communicate: $('#CommunicatePRDetails').val(),
                POno: $('#PONoPRDetails').val(),
                ExpectedDeliveryDate: $('#ExpectedDeliveryDatePRDetails').val(),
                Status: $('#HiddenStatusPRDetails').val(),
                EntryPerson: $('#HiddenEntryPersonPRDetails').val(),
                TotalPRSetPrice: TotalPRSetPrice


            });
        });

        var data = JSON.stringify({
            PRDetails: arr
        });

        $.when(saveButton(data)).then(function (response) {
            console.log(response);
        }).fail(function (err) {
            console.log(err);
        });
    }
};