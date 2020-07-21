
function changeLotTable() {

    var SupplyTermsText = $('#SupplyTermsPODetails option:selected').text();
    if (SupplyTermsText == 'Single') {
        $('.lotColumns').prop("disabled", true);
        $('.lotColumns').val('');
        $('#PODetailsDeliveryDate').removeAttr('disabled');
        $('#PODetailsDeliveryDate').addClass('requiredValidation');
        $('#lotTable > tbody').children('tr:not(:first)').remove();

        let PRCat = $('#POCatPRDetails').val();
        switch (PRCat) {
            case 'RM':
                var seen = {};
                $('#tableRM tbody tr').each(function () {
                    var txt = $(this).find('td:eq(0) span').text();
                    if (seen[txt])
                        $(this).remove();
                    else
                        seen[txt] = true;
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

        //$('#addNew').hide();
        //$('#lotDiv').hide();
        //$(".forSingle").prop('disabled', true);    
        //$('.forSingle').removeClass('requiredValidation');

    }
    else {
        $('.lotColumns').prop("disabled", false);
        $('#PODetailsDeliveryDate').val('');
        $('#PODetailsDeliveryDate').attr('disabled');
        //$('#addNew').show();
        //$('#lotDiv').show();
        //$(".forSingle").prop('disabled', false);
        //$('.forSingle').addClass('requiredValidation');
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
                            let rowHtml = '<tr sn="' + item.SN +'"><td><span class="RMSN">${item.SN}</span></td><td><span class="RMdescription">${item.RMdescription}</span></td><td hidden><span class="RMgrade">${item.RMgrade}<span></td><td hidden><span class="RMHardness">${item.RMHardness}<span></td><td hidden><span class="RMPSLlevel">${item.PSLlevel}<span></td><td hidden><span class="RMOD">${item.OD}<span></td><td hidden><span class="RMWT">${item.WT}</span></td><td hidden><span class="RMLen">${item.Len}</span></td><td hidden><span class="RMQtyReqd">${item.QtyReqd}</span></td><td hidden><span class="RMQtyStock">${item.QtyStock}</span></td><td><span class="RMPRqty">${item.PRqty}</span></td><td><select disabled class="RMLotName form-control lotColumns"><option value="">Select</option><option value="Lot1">Lot1</option><option value="Lot2">Lot2</option><option value="Lot3">Lot3</option></select></td><td><input type="text" disabled class="form-control lotColumns RMLotQty" value="${item.LotQty}" /></td><td><input type="text" disabled class="form-control lotColumns NoEndDate RMLotDate" value="${item.LotDate}" /></td><td><span class="RMUnitPrice">${item.UnitPrice}</span></td><td><span class="PORMTPrice RMPrice">${item.TotalPrice}</span></td><td><input type="text" class="PORMDiscount form-control col-md-6" value="${item.Discount}" /></td><td><input type="text" class="form-control PORMFinalPrice RMTotalPrice" value="${item.TotalPrice}" /></td><td hidden><span class="RMDesc1">${item.Desc1}</span></td><td><a href="#" class="addLotPO">Add Lot Details</a></td><td><a href="#" class="removeRMPO">Remove</a></td></tr>';
                            // create object from html string
                            let $row = $(rowHtml)
                            // set value of the select within this row instance
                            $row.find('.RMSN').text(item.SN);
                            $row.find('.RMdescription').text(item.RMdescription);
                            $row.find('.RMgrade').text(item.RMgrade);
                            $row.find('.RMHardness').text(item.RMHardness);
                            $row.find('.RMPSLlevel').text(item.PSLlevel);
                            $row.find('.RMOD').text(item.OD);
                            $row.find('.RMWT').text(item.WT);
                            $row.find('.RMLen').text(item.Len);
                            $row.find('.RMQtyReqd').text(item.QtyReqd);
                            $row.find('.RMPRqty').text(item.PRqty);
                            $row.find('select.lotColumns').val(item.LotName);
                            $row.find('.RMLotQty').val(item.LotQty);
                            $row.find('.RMLotDate').val(item.LotDate);
                            $row.find('.RMUnitPrice').text(item.UnitPrice);
                            $row.find('.PORMTPrice').text(item.TotalPrice);
                            $row.find('.PORMDiscount').val(item.Discount);
                            $row.find('.RMTotalPrice').val(item.TotalPrice);
                            $row.find('.PORMFinalPrice').val(item.TotalPrice);
                            $row.find('.RMDesc1').text(item.Desc1);
                            // append updated object to DOM
                            $('#tableRM > tbody:last-child').append($row);
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

                            //var suffix = $trNew.find(':input:first').attr('name').match(/\d+/);

                            //$trNew.find("td:last").html('<a href="#" class="remove">Remove</a>');
                            $.each($trNew.find(':input'), function (i, val) {
                                // Replaced Name
                                //var oldN = $(this).attr('name');
                                //var newN = oldN.replace('[' + suffix + ']', '[' + (parseInt(suffix) + 1) + ']');
                                //$(this).attr('name', newN);
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


    var Status = true;
    //Status = GetFormValidationStatus("#formPODetails");

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
            let lotQtyTotal = 0;
            let PRqty = 0;
            let DelvryDate = '';
            $.each($(tableSelected + " tbody tr[sn='" + i + "']"), function () {

                lotQtyTotal = parseInt(lotQtyTotal) + parseInt($(this).find('td:eq(12) input').val());
                PRqty = parseInt($(this).find('td:eq(10) span').text());

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

                if ($('#SupplyTermsPODetails option:selected').text() == "Single") {
                    DelvryDate = $('#PODetailsDeliveryDate').val();
                }
                else {
                    DelvryDate = $(this).find('td:eq(13) input').val();
                    if (lotQtyTotal != PRqty) {
                        alert("LotWise Quantity Total should be Equal to Quantity for each item");
                        return;
                    }
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
                    DeliveryDate: DelvryDate,                    
                    SupplyType: $('#PODetailsSupplyType').val(),
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



//function GetSupplierIds() {
//    var vendorTypeId = $('#POSearchVendorTypeId').val();

//    return $.ajax({
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        type: 'GET',
//        url: window.GetSuppliers,
//        data: { VendorTypeId: vendorTypeId},
//        success: function (res) {
//            if (res.length > 0) {
//                $("#POSearchSupplierId").empty();
//                $.each(res, function (i, item) {
//                    $("#POSearchSupplierId").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
//                })
//            }
//        },
//        error: function () {
//            alert(result)
//        }
//    });
//}


//function GetPORMCategories() {
//    var SupplierId = $('#POSearchSupplierId').val();

//    return $.ajax({
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        type: 'GET',
//        url: window.GetRMCategories,
//        data: { SupplierId: SupplierId },
//        success: function (res) {
//            if (res.length > 0) {
//                $("#POSearchRMCategory").empty();
//                $.each(res, function (i, item) {
//                    $("#POSearchRMCategory").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
//                })
//            }
//        },
//        error: function () {
//            alert(result)
//        }
//    });
//}

//function GetPODeliveryDates() {
//    var RMCategory = $('#POSearchRMCategory option:selected').text();

//    return $.ajax({
//        contentType: 'application/json; charset=utf-8',
//        dataType: 'json',
//        type: 'GET',
//        url: window.GetDeliveryDates,
//        data: { RMCategory: RMCategory },
//        success: function (res) {
//            if (res.length > 0) {
//                $("#POSearchDeliveryDate").empty();
//                $.each(res, function (i, item) {
//                    $("#POSearchDeliveryDate").append($('<option></option>').val(item.DataStringValueField).html(item.DataTextField));
//                })
//            }
//        },
//        error: function () {
//            alert(result)
//        }
//    });
//}

