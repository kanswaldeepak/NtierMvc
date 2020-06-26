
function changeLotTable() {

    var SupplyTermsText = $('#GESupplyTerms option:selected').text();
    if (SupplyTermsText == 'Single') {
        $('#addNew').hide();
        $('#lotDiv').hide();
        $('.lotColumns').prop("disabled", true);
        $('.forSingle').val('');
        //$(".forSingle").prop('disabled', true);    
        $('#lotTable > tbody').children('tr:not(:first)').remove();
        $('.forSingle').removeClass('requiredValidation');
    }
    else {
        $('#addNew').show();
        $('#lotDiv').show();
        $('.lotColumns').prop("disabled", false);
        //$(".forSingle").prop('disabled', false);
        $('.forSingle').addClass('requiredValidation');
    }
}


function CalcTotalWithDiscount(ob) {

    let trob = $(ob).closest('tr');
    let totPrice = parseFloat($.trim(trob.find(".PORMTPrice").text()));
    if (!totPrice || isNaN(totPrice)) {
        totPrice = 0;
    }
    let discnt = parseFloat($.trim(trob.find(".PORMDiscount").val()));
    if (!discnt || isNaN(discnt)) {
        discnt = 0;
    }

    let disctVal = ((totPrice / 100) * discnt).toFixed(2);
    trob.find(".PORMFinalPrice").val((Math.round(((totPrice - disctVal) * 1000) / 10) / 100).toFixed(2));
}

