
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
                $('#RadioList' + data[0].PRcat).prop("checked", true);
                $('.RadioList').attr("disabled", true);
                $('#tableSelected').val(data[0].PRcat);
                //$('#table' + data[0].PRcat + ' tbody tr:first').remove();

                switch (data[0].PRcat) {
                    case 'RM':
                        $('#tableRM').show();
                        $('#tableRM tbody').empty();

                        $.each(data, function (i, item) {
                            $('#tableRM > tbody:last-child').append('<tr><td><span>' + item.SN + '</span></td><td><span>' + item.RMdescription + '</span></td><td><span>' + item.RMgrade + '<span></td><td><span>' + item.RMHardness + '<span></td><td><span>' + item.PSLlevel + '<span></td><td><span>' + item.OD + '<span></td><td><span>' + item.WT + '</span></td><td><span>' + item.Len +'</span></td><td><span>' + item.QtyReqd + '</span></td><td><span>' + item.QtyStock + '</span></td><td><span>' + item.PRqty + '</span></td><td><span>' + item.UnitPrice + '</span></td><td><span class="PORMTPrice">' + item.TotalPrice + '</span></td><td><input name="PORMDiscount" type="text" class="form-control PORMDiscount col-md-6" value="" onkeyup="CalcTotalWithDiscount(this)" /></td><td><input name="TotalPrice" type="text" readonly="readonly" class="form-control PORMFinalPrice" value="' + item.TotalPrice + '" /></td><td><a href="#" class="removePO">Remove</a></td></tr>');

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

        $.each($(tableSelected + " tbody tr"), function () {
            arr.push({
                //Id: $("#Id").val(),
                PRSetno: $('#PRSetNoPODetails').val(),
                PONo: $("#PONoPODetails").val(),
                POSetno: $('#HiddenPOSetno').val(),
                POdate: $("#PODatePODetails").val(),
                PORevNo: $("#PODetailsPORevNo").val(),
                ItemCategory: $("#PODetailsItemCategory").val(),
                GeneralCondition: $("#PODetailsGeneralCondition").val(),
                POQMSRequirement: $("#PODetailsPOQMSRequirement").val(),
                POQuality: $("#PODetailsPOQuality").val(),
                POPackForward: $("#PODetailsPOPackForward").val(),
                PODetailsPORevNo: $("#PODetailsPORevNo").val(),
                ModeOfPayment: $("#PODetailsModeOfPayment").val(),
                PaymentTerms: $("#PODetailsPaymentTerms").val(),
                ModeOfTransport: $("#PODetailsModeOfTransport").val(),
                AnyOtherRequirements: $("#PODetailsAnyOtherRequirements").val(),
                
                SN: $(this).find('td:eq(0) span').text(),
                RMdescription: $(this).find('td:eq(1) span').text(),
                RMgrade: $(this).find('td:eq(2) span').text(),
                RMHardness: $(this).find('td:eq(3) span').text(),
                PSLlevel: $(this).find('td:eq(4) span').text(),
                QtyReqd: $(this).find('td:eq(8) span').text(),
                QtyStock: $(this).find('td:eq(9) span').text(),
                PRqty: $(this).find('td:eq(10) span').text(),
                UnitPrice: $(this).find('td:eq(11) span').text(),
                TotalPrice: $(this).find('td:eq(12) span').text(),
                Discount: $(this).find('td:eq(13) input').val(),
                FinalPrice: $(this).find('td:eq(14) input').val(),

                WorkNo: $('#PODetailsWorkNo').val(),
                DeliveryDate: $('#PODetailsDeliveryDate').val(),
                POValidity: $('#PODetailsPOValidity').val(),
                TotalPRSetPrice: TotalPRSetPrice

            });
        });

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