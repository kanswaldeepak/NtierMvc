
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