function FetchtPRDetailsFromPRSetNo() {
    var PRSetno = $('#PRSetNoPODetails').val();

    $.ajax({
        url: window.GetPRDetailsFromPRSetNo,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ PRSetno: PRSetno }),
        success: function (data) {

            if (data.length > 0) {
                $("#HiddenPRSetno").val(data[0].PRSetno);
                $('#POCatPRDetails').val(data[0].PRcat);
                $('#POCatPRDetails').attr("disabled", true);
                $('#tableSelected').val(data[0].PRcat);
                //$('#table' + data[0].PRcat + ' tbody tr:first').remove();

                switch (data[0].PRcat) {
                    case 'RM':
                        $('#tableRM').show();
                        $('#tableRM tbody').empty();

                        $.each(data, function (i, item) {
                            $('#tableRM > tbody:last-child').append('<tr sn=' + item.SN + '><td><span name="RMSN">' + item.SN + '</span></td><td><span name="RMdescription">' + item.RMdescription + '</span></td><td hidden><span name="RMgrade">' + item.RMgrade + '<span></td><td hidden><span name="RMHardness">' + item.RMHardness + '<span></td><td hidden><span name="RMPSLlevel">' + item.PSLlevel + '<span></td><td hidden><span name="RMOD">' + item.OD + '<span></td><td hidden><span name="RMWT">' + item.WT + '</span></td><td hidden><span name="RMLen">' + item.Len + '</span></td><td hidden><span name="RMQtyReqd">' + item.QtyReqd + '</span></td><td hidden><span name="RMQtyStock">' + item.QtyStock + '</span></td><td><span name="RMPRqty">' + item.PRqty + '</span></td><td><select name="RMLotName" disabled class="form-control lotColumns"><option value="">Select</option><option value="Lot1">Lot1</option><option value="Lot2">Lot2</option><option value="Lot3">Lot3</option></select></td><td><input name="RMLotQty" type="text" disabled class="form-control lotColumns" value="" /></td><td><input name="RMLotDate" type="text" disabled class="form-control lotColumns NoEndDate" value="" /></td><td><span name="RMUnitPrice">' + item.UnitPrice + '</span></td><td><span name="RMPrice" class="PORMTPrice">' + item.TotalPrice + '</span></td><td><input name="PORMDiscount" type="text" class="form-control PORMDiscount col-md-6" value="" onkeyup="CalcTotalWithDiscount(this)" /></td><td><input name="RMTotalPrice" type="text" readonly="readonly" class="form-control PORMFinalPrice" value="' + item.TotalPrice + '" /></td><td hidden><span name="RMDesc1">' + item.Desc1 + '</span></td><td><a href="#" class="addLotPO">Add Lot Details</a></td><td><a href="#" class="removeRMPO">Remove</a></td></tr>');

                        })

                        $('#imgRequestedBy').attr("src", "/Images/Sign/" + data[0].EntryPersonSign);
                        $('#imgStoreEx').attr("src", "/Images/Sign/" + data[0].ApprovePerson1Sign);
                        $('#imgApproverSign').attr("src", "/Images/Sign/" + data[0].ApprovePerson2Sign);

                        $('.NoEndDate').datepicker({

                            format: 'dd-mm-yyyy',
                            autoclose: true,
                            changeMonth: true,
                            changeYear: true,
                            endDate: ''
                        });

                        $('.removeRMPO').on("click", function (e) {
                            e.preventDefault();
                            $(this).parent().parent().remove();
                        });

                        $('.addLotPO').on("click", function (e) {
                            e.preventDefault();
                            //var $tableBody = $("#tableRM");
                            //var $trLast = $tableBody.find("tr:last");
                            var $trLast = $(this).parent().parent();
                            var $trNew = $trLast.clone();

                            //$trNew.find("td:nth-last-child(2)").html('');

                            $trNew.find("td:nth-last-child(2)").html('');
                            $trNew.find("td:nth-last-child(3)").html('');
                            $trNew.find("td:nth-last-child(4)").html('');
                            $trNew.find("td:nth-last-child(5)").html('');
                            $trNew.find("td:nth-last-child(6)").html('');
                            $trNew.find("td:nth-last-child(7)").html('');

                            var suffix = $trNew.find(':input:first').attr('name').match(/\d+/);

                            //$trNew.find("td:last").html('<a href="#" class="remove">Remove</a>');
                            $.each($trNew.find(':input'), function (i, val) {
                                // Replaced Name
                                var oldN = $(this).attr('name');
                                var newN = oldN.replace('[' + suffix + ']', '[' + (parseInt(suffix) + 1) + ']');
                                $(this).attr('name', newN);
                                //Replaced value
                                var type = $(this).attr('type');
                                if (type != undefined && type.toLowerCase() == "text") {
                                    $(this).attr('value', '');
                                }

                                // If you have another Type then replace with default value
                                $(this).removeClass("input-validation-error");
                                //$(this).addClass("requiredValidation");

                            });
                            $trLast.after($trNew);

                            // 2. Remove
                            $('.removeRMPO').on("click", function (e) {
                                e.preventDefault();
                                $(this).parent().parent().remove();
                            });

                            $('.NoEndDate').datepicker({
                                format: 'dd-mm-yyyy',
                                autoclose: true,
                                changeMonth: true,
                                changeYear: true,
                                endDate: '',
                            });

                        });

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

//After Click Save Button Pass All Data View To Controller For Save Database
function saveButtonPODetails(data) {
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

        TotalPRSetPrice = TotalPRSetPrice + Math.round($(this).find('td:eq(14) input').val());
    });


    if (!Status) {
        alert("Kindly Fill all mandatory fields");
        return;
    }
    else {
        var lastValue = $(tableSelected + ' tr:last-child td:first-child span').text();

        var SN = "";
        var RMdescription = "";
        var RMgrade = "";
        var RMHardness = "";
        var PSLlevel = "";
        var QtyReqd = "";
        var QtyStock = "";
        var PRqty = "";
        var UnitPrice = "";
        var TotalPrice = "";
        var Discount = "";
        var FinalPrice = "";

        for (var i = 0; i <= lastValue; i++) {

            let flg = 1;

            $.each($(tableSelected + " tbody tr[sn='" + i + "']"), function () {

                if (flg == 1) {
                    SN = $(this).find('td:eq(0) span').text();
                    RMdescription = $(this).find('td:eq(18) span').text();
                    RMgrade = $(this).find('td:eq(2) span').text();
                    RMHardness = $(this).find('td:eq(3) span').text();
                    PSLlevel = $(this).find('td:eq(4) span').text();
                    QtyReqd = $(this).find('td:eq(8) span').text();
                    QtyStock = $(this).find('td:eq(9) span').text();
                    PRqty = $(this).find('td:eq(10) span').text();
                    UnitPrice = $(this).find('td:eq(14) span').text();
                    TotalPrice = $(this).find('td:eq(15) span').text();
                    Discount = $(this).find('td:eq(16) input').val();
                    FinalPrice = $(this).find('td:eq(17) input').val();
                }

                arr.push({
                    //Id: $("#Id").val(),
                    PRSetno: $('#PRSetNoPODetails').val(),
                    PONo: $("#PONoPODetails").val(),
                    POSetno: $('#HiddenPOSetno').val(),
                    POdate: $("#PODatePODetails").val(),
                    PORevNo: $("#PORevNoPODetails").val(),
                    ItemCategory: $("#PODetailsItemCategory").val(),
                    GeneralCondition: $("#PODetailsGeneralCondition").val(),
                    POQMSRequirement: $("#PODetailsPOQMSRequirement").val(),
                    POQuality: $("#PODetailsPOQuality").val(),
                    POPackForward: $("#PODetailsPOPackForward").val(),
                    ModeOfPayment: $("#PODetailsModeOfPayment").val(),
                    PaymentTerms: $("#PODetailsPaymentTerms").val(),
                    ModeOfTransport: $("#PODetailsModeOfTransport").val(),
                    AnyOtherRequirements: $("#PODetailsAnyOtherRequirements").val(),

                    SN: SN,
                    RMdescription: RMdescription,
                    RMgrade: RMgrade,
                    RMHardness: RMHardness,
                    PSLlevel: PSLlevel,
                    QtyReqd: QtyReqd,
                    QtyStock: QtyStock,
                    PRqty: PRqty,
                    LotName: $(this).find('td:eq(11) select').val(),
                    LotQty: $(this).find('td:eq(12) input').val(),
                    LotDate: $(this).find('td:eq(13) input').val(),
                    UnitPrice: UnitPrice,
                    TotalPrice: TotalPrice,
                    Discount: Discount,
                    FinalPrice: FinalPrice,

                    WorkNo: $('#PODetailsWorkNo').val(),
                    DeliveryDate: $('#PODetailsDeliveryDate').val(),
                    POValidity: $('#PODetailsPOValidity').val(),
                    PORevNo: $('#PODetailsPORevNo').val(),
                    CostCentre: $('#PODetailsCostCentre').val(),
                    TotalPRSetPrice: TotalPRSetPrice

                });

                flg++;
            });


        }

        var data = JSON.stringify({
            PODetails: arr
        });

        $.when(saveButtonPODetails(data)).then(function (response) {
            console.log(response);
        }).fail(function (err) {
            console.log(err);
        });
    }
};