
function GetDetailForGateEntry() {
    var POSetno = $('#GEVendorPONO').val();

    $.ajax({
        url: window.GetDetailsForGateEntry,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ POSetno: POSetno }),
        success: function (data) {

            if (data.length > 0) {
                $("#HiddenPOSetno").val(data[0].POSetno);
                $('#RadioList' + data[0].PRCat).prop("checked", true);
                $('.RadioList').attr("disabled", true);
                $('#tableSelected').val(data[0].PRCat);

                switch (data[0].PRCat) {
                    case 'RM':
                        $('#tableRM').show();
                        $('#tableRM tbody').empty();

                        $.each(data, function (i, item) {
                            $('#tableRM > tbody:last-child').append('<tr><td><span>' + item.SN + '</span></td><td><span>' + item.RMdescription + '</span></td><td><span>' + item.RMgrade + '<span></td><td><span>' + item.RMHardness + '<span></td><td><span>' + item.PSLlevel + '<span></td><td><span>' + item.OD + '<span></td><td><span>' + item.WT + '</span></td><td><span>' + item.Len + '</span></td><td><span>' + item.QtyReqd + '</span></td><td><span>' + item.QtyStock + '</span></td><td><span>' + item.PRqty + '</span></td><td><span>' + item.UnitPrice + '</span></td><td><span>' + item.TotalPrice + '</span></td><td><span>' + item.Discount + '</span></td><td><span>' + item.FinalPrice + '</span></td></tr>');

                        })

                        $('#GEPODate').val(data[0].PODate);
                        $('#GEPODate').val(data[0].POdate);
                        $('#GEPOValidity').val(data[0].POValidity);
                        $('#GEWorkNo').val(data[0].WorkNo);
                        $('#GEGeneralCondition').val(data[0].GeneralCondition);
                        $('#GEDeliveryDate').val(data[0].DeliveryDate);
                        $('#GEQMSRequirement').val(data[0].QMSRequirement);
                        $('#GEQuality').val(data[0].Quality);
                        $('#GEPOValidity').val(data[0].POValidity);
                        $('#GEPackForward').val(data[0].PackForward);
                        $('#GEPORevNo').val(data[0].PORevNo);
                        $('#GEModeOfPayment').val(data[0].ModeOfPayment);
                        $('#GEItemCategory').val(data[0].ItemCategory);
                        $('#GEPaymentTerms').val(data[0].PaymentTerms);
                        $('#GEModeOfTransport').val(data[0].ModeOfTransport);
                        $('#GEAnyOtherRequirements').val(data[0].AnyOtherRequirements);

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
        error: function (res) {
            alert(res);
        }
    })
}